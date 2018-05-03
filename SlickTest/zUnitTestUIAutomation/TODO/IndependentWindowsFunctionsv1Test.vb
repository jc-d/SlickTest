'Imports System.Drawing

'Imports System

'Imports Microsoft.VisualStudio.TestTools.UnitTesting

'Imports APIControls



''''<summary>
''''This is a test class for IndependentWindowsFunctionsv1Test and is intended
''''to contain all IndependentWindowsFunctionsv1Test Unit Tests
''''</summary>
'<TestClass()> _
'Public Class IndependentWindowsFunctionsv1Test


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
'    '''A test for SetText
'    '''</summary>
'    <TestMethod()> _
'    Public Sub SetTextTest()
'        Dim target As IndependentWindowsFunctionsv1 = New IndependentWindowsFunctionsv1 ' TODO: Initialize to an appropriate value
'        Dim text As String = String.Empty ' TODO: Initialize to an appropriate value
'        Dim hwnd As IntPtr = New IntPtr ' TODO: Initialize to an appropriate value
'        target.SetText(text, hwnd)
'        Assert.Inconclusive("A method that does not return a value cannot be verified.")
'    End Sub

'    '''<summary>
'    '''A test for SetCheckBoxState
'    '''</summary>
'    <TestMethod()> _
'    Public Sub SetCheckBoxStateTest()
'        Dim target As IndependentWindowsFunctionsv1 = New IndependentWindowsFunctionsv1 ' TODO: Initialize to an appropriate value
'        Dim Hwnd As IntPtr = New IntPtr ' TODO: Initialize to an appropriate value
'        Dim state As Integer = 0 ' TODO: Initialize to an appropriate value
'        Dim expected As Integer = 0 ' TODO: Initialize to an appropriate value
'        Dim actual As Integer
'        actual = target.SetCheckBoxState(Hwnd, state)
'        Assert.AreEqual(expected, actual)
'        Assert.Inconclusive("Verify the correctness of this test method.")
'    End Sub

'    '''<summary>
'    '''A test for SendTextByHandler
'    '''</summary>
'    <TestMethod()> _
'    Public Sub SendTextByHandlerTest()
'        Dim target As IndependentWindowsFunctionsv1 = New IndependentWindowsFunctionsv1 ' TODO: Initialize to an appropriate value
'        Dim text As String = String.Empty ' TODO: Initialize to an appropriate value
'        Dim hwnd As IntPtr = New IntPtr ' TODO: Initialize to an appropriate value
'        target.SendTextByHandler(text, hwnd)
'        Assert.Inconclusive("A method that does not return a value cannot be verified.")
'    End Sub

'    '''<summary>
'    '''A test for SearchForObjInApp
'    '''</summary>
'    <TestMethod()> _
'    Public Sub SearchForObjInAppTest3()
'        Dim target As IndependentWindowsFunctionsv1 = New IndependentWindowsFunctionsv1 ' TODO: Initialize to an appropriate value
'        Dim FirstPointer As IntPtr = New IntPtr ' TODO: Initialize to an appropriate value
'        Dim ClassName As String = String.Empty ' TODO: Initialize to an appropriate value
'        Dim Value As String = String.Empty ' TODO: Initialize to an appropriate value
'        Dim expected As IntPtr = New IntPtr ' TODO: Initialize to an appropriate value
'        Dim actual As IntPtr
'        actual = target.SearchForObjInApp(FirstPointer, ClassName, Value)
'        Assert.AreEqual(expected, actual)
'        Assert.Inconclusive("Verify the correctness of this test method.")
'    End Sub

'    '''<summary>
'    '''A test for SearchForObjInApp
'    '''</summary>
'    <TestMethod()> _
'    Public Sub SearchForObjInAppTest2()
'        Dim target As IndependentWindowsFunctionsv1 = New IndependentWindowsFunctionsv1 ' TODO: Initialize to an appropriate value
'        Dim AppTitle As String = String.Empty ' TODO: Initialize to an appropriate value
'        Dim ClassName As String = String.Empty ' TODO: Initialize to an appropriate value
'        Dim Value As String = String.Empty ' TODO: Initialize to an appropriate value
'        Dim expected As IntPtr = New IntPtr ' TODO: Initialize to an appropriate value
'        Dim actual As IntPtr
'        actual = target.SearchForObjInApp(AppTitle, ClassName, Value)
'        Assert.AreEqual(expected, actual)
'        Assert.Inconclusive("Verify the correctness of this test method.")
'    End Sub

