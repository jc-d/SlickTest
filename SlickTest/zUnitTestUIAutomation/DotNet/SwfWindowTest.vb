Imports System
Imports System.Collections.Generic
Imports Microsoft.VisualStudio.TestTools.UnitTesting
Imports UIControls



'''<summary>
'''This is a test class for SwfWindowTest and is intended
'''to contain all SwfWindowTest Unit Tests
'''</summary>
<TestClass()> _
Public Class SwfWindowTest
    Inherits DotNetTests

    Public target As SwfWindow

    <TestInitialize()> _
    Public Overrides Sub MyTestInitialize()
        MyBase.MyTestInitialize()
        target = Me.SwfWindow(TestApp.Form1_Form1)
    End Sub


    '''<summary>
    '''A test for SetSize
    '''</summary>
    <TestMethod()> _
    Public Sub SetSizeTest()
        Dim Width As Integer = 150
        Dim Height As Integer = 150
        target.SetSize(Width, Height)
        Assert.AreEqual(Width, target.GetWidth())
        Assert.AreEqual(Height, target.GetHeight())
    End Sub

    '''<summary>
    '''A test for Move
    '''</summary>
    <TestMethod()> _
    Public Sub MoveTest()

        Dim X As Integer = 0
        Dim Y As Integer = 0
        target.Move(X, Y)
        Assert.AreEqual(X, target.GetLocationRect().X)
        Assert.AreEqual(Y, target.GetLocationRect().Y)
    End Sub

    '''<summary>
    '''A test for HasTitleBar
    '''</summary>
    <TestMethod()> _
    Public Sub HasTitleBarTest()
        Dim expected As Boolean = True
        Dim actual As Boolean
        actual = target.HasTitleBar
        Assert.AreEqual(expected, actual)
    End Sub

    '''<summary>
    '''A test for HasMinimizeButton
    '''</summary>
    <TestMethod()> _
    Public Sub HasMinimizeButtonTest()
        Dim expected As Boolean = True
        Dim actual As Boolean
        actual = target.HasMinimizeButton
        Assert.AreEqual(expected, actual)
    End Sub

    '''<summary>
    '''A test for HasMaximizeButton
    '''</summary>
    <TestMethod()> _
    Public Sub HasMaximizeButtonTest()
        Dim expected As Boolean = True
        Dim actual As Boolean
        actual = target.HasMaximizeButton
        Assert.AreEqual(expected, actual)
    End Sub

    '''<summary>
    '''A test for HasBorder
    '''</summary>
    <TestMethod()> _
    Public Sub HasBorderTest()
        Dim expected As Boolean = True
        Dim actual As Boolean
        actual = target.HasBorder
        Assert.AreEqual(expected, actual)
    End Sub

    '''<summary>
    '''A test for Close
    '''</summary>
    <TestMethod()> _
    Public Sub CloseTest()
        target.Close()
        System.Threading.Thread.Sleep(200)
        Assert.IsFalse(target.Exists(2))
    End Sub
End Class
