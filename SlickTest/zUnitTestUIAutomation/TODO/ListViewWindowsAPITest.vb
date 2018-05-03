'Imports System

'Imports Microsoft.VisualStudio.TestTools.UnitTesting

'Imports APIControls



''''<summary>
''''This is a test class for ListViewWindowsAPITest and is intended
''''to contain all ListViewWindowsAPITest Unit Tests
''''</summary>
'<TestClass()> _
'Public Class ListViewWindowsAPITest


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
'    '''A test for WriteProcessMemory
'    '''</summary>
'    <TestMethod(), _
'     DeploymentItem("APIControls.dll")> _
'    Public Sub WriteProcessMemoryTest1()
'        Dim hProcess As IntPtr = New IntPtr ' TODO: Initialize to an appropriate value
'        Dim lpBaseAddress As IntPtr = New IntPtr ' TODO: Initialize to an appropriate value
'        Dim buffer As ListViewWindowsAPI_Accessor.LV_ITEM = Nothing ' TODO: Initialize to an appropriate value
'        Dim bufferExpected As ListViewWindowsAPI_Accessor.LV_ITEM = Nothing ' TODO: Initialize to an appropriate value
'        Dim dwSize As Integer = 0 ' TODO: Initialize to an appropriate value
'        Dim lpNumberOfBytesWritten As IntPtr = New IntPtr ' TODO: Initialize to an appropriate value
'        Dim expected As Boolean = False ' TODO: Initialize to an appropriate value
'        Dim actual As Boolean
'        actual = ListViewWindowsAPI_Accessor.WriteProcessMemory(hProcess, lpBaseAddress, buffer, dwSize, lpNumberOfBytesWritten)
'        Assert.AreEqual(bufferExpected, buffer)
'        Assert.AreEqual(expected, actual)
'        Assert.Inconclusive("Verify the correctness of this test method.")
'    End Sub

'    '''<summary>
'    '''A test for WriteProcessMemory
'    '''</summary>
'    <TestMethod(), _
'     DeploymentItem("APIControls.dll")> _
'    Public Sub WriteProcessMemoryTest()
'        Dim hProcess As IntPtr = New IntPtr ' TODO: Initialize to an appropriate value
'        Dim lpBaseAddress As IntPtr = New IntPtr ' TODO: Initialize to an appropriate value
'        Dim buffer As ListViewWindowsAPI_Accessor.HDITEM = Nothing ' TODO: Initialize to an appropriate value
'        Dim bufferExpected As ListViewWindowsAPI_Accessor.HDITEM = Nothing ' TODO: Initialize to an appropriate value
'        Dim dwSize As Integer = 0 ' TODO: Initialize to an appropriate value
'        Dim lpNumberOfBytesWritten As IntPtr = New IntPtr ' TODO: Initialize to an appropriate value
'        Dim expected As Boolean = False ' TODO: Initialize to an appropriate value
'        Dim actual As Boolean
'        actual = ListViewWindowsAPI_Accessor.WriteProcessMemory(hProcess, lpBaseAddress, buffer, dwSize, lpNumberOfBytesWritten)
'        Assert.AreEqual(bufferExpected, buffer)
'        Assert.AreEqual(expected, actual)
'        Assert.Inconclusive("Verify the correctness of this test method.")
'    End Sub

'    '''<summary>
'    '''A test for VirtualFreeEx
'    '''</summary>
'    <TestMethod(), _
'     DeploymentItem("APIControls.dll")> _
'    Public Sub VirtualFreeExTest()
'        Dim hProcess As IntPtr = New IntPtr ' TODO: Initialize to an appropriate value
'        Dim lpAddress As IntPtr = New IntPtr ' TODO: Initialize to an appropriate value
'        Dim dwSize As Integer = 0 ' TODO: Initialize to an appropriate value
'        Dim dwFreeType As UInteger = 0 ' TODO: Initialize to an appropriate value
'        Dim expected As Boolean = False ' TODO: Initialize to an appropriate value
'        Dim actual As Boolean
'        actual = ListViewWindowsAPI_Accessor.VirtualFreeEx(hProcess, lpAddress, dwSize, dwFreeType)
'        Assert.AreEqual(expected, actual)
'        Assert.Inconclusive("Verify the correctness of this test method.")
'    End Sub

