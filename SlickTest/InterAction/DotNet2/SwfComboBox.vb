'Updated On: 8/23/08
''' <summary>
''' A SwfComboBox is just a specialized SwfWinObject, and it performs 
''' everything a SwfWinObject does.
''' </summary>
''' <remarks></remarks>
Public Class SwfComboBox
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
    ''' Tests to see if the object conforms to the requirements of a ComboBox.
    ''' </summary>
    ''' <returns>Returns true it it appears to conform to the requirements of a ComboBox.</returns>
    ''' <remarks></remarks>
    Public Function IsComboBox() As Boolean
        Try
            If (internalWindow.Exists() = False) Then
                Throw New InvalidExpressionException("Failed to find object: " & internalWindow.lastWindowFoundInfo)
            End If
            Dim CountVal As Long = -1
            If (WindowsFunctions.ComboBox.IsComboBox(internalWindow.currentHwnd) = False) Then
                Return False
            Else
                Return True
            End If

        Catch ex As Exception
            'Report(UIControls.Report.Fail, "Failed to perform requested action", _
            '    "Error: " & ex.Message)
            'If (internalWindow.IgnoreInternalErrors = False) Then
            Throw ex
            'End If
        End Try
        Return False
    End Function

    ''' <summary>
    ''' Returns the current number of items in a ComboBox.
    ''' </summary>
    ''' <returns>The current number of items in the ComboBox.  Returns -1 if the object is unable to
    ''' return the ComboBox count for any reason.</returns>
    ''' <exception cref="Exception">An exception maybe thrown if communication with the
    ''' ComboBox fails.</exception>
    ''' <remarks></remarks>
    Public Function GetItemCount() As Long
        Try
            Return WindowsFunctions.ComboBox.ComboBoxItemsCount(New IntPtr(Me.Hwnd()))
        Catch ex As Exception
            Throw ex
        End Try
        Return -1
    End Function

    ''' <summary>
    ''' Returns an array of all the ComboBox items.
    ''' </summary>
    ''' <returns>Returns the array of items in the ComboBox.  If this fails for any reason, it will 
    ''' return an array with one empty string.</returns>
    ''' <exception cref="Exception">An exception maybe thrown if communication with the
    ''' ComboBox fails.</exception>
    ''' <remarks></remarks>
    Public Function GetAllItems() As String()
        Dim retVal As String() = {""}
        Try
            Return WindowsFunctions.ComboBox.GetComboBoxItems(New IntPtr(Me.Hwnd()))
        Catch ex As Exception
            Throw ex
        End Try
        Return retVal
    End Function

    ''' <summary>
    ''' Selects a specific item in the ComboBox.
    ''' </summary>
    ''' <param name="itemNumber">The item to select.  This allows for all items
    ''' between 0 and count-1.</param>
    ''' <returns>Returns true if it is able to successfully select the item.  Returns false if it is
    ''' unable to select the item for any reason.</returns>
    ''' <exception cref="Exception">An exception maybe thrown if communication with the
    ''' ComboBox fails.</exception>
    ''' <remarks></remarks>
    Public Function SetSelectItem(ByVal ItemNumber As Integer) As Boolean
        Try
            Dim i As Boolean = WindowsFunctions.ComboBox.SelectComboBoxItem(New IntPtr(Me.Hwnd()), ItemNumber)
            Return i
        Catch ex As Exception
            Throw ex
        End Try
        Return False
    End Function

    ''' <summary>
    ''' Gets the index of the item currently selected in the ComboBox.
    ''' </summary>
    ''' <returns>Returns the index number of the currently selected item.  Returns -1 if it is
    ''' unable to get the selected the item for any reason.</returns>
    ''' <exception cref="Exception">An exception maybe thrown if communication with the
    ''' ComboBox fails.</exception>
    ''' <remarks></remarks>
    Public Function GetSelectedItemNumber() As Integer
        Try
            Dim i As Integer = WindowsFunctions.ComboBox.GetComboBoxSelectedItem(New IntPtr(Me.Hwnd()))
            Return i
        Catch ex As Exception
            Throw ex
        End Try
        Return -1
    End Function

    ''' <summary>
    ''' Returns the value of a specific item based upon the index value given.
    ''' </summary>
    ''' <param name="index">The item to return.  This allows for all items
    ''' between 0 and count-1.</param>
    ''' <returns>Returns the string value of the item.</returns>
    ''' <exception cref="Exception">An exception maybe thrown if communication with the
    ''' ComboBox fails.</exception>
    ''' <remarks></remarks>
    Public Function GetItemByIndex(ByVal Index As Integer) As String
        Try
            Dim AppHwnd As IntPtr = New IntPtr(Me.Hwnd())
            If (Index < 0 Or WindowsFunctions.ComboBox.ComboBoxItemsCount(AppHwnd) <= Index) Then
                Throw New IndexOutOfRangeException("Index " & Index & " was outside the range of the ComboBox (" & Me.Name & ").")
            End If
            Return WindowsFunctions.ComboBox.GetComboBoxItem(Index, AppHwnd)
        Catch ex As Exception
            Throw ex
        End Try
        Return ""
    End Function
End Class
