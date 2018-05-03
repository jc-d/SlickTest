Imports APIControls
''' <summary>
''' A WebTableRow is just a specialized WebElement and it supports 
''' everything a WebElement does.
''' </summary>
''' <remarks>Supports tr and caption element tags.</remarks>
Public Class WebTableRow
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
        HtmlTags = APIControls.WebTagDefinitions.TableRowTags
    End Sub
#End Region

    Private Function GetTableRowAPI() As WebTableRowAPI
        Return New WebTableRowAPI(Me.CurrentElement)
    End Function

    ''' <summary>
    ''' Gets the next row if a row exists.
    ''' </summary>
    ''' <returns>Returns Nothing if a next row
    ''' does not exist.</returns>
    ''' <remarks></remarks>
    Public Function GetNextRow() As WebTableRow
        ExistsWithException()
        Dim tr As WebTableRowAPI = GetTableRowAPI()
        tr = tr.GetNextRow()
        If (tr Is Nothing) Then Return Nothing

        Dim desc As IDescription = UIControls.Description.ConvertApiToUiDescription( _
                       tr.CreateFullDescription())
        Dim nextElement As WebTableRow = DirectCast(BuildElement(desc, ElementTypes.TableRow), WebTableRow)

        nextElement.description.RemoveRange(1, nextElement.description.Count - 1) 'since we are building a new fully described
        'element we must clear out the description or the child element will cause detection problems.
        nextElement.description.Add(desc)
        Return nextElement
    End Function

    ''' <summary>
    ''' Gets the previous row if a row exists.
    ''' </summary>
    ''' <returns>Returns Nothing if a previous row
    ''' does not exist.</returns>
    ''' <remarks></remarks>
    Public Function GetPreviousRow() As WebTableRow
        ExistsWithException()
        Dim tr As WebTableRowAPI = GetTableRowAPI()
        tr = tr.GetPreviousRow()
        If (tr Is Nothing) Then Return Nothing

        Dim desc As IDescription = UIControls.Description.ConvertApiToUiDescription( _
                       tr.CreateFullDescription())
        Dim previousElement As WebTableRow = DirectCast(BuildElement(desc, ElementTypes.TableRow), WebTableRow)

        previousElement.description.RemoveRange(1, previousElement.description.Count - 1) 'since we are building a new fully described
        'element we must clear out the description or the child element will cause detection problems.
        previousElement.description.Add(desc)
        Return previousElement
    End Function

    'This just does not work.
    'Public Function GetRowHeight() As Integer
    '    ExistsWithException()
    '    Return GetTableRowAPI().GetRowHeight()
    'End Function

    ''' <summary>
    ''' nn for pixels or nn% for percentage length; offset for alignment char;
    ''' horizontal alignment in cells
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks>Returns empty if a caption element.</remarks>
    Public Function GetCharOffset() As String
        ExistsWithException()
        Return GetTableRowAPI().GetCharOffset()
    End Function

    ''' <summary>
    ''' Gets the index of the current row.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks>Starts with row 0.  Returns -1 if row is a
    ''' caption.</remarks>
    Public Function GetRowIndex() As Integer
        ExistsWithException()
        Return GetTableRowAPI().GetRowIndex()
    End Function

    ''' <summary>
    ''' Gets the cell specified by the index passed in.
    ''' </summary>
    ''' <param name="index">The index starting at 0</param>
    ''' <returns>Returns nothing if no cell is found.</returns>
    ''' <remarks>A caption is not defined as a cell.</remarks>
    Public Function GetCell(ByVal index As Integer) As WebTableCell
        ExistsWithException()
        Dim cell As WebTableCellAPI = GetTableRowAPI().GetCell(index)

        If (cell Is Nothing) Then Return Nothing

        Dim desc As IDescription = UIControls.Description.ConvertApiToUiDescription( _
                       cell.CreateFullDescription())
        Dim cellElement As WebTableCell = DirectCast(BuildElement(desc, ElementTypes.TableCell), WebTableCell)

        cellElement.description.RemoveRange(1, cellElement.description.Count - 1) 'since we are building a new fully described
        'element we must clear out the description or the child element will cause detection problems.
        cellElement.description.Add(desc)
        Return cellElement
    End Function

    ''' <summary>
    ''' Gets the cell count.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks>Returns -1 if table row is a caption.</remarks>
    Public Function GetCellCount() As Integer
        ExistsWithException()
        Return GetTableRowAPI().GetCellCount()
    End Function

    ''' <summary>
    ''' Gets the direct parent's table.
    ''' </summary>
    ''' <returns>Returns nothing if no parent table can be found.</returns>
    ''' <remarks></remarks>
    Public Function GetParentTable() As WebTable
        ExistsWithException()
        Dim tr As WebTableRowAPI = GetTableRowAPI()
        If (tr Is Nothing) Then Return Nothing

        Dim desc As IDescription = UIControls.Description.ConvertApiToUiDescription( _
                       tr.CreateFullDescription())
        Dim parentTable As WebTable = DirectCast(BuildElement(desc, ElementTypes.Table), WebTable)

        parentTable.description.RemoveRange(1, parentTable.description.Count - 1) 'since we are building a new fully described
        'element we must clear out the description or the child element will cause detection problems.
        parentTable.description.Add(desc)
        Return parentTable
    End Function

End Class
