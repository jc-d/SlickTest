Imports System.Collections.Generic
Imports Microsoft.VisualStudio.TestTools.UnitTesting
Imports UIControls

'''<summary>
'''This is a test class for TextBoxTest and is intended
'''to contain all TextBoxTest Unit Tests
'''</summary>
<TestClass()> _
Public Class TextBoxTest

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
    Private Shared target As TextBox

    Public Shared Sub CloseAll(Optional ByVal name As String = "calc")
        For Each pro As System.Diagnostics.Process In System.Diagnostics.Process.GetProcessesByName(name)
            pro.CloseMainWindow()
        Next
        System.Threading.Thread.Sleep(100)
    End Sub
    '
    'You can use the following additional attributes as you write your tests:
    '
    'Use ClassInitialize to run code before running the first test in the class
    <ClassInitialize()> _
    Public Shared Sub MyClassInitialize(ByVal testContext As TestContext)
        
    End Sub

    Public Shared Sub Init()
        CloseAll()
        p = Diagnostics.Process.Start("calc.exe")
        System.Threading.Thread.Sleep(2000)
        Dim Description As UIControls.Description = UIControls.Description.Create("name:=""" & "Edit" & """", False)
        Dim a As UIControls.InterAct
        a = New UIControls.InterAct()
        target = a.Window("name:=""" & "SciCalc" & """").TextBox(Description)
        a = Nothing
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


    '''<summary>
    '''A test for IsPasswordBox
    '''</summary>
    <TestMethod()> _
    Public Sub IsPasswordBoxTest()
        Dim actual As Boolean
        actual = target.IsPasswordBox
        Assert.AreEqual(False, actual)
    End Sub

    '''<summary>
    '''A test for IsItalic
    '''</summary>
    <TestMethod()> _
    Public Sub IsItalicTest()
        Dim actual As Boolean
        actual = target.IsItalic
        Assert.AreEqual(False, actual)
    End Sub

    '''<summary>
    '''A test for FontName
    '''</summary>
    <TestMethod()> _
    Public Sub TestFontName()
        Dim actual As String
        actual = target.FontName
        Assert.AreEqual("MS Shell Dlg", actual)
    End Sub

    '''<summary>
    '''A test for FontSize
    '''</summary>
    <TestMethod()> _
    Public Sub TestFontSize()
        Dim actual As String
        actual = target.FontSize.ToString()
        Assert.AreEqual("8", actual)
    End Sub

    '''<summary>
    '''A test for IsMultiline
    '''</summary>
    <TestMethod()> _
    Public Sub IsMultilineTest()
        Dim actual As Boolean
        actual = target.IsMultiline
        Assert.AreEqual(False, actual)
    End Sub



    '''<summary>
    '''A test for GetLineText
    '''</summary>
    <TestMethod()> _
    Public Sub GetLineTextTest()
        Dim LineNumber As Integer = 0
        Dim expected As String = "0. "
        Dim actual As String
        actual = target.GetLineText(LineNumber)
        Assert.AreEqual(expected, actual)
    End Sub

    '''<summary>
    '''A test for GetLineLength
    '''</summary>
    <TestMethod()> _
    Public Sub GetLineLengthTest()
        Dim LineNumber As Integer = 0 ' TODO: Initialize to an appropriate value
        Dim expected As Integer = 3 ' TODO: Initialize to an appropriate value
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
        actual = target.GetLineCount
        Assert.AreEqual(expected, actual)
    End Sub

    '''<summary>
    '''A test for GetIndexFromCaret
    '''</summary>
    <TestMethod()> _
    Public Sub GetIndexFromCaretTest()
        Dim expected As Integer = 0 ' TODO: Initialize to an appropriate value
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
        actual = target.GetCurrentLineNumber
        Assert.AreEqual(expected, actual)
    End Sub

End Class
