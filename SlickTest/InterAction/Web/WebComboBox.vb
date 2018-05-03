Imports APIControls

''' <summary>
''' The WebComboBox must be the select tag.
''' </summary>
''' <remarks></remarks>
Public Class WebComboBox
    Inherits WebElement

#Region "Constructor"
    ''' <summary>
    ''' Creates a WebElement object.  
    ''' Do not directly create this object, instead use UIControls.[Object].WebElement([desc]).[Method]
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
    ''' Do not directly create this object, instead use UIControls.[Object].WebElement([desc]).[Method]
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
        HtmlTags = APIControls.WebTagDefinitions.DropdownComboBoxTags
    End Sub
#End Region

    Private Function GetComboBoxAPI() As WebDropDownComboBoxAPI
        Return New WebDropDownComboBoxAPI(Me.CurrentElement)
    End Function

    ''' <summary>
    ''' Checks to see if the item is disabled.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function IsDisabled() As Boolean
        ExistsWithException()
        Return GetComboBoxAPI().IsDisabled()
    End Function

    ''' <summary>
    ''' Get the number of items in the combo box.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetItemCount() As Integer
        ExistsWithException()
        Return GetComboBoxAPI().Length()
    End Function

    ''' <summary>
    ''' Allows the user to select multiple items at once.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function AllowMultiItemSelect() As Boolean
        ExistsWithException()
        Return GetComboBoxAPI().AllowMultiSelect()
    End Function

    ''' <summary>
    ''' The selected index is the first item selected.
    ''' </summary>
    ''' <returns>Returns the selected index, if any item is selected.
    ''' Returns -1 if no items are selected.</returns>
    ''' <remarks>Selected index starting with 0.</remarks>
    Public Function GetSelectedIndex() As Integer
        ExistsWithException()
        Return GetComboBoxAPI().SelectedIndex()
    End Function

    ''' <summary>
    ''' The number of items the user will see.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function Size() As Integer
        ExistsWithException()
        Return GetComboBoxAPI().Size()
    End Function

    ''' <summary>
    ''' The type of Combobox.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function Type() As String
        ExistsWithException()
        Return GetComboBoxAPI().Type()
    End Function

    ''' <summary>
    ''' The Value of the selected item.
    ''' </summary>
    ''' <returns>The value of the selected item.</returns>
    ''' <remarks></remarks>
    Public Function SelectedValue() As String
        ExistsWithException()
        Return GetComboBoxAPI().SelectValue()
    End Function

    ''' <summary>
    ''' Get a option as a web element by the name or ID given.
    ''' </summary>
    ''' <param name="IdOrName">This method first searches for an object with a matching id attribute. 
    ''' If a match is not found, then the method searches for an object with a matching name attribute.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetItem(ByVal IdOrName As String) As WebElement
        ExistsWithException()
        Dim item As WebElementAPI = GetComboBoxAPI().GetItem(IdOrName)
        Dim desc As IDescription = UIControls.Description.ConvertApiToUiDescription( _
               item.CreateReasonableDescription())
        Dim foundElement As WebElement = BuildWebElement(desc)

        Return foundElement
    End Function

    ''' <summary>
    ''' Get a option as a web element by index.
    ''' </summary>
    ''' <param name="Index">The index starting at 0.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetItem(ByVal Index As Integer) As WebElement
        ExistsWithException()
        Dim item As WebElementAPI = GetComboBoxAPI().Item(Index)
        Dim desc As IDescription = UIControls.Description.ConvertApiToUiDescription( _
               item.CreateReasonableDescription())
        Dim foundElement As WebElement = BuildWebElement(desc)

        Return foundElement
    End Function

End Class
