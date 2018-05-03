Imports System.Drawing
Imports Microsoft.VisualStudio.TestTools.UnitTesting
Imports UIControls

'''<summary>
'''This is a test class for TabControlTest and is intended
'''to contain all TabControlTest Unit Tests
'''</summary>
<TestClass()> _
Public Class TabControlTest

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
    Private Shared target As TabControl

    Public Shared Sub Kill()
        For Each proc In System.Diagnostics.Process.GetProcessesByName("Wordpad")
            proc.Kill()
        Next
    End Sub

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

    'Use TestInitialize to run code before running each test
    <TestInitialize()> _
    Public Sub MyTestInitialize()
        Kill()
        p = Diagnostics.Process.Start("Wordpad.exe")
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
    <TestCleanup()> _
    Public Sub MyTestCleanup()
        Kill()
    End Sub
    '
#End Region


    '''<summary>
    '''A test for SelectTab
    '''</summary>
    <TestMethod()> _
    Public Sub SelectTabTest()
        Dim index As Integer = 0
        target.SelectTab(index)
        Assert.AreEqual(index, target.GetSelectedTab())
    End Sub

    '''<summary>
    '''A test for IsTabControl
    '''</summary>
    <TestMethod()> _
    Public Sub IsTabControl()
        Dim expected As Boolean = True
        Assert.AreEqual(expected, target.IsTabControl())
    End Sub

    'Currently not functioning.
    ''''<summary>
    ''''A test for GetTabRectangle
    ''''</summary>
    '<TestMethod()> _
    'Public Sub GetTabRectangleTest()
    '    Dim index As Integer = 0
    '    Dim expected As Rectangle = Rectangle.Empty
    '    Dim actual As Rectangle
    '    actual = target.GetTabRectangle(index)
    '    Assert.AreNotEqual(expected, actual)
    'End Sub

    '''<summary>
    '''A test for GetTabCount
    '''</summary>
    <TestMethod()> _
    Public Sub GetTabCountTest()
        Dim expected As Integer = 4
        Dim actual As Integer
        actual = target.GetTabCount
        Assert.AreEqual(expected, actual)
    End Sub

    '''<summary>
    '''A test for GetSelectedTab
    '''</summary>
    <TestMethod()> _
    Public Sub GetSelectedTabTest()
        Dim expected As Integer = 2 ' TODO: Initialize to an appropriate value
        Dim actual As Integer
        target.SelectTab(expected)
        actual = target.GetSelectedTab()
        Assert.AreEqual(expected, actual)
    End Sub

End Class
