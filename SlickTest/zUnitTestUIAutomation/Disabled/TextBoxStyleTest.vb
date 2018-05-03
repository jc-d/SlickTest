'Imports NUnit.Framework

'Imports UIControls



''''<summary>
''''This is a test class for TextBoxStyleTest and is intended
''''to contain all TextBoxStyleTest Unit Tests
''''</summary>
'<TestFixture()>  _
'Public Class TextBoxStyleTest


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
'    '''A test for ES_WANTRETURN
'    '''</summary>
'    <Test()> _
'    Public Sub ES_WANTRETURNTest()
'        Dim target As TextBoxStyle = New TextBoxStyle ' TODO: Initialize to an appropriate value
'        Dim actual As Integer
'        actual = target.ES_WANTRETURN
'        Verify.Inconclusive("Verify the correctness of this test method.")
'    End Sub

'    '''<summary>
'    '''A test for ES_UPPERCASE
'    '''</summary>
'    <Test()> _
'    Public Sub ES_UPPERCASETest()
'        Dim target As TextBoxStyle = New TextBoxStyle ' TODO: Initialize to an appropriate value
'        Dim actual As Integer
'        actual = target.ES_UPPERCASE
'        Verify.Inconclusive("Verify the correctness of this test method.")
'    End Sub

'    '''<summary>
'    '''A test for ES_RIGHT
'    '''</summary>
'    <Test()> _
'    Public Sub ES_RIGHTTest()
'        Dim target As TextBoxStyle = New TextBoxStyle ' TODO: Initialize to an appropriate value
'        Dim actual As Integer
'        actual = target.ES_RIGHT
'        Verify.Inconclusive("Verify the correctness of this test method.")
'    End Sub

'    '''<summary>
'    '''A test for ES_READONLY
'    '''</summary>
'    <Test()> _
'    Public Sub ES_READONLYTest()
'        Dim target As TextBoxStyle = New TextBoxStyle ' TODO: Initialize to an appropriate value
'        Dim actual As Integer
'        actual = target.ES_READONLY
'        Verify.Inconclusive("Verify the correctness of this test method.")
'    End Sub

'    '''<summary>
'    '''A test for ES_PASSWORD
'    '''</summary>
'    <Test()> _
'    Public Sub ES_PASSWORDTest()
'        Dim target As TextBoxStyle = New TextBoxStyle ' TODO: Initialize to an appropriate value
'        Dim actual As Integer
'        actual = target.ES_PASSWORD
'        Verify.Inconclusive("Verify the correctness of this test method.")
'    End Sub

'    '''<summary>
'    '''A test for ES_OEMCONVERT
'    '''</summary>
'    <Test()> _
'    Public Sub ES_OEMCONVERTTest()
'        Dim target As TextBoxStyle = New TextBoxStyle ' TODO: Initialize to an appropriate value
'        Dim actual As Integer
'        actual = target.ES_OEMCONVERT
'        Verify.Inconclusive("Verify the correctness of this test method.")
'    End Sub

'    '''<summary>
'    '''A test for ES_NOHIDESEL
'    '''</summary>
'    <Test()> _
'    Public Sub ES_NOHIDESELTest()
'        Dim target As TextBoxStyle = New TextBoxStyle ' TODO: Initialize to an appropriate value
'        Dim actual As Integer
'        actual = target.ES_NOHIDESEL
'        Verify.Inconclusive("Verify the correctness of this test method.")
'    End Sub

'    '''<summary>
'    '''A test for ES_MULTILINE
'    '''</summary>
'    <Test()> _
'    Public Sub ES_MULTILINETest()
'        Dim target As TextBoxStyle = New TextBoxStyle ' TODO: Initialize to an appropriate value
'        Dim actual As Integer
'        actual = target.ES_MULTILINE
'        Verify.Inconclusive("Verify the correctness of this test method.")
'    End Sub

