Imports System.Windows.Forms
Imports System
Imports NUnit.Framework
Imports UIControls



'''<summary>
'''This is a test class for WindowTest and is intended
'''to contain all WindowTest Unit Tests
'''</summary>
<TestFixture()> _
Public Class WindowTest

#Region "Additional test attributes"
    Private Shared p As System.Diagnostics.Process
    Private Shared hwnd As IntPtr
    Private Shared target As Window

    Public Shared Sub CloseAll(Optional ByVal name As String = "calc")
        For Each pro As System.Diagnostics.Process In System.Diagnostics.Process.GetProcessesByName(name)
            pro.CloseMainWindow()
        Next
        System.Threading.Thread.Sleep(100)
    End Sub


    Public Shared Sub Init()
        CloseAll()
        p = Diagnostics.Process.Start(System.IO.Path.GetFullPath("..\..\..\zUnitTestUIAutomation\Programs\calc.exe"))
        hwnd = p.MainWindowHandle
        Dim a As UIControls.InterAct
        a = New UIControls.InterAct()
        target = a.Window("name:=""" & "SciCalc" & """")
    End Sub

    '
    'You can use the following additional attributes as you write your tests:
    '
    'Use ClassInitialize to run code before running the first test in the class
    <TestFixtureSetUp()> _
    Public Shared Sub MyClassInitialize()
    End Sub

    'Use ClassCleanup to run code after all tests in a class have run
    <TestFixtureTearDown()> _
    Public Shared Sub MyClassCleanup()
    End Sub

    'Use TestInitialize to run code before running each test
    <SetUp()> _
    Public Sub MyTestInitialize()
        Init()
    End Sub

    'Use TestCleanup to run code after each test has run
    <TearDown()> _
    Public Sub MyTestCleanup()
        CloseAll()
    End Sub
    '
#End Region


    '''<summary>
    '''A test for SetWindowState
    ''' WARNING: I have noticed when VS is full screened (maximized) then this test typically
    ''' fails.  But if it set to Normal the test passes.
    '''</summary>
    <Test()> _
    Public Sub SetWindowStateTest()
        Dim State As FormWindowState = FormWindowState.Minimized
        System.Threading.Thread.Sleep(1000)
        target.SetWindowState(State)
        System.Threading.Thread.Sleep(1000)
        Verify.AreEqual(State, target.GetWindowState())
        State = FormWindowState.Normal
        target.SetWindowState(State)
        System.Threading.Thread.Sleep(1000)
        Verify.AreEqual(State, target.GetWindowState())
    End Sub

    'calc isn't designed to resize.
    ''''<summary>
    ''''A test for SetSize
    ''''</summary>
    '<Test()> _
    'Public Sub SetSizeTest()

    '    Dim Width As Integer = 0 ' TODO: Initialize to an appropriate value
    '    Dim Height As Integer = 0 ' TODO: Initialize to an appropriate value
    '    target.SetSize(Width, Height)
    '    Verify.Inconclusive("A method that does not return a value cannot be verified.")
    'End Sub

    '''<summary>
    '''A test for SetActive
    '''</summary>
    <Test()> _
    Public Sub SetActiveTest()
        System.Threading.Thread.Sleep(1000)
        target.SetActive()
        Dim tw As New APIControls.TopWindow()
        Verify.AreEqual(target.GetText(), tw.GetActiveWindow())
    End Sub

    '''<summary>
    '''A test for Move
    '''</summary>
    <Test()> _
    Public Sub MoveTest()
        System.Threading.Thread.Sleep(2000)
        Dim X As Integer = 20 ' TODO: Initialize to an appropriate value
        Dim Y As Integer = 20 ' TODO: Initialize to an appropriate value
        target.Move(X, Y)
        Verify.AreEqual(X, target.GetLocationRect().X)
        Verify.AreEqual(Y, target.GetLocationRect().Y)
    End Sub

    '''<summary>
    '''A test for HasTitleBar
    '''</summary>
    <Test()> _
    Public Sub HasTitleBarTest()
        Dim expected As Boolean = True
        Dim actual As Boolean
        System.Threading.Thread.Sleep(1000)
        actual = target.HasTitleBar
        Verify.AreEqual(expected, actual)
    End Sub

    '''<summary>
    '''A test for HasMinimizeButton
    '''</summary>
    <Test()> _
    Public Sub HasMinimizeButtonTest()
        Dim expected As Boolean = True
        Dim actual As Boolean
        System.Threading.Thread.Sleep(1000)
        actual = target.HasMinimizeButton
        Verify.AreEqual(expected, actual)
    End Sub

    '''<summary>
    '''A test for HasMaximizeButton
    '''</summary>
    <Test()> _
    Public Sub HasMaximizeButtonTest()
        Dim expected As Boolean = False
        Dim actual As Boolean
        System.Threading.Thread.Sleep(1000)
        actual = target.HasMaximizeButton
        Verify.AreEqual(expected, actual)
    End Sub

    '''<summary>
    '''A test for HasBorder
    '''</summary>
    <Test()> _
    Public Sub HasBorderTest()
        Dim expected As Boolean = True 'I think
        Dim actual As Boolean
        System.Threading.Thread.Sleep(1000)
        actual = target.HasBorder
        Verify.AreEqual(expected, actual)
    End Sub

    '''<summary>
    '''A test for GetWindowState
    '''</summary>
    <Test()> _
    Public Sub GetWindowStateTest()
        Dim expected As FormWindowState = FormWindowState.Normal
        target.SetWindowState(expected)
        Dim actual As FormWindowState
        actual = target.GetWindowState
        Verify.AreEqual(expected, actual)
    End Sub

    '''<summary>
    '''A test for DumpWindowData
    '''</summary>
    <Test()> _
    Public Sub DumpWindowData()
        Dim actual As String = target.DumpWindowData()
        Verify.AreNotEqual(0, actual.Length)
    End Sub

    '''<summary>
    '''A test for CloseAll
    '''</summary>
    <Test()> _
    Public Sub CloseAllTest()
        System.Threading.Thread.Sleep(1000)
        target.CloseAll()
        System.Threading.Thread.Sleep(1000)
        Verify.AreEqual(target.Exists(5), False)
        'Init()
    End Sub

    '''<summary>
    '''A test for Close
    '''</summary>
    <Test()> _
    Public Sub CloseTest()
        System.Threading.Thread.Sleep(1000)
        target.Close()
        System.Threading.Thread.Sleep(1000)
        Verify.AreEqual(target.Exists(5), False)
        'Init()
    End Sub

End Class
