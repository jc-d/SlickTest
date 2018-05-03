Imports System.Drawing
Imports NUnit.Framework
Imports UIControls

'''<summary>
'''This is a test class for TabControlTest and is intended
'''to contain all TabControlTest Unit Tests
'''</summary>
<TestFixture()> _
Public Class TabControlTest

#Region "Additional test attributes"
    Private Shared p As System.Diagnostics.Process
    Private Shared target As TabControl

    Public Shared Sub Kill()
        For Each proc In System.Diagnostics.Process.GetProcessesByName("Wordpad")
            proc.Kill()
        Next
    End Sub

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
        Kill()
        p = Diagnostics.Process.Start(System.IO.Path.GetFullPath("..\..\..\zUnitTestUIAutomation\Programs\Wordpad.exe"))
        System.Threading.Thread.Sleep(2000)

        Dim a As UIControls.InterAct = New UIControls.InterAct()
        Dim desc As String = "hwnd:=""" & p.MainWindowHandle.ToString() & """"
        a.Window(desc).SetActive()
        a.Window(desc).Menu.SelectMenuItem(New String() {"View", "Options..."})
        System.Threading.Thread.Sleep(250)
        target = a.Window("name:=""#32770"";;value:=""Options""").TabControl("name:=""SysTabControl32""")

        a = Nothing
    End Sub

    'Use TestCleanup to run code after each test has run
    <TearDown()> _
    Public Sub MyTestCleanup()
        Kill()
    End Sub
    '
#End Region


    '''<summary>
    '''A test for SelectTab
    '''</summary>
    <Test()> _
    Public Sub SelectTabTest()
        Dim index As Integer = 0
        target.SelectTab(index)
        Verify.AreEqual(index, target.GetSelectedTab())
    End Sub

    '''<summary>
    '''A test for IsTabControl
    '''</summary>
    <Test()> _
    Public Sub IsTabControl()
        Dim expected As Boolean = True
        Verify.AreEqual(expected, target.IsTabControl())
    End Sub

    'Currently not functioning.
    ''''<summary>
    ''''A test for GetTabRectangle
    ''''</summary>
    '<Test()> _
    'Public Sub GetTabRectangleTest()
    '    Dim index As Integer = 0
    '    Dim expected As Rectangle = Rectangle.Empty
    '    Dim actual As Rectangle
    '    actual = target.GetTabRectangle(index)
    '    Verify.AreNotEqual(expected, actual)
    'End Sub

    '''<summary>
    '''A test for GetTabCount
    '''</summary>
    <Test()> _
    Public Sub GetTabCountTest()
        Dim expected As Integer = 6
        Dim actual As Integer
        actual = target.GetTabCount
        Verify.AreEqual(expected, actual)
    End Sub

    '''<summary>
    '''A test for GetSelectedTab
    '''</summary>
    <Test()> _
    Public Sub GetSelectedTabTest()
        Dim expected As Integer = 2 ' TODO: Initialize to an appropriate value
        Dim actual As Integer
        target.SelectTab(expected)
        actual = target.GetSelectedTab()
        Verify.AreEqual(expected, actual)
    End Sub

End Class
