Imports System.Collections.Generic
Imports NUnit.Framework
Imports UIControls



'''<summary>
'''This is a test class for SwfRadioButtonTest and is intended
'''to contain all SwfRadioButtonTest Unit Tests
'''</summary>
<TestFixture()> _
Public Class SwfRadioButtonTest
    Inherits DotNetTests

    Public target As SwfRadioButton

    <SetUp()> _
    Public Overrides Sub MyTestInitialize()
        MyBase.MyTestInitialize()
        target = Me.SwfWindow(TestApp.Form1_Form1).SwfRadioButton(TestApp.RadioButton1)
    End Sub

    '''<summary>
    '''A test for Select
    '''</summary>
    <Test()> _
    Public Sub SelectTest()
        target.Select()
        Verify.IsTrue(target.GetSelected())
    End Sub

    '''<summary>
    '''A test for Select
    '''</summary>
    <Test()> _
    Public Sub GetSelectedTest()
        target.Select()
        Verify.IsTrue(target.GetSelected())
        Verify.IsFalse(Me.SwfWindow(TestApp.Form1_Form1).SwfRadioButton(TestApp.RadioButton2).GetSelected())
    End Sub
End Class
