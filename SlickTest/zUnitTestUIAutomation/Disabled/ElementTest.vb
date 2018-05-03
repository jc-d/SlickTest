'Imports mshtml

'Imports Microsoft.VisualStudio.TestTools.UnitTesting

'Imports APIControls



''''<summary>
''''This is a test class for ElementTest and is intended
''''to contain all ElementTest Unit Tests
''''</summary>
'<TestClass()> _
'Public Class ElementTest


'    Private testContextInstance As TestContext

'    '''<summary>
'    '''Gets or sets the test context which provides
'    '''information about and functionality for the current test run.
'    '''</summary>
'    Public Property TestContext() As TestContext
'        Get
'            Return testContextInstance
'        End Get
'        Set(ByVal value As TestContext)
'            testContextInstance = Value
'        End Set
'    End Property

'#Region "Additional test attributes"
'    '
'    'You can use the following additional attributes as you write your tests:
'    '
'    'Use ClassInitialize to run code before running the first test in the class
'    '<ClassInitialize()>  _
'    'Public Shared Sub MyClassInitialize(ByVal testContext As TestContext)
'    'End Sub
'    '
'    'Use ClassCleanup to run code after all tests in a class have run
'    '<ClassCleanup()>  _
'    'Public Shared Sub MyClassCleanup()
'    'End Sub
'    '
'    'Use TestInitialize to run code before running each test
'    '<TestInitialize()>  _
'    'Public Sub MyTestInitialize()
'    'End Sub
'    '
'    'Use TestCleanup to run code after each test has run
'    '<TestCleanup()>  _
'    'Public Sub MyTestCleanup()
'    'End Sub
'    '
'#End Region


'    '''<summary>
'    '''A test for Width
'    '''</summary>
'    <TestMethod()> _
'    Public Sub WidthTest()
'        Dim element As Object = Nothing ' TODO: Initialize to an appropriate value
'        Dim target As IElement = New Element(element) ' TODO: Initialize to an appropriate value
'        Dim actual As Integer
'        actual = target.Width
'        Assert.Inconclusive("Verify the correctness of this test method.")
'    End Sub

'    '''<summary>
'    '''A test for Top
'    '''</summary>
'    <TestMethod()> _
'    Public Sub TopTest()
'        Dim element As Object = Nothing ' TODO: Initialize to an appropriate value
'        Dim target As IElement = New Element(element) ' TODO: Initialize to an appropriate value
'        Dim actual As Integer
'        actual = target.Top
'        Assert.Inconclusive("Verify the correctness of this test method.")
'    End Sub

'    '''<summary>
'    '''A test for Title
'    '''</summary>
'    <TestMethod()> _
'    Public Sub TitleTest()
'        Dim element As Object = Nothing ' TODO: Initialize to an appropriate value
'        Dim target As IElement = New Element(element) ' TODO: Initialize to an appropriate value
'        Dim actual As String
'        actual = target.Title
'        Assert.Inconclusive("Verify the correctness of this test method.")
'    End Sub

'    '''<summary>
'    '''A test for Text
'    '''</summary>
'    <TestMethod()> _
'    Public Sub TextTest()
'        Dim element As Object = Nothing ' TODO: Initialize to an appropriate value
'        Dim target As IElement = New Element(element) ' TODO: Initialize to an appropriate value
'        Dim actual As String
'        actual = target.Text
'        Assert.Inconclusive("Verify the correctness of this test method.")
'    End Sub

'    '''<summary>
'    '''A test for TagName
'    '''</summary>
'    <TestMethod()> _
'    Public Sub TagNameTest()
'        Dim element As Object = Nothing ' TODO: Initialize to an appropriate value
'        Dim target As IElement = New Element(element) ' TODO: Initialize to an appropriate value
'        Dim actual As String
'        actual = target.TagName
'        Assert.Inconclusive("Verify the correctness of this test method.")
'    End Sub

'    '''<summary>
'    '''A test for Right
'    '''</summary>
'    <TestMethod()> _
'    Public Sub RightTest()
'        Dim element As Object = Nothing ' TODO: Initialize to an appropriate value
'        Dim target As IElement = New Element(element) ' TODO: Initialize to an appropriate value
'        Dim actual As Integer
'        actual = target.Right
'        Assert.Inconclusive("Verify the correctness of this test method.")
'    End Sub

