''' <summary>
''' A ListBox is just a specialized WinObject, and so it performs everything a WinObject does.
''' </summary>
''' <remarks></remarks>
Public NotInheritable Class ListBox
    Inherits WinObject

    Private internalWindow As Window

#Region "Constructors"

    Protected Friend Sub New(ByVal win As Window)
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
    ''' Determines if the object is a ListBox.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function IsListBox() As Boolean
        Try
            Return WindowsFunctions.ListBox.IsListBox(Me.Hwnd())
        Catch ex As Exception
            Throw ex
        End Try
        Return False
    End Function

    ''' <summary>
    ''' Returns the current number of items in a ListBox.
    ''' </summary>
    ''' <returns>The current number of items in the ListBox.</returns>
    ''' <exception cref="Exception">An exception maybe thrown if communication with the
    ''' ListBox fails.</exception>
    ''' <remarks></remarks>
    Public Function GetItemCount() As Integer
        Return WindowsFunctions.ListBox.ListBoxItemsCount(Me.Hwnd())
    End Function

    ''' <summary>
    ''' Gets the number of selected items in a multi-select ListBox.
    ''' </summary>
    ''' <returns>Returns -1 when the ListBox isn't multi-select or their are no
    ''' selected items.</returns>
    ''' <remarks>This only works for a multi-select ListBox.</remarks>
    Public Function GetSelectCount() As Integer
        Return WindowsFunctions.ListBox.ListBoxCount(New IntPtr(Me.Hwnd))
    End Function

    ''' <summary>
    ''' Returns an array of all the ListBox items.
    ''' </summary>
    ''' <returns>Returns the array of items in the ListBox.  If this fails for any reason, it will 
    ''' return an array with one empty string.</returns>
    ''' <exception cref="Exception">An exception maybe thrown if communication with the
    ''' ListBox fails.</exception>
    ''' <remarks></remarks>
    Public Function GetAllItems() As String()
        Dim retVal As String() = {""}
        Try
            Return WindowsFunctions.ListBox.GetListBoxItems(Me.Hwnd())
        Catch ex As Exception
            Throw ex
        End Try
        Return retVal
    End Function

    ''' <summary>
    ''' Returns an array of all the selected ListBox items.
    ''' </summary>
    ''' <returns>Returns the array of selected items in the ListBox.  If this fails for any 
    ''' reason, it will return an array with one item with the value -1.  
    ''' </returns>
    ''' <exception cref="Exception">An exception maybe thrown if communication with the
    ''' ListBox fails.</exception>
    ''' <remarks>This requires a ListBox that supports multiple-selection.  If this is not a multi-selection
    ''' Listbox then this will return -1.</remarks>
    Public Function GetSelectedItems() As Integer()
        Dim retVal As Integer() = {-1}
        Try
            Return WindowsFunctions.ListBox.GetListBoxSelectedItems(Me.Hwnd())
        Catch ex As Exception
            Throw ex
        End Try
        Return retVal
    End Function

    ''' <summary>
    ''' Selects a specific item in the ListBox.
    ''' </summary>
    ''' <param name="ItemNumber">The item to select.  This allows for all items
    ''' between 0 and count-1.</param>
    ''' <returns>returns true if it is able to successfully select the item.  Returns false if it is
    ''' unable to select the item for any reason.</returns>
    ''' <exception cref="Exception">An exception maybe thrown if communication with the
    ''' ListBox fails.</exception>
    ''' <remarks></remarks>
    Public Function SetSelectedItem(ByVal ItemNumber As Integer) As Boolean
        Try
            Dim i As Boolean = WindowsFunctions.ListBox.SelectListBoxItem(Me.Hwnd(), ItemNumber)
            Return i
        Catch ex As Exception
            Throw ex
        End Try
        Return False
    End Function

    ''' <summary>
    ''' Gets the index of the item currently selected in the ListBox.
    ''' </summary>
    ''' <returns>returns the index number of the currently selected item.  Returns -1 if it is
    ''' unable to get the selected the item for any reason.</returns>
    ''' <exception cref="Exception">An exception maybe thrown if communication with the
    ''' ListBox fails.</exception>
    ''' <remarks></remarks>
    Public Function GetSelectedItemNumber() As Integer
        Try
            Return WindowsFunctions.ListBox.GetListBoxSelectedItem(Me.Hwnd())
        Catch ex As Exception
            Throw
        End Try
        Return -1
    End Function

    ''' <summary>
    ''' Returns the value of a specific item based upon the index value given.
    ''' </summary>
    ''' <param name="index">The item to return.  This allows for all items
    ''' between 0 and count-1.</param>
    ''' <returns>Returns the string value of the item.</returns>
    ''' <remarks></remarks>
    Public Function GetItemByIndex(ByVal index As Integer) As String
        Try
            Dim AppHwnd As IntPtr = Me.Hwnd()
            If (index < 0 Or WindowsFunctions.ListBox.ListBoxItemsCount(AppHwnd) <= index) Then
                Throw New IndexOutOfRangeException("Index " & index & " was outside the range of the ListBox (" & Me.Name & ").")
            End If
            Return WindowsFunctions.ListBox.GetListBoxItem(index, AppHwnd)
        Catch ex As Exception
            Throw
        End Try
        Return ""
    End Function

End Class
