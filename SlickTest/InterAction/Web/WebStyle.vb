''' <summary>
''' Style information set to an element.
''' </summary>
''' <remarks>Excludes external CSS settings.</remarks>
Public Class WebStyle
    Private WebElement As APIControls.WebElementAPI

    Friend Sub New(ByVal element As APIControls.WebElementAPI)
        WebElement = element
    End Sub

    Public Function GetBackground() As String
        Return WebElement.StyleBackground()
    End Function

    Public Function GetBackgroundImage() As String
        Return WebElement.StyleBackgroundImage()
    End Function

    Public Function GetBackgroundAttachment() As String
        Return WebElement.StyleBackgroundAttachment()
    End Function

    Public Function GetBackgroundPosition() As String
        Return WebElement.StyleBackgroundPosition()
    End Function

    Public Function GetBorder() As String
        Return WebElement.StyleBorder()
    End Function

    Public Function GetBorderColor() As String
        Return WebElement.StyleBorderColor()
    End Function

    Public Function GetBorderStyle() As String
        Return WebElement.StyleBorderStyle()
    End Function

    Public Function GetCssText() As String
        Return WebElement.StyleCssText()
    End Function

    Public Function GetDisplay() As String
        Return WebElement.StyleDisplay()
    End Function

    Public Function GetFont() As String
        Return WebElement.StyleFont()
    End Function

    Public Function GetFontSize() As String
        Return WebElement.StyleFontSize()
    End Function

    Public Function GetFontStyle() As String
        Return WebElement.StyleFontStyle()
    End Function

    Public Function GetFontWeight() As String
        Return WebElement.StyleFontWeight()
    End Function

    Public Function GetMargin() As String
        Return WebElement.StyleMargin()
    End Function

    Public Function GetOverflow() As String
        Return WebElement.StyleOverflow()
    End Function

    Public Function GetPadding() As String
        Return WebElement.StylePadding()
    End Function

    Public Function GetPosition() As String
        Return WebElement.StylePosition()
    End Function

    Public Function GetTextAlign() As String
        Return WebElement.StyleTextAlign()
    End Function

    Public Function GetVisibility() As String
        Return WebElement.StyleVisibility()
    End Function

    Public Function GetZIndex() As String
        Return WebElement.StyleZIndex()
    End Function

    Public Function GetVerticalAlign() As String
        Return WebElement.VerticalAlign()
    End Function
End Class
