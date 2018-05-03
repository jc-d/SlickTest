'Imports System.Collections.Generic

'Imports System

'Imports NUnit.Framework

'Imports APIControls



''''<summary>
''''This is a test class for TreeViewWindowsAPITest and is intended
''''to contain all TreeViewWindowsAPITest Unit Tests
''''</summary>
'<TestFixture()>  _
'Public Class TreeViewWindowsAPITest


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
'    '''A test for WriteProcessMemory
'    '''</summary>
'    <Test(), _
'     DeploymentItem("APIControls.dll")> _
'    Public Sub WriteProcessMemoryTest()
'        Dim hProcess As IntPtr = New IntPtr ' TODO: Initialize to an appropriate value
'        Dim lpBaseAddress As IntPtr = New IntPtr ' TODO: Initialize to an appropriate value
'        Dim buffer As TreeViewWindowsAPI_Accessor.TVITEM = Nothing ' TODO: Initialize to an appropriate value
'        Dim bufferExpected As TreeViewWindowsAPI_Accessor.TVITEM = Nothing ' TODO: Initialize to an appropriate value
'        Dim dwSize As Integer = 0 ' TODO: Initialize to an appropriate value
'        Dim lpNumberOfBytesWritten As IntPtr = New IntPtr ' TODO: Initialize to an appropriate value
'        Dim expected As Boolean = False ' TODO: Initialize to an appropriate value
'        Dim actual As Boolean
'        actual = TreeViewWindowsAPI_Accessor.WriteProcessMemory(hProcess, lpBaseAddress, buffer, dwSize, lpNumberOfBytesWritten)
'        Verify.AreEqual(bufferExpected, buffer)
'        Verify.AreEqual(expected, actual)
'        Verify.Inconclusive("Verify the correctness of this test method.")
'    End Sub

'    '''<summary>
'    '''A test for VirtualFreeEx
'    '''</summary>
'    <Test(), _
'     DeploymentItem("APIControls.dll")> _
'    Public Sub VirtualFreeExTest()
'        Dim hProcess As IntPtr = New IntPtr ' TODO: Initialize to an appropriate value
'        Dim lpAddress As IntPtr = New IntPtr ' TODO: Initialize to an appropriate value
'        Dim dwSize As Integer = 0 ' TODO: Initialize to an appropriate value
'        Dim dwFreeType As UInteger = 0 ' TODO: Initialize to an appropriate value
'        Dim expected As Boolean = False ' TODO: Initialize to an appropriate value
'        Dim actual As Boolean
'        actual = TreeViewWindowsAPI_Accessor.VirtualFreeEx(hProcess, lpAddress, dwSize, dwFreeType)
'        Verify.AreEqual(expected, actual)
'        Verify.Inconclusive("Verify the correctness of this test method.")
'    End Sub

'    '''<summary>
'    '''A test for VirtualAllocEx
'    '''</summary>
'    <Test(), _
'     DeploymentItem("APIControls.dll")> _
'    Public Sub VirtualAllocExTest()
'        Dim hProcess As IntPtr = New IntPtr ' TODO: Initialize to an appropriate value
'        Dim lpAddress As IntPtr = New IntPtr ' TODO: Initialize to an appropriate value
'        Dim dwSize As Integer = 0 ' TODO: Initialize to an appropriate value
'        Dim flAllocationType As UInteger = 0 ' TODO: Initialize to an appropriate value
'        Dim flProtect As UInteger = 0 ' TODO: Initialize to an appropriate value
'        Dim expected As IntPtr = New IntPtr ' TODO: Initialize to an appropriate value
'        Dim actual As IntPtr
'        actual = TreeViewWindowsAPI_Accessor.VirtualAllocEx(hProcess, lpAddress, dwSize, flAllocationType, flProtect)
'        Verify.AreEqual(expected, actual)
'        Verify.Inconclusive("Verify the correctness of this test method.")
'    End Sub