'    '''<summary>
'    '''A test for SearchForObjInApp
'    '''</summary>
'    <TestMethod()> _
'    Public Sub SearchForObjInAppTest1()
'        Dim target As IndependentWindowsFunctionsv1 = New IndependentWindowsFunctionsv1 ' TODO: Initialize to an appropriate value
'        Dim AppTitle As String = String.Empty ' TODO: Initialize to an appropriate value
'        Dim hwnd As IntPtr = New IntPtr ' TODO: Initialize to an appropriate value
'        Dim ClassName As String = String.Empty ' TODO: Initialize to an appropriate value
'        Dim expected As IntPtr = New IntPtr ' TODO: Initialize to an appropriate value
'        Dim actual As IntPtr
'        actual = target.SearchForObjInApp(AppTitle, hwnd, ClassName)
'        Assert.AreEqual(expected, actual)
'        Assert.Inconclusive("Verify the correctness of this test method.")
'    End Sub

'    '''<summary>
'    '''A test for SearchForObjInApp
'    '''</summary>
'    <TestMethod()> _
'    Public Sub SearchForObjInAppTest()
'        Dim target As IndependentWindowsFunctionsv1 = New IndependentWindowsFunctionsv1 ' TODO: Initialize to an appropriate value
'        Dim FirstPointer As IntPtr = New IntPtr ' TODO: Initialize to an appropriate value
'        Dim NextPointer As IntPtr = New IntPtr ' TODO: Initialize to an appropriate value
'        Dim ClassName As String = String.Empty ' TODO: Initialize to an appropriate value
'        Dim Value As String = String.Empty ' TODO: Initialize to an appropriate value
'        Dim expected As IntPtr = New IntPtr ' TODO: Initialize to an appropriate value
'        Dim actual As IntPtr
'        actual = target.SearchForObjInApp(FirstPointer, NextPointer, ClassName, Value)
'        Assert.AreEqual(expected, actual)
'        Assert.Inconclusive("Verify the correctness of this test method.")
'    End Sub

'    '''<summary>
'    '''A test for SearchForObj
'    '''</summary>
'    <TestMethod()> _
'    Public Sub SearchForObjTest()
'        Dim target As IndependentWindowsFunctionsv1 = New IndependentWindowsFunctionsv1 ' TODO: Initialize to an appropriate value
'        Dim desc As Description = Nothing ' TODO: Initialize to an appropriate value
'        Dim hwnd As IntPtr = New IntPtr ' TODO: Initialize to an appropriate value
'        Dim expected As IntPtr = New IntPtr ' TODO: Initialize to an appropriate value
'        Dim actual As IntPtr
'        actual = target.SearchForObj(desc, hwnd)
'        Assert.AreEqual(expected, actual)
'        Assert.Inconclusive("Verify the correctness of this test method.")
'    End Sub

'    '''<summary>
'    '''A test for SearchForApp
'    '''</summary>
'    <TestMethod()> _
'    Public Sub SearchForAppTest()
'        Dim target As IndependentWindowsFunctionsv1 = New IndependentWindowsFunctionsv1 ' TODO: Initialize to an appropriate value
'        Dim AppTitle As String = String.Empty ' TODO: Initialize to an appropriate value
'        Dim AppClassName As String = String.Empty ' TODO: Initialize to an appropriate value
'        Dim expected As IntPtr = New IntPtr ' TODO: Initialize to an appropriate value
'        Dim actual As IntPtr
'        actual = target.SearchForApp(AppTitle, AppClassName)
'        Assert.AreEqual(expected, actual)
'        Assert.Inconclusive("Verify the correctness of this test method.")
'    End Sub

