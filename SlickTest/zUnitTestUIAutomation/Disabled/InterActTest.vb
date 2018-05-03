'Imports System.Timers

'Imports System.Drawing

'Imports System

'Imports Microsoft.VisualStudio.TestTools.UnitTesting

'Imports UIControls



''''<summary>
''''This is a test class for InterActTest and is intended
''''to contain all InterActTest Unit Tests
''''</summary>
'<TestClass()> _
'Public Class InterActTest


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
'    '''A test for Settings
'    '''</summary>
'    <TestMethod()> _
'    Public Sub SettingsTest()
'        Dim target As InterAct = New InterAct ' TODO: Initialize to an appropriate value
'        Dim actual As AutomationSettings
'        actual = target.Settings
'        Assert.Inconclusive("Verify the correctness of this test method.")
'    End Sub

'    '''<summary>
'    '''A test for Screenshot
'    '''</summary>
'    <TestMethod()> _
'    Public Sub ScreenshotTest()
'        Dim target As InterAct = New InterAct ' TODO: Initialize to an appropriate value
'        Dim actual As Screenshot
'        actual = target.Screenshot
'        Assert.Inconclusive("Verify the correctness of this test method.")
'    End Sub

'    '''<summary>
'    '''A test for RunningTimer
'    '''</summary>
'    <TestMethod()> _
'    Public Sub RunningTimerTest()
'        Dim expected As Timer = Nothing ' TODO: Initialize to an appropriate value
'        Dim actual As Timer
'        InterAct.RunningTimer = expected
'        actual = InterAct.RunningTimer
'        Assert.AreEqual(expected, actual)
'        Assert.Inconclusive("Verify the correctness of this test method.")
'    End Sub

'    '''<summary>
'    '''A test for Report
'    '''</summary>
'    <TestMethod()> _
'    Public Sub ReportTest()
'        Dim target As InterAct = New InterAct ' TODO: Initialize to an appropriate value
'        Dim actual As Report
'        actual = target.Report
'        Assert.Inconclusive("Verify the correctness of this test method.")
'    End Sub

'    '''<summary>
'    '''A test for Mouse
'    '''</summary>
'    <TestMethod()> _
'    Public Sub MouseTest()
'        Dim target As InterAct = New InterAct ' TODO: Initialize to an appropriate value
'        Dim actual As Mouse
'        actual = target.Mouse
'        Assert.Inconclusive("Verify the correctness of this test method.")
'    End Sub

'    '''<summary>
'    '''A test for Clipboard
'    '''</summary>
'    <TestMethod()> _
'    Public Sub ClipboardTest()
'        Dim target As InterAct = New InterAct ' TODO: Initialize to an appropriate value
'        Dim actual As Clipboard
'        actual = target.Clipboard
'        Assert.Inconclusive("Verify the correctness of this test method.")
'    End Sub

'    '''<summary>
'    '''A test for Window
'    '''</summary>
'    <TestMethod()> _
'    Public Sub WindowTest()
'        Dim target As InterAct = New InterAct ' TODO: Initialize to an appropriate value
'        Dim description As Object = Nothing ' TODO: Initialize to an appropriate value
'        Dim expected As Window = Nothing ' TODO: Initialize to an appropriate value
'        Dim actual As Window
'        actual = target.Window(description)
'        Assert.AreEqual(expected, actual)
'        Assert.Inconclusive("Verify the correctness of this test method.")
'    End Sub

'    '''<summary>
'    '''A test for SwfWindow
'    '''</summary>
'    <TestMethod()> _
'    Public Sub SwfWindowTest()
'        Dim target As InterAct = New InterAct ' TODO: Initialize to an appropriate value
'        Dim description As Object = Nothing ' TODO: Initialize to an appropriate value
'        Dim expected As SwfWindow = Nothing ' TODO: Initialize to an appropriate value
'        Dim actual As SwfWindow
'        actual = target.SwfWindow(description)
'        Assert.AreEqual(expected, actual)
'        Assert.Inconclusive("Verify the correctness of this test method.")
'    End Sub

