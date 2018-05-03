'Imports System

'Imports Microsoft.VisualStudio.TestTools.UnitTesting

'Imports APIControls



''''<summary>
''''This is a test class for DotNetNamesTest and is intended
''''to contain all DotNetNamesTest Unit Tests
''''</summary>
'<TestClass()> _
'Public Class DotNetNamesTest


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
'    '''A test for XProcGetControlName
'    '''</summary>
'    <TestMethod(), _
'     DeploymentItem("APIControls.dll")> _
'    Public Sub XProcGetControlNameTest()
'        Dim hwnd As IntPtr = New IntPtr ' TODO: Initialize to an appropriate value
'        Dim msg As Integer = 0 ' TODO: Initialize to an appropriate value
'        Dim expected As String = String.Empty ' TODO: Initialize to an appropriate value
'        Dim actual As String
'        actual = DotNetNames_Accessor.XProcGetControlName(hwnd, msg)
'        Assert.AreEqual(expected, actual)
'        Assert.Inconclusive("Verify the correctness of this test method.")
'    End Sub

'    '''<summary>
'    '''A test for GetWinFormsId
'    '''</summary>
'    <TestMethod()> _
'    Public Sub GetWinFormsIdTest()
'        Dim hWnd As IntPtr = New IntPtr ' TODO: Initialize to an appropriate value
'        Dim expected As String = String.Empty ' TODO: Initialize to an appropriate value
'        Dim actual As String
'        actual = DotNetNames.GetWinFormsId(hWnd)
'        Assert.AreEqual(expected, actual)
'        Assert.Inconclusive("Verify the correctness of this test method.")
'    End Sub

'    '''<summary>
'    '''A test for GetProcessIdFromHWnd
'    '''</summary>
'    <TestMethod(), _
'     DeploymentItem("APIControls.dll")> _
'    Public Sub GetProcessIdFromHWndTest()
'        Dim hwnd As IntPtr = New IntPtr ' TODO: Initialize to an appropriate value
'        Dim expected As Integer = 0 ' TODO: Initialize to an appropriate value
'        Dim actual As Integer
'        actual = DotNetNames_Accessor.GetProcessIdFromHWnd(hwnd)
'        Assert.AreEqual(expected, actual)
'        Assert.Inconclusive("Verify the correctness of this test method.")
'    End Sub

'    '''<summary>
'    '''A test for ByteArrayToString
'    '''</summary>
'    <TestMethod(), _
'     DeploymentItem("APIControls.dll")> _
'    Public Sub ByteArrayToStringTest()
'        Dim bytes() As Byte = Nothing ' TODO: Initialize to an appropriate value
'        Dim expected As String = String.Empty ' TODO: Initialize to an appropriate value
'        Dim actual As String
'        actual = DotNetNames_Accessor.ByteArrayToString(bytes)
'        Assert.AreEqual(expected, actual)
'        Assert.Inconclusive("Verify the correctness of this test method.")
'    End Sub

'    '''<summary>
'    '''A test for DotNetNames Constructor
'    '''</summary>
'    <TestMethod()> _
'    Public Sub DotNetNamesConstructorTest()
'        Dim target As DotNetNames = New DotNetNames
'        Assert.Inconclusive("TODO: Implement code to verify target")
'    End Sub
'End Class