'    '''<summary>
'    '''A test for ES_LOWERCASE
'    '''</summary>
'    <Test()> _
'    Public Sub ES_LOWERCASETest()
'        Dim target As TextBoxStyle = New TextBoxStyle ' TODO: Initialize to an appropriate value
'        Dim actual As Integer
'        actual = target.ES_LOWERCASE
'        Verify.Inconclusive("Verify the correctness of this test method.")
'    End Sub

'    '''<summary>
'    '''A test for ES_LEFT
'    '''</summary>
'    <Test()> _
'    Public Sub ES_LEFTTest()
'        Dim target As TextBoxStyle = New TextBoxStyle ' TODO: Initialize to an appropriate value
'        Dim actual As Integer
'        actual = target.ES_LEFT
'        Verify.Inconclusive("Verify the correctness of this test method.")
'    End Sub

'    '''<summary>
'    '''A test for ES_CENTER
'    '''</summary>
'    <Test()> _
'    Public Sub ES_CENTERTest()
'        Dim target As TextBoxStyle = New TextBoxStyle ' TODO: Initialize to an appropriate value
'        Dim actual As Integer
'        actual = target.ES_CENTER
'        Verify.Inconclusive("Verify the correctness of this test method.")
'    End Sub

'    '''<summary>
'    '''A test for ES_AUTOVSCROLL
'    '''</summary>
'    <Test()> _
'    Public Sub ES_AUTOVSCROLLTest()
'        Dim target As TextBoxStyle = New TextBoxStyle ' TODO: Initialize to an appropriate value
'        Dim actual As Integer
'        actual = target.ES_AUTOVSCROLL
'        Verify.Inconclusive("Verify the correctness of this test method.")
'    End Sub

'    '''<summary>
'    '''A test for ES_AUTOHSCROLL
'    '''</summary>
'    <Test()> _
'    Public Sub ES_AUTOHSCROLLTest()
'        Dim target As TextBoxStyle = New TextBoxStyle ' TODO: Initialize to an appropriate value
'        Dim actual As Integer
'        actual = target.ES_AUTOHSCROLL
'        Verify.Inconclusive("Verify the correctness of this test method.")
'    End Sub

'    '''<summary>
'    '''A test for ValueInfo
'    '''</summary>
'    <Test()> _
'    Public Sub ValueInfoTest()
'        Dim target As TextBoxStyle = New TextBoxStyle ' TODO: Initialize to an appropriate value
'        Dim StyleValue As Integer = 0 ' TODO: Initialize to an appropriate value
'        Dim expected As String = String.Empty ' TODO: Initialize to an appropriate value
'        Dim actual As String
'        actual = target.ValueInfo(StyleValue)
'        Verify.AreEqual(expected, actual)
'        Verify.Inconclusive("Verify the correctness of this test method.")
'    End Sub

'    '''<summary>
'    '''A test for ContainsValue
'    '''</summary>
'    <Test()> _
'    Public Sub ContainsValueTest()
'        Dim target As TextBoxStyle = New TextBoxStyle ' TODO: Initialize to an appropriate value
'        Dim StyleValue As Integer = 0 ' TODO: Initialize to an appropriate value
'        Dim ES_Value As Integer = 0 ' TODO: Initialize to an appropriate value
'        Dim expected As Boolean = False ' TODO: Initialize to an appropriate value
'        Dim actual As Boolean
'        actual = target.ContainsValue(StyleValue, ES_Value)
'        Verify.AreEqual(expected, actual)
'        Verify.Inconclusive("Verify the correctness of this test method.")
'    End Sub

'    '''<summary>
'    '''A test for TextBoxStyle Constructor
'    '''</summary>
'    <Test()> _
'    Public Sub TextBoxStyleConstructorTest()
'        Dim target As TextBoxStyle = New TextBoxStyle
'        Verify.Inconclusive("TODO: Implement code to verify target")
'    End Sub
'End Class
