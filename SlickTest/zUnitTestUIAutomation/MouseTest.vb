Imports System.Drawing
Imports System
Imports Microsoft.VisualStudio.TestTools.UnitTesting
Imports UIControls



'''<summary>
'''This is a test class for MouseTest and is intended
'''to contain all MouseTest Unit Tests
'''</summary>
<TestClass()> _
Public Class MouseTest

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
    Private Shared target As WinObject

    Public Shared Sub CloseAll(Optional ByVal name As String = "calc")
        For Each pro As System.Diagnostics.Process In System.Diagnostics.Process.GetProcessesByName(name)
            pro.CloseMainWindow()
        Next
        System.Threading.Thread.Sleep(100)
    End Sub

    Public Shared Sub Init()
        p = Diagnostics.Process.Start("calc.exe")
        hwnd = p.MainWindowHandle
        Dim a As UIControls.InterAct
        a = New UIControls.InterAct()
        target = a.Window("name:=""" & "SciCalc" & """")
        System.Threading.Thread.Sleep(100)
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

    ''''<summary>
    ''''A test for RightDragAndDrop
    ''''</summary>
    '<TestMethod()> _
    'Public Sub RightDragAndDropTest()
    '    Dim StartPoint As Point = New Point(10, 10)
    '    Dim EndPoint As Point = New Point(10, 10)
    '    Mouse.RightDragAndDrop(StartPoint, EndPoint)
    '    Assert.Inconclusive("A method that does not return a value cannot be verified.")
    'End Sub

    '''<summary>
    '''A test for RightClick
    '''</summary>
    <TestMethod()> _
    Public Sub RightClickTest()
        Dim X As Integer = 1
        Dim Y As Integer = 1
        Mouse.RightClick(X, Y)
        Assert.Inconclusive("Currently cannot be verified, except no exception.")
    End Sub

    '''<summary>
    '''A test for RelativeToAbsCoordY
    '''</summary>
    <TestMethod()> _
    Public Sub RelativeToAbsCoordYTest()
        Dim Coord As Integer = 100
        Dim ScreenHeight As Integer = 1024
        Dim expected As Integer = 6400
        Dim actual As Integer
        actual = Mouse.RelativeToAbsCoordY(Coord, ScreenHeight)
        Assert.AreEqual(expected, actual)
    End Sub

    '''<summary>
    '''A test for RelativeToAbsCoordX
    '''</summary>
    <TestMethod()> _
    Public Sub RelativeToAbsCoordXTest()
        Dim Coord As Integer = 100
        Dim ScreenWidth As Integer = 1024
        Dim expected As Integer = 6400
        Dim actual As Integer
        actual = Mouse.RelativeToAbsCoordX(Coord, ScreenWidth)
        Assert.AreEqual(expected, actual)
    End Sub

    ''''<summary>
    ''''A test for MiddleDragAndDrop
    ''''</summary>
    '<TestMethod()> _
    'Public Sub MiddleDragAndDropTest()
    '    Dim StartPoint As Point = New Point ' TODO: Initialize to an appropriate value
    '    Dim EndPoint As Point = New Point ' TODO: Initialize to an appropriate value
    '    Mouse.MiddleDragAndDrop(StartPoint, EndPoint)
    '    Assert.Inconclusive("A method that does not return a value cannot be verified.")
    'End Sub

    '''<summary>
    '''A test for MiddleClick
    '''</summary>
    <TestMethod()> _
    Public Sub MiddleClickTest()
        Dim X As Integer = 1
        Dim Y As Integer = 1
        Mouse.MiddleClick(X, Y)
        Assert.Inconclusive("Currently cannot be verified, except no exception.")
    End Sub

    ''''<summary>
    ''''A test for LeftDragAndDrop
    ''''</summary>
    '<TestMethod()> _
    'Public Sub LeftDragAndDropTest()
    '    Dim StartPoint As Point = New Point ' TODO: Initialize to an appropriate value
    '    Dim EndPoint As Point = New Point ' TODO: Initialize to an appropriate value
    '    Mouse.LeftDragAndDrop(StartPoint, EndPoint)
    '    Assert.Inconclusive("A method that does not return a value cannot be verified.")
    'End Sub

    '''<summary>
    '''A test for LeftClick
    '''</summary>
    <TestMethod()> _
    Public Sub LeftClickTest()
        Dim X As Integer = 1
        Dim Y As Integer = 1
        Mouse.LeftClick(X, Y)
        Assert.Inconclusive("Currently cannot be verified, except no exception.")
    End Sub

    '''<summary>
    '''A test for GotoXY
    '''</summary>
    <TestMethod()> _
    Public Sub GotoXYTest()
        Dim X As Integer = 10
        Dim Y As Integer = 10
        Mouse.GotoXY(X, Y)
        Assert.AreEqual(New Point(X, Y), System.Windows.Forms.Cursor.Position)
    End Sub

    '''<summary>
    '''A test for CurrentY
    '''</summary>
    <TestMethod()> _
    Public Sub CurrentYTest()
        Dim expected As Integer = System.Windows.Forms.Cursor.Position.Y
        Dim actual As Integer
        actual = Mouse.CurrentY
        Assert.AreEqual(expected, actual)
    End Sub

    '''<summary>
    '''A test for CurrentX
    '''</summary>
    <TestMethod()> _
    Public Sub CurrentXTest()
        Dim expected As Integer = System.Windows.Forms.Cursor.Position.X
        Dim actual As Integer
        actual = Mouse.CurrentX
        Assert.AreEqual(expected, actual)
    End Sub

    ''''<summary>
    ''''A test for CurrentAbsY
    ''''</summary>
    '<TestMethod()> _
    'Public Sub CurrentAbsYTest()
    '    Dim expected As Integer = 0 ' TODO: Initialize to an appropriate value
    '    Dim actual As Integer
    '    actual = Mouse.CurrentAbsY
    '    Assert.AreEqual(expected, actual)
    '    Assert.Inconclusive("Verify the correctness of this test method.")
    'End Sub

    ''''<summary>
    ''''A test for CurrentAbsX
    ''''</summary>
    '<TestMethod()> _
    'Public Sub CurrentAbsXTest()
    '    Dim expected As Integer = 0 ' TODO: Initialize to an appropriate value
    '    Dim actual As Integer
    '    actual = Mouse.CurrentAbsX
    '    Assert.AreEqual(expected, actual)
    '    Assert.Inconclusive("Verify the correctness of this test method.")
    'End Sub

    '''<summary>
    '''A test for ClickByHwnd
    '''</summary>
    <TestMethod()> _
    Public Sub ClickByHwndTest()
        Dim Hwnd As IntPtr = New IntPtr(target.Hwnd)
        Dim expected As Boolean = True
        Dim actual As Boolean
        actual = Mouse.ClickByHwnd(Hwnd)
        Assert.AreEqual(expected, actual)
    End Sub

    ''''<summary>
    ''''A test for AbsToRelativeCoordY
    ''''</summary>
    '<TestMethod()> _
    'Public Sub AbsToRelativeCoordYTest()
    '    Dim Coord As Integer = 0 ' TODO: Initialize to an appropriate value
    '    Dim ScreenHeight As Integer = 0 ' TODO: Initialize to an appropriate value
    '    Dim expected As Integer = 0 ' TODO: Initialize to an appropriate value
    '    Dim actual As Integer
    '    actual = Mouse.AbsToRelativeCoordY(Coord, ScreenHeight)
    '    Assert.AreEqual(expected, actual)
    '    Assert.Inconclusive("Verify the correctness of this test method.")
    'End Sub

    ''''<summary>
    ''''A test for AbsToRelativeCoordX
    ''''</summary>
    '<TestMethod()> _
    'Public Sub AbsToRelativeCoordXTest()
    '    Dim Coord As Integer = 0 ' TODO: Initialize to an appropriate value
    '    Dim ScreenWidth As Integer = 0 ' TODO: Initialize to an appropriate value
    '    Dim expected As Integer = 0 ' TODO: Initialize to an appropriate value
    '    Dim actual As Integer
    '    actual = Mouse.AbsToRelativeCoordX(Coord, ScreenWidth)
    '    Assert.AreEqual(expected, actual)
    '    Assert.Inconclusive("Verify the correctness of this test method.")
    'End Sub

    ''''<summary>
    ''''A test for Mouse Constructor
    ''''</summary>
    '<TestMethod()> _
    'Public Sub MouseConstructorTest()
    '    Dim target As Mouse = New Mouse
    '    Assert.Inconclusive("TODO: Implement code to verify target")
    'End Sub
End Class
