'Updated On: 10/18/09

''' <summary>
''' A SwfTreeView is just a specialized SwfWinObject, and so it 
''' performs everything a SwfWinObject does.
''' </summary>
''' <remarks></remarks>
Public NotInheritable Class SwfTreeView
    Inherits SwfWinObject

    Private internalWindow As SwfWindow

#Region "Constructors"

    Protected Friend Sub New(ByVal win As SwfWindow)
        internalWindow = win
        Me.description = win.description
        Me.currentHwnd = win.currentHwnd
        Me.reporter = win.reporter
        Me.currentRectangle = win.currentRectangle
    End Sub

    Protected Friend Sub New()
        'this is a do nothing case, to ignore errors...
    End Sub

#End Region

    ''' <summary>
    ''' Gets the first index of the item's text that matches the string provided.
    ''' </summary>
    ''' <param name="Text">The text to search for.</param>
    ''' <param name="UseWildCard">Allows the search to use wild card matching.</param>
    ''' <exception cref="Exception">If the text is not found an exception will be raised.</exception>
    ''' <returns>Returns the first index of the string being searched for.</returns>
    ''' <remarks></remarks>
    Public Function GetItemIndex(ByVal Text As String, ByVal UseWildCard As Boolean) As Integer
        Dim Index As Integer
        If (UseWildCard = True) Then
            Index = WindowsFunctions.TreeView.GetItemIndexByLikeText(Me.Hwnd(), Text)
        Else
            Index = WindowsFunctions.TreeView.GetItemIndexByExactText(Me.Hwnd(), Text)
        End If
        If (Index = -1) Then
            Throw New SlickTestUIException("Unable to find TreeView item '" & Text & "'.")
        End If
        Return Index
    End Function

    ''' <summary>
    ''' Gets the first index of the item's text that matches the string provided.
    ''' </summary>
    ''' <param name="Text">The exact text to search for.</param>
    ''' <exception cref="Exception">If the text is not found an exception will be raised.</exception>
    ''' <returns>Returns the first index of the string being searched for.</returns>
    ''' <remarks></remarks>
    Public Function GetItemIndex(ByVal Text As String) As Integer
        Return GetItemIndex(Text, False)
    End Function

    ''' <summary>
    ''' Get's the text for an item in the TreeView.
    ''' </summary>
    ''' <param name="Index">The item number in the TreeView.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetItemText(ByVal Index As Integer) As String
        Dim TmpTVHwnd As IntPtr = Me.Hwnd()
        Dim TmpHwnd As IntPtr = WindowsFunctions.TreeView.GetItemHwndByIndex(TmpTVHwnd, Index)
        If (TmpHwnd = IntPtr.Zero) Then Throw New SlickTestUIException("Unable to find item with the index '" & Index & "'.")
        Return WindowsFunctions.TreeView.GetText(TmpTVHwnd, TmpHwnd)
    End Function

    ''' <summary>
    ''' Gets the text for all the items in the TreeView.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetAllItems() As String()
        Return WindowsFunctions.TreeView.GetAllItemsText(Me.Hwnd()).ToArray()
    End Function

    ''' <summary>
    ''' Expands an item in a TreeView using the text of the item.
    ''' </summary>
    ''' <param name="ItemText">The text of the item.</param>
    ''' <remarks></remarks>
    Public Sub ExpandItem(ByVal ItemText As String)
        Dim TmpTVHwnd As IntPtr = Me.Hwnd()
        WindowsFunctions.TreeView.ExpandCollapseItem(TmpTVHwnd, ItemText, True)
    End Sub

    ''' <summary>
    ''' Collapses an item in a TreeView using the text of the item.
    ''' </summary>
    ''' <param name="ItemText">The text of the item.</param>
    ''' <remarks></remarks>
    Public Sub CollapseItem(ByVal ItemText As String)
        Dim TmpTVHwnd As IntPtr = Me.Hwnd()
        WindowsFunctions.TreeView.ExpandCollapseItem(TmpTVHwnd, ItemText, False)
    End Sub

    ''' <summary>
    ''' Expands an item in a TreeView using the index of the item.
    ''' </summary>
    ''' <param name="ItemIndex">The index of the item.</param>
    ''' <remarks></remarks>
    Public Sub ExpandItem(ByVal ItemIndex As Integer)
        Dim TmpTVHwnd As IntPtr = Me.Hwnd()
        WindowsFunctions.TreeView.ExpandCollapseItem(TmpTVHwnd, ItemIndex, True)
    End Sub

    ''' <summary>
    ''' Collapses an item in a TreeView using the index of the item.
    ''' </summary>
    ''' <param name="ItemIndex">The index of the item.</param>
    ''' <remarks></remarks>
    Public Sub CollapseItem(ByVal ItemIndex As Integer)
        Dim TmpTVHwnd As IntPtr = Me.Hwnd()
        WindowsFunctions.TreeView.ExpandCollapseItem(TmpTVHwnd, ItemIndex, False)
    End Sub

    ''' <summary>
    ''' Expands items and then additional items below the first item in 
    ''' a TreeView using the text of the item.
    ''' </summary>
    ''' <param name="ItemsText">The text of each of the items to expand.</param>
    ''' <remarks></remarks>
    Public Sub ExpandItems(ByVal ItemsText() As String)
        Dim TmpTVHwnd As IntPtr = Me.Hwnd()
        For i As Integer = 0 To ItemsText.Length - 1
            WindowsFunctions.TreeView.ExpandCollapseItem(TmpTVHwnd, ItemsText(i), True)
        Next
    End Sub

    ''' <summary>
    ''' Collapses items and then additional items below the first item in 
    ''' a TreeView using the text of the item.
    ''' </summary>
    ''' <param name="ItemsText">The text of each of the items to expand.</param>
    ''' <remarks></remarks>
    Public Sub CollapseItems(ByVal ItemsText() As String)
        Dim TmpTVHwnd As IntPtr = Me.Hwnd()
        For i As Integer = 0 To ItemsText.Length - 1
            WindowsFunctions.TreeView.ExpandCollapseItem(TmpTVHwnd, ItemsText(i), False)
        Next
    End Sub

    ''' <summary>
    ''' Selects an item by the index.
    ''' </summary>
    ''' <param name="Index">The index of the item.</param>
    ''' <remarks>NOTE: At present, this ultimately uses the item's text
    ''' to select the item.  If multiple items have the same text, this may
    ''' select the wrong item.  In the future this will be purely index based.</remarks>
    Public Sub SetSelectedItem(ByVal Index As Integer)
        'WindowsFunctions.TreeView.SetSelectedItem(Me.Hwnd(), Index, True)
        Dim Text As String = Me.GetItemText(Index)
        SetSelectedItem(Text)
    End Sub

    ''' <summary>
    ''' Gets the index of the selected item.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks>NOTE: At present, this ultimately uses the selected item's text
    ''' to find the item.  If multiple items have the same text, this may get the wrong
    ''' item index.  In the future this will just find the selected item index.</remarks>
    Public Function GetSelectedItemNumber() As Integer
        Dim Text As String = Me.GetSelectedText()
        Return Me.GetItemIndex(Text, False)
    End Function

    ''' <summary>
    ''' Selects an item in the treeview based upon the text.
    ''' </summary>
    ''' <param name="Text">The exact text of the treeview.</param>
    ''' <remarks></remarks>
    Public Sub SetSelectedItem(ByVal Text As String)
        WindowsFunctions.TreeView.SelectVisibleItem(Me.Hwnd(), Text, True)
    End Sub

    ''' <summary>
    ''' Gets the item count of the treeview.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks>If you add a node beyond 32767 this returns a negative value. 
    ''' After adding 65536 nodes the count returns to zero.</remarks>
    Public Function GetItemCount() As Integer
        Return WindowsFunctions.TreeView.GetItemCount(Me.Hwnd())
    End Function

    ''' <summary>
    ''' Gets the text for the TreeView item that is selected.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetSelectedText() As String
        Return WindowsFunctions.TreeView.GetSelectedItemText(Me.Hwnd())
    End Function

    ''' <summary>
    ''' Determines if the object is a TreeView.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function IsTreeView() As Boolean
        Return WindowsFunctions.TreeView.IsTreeView(Me.Hwnd())
    End Function
End Class