'    '''<summary>
'    '''A test for SendMessage
'    '''</summary>
'    <Test(), _
'     DeploymentItem("APIControls.dll")> _
'    Public Sub SendMessageTest()
'        Dim window As IntPtr = New IntPtr ' TODO: Initialize to an appropriate value
'        Dim message As Integer = 0 ' TODO: Initialize to an appropriate value
'        Dim wParam As IntPtr = New IntPtr ' TODO: Initialize to an appropriate value
'        Dim lParam As IntPtr = New IntPtr ' TODO: Initialize to an appropriate value
'        Dim expected As Integer = 0 ' TODO: Initialize to an appropriate value
'        Dim actual As Integer
'        actual = TreeViewWindowsAPI_Accessor.SendMessage(window, message, wParam, lParam)
'        Verify.AreEqual(expected, actual)
'        Verify.Inconclusive("Verify the correctness of this test method.")
'    End Sub

'    '''<summary>
'    '''A test for ReadTreeViewItem
'    '''</summary>
'    <Test(), _
'     DeploymentItem("APIControls.dll")> _
'    Public Sub ReadTreeViewItemTest()
'        Dim Hwnd As IntPtr = New IntPtr ' TODO: Initialize to an appropriate value
'        Dim Item As IntPtr = New IntPtr ' TODO: Initialize to an appropriate value
'        Dim expected As String = String.Empty ' TODO: Initialize to an appropriate value
'        Dim actual As String
'        actual = TreeViewWindowsAPI_Accessor.ReadTreeViewItem(Hwnd, Item)
'        Verify.AreEqual(expected, actual)
'        Verify.Inconclusive("Verify the correctness of this test method.")
'    End Sub

'    '''<summary>
'    '''A test for ReadProcessMemory
'    '''</summary>
'    <Test(), _
'     DeploymentItem("APIControls.dll")> _
'    Public Sub ReadProcessMemoryTest()
'        Dim hProcess As IntPtr = New IntPtr ' TODO: Initialize to an appropriate value
'        Dim lpBaseAddress As IntPtr = New IntPtr ' TODO: Initialize to an appropriate value
'        Dim lpBuffer As IntPtr = New IntPtr ' TODO: Initialize to an appropriate value
'        Dim dwSize As Integer = 0 ' TODO: Initialize to an appropriate value
'        Dim lpNumberOfBytesRead As IntPtr = New IntPtr ' TODO: Initialize to an appropriate value
'        Dim expected As Boolean = False ' TODO: Initialize to an appropriate value
'        Dim actual As Boolean
'        actual = TreeViewWindowsAPI_Accessor.ReadProcessMemory(hProcess, lpBaseAddress, lpBuffer, dwSize, lpNumberOfBytesRead)
'        Verify.AreEqual(expected, actual)
'        Verify.Inconclusive("Verify the correctness of this test method.")
'    End Sub

'    '''<summary>
'    '''A test for OpenProcess
'    '''</summary>
'    <Test(), _
'     DeploymentItem("APIControls.dll")> _
'    Public Sub OpenProcessTest()
'        Dim dwDesiredAccess As UInteger = 0 ' TODO: Initialize to an appropriate value
'        Dim bInheritHandle As Boolean = False ' TODO: Initialize to an appropriate value
'        Dim dwProcessId As Integer = 0 ' TODO: Initialize to an appropriate value
'        Dim expected As IntPtr = New IntPtr ' TODO: Initialize to an appropriate value
'        Dim actual As IntPtr
'        actual = TreeViewWindowsAPI_Accessor.OpenProcess(dwDesiredAccess, bInheritHandle, dwProcessId)
'        Verify.AreEqual(expected, actual)
'        Verify.Inconclusive("Verify the correctness of this test method.")
'    End Sub

'    '''<summary>
'    '''A test for IsTreeView
'    '''</summary>
'    <Test()> _
'    Public Sub IsTreeViewTest()
'        Dim wf As IndependentWindowsFunctionsv1 = Nothing ' TODO: Initialize to an appropriate value
'        Dim target As TreeViewWindowsAPI = New TreeViewWindowsAPI(wf) ' TODO: Initialize to an appropriate value
'        Dim hWnd As IntPtr = New IntPtr ' TODO: Initialize to an appropriate value
'        Dim expected As Boolean = False ' TODO: Initialize to an appropriate value
'        Dim actual As Boolean
'        actual = target.IsTreeView(hWnd)
'        Verify.AreEqual(expected, actual)
'        Verify.Inconclusive("Verify the correctness of this test method.")
'    End Sub

