Imports APIControls
''' <summary>
''' A WebTableCell is just a specialized WebElement and it supports 
''' everything a WebElement does.
''' </summary>
''' <remarks></remarks>
Public Class WebTableCell
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
        HtmlTags = APIControls.WebTagDefinitions.TableCellTags
    End Sub
#End Region
    Private Function GetTableCellAPI() As WebTableCellAPI
        Return New WebTableCellAPI(Me.CurrentElement)
    End Function

    ''' <summary>
    ''' Gets the next cell if a cell exists.
    ''' </summary>
    ''' <returns>Returns Nothing if a next cell
    ''' does not exist.</returns>
    ''' <remarks></remarks>
    Public Function GetNextCell() As WebTableCell
        ExistsWithException()
        Dim tc As WebTableCellAPI = GetTableCellAPI()
        tc = tc.GetNextCellInRow()
        If (tc Is Nothing) Then Return Nothing

        Dim desc As IDescription = UIControls.Description.ConvertApiToUiDescription( _
                       tc.CreateFullDescription())
        Dim nextElement As WebTableCell = DirectCast(BuildElement(desc, ElementTypes.TableCell), WebTableCell)

        nextElement.description.RemoveRange(1, nextElement.description.Count - 1) 'since we are building a new fully described
        'element we must clear out the description or the child element will cause detection problems.
        nextElement.description.Add(desc)
        Return nextElement
    End Function

    ''' <summary>
    ''' Gets the previous cell if a cell exists.
    ''' </summary>
    ''' <returns>Returns Nothing if a previous cell
    ''' does not exist.</returns>
    ''' <remarks></remarks>
    Public Function GetPreviousCell() As WebTableCell
        ExistsWithException()
        Dim tc As WebTableCellAPI = GetTableCellAPI()
        tc = tc.GetPreviousCellInRow()
        If (tc Is Nothing) Then Return Nothing

        Dim desc As IDescription = UIControls.Description.ConvertApiToUiDescription( _
                       tc.CreateFullDescription())
        Dim previousElement As WebTableCell = DirectCast(BuildElement(desc, ElementTypes.TableCell), WebTableCell)

        previousElement.description.RemoveRange(1, previousElement.description.Count - 1) 'since we are building a new fully described
        'element we must clear out the description or the child element will cause detection problems.
        previousElement.description.Add(desc)
        Return previousElement
    End Function

    ''' <summary>
    ''' Gets the index of the current cell.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks>Starts with cell 0.</remarks>
    Public Function GetCellIndex() As Integer
        ExistsWithException()
        Return GetTableCellAPI().GetCellIndex()
    End Function

    ''' <summary>
    ''' Get the number of rows that this particular cell spans.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetRowSpan() As Integer
        ExistsWithException()
        Return GetTableCellAPI().GetRowSpan()
    End Function

    ''' <summary>
    ''' Get the number of columns that this particular cell spans.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetColumnSpan() As Integer
        ExistsWithException()
        Return GetTableCellAPI().GetColumnSpan()
    End Function

    'Public Function GetWidth() As Integer
    '    ExistsWithException()
    '    Return GetTableCellAPI().GetWidth()
    'End Function

    'Public Function GetHeaders() As String
    '    ExistsWithException()
    '    Return GetTableCellAPI().GetHeaders()
    'End Function

    ''' <summary>
    ''' nn for pixels or nn% for percentage length; offset for alignment char;
    ''' horizontal alignment in cells
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetCharOffset() As String
        ExistsWithException()
        Return GetTableCellAPI().GetCharOffset()
    End Function

    Public Function GetAlign() As String
        ExistsWithException()
        Return GetTableCellAPI().GetAlign()
    End Function

    ''' <summary>
    ''' Gets the parent row element.
    ''' </summary>
    ''' <returns>Returns Nothing if no table row can be found.</returns>
    ''' <remarks></remarks>
    Public Function GetParentRow() As WebTableRow
        ExistsWithException()
        Dim tc As WebTableCellAPI = GetTableCellAPI()
        Dim tr As WebTableRowAPI = tc.GetParentTableRow()
        If (tr Is Nothing) Then Return Nothing

        Dim desc As IDescription = UIControls.Description.ConvertApiToUiDescription( _
                       tr.CreateFullDescription())
        Dim parentElement As WebTableRow = DirectCast(BuildElement(desc, ElementTypes.TableRow), WebTableRow)

        parentElement.description.RemoveRange(1, parentElement.description.Count - 1) 'since we are building a new fully described
        'element we must clear out the description or the child element will cause detection problems.
        parentElement.description.Add(desc)
        Return parentElement
    End Function

    ''' <summary>
    ''' Gets the parent Table element.
    ''' </summary>
    ''' <returns>Returns Nothing if no table can be found.</returns>
    ''' <remarks></remarks>
    Public Function GetParentTable() As WebTable
        ExistsWithException()
        Dim tc As WebTableCellAPI = GetTableCellAPI()
        Dim tbl As WebTableAPI = tc.GetParentTable()
        If (tbl Is Nothing) Then Return Nothing

        Dim desc As IDescription = UIControls.Description.ConvertApiToUiDescription( _
                       tbl.CreateFullDescription())
        Dim parentElement As WebTable = DirectCast(BuildElement(desc, ElementTypes.Table), WebTable)

        parentElement.description.RemoveRange(1, parentElement.description.Count - 1) 'since we are building a new fully described
        'element we must clear out the description or the child element will cause detection problems.
        parentElement.description.Add(desc)
        Return parentElement
    End Function
End Class
