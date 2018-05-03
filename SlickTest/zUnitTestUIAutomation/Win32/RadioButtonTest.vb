Imports System.Collections.Generic

Imports Microsoft.VisualStudio.TestTools.UnitTesting

Imports UIControls



'''<summary>
'''This is a test class for RadioButtonTest and is intended
'''to contain all RadioButtonTest Unit Tests
'''</summary>
<TestClass()> _
Public Class RadioButtonTest


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
    Private Shared target As RadioButton
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
        Dim Description As UIControls.Description = UIControls.Description.Create("value:=""" & "Hex" & """", False)
        Dim a As UIControls.InterAct
        a = New UIControls.InterAct()
        target = a.Window("name:=""" & "SciCalc" & """").RadioButton(Description)
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
    '''A test for Select
    '''</summary>
    <TestMethod()> _
    Public Sub SelectTest()
        target.[Select]()
        Assert.AreEqual(True, target.GetSelected)
    End Sub

    '''<summary>
    '''A test for GetSelected
    '''</summary>
    <TestMethod()> _
    Public Sub GetSelectedTest()
        Dim expected As Boolean = True
        Dim actual As Boolean
        target.[Select]()
        actual = target.GetSelected
        Assert.AreEqual(expected, actual)
    End Sub
End Class
