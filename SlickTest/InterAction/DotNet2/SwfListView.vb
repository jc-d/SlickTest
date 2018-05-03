'Updated On: 10/18/09

''' <summary>
''' A SwfListView is just a specialized SwfWinObject, and so it performs everything a SwfWinObject does.
''' </summary>
''' <remarks></remarks>
Public NotInheritable Class SwfListView
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
    ''' Gets the number of rows in the listview.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetRowCount() As Integer
        Return WindowsFunctions.ListView.GetItemCount(Me.Hwnd())
    End Function

    ''' <summary>
    ''' Determines if the object is a ListView.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function IsListView() As Boolean
        Return WindowsFunctions.ListView.IsListView(Me.Hwnd())
    End Function

    ''' <summary>
    ''' Gets the number of columns in the listview.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetColumnCount() As Integer
        Return WindowsFunctions.ListView.GetColumnCount(Me.Hwnd())
    End Function

    ''' <summary>
    ''' Gets the width of a column.
    ''' </summary>
    ''' <param name="ColumnText">The column text in the listview you wish to get the width of.  
    ''' This supports wild card matching.</param>
    ''' <returns>The width of a column.</returns>
    ''' <remarks></remarks>
    Public Function GetColumnWidth(ByVal ColumnText As String) As Integer
        Dim TempHwnd As IntPtr = Me.Hwnd()
        Return WindowsFunctions.ListView.GetColumnWidth(TempHwnd, WindowsFunctions.ListView.FindColumnNumber(TempHwnd, ColumnText))
    End Function

    ''' <summary>
    ''' Gets the width of a column.
    ''' </summary>
    ''' <param name="ColumnNumber">The column in the listview you wish to get the width of.</param>
    ''' <returns>The width of a column.</returns>
    ''' <remarks></remarks>
    Public Function GetColumnWidth(ByVal ColumnNumber As Integer) As Integer
        Dim TempHwnd As IntPtr = Me.Hwnd()
        Return WindowsFunctions.ListView.GetColumnWidth(TempHwnd, ColumnNumber)
    End Function

    ''' <summary>
    ''' Returns the text of a single row.
    ''' </summary>
    ''' <param name="Row">The row number you wish to get.</param>
    ''' <returns>The array will include each column within the row.</returns>
    ''' <remarks></remarks>
    Public Function GetRow(ByVal Row As Integer) As String()
        Return WindowsFunctions.ListView.GetRow(Me.Hwnd(), Row)
    End Function

    ''' <summary>
    ''' Returns the entire listview in one large multi-dimensional array.
    ''' </summary>
    ''' <returns>The array will be returned with the row first and the column second.</returns>
    ''' <remarks>Example Usage:<p/>
    ''' Dim str(,) as String = Me.Window(MyWindow).ListView(MyListView).GetAll()<p/>
    ''' Dim iRow as integer = 1<p/>
    ''' Dim iCol as integer = 2<p/>
    ''' System.Windows.Forms.MessageBox.Show(str(iRow,iCol))'prints out the second row, third column into a messagebox.
    ''' </remarks>
    Public Function GetAll() As String(,)
        Return WindowsFunctions.ListView.GetEntireList(Me.Hwnd())
    End Function

    ''' <summary>
    ''' Returns a column header's text based upon the column index given.
    ''' </summary>
    ''' <param name="ColumnNumber">Index starting with 0 which represents the column
    ''' which the text will be returned for.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetColumnHeaderText(ByVal ColumnNumber As Integer) As String
        Return WindowsFunctions.ListView.GetColumnName(Me.Hwnd(), ColumnNumber)
    End Function

    ''' <summary>
    ''' Returns a nicely formatted string of the entire listview, which can be helpful for debugging.
    ''' </summary>
    ''' <returns>The string will be formatted as follows:<p/>
    ''' Columns: col1, col2, col3<p/>
    ''' Row 1: rowitem1, rowitem2,rowitem3<p/>
    ''' Row 2: rowitem1, rowitem2, rowitem3</returns>
    ''' <remarks></remarks>
    Public Function GetAllFormatted() As String
        Return WindowsFunctions.ListView.GetEntireListForPrinting(Me.Hwnd())
    End Function

    ''' <summary>
    ''' Sets a column's width.
    ''' </summary>
    ''' <param name="ColumnNumber">The row in the listview you wish to set the width for.</param>
    ''' <param name="SizeInPixels">The width of the column in pixels.</param>
    ''' <remarks></remarks>
    Public Sub SetColumnWidth(ByVal ColumnNumber As Integer, ByVal SizeInPixels As Integer)
        WindowsFunctions.ListView.SetColumnWidth(Me.Hwnd(), ColumnNumber, SizeInPixels)
    End Sub

    ''' <summary>
    ''' Sets a column's width.
    ''' </summary>
    ''' <param name="ColumnText">The column text in the list view you wish to set the width for.  
    ''' This supports wild card matching. </param>
    ''' <param name="SizeInPixels">The width of the column in pixels.</param>
    ''' <remarks></remarks>
    Public Sub SetColumnWidth(ByVal ColumnText As String, ByVal SizeInPixels As Integer)
        Dim TempHwnd As IntPtr = Me.Hwnd()
        WindowsFunctions.ListView.SetColumnWidth(TempHwnd, WindowsFunctions.ListView.FindColumnNumber(TempHwnd, ColumnText), SizeInPixels)
    End Sub

    ''' <summary>
    ''' Gets the column header text for all the columns.
    ''' </summary>
    ''' <returns>An array of all the column's text in the listview.</returns>
    ''' <remarks></remarks>
    Public Function GetAllColumnsHeaderText() As String()
        Return WindowsFunctions.ListView.GetColumns(Me.Hwnd())
    End Function

    ''' <summary>
    ''' Gets the text of a cell within the list view based upon the row number and the column number given.
    ''' </summary>
    ''' <param name="RowNumber">The row in the listview you wish to get the text of.</param>
    ''' <param name="ColumnNumber">The column number in the list view you wish to get text for.</param>
    ''' <returns>The text for the row/column, if any exists.</returns>
    ''' <remarks>Index for both row and column starts at 0</remarks>
    Public Function GetRowText(ByVal RowNumber As Integer, ByVal ColumnNumber As Integer) As String
        Return APIControls.ListViewWindowsAPI.ReadListViewItem(Me.Hwnd(), RowNumber, ColumnNumber)
    End Function

    ''' <summary>
    ''' Gets the text of a cell within the list view based upon the row number and the column text given.
    ''' </summary>
    ''' <param name="RowNumber">The row in the listview you wish to get the text of.</param>
    ''' <param name="ColumnText">The column text in the list view you wish to get text for.  
    ''' This supports wild card matching. </param>
    ''' <returns>The text for the row/column, if any exists.</returns>
    ''' <exception cref="Exception">If no column text matches the columns in the WinObject, this will throw an exception.</exception>
    ''' <remarks></remarks>
    Public Function GetRowText(ByVal RowNumber As Integer, ByVal ColumnText As String) As String
        Dim TempHwnd As IntPtr = Me.Hwnd()
        Return APIControls.ListViewWindowsAPI.ReadListViewItem(TempHwnd, RowNumber, WindowsFunctions.ListView.FindColumnNumber(TempHwnd, ColumnText))
    End Function

    ''' <summary>
    ''' Gets the first column's text.
    ''' </summary>
    ''' <param name="RowNumber">The row in the listview you wish to get the text of.</param>
    ''' <returns>The first column (with an index of 0).</returns>
    ''' <remarks></remarks>
    Public Function GetRowText(ByVal RowNumber As Integer) As String
        Return GetRowText(RowNumber, 0)
    End Function

    ''' <summary>
    ''' Returns all the row numbers that are selected.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetSelectedRowNumbers() As Integer()
        Return WindowsFunctions.ListView.GetSelectedItems(Me.Hwnd())
    End Function

    ''' <summary>
    ''' Get's the first column's text for each row that is selected.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetSelectedRowsText() As String()
        Dim Rows As New System.Collections.Generic.List(Of String)()
        For Each row As Integer In WindowsFunctions.ListView.GetSelectedItems(Me.Hwnd())
            Rows.Add(Me.GetRowText(row))
        Next
        Return Rows.ToArray()
    End Function

    ''' <summary>
    ''' First unselects all items and then selects the index given.
    ''' </summary>
    ''' <param name="Index">Item to select.</param>
    ''' <remarks></remarks>
    Public Sub SelectItem(ByVal Index As Integer)
        UnselectAll()
        SetSelectState(Index, True)
    End Sub

    ''' <summary>
    ''' Sets the select state of a given item.  This does not unselect all items first.
    ''' </summary>
    ''' <param name="Index">Item to select or unselect.</param>
    ''' <param name="SelectItem">Set the item to a selected or unselected state</param>
    ''' <remarks></remarks>
    Public Sub SetSelectState(ByVal Index As Integer, ByVal SelectItem As Boolean)
        APIControls.ListViewWindowsAPI.SetSelectedItem(Me.Hwnd, Index, SelectItem)
    End Sub

    ''' <summary>
    ''' First unselects all items and then selects the indexes given.
    ''' </summary>
    ''' <param name="ItemIndexes">Items to select.</param>
    ''' <remarks></remarks>
    Public Sub SelectItems(ByVal ItemIndexes() As Integer)
        UnselectAll()
        WindowsFunctions.ListView.SetSelectedItems(Me.Hwnd, ItemIndexes)
    End Sub

    ''' <summary>
    ''' Unselects all rows then finds all rows of text and selects each one of them.
    ''' Throws exception if any row is not found.  
    ''' </summary>
    ''' <param name="RowsText">Text of rows to select. </param>
    ''' <remarks>No rows will be selected if exception occurs while finding text.</remarks>
    Public Sub SetSelectedRows(ByVal RowsText() As String)
        UnselectAll()
        Dim TempHwnd As IntPtr = Me.Hwnd()
        Dim RowNumbers As New System.Collections.Generic.List(Of Integer)()
        For Each Row As String In RowsText
            RowNumbers.Add(WindowsFunctions.ListView.FindRowNumber(TempHwnd, Row))
        Next
        WindowsFunctions.ListView.SetSelectedItems(Me.Hwnd(), RowNumbers.ToArray())
    End Sub

    ''' <summary>
    ''' Selects all items.
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub SelectAll()
        WindowsFunctions.ListView.SelectAll(Me.Hwnd())
    End Sub

    ''' <summary>
    ''' Unselects all items.
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub UnselectAll()
        WindowsFunctions.ListView.UnselectAll(Me.Hwnd())
    End Sub
End Class
