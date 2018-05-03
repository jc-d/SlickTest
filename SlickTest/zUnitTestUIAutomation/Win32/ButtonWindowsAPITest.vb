Imports System

Imports NUnit.Framework

Imports APIControls

'''<summary>
'''This is a test class for ButtonWindowsAPITest and is intended
'''to contain all ButtonWindowsAPITest Unit Tests
'''</summary>
<TestFixture()> _
Public Class ButtonWindowsAPITest


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
        Dim target As UIControls.Description = UIControls.Description.Create("value:=""" & "6" & """", False)
        Dim a As UIControls.InterAct
        a = New UIControls.InterAct()
        hwnd = a.Window("name:=""" & "SciCalc" & """").Button(target).Hwnd
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
    '''A test for IsRadioButton
    '''</summary>
    <Test()> _
    Public Sub IsRadioButtonTest()
        Dim wf As New IndependentWindowsFunctionsv1()
        Dim target As ButtonWindowsAPI = New ButtonWindowsAPI(wf)
        Dim expected As Boolean = False
        Dim actual As Boolean
        actual = target.IsRadioButton(hwnd)
        Verify.AreEqual(expected, actual)
    End Sub

    '''<summary>
    '''A test for IsCheckBox
    '''</summary>
    <Test()> _
    Public Sub IsCheckBoxTest()
        Dim wf As New IndependentWindowsFunctionsv1()
        Dim target As ButtonWindowsAPI = New ButtonWindowsAPI(wf)
        Dim expected As Boolean = False
        Dim actual As Boolean
        actual = target.IsCheckBox(hwnd)
        Verify.AreEqual(expected, actual)
    End Sub

    '''<summary>
    '''A test for IsButton
    '''</summary>
    <Test()> _
    Public Sub IsButtonTest()
        Dim wf As New IndependentWindowsFunctionsv1()
        Dim target As ButtonWindowsAPI = New ButtonWindowsAPI(wf)
        Dim expected As Boolean = True
        Dim actual As Boolean
        actual = target.IsButton(hwnd)
        Verify.AreEqual(expected, actual)
    End Sub
End Class
