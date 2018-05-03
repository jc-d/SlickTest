Friend Class WebButtonAPI
    Inherits WebElementAPI

    Public Sub New(ByVal element As Object)
        MyBase.New(element)
        If (IsButton(Me)) Then Return
        Throw New SlickTestAPIException("Element " & Me.ToString() & " is not a Button.")
    End Sub

    Public Shared Function IsButton(ByVal element As WebElementAPI) As Boolean
        If (element Is Nothing) Then Return False
        For Each buttonTag As String In WebTagDefinitions.ButtonTags
            If (buttonTag = element.TagName.ToLowerInvariant()) Then
                If (WebInputAPI.IsInput(element)) Then
                    Dim at As String = GetElementType(element)
                    If (at = WebTagDefinitions.ButtonType OrElse _
                       at = WebTagDefinitions.ResetButtonType OrElse _
                       at = WebTagDefinitions.SubmitButtonType) Then
                        Return True
                    End If
                Else
                    Return True
                End If
            End If
        Next
        Return False
    End Function

    Private Shared Function GetElementType(ByVal element As WebElementAPI) As String
        Return element.Attribute("type")
    End Function

    Public Function ButtonType() As String
        If (WebInputAPI.IsInput(Me)) Then
            Return GetElementType(Me)
        End If
        Return HTMLButton.type
    End Function

    Protected Friend ReadOnly Property HTMLButton() As mshtml.IHTMLButtonElement
        Get
            Return DirectCast(HTMLElement, mshtml.IHTMLButtonElement)
        End Get
    End Property
End Class