'    '''<summary>
'    '''A test for GetWindowThreadProcessId
'    '''</summary>
'    <Test(), _
'     DeploymentItem("APIControls.dll")> _
'    Public Sub GetWindowThreadProcessIdTest()
'        Dim hWnd As IntPtr = New IntPtr ' TODO: Initialize to an appropriate value
'        Dim lpwdProcessID As Integer = 0 ' TODO: Initialize to an appropriate value
'        Dim lpwdProcessIDExpected As Integer = 0 ' TODO: Initialize to an appropriate value
'        Dim expected As IntPtr = New IntPtr ' TODO: Initialize to an appropriate value
'        Dim actual As IntPtr
'        actual = TreeViewWindowsAPI_Accessor.GetWindowThreadProcessId(hWnd, lpwdProcessID)
'        Verify.AreEqual(lpwdProcessIDExpected, lpwdProcessID)
'        Verify.AreEqual(expected, actual)
'        Verify.Inconclusive("Verify the correctness of this test method.")
'    End Sub

'    '''<summary>
'    '''A test for GetText
'    '''</summary>
'    <Test()> _
'    Public Sub GetTextTest()
'        Dim wf As IndependentWindowsFunctionsv1 = Nothing ' TODO: Initialize to an appropriate value
'        Dim target As TreeViewWindowsAPI = New TreeViewWindowsAPI(wf) ' TODO: Initialize to an appropriate value
'        Dim Hwnd As IntPtr = New IntPtr ' TODO: Initialize to an appropriate value
'        Dim Item As IntPtr = New IntPtr ' TODO: Initialize to an appropriate value
'        Dim expected As String = String.Empty ' TODO: Initialize to an appropriate value
'        Dim actual As String
'        actual = target.GetText(Hwnd, Item)
'        Verify.AreEqual(expected, actual)
'        Verify.Inconclusive("Verify the correctness of this test method.")
'    End Sub

'    '''<summary>
'    '''A test for GetSelectedItemText
'    '''</summary>
'    <Test()> _
'    Public Sub GetSelectedItemTextTest()
'        Dim wf As IndependentWindowsFunctionsv1 = Nothing ' TODO: Initialize to an appropriate value
'        Dim target As TreeViewWindowsAPI = New TreeViewWindowsAPI(wf) ' TODO: Initialize to an appropriate value
'        Dim Hwnd As IntPtr = New IntPtr ' TODO: Initialize to an appropriate value
'        Dim expected As String = String.Empty ' TODO: Initialize to an appropriate value
'        Dim actual As String
'        actual = target.GetSelectedItemText(Hwnd)
'        Verify.AreEqual(expected, actual)
'        Verify.Inconclusive("Verify the correctness of this test method.")
'    End Sub

'    '''<summary>
'    '''A test for GetSelectedItemHwnd
'    '''</summary>
'    <Test(), _
'     DeploymentItem("APIControls.dll")> _
'    Public Sub GetSelectedItemHwndTest()
'        Dim param0 As PrivateObject = Nothing ' TODO: Initialize to an appropriate value
'        Dim target As TreeViewWindowsAPI_Accessor = New TreeViewWindowsAPI_Accessor(param0) ' TODO: Initialize to an appropriate value
'        Dim Hwnd As IntPtr = New IntPtr ' TODO: Initialize to an appropriate value
'        Dim expected As IntPtr = New IntPtr ' TODO: Initialize to an appropriate value
'        Dim actual As IntPtr
'        actual = target.GetSelectedItemHwnd(Hwnd)
'        Verify.AreEqual(expected, actual)
'        Verify.Inconclusive("Verify the correctness of this test method.")
'    End Sub

