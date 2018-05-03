Friend Class WebListItemAPI
    Inherits WebElementAPI

    Public Sub New(ByVal element As Object)
        MyBase.New(element)
        If (IsListItem(Me)) Then Return
        Throw New SlickTestAPIException("Element " & Me.ToString() & " is not a List Element.")
    End Sub

    Public Shared Function IsListItem(ByVal element As WebElementAPI) As Boolean
        If (element Is Nothing) Then Return False
        For Each tableTag As String In WebTagDefinitions.ListItemTags
            If (tableTag = element.TagName.ToLowerInvariant()) Then
                Return True
            End If
        Next
        Return False
    End Function

    Protected Friend ReadOnly Property HTMLLIElement() As mshtml.IHTMLLIElement
        Get
            Return DirectCast(HTMLElement, mshtml.IHTMLLIElement)
        End Get
    End Property

    Public Function Type() As String
        Return HTMLLIElement.type
    End Function

    Public Function LIValue() As Integer
        Return HTMLLIElement.value
    End Function
End Class
