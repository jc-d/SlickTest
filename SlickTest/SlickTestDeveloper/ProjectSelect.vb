Public Class ProjectSelect
    Public Event ProjectSelect(ByVal CurrentProject As CurrentProjectData, ByVal NewProject As Boolean)
    Public Project As New CurrentProjectData
    Protected Friend AllowedToClose As Boolean = True
    Private SlickTestDev As SlickTestDev

    Private Sub ProjectSelect_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If (Me.LanguageDropDown.Items.Count = 0) Then
            Me.LanguageDropDown.Items.Add("VB.NET (.vb)")
            Me.LanguageDropDown.Items.Add("C# (.cs)")
        End If
        Me.TopMost = True
        Me.BringToFront()
        Me.TopMost = False
        Me.LanguageDropDown.SelectedIndex = 0
        If (Me.BrowseButton.Visible = True) Then
            AllowedToClose = False
        End If
    End Sub

    Private Function GetLanguageExtFromDropDown() As String
        Dim Item As String = Me.LanguageDropDown.SelectedItem.ToString()
        Dim StartOfExt As String = Item.Substring(Item.LastIndexOf("."c))
        Return StartOfExt.Replace(")", "")
    End Function

    Private Sub SetupProjectSelected()
        If (ProjectSelectListView.SelectedItems.Count = 0) Then Return 'no null references

        If (ProjectSelectListView.SelectedItems.Item(0).Text = ProjectSelectListView.Items(0).Text) Then 'LargeProject
            Project.ProjectType = CurrentProjectData.ProjectTypes.LibraryProject
        ElseIf (ProjectSelectListView.SelectedItems.Item(0).Text = ProjectSelectListView.Items(1).Text) Then 'TypicalProject
            Project.ProjectType = CurrentProjectData.ProjectTypes.RecordAndGo
        Else
            System.Windows.Forms.MessageBox.Show("Project must be selected in order to continue.", _
                                                 SlickTestDev.MsgBoxTitle, MessageBoxButtons.OK, _
                                                 MessageBoxIcon.Warning)
            Return
        End If
        If (SaveFileDialog1.ShowDialog() = Windows.Forms.DialogResult.OK) Then
            Project.ProjectName = System.IO.Path.GetFileNameWithoutExtension(SaveFileDialog1.FileName)
            Project.OptionExplicit = My.Settings.Project_Default__Option__Explicit
            Project.OptionStrict = My.Settings.Project_Default__Option__Strict
            Project.TakePictureAfterClick = My.Settings.Project_Default__Take__Picture__After__Clicking
            Project.TakePictureBeforeClick = My.Settings.Project_Default__Take__Picture__Before__Clicking
            Project.TakePictureAfterTyping = My.Settings.Project_Default__Take__Picture__After__Typing
            Project.TakePictureBeforeTyping = My.Settings.Project_Default__Take__Picture__Before__Typing
            Project.ExternalReportDatabaseConnectionString = My.Settings.Project_Default__External__Report__Connection__String

            For Each DLL As String In My.Settings.Project_Default__DLL__Auto__Includes
                Project.AddAsm(DLL)
            Next
            Project.ShowUI = My.Settings.Project_Default__Show__UI
            'Project.ShowErrors = True
            Project.RuntimeTimeout = My.Settings.Project_Default__Runtime__Timeout
            Project.ExecuteFileName = Project.ProjectName & ".exe"
            Project.RuntimeClassName = My.Settings.Project_Default__ClassName
            Dim Directory As String = System.IO.Path.GetDirectoryName(SaveFileDialog1.FileName)
            If (Directory.EndsWith("\") = False) Then Directory += "\"
            Directory &= System.IO.Path.GetFileNameWithoutExtension(SaveFileDialog1.FileName)
            If (Directory.EndsWith("\") = False) Then Directory += "\"
            System.IO.Directory.CreateDirectory(Directory)
            Project.OutputFolder = Directory
            System.IO.Directory.CreateDirectory(Directory & "Source")
            Project.ClassName = My.Settings.Project_Default__ClassName
            Project.SourceFileLocation = Directory & "Source\"
            Project.SaveProject(Directory & System.IO.Path.GetFileNameWithoutExtension(SaveFileDialog1.FileName) & _
            SlickTestDev.ProjectFileExt)
            Project.AdditionalCompilerOptions = My.Settings.Project_Default__Additional__Compiler__Options
            Project.ProjectName = System.IO.Path.GetFileNameWithoutExtension(SaveFileDialog1.FileName)
            Project.LanguageExtension = GetLanguageExtFromDropDown()
        Else
            Return
        End If
        RaiseEvent ProjectSelect(Project, True)
        AllowedToClose = True
        Me.Close()
    End Sub

    Private Sub ProjectSelectListView_MouseDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles ProjectSelectListView.MouseDoubleClick
        SetupProjectSelected()
    End Sub

    Private Sub ProjectSelect_FormClosing(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        If (AllowedToClose = False) Then
            If (e.CloseReason = CloseReason.UserClosing) Then
                GC.Collect()
                If (SlickTestDev.CurrentlyOpenedFile <> "" AndAlso AllowedToClose = True) Then
                    System.Windows.Forms.MessageBox.Show("You must select a new project or open an existing project.", _
                    SlickTestDev.MsgBoxTitle, MessageBoxButtons.OK, _
                    MessageBoxIcon.Warning)
                Else
                    If (System.Windows.Forms.MessageBox.Show("You must select a new project or open an existing project." & _
                    "  Would you like to close Slick Test Developer?", _
                    SlickTestDev.MsgBoxTitle, MessageBoxButtons.YesNo, _
                    MessageBoxIcon.Warning) = Windows.Forms.DialogResult.Yes) Then
                        AllowedToClose = True
                        SlickTestDev.CloseOnStart = True
                        Return
                    End If
                End If
            End If
            e.Cancel = True
        End If
    End Sub

    Private Sub BrowseButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BrowseButton.Click
        If (Me.OpenFileDialog1.ShowDialog() = Windows.Forms.DialogResult.OK) Then
            If (System.IO.File.Exists(Me.OpenFileDialog1.FileName) = True) Then
                If (Me.OpenFileDialog1.FileName.ToUpper().EndsWith(SlickTestDev.ProjectFileExt.ToUpper()) = True) Then
                    If (Project.SafeLoadProjectWithMessages(Me.OpenFileDialog1.FileName) = True) Then
                        RaiseEvent ProjectSelect(Project, False)
                        AllowedToClose = True
                        Me.Close()
                    Else
                        Return
                    End If
                Else
                    System.Windows.Forms.MessageBox.Show("File does not appear to be a project file.  " & _
                    "Extension must be '.stp'.  File attempted to be loaded: " & _
                    Me.OpenFileDialog1.FileName & ".", SlickTestDev.MsgBoxTitle)
                End If
            Else
                System.Windows.Forms.MessageBox.Show("File does not appear to exist (" & _
                                                     Me.OpenFileDialog1.FileName & ").", _
                                                     SlickTestDev.MsgBoxTitle)
            End If
        End If
    End Sub

    Public Sub New(ByVal std As SlickTestDev)
        SlickTestDev = std
        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        ' Add any initialization after the InitializeComponent() call.
    End Sub

    Private Sub OkButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OkButton.Click
        SetupProjectSelected()
    End Sub

End Class