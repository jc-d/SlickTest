Friend Class WebFileUploadAPI
    Inherits WebInputAPI

    Public Sub New(ByVal element As Object)
        MyBase.New(element)
        If (IsFileUpload(Me)) Then Return
        Throw New SlickTestAPIException("Element " & Me.ToString() & " is not a FileUpload.")
    End Sub

    Public Shared Function IsFileUpload(ByVal element As WebElementAPI) As Boolean
        If (WebInputAPI.IsInput(element) = False) Then Return False
        If (element.Attribute("type").ToLowerInvariant() = WebTagDefinitions.FileUploadType) Then
            Return True
        End If
        Return False
    End Function
End Class
