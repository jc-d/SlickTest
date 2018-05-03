'Imports System.Windows.Forms

Imports System

Imports System.Collections.Generic

Imports Microsoft.VisualStudio.TestTools.UnitTesting

Imports UIControls



'''<summary>
'''This is a test class for MenuTest and is intended
'''to contain all MenuTest Unit Tests
'''</summary>
<TestClass()> _
Public Class MenuTest


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
    Private Shared target As Menu
    Public Shared Sub CloseAll(Optional ByVal name As String = "calc")
        For Each pro As System.Diagnostics.Process In System.Diagnostics.Process.GetProcessesByName(name)
            pro.CloseMainWindow()
        Next
        System.Threading.Thread.Sleep(200)
    End Sub
    '
    'You can use the following additional attributes as you write your tests:
    '
    'Use ClassInitialize to run code before running the first test in the class
    <ClassInitialize()> _
    Public Shared Sub MyClassInitialize(ByVal testContext As TestContext)

    End Sub

    'Use ClassCleanup to run code after all tests in a class have run
    <ClassCleanup()> _
    Public Shared Sub MyClassCleanup()

    End Sub

    Public Shared Sub Init()
        p = Diagnostics.Process.Start("calc.exe")
        System.Threading.Thread.Sleep(2000)
        Dim a As UIControls.InterAct
        a = New UIControls.InterAct()
        target = a.Window("name:=""" & "SciCalc" & """").Menu()
        a = Nothing
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
    '''A test for SelectMenuItem
    '''</summary>
    <TestMethod()> _
    Public Sub SelectMenuItemTest()
        Dim MenuLocation() As String = {"Edit", "Copy"}
        Dim MouseButtonToOpenMenu As System.Windows.Forms.MouseButtons = System.Windows.Forms.MouseButtons.Left
        target.SelectMenuItem(MenuLocation, MouseButtonToOpenMenu)
        Assert.IsTrue(System.Windows.Forms.Clipboard.GetText().Contains("0"))
    End Sub

    '''<summary>
    '''A test for IsEnabled
    '''</summary>
    <TestMethod()> _
    Public Sub IsEnabledTest()
        Dim MenuLocation() As String = {"Edit", "Copy"}
        Dim expected As Boolean = True
        Dim actual As Boolean
        actual = target.IsEnabled(MenuLocation)
        Assert.AreEqual(expected, actual)
    End Sub

    '''<summary>
    '''A test for IsChecked
    '''</summary>
    <TestMethod()> _
    Public Sub IsCheckedTest()
        Dim MenuLocation() As String = {"Edit", "Copy"}
        Dim expected As Boolean = False
        Dim actual As Boolean
        actual = target.IsChecked(MenuLocation)
        Assert.AreEqual(expected, actual)
    End Sub

    '''<summary>
    '''A test for Hwnd
    '''</summary>
    <TestMethod()> _
    Public Sub HwndTest()
        Dim actual As Integer
        actual = target.Hwnd
        Assert.AreNotEqual(IntPtr.Zero, actual)
    End Sub

    '''<summary>
    '''A test for GetText
    '''</summary>
    <TestMethod()> _
    Public Sub GetTextTest()
        Dim ItemText As String = "*Copy*"
        Dim expected As String = "&Copy Ctrl+C"
        Dim actual As String
        actual = target.GetText(ItemText)
        Assert.AreEqual(expected.Length, actual.Length)
        Assert.AreEqual(True, actual.Contains("Copy"))
        Assert.AreEqual(True, actual.Contains("Ctrl+C"))
    End Sub


    '''<summary>
    '''A test for ContainsMenuItem
    '''</summary>
    <TestMethod()> _
    Public Sub ContainsMenuItemTest()
        Dim ItemText As String = "*Copy*"
        Dim expected As Boolean = True
        Dim actual As Boolean
        actual = target.ContainsMenuItem(ItemText)
        Assert.AreEqual(expected, actual)
    End Sub

    '''<summary>
    '''A test for ContainsMenu
    '''</summary>
    <TestMethod()> _
    Public Sub ContainsMenuTest()
        Dim expected As Boolean = True
        Dim actual As Boolean
        actual = target.ContainsMenu
        Assert.AreEqual(expected, actual)
    End Sub

End Class
