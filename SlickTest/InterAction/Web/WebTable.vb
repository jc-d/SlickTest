Imports APIControls
''' <summary>
''' A WebTable is just a specialized WebElement and it supports 
''' everything a WebElement does.
''' </summary>
''' <remarks></remarks>
Public Class WebTable
    Inherits WebElement

#Region "Constructor"
    ''' <summary>
    ''' Creates a WebElement object.  
    ''' Do not directly create this object, instead use InterAct.[Object].WebElement([desc]).[Method]
    ''' </summary>
    ''' <param name="desc"></param>
    ''' <remarks></remarks>
    Protected Friend Sub New(ByVal desc As String)
        currentRectangle = New System.Drawing.Rectangle(0, 0, 0, 0)
        description = New System.Collections.Generic.List(Of APIControls.IDescription)
        Dim aDesc As UIControls.Description = UIControls.Description.Create()
        description.Add(aDesc)
        If (readDescription(desc) = False) Then
            Throw New InvalidExpressionException("Description not valid: " & desc)
            'error
        End If
        Init()
    End Sub

    Public Overrides ReadOnly Property ListOfSupportedHtmlTags() As System.Collections.Generic.List(Of String)
        Get
            Return HtmlTags
        End Get
    End Property

    Protected Friend Sub New(ByVal desc As APIControls.IDescription, ByVal descObj As System.Collections.Generic.List(Of APIControls.IDescription)) ', ByVal Values As System.Collections.Specialized.StringCollection, ByVal Names As System.Collections.Specialized.StringCollection)
        Me.description = descObj
        description.Add(desc)
        Init()
    End Sub

    ''' <summary>
    ''' Creates a WebElement object.  
    ''' Do not directly create this object, instead use InterAct.[Object].WebElement([desc]).[Method]
    ''' </summary>
    ''' <remarks></remarks>
    Protected Friend Sub New()
        'this is a do nothing case, to ignore errors...
        Init()
    End Sub

    ''' <summary>
    ''' Creates a WebElement object.  
    ''' Do not directly create this object, instead use InterAct.[Object].WebElement([desc]).[Method]
    ''' </summary>
    ''' <param name="desc"></param>
    ''' <remarks></remarks>
    Protected Friend Sub New(ByVal desc As APIControls.IDescription)
        description = New System.Collections.Generic.List(Of APIControls.IDescription)
        description.Add(desc)
        Init()
    End Sub

    ''' <summary>
    ''' Creates a WebElement object.  
    ''' Do not directly create this object, instead use InterAct.[Object].WebElement([desc]).[Method]
    ''' </summary>
    ''' <param name="desc"></param>
    ''' <remarks></remarks>
    Protected Friend Sub New(ByVal desc As System.Collections.Generic.List(Of APIControls.IDescription))
        Me.description = desc
        Init()
    End Sub

    Protected Friend Sub New(ByVal desc As UIControls.Description, ByVal descObj As System.Collections.Generic.List(Of APIControls.IDescription), ByVal reporter As IReport) ', ByVal Values As System.Collections.Specialized.StringCollection, ByVal Names As System.Collections.Specialized.StringCollection)
        Me.reporter = reporter
        Me.description = descObj
        description.Add(desc)
        Init()
    End Sub

    Protected Friend Overrides Sub Init()
        HtmlTags = APIControls.WebTagDefinitions.TableTags
    End Sub
