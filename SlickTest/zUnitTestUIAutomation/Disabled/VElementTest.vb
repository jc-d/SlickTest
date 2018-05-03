'Imports Microsoft.VisualStudio.TestTools.UnitTesting

'Imports APIControls



''''<summary>
''''This is a test class for VElementTest and is intended
''''to contain all VElementTest Unit Tests
''''</summary>
'<TestClass()> _
'Public Class VElementTest


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
'        Dim elem As IElement = Nothing ' TODO: Initialize to an appropriate value
'        Dim target As IElement = New VElement(elem) ' TODO: Initialize to an appropriate value
'        Dim actual As Integer
'        actual = target.Width
'        Assert.Inconclusive("Verify the correctness of this test method.")
'    End Sub

'    '''<summary>
'    '''A test for Top
'    '''</summary>
'    <TestMethod()> _
'    Public Sub TopTest()
'        Dim elem As IElement = Nothing ' TODO: Initialize to an appropriate value
'        Dim target As IElement = New VElement(elem) ' TODO: Initialize to an appropriate value
'        Dim actual As Integer
'        actual = target.Top
'        Assert.Inconclusive("Verify the correctness of this test method.")
'    End Sub

'    '''<summary>
'    '''A test for Title
'    '''</summary>
'    <TestMethod()> _
'    Public Sub TitleTest()
'        Dim elem As IElement = Nothing ' TODO: Initialize to an appropriate value
'        Dim target As IElement = New VElement(elem) ' TODO: Initialize to an appropriate value
'        Dim actual As String
'        actual = target.Title
'        Assert.Inconclusive("Verify the correctness of this test method.")
'    End Sub

'    '''<summary>
'    '''A test for Text
'    '''</summary>
'    <TestMethod()> _
'    Public Sub TextTest()
'        Dim elem As IElement = Nothing ' TODO: Initialize to an appropriate value
'        Dim target As IElement = New VElement(elem) ' TODO: Initialize to an appropriate value
'        Dim actual As String
'        actual = target.Text
'        Assert.Inconclusive("Verify the correctness of this test method.")
'    End Sub

'    '''<summary>
'    '''A test for TagName
'    '''</summary>
'    <TestMethod()> _
'    Public Sub TagNameTest()
'        Dim elem As IElement = Nothing ' TODO: Initialize to an appropriate value
'        Dim target As IElement = New VElement(elem) ' TODO: Initialize to an appropriate value
'        Dim actual As String
'        actual = target.TagName
'        Assert.Inconclusive("Verify the correctness of this test method.")
'    End Sub

'    '''<summary>
'    '''A test for Right
'    '''</summary>
'    <TestMethod()> _
'    Public Sub RightTest()
'        Dim elem As IElement = Nothing ' TODO: Initialize to an appropriate value
'        Dim target As IElement = New VElement(elem) ' TODO: Initialize to an appropriate value
'        Dim actual As Integer
'        actual = target.Right
'        Assert.Inconclusive("Verify the correctness of this test method.")
'    End Sub

'    '''<summary>
'    '''A test for OuterText
'    '''</summary>
'    <TestMethod()> _
'    Public Sub OuterTextTest()
'        Dim elem As IElement = Nothing ' TODO: Initialize to an appropriate value
'        Dim target As IElement = New VElement(elem) ' TODO: Initialize to an appropriate value
'        Dim actual As String
'        actual = target.OuterText
'        Assert.Inconclusive("Verify the correctness of this test method.")
'    End Sub

'    '''<summary>
'    '''A test for OuterHTML
'    '''</summary>
'    <TestMethod()> _
'    Public Sub OuterHTMLTest()
'        Dim elem As IElement = Nothing ' TODO: Initialize to an appropriate value
'        Dim target As IElement = New VElement(elem) ' TODO: Initialize to an appropriate value
'        Dim actual As String
'        actual = target.OuterHTML
'        Assert.Inconclusive("Verify the correctness of this test method.")
'    End Sub

