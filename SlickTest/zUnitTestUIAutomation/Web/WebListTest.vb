Imports NUnit.Framework
Imports UIControls

'''<summary>
'''This is a test class for WebListTest and is intended
'''to contain all WebListTest Unit Tests
'''</summary>
<TestFixture()> _
Public Class WebListTest
    Inherits WebTests

#Region "Additional test attributes"

    Public Shared target As WebList

    'Use TestInitialize to run code before running each test
    <SetUp()> _
    Public Overrides Sub MyTestInitialize()
        MyBase.MyTestInitialize()
        Dim desc As Description = Description.Create()
        desc.Add(APIControls.Description.DescriptionData.WebID, "ListTest")
        target = WebBrowser.WebList(desc)
    End Sub
#End Region

    '''<summary>
    '''A test for GetListItem
    '''</summary>
    <Test()> _
    Public Sub GetListItemTest()
        Dim expected As String = "Item 1"
        Dim actual As WebListItem
        actual = target.GetListItem(0)
        Verify.AreEqual(expected, actual.GetText())
    End Sub

    '''<summary>
    '''A test for GetListItem
    '''</summary>
    <Test()> _
    Public Sub GetListItemTest1()
        Dim expected As String = "Item 1"
        Dim actual As WebListItem
        actual = target.GetListItem(expected)
        Verify.AreEqual(expected, actual.GetText())
    End Sub

    '''<summary>
    '''A test for GetCompact
    '''</summary>
    <Test()> _
    Public Sub GetCompactTest()
        Dim expected As Boolean = False
        Dim actual As Boolean
        actual = target.GetCompact()
        Verify.AreEqual(expected, actual)
    End Sub
End Class