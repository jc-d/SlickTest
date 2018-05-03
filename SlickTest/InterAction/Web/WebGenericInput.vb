Imports APIControls

Public Class WebGenericInput
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

    Private Function GetWebInputAPI() As WebInputAPI
        Return New WebInputAPI(Me.CurrentElement)
    End Function

    Public Sub SetChecked(ByVal state As Boolean)
        ExistsWithException()
        GetWebInputAPI().SetChecked(state)
    End Sub

    Public Function GetChecked() As Boolean
        ExistsWithException()
        Return GetWebInputAPI().IsChecked()
    End Function

    Public Function IsIndeterminate() As Boolean
        ExistsWithException()
        Return GetWebInputAPI().IsIndeterminate()
    End Function

    Public Function GetAlt() As String
        ExistsWithException()
        Return GetWebInputAPI().Alt()
    End Function

    Public Function GetDynSrc() As String
        ExistsWithException()
        Return GetWebInputAPI().DynSrc()
    End Function

    Public Function GetHSpace() As Integer
        ExistsWithException()
        Return GetWebInputAPI().HSpace()
    End Function

    Public Function GetVSpace() As Integer
        ExistsWithException()
        Return GetWebInputAPI().VSpace()
    End Function

    Public Function IsReadOnly() As Boolean
        ExistsWithException()
        Return GetWebInputAPI().IsReadOnly()
    End Function

    Public Function GetInputType() As String
        ExistsWithException()
        Return GetWebInputAPI().GetInputType()
    End Function
    ''' <summary>
    ''' Gets the lower resolution image to display. 
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetLowResolutionSrc() As String
        ExistsWithException()
        Return GetWebInputAPI().LowResolutionSrc()
    End Function

    Public Function GetMaxLength() As Integer
        ExistsWithException()
        Return GetWebInputAPI().MaxLength()
    End Function

    Public Function GetSize() As Integer
        ExistsWithException()
        Return GetWebInputAPI().Size()
    End Function

    Public Function GetSrc() As String
        ExistsWithException()
        Return GetWebInputAPI().Src()
    End Function

    Public Function GetStart() As String
        ExistsWithException()
        Return GetWebInputAPI().Start()
    End Function

    Public Function GetUseMap() As String
        ExistsWithException()
        Return GetWebInputAPI().UseMap()
    End Function

    Public Function GetVrml() As String
        ExistsWithException()
        Return GetWebInputAPI().Vrml()
    End Function

    Public Function GetAlign() As String
        ExistsWithException()
        Return GetWebInputAPI().Align()
    End Function

    'Public Function GetBorder() As Object
    '    ExistsWithException()
    '    Return GetWebInputAPI().Border()
    'End Function

    Public Shared Function IsCheckBox(ByVal element As WebElement) As Boolean
        Return element.Exists() AndAlso WebCheckboxAPI.IsCheckBox(element.CurrentElement)
    End Function

    Public Shared Function IsRadioButton(ByVal element As WebElement) As Boolean
        Return element.Exists() AndAlso WebRadioButtonAPI.IsRadioButton(element.CurrentElement)
    End Function

    Public Shared Function IsButton(ByVal element As WebElement) As Boolean
        Return element.Exists() AndAlso WebInputButtonAPI.IsButton(element.CurrentElement)
    End Function

    Public Shared Function IsImage(ByVal element As WebElement) As Boolean
        Return element.Exists() AndAlso WebInputImageAPI.IsInputImage(element.CurrentElement)
    End Function

    Public Shared Function IsFileUpload(ByVal element As WebElement) As Boolean
        Return element.Exists() AndAlso WebFileUploadAPI.IsFileUpload(element.CurrentElement)
    End Function

    Public Shared Function IsTextField(ByVal element As WebElement) As Boolean
        Return element.Exists() AndAlso WebInputTextFieldAPI.IsTextField(element.CurrentElement)
    End Function

    Public Shared Function IsPasswordTextField(ByVal element As WebElement) As Boolean
        Return element.Exists() AndAlso WebInputTextFieldAPI.IsPasswordTextField(element.CurrentElement)
    End Function
End Class
