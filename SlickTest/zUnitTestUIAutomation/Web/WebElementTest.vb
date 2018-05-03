Imports NUnit.Framework
Imports UIControls



'''<summary>
'''This is a test class for WebElementTest and is intended
'''to contain all WebElementTest Unit Tests
'''</summary>
<TestFixture()> _
Public Class WebElementTest
    Inherits WebTests


#Region "Additional test attributes"

    Public Shared target As WebElement

    'Use TestInitialize to run code before running each test
    <SetUp()> _
    Public Overrides Sub MyTestInitialize()
        MyBase.MyTestInitialize()
        target = WebBrowser.WebElement("WebId:=""OuterTable"";;WebTag=""table""").WebElement("WebTag:=""input""")
    End Sub
#End Region

    '''<summary>
    '''A test for Width
    '''</summary>
    <Test()> _
    Public Sub WidthTest()
        Dim actual As Integer
        actual = target.Width
        Verify.IsTrue(0 < actual)
    End Sub

    '''<summary>
    '''A test for Top
    '''</summary>
    <Test()> _
    Public Sub TopTest()

        Dim actual As Integer
        actual = target.Top
        Verify.IsTrue(0 < actual)
    End Sub

    '''<summary>
    '''A test for Right
    '''</summary>
    <Test()> _
    Public Sub RightTest()

        Dim actual As Integer
        actual = target.Right
        Verify.IsTrue(0 < actual)
    End Sub

    '''<summary>
    '''A test for Left
    '''</summary>
    <Test()> _
    Public Sub LeftTest()

        Dim actual As Integer
        actual = target.Left
        Verify.IsTrue(0 < actual)
    End Sub

    '''<summary>
    '''A test for Height
    '''</summary>
    <Test()> _
    Public Sub HeightTest()

        Dim actual As Integer
        actual = target.Height
        Verify.IsTrue(0 < actual)
    End Sub

    '''<summary>
    '''A test for ClassName
    '''</summary>
    <Test()> _
    Public Sub ClassNameTest()

        Dim actual As String
        actual = target.ClassName()
        Verify.AreEqual("test", actual)
    End Sub

    '''<summary>
    '''A test for Bottom
    '''</summary>
    <Test()> _
    Public Sub BottomTest()

        Dim actual As Integer
        actual = target.Bottom
        Verify.IsTrue(0 < actual)
    End Sub

    '''<summary>
    '''A test for SetText
    '''</summary>
    <Test()> _
    Public Sub SetTextTest()
        Dim Text As String = "test"
        target.SetText(Text)
        Verify.AreEqual(Text, target.GetText())
    End Sub

    '''<summary>
    '''A test for AppendText
    '''</summary>
    <Test()> _
    Public Sub AppendTextTest()
        Dim Text As String = "test"
        Dim expected As String = target.GetText() & Text
        target.AppendText(Text)
        Verify.AreEqual(expected, target.GetText())
    End Sub

    '''<summary>
    '''A test for IsEnabled
    '''</summary>
    <Test()> _
    Public Sub IsEnabledTest()
        Dim expected As Boolean = True
        Dim actual As Boolean
        actual = target.IsEnabled
        Verify.AreEqual(expected, actual)
    End Sub

    '''<summary>
    '''A test for GetTitle
    '''</summary>
    <Test()> _
    Public Sub GetTitleTest()
        Dim expected As String = "Test Title"
        Dim actual As String
        actual = target.GetTitle
        Verify.AreEqual(expected, actual)
    End Sub

    '''<summary>
    '''A test for GetText
    '''</summary>
    <Test()> _
    Public Sub GetTextTest()
        Dim expected As String = "body row 1, cell 2"
        Dim actual As String
        actual = target.GetText
        Verify.AreEqual(expected, actual)
    End Sub

    '''<summary>
    '''A test for GetTagName
    '''</summary>
    <Test()> _
    Public Sub GetTagNameTest()
        Dim expected As String = "Input".ToUpperInvariant()
        Dim actual As String
        actual = target.GetTagName.ToUpperInvariant()
        Verify.AreEqual(expected, actual)
    End Sub

    '''<summary>
    '''A test for GetParentElement
    '''</summary>
    <Test()> _
    Public Sub GetParentElementTest()
        Dim expected As WebElement = Nothing
        Dim actual As WebElement
        actual = target.GetParentElement()
        Verify.AreNotEqual(expected, actual)
    End Sub

    '''<summary>
    '''A test for GetNextElement
    '''</summary>
    <Test()> _
    Public Sub GetNextElementTest()
        Dim expected As WebElement = Nothing
        Dim actual As WebElement
        actual = target.GetNextElement()
        Verify.AreNotEqual(expected, actual)
    End Sub

    '''<summary>
    '''A test for GetPreviousElement
    '''</summary>
    <Test()> _
    Public Sub GetPreviousElementTest()
        Dim expected As WebElement = Nothing
        Dim actual As WebElement
        actual = target.GetPreviousElement()
        Verify.AreNotEqual(expected, actual)
    End Sub

    '''<summary>
    '''A test for GetOuterText
    '''</summary>
    <Test()> _
    Public Sub GetOuterTextTest()
        Dim expected As String = String.Empty
        Dim actual As String
        actual = target.GetOuterText()
        Verify.AreEqual(expected, actual)
    End Sub

    '''<summary>
    '''A test for GetOuterHtml
    '''</summary>
    <Test()> _
    Public Sub GetOuterHtmlTest()
        Dim actual As String = target.GetOuterHtml()
        Dim expected As String = target.GetParentElement().GetInnerHtml()
        Verify.AreEqual(expected, actual)
    End Sub

    '''<summary>
    '''A test for GetIndex
    '''</summary>
    <Test()> _
    Public Sub GetIndexTest()
        Dim expected As Integer = 6
        Dim preBody As WebElement = target.GetParentElement().GetParentElement(). _
        GetParentElement().GetParentElement().GetParentElement()
        Dim body As WebElement = preBody.GetParentElement()
        Dim actual As Integer = body.GetIndex()
        Verify.AreEqual(expected, actual)
    End Sub

    '''<summary>
    '''A test for GetTabIndex
    '''</summary>
    <Test()> _
    Public Sub GetTabIndexTest()
        Dim expected As Integer = 0
        Dim preBody As WebElement = target.GetParentElement().GetParentElement(). _
        GetParentElement().GetParentElement()
        Dim body As WebElement = preBody.GetParentElement()
        Dim actual As Integer = body.GetTabIndex()
        Verify.AreEqual(expected, actual)
    End Sub

    '''<summary>
    '''A test for SetFocus
    '''</summary>
    <Test()> _
    Public Sub SetFocusTest()
        target.SetFocus()
        WebBrowser.TypeKeys("test")
        System.Threading.Thread.Sleep(200)
        Verify.Contains("test", target.GetText())
    End Sub

    '''<summary>
    '''A test for GetInnerHtml
    '''</summary>
    <Test()> _
    Public Sub GetInnerHtmlTest()
        Dim expected As String = String.Empty
        Dim actual As String
        actual = target.GetInnerHtml()
        Verify.AreEqual(expected, actual)
    End Sub

    '''<summary>
    '''A test for GetId
    '''</summary>
    <Test()> _
    Public Sub GetIdTest()
        Dim expected As String = String.Empty
        Dim actual As String
        actual = target.GetId()
        Verify.AreEqual(expected, actual)
    End Sub

    '''<summary>
    '''A test for GetCurrentLanguage
    '''</summary>
    <Test()> _
    Public Sub GetCurrentLanguageTest()
        Dim expected As String = "en"
        Dim actual As String
        actual = target.GetCurrentLanguage()
        Verify.AreEqual(expected, actual)
    End Sub

    '''<summary>
    '''A test for ScrollIntoView
    '''</summary>
    <Test()> _
    Public Sub ScrollIntoViewTest()
        target.ScrollIntoView()
        Verify.AreEqual("This has no exceptions", "This has no exceptions")
    End Sub

    '''<summary>
    '''A test for TagUrn
    '''</summary>
    <Test()> _
    Public Sub TagUrnTest()
        Dim expected As String = Nothing
        Dim actual As String
        actual = target.TagUrn
        Verify.AreEqual(expected, actual)
    End Sub

    '''<summary>
    '''A test for ScopeName
    '''</summary>
    <Test()> _
    Public Sub ScopeNameTest()
        Dim expected As String = "HTML"
        Dim actual As String
        actual = target.ScopeName
        Verify.AreEqual(expected, actual)
    End Sub

    '''<summary>
    '''A test for GetAccessKey
    '''</summary>
    <Test()> _
    Public Sub GetAccessKeyTest()
        Dim expected As String = Nothing
        Dim actual As String
        actual = target.GetAccessKey()
        Verify.AreEqual(expected, actual)
    End Sub

    '''<summary>
    '''A test for IsTabStop
    '''</summary>
    <Test()> _
    Public Sub IsTabStopTest()
        Dim expected As Boolean = True
        Dim actual As Boolean
        actual = target.IsTabStop()
        Verify.AreEqual(expected, actual)
    End Sub

    '''<summary>
    '''A test for Click
    '''</summary>
    <Test()> _
    Public Sub ClickTest()
        Dim expected As Boolean = True
        Dim actual As Boolean
        actual = target.Click
        Verify.AreEqual(expected, actual)
    End Sub

    '''<summary>
    '''A test for Blur
    '''</summary>
    <Test()> _
    Public Sub BlurTest()
        target.Blur()
        Verify.IsNotEmpty("Blur event run.")
    End Sub

    '''<summary>
    '''A test for TypeText
    '''</summary>
    <Test()> _
    Public Sub TypeTextTest()
        Dim expected As String = target.GetText() + "verify"
        target.TypeText("verify")
        Dim actual As String = target.GetText()

        Verify.AreEqual(expected, actual)
    End Sub


    '''<summary>
    '''A test for GetValue
    '''</summary>
    <Test()> _
    Public Sub GetValueTest()
        Dim expected As String = target.GetText()
        Dim actual As String
        actual = target.GetValue()
        Verify.AreEqual(expected, actual)
    End Sub


    '''<summary>
    '''A test for GetAttribute
    '''</summary>
    <Test()> _
    Public Sub GetAttributeTest()
        Dim expected As String = "test"
        Dim actual As String
        actual = target.GetAttribute("customattribute")
        Verify.AreEqual(expected, actual)
    End Sub

    '''<summary>
    '''A test for TypeText with a non-text field.
    '''</summary>
    <Test()> _
    Public Sub TypeTextWithNonInputTest()
        Dim expected As String = WebBrowser.WebElement("WebId:=""OuterTable"";;WebTag=""table""").GetText()
        WebBrowser.WebElement("WebId:=""OuterTable"";;WebTag=""table""").TypeText("verify")
        Dim actual As String = WebBrowser.WebElement("WebId:=""OuterTable"";;WebTag=""table""").GetText()

        Verify.AreEqual(expected, actual)
        Verify.DoesNotContain("verify", target.GetValue())
    End Sub

    '''<summary>
    '''A test for GetChildDescriptions.
    '''</summary>
    <Test()> _
    Public Sub GetChildDescriptionsTest()
        Dim actual As UIControls.Description()
        actual = target.GetParentElement().GetChildDescriptions()
        Verify.AreNotEqual(Nothing, actual)
        Verify.IsTrue(actual.Count > 0, "Verify count is greater than 0.  count: " & actual.Count)
        For Each d As UIControls.Description In actual
            Console.WriteLine(d.WebTag & " " & d.Index & " - " & d.WebText) 'verify descriptions are legit.
        Next
    End Sub

    '''<summary>
    '''A test for DumpWindowData.
    '''</summary>
    <Test()> _
    Public Sub DumpWindowDataTest()
        Dim actual As String
        actual = target.DumpWindowData()
        Verify.AreNotEqual(Nothing, actual)
        Verify.IsTrue(actual.Length > 100)
    End Sub


    ''''<summary>
    ''''A test for Click
    ''''</summary>
    '<Test()> _
    'Public Sub TmpTest()
    '    Dim start As System.DateTime = System.DateTime.Now()
    '    i.IEWebBrowser("name:=""IEFrame"";;value:=""Google - Windows Internet Explorer""").WebElement("name:=""q""").SetText("test")
    '    Dim msg As String = i.IEWebBrowser("name:=""IEFrame"";;value:=""Google - Windows Internet Explorer""").WebElement("webtype:=""inputz"";;name:=""q""").GetOuterHtml().ToString()

    '    '.WebElement("webid:=""csi""").GetOuterHtml().ToString()
    '    '.WebElement("webtype:=""input"";;name:=""q""").GetOuterHtml().ToString()
    '    Dim endtime As System.DateTime = System.DateTime.Now()
    '    Dim timeComp As String = (start - endtime).ToString()
    '    MsgBox(msg & vbNewLine & timeComp)

    'End Sub




    ''''<summary>
    ''''A test for Click
    ''''</summary>
    '<Test()> _
    'Public Sub ClickTest()

    '    Dim X As Integer = 0 ' TODO: Initialize to an appropriate value
    '    Dim Y As Integer = 0 ' TODO: Initialize to an appropriate value
    '    Dim expected As Boolean = False ' TODO: Initialize to an appropriate value
    '    Dim actual As Boolean
    '    actual = target.Click(X, Y)
    '    Verify.AreEqual(expected, actual)
    '    Verify.Inconclusive("Verify the correctness of this test method.")
    'End Sub
End Class