'    '''<summary>
'    '''A test for GetItemIndexByLikeText
'    '''</summary>
'    <Test()> _
'    Public Sub GetItemIndexByLikeTextTest()
'        Dim wf As IndependentWindowsFunctionsv1 = Nothing ' TODO: Initialize to an appropriate value
'        Dim target As TreeViewWindowsAPI = New TreeViewWindowsAPI(wf) ' TODO: Initialize to an appropriate value
'        Dim Hwnd As IntPtr = New IntPtr ' TODO: Initialize to an appropriate value
'        Dim ItemText As String = String.Empty ' TODO: Initialize to an appropriate value
'        Dim expected As Integer = 0 ' TODO: Initialize to an appropriate value
'        Dim actual As Integer
'        actual = target.GetItemIndexByLikeText(Hwnd, ItemText)
'        Verify.AreEqual(expected, actual)
'        Verify.Inconclusive("Verify the correctness of this test method.")
'    End Sub

'    '''<summary>
'    '''A test for GetItemIndexByExactText
'    '''</summary>
'    <Test()> _
'    Public Sub GetItemIndexByExactTextTest()
'        Dim wf As IndependentWindowsFunctionsv1 = Nothing ' TODO: Initialize to an appropriate value
'        Dim target As TreeViewWindowsAPI = New TreeViewWindowsAPI(wf) ' TODO: Initialize to an appropriate value
'        Dim Hwnd As IntPtr = New IntPtr ' TODO: Initialize to an appropriate value
'        Dim ItemText As String = String.Empty ' TODO: Initialize to an appropriate value
'        Dim expected As Integer = 0 ' TODO: Initialize to an appropriate value
'        Dim actual As Integer
'        actual = target.GetItemIndexByExactText(Hwnd, ItemText)
'        Verify.AreEqual(expected, actual)
'        Verify.Inconclusive("Verify the correctness of this test method.")
'    End Sub

'    '''<summary>
'    '''A test for GetItemHwndByText
'    '''</summary>
'    <Test(), _
'     DeploymentItem("APIControls.dll")> _
'    Public Sub GetItemHwndByTextTest()
'        Dim param0 As PrivateObject = Nothing ' TODO: Initialize to an appropriate value
'        Dim target As TreeViewWindowsAPI_Accessor = New TreeViewWindowsAPI_Accessor(param0) ' TODO: Initialize to an appropriate value
'        Dim Hwnd As IntPtr = New IntPtr ' TODO: Initialize to an appropriate value
'        Dim CurrentSearchItem As IntPtr = New IntPtr ' TODO: Initialize to an appropriate value
'        Dim ItemText As String = String.Empty ' TODO: Initialize to an appropriate value
'        Dim UseLike As Boolean = False ' TODO: Initialize to an appropriate value
'        Dim CurrentIndex As Integer = 0 ' TODO: Initialize to an appropriate value
'        Dim CurrentIndexExpected As Integer = 0 ' TODO: Initialize to an appropriate value
'        Dim expected As IntPtr = New IntPtr ' TODO: Initialize to an appropriate value
'        Dim actual As IntPtr
'        actual = target.GetItemHwndByText(Hwnd, CurrentSearchItem, ItemText, UseLike, CurrentIndex)
'        Verify.AreEqual(CurrentIndexExpected, CurrentIndex)
'        Verify.AreEqual(expected, actual)
'        Verify.Inconclusive("Verify the correctness of this test method.")
'    End Sub

'    '''<summary>
'    '''A test for GetItemHwndByLikeText
'    '''</summary>
'    <Test()> _
'    Public Sub GetItemHwndByLikeTextTest()
'        Dim wf As IndependentWindowsFunctionsv1 = Nothing ' TODO: Initialize to an appropriate value
'        Dim target As TreeViewWindowsAPI = New TreeViewWindowsAPI(wf) ' TODO: Initialize to an appropriate value
'        Dim Hwnd As IntPtr = New IntPtr ' TODO: Initialize to an appropriate value
'        Dim ItemText As String = String.Empty ' TODO: Initialize to an appropriate value
'        Dim expected As IntPtr = New IntPtr ' TODO: Initialize to an appropriate value
'        Dim actual As IntPtr
'        actual = target.GetItemHwndByLikeText(Hwnd, ItemText)
'        Verify.AreEqual(expected, actual)
'        Verify.Inconclusive("Verify the correctness of this test method.")
'    End Sub

