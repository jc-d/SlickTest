Imports System.Collections.Generic
Imports Microsoft.VisualStudio.TestTools.UnitTesting
Imports UIControls

'''<summary>
'''This is a test class for CSVReaderTest and is intended
'''to contain all CSVReaderTest Unit Tests
'''</summary>
<TestClass()> _
Public Class CSVReaderTest

    Private testContextInstance As TestContext
    Public Shared FilePath As String = "C:\test.csv"
    Public Shared CSVContents As String = "Column1,Column2 ,Column3" & vbNewLine & _
    "row1,row2,row3" & vbNewLine & "line2-row1,line2-row2,line2-row3"

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
    <ClassInitialize()> _
    Public Shared Sub MyClassInitialize(ByVal testContext As TestContext)
    End Sub

    'Use ClassCleanup to run code after all tests in a class have run
    <ClassCleanup()> _
    Public Shared Sub MyClassCleanup()
    End Sub

    'Use TestInitialize to run code before running each test
    <TestInitialize()> _
    Public Sub MyTestInitialize()
        System.IO.File.WriteAllText(FilePath, CSVContents)
        CSVReader.OpenCSV(FilePath)
    End Sub

    'Use TestCleanup to run code after each test has run
    <TestCleanup()> _
    Public Sub MyTestCleanup()
        System.IO.File.Delete(FilePath)
    End Sub

#End Region

    '''<summary>
    '''A test for RowCount
    '''</summary>
    <TestMethod()> _
    Public Sub RowCountTest()
        Dim expected, actual As Integer
        expected = 3
        actual = CSVReader.RowCount
        Assert.AreEqual(expected, actual)
    End Sub

    '''<summary>
    '''A test for ColumnCount
    '''</summary>
    <TestMethod()> _
    Public Sub ColumnCountTest()
        Dim expected, actual As Integer
        expected = 3
        actual = CSVReader.ColumnCount
        Assert.AreEqual(expected, actual)
    End Sub

    '''<summary>
    '''A test for RowExists
    '''</summary>
    <TestMethod()> _
    Public Sub RowExistsTest()
        Dim RowNumber As Integer = 4
        Dim expected As Boolean = False
        Dim actual As Boolean
        actual = CSVReader.RowExists(RowNumber)
        Assert.AreEqual(expected, actual)
    End Sub

    '''<summary>
    '''A test for OpenCSV
    '''</summary>
    <TestMethod()> _
    Public Sub OpenCSVTest()
        CSVReader.OpenCSV(FilePath)
    End Sub

    '''<summary>
    '''A test for ItemExists
    '''</summary>
    <TestMethod()> _
    Public Sub ItemExistsTest()
        Dim ColumnName As String = "A"
        Dim RowNumber As Integer = 1
        Dim expected As Boolean = True
        Dim actual As Boolean
        actual = CSVReader.ItemExists(ColumnName, RowNumber)
        Assert.AreEqual(expected, actual)
    End Sub

    '''<summary>
    '''A test for GetRow
    '''</summary>
    <TestMethod()> _
    Public Sub GetRowTest()
        Dim RowNumber As Integer = 2
        Dim expected As String = "row1"
        Dim actual() As String
        actual = CSVReader.GetRow(RowNumber)
        Assert.AreEqual(expected, actual(0))
    End Sub

    '''<summary>
    '''A test for GetItem
    '''</summary>
    <TestMethod()> _
    Public Sub GetItemTest1()
        Dim ColumnName As String = "A"
        Dim RowNumber As Integer = 2
        Dim expected As String = "row1"
        Dim actual As String = CSVReader.GetItem(ColumnName, RowNumber)
        Assert.AreEqual(expected, actual)
    End Sub

    '''<summary>
    '''A test for GetItem
    '''</summary>
    <TestMethod()> _
    Public Sub GetItemTest()
        Dim ColumnNameAndRowNumber As String = "A1"
        Dim expected As String = "Column1"
        Dim actual As String
        actual = CSVReader.GetItem(ColumnNameAndRowNumber)
        Assert.AreEqual(expected, actual)
    End Sub

    '''<summary>
    '''A test for GetColumnNameFromIndex
    '''</summary>
    <TestMethod()> _
    Public Sub GetColumnNameFromIndexTest()
        Dim Index As Integer = 0
        Dim expected As String = "A"
        Dim actual As String
        actual = CSVReader.GetColumnNameFromIndex(Index)
        Assert.AreEqual(expected, actual)
    End Sub

    '''<summary>
    '''A test for GetColumn
    '''</summary>
    <TestMethod()> _
    Public Sub GetColumnTest()
        Dim ColumnName As String = "A"
        Dim expected As String = "row1"
        Dim actual() As String
        actual = CSVReader.GetColumn(ColumnName)
        Assert.AreEqual(expected, actual(1))
    End Sub

    '''<summary>
    '''A test for GetAll
    '''</summary>
    <TestMethod()> _
    Public Sub GetAllTest()
        Dim expected As String = "Column1"
        Dim actual()() As String
        actual = CSVReader.GetAll
        Assert.AreEqual(expected, actual(0)(0))
    End Sub

    '''<summary>
    '''A test for ColumnExists
    '''</summary>
    <TestMethod()> _
    Public Sub ColumnExistsTest()
        Dim ColumnName As String = "Zolumn1"
        Dim expected As Boolean = False
        Dim actual As Boolean
        actual = CSVReader.ColumnExists(ColumnName)
        Assert.AreEqual(expected, actual)
    End Sub

End Class