'    '''<summary>
'    '''A test for VirtualAllocEx
'    '''</summary>
'    <TestMethod(), _
'     DeploymentItem("APIControls.dll")> _
'    Public Sub VirtualAllocExTest()
'        Dim hProcess As IntPtr = New IntPtr ' TODO: Initialize to an appropriate value
'        Dim lpAddress As IntPtr = New IntPtr ' TODO: Initialize to an appropriate value
'        Dim dwSize As Integer = 0 ' TODO: Initialize to an appropriate value
'        Dim flAllocationType As UInteger = 0 ' TODO: Initialize to an appropriate value
'        Dim flProtect As UInteger = 0 ' TODO: Initialize to an appropriate value
'        Dim expected As IntPtr = New IntPtr ' TODO: Initialize to an appropriate value
'        Dim actual As IntPtr
'        actual = ListViewWindowsAPI_Accessor.VirtualAllocEx(hProcess, lpAddress, dwSize, flAllocationType, flProtect)
'        Assert.AreEqual(expected, actual)
'        Assert.Inconclusive("Verify the correctness of this test method.")
'    End Sub

'    '''<summary>
'    '''A test for SetSelectedItems
'    '''</summary>
'    <TestMethod()> _
'    Public Sub SetSelectedItemsTest()
'        Dim wf As IndependentWindowsFunctionsv1 = Nothing ' TODO: Initialize to an appropriate value
'        Dim target As ListViewWindowsAPI = New ListViewWindowsAPI(wf) ' TODO: Initialize to an appropriate value
'        Dim hwnd As IntPtr = New IntPtr ' TODO: Initialize to an appropriate value
'        Dim Items() As Integer = Nothing ' TODO: Initialize to an appropriate value
'        target.SetSelectedItems(hwnd, Items)
'        Assert.Inconclusive("A method that does not return a value cannot be verified.")
'    End Sub

'    '''<summary>
'    '''A test for SetSelectedItem
'    '''</summary>
'    <TestMethod()> _
'    Public Sub SetSelectedItemTest()
'        Dim hWnd As IntPtr = New IntPtr ' TODO: Initialize to an appropriate value
'        Dim item As Integer = 0 ' TODO: Initialize to an appropriate value
'        Dim SelectItems As Boolean = False ' TODO: Initialize to an appropriate value
'        ListViewWindowsAPI.SetSelectedItem(hWnd, item, SelectItems)
'        Assert.Inconclusive("A method that does not return a value cannot be verified.")
'    End Sub

'    '''<summary>
'    '''A test for SetColumnWidth
'    '''</summary>
'    <TestMethod()> _
'    Public Sub SetColumnWidthTest()
'        Dim wf As IndependentWindowsFunctionsv1 = Nothing ' TODO: Initialize to an appropriate value
'        Dim target As ListViewWindowsAPI = New ListViewWindowsAPI(wf) ' TODO: Initialize to an appropriate value
'        Dim hwnd As IntPtr = New IntPtr ' TODO: Initialize to an appropriate value
'        Dim Column As Integer = 0 ' TODO: Initialize to an appropriate value
'        Dim ColumnSizeInPixels As Integer = 0 ' TODO: Initialize to an appropriate value
'        target.SetColumnWidth(hwnd, Column, ColumnSizeInPixels)
'        Assert.Inconclusive("A method that does not return a value cannot be verified.")
'    End Sub

