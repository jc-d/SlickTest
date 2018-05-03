Imports UIControls

Public Class RunReport
    Public ReportDatabasePath As String
    Private LastSQL As String = String.Empty
    Private LastBuildPulled As Integer = 0 ' 0 = max build number
    Private ProjectNfo As New System.Collections.Generic.List(Of ProjectNfo)()
    Public Filter As New Filters()
    Dim PreviousResult As String
    Private Shared MainAppTitle As String = "Slick Test"
    Private Report As New Report(System.Guid.Empty, "", "", False, False)
    Private sqlException As Exception

    Public Event ClosingWindow(ByVal MySize As Size, ByVal MyWindowState As System.Windows.Forms.FormWindowState)


#Region "Constructor/load"

    Public Sub New(ByVal DBLocation As String, ByVal ProjectGUID As System.Guid, ByVal IsCurrentlyExternal As Boolean, ByVal MySize As Size, ByVal MyWindowState As System.Windows.Forms.FormWindowState)
        Filter.PCName = FixSQLStr(My.Computer.Name)
        Filter.LastRunGUID = String.Empty
        Filter.TestResults = "%" 'all
        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        ' Add any initialization after the InitializeComponent() call.
        ReportDatabasePath = DBLocation
        Filter.ReportProjectGUID = ProjectGUID
        Me.ExternalReportCheckBox.Checked = IsCurrentlyExternal
        If (MySize <> Size.Empty) Then Me.Size = MySize
        Me.WindowState = MyWindowState
    End Sub

    Public Sub New()
        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        ' Add any initialization after the InitializeComponent() call.

        Filter.PCName = FixSQLStr(My.Computer.Name)
        Filter.LastRunGUID = "%" 'String.Empty
        Filter.TestResults = "%" 'all
        Filter.TestResults = "%"

        Filter.ReportProjectGUID = System.Guid.Empty
        Me.ExternalReportCheckBox.Checked = False
        Dim path As String = System.IO.Path.GetDirectoryName(Application.ExecutablePath)
        If (path.EndsWith("\") = False) Then
            path += "\"
        End If
        path &= "DB\" & "STD.sdf"

        Me.OpenFileDialog1.InitialDirectory = path
        Dim DBLocation As String = String.Empty
        System.Windows.Forms.MessageBox.Show("In a minute you will be asked to" & _
                                            " enter in the database path you wish to load from.")
        Me.OpenFileDialog1.Title = "Select the database you wish to load."
        If (Me.OpenFileDialog1.ShowDialog() = Windows.Forms.DialogResult.OK) Then
            DBLocation = Me.OpenFileDialog1.FileName
        End If

        ReportDatabasePath = DBLocation

    End Sub

    Private Sub RunReport_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        Dim StoredWindowState As System.Windows.Forms.FormWindowState = Me.WindowState
        If (Me.WindowState <> FormWindowState.Normal) Then
            Me.WindowState = FormWindowState.Normal
        End If
        Dim StoredSize As Size = Me.Size
        RaiseEvent ClosingWindow(StoredSize, StoredWindowState)
    End Sub

    Private Sub RunReport_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If (System.IO.File.Exists(ReportDatabasePath) = False OrElse ReportDatabasePath = String.Empty) Then
            If (ReportDatabasePath <> String.Empty) Then
                System.Windows.Forms.MessageBox.Show("Unable to find database at '" & _
                ReportDatabasePath & "'.  No results to view.", "Slick Test Results Viewer", _
                MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
            Me.Close()
            Return
        End If
        'GetRunData()
        If (LoadDropdownBox(False) = False) Then Return
        BuildAndShow()
    End Sub
#End Region

#Region "Sql"
    Private Function GetSQLRunName() As String
        Return SQLReporting.GetSQLRunName(ExternalReportCheckBox.Checked)
    End Function

    Private Function GetSQLRunLineName() As String
        Return SQLReporting.GetSQLRunLineName(ExternalReportCheckBox.Checked)
    End Function

    Private Function FixSQLStr(ByVal Str As String) As String
        Return Str 'Str.Replace("'", "''")
    End Function

    Private ReadOnly Property LocalConnectionString() As String
        Get
            Return "Data Source =""" & ReportDatabasePath & """;Max Database Size=4091;"
        End Get
    End Property

    Private Function RunNonSelectCommand(ByVal cmd As SqlServerCe.SqlCeCommand) As Boolean
        sqlException = Nothing
        Try
            Using connection As New SqlServerCe.SqlCeConnection(LocalConnectionString)
                If Not System.IO.File.Exists(connection.Database) Then
                    Dim engine As New System.Data.SqlServerCe.SqlCeEngine(LocalConnectionString)
                    engine.CreateDatabase()
                End If
                cmd.Connection = connection
                connection.Open()

                cmd.ExecuteNonQuery()
                LastSQL = cmd.CommandText

                cmd.Dispose()
                connection.Close()
            End Using
        Catch ex As Exception
            sqlException = ex
            Return False
        End Try
        Return True 'success
    End Function

    Private Function RunSelectCommand(ByVal cmd As SqlServerCe.SqlCeCommand) As DataSet
        sqlException = Nothing
        Dim ds As New DataSet()

        Try
            Using connection As New SqlServerCe.SqlCeConnection(LocalConnectionString)
                If Not System.IO.File.Exists(connection.Database) Then
                    Dim engine As New System.Data.SqlServerCe.SqlCeEngine(LocalConnectionString)
                    engine.CreateDatabase()
                End If
                cmd.Connection = connection
                connection.Open()

                Dim da As System.Data.SqlServerCe.SqlCeDataAdapter
                da = New SqlServerCe.SqlCeDataAdapter
                da.SelectCommand = cmd
                da.Fill(ds)
                LastSQL = cmd.CommandText

                cmd.Dispose()
                connection.Close()
            End Using
        Catch ex As Exception
            sqlException = ex
            Return Nothing
        End Try
        Return ds
    End Function

#End Region

    Public WriteOnly Property ExternalReportChecked() As Boolean
        Set(ByVal value As Boolean)
            ExternalReportCheckBox.Checked = value
        End Set
    End Property

    Private Sub ExternalReportCheckBox_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ExternalReportCheckBox.CheckedChanged
        LoadDropdownBox()
    End Sub

    Private Function LoadDropdownBox(Optional ByVal ShowError As Boolean = True) As Boolean
        Me.ProjectComboBox.Items.Clear()
        Me.ProjectComboBox.Text = ""
        Me.BuildAndShowButton.Enabled = True
        ProjectNfo.Clear()
        Dim cmd As New SqlServerCe.SqlCeCommand()
        Dim ProjectSearchQueryText As String
        ''''''''''''''''''
        If (Me.Filter.LastRunGUID = "%") Then
            cmd.Parameters.AddWithValue("@pProjectGUID", "%")
            ProjectSearchQueryText = ".ProjectGUID LIKE @pProjectGUID"
        Else
            cmd.Parameters.AddWithValue("@pProjectGUID", System.Data.SqlTypes.SqlGuid.Parse(Filter.ReportProjectGUID.ToString()).ToString())
            ProjectSearchQueryText = ".ProjectGUID = @pProjectGUID"
        End If

        cmd.Parameters.AddWithValue("@pComputerName", Filter.PCName)
        cmd.Parameters.AddWithValue("@pTestResults", Filter.TestResultValue())

        cmd.CommandText = "SELECT " & GetSQLRunName() & ".ReportStartTime, " & _
        GetSQLRunName() & ".ProjectName, " & GetSQLRunName() & _
        ".RunNumber, " & GetSQLRunName() & ".ProjectGUID FROM " & GetSQLRunName() & _
        " WHERE " & GetSQLRunName() & ProjectSearchQueryText & _
        " AND " & GetSQLRunName() & ".PCName LIKE @pComputerName" & _
        " AND " & GetSQLRunName() & ".ReportType LIKE @pTestResults"

        If (Filter.TestResults = "Pass") Then ' we need to include "info resulting tests"
            cmd.CommandText = cmd.CommandText & " OR " & GetSQLRunName() & ".ReportType LIKE '" & Report.Info & "'"
        End If
        cmd.CommandText = cmd.CommandText & " ORDER BY " & GetSQLRunName() & ".ReportStartTime DESC"

        Dim rdr As DataTable = RunSelectCommand(cmd).Tables(0)
        If (rdr Is Nothing) Then Throw sqlException

        Dim pjNfo As ProjectNfo
        Dim Count As Integer = 0

        While rdr.Rows.Count > Count
            pjNfo = New ProjectNfo()
            pjNfo.NumberInList = Count
            pjNfo.ProjectName = rdr.Rows(Count).Item("ProjectName").ToString()
            pjNfo.GUID = rdr.Rows(Count).Item("ProjectGUID").ToString()
            ProjectNfo.Add(pjNfo)
            Me.ProjectComboBox.Items.Add("Run: " & rdr.Rows(Count).Item("RunNumber").ToString() & " - " & pjNfo.ProjectName & " - " & rdr.Rows(Count).Item("ReportStartTime").ToString())
            Count += 1
        End While
        If (Me.ProjectComboBox.Items.Count = 0) Then
            Me.TestResultsTextBox.Text = ""
            Me.TestResultsComboBox.Enabled = False
            Me.BuildAndShowButton.Enabled = False
            If (ShowError = True) Then
                System.Windows.Forms.MessageBox.Show("Unable to locate any runs for the current filter stratagy.", MainAppTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            End If
            Return False
        Else
            Me.ProjectComboBox.SelectedIndex = 0
        End If
        '''''''''''''''''
        rdr.Dispose()
        Return True
    End Function

    Enum DataFormats As Byte
        Viewer
        CSV
        Email
        Text
    End Enum

    Private Function ReadAndFormatRunLineData(ByVal rdr As System.Data.DataTable, ByVal Format As DataFormats) As String
        Dim LastTestName As String = String.Empty
        Dim LastTestNumber As String = String.Empty
        Dim LastStepName As String = String.Empty
        Dim LastStepNumber As String = String.Empty
        Dim TotalCount As Int64 = 0
        Dim TestToResult As New System.Collections.Generic.Dictionary(Of String, Boolean) 'Bool = IsPass
        Dim WarningCount As Integer = 0
        Dim PassCount As Integer = 0
        Dim FailCount As Integer = 0
        Dim NumberOfRuns As Integer = 0

        Dim WriteStr As New System.Text.StringBuilder(4000)
        Dim Fail As String = Report.Fail.ToString()
        Dim Pass As String = Report.Pass.ToString()
        Dim Warning As String = Report.Warning.ToString()
        Dim Info As String = Report.Info.ToString()
        Dim ReportType As String = String.Empty
        Dim FirstLoop As Boolean = True
        Dim LoopNumber As Integer = 0
        Dim SkipNewTestLine As Boolean = False
        Dim ClassDisplayType As String = "CLASS =""P"""
        'This needs not to be added to WriteStr because new lines are converted to <BR>'s
        Dim HTMLTopTags As String = "<HTML><TITLE>Slick Test Report</TITLE>" & _
        "<style type=""text/css"" name=""F"">display:""block""</style>" & vbNewLine & _
        "<style type=""text/css"" name=""P"">display:""block""</style>" & vbNewLine & _
        "<style type=""text/css"" name=""W"">display:""block""</style>" & vbNewLine & _
        "<style type=""text/css"" name=""I"">display:""block""</style>" & vbNewLine & _
        "<style type=""text/css"" name=""TestResults"">display:""block""</style>" & vbNewLine & _
        "<style type=""text/css"" name=""TestStats"">display:""block""</style>" & vbNewLine & _
        "<SCRIPT language=""JavaScript""><!-- " & vbNewLine & _
        "function CheckItem(obj,elemStyle){" & vbNewLine & _
        "var dval = obj.checked? ""block"":""none"";" & vbNewLine & _
        "setStyleByClass(obj.name,""display"",dval,elemStyle);}" & vbNewLine & _
        "function setStyleByClass(c,p,v,elemStyle){" & vbNewLine & _
        "var elements = document.getElementsByTagName(String(elemStyle)); " & vbNewLine & _
        "for(var i = 0; i < elements.length; i++){" & vbNewLine & _
        "var node = elements.item(i);" & vbNewLine & _
        "for(var j = 0; j < node.attributes.length; j++) {" & vbNewLine & _
        "if(node.attributes.item(j).nodeName == 'class') {" & vbNewLine & _
        "if(node.attributes.item(j).nodeValue == c) {" & vbNewLine & _
        "eval('node.style.' + p + "" = '"" + v + ""'"");" & vbNewLine & _
        "}}}}}" & vbNewLine & _
        "//--></SCRIPT>" & vbNewLine & _
        "<BODY BGCOLOR =""#C0C0C0"">" & vbNewLine & _
        "<TABLE><TR><TD>" & _
        "<INPUT TYPE=""checkbox"" NAME=""P"" CHECKED=""true"" onClick=""CheckItem(this,'TR')""> Show Passes<BR>" & vbNewLine & _
        "<INPUT TYPE=""checkbox"" NAME=""F"" CHECKED=""true"" onClick=""CheckItem(this,'TR')""> Show Fails<BR>" & vbNewLine & _
        "<INPUT TYPE=""checkbox"" NAME=""W"" CHECKED=""true"" onClick=""CheckItem(this,'TR')""> Show Warns<BR>" & vbNewLine & _
        "<INPUT TYPE=""checkbox"" NAME=""I"" CHECKED=""true"" onClick=""CheckItem(this,'TR')""> Show Infos<BR>" & vbNewLine & _
        "</TD><TD>" & vbNewLine & _
        "<INPUT TYPE=""checkbox"" NAME=""TestResults"" CHECKED=""true"" onClick=""CheckItem(this,'TABLE')""> Show Testset Results<BR>" & vbNewLine & _
        "<INPUT TYPE=""checkbox"" NAME=""TestStats"" CHECKED=""true"" onClick=""CheckItem(this,'TABLE')""> Show Test Stats<BR>" & vbNewLine & _
        "</TD></TR></TABLE>" & vbNewLine
        Dim TestResultsTopTable As String = _
        "<TABLE border=""2"" CLASS=""TestResults"">" & vbNewLine & _
        "<THEAD><TR><TH width=""8%"">Loop</TH> <TH width=""8%"">Results</TH>  <TH width=""8%"">Test Name</TH> <TH width=""7%"">Step Name</TH> <TH width=""32%"">Main Message</TH> " & vbNewLine & _
        "<TH width=""25%"">Additional Information</TH> <TH width=""10%"">Date/Time</TH> </TR></THEAD><TBODY>"

        Dim TestStatsTopTable As String = _
        "<TABLE border=""2"" CLASS=""TestStats"">" & vbNewLine & _
        "<THEAD><TR><TH width=""12%"">Passes</TH> <TH width=""12%"">Fails</TH>  <TH width=""12%"">Warnings</TH> <TH width=""17%""># of Tests</TH> <TH width=""17%""># of Runs</TH> " & vbNewLine & _
        " <TH width=""15%"">Tests Passing</TH> <TH width=""15%"">Tests Failing</TH> </TR></THEAD><TBODY>"

        Dim TestStats As String = ""

        'Loop, Test/Stepname, Main Message, Additional Info, Date/Time
        Dim HTMLBottomTags As String = "</TBODY></TABLE></BODY></HTML>"
        If (Format = DataFormats.Viewer OrElse Format = DataFormats.Email) Then
            For Count As Integer = 0 To rdr.Rows.Count - 1
                If (FirstLoop = True) Then
                    FirstLoop = False
                    Filter.LastRunGUID = rdr.Rows(Count).Item("RunGUID").ToString()
                End If
                LoopNumber = Convert.ToInt32(rdr.Rows(Count).Item("LoopRunNumber"))
                If (rdr.Rows(Count).Item("TestName").ToString() <> LastTestName) Then
                    LastTestName = rdr.Rows(Count).Item("TestName").ToString()
                    If (rdr.Rows(Count).Item("TestNumber").ToString() <> LastTestNumber) Then
                        LastTestNumber = rdr.Rows(Count).Item("TestNumber").ToString()
                        TestToResult.Add(LastTestNumber, True)
                        If (rdr.Rows(Count).Item("StepName").ToString() <> LastStepName) Then
                            LastStepName = rdr.Rows(Count).Item("StepName").ToString()
                            If (rdr.Rows(Count).Item("StepNumber").ToString() <> LastStepNumber) Then
                                LastStepNumber = rdr.Rows(Count).Item("StepNumber").ToString()
                                'WriteStr.Append("Step: <B>" & LastStepName & "</B> -- Step Number: <B>" & LastStepNumber & "</B>" & vbNewLine)  
                            End If
                        End If
                        'We don't care if the step name changes or not, just the test name.
                        If (LoopNumber = -1) Then
                            SkipNewTestLine = True
                        End If
                        If (SkipNewTestLine = False) Then
                            WriteStr.Append("<TR><TD>" & LoopNumber & "</TD><TD>-</TD><TD>" & LastTestName & _
                                    "</TD><TD>" & LastStepName & "</TD><TD><B>New Test.</B></TD><TD>-</TD><TD>-</TD></TR>")
                        End If
                        'WriteStr.Append("Test: <B>" & LastTestName & "</B> -- Test Number: <B>" & LastTestNumber & "</B> -- Loop Number: " & vbNewLine)
                    End If
                End If
                If (rdr.Rows(Count).Item("StepName").ToString() <> LastStepName) Then
                    LastStepName = rdr.Rows(Count).Item("StepName").ToString()
                    If (rdr.Rows(Count).Item("StepNumber").ToString() <> LastStepNumber) Then
                        LastStepNumber = rdr.Rows(Count).Item("StepNumber").ToString()
                        'WriteStr.Append("Step: <B>" & LastStepName & "</B> -- Step Number: <B>" & LastStepNumber & "</B>" & vbNewLine)
                        WriteStr.Append("<TR><TD>" & LoopNumber & "</TD><TD>-</TD><TD>" & LastTestName & _
                        "</TD><TD>" & LastStepName & "</TD><TD>New Step.</TD><TD>-</TD><TD>-</TD></TR>")
                    End If
                End If

                Select Case rdr.Rows(Count).Item("ReportType").ToString()
                    Case Fail
                        ReportType = "<TD><font color=""#CC0000""><B>Fail</B></font></TD>"
                        ClassDisplayType = " CLASS=""F"""
                        TestToResult.Item(LastTestNumber) = False
                        FailCount += 1
                    Case Pass
                        ReportType = "<TD><font color=""#009900""><B>Pass</B></font></TD>"
                        ClassDisplayType = " CLASS=""P"""
                        PassCount += 1
                    Case Warning
                        ReportType = "<TD><font color=""#FFFF00""><B>Warning</B></font></TD>"
                        ClassDisplayType = " CLASS=""W"""
                        WarningCount += 1
                    Case Info
                        ReportType = "<TD><B>Info</B></TD>"
                        ClassDisplayType = " CLASS=""I"""
                End Select

                If (NumberOfRuns < LoopNumber) Then NumberOfRuns = LoopNumber

                Dim MajorText As String = rdr.Rows(Count).Item("MajorText").ToString()
                Dim MinorText As String = rdr.Rows(Count).Item("MinorText").ToString()
                If (MajorText = String.Empty) Then MajorText = "-"
                If (MinorText = String.Empty) Then MinorText = "-"
                SkipNewTestLine = False
                'WriteStr.Append("<" & ReportType & "> - " & rdr.Rows(Count).Item("MajorText").ToString() & " -- " & rdr.Rows(Count).Item("MinorText").ToString() & " occurred at " & rdr.Rows(Count).Item("LineRunTime").ToString() & vbNewLine)
                WriteStr.Append("<TR" & ClassDisplayType & "><TD>" & LoopNumber & "</TD>" & ReportType & _
                "<TD>" & LastTestName & "</TD>" & _
                "<TD>" & LastStepName & "</TD><TD>" & MajorText & "</TD><TD>" & _
                MinorText & "</TD><TD>" & rdr.Rows(Count).Item("LineRunTime").ToString() & "</TD></TR>" & vbCr)
            Next
            TotalCount = PassCount + FailCount + WarningCount
            Dim PassingTests As Integer = 0
            For Each IsPassing As Boolean In TestToResult.Values
                If (IsPassing) Then
                    PassingTests += 1
                End If
            Next
            Dim FailingTests As Integer = TestToResult.Count - PassingTests

            TestStats = "<TR><TD>" & PassCount & " (" & FormatNumber((PassCount / TotalCount) * 100) & "%)</TD><TD>" & _
            FailCount & " (" & FormatNumber((FailCount / TotalCount) * 100) & "%)</TD>" & _
                "<TD>" & WarningCount & " (" & FormatNumber((WarningCount / TotalCount) * 100) & _
                "%)</TD><TD>" & TestToResult.Count & "</TD><TD>" & _
                NumberOfRuns & "</TD>" & _
                "<TD>" & PassingTests & " (" & FormatNumber((PassingTests / TestToResult.Count) * 100) & "%)</TD>" & _
                "<TD>" & FailingTests & " (" & FormatNumber((FailingTests / TestToResult.Count) * 100) & "%)</TD>" & _
                "</TR>" & vbCr
            rdr.Dispose()
            WriteStr.Append(HTMLBottomTags)
            Return HTMLTopTags & TestStatsTopTable & _
            TestStats & HTMLBottomTags & TestResultsTopTable & WriteStr.ToString().Replace(vbNewLine, "<BR>")
        ElseIf (Format = DataFormats.Text OrElse Format = DataFormats.CSV) Then
            For Count As Integer = 0 To rdr.Rows.Count - 1
                If (FirstLoop = True) Then
                    FirstLoop = False
                    Filter.LastRunGUID = rdr.Rows(Count).Item("RunGUID").ToString()
                End If
                If (rdr.Rows(Count).Item("TestName").ToString() <> LastTestName) Then
                    LastTestName = rdr.Rows(Count).Item("TestName").ToString()
                    If (rdr.Rows(Count).Item("TestNumber").ToString() <> LastTestNumber) Then
                        LastTestNumber = rdr.Rows(Count).Item("TestNumber").ToString()
                    End If
                End If
                If (rdr.Rows(Count).Item("StepName").ToString() <> LastStepName) Then
                    LastStepName = rdr.Rows(Count).Item("StepName").ToString()
                    If (rdr.Rows(Count).Item("StepNumber").ToString() <> LastStepNumber) Then
                        LastStepNumber = rdr.Rows(Count).Item("StepNumber").ToString()
                    End If
                End If

                Select Case rdr.Rows(Count).Item("ReportType").ToString()
                    Case Fail
                        ReportType = "Fail"
                    Case Pass
                        ReportType = "Pass"
                    Case Warning
                        ReportType = "Warning"
                    Case Info
                        ReportType = "Info"
                End Select

                If (Format = DataFormats.Text) Then
                    WriteStr.Append(ReportType & " - " & LastTestName & _
                    " - " & LastTestNumber & _
                    " - " & LastStepName & _
                    " - " & LastStepNumber & _
                    " - " & rdr.Rows(Count).Item("MajorText").ToString() & _
                    " - " & rdr.Rows(Count).Item("MinorText").ToString() & _
                    " - " & rdr.Rows(Count).Item("LineRunTime").ToString() & _
                    vbNewLine)
                ElseIf (Format = DataFormats.CSV) Then
                    WriteStr.Append( _
                    FixCSVText(ReportType) & _
                    "," & FixCSVText(LastTestName) & _
                    "," & FixCSVText(LastTestNumber) & _
                    "," & FixCSVText(LastStepName) & _
                    "," & FixCSVText(LastStepNumber) & _
                    "," & FixCSVText(rdr.Rows(Count).Item("MajorText").ToString()) & _
                    "," & FixCSVText(rdr.Rows(Count).Item("MinorText").ToString()) & _
                    "," & FixCSVText(rdr.Rows(Count).Item("LineRunTime").ToString()) & _
                    vbNewLine)
                End If
            Next
            rdr.Dispose()
            'WriteStr.Append(HTMLBottomTags)
            Return WriteStr.ToString()
        End If
        Return ""
    End Function

    Private Function FormatNumber(ByVal number As Double) As String
        Return String.Format("{0:n}", number)
    End Function

    Function FixCSVText(ByVal txt As String) As String
        Dim tmp As String = txt.Replace("""", """""")
        If (tmp.Contains(vbNewLine) = True OrElse tmp.Contains(",") = True) Then
            Return """" & tmp & """"
        End If
        Return tmp
    End Function

    Private Sub WriteText(ByVal text As String)
        Me.ReportTextBox.Document.OpenNew(True)
        Me.ReportTextBox.Document.Write(text)
    End Sub

    Private Sub GetRunData(Optional ByVal RunNumber As String = "", Optional ByVal Format As DataFormats = DataFormats.Viewer)
        If (RunNumber = "") Then
            RunNumber = "MAX(RunNumber)"
            LastBuildPulled = 0
        Else
            LastBuildPulled = Convert.ToInt32(RunNumber)
        End If
        Dim cmd As New SqlServerCe.SqlCeCommand()
        cmd.Parameters.Clear()
        'cmd.Parameters.AddWithValue("@pComputerName", PCName)
        ''''''''**********************MUST ADD Computer filter
        '''''MUST add catch if nothing is ever logged.
        cmd.Parameters.AddWithValue("@pProjectGUID", System.Data.SqlTypes.SqlGuid.Parse(Filter.ReportProjectGUID.ToString()).ToString())

        cmd.CommandText = "SELECT " & GetSQLRunLineName() & ".ReportType, " & _
        GetSQLRunLineName() & ".MajorText, " & GetSQLRunLineName() & ".MinorText," & _
        " " & GetSQLRunLineName() & ".StepNumber, " & GetSQLRunLineName() & _
        ".StepName, " & GetSQLRunLineName() & ".LoopRunNumber, " & _
        GetSQLRunLineName() & ".RunGUID," & _
        GetSQLRunLineName() & ".LineRunTime," & " " & GetSQLRunLineName() & _
        ".TestName, " & GetSQLRunLineName() & ".TestNumber FROM " & GetSQLRunLineName() & " WHERE" & _
        " " & GetSQLRunLineName() & ".RunNumber IN (SELECT " & RunNumber & " FROM " & GetSQLRunName() & ")" & _
        " AND " & GetSQLRunLineName() & ".ProjectGUID = @pProjectGUID ORDER BY " & GetSQLRunLineName() & ".ResultID"
        Dim rdr As System.Data.DataTable = Me.RunSelectCommand(cmd).Tables(0)
        If (rdr Is Nothing) Then Throw sqlException

        If (Me.ReportTextBox.Document = Nothing) Then
            Me.ReportTextBox.DocumentText = ""
        End If
        Me.WriteText(ReadAndFormatRunLineData(rdr, Format))
        If (Format = DataFormats.CSV) Then
            Return 'exit out early for now, to be updated
        End If
        '''''''''''''''''
        'Now we need to get the state of the test we pulled from.
        cmd = New SqlServerCe.SqlCeCommand()

        If (Filter.LastRunGUID = String.Empty) Then
            cmd.Parameters.AddWithValue("@pComputerName", Filter.PCName)
            cmd.CommandText = "SELECT " & _
            GetSQLRunName() & ".ReportType, " & _
            GetSQLRunName() & ".RunComments FROM " & GetSQLRunName() & _
            " WHERE " & GetSQLRunName() & ".RunNumber IN (SELECT " & _
            RunNumber & " FROM " & GetSQLRunName() & ")" & " AND " & _
            GetSQLRunName() & ".PCName LIKE @pComputerName"
            WriteText("No reporting occurred in this run.")
        Else
            cmd.Parameters.AddWithValue("@pRunGUID", Filter.LastRunGUID)
            cmd.CommandText = "SELECT " & _
            GetSQLRunName() & ".ReportType, " & _
            GetSQLRunName() & ".RunComments FROM " & GetSQLRunName() & _
            " WHERE " & GetSQLRunName() & ".RunGUID LIKE @pRunGUID"
        End If

        rdr = Me.RunSelectCommand(cmd).Tables(0)
        If (rdr Is Nothing) Then Throw sqlException

        Dim ReportType As String = String.Empty
        Dim Fail As String = Report.Fail.ToString()
        Dim Pass As String = Report.Pass.ToString()
        Dim Warning As String = Report.Warning.ToString()
        Dim Info As String = Report.Info.ToString()
        Dim Count As Integer = 0
        Try
            Select Case rdr.Rows(Count).Item("ReportType").ToString()
                Case Fail
                    ReportType = "Fail"
                Case Pass
                    ReportType = "Pass"
                Case Warning
                    ReportType = "Warning"
                Case Info
                    ReportType = "Pass"
            End Select
            SetColor(rdr.Rows(Count).Item("ReportType").ToString())
            Me.TestResultsTextBox.Text = rdr.Rows(Count).Item("RunComments").ToString()
        Catch ex As Exception
            ReportType = "Pass"
            SetColor(Pass)
            WriteText("WARNING: An error occurred in reading the report information.")
        End Try

        For i As Integer = 0 To Me.TestResultsComboBox.Items.Count - 1
            If (Me.TestResultsComboBox.Items(i).ToString() = ReportType) Then
                Me.TestResultsComboBox.SelectedIndex = i
                Exit For
            End If
        Next
    End Sub

    Private Sub SetColor(ByVal state As String)
        Dim Fail As String = Report.Fail.ToString()
        Dim Pass As String = Report.Pass.ToString()
        Dim Warning As String = Report.Warning.ToString()
        Dim Info As String = Report.Info.ToString()
        Select Case state
            Case Fail
                Me.BackColor = Color.Red
            Case Pass
                Me.BackColor = Color.Green
            Case Warning
                Me.BackColor = Color.Gold 'it has been found that this color isn't too hard on the eyes.
            Case Info
                Me.BackColor = Color.Green
        End Select
    End Sub

    Private Sub GetBuildFromDropDown()
        If (Me.ProjectComboBox.Items.Count = 0) Then
            System.Windows.Forms.MessageBox.Show( _
            "Unable to locate any runs for the current test.", MainAppTitle, _
            MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Return
        End If
        Dim ProjectRunNumber As String = Me.ProjectComboBox.SelectedItem.ToString()
        Dim CurrentRunNumber As String = String.Empty
        For Each chr As Char In ProjectRunNumber.Substring( _
        ProjectRunNumber.IndexOf("Run: ") + "Run: ".Length).ToCharArray()

            If (Char.IsDigit(chr) = True) Then
                CurrentRunNumber = CurrentRunNumber & chr
            Else
                Exit For
            End If
        Next
        GetRunData(CurrentRunNumber)
    End Sub

    Private Sub BuildAndShowButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BuildAndShowButton.Click
        BuildAndShow()
    End Sub

    Private Sub BuildAndShow()
        Dim index As Integer = -1
        If (Me.ProjectComboBox.SelectedIndex <> -1) Then
            If (ProjectNfo(Me.ProjectComboBox.SelectedIndex).NumberInList = Me.ProjectComboBox.SelectedIndex) Then
                index = Me.ProjectComboBox.SelectedIndex
            Else

                For Each pjNfo As ProjectNfo In ProjectNfo
                    If (pjNfo.NumberInList = Me.ProjectComboBox.SelectedIndex) Then
                        index = pjNfo.NumberInList
                        Exit For
                    End If
                Next
            End If
        End If
        If (index = -1) Then
            System.Windows.Forms.MessageBox.Show("Unable to load results due to an unexpected error." & _
            "  It is possbile the database is corrupted or that no data is currently in the database.", MainAppTitle, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return
        End If
        Me.Filter.ReportProjectGUID = New System.Guid(ProjectNfo(index).GUID)
        GetBuildFromDropDown()
    End Sub

    Private Sub SaveToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SaveToolStripMenuItem.Click
        Me.SaveFileDialog1.Filter = "Text|*.txt"
        Me.SaveFileDialog1.DefaultExt = "txt"
        If (Me.SaveFileDialog1.ShowDialog() = Windows.Forms.DialogResult.OK) Then
            Try
                Dim sw As New System.IO.StreamWriter(Me.SaveFileDialog1.FileName, False, System.Text.Encoding.UTF8)
                sw.Write(Me.ReportTextBox.DocumentText)
                sw.Flush()
                sw.Close()
            Catch ex As Exception
                System.Windows.Forms.MessageBox.Show("Exception occured while trying to write a file. [" & Me.SaveFileDialog1.FileName & "]" & _
                "Exception: " & ex.Message)
            End Try
        End If
    End Sub

    Private Sub CloseToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CloseToolStripMenuItem.Click
        Me.Close()
    End Sub

    Private Sub ClearDatabaseToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ClearDatabaseToolStripMenuItem.Click
        If (ExternalReportCheckBox.Checked = True) Then
            System.Windows.Forms.MessageBox.Show("You can not clear External reports.")
            Return
        Else
        End If
        Dim cmd As New SqlServerCe.SqlCeCommand()
        cmd.CommandText = "DELETE FROM ReportRun WHERE " & _
        "RunNumber NOT IN (SELECT MAX(RunNumber) FROM ReportRun)"
        RunNonSelectCommand(cmd)

        cmd = New SqlServerCe.SqlCeCommand()
        cmd.CommandText = "DELETE FROM ReportLine WHERE " & _
        "RunNumber NOT IN (SELECT MAX(RunNumber) FROM ReportLine)"
        RunNonSelectCommand(cmd)

        LoadDropdownBox()
    End Sub

    Private Sub EditResultsButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles EditResultsButton.Click
        If (Me.ProjectComboBox.Items.Count = 0) Then
            System.Windows.Forms.MessageBox.Show("Unable to locate any runs to be edited for the current test.", MainAppTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Return
        End If
        ResetWindow()
        If (EditResultsButton.Text.Contains("Edit Results") = True) Then
            EditResultsButton.Text = "Cancel Edit"
            PreviousResult = Me.TestResultsComboBox.SelectedItem.ToString()
        Else
            GetBuildFromDropDown()
            EditResultsButton.Text = "Edit Results"
        End If
    End Sub

    Private Sub ResetWindow()
        Me.SaveResultsButton.Enabled = Not SaveResultsButton.Enabled
        Me.TestResultsComboBox.Enabled = Not TestResultsComboBox.Enabled 'combo box
        Me.TestResultsTextBox.Enabled = Not TestResultsTextBox.Enabled 'text box
        Me.ReportViewGroupBox.Enabled = Not Me.ReportViewGroupBox.Enabled
        Me.BuildAndShowButton.Enabled = Not Me.BuildAndShowButton.Enabled
    End Sub

    Private Sub SaveResultsButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SaveResultsButton.Click
        EditResultsButton.Text = "Edit Results"
        ResetWindow()
        Dim cmd As New SqlServerCe.SqlCeCommand()
        ''''''''
        Dim TestResults As Byte
        Select Case Me.TestResultsComboBox.SelectedItem.ToString()
            Case "Fail"
                TestResults = Report.Fail
            Case "Pass"
                TestResults = Report.Pass
            Case "Warning"
                TestResults = Report.Warning
        End Select
        SetColor(TestResults.ToString())
        If (PreviousResult <> Me.TestResultsComboBox.SelectedItem.ToString()) Then
            If (Me.TestResultsTextBox.Text <> "") Then
                Me.TestResultsTextBox.Text += vbNewLine & "The test run was previously set to: " & PreviousResult
            Else
                Me.TestResultsTextBox.Text += "The test run was previously set to: " & PreviousResult
            End If

        End If
        cmd.Parameters.AddWithValue("@pTestResults", TestResults)
        cmd.Parameters.AddWithValue("@pRunGUID", Filter.LastRunGUID)
        cmd.Parameters.AddWithValue("@pRunComments", Me.TestResultsTextBox.Text)
        cmd.CommandText = "UPDATE " & GetSQLRunName() & " SET ReportType = " & _
        "@pTestResults, RunComments = @pRunComments" & " WHERE " & _
        "RunGUID LIKE " & "@pRunGUID" & ";"
        If (RunNonSelectCommand(cmd) = False) Then Throw sqlException
    End Sub

    Private Sub GetLastSQLCommandToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GetLastSQLCommandToolStripMenuItem.Click
        InputBox("Here is the last SQL statement:", "", LastSQL)
    End Sub

    Private Sub ProjectComboBox_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles ProjectComboBox.KeyDown
        If (Keys.Up = e.KeyCode OrElse Keys.Down = e.KeyCode OrElse _
        Keys.Left = e.KeyCode OrElse Keys.Right = e.KeyCode) Then
        Else
            e.Handled = True
        End If
    End Sub

    Private Sub ProjectComboBox_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles ProjectComboBox.KeyPress
        e.Handled = True
    End Sub

    Private Sub TestResultsComboBox_EnabledChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles TestResultsComboBox.EnabledChanged
        'MsgBox("Enable changed")
    End Sub

    Private Sub TestResultsComboBox_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TestResultsComboBox.KeyDown
        If (Keys.Up = e.KeyCode OrElse Keys.Down = e.KeyCode OrElse _
                Keys.Left = e.KeyCode OrElse Keys.Right = e.KeyCode) Then
        Else
            e.Handled = True
        End If
    End Sub

    Private Sub TestResultsComboBox_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TestResultsComboBox.KeyPress
        e.Handled = True
    End Sub

    Private Sub SaveAsCSVToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SaveAsCSVToolStripMenuItem.Click
        Me.SaveFileDialog1.Filter = "CSV|*.csv"
        Me.SaveFileDialog1.DefaultExt = "csv"
        If (Me.SaveFileDialog1.ShowDialog() = Windows.Forms.DialogResult.OK) Then
            Try
                Dim sw As New System.IO.StreamWriter(Me.SaveFileDialog1.FileName, False, System.Text.Encoding.UTF8)
                Dim tmp As String = Me.ReportTextBox.DocumentText
                If (LastBuildPulled = 0) Then
                    Me.GetRunData(, DataFormats.CSV)
                Else
                    Me.GetRunData(LastBuildPulled.ToString(), DataFormats.CSV)
                End If

                sw.Write(Me.ReportTextBox.DocumentText)
                Me.WriteText(tmp)
                sw.Flush()
                sw.Close()
            Catch ex As Exception
                System.Windows.Forms.MessageBox.Show("Exception occured while trying to write a file. [" & Me.SaveFileDialog1.FileName & "]" & _
                "Exception: " & ex.Message)
            End Try
        End If
    End Sub

    Private Sub EmailReportToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles EmailReportToolStripMenuItem.Click
        'Run: 1 - Test1 - 11/28/2007 9:54:18 PM
        Dim EmailName As String = InputBox("Enter all Email address you wish to send to.")
        If (EmailName.Contains("@") = False) Then
            System.Windows.Forms.MessageBox.Show("Invalid email address entered!")
            Return
        End If
        Dim TestName As String = Me.ProjectComboBox.Text.Substring(Me.ProjectComboBox.Text.IndexOf(" - "))
        TestName = TestName.Substring(3, TestName.LastIndexOf(" - ") - 3)
        Dim Subject As String = String.Empty
        If (Me.ExternalReportCheckBox.Checked = True) Then
            Subject = "External "
        Else
            Subject = "Internal "
        End If
        Subject += "Report for " & TestName & " which had a overall result: " & Me.TestResultsComboBox.Text

        Try
            Dim oOutL As Object = Microsoft.VisualBasic.CreateObject("Outlook.Application")
            Dim oMail As Object

            oMail = oOutL.CreateItem(0) 'olMailItem = 0
            oMail.To = EmailName
            oMail.Subject = Subject
            'oMail.Body = Me.ReportTextBox.DocumentText
            'oMail.Body = "Test"
            oMail.BodyFormat = 2 '2 = html
            Dim s As String = Me.ReportTextBox.DocumentText
            oMail.HTMLBody = s
            Dim at As String = "<img src=""" 'attachments
            Dim endAt As String = """>"
            Dim index As Integer = 0
            Dim index2 As Integer = 0
            Dim Attachment As String = String.Empty
            s = s.ToLower()
            Try
                If (s.Contains(at) = True) Then
                    While (s.IndexOf(at, index2) > index2)
                        index = s.IndexOf(at, index2) + at.Length
                        index2 = s.IndexOf(endAt, index)
                        Attachment = s.Substring(index, index2 - index)
                        oMail.Attachments.Add(Attachment)
                    End While
                End If
            Catch ex2 As Exception
                System.Windows.Forms.MessageBox.Show("An error occurred while attaching '" & _
                                                     Attachment & "'.  Some attachments may not be included, but" & _
                                                     " the email will still be sent if possible.")
            End Try
            oMail.Send()
        Catch ex As Exception
            System.Windows.Forms.MessageBox.Show("Outlook must be installed in order to" & _
            " send an Email.  Error Message: " & ex.Message.ToString())
        End Try

    End Sub

    Private Sub ProjectToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ProjectFiltersToolStripMenuItem.Click
        Dim ReportFilterUI As New ReportProjects(Me)
        If (ReportFilterUI.ShowDialog() = Windows.Forms.DialogResult.OK) Then
            Filter.LastRunGUID = ReportFilterUI.Filter.LastRunGUID
            Filter.PCName = ReportFilterUI.Filter.PCName
            Filter.ReportProjectGUID = ReportFilterUI.Filter.ReportProjectGUID
            Filter.TestResults = ReportFilterUI.Filter.TestResults
            If (Me.LoadDropdownBox()) Then
                Me.BuildAndShow()
            End If

        End If
        ReportFilterUI.Dispose()

    End Sub

    Private Sub CompactDatabaseToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CompactDatabaseToolStripMenuItem.Click
        If (System.Windows.Forms.MessageBox.Show("Warning: A temporary copy of the database will be created in order to perform this operation.  Do you wish to continue?", MainAppTitle, MessageBoxButtons.YesNo) = Windows.Forms.DialogResult.No) Then
            Return
        End If
        Dim tmp As String = System.IO.Path.GetFileNameWithoutExtension(ReportDatabasePath)
        tmp = ReportDatabasePath.Replace(tmp, tmp & "1")
        If (System.IO.File.Exists(tmp) = True) Then
            Try
                System.IO.File.Delete(tmp)
            Catch ex As Exception
                System.Windows.Forms.MessageBox.Show("Unable to compact file.  Temporary file '" & tmp & "' could not be deleted.")
                Return
            End Try
        End If
        Dim NewLocalConnectionString As String = "Data Source =""" & tmp & """;Max Database Size=4091;"


        If System.IO.File.Exists(ReportDatabasePath) Then
            Try
                Dim engine As New System.Data.SqlServerCe.SqlCeEngine(LocalConnectionString)
                engine.Compact(NewLocalConnectionString)
            Catch ex As Exception
                System.Windows.Forms.MessageBox.Show("Unable to compact file: " & ex.Message)
                Return
            End Try

            Try
                System.IO.File.Delete(ReportDatabasePath)
                System.IO.File.Copy(tmp, ReportDatabasePath)
                System.IO.File.Delete(tmp)
            Catch ex As Exception
                System.Windows.Forms.MessageBox.Show("Unable to compact file.  Database '" & ReportDatabasePath & "' could not be deleted and replaced with '" & tmp & "'.")
                Return
            End Try
        Else
            System.Windows.Forms.MessageBox.Show("Unable to compact file.  Database '" & ReportDatabasePath & "' could not be found.")
            Return
        End If
        System.Windows.Forms.MessageBox.Show("Compact complete!")
    End Sub

    Private Sub ShrinkDatabaseToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ShrinkDatabaseToolStripMenuItem.Click
        If (System.IO.File.Exists(ReportDatabasePath) = True) Then
            Try
                Dim engine As New System.Data.SqlServerCe.SqlCeEngine(LocalConnectionString)
                engine.Shrink()
            Catch ex As Exception
                System.Windows.Forms.MessageBox.Show("Unable to shrink file: " & ex.Message)
                Return
            End Try
        Else
            System.Windows.Forms.MessageBox.Show("Unable to shrink database.  Database '" & ReportDatabasePath & "' could not be found.")
            Return
        End If
        System.Windows.Forms.MessageBox.Show("Shrink complete!")
    End Sub

    Private Sub OpenDatabaseToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OpenDatabaseToolStripMenuItem.Click
        Me.OpenFileDialog1.InitialDirectory = ReportDatabasePath
        If (Me.OpenFileDialog1.ShowDialog() = Windows.Forms.DialogResult.OK) Then
            Dim tmp As String = ReportDatabasePath
            ReportDatabasePath = Me.OpenFileDialog1.FileName
            If (System.IO.File.Exists(ReportDatabasePath) = False OrElse ReportDatabasePath = String.Empty OrElse _
                System.IO.Path.GetExtension(ReportDatabasePath).ToLower.Contains("sdf") = False) Then
                ReportDatabasePath = tmp
                System.Windows.Forms.MessageBox.Show("Error: Unable to load file.")
                Return
            End If
            Try
                If (LoadDropdownBox(False) = False) Then Throw New Exception("Failed to load drop down box.")
                BuildAndShow()
            Catch ex As Exception
                System.Windows.Forms.MessageBox.Show("Exception occurred while trying to open Database. [" & Me.OpenFileDialog1.FileName & "]" & _
                "Previous database will be reloaded.  Error: " & ex.Message)
                ReportDatabasePath = tmp
                If (LoadDropdownBox(False) = False) Then
                    System.Windows.Forms.MessageBox.Show("An error occurred while attempting to restore database.  Closing report viewer.")
                    Me.Close()
                End If
                BuildAndShow()
            End Try
        End If
    End Sub

    Private Sub RefreshReportsListToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RefreshReportsListToolStripMenuItem.Click
        LoadDropdownBox(True)
    End Sub

    Private Sub PrintToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PrintToolStripMenuItem.Click
        Me.ReportTextBox.ShowPrintDialog()
    End Sub

    Private Sub PrintPreviewToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PrintPreviewToolStripMenuItem.Click
        Me.ReportTextBox.ShowPrintPreviewDialog()
    End Sub

    Private Sub SaveAsHTMLToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SaveAsHTMLToolStripMenuItem.Click
        Me.ReportTextBox.ShowSaveAsDialog()
    End Sub
End Class

Friend Class SQLReporting

    Public Shared Function GetSQLRunName(ByVal IsExternal As Boolean) As String
        If (IsExternal = True) Then
            Return "ExternalReportRun"
        Else
            Return "ReportRun"
        End If
    End Function

    Public Shared Function GetSQLRunLineName(ByVal IsExternal As Boolean) As String
        If (IsExternal = True) Then
            Return "ExternalReportLine"
        Else
            Return "ReportLine"
        End If
    End Function

End Class

Public Class Filters
    Public PCName As String
    Public TestResults As String
    Public ReportProjectGUID As System.Guid
    Public LastRunGUID As String
    Private Shared Report As New Report(System.Guid.Empty, "", "", False, False)


    Public Function TestResultValue() As String
        Select Case TestResults
            Case "Fail"
                Return Report.Fail.ToString()
            Case "Pass"
                Return Report.Pass.ToString()
            Case "Warning"
                Return Report.Warning.ToString()
            Case "Info"
                Return Report.Pass.ToString()
            Case "*", "%"
                Return "%"
        End Select
        Return "%"
    End Function
End Class