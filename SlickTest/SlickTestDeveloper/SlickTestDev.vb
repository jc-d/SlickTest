'Slick Test Developer, Copyright (c) 2007-2010 Jeremy Carey-dressler
Imports VBEditor
Imports System.Threading
Imports System.Reflection
Imports TextEditor.TextEditorBox

Public Class SlickTestDev
    Inherits VBEditor.MainForm

#Region "Variables"
    Public Const MsgBoxTitle As String = "Slick Test"
    Public Const ChmHelp As String = ".\Guides\SlickTestAPI.chm"
    Public Const ProjectFileExt As String = ".stp"
    Private Const TitleText As String = MsgBoxTitle & " Developer"
    Public Shared IsFirstCompile As Boolean = True
    Private FormLoaded As Boolean = False 'used to store resize information.
    Public Shared ArgsFromUser As New System.Collections.Generic.List(Of String)()
#Region "File Handling"
    Private Files As New System.Collections.Generic.SortedDictionary(Of String, FileInfo)
    Private MD5ForCRC As New System.Security.Cryptography.MD5CryptoServiceProvider()
#End Region
#Region "Forms"
    Private FindWindowViaHwnd As FindMyHwnd
    Private WithEvents ObjSpy As ObjSpy
    Private options As OptionsForm
    Private VerifyDescription As VerifyDescription


#Region "Recorder"
    Private Keys As HandleInput.MouseAndKeys
    Friend WithEvents RecorderForm As Recorder

#End Region
#Region "Find/Replace/Goto"
    Private GotoLine As GotoLine
    Private Replace As frmReplace
    Private Find As frmFind
#End Region
#Region "Project Data/Changes to files"
    Friend WithEvents ProjectSelectionForm As ProjectSelect
    Private Project As CurrentProjectData
    Public CloseOnStart As Boolean = False
    Private IgnoredSingleTxtChange As Boolean = False 'used to keep some changes as registering as "dirty" changes to file or project.


    Public Property ProjectCurrentlySaved() As Boolean
        Get
            Return Me.TxtEdit.CurrentlySaved
        End Get
        Set(ByVal value As Boolean)
            Me.TxtEdit.CurrentlySaved = value
        End Set
    End Property
#End Region
#Region "Compile/Execute/Report"
    Private WithEvents RunReportForm As ResultsViewer.RunReport
    Private ReporterSize As Size = Size.Empty
    Private ReporterWindowState As System.Windows.Forms.FormWindowState = FormWindowState.Normal

    Private ExecutionCompleted As Boolean = False
    Friend WithEvents vbc As DOTNETCompiler.AbstractCompiler
    Private PIDForExecutable As Integer
    Private Report As New UIControls.Report(System.Guid.Empty, "", "", False, False)
#End Region

#End Region
#Region "Language Data"
    Private Const DefaultFileNameNoExt As String = "file"
    'Private Const VBLANGUAGEEXT As String = ".vb"
    'Private Const CSHARPLANGUAGEEXT As String = ".cs"

    Public Property LanguageExt() As String
        Get
            If (Project Is Nothing) Then
                Return InternalLanguageExt
            End If
            Return Project.LanguageExtension
        End Get
        Set(ByVal value As String)
            InternalLanguageExt = value
        End Set
    End Property

    Public Shared ReadOnly Property CurrentLanguage() As CodeTranslator.Languages
        Get
            If (InternalLanguageExt = CSHARPLANGUAGEEXT) Then
                Return CodeTranslator.Languages.CSharp
            Else
                Return CodeTranslator.Languages.VBNet
            End If
        End Get
    End Property

    Private Shared InternalLanguageExt As String = VBLANGUAGEEXT
    Public ReadOnly Property DefaultFileName() As String
        Get
            Return DefaultFileNameNoExt & LanguageExt
        End Get
    End Property

    Private InternalCurrentlyOpenedFile As String = ""
    Public Property CurrentlyOpenedFile() As String
        Get
            If (String.IsNullOrEmpty(InternalCurrentlyOpenedFile)) Then
                Return DefaultFileName
            End If
            Return InternalCurrentlyOpenedFile
        End Get
        Set(ByVal value As String)
            InternalCurrentlyOpenedFile = value
        End Set
    End Property
#End Region
    Friend WithEvents MenuStrip1 As System.Windows.Forms.MenuStrip
    Friend WithEvents SaveFileDialog1 As System.Windows.Forms.SaveFileDialog
    Friend WithEvents FileToolStripMenuItem1 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents NewToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents OpenToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents toolStripSeparator As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents SaveToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents SaveAsToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents toolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents PrintToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents PrintPreviewToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents toolStripSeparator2 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents ExitToolStripMenuItem1 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents EditToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents UndoToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents RedoToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents toolStripSeparator3 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents CutToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents CopyToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents PasteToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents toolStripSeparator4 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents SelectAllToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolsToolStripMenuItem1 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents TakeScreenshotToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ObjectSpyToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents HelpToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ContentsToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents IndexToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents SearchToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents toolStripSeparator5 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents AboutToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents RunToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ScreenShotToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents StopToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents RecordingToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents OpenFileDialog1 As System.Windows.Forms.OpenFileDialog
    Friend WithEvents SpyToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ExportForExternalUseToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem

    Friend WithEvents AddAsmTimer As System.Windows.Forms.Timer
    Friend WithEvents ProjectToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents FileToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents CompilerIssueDataGridView As System.Windows.Forms.DataGridView
    Friend WithEvents ContextMenuCopyOnly As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents CopyMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ContextMenuAddFiles As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents TextEditorEvents As TextEditor.TextEditorBox
    'These are for the treeview
    Friend WithEvents OpenFileToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents RenameFileToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents RemoveFileToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents SaveProjectToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents BuildToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ExecuteToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents CompileToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents StartRecordingToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents OpenProjectToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents FileToolStripMenuItem2 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator8 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents FindToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ReplaceToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents GotoToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents SaveAllToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator9 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents SaveProjectAsToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents CodeHelpToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents IfToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents IfElseToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents DoWhileToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ForLoopToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents WhileLoopToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents TryCatchToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ClassToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents FunctionToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents SubToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator11 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents CommentSelectedLinesToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents UncommentSelectedLinesToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator12 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents CommentSelectedLinesToolStripMenuItem1 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents UncommentSelectedLinesToolStripMenuItem1 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents Splitter1 As System.Windows.Forms.Splitter
    Friend WithEvents SplitViewContainer As System.Windows.Forms.SplitContainer
    Friend WithEvents FileTreeView As System.Windows.Forms.TreeView
    Friend WithEvents CleanCompileToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ResultsTimer As System.Windows.Forms.Timer
    Friend WithEvents ExternalRunToolStripMenuItem As System.Windows.Forms.ToolStripComboBox
    Friend WithEvents ShowTestReportsToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator13 As System.Windows.Forms.ToolStripSeparator
    'Friend WithEvents CorrectTabsToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem1 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents SplitViewToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ProjectSettingsToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents OptionsToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator10 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents ToUpperSelectedTextToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToLowerSelectedTextToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents SaveButtonToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents APIToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents UsingSlickTestToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents FindWindowViaHwndToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator7 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents ToolStripSeparator6 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents ToolStripSeparator14 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents ToolStripSeparator16 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents ToolStripSeparator15 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents SaveFileDialogForProject As System.Windows.Forms.SaveFileDialog
    Friend WithEvents CurrentFileWatcher As System.IO.FileSystemWatcher
    Friend WithEvents ResetCacheToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator17 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents ArgumentTimer As System.Windows.Forms.Timer
    Friend WithEvents UserGuideToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents DescriptionTesterToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents AddFileToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
#End Region

