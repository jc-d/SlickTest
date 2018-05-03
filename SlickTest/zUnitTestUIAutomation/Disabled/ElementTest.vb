'Imports mshtml

'Imports NUnit.Framework

'Imports APIControls



''''<summary>
''''This is a test class for ElementTest and is intended
''''to contain all ElementTest Unit Tests
''''</summary>
'<TestFixture()>  _
'Public Class ElementTest


'

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
'    'Public Shared Sub MyClassInitialize()
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
'    <Test()> _
'    Public Sub WidthTest()
'        Dim element As Object = Nothing ' TODO: Initialize to an appropriate value
'        Dim target As IElement = New Element(element) ' TODO: Initialize to an appropriate value
'        Dim actual As Integer
'        actual = target.Width
'        Verify.Inconclusive("Verify the correctness of this test method.")
'    End Sub

'    '''<summary>
'    '''A test for Top
'    '''</summary>
'    <Test()> _
'    Public Sub TopTest()
'        Dim element As Object = Nothing ' TODO: Initialize to an appropriate value
'        Dim target As IElement = New Element(element) ' TODO: Initialize to an appropriate value
'        Dim actual As Integer
'        actual = target.Top
'        Verify.Inconclusive("Verify the correctness of this test method.")
'    End Sub

'    '''<summary>
'    '''A test for Title
'    '''</summary>
'    <Test()> _
'    Public Sub TitleTest()
'        Dim element As Object = Nothing ' TODO: Initialize to an appropriate value
'        Dim target As IElement = New Element(element) ' TODO: Initialize to an appropriate value
'        Dim actual As String
'        actual = target.Title
'        Verify.Inconclusive("Verify the correctness of this test method.")
'    End Sub

'    '''<summary>
'    '''A test for Text
'    '''</summary>
'    <Test()> _
'    Public Sub TextTest()
'        Dim element As Object = Nothing ' TODO: Initialize to an appropriate value
'        Dim target As IElement = New Element(element) ' TODO: Initialize to an appropriate value
'        Dim actual As String
'        actual = target.Text
'        Verify.Inconclusive("Verify the correctness of this test method.")
'    End Sub

'    '''<summary>
'    '''A test for TagName
'    '''</summary>
'    <Test()> _
'    Public Sub TagNameTest()
'        Dim element As Object = Nothing ' TODO: Initialize to an appropriate value
'        Dim target As IElement = New Element(element) ' TODO: Initialize to an appropriate value
'        Dim actual As String
'        actual = target.TagName
'        Verify.Inconclusive("Verify the correctness of this test method.")
'    End Sub

'    '''<summary>
'    '''A test for Right
'    '''</summary>
'    <Test()> _
'    Public Sub RightTest()
'        Dim element As Object = Nothing ' TODO: Initialize to an appropriate value
'        Dim target As IElement = New Element(element) ' TODO: Initialize to an appropriate value
'        Dim actual As Integer
'        actual = target.Right
'        Verify.Inconclusive("Verify the correctness of this test method.")
'    End Sub

'    '''<summary>
'    '''A test for OuterText
'    '''</summary>
'    <Test()> _
'    Public Sub OuterTextTest()
'        Dim element As Object = Nothing ' TODO: Initialize to an appropriate value
'        Dim target As IElement = New Element(element) ' TODO: Initialize to an appropriate value
'        Dim actual As String
'        actual = target.OuterText
'        Verify.Inconclusive("Verify the correctness of this test method.")
'    End Sub

'    '''<summary>
'    '''A test for Left
'    '''</summary>
'    <Test()> _
'    Public Sub LeftTest()
'        Dim element As Object = Nothing ' TODO: Initialize to an appropriate value
'        Dim target As IElement = New Element(element) ' TODO: Initialize to an appropriate value
'        Dim actual As Integer
'        actual = target.Left
'        Verify.Inconclusive("Verify the correctness of this test method.")
'    End Sub

