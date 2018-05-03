Friend Class WebSpanAPI
    Inherits WebElementAPI

    Public Sub New(ByVal element As Object)
        MyBase.New(element)
        If (IsSpan(Me)) Then Return
        Throw New SlickTestAPIException("Element " & Me.ToString() & " is not a Span.")
    End Sub

    Public Shared Function IsSpan(ByVal element As WebElementAPI) As Boolean
        If (element Is Nothing) Then Return False
        For Each tableTag As String In WebTagDefinitions.SpanTags
            If (tableTag = element.TagName.ToLowerInvariant()) Then
                Return True
            End If
        Next
        Return False
    End Function

    'Protected Friend ReadOnly Property HTMLSpan() As mshtml.IHTMLSpanElement
    '    Get
    '        Return DirectCast(HTMLElement, mshtml.IHTMLSpanElement)
    '    End Get
    'End Property

End Class
