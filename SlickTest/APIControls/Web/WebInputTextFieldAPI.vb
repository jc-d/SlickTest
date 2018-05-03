Friend Class WebInputTextFieldAPI
    Inherits WebInputAPI

    Public Sub New(ByVal element As Object)
        MyBase.New(element)
        If (IsTextField(Me)) Then Return
        Throw New SlickTestAPIException("Element " & Me.ToString() & " is not a FileUpload.")
    End Sub

    Public Shared Function IsTextField(ByVal element As WebElementAPI) As Boolean
        If (WebInputAPI.IsInput(element) = False) Then Return False
        Dim type As String = element.Attribute("type").ToLowerInvariant()
        If (type = WebTagDefinitions.TextType _
            OrElse type = WebTagDefinitions.PasswordType) Then
            Return True
        End If
        Return False
    End Function


    Public Shared Function IsPasswordTextField(ByVal element As WebElementAPI) As Boolean
        If (WebInputAPI.IsInput(element) = False) Then Return False
        If (element.Attribute("type").ToLowerInvariant() = WebTagDefinitions.PasswordType) Then
            Return True
        End If
        Return False
    End Function
End Class
