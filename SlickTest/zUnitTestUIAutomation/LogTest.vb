Imports Microsoft.VisualStudio.TestTools.UnitTesting

Imports UIControls



'''<summary>
'''This is a test class for LogTest and is intended
'''to contain all LogTest Unit Tests
'''</summary>
<TestClass()> _
Public Class LogTest


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
    '
    'You can use the following additional attributes as you write your tests:
    '
    'Use ClassInitialize to run code before running the first test in the class
    '<ClassInitialize()>  _
    Public Shared Sub MyClassInitialize(ByVal testContext As TestContext)
        Log.StoreLogLocation = Log.LogLocation.ToFileAndConsole
        Dim rand As New Random()

        Log.FileLogLocation = "C:\MyFile" & (rand.NextDouble() * 1002).ToString() & ".txt"
    End Sub
    '
    'Use ClassCleanup to run code after all tests in a class have run
    '<ClassCleanup()>  _
    'Public Shared Sub MyClassCleanup()
    'End Sub
    '
    'Use TestInitialize to run code before running each test
    '<TestInitialize()>  _
    'Public Sub MyTestInitialize()
    'End Sub
    '
    'Use TestCleanup to run code after each test has run
    <TestCleanup()> _
    Public Sub MyTestCleanup()
        Try
            System.IO.File.Delete(Log.FileLogLocation)
        Catch ex As Exception

        End Try
    End Sub
    '
#End Region


    '''<summary>
    '''A test for Filter
    '''</summary>
    <TestMethod()> _
    Public Sub FilterTest()
        Dim expected As Byte
        Dim actual As Byte
        For expected = 1 To 10
            Log.Filter = expected
            actual = Log.Filter
            Assert.AreEqual(expected, actual)
        Next
    End Sub

    '''<summary>
    '''A test for LogData
    '''</summary>
    <TestMethod()> _
    Public Sub LogDataTest1()
        Log.StoreLogLocation = Log.LogLocation.ToFileAndConsole
        Dim LogInfo As String = "Hello world"
        Dim PriorityFilter As Byte = 3
        Log.Filter = 1
        Log.LogData(LogInfo, PriorityFilter)
        Log.LogData(LogInfo, PriorityFilter)
        Assert.AreEqual(True, System.IO.File.Exists(Log.FileLogLocation))
        System.IO.File.Delete(Log.FileLogLocation)
    End Sub

    '''<summary>
    '''A test for LogData
    '''</summary>
    <TestMethod()> _
    Public Sub LogDataTest()
        Log.StoreLogLocation = Log.LogLocation.ToFileAndConsole
        Dim LogInfo As String = "Hello world"
        Log.LogData(LogInfo)
        Log.LogData(LogInfo)
        Assert.AreEqual(System.IO.File.Exists(Log.FileLogLocation), True)
        System.IO.File.Delete(Log.FileLogLocation)
    End Sub
End Class
