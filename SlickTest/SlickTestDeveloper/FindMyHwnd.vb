Public Class FindMyHwnd
    Private Const TotalFlashes As Integer = 5 'doubles for the on and off
    Private Highlighter As UIControls.FlashHighlighter

    Public Sub New()

        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        ' Add any initialization after the InitializeComponent() call.

        Highlighter = New UIControls.FlashHighlighter()
    End Sub

    Private Sub SearchButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SearchButton.Click
        StartHwndSearch()
    End Sub

    Private Sub StartHwndSearch()
        Dim hwnd As IntPtr
        Try
            Dim handle As Int64 = Convert.ToInt64(Me.HwndTextBox.Text.Trim())
            hwnd = New IntPtr(handle)
        Catch ex As Exception
            System.Windows.Forms.MessageBox.Show("Hwnds are only numbers.  No string valeus allowed.", "Illegal value", MessageBoxButtons.OK)
            Me.HwndTextBox.Text = ""
            Return
        End Try

        If (Highlighter.Start(hwnd, TotalFlashes) = False) Then 'rec.X <= 0 OrElse rec.Y <= 0) Then
            System.Windows.Forms.MessageBox.Show("Hwnd is either invalid, on a second monitor or minimized.")
            Return
        End If
    End Sub

    Private Sub FindMyHwnd_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        Highlighter.Close()
    End Sub

    Private Sub HwndTextBox_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles HwndTextBox.KeyDown
        If (e.KeyCode = Keys.Enter) Then
            StartHwndSearch()
        End If
    End Sub
End Class