'    '''<summary>
'    '''A test for SetupWinObjects
'    '''</summary>
'    <TestMethod(), _
'     DeploymentItem("InterAction.dll")> _
'    Public Sub SetupWinObjectsTest1()
'        Dim target As InterAct_Accessor = New InterAct_Accessor ' TODO: Initialize to an appropriate value
'        Dim win As WinObject = Nothing ' TODO: Initialize to an appropriate value
'        Dim winExpected As WinObject = Nothing ' TODO: Initialize to an appropriate value
'        Dim expected As WinObject = Nothing ' TODO: Initialize to an appropriate value
'        Dim actual As WinObject
'        actual = target.SetupWinObjects(win)
'        Assert.AreEqual(winExpected, win)
'        Assert.AreEqual(expected, actual)
'        Assert.Inconclusive("Verify the correctness of this test method.")
'    End Sub

'    '''<summary>
'    '''A test for SetupWinObjects
'    '''</summary>
'    <TestMethod(), _
'     DeploymentItem("InterAction.dll")> _
'    Public Sub SetupWinObjectsTest()
'        Dim target As InterAct_Accessor = New InterAct_Accessor ' TODO: Initialize to an appropriate value
'        target.SetupWinObjects()
'        Assert.Inconclusive("A method that does not return a value cannot be verified.")
'    End Sub

'    '''<summary>
'    '''A test for SendInputPrivate
'    '''</summary>
'    <TestMethod(), _
'     DeploymentItem("InterAction.dll")> _
'    Public Sub SendInputPrivateTest()
'        Dim target As InterAct_Accessor = New InterAct_Accessor ' TODO: Initialize to an appropriate value
'        Dim Input As String = String.Empty ' TODO: Initialize to an appropriate value
'        Dim expected As Boolean = False ' TODO: Initialize to an appropriate value
'        Dim actual As Boolean
'        actual = target.SendInputPrivate(Input)
'        Assert.AreEqual(expected, actual)
'        Assert.Inconclusive("Verify the correctness of this test method.")
'    End Sub

'    '''<summary>
'    '''A test for SendInput
'    '''</summary>
'    <TestMethod()> _
'    Public Sub SendInputTest()
'        Dim target As InterAct = New InterAct ' TODO: Initialize to an appropriate value
'        Dim Input As String = String.Empty ' TODO: Initialize to an appropriate value
'        Dim expected As Boolean = False ' TODO: Initialize to an appropriate value
'        Dim actual As Boolean
'        actual = target.SendInput(Input)
'        Assert.AreEqual(expected, actual)
'        Assert.Inconclusive("Verify the correctness of this test method.")
'    End Sub

'    '''<summary>
'    '''A test for RunTimeReset
'    '''</summary>
'    <TestMethod()> _
'    Public Sub RunTimeResetTest()
'        InterAct.RunTimeReset()
'        Assert.Inconclusive("A method that does not return a value cannot be verified.")
'    End Sub

'    '''<summary>
'    '''A test for RunningTimer_Elapsed
'    '''</summary>
'    <TestMethod(), _
'     DeploymentItem("InterAction.dll")> _
'    Public Sub RunningTimer_ElapsedTest()
'        Dim sender As Object = Nothing ' TODO: Initialize to an appropriate value
'        Dim e As ElapsedEventArgs = Nothing ' TODO: Initialize to an appropriate value
'        InterAct_Accessor.RunningTimer_Elapsed(sender, e)
'        Assert.Inconclusive("A method that does not return a value cannot be verified.")
'    End Sub

'    '''<summary>
'    '''A test for ProcessInput
'    '''</summary>
'    <TestMethod(), _
'     DeploymentItem("InterAction.dll")> _
'    Public Sub ProcessInputTest()
'        Dim target As InterAct_Accessor = New InterAct_Accessor ' TODO: Initialize to an appropriate value
'        Dim Input As String = String.Empty ' TODO: Initialize to an appropriate value
'        Dim SearchString As String = String.Empty ' TODO: Initialize to an appropriate value
'        Dim Index As Integer = 0 ' TODO: Initialize to an appropriate value
'        Dim expected As Point = New Point ' TODO: Initialize to an appropriate value
'        Dim actual As Point
'        actual = target.ProcessInput(Input, SearchString, Index)
'        Assert.AreEqual(expected, actual)
'        Assert.Inconclusive("Verify the correctness of this test method.")
'    End Sub