'    '''<summary>
'    '''A test for IsWebPartIEHTML
'    '''</summary>
'    <TestMethod()> _
'    Public Sub IsWebPartIEHTMLTest()
'        Dim target As IndependentWindowsFunctionsv1 = New IndependentWindowsFunctionsv1 ' TODO: Initialize to an appropriate value
'        Dim Hwnd As IntPtr = New IntPtr ' TODO: Initialize to an appropriate value
'        Dim HwndExpected As IntPtr = New IntPtr ' TODO: Initialize to an appropriate value
'        Dim expected As Boolean = False ' TODO: Initialize to an appropriate value
'        Dim actual As Boolean
'        actual = target.IsWebPartIEHTML(Hwnd)
'        Assert.AreEqual(HwndExpected, Hwnd)
'        Assert.AreEqual(expected, actual)
'        Assert.Inconclusive("Verify the correctness of this test method.")
'    End Sub

'    '''<summary>
'    '''A test for IsWebPartIE
'    '''</summary>
'    <TestMethod()> _
'    Public Sub IsWebPartIETest()
'        Dim target As IndependentWindowsFunctionsv1 = New IndependentWindowsFunctionsv1 ' TODO: Initialize to an appropriate value
'        Dim Hwnd As IntPtr = New IntPtr ' TODO: Initialize to an appropriate value
'        Dim HwndExpected As IntPtr = New IntPtr ' TODO: Initialize to an appropriate value
'        Dim expected As Boolean = False ' TODO: Initialize to an appropriate value
'        Dim actual As Boolean
'        actual = target.IsWebPartIE(Hwnd)
'        Assert.AreEqual(HwndExpected, Hwnd)
'        Assert.AreEqual(expected, actual)
'        Assert.Inconclusive("Verify the correctness of this test method.")
'    End Sub

'    '''<summary>
'    '''A test for IsWebPartExactlyIE
'    '''</summary>
'    <TestMethod()> _
'    Public Sub IsWebPartExactlyIETest()
'        Dim target As IndependentWindowsFunctionsv1 = New IndependentWindowsFunctionsv1 ' TODO: Initialize to an appropriate value
'        Dim Hwnd As IntPtr = New IntPtr ' TODO: Initialize to an appropriate value
'        Dim expected As Boolean = False ' TODO: Initialize to an appropriate value
'        Dim actual As Boolean
'        actual = target.IsWebPartExactlyIE(Hwnd)
'        Assert.AreEqual(expected, actual)
'        Assert.Inconclusive("Verify the correctness of this test method.")
'    End Sub

'    '''<summary>
'    '''A test for IsEnabled
'    '''</summary>
'    <TestMethod()> _
'    Public Sub IsEnabledTest()
'        Dim target As IndependentWindowsFunctionsv1 = New IndependentWindowsFunctionsv1 ' TODO: Initialize to an appropriate value
'        Dim Hwnd As IntPtr = New IntPtr ' TODO: Initialize to an appropriate value
'        Dim expected As Boolean = False ' TODO: Initialize to an appropriate value
'        Dim actual As Boolean
'        actual = target.IsEnabled(Hwnd)
'        Assert.AreEqual(expected, actual)
'        Assert.Inconclusive("Verify the correctness of this test method.")
'    End Sub

'    '''<summary>
'    '''A test for IsAParent
'    '''</summary>
'    <TestMethod(), _
'     DeploymentItem("APIControls.dll")> _
'    Public Sub IsAParentTest()
'        Dim target As IndependentWindowsFunctionsv1_Accessor = New IndependentWindowsFunctionsv1_Accessor ' TODO: Initialize to an appropriate value
'        Dim child As IntPtr = New IntPtr ' TODO: Initialize to an appropriate value
'        Dim parent As IntPtr = New IntPtr ' TODO: Initialize to an appropriate value
'        Dim expected As Boolean = False ' TODO: Initialize to an appropriate value
'        Dim actual As Boolean
'        actual = target.IsAParent(child, parent)
'        Assert.AreEqual(expected, actual)
'        Assert.Inconclusive("Verify the correctness of this test method.")
'    End Sub