'    '''<summary>
'    '''A test for Id
'    '''</summary>
'    <Test()> _
'    Public Sub IdTest()
'        Dim element As Object = Nothing ' TODO: Initialize to an appropriate value
'        Dim target As IElement = New Element(element) ' TODO: Initialize to an appropriate value
'        Dim actual As String
'        actual = target.Id
'        Verify.Inconclusive("Verify the correctness of this test method.")
'    End Sub

'    '''<summary>
'    '''A test for htmlElement3
'    '''</summary>
'    <Test()> _
'    Public Sub htmlElement3Test()
'        Dim element As Object = Nothing ' TODO: Initialize to an appropriate value
'        Dim target As Element = New Element(element) ' TODO: Initialize to an appropriate value
'        Dim actual As IHTMLElement3
'        actual = target.htmlElement3
'        Verify.Inconclusive("Verify the correctness of this test method.")
'    End Sub

'    '''<summary>
'    '''A test for htmlElement2
'    '''</summary>
'    <Test()> _
'    Public Sub htmlElement2Test()
'        Dim element As Object = Nothing ' TODO: Initialize to an appropriate value
'        Dim target As Element = New Element(element) ' TODO: Initialize to an appropriate value
'        Dim actual As IHTMLElement2
'        actual = target.htmlElement2
'        Verify.Inconclusive("Verify the correctness of this test method.")
'    End Sub

'    '''<summary>
'    '''A test for Height
'    '''</summary>
'    <Test()> _
'    Public Sub HeightTest()
'        Dim element As Object = Nothing ' TODO: Initialize to an appropriate value
'        Dim target As IElement = New Element(element) ' TODO: Initialize to an appropriate value
'        Dim actual As Integer
'        actual = target.Height
'        Verify.Inconclusive("Verify the correctness of this test method.")
'    End Sub

'    '''<summary>
'    '''A test for Enabled
'    '''</summary>
'    <Test()> _
'    Public Sub EnabledTest()
'        Dim element As Object = Nothing ' TODO: Initialize to an appropriate value
'        Dim target As IElement = New Element(element) ' TODO: Initialize to an appropriate value
'        Dim actual As Boolean
'        actual = target.Enabled
'        Verify.Inconclusive("Verify the correctness of this test method.")
'    End Sub

'    '''<summary>
'    '''A test for ClassName
'    '''</summary>
'    <Test()> _
'    Public Sub ClassNameTest()
'        Dim element As Object = Nothing ' TODO: Initialize to an appropriate value
'        Dim target As Element = New Element(element) ' TODO: Initialize to an appropriate value
'        Dim actual As String
'        actual = target.ClassName
'        Verify.Inconclusive("Verify the correctness of this test method.")
'    End Sub

'    '''<summary>
'    '''A test for Bottom
'    '''</summary>
'    <Test()> _
'    Public Sub BottomTest()
'        Dim element As Object = Nothing ' TODO: Initialize to an appropriate value
'        Dim target As IElement = New Element(element) ' TODO: Initialize to an appropriate value
'        Dim actual As Integer
'        actual = target.Bottom
'        Verify.Inconclusive("Verify the correctness of this test method.")
'    End Sub

'    '''<summary>
'    '''A test for Attribute
'    '''</summary>
'    <Test()> _
'    Public Sub AttributeTest()
'        Dim element As Object = Nothing ' TODO: Initialize to an appropriate value
'        Dim target As IElement = New Element(element) ' TODO: Initialize to an appropriate value
'        Dim AttributeName As String = String.Empty ' TODO: Initialize to an appropriate value
'        Dim actual As String
'        actual = target.Attribute(AttributeName)
'        Verify.Inconclusive("Verify the correctness of this test method.")
'    End Sub

'    '''<summary>
'    '''A test for GetParent
'    '''</summary>
'    <Test()> _
'    Public Sub GetParentTest()
'        Dim element As Object = Nothing ' TODO: Initialize to an appropriate value
'        Dim target As Element = New Element(element) ' TODO: Initialize to an appropriate value
'        Dim expected As Element = Nothing ' TODO: Initialize to an appropriate value
'        Dim actual As Element
'        actual = target.GetParent
'        Verify.AreEqual(expected, actual)
'        Verify.Inconclusive("Verify the correctness of this test method.")
'    End Sub

