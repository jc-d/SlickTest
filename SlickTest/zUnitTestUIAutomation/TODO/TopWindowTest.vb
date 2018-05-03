'Imports System.Text

'Imports System

'Imports Microsoft.VisualStudio.TestTools.UnitTesting

'Imports APIControls



''''<summary>
''''This is a test class for TopWindowTest and is intended
''''to contain all TopWindowTest Unit Tests
''''</summary>
'<TestClass()> _
'Public Class TopWindowTest


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
'    '''A test for ShowWindow
'    '''</summary>
'    <TestMethod()> _
'    Public Sub ShowWindowTest1()
'        Dim target As TopWindow = New TopWindow ' TODO: Initialize to an appropriate value
'        Dim hWnd As IntPtr = New IntPtr ' TODO: Initialize to an appropriate value
'        Dim expected As Boolean = False ' TODO: Initialize to an appropriate value
'        Dim actual As Boolean
'        actual = target.ShowWindow(hWnd)
'        Assert.AreEqual(expected, actual)
'        Assert.Inconclusive("Verify the correctness of this test method.")
'    End Sub

'    '''<summary>
'    '''A test for ShowWindow
'    '''</summary>
'    <TestMethod(), _
'     DeploymentItem("APIControls.dll")> _
'    Public Sub ShowWindowTest()
'        Dim hWnd As IntPtr = New IntPtr ' TODO: Initialize to an appropriate value
'        Dim nCmdShow As TopWindow_Accessor.WindowShowStyle = Nothing ' TODO: Initialize to an appropriate value
'        Dim expected As Boolean = False ' TODO: Initialize to an appropriate value
'        Dim actual As Boolean
'        actual = TopWindow_Accessor.ShowWindow(hWnd, nCmdShow)
'        Assert.AreEqual(expected, actual)
'        Assert.Inconclusive("Verify the correctness of this test method.")
'    End Sub

'    '''<summary>
'    '''A test for IsZoomed
'    '''</summary>
'    <TestMethod()> _
'    Public Sub IsZoomedTest()
'        Dim hwnd As IntPtr = New IntPtr ' TODO: Initialize to an appropriate value
'        Dim expected As Boolean = False ' TODO: Initialize to an appropriate value
'        Dim actual As Boolean
'        actual = TopWindow.IsZoomed(hwnd)
'        Assert.AreEqual(expected, actual)
'        Assert.Inconclusive("Verify the correctness of this test method.")
'    End Sub

'    '''<summary>
'    '''A test for IsIconic
'    '''</summary>
'    <TestMethod()> _
'    Public Sub IsIconicTest()
'        Dim hwnd As IntPtr = New IntPtr ' TODO: Initialize to an appropriate value
'        Dim expected As Boolean = False ' TODO: Initialize to an appropriate value
'        Dim actual As Boolean
'        actual = TopWindow.IsIconic(hwnd)
'        Assert.AreEqual(expected, actual)
'        Assert.Inconclusive("Verify the correctness of this test method.")
'    End Sub

'    '''<summary>
'    '''A test for GetWindowText
'    '''</summary>
'    <TestMethod(), _
'     DeploymentItem("APIControls.dll")> _
'    Public Sub GetWindowTextTest()
'        Dim hWnd As Integer = 0 ' TODO: Initialize to an appropriate value
'        Dim text As StringBuilder = Nothing ' TODO: Initialize to an appropriate value
'        Dim count As Integer = 0 ' TODO: Initialize to an appropriate value
'        Dim expected As Integer = 0 ' TODO: Initialize to an appropriate value
'        Dim actual As Integer
'        actual = TopWindow_Accessor.GetWindowText(hWnd, text, count)
'        Assert.AreEqual(expected, actual)
'        Assert.Inconclusive("Verify the correctness of this test method.")
'    End Sub

'    '''<summary>
'    '''A test for GetForegroundWindow
'    '''</summary>
'    <TestMethod(), _
'     DeploymentItem("APIControls.dll")> _
'    Public Sub GetForegroundWindowTest()
'        Dim expected As Integer = 0 ' TODO: Initialize to an appropriate value
'        Dim actual As Integer
'        actual = TopWindow_Accessor.GetForegroundWindow
'        Assert.AreEqual(expected, actual)
'        Assert.Inconclusive("Verify the correctness of this test method.")
'    End Sub

'    '''<summary>
'    '''A test for GetActiveWindowHandle
'    '''</summary>
'    <TestMethod()> _
'    Public Sub GetActiveWindowHandleTest()
'        Dim target As TopWindow = New TopWindow ' TODO: Initialize to an appropriate value
'        Dim expected As IntPtr = New IntPtr ' TODO: Initialize to an appropriate value
'        Dim actual As IntPtr
'        actual = target.GetActiveWindowHandle
'        Assert.AreEqual(expected, actual)
'        Assert.Inconclusive("Verify the correctness of this test method.")
'    End Sub

'    '''<summary>
'    '''A test for GetActiveWindow
'    '''</summary>
'    <TestMethod()> _
'    Public Sub GetActiveWindowTest()
'        Dim target As TopWindow = New TopWindow ' TODO: Initialize to an appropriate value
'        Dim expected As String = String.Empty ' TODO: Initialize to an appropriate value
'        Dim actual As String
'        actual = target.GetActiveWindow
'        Assert.AreEqual(expected, actual)
'        Assert.Inconclusive("Verify the correctness of this test method.")
'    End Sub

'    '''<summary>
'    '''A test for TopWindow Constructor
'    '''</summary>
'    <TestMethod()> _
'    Public Sub TopWindowConstructorTest()
'        Dim target As TopWindow = New TopWindow
'        Assert.Inconclusive("TODO: Implement code to verify target")
'    End Sub
'End Class