'    '''<summary>
'    '''A test for GetWindowsStyle
'    '''</summary>
'    <TestMethod()> _
'    Public Sub GetWindowsStyleTest()
'        Dim target As IndependentWindowsFunctionsv1 = New IndependentWindowsFunctionsv1 ' TODO: Initialize to an appropriate value
'        Dim hwnd As IntPtr = New IntPtr ' TODO: Initialize to an appropriate value
'        Dim expected As Integer = 0 ' TODO: Initialize to an appropriate value
'        Dim actual As Integer
'        actual = target.GetWindowsStyle(hwnd)
'        Assert.AreEqual(expected, actual)
'        Assert.Inconclusive("Verify the correctness of this test method.")
'    End Sub

'    '''<summary>
'    '''A test for GetWindowsID
'    '''</summary>
'    <TestMethod()> _
'    Public Sub GetWindowsIDTest()
'        Dim target As IndependentWindowsFunctionsv1 = New IndependentWindowsFunctionsv1 ' TODO: Initialize to an appropriate value
'        Dim hwnd As IntPtr = New IntPtr ' TODO: Initialize to an appropriate value
'        Dim expected As Integer = 0 ' TODO: Initialize to an appropriate value
'        Dim actual As Integer
'        actual = target.GetWindowsID(hwnd)
'        Assert.AreEqual(expected, actual)
'        Assert.Inconclusive("Verify the correctness of this test method.")
'    End Sub

'    '''<summary>
'    '''A test for GetWindowRect
'    '''</summary>
'    <TestMethod()> _
'    Public Sub GetWindowRectTest()
'        Dim hWnd As Integer = 0 ' TODO: Initialize to an appropriate value
'        Dim lpRect As IndependentWindowsFunctionsv1.RECT = New IndependentWindowsFunctionsv1.RECT ' TODO: Initialize to an appropriate value
'        Dim lpRectExpected As IndependentWindowsFunctionsv1.RECT = New IndependentWindowsFunctionsv1.RECT ' TODO: Initialize to an appropriate value
'        Dim expected As Integer = 0 ' TODO: Initialize to an appropriate value
'        Dim actual As Integer
'        actual = IndependentWindowsFunctionsv1.GetWindowRect(hWnd, lpRect)
'        Assert.AreEqual(lpRectExpected, lpRect)
'        Assert.AreEqual(expected, actual)
'        Assert.Inconclusive("Verify the correctness of this test method.")
'    End Sub

'    '''<summary>
'    '''A test for GetTopParent
'    '''</summary>
'    <TestMethod()> _
'    Public Sub GetTopParentTest()
'        Dim target As IndependentWindowsFunctionsv1 = New IndependentWindowsFunctionsv1 ' TODO: Initialize to an appropriate value
'        Dim hChild As IntPtr = New IntPtr ' TODO: Initialize to an appropriate value
'        Dim expected As IntPtr = New IntPtr ' TODO: Initialize to an appropriate value
'        Dim actual As IntPtr
'        actual = target.GetTopParent(hChild)
'        Assert.AreEqual(expected, actual)
'        Assert.Inconclusive("Verify the correctness of this test method.")
'    End Sub

'    '''<summary>
'    '''A test for GetText
'    '''</summary>
'    <TestMethod()> _
'    Public Sub GetTextTest()
'        Dim target As IndependentWindowsFunctionsv1 = New IndependentWindowsFunctionsv1 ' TODO: Initialize to an appropriate value
'        Dim hwnd As IntPtr = New IntPtr ' TODO: Initialize to an appropriate value
'        Dim expected As String = String.Empty ' TODO: Initialize to an appropriate value
'        Dim actual As String
'        actual = target.GetText(hwnd)
'        Assert.AreEqual(expected, actual)
'        Assert.Inconclusive("Verify the correctness of this test method.")
'    End Sub

'    '''<summary>
'    '''A test for GetProcessName
'    '''</summary>
'    <TestMethod()> _
'    Public Sub GetProcessNameTest()
'        Dim target As IndependentWindowsFunctionsv1 = New IndependentWindowsFunctionsv1 ' TODO: Initialize to an appropriate value
'        Dim handle As IntPtr = New IntPtr ' TODO: Initialize to an appropriate value
'        Dim expected As String = String.Empty ' TODO: Initialize to an appropriate value
'        Dim actual As String
'        actual = target.GetProcessName(handle)
'        Assert.AreEqual(expected, actual)
'        Assert.Inconclusive("Verify the correctness of this test method.")
'    End Sub

