Imports System.Drawing
Imports System
Imports NUnit.Framework
Imports UIControls

'''<summary>
'''This is a test class for MouseTest and is intended
'''to contain all MouseTest Unit Tests
'''</summary>
<TestFixture()> _
Public Class MouseTest

#Region "Additional test attributes"
    Private Shared p As System.Diagnostics.Process
    Private Shared target As WinObject

    Public Shared Sub CloseAll(Optional ByVal name As String = "calc")
        For Each pro As System.Diagnostics.Process In System.Diagnostics.Process.GetProcessesByName(name)
            pro.CloseMainWindow()
        Next
        System.Threading.Thread.Sleep(100)
    End Sub

    Public Shared Sub Init()
        p = Diagnostics.Process.Start(System.IO.Path.GetFullPath("..\..\..\zUnitTestUIAutomation\Programs\calc.exe"))
        Dim a As UIControls.InterAct
        a = New UIControls.InterAct()
        target = a.Window("name:=""" & "SciCalc" & """")
        System.Threading.Thread.Sleep(100)
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

    ''''<summary>
    ''''A test for RightDragAndDrop
    ''''</summary>
    '<Test()> _
    'Public Sub RightDragAndDropTest()
    '    Dim StartPoint As Point = New Point(10, 10)
    '    Dim EndPoint As Point = New Point(10, 10)
    '    Mouse.RightDragAndDrop(StartPoint, EndPoint)
    '    Verify.Inconclusive("A method that does not return a value cannot be verified.")
    'End Sub

    '''<summary>
    '''A test for RightClick
    '''</summary>
    <Test()> _
    Public Sub RightClickTest()
        Dim X As Integer = 1
        Dim Y As Integer = 1
        Mouse.RightClick(X, Y)
        Verify.IsNotEmpty("Currently cannot be verified, except no exception.")
    End Sub

    '''<summary>
    '''A test for RelativeToAbsCoordY
    '''</summary>
    <Test()> _
    Public Sub RelativeToAbsCoordYTest()
        Dim Coord As Integer = 100
        Dim ScreenHeight As Integer = 1024
        Dim expected As Integer = 6400
        Dim actual As Integer
        actual = Mouse.RelativeToAbsCoordY(Coord, ScreenHeight)
        Verify.AreEqual(expected, actual)
    End Sub

    '''<summary>
    '''A test for RelativeToAbsCoordX
    '''</summary>
    <Test()> _
    Public Sub RelativeToAbsCoordXTest()
        Dim Coord As Integer = 100
        Dim ScreenWidth As Integer = 1024
        Dim expected As Integer = 6400
        Dim actual As Integer
        actual = Mouse.RelativeToAbsCoordX(Coord, ScreenWidth)
        Verify.AreEqual(expected, actual)
    End Sub

    ''''<summary>
    ''''A test for MiddleDragAndDrop
    ''''</summary>
    '<Test()> _
    'Public Sub MiddleDragAndDropTest()
    '    Dim StartPoint As Point = New Point ' TODO: Initialize to an appropriate value
    '    Dim EndPoint As Point = New Point ' TODO: Initialize to an appropriate value
    '    Mouse.MiddleDragAndDrop(StartPoint, EndPoint)
    '    Verify.Inconclusive("A method that does not return a value cannot be verified.")
    'End Sub

    '''<summary>
    '''A test for MiddleClick
    '''</summary>
    <Test()> _
    Public Sub MiddleClickTest()
        Dim X As Integer = 1
        Dim Y As Integer = 1
        Mouse.MiddleClick(X, Y)
        Verify.IsNotEmpty("Currently cannot be verified, except no exception.")
    End Sub

    ''''<summary>
    ''''A test for LeftDragAndDrop
    ''''</summary>
    '<Test()> _
    'Public Sub LeftDragAndDropTest()
    '    Dim StartPoint As Point = New Point ' TODO: Initialize to an appropriate value
    '    Dim EndPoint As Point = New Point ' TODO: Initialize to an appropriate value
    '    Mouse.LeftDragAndDrop(StartPoint, EndPoint)
    '    Verify.Inconclusive("A method that does not return a value cannot be verified.")
    'End Sub

    '''<summary>
    '''A test for LeftClick
    '''</summary>
    <Test()> _
    Public Sub LeftClickTest()
        Dim X As Integer = 1
        Dim Y As Integer = 1
        Mouse.LeftClick(X, Y)
        Verify.IsNotEmpty("Currently cannot be verified, except no exception.")
    End Sub

    '''<summary>
    '''A test for GotoXY
    '''</summary>
    <Test()> _
    Public Sub GotoXYTest()
        Dim X As Integer = 10
        Dim Y As Integer = 10
        Mouse.GotoXY(X, Y)
        Verify.AreEqual(New Point(X, Y), System.Windows.Forms.Cursor.Position)
    End Sub

    '''<summary>
    '''A test for GotoXYSlowMovement
    '''</summary>
    <Test()> _
    Public Sub GotoXYSlowMovementTest()
        Dim X As Integer = 10
        Dim Y As Integer = 10
        target.AppActivateByHwnd(target.Hwnd)
        Dim rect As System.Drawing.Rectangle = _
        target.GetLocationRect()

        Mouse.GotoXY(rect.X + 60, rect.Y + 6)
        InternalMouse.LeftDown()
        System.Threading.Thread.Sleep(200)
        Mouse.GotoXY(X, Y, True)
        InternalMouse.LeftUp()
        Verify.AreEqual(New Point(X, Y), System.Windows.Forms.Cursor.Position)
        Verify.AreNotEqual(rect, target.GetLocationRect())
    End Sub

    '''<summary>
    '''A test for CurrentY
    '''</summary>
    <Test()> _
    Public Sub CurrentYTest()
        Dim expected As Integer = System.Windows.Forms.Cursor.Position.Y
        Dim actual As Integer
        actual = Mouse.CurrentY
        Verify.AreEqual(expected, actual)
    End Sub

    '''<summary>
    '''A test for CurrentX
    '''</summary>
    <Test()> _
    Public Sub CurrentXTest()
        Dim expected As Integer = System.Windows.Forms.Cursor.Position.X
        Dim actual As Integer
        actual = Mouse.CurrentX
        Verify.AreEqual(expected, actual)
    End Sub

    ''''<summary>
    ''''A test for CurrentAbsY
    ''''</summary>
    '<Test()> _
    'Public Sub CurrentAbsYTest()
    '    Dim expected As Integer = 0 ' TODO: Initialize to an appropriate value
    '    Dim actual As Integer
    '    actual = Mouse.CurrentAbsY
    '    Verify.AreEqual(expected, actual)
    '    Verify.Inconclusive("Verify the correctness of this test method.")
    'End Sub

    ''''<summary>
    ''''A test for CurrentAbsX
    ''''</summary>
    '<Test()> _
    'Public Sub CurrentAbsXTest()
    '    Dim expected As Integer = 0 ' TODO: Initialize to an appropriate value
    '    Dim actual As Integer
    '    actual = Mouse.CurrentAbsX
    '    Verify.AreEqual(expected, actual)
    '    Verify.Inconclusive("Verify the correctness of this test method.")
    'End Sub

    '''<summary>
    '''A test for ClickByHwnd
    '''</summary>
    <Test()> _
    Public Sub ClickByHwndTest()
        Dim Hwnd As IntPtr = New IntPtr(target.Hwnd)
        Dim expected As Boolean = True
        Dim actual As Boolean
        actual = Mouse.ClickByHwnd(Hwnd)
        Verify.AreEqual(expected, actual)
    End Sub

    ''''<summary>
    ''''A test for AbsToRelativeCoordY
    ''''</summary>
    '<Test()> _
    'Public Sub AbsToRelativeCoordYTest()
    '    Dim Coord As Integer = 0 ' TODO: Initialize to an appropriate value
    '    Dim ScreenHeight As Integer = 0 ' TODO: Initialize to an appropriate value
    '    Dim expected As Integer = 0 ' TODO: Initialize to an appropriate value
    '    Dim actual As Integer
    '    actual = Mouse.AbsToRelativeCoordY(Coord, ScreenHeight)
    '    Verify.AreEqual(expected, actual)
    '    Verify.Inconclusive("Verify the correctness of this test method.")
    'End Sub

    ''''<summary>
    ''''A test for AbsToRelativeCoordX
    ''''</summary>
    '<Test()> _
    'Public Sub AbsToRelativeCoordXTest()
    '    Dim Coord As Integer = 0 ' TODO: Initialize to an appropriate value
    '    Dim ScreenWidth As Integer = 0 ' TODO: Initialize to an appropriate value
    '    Dim expected As Integer = 0 ' TODO: Initialize to an appropriate value
    '    Dim actual As Integer
    '    actual = Mouse.AbsToRelativeCoordX(Coord, ScreenWidth)
    '    Verify.AreEqual(expected, actual)
    '    Verify.Inconclusive("Verify the correctness of this test method.")
    'End Sub
End Class
