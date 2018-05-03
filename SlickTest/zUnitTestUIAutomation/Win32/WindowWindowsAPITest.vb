Imports System

Imports Microsoft.VisualStudio.TestTools.UnitTesting

Imports APIControls



'''<summary>
'''This is a test class for WindowWindowsAPITest and is intended
'''to contain all WindowWindowsAPITest Unit Tests
'''</summary>
<TestClass()> _
Public Class WindowWindowsAPITest


    Private testContextInstance As TestContext

    '''<summary>
    '''Gets or sets the test context which provides
    '''information about and functionality for the current test run.
    '''</summary>
    Public Property TestContext() As TestContext
        Get
            Return testContextInstance
        End Get
        Set(ByVal value As TestContext)
            testContextInstance = Value
        End Set
    End Property

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
    <ClassInitialize()> _
    Public Shared Sub MyClassInitialize(ByVal testContext As TestContext)
        
    End Sub

    Public Shared Sub Init()
        p = Diagnostics.Process.Start("calc.exe")
        System.Threading.Thread.Sleep(100)
        hwnd = p.MainWindowHandle
    End Sub

    'Use ClassCleanup to run code after all tests in a class have run
    <ClassCleanup()> _
    Public Shared Sub MyClassCleanup()

    End Sub

    'Use TestInitialize to run code before running each test
    <TestInitialize()> _
    Public Sub MyTestInitialize()
        Init()
    End Sub

    'Use TestCleanup to run code after each test has run
    <TestCleanup()> _
    Public Sub MyTestCleanup()
        CloseAll()
    End Sub
    '
#End Region


    '''<summary>
    '''A test for SetToForeGround
    '''</summary>
    <TestMethod()> _
    Public Sub SetToForeGroundTest()
        Dim wf As New IndependentWindowsFunctionsv1()
        Dim target As WindowWindowsAPI = New WindowWindowsAPI(wf)
        target.SetToForeGround(hwnd)
        Assert.AreEqual(hwnd, TopWindow.GetCurrentlyActiveWindowHandle())
    End Sub

    '''<summary>
    '''A test for IsWindow
    '''</summary>
    <TestMethod()> _
    Public Sub IsWindowTest()
        Dim wf As New IndependentWindowsFunctionsv1()
        Dim target As WindowWindowsAPI = New WindowWindowsAPI(wf)
        Dim expected As Boolean = True
        Dim actual As Boolean
        actual = target.IsWindow(hwnd)
        Assert.AreEqual(expected, actual)
    End Sub

    '''<summary>
    '''A test for HasTitleBar
    '''</summary>
    <TestMethod()> _
    Public Sub HasTitleBarTest()
        Dim wf As New IndependentWindowsFunctionsv1()
        Dim target As WindowWindowsAPI = New WindowWindowsAPI(wf)
        Dim expected As Boolean = True
        Dim actual As Boolean
        actual = target.HasTitleBar(hwnd)
        Assert.AreEqual(expected, actual)
    End Sub

    '''<summary>
    '''A test for HasMin
    '''</summary>
    <TestMethod()> _
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
        Assert.AreEqual(expected, actual)
    End Sub

    '''<summary>
    '''A test for HasMax
    '''</summary>
    <TestMethod()> _
    Public Sub HasMaxTest()
        Dim wf As New IndependentWindowsFunctionsv1()
        Dim target As WindowWindowsAPI = New WindowWindowsAPI(wf)
        Dim expected As Boolean = False
        Dim actual As Boolean
        actual = target.HasMax(hwnd)
        Assert.AreEqual(expected, actual)
    End Sub

    '''<summary>
    '''A test for HasBorder
    '''</summary>
    <TestMethod()> _
    Public Sub HasBorderTest()
        Dim wf As New IndependentWindowsFunctionsv1()
        Dim target As WindowWindowsAPI = New WindowWindowsAPI(wf)
        Dim expected As Boolean = True
        Dim actual As Boolean
        actual = target.HasBorder(hwnd)
        Assert.AreEqual(expected, actual)
    End Sub

    ''''<summary>
    ''''A test for BringWindowToTop
    ''''</summary>
    '<TestMethod(), _
    ' DeploymentItem("APIControls.dll")> _
    'Public Sub BringWindowToTopTest()
    '    Dim expected As Boolean = True
    '    Dim actual As Boolean
    '    actual = WindowWindowsAPI_Accessor.BringWindowToTop(hwnd)
    '    Assert.AreEqual(expected, actual)
    '    Assert.Inconclusive("Verify the correctness of this test method.")
    'End Sub
End Class
