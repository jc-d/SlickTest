Imports System

Imports Microsoft.VisualStudio.TestTools.UnitTesting

Imports APIControls



'''<summary>
'''This is a test class for TextBoxWindowsAPITest and is intended
'''to contain all TextBoxWindowsAPITest Unit Tests
'''</summary>
<TestClass()> _
Public Class TextBoxWindowsAPITest


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
    <ClassInitialize()> _
    Public Shared Sub MyClassInitialize(ByVal testContext As TestContext)

    End Sub

    Public Shared Sub Init()
        p = Diagnostics.Process.Start("calc.exe")
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
    '''A test for SetLineIndex
    '''</summary>
    <TestMethod()> _
    Public Sub SetLineIndexTest()
        Dim wf As New IndependentWindowsFunctionsv1()
        Dim target As TextBoxWindowsAPI = New TextBoxWindowsAPI(wf)

        Dim Index As Integer = 0 ' TODO: Initialize to an appropriate value
        target.SetLineIndex(hwnd, Index)
        Assert.Inconclusive("Currently can't be verified, except for no exceptions.")
    End Sub

    '''<summary>
    '''A test for IsTextBox
    '''</summary>
    <TestMethod()> _
    Public Sub IsTextBoxTest()
        Dim wf As New IndependentWindowsFunctionsv1()
        Dim target As TextBoxWindowsAPI = New TextBoxWindowsAPI(wf)

        Dim expected As Boolean = True
        Dim actual As Boolean
        actual = target.IsTextBox(hwnd)
        Assert.AreEqual(expected, actual)
    End Sub

    '''<summary>
    '''A test for GetLineText
    '''</summary>
    <TestMethod()> _
    Public Sub GetLineTextTest()
        Dim wf As New IndependentWindowsFunctionsv1()
        Dim target As TextBoxWindowsAPI = New TextBoxWindowsAPI(wf)

        Dim lineNumber As Integer = 0 ' TODO: Initialize to an appropriate value
        Dim expected As String = String.Empty ' TODO: Initialize to an appropriate value
        Dim actual As String
        actual = target.GetLineText(hwnd, lineNumber)
        Assert.AreEqual("0. ", actual)
    End Sub

    '''<summary>
    '''A test for GetLineNumber
    '''</summary>
    <TestMethod()> _
    Public Sub GetLineNumberTest()
        Dim wf As New IndependentWindowsFunctionsv1()
        Dim target As TextBoxWindowsAPI = New TextBoxWindowsAPI(wf)

        Dim expected As Integer = 0 ' TODO: Initialize to an appropriate value
        Dim actual As Integer
        actual = target.GetLineNumber(hwnd)
        Assert.AreEqual(expected, actual)
    End Sub

    '''<summary>
    '''A test for GetLineLength
    '''</summary>
    <TestMethod()> _
    Public Sub GetLineLengthTest()
        Dim wf As New IndependentWindowsFunctionsv1()
        Dim target As TextBoxWindowsAPI = New TextBoxWindowsAPI(wf)

        Dim lineNumber As Integer = 0 ' TODO: Initialize to an appropriate value
        Dim expected As Integer = 3 ' TODO: Initialize to an appropriate value
        Dim actual As Integer
        actual = target.GetLineLength(hwnd, lineNumber)
        Assert.AreEqual(expected, actual)
    End Sub

    '''<summary>
    '''A test for GetLineIndexForLine
    '''</summary>
    <TestMethod()> _
    Public Sub GetLineIndexForLineTest()
        Dim wf As New IndependentWindowsFunctionsv1()
        Dim target As TextBoxWindowsAPI = New TextBoxWindowsAPI(wf)

        Dim lineNumber As Integer = 0 ' TODO: Initialize to an appropriate value
        Dim expected As Integer = 0 ' TODO: Initialize to an appropriate value
        Dim actual As Integer
        actual = target.GetLineIndexForLine(hwnd, lineNumber)
        Assert.AreEqual(expected, actual)
    End Sub

    '''<summary>
    '''A test for GetLineIndex
    '''</summary>
    <TestMethod()> _
    Public Sub GetLineIndexTest()
        Dim wf As New IndependentWindowsFunctionsv1()
        Dim target As TextBoxWindowsAPI = New TextBoxWindowsAPI(wf)

        Dim expected As Integer = 0 ' TODO: Initialize to an appropriate value
        Dim actual As Integer
        actual = target.GetCaretLineIndex(hwnd)
        Assert.AreEqual(expected, actual)
    End Sub

    '''<summary>
    '''A test for GetLineCount
    '''</summary>
    <TestMethod()> _
    Public Sub GetLineCountTest()
        Dim wf As New IndependentWindowsFunctionsv1()
        Dim target As TextBoxWindowsAPI = New TextBoxWindowsAPI(wf)

        Dim expected As Integer = 1 ' TODO: Initialize to an appropriate value
        Dim actual As Integer
        actual = target.GetLineCount(hwnd)
        Assert.AreEqual(expected, actual)
    End Sub

    '''<summary>
    '''A test for GetAllWindowText
    '''</summary>
    <TestMethod()> _
    Public Sub GetAllWindowTextTest()
        Dim wf As New IndependentWindowsFunctionsv1()
        Dim target As TextBoxWindowsAPI = New TextBoxWindowsAPI(wf)
        Dim expected As String = String.Empty ' TODO: Initialize to an appropriate value
        Dim actual As String
        actual = target.GetAllWindowText(hwnd)
        Assert.AreNotEqual("", actual)
    End Sub

End Class
