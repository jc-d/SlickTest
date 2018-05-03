Imports NUnit.Framework
Imports UIControls


'''<summary>
'''This is a test class for LogTest and is intended
'''to contain all LogTest Unit Tests
'''</summary>
<TestFixture()> _
Public Class LogTest

#Region "Additional test attributes"
    '
    'You can use the following additional attributes as you write your tests:
    '
    'Use ClassInitialize to run code before running the first test in the class
    '<ClassInitialize()>  _
    Public Shared Sub MyClassInitialize()
        Log.StoreLogLocation = LogLocation.ToFileAndConsole
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
    <TearDown()> _
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
    <Test()> _
    Public Sub FilterTest()
        Dim expected As Byte
        Dim actual As Byte
        For expected = 1 To 10
            Log.Filter = expected
            actual = Log.Filter
            Verify.AreEqual(expected, actual)
        Next
    End Sub

    '''<summary>
    '''A test for LogData
    '''</summary>
    <Test()> _
    Public Sub LogDataTest1()
        Log.StoreLogLocation = LogLocation.ToFileAndConsole
        Dim LogInfo As String = "Hello world"
        Dim PriorityFilter As Byte = 3
        Log.Filter = 1
        Log.LogData(LogInfo, PriorityFilter)
        Log.LogData(LogInfo, PriorityFilter)
        Verify.AreEqual(True, System.IO.File.Exists(Log.FileLogLocation))
        System.IO.File.Delete(Log.FileLogLocation)
    End Sub

    '''<summary>
    '''A test for LogData
    '''</summary>
    <Test()> _
    Public Sub LogDataTest()
        Log.StoreLogLocation = LogLocation.ToFileAndConsole
        Dim LogInfo As String = "Hello world"
        Log.LogData(LogInfo)
        Log.LogData(LogInfo)
        Verify.AreEqual(System.IO.File.Exists(Log.FileLogLocation), True)
        System.IO.File.Delete(Log.FileLogLocation)
    End Sub
End Class