'    '''<summary>
'    '''A test for GetXYVals
'    '''</summary>
'    <TestMethod(), _
'     DeploymentItem("InterAction.dll")> _
'    Public Sub GetXYValsTest()
'        Dim target As InterAct_Accessor = New InterAct_Accessor ' TODO: Initialize to an appropriate value
'        Dim Input As String = String.Empty ' TODO: Initialize to an appropriate value
'        Dim SearchString As String = String.Empty ' TODO: Initialize to an appropriate value
'        Dim Index As Integer = 0 ' TODO: Initialize to an appropriate value
'        Dim expected As Point = New Point ' TODO: Initialize to an appropriate value
'        Dim actual As Point
'        actual = target.GetXYVals(Input, SearchString, Index)
'        Assert.AreEqual(expected, actual)
'        Assert.Inconclusive("Verify the correctness of this test method.")
'    End Sub

'    '''<summary>
'    '''A test for GetDescription
'    '''</summary>
'    <TestMethod(), _
'     DeploymentItem("InterAction.dll")> _
'    Public Sub GetDescriptionTest()
'        Dim target As InterAct_Accessor = New InterAct_Accessor ' TODO: Initialize to an appropriate value
'        Dim Input As String = String.Empty ' TODO: Initialize to an appropriate value
'        Dim SearchString As String = String.Empty ' TODO: Initialize to an appropriate value
'        Dim Index As Integer = 0 ' TODO: Initialize to an appropriate value
'        Dim expected As Point = New Point ' TODO: Initialize to an appropriate value
'        Dim actual As Point
'        actual = target.GetDescription(Input, SearchString, Index)
'        Assert.AreEqual(expected, actual)
'        Assert.Inconclusive("Verify the correctness of this test method.")
'    End Sub

'    '''<summary>
'    '''A test for ExceptionHandler
'    '''</summary>
'    <TestMethod(), _
'     DeploymentItem("InterAction.dll")> _
'    Public Sub ExceptionHandlerTest()
'        Dim target As InterAct_Accessor = New InterAct_Accessor ' TODO: Initialize to an appropriate value
'        Dim sender As Object = Nothing ' TODO: Initialize to an appropriate value
'        Dim args As UnhandledExceptionEventArgs = Nothing ' TODO: Initialize to an appropriate value
'        target.ExceptionHandler(sender, args)
'        Assert.Inconclusive("A method that does not return a value cannot be verified.")
'    End Sub

'    '''<summary>
'    '''A test for Constructor
'    '''</summary>
'    <TestMethod(), _
'     DeploymentItem("InterAction.dll")> _
'    Public Sub ConstructorTest()
'        Dim target As InterAct_Accessor = New InterAct_Accessor ' TODO: Initialize to an appropriate value
'        target.Constructor()
'        Assert.Inconclusive("A method that does not return a value cannot be verified.")
'    End Sub

'    '''<summary>
'    '''A test for AppActivateByHwnd
'    '''</summary>
'    <TestMethod()> _
'    Public Sub AppActivateByHwndTest()
'        Dim target As InterAct = New InterAct ' TODO: Initialize to an appropriate value
'        Dim Hwnd As Integer = 0 ' TODO: Initialize to an appropriate value
'        target.AppActivateByHwnd(Hwnd)
'        Assert.Inconclusive("A method that does not return a value cannot be verified.")
'    End Sub

'    '''<summary>
'    '''A test for AppActivate
'    '''</summary>
'    <TestMethod()> _
'    Public Sub AppActivateTest1()
'        Dim target As InterAct = New InterAct ' TODO: Initialize to an appropriate value
'        Dim AppTitle As String = String.Empty ' TODO: Initialize to an appropriate value
'        target.AppActivate(AppTitle)
'        Assert.Inconclusive("A method that does not return a value cannot be verified.")
'    End Sub

'    '''<summary>
'    '''A test for AppActivate
'    '''</summary>
'    <TestMethod()> _
'    Public Sub AppActivateTest()
'        Dim target As InterAct = New InterAct ' TODO: Initialize to an appropriate value
'        Dim AppID As Integer = 0 ' TODO: Initialize to an appropriate value
'        target.AppActivate(AppID)
'        Assert.Inconclusive("A method that does not return a value cannot be verified.")
'    End Sub

'    '''<summary>
'    '''A test for InterAct Constructor
'    '''</summary>
'    <TestMethod()> _
'    Public Sub InterActConstructorTest()
'        Dim target As InterAct = New InterAct
'        Assert.Inconclusive("TODO: Implement code to verify target")
'    End Sub
'End Class