'    '''<summary>
'    '''A test for GetProcessID
'    '''</summary>
'    <TestMethod()> _
'    Public Sub GetProcessIDTest()
'        Dim target As IndependentWindowsFunctionsv1 = New IndependentWindowsFunctionsv1 ' TODO: Initialize to an appropriate value
'        Dim handle As IntPtr = New IntPtr ' TODO: Initialize to an appropriate value
'        Dim expected As Integer = 0 ' TODO: Initialize to an appropriate value
'        Dim actual As Integer
'        actual = target.GetProcessID(handle)
'        Assert.AreEqual(expected, actual)
'        Assert.Inconclusive("Verify the correctness of this test method.")
'    End Sub

'    '''<summary>
'    '''A test for GetParent
'    '''</summary>
'    <TestMethod()> _
'    Public Sub GetParentTest()
'        Dim target As IndependentWindowsFunctionsv1 = New IndependentWindowsFunctionsv1 ' TODO: Initialize to an appropriate value
'        Dim hwnd As IntPtr = New IntPtr ' TODO: Initialize to an appropriate value
'        Dim expected As IntPtr = New IntPtr ' TODO: Initialize to an appropriate value
'        Dim actual As IntPtr
'        actual = target.GetParent(hwnd)
'        Assert.AreEqual(expected, actual)
'        Assert.Inconclusive("Verify the correctness of this test method.")
'    End Sub

'    '''<summary>
'    '''A test for GetObjectTypeAsString
'    '''</summary>
'    <TestMethod()> _
'    Public Sub GetObjectTypeAsStringTest()
'        Dim target As IndependentWindowsFunctionsv1 = New IndependentWindowsFunctionsv1 ' TODO: Initialize to an appropriate value
'        Dim hwnd As IntPtr = New IntPtr ' TODO: Initialize to an appropriate value
'        Dim expected As String = String.Empty ' TODO: Initialize to an appropriate value
'        Dim actual As String
'        actual = target.GetObjectTypeAsString(hwnd)
'        Assert.AreEqual(expected, actual)
'        Assert.Inconclusive("Verify the correctness of this test method.")
'    End Sub

'    '''<summary>
'    '''A test for GetLocation
'    '''</summary>
'    <TestMethod()> _
'    Public Sub GetLocationTest()
'        Dim target As IndependentWindowsFunctionsv1 = New IndependentWindowsFunctionsv1 ' TODO: Initialize to an appropriate value
'        Dim hwnd As IntPtr = New IntPtr ' TODO: Initialize to an appropriate value
'        Dim expected As Rectangle = New Rectangle ' TODO: Initialize to an appropriate value
'        Dim actual As Rectangle
'        actual = target.GetLocation(hwnd)
'        Assert.AreEqual(expected, actual)
'        Assert.Inconclusive("Verify the correctness of this test method.")
'    End Sub

'    '''<summary>
'    '''A test for GetHwndByXY
'    '''</summary>
'    <TestMethod()> _
'    Public Sub GetHwndByXYTest()
'        Dim target As IndependentWindowsFunctionsv1 = New IndependentWindowsFunctionsv1 ' TODO: Initialize to an appropriate value
'        Dim x As Integer = 0 ' TODO: Initialize to an appropriate value
'        Dim y As Integer = 0 ' TODO: Initialize to an appropriate value
'        Dim expected As IntPtr = New IntPtr ' TODO: Initialize to an appropriate value
'        Dim actual As IntPtr
'        actual = target.GetHwndByXY(x, y)
'        Assert.AreEqual(expected, actual)
'        Assert.Inconclusive("Verify the correctness of this test method.")
'    End Sub

