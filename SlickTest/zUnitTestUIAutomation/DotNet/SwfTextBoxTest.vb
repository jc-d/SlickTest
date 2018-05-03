Imports System.Collections.Generic
Imports NUnit.Framework
Imports UIControls

'''<summary>
'''This is a test class for SwfTextBoxTest and is intended
'''to contain all SwfTextBoxTest Unit Tests
'''</summary>
<TestFixture()> _
Public Class SwfTextBoxTest
    Inherits DotNetTests

    Public target As SwfTextBox

    <SetUp()> _
    Public Overrides Sub MyTestInitialize()
        MyBase.MyTestInitialize()
        target = Me.SwfWindow(TestApp.Form1_Form1).SwfTextBox(TestApp.TextBox2)
    End Sub

    '''<summary>
    '''A test for IsPasswordBox
    '''</summary>
    <Test()> _
    Public Sub IsPasswordBoxTest()
        Dim actual As Boolean
        actual = target.IsPasswordBox()
        Verify.AreEqual(False, actual)
    End Sub

    '''<summary>
    '''A test for IsMultiline
    '''</summary>
    <Test()> _
    Public Sub IsMultilineTest()
        Dim actual As Boolean
        actual = target.IsMultiline()
        Verify.AreEqual(True, actual)
    End Sub

    '''<summary>
    '''A test for IsItalic
    '''</summary>
    <Test()> _
    Public Sub IsItalicTest()
        Dim actual As Boolean
        actual = target.IsItalic()
        Verify.AreEqual(False, actual)
    End Sub

    '''<summary>
    '''A test for IsReadOnly
    '''</summary>
    <Test()> _
    Public Sub IsReadOnlyTest()
        Dim actual As Boolean
        actual = target.IsReadOnly()
        Verify.AreEqual(False, actual)
    End Sub

    '''<summary>
    '''A test for FontName
    '''</summary>
    <Test()> _
    Public Sub FontNameTest()
        Dim actual As String
        Dim expected As String = "Microsoft Sans Serif"
        actual = target.FontName()
        Verify.AreEqual(expected, actual)
    End Sub

    '''<summary>
    '''A test for FontSize
    '''</summary>
    <Test()> _
    Public Sub FontSizeTest()
        Dim actual As Double
        Dim expected As Double = 8
        actual = target.FontSize()
        Verify.AreEqual(expected, actual)
    End Sub

    '''<summary>
    '''A test for GetLineText
    '''</summary>
    <Test()> _
    Public Sub GetLineTextTest()
        Dim LineNumber As Integer = 0
        Dim expected As String = "test"
        Dim actual As String
        actual = target.GetLineText(LineNumber)
        Verify.AreEqual(expected, actual)
    End Sub

    '''<summary>
    '''A test for GetLineLength
    '''</summary>
    <Test()> _
    Public Sub GetLineLengthTest()
        Dim LineNumber As Integer = 0
        Dim expected As Integer = 4
        Dim actual As Integer
        actual = target.GetLineLength(LineNumber)
        Verify.AreEqual(expected, actual)
    End Sub

    '''<summary>
    '''A test for GetLineCount
    '''</summary>
    <Test()> _
    Public Sub GetLineCountTest()
        Dim expected As Integer = 1
        Dim actual As Integer
        actual = target.GetLineCount()
        Verify.AreEqual(expected, actual)
    End Sub

    '''<summary>
    '''A test for GetIndexFromCaret
    '''</summary>
    <Test()> _
    Public Sub GetIndexFromCaretTest()
        Dim expected As Integer = 0
        Dim actual As Integer
        actual = target.GetIndexFromCaret()
        Verify.AreEqual(expected, actual)
    End Sub

    '''<summary>
    '''A test for GetCurrentLineNumber
    '''</summary>
    <Test()> _
    Public Sub GetCurrentLineNumberTest()
        Dim expected As Integer = 1
        Dim actual As Integer
        actual = target.GetCurrentLineNumber()
        Verify.AreEqual(expected, actual)
    End Sub
End Class