'    '''<summary>
'    '''A test for SendMessageLVItem
'    '''</summary>
'    <TestMethod(), _
'     DeploymentItem("APIControls.dll")> _
'    Public Sub SendMessageLVItemTest()
'        Dim hWnd As IntPtr = New IntPtr ' TODO: Initialize to an appropriate value
'        Dim msg As Integer = 0 ' TODO: Initialize to an appropriate value
'        Dim wParam As Integer = 0 ' TODO: Initialize to an appropriate value
'        Dim lParam As ListViewWindowsAPI_Accessor.LV_ITEM = Nothing ' TODO: Initialize to an appropriate value
'        Dim lParamExpected As ListViewWindowsAPI_Accessor.LV_ITEM = Nothing ' TODO: Initialize to an appropriate value
'        Dim expected As Boolean = False ' TODO: Initialize to an appropriate value
'        Dim actual As Boolean
'        actual = ListViewWindowsAPI_Accessor.SendMessageLVItem(hWnd, msg, wParam, lParam)
'        Assert.AreEqual(lParamExpected, lParam)
'        Assert.AreEqual(expected, actual)
'        Assert.Inconclusive("Verify the correctness of this test method.")
'    End Sub

'    '''<summary>
'    '''A test for SendMessageHDItem
'    '''</summary>
'    <TestMethod(), _
'     DeploymentItem("APIControls.dll")> _
'    Public Sub SendMessageHDItemTest()
'        Dim hWnd As IntPtr = New IntPtr ' TODO: Initialize to an appropriate value
'        Dim msg As Integer = 0 ' TODO: Initialize to an appropriate value
'        Dim wParam As Integer = 0 ' TODO: Initialize to an appropriate value
'        Dim lParam As ListViewWindowsAPI_Accessor.HDITEM = Nothing ' TODO: Initialize to an appropriate value
'        Dim lParamExpected As ListViewWindowsAPI_Accessor.HDITEM = Nothing ' TODO: Initialize to an appropriate value
'        Dim expected As Boolean = False ' TODO: Initialize to an appropriate value
'        Dim actual As Boolean
'        actual = ListViewWindowsAPI_Accessor.SendMessageHDItem(hWnd, msg, wParam, lParam)
'        Assert.AreEqual(lParamExpected, lParam)
'        Assert.AreEqual(expected, actual)
'        Assert.Inconclusive("Verify the correctness of this test method.")
'    End Sub

'    '''<summary>
'    '''A test for SendMessage
'    '''</summary>
'    <TestMethod(), _
'     DeploymentItem("APIControls.dll")> _
'    Public Sub SendMessageTest()
'        Dim hWnd As IntPtr = New IntPtr ' TODO: Initialize to an appropriate value
'        Dim msg As Integer = 0 ' TODO: Initialize to an appropriate value
'        Dim wParam As Integer = 0 ' TODO: Initialize to an appropriate value
'        Dim lParam As IntPtr = New IntPtr ' TODO: Initialize to an appropriate value
'        Dim expected As Boolean = False ' TODO: Initialize to an appropriate value
'        Dim actual As Boolean
'        actual = ListViewWindowsAPI_Accessor.SendMessage(hWnd, msg, wParam, lParam)
'        Assert.AreEqual(expected, actual)
'        Assert.Inconclusive("Verify the correctness of this test method.")
'    End Sub

'    '''<summary>
'    '''A test for SelectAll
'    '''</summary>
'    <TestMethod()> _
'    Public Sub SelectAllTest()
'        Dim wf As IndependentWindowsFunctionsv1 = Nothing ' TODO: Initialize to an appropriate value
'        Dim target As ListViewWindowsAPI = New ListViewWindowsAPI(wf) ' TODO: Initialize to an appropriate value
'        Dim hwnd As IntPtr = New IntPtr ' TODO: Initialize to an appropriate value
'        target.SelectAll(hwnd)
'        Assert.Inconclusive("A method that does not return a value cannot be verified.")
'    End Sub

