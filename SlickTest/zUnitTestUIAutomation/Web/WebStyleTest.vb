Imports UIControls
Imports NUnit.Framework

'''<summary>
'''This is a test class for WebStyleTest and is intended
'''to contain all WebStyleTest Unit Tests
'''</summary>
<TestFixture()> _
Public Class WebStyleTest
    Inherits WebTests

    <SetUp()> _
    Public Overrides Sub MyTestInitialize()
        WebTests.RunKill = False
    End Sub

    Private target As WebStyle

    <NUnit.Framework.TestFixtureTearDown()> _
    Public Sub ClassTeardown()
        WebTests.RunKill = True
        WebTests.KillWebBrowsers(False)
    End Sub

    <TestFixtureSetUp()> _
    Public Overrides Sub MyClassInitialize()
        MyBase.MyClassInitialize()
        MyBase.MyTestInitialize()
        target = WebBrowser.WebTableCell("WebId:=""WithId""").GetStyleInfo
    End Sub


    '''<summary>
    '''A test for GetVisibility
    '''</summary>
    <Test()> _
    Public Sub GetVisibilityTest()
        Dim expected As String = "visible"
        Dim actual As String
        actual = target.GetVisibility
        Verify.AreEqual(expected, actual)
    End Sub

    '''<summary>
    '''A test for GetTextAlign
    '''</summary>
    <Test()> _
    Public Sub GetTextAlignTest()
        Dim expected As String = "center"
        Dim actual As String
        actual = target.GetTextAlign
        Verify.AreEqual(expected, actual)
    End Sub

    '''<summary>
    '''A test for GetPosition
    '''</summary>
    <Test()> _
    Public Sub GetPositionTest()
        Dim expected As String = "absolute"
        Dim actual As String
        actual = target.GetPosition
        Verify.AreEqual(expected, actual)
    End Sub

    '''<summary>
    '''A test for GetPadding
    '''</summary>
    <Test()> _
    Public Sub GetPaddingTest()
        Dim expected As String = "inherit"
        Dim actual As String
        actual = target.GetPadding
        Verify.AreEqual(expected, actual)
    End Sub

    '''<summary>
    '''A test for GetOverflow
    '''</summary>
    <Test()> _
    Public Sub GetOverflowTest()
        Dim expected As String = "auto"
        Dim actual As String
        actual = target.GetOverflow
        Verify.AreEqual(expected, actual)
    End Sub

    '''<summary>
    '''A test for GetMargin
    '''</summary>
    <Test()> _
    Public Sub GetMarginTest()
        Dim expected As String = "inherit"
        Dim actual As String
        actual = target.GetMargin
        Verify.AreEqual(expected, actual)
    End Sub

    '''<summary>
    '''A test for GetFontWeight
    '''</summary>
    <Test()> _
    Public Sub GetFontWeightTest()
        Dim expected As String = "bold"
        Dim actual As String
        actual = target.GetFontWeight
        Verify.AreEqual(expected, actual)
    End Sub

    '''<summary>
    '''A test for GetFontStyle
    '''</summary>
    <Test()> _
    Public Sub GetFontStyleTest()
        Dim expected As String = "normal"
        Dim actual As String
        actual = target.GetFontStyle
        Verify.AreEqual(expected, actual)
    End Sub

    '''<summary>
    '''A test for GetFontSize
    '''</summary>
    <Test()> _
    Public Sub GetFontSizeTest()
        Dim expected As String = "medium"
        Dim actual As String
        actual = target.GetFontSize
        Verify.AreEqual(expected, actual)
    End Sub

    '''<summary>
    '''A test for GetFont
    '''</summary>
    <Test()> _
    Public Sub GetFontTest()
        Dim actual As String
        Dim expected As String = String.Empty
        actual = target.GetFont
        Verify.AreEqual(expected, actual)
    End Sub

    '''<summary>
    '''A test for GetDisplay
    '''</summary>
    <Test()> _
    Public Sub GetDisplayTest()
        Dim expected As String = "table"
        Dim actual As String
        actual = target.GetDisplay
        Verify.AreEqual(expected, actual)
    End Sub

    '''<summary>
    '''A test for GetCssText
    '''</summary>
    <Test()> _
    Public Sub GetCssTextTest()
        Dim actual As String
        actual = target.GetCssText
        Verify.IsNotEmpty(actual)
    End Sub

    '''<summary>
    '''A test for GetBorderStyle
    '''</summary>
    <Test()> _
    Public Sub GetBorderStyleTest()
        Dim expected As String = "inherit"
        Dim actual As String
        actual = target.GetBorderStyle
        Verify.AreEqual(expected, actual)
    End Sub

    '''<summary>
    '''A test for GetBorderColor
    '''</summary>
    <Test()> _
    Public Sub GetBorderColorTest()
        Dim expected As String = "inherit"
        Dim actual As String
        actual = target.GetBorderColor
        Verify.AreEqual(expected, actual)
    End Sub

    '''<summary>
    '''A test for GetBorder
    '''</summary>
    <Test()> _
    Public Sub GetBorderTest()
        Dim expected As String = "inherit"
        Dim actual As String
        actual = target.GetBorder
        Verify.AreEqual(expected, actual)
    End Sub

    '''<summary>
    '''A test for GetBackgroundPosition
    '''</summary>
    <Test()> _
    Public Sub GetBackgroundPositionTest()
        Dim expected As String = "center 50%"
        Dim actual As String
        actual = target.GetBackgroundPosition
        Verify.AreEqual(expected, actual)
    End Sub

    '''<summary>
    '''A test for GetBackgroundImage
    '''</summary>
    <Test()> _
    Public Sub GetBackgroundImageTest()
        Dim expected As String = "url(background.png)"
        Dim actual As String
        actual = target.GetBackgroundImage
        Verify.AreEqual(expected, actual)
    End Sub

    '''<summary>
    '''A test for GetBackgroundAttachment
    '''</summary>
    <Test()> _
    Public Sub GetBackgroundAttachmentTest()
        Dim expected As String = "fixed"
        Dim actual As String
        actual = target.GetBackgroundAttachment
        Verify.AreEqual(expected, actual)
    End Sub

    '''<summary>
    '''A test for GetBackground
    '''</summary>
    <Test()> _
    Public Sub GetBackgroundTest()
        Dim expected As String = "url(background.png"
        Dim actual As String
        actual = target.GetBackground()
        Verify.Contains(expected, actual)
    End Sub

    '''<summary>
    '''A test for GetZIndex
    '''</summary>
    <Test()> _
    Public Sub GetZIndexTest()
        Dim expected As String = "auto"
        Dim actual As String
        actual = target.GetZIndex()
        Verify.AreEqual(expected, actual)
    End Sub

    '''<summary>
    '''A test for GetVerticalAlign
    '''</summary>
    <Test()> _
    Public Sub GetVerticalAlignTest()
        Dim expected As String = "bottom"
        Dim actual As String
        actual = target.GetVerticalAlign()
        Verify.AreEqual(expected, actual)
    End Sub
End Class
