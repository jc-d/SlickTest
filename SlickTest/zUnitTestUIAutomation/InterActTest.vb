
Imports NUnit.Framework

''''<summary>
''''This is a test class for InterActTest and is intended
''''to contain all InterActTest Unit Tests
''''</summary>
<NUnit.Framework.TestFixture()> _
Public Class InterActTest

#Region "Additional test attributes"

    'You can use the following additional attributes as you write your tests:

    'Use ClassInitialize to run code before running the first test in the class
    <NUnit.Framework.SetUp()> _
    Public Shared Sub MyClassInitialize()
    End Sub

    'Use ClassCleanup to run code after all tests in a class have run
    <NUnit.Framework.TestFixtureTearDown()> _
    Public Shared Sub MyClassCleanup()
    End Sub

    'Use TestInitialize to run code before running each test
    <NUnit.Framework.TestFixtureSetUp()> _
    Public Sub MyTestInitialize()
    End Sub

    'Use TestCleanup to run code after each test has run
    <NUnit.Framework.TearDown()> _
    Public Sub MyTestCleanup()
    End Sub

#End Region

    <Test()> _
    Public Sub StartProgram()
        Dim i As New UIControls.InterAct()
        Dim desc As UIControls.Description = i.StartProgram("Calc.exe")

        Verify.IsTrue(i.Window(desc).Exists(10), "Window found.")
        i.Window(desc).Close()
    End Sub

    <Test()> _
    Public Sub StartProgram1()
        Dim i As New UIControls.InterAct()
        Dim psi As New System.Diagnostics.ProcessStartInfo("Calc.exe")
        Dim desc As UIControls.Description = i.StartProgram(psi)

        Verify.IsTrue(i.Window(desc).Exists(10), "Window found.")
        i.Window(desc).Close()
    End Sub

