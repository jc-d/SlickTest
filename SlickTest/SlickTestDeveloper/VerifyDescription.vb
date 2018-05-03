Public Class VerifyDescription
    Private IsVisualBasic As Boolean = True
    Public compiler As DOTNETCompiler.ICompiler = New DOTNETCompiler.VBCompiler()
    Private SpecialAssemblies As System.Collections.Generic.List(Of String)

    Public Property IsVb() As Boolean
        Get
            Return IsVisualBasic
        End Get
        Set(ByVal value As Boolean)
            IsVisualBasic = value
            Me.DescriptionText.SetHighlighting(IsVisualBasic)
            SetupCompiler(value)
        End Set
    End Property

    Private Sub SetupCompiler(ByVal IsVisualBasic As Boolean)
        If (IsVisualBasic) Then
            compiler = New DOTNETCompiler.VBCompiler()
        Else
            compiler = New DOTNETCompiler.CSharpCompiler()
        End If
        compiler.Reset()
        compiler.SourceCode = New System.Collections.Generic.List(Of String)()
        compiler.ExecutablePath = System.IO.Path.GetDirectoryName(System.Windows.Forms.Application.ExecutablePath) & "\"
        compiler.ExecutableName = "Test.exe"
        compiler.IncludedAssemblies.AddRange(SpecialAssemblies)
        compiler.ShowConsoleUI = False
        compiler.CompileMethod = DOTNETCompiler.CompileType.SourceCode
        compiler.OptionStrict = False
        compiler.OptionExplicit = False
        compiler.TreatWarningsAsErrors = False
    End Sub

    Public Sub New()

        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        Dim path As String = System.IO.Path.GetDirectoryName(System.Windows.Forms.Application.ExecutablePath) & "\"

        ' Add any initialization after the InitializeComponent() call.
        SpecialAssemblies = New System.Collections.Generic.List(Of String)()
        SpecialAssemblies.Add(path & "InterAction.dll")
        SpecialAssemblies.Add(path & "APIControls.dll")
        SpecialAssemblies.Add(path & "WindowsHookLib.dll")
        SpecialAssemblies.Add(path & "WinAPI.dll")
        SpecialAssemblies.Add(path & "XmlSettings.dll")
        SpecialAssemblies.Add("System.Windows.Forms.dll")

#If (IncludeWeb = 1) Then
            SpecialAssemblies.Add(path & "Interop.SHDocVw.dll")
#End If
        AddHandler DescriptionText.ActiveTextAreaControl.TextArea.KeyDown, AddressOf DescriptionText_KeyDown
        AddHandler DescriptionText.Document.DocumentChanged, AddressOf DescriptionText_DocChanged

    End Sub

    Private Sub VerifyDescription_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Me.DescriptionText.Text = "Window(""name:=""""Progman"""";;processname:=""""explorer"""""").Exists(1)"
        If (IsVb = False) Then
            Me.DescriptionText.Text = Me.DescriptionText.Text.Replace("Window(", "Window(@")
        End If


    End Sub

    Private Function GetTextToCompile() As String
        Dim text As String
        If (IsVb) Then
            text = "Public Class TestRunner" & vbNewLine & _
            "Inherits UIControls.InterAct" & vbNewLine & _
            "    Public Shared Sub Main()" & vbNewLine & _
            "System.Windows.Forms.MessageBox.Show(new UIControls.InterAct("".\Test.stp"",false)." & _
            Me.DescriptionText.Text & ")" & vbNewLine & _
            "    End Sub" & vbNewLine & _
            "End Class"
        Else
            text = "public class TestRunner : UIControls.InterAct{" & vbNewLine & _
            "static void Main(){" & vbNewLine & _
            "System.Windows.Forms.MessageBox.Show(new UIControls.InterAct(@"".\Test.stp"",false)." & _
            Me.DescriptionText.Text.TrimEnd(New Char() {vbCr, vbLf, vbTab, " "c, ";"c}) & ".ToString()" & ");" _
            & vbNewLine & "}}"
        End If
        Return text
    End Function

    Private Sub DescriptionText_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs)
        If (e.KeyData = Keys.F5) Then
            Me.ToolStripStatusLabel1.Text = "Building..."
            e.Handled = True
            e.SuppressKeyPress = True
            compiler.SourceCode.Clear()
            compiler.SourceCode.Add(GetTextToCompile())
            If (compiler.Compile(True, "") = False) Then
                Me.ToolStripStatusLabel1.Text = "Failed to build... "
            Else
                Me.ToolStripStatusLabel1.Text = "Executing..."
            End If
        End If
    End Sub

    Private Sub DescriptionText_DocChanged(ByVal sender As Object, ByVal e As ICSharpCode.TextEditor.Document.DocumentEventArgs)

        If (Me.DescriptionText.Text.Contains(vbCr) OrElse Me.DescriptionText.Text.Contains(vbLf)) Then
            EnterAsExecuteTimer.Start()
        End If
    End Sub


    Private Sub EnterAsExecuteTimer_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles EnterAsExecuteTimer.Tick
        EnterAsExecuteTimer.Stop()
        Me.DescriptionText.Text = Me.DescriptionText.Text.Replace(vbCr, "").Replace(vbLf, "")
        DescriptionText_KeyDown(Nothing, New KeyEventArgs(Keys.F5))
    End Sub

    Private Sub RunToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RunToolStripMenuItem.Click
        DescriptionText_KeyDown(Nothing, New KeyEventArgs(Keys.F5))
    End Sub

    Private Sub SeeExactCodeToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SeeExactCodeToolStripMenuItem.Click
        Try
            Dim text As String = compiler.SourceCode(0) & vbNewLine & "-------------" & vbNewLine
            For Each ErrorMessage As String In compiler.Errors
                text += "* " & ErrorMessage & vbNewLine
            Next
            System.Windows.Forms.MessageBox.Show(text)
        Catch ex As Exception
            System.Windows.Forms.MessageBox.Show("Code has yet to be compiled.")
        End Try
    End Sub

    Private Sub ToolStripSplitButton1_ButtonClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripSplitButton1.ButtonClick
        DescriptionText_KeyDown(Nothing, New KeyEventArgs(Keys.F5))
    End Sub
End Class