Imports NUnit.Framework
Imports UIControls



'''<summary>
'''This is a test class for WebElementTest and is intended
'''to contain all WebElementTest Unit Tests
'''</summary>
<TestFixture()> _
Public Class WebTableCellTest
    Inherits WebTests


#Region "Additional test attributes"

    Public Shared target As WebTableCell

    'Use TestInitialize to run code before running each test
    <SetUp()> _
    Public Overrides Sub MyTestInitialize()
        MyBase.MyTestInitialize()
        Dim desc As Description = Description.Create()
        desc.Add(APIControls.Description.DescriptionData.WebTag, "table")
        desc.Add(APIControls.Description.DescriptionData.WebID, "TableHeader")
        desc.WildCard = True
        '<td colSpan="2">
        target = WebBrowser.WebTable(desc).WebTableCell("WebTag:=""th""")
    End Sub
#End Region

    '''<summary>
    '''A test for GetNextRow
    '''</summary>
    <Test()> _
    Public Sub GetNextRowTest()
        Dim actual As WebTableCell
        actual = target.GetNextCell()
        Verify.IsNotNull(actual)
    End Sub

    '''<summary>
    '''A test for GetPreviousRow
    '''</summary>
    <Test()> _
    Public Sub GetPreviousRowTest()
        Dim actual As WebTableCell
        actual = target.GetNextCell().GetPreviousCell()
        Verify.IsNotNull(actual)
    End Sub

    '''<summary>
    '''A test for GetParentRow
    '''</summary>
    <Test()> _
    Public Sub GetParentRowTest()
        Dim actual As WebTableRow
        actual = target.GetParentRow()
        Verify.IsNotNull(actual)
    End Sub

    '''<summary>
    '''A test for GetParentTable
    '''</summary>
    <Test()> _
    Public Sub GetParentTableTest()
        Dim actual As WebTable
        actual = target.GetParentTable()
        Verify.IsNotNull(actual)
    End Sub

    '''<summary>
    '''A test for GetRowSpan
    '''</summary>
    <Test()> _
    Public Sub GetRowSpanTest()
        Dim actual As Integer = target.GetRowSpan()
        Dim expected As Integer = 1
        Verify.AreEqual(expected, actual)
    End Sub

    '''<summary>
    '''A test for GetColumnSpan
    '''</summary>
    <Test()> _
    Public Sub GetColumnSpanTest()
        Dim actual As Integer = target.GetColumnSpan()
        Dim expected As Integer = 1
        Verify.AreEqual(expected, actual)
    End Sub

    '''<summary>
    '''A test for GetAlign
    '''</summary>
    <Test()> _
    Public Sub GetAlignTest()
        Dim actual As String = target.GetAlign()
        Dim expected As String = "left"
        Verify.AreEqual(expected, actual)
    End Sub

    ''''<summary>
    ''''A test for GetHeaders
    ''''</summary>
    '<Test()> _
    'Public Sub GetHeadersTest()
    '    Dim actual As String = target.GetHeaders()
    '    Dim expected As String = ""
    '    Verify.AreEqual(expected, actual)
    'End Sub

    ''''<summary>
    ''''A test for GetWidth
    ''''</summary>
    '<Test()> _
    'Public Sub GetWidthTest()
    '    Dim actual As Integer = target.GetWidth()
    '    Dim expected As Integer = 0
    '    Verify.AreNotEqual(expected, actual)
    'End Sub

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