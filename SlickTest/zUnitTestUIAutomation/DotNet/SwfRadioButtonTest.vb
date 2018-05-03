Imports System.Collections.Generic
Imports Microsoft.VisualStudio.TestTools.UnitTesting
Imports UIControls



'''<summary>
'''This is a test class for SwfRadioButtonTest and is intended
'''to contain all SwfRadioButtonTest Unit Tests
'''</summary>
<TestClass()> _
Public Class SwfRadioButtonTest
    Inherits DotNetTests

    Public target As SwfRadioButton

    <TestInitialize()> _
    Public Overrides Sub MyTestInitialize()
        MyBase.MyTestInitialize()
        target = Me.SwfWindow(TestApp.Form1_Form1).SwfRadioButton(TestApp.RadioButton1)
    End Sub

    '''<summary>
    '''A test for Select
    '''</summary>
    <TestMethod()> _
    Public Sub SelectTest()
        target.Select()
        Assert.IsTrue(target.GetSelected())
    End Sub

    '''<summary>
    '''A test for Select
    '''</summary>
    <TestMethod()> _
    Public Sub GetSelectedTest()
        target.Select()
        Assert.IsTrue(target.GetSelected())
        Assert.IsFalse(Me.SwfWindow(TestApp.Form1_Form1).SwfRadioButton(TestApp.RadioButton2).GetSelected())
    End Sub
End Class