'    '''<summary>
'    '''A test for GetEXWindowsStyle
'    '''</summary>
'    <TestMethod()> _
'    Public Sub GetEXWindowsStyleTest()
'        Dim target As IndependentWindowsFunctionsv1 = New IndependentWindowsFunctionsv1 ' TODO: Initialize to an appropriate value
'        Dim hwnd As IntPtr = New IntPtr ' TODO: Initialize to an appropriate value
'        Dim expected As Integer = 0 ' TODO: Initialize to an appropriate value
'        Dim actual As Integer
'        actual = target.GetEXWindowsStyle(hwnd)
'        Assert.AreEqual(expected, actual)
'        Assert.Inconclusive("Verify the correctness of this test method.")
'    End Sub

'    '''<summary>
'    '''A test for GetDotNetClassName
'    '''</summary>
'    <TestMethod()> _
'    Public Sub GetDotNetClassNameTest()
'        Dim target As IndependentWindowsFunctionsv1 = New IndependentWindowsFunctionsv1 ' TODO: Initialize to an appropriate value
'        Dim hWnd As IntPtr = New IntPtr ' TODO: Initialize to an appropriate value
'        Dim expected As String = String.Empty ' TODO: Initialize to an appropriate value
'        Dim actual As String
'        actual = target.GetDotNetClassName(hWnd)
'        Assert.AreEqual(expected, actual)
'        Assert.Inconclusive("Verify the correctness of this test method.")
'    End Sub

'    '''<summary>
'    '''A test for GetControlName
'    '''</summary>
'    <TestMethod()> _
'    Public Sub GetControlNameTest()
'        Dim target As IndependentWindowsFunctionsv1 = New IndependentWindowsFunctionsv1 ' TODO: Initialize to an appropriate value
'        Dim hwnd As IntPtr = New IntPtr ' TODO: Initialize to an appropriate value
'        Dim expected As String = String.Empty ' TODO: Initialize to an appropriate value
'        Dim actual As String
'        actual = target.GetControlName(hwnd)
'        Assert.AreEqual(expected, actual)
'        Assert.Inconclusive("Verify the correctness of this test method.")
'    End Sub

'    '''<summary>
'    '''A test for GetClassNameNoDotNet
'    '''</summary>
'    <TestMethod()> _
'    Public Sub GetClassNameNoDotNetTest()
'        Dim target As IndependentWindowsFunctionsv1 = New IndependentWindowsFunctionsv1 ' TODO: Initialize to an appropriate value
'        Dim hWnd As IntPtr = New IntPtr ' TODO: Initialize to an appropriate value
'        Dim expected As String = String.Empty ' TODO: Initialize to an appropriate value
'        Dim actual As String
'        actual = target.GetClassNameNoDotNet(hWnd)
'        Assert.AreEqual(expected, actual)
'        Assert.Inconclusive("Verify the correctness of this test method.")
'    End Sub

'    '''<summary>
'    '''A test for GetClassName
'    '''</summary>
'    <TestMethod()> _
'    Public Sub GetClassNameTest()
'        Dim target As IndependentWindowsFunctionsv1 = New IndependentWindowsFunctionsv1 ' TODO: Initialize to an appropriate value
'        Dim hWnd As IntPtr = New IntPtr ' TODO: Initialize to an appropriate value
'        Dim expected As String = String.Empty ' TODO: Initialize to an appropriate value
'        Dim actual As String
'        actual = target.GetClassName(hWnd)
'        Assert.AreEqual(expected, actual)
'        Assert.Inconclusive("Verify the correctness of this test method.")
'    End Sub

'    '''<summary>
'    '''A test for GetCheckBoxState
'    '''</summary>
'    <TestMethod()> _
'    Public Sub GetCheckBoxStateTest()
'        Dim target As IndependentWindowsFunctionsv1 = New IndependentWindowsFunctionsv1 ' TODO: Initialize to an appropriate value
'        Dim Hwnd As IntPtr = New IntPtr ' TODO: Initialize to an appropriate value
'        Dim expected As Integer = 0 ' TODO: Initialize to an appropriate value
'        Dim actual As Integer
'        actual = target.GetCheckBoxState(Hwnd)
'        Assert.AreEqual(expected, actual)
'        Assert.Inconclusive("Verify the correctness of this test method.")
'    End Sub

