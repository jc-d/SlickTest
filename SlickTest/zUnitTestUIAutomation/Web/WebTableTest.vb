Imports NUnit.Framework
Imports UIControls



'''<summary>
'''This is a test class for WebElementTest and is intended
'''to contain all WebElementTest Unit Tests
'''</summary>
<TestFixture()> _
Public Class WebTableTest
    Inherits WebTests


#Region "Additional test attributes"

    Public Shared target As WebTable

    'Use TestInitialize to run code before running each test
    <SetUp()> _
    Public Overrides Sub MyTestInitialize()
        MyBase.MyTestInitialize()
        Dim desc As Description = Description.Create()
        desc.Add(APIControls.Description.DescriptionData.WebTag, "table")
        desc.Add(APIControls.Description.DescriptionData.WebID, "TableHeader")

        target = WebBrowser.WebTable(desc)
    End Sub
#End Region

    '''<summary>
    '''A test for GetCell
    '''</summary>
    <Test()> _
    Public Sub GetCellTest()
        Dim actual As WebTableCell
        actual = target.GetCell(0, 0)
        Verify.IsNotNull(actual)
    End Sub

    '''<summary>
    '''A test for GetRowCount
    '''</summary>
    <Test()> _
    Public Sub GetRowCountTest()
        Dim actual As Integer
        Dim expected As Integer = 3
        actual = target.GetRowCount()
        Verify.AreEqual(expected, actual)
    End Sub

    '''<summary>
    '''A test for GetHeight
    '''</summary>
    <Test()> _
    Public Sub GetHeightTest()
        Dim actual As Integer
        Dim expected As Integer = 0
        actual = target.GetHeight()
        Verify.AreEqual(expected, actual)
    End Sub

    '''<summary>
    '''A test for GetWidth
    '''</summary>
    <Test()> _
    Public Sub GetWidthTest()
        Dim actual As Integer
        Dim expected As Integer = 0
        actual = target.GetWidth()
        Verify.AreEqual(expected, actual)
    End Sub

    '''<summary>
    '''A test for GetParentTable
    '''</summary>
    <Test()> _
    Public Sub GetParentTableTest()
        Dim actual As WebTable
        actual = target.GetParentTable()
        Verify.IsNull(actual)
    End Sub

    '''<summary>
    '''A test for GetTBodyRow
    '''</summary>
    <Test()> _
    Public Sub GetTBodyRowTest()
        Dim actual As WebTableRow
        actual = target.GetTBodyRow(0, 0)
        Verify.IsNotNull(actual)
    End Sub

    '''<summary>
    '''A test for GetTHeadRow
    '''</summary>
    <Test()> _
    Public Sub GetTHeadRowTest()
        Dim actual As WebTableRow
        actual = target.GetTHeadRow(0)
        Verify.IsNotNull(actual)
    End Sub

    '''<summary>
    '''A test for GetTFootRow
    '''</summary>
    <Test()> _
    Public Sub GetTFootRowTest()
        Dim actual As WebTableRow
        actual = target.GetTFootRow(0)
        Verify.IsNotNull(actual)
    End Sub

    '''<summary>
    '''A test for GetTFootRowCount
    '''</summary>
    <Test()> _
    Public Sub GetTFootRowCountTest()
        Dim actual As Integer
        Dim expected As Integer = 1
        actual = target.GetTFootRowCount()
        Verify.AreEqual(expected, actual)
    End Sub

    '''<summary>
    '''A test for GetTHeadRowCount
    '''</summary>
    <Test()> _
    Public Sub GetTHeadRowCountTest()
        Dim actual As Integer
        Dim expected As Integer = 1
        actual = target.GetTHeadRowCount()
        Verify.AreEqual(expected, actual)
    End Sub

    '''<summary>
    '''A test for GetTBodiesCount
    '''</summary>
    <Test()> _
    Public Sub GetTBodiesCountTest()
        Dim actual As Integer
        Dim expected As Integer = 1
        actual = target.GetTBodiesCount()
        Verify.AreEqual(expected, actual)
    End Sub

    '''<summary>
    '''A test for GetTBodyRowCount
    '''</summary>
    <Test()> _
    Public Sub GetTBodyRowCountTest()
        Dim actual As Integer
        Dim expected As Integer = 1
        actual = target.GetTBodyRowCount(0)
        Verify.AreEqual(expected, actual)
    End Sub
End Class