'    '''<summary>
'    '''A test for Left
'    '''</summary>
'    <TestMethod()> _
'    Public Sub LeftTest()
'        Dim elem As IElement = Nothing ' TODO: Initialize to an appropriate value
'        Dim target As IElement = New VElement(elem) ' TODO: Initialize to an appropriate value
'        Dim actual As Integer
'        actual = target.Left
'        Assert.Inconclusive("Verify the correctness of this test method.")
'    End Sub

'    '''<summary>
'    '''A test for InnerHTML
'    '''</summary>
'    <TestMethod()> _
'    Public Sub InnerHTMLTest()
'        Dim elem As IElement = Nothing ' TODO: Initialize to an appropriate value
'        Dim target As IElement = New VElement(elem) ' TODO: Initialize to an appropriate value
'        Dim actual As String
'        actual = target.InnerHTML
'        Assert.Inconclusive("Verify the correctness of this test method.")
'    End Sub

'    '''<summary>
'    '''A test for Id
'    '''</summary>
'    <TestMethod()> _
'    Public Sub IdTest()
'        Dim elem As IElement = Nothing ' TODO: Initialize to an appropriate value
'        Dim target As IElement = New VElement(elem) ' TODO: Initialize to an appropriate value
'        Dim actual As String
'        actual = target.Id
'        Assert.Inconclusive("Verify the correctness of this test method.")
'    End Sub

'    '''<summary>
'    '''A test for Height
'    '''</summary>
'    <TestMethod()> _
'    Public Sub HeightTest()
'        Dim elem As IElement = Nothing ' TODO: Initialize to an appropriate value
'        Dim target As IElement = New VElement(elem) ' TODO: Initialize to an appropriate value
'        Dim actual As Integer
'        actual = target.Height
'        Assert.Inconclusive("Verify the correctness of this test method.")
'    End Sub

'    '''<summary>
'    '''A test for Enabled
'    '''</summary>
'    <TestMethod()> _
'    Public Sub EnabledTest()
'        Dim elem As IElement = Nothing ' TODO: Initialize to an appropriate value
'        Dim target As IElement = New VElement(elem) ' TODO: Initialize to an appropriate value
'        Dim actual As Boolean
'        actual = target.Enabled
'        Assert.Inconclusive("Verify the correctness of this test method.")
'    End Sub

'    '''<summary>
'    '''A test for Bottom
'    '''</summary>
'    <TestMethod()> _
'    Public Sub BottomTest()
'        Dim elem As IElement = Nothing ' TODO: Initialize to an appropriate value
'        Dim target As IElement = New VElement(elem) ' TODO: Initialize to an appropriate value
'        Dim actual As Integer
'        actual = target.Bottom
'        Assert.Inconclusive("Verify the correctness of this test method.")
'    End Sub

'    '''<summary>
'    '''A test for Attribute
'    '''</summary>
'    <TestMethod()> _
'    Public Sub AttributeTest()
'        Dim elem As IElement = Nothing ' TODO: Initialize to an appropriate value
'        Dim target As IElement = New VElement(elem) ' TODO: Initialize to an appropriate value
'        Dim AttributeName As String = String.Empty ' TODO: Initialize to an appropriate value
'        Dim actual As String
'        actual = target.Attribute(AttributeName)
'        Assert.Inconclusive("Verify the correctness of this test method.")
'    End Sub

'    '''<summary>
'    '''A test for VElement Constructor
'    '''</summary>
'    <TestMethod()> _
'    Public Sub VElementConstructorTest1()
'        Dim elem As IElement = Nothing ' TODO: Initialize to an appropriate value
'        Dim target As VElement = New VElement(elem)
'        Assert.Inconclusive("TODO: Implement code to verify target")
'    End Sub

'    '''<summary>
'    '''A test for VElement Constructor
'    '''</summary>
'    <TestMethod()> _
'    Public Sub VElementConstructorTest()
'        Dim elem1 As Object = Nothing ' TODO: Initialize to an appropriate value
'        Dim target As VElement = New VElement(elem1)
'        Assert.Inconclusive("TODO: Implement code to verify target")
'    End Sub
'End Class
