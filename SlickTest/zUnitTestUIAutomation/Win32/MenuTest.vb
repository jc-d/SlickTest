Imports System
Imports NUnit.Framework
Imports UIControls



'''<summary>
'''This is a test class for MenuTest and is intended
'''to contain all MenuTest Unit Tests
'''</summary>
<TestFixture()> _
Public Class MenuTest

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
    <TestFixtureSetUp()> _
    Public Shared Sub MyClassInitialize()

    End Sub

    'Use ClassCleanup to run code after all tests in a class have run
    <TestFixtureTearDown()> _
    Public Shared Sub MyClassCleanup()

    End Sub

    Public Shared Sub Init()
        p = Diagnostics.Process.Start(System.IO.Path.GetFullPath("..\..\..\zUnitTestUIAutomation\Programs\calc.exe"))
        System.Threading.Thread.Sleep(2000)
        Dim a As UIControls.InterAct
        a = New UIControls.InterAct()
        target = a.Window("name:=""" & "SciCalc" & """").Menu()
        a = Nothing
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
    '''A test for SelectMenuItem
    '''</summary>
    <Test(), RequiresSTA()> _
    Public Sub SelectMenuItemTest()
        Dim MenuLocation() As String = {"Edit", "Copy"}
        Dim MouseButtonToOpenMenu As System.Windows.Forms.MouseButtons = System.Windows.Forms.MouseButtons.Left
        target.SelectMenuItem(MenuLocation, MouseButtonToOpenMenu)
        Console.WriteLine("Warning: sometimes this test fails due to issues with access to the clipboard.")
        Verify.IsTrue(System.Windows.Forms.Clipboard.GetText().Contains("0"))
    End Sub

    '''<summary>
    '''A test for IsEnabled
    '''</summary>
    <Test()> _
    Public Sub IsEnabledTest()
        Dim MenuLocation() As String = {"Edit", "Copy"}
        Dim expected As Boolean = True
        Dim actual As Boolean
        actual = target.IsEnabled(MenuLocation)
        Verify.AreEqual(expected, actual)
    End Sub

    '''<summary>
    '''A test for IsChecked
    '''</summary>
    <Test()> _
    Public Sub IsCheckedTest()
        Dim MenuLocation() As String = {"Edit", "Copy"}
        Dim expected As Boolean = False
        Dim actual As Boolean
        actual = target.IsChecked(MenuLocation)
        Verify.AreEqual(expected, actual)
    End Sub

    '''<summary>
    '''A test for Hwnd
    '''</summary>
    <Test()> _
    Public Sub HwndTest()
        Dim actual As Integer
        actual = target.Hwnd
        Verify.AreNotEqual(IntPtr.Zero, actual)
    End Sub

    '''<summary>
    '''A test for GetText
    '''</summary>
    <Test()> _
    Public Sub GetTextTest()
        Dim ItemText As String = "*Copy*"
        Dim expected As String = "&Copy Ctrl+C"
        Dim actual As String
        actual = target.GetText(ItemText)
        Verify.AreEqual(expected.Length, actual.Length)
        Verify.AreEqual(True, actual.Contains("Copy"))
        Verify.AreEqual(True, actual.Contains("Ctrl+C"))
    End Sub


    '''<summary>
    '''A test for ContainsMenuItem
    '''</summary>
    <Test()> _
    Public Sub ContainsMenuItemTest()
        Dim ItemText As String = "*Copy*"
        Dim expected As Boolean = True
        Dim actual As Boolean
        actual = target.ContainsMenuItem(ItemText)
        Verify.AreEqual(expected, actual)
    End Sub

    '''<summary>
    '''A test for ContainsMenu
    '''</summary>
    <Test()> _
    Public Sub ContainsMenuTest()
        Dim expected As Boolean = True
        Dim actual As Boolean
        actual = target.ContainsMenu
        Verify.AreEqual(expected, actual)
    End Sub

End Class
