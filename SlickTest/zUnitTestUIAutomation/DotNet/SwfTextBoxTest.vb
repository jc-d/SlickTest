Imports System.Collections.Generic
Imports Microsoft.VisualStudio.TestTools.UnitTesting
Imports UIControls

'''<summary>
'''This is a test class for SwfTextBoxTest and is intended
'''to contain all SwfTextBoxTest Unit Tests
'''</summary>
<TestClass()> _
Public Class SwfTextBoxTest
    Inherits DotNetTests

    Public target As SwfTextBox

    <TestInitialize()> _
    Public Overrides Sub MyTestInitialize()
        MyBase.MyTestInitialize()
        target = Me.SwfWindow(TestApp.Form1_Form1).SwfTextBox(TestApp.TextBox2)
    End Sub

    '''<summary>
    '''A test for IsPasswordBox
    '''</summary>
    <TestMethod()> _
    Public Sub IsPasswordBoxTest()
        Dim actual As Boolean
        actual = target.IsPasswordBox()
        Assert.AreEqual(False, actual)
    End Sub

    '''<summary>
    '''A test for IsMultiline
    '''</summary>
    <TestMethod()> _
    Public Sub IsMultilineTest()
        Dim actual As Boolean
        actual = target.IsMultiline()
        Assert.AreEqual(True, actual)
    End Sub

    '''<summary>
    '''A test for IsItalic
    '''</summary>
    <TestMethod()> _
    Public Sub IsItalicTest()
        Dim actual As Boolean
        actual = target.IsItalic()
        Assert.AreEqual(False, actual)
    End Sub

    '''<summary>
    '''A test for IsReadOnly
    '''</summary>
    <TestMethod()> _
    Public Sub IsReadOnlyTest()
        Dim actual As Boolean
        actual = target.IsReadOnly()
        Assert.AreEqual(False, actual)
    End Sub

    '''<summary>
    '''A test for FontName
    '''</summary>
    <TestMethod()> _
    Public Sub FontNameTest()
        Dim actual As String
        Dim expected As String = "Microsoft Sans Serif"
        actual = target.FontName()
        Assert.AreEqual(expected, actual)
    End Sub

    '''<summary>
    '''A test for FontSize
    '''</summary>
    <TestMethod()> _
    Public Sub FontSizeTest()
        Dim actual As Double
        Dim expected As Double = 8
        actual = target.FontSize()
        Assert.AreEqual(expected, actual)
    End Sub

    '''<summary>
    '''A test for GetLineText
    '''</summary>
    <TestMethod()> _
    Public Sub GetLineTextTest()
        Dim LineNumber As Integer = 0
        Dim expected As String = "test"
        Dim actual As String
        actual = target.GetLineText(LineNumber)
        Assert.AreEqual(expected, actual)
    End Sub

    '''<summary>
    '''A test for GetLineLength
    '''</summary>
    <TestMethod()> _
    Public Sub GetLineLengthTest()
        Dim LineNumber As Integer = 0
        Dim expected As Integer = 4
        Dim actual As Integer
        actual = target.GetLineLength(LineNumber)
        Assert.AreEqual(expected, actual)
    End Sub

    '''<summary>
    '''A test for GetLineCount
    '''</summary>
    <TestMethod()> _
    Public Sub GetLineCountTest()
        Dim expected As Integer = 1
        Dim actual As Integer
        actual = target.GetLineCount()
        Assert.AreEqual(expected, actual)
    End Sub

    '''<summary>
    '''A test for GetIndexFromCaret
    '''</summary>
    <TestMethod()> _
    Public Sub GetIndexFromCaretTest()
        Dim expected As Integer = 0
        Dim actual As Integer
        actual = target.GetIndexFromCaret()
        Assert.AreEqual(expected, actual)
    End Sub

    '''<summary>
    '''A test for GetCurrentLineNumber
    '''</summary>
    <TestMethod()> _
    Public Sub GetCurrentLineNumberTest()
        Dim expected As Integer = 1
        Dim actual As Integer
        actual = target.GetCurrentLineNumber()
        Assert.AreEqual(expected, actual)
    End Sub
End Class
