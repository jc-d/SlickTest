Friend Class WebInputImageAPI
    Inherits WebInputAPI


    Public Sub New(ByVal element As Object)
        MyBase.New(element)
        If (WebInputImageAPI.IsInputImage(Me)) Then Return
        Throw New SlickTestAPIException("Element " & Me.ToString() & " is not a Image.")
    End Sub

    Public Shared Function IsInputImage(ByVal element As WebElementAPI) As Boolean
        If (WebInputAPI.IsInput(element) = False) Then Return False
        If (element.Attribute("type").ToLowerInvariant() = WebTagDefinitions.ImageType) Then
            Return True
        End If
        Return False
    End Function

    Public Function FileSize() As String
        Return Nothing
    End Function

    Public Function MimeType() As String
        Return Nothing
    End Function

    Public Function FileModifiedDate() As String
        Return Nothing
    End Function

    Public Function FileCreatedDate() As String
        Return Nothing
    End Function
End Class
