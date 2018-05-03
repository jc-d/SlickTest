Imports NUnit.Framework
Imports UIControls

'''<summary>
'''This is a test class for WebComboBoxTest and is intended
'''to contain all WebComboBoxTest Unit Tests
'''</summary>
<TestFixture()> _
Public Class WebComboBoxTest
    Inherits WebTests


#Region "Additional test attributes"

    Public Shared target As WebComboBox

    'Use TestInitialize to run code before running each test
    <SetUp()> _
    Public Overrides Sub MyTestInitialize()
        MyBase.MyTestInitialize()
        Dim desc As Description = Description.Create()
        desc.Add(APIControls.Description.DescriptionData.Name, "MySelect")
        target = WebBrowser.WebComboBox(desc)
    End Sub
#End Region

    '''<summary>
    '''A test for Type
    '''</summary>
    <Test()> _
    Public Sub TypeTest()

        Dim expected As String = "select-one"
        Dim actual As String
        actual = target.Type
        Verify.AreEqual(expected, actual)
    End Sub

    '''<summary>
    '''A test for Size
    '''</summary>
    <Test()> _
    Public Sub SizeTest()

        Dim expected As Integer = 2
        Dim actual As Integer
        actual = target.Size
        Verify.AreEqual(expected, actual)
    End Sub

    '''<summary>
    '''A test for SelectedValue
    '''</summary>
    <Test()> _
    Public Sub SelectedValueTest()
        Dim expected As String = "One"
        Dim actual As String
        actual = target.SelectedValue
        Verify.AreEqual(expected, actual)
    End Sub

    '''<summary>
    '''A test for SelectedIndex
    '''</summary>
    <Test()> _
    Public Sub SelectedIndexTest()
        Dim expected As Integer = 0
        Dim actual As Integer
        actual = target.GetSelectedIndex
        Verify.AreEqual(expected, actual)
    End Sub

    '''<summary>
    '''A test for IsDisabled
    '''</summary>
    <Test()> _
    Public Sub IsDisabledTest()
        Dim expected As Boolean = False
        Dim actual As Boolean
        actual = target.IsDisabled
        Verify.AreEqual(expected, actual)
    End Sub

    '''<summary>
    '''A test for GetItem
    '''</summary>
    <Test()> _
    Public Sub GetItemTest1()
        Dim index As Integer = 0
        Dim expected As String = "1"
        Dim actual As WebElement
        actual = target.GetItem(index)
        Verify.AreEqual(expected, actual.GetText())
    End Sub

    '''<summary>
    '''A test for GetItem
    '''</summary>
    <Test()> _
    Public Sub GetItemTest()
        Dim IdOrName As String = "SelectOption2"
        Dim expected As WebElement = Nothing
        Dim actual As WebElement
        actual = target.GetItem(IdOrName)
        Verify.AreNotEqual(expected, actual)
    End Sub

    '''<summary>
    '''A test for Count
    '''</summary>
    <Test()> _
    Public Sub CountTest()
        Dim expected As Integer = 2
        Dim actual As Integer
        actual = target.GetItemCount()
        Verify.AreEqual(expected, actual)
    End Sub

    '''<summary>
    '''A test for AllowMultiItemSelect
    '''</summary>
    <Test()> _
    Public Sub AllowMultiItemSelectTest()
        Dim expected As Boolean = False
        Dim actual As Boolean
        actual = target.AllowMultiItemSelect
        Verify.AreEqual(expected, actual)
    End Sub
End Class
