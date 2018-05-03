Imports System.Collections.Generic

Imports Microsoft.VisualStudio.TestTools.UnitTesting

Imports UIControls



'''<summary>
'''This is a test class for CheckBoxTest and is intended
'''to contain all CheckBoxTest Unit Tests
'''</summary>
<TestClass()> _
Public Class CheckBoxTest


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
    Private Shared target As CheckBox
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

    'Use ClassCleanup to run code after all tests in a class have run
    <ClassCleanup()> _
    Public Shared Sub MyClassCleanup()

    End Sub

    Public Shared Sub Init()
        p = Diagnostics.Process.Start("calc.exe")
        System.Threading.Thread.Sleep(2000)
        Dim Description As UIControls.Description = UIControls.Description.Create("value:=""" & "Inv" & """", False)
        Dim a As UIControls.InterAct
        a = New UIControls.InterAct()
        target = a.Window("name:=""" & "SciCalc" & """").CheckBox(Description)
        a = Nothing
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
    '''A test for UnCheck
    '''</summary>
    <TestMethod()> _
    Public Sub UnCheckTest()
        Dim expected As Integer = 0
        target.UnCheck()
        Assert.AreEqual(target.GetChecked, expected)
    End Sub

    '''<summary>
    '''A test for Toggle
    '''</summary>
    <TestMethod()> _
    Public Sub ToggleTest()
        Dim expected As Integer = target.GetChecked()
        target.Toggle()
        Assert.AreNotEqual(expected, target.GetChecked())
    End Sub

    '''<summary>
    '''A test for SetChecked
    '''</summary>
    <TestMethod()> _
    Public Sub SetCheckedTest()
        Dim State As Integer = 0
        target.SetChecked(State)
        Assert.AreEqual(State, target.GetChecked())
    End Sub

    ''''<summary>
    ''''A test for Is3StateAuto
    ''''</summary>
    '<TestMethod()> _
    'Public Sub Is3StateAutoTest()
    '    
    '    Dim expected As Boolean = False ' TODO: Initialize to an appropriate value
    '    Dim actual As Boolean
    '    actual = target.Is3StateAuto
    '    Assert.AreEqual(expected, actual)
    '    Assert.Inconclusive("Verify the correctness of this test method.")
    'End Sub

    ''''<summary>
    ''''A test for Is3State
    ''''</summary>
    '<TestMethod()> _
    'Public Sub Is3StateTest()
    '    
    '    Dim expected As Boolean = False ' TODO: Initialize to an appropriate value
    '    Dim actual As Boolean
    '    actual = target.Is3State
    '    Assert.AreEqual(expected, actual)
    '    Assert.Inconclusive("Verify the correctness of this test method.")
    'End Sub

    '''<summary>
    '''A test for GetCheckedString
    '''</summary>
    <TestMethod()> _
    Public Sub GetCheckedStringTest()
        Dim expected As String = "checked"
        Dim State As Integer = 1
        target.SetChecked(State)
        Dim actual As String
        actual = target.GetCheckedString()
        Assert.AreEqual(expected, actual)
    End Sub

    '''<summary>
    '''A test for GetChecked
    '''</summary>
    <TestMethod()> _
    Public Sub GetCheckedTest()
        Dim expected As Integer = 1 ' TODO: Initialize to an appropriate value
        Dim actual As Integer
        target.SetChecked(expected)
        actual = target.GetChecked
        Assert.AreEqual(expected, actual)
    End Sub

    '''<summary>
    '''A test for Check
    '''</summary>
    <TestMethod()> _
    Public Sub CheckTest()
        Dim expected As Integer = 1
        target.Check()
        Assert.AreEqual(target.GetChecked, expected)
    End Sub
End Class
