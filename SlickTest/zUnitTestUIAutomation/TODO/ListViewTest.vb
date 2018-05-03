'Imports Microsoft.VisualStudio.TestTools.UnitTesting

'Imports UIControls



''''<summary>
''''This is a test class for ListViewTest and is intended
''''to contain all ListViewTest Unit Tests
''''</summary>
'<TestClass()> _
'Public Class ListViewTest


'    Private testContextInstance As TestContext

'    '''<summary>
'    '''Gets or sets the test context which provides
'    '''information about and functionality for the current test run.
'    '''</summary>
'    Public Property TestContext() As TestContext
'        Get
'            Return testContextInstance
'        End Get
'        Set(ByVal value As TestContext)
'            testContextInstance = Value
'        End Set
'    End Property

'#Region "Additional test attributes"
'    '
'    'You can use the following additional attributes as you write your tests:
'    '
'    'Use ClassInitialize to run code before running the first test in the class
'    '<ClassInitialize()>  _
'    'Public Shared Sub MyClassInitialize(ByVal testContext As TestContext)
'    'End Sub
'    '
'    'Use ClassCleanup to run code after all tests in a class have run
'    '<ClassCleanup()>  _
'    'Public Shared Sub MyClassCleanup()
'    'End Sub
'    '
'    'Use TestInitialize to run code before running each test
'    '<TestInitialize()>  _
'    'Public Sub MyTestInitialize()
'    'End Sub
'    '
'    'Use TestCleanup to run code after each test has run
'    '<TestCleanup()>  _
'    'Public Sub MyTestCleanup()
'    'End Sub
'    '
'#End Region


'    '''<summary>
'    '''A test for SetSelectedRows
'    '''</summary>
'    <TestMethod(), _
'     DeploymentItem("InterAction.dll")> _
'    Public Sub SetSelectedRowsTest1()
'        Dim target As ListView_Accessor = New ListView_Accessor ' TODO: Initialize to an appropriate value
'        Dim Rows() As Integer = Nothing ' TODO: Initialize to an appropriate value
'        target.SetSelectedRows(Rows)
'        Assert.Inconclusive("A method that does not return a value cannot be verified.")
'    End Sub

'    '''<summary>
'    '''A test for SetSelectedRows
'    '''</summary>
'    <TestMethod(), _
'     DeploymentItem("InterAction.dll")> _
'    Public Sub SetSelectedRowsTest()
'        Dim target As ListView_Accessor = New ListView_Accessor ' TODO: Initialize to an appropriate value
'        Dim RowsText() As String = Nothing ' TODO: Initialize to an appropriate value
'        target.SetSelectedRows(RowsText)
'        Assert.Inconclusive("A method that does not return a value cannot be verified.")
'    End Sub

'    '''<summary>
'    '''A test for SetColumnWidth
'    '''</summary>
'    <TestMethod()> _
'    Public Sub SetColumnWidthTest1()
'        Dim target As ListView = New ListView ' TODO: Initialize to an appropriate value
'        Dim ColumnNumber As Integer = 0 ' TODO: Initialize to an appropriate value
'        Dim SizeInPixels As Integer = 0 ' TODO: Initialize to an appropriate value
'        target.SetColumnWidth(ColumnNumber, SizeInPixels)
'        Assert.Inconclusive("A method that does not return a value cannot be verified.")
'    End Sub

'    '''<summary>
'    '''A test for SetColumnWidth
'    '''</summary>
'    <TestMethod()> _
'    Public Sub SetColumnWidthTest()
'        Dim target As ListView = New ListView ' TODO: Initialize to an appropriate value
'        Dim ColumnText As String = String.Empty ' TODO: Initialize to an appropriate value
'        Dim SizeInPixels As Integer = 0 ' TODO: Initialize to an appropriate value
'        target.SetColumnWidth(ColumnText, SizeInPixels)
'        Assert.Inconclusive("A method that does not return a value cannot be verified.")
'    End Sub

'    '''<summary>
'    '''A test for SelectAll
'    '''</summary>
'    <TestMethod(), _
'     DeploymentItem("InterAction.dll")> _
'    Public Sub SelectAllTest()
'        Dim target As ListView_Accessor = New ListView_Accessor ' TODO: Initialize to an appropriate value
'        target.SelectAll()
'        Assert.Inconclusive("A method that does not return a value cannot be verified.")
'    End Sub

'    '''<summary>
'    '''A test for IsListView
'    '''</summary>
'    <TestMethod()> _
'    Public Sub IsListViewTest()
'        Dim target As ListView = New ListView ' TODO: Initialize to an appropriate value
'        Dim expected As Boolean = False ' TODO: Initialize to an appropriate value
'        Dim actual As Boolean
'        actual = target.IsListView
'        Assert.AreEqual(expected, actual)
'        Assert.Inconclusive("Verify the correctness of this test method.")
'    End Sub

