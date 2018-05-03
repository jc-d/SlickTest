'Imports System

'Imports NUnit.Framework

'Imports APIControls



''''<summary>
''''This is a test class for EnumerateWindowsTest and is intended
''''to contain all EnumerateWindowsTest Unit Tests
''''</summary>
'<TestFixture()>  _
'Public Class EnumerateWindowsTest


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
'    '''A test for Show
'    '''</summary>
'    <Test()> _
'    Public Sub ShowTest()
'        Dim title As String = String.Empty ' TODO: Initialize to an appropriate value
'        EnumerateWindows.Show(title)
'        Verify.Inconclusive("A method that does not return a value cannot be verified.")
'    End Sub

'    '''<summary>
'    '''A test for SearchChildHandlesWild
'    '''</summary>
'    <Test()> _
'    Public Sub SearchChildHandlesWildTest()
'        Dim handle As IntPtr = New IntPtr ' TODO: Initialize to an appropriate value
'        Dim Name As String = String.Empty ' TODO: Initialize to an appropriate value
'        Dim Value As String = String.Empty ' TODO: Initialize to an appropriate value
'        Dim expected As IntPtr = New IntPtr ' TODO: Initialize to an appropriate value
'        Dim actual As IntPtr
'        actual = EnumerateWindows.SearchChildHandlesWild(handle, Name, Value)
'        Verify.AreEqual(expected, actual)
'        Verify.Inconclusive("Verify the correctness of this test method.")
'    End Sub

'    '''<summary>
'    '''A test for SearchChildHandles
'    '''</summary>
'    <Test()> _
'    Public Sub SearchChildHandlesTest()
'        Dim handle As IntPtr = New IntPtr ' TODO: Initialize to an appropriate value
'        Dim Name As String = String.Empty ' TODO: Initialize to an appropriate value
'        Dim Value As String = String.Empty ' TODO: Initialize to an appropriate value
'        Dim expected As IntPtr = New IntPtr ' TODO: Initialize to an appropriate value
'        Dim actual As IntPtr
'        actual = EnumerateWindows.SearchChildHandles(handle, Name, Value)
'        Verify.AreEqual(expected, actual)
'        Verify.Inconclusive("Verify the correctness of this test method.")
'    End Sub

'    '''<summary>
'    '''A test for isChildDirectlyConnectedToParent
'    '''</summary>
'    <Test()> _
'    Public Sub isChildDirectlyConnectedToParentTest()
'        Dim parent As IntPtr = New IntPtr ' TODO: Initialize to an appropriate value
'        Dim child As IntPtr = New IntPtr ' TODO: Initialize to an appropriate value
'        Dim expected As Boolean = False ' TODO: Initialize to an appropriate value
'        Dim actual As Boolean
'        actual = EnumerateWindows.isChildDirectlyConnectedToParent(parent, child)
'        Verify.AreEqual(expected, actual)
'        Verify.Inconclusive("Verify the correctness of this test method.")
'    End Sub

'    '''<summary>
'    '''A test for Hide
'    '''</summary>
'    <Test()> _
'    Public Sub HideTest()
'        Dim title As String = String.Empty ' TODO: Initialize to an appropriate value
'        EnumerateWindows.Hide(title)
'        Verify.Inconclusive("A method that does not return a value cannot be verified.")
'    End Sub

'    '''<summary>
'    '''A test for HandleChildCallback
'    '''</summary>
'    <Test(), _
'     DeploymentItem("APIControls.dll")> _
'    Public Sub HandleChildCallbackTest()
'        Dim handle As IntPtr = New IntPtr ' TODO: Initialize to an appropriate value
'        Dim parameter As Integer = 0 ' TODO: Initialize to an appropriate value
'        Dim expected As Boolean = False ' TODO: Initialize to an appropriate value
'        Dim actual As Boolean
'        actual = EnumerateWindows_Accessor.HandleChildCallback(handle, parameter)
'        Verify.AreEqual(expected, actual)
'        Verify.Inconclusive("Verify the correctness of this test method.")
'    End Sub

'    '''<summary>
'    '''A test for GetTitle
'    '''</summary>
'    <Test()> _
'    Public Sub GetTitleTest()
'        Dim hWnd As IntPtr = New IntPtr ' TODO: Initialize to an appropriate value
'        Dim expected As String = String.Empty ' TODO: Initialize to an appropriate value
'        Dim actual As String
'        actual = EnumerateWindows.GetTitle(hWnd)
'        Verify.AreEqual(expected, actual)
'        Verify.Inconclusive("Verify the correctness of this test method.")
'    End Sub

'    '''<summary>
'    '''A test for GetHandles
'    '''</summary>
'    <Test()> _
'    Public Sub GetHandlesTest()
'        Dim expected() As IntPtr = Nothing ' TODO: Initialize to an appropriate value
'        Dim actual() As IntPtr
'        actual = EnumerateWindows.GetHandles
'        Verify.AreEqual(expected, actual)
'        Verify.Inconclusive("Verify the correctness of this test method.")
'    End Sub