'    '''<summary>
'    '''A test for OuterText
'    '''</summary>
'    <TestMethod()> _
'    Public Sub OuterTextTest()
'        Dim element As Object = Nothing ' TODO: Initialize to an appropriate value
'        Dim target As IElement = New Element(element) ' TODO: Initialize to an appropriate value
'        Dim actual As String
'        actual = target.OuterText
'        Assert.Inconclusive("Verify the correctness of this test method.")
'    End Sub

'    '''<summary>
'    '''A test for Left
'    '''</summary>
'    <TestMethod()> _
'    Public Sub LeftTest()
'        Dim element As Object = Nothing ' TODO: Initialize to an appropriate value
'        Dim target As IElement = New Element(element) ' TODO: Initialize to an appropriate value
'        Dim actual As Integer
'        actual = target.Left
'        Assert.Inconclusive("Verify the correctness of this test method.")
'    End Sub

'    '''<summary>
'    '''A test for Id
'    '''</summary>
'    <TestMethod()> _
'    Public Sub IdTest()
'        Dim element As Object = Nothing ' TODO: Initialize to an appropriate value
'        Dim target As IElement = New Element(element) ' TODO: Initialize to an appropriate value
'        Dim actual As String
'        actual = target.Id
'        Assert.Inconclusive("Verify the correctness of this test method.")
'    End Sub

'    '''<summary>
'    '''A test for htmlElement3
'    '''</summary>
'    <TestMethod()> _
'    Public Sub htmlElement3Test()
'        Dim element As Object = Nothing ' TODO: Initialize to an appropriate value
'        Dim target As Element = New Element(element) ' TODO: Initialize to an appropriate value
'        Dim actual As IHTMLElement3
'        actual = target.htmlElement3
'        Assert.Inconclusive("Verify the correctness of this test method.")
'    End Sub

'    '''<summary>
'    '''A test for htmlElement2
'    '''</summary>
'    <TestMethod()> _
'    Public Sub htmlElement2Test()
'        Dim element As Object = Nothing ' TODO: Initialize to an appropriate value
'        Dim target As Element = New Element(element) ' TODO: Initialize to an appropriate value
'        Dim actual As IHTMLElement2
'        actual = target.htmlElement2
'        Assert.Inconclusive("Verify the correctness of this test method.")
'    End Sub

'    '''<summary>
'    '''A test for Height
'    '''</summary>
'    <TestMethod()> _
'    Public Sub HeightTest()
'        Dim element As Object = Nothing ' TODO: Initialize to an appropriate value
'        Dim target As IElement = New Element(element) ' TODO: Initialize to an appropriate value
'        Dim actual As Integer
'        actual = target.Height
'        Assert.Inconclusive("Verify the correctness of this test method.")
'    End Sub

'    '''<summary>
'    '''A test for Enabled
'    '''</summary>
'    <TestMethod()> _
'    Public Sub EnabledTest()
'        Dim element As Object = Nothing ' TODO: Initialize to an appropriate value
'        Dim target As IElement = New Element(element) ' TODO: Initialize to an appropriate value
'        Dim actual As Boolean
'        actual = target.Enabled
'        Assert.Inconclusive("Verify the correctness of this test method.")
'    End Sub

'    '''<summary>
'    '''A test for ClassName
'    '''</summary>
'    <TestMethod()> _
'    Public Sub ClassNameTest()
'        Dim element As Object = Nothing ' TODO: Initialize to an appropriate value
'        Dim target As Element = New Element(element) ' TODO: Initialize to an appropriate value
'        Dim actual As String
'        actual = target.ClassName
'        Assert.Inconclusive("Verify the correctness of this test method.")
'    End Sub

'    '''<summary>
'    '''A test for Bottom
'    '''</summary>
'    <TestMethod()> _
'    Public Sub BottomTest()
'        Dim element As Object = Nothing ' TODO: Initialize to an appropriate value
'        Dim target As IElement = New Element(element) ' TODO: Initialize to an appropriate value
'        Dim actual As Integer
'        actual = target.Bottom
'        Assert.Inconclusive("Verify the correctness of this test method.")
'    End Sub

'    '''<summary>
'    '''A test for Attribute
'    '''</summary>
'    <TestMethod()> _
'    Public Sub AttributeTest()
'        Dim element As Object = Nothing ' TODO: Initialize to an appropriate value
'        Dim target As IElement = New Element(element) ' TODO: Initialize to an appropriate value
'        Dim AttributeName As String = String.Empty ' TODO: Initialize to an appropriate value
'        Dim actual As String
'        actual = target.Attribute(AttributeName)
'        Assert.Inconclusive("Verify the correctness of this test method.")
'    End Sub