'    '''<summary>
'    '''A test for GetItemHwndByIndex
'    '''</summary>
'    <Test()> _
'    Public Sub GetItemHwndByIndexTest1()
'        Dim wf As IndependentWindowsFunctionsv1 = Nothing ' TODO: Initialize to an appropriate value
'        Dim target As TreeViewWindowsAPI = New TreeViewWindowsAPI(wf) ' TODO: Initialize to an appropriate value
'        Dim Hwnd As IntPtr = New IntPtr ' TODO: Initialize to an appropriate value
'        Dim SearchItemIndex As Integer = 0 ' TODO: Initialize to an appropriate value
'        Dim expected As IntPtr = New IntPtr ' TODO: Initialize to an appropriate value
'        Dim actual As IntPtr
'        actual = target.GetItemHwndByIndex(Hwnd, SearchItemIndex)
'        Verify.AreEqual(expected, actual)
'        Verify.Inconclusive("Verify the correctness of this test method.")
'    End Sub

'    '''<summary>
'    '''A test for GetItemHwndByIndex
'    '''</summary>
'    <Test(), _
'     DeploymentItem("APIControls.dll")> _
'    Public Sub GetItemHwndByIndexTest()
'        Dim param0 As PrivateObject = Nothing ' TODO: Initialize to an appropriate value
'        Dim target As TreeViewWindowsAPI_Accessor = New TreeViewWindowsAPI_Accessor(param0) ' TODO: Initialize to an appropriate value
'        Dim Hwnd As IntPtr = New IntPtr ' TODO: Initialize to an appropriate value
'        Dim CurrentSearchItem As IntPtr = New IntPtr ' TODO: Initialize to an appropriate value
'        Dim SearchItemIndex As Integer = 0 ' TODO: Initialize to an appropriate value
'        Dim CurrentIndex As Integer = 0 ' TODO: Initialize to an appropriate value
'        Dim CurrentIndexExpected As Integer = 0 ' TODO: Initialize to an appropriate value
'        Dim expected As IntPtr = New IntPtr ' TODO: Initialize to an appropriate value
'        Dim actual As IntPtr
'        actual = target.GetItemHwndByIndex(Hwnd, CurrentSearchItem, SearchItemIndex, CurrentIndex)
'        Verify.AreEqual(CurrentIndexExpected, CurrentIndex)
'        Verify.AreEqual(expected, actual)
'        Verify.Inconclusive("Verify the correctness of this test method.")
'    End Sub

'    '''<summary>
'    '''A test for GetItemHwndByExactText
'    '''</summary>
'    <Test()> _
'    Public Sub GetItemHwndByExactTextTest()
'        Dim wf As IndependentWindowsFunctionsv1 = Nothing ' TODO: Initialize to an appropriate value
'        Dim target As TreeViewWindowsAPI = New TreeViewWindowsAPI(wf) ' TODO: Initialize to an appropriate value
'        Dim Hwnd As IntPtr = New IntPtr ' TODO: Initialize to an appropriate value
'        Dim ItemText As String = String.Empty ' TODO: Initialize to an appropriate value
'        Dim expected As IntPtr = New IntPtr ' TODO: Initialize to an appropriate value
'        Dim actual As IntPtr
'        actual = target.GetItemHwndByExactText(Hwnd, ItemText)
'        Verify.AreEqual(expected, actual)
'        Verify.Inconclusive("Verify the correctness of this test method.")
'    End Sub

'    '''<summary>
'    '''A test for GetItemCount
'    '''</summary>
'    <Test()> _
'    Public Sub GetItemCountTest()
'        Dim wf As IndependentWindowsFunctionsv1 = Nothing ' TODO: Initialize to an appropriate value
'        Dim target As TreeViewWindowsAPI = New TreeViewWindowsAPI(wf) ' TODO: Initialize to an appropriate value
'        Dim Hwnd As IntPtr = New IntPtr ' TODO: Initialize to an appropriate value
'        Dim expected As Integer = 0 ' TODO: Initialize to an appropriate value
'        Dim actual As Integer
'        actual = target.GetItemCount(Hwnd)
'        Verify.AreEqual(expected, actual)
'        Verify.Inconclusive("Verify the correctness of this test method.")
'    End Sub

