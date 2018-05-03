''' <summary>
''' This class provides four subroutines used to:
''' Find (find the first instance of a search term)
''' Find Next (find other instances of the search term after the first one is found)
''' Replace (replace the current selection with replacement text)
''' Replace All (replace all instances of search term with replacement text)
''' </summary>
''' <remarks></remarks>

Public Class frmReplace

    Private TextBoxControl As TextEditor.TextEditorBox
    Private RealEnter As Boolean = False
    Public Sub New(ByVal TextBox As TextEditor.TextEditorBox)
        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        ' Add any initialization after the InitializeComponent() call.
        TextBoxControl = TextBox
        txtSearchTerm.Text = TextBox.ActiveTextAreaControl.SelectionManager.SelectedText
    End Sub

    Private Sub btnFind_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFind.Click
        DoFind()
    End Sub

    Private Function DoFind(Optional ByVal ShowMessage As Boolean = True) As Boolean
        Dim StartPosition As Integer = 1
        Dim offset As Integer = TextBoxControl.ActiveTextAreaControl.Caret.Offset + 1
        Dim SearchType As Microsoft.VisualBasic.CompareMethod
        If chkMatchCase.Checked = True Then
            SearchType = CompareMethod.Binary
        Else
            SearchType = CompareMethod.Text
        End If

        Dim TempOffset As Integer = offset
        If (ChkStringReverse.Checked = False) Then
            'Stupid Kludge, but I can't get the start selection location :(
            For i As Integer = 0 To TextBoxControl.ActiveTextAreaControl.SelectionManager.SelectedText.Length

                If (TextBoxControl.ActiveTextAreaControl.SelectionManager.IsSelected(offset + i) = True) Then
                    TempOffset += 1
                Else

                    Exit For
                End If
            Next
            offset = TempOffset
            If (offset > TextBoxControl.Text.Length) Then offset = Me.TextBoxControl.Text.Length
            If (offset <= 0) Then offset = 1
            StartPosition = offset

            StartPosition = InStr(StartPosition, TextBoxControl.Text, txtSearchTerm.Text, SearchType)
        Else
            'Stupid Kludge, but I can't get the start selection location :(
            For i As Integer = 0 To TextBoxControl.ActiveTextAreaControl.SelectionManager.SelectedText.Length

                If (TextBoxControl.ActiveTextAreaControl.SelectionManager.IsSelected(offset - i) = True) Then
                    TempOffset -= 1
                Else
                    Exit For
                End If
            Next
            offset = TempOffset
            If (offset > TextBoxControl.Text.Length) Then offset = Me.TextBoxControl.Text.Length
            If (offset <= 0) Then offset = 1
            StartPosition = offset
            StartPosition = InStrRev(TextBoxControl.Text, txtSearchTerm.Text, StartPosition, SearchType)
        End If

        If StartPosition = 0 Then
            If (ShowMessage = True) Then
                MessageBox.Show("The string '" & txtSearchTerm.Text.ToString() & "' was not found.", "No Matches", MessageBoxButtons.OK, MessageBoxIcon.Asterisk)
            End If
            Return False
        End If
        TextBoxControl.ActiveTextAreaControl.Caret.Position = TextBoxControl.Document.OffsetToPosition(StartPosition - 1)
        TextBoxControl.ActiveTextAreaControl.SelectionManager.SetSelection(TextBoxControl.Document.OffsetToPosition(StartPosition - 1), TextBoxControl.Document.OffsetToPosition((StartPosition - 1) + txtSearchTerm.Text.Length))
        TextBoxControl.ActiveTextAreaControl.ScrollToCaret()
        TextBoxControl.Refresh()
        Return True
    End Function

    Private Sub btnReplace_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReplace.Click
        Dim SearchType As System.StringComparison
        If chkMatchCase.Checked = True Then
            SearchType = System.StringComparison.Ordinal
        Else
            SearchType = System.StringComparison.OrdinalIgnoreCase
        End If
        If TextBoxControl.ActiveTextAreaControl.SelectionManager.HasSomethingSelected = False Then
            If (DoFind(False) = False) Then
                Return
            End If
        Else
            If (TextBoxControl.ActiveTextAreaControl.SelectionManager.SelectedText.IndexOf(txtSearchTerm.Text, SearchType) >= 0) Then
                Dim NewText As String = TextBoxControl.ActiveTextAreaControl.SelectionManager.SelectedText.Replace(txtSearchTerm.Text, txtReplacementText.Text)
                TextBoxControl.Delete()
                TextBoxControl.Document.Insert(TextBoxControl.ActiveTextAreaControl.Caret.Offset, NewText)
                TextBoxControl.Refresh()
                Return
            Else
                Return
            End If
        End If
    End Sub

    Private Sub btnReplaceAll_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReplaceAll.Click
        TextBoxControl.Text = TextBoxControl.Text.Replace(txtSearchTerm.Text, txtReplacementText.Text)
        TextBoxControl.Refresh()
    End Sub

    Private Sub KeyDownForm(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtSearchTerm.KeyDown, txtReplacementText.KeyDown, btnFind.KeyDown, btnReplace.KeyDown, btnReplaceAll.KeyDown, chkMatchCase.KeyDown, ChkStringReverse.KeyDown
        If (e.KeyCode = Keys.Escape) Then
            Me.Close()
        End If
    End Sub

    Private Sub KeyPressHandler(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtSearchTerm.KeyPress, txtSearchTerm.KeyPress
        If ((Convert.ToInt32(e.KeyChar) = 13) = True) Then
            If (RealEnter = False) Then
                e.Handled = True
                DoFind()
            Else
                RealEnter = False
            End If
        ElseIf ((Convert.ToInt32(e.KeyChar) = 10) = True) Then
            e.KeyChar = Chr(13)
            RealEnter = True
        End If
    End Sub

    Private Sub frmReplace_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        GC.Collect()
        Me.Enabled = False
    End Sub
End Class