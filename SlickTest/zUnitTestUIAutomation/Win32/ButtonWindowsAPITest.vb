Imports System

Imports Microsoft.VisualStudio.TestTools.UnitTesting

Imports APIControls

Imports UIControls



'''<summary>
'''This is a test class for ButtonWindowsAPITest and is intended
'''to contain all ButtonWindowsAPITest Unit Tests
'''</summary>
<TestClass()> _
Public Class ButtonWindowsAPITest


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
        Dim target As UIControls.Description = UIControls.Description.Create("value:=""" & "6" & """", False)
        Dim a As UIControls.InterAct
        a = New UIControls.InterAct()
        hwnd = a.Window("name:=""" & "SciCalc" & """").Button(target).Hwnd
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
    '''A test for IsRadioButton
    '''</summary>
    <TestMethod()> _
    Public Sub IsRadioButtonTest()
        Dim wf As New IndependentWindowsFunctionsv1()
        Dim target As ButtonWindowsAPI = New ButtonWindowsAPI(wf)
        Dim expected As Boolean = False
        Dim actual As Boolean
        actual = target.IsRadioButton(hwnd)
        Assert.AreEqual(expected, actual)
    End Sub

    '''<summary>
    '''A test for IsCheckBox
    '''</summary>
    <TestMethod()> _
    Public Sub IsCheckBoxTest()
        Dim wf As New IndependentWindowsFunctionsv1()
        Dim target As ButtonWindowsAPI = New ButtonWindowsAPI(wf)
        Dim expected As Boolean = False
        Dim actual As Boolean
        actual = target.IsCheckBox(hwnd)
        Assert.AreEqual(expected, actual)
    End Sub

    '''<summary>
    '''A test for IsButton
    '''</summary>
    <TestMethod()> _
    Public Sub IsButtonTest()
        Dim wf As New IndependentWindowsFunctionsv1()
        Dim target As ButtonWindowsAPI = New ButtonWindowsAPI(wf)
        Dim expected As Boolean = True
        Dim actual As Boolean
        actual = target.IsButton(hwnd)
        Assert.AreEqual(expected, actual)
    End Sub
End Class
