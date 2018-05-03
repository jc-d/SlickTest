Imports APIControls

''' <summary>
''' The WebCheckbox is a input with the type checkbox.
''' </summary>
''' <remarks></remarks>
Public Class WebCheckbox
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
        HtmlTags = APIControls.WebTagDefinitions.InputTags
    End Sub
#End Region

    Private Function GetCheckboxAPI() As WebCheckboxAPI
        Return New WebCheckboxAPI(Me.CurrentElement)
    End Function

    ''' <summary>
    ''' Sets the Checkbox to either checked or unchecked by
    ''' clicking on the checkbox is the state does not match
    ''' the state provided.
    ''' </summary>
    ''' <param name="state">The state that you wish to set the checkbox to.
    ''' If the state is set to true, the checkbox will be checked, and false will
    ''' uncheck the checkbox.</param>
    ''' <returns>Returns true if the checkbox state has been clicked on
    ''' and false if the checkbox has not been clicked on.</returns>
    ''' <remarks>Readonly checkboxes will not be clicked on.</remarks>
    Public Function SetChecked(ByVal state As Boolean) As Boolean
        ExistsWithException()
        Return GetCheckboxAPI().SetChecked(state)
    End Function

    ''' <summary>
    ''' Gets the state of the checkbox.
    ''' </summary>
    ''' <returns>Returns true if the checkbox is checked and false if the
    ''' checkbox is unchecked.</returns>
    ''' <remarks></remarks>
    Public Function GetChecked() As Boolean
        ExistsWithException()
        Return GetCheckboxAPI().IsChecked()
    End Function

    ''' <summary>
    ''' Gets the state of the checkbox to see if it is setup as a 
    ''' indeterminate checkbox.
    ''' </summary>
    ''' <returns>Returns true if the checkbox is indeterminate.</returns>
    ''' <remarks>When the indeterminate state is used for a checkbox, it behaves as a 
    ''' three-state control. A checkbox cannot be changed to indeterminate 
    ''' state through the user interface, this state can only be set 
    ''' from scripts. This state can be used to force the user to check or 
    ''' uncheck a checkbox.</remarks>
    Public Function IsIndeterminate() As Boolean
        ExistsWithException()
        Return GetCheckboxAPI().IsIndeterminate()
    End Function

    ''' <summary>
    ''' Determines if the checkbox is read only.
    ''' </summary>
    ''' <returns>Returns true if the checkbox is set to read only.</returns>
    ''' <remarks></remarks>
    Public Function IsReadOnly() As Boolean
        ExistsWithException()
        Return GetCheckboxAPI().IsReadOnly()
    End Function

    'Image
    'Public Function GetAlign() As String
    '    ExistsWithException()
    '    Return GetCheckboxAPI().Align()
    'End Function

    'Image
    'Public Function GetAlt() As String
    '    ExistsWithException()
    '    Return GetCheckboxAPI().Alt()
    'End Function

    'Images
    'Public Function GetDynSrc() As String
    '    ExistsWithException()
    '    Return GetCheckboxAPI().DynSrc()
    'End Function

    'Images
    'Public Function GetHSpace() As Integer
    '    ExistsWithException()
    '    Return GetCheckboxAPI().HSpace()
    'End Function

    'Images
    'Public Function GetVSpace() As Integer
    '    ExistsWithException()
    '    Return GetCheckboxAPI().VSpace()
    'End Function

    'For a Input="Image"
    '''' <summary>
    '''' Gets the lower resolution image to display. 
    '''' </summary>
    '''' <returns></returns>
    '''' <remarks></remarks>
    'Public Function GetLowResolutionSrc() As String
    '    ExistsWithException()
    '    Return GetCheckboxAPI().LowResolutionSrc()
    'End Function

    'For a checkbox?
    'Public Function GetMaxLength() As Integer
    '    ExistsWithException()
    '    Return GetCheckboxAPI().GetMaxLength()
    'End Function

    'not checkbox
    'Public Function GetSize() As Integer
    '    ExistsWithException()
    '    Return GetCheckboxAPI().Size()
    'End Function

    'not checkbox
    'Public Function GetSrc() As String
    '    ExistsWithException()
    '    Return GetCheckboxAPI().Src()
    'End Function

    'Image
    'Public Function GetStart() As String
    '    ExistsWithException()
    '    Return GetCheckboxAPI().Start()
    'End Function

    'Just for images
    'Public Function GetUseMap() As String
    '    ExistsWithException()
    '    Return GetCheckboxAPI().UseMap()
    'End Function

    'Images
    'Public Function GetVrml() As String
    '    ExistsWithException()
    '    Return GetCheckboxAPI().Vrml()
    'End Function

    'Public Function GetBorder() As Object
    '    ExistsWithException()
    '    Return GetCheckboxAPI().Border()
    'End Function

End Class
