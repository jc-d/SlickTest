'Imports System.Text

'Imports System

'Imports NUnit.Framework

'Imports APIControls



''''<summary>
''''This is a test class for TopWindowTest and is intended
''''to contain all TopWindowTest Unit Tests
''''</summary>
'<TestFixture()>  _
'Public Class TopWindowTest


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
'    '''A test for ShowWindow
'    '''</summary>
'    <Test()> _
'    Public Sub ShowWindowTest1()
'        Dim target As TopWindow = New TopWindow ' TODO: Initialize to an appropriate value
'        Dim hWnd As IntPtr = New IntPtr ' TODO: Initialize to an appropriate value
'        Dim expected As Boolean = False ' TODO: Initialize to an appropriate value
'        Dim actual As Boolean
'        actual = target.ShowWindow(hWnd)
'        Verify.AreEqual(expected, actual)
'        Verify.Inconclusive("Verify the correctness of this test method.")
'    End Sub

'    '''<summary>
'    '''A test for ShowWindow
'    '''</summary>
'    <Test(), _
'     DeploymentItem("APIControls.dll")> _
'    Public Sub ShowWindowTest()
'        Dim hWnd As IntPtr = New IntPtr ' TODO: Initialize to an appropriate value
'        Dim nCmdShow As TopWindow_Accessor.WindowShowStyle = Nothing ' TODO: Initialize to an appropriate value
'        Dim expected As Boolean = False ' TODO: Initialize to an appropriate value
'        Dim actual As Boolean
'        actual = TopWindow_Accessor.ShowWindow(hWnd, nCmdShow)
'        Verify.AreEqual(expected, actual)
'        Verify.Inconclusive("Verify the correctness of this test method.")
'    End Sub

'    '''<summary>
'    '''A test for IsZoomed
'    '''</summary>
'    <Test()> _
'    Public Sub IsZoomedTest()
'        Dim hwnd As IntPtr = New IntPtr ' TODO: Initialize to an appropriate value
'        Dim expected As Boolean = False ' TODO: Initialize to an appropriate value
'        Dim actual As Boolean
'        actual = TopWindow.IsZoomed(hwnd)
'        Verify.AreEqual(expected, actual)
'        Verify.Inconclusive("Verify the correctness of this test method.")
'    End Sub

'    '''<summary>
'    '''A test for IsIconic
'    '''</summary>
'    <Test()> _
'    Public Sub IsIconicTest()
'        Dim hwnd As IntPtr = New IntPtr ' TODO: Initialize to an appropriate value
'        Dim expected As Boolean = False ' TODO: Initialize to an appropriate value
'        Dim actual As Boolean
'        actual = TopWindow.IsIconic(hwnd)
'        Verify.AreEqual(expected, actual)
'        Verify.Inconclusive("Verify the correctness of this test method.")
'    End Sub

'    '''<summary>
'    '''A test for GetWindowText
'    '''</summary>
'    <Test(), _
'     DeploymentItem("APIControls.dll")> _
'    Public Sub GetWindowTextTest()
'        Dim hWnd As Integer = 0 ' TODO: Initialize to an appropriate value
'        Dim text As StringBuilder = Nothing ' TODO: Initialize to an appropriate value
'        Dim count As Integer = 0 ' TODO: Initialize to an appropriate value
'        Dim expected As Integer = 0 ' TODO: Initialize to an appropriate value
'        Dim actual As Integer
'        actual = TopWindow_Accessor.GetWindowText(hWnd, text, count)
'        Verify.AreEqual(expected, actual)
'        Verify.Inconclusive("Verify the correctness of this test method.")
'    End Sub

'    '''<summary>
'    '''A test for GetForegroundWindow
'    '''</summary>
'    <Test(), _
'     DeploymentItem("APIControls.dll")> _
'    Public Sub GetForegroundWindowTest()
'        Dim expected As Integer = 0 ' TODO: Initialize to an appropriate value
'        Dim actual As Integer
'        actual = TopWindow_Accessor.GetForegroundWindow
'        Verify.AreEqual(expected, actual)
'        Verify.Inconclusive("Verify the correctness of this test method.")
'    End Sub

'    '''<summary>
'    '''A test for GetActiveWindowHandle
'    '''</summary>
'    <Test()> _
'    Public Sub GetActiveWindowHandleTest()
'        Dim target As TopWindow = New TopWindow ' TODO: Initialize to an appropriate value
'        Dim expected As IntPtr = New IntPtr ' TODO: Initialize to an appropriate value
'        Dim actual As IntPtr
'        actual = target.GetActiveWindowHandle
'        Verify.AreEqual(expected, actual)
'        Verify.Inconclusive("Verify the correctness of this test method.")
'    End Sub

'    '''<summary>
'    '''A test for GetActiveWindow
'    '''</summary>
'    <Test()> _
'    Public Sub GetActiveWindowTest()
'        Dim target As TopWindow = New TopWindow ' TODO: Initialize to an appropriate value
'        Dim expected As String = String.Empty ' TODO: Initialize to an appropriate value
'        Dim actual As String
'        actual = target.GetActiveWindow
'        Verify.AreEqual(expected, actual)
'        Verify.Inconclusive("Verify the correctness of this test method.")
'    End Sub

'    '''<summary>
'    '''A test for TopWindow Constructor
'    '''</summary>
'    <Test()> _
'    Public Sub TopWindowConstructorTest()
'        Dim target As TopWindow = New TopWindow
'        Verify.Inconclusive("TODO: Implement code to verify target")
'    End Sub
'End Class