#End Region

    Private Function GetTableAPI() As WebTableAPI
        Return New WebTableAPI(Me.CurrentElement)
    End Function

    ''' <summary>
    ''' Gets the direct parent's table.
    ''' </summary>
    ''' <returns>Returns nothing if no parent table can be found.</returns>
    ''' <remarks></remarks>
    Public Function GetParentTable() As WebTable
        ExistsWithException()
        Dim tbl As WebTableAPI = GetTableAPI()
        Dim ptbl As WebTableAPI = tbl.GetParentTable()
        If (ptbl Is Nothing) Then Return Nothing

        Dim desc As IDescription = UIControls.Description.ConvertApiToUiDescription( _
                       ptbl.CreateFullDescription())
        Dim parentElement As WebTable = DirectCast(BuildElement(desc, ElementTypes.Table), WebTable)

        parentElement.description.RemoveRange(1, parentElement.description.Count - 1) 'since we are building a new fully described
        'element we must clear out the description or the child element will cause detection problems.
        parentElement.description.Add(desc)
        Return parentElement
    End Function

    Public Function GetHeight() As Integer
        ExistsWithException()
        Return GetTableAPI().GetHeight()
    End Function

    Public Function GetWidth() As Integer
        ExistsWithException()
        Return GetTableAPI().GetWidth()
    End Function

#Region "Rows"

    ''' <summary>
    ''' The number of rows in the table.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetRowCount() As Integer
        ExistsWithException()
        Return GetTableAPI().GetRowCount()
    End Function

    Public Function GetTBodiesCount() As Integer
        ExistsWithException()
        Return GetTableAPI().GetTBodiesCount()
    End Function

    Public Function GetTBodyRowCount(ByVal TBodyIndex As Integer) As Integer
        ExistsWithException()
        Return GetTableAPI().GetTBodyRowCount(TBodyIndex)
    End Function

    Public Function GetTFootRowCount() As Integer
        ExistsWithException()
        Return GetTableAPI().GetTFootRowCount()
    End Function

    Public Function GetTHeadRowCount() As Integer
        ExistsWithException()
        Return GetTableAPI().GetTHeadRowCount()
    End Function

    Public Function GetTHeadRow(ByVal RowIndex As Integer) As WebTableRow
        ExistsWithException()
        Dim row As WebTableRowAPI = GetTableAPI().GetTHeadRow(RowIndex)
        If (row Is Nothing) Then Return Nothing

        Dim desc As IDescription = UIControls.Description.ConvertApiToUiDescription( _
                       row.CreateFullDescription())
        Dim rowElement As WebTableRow = DirectCast(BuildElement(desc, ElementTypes.TableRow), WebTableRow)

        rowElement.description.RemoveRange(1, rowElement.description.Count - 1) 'since we are building a new fully described
        'element we must clear out the description or the child element will cause detection problems.
        rowElement.description.Add(desc)
        Return rowElement
    End Function

    Public Function GetTBodyRow(ByVal TBodyIndex As Integer, ByVal RowIndex As Integer) As WebTableRow
        ExistsWithException()
        Dim row As WebTableRowAPI = GetTableAPI().GetTBodyRow(TBodyIndex, RowIndex)
        If (row Is Nothing) Then Return Nothing

        Dim desc As IDescription = UIControls.Description.ConvertApiToUiDescription( _
                       row.CreateFullDescription())
        Dim rowElement As WebTableRow = DirectCast(BuildElement(desc, ElementTypes.TableRow), WebTableRow)

        rowElement.description.RemoveRange(1, rowElement.description.Count - 1) 'since we are building a new fully described
        'element we must clear out the description or the child element will cause detection problems.
        rowElement.description.Add(desc)
        Return rowElement
    End Function

    Public Function GetTFootRow(ByVal RowIndex As Integer) As WebTableRow
        ExistsWithException()
        Dim row As WebTableRowAPI = GetTableAPI().GetTFootRow(RowIndex)
        If (row Is Nothing) Then Return Nothing

        Dim desc As IDescription = UIControls.Description.ConvertApiToUiDescription( _
                       row.CreateFullDescription())
        Dim rowElement As WebTableRow = DirectCast(BuildElement(desc, ElementTypes.TableRow), WebTableRow)

        rowElement.description.RemoveRange(1, rowElement.description.Count - 1) 'since we are building a new fully described
        'element we must clear out the description or the child element will cause detection problems.
        rowElement.description.Add(desc)
        Return rowElement
    End Function

    Public Function GetRow(ByVal Index As Integer) As WebTableRow
        ExistsWithException()
        Dim row As WebTableRowAPI = GetTableAPI().GetRow(Index)
        If (row Is Nothing) Then Return Nothing

        Dim desc As IDescription = UIControls.Description.ConvertApiToUiDescription( _
                       row.CreateFullDescription())
        Dim rowElement As WebTableRow = DirectCast(BuildElement(desc, ElementTypes.TableRow), WebTableRow)

        rowElement.description.RemoveRange(1, rowElement.description.Count - 1) 'since we are building a new fully described
        'element we must clear out the description or the child element will cause detection problems.
        rowElement.description.Add(desc)
        Return rowElement
    End Function

#End Region


    ''' <summary>
    ''' Gets the cell in a table.
    ''' </summary>
    ''' <param name="RowIndex">Row you want to get</param>
    ''' <param name="CellIndex">The cell in the row given</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetCell(ByVal RowIndex As Integer, ByVal CellIndex As Integer) As WebTableCell
        ExistsWithException()
        Dim cell As WebTableCellAPI = GetTableAPI().GetCell(RowIndex, CellIndex)
        If (cell Is Nothing) Then Return Nothing

        Dim desc As IDescription = UIControls.Description.ConvertApiToUiDescription( _
                       cell.CreateFullDescription())
        Dim cellElement As WebTableCell = DirectCast(BuildElement(desc, ElementTypes.TableCell), WebTableCell)

        cellElement.description.RemoveRange(1, cellElement.description.Count - 1) 'since we are building a new fully described
        'element we must clear out the description or the child element will cause detection problems.
        cellElement.description.Add(desc)
        Return cellElement
    End Function
End Class
