Imports NUnit.Framework
Imports UIControls



'''<summary>
'''This is a test class for WebElementTest and is intended
'''to contain all WebElementTest Unit Tests
'''</summary>
<TestFixture()> _
Public Class WebTableRowTest
    Inherits WebTests


#Region "Additional test attributes"

    Public Shared target As WebTableRow

    'Use TestInitialize to run code before running each test
    <SetUp()> _
    Public Overrides Sub MyTestInitialize()
        MyBase.MyTestInitialize()
        Dim desc As Description = Description.Create()
        desc.Add(APIControls.Description.DescriptionData.WebTag, "table")
        desc.Add(APIControls.Description.DescriptionData.WebID, "*border*")
        desc.WildCard = True

        target = WebBrowser.WebTable(desc).WebTableRow("WebTag:=""tr""")
    End Sub
#End Region

    '''<summary>
    '''A test for GetNextRow
    '''</summary>
    <Test()> _
    Public Sub GetNextRowTest()
        Dim actual As WebTableRow
        Dim html As String = target.GetOuterHtml()
        actual = target.GetNextRow()
        Verify.IsNotNull(actual)
        Dim NextRowHtml As String = actual.GetInnerHtml()
        Verify.Contains(NextRowHtml, html)
    End Sub

    '''<summary>
    '''A test for GetPreviousRow
    '''</summary>
    <Test()> _
    Public Sub GetPreviousRowTest()
        Dim actual As WebTableRow
        Dim html As String = target.GetOuterHtml()

        actual = target.GetNextRow().GetPreviousRow()
        Verify.AreEqual(html, actual.GetOuterHtml())
    End Sub

    ''''<summary>
    ''''A test for GetRowHeight
    ''''</summary>
    '<Test()> _
    'Public Sub GetRowHeightTest()
    '    Dim actual As Integer = target.GetRowHeight()
    '    Dim notExpected As Integer = 0
    '    Verify.AreNotEqual(notExpected, actual)
    'End Sub

    '''<summary>
    '''A test for GetRowIndex
    '''</summary>
    <Test()> _
    Public Sub GetRowIndexTest()
        Dim actual As Integer = target.GetNextRow().GetRowIndex()
        Dim expected As Integer = 1
        Verify.AreEqual(expected, actual)
    End Sub

    '''<summary>
    '''A test for GetCell
    '''</summary>
    <Test()> _
    Public Sub GetCellTest()
        Dim index As Integer = 0
        Dim actual As WebTableCell = target.GetCell(0)
        Dim notExpected As String = ""
        Verify.AreNotEqual(notExpected, actual.GetText())
    End Sub

    '''<summary>
    '''A test for GetCellCount
    '''</summary>
    <Test()> _
    Public Sub GetCellCountTest()
        Dim actual As Integer = target.GetCellCount()
        Dim expected As Integer = 3
        Verify.AreEqual(expected, actual)
    End Sub

    '''<summary>
    '''A test for GetParentTable
    '''</summary>
    <Test()> _
    Public Sub GetParentTableTest()
        Dim actual As WebTable = target.GetParentTable()
        Verify.IsNotNull(actual)
    End Sub

    '''<summary>
    '''A test for GetCharOffset
    '''</summary>
    <Test()> _
    Public Sub GetCharOffsetTest()
        Dim actual As String = target.GetCharOffset()
        Dim expected As String = Nothing
        Verify.AreEqual(expected, actual)
    End Sub

End Class