Imports System.Security
Imports System.Reflection
Imports System.Runtime.Remoting.Lifetime
Imports System.IO

Public Class SlickUnitRunnerForm
    Private Shared Framework As SlickUnit.Framework
    Private Shared InternalAppDomain As AppDomain = Nothing
    Private Shared runner As SlickUnit.GenericRunner
    Private Shared Info As SlickUnit.IRunnerInfo


    Private Const NamespaceString As String = "Namespace"
    Private Const ClassString As String = "Class"
    Private Const MethodString As String = "Method"
    Public Const MsgBoxTitle As String = "Slick Test"
    Public AllowClose As Boolean = False
    Public DBFilePath As String = Nothing
    Public DatabaseConnectionString As String = Nothing 'not used, until I develop a plugin system.
    Public ProjectGUID As System.Guid
    Public IsExternal As Boolean
    Public ReportSize As System.Drawing.Size
    Public ReportWindowState As FormWindowState
    Private sqlException As Exception
    Private LastSQL As String
    Private WithEvents RunReportForm As ResultsViewer.RunReport
    Private FileList As New System.Collections.Generic.List(Of String)
    Private DefaultFrameworkType As FrameworkType = FrameworkType.SlickUnit
    Private DefaultDirectory As String = ".\TmpCache\"
    Private WithEvents cmd As System.Diagnostics.Process ' current process
    Private ResultString As New System.Text.StringBuilder()
    Private CurrentRunResultString As New System.Text.StringBuilder()


    Private ReadOnly Property LocalConnectionString() As String
        Get
            If (Not String.IsNullOrEmpty(DBFilePath)) Then
                Return "Data Source =""" & DBFilePath & """;Max Database Size=4091;"
            End If
            Return String.Empty
        End Get
    End Property

    Private Sub SlickUnitRunnerForm_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        Framework.Dispose()

        If (Not InternalAppDomain Is Nothing) Then
            System.AppDomain.Unload(InternalAppDomain)
        End If
    End Sub

    Public Function GetAsmsLoaded() As String
        Dim sb As New System.Text.StringBuilder()
        For Each asm As System.Reflection.Assembly In InternalAppDomain.GetAssemblies()
            sb.AppendLine(asm.FullName)
        Next
        Return sb.ToString()
    End Function

    Private Sub SlickUnitRunnerForm_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        If (AllowClose = False) Then e.Cancel = True
        Me.Hide()
    End Sub

    Private Sub SlickUnitRunner_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            ResetFramework(Me.DefaultFrameworkType)
            LoadFiles(FileList.ToArray())
            Dim TestType As System.Type = GetType(SlickUnit.Test)
            Dim TestFixture As System.Type = GetType(SlickUnit.TestFixture)
            If (Me.DefaultFrameworkType = FrameworkType.NUnit) Then
                Dim ImportsType As String = "NUnit.Framework."
                TestType = Framework.GetTypeByString(ImportsType & "TestAttribute")
                TestFixture = Framework.GetTypeByString(ImportsType & "TestFixtureAttribute")
            End If
            If (TestType Is Nothing OrElse TestFixture Is Nothing) Then
                System.Windows.Forms.MessageBox.Show("Unable to filter tests due to an invalid filter for method or class.  Please verify Unit Test framework path.", MsgBoxTitle)
                Return
            End If
            Dim methods As SlickUnit.MethodAndTypeInfo() = Framework.GetValidMethodAndTypeInfo(TestType, TestFixture).ToArray()
            Dim FutureTreeNamespace As New System.Collections.Generic.List(Of String)
            Dim FutureTreeClass As New System.Collections.Generic.List(Of String)
            Dim AlreadyCreatedPath As New System.Collections.Generic.List(Of String)
            For Each m As SlickUnit.MethodAndTypeInfo In methods
                If (m Is Nothing) Then Continue For
                Dim t As Type = m.Type
                If (t Is Nothing) Then Continue For
                If (String.IsNullOrEmpty(t.Namespace) = False) Then
                    If (FutureTreeNamespace.Contains(t.Namespace) = False) Then
                        FutureTreeNamespace.Add(t.Namespace)
                        Me.TreeView.Nodes.Add(t.Namespace, t.Namespace)
                        Me.TreeView.Nodes(t.Namespace).Tag = NamespaceString
                    End If
                End If
                If (String.IsNullOrEmpty(t.Name) = False) Then
                    If (FutureTreeClass.Contains(t.Name) = False) Then
                        Me.TreeView.Nodes(t.Namespace).Nodes().Add(t.Name, t.Name)
                        Me.TreeView.Nodes(t.Namespace).Nodes(t.Name).Tag = ClassString
                        FutureTreeClass.Add(t.Name)
                    End If
                End If
                If (Not m.Method Is Nothing) Then
                    If (String.IsNullOrEmpty(m.Method.Name) = False) Then
                        Dim FullName As String = t.Namespace & "." & t.Name & "." & m.Method.Name
                        If (AlreadyCreatedPath.Contains(FullName)) Then Continue For
                        AlreadyCreatedPath.Add(FullName)
                        Me.TreeView.Nodes(t.Namespace).Nodes(t.Name).Nodes().Add(m.Method.Name, m.Method.Name)
                        Me.TreeView.Nodes(t.Namespace).Nodes(t.Name).Nodes(m.Method.Name).Tag = MethodString
                    End If
                End If
            Next
            Me.TreeView.ExpandAll()
            Me.Activate()
        Catch ex As Exception
            System.Windows.Forms.MessageBox.Show(ex.ToString(), MsgBoxTitle)
        End Try
    End Sub

    Public Sub New(ByVal Files As System.Collections.Generic.List(Of String), ByVal DatabaseFilePath As String, _
                   ByVal ProjectGUID As System.Guid, ByVal IsCurrentlyExternal As Boolean, _
                   ByVal ReportSize As System.Drawing.Size, _
                   ByVal ReportWindowState As FormWindowState, _
                   ByVal DatabaseConnectionString As String, _
                   ByVal DefaultDirectory As String)
        InitializeComponent()

        NonUIInit(Files, DatabaseFilePath, ProjectGUID, IsCurrentlyExternal, ReportSize, ReportWindowState, DatabaseConnectionString, FrameworkType.SlickUnit, DefaultDirectory)
        If (Files Is Nothing) Then
            Me.Close()
        End If

    End Sub

    Public Sub NonUIInit(ByVal Files As System.Collections.Generic.List(Of String), ByVal DatabaseFilePath As String, _
                   ByVal ProjectGUID As System.Guid, ByVal IsCurrentlyExternal As Boolean, _
                   ByVal ReportSize As System.Drawing.Size, _
                   ByVal ReportWindowState As FormWindowState, _
                   ByVal DatabaseConnectionString As String, _
                   ByVal DefaultFramework As Integer, _
                   ByVal DefaultDirectory As String)


        Me.DBFilePath = DatabaseFilePath
        Me.ProjectGUID = ProjectGUID
        Me.IsExternal = IsCurrentlyExternal
        Me.ReportSize = ReportSize
        Me.ReportWindowState = ReportWindowState
        Me.DatabaseConnectionString = DatabaseConnectionString
        Me.DefaultFrameworkType = DirectCast(DefaultFramework, FrameworkType)
        Me.DefaultDirectory = DefaultDirectory
        If (Not Files Is Nothing) Then
            FileList.AddRange(Files.ToArray())
        End If

    End Sub

    Private Sub LoadFiles(ByVal Files As String())
        Try
            For Each file As String In Files
                Framework.AddDll(file)
            Next
            Framework.LoadDlls()
        Catch ex As Exception
            System.Windows.Forms.MessageBox.Show("WARNING: Current this fails to run the first time.  After that" & _
                                                 " if you load the DLL from anywhere but " & _
                                                 System.IO.Directory.GetCurrentDirectory() & _
                                                 ".  Restart the app and load the DLLs from some other directory" & _
                                                 " and all will be well. Ex: " & ex.ToString(), MsgBoxTitle)
        End Try
    End Sub

    Public Sub New()
        AllowClose = True
        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        Dim path As String = System.IO.Path.GetDirectoryName(Application.ExecutablePath)
        If (path.EndsWith("\") = False) Then
            path += "\"
        End If
        'Temporary...
        Me.OpenFileDialog1.InitialDirectory = "..\..\..\Tools\SlickUnitSample\bin\Debug"

        Dim DBLocation As String = String.Empty
        'System.Windows.Forms.MessageBox.Show("In a minute you will be asked to" & _
        '                                    " enter in the file path you wish to load from.")
        Me.OpenFileDialog1.Title = "Select the files you wish to load."
        If (Me.OpenFileDialog1.ShowDialog() = Windows.Forms.DialogResult.OK) Then
            FileList.AddRange(Me.OpenFileDialog1.FileNames)
        Else
            Me.Close()
        End If
        ' Add any initialization after the InitializeComponent() call.

    End Sub

    Private Sub ResetFramework(ByVal frameworkType As FrameworkType)
        If (Framework Is Nothing) Then
            CreateFramework(True, frameworkType, DefaultDirectory, AppDomain.CurrentDomain.SetupInformation.PrivateBinPath)
            Return
        End If
        Dim l As New System.Collections.Generic.List(Of String)
        l.AddRange(Framework.SearchedDllLocations)
        Framework.Dispose()
        CreateFramework(True, frameworkType, DefaultDirectory, AppDomain.CurrentDomain.SetupInformation.PrivateBinPath)
        For Each file As String In l
            Framework.AddDll(file)
        Next
        Framework.LoadDlls()
    End Sub

    Public Shared Function RecursiveSearch(ByVal Directory As String, ByVal fileExt As String, ByVal list As System.Collections.Generic.List(Of String)) As System.Collections.Generic.List(Of String)
        For Each file As String In System.IO.Directory.GetFiles(Directory, fileExt)
            list.Add(file)
        Next
        For Each Dir As String In System.IO.Directory.GetDirectories(Directory)
            list = RecursiveSearch(Dir, fileExt, list)
        Next
        Return list
    End Function

    Public Shared Sub CreateFramework(ByVal includeRunner As Boolean, ByVal frameworkType As FrameworkType, ByVal DirectoryCachePath As String, ByVal ExternalFrameworkPath As String)
        Dim path As String = System.IO.Path.GetDirectoryName(DirectoryCachePath)
        Dim name As String = "SlickUnit.dll"

        If (path.EndsWith("\") = False) Then path += "\"
        path += "..\"
        If (System.IO.Directory.Exists(DirectoryCachePath) = False) Then
            Try
                System.IO.Directory.CreateDirectory(DirectoryCachePath)
            Catch ex As Exception
                System.Windows.Forms.MessageBox.Show("Unable to create folder " & DirectoryCachePath & ".  Please create folder manually.  " & _
                                                     "Slick Test may need to be restarted after creating the folder in order to get the system to work.", _
                                                     MsgBoxTitle, MessageBoxButtons.OK)
            End Try
        End If
        If (Not InternalAppDomain Is Nothing) Then
            RemoveHandler InternalAppDomain.AssemblyResolve, AddressOf CurrentDomain_AssemblyResolve
            System.AppDomain.Unload(InternalAppDomain)

        Else
            Try
                System.IO.File.Copy(path & name, DirectoryCachePath & name, False)

            Catch ex As Exception

            End Try
        End If
        Dim setup As AppDomainSetup = New AppDomainSetup()
        setup.ApplicationName = "Framework"
        setup.ApplicationBase = path
        Dim AddFiles As New System.Collections.Generic.List(Of String)
        If (System.IO.Directory.Exists(ExternalFrameworkPath)) Then
            AddFiles = RecursiveSearch(ExternalFrameworkPath, ".DLL", New System.Collections.Generic.List(Of String)())
            For Each f As String In AddFiles
                Try
                    System.IO.File.Copy(f, DirectoryCachePath & System.IO.Path.GetFileName(f), True)
                Catch ex As Exception
                End Try
            Next
        End If

        setup.PrivateBinPath = DirectoryCachePath

        Dim evidence As New Security.Policy.Evidence(AppDomain.CurrentDomain.Evidence)
        If (evidence.Count = 0) Then
            Dim zone As New Security.Policy.Zone(SecurityZone.MyComputer)
            evidence.AddHost(zone)
            Dim Assembly As Reflection.Assembly = Reflection.Assembly.GetExecutingAssembly()
            Dim url As New Security.Policy.Url(Assembly.CodeBase)
            evidence.AddHost(url)
            Dim hash As New Security.Policy.Hash(Assembly)
            evidence.AddHost(hash)
        End If
        Dim fullName As String = String.Empty
        For Each asm As System.Reflection.Assembly In AppDomain.CurrentDomain.GetAssemblies()
            If (asm.FullName.Contains("SlickUnit") AndAlso Not asm.FullName.Contains("Runner")) Then
                fullName = asm.Location
                Exit For
            End If
        Next
        InternalAppDomain = System.AppDomain.CreateDomain(setup.ApplicationName & "Domain", evidence, setup)
        System.Console.WriteLine("InternalAppDomain primary directory: " & InternalAppDomain.SetupInformation.ApplicationBase & vbNewLine & _
                         "Secondary directories: " & InternalAppDomain.SetupInformation.PrivateBinPath)
        AddHandler InternalAppDomain.AssemblyResolve, AddressOf CurrentDomain_AssemblyResolve

        Console.WriteLine("Creating framework using ASM full name:" & fullName)
        Framework = DirectCast(InternalAppDomain.CreateInstanceFrom(DirectoryCachePath & name, "SlickUnit.Framework").Unwrap(), SlickUnit.Framework)
        If (includeRunner) Then
            Dim RunnerType As String = "SlickUnit.SlickRunner"
            If (frameworkType = SlickUnitRunner.FrameworkType.NUnit) Then RunnerType = "SlickUnit.NUnitRunner"
            runner = DirectCast(InternalAppDomain.CreateInstanceAndUnwrap(fullName, RunnerType), SlickUnit.GenericRunner)
            Info = DirectCast(InternalAppDomain.CreateInstanceAndUnwrap(fullName, "SlickUnit.RunnerInfo"), SlickUnit.RunnerInfo)
        End If
        Framework.Init()
        Framework.InitDefaultExcludeItems()
        For Each file As String In AddFiles
            Framework.AddDll(file)
        Next
    End Sub

    Public Shared Function CurrentDomain_AssemblyResolve(ByVal sender As Object, ByVal args As ResolveEventArgs) As Assembly
        Try
            Dim assembly As Reflection.Assembly = System.Reflection.Assembly.LoadFrom(args.Name)
            If (Not (assembly) Is Nothing) Then
                Return assembly
            End If
        Catch ex As System.Exception
            ' ignore load error }
            ' *** Try to load by filename - split out the filename of the full assembly name
            ' *** and append the base path of the original assembly (ie. look in the same dir)
            ' *** NOTE: this doesn't account for special search paths but then that never
            '           worked before either.
            Dim Parts() As String = args.Name.Split(","c)
            Dim File As String = (System.IO.Path.GetDirectoryName(Assembly.GetExecutingAssembly.Location) + ("\\" _
                        + (Parts(0).Trim + ".dll")))
            Return System.Reflection.Assembly.LoadFrom(File)
        End Try
        Return Nothing
    End Function


    Private Sub RunTestButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RunTestButton.Click
        RunTest()
    End Sub

    Private Sub RunTestsToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RunTestsToolStripMenuItem.Click
        RunTest()
    End Sub

    Private Sub RunTest()
        Me.RunTestButton.Enabled = False
        If (Me.TreeView.SelectedNode Is Nothing) Then
            System.Windows.Forms.MessageBox.Show("At least one item must be selected.", MsgBoxTitle)
            Return
        End If
        If (Me.IncludeFilterTextBox.Text <> "" OrElse _
            Me.ExcludeFilterTextBox.Text <> "") Then
            If (Me.TreeView.SelectedNode.Tag.ToString() = MethodString) Then
                If (System.Windows.Forms.MessageBox.Show("Filters cannot be used on single tests.  Ignore the filters?", _
                                                         MsgBoxTitle, MessageBoxButtons.YesNo) = Windows.Forms.DialogResult.No) Then
                    Return
                End If
            End If
        End If

        RunTestsUsingCmdLine()
        'If (True) Then Return

        'Dim Node As TreeNode = Me.TreeView.SelectedNode
        'Dim text As String = ""
        'Dim MethodType As String = Node.Tag

        'Dim tmpFilter As String

        'If (Me.IncludeFilterTextBox.Text <> "") Then
        '    tmpFilter = IncludeFilterTextBox.Text
        '    tmpFilter = "*" + tmpFilter.Replace("|", "*|*").Trim() + "*"

        '    Info.LikeMethodName = New System.Collections.Generic.List(Of String)(tmpFilter.Split("|"c))
        'End If
        'If (Me.ExcludeFilterTextBox.Text <> "") Then
        '    tmpFilter = ExcludeFilterTextBox.Text
        '    tmpFilter = "*" + tmpFilter.Replace("|", "*|*").Trim() + "*"

        '    Info.NotLikeMethodName = New System.Collections.Generic.List(Of String)(tmpFilter.Split("|"c))
        'End If

        'While (Not Node Is Nothing)
        '    text = Node.Text.Trim() + "." + text
        '    Node = Node.Parent
        'End While

        'text = text.TrimEnd(".".ToCharArray())
        'Dim t As System.Type
        'ResetFramework(Me.DefaultFrameworkType)
        'Info.Framework = Framework
        'Info.ExactCase = Me.UseExactTextCheckBox.Checked
        'runner.InitTypes(Framework)

        'Select Case MethodType
        '    Case MethodString
        '        t = Framework.GetTypeByString(text.Substring(0, text.LastIndexOf(".")))
        '        Dim TestType As System.Type = GetType(SlickUnit.Test)
        '        Dim TestFixtureType As System.Type = GetType(SlickUnit.TestFixture)
        '        If (Me.DefaultFrameworkType = FrameworkType.NUnit) Then
        '            'Dim ImportsType As String = "NUnit.Framework."
        '            TestFixtureType = runner.ClassAttributeToRunType 'Framework.GetTypeByString(ImportsType & "TestFixtureAttribute")
        '            TestType = runner.TestType 'Framework.GetTypeByString(ImportsType & "TestAttribute")
        '        End If
        '        Dim method As SlickUnit.MethodAndTypeInfo = Framework.GetValidMethodAndTypeInfo(TestType, TestFixtureType, t, text.Substring(text.LastIndexOf(".") + 1))(0)
        '        Info.ClassTypeFilter = t
        '        Info.Test = method
        '        runner.RunTestInClass(Info)
        '    Case ClassString
        '        t = Framework.GetTypeByString(text)
        '        Info.ClassTypeFilter = t
        '        runner.RunTestsInClass(Info)
        '    Case NamespaceString
        '        For Each NodeToRun As TreeNode In Me.TreeView.SelectedNode.Nodes
        '            text = ""
        '            Node = NodeToRun
        '            While (Not Node Is Nothing)
        '                text = Node.Text.Trim() + "." + text
        '                Node = Node.Parent
        '            End While
        '            text = text.TrimEnd(".".ToCharArray())
        '            t = Framework.GetTypeByString(text)
        '            Info.ClassTypeFilter = t
        '            runner.RunTestsInClass(Info)
        '        Next
        'End Select

        'Dim results As New System.Text.StringBuilder((1 + runner.TestResults.Count) * 120)
        'Dim count As Integer = 0
        'SlickUnit.ConsoleHandler.DetachConsoleProcess()
        'For Each item As SlickUnit.GenericTestResults In runner.TestResults
        '    If (Not String.IsNullOrEmpty(Me.DBFilePath)) Then
        '        Me.AddConsoleReporting(item.ToString(), item.TestName, count)
        '        count += 1
        '    End If
        '    results.AppendLine(item.ToString())
        '    results.AppendLine("////////********************/////////")

        'Next

        'If (Not String.IsNullOrEmpty(Me.DBFilePath)) Then
        '    If (count <> 0) Then
        '        Me.RunReportForm = New ResultsViewer.RunReport(Me.DBFilePath, Me.ProjectGUID, _
        '                                          Me.IsExternal, Me.ReportSize, Me.ReportWindowState)
        '        Me.AddOwnedForm(RunReportForm)
        '        RunReportForm.ShowDialog()
        '        Me.RunReportForm.Close()
        '        System.Windows.Forms.MessageBox.Show(results.ToString(), MsgBoxTitle + " Results", MessageBoxButtons.OK)

        '    Else
        '        System.Windows.Forms.MessageBox.Show("No results found.  Invalid filter?", MsgBoxTitle + " Results", MessageBoxButtons.OK)
        '    End If

        'Else
        '    If (results.Length = 0) Then
        '        System.Windows.Forms.MessageBox.Show("No results found.  Invalid filter?", MsgBoxTitle + " Results", MessageBoxButtons.OK)
        '    Else
        '        System.Windows.Forms.MessageBox.Show(results.ToString(), MsgBoxTitle + " Results", MessageBoxButtons.OK)
        '    End If
        'End If
    End Sub

    Public Sub RunTestsUsingCmdLine()
        Dim proc As New System.Diagnostics.ProcessStartInfo()
        proc.WorkingDirectory = System.Windows.Forms.Application.StartupPath
        proc.FileName = "SlickUnitRunnerCmd.exe"
        proc.WindowStyle = ProcessWindowStyle.Hidden
        Dim sb As New System.Text.StringBuilder()
        Dim Node As TreeNode = Me.TreeView.SelectedNode
        Dim MethodType As String = Node.Tag
        Dim Text As String = ""

        While (Not Node Is Nothing)
            Text = Node.Text.Trim() + "." + Text
            Node = Node.Parent
        End While

        Text = Text.TrimEnd(".".ToCharArray())

        Select MethodType
            Case MethodString
                sb.Append("""-RunInTest:")
            Case ClassString
                sb.Append("""-RunInClass:")
            Case NamespaceString
                sb.Append("""-RunInNamespace:")
            Case Else
                System.Windows.Forms.MessageBox.Show("Invalid method type: " & MethodType & ".", MsgBoxTitle)
                Return
        End Select
        sb.Append(Text & """")

        sb.Append(" -FRAMEWORK:")
        If (DefaultFrameworkType = FrameworkType.NUnit) Then
            sb.Append("NUNIT")
        Else
            sb.Append("SLICKUNIT")
        End If
        If (Not Framework.NotSearchedDllLocations Is Nothing) Then
            For Each additionalDll As String In Framework.NotSearchedDllLocations
                sb.Append(" -ADDITIONALDLLS:" & additionalDll)
            Next
        End If
        If (Framework.SearchedDllLocations Is Nothing) Then
            System.Windows.Forms.MessageBox.Show("No used DLLs in list.", MsgBoxTitle)
        End If
        For Each additionalDll As String In Framework.SearchedDllLocations
            sb.Append(" """ & additionalDll & """")
        Next
        If (Not System.String.IsNullOrEmpty(Me.IncludeFilterTextBox.Text)) Then sb.Append(" -Include:" & Me.IncludeFilterTextBox.Text)
        If (Not System.String.IsNullOrEmpty(Me.ExcludeFilterTextBox.Text)) Then sb.Append(" -Exclude:" & Me.ExcludeFilterTextBox.Text)

        sb.Append(" -OUT:Tmp.txt ")
        sb.Append(" -RESULTS:TmpResults.txt ")

        'sb.Append("-WAIT")
        proc.Arguments = sb.ToString()
        cmd = New System.Diagnostics.Process()
        cmd.EnableRaisingEvents = True
        cmd.StartInfo = proc
        If (System.IO.File.Exists(System.Windows.Forms.Application.StartupPath & "\Tmp.txt")) Then
            System.IO.File.Delete(System.Windows.Forms.Application.StartupPath & "\Tmp.txt")
        End If
        If (System.IO.File.Exists(System.Windows.Forms.Application.StartupPath & "\TmpResults.txt")) Then
            System.IO.File.Delete(System.Windows.Forms.Application.StartupPath & "\TmpResults.txt")
        End If
        cmd.Start()
        Timer1.Start()
        Timer1.Enabled = True
        Me.StopTestsButton.Enabled = True
    End Sub

    'Case "-OUT"
    '           Data.OutputFile = argValue
    '       Case "-XML"
    '           Data.Xml = argValue
    '       Case "-DISPLAY"
    '           Data.DisplayMethod = True


    Private Sub StopTestsButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles StopTestsButton.Click
        Me.RunTestButton.Enabled = True
        If (cmd Is Nothing) Then Return
        Me.StopTestsButton.Enabled = False
        Try
            cmd.Close()
        Catch ex As Exception
            System.Windows.Forms.MessageBox.Show("Failed to stop tests.  Error:" & ex.ToString(), MsgBoxTitle)
        End Try
        Timer1.Stop()
        Timer1.Enabled = False

    End Sub

    Private Sub TreeView_MouseClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles TreeView.MouseClick
        If (e.Button = Windows.Forms.MouseButtons.Right) Then
            Me.TreeView.SelectedNode = Me.TreeView.GetNodeAt(e.X, e.Y)
        Else
            'display message
        End If
    End Sub

    Private Function RunNonSelectCommand(ByRef cmd As SqlServerCe.SqlCeCommand) As Boolean
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
                'connection.Close()
                'connection.Dispose()
            End Using
        Catch ex As Exception
            sqlException = ex
            Return False
        End Try
        Return True 'success
    End Function

    Private Function RunSelectCommand(ByRef cmd As SqlServerCe.SqlCeCommand) As DataSet
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
                'connection.Close()
                'connection.Dispose()
            End Using
        Catch ex As Exception
            sqlException = ex
            Return Nothing
        End Try
        Return ds
    End Function

    Private Sub AddConsoleReporting(ByVal Report As String, ByVal DisplayTestName As String, Optional ByVal ReportConsoleOffset As Integer = 0)
        If (String.IsNullOrEmpty(Report)) Then Return
        If (String.IsNullOrEmpty(Report.Trim())) Then Return
        Dim TestName As String = DisplayTestName
        If (String.IsNullOrEmpty(TestName)) Then
            TestName = "Console Report"
        End If

        '--Get last report GUID
        Dim cmd As New SqlServerCe.SqlCeCommand()
        cmd.Parameters.Clear()
        cmd.Parameters.AddWithValue("@pComputerName", FixSQLStr(My.Computer.Name))
        cmd.Parameters.AddWithValue("@pUserName", FixSQLStr(System.Windows.Forms.SystemInformation.UserName))

        Dim GetGuidsAndCompleteTime As String = _
        "SELECT top(" & (ReportConsoleOffset + 1) & ") ProjectGUID, RunGUID FROM ReportRun " & _
        "WHERE PCName = @pComputerName AND " & _
        "UserName = @pUserName " & _
        "ORDER BY RunNumber DESC, ReportStartTime DESC, ReportCompletedTime DESC"
        'SELECT  * FROM ReportRun ORDER BY RunNumber DESC, ReportStartTime DESC, ReportCompletedTime DESC 
        cmd.CommandText = GetGuidsAndCompleteTime
        Dim ds As DataSet = RunSelectCommand(cmd)
        If (ds Is Nothing) Then Throw sqlException
        Dim rdr As DataTable = ds.Tables(0)
        If (rdr Is Nothing) Then Throw sqlException
        Dim ProjectGUID As String
        Dim RunGUID As String

        If (Not (rdr.Rows.Count > ReportConsoleOffset)) Then Return

        ProjectGUID = rdr.Rows(ReportConsoleOffset).Item("ProjectGUID").ToString()
        RunGUID = rdr.Rows(ReportConsoleOffset).Item("RunGUID").ToString()

        rdr.Clear()

        'Do I need this?

        cmd = New SqlServerCe.SqlCeCommand()
        cmd.Parameters.AddWithValue("@pProjectGUID", FixSQLStr(ProjectGUID))
        cmd.Parameters.AddWithValue("@pRunGUID", FixSQLStr(RunGUID))

        Dim GetLatestRunNumberAndTestNumber As String = _
        "SELECT RunNumber, TestNumber, LoopRunNumber FROM ReportLine " & _
        "WHERE ProjectGUID = @pProjectGUID AND " & _
        "RunGUID = @pRunGUID " & _
        "ORDER BY LineRunTime DESC, RunNumber DESC"

        cmd.CommandText = GetLatestRunNumberAndTestNumber
        ds = RunSelectCommand(cmd)
        If (ds Is Nothing) Then Throw sqlException
        rdr = ds.Tables(0)
        If (rdr Is Nothing) Then Throw sqlException
        Dim RunNumber As Long
        Dim TestNumber As Long
        Dim LoopRunNumber As Long
        If (Not (rdr.Rows.Count > ReportConsoleOffset)) Then Return

        RunNumber = Convert.ToInt64(rdr.Rows(ReportConsoleOffset).Item("RunNumber").ToString())
        TestNumber = Convert.ToInt64(rdr.Rows(ReportConsoleOffset).Item("TestNumber").ToString()) + 1
        LoopRunNumber = Convert.ToInt64(rdr.Rows(ReportConsoleOffset).Item("LoopRunNumber").ToString()) + 1

        rdr.Clear()

        cmd = New SqlServerCe.SqlCeCommand()

        '--- insert report...

        cmd.Parameters.AddWithValue("@pCurrentRunNumber", RunNumber)
        cmd.Parameters.AddWithValue("@pTypeOfMessage", 2) 'info
        cmd.Parameters.AddWithValue("@pMajorMessage", FixSQLStr(Report))
        cmd.Parameters.AddWithValue("@pMinorMessage", FixSQLStr(""))
        cmd.Parameters.AddWithValue("@pStepNumber", 1)
        cmd.Parameters.AddWithValue("@pStepName", FixSQLStr("Console Output"))
        cmd.Parameters.AddWithValue("@pTestNumber", 1)
        cmd.Parameters.AddWithValue("@pTestName", FixSQLStr(TestName))
        cmd.Parameters.AddWithValue("@pCurrentLoopInRuns", LoopRunNumber)
        cmd.Parameters.AddWithValue("@pRunLineGUID", System.Data.SqlTypes.SqlGuid.Parse(System.Guid.NewGuid().ToString()).ToString())
        cmd.Parameters.AddWithValue("@pProjectGUID", FixSQLStr(ProjectGUID))
        cmd.Parameters.AddWithValue("@pRunGUID", FixSQLStr(RunGUID))

        cmd.CommandText = _
        "INSERT INTO " & GetSQLRunLineName() & _
        "([RunNumber],[ReportType],[MajorText],[MinorText],[StepNumber]," & _
        "[StepName],[TestNumber],[TestName],[LoopRunNumber],[LineRunTime]," & _
        "[RunGUID],[ProjectGUID],[RunLineGUID]) " & _
        " VALUES(" & "@pCurrentRunNumber" & _
        ", " & "@pTypeOfMessage" & ", " & "@pMajorMessage" & ", " & "@pMinorMessage" & _
        ", " & "@pStepNumber" & " , " & "@pStepName" & ", " & "@pTestNumber" & _
        ", " & "@pTestName" & " , " & "@pCurrentLoopInRuns" & ", '" & GetTime() & "'" & _
        ", " & "@pRunGUID, @pProjectGUID, @pRunLineGUID);"

        If (Me.RunNonSelectCommand(cmd) = False) Then
            Throw sqlException
        End If
        rdr.Dispose()
        ds.Dispose()
        cmd = New SqlServerCe.SqlCeCommand()

    End Sub

    Private Sub RunReportForm_ClosingWindow(ByVal MySize As System.Drawing.Size, ByVal MyWindowState As System.Windows.Forms.FormWindowState) Handles RunReportForm.ClosingWindow
        Me.ReportSize = MySize
        Me.ReportWindowState = MyWindowState
    End Sub

    Private Function GetSQLRunLineName() As String
        If (IsExternal = True) Then
            Return "ExternalReportLine"
        Else
            Return "ReportLine"
        End If
    End Function

    Private Function GetTime() As String
        Return Now.ToString("yyyy-MM-dd HH:mm:ss")
    End Function

    Private Function FixSQLStr(ByVal Str As String) As String
        Return Str 'Str.Replace("'", "''")
    End Function

    Public Overrides Function InitializeLifetimeService() As Object
        Dim lease As ILease = DirectCast(MyBase.InitializeLifetimeService(), ILease)
        If (lease.CurrentState = LeaseState.Initial) Then
            lease.InitialLeaseTime = TimeSpan.FromDays(5)
            lease.RenewOnCallTime = TimeSpan.FromDays(5)
            lease.SponsorshipTimeout = TimeSpan.FromDays(5)
        End If
        Return lease
    End Function

    Private Sub SetStopButton(ByVal isEnabled As String)
        ' InvokeRequired required compares the thread ID of the
        ' calling thread to the thread ID of the creating thread.
        ' If these threads are different, it returns true.
        If Me.StopTestsButton.InvokeRequired Then
            Dim d As New SetStopButtonCallback(AddressOf SetStopButton)
            Me.Invoke(d, New Object() {isEnabled})
        Else
            Me.StopTestsButton.Enabled = isEnabled
        End If
    End Sub

    Delegate Sub SetStopButtonCallback(ByVal isEnabled As String)

    Private Sub SetRunButton(ByVal isEnabled As String)
        If Me.StopTestsButton.InvokeRequired Then
            Dim d As New SetRunButtonCallback(AddressOf SetRunButton)
            Me.Invoke(d, New Object() {isEnabled})
        Else
            Me.RunTestButton.Enabled = isEnabled
        End If
    End Sub

    Delegate Sub SetTextBoxResultsCallback(ByVal text As String)

    Private Sub SetTextBoxResults(ByVal text As String)
        If Me.StopTestsButton.InvokeRequired Then
            Dim d As New SetTextBoxResultsCallback(AddressOf SetTextBoxResults)
            Me.Invoke(d, New Object() {text})
        Else
            Me.TextBoxResults.Text = text
        End If
    End Sub

    Delegate Sub SetRunButtonCallback(ByVal isEnabled As String)


    Private Sub cmd_Exited(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmd.Exited
        Timer1.Stop()
        Timer1.Enabled = False
        SetStopButton(False)
        SetRunButton(True)
        'System.Windows.Forms.MessageBox.Show("Done with Args: " & cmd.StartInfo.Arguments, MsgBoxTitle)
        'System.Windows.Forms.MessageBox.Show("Result: " & _
        '                                     System.IO.File.ReadAllText(System.Windows.Forms.Application.StartupPath & _
        '                                                                "\Tmp.txt"), MsgBoxTitle)
        ResultString.AppendLine(CurrentRunResultString.ToString())
        If (System.IO.File.Exists(System.Windows.Forms.Application.StartupPath & "\TmpResults.txt")) Then
            Dim file As String = System.IO.File.ReadAllText(System.Windows.Forms.Application.StartupPath & "\TmpResults.txt")
            SetTextBoxResults(file)
        End If
        CurrentRunResultString = New System.Text.StringBuilder()
    End Sub

    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        Timer1.Stop()
        Dim path As String = System.Windows.Forms.Application.StartupPath & "\Tmp.txt"
        Dim file As String = GetFileContents(path)
        Dim length As Long = CurrentRunResultString.Length - 1
        If (length = -1) Then length = 0
        CurrentRunResultString.Append(file.Substring(Math.Min(length, file.Length)))
        Me.TextBoxResults.Text = file
        Timer1.Start()
    End Sub
    Public Function GetFileContents(ByVal FullPath As String) As String
        If (System.IO.File.Exists(FullPath) = False) Then Return ""
        Dim strContents As String
        Dim objReader As StreamReader
        Try

            objReader = New StreamReader(FullPath, True)
            strContents = objReader.ReadToEnd()
            objReader.Close()
            Return strContents
        Catch Ex As Exception
        End Try
        Return String.Empty
    End Function
End Class
<Serializable()> _
Public Enum FrameworkType
    NUnit
    SlickUnit
End Enum