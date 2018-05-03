Imports APIControls

Public Class WebImage
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
        HtmlTags = APIControls.WebTagDefinitions.ImageTags
    End Sub
#End Region

    Private Function GetImageAPI() As WebGenericImageAPI
        Return New WebGenericImageAPI(Me.CurrentElement)
    End Function

    ''' <summary>
    ''' Align sets the alignment of the image 
    ''' relative to the text around it.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetAlign() As String
        ExistsWithException()
        Return GetImageAPI().Align()
    End Function

    '''' <summary>
    '''' 
    '''' </summary>
    '''' <returns></returns>
    '''' <remarks></remarks>
    'Public Function GetImageHeight() As Integer
    '    ExistsWithException()
    '    Return GetImageAPI().ImgHeight()
    'End Function

    '''' <summary>
    '''' 
    '''' </summary>
    '''' <returns></returns>
    '''' <remarks></remarks>
    'Public Function GetImageWidth() As Integer
    '    ExistsWithException()
    '    Return GetImageAPI().ImgWidth()
    'End Function

    Public Function GetMimeType() As String
        ExistsWithException()
        Return GetImageAPI().MimeType()
    End Function

    ''' <summary>
    ''' The location of the image.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetSrc() As String
        ExistsWithException()
        Return GetImageAPI().Src()
    End Function

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetUseMap() As String
        ExistsWithException()
        Return GetImageAPI().UseMap()
    End Function

    '''' <summary>
    '''' 
    '''' </summary>
    '''' <returns></returns>
    '''' <remarks></remarks>
    'Public Function GetVrml() As String
    '    ExistsWithException()
    '    Return GetImageAPI().Vrml()
    'End Function

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetVSpace() As Integer
        ExistsWithException()
        Return GetImageAPI().VSpace()
    End Function

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetHSpace() As Integer
        ExistsWithException()
        Return GetImageAPI().HSpace()
    End Function

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks>Only works with img tag, otherwise it returns null</remarks>
    Public Function GetFileCreatedDate() As String
        ExistsWithException()
        Return GetImageAPI().FileCreatedDate()
    End Function

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks>Only works with img tag, otherwise it returns null</remarks>
    Public Function GetFileModifiedDate() As String
        ExistsWithException()
        Return GetImageAPI().FileModifiedDate()
    End Function

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks>Only works with img tag, otherwise it returns null</remarks>
    Public Function GetFileSize() As String
        ExistsWithException()
        Return GetImageAPI().FileSize()
    End Function

    ''' <summary>
    ''' The alt text when mousing over an image.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks>Sometimes called a tool tip</remarks>
    Public Function GetAlt() As String
        ExistsWithException()
        Return GetImageAPI().Alt()
    End Function
End Class
