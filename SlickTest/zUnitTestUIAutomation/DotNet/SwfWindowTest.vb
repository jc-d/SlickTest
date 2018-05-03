Imports System
Imports System.Collections.Generic
Imports NUnit.Framework
Imports UIControls



'''<summary>
'''This is a test class for SwfWindowTest and is intended
'''to contain all SwfWindowTest Unit Tests
'''</summary>
<TestFixture()> _
Public Class SwfWindowTest
    Inherits DotNetTests

    Public target As SwfWindow

    <SetUp()> _
    Public Overrides Sub MyTestInitialize()
        MyBase.MyTestInitialize()
        target = Me.SwfWindow(TestApp.Form1_Form1)
    End Sub


    '''<summary>
    '''A test for SetSize
    '''</summary>
    <Test()> _
    Public Sub SetSizeTest()
        Dim Width As Integer = 150
        Dim Height As Integer = 150
        target.SetSize(Width, Height)
        Verify.AreEqual(Width, target.GetWidth())
        Verify.AreEqual(Height, target.GetHeight())
    End Sub

    '''<summary>
    '''A test for Move
    '''</summary>
    <Test()> _
    Public Sub MoveTest()

        Dim X As Integer = 0
        Dim Y As Integer = 0
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
        actual = target.HasMinimizeButton
        Verify.AreEqual(expected, actual)
    End Sub

    '''<summary>
    '''A test for HasMaximizeButton
    '''</summary>
    <Test()> _
    Public Sub HasMaximizeButtonTest()
        Dim expected As Boolean = True
        Dim actual As Boolean
        actual = target.HasMaximizeButton
        Verify.AreEqual(expected, actual)
    End Sub

    '''<summary>
    '''A test for HasBorder
    '''</summary>
    <Test()> _
    Public Sub HasBorderTest()
        Dim expected As Boolean = True
        Dim actual As Boolean
        actual = target.HasBorder
        Verify.AreEqual(expected, actual)
    End Sub

    '''<summary>
    '''A test for Close
    '''</summary>
    <Test()> _
    Public Sub CloseTest()
        target.Close()
        System.Threading.Thread.Sleep(200)
        Verify.IsFalse(target.Exists(2))
    End Sub
End Class
