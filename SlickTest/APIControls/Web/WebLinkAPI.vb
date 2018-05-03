Friend Class WebLinkAPI
    Inherits WebElementAPI

    Public Sub New(ByVal element As Object)
        MyBase.New(element)
        If (IsLink(Me)) Then Return
        Throw New SlickTestAPIException("Element " & Me.ToString() & " is not a Link.")
    End Sub

    Public Shared Function IsLink(ByVal element As WebElementAPI) As Boolean
        If (element Is Nothing) Then Return False
        For Each tableTag As String In WebTagDefinitions.LinkTags
            If (tableTag = element.TagName.ToLowerInvariant()) Then
                Return True
            End If
        Next
        Return False
    End Function

    'Protected Friend ReadOnly Property HTMLLink() As mshtml.IHTMLLinkElement
    '    Get
    '        Return DirectCast(HTMLElement, mshtml.IHTMLLinkElement)
    '    End Get
    'End Property

    'Protected Friend ReadOnly Property HTMLLink2() As mshtml.IHTMLLinkElement2
    '    Get
    '        Return DirectCast(HTMLElement, mshtml.IHTMLLinkElement2)
    '    End Get
    'End Property

    'Protected Friend ReadOnly Property HTMLLink3() As mshtml.IHTMLLinkElement3
    '    Get
    '        Return DirectCast(HTMLElement, mshtml.IHTMLLinkElement3)
    '    End Get
    'End Property

    Protected Friend ReadOnly Property HTMLAnchor() As mshtml.IHTMLAnchorElement
        Get
            Return DirectCast(HTMLElement, mshtml.IHTMLAnchorElement)
        End Get
    End Property

    Protected Friend ReadOnly Property HTMLAnchor2() As mshtml.IHTMLAnchorElement2
        Get
            Return DirectCast(HTMLElement, mshtml.IHTMLAnchorElement2)
        End Get
    End Property


    'Public Function IsDisabled() As Boolean
    '    Return HTMLAnchor.disabled
    'End Function

    Public Function HRef() As String
        Return HTMLAnchor.href
    End Function

    Public Function Host() As String
        Return HTMLAnchor.host
    End Function

    ''' <summary>
    ''' Sets or retrieves the relationship between 
    ''' the object and the destination of the link.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function Rel() As String
        Return HTMLAnchor.rel
    End Function

    ''' <summary>
    ''' Sets or retrieves the relationship between the 
    ''' object and the destination of the link.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function Rev() As String
        Return HTMLAnchor.rev
    End Function

    ''' <summary>
    ''' MIME type
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function Type() As String
        Return HTMLAnchor2.type
    End Function

    Public Function Target() As String
        Return HTMLAnchor.target
    End Function

    Public Function HRefLang() As String
        Return HTMLAnchor2.hreflang
    End Function

    Public Function CharSet() As String
        Return HTMLAnchor2.charset
    End Function

    Public Function MimeType() As String
        Return HTMLAnchor.mimeType
    End Function

    Public Function NameProp() As String
        Return HTMLAnchor.nameProp
    End Function

    Public Function PathName() As String
        Return HTMLAnchor.pathname
    End Function

    Public Function Protocol() As String
        Return HTMLAnchor.protocol
    End Function

    Public Function ProtocolLong() As String
        Return HTMLAnchor.protocolLong
    End Function
End Class