'    '''<summary>
'    '''A test for GetHandle
'    '''</summary>
'    <Test()> _
'    Public Sub GetHandleTest()
'        Dim title As String = String.Empty ' TODO: Initialize to an appropriate value
'        Dim expected As IntPtr = New IntPtr ' TODO: Initialize to an appropriate value
'        Dim actual As IntPtr
'        actual = EnumerateWindows.GetHandle(title)
'        Verify.AreEqual(expected, actual)
'        Verify.Inconclusive("Verify the correctness of this test method.")
'    End Sub

'    '''<summary>
'    '''A test for GetChildText
'    '''</summary>
'    <Test()> _
'    Public Sub GetChildTextTest()
'        Dim handle As IntPtr = New IntPtr ' TODO: Initialize to an appropriate value
'        Dim expected As String = String.Empty ' TODO: Initialize to an appropriate value
'        Dim actual As String
'        actual = EnumerateWindows.GetChildText(handle)
'        Verify.AreEqual(expected, actual)
'        Verify.Inconclusive("Verify the correctness of this test method.")
'    End Sub

'    '''<summary>
'    '''A test for GetChildHandles
'    '''</summary>
'    <Test()> _
'    Public Sub GetChildHandlesTest()
'        Dim handle As IntPtr = New IntPtr ' TODO: Initialize to an appropriate value
'        Dim expected() As IntPtr = Nothing ' TODO: Initialize to an appropriate value
'        Dim actual() As IntPtr
'        actual = EnumerateWindows.GetChildHandles(handle)
'        Verify.AreEqual(expected, actual)
'        Verify.Inconclusive("Verify the correctness of this test method.")
'    End Sub

'    '''<summary>
'    '''A test for FindWindowWild
'    '''</summary>
'    <Test()> _
'    Public Sub FindWindowWildTest()
'        Dim sWildName As String = String.Empty ' TODO: Initialize to an appropriate value
'        Dim sWildValue As String = String.Empty ' TODO: Initialize to an appropriate value
'        Dim bMatchCase As Boolean = False ' TODO: Initialize to an appropriate value
'        Dim expected As IntPtr = New IntPtr ' TODO: Initialize to an appropriate value
'        Dim actual As IntPtr
'        actual = EnumerateWindows.FindWindowWild(sWildName, sWildValue, bMatchCase)
'        Verify.AreEqual(expected, actual)
'        Verify.Inconclusive("Verify the correctness of this test method.")
'    End Sub

'    '''<summary>
'    '''A test for Exists
'    '''</summary>
'    <Test()> _
'    Public Sub ExistsTest()
'        Dim title As String = String.Empty ' TODO: Initialize to an appropriate value
'        Dim text As String = String.Empty ' TODO: Initialize to an appropriate value
'        Dim expected As Boolean = False ' TODO: Initialize to an appropriate value
'        Dim actual As Boolean
'        actual = EnumerateWindows.Exists(title, text)
'        Verify.AreEqual(expected, actual)
'        Verify.Inconclusive("Verify the correctness of this test method.")
'    End Sub

'    '''<summary>
'    '''A test for EnumWinProc
'    '''</summary>
'    <Test(), _
'     DeploymentItem("APIControls.dll")> _
'    Public Sub EnumWinProcTest()
'        Dim hwnd As IntPtr = New IntPtr ' TODO: Initialize to an appropriate value
'        Dim lParam As Integer = 0 ' TODO: Initialize to an appropriate value
'        Dim expected As Boolean = False ' TODO: Initialize to an appropriate value
'        Dim actual As Boolean
'        actual = EnumerateWindows_Accessor.EnumWinProc(hwnd, lParam)
'        Verify.AreEqual(expected, actual)
'        Verify.Inconclusive("Verify the correctness of this test method.")
'    End Sub

'    '''<summary>
'    '''A test for EnumWindowsProc
'    '''</summary>
'    <Test(), _
'     DeploymentItem("APIControls.dll")> _
'    Public Sub EnumWindowsProcTest()
'        Dim handle As IntPtr = New IntPtr ' TODO: Initialize to an appropriate value
'        Dim parameter As Integer = 0 ' TODO: Initialize to an appropriate value
'        Dim expected As Boolean = False ' TODO: Initialize to an appropriate value
'        Dim actual As Boolean
'        actual = EnumerateWindows_Accessor.EnumWindowsProc(handle, parameter)
'        Verify.AreEqual(expected, actual)
'        Verify.Inconclusive("Verify the correctness of this test method.")
'    End Sub

'    '''<summary>
'    '''A test for Close
'    '''</summary>
'    <Test()> _
'    Public Sub CloseTest()
'        Dim title As String = String.Empty ' TODO: Initialize to an appropriate value
'        EnumerateWindows.Close(title)
'        Verify.Inconclusive("A method that does not return a value cannot be verified.")
'    End Sub

'    '''<summary>
'    '''A test for EnumerateWindows Constructor
'    '''</summary>
'    <Test()> _
'    Public Sub EnumerateWindowsConstructorTest()
'        Dim target As EnumerateWindows = New EnumerateWindows
'        Verify.Inconclusive("TODO: Implement code to verify target")
'    End Sub
'End Class
