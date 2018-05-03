'Imports System

'Imports Microsoft.VisualStudio.TestTools.UnitTesting

'Imports APIControls



''''<summary>
''''This is a test class for EnumerateWindowsTest and is intended
''''to contain all EnumerateWindowsTest Unit Tests
''''</summary>
'<TestClass()> _
'Public Class EnumerateWindowsTest


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
'    '''A test for Show
'    '''</summary>
'    <TestMethod()> _
'    Public Sub ShowTest()
'        Dim title As String = String.Empty ' TODO: Initialize to an appropriate value
'        EnumerateWindows.Show(title)
'        Assert.Inconclusive("A method that does not return a value cannot be verified.")
'    End Sub

'    '''<summary>
'    '''A test for SearchChildHandlesWild
'    '''</summary>
'    <TestMethod()> _
'    Public Sub SearchChildHandlesWildTest()
'        Dim handle As IntPtr = New IntPtr ' TODO: Initialize to an appropriate value
'        Dim Name As String = String.Empty ' TODO: Initialize to an appropriate value
'        Dim Value As String = String.Empty ' TODO: Initialize to an appropriate value
'        Dim expected As IntPtr = New IntPtr ' TODO: Initialize to an appropriate value
'        Dim actual As IntPtr
'        actual = EnumerateWindows.SearchChildHandlesWild(handle, Name, Value)
'        Assert.AreEqual(expected, actual)
'        Assert.Inconclusive("Verify the correctness of this test method.")
'    End Sub

'    '''<summary>
'    '''A test for SearchChildHandles
'    '''</summary>
'    <TestMethod()> _
'    Public Sub SearchChildHandlesTest()
'        Dim handle As IntPtr = New IntPtr ' TODO: Initialize to an appropriate value
'        Dim Name As String = String.Empty ' TODO: Initialize to an appropriate value
'        Dim Value As String = String.Empty ' TODO: Initialize to an appropriate value
'        Dim expected As IntPtr = New IntPtr ' TODO: Initialize to an appropriate value
'        Dim actual As IntPtr
'        actual = EnumerateWindows.SearchChildHandles(handle, Name, Value)
'        Assert.AreEqual(expected, actual)
'        Assert.Inconclusive("Verify the correctness of this test method.")
'    End Sub

'    '''<summary>
'    '''A test for isChildDirectlyConnectedToParent
'    '''</summary>
'    <TestMethod()> _
'    Public Sub isChildDirectlyConnectedToParentTest()
'        Dim parent As IntPtr = New IntPtr ' TODO: Initialize to an appropriate value
'        Dim child As IntPtr = New IntPtr ' TODO: Initialize to an appropriate value
'        Dim expected As Boolean = False ' TODO: Initialize to an appropriate value
'        Dim actual As Boolean
'        actual = EnumerateWindows.isChildDirectlyConnectedToParent(parent, child)
'        Assert.AreEqual(expected, actual)
'        Assert.Inconclusive("Verify the correctness of this test method.")
'    End Sub

'    '''<summary>
'    '''A test for Hide
'    '''</summary>
'    <TestMethod()> _
'    Public Sub HideTest()
'        Dim title As String = String.Empty ' TODO: Initialize to an appropriate value
'        EnumerateWindows.Hide(title)
'        Assert.Inconclusive("A method that does not return a value cannot be verified.")
'    End Sub

'    '''<summary>
'    '''A test for HandleChildCallback
'    '''</summary>
'    <TestMethod(), _
'     DeploymentItem("APIControls.dll")> _
'    Public Sub HandleChildCallbackTest()
'        Dim handle As IntPtr = New IntPtr ' TODO: Initialize to an appropriate value
'        Dim parameter As Integer = 0 ' TODO: Initialize to an appropriate value
'        Dim expected As Boolean = False ' TODO: Initialize to an appropriate value
'        Dim actual As Boolean
'        actual = EnumerateWindows_Accessor.HandleChildCallback(handle, parameter)
'        Assert.AreEqual(expected, actual)
'        Assert.Inconclusive("Verify the correctness of this test method.")
'    End Sub

'    '''<summary>
'    '''A test for GetTitle
'    '''</summary>
'    <TestMethod()> _
'    Public Sub GetTitleTest()
'        Dim hWnd As IntPtr = New IntPtr ' TODO: Initialize to an appropriate value
'        Dim expected As String = String.Empty ' TODO: Initialize to an appropriate value
'        Dim actual As String
'        actual = EnumerateWindows.GetTitle(hWnd)
'        Assert.AreEqual(expected, actual)
'        Assert.Inconclusive("Verify the correctness of this test method.")
'    End Sub

'    '''<summary>
'    '''A test for GetHandles
'    '''</summary>
'    <TestMethod()> _
'    Public Sub GetHandlesTest()
'        Dim expected() As IntPtr = Nothing ' TODO: Initialize to an appropriate value
'        Dim actual() As IntPtr
'        actual = EnumerateWindows.GetHandles
'        Assert.AreEqual(expected, actual)
'        Assert.Inconclusive("Verify the correctness of this test method.")
'    End Sub

