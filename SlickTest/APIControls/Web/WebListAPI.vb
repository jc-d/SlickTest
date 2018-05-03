Friend Class WebListAPI
    Inherits WebElementAPI

    Public Sub New(ByVal element As Object)
        MyBase.New(element)
        If (IsList(Me)) Then Return
        Throw New SlickTestAPIException("Element " & Me.ToString() & " is not a List.")
    End Sub

    Public Shared Function IsList(ByVal element As WebElementAPI) As Boolean
        If (element Is Nothing) Then Return False
        For Each tableTag As String In WebTagDefinitions.ListTags
            If (tableTag = element.TagName.ToLowerInvariant()) Then
                Return True
            End If
        Next
        Return False
    End Function

    'Protected Friend ReadOnly Property HTMLList() As mshtml.IHTMLListElement
    '    Get
    '        Return DirectCast(HTMLElement, mshtml.IHTMLListElement)
    '    End Get
    'End Property

    Protected Friend ReadOnly Property HTMLList2() As mshtml.IHTMLListElement2
        Get
            Return DirectCast(HTMLElement, mshtml.IHTMLListElement2)
        End Get
    End Property

    ''' <summary>
    ''' Sets or retrieves a Boolean value indicating 
    ''' whether the list should be compacted by 
    ''' removing extra space between list objects.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function Compact() As Boolean
        Return HTMLList2.compact
    End Function

    ''' <summary>
    ''' Returns a list item from a given list.
    ''' </summary>
    ''' <param name="index">The list item you want to get.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetListItem(ByVal index As Integer) As WebListItemAPI
        Dim li As WebListItemAPI = Nothing
        Dim currentElement As WebElementAPI = New WebElementAPI(Me.DOMNode.firstChild)
        For i As Integer = 0 To index - 1
            If (WebListItemAPI.IsListItem(currentElement) = False) Then Return Nothing
            currentElement = currentElement.NextElement()
        Next
        If (WebListItemAPI.IsListItem(currentElement) = False) Then Return Nothing

        Return New WebListItemAPI(currentElement)
    End Function

    ''' <summary>
    ''' Returns a list item from a given list.
    ''' </summary>
    ''' <param name="text">The exact text (case sensative) for a list item.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetListItem(ByVal text As String) As WebListItemAPI
        Dim li As WebListItemAPI = Nothing
        Dim currentElement As WebElementAPI = New WebElementAPI(Me.DOMNode.firstChild)
        Do
            If (currentElement Is Nothing) Then Return Nothing
            If (currentElement.Text = text AndAlso WebListItemAPI.IsListItem(currentElement)) Then Return New WebListItemAPI(currentElement)
            currentElement = currentElement.NextElement()
        Loop While (WebListItemAPI.IsListItem(currentElement))
        Return Nothing
    End Function
End Class
