Imports System
Imports NUnit.Framework
Imports APIControls



'''<summary>
'''This is a test class for TextBoxWindowsAPITest and is intended
'''to contain all TextBoxWindowsAPITest Unit Tests
'''</summary>
<TestFixture()> _
Public Class TextBoxWindowsAPITest

#Region "Additional test attributes"
    Private Shared p As System.Diagnostics.Process
    Private Shared hwnd As IntPtr

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
        p = Diagnostics.Process.Start(System.IO.Path.GetFullPath("..\..\..\zUnitTestUIAutomation\Programs\calc.exe"))
        System.Threading.Thread.Sleep(2000)
        Dim target As UIControls.Description = UIControls.Description.Create("name:=""" & "Edit" & """", False)
        Console.WriteLine("Description: " & target.ToString())
        Dim a As UIControls.InterAct
        a = New UIControls.InterAct()

        hwnd = a.Window("name:=""" & "SciCalc" & """").TextBox(target).Hwnd
        Console.WriteLine("hwnd: " & hwnd.ToString())
        Console.WriteLine("window hwnd: " & a.Window("name:=""" & "SciCalc" & """").Hwnd)

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
    '''A test for SetLineIndex
    '''</summary>
    <Test()> _
    Public Sub SetLineIndexTest()
        Dim wf As New IndependentWindowsFunctionsv1()
        Dim target As TextBoxWindowsAPI = New TextBoxWindowsAPI(wf)

        Dim Index As Integer = 0 ' TODO: Initialize to an appropriate value
        target.SetLineIndex(hwnd, Index)
        Verify.IsNotEmpty("Currently can't be verified, except for no exceptions.")
    End Sub

    '''<summary>
    '''A test for IsTextBox
    '''</summary>
    <Test()> _
    Public Sub IsTextBoxTest()
        Dim wf As New IndependentWindowsFunctionsv1()
        Dim target As TextBoxWindowsAPI = New TextBoxWindowsAPI(wf)

        Dim expected As Boolean = True
        Dim actual As Boolean
        actual = target.IsTextBox(hwnd)
        Verify.AreEqual(expected, actual)
    End Sub

    '''<summary>
    '''A test for GetLineText
    '''</summary>
    <Test()> _
    Public Sub GetLineTextTest()
        Dim wf As New IndependentWindowsFunctionsv1()
        Dim target As TextBoxWindowsAPI = New TextBoxWindowsAPI(wf)

        Dim lineNumber As Integer = 0 ' TODO: Initialize to an appropriate value
        Dim expected As String = String.Empty ' TODO: Initialize to an appropriate value
        Dim actual As String
        actual = target.GetLineText(hwnd, lineNumber)
        Verify.AreEqual("0. ", actual)
    End Sub

    '''<summary>
    '''A test for GetLineNumber
    '''</summary>
    <Test()> _
    Public Sub GetLineNumberTest()
        Dim wf As New IndependentWindowsFunctionsv1()
        Dim target As TextBoxWindowsAPI = New TextBoxWindowsAPI(wf)

        Dim expected As Integer = 0 ' TODO: Initialize to an appropriate value
        Dim actual As Integer
        actual = target.GetLineNumber(hwnd)
        Verify.AreEqual(expected, actual)
    End Sub

    '''<summary>
    '''A test for GetLineLength
    '''</summary>
    <Test()> _
    Public Sub GetLineLengthTest()
        Dim wf As New IndependentWindowsFunctionsv1()
        Dim target As TextBoxWindowsAPI = New TextBoxWindowsAPI(wf)

        Dim lineNumber As Integer = 0 ' TODO: Initialize to an appropriate value
        Dim expected As Integer = 3 ' TODO: Initialize to an appropriate value
        Dim actual As Integer
        actual = target.GetLineLength(hwnd, lineNumber)
        Verify.AreEqual(expected, actual)
    End Sub

    '''<summary>
    '''A test for GetLineIndexForLine
    '''</summary>
    <Test()> _
    Public Sub GetLineIndexForLineTest()
        Dim wf As New IndependentWindowsFunctionsv1()
        Dim target As TextBoxWindowsAPI = New TextBoxWindowsAPI(wf)

        Dim lineNumber As Integer = 0 ' TODO: Initialize to an appropriate value
        Dim expected As Integer = 0 ' TODO: Initialize to an appropriate value
        Dim actual As Integer
        actual = target.GetLineIndexForLine(hwnd, lineNumber)
        Verify.AreEqual(expected, actual)
    End Sub

    '''<summary>
    '''A test for GetLineIndex
    '''</summary>
    <Test()> _
    Public Sub GetLineIndexTest()
        Dim wf As New IndependentWindowsFunctionsv1()
        Dim target As TextBoxWindowsAPI = New TextBoxWindowsAPI(wf)

        Dim expected As Integer = 0 ' TODO: Initialize to an appropriate value
        Dim actual As Integer
        actual = target.GetCaretLineIndex(hwnd)
        Verify.AreEqual(expected, actual)
    End Sub

    '''<summary>
    '''A test for GetLineCount
    '''</summary>
    <Test()> _
    Public Sub GetLineCountTest()
        Dim wf As New IndependentWindowsFunctionsv1()
        Dim target As TextBoxWindowsAPI = New TextBoxWindowsAPI(wf)

        Dim expected As Integer = 1 ' TODO: Initialize to an appropriate value
        Dim actual As Integer
        actual = target.GetLineCount(hwnd)
        Verify.AreEqual(expected, actual)
    End Sub

    '''<summary>
    '''A test for GetAllWindowText
    '''</summary>
    <Test()> _
    Public Sub GetAllWindowTextTest()
        Dim wf As New IndependentWindowsFunctionsv1()
        Dim target As TextBoxWindowsAPI = New TextBoxWindowsAPI(wf)
        Dim expected As String = String.Empty ' TODO: Initialize to an appropriate value
        Dim actual As String
        actual = target.GetAllWindowText(hwnd)
        Verify.AreNotEqual("", actual)
    End Sub

End Class
