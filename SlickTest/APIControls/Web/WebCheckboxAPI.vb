Friend Class WebCheckboxAPI
    Inherits WebInputAPI

    Public Sub New(ByVal element As Object)
        MyBase.New(element)
        If (IsCheckBox(Me)) Then Return
        Throw New SlickTestAPIException("Element " & Me.ToString() & " is not a Checkbox.")
    End Sub

    Public Shared Function IsCheckBox(ByVal element As WebElementAPI) As Boolean
        If (WebInputAPI.IsInput(element) = False) Then Return False
        If (element.Attribute("type").ToLowerInvariant() = WebTagDefinitions.CheckboxType) Then
            Return True
        End If
        Return False
    End Function
End Class