'    '''<summary>
'    '''A test for GetChildren
'    '''</summary>
'    <Test()> _
'    Public Sub GetChildrenTest()
'        Dim element As Object = Nothing ' TODO: Initialize to an appropriate value
'        Dim target As Element = New Element(element) ' TODO: Initialize to an appropriate value
'        Dim expected() As Element = Nothing ' TODO: Initialize to an appropriate value
'        Dim actual() As Element
'        actual = target.GetChildren
'        Verify.AreEqual(expected, actual)
'        Verify.Inconclusive("Verify the correctness of this test method.")
'    End Sub

'    '''<summary>
'    '''A test for GetAllChildrenVElement
'    '''</summary>
'    <Test()> _
'    Public Sub GetAllChildrenVElementTest()
'        Dim element As Object = Nothing ' TODO: Initialize to an appropriate value
'        Dim target As Element = New Element(element) ' TODO: Initialize to an appropriate value
'        Dim expected() As VElement = Nothing ' TODO: Initialize to an appropriate value
'        Dim actual() As VElement
'        actual = target.GetAllChildrenVElement
'        Verify.AreEqual(expected, actual)
'        Verify.Inconclusive("Verify the correctness of this test method.")
'    End Sub

'    '''<summary>
'    '''A test for GetAllChildren
'    '''</summary>
'    <Test()> _
'    Public Sub GetAllChildrenTest()
'        Dim element As Object = Nothing ' TODO: Initialize to an appropriate value
'        Dim target As Element = New Element(element) ' TODO: Initialize to an appropriate value
'        Dim expected() As Element = Nothing ' TODO: Initialize to an appropriate value
'        Dim actual() As Element
'        actual = target.GetAllChildren
'        Verify.AreEqual(expected, actual)
'        Verify.Inconclusive("Verify the correctness of this test method.")
'    End Sub

'    '''<summary>
'    '''A test for Click
'    '''</summary>
'    <Test()> _
'    Public Sub ClickTest()
'        Dim element As Object = Nothing ' TODO: Initialize to an appropriate value
'        Dim target As Element = New Element(element) ' TODO: Initialize to an appropriate value
'        target.Click()
'        Verify.Inconclusive("A method that does not return a value cannot be verified.")
'    End Sub

'    '''<summary>
'    '''A test for ActualY
'    '''</summary>
'    <Test()> _
'    Public Sub ActualYTest()
'        Dim element As Object = Nothing ' TODO: Initialize to an appropriate value
'        Dim target As Element = New Element(element) ' TODO: Initialize to an appropriate value
'        Dim expected As Integer = 0 ' TODO: Initialize to an appropriate value
'        Dim actual As Integer
'        actual = target.ActualY
'        Verify.AreEqual(expected, actual)
'        Verify.Inconclusive("Verify the correctness of this test method.")
'    End Sub

'    '''<summary>
'    '''A test for ActualX
'    '''</summary>
'    <Test()> _
'    Public Sub ActualXTest()
'        Dim element As Object = Nothing ' TODO: Initialize to an appropriate value
'        Dim target As Element = New Element(element) ' TODO: Initialize to an appropriate value
'        Dim expected As Integer = 0 ' TODO: Initialize to an appropriate value
'        Dim actual As Integer
'        actual = target.ActualX
'        Verify.AreEqual(expected, actual)
'        Verify.Inconclusive("Verify the correctness of this test method.")
'    End Sub

'    '''<summary>
'    '''A test for Element Constructor
'    '''</summary>
'    <Test()> _
'    Public Sub ElementConstructorTest()
'        Dim element As Object = Nothing ' TODO: Initialize to an appropriate value
'        Dim target As Element = New Element(element)
'        Verify.Inconclusive("TODO: Implement code to verify target")
'    End Sub
'End Class
