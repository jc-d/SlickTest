Imports System
Imports System.Collections.Generic
Imports NUnit.Framework
Imports UIControls



'''<summary>
'''This is a test class for SwfWinObjectTest and is intended
'''to contain all SwfWinObjectTest Unit Tests
'''</summary>
<TestFixture()> _
Public Class SwfWinObjectTest
    Inherits DotNetTests

    Public target As SwfWinObject

    <SetUp()> _
    Public Overrides Sub MyTestInitialize()
        MyBase.MyTestInitialize()
        target = Me.SwfWindow(TestApp.Form1_Form1).SwfWinObject(TestApp.TextBox1)
    End Sub

    '''<summary>
    '''A test for GetText
    '''</summary>
    <Test()> _
    Public Sub GetTextTest()
        Dim expected As String = ""
        Dim actual As String
        actual = target.GetText()
        Verify.AreEqual(expected, actual)
    End Sub

    '''<summary>
    '''A test for GetControlName
    '''</summary>
    <Test()> _
    Public Sub GetControlNameTest()
        Dim expected As String = "TextBox1"
        Dim actual As String
        actual = target.GetControlName
        Verify.AreEqual(expected, actual)
    End Sub

    '''<summary>
    '''A test for GetChildDescriptions
    '''</summary>
    <Test()> _
    Public Sub GetChildDescriptionsTest()
        Dim actual() As Description
        actual = target.GetChildDescriptions()
        Verify.AreEqual(0, actual.Count)
    End Sub

    '''<summary>
    '''A test for CloseParent
    '''</summary>
    <Test()> _
    Public Sub CloseParentTest()
        target.CloseParent()
        System.Threading.Thread.Sleep(200)
        Verify.IsFalse(target.Exists(2))
    End Sub

    '''<summary>
    '''A test for AppendText
    '''</summary>
    <Test()> _
    Public Sub AppendTextTest()
        Dim text As String = "Test - TextBox1"
        target.AppendText(text)
        Verify.AreEqual(text, target.GetText())
    End Sub

    '''<summary>
    '''A test for SetText
    '''</summary>
    <Test()> _
    Public Sub SetTextTest()
        Dim text As String = "Test - TextBox1"
        For Each c As Char In text.ToCharArray()
            target.SetText(c.ToString())
            Verify.AreEqual(c.ToString(), target.GetText())
        Next
        target.SetText(text)
        Verify.AreEqual(text, target.GetText())
    End Sub

    '''<summary>
    '''A test for TypeKeys
    '''</summary>
    <Test()> _
    Public Sub TypeKeysTest()
        Dim text As String = "Test - TextBox1 - "

        Dim sb As New System.Text.StringBuilder()
        For i As Integer = 33 To 126
            sb.Append(Convert.ToChar(i))
        Next
        Dim cList As String = "{~+%^*"
        text += sb.ToString()
        Dim tmpText As String = text
        For Each c As Char In cList.ToCharArray()
            tmpText = tmpText.Replace(c.ToString(), "{" & c & "}")
        Next

        target.TypeKeys(tmpText)
        System.Threading.Thread.Sleep(200)
        Verify.AreEqual(text, target.GetText())
    End Sub

    '''<summary>
    '''A test for GetIndex
    '''</summary>
    <Test()> _
    Public Sub GetIndexTest()
        Dim Index As Integer = target.GetIndex()
        Verify.IsTrue(Index > 0)
    End Sub

End Class