'    '''<summary>
'    '''A test for GetParent
'    '''</summary>
'    <TestMethod()> _
'    Public Sub GetParentTest()
'        Dim element As Object = Nothing ' TODO: Initialize to an appropriate value
'        Dim target As Element = New Element(element) ' TODO: Initialize to an appropriate value
'        Dim expected As Element = Nothing ' TODO: Initialize to an appropriate value
'        Dim actual As Element
'        actual = target.GetParent
'        Assert.AreEqual(expected, actual)
'        Assert.Inconclusive("Verify the correctness of this test method.")
'    End Sub

'    '''<summary>
'    '''A test for GetChildren
'    '''</summary>
'    <TestMethod()> _
'    Public Sub GetChildrenTest()
'        Dim element As Object = Nothing ' TODO: Initialize to an appropriate value
'        Dim target As Element = New Element(element) ' TODO: Initialize to an appropriate value
'        Dim expected() As Element = Nothing ' TODO: Initialize to an appropriate value
'        Dim actual() As Element
'        actual = target.GetChildren
'        Assert.AreEqual(expected, actual)
'        Assert.Inconclusive("Verify the correctness of this test method.")
'    End Sub

'    '''<summary>
'    '''A test for GetAllChildrenVElement
'    '''</summary>
'    <TestMethod()> _
'    Public Sub GetAllChildrenVElementTest()
'        Dim element As Object = Nothing ' TODO: Initialize to an appropriate value
'        Dim target As Element = New Element(element) ' TODO: Initialize to an appropriate value
'        Dim expected() As VElement = Nothing ' TODO: Initialize to an appropriate value
'        Dim actual() As VElement
'        actual = target.GetAllChildrenVElement
'        Assert.AreEqual(expected, actual)
'        Assert.Inconclusive("Verify the correctness of this test method.")
'    End Sub

'    '''<summary>
'    '''A test for GetAllChildren
'    '''</summary>
'    <TestMethod()> _
'    Public Sub GetAllChildrenTest()
'        Dim element As Object = Nothing ' TODO: Initialize to an appropriate value
'        Dim target As Element = New Element(element) ' TODO: Initialize to an appropriate value
'        Dim expected() As Element = Nothing ' TODO: Initialize to an appropriate value
'        Dim actual() As Element
'        actual = target.GetAllChildren
'        Assert.AreEqual(expected, actual)
'        Assert.Inconclusive("Verify the correctness of this test method.")
'    End Sub

'    '''<summary>
'    '''A test for Click
'    '''</summary>
'    <TestMethod()> _
'    Public Sub ClickTest()
'        Dim element As Object = Nothing ' TODO: Initialize to an appropriate value
'        Dim target As Element = New Element(element) ' TODO: Initialize to an appropriate value
'        target.Click()
'        Assert.Inconclusive("A method that does not return a value cannot be verified.")
'    End Sub

'    '''<summary>
'    '''A test for ActualY
'    '''</summary>
'    <TestMethod()> _
'    Public Sub ActualYTest()
'        Dim element As Object = Nothing ' TODO: Initialize to an appropriate value
'        Dim target As Element = New Element(element) ' TODO: Initialize to an appropriate value
'        Dim expected As Integer = 0 ' TODO: Initialize to an appropriate value
'        Dim actual As Integer
'        actual = target.ActualY
'        Assert.AreEqual(expected, actual)
'        Assert.Inconclusive("Verify the correctness of this test method.")
'    End Sub

'    '''<summary>
'    '''A test for ActualX
'    '''</summary>
'    <TestMethod()> _
'    Public Sub ActualXTest()
'        Dim element As Object = Nothing ' TODO: Initialize to an appropriate value
'        Dim target As Element = New Element(element) ' TODO: Initialize to an appropriate value
'        Dim expected As Integer = 0 ' TODO: Initialize to an appropriate value
'        Dim actual As Integer
'        actual = target.ActualX
'        Assert.AreEqual(expected, actual)
'        Assert.Inconclusive("Verify the correctness of this test method.")
'    End Sub

'    '''<summary>
'    '''A test for Element Constructor
'    '''</summary>
'    <TestMethod()> _
'    Public Sub ElementConstructorTest()
'        Dim element As Object = Nothing ' TODO: Initialize to an appropriate value
'        Dim target As Element = New Element(element)
'        Assert.Inconclusive("TODO: Implement code to verify target")
'    End Sub
'End Class