#Region " Windows Form Designer generated code "

    Public Sub New()
        MyBase.New()
        'System.Threading.Thread.CurrentThread.Name = "Public Sub New()"

        'AddHandler Application.ThreadException, AddressOf Application_ThreadException
        PIDForExecutable = 0
        'This call is required by the Windows Form Designer.
        InitializeComponent()
        If (My.Settings.ShouldUpdateSettings = True) Then
            My.Settings.Upgrade()
            My.Settings.ShouldUpdateSettings = False
            Dim tmpcollection As New System.Collections.Specialized.StringCollection
            For Each item As String In My.Settings.Project_Default__DLL__Auto__Includes
                tmpcollection.Add(item)
            Next

            For Each line As String In tmpcollection
                If (line.ToLower.Contains("interaction.dll") = True) Then
                    My.Settings.Project_Default__DLL__Auto__Includes.Remove(line)
                    Dim f As String = System.IO.Path.GetDirectoryName(Application.ExecutablePath)
                    If (f.EndsWith("\") = False) Then f += "\"
                    f += "InterAction.dll"
                    f = "InterAction;" + f
                    My.Settings.Project_Default__DLL__Auto__Includes.Add(f)
                End If
            Next
        End If 'end autoupdate
        CurrentProjectData.DefAsms = New System.Collections.Generic.List(Of String)()
        Try
            For Each asm As String In My.Settings.Project_Default__DLL__Auto__Includes
                If (asm IsNot Nothing) Then CurrentProjectData.DefAsms.Add(asm)
            Next
        Catch ex As Exception
        End Try
        Project = New CurrentProjectData()
        'Lame way to add events for TxtEdit control
        AddHandler TxtEdit.ActiveTextAreaControl.TextArea.KeyDown, AddressOf TxtEdit_KeyDown
        AddHandler TxtEdit.ActiveTextAreaControl.TextArea.Caret.PositionChanged, AddressOf Caret_PositionChanged
        'AddHandler TxtEdit.ActiveTextAreaControl.TextArea.KeyPress, AddressOf TxtEdit_KeyPressed
        AddHandler TxtEdit.Document.DocumentChanged, AddressOf TxtEdit_DocChanged

        Dim ProjectLibInit As String = Application.StartupPath.ToLower()
        If (ProjectLibInit.EndsWith("\") = False) Then ProjectLibInit += "\"
        ProjectLibInit += "Library\"
        If (My.Settings.Project_Library__Location = "") Then
            My.Settings.Project_Library__Location = ProjectLibInit
        Else
            If (System.IO.Directory.Exists(My.Settings.Project_Library__Location) = False) Then
                If (System.IO.Directory.Exists(ProjectLibInit)) Then
                    If (System.Windows.Forms.MessageBox.Show("'" & My.Settings.Project_Library__Location & "' was not found. " & _
                                                         "Would you like the Special Library folder path set to '" & _
                                                         ProjectLibInit & "'?", MsgBoxTitle, MessageBoxButtons.YesNo) = _
                                                    Windows.Forms.DialogResult.Yes) Then
                        My.Settings.Project_Library__Location = ProjectLibInit
                    Else
                        System.Windows.Forms.MessageBox.Show("The Special Library folder '" & My.Settings.Project_Library__Location & _
                                                             "' does not exist.  Special Libraries will not work correctly.", MsgBoxTitle, _
                                                             MessageBoxButtons.OK, MessageBoxIcon.Error)
                    End If
                End If
            End If
        End If


        If (CurrentLanguage = CodeTranslator.Languages.VBNet) Then
            vbc = New DOTNETCompiler.VBCompiler()
        Else
            vbc = New DOTNETCompiler.CSharpCompiler()
        End If

        ObjSpy = Nothing
        Me.Size = My.Settings.FormSize
        Me.Location = My.Settings.FormLocation
        Me.WindowState = My.Settings.FormState
        'startup text
        IgnoredSingleTxtChange = True
        Me.TxtEdit.Text = ""
        Keys = New HandleInput.MouseAndKeys()
        RecorderForm = New Recorder(Keys)
        'Add any initialization after the InitializeComponent() call
        FormLoaded = True

        Dim path As String = System.IO.Path.GetDirectoryName(Application.ExecutablePath)
        If (path.EndsWith("\") = False) Then path += "\"


        'AddAsmTimer.Enabled = True
        'AddAsmTimer.Start()
        UpdateUISettings()
        'LoadProject(True)
        If (My.Settings.Project_Default__Main__File__Template.StartsWith("\") = True) Then
            My.Settings.Project_Default__Main__File__Template = path.Substring(0, path.Length - 1) & My.Settings.Project_Default__Main__File__Template
        End If
        If (My.Settings.Project_Default__New__File__Template.StartsWith("\") = True) Then
            My.Settings.Project_Default__New__File__Template = path.Substring(0, path.Length - 1) & My.Settings.Project_Default__New__File__Template
        End If
    End Sub

    Private Sub SlickTestDev_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        ArgumentTimer.Start()
        UIControls.InterAct.RunningTimer.Stop()
        Me.CurrentFileWatcher.NotifyFilter = IO.NotifyFilters.LastWrite Or IO.NotifyFilters.CreationTime
        Me.Text = TitleText
        If (My.Settings.LastProject <> "") Then
            If (System.IO.File.Exists(My.Settings.LastProject) = False) Then
                My.Settings.LastProject = ""
            End If
        End If
        If (My.Settings.LastProject = "" OrElse My.Settings.Project_Default__Load__Last__Project = False) Then
            ProjectSelectionForm = New ProjectSelect(Me)
            ProjectSelectionForm.ShowDialog(Me)
            If (CloseOnStart = False) Then
                GC.Collect()
                LoadProject(True)
                Me.BringToFront()
            Else
                Me.Close()
                Return
            End If
        ElseIf (My.Settings.Project_Default__Load__Last__Project = True) Then
            'Auto-load project
            GC.Collect()
            Project = New CurrentProjectData()
            If (Project.SafeLoadProjectWithMessages(My.Settings.LastProject) = True) Then
                LoadProject(False)
                Me.BringToFront()
            Else
                ProjectSelectionForm = New ProjectSelect(Me)
                ProjectSelectionForm.ShowDialog(Me)
                If (CloseOnStart = False) Then
                    GC.Collect()
                    LoadProject(True)
                    Me.BringToFront()
                Else
                    Me.Close()
                    Return
                End If
            End If
        End If
    End Sub

    Private Sub TxtEdit_DocChanged(ByVal sender As Object, ByVal e As ICSharpCode.TextEditor.Document.DocumentEventArgs)
        If (IgnoredSingleTxtChange = True) Then
            IgnoredSingleTxtChange = False
        Else
            Me.UpdateTitle(True)
            Me.ProjectCurrentlySaved = False
            If (Me.Files.ContainsKey(Me.ConvertFilesKey(CurrentlyOpenedFile))) Then
                Me.Files(Me.ConvertFilesKey(CurrentlyOpenedFile)).IsSaved = False
            End If
        End If
    End Sub

    Private Sub AddDLLs()
        For Each item As String In Project.Assemblies
            If (item.Contains(";") = True) Then
                If (System.IO.File.Exists(item.Split(";"c)(1)) = True) Then
                    If (AddedAsms.Contains(item) = False) Then
                        AddedAsms.Add(item)
                    End If
                End If
            Else
                If (AddedAsms.Contains(item) = False) Then
                    AddedAsms.Add(item)
                End If
            End If
        Next
        Dim tmp As String = Project.OutputFolder
        If (tmp.EndsWith("\") = False) Then
            tmp += "\"
        End If
        For Each item As String In Project.UserAssemblies
            item = tmp & System.IO.Path.GetFileName(item)
            If (item.Contains(";") = True) Then
                If (System.IO.File.Exists(item.Split(";"c)(1)) = True) Then
                    If (AddedAsms.Contains(item) = False) Then
                        AddedAsms.Add(item)
                    End If
                End If
            Else
                If (System.IO.File.Exists(item)) Then
                    If (AddedAsms.Contains(item) = False) Then
                        AddedAsms.Add(item)
                    End If
                End If
            End If
        Next
        Dim path As String = System.IO.Path.GetDirectoryName(Application.ExecutablePath)
        If (path.EndsWith("\") = False) Then path += "\"
        Dim asm As String = path & "InterAction.dll"
        If (AddedAsms.Contains(asm) = False) Then
            AddedAsms.Add(asm)
        End If
        asm = path & "APIControls.dll"
        If (AddedAsms.Contains(asm) = False) Then
            AddedAsms.Add(asm)
        End If

    End Sub

    Private Sub Caret_PositionChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        UserInformationBar.Text = "Location: " & (TxtEdit.ActiveTextAreaControl.Caret.Position.X + 1).ToString() & _
       ", " & (TxtEdit.ActiveTextAreaControl.Caret.Position.Y + 1).ToString()
    End Sub

    'Private Sub Application_ThreadException(ByVal sender As Object, ByVal e As System.Threading.ThreadExceptionEventArgs)
    '    System.Windows.Forms.MessageBox.Show("Sender: " & sender.ToString() & " Exception: " & e.ToString())
    'End Sub
    'Form overrides dispose to clean up the component list.
    Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing Then
            If Not (components Is Nothing) Then
                components.Dispose()
            End If
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    '<System.Diagnostics.DebuggerStepThrough()> Sub InitializeComponent()
    Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(SlickTestDev))
        Dim TreeNode1 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("Files")
        Dim TreeNode2 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("DLL")
        Dim TreeNode3 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("User Libraries")
        Me.MenuStrip1 = New System.Windows.Forms.MenuStrip
        Me.FileToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem
        Me.NewToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.ProjectToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.FileToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.OpenToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.OpenProjectToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.FileToolStripMenuItem2 = New System.Windows.Forms.ToolStripMenuItem
        Me.toolStripSeparator = New System.Windows.Forms.ToolStripSeparator
        Me.SaveToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.SaveAsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.SaveAllToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripSeparator9 = New System.Windows.Forms.ToolStripSeparator
        Me.SaveProjectToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.SaveProjectAsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.ExportForExternalUseToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.toolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator
        Me.PrintToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.PrintPreviewToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.toolStripSeparator2 = New System.Windows.Forms.ToolStripSeparator
        Me.ExitToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem
        Me.EditToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.UndoToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.RedoToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.toolStripSeparator3 = New System.Windows.Forms.ToolStripSeparator
        Me.CutToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.CopyToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.PasteToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.toolStripSeparator4 = New System.Windows.Forms.ToolStripSeparator
        Me.SelectAllToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripSeparator8 = New System.Windows.Forms.ToolStripSeparator
        Me.FindToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.ReplaceToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.GotoToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripSeparator11 = New System.Windows.Forms.ToolStripSeparator
        Me.CommentSelectedLinesToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.UncommentSelectedLinesToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripSeparator13 = New System.Windows.Forms.ToolStripSeparator
        Me.ToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem
        Me.OptionsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.ProjectSettingsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripSeparator7 = New System.Windows.Forms.ToolStripSeparator
        Me.SplitViewToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripSeparator6 = New System.Windows.Forms.ToolStripSeparator
        Me.ShowTestReportsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.BuildToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.ExecuteToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.CompileToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripSeparator14 = New System.Windows.Forms.ToolStripSeparator
        Me.CleanCompileToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolsToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem
        Me.CodeHelpToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.IfToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.IfElseToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.DoWhileToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.ForLoopToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.WhileLoopToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.TryCatchToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.ClassToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.FunctionToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.SubToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripSeparator16 = New System.Windows.Forms.ToolStripSeparator
        Me.StartRecordingToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripSeparator15 = New System.Windows.Forms.ToolStripSeparator
        Me.TakeScreenshotToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.ObjectSpyToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.FindWindowViaHwndToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.DescriptionTesterToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.HelpToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.APIToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.ContentsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.IndexToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.SearchToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.UserGuideToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.UsingSlickTestToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.toolStripSeparator5 = New System.Windows.Forms.ToolStripSeparator
        Me.ResetCacheToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripSeparator17 = New System.Windows.Forms.ToolStripSeparator
        Me.AboutToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.SaveButtonToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.RunToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.StopToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.ScreenShotToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.SpyToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.RecordingToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.ExternalRunToolStripMenuItem = New System.Windows.Forms.ToolStripComboBox
        Me.SaveFileDialog1 = New System.Windows.Forms.SaveFileDialog
        Me.ToolStripSeparator12 = New System.Windows.Forms.ToolStripSeparator
        Me.OpenFileDialog1 = New System.Windows.Forms.OpenFileDialog
        Me.AddAsmTimer = New System.Windows.Forms.Timer(Me.components)
        Me.CompilerIssueDataGridView = New System.Windows.Forms.DataGridView
        Me.ContextMenuCopyOnly = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.CopyMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.ContextMenuAddFiles = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.OpenFileToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.RenameFileToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.RemoveFileToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.AddFileToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.Splitter1 = New System.Windows.Forms.Splitter
        Me.SplitViewContainer = New System.Windows.Forms.SplitContainer
        Me.FileTreeView = New System.Windows.Forms.TreeView
        Me.ResultsTimer = New System.Windows.Forms.Timer(Me.components)
        Me.SaveFileDialogForProject = New System.Windows.Forms.SaveFileDialog
        Me.CurrentFileWatcher = New System.IO.FileSystemWatcher
        Me.ArgumentTimer = New System.Windows.Forms.Timer(Me.components)
        Me.MenuStrip1.SuspendLayout()
        CType(Me.CompilerIssueDataGridView, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ContextMenuCopyOnly.SuspendLayout()
        Me.SplitViewContainer.Panel1.SuspendLayout()
        Me.SplitViewContainer.Panel2.SuspendLayout()
        Me.SplitViewContainer.SuspendLayout()
        CType(Me.CurrentFileWatcher, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'TxtEdit
        '
        Me.TxtEdit.Margin = New System.Windows.Forms.Padding(4)
        Me.TxtEdit.Size = New System.Drawing.Size(698, 406)
        '
        'MenuStrip1
        '
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.FileToolStripMenuItem1, Me.EditToolStripMenuItem, Me.ToolStripMenuItem1, Me.BuildToolStripMenuItem, Me.ToolsToolStripMenuItem1, Me.HelpToolStripMenuItem, Me.SaveButtonToolStripMenuItem, Me.RunToolStripMenuItem, Me.StopToolStripMenuItem, Me.ScreenShotToolStripMenuItem, Me.SpyToolStripMenuItem, Me.RecordingToolStripMenuItem, Me.ExternalRunToolStripMenuItem})
        Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.Size = New System.Drawing.Size(828, 25)
        Me.MenuStrip1.TabIndex = 10
        Me.MenuStrip1.Text = "MenuStrip1"
        '
        'FileToolStripMenuItem1
        '
        Me.FileToolStripMenuItem1.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.NewToolStripMenuItem, Me.OpenToolStripMenuItem, Me.toolStripSeparator, Me.SaveToolStripMenuItem, Me.SaveAsToolStripMenuItem, Me.SaveAllToolStripMenuItem, Me.ToolStripSeparator9, Me.SaveProjectToolStripMenuItem, Me.SaveProjectAsToolStripMenuItem, Me.ExportForExternalUseToolStripMenuItem, Me.toolStripSeparator1, Me.PrintToolStripMenuItem, Me.PrintPreviewToolStripMenuItem, Me.toolStripSeparator2, Me.ExitToolStripMenuItem1})
        Me.FileToolStripMenuItem1.Name = "FileToolStripMenuItem1"
        Me.FileToolStripMenuItem1.Size = New System.Drawing.Size(35, 21)
        Me.FileToolStripMenuItem1.Text = "&File"
        '
        'NewToolStripMenuItem
        '
        Me.NewToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ProjectToolStripMenuItem, Me.FileToolStripMenuItem})
        Me.NewToolStripMenuItem.Image = CType(resources.GetObject("NewToolStripMenuItem.Image"), System.Drawing.Image)
        Me.NewToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.NewToolStripMenuItem.Name = "NewToolStripMenuItem"
        Me.NewToolStripMenuItem.Size = New System.Drawing.Size(189, 22)
        Me.NewToolStripMenuItem.Text = "&New"
        '
        'ProjectToolStripMenuItem
        '
        Me.ProjectToolStripMenuItem.Name = "ProjectToolStripMenuItem"
        Me.ProjectToolStripMenuItem.Size = New System.Drawing.Size(108, 22)
        Me.ProjectToolStripMenuItem.Text = "Project"
        '
        'FileToolStripMenuItem
        '
        Me.FileToolStripMenuItem.Name = "FileToolStripMenuItem"
        Me.FileToolStripMenuItem.Size = New System.Drawing.Size(108, 22)
        Me.FileToolStripMenuItem.Text = "File"
        '
        'OpenToolStripMenuItem
        '
        Me.OpenToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.OpenProjectToolStripMenuItem, Me.FileToolStripMenuItem2})
        Me.OpenToolStripMenuItem.Image = CType(resources.GetObject("OpenToolStripMenuItem.Image"), System.Drawing.Image)
        Me.OpenToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.OpenToolStripMenuItem.Name = "OpenToolStripMenuItem"
        Me.OpenToolStripMenuItem.Size = New System.Drawing.Size(189, 22)
        Me.OpenToolStripMenuItem.Text = "&Open"
        '
        'OpenProjectToolStripMenuItem
        '
        Me.OpenProjectToolStripMenuItem.Name = "OpenProjectToolStripMenuItem"
        Me.OpenProjectToolStripMenuItem.Size = New System.Drawing.Size(108, 22)
        Me.OpenProjectToolStripMenuItem.Text = "Project"
        '
        'FileToolStripMenuItem2
        '
        Me.FileToolStripMenuItem2.Name = "FileToolStripMenuItem2"
        Me.FileToolStripMenuItem2.Size = New System.Drawing.Size(108, 22)
        Me.FileToolStripMenuItem2.Text = "File"
        '
        'toolStripSeparator
        '
        Me.toolStripSeparator.Name = "toolStripSeparator"
        Me.toolStripSeparator.Size = New System.Drawing.Size(186, 6)
        '
        'SaveToolStripMenuItem
        '
        Me.SaveToolStripMenuItem.Image = CType(resources.GetObject("SaveToolStripMenuItem.Image"), System.Drawing.Image)
        Me.SaveToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.SaveToolStripMenuItem.Name = "SaveToolStripMenuItem"
        Me.SaveToolStripMenuItem.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.S), System.Windows.Forms.Keys)
        Me.SaveToolStripMenuItem.Size = New System.Drawing.Size(189, 22)
        Me.SaveToolStripMenuItem.Text = "&Save"
        '
        'SaveAsToolStripMenuItem
        '
        Me.SaveAsToolStripMenuItem.Image = Global.SlickTestDeveloper.My.Resources.Resources._active__Save_As
        Me.SaveAsToolStripMenuItem.Name = "SaveAsToolStripMenuItem"
        Me.SaveAsToolStripMenuItem.Size = New System.Drawing.Size(189, 22)
        Me.SaveAsToolStripMenuItem.Text = "Save &As"
        '
        'SaveAllToolStripMenuItem
        '
        Me.SaveAllToolStripMenuItem.Image = Global.SlickTestDeveloper.My.Resources.Resources._active__Save_All
        Me.SaveAllToolStripMenuItem.Name = "SaveAllToolStripMenuItem"
        Me.SaveAllToolStripMenuItem.Size = New System.Drawing.Size(189, 22)
        Me.SaveAllToolStripMenuItem.Text = "Save All"
        '
        'ToolStripSeparator9
        '
        Me.ToolStripSeparator9.Name = "ToolStripSeparator9"
        Me.ToolStripSeparator9.Size = New System.Drawing.Size(186, 6)
        '
        'SaveProjectToolStripMenuItem
        '
        Me.SaveProjectToolStripMenuItem.Image = Global.SlickTestDeveloper.My.Resources.Resources._active__Save
        Me.SaveProjectToolStripMenuItem.Name = "SaveProjectToolStripMenuItem"
        Me.SaveProjectToolStripMenuItem.Size = New System.Drawing.Size(189, 22)
        Me.SaveProjectToolStripMenuItem.Text = "Save Pro&ject"
        '
        'SaveProjectAsToolStripMenuItem
        '
        Me.SaveProjectAsToolStripMenuItem.Image = Global.SlickTestDeveloper.My.Resources.Resources.Save_As
        Me.SaveProjectAsToolStripMenuItem.Name = "SaveProjectAsToolStripMenuItem"
        Me.SaveProjectAsToolStripMenuItem.Size = New System.Drawing.Size(189, 22)
        Me.SaveProjectAsToolStripMenuItem.Text = "Save Project As"
        '
        'ExportForExternalUseToolStripMenuItem
        '
        Me.ExportForExternalUseToolStripMenuItem.Name = "ExportForExternalUseToolStripMenuItem"
        Me.ExportForExternalUseToolStripMenuItem.Size = New System.Drawing.Size(189, 22)
        Me.ExportForExternalUseToolStripMenuItem.Text = "Export For External Use"
        Me.ExportForExternalUseToolStripMenuItem.ToolTipText = "If you wish to run the test without the GUI, you must export the test."
        '
        'toolStripSeparator1
        '
        Me.toolStripSeparator1.Name = "toolStripSeparator1"
        Me.toolStripSeparator1.Size = New System.Drawing.Size(186, 6)
        '
        'PrintToolStripMenuItem
        '
        Me.PrintToolStripMenuItem.Image = CType(resources.GetObject("PrintToolStripMenuItem.Image"), System.Drawing.Image)
        Me.PrintToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.PrintToolStripMenuItem.Name = "PrintToolStripMenuItem"
        Me.PrintToolStripMenuItem.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.P), System.Windows.Forms.Keys)
        Me.PrintToolStripMenuItem.Size = New System.Drawing.Size(189, 22)
        Me.PrintToolStripMenuItem.Text = "&Print"
        '
        'PrintPreviewToolStripMenuItem
        '
        Me.PrintPreviewToolStripMenuItem.Image = CType(resources.GetObject("PrintPreviewToolStripMenuItem.Image"), System.Drawing.Image)
        Me.PrintPreviewToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.PrintPreviewToolStripMenuItem.Name = "PrintPreviewToolStripMenuItem"
        Me.PrintPreviewToolStripMenuItem.Size = New System.Drawing.Size(189, 22)
        Me.PrintPreviewToolStripMenuItem.Text = "Print Pre&view"
        '
        'toolStripSeparator2
        '
        Me.toolStripSeparator2.Name = "toolStripSeparator2"
        Me.toolStripSeparator2.Size = New System.Drawing.Size(186, 6)
        '
        'ExitToolStripMenuItem1
        '
        Me.ExitToolStripMenuItem1.Image = Global.SlickTestDeveloper.My.Resources.Resources._Exit
        Me.ExitToolStripMenuItem1.Name = "ExitToolStripMenuItem1"
        Me.ExitToolStripMenuItem1.Size = New System.Drawing.Size(189, 22)
        Me.ExitToolStripMenuItem1.Text = "E&xit"
        '
        'EditToolStripMenuItem
        '
        Me.EditToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.UndoToolStripMenuItem, Me.RedoToolStripMenuItem, Me.toolStripSeparator3, Me.CutToolStripMenuItem, Me.CopyToolStripMenuItem, Me.PasteToolStripMenuItem, Me.toolStripSeparator4, Me.SelectAllToolStripMenuItem, Me.ToolStripSeparator8, Me.FindToolStripMenuItem, Me.ReplaceToolStripMenuItem, Me.GotoToolStripMenuItem, Me.ToolStripSeparator11, Me.CommentSelectedLinesToolStripMenuItem, Me.UncommentSelectedLinesToolStripMenuItem, Me.ToolStripSeparator13})
        Me.EditToolStripMenuItem.Name = "EditToolStripMenuItem"
        Me.EditToolStripMenuItem.Size = New System.Drawing.Size(37, 21)
        Me.EditToolStripMenuItem.Text = "&Edit"
        '
        'UndoToolStripMenuItem
        '
        Me.UndoToolStripMenuItem.Name = "UndoToolStripMenuItem"
        Me.UndoToolStripMenuItem.Size = New System.Drawing.Size(201, 22)
        Me.UndoToolStripMenuItem.Text = "&Undo"
        '
        'RedoToolStripMenuItem
        '
        Me.RedoToolStripMenuItem.Name = "RedoToolStripMenuItem"
        Me.RedoToolStripMenuItem.Size = New System.Drawing.Size(201, 22)
        Me.RedoToolStripMenuItem.Text = "&Redo"
        '
        'toolStripSeparator3
        '
        Me.toolStripSeparator3.Name = "toolStripSeparator3"
        Me.toolStripSeparator3.Size = New System.Drawing.Size(198, 6)
        '
        'CutToolStripMenuItem
        '
        Me.CutToolStripMenuItem.Image = CType(resources.GetObject("CutToolStripMenuItem.Image"), System.Drawing.Image)
        Me.CutToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.CutToolStripMenuItem.Name = "CutToolStripMenuItem"
        Me.CutToolStripMenuItem.Size = New System.Drawing.Size(201, 22)
        Me.CutToolStripMenuItem.Text = "Cu&t"
        '
        'CopyToolStripMenuItem
        '
        Me.CopyToolStripMenuItem.Image = CType(resources.GetObject("CopyToolStripMenuItem.Image"), System.Drawing.Image)
        Me.CopyToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.CopyToolStripMenuItem.Name = "CopyToolStripMenuItem"
        Me.CopyToolStripMenuItem.Size = New System.Drawing.Size(201, 22)
        Me.CopyToolStripMenuItem.Text = "&Copy"
        '
        'PasteToolStripMenuItem
        '
        Me.PasteToolStripMenuItem.Image = CType(resources.GetObject("PasteToolStripMenuItem.Image"), System.Drawing.Image)
        Me.PasteToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.PasteToolStripMenuItem.Name = "PasteToolStripMenuItem"
        Me.PasteToolStripMenuItem.Size = New System.Drawing.Size(201, 22)
        Me.PasteToolStripMenuItem.Text = "&Paste"
        '
        'toolStripSeparator4
        '
        Me.toolStripSeparator4.Name = "toolStripSeparator4"
        Me.toolStripSeparator4.Size = New System.Drawing.Size(198, 6)
        '
        'SelectAllToolStripMenuItem
        '
        Me.SelectAllToolStripMenuItem.Name = "SelectAllToolStripMenuItem"
        Me.SelectAllToolStripMenuItem.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.A), System.Windows.Forms.Keys)
        Me.SelectAllToolStripMenuItem.Size = New System.Drawing.Size(201, 22)
        Me.SelectAllToolStripMenuItem.Text = "Select &All"
        '
        'ToolStripSeparator8
        '
        Me.ToolStripSeparator8.Name = "ToolStripSeparator8"
        Me.ToolStripSeparator8.Size = New System.Drawing.Size(198, 6)
        '
        'FindToolStripMenuItem
        '
        Me.FindToolStripMenuItem.Image = Global.SlickTestDeveloper.My.Resources.Resources.Preview
        Me.FindToolStripMenuItem.Name = "FindToolStripMenuItem"
        Me.FindToolStripMenuItem.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.F), System.Windows.Forms.Keys)
        Me.FindToolStripMenuItem.Size = New System.Drawing.Size(201, 22)
        Me.FindToolStripMenuItem.Text = "&Find"
        '
        'ReplaceToolStripMenuItem
        '
        Me.ReplaceToolStripMenuItem.Name = "ReplaceToolStripMenuItem"
        Me.ReplaceToolStripMenuItem.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.H), System.Windows.Forms.Keys)
        Me.ReplaceToolStripMenuItem.Size = New System.Drawing.Size(201, 22)
        Me.ReplaceToolStripMenuItem.Text = "Replace"
        '
        'GotoToolStripMenuItem
        '
        Me.GotoToolStripMenuItem.Name = "GotoToolStripMenuItem"
        Me.GotoToolStripMenuItem.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.G), System.Windows.Forms.Keys)
        Me.GotoToolStripMenuItem.Size = New System.Drawing.Size(201, 22)
        Me.GotoToolStripMenuItem.Text = "&Goto..."
        '
        'ToolStripSeparator11
        '
        Me.ToolStripSeparator11.Name = "ToolStripSeparator11"
        Me.ToolStripSeparator11.Size = New System.Drawing.Size(198, 6)
        '
        'CommentSelectedLinesToolStripMenuItem
        '
        Me.CommentSelectedLinesToolStripMenuItem.Name = "CommentSelectedLinesToolStripMenuItem"
        Me.CommentSelectedLinesToolStripMenuItem.Size = New System.Drawing.Size(201, 22)
        Me.CommentSelectedLinesToolStripMenuItem.Text = "Comment Selected Lines"
        '
        'UncommentSelectedLinesToolStripMenuItem
        '
        Me.UncommentSelectedLinesToolStripMenuItem.Name = "UncommentSelectedLinesToolStripMenuItem"
        Me.UncommentSelectedLinesToolStripMenuItem.Size = New System.Drawing.Size(201, 22)
        Me.UncommentSelectedLinesToolStripMenuItem.Text = "Uncomment Selected Lines"
        '
        'ToolStripSeparator13
        '
        Me.ToolStripSeparator13.Name = "ToolStripSeparator13"
        Me.ToolStripSeparator13.Size = New System.Drawing.Size(198, 6)
        '
        'ToolStripMenuItem1
        '
        Me.ToolStripMenuItem1.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.OptionsToolStripMenuItem, Me.ProjectSettingsToolStripMenuItem, Me.ToolStripSeparator7, Me.SplitViewToolStripMenuItem, Me.ToolStripSeparator6, Me.ShowTestReportsToolStripMenuItem})
        Me.ToolStripMenuItem1.Name = "ToolStripMenuItem1"
        Me.ToolStripMenuItem1.Size = New System.Drawing.Size(41, 21)
        Me.ToolStripMenuItem1.Text = "&View"
        '
        'OptionsToolStripMenuItem
        '
        Me.OptionsToolStripMenuItem.Name = "OptionsToolStripMenuItem"
        Me.OptionsToolStripMenuItem.Size = New System.Drawing.Size(175, 22)
        Me.OptionsToolStripMenuItem.Text = "Defaults and &Options"
        '
        'ProjectSettingsToolStripMenuItem
        '
        Me.ProjectSettingsToolStripMenuItem.Name = "ProjectSettingsToolStripMenuItem"
        Me.ProjectSettingsToolStripMenuItem.Size = New System.Drawing.Size(175, 22)
        Me.ProjectSettingsToolStripMenuItem.Text = "Project Settings"
        '
        'ToolStripSeparator7
        '
        Me.ToolStripSeparator7.Name = "ToolStripSeparator7"
        Me.ToolStripSeparator7.Size = New System.Drawing.Size(172, 6)
        '
        'SplitViewToolStripMenuItem
        '
        Me.SplitViewToolStripMenuItem.Name = "SplitViewToolStripMenuItem"
        Me.SplitViewToolStripMenuItem.Size = New System.Drawing.Size(175, 22)
        Me.SplitViewToolStripMenuItem.Text = "Split View"
        '
        'ToolStripSeparator6
        '
        Me.ToolStripSeparator6.Name = "ToolStripSeparator6"
        Me.ToolStripSeparator6.Size = New System.Drawing.Size(172, 6)
        '
        'ShowTestReportsToolStripMenuItem
        '
        Me.ShowTestReportsToolStripMenuItem.Name = "ShowTestReportsToolStripMenuItem"
        Me.ShowTestReportsToolStripMenuItem.Size = New System.Drawing.Size(175, 22)
        Me.ShowTestReportsToolStripMenuItem.Text = "Show Test Reports"
        '
        'BuildToolStripMenuItem
        '
        Me.BuildToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ExecuteToolStripMenuItem, Me.CompileToolStripMenuItem, Me.ToolStripSeparator14, Me.CleanCompileToolStripMenuItem})
        Me.BuildToolStripMenuItem.Name = "BuildToolStripMenuItem"
        Me.BuildToolStripMenuItem.Size = New System.Drawing.Size(41, 21)
        Me.BuildToolStripMenuItem.Text = "&Build"
        '
        'ExecuteToolStripMenuItem
        '
        Me.ExecuteToolStripMenuItem.Name = "ExecuteToolStripMenuItem"
        Me.ExecuteToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F5
        Me.ExecuteToolStripMenuItem.Size = New System.Drawing.Size(185, 22)
        Me.ExecuteToolStripMenuItem.Text = "&Execute"
        '
        'CompileToolStripMenuItem
        '
        Me.CompileToolStripMenuItem.Name = "CompileToolStripMenuItem"
        Me.CompileToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F7
        Me.CompileToolStripMenuItem.Size = New System.Drawing.Size(185, 22)
        Me.CompileToolStripMenuItem.Text = "&Compile"
        '
        'ToolStripSeparator14
        '
        Me.ToolStripSeparator14.Name = "ToolStripSeparator14"
        Me.ToolStripSeparator14.Size = New System.Drawing.Size(182, 6)
        '
        'CleanCompileToolStripMenuItem
        '
        Me.CleanCompileToolStripMenuItem.Name = "CleanCompileToolStripMenuItem"
        Me.CleanCompileToolStripMenuItem.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.F7), System.Windows.Forms.Keys)
        Me.CleanCompileToolStripMenuItem.Size = New System.Drawing.Size(185, 22)
        Me.CleanCompileToolStripMenuItem.Text = "Clea&n Compile"
        '
        'ToolsToolStripMenuItem1
        '
        Me.ToolsToolStripMenuItem1.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.CodeHelpToolStripMenuItem, Me.ToolStripSeparator16, Me.StartRecordingToolStripMenuItem, Me.ToolStripSeparator15, Me.TakeScreenshotToolStripMenuItem, Me.ObjectSpyToolStripMenuItem, Me.FindWindowViaHwndToolStripMenuItem, Me.DescriptionTesterToolStripMenuItem})
        Me.ToolsToolStripMenuItem1.Name = "ToolsToolStripMenuItem1"
        Me.ToolsToolStripMenuItem1.Size = New System.Drawing.Size(44, 21)
        Me.ToolsToolStripMenuItem1.Text = "&Tools"
        '
        'CodeHelpToolStripMenuItem
        '
        Me.CodeHelpToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.IfToolStripMenuItem, Me.IfElseToolStripMenuItem, Me.DoWhileToolStripMenuItem, Me.ForLoopToolStripMenuItem, Me.WhileLoopToolStripMenuItem, Me.TryCatchToolStripMenuItem, Me.ClassToolStripMenuItem, Me.FunctionToolStripMenuItem, Me.SubToolStripMenuItem})
        Me.CodeHelpToolStripMenuItem.Name = "CodeHelpToolStripMenuItem"
        Me.CodeHelpToolStripMenuItem.Size = New System.Drawing.Size(182, 22)
        Me.CodeHelpToolStripMenuItem.Text = "&Code Help"
        '
        'IfToolStripMenuItem
        '
        Me.IfToolStripMenuItem.Name = "IfToolStripMenuItem"
        Me.IfToolStripMenuItem.Size = New System.Drawing.Size(152, 22)
        Me.IfToolStripMenuItem.Text = "If"
        '
        'IfElseToolStripMenuItem
        '
        Me.IfElseToolStripMenuItem.Name = "IfElseToolStripMenuItem"
        Me.IfElseToolStripMenuItem.Size = New System.Drawing.Size(152, 22)
        Me.IfElseToolStripMenuItem.Text = "&If-Else"
        '
        'DoWhileToolStripMenuItem
        '
        Me.DoWhileToolStripMenuItem.Name = "DoWhileToolStripMenuItem"
        Me.DoWhileToolStripMenuItem.Size = New System.Drawing.Size(152, 22)
        Me.DoWhileToolStripMenuItem.Text = "&Do Loop"
        '
        'ForLoopToolStripMenuItem
        '
        Me.ForLoopToolStripMenuItem.Name = "ForLoopToolStripMenuItem"
        Me.ForLoopToolStripMenuItem.Size = New System.Drawing.Size(152, 22)
        Me.ForLoopToolStripMenuItem.Text = "For &Loop"
        '
        'WhileLoopToolStripMenuItem
        '
        Me.WhileLoopToolStripMenuItem.Name = "WhileLoopToolStripMenuItem"
        Me.WhileLoopToolStripMenuItem.Size = New System.Drawing.Size(152, 22)
        Me.WhileLoopToolStripMenuItem.Text = "&While Loop"
        '
        'TryCatchToolStripMenuItem
        '
        Me.TryCatchToolStripMenuItem.Name = "TryCatchToolStripMenuItem"
        Me.TryCatchToolStripMenuItem.Size = New System.Drawing.Size(152, 22)
        Me.TryCatchToolStripMenuItem.Text = "&Try Catch"
        '
        'ClassToolStripMenuItem
        '
        Me.ClassToolStripMenuItem.Name = "ClassToolStripMenuItem"
        Me.ClassToolStripMenuItem.Size = New System.Drawing.Size(152, 22)
        Me.ClassToolStripMenuItem.Text = "Class"
        '
        'FunctionToolStripMenuItem
        '
        Me.FunctionToolStripMenuItem.Name = "FunctionToolStripMenuItem"
        Me.FunctionToolStripMenuItem.Size = New System.Drawing.Size(152, 22)
        Me.FunctionToolStripMenuItem.Text = "&Function"
        '
        'SubToolStripMenuItem
        '
        Me.SubToolStripMenuItem.Name = "SubToolStripMenuItem"
        Me.SubToolStripMenuItem.Size = New System.Drawing.Size(152, 22)
        Me.SubToolStripMenuItem.Text = "&Sub"
        '
        'ToolStripSeparator16
        '
        Me.ToolStripSeparator16.Name = "ToolStripSeparator16"
        Me.ToolStripSeparator16.Size = New System.Drawing.Size(179, 6)
        '
        'StartRecordingToolStripMenuItem
        '
        Me.StartRecordingToolStripMenuItem.Image = Global.SlickTestDeveloper.My.Resources.Resources.voicepad_118
        Me.StartRecordingToolStripMenuItem.Name = "StartRecordingToolStripMenuItem"
        Me.StartRecordingToolStripMenuItem.Size = New System.Drawing.Size(182, 22)
        Me.StartRecordingToolStripMenuItem.Text = "&Start Recording"
        '
        'ToolStripSeparator15
        '
        Me.ToolStripSeparator15.Name = "ToolStripSeparator15"
        Me.ToolStripSeparator15.Size = New System.Drawing.Size(179, 6)
        '
        'TakeScreenshotToolStripMenuItem
        '
        Me.TakeScreenshotToolStripMenuItem.Image = CType(resources.GetObject("TakeScreenshotToolStripMenuItem.Image"), System.Drawing.Image)
        Me.TakeScreenshotToolStripMenuItem.Name = "TakeScreenshotToolStripMenuItem"
        Me.TakeScreenshotToolStripMenuItem.Size = New System.Drawing.Size(182, 22)
        Me.TakeScreenshotToolStripMenuItem.Text = "Capture &Screenshot"
        '
        'ObjectSpyToolStripMenuItem
        '
        Me.ObjectSpyToolStripMenuItem.Image = Global.SlickTestDeveloper.My.Resources.Resources.Find
        Me.ObjectSpyToolStripMenuItem.Name = "ObjectSpyToolStripMenuItem"
        Me.ObjectSpyToolStripMenuItem.Size = New System.Drawing.Size(182, 22)
        Me.ObjectSpyToolStripMenuItem.Text = "&Object Spy"
        '
        'FindWindowViaHwndToolStripMenuItem
        '
        Me.FindWindowViaHwndToolStripMenuItem.Image = Global.SlickTestDeveloper.My.Resources.Resources.Help
        Me.FindWindowViaHwndToolStripMenuItem.Name = "FindWindowViaHwndToolStripMenuItem"
        Me.FindWindowViaHwndToolStripMenuItem.Size = New System.Drawing.Size(182, 22)
        Me.FindWindowViaHwndToolStripMenuItem.Text = "Find Window Via &Hwnd"
        '
        'DescriptionTesterToolStripMenuItem
        '
        Me.DescriptionTesterToolStripMenuItem.Image = Global.SlickTestDeveloper.My.Resources.Resources.Preview
        Me.DescriptionTesterToolStripMenuItem.Name = "DescriptionTesterToolStripMenuItem"
        Me.DescriptionTesterToolStripMenuItem.Size = New System.Drawing.Size(182, 22)
        Me.DescriptionTesterToolStripMenuItem.Text = "Description Tester"
        '
        'HelpToolStripMenuItem
        '
        Me.HelpToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.APIToolStripMenuItem, Me.UserGuideToolStripMenuItem, Me.UsingSlickTestToolStripMenuItem, Me.toolStripSeparator5, Me.ResetCacheToolStripMenuItem, Me.ToolStripSeparator17, Me.AboutToolStripMenuItem})
        Me.HelpToolStripMenuItem.Name = "HelpToolStripMenuItem"
        Me.HelpToolStripMenuItem.Size = New System.Drawing.Size(40, 21)
        Me.HelpToolStripMenuItem.Text = "&Help"
        '
        'APIToolStripMenuItem
        '
        Me.APIToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ContentsToolStripMenuItem, Me.IndexToolStripMenuItem, Me.SearchToolStripMenuItem})
        Me.APIToolStripMenuItem.Name = "APIToolStripMenuItem"
        Me.APIToolStripMenuItem.Size = New System.Drawing.Size(162, 22)
        Me.APIToolStripMenuItem.Text = "AP&I"
        '
        'ContentsToolStripMenuItem
        '
        Me.ContentsToolStripMenuItem.Image = Global.SlickTestDeveloper.My.Resources.Resources.Help
        Me.ContentsToolStripMenuItem.Name = "ContentsToolStripMenuItem"
        Me.ContentsToolStripMenuItem.Size = New System.Drawing.Size(152, 22)
        Me.ContentsToolStripMenuItem.Text = "&Contents"
        '
        'IndexToolStripMenuItem
        '
        Me.IndexToolStripMenuItem.Name = "IndexToolStripMenuItem"
        Me.IndexToolStripMenuItem.Size = New System.Drawing.Size(152, 22)
        Me.IndexToolStripMenuItem.Text = "&Index"
        '
        'SearchToolStripMenuItem
        '
        Me.SearchToolStripMenuItem.Name = "SearchToolStripMenuItem"
        Me.SearchToolStripMenuItem.Size = New System.Drawing.Size(152, 22)
        Me.SearchToolStripMenuItem.Text = "&Search"
        '
        'UserGuideToolStripMenuItem
        '
        Me.UserGuideToolStripMenuItem.Name = "UserGuideToolStripMenuItem"
        Me.UserGuideToolStripMenuItem.Size = New System.Drawing.Size(162, 22)
        Me.UserGuideToolStripMenuItem.Text = "&User Guide"
        '
        'UsingSlickTestToolStripMenuItem
        '
        Me.UsingSlickTestToolStripMenuItem.Name = "UsingSlickTestToolStripMenuItem"
        Me.UsingSlickTestToolStripMenuItem.Size = New System.Drawing.Size(162, 22)
        Me.UsingSlickTestToolStripMenuItem.Text = "Automation &Primer"
        '
        'toolStripSeparator5
        '
        Me.toolStripSeparator5.Name = "toolStripSeparator5"
        Me.toolStripSeparator5.Size = New System.Drawing.Size(159, 6)
        '
        'ResetCacheToolStripMenuItem
        '
        Me.ResetCacheToolStripMenuItem.Name = "ResetCacheToolStripMenuItem"
        Me.ResetCacheToolStripMenuItem.Size = New System.Drawing.Size(162, 22)
        Me.ResetCacheToolStripMenuItem.Text = "Reset &Cache"
        '
        'ToolStripSeparator17
        '
        Me.ToolStripSeparator17.Name = "ToolStripSeparator17"
        Me.ToolStripSeparator17.Size = New System.Drawing.Size(159, 6)
        '
        'AboutToolStripMenuItem
        '
        Me.AboutToolStripMenuItem.Name = "AboutToolStripMenuItem"
        Me.AboutToolStripMenuItem.Size = New System.Drawing.Size(162, 22)
        Me.AboutToolStripMenuItem.Text = "&About..."
        '
        'SaveButtonToolStripMenuItem
        '
        Me.SaveButtonToolStripMenuItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.SaveButtonToolStripMenuItem.Image = CType(resources.GetObject("SaveButtonToolStripMenuItem.Image"), System.Drawing.Image)
        Me.SaveButtonToolStripMenuItem.Name = "SaveButtonToolStripMenuItem"
        Me.SaveButtonToolStripMenuItem.Size = New System.Drawing.Size(28, 21)
        Me.SaveButtonToolStripMenuItem.Text = "SaveButton"
        Me.SaveButtonToolStripMenuItem.ToolTipText = "Save File"
        '
        'RunToolStripMenuItem
        '
        Me.RunToolStripMenuItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.RunToolStripMenuItem.Image = Global.SlickTestDeveloper.My.Resources.Resources.wmploc_500
        Me.RunToolStripMenuItem.Name = "RunToolStripMenuItem"
        Me.RunToolStripMenuItem.Size = New System.Drawing.Size(28, 21)
        Me.RunToolStripMenuItem.Text = "Run"
        Me.RunToolStripMenuItem.ToolTipText = "Start Test (Compile)"
        '
        'StopToolStripMenuItem
        '
        Me.StopToolStripMenuItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.StopToolStripMenuItem.Image = CType(resources.GetObject("StopToolStripMenuItem.Image"), System.Drawing.Image)
        Me.StopToolStripMenuItem.Name = "StopToolStripMenuItem"
        Me.StopToolStripMenuItem.Size = New System.Drawing.Size(28, 21)
        Me.StopToolStripMenuItem.Text = "Stop"
        Me.StopToolStripMenuItem.ToolTipText = "Stop Test"
        '
        'ScreenShotToolStripMenuItem
        '
        Me.ScreenShotToolStripMenuItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ScreenShotToolStripMenuItem.Image = CType(resources.GetObject("ScreenShotToolStripMenuItem.Image"), System.Drawing.Image)
        Me.ScreenShotToolStripMenuItem.Name = "ScreenShotToolStripMenuItem"
        Me.ScreenShotToolStripMenuItem.Size = New System.Drawing.Size(28, 21)
        Me.ScreenShotToolStripMenuItem.Text = "ScreenShot"
        Me.ScreenShotToolStripMenuItem.ToolTipText = "Take A Screenshot"
        '
        'SpyToolStripMenuItem
        '
        Me.SpyToolStripMenuItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.SpyToolStripMenuItem.Image = Global.SlickTestDeveloper.My.Resources.Resources.Find
        Me.SpyToolStripMenuItem.Name = "SpyToolStripMenuItem"
        Me.SpyToolStripMenuItem.Size = New System.Drawing.Size(28, 21)
        Me.SpyToolStripMenuItem.Text = "Spy"
        Me.SpyToolStripMenuItem.ToolTipText = "Object Spy"
        '
        'RecordingToolStripMenuItem
        '
        Me.RecordingToolStripMenuItem.BackColor = System.Drawing.SystemColors.Control
        Me.RecordingToolStripMenuItem.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RecordingToolStripMenuItem.ForeColor = System.Drawing.SystemColors.ControlText
        Me.RecordingToolStripMenuItem.Image = Global.SlickTestDeveloper.My.Resources.Resources.voicepad_118
        Me.RecordingToolStripMenuItem.Name = "RecordingToolStripMenuItem"
        Me.RecordingToolStripMenuItem.Size = New System.Drawing.Size(69, 21)
        Me.RecordingToolStripMenuItem.Text = "Record"
        Me.RecordingToolStripMenuItem.ToolTipText = "Record Test"
        '
        'ExternalRunToolStripMenuItem
        '
        Me.ExternalRunToolStripMenuItem.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest
        Me.ExternalRunToolStripMenuItem.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.ExternalRunToolStripMenuItem.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ExternalRunToolStripMenuItem.DropDownWidth = 150
        Me.ExternalRunToolStripMenuItem.IntegralHeight = False
        Me.ExternalRunToolStripMenuItem.Items.AddRange(New Object() {"Externally Reported Run", "Internal Reported Run"})
        Me.ExternalRunToolStripMenuItem.MaxDropDownItems = 2
        Me.ExternalRunToolStripMenuItem.Name = "ExternalRunToolStripMenuItem"
        Me.ExternalRunToolStripMenuItem.Size = New System.Drawing.Size(141, 21)
        '
        'SaveFileDialog1
        '
        Me.SaveFileDialog1.DefaultExt = "stp"
        Me.SaveFileDialog1.Filter = "Slick Test Files|*.st?;*.vb|vb Files|*.vb|All Files|*.*"
        '
        'ToolStripSeparator12
        '
        Me.ToolStripSeparator12.Name = "ToolStripSeparator12"
        Me.ToolStripSeparator12.Size = New System.Drawing.Size(209, 6)
        '
        'OpenFileDialog1
        '
        Me.OpenFileDialog1.DefaultExt = "vb"
        Me.OpenFileDialog1.Filter = "Slick Test Files|*.st?;*.vb;*.cs|Code Files|*.vb;*.cs|All Files|*.*"
        '
        'CompilerIssueDataGridView
        '
        Me.CompilerIssueDataGridView.AllowUserToAddRows = False
        Me.CompilerIssueDataGridView.AllowUserToDeleteRows = False
        Me.CompilerIssueDataGridView.AllowUserToOrderColumns = True
        Me.CompilerIssueDataGridView.BackgroundColor = System.Drawing.Color.WhiteSmoke
        Me.CompilerIssueDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.CompilerIssueDataGridView.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.CompilerIssueDataGridView.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnF2
        Me.CompilerIssueDataGridView.GridColor = System.Drawing.Color.Gray
        Me.CompilerIssueDataGridView.Location = New System.Drawing.Point(0, 431)
        Me.CompilerIssueDataGridView.Name = "CompilerIssueDataGridView"
        Me.CompilerIssueDataGridView.ReadOnly = True
        Me.CompilerIssueDataGridView.ShowCellErrors = False
        Me.CompilerIssueDataGridView.ShowEditingIcon = False
        Me.CompilerIssueDataGridView.ShowRowErrors = False
        Me.CompilerIssueDataGridView.Size = New System.Drawing.Size(828, 0)
        Me.CompilerIssueDataGridView.TabIndex = 11
        '
        'ContextMenuCopyOnly
        '
        Me.ContextMenuCopyOnly.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.CopyMenuItem})
        Me.ContextMenuCopyOnly.Name = "ContextMenuCopyOnly"
        Me.ContextMenuCopyOnly.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional
        Me.ContextMenuCopyOnly.Size = New System.Drawing.Size(100, 26)
        '
        'CopyMenuItem
        '
        Me.CopyMenuItem.Image = Global.SlickTestDeveloper.My.Resources.Resources.Copy
        Me.CopyMenuItem.Name = "CopyMenuItem"
        Me.CopyMenuItem.Size = New System.Drawing.Size(99, 22)
        Me.CopyMenuItem.Text = "Copy"
        '
        'ContextMenuAddFiles
        '
        Me.ContextMenuAddFiles.Name = "ContextMenuAddFiles"
        Me.ContextMenuAddFiles.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional
        Me.ContextMenuAddFiles.Size = New System.Drawing.Size(61, 4)
        '
        'OpenFileToolStripMenuItem
        '
        Me.OpenFileToolStripMenuItem.Name = "OpenFileToolStripMenuItem"
        Me.OpenFileToolStripMenuItem.Size = New System.Drawing.Size(32, 19)
        '
        'RenameFileToolStripMenuItem
        '
        Me.RenameFileToolStripMenuItem.Name = "RenameFileToolStripMenuItem"
        Me.RenameFileToolStripMenuItem.Size = New System.Drawing.Size(32, 19)
        '
        'RemoveFileToolStripMenuItem
        '
        Me.RemoveFileToolStripMenuItem.Name = "RemoveFileToolStripMenuItem"
        Me.RemoveFileToolStripMenuItem.Size = New System.Drawing.Size(32, 19)
        '
        'AddFileToolStripMenuItem
        '
        Me.AddFileToolStripMenuItem.Name = "AddFileToolStripMenuItem"
        Me.AddFileToolStripMenuItem.Size = New System.Drawing.Size(32, 19)
        '
        'Splitter1
        '
        Me.Splitter1.Location = New System.Drawing.Point(0, 25)
        Me.Splitter1.Name = "Splitter1"
        Me.Splitter1.Size = New System.Drawing.Size(3, 406)
        Me.Splitter1.TabIndex = 14
        Me.Splitter1.TabStop = False
        '
        'SplitViewContainer
        '
        Me.SplitViewContainer.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitViewContainer.Location = New System.Drawing.Point(3, 25)
        Me.SplitViewContainer.Name = "SplitViewContainer"
        '
        'SplitViewContainer.Panel1
        '
        Me.SplitViewContainer.Panel1.AutoScroll = True
        Me.SplitViewContainer.Panel1.Controls.Add(Me.FileTreeView)
        '
        'SplitViewContainer.Panel2
        '
        Me.SplitViewContainer.Panel2.AutoScroll = True
        Me.SplitViewContainer.Panel2.Controls.Add(Me.TxtEdit)
        Me.SplitViewContainer.Size = New System.Drawing.Size(825, 406)
        Me.SplitViewContainer.SplitterDistance = 123
        Me.SplitViewContainer.TabIndex = 1
        Me.SplitViewContainer.Text = "SplitContainer1"
        '
        'FileTreeView
        '
        Me.FileTreeView.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.FileTreeView.ContextMenuStrip = Me.ContextMenuAddFiles
        Me.FileTreeView.Dock = System.Windows.Forms.DockStyle.Fill
        Me.FileTreeView.Location = New System.Drawing.Point(0, 0)
        Me.FileTreeView.Name = "FileTreeView"
        TreeNode1.Name = "FileNameNode"
        TreeNode1.Text = "Files"
        TreeNode2.Name = "DLLNode"
        TreeNode2.Text = "DLL"
        TreeNode3.Name = "UserLibs"
        TreeNode3.Text = "User Libraries"
        Me.FileTreeView.Nodes.AddRange(New System.Windows.Forms.TreeNode() {TreeNode1, TreeNode2, TreeNode3})
        Me.FileTreeView.ShowNodeToolTips = True
        Me.FileTreeView.Size = New System.Drawing.Size(123, 406)
        Me.FileTreeView.TabIndex = 19
        '
        'ResultsTimer
        '
        Me.ResultsTimer.Interval = 40
        '
        'SaveFileDialogForProject
        '
        Me.SaveFileDialogForProject.DefaultExt = "vbproj"
        Me.SaveFileDialogForProject.Filter = "Project Files|*.vbproj;*.csproj|All Files|*.*"
        '
        'CurrentFileWatcher
        '
        Me.CurrentFileWatcher.EnableRaisingEvents = True
        Me.CurrentFileWatcher.NotifyFilter = CType((System.IO.NotifyFilters.LastWrite Or System.IO.NotifyFilters.CreationTime), System.IO.NotifyFilters)
        Me.CurrentFileWatcher.SynchronizingObject = Me
        '
        'ArgumentTimer
        '
        Me.ArgumentTimer.Enabled = True
        Me.ArgumentTimer.Interval = 500
        '
        'SlickTestDev
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.ClientSize = New System.Drawing.Size(828, 453)
        Me.Controls.Add(Me.SplitViewContainer)
        Me.Controls.Add(Me.Splitter1)
        Me.Controls.Add(Me.CompilerIssueDataGridView)
        Me.Controls.Add(Me.MenuStrip1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.MinimumSize = New System.Drawing.Size(475, 199)
        Me.Name = "SlickTestDev"
        Me.Text = "Slick Test Developer"
        Me.Controls.SetChildIndex(Me.MenuStrip1, 0)
        Me.Controls.SetChildIndex(Me.CompilerIssueDataGridView, 0)
        Me.Controls.SetChildIndex(Me.Splitter1, 0)
        Me.Controls.SetChildIndex(Me.SplitViewContainer, 0)
        Me.MenuStrip1.ResumeLayout(False)
        Me.MenuStrip1.PerformLayout()
        CType(Me.CompilerIssueDataGridView, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ContextMenuCopyOnly.ResumeLayout(False)
        Me.SplitViewContainer.Panel1.ResumeLayout(False)
        Me.SplitViewContainer.Panel2.ResumeLayout(False)
        Me.SplitViewContainer.ResumeLayout(False)
        CType(Me.CurrentFileWatcher, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

#End Region

#Region "Code To Work On"

    Private Sub ExportForExternalUseToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ExportForExternalUseToolStripMenuItem.Click
        If (Me.SaveFileDialogForProject.ShowDialog() <> Windows.Forms.DialogResult.OK) Then
            Return
        End If

        Try
            Dim engine As New Microsoft.Build.BuildEngine.Engine()
            engine.BinPath = System.Runtime.InteropServices.RuntimeEnvironment.GetRuntimeDirectory()
            Dim project As Microsoft.Build.BuildEngine.Project = engine.CreateNewProject()
            Dim prop As Microsoft.Build.BuildEngine.BuildPropertyGroup = project.AddNewPropertyGroup(False)
            Dim propGroup2 As Microsoft.Build.BuildEngine.BuildPropertyGroup = project.AddNewPropertyGroup(False)


            prop.AddNewProperty("Configuration", "Debug")
            For Each p As Microsoft.Build.BuildEngine.BuildProperty In prop
                p.Condition = " '$(Configuration)' == '' "
            Next

            prop.AddNewProperty("AssemblyName", Me.Project.ProjectName)
            If (Me.Project.ProjectType = CurrentProjectData.ProjectTypes.LibraryProject) Then
                prop.AddNewProperty("OutputType", "Library")
            Else
                prop.AddNewProperty("OutputType", "Exe")
                prop.AddNewProperty("MyType", "Console")
            End If
            prop.AddNewProperty("TargetFrameworkVersion", "v3.5")
            prop.AddNewProperty("OptionExplicit", ConvertBoolAsOnOrOff(Me.Project.OptionExplicit))
            prop.AddNewProperty("OptionStrict", ConvertBoolAsOnOrOff(Me.Project.OptionStrict))
            prop.AddNewProperty("OptionCompare", "Binary")
            prop.AddNewProperty("OptionInfer", "On")

            Dim items As Microsoft.Build.BuildEngine.BuildItemGroup = project.AddNewItemGroup()
            For Each item As String In Me.Project.Assemblies
                items.AddNewItem("Reference", item)
            Next
            For Each item As String In Me.Project.SpecialAssemblies
                items.AddNewItem("Reference", item)
            Next

            For Each item As String In Me.Project.UserAssemblies
                items.AddNewItem("Reference", item)
            Next

            Dim IncludeSTP As Microsoft.Build.BuildEngine.BuildItemGroup = project.AddNewItemGroup()
            IncludeSTP.AddNewItem("None", Me.Project.LoadLocation)
            IncludeSTP(IncludeSTP.Count - 1).SetMetadata("CopyToOutputDirectory", "Always")

            For Each item As String In Me.Project.BuildFiles
                items.AddNewItem("Compile", Me.Project.SourceFileLocation & item)
            Next

            propGroup2.Condition = " '$(Configuration)|$(Platform)' == 'Debug|AnyCPU'"
            propGroup2.AddNewProperty("DebugSymbols", "true")
            propGroup2.AddNewProperty("DebugType", "full")
            propGroup2.AddNewProperty("DefineDebug", "true")
            propGroup2.AddNewProperty("DefineTrace", "true")
            propGroup2.AddNewProperty("OutputPath", "bin\Debug\")
            propGroup2.AddNewProperty("DocumentationFile", Me.Project.ProjectName & ".xml")
            propGroup2.AddNewProperty("NoWarn", "42016,41999,42017,42018,42019,42032,42036,42020,42021,42022")

            project.AddNewImport("$(MSBuildToolsPath)\Microsoft.VisualBasic.targets", "")
            '        <Import Project="$(MSBuildToolsPath)\Microsoft.VisualBasic.targets" />
            '<!-- To modify your build process, add your task inside one of the targets below and uncomment it. 
            '     Other similar extension points exist, see Microsoft.Common.targets.
            '<Target Name="BeforeBuild">
            '</Target>
            '<Target Name="AfterBuild">
            '</Target>
            '-->

            If (System.IO.File.Exists(Me.SaveFileDialogForProject.FileName)) Then
                System.IO.File.Delete(Me.SaveFileDialogForProject.FileName)
            End If

            project.Save(Me.SaveFileDialogForProject.FileName)
            Me.UserInformationBar.Text = "Project saved (" & Me.SaveFileDialogForProject.FileName & ")."
            System.Windows.Forms.MessageBox.Show("Project Saved.")
            '<Configuration Condition=" '$(Configuration)' == '' ">Debug</Configuration>
            '<PropertyGroup Condition=" '$(Configuration)|$(Platform)' == 'Debug|AnyCPU' ">
            '  <DebugSymbols>true</DebugSymbols>
            '  <DebugType>full</DebugType>
            '  <DefineDebug>true</DefineDebug>
            '  <DefineTrace>true</DefineTrace>
            '  <OutputPath>bin\Debug\</OutputPath>
            '  <DocumentationFile>SampleTestProject.xml</DocumentationFile>
            '  <NoWarn>42016,41999,42017,42018,42019,42032,42036,42020,42021,42022</NoWarn>
            '</PropertyGroup>
        Catch ex As Exception
            System.Windows.Forms.MessageBox.Show("Project was not saved.  An error occured: " & ex.ToString())
        End Try

    End Sub
    Private Function ConvertBoolAsOnOrOff(ByVal b As Boolean) As String
        If (b) Then Return "On"
        Return "Off"
    End Function

    'Appears to work, but not well tested.
    Private Sub SaveProjectAsToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SaveProjectAsToolStripMenuItem.Click
        Dim TmpDefExt As String = Me.SaveFileDialog1.DefaultExt
        Me.SaveFileDialog1.DefaultExt = "stp"
        Try
            SaveFileDialog1.FileName = String.Empty
            SaveFileDialog1.InitialDirectory = System.IO.Path.GetDirectoryName(SaveFileDialog1.InitialDirectory)
        Catch ex As Exception

        End Try
        Try
            If (Me.SaveFileDialog1.ShowDialog() = Windows.Forms.DialogResult.OK) Then
                Dim FName As String = SaveFileDialog1.FileName
                'Dim FDir As String = System.IO.Path.GetDirectoryName(FName)
                'If (FDir.EndsWith("\") = False) Then
                '    FDir = FDir & "\"
                'End If

                'FName = FDir & System.IO.Path.GetFileNameWithoutExtension(FName) & "\" & System.IO.Path.GetFileName(FName)
                Me.SaveProject(False, True, System.IO.Path.GetFileNameWithoutExtension(FName), FName)
                Me.UpdateTitle()
            End If
        Catch ex As Exception
            System.Windows.Forms.MessageBox.Show("Project failed to save due to the following error: " & ex.Message)
        Finally
            Me.SaveFileDialog1.DefaultExt = TmpDefExt
        End Try
    End Sub

    Private Sub FileToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles FileToolStripMenuItem.Click
        AddFileToProject()
    End Sub

#End Region

#Region "File Read-Save"

    Private Sub LoadProject(ByVal FilePath As String)
        Project.LoadProject(FilePath)
        LoadProject(False)
    End Sub

    Private Sub LoadProject(ByVal NewProject As Boolean)
        IsFirstCompile = True
        Files.Clear()
        FileTreeView.Nodes("DLLNode").Nodes.Clear()
        FileTreeView.Nodes("FileNameNode").Nodes.Clear()
        FileTreeView.Nodes("UserLibs").Nodes.Clear()
        LanguageExt = Project.LanguageExtension
        If (CurrentLanguage = CodeTranslator.Languages.CSharp) Then
            vbc = New DOTNETCompiler.CSharpCompiler()
        Else
            vbc = New DOTNETCompiler.VBCompiler()
        End If

        If (Project.LoadLocation <> "") Then
            If (System.IO.File.Exists(Project.LoadLocation) = False) Then
                System.Windows.Forms.MessageBox.Show("Unable to find project file: " & Project.LoadLocation & _
                ".  Slick Test will now close.", MsgBoxTitle)
                Me.Close()
                Return
            End If
        End If

        Try
            For Each file As String In System.IO.Directory.GetFiles(System.IO.Path.GetDirectoryName(Project.SourceFileLocation))
                If (file.ToLower.EndsWith(LanguageExt)) Then
                    Project.AddFile(file)
                End If
            Next
        Catch ex As Exception
        End Try
        For Each asm As String In Project.Assemblies
            Try
                If (asm.Contains(";") = True) Then
                    asm = asm.Split(";"c)(1)
                End If
                FileTreeView.Nodes("DLLNode").Nodes.Add( _
                            New TreeNode(System.IO.Path.GetFileNameWithoutExtension(asm) & _
                            " - " & System.Reflection.AssemblyName.GetAssemblyName(asm).Version.ToString()))
            Catch ex As Exception
                System.Windows.Forms.MessageBox.Show( _
                "Unable to load Assembly into the project: " & asm & ".", MsgBoxTitle)
            End Try
        Next

        For Each asm As String In Project.UserAssemblies
            Try
                Dim CopyTo As String = Project.OutputFolder
                If (CopyTo.EndsWith("\") = False) Then
                    CopyTo += "\"
                End If
                CopyTo += System.IO.Path.GetFileName(asm)
                Try
                    System.IO.File.Copy(asm, CopyTo, True)
                Catch ex As Exception
                    System.Windows.Forms.MessageBox.Show(ex.Message, MsgBoxTitle)
                End Try
                If (asm.Contains(";") = True) Then
                    asm = asm.Split(";"c)(1)
                End If
                FileTreeView.Nodes("UserLibs").Nodes.Add( _
                            New TreeNode( _
                            IO.Path.GetFileNameWithoutExtension(asm)))
            Catch ex As Exception
                System.Windows.Forms.MessageBox.Show("Unable to load Assembly into the project: " & asm & ".", _
                                                     MsgBoxTitle, MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        Next

        For Each file As String In Project.BuildFiles
            FileTreeView.Nodes("FileNameNode").Nodes.Add(System.IO.Path.GetFileName(file))
            Try
                Dim CRC As String = ""
                Dim FileData As String = ReadFile(Project.SourceFileLocation & file, CRC)
                AddFiles(FileData, file, True, CRC)
            Catch ex As Exception
            End Try
        Next
        If (NewProject = False) Then
            IgnoredSingleTxtChange = True
            Me.TxtEdit.Text = ""
            Try
                If (Project.LastOpenedFile = "") Then
                    Project.LastOpenedFile = Project.BuildFiles.Item(0)
                End If
                If (System.IO.File.Exists(Project.SourceFileLocation & Project.LastOpenedFile) = False) Then
                    Project.LastOpenedFile = Project.BuildFiles.Item(0)
                End If
                Dim CRC As String = ""
                IgnoredSingleTxtChange = True
                Me.TxtEdit.Text = ReadFile(Project.SourceFileLocation & Project.LastOpenedFile, CRC)
                CurrentlyOpenedFile = Project.LastOpenedFile
                AddFiles(Me.TxtEdit.Text, Project.LastOpenedFile, True, CRC)
                Me.Text = TitleText & " - " & Project.ProjectName & " - " & Project.LastOpenedFile
                Dim IsVB As Boolean = (LanguageExt = VBLANGUAGEEXT)
                UpdateLanguageForEditor(IsVB) 'Force update, needed or not.

            Catch ex As Exception
                'No files in the list means the user manually deleted the file while Slick Test was closed.
                If (FileTreeView.Nodes("FileNameNode").Nodes.Count = 0) Then
                    CreateBuildFilesIfRequired(True)
                    AddFiles(Me.TxtEdit.Text, DefaultFileName, False)
                    FileTreeView.Nodes("FileNameNode").Nodes.Add(DefaultFileName)
                End If
            End Try
        Else
            CurrentlyOpenedFile = Project.BuildFiles.Item(0)
            UpdateTitle()
            IgnoredSingleTxtChange = True
            If (Files.ContainsKey(CurrentlyOpenedFile)) Then
                Me.TxtEdit.Text = Files(CurrentlyOpenedFile).FileText
            Else
                Dim filePath As String = Project.SourceFileLocation & CurrentlyOpenedFile
                Me.TxtEdit.Text = ReadFile(filePath)
                AddFiles(Me.TxtEdit.Text, Project.LastOpenedFile, True, Me.GetCRC(filePath))

            End If
        End If
        Me.TxtEdit.Refresh()
        If (Project.IsOfficialRun = True) Then
            Me.ExternalRunToolStripMenuItem.SelectedIndex = 0
        Else
            Me.ExternalRunToolStripMenuItem.SelectedIndex = 1
        End If

        Me.ProjectCurrentlySaved = True
        AddedAsms.Clear()
        Me.AddDLLs()
        Me.AddAssemblies()
        If (Not System.IO.Directory.Exists(Me.Project.SourceFileLocation)) Then
            If (System.Windows.Forms.MessageBox.Show("Unable to find source code folder " & _
                                                     Me.Project.SourceFileLocation & _
                                                     ".  Would you like Slick Test to attempt to create the directory?" & _
                                                     " WARNING: By not creating a source directory, you may destablize " & _
                                                     MsgBoxTitle & ".", MsgBoxTitle, MessageBoxButtons.OKCancel, MessageBoxIcon.Error) = Windows.Forms.DialogResult.OK) Then
                System.IO.Directory.CreateDirectory(Me.Project.SourceFileLocation)
                CreateBuildFilesIfRequired()
            Else
                Return 'Is this right?
            End If
        End If
        Me.CurrentFileWatcher.Path = Me.Project.SourceFileLocation
        'Internal Reported Run
    End Sub

    Private Sub AddFiles(ByVal Text As String, ByVal FileNameOrPath As String, Optional ByVal IsSaved As Boolean = False, Optional ByVal CRC As String = "")
        Dim fi As New FileInfo()
        fi.FileName = FileNameOrPath
        fi.FileText = Text
        fi.IsSaved = IsSaved
        fi.CRC = CRC
        Dim fn As String = ConvertFilesKey(FileNameOrPath)
        If (Files.ContainsKey(fn) = True) Then
            If (fi.IsSaved = True) Then
                If (Files(fn).FileText <> Text) Then
                    fi.IsSaved = False
                End If
            End If
            Files.Remove(fn)
            Files.Add(fn, fi)
        Else
            Files.Add(fn, fi)
        End If
    End Sub

    Private Sub RemoveFiles(ByVal FileNameOrPath As String)
        Dim FileName As String = ConvertFilesKey(FileNameOrPath)
        If (Files.ContainsKey(FileName) = True) Then
            Debug.WriteLine("About to remove " & FileName)
            Files.Remove(FileName)
        Else
            Debug.WriteLine("Will not remove " & FileName)
        End If
    End Sub

    Private Function ConvertFilesKey(ByVal key As String) As String
        Try
            Return System.IO.Path.GetFileName(key).ToLower.Trim()
        Catch ex As Exception
            Return key.ToLower.Trim()
        End Try
    End Function

    Public Sub CreateNewFile(ByVal FilePath As String)
        If (System.IO.File.Exists(FilePath) = False) Then
            Dim tmpFile As String = String.Empty
            Try
                Dim path As String = My.Settings.Project_Default__New__File__Template
                path = System.IO.Path.Combine(System.IO.Path.GetDirectoryName(path), System.IO.Path.GetFileNameWithoutExtension(path))

                tmpFile = Me.ReadFile(path & LanguageExt)
                tmpFile = UpdateFileWithTemplate(tmpFile, System.IO.Path.GetFileName(FilePath))
            Catch ex As Exception
            End Try
            SaveFile(tmpFile, Project.SourceFileLocation & System.IO.Path.GetFileName(FilePath))
        End If
        FileTreeView.Nodes("FileNameNode").Nodes.Add(System.IO.Path.GetFileName(FilePath))
        Project.BuildFiles.Add(System.IO.Path.GetFileName(FilePath))
    End Sub

    Private Function OpenFileDynamically(ByVal OpenFileName As String, Optional ByVal GenerateFile As Boolean = False) As Boolean
        Dim CRC As String = String.Empty
        If (CurrentlyOpenedFile <> "") Then
            'I don't know why this code was commented out.  If uncommented it will catch when reloading
            'the currently file and prompt about a change.  Since I want my change catching even better than
            'that I will leave this commented... for now.
            'If (Me.Files.ContainsKey(Me.ConvertFilesKey(CurrentlyOpenedFile)) = False) Then
            If (System.IO.File.Exists(Project.SourceFileLocation & CurrentlyOpenedFile) = True) Then
                CRC = Me.GetCRC(Project.SourceFileLocation & CurrentlyOpenedFile)
                Dim IsSaved As Boolean = ProjectCurrentlySaved
                If (Me.Files.ContainsKey(Me.ConvertFilesKey(CurrentlyOpenedFile)) = True) Then
                    IsSaved = Me.Files(Me.ConvertFilesKey(CurrentlyOpenedFile)).IsSaved
                End If

                AddFiles(Me.TxtEdit.Text, CurrentlyOpenedFile, IsSaved, CRC)
            End If
            'End If
        End If

        Dim TmpRoleBack As String = CurrentlyOpenedFile
        CurrentlyOpenedFile = OpenFileName
        Me.UpdateTitle()
        If (Me.Files.ContainsKey(Me.ConvertFilesKey(CurrentlyOpenedFile)) = True) Then
            'ProjectCurrentlySaved = Me.Files.Item(CurrentlyOpenedFile).IsSaved

            If (Me.GetCRC(Project.SourceFileLocation & OpenFileName) = Me.Files.Item(Me.ConvertFilesKey(CurrentlyOpenedFile)).CRC) Then
                IgnoredSingleTxtChange = True
                Me.TxtEdit.Text = Me.Files.Item(Me.ConvertFilesKey(CurrentlyOpenedFile)).FileText
            Else
                If (System.Windows.Forms.MessageBox.Show( _
            "Warning: The file you wish to open (" & OpenFileName & ") appears to have been edited while in use by Slick Test Developer.  " & _
            "Do you wish to reload the file?", MsgBoxTitle, _
            MessageBoxButtons.YesNo, MessageBoxIcon.Warning) = Windows.Forms.DialogResult.Yes) Then
                    Me.AddFiles(ReadFile(Project.SourceFileLocation & OpenFileName, CRC), OpenFileName, True, CRC)
                    IgnoredSingleTxtChange = True
                End If
                Me.TxtEdit.Text = Me.Files.Item(Me.ConvertFilesKey(CurrentlyOpenedFile)).FileText
                IgnoredSingleTxtChange = False 'incase the reload was for no good reason.
            End If
            Me.TxtEdit.Refresh()
            Return True
        Else
            If (System.IO.File.Exists(Project.SourceFileLocation & OpenFileName) = True) Then
                'ProjectCurrentlySaved = True
                Me.TxtEdit.Text = ReadFile(Project.SourceFileLocation & OpenFileName, CRC)
                Me.AddFiles(Me.TxtEdit.Text, OpenFileName, True, CRC)
                Me.TxtEdit.Refresh()
                Return True
            Else
                If (GenerateFile = True) Then
                    If (SaveFile("", Project.SourceFileLocation & CurrentlyOpenedFile) = True) Then
                        Me.TxtEdit.Text = ""
                        Me.TxtEdit.Refresh()
                        CRC = Me.GetCRC(Project.SourceFileLocation & CurrentlyOpenedFile)
                        Me.AddFiles(Me.TxtEdit.Text, OpenFileName, True, CRC)

                        'ProjectCurrentlySaved = True
                        Return True
                    End If
                Else
                    'Role back changes
                    CurrentlyOpenedFile = TmpRoleBack
                    UpdateTitle()
                End If
                Return False
            End If
        End If
        Me.TxtEdit.Refresh()
        Return False
    End Function
    Private LastTimeFileWatcherEventRaised As DateTime
    Private Sub CurrentFileWatcher_Changed(ByVal sender As System.Object, ByVal e As System.IO.FileSystemEventArgs) Handles CurrentFileWatcher.Changed
        If (System.DateTime.Now.Subtract(LastTimeFileWatcherEventRaised).TotalMilliseconds < 500) Then
            Return
        End If
        If (e.ChangeType = IO.WatcherChangeTypes.Changed OrElse e.ChangeType = IO.WatcherChangeTypes.Created) Then
            If (e.Name.ToLowerInvariant() = CurrentlyOpenedFile.ToLowerInvariant()) Then
                Dim CRC As String = "InvalidCRC"
                If (Me.Files.ContainsKey(Me.ConvertFilesKey(CurrentlyOpenedFile))) Then
                    CRC = Me.Files(Me.ConvertFilesKey(CurrentlyOpenedFile)).CRC
                End If
                If (Me.GetCRC(e.FullPath) <> CRC) Then
                    LastTimeFileWatcherEventRaised = System.DateTime.Now

                    If (System.Windows.Forms.MessageBox.Show( _
                        "Warning: The file you are editing (" & e.Name & ") appears to have been edited while in use by Slick Test Developer.  " & _
                        "Do you wish to reload the file?", MsgBoxTitle, _
                        MessageBoxButtons.YesNo, MessageBoxIcon.Warning) = Windows.Forms.DialogResult.Yes) Then
                        Me.Files.Remove(Me.ConvertFilesKey(CurrentlyOpenedFile))
                        Dim file As String = CurrentlyOpenedFile
                        CurrentlyOpenedFile = ""
                        IgnoredSingleTxtChange = True
                        Me.OpenFileDynamically(file, False)
                        IgnoredSingleTxtChange = False 'incase the reload wasn't for a good reason.
                    End If
                End If
            End If
        End If

    End Sub

    Private Function GetCRC(ByVal filePath As String) As String
        Dim CRC As New System.Text.StringBuilder(256)
        Dim f As New System.IO.FileStream(filePath, IO.FileMode.Open, _
        IO.FileAccess.Read, IO.FileShare.Read)
        For Each B As Byte In MD5ForCRC.ComputeHash(CType(f, System.IO.Stream))
            CRC.Append(B.ToString())
        Next
        f.Close()
        Return CRC.ToString()
    End Function

    Private Function ReadFile(ByVal filename As String, Optional ByRef CRC As String = "Ignore") As String
        Try

            If (CRC <> "Ignore") Then
                CRC = GetCRC(filename)
            End If
            Dim sr As New System.IO.StreamReader(filename, System.Text.Encoding.UTF8, True)
            Dim str As String = sr.ReadToEnd()
            sr.Close()
            Return str
        Catch ex As Exception
            System.Windows.Forms.MessageBox.Show("Exception occured while trying to read a file. " & _
            "Exception: " & ex.Message, MsgBoxTitle)
            CRC = ""
            Return ""
        End Try
    End Function

    Private Function WriteFile(ByVal filename As String, ByVal text As String) As Boolean
        Try
            WriteFileUnsafe(filename, text)
        Catch ex As Exception
            System.Windows.Forms.MessageBox.Show("Exception occured while trying to write a file. [File name: " & filename & "] " & _
            "Exception Message: " & ex.Message & vbNewLine & vbNewLine & "Details: " & ex.ToString(), MsgBoxTitle)
            Me.UserInformationBar.Text = "Failed to save file '" & filename & "' due to the following error: " & ex.Message
            Return False
        End Try
        Return True
    End Function

    Private Sub WriteFileUnsafe(ByVal filename As String, ByVal text As String)
        Dim sw As New System.IO.StreamWriter(filename, False, System.Text.Encoding.UTF8)
        sw.Write(text)
        sw.Flush()
        sw.Close()
    End Sub

    Function SaveFile(ByVal info As String, ByVal FileLocation As String) As Boolean
        Try
            Me.CurrentFileWatcher.EnableRaisingEvents = False
            Dim fileExt As String = System.IO.Path.GetExtension(FileLocation).ToUpper()
            If (Me.CurrentFileWatcher.Path = "" AndAlso fileExt.Equals(LanguageExt.ToUpper())) Then
                'Error protection.
                Me.CurrentFileWatcher.Path = System.IO.Path.GetDirectoryName(FileLocation)
            End If

            If (System.IO.File.Exists(FileLocation)) Then 'delete file before saving.
                System.IO.File.Delete(FileLocation)
            End If
            WriteFileUnsafe(FileLocation, info)
            If (System.IO.Path.GetFileName(FileLocation).ToUpperInvariant().Equals(CurrentlyOpenedFile.ToUpperInvariant())) Then
                Dim FilesKey As String = Me.ConvertFilesKey(FileLocation)
                If (Me.Files.ContainsKey(FilesKey)) Then
                    Me.Files(FilesKey).IsSaved = True
                    Me.Files(FilesKey).FileText = info
                    Me.Files(FilesKey).CRC = Me.GetCRC(FileLocation)
                Else
                    'first time.
                    Me.AddFiles(info, FileLocation, True, Me.GetCRC(FileLocation))
                End If
            End If
            'If (fileExt = Me.LanguageExt.ToUpper()) Then
            '    AddFiles(info, FileLocation, True,Me.GetCRC(FileLocation))
            'End If
            Me.UserInformationBar.Text = "File saved: " & FileLocation & "."
            Me.UpdateTitle()
            Me.CurrentFileWatcher.EnableRaisingEvents = True

        Catch ex As Exception
            System.Windows.Forms.MessageBox.Show("Exception occured while trying to write a file. " & _
            "Exception: " & ex.Message & vbNewLine & vbNewLine & "Details: " & ex.ToString(), MsgBoxTitle)
            Me.UserInformationBar.Text = "File NOT saved: " & FileLocation & ".  Error: " & ex.Message

            Me.CurrentFileWatcher.EnableRaisingEvents = True
            Return False
        End Try
        Return True
    End Function

    ''' <summary> 
    ''' Recursively Copies source folders and files to the destination folder. 
    ''' </summary> 
    ''' <param name="sourceFolderPath">The folder to be copied.</param> 
    ''' <param name="destinationFolderPath">The folder to where the folder to be copied will be copied.</param> 
    ''' <param name="overwrite">Overwrite existing files and folders that exist.</param> 
    Private Sub CopyFolder(ByVal sourceFolderPath As String, ByVal destinationFolderPath As String, ByVal overwrite As Boolean)
        If System.IO.Directory.Exists(sourceFolderPath) Then
            If (destinationFolderPath.EndsWith("\") = True) Then
                destinationFolderPath = destinationFolderPath.Substring(0, destinationFolderPath.Length - 1)
            End If
            If System.IO.Directory.Exists(destinationFolderPath) AndAlso (Not overwrite) Then
                Throw New System.Exception("Sorry, but the folder " + destinationFolderPath + " already exists.")
            ElseIf Not System.IO.Directory.Exists(destinationFolderPath) Then
                System.IO.Directory.CreateDirectory(destinationFolderPath)
            End If
            'Copy all the files 
            Dim fiA As System.IO.FileInfo() = (New System.IO.DirectoryInfo(sourceFolderPath).GetFiles())
            For Each fi As System.IO.FileInfo In fiA
                fi.CopyTo(destinationFolderPath + "\" + fi.Name)
            Next
            'Recursively fill the child directories 
            Dim diA As System.IO.DirectoryInfo() = (New System.IO.DirectoryInfo(sourceFolderPath).GetDirectories())
            Dim TmpFolder As String
            For Each di As System.IO.DirectoryInfo In diA
                TmpFolder = di.FullName
                If (TmpFolder.EndsWith("\") = False) Then
                    TmpFolder += "\"
                End If
                TmpFolder = System.IO.Path.GetDirectoryName(TmpFolder).Substring(System.IO.Path.GetDirectoryName(TmpFolder).LastIndexOf("\") + 1)
                CopyFolder(di.FullName, destinationFolderPath + "\" + TmpFolder, overwrite)
            Next
        Else
            Throw New System.Exception("Sorry, source folder " + sourceFolderPath + " doesn't exist.")
        End If
    End Sub

    ''' <summary>
    ''' Attempts to save the entire project.
    ''' </summary>
    ''' <param name="PromptToSave"></param>
    ''' <returns></returns>
    ''' <remarks>returns "Windows.Forms.DialogResult.None" if user doesn't save or the save process fails.</remarks>
    Public Function SaveProject(Optional ByVal PromptToSave As Boolean = True, Optional ByVal ForceSave As Boolean = False, Optional ByVal NewName As String = "", Optional ByVal NewFilePath As String = "") As System.Windows.Forms.DialogResult
        Dim TmpResultsIsSaved As Boolean = ProjectCurrentlySaved
        Dim Info As New System.Text.StringBuilder(100)
        Info.AppendLine(vbNewLine & vbNewLine & "Here are the reasons for this prompt: ")
        If (ProjectCurrentlySaved) Then
            Info.AppendLine("A project setting has been modified.")
        End If
        For Each file As String In Me.Files.Keys
            If (Me.Files(file).IsSaved = False) Then
                TmpResultsIsSaved = False
                Info.AppendLine("File " & file & " is not saved.")
            End If
        Next
        'We have opened a new file, so the last opened file is different or renamed.
        If (Me.Project.LastOpenedFile <> CurrentlyOpenedFile) Then
            If (System.IO.File.Exists(Me.Project.SourceFileLocation & CurrentlyOpenedFile)) Then
                TmpResultsIsSaved = False
                Info.AppendLine("Last opened file has changed.")
            End If
        End If
        If (NewFilePath = "") Then
            NewFilePath = Project.LoadLocation
        Else
            ''''''''''''''''''''''''''''''''''''''''''''''''''''
            If (NewName = "") Then NewName = System.IO.Path.GetFileNameWithoutExtension(NewFilePath)
            Project.ProjectName = NewName
            Project.ExecuteFileName = NewName & ".exe"
            Dim Directory As String = System.IO.Path.GetDirectoryName(NewFilePath)
            If (Directory.EndsWith("\") = False) Then Directory += "\"
            Directory &= System.IO.Path.GetFileNameWithoutExtension(NewFilePath)
            If (Directory.EndsWith("\") = False) Then Directory += "\"
            'System.IO.Directory.CreateDirectory(Directory)
            CopyFolder(Project.OutputFolder, Directory, True)
            Project.OutputFolder = Directory
            If (System.IO.Directory.Exists(Directory & "Source") = True) Then
                System.IO.Directory.CreateDirectory(Directory & "Source")
            End If

            Project.SourceFileLocation = Directory & "Source\"
            Dim ToDelete As String = Directory & System.IO.Path.GetFileName(Project.LoadLocation)
            Project.LoadLocation = Directory & NewName & ProjectFileExt
            If (System.IO.File.Exists(ToDelete) = True) Then
                System.IO.File.Delete(ToDelete) 'One of the few files we don't want saved.
            End If
            Project.GUID = System.Guid.NewGuid()
            NewFilePath = Project.LoadLocation
            '''''''''''''''''''''''''''''''''''''''''''''''''''''
        End If

        If (ForceSave = True) Then
            TmpResultsIsSaved = False
            Info.AppendLine("User action requires a request to save.")

        End If
        If (TmpResultsIsSaved = False) Then
            Dim reasonsForPrompt As String = Info.ToString()
            If (Info.Length < 45) Then reasonsForPrompt = String.Empty
            Dim res As System.Windows.Forms.DialogResult
            If (PromptToSave = True) Then
                res = System.Windows.Forms.MessageBox.Show("Would you like to save the " & _
                                                           "project files for " & _
                                                           Project.ProjectName & "?  " & _
                                                           reasonsForPrompt, _
                      MsgBoxTitle, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Exclamation)
            Else
                res = Windows.Forms.DialogResult.Yes
            End If

            If (res = Windows.Forms.DialogResult.Yes) Then
                Project.LastOpenedFile = CurrentlyOpenedFile
                Dim projectSaveSuccess As Boolean = False
                Dim exMessage As String = ""
                Try
                    Project.SaveProject(Project.LoadLocation)
                    projectSaveSuccess = True
                Catch ex As Exception
                    exMessage = ex.Message
                End Try
                If (SaveAllFiles(ForceSave) = False) Then
                    Return Windows.Forms.DialogResult.None
                End If
                If (projectSaveSuccess = True) Then
                    Me.UserInformationBar.Text += " Project file saved."
                Else
                    Me.UserInformationBar.Text += " Project file NOT saved (Error: " & _
                    exMessage & ")."
                End If
            Else
            End If
            Return res
        ElseIf (PromptToSave = False) Then
            Try
                Project.SaveProject(Project.LoadLocation)
                Me.UserInformationBar.Text = "Project saved."
            Catch ex As Exception
                Me.UserInformationBar.Text = "Project NOT saved (Error: " & ex.Message & ")."
            End Try

            Return Windows.Forms.DialogResult.Yes 'returns yes because that is the save as "success"
        End If
        Return Windows.Forms.DialogResult.None
    End Function

    Public Function SaveAllFiles(Optional ByVal ForceSave As Boolean = False) As Boolean
        For Each file As String In Me.Files.Keys
            If (Me.Files(file).IsSaved = False OrElse ForceSave = True) Then
                Try
                    SaveFile(Me.Files(file).FileText, Project.SourceFileLocation & Me.Files(file).FileName)
                    Me.Files(file).IsSaved = True
                    Me.Files(file).CRC = Me.GetCRC(Project.SourceFileLocation & Me.Files(file).FileName)
                Catch ex As Exception
                    Me.UserInformationBar.Text = "Files in project NOT saved (Error: " & ex.Message & ")."
                    Return False
                End Try
            End If
        Next
        If (Me.ProjectCurrentlySaved = False OrElse ForceSave = True) Then
            Try
                SaveFile(Me.TxtEdit.Text, Me.Project.SourceFileLocation & Me.CurrentlyOpenedFile)
                Me.Files(ConvertFilesKey(Me.CurrentlyOpenedFile)).CRC = Me.GetCRC(Project.SourceFileLocation & Me.CurrentlyOpenedFile)
                Me.Files(ConvertFilesKey(Me.CurrentlyOpenedFile)).FileText = Me.TxtEdit.Text
                Me.Files(ConvertFilesKey(Me.CurrentlyOpenedFile)).IsSaved = True
            Catch ex As Exception
                Me.UserInformationBar.Text = "NOT all files in project saved (Error: " & ex.Message & ")."
                Return False
            End Try
            ProjectCurrentlySaved = True
        End If
        Me.UserInformationBar.Text = "Files in project saved (" & Project.ProjectName & ")."
        Return True
    End Function

    Private Sub SaveToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SaveToolStripMenuItem.Click, SaveButtonToolStripMenuItem.Click
        Dim file As String = Project.SourceFileLocation & CurrentlyOpenedFile
        If (file.ToLower().EndsWith(LanguageExt) = False) Then file += LanguageExt
        If (SaveFile(TxtEdit.Text, file) = True) Then
            Dim FilesKeys As String = ConvertFilesKey(file)
            Me.Files(FilesKeys).IsSaved = True
            Me.Files(FilesKeys).CRC = Me.GetCRC(file)
            Me.Files(FilesKeys).FileText = Me.TxtEdit.Text
        End If
        'Me.SaveProject(False, True)
        'Me.ProjectCurrentlySaved = True
    End Sub

    Private Sub SaveAsToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SaveAsToolStripMenuItem.Click
        SaveFileDialog1.DefaultExt = LanguageExt
        SaveFileDialog1.Filter = "Slick Test Files|*" & LanguageExt & "|All Files|*.*"
        SaveFileDialog1.InitialDirectory = Project.SourceFileLocation

        Dim file As String = Project.SourceFileLocation & CurrentlyOpenedFile
        If (file.ToUpperInvariant().EndsWith(LanguageExt.ToUpperInvariant()) = False) Then file += LanguageExt
        Dim FileName As String = String.Empty
        Dim d As System.Windows.Forms.DialogResult = SaveFileDialog1.ShowDialog()
        If (d <> Windows.Forms.DialogResult.OK) Then
            Return
        Else
            FileName = SaveFileDialog1.FileName
        End If
        If (System.IO.File.Exists(FileName) = True) Then
            If (System.Windows.Forms.MessageBox.Show( _
                "Warning: You are about to over write a file.  Would you " & _
                "like to continue?", MsgBoxTitle, MessageBoxButtons.YesNo, _
                MessageBoxIcon.Warning) = Windows.Forms.DialogResult.No) Then
                Return
            End If
        End If
        If (SaveFile(TxtEdit.Text, FileName) = True) Then
            AddFiles(TxtEdit.Text, FileName, True, Me.GetCRC(FileName))
            AddFiles(TxtEdit.Text, file, True, Me.GetCRC(file))

            Try
                Dim Prompt As String
                Prompt = "Would you liked to add the file to the project?"
                If (FileName.ToUpperInvariant().Contains(Project.SourceFileLocation.ToUpperInvariant()) = True AndAlso _
                FileName.ToUpperInvariant() <> (Project.SourceFileLocation & System.IO.Path.GetFileName(FileName)).ToUpperInvariant()) Then
                    If (System.IO.File.Exists(Project.SourceFileLocation & System.IO.Path.GetFileName(FileName)) = True) Then
                        'File already exists
                        Prompt += " Adding the file to the project will erase the existing file '" & System.IO.Path.GetFileName(FileName) & "'."
                    End If
                End If
                If (System.Windows.Forms.MessageBox.Show(Prompt, MsgBoxTitle, _
                                                         MessageBoxButtons.YesNo) = Windows.Forms.DialogResult.Yes) Then
                    'System.IO.File.Delete(file)
                    'reload file name
                    'If (Me.Files.ContainsKey(CurrentlyOpenedFile) = True) Then Me.Files.Remove(CurrentlyOpenedFile)
                    If (FileName.ToUpperInvariant() <> (Project.SourceFileLocation & System.IO.Path.GetFileName(FileName)).ToUpperInvariant()) Then
                        Try
                            System.IO.File.Copy(FileName, Project.SourceFileLocation & System.IO.Path.GetFileName(FileName), True)
                        Catch ex As Exception
                            System.Windows.Forms.MessageBox.Show( _
                            "Error adding file to project.  Error message: " _
                            & ex.Message, MsgBoxTitle, MessageBoxButtons.OK, _
                            MessageBoxIcon.Error)
                            Return
                        End Try
                    End If
                    CurrentlyOpenedFile = System.IO.Path.GetFileName(FileName)
                    Me.ProjectCurrentlySaved = True
                    Dim CRC As String = Me.GetCRC(FileName)
                    Me.AddFiles(Me.TxtEdit.Text, CurrentlyOpenedFile, True, CRC)
                    If (FileTreeView.Nodes("FileNameNode").Nodes.ContainsKey(CurrentlyOpenedFile) = False) Then FileTreeView.Nodes("FileNameNode").Nodes.Add(CurrentlyOpenedFile)
                    UpdateTitle()
                End If
            Catch ex As Exception
                System.Windows.Forms.MessageBox.Show( _
                "Save As failed to complete due to: " & ex.Message, _
                MsgBoxTitle, MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End If
    End Sub


#End Region

#Region "Tool commands -- Probably does not need to be edited much"

    Private Sub ShowAbout()
        With New AboutBox
            .AppTitle = Application.ProductName 'Me.Text
            .AppVersion = System.Reflection.Assembly.GetExecutingAssembly.GetName().Version.ToString()
            .ShowDialog(Me)
        End With
    End Sub

    Private Sub SplitViewToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SplitViewToolStripMenuItem.Click
        Me.TxtEdit.ActiveTextAreaControl.TextArea.MotherTextEditorControl.Split()
        Me.SplitViewToolStripMenuItem.Checked = Not Me.SplitViewToolStripMenuItem.Checked
    End Sub

    Private Sub StopExecutable()
        If (vbc.KillProcess() = False) Then
            System.Windows.Forms.MessageBox.Show("Failed to end test or no test is running.")
        End If
    End Sub

    'Private Sub TxtEdit_KeyPressed(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
    '    'If (Char.IsLetterOrDigit(e.KeyChar) = True) Then CurrentlySaved = False
    'End Sub

    Private Sub ExitToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ExitToolStripMenuItem1.Click
        Me.Close()
    End Sub

    Private Sub SaveProjectToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SaveProjectToolStripMenuItem.Click
        Me.SaveProject(False, True)
        'ProjectSelectionForm = New ProjectSelect(Me)
        'ProjectSelectionForm.Show(Me)
    End Sub

    Private Sub CompilerErrorsResize()
        If (CompilerIssueDataGridView.Size.Height <> 0) Then
            CompilerIssueDataGridView.Columns("Number").Width = Math.Max(1, Convert.ToInt32(Me.Size.Width * 0.05))
            CompilerIssueDataGridView.Columns("File").Width = Math.Max(1, Convert.ToInt32(Me.Size.Width * 0.2))
            CompilerIssueDataGridView.Columns("Line").Width = Math.Max(1, Convert.ToInt32(Me.Size.Width * 0.05))
            CompilerIssueDataGridView.Columns("Description").Width = Math.Max(10, Convert.ToInt32(Me.Size.Width * 0.6))
            Dim TempHeight As Integer = Math.Min((CompilerIssueDataGridView.Rows(0).Height * vbc.Errors.Count) + CompilerIssueDataGridView.Rows(0).HeaderCell.Size.Height, 250)
            TempHeight = Math.Min(TempHeight, Convert.ToInt32(Me.Size.Height / 2))
            CompilerIssueDataGridView.Height = Math.Max(TempHeight, 1)
        End If
    End Sub

    Private Sub StartRecordingToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Record()
    End Sub

    Private Sub IfToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles IfToolStripMenuItem.Click
        If (Me.LanguageExt = CSHARPLANGUAGEEXT) Then
            Me.TxtEdit.DeleteAndInsertOverTopWithTabing("if(""Var1""==""Var2""){" & vbNewLine & _
                                  SelectedTextForCodeHelp() & vbNewLine & "}" & vbNewLine)

        Else
            Me.TxtEdit.DeleteAndInsertOverTopWithTabing("If(""Var1""=""Var2"") Then" & vbNewLine & _
                                              SelectedTextForCodeHelp() & vbNewLine & "End If" & vbNewLine)
        End If
        ProjectCurrentlySaved = False

    End Sub

    Private Sub IfElseToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles IfElseToolStripMenuItem.Click
        If (Me.LanguageExt = CSHARPLANGUAGEEXT) Then
            Me.TxtEdit.DeleteAndInsertOverTopWithTabing( _
            "If(""Var1""=""Var2"") Then" & vbNewLine & SelectedTextForCodeHelp() & _
            vbNewLine & "Else" & vbNewLine & "End If" & vbNewLine)
        Else
            Me.TxtEdit.DeleteAndInsertOverTopWithTabing( _
            "if(""Var1""==""Var2""){" & vbNewLine & SelectedTextForCodeHelp() & _
            vbNewLine & "}else{" & vbNewLine & "}" & vbNewLine)

        End If
        ProjectCurrentlySaved = False
    End Sub

    Private Sub DoWhileToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DoWhileToolStripMenuItem.Click
        If (Me.LanguageExt = CSHARPLANGUAGEEXT) Then
            Me.TxtEdit.DeleteAndInsertOverTopWithTabing("do{" & vbNewLine & SelectedTextForCodeHelp() & _
                                  vbNewLine & "}while(""Var1""==""Var2"")" & vbNewLine)

        Else
            Me.TxtEdit.DeleteAndInsertOverTopWithTabing("Do" & vbNewLine & SelectedTextForCodeHelp() & _
                                              vbNewLine & "Loop While(""Var1""=""Var2"")" & vbNewLine)
        End If
        ProjectCurrentlySaved = False
    End Sub

    Private Sub ForLoopToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ForLoopToolStripMenuItem.Click
        If (Me.LanguageExt = CSHARPLANGUAGEEXT) Then
            Me.TxtEdit.DeleteAndInsertOverTopWithTabing("For(int i =0;i!=10;i++){" & _
vbNewLine & SelectedTextForCodeHelp() & vbNewLine & "}" & vbNewLine)

        Else
            Me.TxtEdit.DeleteAndInsertOverTopWithTabing("For i As Integer = 0 To 10" & _
            vbNewLine & SelectedTextForCodeHelp() & vbNewLine & "Next" & vbNewLine)
        End If
        ProjectCurrentlySaved = False
    End Sub

    Private Sub WhileLoopToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles WhileLoopToolStripMenuItem.Click
        If (Me.LanguageExt = CSHARPLANGUAGEEXT) Then
            Me.TxtEdit.DeleteAndInsertOverTopWithTabing("while (""Str1"" == ""Str2""){" & _
            vbNewLine & SelectedTextForCodeHelp() & vbNewLine & "}" & vbNewLine)
        Else
            Me.TxtEdit.DeleteAndInsertOverTopWithTabing("While (""Str1"" = ""Str2"")" & _
            vbNewLine & SelectedTextForCodeHelp() & vbNewLine & "End While" & vbNewLine)
        End If
        ProjectCurrentlySaved = False
    End Sub

    Private Sub TryCatchToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TryCatchToolStripMenuItem.Click
        If (Me.LanguageExt = CSHARPLANGUAGEEXT) Then
            Me.TxtEdit.DeleteAndInsertOverTopWithTabing("try{" & _
            vbNewLine & SelectedTextForCodeHelp() & vbNewLine & "}catch(Exception ex){" & vbNewLine & _
            "'Reporter.RecordEvent(UIControls.Report.Fail,""An Exception has occurred: "" + ex.ToString(),""Exception"")" & _
            vbNewLine & "}" & vbNewLine)

        Else
            Me.TxtEdit.DeleteAndInsertOverTopWithTabing("Try" & _
            vbNewLine & SelectedTextForCodeHelp() & vbNewLine & "Catch ex As Exception" & vbNewLine & _
            "'Reporter.RecordEvent(UIControls.Report.Fail,""An Exception has occurred: "" & ex.ToString(),""Exception"")" & _
            vbNewLine & "End Try" & vbNewLine)
        End If
        ProjectCurrentlySaved = False
    End Sub

    Private Sub ClassToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ClassToolStripMenuItem.Click
        If (Me.LanguageExt = CSHARPLANGUAGEEXT) Then
            Me.TxtEdit.DeleteAndInsertOverTopWithTabing( _
            "//This class does not Inherit from UIControls.InterAct by default." & _
            vbNewLine & "public class MyNewClass" & _
            vbNewLine & "{// : UIControls.InterAct" & _
            vbNewLine & vbNewLine & "public void MyNewClass(){//Constructor" & vbNewLine & _
            SelectedTextForCodeHelp() & vbNewLine & "}" & _
            vbNewLine & "}" & vbNewLine)
        Else
            Me.TxtEdit.DeleteAndInsertOverTopWithTabing( _
            "'This class does not Inherit from UIControls.InterAct by default." & _
            vbNewLine & "Public Class MyNewClass" & _
            vbNewLine & "'Inherits UIControls.InterAct" & _
            vbNewLine & vbNewLine & "Public Sub New()'Constructor" & vbNewLine & _
            SelectedTextForCodeHelp() & vbNewLine & "End Sub" & _
            vbNewLine & "End Class" & vbNewLine)

        End If
        ProjectCurrentlySaved = False
    End Sub

    Private Function SelectedTextForCodeHelp() As String
        Return Me.TxtEdit.SelectedText().TrimStart(vbTab).TrimEnd(vbNewLine)
    End Function


    Private Sub FunctionToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles FunctionToolStripMenuItem.Click
        If (Me.LanguageExt = CSHARPLANGUAGEEXT) Then
            Me.TxtEdit.DeleteAndInsertOverTopWithTabing("public bool MyFunction(){" & _
                vbNewLine & SelectedTextForCodeHelp() & vbNewLine & "return true;" & vbNewLine & _
                "}" & vbNewLine)
        Else
            Me.TxtEdit.DeleteAndInsertOverTopWithTabing("Public Function MyFunction() as Boolean" & _
                            vbNewLine & SelectedTextForCodeHelp() & vbNewLine & "return True" & vbNewLine & _
                            "End Function" & vbNewLine)
        End If
        ProjectCurrentlySaved = False
    End Sub

    Private Sub SubToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SubToolStripMenuItem.Click
        If (Me.LanguageExt = CSHARPLANGUAGEEXT) Then
            Me.TxtEdit.DeleteAndInsertOverTopWithTabing("public void MySub(){" & _
                vbNewLine & SelectedTextForCodeHelp() & vbNewLine & _
                "}" & vbNewLine)

        Else

            Me.TxtEdit.DeleteAndInsertOverTopWithTabing("Public Sub MySub()" & _
                            vbNewLine & SelectedTextForCodeHelp() & vbNewLine & _
                            "End Sub" & vbNewLine)
        End If
        ProjectCurrentlySaved = False
    End Sub

    Private Sub CutToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CutToolStripMenuItem.Click
        TxtEdit.Cut()
        ProjectCurrentlySaved = False
    End Sub

    Private Sub CopyToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CopyToolStripMenuItem.Click
        TxtEdit.Copy()
    End Sub

    Private Sub PasteToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PasteToolStripMenuItem.Click
        TxtEdit.Paste()
        ProjectCurrentlySaved = False
    End Sub

    Private Sub SelectAllToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SelectAllToolStripMenuItem.Click
        TxtEdit.SelectAll()
    End Sub

    Private Sub UndoToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles UndoToolStripMenuItem.Click
        TxtEdit.Undo()
        ProjectCurrentlySaved = False
    End Sub

    Private Sub RedoToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RedoToolStripMenuItem.Click
        TxtEdit.Redo()
        ProjectCurrentlySaved = False
    End Sub

    Private Sub FindToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles FindToolStripMenuItem.Click
        Try
            If (Find.Enabled = False) Then
                'Lame method
                Find = New frmFind(Me.TxtEdit)
                Find.Show(Me)
                Find.Location = New Point((Me.Location.X + Me.Width) / 2 - (Me.Find.Width / 2), (Me.Location.Y + Me.Height) / 2 - (Find.Height / 2))
            End If
        Catch ex As Exception
            Find = New frmFind(Me.TxtEdit)
            Find.Show(Me)
            Find.Location = New Point((Me.Location.X + Me.Width) / 2 - (Me.Find.Width / 2), (Me.Location.Y + Me.Height) / 2 - (Find.Height / 2))
        End Try
        GC.Collect()
    End Sub

    Private Sub ReplaceToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ReplaceToolStripMenuItem.Click
        Try
            If (Replace.Enabled = False) Then
                Replace = New frmReplace(Me.TxtEdit)
                Replace.Show(Me)
                Replace.Location = New Point((Me.Location.X + Me.Width) / 2 - (Me.Replace.Width / 2), (Me.Location.Y + Me.Height) / 2 - (Replace.Height / 2))
            End If
        Catch ex As Exception
            Replace = New frmReplace(Me.TxtEdit)
            Replace.Show(Me)
            Replace.Location = New Point((Me.Location.X + Me.Width) / 2 - (Me.Replace.Width / 2), (Me.Location.Y + Me.Height) / 2 - (Replace.Height / 2))
        End Try
        'Dim Replace As New frmReplace(Me.TxtEdit)
        'Replace.Show(Me)
        GC.Collect()
    End Sub

    Private Sub GotoToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GotoToolStripMenuItem.Click
        Try
            If (GotoLine.Enabled = False) Then
                GotoLine = New GotoLine(Me.TxtEdit)
                GotoLine.Show(Me)
                GotoLine.Location = New Point((Me.Location.X + Me.Width) / 2 - (Me.GotoLine.Width / 2), (Me.Location.Y + Me.Height) / 2 - (GotoLine.Height / 2))
            End If
        Catch ex As Exception
            GotoLine = New GotoLine(Me.TxtEdit)
            GotoLine.Show(Me)
            GotoLine.Location = New Point((Me.Location.X + Me.Width) / 2 - (Me.GotoLine.Width / 2), (Me.Location.Y + Me.Height) / 2 - (GotoLine.Height / 2))
        End Try
        GC.Collect()
    End Sub

    Private Sub CompileToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CompileToolStripMenuItem.Click
        If (Me.VBNETCompile(False) = True) Then
            Me.UserInformationBar.Text = "Build Completed."
        Else
            Me.UserInformationBar.Text = "Build Failed."
        End If
    End Sub

    Private Sub ExecuteToolStripMenuItem_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ExecuteToolStripMenuItem.Click
        If (VBNETCompile(, , True) = True) Then
            Me.UserInformationBar.Text = "Build Completed."
        Else
            Me.UserInformationBar.Text = "Build Failed."
        End If
    End Sub

    Private Sub SaveAllToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SaveAllToolStripMenuItem.Click
        SaveAllFiles()
    End Sub

    Function ProximityPlusOrMinus(ByVal Location1 As Integer, ByVal Location2 As Integer, ByVal HowClose As Integer) As Boolean
        ProximityPlusOrMinus = False
        If (Location1 + HowClose > Location2) Then
            If (Location2 >= Location1) Then
                ProximityPlusOrMinus = True
            End If
        End If
        If (Location1 - HowClose < Location2) Then
            If (Location2 <= Location1) Then
                ProximityPlusOrMinus = True
            End If
        End If
    End Function

    Private Sub SlickTestDev_SizeChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.SizeChanged
        CompilerErrorsResize()
    End Sub

    Private Sub AboutToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AboutToolStripMenuItem.Click
        ShowAbout()
    End Sub

    Private Sub StopToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles StopToolStripMenuItem.Click
        StopExecutable()
    End Sub

    Private Sub SpyToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SpyToolStripMenuItem.Click, ObjectSpyToolStripMenuItem.Click
        ObjectSpy()
    End Sub

    Private Sub Record()
        If (Not Me.ObjSpy Is Nothing) Then
            ObjSpy.TurnOffAndHide()
        End If
        If (vbc.IsRunning = False) Then
            Me.WindowState = FormWindowState.Minimized 'Get this out of the way...
            If (RecorderForm.IsDisposed = True) Then RecorderForm = New Recorder(Keys)
            RecorderForm.Show()
            'start recorder...
        End If
    End Sub

    Private Sub ProjectToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ProjectToolStripMenuItem.Click
        ProjectSelectionForm = New ProjectSelect(Me)
        ProjectSelectionForm.BrowseButton.Hide()
        ProjectSelectionForm.Show(Me)
    End Sub

    Private Sub ProjectSettingsToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ProjectSettingsToolStripMenuItem.Click
        Dim PS As New frmProjectSettings(Me.Project)
        PS.ShowDialog(Me)
        GC.Collect()
    End Sub

#Region "Correct Tabs"
    Private Function GetNonCommentedText(ByVal NonQuoteText As String) As String
        If (NonQuoteText.Contains("'") = True) Then
            Dim NonCommentedText As String = String.Empty
            For Each chr As Char In Text.ToCharArray()
                If (chr = "'"c) Then Return NonCommentedText
                NonCommentedText += chr
            Next
        End If
        Return NonQuoteText
    End Function

    Private Function GetNonStringText(ByVal Text As String) As String
        If (Text.Contains("""") = False) Then Return Text
        Dim NonStrText As String = String.Empty
        Dim QuoteCount As Int32 = 0

        For Each chr As Char In Text.ToCharArray()
            If (chr = """"c) Then
                QuoteCount += 1
            End If
            If ((QuoteCount Mod 2) = 0) Then
                If (chr <> """"c) Then NonStrText += chr
            End If
        Next
        Return NonStrText
    End Function

    Private Function GetNonCommentedAndStringText(ByVal Text As String) As String
        Return GetNonCommentedText(GetNonStringText(Text))
    End Function

    Private Function TrimTab(ByVal Tabs As String) As String
        If (Tabs.Length = 0) Then Return Tabs
        Return Tabs.Substring(0, Tabs.Length - 1)
    End Function

    'Private Sub CorrectTabsToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CorrectTabsToolStripMenuItem.Click
    '    If (System.Windows.Forms.MessageBox.Show( _
    '    "In some cases this may cause text to be formatted incorrectly.  " & _
    '    "You will not be able to undo this action.  This may take some time to complete." & _
    '    vbNewLine & vbNewLine & "Do you wish to continue?" _
    '    , MsgBoxTitle, MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) = Windows.Forms.DialogResult.No) Then
    '        Return
    '    End If
    '    Dim Doc As New System.Text.StringBuilder(Me.TxtEdit.Document.TextLength)
    '    Dim TabsCount As Int32 = 0
    '    Dim Tabs As String = String.Empty
    '    Dim Remove() As Char = (vbTab & " " & Chr(10) & Chr(13)).ToCharArray()
    '    Dim TempLine As String
    '    Dim Text() As String = Me.TxtEdit.Document.GetText(0, Me.TxtEdit.Document.TextLength).Split(Chr(10))
    '    For Each line As String In Text
    '        TempLine = line
    '        If (TempLine.StartsWith("'") = False AndAlso TempLine.StartsWith("REM") = False) Then
    '            TempLine = line.TrimStart(Remove)
    '            TempLine = TempLine.ToUpper()
    '            TempLine = GetNonCommentedAndStringText(TempLine)
    '            If (TempLine.StartsWith("IMPORTS") = True) Then
    '                TabsCount = 0
    '            Else
    '                TabsCount = TabsCount
    '            End If
    '            If (TempLine.Contains("LOOP WHILE(")) Then
    '                TempLine = TempLine
    '            End If
    '            If (TempLine.Replace(" ", "") Like "*PUBLICCLASS*") Then
    '                TabsCount += 1
    '            End If
    '            If (TempLine.Replace(" ", "") Like "*PROTECTEDCLASS*") Then
    '                TabsCount += 1
    '            End If
    '            If (TempLine.Replace(" ", "") Like "*PRIVATECLASS*") Then
    '                TabsCount += 1
    '            End If
    '            If (TempLine.Replace(" ", "") Like "*FRIENDCLASS*") Then
    '                TabsCount += 1
    '            End If
    '            If (TempLine Like "*FUNCTION *(*)*") Then
    '                TabsCount += 1
    '            End If
    '            If (TempLine Like "*SUB *(*)*") Then
    '                TabsCount += 1
    '            End If
    '            If (TempLine.Replace(" ", "") Like "*DOWHILE*") Then
    '                TabsCount += 1
    '            ElseIf (TempLine.Replace(" ", "") Like "*DOUNTIL*") Then
    '                TabsCount += 1
    '            ElseIf (TempLine Like "*DO*") Then
    '                If (TempLine.Trim(Remove) = "DO") Then TabsCount += 1
    '            End If
    '        End If
    '        If (TempLine.Contains("TRY")) Then
    '            TabsCount += 1
    '        End If
    '        If (TempLine.Replace(" ", "") Like "*IFTHEN*") Then
    '            TabsCount += 1
    '        End If
    '        If (TempLine.Replace(" ", "") Like "*REGION*") Then
    '            If (TempLine.Contains("#") = True) Then Tabs = String.Empty
    '            line = line.TrimStart(Remove)
    '        End If
    '        If (TempLine.Replace(" ", "") Like "*FOREACH*") Then
    '            TabsCount += 1
    '        End If
    '        If (TempLine Like "*WHILE*") Then
    '            If (TempLine.Replace(" ", "") Like "*ENDWHILE*") Then
    '                TabsCount -= 1
    '            Else
    '                If Not (TempLine.Replace(" ", "") Like "*LOOPWHILE*") Then
    '                    TabsCount += 1
    '                End If
    '            End If
    '        End If
    '        If (TempLine.Replace(" ", "") Like "*FORTO*") Then
    '            TabsCount += 1
    '        End If
    '        If (TempLine.Replace(" ", "") Like "*IFTHEN*") Then
    '            If (TempLine.Substring(TempLine.LastIndexOf("THEN") + 4).TrimEnd(Remove) = "") Then
    '                TabsCount += 1
    '            End If
    '        End If
    '        If (TempLine.Replace(" ", "") Like "*LOOPWHILE*") Then
    '            Tabs = TrimTab(Tabs)
    '            TabsCount -= 1
    '        ElseIf (TempLine.Replace(" ", "") Like "*LOOPUNTIL*") Then
    '            Tabs = TrimTab(Tabs)
    '            TabsCount -= 1
    '        ElseIf (TempLine Like "*LOOP*") Then
    '            If (TempLine.Trim(Remove) = "LOOP") Then
    '                Tabs = TrimTab(Tabs)
    '                TabsCount -= 1
    '            End If
    '        End If
    '        If (TempLine.Replace(" ", "") Like "*ENDIF*") Then
    '            Tabs = TrimTab(Tabs)
    '            TabsCount -= 1
    '        End If
    '        If (TempLine.Contains("CATCH")) Then
    '            Tabs = TrimTab(Tabs)
    '            TabsCount -= 1
    '        End If
    '        If (TempLine.Replace(" ", "") Like "*ENDFUNCTION*") Then
    '            Tabs = TrimTab(Tabs)
    '            TabsCount -= 1
    '        End If
    '        If (TempLine Like "*NEXT*") Then
    '            Tabs = TrimTab(Tabs)
    '            TabsCount -= 1
    '        End If
    '        If (TempLine.Replace(" ", "") Like "*ENDSUB*") Then
    '            Tabs = TrimTab(Tabs)
    '            TabsCount -= 1
    '        End If
    '        If (TempLine.Replace(" ", "") Like "*ENDCLASS*") Then
    '            Tabs = TrimTab(Tabs)
    '            TabsCount -= 1
    '        End If
    '        If (TempLine.Replace(" ", "") Like "*END?REGION*") Then
    '            If (TempLine.Contains("#") = True) Then Tabs = String.Empty
    '            line = line.TrimStart(Remove)
    '        End If
    '        If (TabsCount > 0) Then
    '            line = line.TrimStart(Remove)
    '        End If
    '        If (Doc.Length = 0) Then
    '            Doc.Append(Tabs & line)
    '        Else
    '            If (line.StartsWith(Chr(10)) = False) Then
    '                Doc.Append(Chr(10) & Tabs & line)
    '            Else
    '                Doc.Append(Tabs & line)
    '            End If
    '        End If

    '        Tabs = String.Empty
    '        For i As Integer = 0 To TabsCount - 1
    '            Tabs += vbTab
    '        Next
    '    Next
    '    Me.TxtEdit.Text = Doc.ToString()
    'End Sub
#End Region

#End Region

#Region "File TreeView"

    Private Sub FileTreeView_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles FileTreeView.KeyDown
        SelectedFileItemInTreeView()
    End Sub

    Private Sub FileTreeView_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles FileTreeView.DoubleClick
        Try
            If (FileTreeView.SelectedNode.Parent IsNot Nothing) Then
                If (FileTreeView.SelectedNode.Parent.Text = "Files") Then
                    Dim file As String = FileTreeView.SelectedNode.Text
                    If (file.ToLower.EndsWith(LanguageExt) = False) Then
                        file += LanguageExt
                    End If
                    Me.UserInformationBar.Text = "Loading " & file & "..."
                    IgnoredSingleTxtChange = True
                    OpenFileDynamically(file)
                    Me.UserInformationBar.Text = "File loaded."
                    'AddFiles(Me.TxtEdit.Text, CurrentlyOpenedFile, CurrentlySaved)
                    'CurrentlyOpenedFile = FileTreeView.SelectedNode.Text
                    'CurrentlySaved = True
                    'If (System.IO.File.Exists(Project.SourceFileLocation & CurrentlyOpenedFile) = False) Then
                    '    WriteFile(Project.SourceFileLocation & CurrentlyOpenedFile, "")
                    'End If
                    'If (Files.ContainsKey(ConvertFilesKey(CurrentlyOpenedFile)) = False) Then
                    '    AddFiles(ReadFile(Project.SourceFileLocation & CurrentlyOpenedFile), CurrentlyOpenedFile, True)
                    '    Me.TxtEdit.Text = Files(ConvertFilesKey(CurrentlyOpenedFile)).FileText
                    'Else
                    '    Me.TxtEdit.Text = Files(ConvertFilesKey(CurrentlyOpenedFile)).FileText
                    'End If
                    'Me.TxtEdit.Refresh()
                    'Me.Text = TitleText & " - " & Project.ProjectName & " - " & CurrentlyOpenedFile
                End If
            End If
        Catch ex As Exception
            System.Windows.Forms.MessageBox.Show(ex.ToString())
        Finally
            IgnoredSingleTxtChange = False
        End Try
    End Sub

    Private Sub FileTreeView_MouseClick(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles FileTreeView.MouseClick
        If (e.Button = Windows.Forms.MouseButtons.Right) Then
            'make right clicking work correctly.
            Dim node As TreeNode = FileTreeView.GetNodeAt(New Point(e.X, e.Y))
            If (node IsNot Nothing) Then
                FileTreeView.SelectedNode = node
                FileTreeView.ContextMenuStrip = ContextMenuAddFiles
            End If
        End If
        SelectedFileItemInTreeView()
    End Sub

    Private Sub SelectedFileItemInTreeView()
        If (FileTreeView.SelectedNode Is Nothing) Then
            Return
        End If
        'we are actually selecting DLL or Files.
        If (FileTreeView.SelectedNode.Text = "DLL") Then
            Me.ContextMenuAddFiles.Items.Clear()
            AddFileToolStripMenuItem.Text = "Add DLL"
            Me.ContextMenuAddFiles.Items.Add(AddFileToolStripMenuItem)
            Return
        ElseIf (FileTreeView.SelectedNode.Text = "Files") Then
            Me.ContextMenuAddFiles.Items.Clear()
            AddFileToolStripMenuItem.Text = "Add File"
            Me.ContextMenuAddFiles.Items.Add(AddFileToolStripMenuItem)
            Return
        ElseIf (FileTreeView.SelectedNode.Text = "User Libraries") Then
            Me.ContextMenuAddFiles.Items.Clear()
            AddFileToolStripMenuItem.Text = "Add Library"
            Me.ContextMenuAddFiles.Items.Add(AddFileToolStripMenuItem)
            Return
        End If
        Select Case FileTreeView.SelectedNode.Parent.Text
            Case "DLL"
                Me.ContextMenuAddFiles.Items.Clear()
                AddFileToolStripMenuItem.Text = "Add DLL"
                Me.ContextMenuAddFiles.Items.Add(AddFileToolStripMenuItem)
                RemoveFileToolStripMenuItem.Text = "Remove DLL"
                Me.ContextMenuAddFiles.Items.Add(RemoveFileToolStripMenuItem)
            Case "Files"
                Me.ContextMenuAddFiles.Items.Clear()
                AddFileToolStripMenuItem.Text = "Add File"
                Me.ContextMenuAddFiles.Items.Add(AddFileToolStripMenuItem)
                RemoveFileToolStripMenuItem.Text = "Delete File"
                Me.ContextMenuAddFiles.Items.Add(RemoveFileToolStripMenuItem)
                OpenFileToolStripMenuItem.Text = "Open File"
                Me.ContextMenuAddFiles.Items.Add(OpenFileToolStripMenuItem)
                Me.RenameFileToolStripMenuItem.Text = "Rename File"
                Me.ContextMenuAddFiles.Items.Add(RenameFileToolStripMenuItem)
            Case "User Libraries"
                Me.ContextMenuAddFiles.Items.Clear()
                AddFileToolStripMenuItem.Text = "Add Library"
                Me.ContextMenuAddFiles.Items.Add(AddFileToolStripMenuItem)
                RemoveFileToolStripMenuItem.Text = "Remove Library"
                Me.ContextMenuAddFiles.Items.Add(RemoveFileToolStripMenuItem)
        End Select
    End Sub

    Private Sub RenameFileToolStripMenuItem_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles RenameFileToolStripMenuItem.Click
        Dim NewFileName As String = String.Empty
        Try
            If (FileTreeView.SelectedNode.Parent.Text = "Files") Then
                NewFileName = InputBox("WARNING: This will automatically save the file.  What would you like to name the file?", MsgBoxTitle, FileTreeView.SelectedNode.Text)
                If (NewFileName = "") Then Return 'pressed cancel
                If (NewFileName.ToUpper() = FileTreeView.SelectedNode.Text.ToUpper()) Then Return 'not renaming
                If (NewFileName.StartsWith(".")) Then
                    System.Windows.Forms.MessageBox.Show( _
                    "Filename must be more than just a file extention.  " & _
                    "Operation aborted.", MsgBoxTitle, MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Return
                End If
                If (NewFileName.ToLower().EndsWith(LanguageExt) = False) Then
                    NewFileName += LanguageExt
                End If
                If (System.IO.File.Exists(Project.SourceFileLocation & NewFileName) = True) Then
                    System.Windows.Forms.MessageBox.Show("File already exists.  Operation aborted.", MsgBoxTitle, MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Return
                End If
                If (Project.BuildFiles.Contains(FileTreeView.SelectedNode.Text) = True) Then
                    Project.BuildFiles.Remove(FileTreeView.SelectedNode.Text)
                    Project.BuildFiles.Add(NewFileName)
                End If
                If (Me.Files.ContainsKey(Me.ConvertFilesKey(FileTreeView.SelectedNode.Text)) = True) Then
                    Me.SaveFile(Me.Files(Me.ConvertFilesKey(FileTreeView.SelectedNode.Text)).FileText, Project.SourceFileLocation & FileTreeView.SelectedNode.Text)
                End If

                System.IO.File.Move(Project.SourceFileLocation & FileTreeView.SelectedNode.Text, Project.SourceFileLocation & NewFileName)
                Dim CRC As String = Me.GetCRC(Project.SourceFileLocation & NewFileName)
                If (Me.Files.ContainsKey(FileTreeView.SelectedNode.Text)) Then
                    If (Me.CurrentlyOpenedFile = FileTreeView.SelectedNode.Text) Then
                        Me.AddFiles(Me.Files(FileTreeView.SelectedNode.Text).FileText, NewFileName, Me.ProjectCurrentlySaved, CRC)
                        Me.CurrentlyOpenedFile = NewFileName
                    Else
                        Me.AddFiles(Me.Files(FileTreeView.SelectedNode.Text).FileText, NewFileName, Me.Files(FileTreeView.SelectedNode.Text).IsSaved, CRC)
                    End If
                    Me.Files.Remove(FileTreeView.SelectedNode.Text)
                End If

                FileTreeView.SelectedNode.Text = NewFileName 'Do this last
            End If
        Catch ex As Exception
            System.Windows.Forms.MessageBox.Show( _
            "Unable to rename file '" & Project.SourceFileLocation & FileTreeView.SelectedNode.Text & "'.")
        End Try
    End Sub

    Private Sub AddFileToolStripMenuItem_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles AddFileToolStripMenuItem.Click
        If (AddFileToolStripMenuItem.Text.Contains("DLL") = True) Then
            Me.FileTreeView.SelectedNode = FileTreeView.Nodes("DLLNode")
            Dim AddDLLs As New AddDLLs()
            AddDLLs.Exclude(Project.Assemblies.ToArray)
            AddDLLs.Exclude(Project.SpecialAssemblies.ToArray)
            AddDLLs.ShowDialog()
            For Each file As String In AddDLLs.FilesAdded()
                If (Project.Assemblies.Contains(file) = False) Then
                    Try
                        Me.FileTreeView.SelectedNode.Nodes.Add(New TreeNode(System.IO.Path.GetFileNameWithoutExtension(file) & _
                        " - " & System.Reflection.AssemblyName.GetAssemblyName(file).Version.ToString()))
                        Project.AddAsm(file)
                        SyncLock (AddedAsms)
                            AddedAsms.Add(file)
                        End SyncLock
                        ProjectCurrentlySaved = False
                        Me.AddAssemblies()
                    Catch ex As Exception
                        System.Windows.Forms.MessageBox.Show( _
                        "Failed to add an Assembly.  Error Details:" & vbNewLine & ex.ToString(), MsgBoxTitle, MessageBoxButtons.OK, MessageBoxIcon.Error)
                    End Try
                End If
            Next
        ElseIf (AddFileToolStripMenuItem.Text.Contains("File") = True) Then 'must be a file
            AddFileToProject()
        Else 'library based DLLs
            OpenFileDialog1.DefaultExt = "dll"
            OpenFileDialog1.Filter = "Library Files|*.dll"
            Dim InitLibLocation As String = My.Settings.Project_Library__Location
            'Application.StartupPath.ToLower()
            'If (InitLibLocation.EndsWith("\") = False) Then InitLibLocation += "\"
            'InitLibLocation += "Library\"
            OpenFileDialog1.InitialDirectory = InitLibLocation
            If (OpenFileDialog1.ShowDialog() <> Windows.Forms.DialogResult.OK) Then
                Return
            Else
                If (System.IO.Path.GetDirectoryName(OpenFileDialog1.FileName).ToUpperInvariant().Contains(InitLibLocation.ToUpperInvariant().TrimEnd("\")) = False) Then
                    System.Windows.Forms.MessageBox.Show( _
                    "Library DLLs must be in the folder: '" & InitLibLocation & _
                    "'.  You can change this directory by changing the 'Library Location' in options.", _
                    MsgBoxTitle, MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Return
                End If
                If (System.IO.File.Exists(OpenFileDialog1.FileName) = False) Then
                    System.Windows.Forms.MessageBox.Show( _
                    "Unable to locate file: " & OpenFileDialog1.FileName, _
                    MsgBoxTitle, MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Return
                End If
                If (OpenFileDialog1.FileName.ToLower.EndsWith(".dll") = False) Then
                    System.Windows.Forms.MessageBox.Show( _
                    "File '" & System.IO.Path.GetFileName(OpenFileDialog1.FileName) & _
                    "' does not appear to be a DLL.", MsgBoxTitle, MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Return
                End If
                Dim Name As String = System.IO.Path.GetFileNameWithoutExtension(OpenFileDialog1.FileName)
                For Each Node As System.Windows.Forms.TreeNode In FileTreeView.Nodes("UserLibs").Nodes
                    If (Node.Text.ToUpper() = Name.ToUpper()) Then
                        System.Windows.Forms.MessageBox.Show( _
                        "DLL with the same name already added.", MsgBoxTitle, _
                        MessageBoxButtons.OK, MessageBoxIcon.Error)
                        Return
                    End If
                Next
                FileTreeView.Nodes("UserLibs").Nodes.Add(Name)
                Me.Project.AddUserAsm(OpenFileDialog1.FileName)
                ProjectCurrentlySaved = False
                SyncLock (AddedAsms)
                    AddedAsms.Add(OpenFileDialog1.FileName)
                End SyncLock
                Me.AddAssemblies()
            End If
        End If
    End Sub

    Private Sub OpenFileToolStripMenuItem_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles OpenFileToolStripMenuItem.Click
        Try
            If (FileTreeView.SelectedNode.Parent.Text = "Files") Then
                Dim file As String = FileTreeView.SelectedNode.Text
                If (file.ToLower.EndsWith(LanguageExt) = False) Then
                    file += LanguageExt
                End If
                OpenFileDynamically(file)
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub RemoveFileToolStripMenuItem_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles RemoveFileToolStripMenuItem.Click
        If (RemoveFileToolStripMenuItem.Text.Contains("DLL") = True) Then
            Try
                Dim AsmName As String = FileTreeView.SelectedNode.Text.Split(" - ")(0).ToLower()
                AsmName += ".dll"
                For Each asm As String In Project.Assemblies
                    If (asm.ToLower().EndsWith(AsmName) = True) Then
                        Project.Assemblies.Remove(asm)
                        ''''''''
                        Me.AddedAsms.Remove(asm)
                        Me.AddAssemblies()
                        ''''''''''
                        ProjectCurrentlySaved = False
                        Exit For
                    End If
                Next
                FileTreeView.SelectedNode.Remove()
            Catch ex As Exception
            End Try
        ElseIf (RemoveFileToolStripMenuItem.Text.Contains("File") = True) Then 'must be a file
            If (FileTreeView.Nodes("FileNameNode").Nodes.Count <= 1) Then
                System.Windows.Forms.MessageBox.Show( _
                "You cannot delete the last file in the project.  " & _
                "If you wish to rename the file, please choose 'Rename File'.", _
                MsgBoxTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Return
            End If
            Try
                Dim FileName As String = FileTreeView.SelectedNode.Text.ToLower()
                For Each file As String In Project.BuildFiles
                    If (file.ToLower().EndsWith(FileName) = True) Then
                        Project.BuildFiles.Remove(file)
                        RemoveFiles(file)
                        'CurrentlySaved = False'Deleting files doesn't really change the project.
                        Exit For
                    End If
                Next
                FileTreeView.SelectedNode.Remove()
                If (FileName.EndsWith(LanguageExt) = False) Then FileName += LanguageExt
                Debug.WriteLine("About to delete: " & Project.SourceFileLocation & FileName)
                System.IO.File.Delete(Project.SourceFileLocation & FileName)
                'Now we need to open a new file up if and only if the current file is the file deleted.
                If (FileName.ToUpper() = CurrentlyOpenedFile.ToUpper()) Then
                    OpenFileDynamically(Project.BuildFiles.Item(0))
                End If
            Catch ex As Exception
            End Try

        Else 'User DLL
            If (FileTreeView.SelectedNode.Parent IsNot Nothing) Then
                Try
                    Dim AsmName As String = FileTreeView.SelectedNode.Text.ToLower()
                    If (AsmName.EndsWith(".dll") = False) Then AsmName += ".dll"
                    For Each asm As String In Project.UserAssemblies
                        If (asm.ToLower().EndsWith(AsmName) = True) Then
                            Project.RemoveUserAsm(asm)
                            ''''''''
                            Me.AddedAsms.Remove(asm)
                            Me.AddAssemblies()
                            ''''''''''''
                            ProjectCurrentlySaved = False
                            Exit For
                        End If
                    Next
                    FileTreeView.SelectedNode.Remove()
                Catch ex As Exception
                End Try
            End If
        End If
    End Sub

#End Region

#Region "SQL Stuff"
    Private Function GetSQLRunName() As String
        If (Project.IsOfficialRun = True) Then
            Return "ExternalReportRun"
        Else
            Return "ReportRun"
        End If
    End Function

    Private Function GetSQLRunLineName() As String
        If (Project.IsOfficialRun = True) Then
            Return "ExternalReportLine"
        Else
            Return "ReportLine"
        End If
    End Function

    Private Function GetTime() As String
        Return Now.ToString("yyyy-MM-dd HH:mm:ss")
    End Function

#End Region

#Region "Building and Running"

    Private Sub RunToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RunToolStripMenuItem.Click
        BuildAndRun()
    End Sub

    Private Sub BuildAndRun()
        If (Me.VBNETCompile() = True) Then
            Me.UserInformationBar.Text = "Build Completed."
        Else
            Me.UserInformationBar.Text = "Build Failed."
        End If
    End Sub

    Private Function VBNETCompile(Optional ByVal Run As Boolean = True, Optional ByVal Args As String = "", Optional ByVal ForceCompile As Boolean = False) As Boolean
        Me.UserInformationBar.Text = "Build Started..."
        Dim DoCompile As Boolean = ForceCompile
        If (vbc.IsRunning = True) Then Return False
        If (ProjectCurrentlySaved = False) Then
            DoCompile = True
        End If
        If (IsFirstCompile = True) Then
            IsFirstCompile = False
            DoCompile = True
        End If
        If (DoCompile = False) Then
            For Each fi As FileInfo In Me.Files.Values
                If (fi.IsSaved = False) Then
                    DoCompile = True
                    Exit For
                End If
            Next
        End If

        If (SaveProject(False, True) = Windows.Forms.DialogResult.None) Then 'saves the project files, including the compiled files.
            System.Windows.Forms.MessageBox.Show("Unable to save one or more the files for compiling.", _
            MsgBoxTitle, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        End If
        Dim Path As String = Application.StartupPath
        If (Path.EndsWith("\") = False) Then Path += "\"
        Dim CopyFrom As String
        Dim reply As System.Windows.Forms.DialogResult
        vbc.Reset()
        If (Project.UserAssemblies.Count <> 0) Then
            DoCompile = True
            For Each asm As String In Project.UserAssemblies
                CopyFrom = Path & "Library\" & System.IO.Path.GetFileName(asm)
                Dim CopyTo As String = Project.OutputFolder
                If (CopyTo.EndsWith("\") = False) Then
                    CopyTo += "\"
                End If
                CopyTo += System.IO.Path.GetFileName(asm)
                If (System.IO.File.Exists(CopyFrom) = False) Then
                    Do
                        If (System.IO.File.Exists(CopyFrom) = False) Then
                            Dim ErrorMsg As String = "Unable to find the assembly: " & CopyFrom & _
                            " for compiling."
                            If (System.IO.File.Exists(asm) = True) Then
                                ErrorMsg += " Ignore this error if you would like to use the Assembly currently located in the project."
                            End If

                            reply = System.Windows.Forms.MessageBox.Show(ErrorMsg, _
                                                MsgBoxTitle, _
                                                MessageBoxButtons.AbortRetryIgnore, _
                                                MessageBoxIcon.Error)
                        Else
                            Exit Do 'no error condition anymore.
                        End If
                    Loop While (reply = Windows.Forms.DialogResult.Retry)
                    If (reply = Windows.Forms.DialogResult.Abort) Then Return False
                Else
                    Try
                        System.IO.File.Copy(CopyFrom, CopyTo, True)
                        'System.IO
                        vbc.IncludedAssemblies.Add(CopyTo)
                    Catch ex As Exception
                        reply = System.Windows.Forms.MessageBox.Show( _
                        ex.Message & "  Would you like to continue?", MsgBoxTitle, _
                        MessageBoxButtons.OKCancel)
                        If (reply <> Windows.Forms.DialogResult.OK) Then
                            Return False
                        End If
                    End Try
                End If
            Next
        End If
        vbc.StartingClass = Project.ClassName
        vbc.ShowConsoleUI = Project.ShowUI
        vbc.ExecutableName = Project.ExecuteFileName
        vbc.ExecutablePath = Project.OutputFolder
        vbc.OptionExplicit = Project.OptionExplicit
        vbc.OptionStrict = Project.OptionStrict
        vbc.CompileWithDebugInformation = Project.CompileAsDebug 'testing only
        vbc.CreateDLL = (Project.ProjectType = CurrentProjectData.ProjectTypes.LibraryProject)

        If (vbc.CompileMethod = DOTNETCompiler.CompileType.FromFiles) Then
            'If (Me.WriteFile(Project.SourceFileLocation & DefaultFileName, Me.TxtEdit.Text) = False) Then
            '    Return False
            'End If
        Else
            vbc.SourceCode.Add(TxtEdit.Text)
        End If
        For Each file As String In Project.BuildFiles
            If (System.IO.File.Exists(Project.SourceFileLocation & file) = False) Then
                'do something?
            Else
                vbc.FilePaths.Add(Project.SourceFileLocation & file)
            End If
        Next
        vbc.TreatWarningsAsErrors = False
        For Each asm As String In Project.Assemblies
            If (asm.Contains(";") = True) Then
                asm = asm.Split(";"c)(1) 'gets the file path part of the DLL
            End If
            vbc.IncludedAssemblies.Add(asm)
        Next
        Dim asm1 As String
        For i As Integer = 0 To Project.SpecialAssemblies.Count - 1
            asm1 = Project.SpecialAssemblies.Item(i)
            If (System.IO.File.Exists(asm1) = False) Then
                'Dim path As String
                Path = System.Windows.Forms.Application.StartupPath
                If (Path.EndsWith("\") = False) Then Path += "\"
                Path += System.IO.Path.GetFileName(asm1)
                If (System.IO.File.Exists(Path) = False) Then
                    Dim ErrorMsg As String = "Unable to find the assembly: " & Path & _
                    " for compiling."
                    Do
                        reply = System.Windows.Forms.MessageBox.Show(ErrorMsg, _
                                            MsgBoxTitle, MessageBoxButtons.AbortRetryIgnore, MessageBoxIcon.Error)
                    Loop While (reply = Windows.Forms.DialogResult.Retry)
                    If (reply = Windows.Forms.DialogResult.Abort) Then Return False
                End If
                Project.RemoveSpecialAsm(asm1)
                Project.AddSpecialAsm(Path)
            End If
        Next
        For Each asm As String In Project.SpecialAssemblies
            If (asm.Contains(";") = True) Then
                asm = asm.Split(";"c)(1) 'gets the file path part of the DLL
            End If

            If (System.IO.File.Exists(Project.OutputFolder & System.IO.Path.GetFileName(asm)) = False) Then
                If (System.IO.File.Exists(asm) = False) Then
                    Dim ErrorMsg As String = "Unable to find the assembly: " & asm & _
                        " for compiling.  You may have reinstalled slick test to a new folder location." & _
                        "  Please fix the .stp to reference the correct folder path and try again."
                    System.Windows.Forms.MessageBox.Show(ErrorMsg, MsgBoxTitle, _
                                                         MessageBoxButtons.OK, MessageBoxIcon.Error)

                    Return False
                End If
                System.IO.File.Copy(asm, Project.OutputFolder & System.IO.Path.GetFileName(asm))
            End If
            vbc.IncludedAssemblies.Add(Project.OutputFolder & System.IO.Path.GetFileName(asm))
        Next

        If (vbc.CreateDLL) Then Run = False 'You can't run a DLL.
        'vbc.IncludedAssemblies.Add(My.Settings.Sample_Interaction__DLL__Location)
        Try
            If (Run = True) Then
                If (My.Settings.Reporter_Show__Report = True) Then
                    Me.ResultsTimer.Enabled = True
                    Me.ResultsTimer.Start()
                End If
            End If
            If (Run = True) Then DoCompile = True
            If (DoCompile = False) Then
                If (Not System.IO.File.Exists(vbc.ExecutableFilePath)) Then
                    DoCompile = True
                End If
            End If
            If (DoCompile = True) Then
                vbc.Compile(Run, Args)
            End If

        Catch ex As Exception 'probably a previously running process.
            vbc.Reset()
            System.Windows.Forms.MessageBox.Show(ex.Message.ToString(), MsgBoxTitle)
            Return False
        End Try

        If (vbc.Errors.Count <> 0) Then
            Dim dt As New DataTable
            dt.Columns.Add("Number", GetType(Integer))
            dt.Columns.Add("Description", GetType(String))
            dt.Columns.Add("File", GetType(String))
            dt.Columns.Add("Line", GetType(Integer))
            Dim dr As DataRow
            Me.CompilerIssueDataGridView.DataSource = dt
            Dim ErrorNumber As Integer = 1
            For Each errorMsg As System.CodeDom.Compiler.CompilerError In vbc.ErrorResults
                dr = dt.NewRow
                dr("Number") = ErrorNumber
                dr("Description") = errorMsg.ErrorText
                dr("File") = System.IO.Path.GetFileNameWithoutExtension(errorMsg.FileName.Replace(" : ", ""))
                dr("Line") = errorMsg.Line
                dt.Rows.Add(dr)
                ErrorNumber += 1
            Next
            CompilerIssueDataGridView.Sort(CompilerIssueDataGridView.Columns(0), System.ComponentModel.ListSortDirection.Descending)
            CompilerIssueDataGridView.Size = New System.Drawing.Size(CompilerIssueDataGridView.Size.Width, Math.Min(((CompilerIssueDataGridView.Rows(0).Height) * vbc.Errors.Count) + CompilerIssueDataGridView.Rows(0).HeaderCell.Size.Height, 250))
            CompilerErrorsResize()
            Return False
        End If
        If (My.Settings.Project_Library__Projects__Auto__Copied = True) Then
            If (Me.Project.ProjectType = CurrentProjectData.ProjectTypes.LibraryProject) Then
                Dim CopyTo As String = My.Settings.Project_Library__Location
                If (Not CopyTo.EndsWith("\")) Then CopyTo += "\"
                CopyTo += vbc.ExecutableName
                Try
                    System.IO.File.Copy(vbc.ExecutableFilePath, _
                           CopyTo, True)
                Catch ex As Exception
                    MessageBox.Show("System successfully compiled the file, but the file '" & _
                                    vbc.ExecutableFilePath & "' was not successfully copied to" & _
                                    " the library folder (" & CopyTo & ").", MsgBoxTitle, _
                                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                End Try
            End If
        End If

        vbc.Reset()
        CompilerIssueDataGridView.Size = New System.Drawing.Size(CompilerIssueDataGridView.Size.Width, 0)
        Return True
    End Function

    Private Sub CompilerIssueDataGridView_CellDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles CompilerIssueDataGridView.CellDoubleClick
        Try
            'Me.Files.Keys
            OpenFileDynamically(Me.CompilerIssueDataGridView.Rows(e.RowIndex).Cells("File").Value.ToString() & LanguageExt, False)
            Me.TxtEdit.Focus()
            Me.TxtEdit.ActiveTextAreaControl.Caret.Line = CType((Me.CompilerIssueDataGridView.Rows(e.RowIndex).Cells("Line").Value - 1), Integer)
        Catch ex As Exception
        End Try
    End Sub

    Private Sub CleanCompileToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CleanCompileToolStripMenuItem.Click
        For Each asm As String In Project.SpecialAssemblies
            If (asm.Contains(";") = True) Then
                asm = asm.Split(";"c)(1) 'gets the file path part of the DLL
            End If
            If (System.IO.File.Exists(Project.OutputFolder & System.IO.Path.GetFileName(asm)) = True) Then
                Try
                    System.IO.File.Delete(Project.OutputFolder & System.IO.Path.GetFileName(asm))
                Catch ex As Exception
                    System.Windows.Forms.MessageBox.Show( _
                    "Unable to clean out compile folder due to the following error: " & _
                    ex.Message.ToString(), MsgBoxTitle, MessageBoxButtons.OK, MessageBoxIcon.Error)
                End Try
            End If
        Next
        If (Me.VBNETCompile(False, , True) = True) Then
            Me.UserInformationBar.Text = "Build Completed."
        Else
            Me.UserInformationBar.Text = "Build Failed."
        End If
    End Sub

    Private Sub vbc_ExecutionComplete(ByVal ExitCode As Integer, ByVal StartDateTime As DateTime) Handles vbc.ExecutionComplete
        Dim LocalConnectionString As String = "Data Source =""" & Project.ReportDatabasePath & """;Max Database Size=4091;"
        Try
            Using connection As New SqlServerCe.SqlCeConnection(LocalConnectionString)
                If Not System.IO.File.Exists(connection.Database) Then
                    Return
                    'Dim engine As New System.Data.SqlServerCe.SqlCeEngine(LocalConnectionString)
                    'engine.CreateDatabase()
                End If
                Dim cmd As New SqlServerCe.SqlCeCommand()
                cmd.Connection = connection
                Try
                    connection.Open()
                Catch ex As Exception
                    If System.IO.File.Exists(connection.Database) Then
                        Dim engine As New System.Data.SqlServerCe.SqlCeEngine(LocalConnectionString)
                        engine.Upgrade()
                    End If
                    connection.Open()
                End Try

                Dim rdr As System.Data.SqlServerCe.SqlCeDataReader
                'LastRunInfo
                cmd.CommandText = "SELECT RunNumber, ProjectName, ProjectGUID," & _
                " RunGUID, StartRunTime FROM LastRunInfo"
                Try
                    rdr = cmd.ExecuteReader()
                Catch ex As Exception
                    Return 'Failed to read, probably an invalid db error
                End Try
                If (rdr.Read() = False) Then
                    Return
                End If
                Dim RunNumber As Integer = rdr.Item("RunNumber")
                Dim ProjectName As String = rdr.Item("ProjectName")
                Dim ProjectGUID As String = rdr.Item("ProjectGUID")
                Dim RunGUID As String = rdr.Item("RunGUID")
                Dim DbStartDateTime As String = rdr.Item("StartRunTime") '9/12/2009 8:58 PM
                Dim ConvertedDateTime As DateTime
                If (String.IsNullOrEmpty(DbStartDateTime)) Then
                    ConvertedDateTime = StartDateTime.AddDays(-1)
                Else
                    ConvertedDateTime = DateTime.Parse(DbStartDateTime)
                End If
                rdr.Close()
                rdr.Dispose()
                If (ConvertedDateTime < StartDateTime.AddSeconds(-1)) Then 'give or take 1 second.
                    'turns out that the last run was not recorded in the database.
                    'this means more than likely someone overrided the system and is using their own
                    'reporting.
                    Return
                End If

                If (ExitCode < 0) Then
                    cmd.Parameters.AddWithValue("@pCurrentRunNumber", RunNumber)
                    cmd.Parameters.AddWithValue("@pTypeOfMessage", Report.Fail)
                    cmd.Parameters.AddWithValue("@pMajorMessage", "Run aborted.")
                    cmd.Parameters.AddWithValue("@pMinorMessage", "")
                    cmd.Parameters.AddWithValue("@pTestNumber", "-1")
                    cmd.Parameters.AddWithValue("@pTestName", "Unknown")
                    cmd.Parameters.AddWithValue("@pStepNumber", "-1")
                    cmd.Parameters.AddWithValue("@pStepName", "Unknown")
                    cmd.Parameters.AddWithValue("@pCurrentLoopInRuns", "-1")
                    cmd.Parameters.AddWithValue("@pRunGUID", System.Data.SqlTypes.SqlGuid.Parse(RunGUID.ToString()).ToString())
                    cmd.Parameters.AddWithValue("@pProjectGUID", System.Data.SqlTypes.SqlGuid.Parse(ProjectGUID.ToString()).ToString())
                    cmd.Parameters.AddWithValue("@pRunLineGUID", System.Data.SqlTypes.SqlGuid.Parse(System.Guid.NewGuid().ToString()).ToString())

                    cmd.CommandText = _
                    "INSERT INTO " & GetSQLRunLineName() & _
                    "([RunNumber],[ReportType],[MajorText],[MinorText],[StepNumber]," & _
                    "[StepName],[TestNumber],[TestName],[LoopRunNumber],[LineRunTime]," & _
                    "[RunGUID],[ProjectGUID],[RunLineGUID]) " & _
                    " VALUES(" & "@pCurrentRunNumber" & _
                    ", " & "@pTypeOfMessage" & ", " & "@pMajorMessage" & ", " & "@pMinorMessage" & _
                    ", " & "@pStepNumber" & " , " & "@pStepName" & ", " & _
                    "@pTestNumber" & ", " & "@pTestName" & ", " & "@pCurrentLoopInRuns" & _
                    ", '" & Me.GetTime() & "', " & "@pRunGUID" & ", " & "@pProjectGUID" & ", " & "@pRunLineGUID" & ");"

                    cmd.ExecuteNonQuery()
                    '''''''''''''''''''''''''''''
                    cmd.Parameters.Clear()
                    'we must update results in DB.
                    cmd.Parameters.AddWithValue("@pRunGUID", System.Data.SqlTypes.SqlGuid.Parse(RunGUID.ToString()).ToString())
                    cmd.Parameters.AddWithValue("@pCurrentRunNumber", RunNumber)
                    cmd.Parameters.AddWithValue("@pTestResults", Report.Fail)

                    cmd.CommandText = "UPDATE " & GetSQLRunName() & " SET ReportType = " & _
                    "@pTestResults" & " WHERE RunNumber = " & "@pCurrentRunNumber" & _
                    " AND RunGUID = " & "@pRunGUID" & ";"

                    cmd.ExecuteNonQuery()
                    ''''''''''''''''''''''''''''
                End If

                cmd.Parameters.Clear()
                cmd.Parameters.Add("@pRunGUID", RunGUID)
                cmd.CommandText = "UPDATE " & GetSQLRunName() & " SET " & _
                        "ReportCompletedTime = '" & GetTime() & "' WHERE " & _
                        "RunGUID = " & "@pRunGUID"
                cmd.ExecuteNonQuery()
                cmd.Dispose()

                connection.Close()
            End Using

        Catch ex As Exception
            MessageBox.Show("System failed to update the last run's " & _
                            "report due to an error.  Error: " & ex.ToString(), _
                            MsgBoxTitle, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

        ExecutionCompleted = True
    End Sub

    Private Sub ResultsTimer_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ResultsTimer.Tick
        If (ExecutionCompleted = True) Then
            ResultsTimer.Stop()
            ResultsTimer.Enabled = False
            ExecutionCompleted = False
            RunReportForm = New ResultsViewer.RunReport(Project.ReportDatabasePath, Project.GUID, Project.IsOfficialRun, ReporterSize, ReporterWindowState)
            Me.AddOwnedForm(RunReportForm)
            RunReportForm.ShowDialog()
        End If
    End Sub

#End Region

#Region "Misc Form Events, External Forms"

    Private Sub Form1_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        If (Me.CurrentlyOpenedFile <> "") Then
            If (SaveProject() = Windows.Forms.DialogResult.Cancel) Then
                e.Cancel = True
                Return
            End If
        End If
        Keys.StopHandlers()
        If (Not ObjSpy Is Nothing) Then
            Me.ObjSpy.Hide()
            Me.ObjSpy = Nothing
        End If
        e.Cancel = False
        My.Settings.FormState = Me.WindowState
        If (Me.WindowState = FormWindowState.Maximized) Then
            Me.WindowState = FormWindowState.Normal
            My.Settings.FormState = FormWindowState.Maximized
        End If
        If (Me.WindowState = FormWindowState.Minimized) Then
            Me.WindowState = FormWindowState.Normal
            My.Settings.FormState = FormWindowState.Minimized
        End If
        My.Settings.FormSize = Me.Size
        My.Settings.FormLocation = Me.Location
        My.Settings.LastProject = Project.LoadLocation
        My.Settings.Save()
        Me.Keys.CloseHandles()

    End Sub

    Private Sub ProjectSelectionForm_ProjectSelect(ByVal Project As CurrentProjectData, ByVal NewProject As Boolean) Handles ProjectSelectionForm.ProjectSelect
        IgnoredSingleTxtChange = True
        Me.TxtEdit.Text = ""
        If (Project.IsDirty = True) Then
            If (SaveProject() = Windows.Forms.DialogResult.Cancel) Then
                Return
            End If
            Me.Project = Project
        Else
            Me.Project = Project
        End If
        If (NewProject) Then
            Me.Files.Clear() 'What if you have the same file name twice.  The add file would conflict.
        End If

        LanguageExt = Project.LanguageExtension 'I hope this is right... worst case, we force VB.NET

        If (CurrentLanguage = CodeTranslator.Languages.CSharp) Then
            vbc = New DOTNETCompiler.CSharpCompiler()
        Else
            LanguageExt = VBLANGUAGEEXT
            vbc = New DOTNETCompiler.VBCompiler()
        End If
        Dim IsVB As Boolean = (LanguageExt = VBLANGUAGEEXT)
        UpdateLanguageForEditor(IsVB) 'Force update, needed or not.

        Dim SourceDirectory As String = System.IO.Path.GetDirectoryName(Project.SourceFileLocation)
        If (System.IO.Directory.Exists(SourceDirectory)) Then
            For Each file As String In System.IO.Directory.GetFiles(SourceDirectory)
                If (file.ToLower.EndsWith(LanguageExt)) Then
                    Me.Project.AddFile(file)
                End If
            Next
        Else
            If (System.Windows.Forms.MessageBox.Show("Unable to find source code folder " & SourceDirectory & ".  Would you like Slick Test to attempt to create the directory? WARNING: By not creating a source directory, you may destablize " & MsgBoxTitle & ".", MsgBoxTitle, MessageBoxButtons.OKCancel, MessageBoxIcon.Error) = Windows.Forms.DialogResult.OK) Then
                System.IO.Directory.CreateDirectory(SourceDirectory)
            Else
                Return 'Is this right?
            End If

        End If
        CreateBuildFilesIfRequired()
        For Each file As String In Me.Project.BuildFiles
            Dim filename As String = Project.SourceFileLocation & file
            If (System.IO.File.Exists(filename) = False) Then
                SaveFile("", filename)
            End If
        Next
        ProjectCurrentlySaved = True
        CurrentlyOpenedFile = DefaultFileName
        LoadProject(NewProject)
    End Sub

    'kludge to load "maximized" and "minimized" state and allow a return to the previous normal states.
    Private Sub Form1_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Resize
        If (FormLoaded = True) Then
            CompilerErrorsResize()
            If (My.Settings.FormState = FormWindowState.Maximized) Then
                If (Me.WindowState <> FormWindowState.Maximized) Then
                    'great!  We can now offload the correct state!
                    My.Settings.FormState = FormWindowState.Normal
                    Me.Location = My.Settings.FormLocation
                    Me.Size = My.Settings.FormSize
                End If
            End If
        End If
    End Sub

    Private Sub RecorderForm_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles RecorderForm.FormClosing
        If (Me.RecorderForm.ReturnText <> "") Then
            Dim CommentCode As String = "'"
            If (CurrentLanguage = CodeTranslator.Languages.CSharp) Then CommentCode = "//"
            Me.TxtEdit.Text += vbNewLine & vbNewLine & _
            CommentCode & "****Add the following line of code into shared main(): " & vbNewLine & CommentCode

            If (CurrentLanguage = CodeTranslator.Languages.VBNet) Then
                Me.TxtEdit.Text += "Dim TestInstance_" & Me.RecorderForm.ReturnClass & _
                 " As New " & Me.RecorderForm.ReturnClass & "()"
            Else
                Me.TxtEdit.Text += Me.RecorderForm.ReturnClass & " TestInstance_" & Me.RecorderForm.ReturnClass & _
                 " = new " & Me.RecorderForm.ReturnClass & "();"
            End If

            Me.TxtEdit.Text += vbNewLine & Me.RecorderForm.ReturnText

            Me.Files.Item(Me.ConvertFilesKey(CurrentlyOpenedFile)).IsSaved = False
        End If
    End Sub

    Private Sub ObjectSpy()
        If (ObjSpy Is Nothing) Then
            ObjSpy = New ObjSpy()
            Keys.StartHandlers()
            ObjSpy.Keys = Me.Keys
            ObjSpy.Show()
            'Me.AddOwnedForm(ObjSpy)
            GC.Collect()
        Else
            Keys.StartHandlers()
            ObjSpy.ShowApp()
            GC.Collect()
        End If
        Me.Hide()
    End Sub

    Private Sub screenshot()
        Dim sc As New ScreenCapture()
        sc.Hide()
        sc.ShowDialog()
        sc.Dispose()
        GC.Collect()
    End Sub

    Private Sub TakeScreenshotToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TakeScreenshotToolStripMenuItem.Click
        screenshot()
    End Sub

    Private Sub PrintToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PrintToolStripMenuItem.Click
        Dim ppd As System.Windows.Forms.PrintDialog = New System.Windows.Forms.PrintDialog()
        ppd.Document = TxtEdit.PrintDocument
        If (ppd.ShowDialog() = Windows.Forms.DialogResult.OK) Then
            ppd.Document.Print()
        End If
    End Sub

    Private Sub PrintPreviewToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PrintPreviewToolStripMenuItem.Click
        Dim ppd As System.Windows.Forms.PrintPreviewDialog = New System.Windows.Forms.PrintPreviewDialog()
        ppd.Document = TxtEdit.PrintDocument
        ppd.ShowDialog()
    End Sub

    Private Sub ScreenShotToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ScreenShotToolStripMenuItem.Click
        screenshot()
    End Sub

    Private Sub RecordingToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RecordingToolStripMenuItem.Click, StartRecordingToolStripMenuItem.Click
        Record()
    End Sub

    'Lame way to add events for TxtEdit control
    Private Sub TxtEdit_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs)
        If (e.KeyCode = Windows.Forms.Keys.Back) Then
            ProjectCurrentlySaved = False
        End If

        If (e.KeyCode = Windows.Forms.Keys.Delete) Then
            ProjectCurrentlySaved = False
        End If

        If (e.KeyCode = Windows.Forms.Keys.F1) Then
            If (TxtEdit.ActiveTextAreaControl.SelectionManager.SelectedText.Length <> 0) Then
                Dim id As Integer
                Try
                    id = System.Diagnostics.Process.Start(".\SlickTestAPI.chm").Id
                Catch ex As Exception
                    System.Windows.Forms.MessageBox.Show("Help file does not appear to exist or refuses to start.", MsgBoxTitle)
                    Return
                End Try
                Dim Interact As UIControls.InterAct = New UIControls.InterAct
                System.Threading.Thread.Sleep(100)
                Dim i As Integer = 0
                While (id <> 0)
                    Try
                        Interact.AppActivate(id.ToString())
                        id = 0
                    Catch ex As Exception When i <> 10
                        System.Threading.Thread.Sleep(100)
                    End Try
                    If (i = 10) Then Return
                End While
                Interact.SendInput("%(s)")
                System.Threading.Thread.Sleep(20)
                Interact.SendInput("%(w)")
                System.Threading.Thread.Sleep(10)
                Interact.SendInput(TxtEdit.ActiveTextAreaControl.SelectionManager.SelectedText & "{ENTER}")
            Else
            End If
            Try
                System.Diagnostics.Process.Start(ChmHelp)
            Catch ex As Exception
                System.Windows.Forms.MessageBox.Show("Help file does not appear to exist or refuses to start.", MsgBoxTitle)
            End Try
        End If
    End Sub

    Private Sub AddFileToProject()
        SaveFileDialog1.DefaultExt = LanguageExt.Replace(".", "")
        SaveFileDialog1.Filter = "Slick Test Files|*" & LanguageExt
        SaveFileDialog1.InitialDirectory = Project.SourceFileLocation
        If (SaveFileDialog1.ShowDialog() <> Windows.Forms.DialogResult.OK) Then
            Return
        Else
            If (SaveFileDialog1.FileName.ToLowerInvariant().Contains(Project.SourceFileLocation.ToLowerInvariant()) = False) Then
                If (System.Windows.Forms.MessageBox.Show("Warning, all project files must be in the source folder.  " & _
                "Would you like to continue, but place the source file in the correct directory?", _
                MsgBoxTitle, MessageBoxButtons.YesNo, MessageBoxIcon.Warning) = Windows.Forms.DialogResult.No) Then
                    Return
                Else
                    System.IO.File.Copy(SaveFileDialog1.FileName, Project.SourceFileLocation & System.IO.Path.GetFileName(SaveFileDialog1.FileName), True)
                End If
            End If
            CreateNewFile(Project.SourceFileLocation & System.IO.Path.GetFileName(SaveFileDialog1.FileName))
        End If
    End Sub

    Private Sub OpenProjectToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OpenProjectToolStripMenuItem.Click
        OpenFileDialog1.DefaultExt = "stp"
        OpenFileDialog1.Filter = "Slick Test Project|*.stp"
        OpenFileDialog1.InitialDirectory = Project.SourceFileLocation
        Dim tmp As String = Project.LoadLocation
        If (Me.OpenFileDialog1.ShowDialog() = Windows.Forms.DialogResult.OK) Then
            If (Project.SafeLoadProjectWithMessages(Me.OpenFileDialog1.FileName) = True) Then
                LoadProject(False)
                Return
            End If
        End If
    End Sub

    Private Sub FileToolStripMenuItem2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles FileToolStripMenuItem2.Click
        OpenFileDialog1.DefaultExt = LanguageExt.Replace(".", "")
        OpenFileDialog1.Filter = "Slick Test Files|*" & LanguageExt & "|All Files|*.*"
        OpenFileDialog1.InitialDirectory = Project.SourceFileLocation
        SaveFileDialog1.FileName = String.Empty

        If (Me.OpenFileDialog1.ShowDialog() = Windows.Forms.DialogResult.OK) Then
            If (Me.OpenFileDialog1.FileName.EndsWith(LanguageExt) = False) Then
                System.Windows.Forms.MessageBox.Show( _
                "The file type must be '" & LanguageExt & "' in order to be opened and added to the " & _
                "project.", MsgBoxTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If
            If (System.Windows.Forms.MessageBox.Show("Warning: By opening this " & _
            "file, it will be added to the Project.", MsgBoxTitle, MessageBoxButtons.YesNo, _
            MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1) = Windows.Forms.DialogResult.Yes) Then
                Dim NewFileLocation As String = Me.Project.SourceFileLocation & System.IO.Path.GetFileName(Me.OpenFileDialog1.FileName)
                If (NewFileLocation.ToLower <> Me.OpenFileDialog1.FileName) Then 'you are opening a file in the project
                    If (System.IO.File.Exists(NewFileLocation) = True) Then
                        If (System.Windows.Forms.MessageBox.Show( _
                        "The file you are about to open already exists in the project." & _
                        "Would you like to replace it with the file you selected?", _
                        MsgBoxTitle, MessageBoxButtons.YesNo, _
                        MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) = Windows.Forms.DialogResult.Yes) Then
                            Try
                                System.IO.File.Delete(NewFileLocation)
                            Catch ex As Exception
                                System.Windows.Forms.MessageBox.Show("Unable to replace file.  " & _
                                "The file maybe opened by an application.", MsgBoxTitle, _
                                MessageBoxButtons.OK, MessageBoxIcon.Error)
                                Return
                            End Try
                        Else
                            Return
                        End If
                    End If
                    System.IO.File.Copy(Me.OpenFileDialog1.FileName, NewFileLocation)
                    Dim CRC As String = String.Empty
                    Dim FileData As String = ReadFile(Me.Project.SourceFileLocation & System.IO.Path.GetFileName(Me.OpenFileDialog1.FileName), CRC)
                    Me.AddFiles(FileData, Me.OpenFileDialog1.FileName, True, CRC)
                    TxtEdit.Text = Me.Files(System.IO.Path.GetFileName(Me.OpenFileDialog1.FileName)).FileText
                    FileTreeView.Nodes("FileNameNode").Nodes.Add(System.IO.Path.GetFileName(System.IO.Path.GetFileName(Me.OpenFileDialog1.FileName)))
                Else
                    Dim CRC As String = String.Empty
                    Dim FileData As String = ReadFile(Me.Project.SourceFileLocation & System.IO.Path.GetFileName(Me.OpenFileDialog1.FileName), CRC)
                    Me.AddFiles(FileData, _
                    System.IO.Path.GetFileName(Me.OpenFileDialog1.FileName), True, CRC)
                    TxtEdit.Text = Me.Files(Me.ConvertFilesKey(Me.OpenFileDialog1.FileName)).FileText
                End If
            End If
        End If
    End Sub

    Private Sub RunReportForm_ClosingWindow(ByVal MySize As System.Drawing.Size, ByVal MyWindowState As System.Windows.Forms.FormWindowState) Handles RunReportForm.ClosingWindow
        Me.ReporterSize = MySize
        Me.ReporterWindowState = MyWindowState
    End Sub

    Private Sub ExternalRunToolStripMenuItem_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ExternalRunToolStripMenuItem.SelectedIndexChanged
        If (Me.ExternalRunToolStripMenuItem.SelectedItem.ToString().ToLower.Contains("internal") = True) Then
            'internal
            Project.IsOfficialRun = False
        Else
            'external
            Project.IsOfficialRun = True
        End If
        Project.SaveProject(Project.LoadLocation)
    End Sub

    Private Sub ShowTestReportsToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ShowTestReportsToolStripMenuItem.Click
        RunReportForm = New ResultsViewer.RunReport(Project.ReportDatabasePath, Project.GUID, Project.IsOfficialRun, ReporterSize, ReporterWindowState)
        Me.AddOwnedForm(RunReportForm)
        RunReportForm.ShowDialog()
    End Sub

    Private Sub UsingSlickTestToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles UsingSlickTestToolStripMenuItem.Click
        Try
            System.Diagnostics.Process.Start("https://sourceforge.net/apps/mediawiki/slicktest/index.php?title=Main_Page")
        Catch ex As Exception
            System.Windows.Forms.MessageBox.Show("Unable to find or start https://sourceforge.net/apps/mediawiki/slicktest/index.php?title=Main_Page", MsgBoxTitle)
        End Try

    End Sub

    Private Sub FindWindowViaHwndToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles FindWindowViaHwndToolStripMenuItem.Click
        If (FindWindowViaHwnd Is Nothing) Then
            FindWindowViaHwnd = New FindMyHwnd()
        End If
        FindWindowViaHwnd.ShowDialog(Me)
    End Sub

    Private Sub ProjectSelectionFormCloseSafely()
        If (Not Me.ProjectSelectionForm Is Nothing) Then
            Me.ProjectSelectionForm.AllowedToClose = True
            Me.ProjectSelectionForm.Close()
        End If
    End Sub

    Private Sub UserGuideToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles UserGuideToolStripMenuItem.Click
        Try
            System.Diagnostics.Process.Start(".\Guides\UserGuide.pdf")
        Catch ex As Exception
            System.Windows.Forms.MessageBox.Show("Unable to open the User Guide.  Error: " & ex.Message)
        End Try
    End Sub

    Private Sub ObjSpy_HidingForm() Handles ObjSpy.HidingForm
        Me.Show()
    End Sub

    Private Sub DescriptionTesterToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DescriptionTesterToolStripMenuItem.Click
        If (Not VerifyDescription Is Nothing) Then
            If (VerifyDescription.Visible <> False) Then
                VerifyDescription.BringToFront()
                Return
            End If
            VerifyDescription.Close()
        End If
        VerifyDescription = New VerifyDescription()
        VerifyDescription.IsVb = IsVisualBasic
        VerifyDescription.Show()
    End Sub

    Private Sub ResetCacheToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ResetCacheToolStripMenuItem.Click
        ResetCacheNextStartup()
        MessageBox.Show("Cache will be reset on the next restart of " & MsgBoxTitle & ".", MsgBoxTitle, MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Sub

    Private Sub OptionsToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OptionsToolStripMenuItem.Click
        options = New OptionsForm()
        options.Style = OptionsForm.OptionsStyle.TabPages
        options.ShowDialog(Me)
        Me.Keys.RecordXY = My.Settings.Recorder_Default__XY__Record__Style
        If (My.Settings.Recorder_Default__Total__Description__Length > 255 OrElse My.Settings.Recorder_Default__Total__Description__Length < 20) Then
            My.Settings.Recorder_Default__Total__Description__Length = 25
            My.Settings.Save()
        End If

        UpdateUISettings()
        AddDLLs()
    End Sub

    Private Sub ContentsToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ContentsToolStripMenuItem.Click
        Try
            Help.ShowHelp(Me, ChmHelp)
        Catch ex As Exception
            System.Windows.Forms.MessageBox.Show("Help file does not appear to exist or refuses to start.", MsgBoxTitle)
        End Try
    End Sub

    Private Sub IndexToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles IndexToolStripMenuItem.Click
        Try
            Help.ShowHelpIndex(Me, ChmHelp)
        Catch ex As Exception
            System.Windows.Forms.MessageBox.Show("Help file does not appear to exist or refuses to start.", MsgBoxTitle)
        End Try
    End Sub

    Private Sub SearchToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SearchToolStripMenuItem.Click
        Try
            Help.ShowHelpIndex(Me, ChmHelp)
        Catch ex As Exception
            System.Windows.Forms.MessageBox.Show("Help file does not appear to exist or refuses to start.", MsgBoxTitle)
            Return
        End Try
        System.Threading.Thread.Sleep(100)
        System.Windows.Forms.SendKeys.SendWait("%s")
    End Sub

#End Region

    Private Sub UpdateUISettings()
        If (My.Settings.UI_Highlight__Edit__Line = True) Then
            Me.TxtEdit.LineViewerStyle = ICSharpCode.TextEditor.Document.LineViewerStyle.FullRow
        Else
            Me.TxtEdit.LineViewerStyle = ICSharpCode.TextEditor.Document.LineViewerStyle.None
        End If
        Me.TxtEdit.ShowTabs = My.Settings.UI_Show__Tab__Markers
        Me.TxtEdit.ShowLineNumbers = My.Settings.UI_Show__Line__Numbers
        TxtEdit.Font = My.Settings.UI_Font
        TxtEdit.ShowSpaces = My.Settings.UI_Show__Space__Markers
        TxtEdit.ShowEOLMarkers = My.Settings.UI_Show__New__Line__Markers
        TxtEdit.ShowMatchingBracket = My.Settings.UI_Show__Matching__Brackets
        MyBase.SetupFolding(My.Settings.UI_Enable__Folding)
    End Sub

    Private Sub CreateBuildFilesIfRequired(Optional ByVal ForceSave As Boolean = False)
        If (ForceSave = True OrElse Me.Project.BuildFiles.Count = 0) Then
            Me.Project.AddFile(DefaultFileName)
            Dim tmpFile As String = String.Empty
            Dim FileName As String = Project.SourceFileLocation & DefaultFileName
            Try
                Dim path As String = My.Settings.Project_Default__Main__File__Template
                path = System.IO.Path.Combine(System.IO.Path.GetDirectoryName(path), System.IO.Path.GetFileNameWithoutExtension(path))
                tmpFile = Me.ReadFile(path & LanguageExt)
                tmpFile = UpdateFileWithTemplate(tmpFile, FileName)
            Catch ex As Exception
            End Try

            SaveFile(tmpFile, FileName)
        End If
    End Sub

    Private Function UpdateFileWithTemplate(ByVal FileText As String, ByVal FileName As String) As String
        FileText = FileText.Replace("{{FILENAMEWITHNOEXT}}", System.IO.Path.GetFileNameWithoutExtension(FileName)). _
                Replace("{{DATECREATED}}", System.DateTime.Today.ToString("D"))
        FileText = FileText.Replace("{{USERNAME}}", System.Windows.Forms.SystemInformation.UserName)
        Return FileText
    End Function

    Private Sub CopyMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CopyMenuItem.Click
        System.Windows.Forms.Clipboard.Clear()
        Try
            System.Windows.Forms.Clipboard.SetText(Me.CompilerIssueDataGridView.SelectedCells(0).Value.ToString())
        Catch ex As Exception
            System.Windows.Forms.MessageBox.Show( _
            "Failed to copy to the clipboard!  Error: " & ex.ToString(), MsgBoxTitle, _
            MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub UpdateTitle(Optional ByVal ForceIsFileChanged As Boolean = False)
        Me.Text = TitleText & " - " & Project.ProjectName & " - " & CurrentlyOpenedFile
        Try
            If (ForceIsFileChanged = False) Then
                If (Me.Files.ContainsKey(Me.ConvertFilesKey(CurrentlyOpenedFile))) Then
                    ForceIsFileChanged = Not Me.Files(Me.ConvertFilesKey(CurrentlyOpenedFile)).IsSaved
                End If
            End If
            If (ForceIsFileChanged) Then Me.Text += " *"
        Catch ex As Exception

        End Try
    End Sub

    Private Sub ResetCacheNextStartup()
        Dim CodeCompletePath As String = ""
        Try
            CodeCompletePath = IO.Path.Combine(IO.Path.GetTempPath(), "SlickTestCodeCompletion")
            System.IO.File.Create(IO.Path.Combine(CodeCompletePath, "ResetCache.txt"))
        Catch ex As Exception
            MessageBox.Show("Failed to reset cache: " & CodeCompletePath)
        End Try
    End Sub

    Friend Sub HandleApplicationArguments(ByVal e As String(), ByVal IsInitialStartup As Boolean)
        If (Not e Is Nothing) Then
            For Each arg As String In e
                If (Not arg Is Nothing) Then
                    If (System.IO.Path.GetExtension(arg).ToUpperInvariant().EndsWith("STP") AndAlso System.IO.File.Exists(arg)) Then
                        If (IsInitialStartup = False) Then
                            If (Me.ProjectSelectionForm Is Nothing) Then
                                If (Me.SaveProject(True, False) = Windows.Forms.DialogResult.Cancel) Then
                                    Continue For
                                End If
                            End If
                        Else
                            System.Threading.Thread.Sleep(100)
                        End If
                        ProjectSelectionFormCloseSafely()
                        LoadProject(arg)
                        Me.Visible = True
                    Else
                        Dim TrimList As Char() = {" "c, "-"c, "/"c, "\"c}
                        Dim PostColon As String = ""
                        Dim PreColon As String = arg
                        If (arg.Contains(":")) Then
                            PostColon = arg.Substring(arg.IndexOf(":") + 1)
                            PreColon = arg.Substring(0, arg.IndexOf(":"))
                        End If
                        Select Case PreColon.ToUpperInvariant().Trim(TrimList)
                            Case "RESETCACHENEXTRESTART"
                                ResetCacheNextStartup()
                            Case "BUILDANDRUN"
                                If (arg.Contains(":")) Then
                                    Dim file As String = PostColon
                                    If (System.IO.Path.GetExtension(file).ToUpperInvariant().EndsWith("STP") AndAlso System.IO.File.Exists(file)) Then
                                        If (Me.ProjectSelectionForm Is Nothing) Then
                                            If (Me.SaveProject(False, True) = Windows.Forms.DialogResult.Cancel) Then
                                                Return
                                            End If
                                        End If
                                        ProjectSelectionFormCloseSafely()

                                        LoadProject(file)
                                        Me.Visible = True
                                        If (IsInitialStartup) Then
                                            BuildAndRun()
                                        End If
                                    End If
                                End If
                            Case "?"
                                MessageBox.Show("Supported Commands: " & vbNewLine & _
                                                "-ResetCacheNextRestart : This will reset the autocomplete cache on the next restart of " & MsgBoxTitle & "." & vbNewLine & _
                                                "<file>.stp : This will open a project and save any currently opened project." & vbNewLine & _
                                                "-BuildAndRun:<file>.stp : This will open a project, build it and run it.  This will attempt to save any currently opened project without a prompt." & vbNewLine & _
                                                "", _
                                                MsgBoxTitle, MessageBoxButtons.OK, MessageBoxIcon.Information)
                            Case Else
                                MessageBox.Show("Arg: " & arg)
                        End Select
                    End If
                End If
            Next
        Else
            Me.Focus()
        End If
    End Sub

    Private Sub ArgumentTimer_Tick(ByVal sender As Object, ByVal e As System.EventArgs) Handles ArgumentTimer.Tick
        If (ArgsFromUser.Count <> 0) Then
            Dim IsInitialStartup As Boolean = False
            If (ArgsFromUser(0).ToUpperInvariant().Equals("TRUE")) Then
                IsInitialStartup = True
            End If
            ArgsFromUser.RemoveAt(0)
            HandleApplicationArguments(ArgsFromUser.ToArray(), IsInitialStartup)
            ArgsFromUser.Clear()
        End If
    End Sub
End Class

Public Class FileInfo
    Public FileText As String
    Public FileName As String
    Public CRC As String
    Private IsFileSaved As Boolean
    Public Property IsSaved() As Boolean
        Get
            Return IsFileSaved
        End Get
        Set(ByVal value As Boolean)
            'If (value <> IsFileSaved) Then
            'IsFileSaved = value
            'End If
            IsFileSaved = value
        End Set
    End Property

    Public Sub New()
        IsSaved = False
        FileText = String.Empty
        FileName = String.Empty
        CRC = "0"
    End Sub
End Class

Namespace My
    Partial Friend Class MyApplication
        Private WithEvents timer As System.Timers.Timer
        Private Shared cmdLine As ObjectModel.ReadOnlyCollection(Of String)
        Private Sub MyApplication_Startup(ByVal sender As Object, ByVal e As Microsoft.VisualBasic.ApplicationServices.StartupEventArgs) Handles Me.Startup
            timer = New System.Timers.Timer()
            timer.Enabled = True
            timer.Interval = 50
            cmdLine = e.CommandLine
            timer.Start()
        End Sub

        Private Sub MyApplication_StartupNextInstance(ByVal sender As Object, ByVal e As Microsoft.VisualBasic.ApplicationServices.StartupNextInstanceEventArgs) Handles Me.StartupNextInstance
            HandleArgs(e.CommandLine)
            e.BringToForeground = True
        End Sub

        Private Sub HandleArgs(ByVal CommandLine As ObjectModel.ReadOnlyCollection(Of String), Optional ByVal IsInitialStartup As Boolean = False)
            Try
                If TypeOf Me.MainForm Is SlickTestDev Then
                    Dim Args(CommandLine.Count) As String
                    CommandLine.CopyTo(Args, 0)
                    SyncLock (SlickTestDev.ArgsFromUser)
                        SlickTestDev.ArgsFromUser.Add(IsInitialStartup)
                        For Each arg As String In Args
                            SlickTestDev.ArgsFromUser.Add(arg)
                        Next
                    End SyncLock
                End If
            Catch ex As Exception
                Dim str As String = ex.InnerException.ToString() & vbNewLine & _
                ex.Message.ToString() & vbNewLine & ex.Source.ToString() & vbNewLine & _
                ex.StackTrace.ToString() & vbNewLine & ex.TargetSite.ToString()

                MessageBox.Show("Error handling arguments: " & ex.Message & _
                                vbNewLine & "Error Details: " & str, SlickTestDev.MsgBoxTitle)
            End Try

        End Sub

        <Global.System.Diagnostics.DebuggerStepThroughAttribute()> _
        Public Sub New()
            MyBase.New(Global.Microsoft.VisualBasic.ApplicationServices.AuthenticationMode.Windows)
            Me.IsSingleInstance = True
            Me.ShutdownStyle = Global.Microsoft.VisualBasic.ApplicationServices.ShutdownMode.AfterMainFormCloses
        End Sub

        <Global.System.Diagnostics.DebuggerStepThroughAttribute()> _
        Protected Overrides Sub OnCreateMainForm()
            Me.MainForm = Global.SlickTestDeveloper.SlickTestDev
        End Sub

        Private Sub timer_Elapsed(ByVal sender As Object, ByVal e As System.Timers.ElapsedEventArgs) Handles timer.Elapsed
            If Not Me.MainForm Is Nothing Then
                timer.Stop()
                HandleArgs(cmdLine, True)
            End If
        End Sub
    End Class
End Namespace

#Region "Test Code"

'For Each item As String In InterAct.SwfWindow("name:=""Form1"";;value:=""Form1""").SwfListBox("name:=""ListBox1""").GetSelectedItems()
'    MsgBox(item)
'Next

'InterAct.Window("name:=""Notepad""").WinObject("name:=""Edit""").Click(419, 35)
'System.Threading.Thread.Sleep(100)
'MsgBox(InterAction.StyleInfo.TextBoxStyle.TEMP_WriteCode())
'InterAct.Window("name:=""SciCalc""").CheckBox("value:=""Inv""").SetChecked(InterAction.CheckBox.CHECKED)
'Threading.Thread.Sleep(500)
'InterAct.Window("name:=""SciCalc""").CheckBox("value:=""Inv""").SetChecked(InterAction.CheckBox.CHECKED)
'Threading.Thread.Sleep(500)
'InterAct.Window("name:=""SciCalc""").CheckBox("value:=""Inv""").SetChecked(InterAction.CheckBox.UNCHECKED)
'Threading.Thread.Sleep(500)


'For Each item As String In InterAct.Window("name:=""Form1""").ComboBox("name:=""PlayListBox1""").Items()
'    MsgBox(item)
'Next
'MsgBox(InterAct.Window("name:=""PlaylistForm""").ListBox("name:=""PlayListCheckBox""").GetItem(0))
'MsgBox(InterAct.Window("name:=""PlaylistForm""").ListBox("name:=""PlayListCheckBox""").SelectedItemNumber())
'MsgBox(InterAct.Window("name:=""PlaylistForm""").ListBox("name:=""PlayListCheckBox""").SelectItem(100))
'For Each item As String In InterAct.Window("name:=""PlaylistForm""").ListBox("name:=""PlayListCheckBox""").Items()
'    MsgBox(item)
'Next
'MsgBox("GetCurrentLineNumber: " & InterAct.Window("name:=""Notepad""").TextBox("name:=""Edit""").GetCurrentLineNumber())
'MsgBox("GetLineLength(3): " & InterAct.Window("name:=""Notepad""").TextBox("name:=""Edit""").GetLineLength(3))
'MsgBox("GetLineText(3): " & InterAct.Window("name:=""Notepad""").TextBox("name:=""Edit""").GetLineText(3))
''MsgBox(InterAct.Window("name:=""Notepad""").TextBox("name:=""Edit""").GetIndexFromCaret())
'MsgBox("?GetLineText: " & InterAct.Window("name:=""Notepad""").TextBox("name:=""Edit""").GetLineText(InterAct.Window("name:=""Notepad""").TextBox("name:=""Edit""").GetLineCount()) & " ; " & InterAct.Window("name:=""Notepad""").TextBox("name:=""Edit""").GetLineCount())
'MsgBox(InterAct.TextBox("name:=""GAmpInterop""").GetLineCount())

'InterAct.Window("Name:=""Notepad""").WinObject("Name:=""Edit""").Click(75, 29)
'Threading.Thread.Sleep(500)
'InterAct.SendInput("*(D)")



'InterAct.UseDotNet = True
''InterAct.SendInput("{lwin}{up}{up}{up}{up}{up}{up}{up}{right}")
'Threading.Thread.Sleep(500)
'Dim SciCalc As New APIControls.Description()
'SciCalc.Add("Name", "SciCalc")


'Dim SciCalc_1 As New APIControls.Description()
'SciCalc_1.Add("Value", "1")


'Dim SciCalc_2 As New APIControls.Description()
'SciCalc_2.Add("Value", "2")



'If (InterAct.Window(SciCalc).Exists(2) = False) Then MsgBox("It's offical, you suck!")

'InterAct.Window("Name:=""SciCalc""").WinObject("Value:=""1""").CloseParent()
'Threading.Thread.Sleep(500)
'InterAct.Window("Name:=""SciCalc""").WinObject("Value:=""2""").Click(17, 19)
'Threading.Thread.Sleep(500)

'InterAct.Window(SciCalc).Window("Value:=""1""").Click(22, 17)
'Threading.Thread.Sleep(500)
'InterAct.Window(SciCalc).WinObject(SciCalc_2).Click(17, 19)
'Threading.Thread.Sleep(500)
'InterAct.UseDotNet = True
'Try
'68942
'MsgBox(InterAct.Window("Name:=""Form1""").WinObject("Name:=""NameLabel""").IsObjectAtAbsLocation(InterAction.Mouse.CurrentAbsX, InterAction.Mouse.CurrentAbsY))
'InterAct.Window("Name:=""Form1""").WinObject("Name:=""NameLabel""").Click(1, 1)
'Catch ex As Exception
'MsgBox("ex:" & ex.ToString)
'End Try
'InterAct.ignoreErrors = True
'InterAct.Window("Name:=""IMWindowClass""").WinObject("Name:=""DirectUIHWND""").Click(18022, 18112)
'InterAct.Window("Name:=""Form1""").Window("Name:=""TitleBar""").CaptureImage("C:\test.bmp")
'InterAct.Window("Name:=""Form1""").Window("Name:=""TitleBar""").Click(65, 7)
'InterAct.Window("Name:=""Winamp v1.x""").Click()
'MsgBox(InterAct.Window("Name:=""Winamp v1.x""").CaptureImage("C:\Test.bmp", 10, 10, 10, 10))
'Return

'InterAct.CompareImagesByPercentDiff("C:\testFile2.bmp", "C:\testFile3.bmp", True)
'InterAction.Win32Window.DebugWindowsHandles(InterAct.Window("Value:=""*Title:*""").WildCard.Hwnd)
'InterAction.Windows.getClassInfo(InterAct.Window("Value:=""Options"";;Name:=""WindowsForms10.Window.8.app.0.378734a""").Window("Name:=""WindowsForms10.SysTabControl32.app.0.378734a""").Window("Name:=""WindowsForms10.Window.8.app.0.378734a""").Window("Name:=""WindowsForms10.LISTBOX.app.0.378734a""").Hwnd())
'InterAction.Windows.getClassInfo(InterAct.Window("Name:=""SciCalc""").Window("Value:=""1""").Hwnd())
'InterAct.ignoreErrors = True
'InterAct.Window("Value:=""*Title:*""").WildCard.Window("Value:=""*Name:*""").WildCard.Click(23, 8)
'Return
'Return


'Dim window As InterAction.Window
''Window("Name:=""PlayList""").ListBox("Name:=""""").Click(171, 80)
'Dim OptionsWindow As APIControls.Description = InterAct.CreateDescription()
'OptionsWindow.Add("Name", "OptionsForm")
'Dim TabControl As APIControls.Description = InterAct.CreateDescription()
'TabControl.Add("Name", "OptionsTab")
'Dim PlayList As APIControls.Description = InterAct.CreateDescription()
'PlayList.Add("Name", "PlayListCheckBox")
'InterAct.useDotNet = True
'Dim rep As New Reporter.Report()
'rep.ReporterPath = "D:\Development\dotNet\sendkeys\"
'rep.RecordEvent(rep.Warning, "The dog is bad.", "Should she be punished?")
'rep.NextStep()
'rep.RecordEvent(rep.Fail, "Mom is too nice.", "She didn't punish the dog!")
'rep.WriteCurrentReport()
'Dim x1 As String = rep.ReporterPath
'rep = New Reporter.Report()
'rep.OpenReportInMemory(x1, 1)
'rep.RecordEvent(rep.Fail, "Mom is too nice2.", "She didn't punish the dog!")
''rep.NextTest()
'If (InterAct.Window(OptionsWindow).Window(TabControl).ListBox(PlayList).exists) Then
'    'MsgBox(InterAct.Window(OptionsWindow).Window(TabControl).ListBox(PlayList).SelectItem(1))
'    'MsgBox(InterAct.Window(OptionsWindow).Window(TabControl).ListBox(PlayList).GetItem(530))
'    For i As Integer = 0 To 10 'InterAct.Window(OptionsWindow).Window(TabControl).ListBox(PlayList).Count(-1)
'        InterAct.Window(OptionsWindow).Window(TabControl).ListBox(PlayList).SelectItem(i)
'        System.Threading.Thread.Sleep(1)
'        Dim x As Integer = InterAct.Window(OptionsWindow).Window(TabControl).ListBox(PlayList).SelectedItemNumber()
'        If (i = x) Then
'            rep.RecordEvent(rep.Pass, "Playlist item selected", "Playlist item #: " & i & " Item: " & _
'            InterAct.Window(OptionsWindow).Window(TabControl).ListBox(PlayList).GetItem(i))
'        Else
'            rep.RecordEvent(rep.Fail, "Wrong playlist item selected", "Playlist item #: " & _
'            i & " v " & x & vbNewLine & "Expected Selected Item: " & _
'            InterAct.Window(OptionsWindow).Window(TabControl).ListBox(PlayList).GetItem(i) & _
'            vbNewLine & "Expected Selected Item: " & _
'            InterAct.Window(OptionsWindow).Window(TabControl).ListBox(PlayList).GetItem(x))
'        End If
'    Next
'Else
'    rep.RecordEvent(rep.Info, "GAmp not setup", "GAmp is not running or GAmp playlist not open, so GAmp test skipped")
'End If
'rep.RunReport()
'Return
'Dim GampForm As APIControls.Description = InterAct.CreateDescription()
'GampForm.Add("Value", "*Title: *")
'GampForm.Add("Name", "*WindowsForms10*")
'GampForm.WildCard = True

'Dim GampFileName As APIControls.Description = InterAct.CreateDescription()
'GampFileName.Add("Value", "*Name: *")
'GampFileName.Add("Name", "*WindowsForms10*")
'GampFileName.WildCard = True

'window = InterAct.Window(GampForm).Window(GampFileName)
'MsgBox(window.GetControlName())
'Return
'InterAct.Window("Name:=""Notepad""").Window("Name:=""Edit""").sText("test{BS}{BS}{BS}{BS}{BS}")
'Console.WriteLine("*************************")

'Return
'InterAct.ignoreErrors = True
'Dim Strs As String = ""
'Dim Counter As Integer = 0
'For Each s As String In InterAct.Window("Value:=""Options"";;Name:=""WindowsForms10.Window.8.app.0.378734a""").Window("Name:=""WindowsForms10.SysTabControl32.app.0.378734a""").Window("Name:=""WindowsForms10.Window.8.app.0.378734a""").ListBox("Name:=""WindowsForms10.LISTBOX.app.0.378734a""").Items()
'    Strs += s & vbNewLine
'    Counter += 1
'    If ((Counter Mod 20) = 0) Then
'        MsgBox(Strs)
'        Strs = ""
'    End If
'Next
'InterAct.Window("Value:=""Title: Final Fantasy VII - Prodigy - Brbeathe"";;Name:=""WindowsForms10.Window.8.app.0.378734a""").Click()
'Return

''Dim Calc As APIControls.Description = InterAct.CreateDescription()
''Calc.Add("value", "Calculator")
''Calc.Add("name", "SciCalc")

''Dim Button As APIControls.Description = InterAct.CreateDescription()
''Button.Add("name", "Button")
''Button.Add("text", "1")

''InterAct.Window(Calc).Window(Button).Click()
''Return

'Try
'    window = InterAct.Window("name:=""*Title: *"";;value:=""WindowsForms10.Window.8.app.0.378734a""").WildCard
'    If (window.WildCard.exists = False) Then
'        System.Windows.Forms.MessageBox.Show("GAmp 1.1 not found for wildcard test!")
'        Return
'    End If
'    If (window.Window("name:=""WindowsForms10.BUTTON.app.0.378734a"";;value:=""||""").WildCard.exists) Then
'        System.Console.WriteLine("*************Attempting to click ||")
'        window.Click()
'        Return
'    Else
'        window = InterAct.Window("name:=""*Title: *"";;value:=""WindowsForms10.Window.8.app.0.378734a""").WildCard
'        If (window.Window("name:=""WindowsForms10.BUTTON.app.0.378734a"";;value:="">""").WildCard.exists) Then
'            System.Console.WriteLine("***************Attempting to click >")
'            window.Click()
'            Return
'        End If
'    End If
'    System.Console.WriteLine("Failed to find play/pause button")
'Catch ex As Exception
'    Console.WriteLine(ex.ToString())
'End Try

'Private Sub TestItToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TestItToolStripMenuItem.Click
'    'Me.Window("name:=""GAmpInterop""").RightClick()

'    'MsgBox(Me.Window("windowtype:=""window"";;name:=""#32768""").Menu.GetText("*"))

'    Dim win As New UIControls.Window("name:=""MSPaintApp""")
'    Dim MenuArray() As String = {"View", "Zoom", "Cus*"}
'    win.Menu.SelectMenuItem(MenuArray)
'    'win.Menu.GetInfo()
'    '

'    '		MsgBox(Me.Window("name:=""Notepad""").Menu.GetText("File\N*"))


'    Return
'    'Dim loc As String = "M:\My Documents\Visual Studio 2005\Projects\SlickTestDeveloper\SlickTestDeveloper\bin\Release\DB\STD.sdf"
'    'Dim rep As New UIControls.Report(System.Guid.NewGuid(), loc, "Fake Project", False)
'    'Dim InterAct1 As UIControls.InterAct
'    'InterAct1 = New UIControls.InterAct()

'    'Try
'    '    'Me.IgnoreExceptions=false
'    '    InterAct1.IEWebBrowser("name:=""IEFrame"";;value:=""Yahoo! - Microsoft Internet Explorer""").WinObject("width:=""906"";;name:=""WorkerW"";;value:=""""").WinObject("name:=""ReBarWindow32""").ComboBox("name:=""ComboBoxEx32""").ComboBox("name:=""ComboBox""").TextBox("name:=""Edit""").Click(160, 4)
'    '    InterAct1.SendInput("www,{bs}.googke{bs}{bs}le.com{enter}")
'    '    Threading.Thread.Sleep(500)
'    '    InterAct1.IEWebBrowser("name:=""IEFrame"";;value:=""Google - Microsoft Internet Explorer""").WebElement("name:=""q""").Click(64, 175)
'    '    InterAct1.SendInput("This is a test{enter}")
'    '    Threading.Thread.Sleep(500)
'    '    InterAct1.IEWebBrowser("name:=""IEFrame"";;value:=""This is a test - Google Search - Microsoft Internet Explorer""").WebElement("webtext:=""Official Google Blog: This is a test. This is only a test."";;webtype:=""a""").Click(-210, 184)
'    'Catch ex As Exception
'    '    UIControls.MessageBox.Show("Ex: " & ex.ToString())
'    '    'Reporter.RecordEvent(UIControls.Report.Fail,"An Exception has occurred: " & ex.ToString(),"Exception")
'    'End Try

'    'Try
'    '    'Me.IgnoreExceptions=false
'    '    InterAct1.IEWebBrowser("name:=""IEFrame"";;value:=""Win32::IEAutomation - Web application automation using Internet Explorer - search.cpan.org - Microsoft Internet Explorer""").WebElement("webtext:=""NAME"";;webtype:=""a""").Click(-24, 107)
'    'Catch ex As Exception
'    '    UIControls.MessageBox.Show("Ex: " & ex.ToString())
'    '    'Reporter.RecordEvent(UIControls.Report.Fail,"An Exception has occurred: " & ex.ToString(),"Exception")
'    'End Try
'    'Try
'    '    InterAct1.IgnoreExceptions = False
'    '    Dim ie As UIControls.IEWebBrowser = InterAct1.IEWebBrowser("name:=""IEFrame"";;value:=""Yahoo! - Microsoft Internet Explorer""")
'    '    MsgBox(ie.Hwnd)
'    '    ie.Forward()
'    'Catch ex As Exception
'    '    MsgBox(ex.ToString)
'    'End Try


'    'Dim i As Integer = 0
'    'Dim i1 As Integer = 0
'End Sub

'Private Sub ClearTestToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ClearTestToolStripMenuItem.Click
'    InitText()
'End Sub
'Private Sub CommentSelectedLinesToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CommentSelectedLinesToolStripMenuItem.Click, CommentSelectedLinesToolStripMenuItem1.Click
'    If (Me.TxtEdit.ActiveTextAreaControl.TextArea.SelectionManager.HasSomethingSelected = True) Then
'        With Me.TxtEdit.ActiveTextAreaControl.TextArea.SelectionManager.SelectionCollection.Item(0)
'            For i As Integer = .StartPosition.Y To .EndPosition.Y
'                Me.TxtEdit.Document.Insert(Me.TxtEdit.Document.PositionToOffset(New Point(0, i)), "'")
'            Next
'        End With
'    End If
'End Sub

'Private Sub UncommentSelectedLinesToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles UncommentSelectedLinesToolStripMenuItem.Click, UncommentSelectedLinesToolStripMenuItem1.Click
'    If (Me.TxtEdit.ActiveTextAreaControl.TextArea.SelectionManager.HasSomethingSelected = True) Then
'        With Me.TxtEdit.ActiveTextAreaControl.TextArea.SelectionManager.SelectionCollection.Item(0)
'            Dim c As Char = "'"c
'            For i As Integer = .StartPosition.Y To .EndPosition.Y
'                For chLoc As Integer = 0 To 25 'We're not scanning the whole line, just the first part of it.
'                    Try
'                        c = Me.TxtEdit.Document.GetCharAt(Me.TxtEdit.Document.PositionToOffset(New Point(chLoc, i)))
'                    Catch ex As Exception
'                        Exit For 'No character, we're done.
'                    End Try
'                    If (c = "'") Then
'                        Me.TxtEdit.Document.Remove(Me.TxtEdit.Document.PositionToOffset(New Point(chLoc, i)), 1)
'                        Exit For
'                    ElseIf (c <> " "c AndAlso c <> vbTab) Then
'                        Exit For
'                    End If
'                Next
'            Next
'        End With
'    End If
'End Sub

'Private Sub TestSendStringToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
'    Dim interact As UIControls.InterAct = New UIControls.InterAct
'    Dim str As String = InputBox("Enter in a SendInput string to check syntax...", "Syntax Check")
'    If (str = "") Then
'        Return
'    End If
'    If (str.Chars(0) <> """") Then
'        System.Windows.Forms.MessageBox.Show("You must start with """", not """ + str.Chars(0).ToString + """")
'    End If
'    str = str.Replace("""" & """", """")
'    str = str.Substring(1, str.Length - 2)
'    If (interact.SendInput(str) = False) Then
'        System.Windows.Forms.MessageBox.Show("This item was not found... Debug info saved if any present!")
'    Else
'        System.Windows.Forms.MessageBox.Show("The item was found and was clicked.")
'    End If
'End Sub

'Private Sub TestWindowToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
'    Dim interact As UIControls.InterAct = New UIControls.InterAct

'    Dim str As String = InputBox("Enter in a InterAct.Window(str) string to check syntax... this will use .exist property", _
'    "Syntax Check", """name:=""Calculator""""")
'    If (str = "") Then
'        Return
'    End If
'    If (str.Chars(0) <> """") Then
'        System.Windows.Forms.MessageBox.Show("You must start with """", not """ + str.Chars(0).ToString + """")
'    End If
'    str = str.Replace("""" & """", """")
'    str = str.Substring(1, str.Length - 2)
'    Try
'        If (interact.Window(str).Exists() = False) Then
'            System.Windows.Forms.MessageBox.Show("Item does not exist")
'        Else
'            System.Windows.Forms.MessageBox.Show("The item was found.")
'        End If
'    Catch ex As Exception
'        System.Windows.Forms.MessageBox.Show(ex.ToString())
'    End Try
'End Sub

'Sub InitText()
'    TxtEdit.Text = "Imports System" & vbNewLine & _
'        "Imports System.Drawing" & vbNewLine & _
'        "Imports Microsoft" & vbNewLine & _
'        "Imports System.Windows.Forms" & vbNewLine & _
'        "Imports UIControls" & vbNewLine & _
'        "Imports Microsoft.VisualBasic" & vbNewLine & _
'        "Public Class X" & vbNewLine & _
'        vbTab & "Inherits UIControls.InterAct" & vbNewLine & _
'        vbTab & "Sub new()" & vbNewLine & _
'        vbTab & vbTab & "for each i as IntPtr in Me.Window(""Name:=""""SciCalc"""""").GetObjectHwnds()" & vbNewLine & _
'        vbTab & vbTab & "Me.Window(""Hwnd:="""""" & i.ToString() & """""""").Close()" & vbNewLine & _
'        vbTab & vbTab & "next" & vbNewLine & _
'        vbTab & vbTab & "System.Threading.Thread.Sleep(1000)" & vbNewLine & _
'        vbTab & vbTab & "System.Diagnostics.Process.Start(""Calc.exe"")" & vbNewLine & _
'        vbTab & vbTab & "UseDotNet = True" & vbNewLine & _
'        vbTab & vbTab & "IgnoreExceptions = True" & vbNewLine & _
'        vbTab & vbTab & "if(Me.Window(""Name:=""""SciCalc"""""").exists(2)=false) then MsgBox(""It's offical, you suck!"")" & vbNewLine & _
'        vbTab & vbTab & "" & vbNewLine & _
'        vbTab & vbTab & "Me.Window(""Name:=""""SciCalc"""""").WinObject(""Value:=""""1"""""").Click(22,17)" & vbNewLine & _
'        vbTab & vbTab & "Threading.Thread.Sleep(500)" & vbNewLine & _
'        vbTab & vbTab & "Me.Window(""Name:=""""SciCalc"""""").WinObject(""Value:=""""2"""""").Click(17,19)" & vbNewLine & _
'        vbTab & vbTab & "Threading.Thread.Sleep(500)" & vbNewLine & _
'        vbTab & vbTab & "Me.Window(""Name:=""""SciCalc"""""").WinObject(""Value:=""""*"""""").Click(14,7)" & vbNewLine & _
'        vbTab & vbTab & "Threading.Thread.Sleep(500)" & vbNewLine & _
'        vbTab & vbTab & "Me.Window(""Name:=""""SciCalc"""""").WinObject(""Value:=""""3"""""").Click(18,15)" & vbNewLine & _
'        vbTab & vbTab & "Threading.Thread.Sleep(500)" & vbNewLine & _
'        vbTab & vbTab & "Me.Window(""Name:=""""SciCalc"""""").WinObject(""Value:=""""4"""""").Click(21,19)" & vbNewLine & _
'        vbTab & vbTab & "Threading.Thread.Sleep(500)" & vbNewLine & _
'        vbTab & vbTab & "Me.Window(""Name:=""""SciCalc"""""").WinObject(""Value:=""""="""""").Click(20,17)" & vbNewLine & _
'        vbTab & vbTab & "Threading.Thread.Sleep(500)" & vbNewLine & _
'        vbTab & vbTab & "Me.Window(""Name:=""""SciCalc"""""").Close()" & vbNewLine & _
'        vbTab & "End Sub" & vbNewLine & _
'        vbTab & "Public Shared Sub Main(args() as string)" & vbNewLine & _
'        vbTab & vbTab & "Dim a as New X()" & vbNewLine & _
'        vbTab & "End Sub" & vbNewLine & _
'        "End Class" & vbNewLine & _
'        ""
'End Sub


#End Region