'    '''<summary>
'    '''A test for GetSelectedRowsText
'    '''</summary>
'    <TestMethod()> _
'    Public Sub GetSelectedRowsTextTest()
'        Dim target As ListView = New ListView ' TODO: Initialize to an appropriate value
'        Dim expected() As String = Nothing ' TODO: Initialize to an appropriate value
'        Dim actual() As String
'        actual = target.GetSelectedRowsText
'        Assert.AreEqual(expected, actual)
'        Assert.Inconclusive("Verify the correctness of this test method.")
'    End Sub

'    '''<summary>
'    '''A test for GetSelectedRowNumbers
'    '''</summary>
'    <TestMethod()> _
'    Public Sub GetSelectedRowNumbersTest()
'        Dim target As ListView = New ListView ' TODO: Initialize to an appropriate value
'        Dim expected() As Integer = Nothing ' TODO: Initialize to an appropriate value
'        Dim actual() As Integer
'        actual = target.GetSelectedRowNumbers
'        Assert.AreEqual(expected, actual)
'        Assert.Inconclusive("Verify the correctness of this test method.")
'    End Sub

'    '''<summary>
'    '''A test for GetRowText
'    '''</summary>
'    <TestMethod()> _
'    Public Sub GetRowTextTest2()
'        Dim target As ListView = New ListView ' TODO: Initialize to an appropriate value
'        Dim RowNumber As Integer = 0 ' TODO: Initialize to an appropriate value
'        Dim expected As String = String.Empty ' TODO: Initialize to an appropriate value
'        Dim actual As String
'        actual = target.GetRowText(RowNumber)
'        Assert.AreEqual(expected, actual)
'        Assert.Inconclusive("Verify the correctness of this test method.")
'    End Sub

'    '''<summary>
'    '''A test for GetRowText
'    '''</summary>
'    <TestMethod()> _
'    Public Sub GetRowTextTest1()
'        Dim target As ListView = New ListView ' TODO: Initialize to an appropriate value
'        Dim RowNumber As Integer = 0 ' TODO: Initialize to an appropriate value
'        Dim ColumnNumber As Integer = 0 ' TODO: Initialize to an appropriate value
'        Dim expected As String = String.Empty ' TODO: Initialize to an appropriate value
'        Dim actual As String
'        actual = target.GetRowText(RowNumber, ColumnNumber)
'        Assert.AreEqual(expected, actual)
'        Assert.Inconclusive("Verify the correctness of this test method.")
'    End Sub

'    '''<summary>
'    '''A test for GetRowText
'    '''</summary>
'    <TestMethod()> _
'    Public Sub GetRowTextTest()
'        Dim target As ListView = New ListView ' TODO: Initialize to an appropriate value
'        Dim RowNumber As Integer = 0 ' TODO: Initialize to an appropriate value
'        Dim ColumnText As String = String.Empty ' TODO: Initialize to an appropriate value
'        Dim expected As String = String.Empty ' TODO: Initialize to an appropriate value
'        Dim actual As String
'        actual = target.GetRowText(RowNumber, ColumnText)
'        Assert.AreEqual(expected, actual)
'        Assert.Inconclusive("Verify the correctness of this test method.")
'    End Sub

'    '''<summary>
'    '''A test for GetRowCount
'    '''</summary>
'    <TestMethod()> _
'    Public Sub GetRowCountTest()
'        Dim target As ListView = New ListView ' TODO: Initialize to an appropriate value
'        Dim expected As Integer = 0 ' TODO: Initialize to an appropriate value
'        Dim actual As Integer
'        actual = target.GetRowCount
'        Assert.AreEqual(expected, actual)
'        Assert.Inconclusive("Verify the correctness of this test method.")
'    End Sub

'    '''<summary>
'    '''A test for GetRow
'    '''</summary>
'    <TestMethod()> _
'    Public Sub GetRowTest()
'        Dim target As ListView = New ListView ' TODO: Initialize to an appropriate value
'        Dim Row As Integer = 0 ' TODO: Initialize to an appropriate value
'        Dim expected() As String = Nothing ' TODO: Initialize to an appropriate value
'        Dim actual() As String
'        actual = target.GetRow(Row)
'        Assert.AreEqual(expected, actual)
'        Assert.Inconclusive("Verify the correctness of this test method.")
'    End Sub

