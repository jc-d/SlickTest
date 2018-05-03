'Imports NUnit.Framework

'Imports UIControls



''''<summary>
''''This is a test class for AutomationSettingsTest and is intended
''''to contain all AutomationSettingsTest Unit Tests
''''</summary>
'<TestFixture()>  _
'Public Class AutomationSettingsTest


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
'    '''A test for Timeout
'    '''</summary>
'    <Test()> _
'    Public Sub TimeoutTest()
'        Dim expected As Integer = 10
'        Dim actual As Integer
'        AutomationSettings.Timeout = expected
'        actual = AutomationSettings.Timeout
'        Verify.AreEqual(expected, actual)
'    End Sub

'    '''<summary>
'    '''A test for ProjectType
'    '''</summary>
'    <Test()> _
'    Public Sub ProjectTypeTest()
'        Dim actual As String
'        actual = AutomationSettings.ProjectType
'        Verify.Inconclusive("Verify the correctness of this test method.")
'    End Sub

'    '''<summary>
'    '''A test for ProjectName
'    '''</summary>
'    <Test()> _
'    Public Sub ProjectNameTest()
'        Dim actual As String
'        actual = AutomationSettings.ProjectName
'        Verify.Inconclusive("Verify the correctness of this test method.")
'    End Sub

'    '''<summary>
'    '''A test for ExistTimeout
'    '''</summary>
'    <Test()> _
'    Public Sub ExistTimeoutTest()
'        Dim Path As String = String.Empty ' TODO: Initialize to an appropriate value
'        Dim target As AutomationSettings = New AutomationSettings(Path) ' TODO: Initialize to an appropriate value
'        Dim expected As Integer = 0 ' TODO: Initialize to an appropriate value
'        Dim actual As Integer
'        target.ExistTimeout = expected
'        actual = target.ExistTimeout
'        Verify.AreEqual(expected, actual)
'        Verify.Inconclusive("Verify the correctness of this test method.")
'    End Sub

'    '''<summary>
'    '''A test for AutomationPath
'    '''</summary>
'    <Test()> _
'    Public Sub AutomationPathTest()
'        Dim Path As String = String.Empty ' TODO: Initialize to an appropriate value
'        Dim target As AutomationSettings = New AutomationSettings(Path) ' TODO: Initialize to an appropriate value
'        Dim actual As String
'        actual = target.AutomationPath
'        Verify.Inconclusive("Verify the correctness of this test method.")
'    End Sub

'    '''<summary>
'    '''A test for AutomationSettings Constructor
'    '''</summary>
'    <Test()> _
'    Public Sub AutomationSettingsConstructorTest()
'        Dim Path As String = String.Empty ' TODO: Initialize to an appropriate value
'        Dim target As AutomationSettings = New AutomationSettings(Path)
'        Verify.Inconclusive("TODO: Implement code to verify target")
'    End Sub
'End Class