'    '''<summary>
'    '''A test for GetHandle
'    '''</summary>
'    <TestMethod()> _
'    Public Sub GetHandleTest()
'        Dim title As String = String.Empty ' TODO: Initialize to an appropriate value
'        Dim expected As IntPtr = New IntPtr ' TODO: Initialize to an appropriate value
'        Dim actual As IntPtr
'        actual = EnumerateWindows.GetHandle(title)
'        Assert.AreEqual(expected, actual)
'        Assert.Inconclusive("Verify the correctness of this test method.")
'    End Sub

'    '''<summary>
'    '''A test for GetChildText
'    '''</summary>
'    <TestMethod()> _
'    Public Sub GetChildTextTest()
'        Dim handle As IntPtr = New IntPtr ' TODO: Initialize to an appropriate value
'        Dim expected As String = String.Empty ' TODO: Initialize to an appropriate value
'        Dim actual As String
'        actual = EnumerateWindows.GetChildText(handle)
'        Assert.AreEqual(expected, actual)
'        Assert.Inconclusive("Verify the correctness of this test method.")
'    End Sub

'    '''<summary>
'    '''A test for GetChildHandles
'    '''</summary>
'    <TestMethod()> _
'    Public Sub GetChildHandlesTest()
'        Dim handle As IntPtr = New IntPtr ' TODO: Initialize to an appropriate value
'        Dim expected() As IntPtr = Nothing ' TODO: Initialize to an appropriate value
'        Dim actual() As IntPtr
'        actual = EnumerateWindows.GetChildHandles(handle)
'        Assert.AreEqual(expected, actual)
'        Assert.Inconclusive("Verify the correctness of this test method.")
'    End Sub

'    '''<summary>
'    '''A test for FindWindowWild
'    '''</summary>
'    <TestMethod()> _
'    Public Sub FindWindowWildTest()
'        Dim sWildName As String = String.Empty ' TODO: Initialize to an appropriate value
'        Dim sWildValue As String = String.Empty ' TODO: Initialize to an appropriate value
'        Dim bMatchCase As Boolean = False ' TODO: Initialize to an appropriate value
'        Dim expected As IntPtr = New IntPtr ' TODO: Initialize to an appropriate value
'        Dim actual As IntPtr
'        actual = EnumerateWindows.FindWindowWild(sWildName, sWildValue, bMatchCase)
'        Assert.AreEqual(expected, actual)
'        Assert.Inconclusive("Verify the correctness of this test method.")
'    End Sub

'    '''<summary>
'    '''A test for Exists
'    '''</summary>
'    <TestMethod()> _
'    Public Sub ExistsTest()
'        Dim title As String = String.Empty ' TODO: Initialize to an appropriate value
'        Dim text As String = String.Empty ' TODO: Initialize to an appropriate value
'        Dim expected As Boolean = False ' TODO: Initialize to an appropriate value
'        Dim actual As Boolean
'        actual = EnumerateWindows.Exists(title, text)
'        Assert.AreEqual(expected, actual)
'        Assert.Inconclusive("Verify the correctness of this test method.")
'    End Sub

'    '''<summary>
'    '''A test for EnumWinProc
'    '''</summary>
'    <TestMethod(), _
'     DeploymentItem("APIControls.dll")> _
'    Public Sub EnumWinProcTest()
'        Dim hwnd As IntPtr = New IntPtr ' TODO: Initialize to an appropriate value
'        Dim lParam As Integer = 0 ' TODO: Initialize to an appropriate value
'        Dim expected As Boolean = False ' TODO: Initialize to an appropriate value
'        Dim actual As Boolean
'        actual = EnumerateWindows_Accessor.EnumWinProc(hwnd, lParam)
'        Assert.AreEqual(expected, actual)
'        Assert.Inconclusive("Verify the correctness of this test method.")
'    End Sub

'    '''<summary>
'    '''A test for EnumWindowsProc
'    '''</summary>
'    <TestMethod(), _
'     DeploymentItem("APIControls.dll")> _
'    Public Sub EnumWindowsProcTest()
'        Dim handle As IntPtr = New IntPtr ' TODO: Initialize to an appropriate value
'        Dim parameter As Integer = 0 ' TODO: Initialize to an appropriate value
'        Dim expected As Boolean = False ' TODO: Initialize to an appropriate value
'        Dim actual As Boolean
'        actual = EnumerateWindows_Accessor.EnumWindowsProc(handle, parameter)
'        Assert.AreEqual(expected, actual)
'        Assert.Inconclusive("Verify the correctness of this test method.")
'    End Sub

'    '''<summary>
'    '''A test for Close
'    '''</summary>
'    <TestMethod()> _
'    Public Sub CloseTest()
'        Dim title As String = String.Empty ' TODO: Initialize to an appropriate value
'        EnumerateWindows.Close(title)
'        Assert.Inconclusive("A method that does not return a value cannot be verified.")
'    End Sub

'    '''<summary>
'    '''A test for EnumerateWindows Constructor
'    '''</summary>
'    <TestMethod()> _
'    Public Sub EnumerateWindowsConstructorTest()
'        Dim target As EnumerateWindows = New EnumerateWindows
'        Assert.Inconclusive("TODO: Implement code to verify target")
'    End Sub
'End Class