'    '''<summary>
'    '''A test for ReadProcessMemory
'    '''</summary>
'    <TestMethod(), _
'     DeploymentItem("APIControls.dll")> _
'    Public Sub ReadProcessMemoryTest()
'        Dim hProcess As IntPtr = New IntPtr ' TODO: Initialize to an appropriate value
'        Dim lpBaseAddress As IntPtr = New IntPtr ' TODO: Initialize to an appropriate value
'        Dim lpBuffer As IntPtr = New IntPtr ' TODO: Initialize to an appropriate value
'        Dim dwSize As Integer = 0 ' TODO: Initialize to an appropriate value
'        Dim lpNumberOfBytesRead As IntPtr = New IntPtr ' TODO: Initialize to an appropriate value
'        Dim expected As Boolean = False ' TODO: Initialize to an appropriate value
'        Dim actual As Boolean
'        actual = ListViewWindowsAPI_Accessor.ReadProcessMemory(hProcess, lpBaseAddress, lpBuffer, dwSize, lpNumberOfBytesRead)
'        Assert.AreEqual(expected, actual)
'        Assert.Inconclusive("Verify the correctness of this test method.")
'    End Sub

'    '''<summary>
'    '''A test for ReadListViewItem
'    '''</summary>
'    <TestMethod()> _
'    Public Sub ReadListViewItemTest()
'        Dim hWnd As IntPtr = New IntPtr ' TODO: Initialize to an appropriate value
'        Dim item As Integer = 0 ' TODO: Initialize to an appropriate value
'        Dim ColumnNumber As Integer = 0 ' TODO: Initialize to an appropriate value
'        Dim expected As String = String.Empty ' TODO: Initialize to an appropriate value
'        Dim actual As String
'        actual = ListViewWindowsAPI.ReadListViewItem(hWnd, item, ColumnNumber)
'        Assert.AreEqual(expected, actual)
'        Assert.Inconclusive("Verify the correctness of this test method.")
'    End Sub

'    '''<summary>
'    '''A test for ReadListViewHeader
'    '''</summary>
'    <TestMethod()> _
'    Public Sub ReadListViewHeaderTest()
'        Dim hWnd As IntPtr = New IntPtr ' TODO: Initialize to an appropriate value
'        Dim ColumnNumber As Integer = 0 ' TODO: Initialize to an appropriate value
'        Dim expected As String = String.Empty ' TODO: Initialize to an appropriate value
'        Dim actual As String
'        actual = ListViewWindowsAPI.ReadListViewHeader(hWnd, ColumnNumber)
'        Assert.AreEqual(expected, actual)
'        Assert.Inconclusive("Verify the correctness of this test method.")
'    End Sub

'    '''<summary>
'    '''A test for OpenProcess
'    '''</summary>
'    <TestMethod(), _
'     DeploymentItem("APIControls.dll")> _
'    Public Sub OpenProcessTest()
'        Dim dwDesiredAccess As UInteger = 0 ' TODO: Initialize to an appropriate value
'        Dim bInheritHandle As Boolean = False ' TODO: Initialize to an appropriate value
'        Dim dwProcessId As Integer = 0 ' TODO: Initialize to an appropriate value
'        Dim expected As IntPtr = New IntPtr ' TODO: Initialize to an appropriate value
'        Dim actual As IntPtr
'        actual = ListViewWindowsAPI_Accessor.OpenProcess(dwDesiredAccess, bInheritHandle, dwProcessId)
'        Assert.AreEqual(expected, actual)
'        Assert.Inconclusive("Verify the correctness of this test method.")
'    End Sub

'    '''<summary>
'    '''A test for IsListView
'    '''</summary>
'    <TestMethod()> _
'    Public Sub IsListViewTest()
'        Dim wf As IndependentWindowsFunctionsv1 = Nothing ' TODO: Initialize to an appropriate value
'        Dim target As ListViewWindowsAPI = New ListViewWindowsAPI(wf) ' TODO: Initialize to an appropriate value
'        Dim hWnd As IntPtr = New IntPtr ' TODO: Initialize to an appropriate value
'        Dim expected As Boolean = False ' TODO: Initialize to an appropriate value
'        Dim actual As Boolean
'        actual = target.IsListView(hWnd)
'        Assert.AreEqual(expected, actual)
'        Assert.Inconclusive("Verify the correctness of this test method.")
'    End Sub

