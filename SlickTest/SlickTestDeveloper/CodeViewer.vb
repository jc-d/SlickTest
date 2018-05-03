Public Class CodeViewer
    Public IsClosed As Boolean
    Public Sub New()

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        IsClosed = False
    End Sub

    Private selectAll As Boolean

    Public Sub New(ByVal selectAllText As Boolean)
        InitializeComponent()
        selectAll = selectAllText
        IsClosed = False

    End Sub

    Private Sub CodeTextBox_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CodeTextBox.TextChanged
        If (selectAll) Then
            Timer1.Start()
        End If
    End Sub

    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        Me.CodeTextBox.SelectAll()
        Timer1.Stop()
    End Sub

    Private Sub CodeViewer_FormClosing(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        IsClosed = True
    End Sub

    Private Sub CodeViewer_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Me.CodeTextBox.SetHighlightStyleByExt(SlickTestDev.LanguageExt)
    End Sub
End Class