'    '''<summary>
'    '''A test for GetColumnWidth
'    '''</summary>
'    <TestMethod()> _
'    Public Sub GetColumnWidthTest1()
'        Dim target As ListView = New ListView ' TODO: Initialize to an appropriate value
'        Dim ColumnText As String = String.Empty ' TODO: Initialize to an appropriate value
'        Dim expected As Integer = 0 ' TODO: Initialize to an appropriate value
'        Dim actual As Integer
'        actual = target.GetColumnWidth(ColumnText)
'        Assert.AreEqual(expected, actual)
'        Assert.Inconclusive("Verify the correctness of this test method.")
'    End Sub

'    '''<summary>
'    '''A test for GetColumnWidth
'    '''</summary>
'    <TestMethod()> _
'    Public Sub GetColumnWidthTest()
'        Dim target As ListView = New ListView ' TODO: Initialize to an appropriate value
'        Dim ColumnNumber As Integer = 0 ' TODO: Initialize to an appropriate value
'        Dim expected As Integer = 0 ' TODO: Initialize to an appropriate value
'        Dim actual As Integer
'        actual = target.GetColumnWidth(ColumnNumber)
'        Assert.AreEqual(expected, actual)
'        Assert.Inconclusive("Verify the correctness of this test method.")
'    End Sub

'    '''<summary>
'    '''A test for GetColumnHeaderText
'    '''</summary>
'    <TestMethod()> _
'    Public Sub GetColumnHeaderTextTest()
'        Dim target As ListView = New ListView ' TODO: Initialize to an appropriate value
'        Dim ColumnNumber As Integer = 0 ' TODO: Initialize to an appropriate value
'        Dim expected As String = String.Empty ' TODO: Initialize to an appropriate value
'        Dim actual As String
'        actual = target.GetColumnHeaderText(ColumnNumber)
'        Assert.AreEqual(expected, actual)
'        Assert.Inconclusive("Verify the correctness of this test method.")
'    End Sub

'    '''<summary>
'    '''A test for GetColumnCount
'    '''</summary>
'    <TestMethod()> _
'    Public Sub GetColumnCountTest()
'        Dim target As ListView = New ListView ' TODO: Initialize to an appropriate value
'        Dim expected As Integer = 0 ' TODO: Initialize to an appropriate value
'        Dim actual As Integer
'        actual = target.GetColumnCount
'        Assert.AreEqual(expected, actual)
'        Assert.Inconclusive("Verify the correctness of this test method.")
'    End Sub

'    '''<summary>
'    '''A test for GetAllFormatted
'    '''</summary>
'    <TestMethod()> _
'    Public Sub GetAllFormattedTest()
'        Dim target As ListView = New ListView ' TODO: Initialize to an appropriate value
'        Dim expected As String = String.Empty ' TODO: Initialize to an appropriate value
'        Dim actual As String
'        actual = target.GetAllFormatted
'        Assert.AreEqual(expected, actual)
'        Assert.Inconclusive("Verify the correctness of this test method.")
'    End Sub

'    '''<summary>
'    '''A test for GetAllColumnsHeaderText
'    '''</summary>
'    <TestMethod()> _
'    Public Sub GetAllColumnsHeaderTextTest()
'        Dim target As ListView = New ListView ' TODO: Initialize to an appropriate value
'        Dim expected() As String = Nothing ' TODO: Initialize to an appropriate value
'        Dim actual() As String
'        actual = target.GetAllColumnsHeaderText
'        Assert.AreEqual(expected, actual)
'        Assert.Inconclusive("Verify the correctness of this test method.")
'    End Sub

'    '''<summary>
'    '''A test for GetAll
'    '''</summary>
'    <TestMethod()> _
'    Public Sub GetAllTest()
'        Dim target As ListView = New ListView ' TODO: Initialize to an appropriate value
'        Dim expected(,) As String = Nothing ' TODO: Initialize to an appropriate value
'        Dim actual(,) As String
'        actual = target.GetAll
'        Assert.AreEqual(expected, actual)
'        Assert.Inconclusive("Verify the correctness of this test method.")
'    End Sub

'    '''<summary>
'    '''A test for ListView Constructor
'    '''</summary>
'    <TestMethod()> _
'    Public Sub ListViewConstructorTest1()
'        Dim target As ListView = New ListView
'        Assert.Inconclusive("TODO: Implement code to verify target")
'    End Sub

'    '''<summary>
'    '''A test for ListView Constructor
'    '''</summary>
'    <TestMethod()> _
'    Public Sub ListViewConstructorTest()
'        Dim win As Window = Nothing ' TODO: Initialize to an appropriate value
'        Dim target As ListView = New ListView(win)
'        Assert.Inconclusive("TODO: Implement code to verify target")
'    End Sub
'End Class
