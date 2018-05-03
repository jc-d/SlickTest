Imports APIControls

Public Class WebLink
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
        HtmlTags = APIControls.WebTagDefinitions.ListTags
    End Sub
#End Region

    Private Function GetWebLinkAPI() As WebLinkAPI
        Return New WebLinkAPI(Me.CurrentElement)
    End Function

    Public Function GetRev() As String
        ExistsWithException()
        Return GetWebLinkAPI().Rev()
    End Function

    Public Function GetRel() As String
        ExistsWithException()
        Return GetWebLinkAPI().Rel()
    End Function

    Public Function GetCharSet() As String
        ExistsWithException()
        Return GetWebLinkAPI().CharSet()
    End Function

    Public Function GetLinkType() As String
        ExistsWithException()
        Return GetWebLinkAPI().Type()
    End Function

    Public Function GetTarget() As String
        ExistsWithException()
        Return GetWebLinkAPI().Target()
    End Function

    Public Function GetHRefLang() As String
        ExistsWithException()
        Return GetWebLinkAPI().HRefLang()
    End Function

    Public Function GetHRef() As String
        ExistsWithException()
        Return GetWebLinkAPI().HRef()
    End Function

    Public Function GetMimeType() As String
        ExistsWithException()
        Return GetWebLinkAPI().MimeType()
    End Function

    Public Function GetNameProp() As String
        ExistsWithException()
        Return GetWebLinkAPI().NameProp()
    End Function

    Public Function GetPathName() As String
        ExistsWithException()
        Return GetWebLinkAPI().PathName()
    End Function

    Public Function GetProtocol() As String
        ExistsWithException()
        Return GetWebLinkAPI().Protocol()
    End Function

    Public Function GetProtocolLong() As String
        ExistsWithException()
        Return GetWebLinkAPI().ProtocolLong()
    End Function
End Class
