Friend Class WebTableAPI
    Inherits WebElementAPI

    Public Sub New(ByVal element As Object)
        MyBase.New(element)
        If (IsTable(Me)) Then Return
        Throw New SlickTestAPIException("Element " & Me.ToString() & " is not a Table.")
    End Sub

    Public Shared Function IsTable(ByVal element As WebElementAPI) As Boolean
        If (element Is Nothing) Then Return False
        For Each tableTag As String In WebTagDefinitions.TableTags
            If (tableTag = element.TagName.ToLowerInvariant()) Then
                Return True
            End If
        Next
        Return False
    End Function

    Protected Friend ReadOnly Property HTMLTable() As mshtml.IHTMLTable
        Get
            Return DirectCast(HTMLElement, mshtml.IHTMLTable)
        End Get
    End Property

    Protected Friend ReadOnly Property HTMLTableSection() As mshtml.IHTMLTableSection
        Get
            Return DirectCast(HTMLElement, mshtml.IHTMLTableSection)
        End Get
    End Property

    Protected Friend ReadOnly Property HTMLTableSection2() As mshtml.IHTMLTableSection2
        Get
            Return DirectCast(HTMLElement, mshtml.IHTMLTableSection2)
        End Get
    End Property

    Protected Friend ReadOnly Property HTMLTableSection3() As mshtml.IHTMLTableSection3
        Get
            Return DirectCast(HTMLElement, mshtml.IHTMLTableSection3)
        End Get
    End Property

    Protected Friend ReadOnly Property HTMLTable2() As mshtml.IHTMLTable2
        Get
            Return DirectCast(HTMLElement, mshtml.IHTMLTable2)
        End Get
    End Property

    Protected Friend ReadOnly Property HTMLTable3() As mshtml.IHTMLTable3
        Get
            Return DirectCast(HTMLElement, mshtml.IHTMLTable3)
        End Get
    End Property

    Private Function IsActualTable() As Boolean
        Return Me.TagName.ToLowerInvariant() = "table"
    End Function

    Public Function GetWidth() As Integer
        Return Convert.ToInt32(HTMLTable.width)
    End Function

    Public Function GetBorder() As Integer
        If (IsActualTable()) Then Return Convert.ToInt32(HTMLTable.border)
        Return 0
    End Function

    Public Function GetHeight() As Integer
        If (IsActualTable()) Then Return Convert.ToInt32(HTMLTable.height)
        Return 0
    End Function

#Region "TBody"
    Public Function GetTBodiesCount() As Integer
        If (IsActualTable() = False) Then Return 0
        If (HTMLTable.tBodies Is Nothing) Then Return 0
        Return HTMLTable.tBodies.length
    End Function

    Protected Friend Function GetTBody(ByVal index As Integer) As mshtml.IHTMLTableSection
        If (HTMLTable.tBodies Is Nothing) Then Return Nothing
        Return DirectCast(HTMLTable.tBodies.item(, index), mshtml.IHTMLTableSection)
    End Function

    Public Function GetTBodyRow(ByVal TBodyIndex As Integer, ByVal RowIndex As Integer) As WebTableRowAPI
        Return New WebTableRowAPI(GetTBody(TBodyIndex).rows.item(, RowIndex))
    End Function

    Public Function GetTBodyRowCount(ByVal TBodyIndex As Integer) As Integer
        Return GetTBody(TBodyIndex).rows.length
    End Function

#End Region

#Region "TFoot"
    Protected Friend Function GetTFoot(ByVal index As Integer) As mshtml.HTMLTableRowClass

        If (HTMLTable.tFoot Is Nothing) Then Return Nothing
        Return DirectCast(HTMLTable.tFoot.rows.item(, index), mshtml.HTMLTableRowClass)
    End Function

    Public Function GetTFootRowCount() As Integer
        If (IsActualTable() = False) Then Return 0
        If (HTMLTable.tFoot Is Nothing) Then Return 0
        Return HTMLTable.tFoot.rows.length
    End Function

    Public Function GetTFootRow(ByVal RowIndex As Integer) As WebTableRowAPI
        If (GetTFootRowCount() < RowIndex OrElse RowIndex < 0) Then Return Nothing
        Return New WebTableRowAPI(GetTFoot(RowIndex))
    End Function
#End Region

#Region "THead"
    Protected Friend Function GetTHead(ByVal index As Integer) As mshtml.HTMLTableRowClass
        If (HTMLTable.tHead Is Nothing) Then Return Nothing
        Return DirectCast(HTMLTable.tHead.rows.item(, index), mshtml.HTMLTableRowClass)
    End Function

    Public Function GetTHeadRowCount() As Integer
        If (IsActualTable() = False) Then Return 0
        If (HTMLTable.tHead Is Nothing) Then Return 0
        Return HTMLTable.tHead.rows.length
    End Function

    Public Function GetTHeadRow(ByVal RowIndex As Integer) As WebTableRowAPI
        If (GetTHeadRowCount() < RowIndex OrElse RowIndex < 0) Then Return Nothing
        Return New WebTableRowAPI(GetTHead(RowIndex))
    End Function
#End Region

    Public Function GetRowCount() As Integer
        If (IsActualTable()) Then
            Return HTMLTable.rows.length
        Else
            Return HTMLTableSection.rows.length
        End If
    End Function

    Public Function GetRow(ByVal index As Integer) As WebTableRowAPI
        If (GetRowCount() < index OrElse index < 0) Then Return Nothing
        If (IsActualTable()) Then
            Return New WebTableRowAPI(HTMLTable.rows.item(, index))
        Else
            Return New WebTableRowAPI(HTMLTableSection.rows.item(, index))
        End If
    End Function

    Public Function GetCell(ByVal RowIndex As Integer, ByVal CellIndex As Integer) As WebTableCellAPI
        Dim r As WebTableRowAPI = GetRow(RowIndex)
        If (r Is Nothing) Then Return Nothing
        Return r.GetCell(CellIndex)
    End Function

    Public Sub GotoFirstPage()
        HTMLTable2.firstPage()
    End Sub

    Public Sub GotoLastPage()
        HTMLTable2.lastPage()
    End Sub

    Private Const Table As String = "table"

    Public Function GetParentTable() As WebTableAPI
        Dim LoopCount As Integer = 1
        If (Me.TagName.ToLowerInvariant() = Table) Then 'must be a header, etc.
            LoopCount = 2
        End If
        Dim element As WebElementAPI = Me.GetParent()
        For i As Integer = 0 To LoopCount

            While (element.TagName.ToLowerInvariant() <> Table)
                element = element.GetParent()
                If (element Is Nothing) Then Return Nothing
            End While
        Next

        Return DirectCast(element, WebTableAPI)
    End Function

End Class
