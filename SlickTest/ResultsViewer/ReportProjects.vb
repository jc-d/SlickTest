Public Class ReportProjects

    'Private ReportProjectsGUI As RunReport
    Public Filter As Filters
    Public ExternalReportChecked As Boolean
    Private ReportDatabasePath As String
    Private ProjectNfo As New System.Collections.Generic.List(Of ProjectNfo)()

    Public Sub New(ByRef ReportProjectResultViewer As RunReport)
        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        ' Add any initialization after the InitializeComponent() call.

        Filter = New Filters()
        Me.ExternalReportsCheckBox.Checked = ReportProjectResultViewer.ExternalReportCheckBox.Checked
        ReportDatabasePath = ReportProjectResultViewer.ReportDatabasePath
        Filter.LastRunGUID = ReportProjectResultViewer.Filter.LastRunGUID
        Filter.PCName = ReportProjectResultViewer.Filter.PCName
        Filter.ReportProjectGUID = ReportProjectResultViewer.Filter.ReportProjectGUID
        Filter.TestResults = ReportProjectResultViewer.Filter.TestResults

    End Sub

    Private Sub Reload()
        Dim LocalConnectionString As String = "Data Source =""" & ReportDatabasePath & """;Max Database Size=4091;"
        Me.PCComboBox.Items.Clear()
        Me.ProjectSelectComboBox.Items.Clear()
        Using connection As New SqlServerCe.SqlCeConnection(LocalConnectionString)
            Dim cmd As New SqlServerCe.SqlCeCommand()
            cmd.Connection = connection
            connection.Open()
            Dim rdr As System.Data.SqlServerCe.SqlCeDataReader
            cmd.Parameters.Clear()

            cmd.CommandText = "SELECT DISTINCT " & SQLReporting.GetSQLRunName(ExternalReportsCheckBox.Checked) & ".ProjectName, " _
            & SQLReporting.GetSQLRunName(ExternalReportsCheckBox.Checked) & ".PCName, " & _
            SQLReporting.GetSQLRunName(ExternalReportsCheckBox.Checked) & ".ProjectGUID FROM " & _
            SQLReporting.GetSQLRunName(ExternalReportsCheckBox.Checked) & ""

            rdr = cmd.ExecuteReader()
            PCComboBox.Items.Add("*")
            ProjectSelectComboBox.Items.Add("*")
            Dim pjNfo As ProjectNfo

            Dim Count As Integer = 0
            While (rdr.Read())
                pjNfo = New ProjectNfo()

                pjNfo.NumberInList = Count
                pjNfo.ProjectName = rdr.Item("ProjectName").ToString()
                pjNfo.GUID = rdr.Item("ProjectGUID").ToString()
                ProjectNfo.Add(pjNfo)
                If (PCComboBox.Items.Contains(rdr.Item("PCName").ToString()) = False) Then
                    PCComboBox.Items.Add(rdr.Item("PCName").ToString())
                End If
                ProjectSelectComboBox.Items.Add(pjNfo)
                Count += 1
            End While
            ProjectSelectComboBox.SelectedIndex = 0
            PCComboBox.SelectedIndex = 0
            Me.TestResultsComboBox.SelectedIndex = 0
            rdr.Dispose()
            connection.Close()
        End Using
    End Sub

    Private Sub ReportProjects_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Reload()
    End Sub

    Private Sub OkButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OkButton.Click
        Dim Project As String = Me.ProjectSelectComboBox.SelectedItem.ToString()
        If (Project.Trim() = "") Then
            Project = "*"
        End If

        Dim PC As String = Me.PCComboBox.SelectedItem.ToString()
        If (PC.Trim() = "") Then
            PC = "*"
        End If
        If (Me.TestResultsComboBox.SelectedItem.ToString().Trim = "") Then
            Filter.TestResults = "%"
        Else
            Filter.TestResults = Me.TestResultsComboBox.SelectedItem.ToString
        End If
        Filter.PCName = PC
        Filter.PCName = Filter.PCName.Replace("*", "%")
        Filter.TestResults = Filter.TestResults.Replace("*", "%")
        If (Project <> "*") Then
            For Each pjNfo As ProjectNfo In ProjectNfo
                If (pjNfo.ProjectName = Project) Then
                    Filter.LastRunGUID = pjNfo.GUID
                    Filter.ReportProjectGUID = New System.Guid(pjNfo.GUID)
                    Exit For
                End If
            Next
        Else
            Filter.LastRunGUID = "*"
        End If
        Me.ExternalReportChecked = Me.ExternalReportsCheckBox.Checked
        Filter.LastRunGUID = Filter.LastRunGUID.Replace("*", "%")
        Me.DialogResult = Windows.Forms.DialogResult.OK
        Me.Close()
    End Sub

    Private Sub CancelButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CancelProjectSelectButton.Click
        Me.DialogResult = Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub

    Private Sub TestResultsComboBox_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles PCComboBox.KeyDown, ProjectSelectComboBox.KeyDown, TestResultsComboBox.KeyDown
        If (Keys.Up = e.KeyCode OrElse Keys.Down = e.KeyCode OrElse _
                Keys.Left = e.KeyCode OrElse Keys.Right = e.KeyCode) Then
        Else
            e.Handled = True
        End If
    End Sub

    Private Sub TestResultsComboBox_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles PCComboBox.KeyPress, ProjectSelectComboBox.KeyPress, TestResultsComboBox.KeyPress
        e.Handled = True
    End Sub

    Private Sub ExternalReportsCheckBox_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ExternalReportsCheckBox.CheckedChanged

    End Sub
End Class

Public Class ProjectNfo
    Public ProjectName As String
    Public GUID As String
    Public NumberInList As Int32

    Public Overrides Function ToString() As String
        Return ProjectName
    End Function
End Class