'    '''<summary>
'    '''A test for GetWindowThreadProcessId
'    '''</summary>
'    <TestMethod(), _
'     DeploymentItem("APIControls.dll")> _
'    Public Sub GetWindowThreadProcessIdTest()
'        Dim hWnd As IntPtr = New IntPtr ' TODO: Initialize to an appropriate value
'        Dim lpwdProcessID As Integer = 0 ' TODO: Initialize to an appropriate value
'        Dim lpwdProcessIDExpected As Integer = 0 ' TODO: Initialize to an appropriate value
'        Dim expected As IntPtr = New IntPtr ' TODO: Initialize to an appropriate value
'        Dim actual As IntPtr
'        actual = ListViewWindowsAPI_Accessor.GetWindowThreadProcessId(hWnd, lpwdProcessID)
'        Assert.AreEqual(lpwdProcessIDExpected, lpwdProcessID)
'        Assert.AreEqual(expected, actual)
'        Assert.Inconclusive("Verify the correctness of this test method.")
'    End Sub

'    '''<summary>
'    '''A test for GetSelectedItems
'    '''</summary>
'    <TestMethod()> _
'    Public Sub GetSelectedItemsTest()
'        Dim wf As IndependentWindowsFunctionsv1 = Nothing ' TODO: Initialize to an appropriate value
'        Dim target As ListViewWindowsAPI = New ListViewWindowsAPI(wf) ' TODO: Initialize to an appropriate value
'        Dim hwnd As IntPtr = New IntPtr ' TODO: Initialize to an appropriate value
'        Dim expected() As Integer = Nothing ' TODO: Initialize to an appropriate value
'        Dim actual() As Integer
'        actual = target.GetSelectedItems(hwnd)
'        Assert.AreEqual(expected, actual)
'        Assert.Inconclusive("Verify the correctness of this test method.")
'    End Sub

'    '''<summary>
'    '''A test for GetSelectedItemCount
'    '''</summary>
'    <TestMethod()> _
'    Public Sub GetSelectedItemCountTest()
'        Dim wf As IndependentWindowsFunctionsv1 = Nothing ' TODO: Initialize to an appropriate value
'        Dim target As ListViewWindowsAPI = New ListViewWindowsAPI(wf) ' TODO: Initialize to an appropriate value
'        Dim hwnd As IntPtr = New IntPtr ' TODO: Initialize to an appropriate value
'        Dim expected As Integer = 0 ' TODO: Initialize to an appropriate value
'        Dim actual As Integer
'        actual = target.GetSelectedItemCount(hwnd)
'        Assert.AreEqual(expected, actual)
'        Assert.Inconclusive("Verify the correctness of this test method.")
'    End Sub

'    '''<summary>
'    '''A test for GetRow
'    '''</summary>
'    <TestMethod()> _
'    Public Sub GetRowTest()
'        Dim wf As IndependentWindowsFunctionsv1 = Nothing ' TODO: Initialize to an appropriate value
'        Dim target As ListViewWindowsAPI = New ListViewWindowsAPI(wf) ' TODO: Initialize to an appropriate value
'        Dim hwnd As IntPtr = New IntPtr ' TODO: Initialize to an appropriate value
'        Dim Row As Integer = 0 ' TODO: Initialize to an appropriate value
'        Dim expected() As String = Nothing ' TODO: Initialize to an appropriate value
'        Dim actual() As String
'        actual = target.GetRow(hwnd, Row)
'        Assert.AreEqual(expected, actual)
'        Assert.Inconclusive("Verify the correctness of this test method.")
'    End Sub

'    '''<summary>
'    '''A test for GetItemCount
'    '''</summary>
'    <TestMethod()> _
'    Public Sub GetItemCountTest()
'        Dim wf As IndependentWindowsFunctionsv1 = Nothing ' TODO: Initialize to an appropriate value
'        Dim target As ListViewWindowsAPI = New ListViewWindowsAPI(wf) ' TODO: Initialize to an appropriate value
'        Dim hwnd As IntPtr = New IntPtr ' TODO: Initialize to an appropriate value
'        Dim expected As Integer = 0 ' TODO: Initialize to an appropriate value
'        Dim actual As Integer
'        actual = target.GetItemCount(hwnd)
'        Assert.AreEqual(expected, actual)
'        Assert.Inconclusive("Verify the correctness of this test method.")
'    End Sub

