Imports System.Collections.Generic
Imports Microsoft.VisualStudio.TestTools.UnitTesting
Imports UIControls

'''<summary>
'''This is a test class for SwfCheckBoxTest and is intended
'''to contain all SwfCheckBoxTest Unit Tests
'''</summary>
<TestClass()> _
Public Class SwfCheckBoxTest
    Inherits DotNetTests

    Public target As SwfCheckBox

    <TestInitialize()> _
    Public Overrides Sub MyTestInitialize()
        MyBase.MyTestInitialize()
        target = Me.SwfWindow(TestApp.Form1_Form1).SwfCheckBox(TestApp.CheckBox1)
    End Sub


    '''<summary>
    '''A test for SetChecked
    '''</summary>
    <TestMethod()> _
    Public Sub SetCheckedTest()
        Dim State As Integer = SwfCheckBox.CHECKED
        target.SetChecked(State)
        Assert.AreEqual(State, target.GetChecked())

    End Sub

    '''<summary>
    '''A test for Is3StateAuto
    '''</summary>
    <TestMethod()> _
    Public Sub Is3StateAutoTest()
        Dim expected As Boolean = False
        Dim actual As Boolean
        actual = target.Is3StateAuto()
        Assert.AreEqual(expected, actual)
    End Sub

    '''<summary>
    '''A test for Is3State
    '''</summary>
    <TestMethod()> _
    Public Sub Is3StateTest()
        Dim expected As Boolean = False
        Dim actual As Boolean
        actual = target.Is3State()
        Assert.AreEqual(expected, actual)
    End Sub

    '''<summary>
    '''A test for GetChecked
    '''</summary>
    <TestMethod()> _
    Public Sub GetCheckedTest()
        Dim expected As Integer = SwfCheckBox.UNCHECKED
        target.SetChecked(expected)
        Dim actual As Integer = target.GetChecked()
        Assert.AreEqual(expected, actual)
    End Sub

End Class
