Imports System
Imports NUnit.Framework
Imports APIControls



'''<summary>
'''This is a test class for WindowWindowsAPITest and is intended
'''to contain all WindowWindowsAPITest Unit Tests
'''</summary>
<TestFixture()> _
Public Class WindowWindowsAPITest

#Region "Additional test attributes"
    Private Shared p As System.Diagnostics.Process
    Private Shared hwnd As IntPtr

    Public Shared Sub CloseAll(Optional ByVal name As String = "calc")
        For Each pro As System.Diagnostics.Process In System.Diagnostics.Process.GetProcessesByName(name)
            pro.CloseMainWindow()
        Next
        System.Threading.Thread.Sleep(100)
    End Sub
    '
    'You can use the following additional attributes as you write your tests:
    '
    'Use ClassInitialize to run code before running the first test in the class
    <TestFixtureSetUp()> _
    Public Shared Sub MyClassInitialize()

    End Sub

    Public Shared Sub Init()
        p = Diagnostics.Process.Start(System.IO.Path.GetFullPath("..\..\..\zUnitTestUIAutomation\Programs\calc.exe"))
        System.Threading.Thread.Sleep(100)
        hwnd = p.MainWindowHandle
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
    '''A test for SetToForeGround
    '''</summary>
    <Test()> _
    Public Sub SetToForeGroundTest()
        Dim wf As New IndependentWindowsFunctionsv1()
        Dim target As WindowWindowsAPI = New WindowWindowsAPI(wf)
        target.SetToForeGround(hwnd)
        Verify.AreEqual(hwnd, TopWindow.GetCurrentlyActiveWindowHandle())
    End Sub

    '''<summary>
    '''A test for IsWindow
    '''</summary>
    <Test()> _
    Public Sub IsWindowTest()
        Dim wf As New IndependentWindowsFunctionsv1()
        Dim target As WindowWindowsAPI = New WindowWindowsAPI(wf)
        Dim expected As Boolean = True
        Dim actual As Boolean
        actual = target.IsWindow(hwnd)
        Verify.AreEqual(expected, actual)
    End Sub

    '''<summary>
    '''A test for HasTitleBar
    '''</summary>
    <Test()> _
    Public Sub HasTitleBarTest()
        Dim wf As New IndependentWindowsFunctionsv1()
        Dim target As WindowWindowsAPI = New WindowWindowsAPI(wf)
        Dim expected As Boolean = True
        Dim actual As Boolean
        actual = target.HasTitleBar(hwnd)
        Verify.AreEqual(expected, actual)
    End Sub

    '''<summary>
    '''A test for HasMin
    '''</summary>
    <Test()> _
    Public Sub HasMinTest()
        Dim wf As New IndependentWindowsFunctionsv1()
        Dim target As WindowWindowsAPI = New WindowWindowsAPI(wf)
        Dim expected As Boolean = True
        Dim actual As Boolean
        If (target.IsWindow(hwnd)) Then
            Console.WriteLine("hwnd is actually a window")
        Else
            Console.WriteLine("hwnd is NOT actually a window")
        End If
        actual = target.HasMin(hwnd)
        Verify.AreEqual(expected, actual)
    End Sub

    '''<summary>
    '''A test for HasMax
    '''</summary>
    <Test()> _
    Public Sub HasMaxTest()
        Dim wf As New IndependentWindowsFunctionsv1()
        Dim target As WindowWindowsAPI = New WindowWindowsAPI(wf)
        Dim expected As Boolean = False
        Dim actual As Boolean
        actual = target.HasMax(hwnd)
        Verify.AreEqual(expected, actual)
    End Sub

    '''<summary>
    '''A test for HasBorder
    '''</summary>
    <Test()> _
    Public Sub HasBorderTest()
        Dim wf As New IndependentWindowsFunctionsv1()
        Dim target As WindowWindowsAPI = New WindowWindowsAPI(wf)
        Dim expected As Boolean = True
        Dim actual As Boolean
        actual = target.HasBorder(hwnd)
        Verify.AreEqual(expected, actual)
    End Sub

    ''''<summary>
    ''''A test for BringWindowToTop
    ''''</summary>
    '<Test(), _
    ' DeploymentItem("APIControls.dll")> _
    'Public Sub BringWindowToTopTest()
    '    Dim expected As Boolean = True
    '    Dim actual As Boolean
    '    actual = WindowWindowsAPI_Accessor.BringWindowToTop(hwnd)
    '    Verify.AreEqual(expected, actual)
    '    Verify.Inconclusive("Verify the correctness of this test method.")
    'End Sub
End Class
