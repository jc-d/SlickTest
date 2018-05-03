Friend Class WebDivAPI
    Inherits WebElementAPI

    Public Sub New(ByVal element As Object)
        MyBase.New(element)
        If (IsDiv(Me)) Then Return
        Throw New SlickTestAPIException("Element " & Me.ToString() & " is not a Div.")
    End Sub

    Public Shared Function IsDiv(ByVal element As WebElementAPI) As Boolean
        If (element Is Nothing) Then Return False
        For Each tableTag As String In WebTagDefinitions.DivTags
            If (tableTag = element.TagName.ToLowerInvariant()) Then
                Return True
            End If
        Next
        Return False
    End Function

    Protected Friend ReadOnly Property HTMLDiv() As mshtml.IHTMLDivElement
        Get
            Return DirectCast(HTMLElement, mshtml.IHTMLDivElement)
        End Get
    End Property

    Public Function IsNoWrap() As Boolean
        Return HTMLDiv.noWrap()
    End Function

    Public Function Align() As String
        Return HTMLDiv.align()
    End Function

End Class
