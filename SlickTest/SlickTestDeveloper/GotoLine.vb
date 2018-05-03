Public Class GotoLine

    Private TextBoxControl As TextEditor.TextEditorBox

    Private Sub GoToLineButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GoToLineButton.Click
        'TextBoxControl.ActiveTextAreaControl.Caret.Line = Me.LineNumberTextBox.Text - 1
        Try
            Me.TextBoxControl.ActiveTextAreaControl.TextArea.MotherTextAreaControl.JumpTo(Me.LineNumberTextBox.Text - 1)
        Catch ex As Exception
        End Try
        Me.TextBoxControl.Refresh()
        Me.Close()
    End Sub

    Public Sub New(ByVal TextBox As TextEditor.TextEditorBox)
        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        TextBoxControl = TextBox
        Me.Enabled = True
        ' Add any initialization after the InitializeComponent() call.
    End Sub

    Private Sub GotoLine_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        GC.Collect()
        Me.Enabled = False
    End Sub

    Private Sub GotoLine_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Me.LineNumberTextBox.Text = Convert.ToString(TextBoxControl.ActiveTextAreaControl.Caret.Line + 1)
        Me.LineNumberTextBox.SelectAll()
    End Sub

    Private Sub LineNumberTextBox_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles LineNumberTextBox.KeyDown
        If (e.KeyCode = Keys.Enter) Then
            If (Me.LineNumberTextBox.Text <> "") Then
                Try
                    Me.TextBoxControl.ActiveTextAreaControl.TextArea.MotherTextAreaControl.JumpTo(Me.LineNumberTextBox.Text - 1)
                    'TextBoxControl.ActiveTextAreaControl.Caret.Line = Me.LineNumberTextBox.Text - 1
                Catch ex As Exception
                End Try
                TextBoxControl.Refresh()
            End If
            Me.Close()
        End If
        If (e.KeyCode = Keys.Escape) Then
            Me.Close()
        End If
    End Sub

    Private Sub KeyDownHandler(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles GoToLineButton.KeyDown
        If (e.KeyCode = Keys.Escape) Then
            Me.Close()
        End If
    End Sub

    Private Sub LineNumberTextBox_MaskInputRejected(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MaskInputRejectedEventArgs) Handles LineNumberTextBox.MaskInputRejected

    End Sub
End Class