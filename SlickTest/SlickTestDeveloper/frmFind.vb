''' <summary>
''' This class provides two subroutines used to:
''' Find (find the first instance of a search term)
''' Find Next (find other instances of the search term after the first one is found)
''' </summary>
''' <remarks></remarks>

Public Class frmFind

    Private TextBoxControl As TextEditor.TextEditorBox
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

    Private Sub DoFind()
        Dim StartPosition As Integer = 1
        Dim offset As Integer = TextBoxControl.ActiveTextAreaControl.Caret.Offset + 1
        Dim SearchType As Microsoft.VisualBasic.CompareMethod
        If chkMatchCase.Checked = True Then
            SearchType = CompareMethod.Binary
        Else
            SearchType = CompareMethod.Text
        End If

        'Dim TempOffset As Integer = offset
        If (ChkStringReverse.Checked = False) Then
            'Stupid Kludge, but I can't get the start selection location :(
            If (TextBoxControl.ActiveTextAreaControl.SelectionManager.HasSomethingSelected = True) Then
                offset = TextBoxControl.ActiveTextAreaControl.SelectionManager.SelectionCollection(0).Offset + TextBoxControl.ActiveTextAreaControl.SelectionManager.SelectionCollection(0).Length + 1
            End If
            'For i As Integer = 0 To TextBoxControl.ActiveTextAreaControl.SelectionManager.SelectedText.Length
            '    If (TextBoxControl.ActiveTextAreaControl.SelectionManager.IsSelected(offset + i) = True) Then
            '        TempOffset += 1
            '    Else
            '        Exit For
            '    End If
            'Next
            'offset = TempOffset
            If (offset > TextBoxControl.Text.Length) Then offset = Me.TextBoxControl.Text.Length
            If (offset <= 0) Then offset = 1
            StartPosition = offset

            StartPosition = InStr(StartPosition, TextBoxControl.Text, txtSearchTerm.Text, SearchType)
        Else
            If (TextBoxControl.ActiveTextAreaControl.SelectionManager.HasSomethingSelected = True) Then
                offset = (TextBoxControl.ActiveTextAreaControl.SelectionManager.SelectionCollection(0).Offset - TextBoxControl.ActiveTextAreaControl.SelectionManager.SelectionCollection(0).Length) + 1
            End If
            'Stupid Kludge, but I can't get the start selection location :(
            'For i As Integer = 0 To TextBoxControl.ActiveTextAreaControl.SelectionManager.SelectedText.Length


            '    If (TextBoxControl.ActiveTextAreaControl.SelectionManager.IsSelected(offset - i) = True) Then
            '        TempOffset -= 1
            '    Else

            '        Exit For
            '    End If
            'Next
            'offset = TempOffset
            If (offset > TextBoxControl.Text.Length) Then offset = Me.TextBoxControl.Text.Length
            If (offset <= 0) Then offset = 1
            StartPosition = offset
            StartPosition = InStrRev(TextBoxControl.Text, txtSearchTerm.Text, StartPosition, SearchType)
        End If


        If StartPosition = 0 Then
            MessageBox.Show("The string '" & txtSearchTerm.Text.ToString() & "' was not found.", "No Matches", MessageBoxButtons.OK, MessageBoxIcon.Asterisk)
            Exit Sub
        End If
        TextBoxControl.ActiveTextAreaControl.Caret.Position = TextBoxControl.Document.OffsetToPosition(StartPosition - 1)
        TextBoxControl.ActiveTextAreaControl.SelectionManager.SetSelection(TextBoxControl.Document.OffsetToPosition(StartPosition - 1), TextBoxControl.Document.OffsetToPosition((StartPosition - 1) + txtSearchTerm.Text.Length))
        TextBoxControl.ActiveTextAreaControl.ScrollToCaret()
        TextBoxControl.Refresh()
    End Sub

    Private Sub KeyDownForm(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtSearchTerm.KeyDown, btnFind.KeyDown, chkMatchCase.KeyDown, ChkStringReverse.KeyDown
        If (e.KeyCode = Keys.Escape) Then
            Me.Close()
        End If
        If (e.KeyCode = Keys.Enter) Then
            If (e.Modifiers <> Keys.ControlKey) Then
                DoFind()
                e.Handled = True
            End If
        End If
    End Sub

    Private Sub frmFind_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        GC.Collect()
        Me.Enabled = False
    End Sub
End Class