'    '''<summary>
'    '''A test for GetAllWindowTextSmart
'    '''</summary>
'    <TestMethod()> _
'    Public Sub GetAllWindowTextSmartTest()
'        Dim target As IndependentWindowsFunctionsv1 = New IndependentWindowsFunctionsv1 ' TODO: Initialize to an appropriate value
'        Dim WindowHandle As IntPtr = New IntPtr ' TODO: Initialize to an appropriate value
'        Dim expected As String = String.Empty ' TODO: Initialize to an appropriate value
'        Dim actual As String
'        actual = target.GetAllWindowTextSmart(WindowHandle)
'        Assert.AreEqual(expected, actual)
'        Assert.Inconclusive("Verify the correctness of this test method.")
'    End Sub

'    '''<summary>
'    '''A test for GetAllWindowText
'    '''</summary>
'    <TestMethod()> _
'    Public Sub GetAllWindowTextTest()
'        Dim target As IndependentWindowsFunctionsv1 = New IndependentWindowsFunctionsv1 ' TODO: Initialize to an appropriate value
'        Dim WindowHandle As IntPtr = New IntPtr ' TODO: Initialize to an appropriate value
'        Dim expected As String = String.Empty ' TODO: Initialize to an appropriate value
'        Dim actual As String
'        actual = target.GetAllWindowText(WindowHandle)
'        Assert.AreEqual(expected, actual)
'        Assert.Inconclusive("Verify the correctness of this test method.")
'    End Sub

'    '''<summary>
'    '''A test for GetAllText
'    '''</summary>
'    <TestMethod()> _
'    Public Sub GetAllTextTest()
'        Dim target As IndependentWindowsFunctionsv1 = New IndependentWindowsFunctionsv1 ' TODO: Initialize to an appropriate value
'        Dim WindowHandle As IntPtr = New IntPtr ' TODO: Initialize to an appropriate value
'        Dim expected As String = String.Empty ' TODO: Initialize to an appropriate value
'        Dim actual As String
'        actual = target.GetAllText(WindowHandle)
'        Assert.AreEqual(expected, actual)
'        Assert.Inconclusive("Verify the correctness of this test method.")
'    End Sub

'    '''<summary>
'    '''A test for FindParentTitle
'    '''</summary>
'    <TestMethod(), _
'     DeploymentItem("APIControls.dll")> _
'    Public Sub FindParentTitleTest()
'        Dim target As IndependentWindowsFunctionsv1_Accessor = New IndependentWindowsFunctionsv1_Accessor ' TODO: Initialize to an appropriate value
'        Dim hChild As IntPtr = New IntPtr ' TODO: Initialize to an appropriate value
'        Dim expected As String = String.Empty ' TODO: Initialize to an appropriate value
'        Dim actual As String
'        actual = target.FindParentTitle(hChild)
'        Assert.AreEqual(expected, actual)
'        Assert.Inconclusive("Verify the correctness of this test method.")
'    End Sub

'    '''<summary>
'    '''A test for FindIndex
'    '''</summary>
'    <TestMethod()> _
'    Public Sub FindIndexTest()
'        Dim target As IndependentWindowsFunctionsv1 = New IndependentWindowsFunctionsv1 ' TODO: Initialize to an appropriate value
'        Dim TopHwnd As IntPtr = New IntPtr ' TODO: Initialize to an appropriate value
'        Dim SearchHwnd As IntPtr = New IntPtr ' TODO: Initialize to an appropriate value
'        Dim expected As Integer = 0 ' TODO: Initialize to an appropriate value
'        Dim actual As Integer
'        actual = target.FindIndex(TopHwnd, SearchHwnd)
'        Assert.AreEqual(expected, actual)
'        Assert.Inconclusive("Verify the correctness of this test method.")
'    End Sub

'    '''<summary>
'    '''A test for CreateDescriptionFromHwnd
'    '''</summary>
'    <TestMethod()> _
'    Public Sub CreateDescriptionFromHwndTest()
'        Dim target As IndependentWindowsFunctionsv1 = New IndependentWindowsFunctionsv1 ' TODO: Initialize to an appropriate value
'        Dim hwnd As IntPtr = New IntPtr ' TODO: Initialize to an appropriate value
'        Dim expected As Description = Nothing ' TODO: Initialize to an appropriate value
'        Dim actual As Description
'        actual = target.CreateDescriptionFromHwnd(hwnd)
'        Assert.AreEqual(expected, actual)
'        Assert.Inconclusive("Verify the correctness of this test method.")
'    End Sub

