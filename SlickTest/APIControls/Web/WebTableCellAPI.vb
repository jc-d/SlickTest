'http://www.december.com/html/4/element/th.html
'http://htmlhelp.com/reference/html40/tables/th.html

Friend Class WebTableCellAPI
    Inherits WebElementAPI

    Public Sub New(ByVal element As Object)
        MyBase.New(element)
        If (IsTableCell(Me)) Then Return
        Throw New SlickTestAPIException("Element " & Me.ToString() & " is not a TableCell but a " & Me.TagName)
    End Sub

    Public Shared Function IsTableCell(ByVal element As WebElementAPI) As Boolean
        If (element Is Nothing) Then Return False
        For Each tableTag As String In WebTagDefinitions.TableCellTags
            If (tableTag = element.TagName.ToLowerInvariant()) Then
                Return True
            End If
        Next
        Return False
    End Function

    Public Function GetNextCellInRow() As WebTableCellAPI
        Dim element As WebElementAPI = Me.NextElement()
        If (IsTableCell(element)) Then Return New WebTableCellAPI(element)
        Return Nothing
    End Function

    Public Function GetPreviousCellInRow() As WebTableCellAPI
        Dim element As WebElementAPI = Me.PreviousElement()
        If (IsTableCell(element)) Then Return New WebTableCellAPI(element)
        Return Nothing
    End Function

    Protected Friend ReadOnly Property HTMLTableCell() As mshtml.IHTMLTableCell
        Get
            Return DirectCast(HTMLElement, mshtml.IHTMLTableCell)
        End Get
    End Property

    Protected Friend ReadOnly Property HTMLTableCell2() As mshtml.IHTMLTableCell2
        Get
            Return DirectCast(HTMLElement, mshtml.IHTMLTableCell2)
        End Get
    End Property

    Public Function GetCellIndex() As Integer
        Return HTMLTableCell.cellIndex
    End Function

    Public Function GetColumnSpan() As Integer
        Return HTMLTableCell.colSpan
    End Function

    Public Function IsWrapping() As Boolean
        Return Not HTMLTableCell.noWrap
    End Function

    Public Function GetRowSpan() As Integer
        Return HTMLTableCell.rowSpan
    End Function

    Public Function GetWidth() As Integer
        Return Convert.ToInt32(HTMLTableCell.width)
    End Function

    Public Function GetAlign() As String
        Return HTMLTableCell.align
    End Function

    Public Function GetHeaders() As String
        Return HTMLTableCell2.headers
    End Function

    ''' <summary>
    ''' nn for pixels or nn% for percentage length; offset for alignment char;
    ''' horizontal alignment in cells
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetCharOffset() As String
        Return HTMLTableCell2.chOff
    End Function

    Public Function GetParentTable() As WebTableAPI
        Dim p As WebElementAPI = Me.GetParent()
        While (WebTableAPI.IsTable(p) = False)
            If (p Is Nothing) Then Return Nothing
            p = p.GetParent()
        End While
        Return New WebTableAPI(p)
    End Function

    Public Function GetParentTableRow() As WebTableRowAPI
        Dim p As WebElementAPI = Me.GetParent()
        While (WebTableRowAPI.IsTableRow(p) = False)
            If (p Is Nothing) Then Return Nothing
            p = p.GetParent()
        End While
        Return New WebTableRowAPI(p)
    End Function

End Class
