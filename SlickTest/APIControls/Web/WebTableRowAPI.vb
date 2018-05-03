Friend Class WebTableRowAPI
    Inherits WebElementAPI

    Public Sub New(ByVal element As Object)
        MyBase.New(element)
        If (IsTableRow(Me)) Then Return
        Throw New SlickTestAPIException("Element " & Me.ToString() & " is not a TableRow but a " & Me.TagName)
    End Sub

    Protected Friend ReadOnly Property HTMLTableRow() As mshtml.IHTMLTableRow
        Get
            Return DirectCast(HTMLElement, mshtml.IHTMLTableRow)
        End Get
    End Property

    Protected Friend ReadOnly Property HTMLTableRow2() As mshtml.IHTMLTableRow2
        Get
            Return DirectCast(HTMLElement, mshtml.IHTMLTableRow2)
        End Get
    End Property

    Protected Friend ReadOnly Property HTMLTableRow3() As mshtml.IHTMLTableRow3
        Get
            Return DirectCast(HTMLElement, mshtml.IHTMLTableRow3)
        End Get
    End Property

    Protected Friend ReadOnly Property HTMLTableCaption() As mshtml.HTMLTableCaptionClass
        Get
            Return DirectCast(HTMLElement, mshtml.HTMLTableCaptionClass)
        End Get
    End Property

    Private Function IsTableCaption() As Boolean
        Return Me.TagName.ToLowerInvariant() = "caption"
    End Function

    Public Shared Function IsTableRow(ByVal element As WebElementAPI) As Boolean
        If (element Is Nothing) Then Return False
        For Each tableTag As String In WebTagDefinitions.TableRowTags
            If (tableTag = element.TagName.ToLowerInvariant()) Then
                Return True
            End If
        Next
        Return False
    End Function

    Public Function GetNextRow() As WebTableRowAPI
        Dim element As WebElementAPI = Me.NextElement()
        If (IsTableRow(element)) Then Return New WebTableRowAPI(element)
        Return Nothing
    End Function

    Public Function GetPreviousRow() As WebTableRowAPI
        Dim element As WebElementAPI = Me.PreviousElement()
        If (IsTableRow(element)) Then Return New WebTableRowAPI(element)
        Return Nothing
    End Function

    Public Function GetCellCount() As Integer
        If (IsTableCaption()) Then
            Return -1
        Else
            Return HTMLTableRow.cells.length
        End If
    End Function

    Public Function GetRowIndex() As Integer
        If (IsTableCaption()) Then
            Return -1
        Else
            Return HTMLTableRow.rowIndex
        End If
    End Function

    Public Function GetAlign() As String
        If (IsTableCaption()) Then
            Return HTMLTableCaption.align
        Else
            Return HTMLTableRow.align
        End If
    End Function

    Public Function GetRowHeight() As Integer
        If (IsTableCaption()) Then
            Return -1
        Else
            Return Convert.ToInt32(HTMLTableRow2.height)
        End If
    End Function

    Public Function GetCell(ByVal index As Integer) As WebTableCellAPI
        If (GetCellCount() < index OrElse index < 0) Then Return Nothing
        Return New WebTableCellAPI(HTMLTableRow.cells.item(, index))
    End Function

    Public Function GetParentTable() As WebTableAPI
        Dim p As WebElementAPI = Me.GetParent()
        While (WebTableAPI.IsTable(p) = False)
            If (p Is Nothing) Then Return Nothing
            p = p.GetParent()
        End While
        Return New WebTableAPI(p)
    End Function

    ''' <summary>
    ''' nn for pixels or nn% for percentage length; offset for alignment char;
    ''' horizontal alignment in cells
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetCharOffset() As String
        If (IsTableCaption()) Then
            Return String.Empty
        Else
            Return HTMLTableRow3.chOff
        End If
    End Function
End Class