#Region "Commented Out Code"

    '    '''<summary>
    '    '''A test for Settings
    '    '''</summary>
    '    <Test()> _
    '    Public Sub SettingsTest()
    '        Dim target As InterAct = New InterAct ' TODO: Initialize to an appropriate value
    '        Dim actual As AutomationSettings
    '        actual = target.Settings
    '        Verify.Inconclusive("Verify the correctness of this test method.")
    '    End Sub

    '    '''<summary>
    '    '''A test for Screenshot
    '    '''</summary>
    '    <Test()> _
    '    Public Sub ScreenshotTest()
    '        Dim target As InterAct = New InterAct ' TODO: Initialize to an appropriate value
    '        Dim actual As Screenshot
    '        actual = target.Screenshot
    '        Verify.Inconclusive("Verify the correctness of this test method.")
    '    End Sub

    '    '''<summary>
    '    '''A test for RunningTimer
    '    '''</summary>
    '    <Test()> _
    '    Public Sub RunningTimerTest()
    '        Dim expected As Timer = Nothing ' TODO: Initialize to an appropriate value
    '        Dim actual As Timer
    '        InterAct.RunningTimer = expected
    '        actual = InterAct.RunningTimer
    '        Verify.AreEqual(expected, actual)
    '        Verify.Inconclusive("Verify the correctness of this test method.")
    '    End Sub

    '    '''<summary>
    '    '''A test for Report
    '    '''</summary>
    '    <Test()> _
    '    Public Sub ReportTest()
    '        Dim target As InterAct = New InterAct ' TODO: Initialize to an appropriate value
    '        Dim actual As Report
    '        actual = target.Report
    '        Verify.Inconclusive("Verify the correctness of this test method.")
    '    End Sub

    '    '''<summary>
    '    '''A test for Mouse
    '    '''</summary>
    '    <Test()> _
    '    Public Sub MouseTest()
    '        Dim target As InterAct = New InterAct ' TODO: Initialize to an appropriate value
    '        Dim actual As Mouse
    '        actual = target.Mouse
    '        Verify.Inconclusive("Verify the correctness of this test method.")
    '    End Sub

    '    '''<summary>
    '    '''A test for Clipboard
    '    '''</summary>
    '    <Test()> _
    '    Public Sub ClipboardTest()
    '        Dim target As InterAct = New InterAct ' TODO: Initialize to an appropriate value
    '        Dim actual As Clipboard
    '        actual = target.Clipboard
    '        Verify.Inconclusive("Verify the correctness of this test method.")
    '    End Sub

    '    '''<summary>
    '    '''A test for Window
    '    '''</summary>
    '    <Test()> _
    '    Public Sub WindowTest()
    '        Dim target As InterAct = New InterAct ' TODO: Initialize to an appropriate value
    '        Dim description As Object = Nothing ' TODO: Initialize to an appropriate value
    '        Dim expected As Window = Nothing ' TODO: Initialize to an appropriate value
    '        Dim actual As Window
    '        actual = target.Window(description)
    '        Verify.AreEqual(expected, actual)
    '        Verify.Inconclusive("Verify the correctness of this test method.")
    '    End Sub

    '    '''<summary>
    '    '''A test for SwfWindow
    '    '''</summary>
    '    <Test()> _
    '    Public Sub SwfWindowTest()
    '        Dim target As InterAct = New InterAct ' TODO: Initialize to an appropriate value
    '        Dim description As Object = Nothing ' TODO: Initialize to an appropriate value
    '        Dim expected As SwfWindow = Nothing ' TODO: Initialize to an appropriate value
    '        Dim actual As SwfWindow
    '        actual = target.SwfWindow(description)
    '        Verify.AreEqual(expected, actual)
    '        Verify.Inconclusive("Verify the correctness of this test method.")
    '    End Sub

    '    '''<summary>
    '    '''A test for SetupWinObjects
    '    '''</summary>
    '    <Test(), _
    '     DeploymentItem("InterAction.dll")> _
    '    Public Sub SetupWinObjectsTest1()
    '        Dim target As InterAct_Accessor = New InterAct_Accessor ' TODO: Initialize to an appropriate value
    '        Dim win As WinObject = Nothing ' TODO: Initialize to an appropriate value
    '        Dim winExpected As WinObject = Nothing ' TODO: Initialize to an appropriate value
    '        Dim expected As WinObject = Nothing ' TODO: Initialize to an appropriate value
    '        Dim actual As WinObject
    '        actual = target.SetupWinObjects(win)
    '        Verify.AreEqual(winExpected, win)
    '        Verify.AreEqual(expected, actual)
    '        Verify.Inconclusive("Verify the correctness of this test method.")
    '    End Sub

    '    '''<summary>
    '    '''A test for SetupWinObjects
    '    '''</summary>
    '    <Test(), _
    '     DeploymentItem("InterAction.dll")> _
    '    Public Sub SetupWinObjectsTest()
    '        Dim target As InterAct_Accessor = New InterAct_Accessor ' TODO: Initialize to an appropriate value
    '        target.SetupWinObjects()
    '        Verify.Inconclusive("A method that does not return a value cannot be verified.")
    '    End Sub

    '    '''<summary>
    '    '''A test for SendInputPrivate
    '    '''</summary>
    '    <Test(), _
    '     DeploymentItem("InterAction.dll")> _
    '    Public Sub SendInputPrivateTest()
    '        Dim target As InterAct_Accessor = New InterAct_Accessor ' TODO: Initialize to an appropriate value
    '        Dim Input As String = String.Empty ' TODO: Initialize to an appropriate value
    '        Dim expected As Boolean = False ' TODO: Initialize to an appropriate value
    '        Dim actual As Boolean
    '        actual = target.SendInputPrivate(Input)
    '        Verify.AreEqual(expected, actual)
    '        Verify.Inconclusive("Verify the correctness of this test method.")
    '    End Sub

    '    '''<summary>
    '    '''A test for SendInput
    '    '''</summary>
    '    <Test()> _
    '    Public Sub SendInputTest()
    '        Dim target As InterAct = New InterAct ' TODO: Initialize to an appropriate value
    '        Dim Input As String = String.Empty ' TODO: Initialize to an appropriate value
    '        Dim expected As Boolean = False ' TODO: Initialize to an appropriate value
    '        Dim actual As Boolean
    '        actual = target.SendInput(Input)
    '        Verify.AreEqual(expected, actual)
    '        Verify.Inconclusive("Verify the correctness of this test method.")
    '    End Sub

    '    '''<summary>
    '    '''A test for RunTimeReset
    '    '''</summary>
    '    <Test()> _
    '    Public Sub RunTimeResetTest()
    '        InterAct.RunTimeReset()
    '        Verify.Inconclusive("A method that does not return a value cannot be verified.")
    '    End Sub

    '    '''<summary>
    '    '''A test for RunningTimer_Elapsed
    '    '''</summary>
    '    <Test(), _
    '     DeploymentItem("InterAction.dll")> _
    '    Public Sub RunningTimer_ElapsedTest()
    '        Dim sender As Object = Nothing ' TODO: Initialize to an appropriate value
    '        Dim e As ElapsedEventArgs = Nothing ' TODO: Initialize to an appropriate value
    '        InterAct_Accessor.RunningTimer_Elapsed(sender, e)
    '        Verify.Inconclusive("A method that does not return a value cannot be verified.")
    '    End Sub

    '    '''<summary>
    '    '''A test for ProcessInput
    '    '''</summary>
    '    <Test(), _
    '     DeploymentItem("InterAction.dll")> _
    '    Public Sub ProcessInputTest()
    '        Dim target As InterAct_Accessor = New InterAct_Accessor ' TODO: Initialize to an appropriate value
    '        Dim Input As String = String.Empty ' TODO: Initialize to an appropriate value
    '        Dim SearchString As String = String.Empty ' TODO: Initialize to an appropriate value
    '        Dim Index As Integer = 0 ' TODO: Initialize to an appropriate value
    '        Dim expected As Point = New Point ' TODO: Initialize to an appropriate value
    '        Dim actual As Point
    '        actual = target.ProcessInput(Input, SearchString, Index)
    '        Verify.AreEqual(expected, actual)
    '        Verify.Inconclusive("Verify the correctness of this test method.")
    '    End Sub

    '    '''<summary>
    '    '''A test for GetXYVals
    '    '''</summary>
    '    <Test(), _
    '     DeploymentItem("InterAction.dll")> _
    '    Public Sub GetXYValsTest()
    '        Dim target As InterAct_Accessor = New InterAct_Accessor ' TODO: Initialize to an appropriate value
    '        Dim Input As String = String.Empty ' TODO: Initialize to an appropriate value
    '        Dim SearchString As String = String.Empty ' TODO: Initialize to an appropriate value
    '        Dim Index As Integer = 0 ' TODO: Initialize to an appropriate value
    '        Dim expected As Point = New Point ' TODO: Initialize to an appropriate value
    '        Dim actual As Point
    '        actual = target.GetXYVals(Input, SearchString, Index)
    '        Verify.AreEqual(expected, actual)
    '        Verify.Inconclusive("Verify the correctness of this test method.")
    '    End Sub

    '    '''<summary>
    '    '''A test for GetDescription
    '    '''</summary>
    '    <Test(), _
    '     DeploymentItem("InterAction.dll")> _
    '    Public Sub GetDescriptionTest()
    '        Dim target As InterAct_Accessor = New InterAct_Accessor ' TODO: Initialize to an appropriate value
    '        Dim Input As String = String.Empty ' TODO: Initialize to an appropriate value
    '        Dim SearchString As String = String.Empty ' TODO: Initialize to an appropriate value
    '        Dim Index As Integer = 0 ' TODO: Initialize to an appropriate value
    '        Dim expected As Point = New Point ' TODO: Initialize to an appropriate value
    '        Dim actual As Point
    '        actual = target.GetDescription(Input, SearchString, Index)
    '        Verify.AreEqual(expected, actual)
    '        Verify.Inconclusive("Verify the correctness of this test method.")
    '    End Sub

    '    '''<summary>
    '    '''A test for ExceptionHandler
    '    '''</summary>
    '    <Test(), _
    '     DeploymentItem("InterAction.dll")> _
    '    Public Sub ExceptionHandlerTest()
    '        Dim target As InterAct_Accessor = New InterAct_Accessor ' TODO: Initialize to an appropriate value
    '        Dim sender As Object = Nothing ' TODO: Initialize to an appropriate value
    '        Dim args As UnhandledExceptionEventArgs = Nothing ' TODO: Initialize to an appropriate value
    '        target.ExceptionHandler(sender, args)
    '        Verify.Inconclusive("A method that does not return a value cannot be verified.")
    '    End Sub

    '    '''<summary>
    '    '''A test for Constructor
    '    '''</summary>
    '    <Test(), _
    '     DeploymentItem("InterAction.dll")> _
    '    Public Sub ConstructorTest()
    '        Dim target As InterAct_Accessor = New InterAct_Accessor ' TODO: Initialize to an appropriate value
    '        target.Constructor()
    '        Verify.Inconclusive("A method that does not return a value cannot be verified.")
    '    End Sub

    '    '''<summary>
    '    '''A test for AppActivateByHwnd
    '    '''</summary>
    '    <Test()> _
    '    Public Sub AppActivateByHwndTest()
    '        Dim target As InterAct = New InterAct ' TODO: Initialize to an appropriate value
    '        Dim Hwnd As Integer = 0 ' TODO: Initialize to an appropriate value
    '        target.AppActivateByHwnd(Hwnd)
    '        Verify.Inconclusive("A method that does not return a value cannot be verified.")
    '    End Sub

    '    '''<summary>
    '    '''A test for AppActivate
    '    '''</summary>
    '    <Test()> _
    '    Public Sub AppActivateTest1()
    '        Dim target As InterAct = New InterAct ' TODO: Initialize to an appropriate value
    '        Dim AppTitle As String = String.Empty ' TODO: Initialize to an appropriate value
    '        target.AppActivate(AppTitle)
    '        Verify.Inconclusive("A method that does not return a value cannot be verified.")
    '    End Sub

    '    '''<summary>
    '    '''A test for AppActivate
    '    '''</summary>
    '    <Test()> _
    '    Public Sub AppActivateTest()
    '        Dim target As InterAct = New InterAct ' TODO: Initialize to an appropriate value
    '        Dim AppID As Integer = 0 ' TODO: Initialize to an appropriate value
    '        target.AppActivate(AppID)
    '        Verify.Inconclusive("A method that does not return a value cannot be verified.")
    '    End Sub

    '    '''<summary>
    '    '''A test for InterAct Constructor
    '    '''</summary>
    '    <Test()> _
    '    Public Sub InterActConstructorTest()
    '        Dim target As InterAct = New InterAct
    '        Verify.Inconclusive("TODO: Implement code to verify target")
    '    End Sub
#End Region

End Class
