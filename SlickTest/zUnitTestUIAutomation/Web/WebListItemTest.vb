Imports NUnit.Framework
Imports UIControls

'''<summary>
'''This is a test class for WebListItemTest and is intended
'''to contain all WebListItemTest Unit Tests
'''</summary>
<TestFixture()> _
Public Class WebListItemTest
    Inherits WebTests

#Region "Additional test attributes"

    Public Shared target As WebListItem

    'Use TestInitialize to run code before running each test
    <SetUp()> _
    Public Overrides Sub MyTestInitialize()
        MyBase.MyTestInitialize()
        Dim desc As Description = Description.Create()
        desc.Add(APIControls.Description.DescriptionData.WebTag, "li")
        target = WebBrowser.WebListItem(desc)
    End Sub
#End Region

    '''<summary>
    '''A test for GetListValue
    '''</summary>
    <Test()> _
    Public Sub GetListValueTest()
        Dim expected As Integer = 2
        Dim actual As Integer
        actual = target.GetListValue()
        Verify.AreEqual(expected, actual)
    End Sub

    '''<summary>
    '''A test for GetItemType
    '''</summary>
    <Test()> _
    Public Sub GetItemTypeTest()
        Dim expected As String = Nothing
        Dim actual As String
        actual = target.GetItemType()
        Verify.AreEqual(expected, actual)
    End Sub
End Class