'    '''<summary>
'    '''A test for CreateDescription
'    '''</summary>
'    <TestMethod()> _
'    Public Sub CreateDescriptionTest()
'        Dim target As IndependentWindowsFunctionsv1 = New IndependentWindowsFunctionsv1 ' TODO: Initialize to an appropriate value
'        Dim TopWindowhwnd As IntPtr = New IntPtr ' TODO: Initialize to an appropriate value
'        Dim hwnd As IntPtr = New IntPtr ' TODO: Initialize to an appropriate value
'        Dim expected As Description = Nothing ' TODO: Initialize to an appropriate value
'        Dim actual As Description
'        actual = target.CreateDescription(TopWindowhwnd, hwnd)
'        Assert.AreEqual(expected, actual)
'        Assert.Inconclusive("Verify the correctness of this test method.")
'    End Sub

'    '''<summary>
'    '''A test for CloseWindow
'    '''</summary>
'    <TestMethod()> _
'    Public Sub CloseWindowTest()
'        Dim target As IndependentWindowsFunctionsv1 = New IndependentWindowsFunctionsv1 ' TODO: Initialize to an appropriate value
'        Dim hWnd As IntPtr = New IntPtr ' TODO: Initialize to an appropriate value
'        target.CloseWindow(hWnd)
'        Assert.Inconclusive("A method that does not return a value cannot be verified.")
'    End Sub

'    '''<summary>
'    '''A test for BuildList
'    '''</summary>
'    <TestMethod(), _
'     DeploymentItem("APIControls.dll")> _
'    Public Sub BuildListTest()
'        Dim target As IndependentWindowsFunctionsv1_Accessor = New IndependentWindowsFunctionsv1_Accessor ' TODO: Initialize to an appropriate value
'        Dim TopWindowhwnd As IntPtr = New IntPtr ' TODO: Initialize to an appropriate value
'        Dim hwnd As IntPtr = New IntPtr ' TODO: Initialize to an appropriate value
'        Dim expected() As IntPtr = Nothing ' TODO: Initialize to an appropriate value
'        Dim actual() As IntPtr
'        actual = target.BuildList(TopWindowhwnd, hwnd)
'        Assert.AreEqual(expected, actual)
'        Assert.Inconclusive("Verify the correctness of this test method.")
'    End Sub

'    '''<summary>
'    '''A test for AppendText
'    '''</summary>
'    <TestMethod()> _
'    Public Sub AppendTextTest()
'        Dim target As IndependentWindowsFunctionsv1 = New IndependentWindowsFunctionsv1 ' TODO: Initialize to an appropriate value
'        Dim text As String = String.Empty ' TODO: Initialize to an appropriate value
'        Dim hwnd As IntPtr = New IntPtr ' TODO: Initialize to an appropriate value
'        target.AppendText(text, hwnd)
'        Assert.Inconclusive("A method that does not return a value cannot be verified.")
'    End Sub

'    '''<summary>
'    '''A test for AppActivateByHwnd
'    '''</summary>
'    <TestMethod()> _
'    Public Sub AppActivateByHwndTest()
'        Dim target As IndependentWindowsFunctionsv1 = New IndependentWindowsFunctionsv1 ' TODO: Initialize to an appropriate value
'        Dim Hwnd As IntPtr = New IntPtr ' TODO: Initialize to an appropriate value
'        target.AppActivateByHwnd(Hwnd)
'        Assert.Inconclusive("A method that does not return a value cannot be verified.")
'    End Sub

'    '''<summary>
'    '''A test for IndependentWindowsFunctionsv1 Constructor
'    '''</summary>
'    <TestMethod()> _
'    Public Sub IndependentWindowsFunctionsv1ConstructorTest()
'        Dim target As IndependentWindowsFunctionsv1 = New IndependentWindowsFunctionsv1
'        Assert.Inconclusive("TODO: Implement code to verify target")
'    End Sub
'End Class