'    '''<summary>
'    '''A test for GetEntireListForPrinting
'    '''</summary>
'    <TestMethod()> _
'    Public Sub GetEntireListForPrintingTest()
'        Dim wf As IndependentWindowsFunctionsv1 = Nothing ' TODO: Initialize to an appropriate value
'        Dim target As ListViewWindowsAPI = New ListViewWindowsAPI(wf) ' TODO: Initialize to an appropriate value
'        Dim hwnd As IntPtr = New IntPtr ' TODO: Initialize to an appropriate value
'        Dim expected As String = String.Empty ' TODO: Initialize to an appropriate value
'        Dim actual As String
'        actual = target.GetEntireListForPrinting(hwnd)
'        Assert.AreEqual(expected, actual)
'        Assert.Inconclusive("Verify the correctness of this test method.")
'    End Sub

'    '''<summary>
'    '''A test for GetEntireList
'    '''</summary>
'    <TestMethod()> _
'    Public Sub GetEntireListTest()
'        Dim wf As IndependentWindowsFunctionsv1 = Nothing ' TODO: Initialize to an appropriate value
'        Dim target As ListViewWindowsAPI = New ListViewWindowsAPI(wf) ' TODO: Initialize to an appropriate value
'        Dim hwnd As IntPtr = New IntPtr ' TODO: Initialize to an appropriate value
'        Dim expected(,) As String = Nothing ' TODO: Initialize to an appropriate value
'        Dim actual(,) As String
'        actual = target.GetEntireList(hwnd)
'        Assert.AreEqual(expected, actual)
'        Assert.Inconclusive("Verify the correctness of this test method.")
'    End Sub

'    '''<summary>
'    '''A test for GetColumnWidth
'    '''</summary>
'    <TestMethod()> _
'    Public Sub GetColumnWidthTest()
'        Dim wf As IndependentWindowsFunctionsv1 = Nothing ' TODO: Initialize to an appropriate value
'        Dim target As ListViewWindowsAPI = New ListViewWindowsAPI(wf) ' TODO: Initialize to an appropriate value
'        Dim hwnd As IntPtr = New IntPtr ' TODO: Initialize to an appropriate value
'        Dim Column As Integer = 0 ' TODO: Initialize to an appropriate value
'        Dim expected As Integer = 0 ' TODO: Initialize to an appropriate value
'        Dim actual As Integer
'        actual = target.GetColumnWidth(hwnd, Column)
'        Assert.AreEqual(expected, actual)
'        Assert.Inconclusive("Verify the correctness of this test method.")
'    End Sub

'    '''<summary>
'    '''A test for GetColumns
'    '''</summary>
'    <TestMethod()> _
'    Public Sub GetColumnsTest()
'        Dim wf As IndependentWindowsFunctionsv1 = Nothing ' TODO: Initialize to an appropriate value
'        Dim target As ListViewWindowsAPI = New ListViewWindowsAPI(wf) ' TODO: Initialize to an appropriate value
'        Dim hwnd As IntPtr = New IntPtr ' TODO: Initialize to an appropriate value
'        Dim expected() As String = Nothing ' TODO: Initialize to an appropriate value
'        Dim actual() As String
'        actual = target.GetColumns(hwnd)
'        Assert.AreEqual(expected, actual)
'        Assert.Inconclusive("Verify the correctness of this test method.")
'    End Sub

'    '''<summary>
'    '''A test for GetColumnName
'    '''</summary>
'    <TestMethod()> _
'    Public Sub GetColumnNameTest()
'        Dim wf As IndependentWindowsFunctionsv1 = Nothing ' TODO: Initialize to an appropriate value
'        Dim target As ListViewWindowsAPI = New ListViewWindowsAPI(wf) ' TODO: Initialize to an appropriate value
'        Dim hwnd As IntPtr = New IntPtr ' TODO: Initialize to an appropriate value
'        Dim Column As Integer = 0 ' TODO: Initialize to an appropriate value
'        Dim expected As String = String.Empty ' TODO: Initialize to an appropriate value
'        Dim actual As String
'        actual = target.GetColumnName(hwnd, Column)
'        Assert.AreEqual(expected, actual)
'        Assert.Inconclusive("Verify the correctness of this test method.")
'    End Sub

