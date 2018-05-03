Imports System.Collections.Generic
Imports NUnit.Framework
Imports UIControls

'''<summary>
'''This is a test class for CSVReaderTest and is intended
'''to contain all CSVReaderTest Unit Tests
'''</summary>
<TestFixture()> _
Public Class CSVReaderTest


    Public Shared FilePath As String = "C:\test.csv"
    Public Shared CSVContents As String = "Column1,Column2 ,Column3" & vbNewLine & _
    "row1,row2,row3" & vbNewLine & "line2-row1,line2-row2,line2-row3"


#Region "Additional test attributes"
    '
    'You can use the following additional attributes as you write your tests:
    '
    'Use ClassInitialize to run code before running the first test in the class
    <TestFixtureSetUp()> _
    Public Shared Sub MyClassInitialize()
    End Sub

    'Use ClassCleanup to run code after all tests in a class have run
    <TestFixtureTearDown()> _
    Public Shared Sub MyClassCleanup()
    End Sub

    'Use TestInitialize to run code before running each test
    <SetUp()> _
    Public Sub MyTestInitialize()
        System.IO.File.WriteAllText(FilePath, CSVContents)
        CSVReader.OpenCSV(FilePath)
    End Sub

    'Use TestCleanup to run code after each test has run
    <TearDown()> _
    Public Sub MyTestCleanup()
        System.IO.File.Delete(FilePath)
    End Sub

#End Region

    '''<summary>
    '''A test for RowCount
    '''</summary>
    <Test()> _
    Public Sub RowCountTest()
        Dim expected, actual As Integer
        expected = 3
        actual = CSVReader.RowCount
        Verify.AreEqual(expected, actual)
    End Sub

    '''<summary>
    '''A test for ColumnCount
    '''</summary>
    <Test()> _
    Public Sub ColumnCountTest()
        Dim expected, actual As Integer
        expected = 3
        actual = CSVReader.ColumnCount
        Verify.AreEqual(expected, actual)
    End Sub

    '''<summary>
    '''A test for RowExists
    '''</summary>
    <Test()> _
    Public Sub RowExistsTest()
        Dim RowNumber As Integer = 4
        Dim expected As Boolean = False
        Dim actual As Boolean
        actual = CSVReader.RowExists(RowNumber)
        Verify.AreEqual(expected, actual)
    End Sub

    '''<summary>
    '''A test for OpenCSV
    '''</summary>
    <Test()> _
    Public Sub OpenCSVTest()
        CSVReader.OpenCSV(FilePath)
    End Sub

    '''<summary>
    '''A test for ItemExists
    '''</summary>
    <Test()> _
    Public Sub ItemExistsTest()
        Dim ColumnName As String = "A"
        Dim RowNumber As Integer = 1
        Dim expected As Boolean = True
        Dim actual As Boolean
        actual = CSVReader.ItemExists(ColumnName, RowNumber)
        Verify.AreEqual(expected, actual)
    End Sub

    '''<summary>
    '''A test for GetRow
    '''</summary>
    <Test()> _
    Public Sub GetRowTest()
        Dim RowNumber As Integer = 2
        Dim expected As String = "row1"
        Dim actual() As String
        actual = CSVReader.GetRow(RowNumber)
        Verify.AreEqual(expected, actual(0))
    End Sub

    '''<summary>
    '''A test for GetItem
    '''</summary>
    <Test()> _
    Public Sub GetItemTest1()
        Dim ColumnName As String = "A"
        Dim RowNumber As Integer = 2
        Dim expected As String = "row1"
        Dim actual As String = CSVReader.GetItem(ColumnName, RowNumber)
        Verify.AreEqual(expected, actual)
    End Sub

    '''<summary>
    '''A test for GetItem
    '''</summary>
    <Test()> _
    Public Sub GetItemTest()
        Dim ColumnNameAndRowNumber As String = "A1"
        Dim expected As String = "Column1"
        Dim actual As String
        actual = CSVReader.GetItem(ColumnNameAndRowNumber)
        Verify.AreEqual(expected, actual)
    End Sub

    '''<summary>
    '''A test for GetColumnNameFromIndex
    '''</summary>
    <Test()> _
    Public Sub GetColumnNameFromIndexTest()
        Dim Index As Integer = 0
        Dim expected As String = "A"
        Dim actual As String
        actual = CSVReader.GetColumnNameFromIndex(Index)
        Verify.AreEqual(expected, actual)
    End Sub

    '''<summary>
    '''A test for GetColumn
    '''</summary>
    <Test()> _
    Public Sub GetColumnTest()
        Dim ColumnName As String = "A"
        Dim expected As String = "row1"
        Dim actual() As String
        actual = CSVReader.GetColumn(ColumnName)
        Verify.AreEqual(expected, actual(1))
    End Sub

    '''<summary>
    '''A test for GetAll
    '''</summary>
    <Test()> _
    Public Sub GetAllTest()
        Dim expected As String = "Column1"
        Dim actual()() As String
        actual = CSVReader.GetAll
        Verify.AreEqual(expected, actual(0)(0))
    End Sub

    '''<summary>
    '''A test for ColumnExists
    '''</summary>
    <Test()> _
    Public Sub ColumnExistsTest()
        Dim ColumnName As String = "Zolumn1"
        Dim expected As Boolean = False
        Dim actual As Boolean
        actual = CSVReader.ColumnExists(ColumnName)
        Verify.AreEqual(expected, actual)
    End Sub

End Class
