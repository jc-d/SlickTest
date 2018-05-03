Friend Class WebRadioButtonAPI
    Inherits WebInputAPI

    Public Sub New(ByVal element As Object)
        MyBase.New(element)
        If (IsRadioButton(Me)) Then Return
        Throw New SlickTestAPIException("Element " & Me.ToString() & " is not a RadioButton.")
    End Sub

    Public Shared Function IsRadioButton(ByVal element As WebElementAPI) As Boolean
        If (WebInputAPI.IsInput(element) = False) Then Return False
        If (element.Attribute("type").ToLowerInvariant() = WebTagDefinitions.RadioButtonType) Then
            Return True
        End If
        Return False
    End Function
End Class