'    '''<summary>
'    '''A test for GetColumnCount
'    '''</summary>
'    <TestMethod()> _
'    Public Sub GetColumnCountTest()
'        Dim wf As IndependentWindowsFunctionsv1 = Nothing ' TODO: Initialize to an appropriate value
'        Dim target As ListViewWindowsAPI = New ListViewWindowsAPI(wf) ' TODO: Initialize to an appropriate value
'        Dim hwnd As IntPtr = New IntPtr ' TODO: Initialize to an appropriate value
'        Dim expected As Integer = 0 ' TODO: Initialize to an appropriate value
'        Dim actual As Integer
'        actual = target.GetColumnCount(hwnd)
'        Assert.AreEqual(expected, actual)
'        Assert.Inconclusive("Verify the correctness of this test method.")
'    End Sub

'    '''<summary>
'    '''A test for FindRowNumber
'    '''</summary>
'    <TestMethod()> _
'    Public Sub FindRowNumberTest()
'        Dim wf As IndependentWindowsFunctionsv1 = Nothing ' TODO: Initialize to an appropriate value
'        Dim target As ListViewWindowsAPI = New ListViewWindowsAPI(wf) ' TODO: Initialize to an appropriate value
'        Dim hwnd As IntPtr = New IntPtr ' TODO: Initialize to an appropriate value
'        Dim RowTextWildCard As String = String.Empty ' TODO: Initialize to an appropriate value
'        Dim expected As Integer = 0 ' TODO: Initialize to an appropriate value
'        Dim actual As Integer
'        actual = target.FindRowNumber(hwnd, RowTextWildCard)
'        Assert.AreEqual(expected, actual)
'        Assert.Inconclusive("Verify the correctness of this test method.")
'    End Sub

'    '''<summary>
'    '''A test for FindColumnNumber
'    '''</summary>
'    <TestMethod()> _
'    Public Sub FindColumnNumberTest()
'        Dim wf As IndependentWindowsFunctionsv1 = Nothing ' TODO: Initialize to an appropriate value
'        Dim target As ListViewWindowsAPI = New ListViewWindowsAPI(wf) ' TODO: Initialize to an appropriate value
'        Dim hwnd As IntPtr = New IntPtr ' TODO: Initialize to an appropriate value
'        Dim ColumnNameWildCard As String = String.Empty ' TODO: Initialize to an appropriate value
'        Dim expected As Integer = 0 ' TODO: Initialize to an appropriate value
'        Dim actual As Integer
'        actual = target.FindColumnNumber(hwnd, ColumnNameWildCard)
'        Assert.AreEqual(expected, actual)
'        Assert.Inconclusive("Verify the correctness of this test method.")
'    End Sub

'    '''<summary>
'    '''A test for CloseHandle
'    '''</summary>
'    <TestMethod(), _
'     DeploymentItem("APIControls.dll")> _
'    Public Sub CloseHandleTest()
'        Dim hObject As IntPtr = New IntPtr ' TODO: Initialize to an appropriate value
'        Dim expected As Boolean = False ' TODO: Initialize to an appropriate value
'        Dim actual As Boolean
'        actual = ListViewWindowsAPI_Accessor.CloseHandle(hObject)
'        Assert.AreEqual(expected, actual)
'        Assert.Inconclusive("Verify the correctness of this test method.")
'    End Sub

'    '''<summary>
'    '''A test for ListViewWindowsAPI Constructor
'    '''</summary>
'    <TestMethod()> _
'    Public Sub ListViewWindowsAPIConstructorTest()
'        Dim wf As IndependentWindowsFunctionsv1 = Nothing ' TODO: Initialize to an appropriate value
'        Dim target As ListViewWindowsAPI = New ListViewWindowsAPI(wf)
'        Assert.Inconclusive("TODO: Implement code to verify target")
'    End Sub
'End Class
