Friend Class WebInputButtonAPI
    Inherits WebInputAPI

    Public Sub New(ByVal element As Object)
        MyBase.New(element)
        If (IsButton(Me)) Then Return
        Throw New SlickTestAPIException("Element " & Me.ToString() & " is not a Button.")
    End Sub

    Public Shared Function IsButton(ByVal element As WebElementAPI) As Boolean
        If (WebInputAPI.IsInput(element) = False) Then Return False
        Dim type As String = element.Attribute("type").ToLowerInvariant()
        If (type = WebTagDefinitions.ButtonType OrElse _
            type = WebTagDefinitions.ResetButtonType OrElse _
            type = WebTagDefinitions.SubmitButtonType) Then
            Return True
        End If
        Return False
    End Function
End Class