'    '''<summary>
'    '''A test for GetAllItemsText
'    '''</summary>
'    <Test()> _
'    Public Sub GetAllItemsTextTest1()
'        Dim wf As IndependentWindowsFunctionsv1 = Nothing ' TODO: Initialize to an appropriate value
'        Dim target As TreeViewWindowsAPI = New TreeViewWindowsAPI(wf) ' TODO: Initialize to an appropriate value
'        Dim Hwnd As IntPtr = New IntPtr ' TODO: Initialize to an appropriate value
'        Dim expected As List(Of String) = Nothing ' TODO: Initialize to an appropriate value
'        Dim actual As List(Of String)
'        actual = target.GetAllItemsText(Hwnd)
'        Verify.AreEqual(expected, actual)
'        Verify.Inconclusive("Verify the correctness of this test method.")
'    End Sub

'    '''<summary>
'    '''A test for GetAllItemsText
'    '''</summary>
'    <Test(), _
'     DeploymentItem("APIControls.dll")> _
'    Public Sub GetAllItemsTextTest()
'        Dim param0 As PrivateObject = Nothing ' TODO: Initialize to an appropriate value
'        Dim target As TreeViewWindowsAPI_Accessor = New TreeViewWindowsAPI_Accessor(param0) ' TODO: Initialize to an appropriate value
'        Dim Hwnd As IntPtr = New IntPtr ' TODO: Initialize to an appropriate value
'        Dim CurrentSearchItem As IntPtr = New IntPtr ' TODO: Initialize to an appropriate value
'        Dim StrCol As List(Of String) = Nothing ' TODO: Initialize to an appropriate value
'        Dim StrColExpected As List(Of String) = Nothing ' TODO: Initialize to an appropriate value
'        target.GetAllItemsText(Hwnd, CurrentSearchItem, StrCol)
'        Verify.AreEqual(StrColExpected, StrCol)
'        Verify.Inconclusive("A method that does not return a value cannot be verified.")
'    End Sub

'    '''<summary>
'    '''A test for DoCompare
'    '''</summary>
'    <Test(), _
'     DeploymentItem("APIControls.dll")> _
'    Public Sub DoCompareTest()
'        Dim param0 As PrivateObject = Nothing ' TODO: Initialize to an appropriate value
'        Dim target As TreeViewWindowsAPI_Accessor = New TreeViewWindowsAPI_Accessor(param0) ' TODO: Initialize to an appropriate value
'        Dim Txt1 As String = String.Empty ' TODO: Initialize to an appropriate value
'        Dim Txt2 As String = String.Empty ' TODO: Initialize to an appropriate value
'        Dim UseLike As Boolean = False ' TODO: Initialize to an appropriate value
'        Dim expected As Boolean = False ' TODO: Initialize to an appropriate value
'        Dim actual As Boolean
'        actual = target.DoCompare(Txt1, Txt2, UseLike)
'        Verify.AreEqual(expected, actual)
'        Verify.Inconclusive("Verify the correctness of this test method.")
'    End Sub

'    '''<summary>
'    '''A test for CloseHandle
'    '''</summary>
'    <Test(), _
'     DeploymentItem("APIControls.dll")> _
'    Public Sub CloseHandleTest()
'        Dim hObject As IntPtr = New IntPtr ' TODO: Initialize to an appropriate value
'        Dim expected As Boolean = False ' TODO: Initialize to an appropriate value
'        Dim actual As Boolean
'        actual = TreeViewWindowsAPI_Accessor.CloseHandle(hObject)
'        Verify.AreEqual(expected, actual)
'        Verify.Inconclusive("Verify the correctness of this test method.")
'    End Sub

'    '''<summary>
'    '''A test for TreeViewWindowsAPI Constructor
'    '''</summary>
'    <Test()> _
'    Public Sub TreeViewWindowsAPIConstructorTest()
'        Dim wf As IndependentWindowsFunctionsv1 = Nothing ' TODO: Initialize to an appropriate value
'        Dim target As TreeViewWindowsAPI = New TreeViewWindowsAPI(wf)
'        Verify.Inconclusive("TODO: Implement code to verify target")
'    End Sub
'End Class
