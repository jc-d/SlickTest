Imports NUnit.Framework
Imports UIControls

'''<summary>
'''This is a test class for TextBoxTest and is intended
'''to contain all TextBoxTest Unit Tests
'''</summary>
<TestFixture()> _
Public Class TextBoxTest

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
    <TestFixtureSetUp()> _
    Public Shared Sub MyClassInitialize()

    End Sub

    Public Shared Sub Init()
        CloseAll()
        p = Diagnostics.Process.Start(System.IO.Path.GetFullPath("..\..\..\zUnitTestUIAutomation\Programs\calc.exe"))
        System.Threading.Thread.Sleep(2000)
        Dim Description As UIControls.Description = UIControls.Description.Create("name:=""" & "Edit" & """", False)
        Dim a As UIControls.InterAct
        a = New UIControls.InterAct()
        target = a.Window("name:=""" & "SciCalc" & """").TextBox(Description)
        a = Nothing
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


    '''<summary>
    '''A test for IsPasswordBox
    '''</summary>
    <Test()> _
    Public Sub IsPasswordBoxTest()
        Dim actual As Boolean
        actual = target.IsPasswordBox
        Verify.AreEqual(False, actual)
    End Sub

    '''<summary>
    '''A test for IsItalic
    '''</summary>
    <Test()> _
    Public Sub IsItalicTest()
        Dim actual As Boolean
        actual = target.IsItalic
        Verify.AreEqual(False, actual)
    End Sub

    '''<summary>
    '''A test for FontName
    '''</summary>
    <Test()> _
    Public Sub TestFontName()
        Dim actual As String
        actual = target.FontName
        Verify.AreEqual("MS Shell Dlg", actual)
    End Sub

    '''<summary>
    '''A test for FontSize
    '''</summary>
    <Test()> _
    Public Sub TestFontSize()
        Dim actual As String
        actual = target.FontSize.ToString()
        Verify.AreEqual("8", actual)
    End Sub

    '''<summary>
    '''A test for IsMultiline
    '''</summary>
    <Test()> _
    Public Sub IsMultilineTest()
        Dim actual As Boolean
        actual = target.IsMultiline
        Verify.AreEqual(False, actual)
    End Sub



    '''<summary>
    '''A test for GetLineText
    '''</summary>
    <Test()> _
    Public Sub GetLineTextTest()
        Dim LineNumber As Integer = 0
        Dim expected As String = "0. "
        Dim actual As String
        actual = target.GetLineText(LineNumber)
        Verify.AreEqual(expected, actual)
    End Sub

    '''<summary>
    '''A test for GetLineLength
    '''</summary>
    <Test()> _
    Public Sub GetLineLengthTest()
        Dim LineNumber As Integer = 0 ' TODO: Initialize to an appropriate value
        Dim expected As Integer = 3 ' TODO: Initialize to an appropriate value
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
        actual = target.GetLineCount
        Verify.AreEqual(expected, actual)
    End Sub

    '''<summary>
    '''A test for GetIndexFromCaret
    '''</summary>
    <Test()> _
    Public Sub GetIndexFromCaretTest()
        Dim expected As Integer = 0 ' TODO: Initialize to an appropriate value
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
        actual = target.GetCurrentLineNumber
        Verify.AreEqual(expected, actual)
    End Sub

End Class
