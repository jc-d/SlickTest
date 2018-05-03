'Imports System

'Imports Microsoft.VisualStudio.TestTools.UnitTesting

'Imports APIControls



''''<summary>
''''This is a test class for ListBoxWindowsAPITest and is intended
''''to contain all ListBoxWindowsAPITest Unit Tests
''''</summary>
'<TestClass()> _
'Public Class ListBoxWindowsAPITest


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
'    '''A test for SelectListBoxItem
'    '''</summary>
'    <TestMethod()> _
'    Public Sub SelectListBoxItemTest()
'        Dim wf As New IndependentWindowsFunctionsv1()
'        Dim target As ListBoxWindowsAPI = New ListBoxWindowsAPI(wf) ' TODO: Initialize to an appropriate value
'        Dim hwnd As IntPtr = New IntPtr ' TODO: Initialize to an appropriate value
'        Dim item As Integer = 0 ' TODO: Initialize to an appropriate value
'        Dim expected As Boolean = False ' TODO: Initialize to an appropriate value
'        Dim actual As Boolean
'        actual = target.SelectListBoxItem(hwnd, item)
'        Assert.AreEqual(expected, actual)
'        Assert.Inconclusive("Verify the correctness of this test method.")
'    End Sub

'    '''<summary>
'    '''A test for ListBoxItemsCount
'    '''</summary>
'    <TestMethod()> _
'    Public Sub ListBoxItemsCountTest()
'        Dim wf As New IndependentWindowsFunctionsv1()
'        Dim target As ListBoxWindowsAPI = New ListBoxWindowsAPI(wf) ' TODO: Initialize to an appropriate value
'        Dim lhWnd As IntPtr = New IntPtr ' TODO: Initialize to an appropriate value
'        Dim expected As Integer = 0 ' TODO: Initialize to an appropriate value
'        Dim actual As Integer
'        actual = target.ListBoxItemsCount(lhWnd)
'        Assert.AreEqual(expected, actual)
'        Assert.Inconclusive("Verify the correctness of this test method.")
'    End Sub

'    '''<summary>
'    '''A test for ListBoxCount
'    '''</summary>
'    <TestMethod()> _
'    Public Sub ListBoxCountTest()
'        Dim wf As New IndependentWindowsFunctionsv1()
'        Dim target As ListBoxWindowsAPI = New ListBoxWindowsAPI(wf) ' TODO: Initialize to an appropriate value
'        Dim Hwnd As IntPtr = New IntPtr ' TODO: Initialize to an appropriate value
'        Dim expected As Integer = 0 ' TODO: Initialize to an appropriate value
'        Dim actual As Integer
'        actual = target.ListBoxCount(Hwnd)
'        Assert.AreEqual(expected, actual)
'        Assert.Inconclusive("Verify the correctness of this test method.")
'    End Sub

'    '''<summary>
'    '''A test for IsListBox
'    '''</summary>
'    <TestMethod()> _
'    Public Sub IsListBoxTest()
'        Dim wf As New IndependentWindowsFunctionsv1()
'        Dim target As ListBoxWindowsAPI = New ListBoxWindowsAPI(wf) ' TODO: Initialize to an appropriate value
'        Dim hWnd As IntPtr = New IntPtr ' TODO: Initialize to an appropriate value
'        Dim expected As Boolean = False ' TODO: Initialize to an appropriate value
'        Dim actual As Boolean
'        actual = target.IsListBox(hWnd)
'        Assert.AreEqual(expected, actual)
'        Assert.Inconclusive("Verify the correctness of this test method.")
'    End Sub

'    '''<summary>
'    '''A test for GetListBoxSelectedItems
'    '''</summary>
'    <TestMethod()> _
'    Public Sub GetListBoxSelectedItemsTest()
'        Dim wf As New IndependentWindowsFunctionsv1()
'        Dim target As ListBoxWindowsAPI = New ListBoxWindowsAPI(wf) ' TODO: Initialize to an appropriate value
'        Dim lhWnd As IntPtr = New IntPtr ' TODO: Initialize to an appropriate value
'        Dim expected() As Integer = Nothing ' TODO: Initialize to an appropriate value
'        Dim actual() As Integer
'        actual = target.GetListBoxSelectedItems(lhWnd)
'        Assert.AreEqual(expected, actual)
'        Assert.Inconclusive("Verify the correctness of this test method.")
'    End Sub

'    '''<summary>
'    '''A test for GetListBoxSelectedItem
'    '''</summary>
'    <TestMethod()> _
'    Public Sub GetListBoxSelectedItemTest()
'        Dim wf As New IndependentWindowsFunctionsv1()
'        Dim target As ListBoxWindowsAPI = New ListBoxWindowsAPI(wf) ' TODO: Initialize to an appropriate value
'        Dim hwnd As IntPtr = New IntPtr ' TODO: Initialize to an appropriate value
'        Dim expected As Integer = 0 ' TODO: Initialize to an appropriate value
'        Dim actual As Integer
'        actual = target.GetListBoxSelectedItem(hwnd)
'        Assert.AreEqual(expected, actual)
'        Assert.Inconclusive("Verify the correctness of this test method.")
'    End Sub

'    '''<summary>
'    '''A test for GetListBoxItems
'    '''</summary>
'    <TestMethod()> _
'    Public Sub GetListBoxItemsTest()
'        Dim wf As New IndependentWindowsFunctionsv1()
'        Dim target As ListBoxWindowsAPI = New ListBoxWindowsAPI(wf) ' TODO: Initialize to an appropriate value
'        Dim lhWnd As IntPtr = New IntPtr ' TODO: Initialize to an appropriate value
'        Dim expected() As String = Nothing ' TODO: Initialize to an appropriate value
'        Dim actual() As String
'        actual = target.GetListBoxItems(lhWnd)
'        Assert.AreEqual(expected, actual)
'        Assert.Inconclusive("Verify the correctness of this test method.")
'    End Sub

'    '''<summary>
'    '''A test for GetListBoxItem
'    '''</summary>
'    <TestMethod()> _
'    Public Sub GetListBoxItemTest()
'        Dim wf As New IndependentWindowsFunctionsv1()
'        Dim target As ListBoxWindowsAPI = New ListBoxWindowsAPI(wf) ' TODO: Initialize to an appropriate value
'        Dim index As Integer = 0 ' TODO: Initialize to an appropriate value
'        Dim hWnd As IntPtr = New IntPtr ' TODO: Initialize to an appropriate value
'        Dim expected As String = String.Empty ' TODO: Initialize to an appropriate value
'        Dim actual As String
'        actual = target.GetListBoxItem(index, hWnd)
'        Assert.AreEqual(expected, actual)
'        Assert.Inconclusive("Verify the correctness of this test method.")
'    End Sub

'    '''<summary>
'    '''A test for ListBoxWindowsAPI Constructor
'    '''</summary>
'    <TestMethod()> _
'    Public Sub ListBoxWindowsAPIConstructorTest()
'        Dim wf As New IndependentWindowsFunctionsv1()
'        Dim target As ListBoxWindowsAPI = New ListBoxWindowsAPI(wf)
'        Assert.Inconclusive("TODO: Implement code to verify target")
'    End Sub
'End Class
