Imports NUnit.Framework
Imports UIControls


'''<summary>
'''This is a test class for SwfComboBoxTest and is intended
'''to contain all SwfComboBoxTest Unit Tests
'''</summary>
<TestFixture()> _
Public Class SwfComboBoxTest
    Inherits DotNetTests

    Public target As SwfComboBox

    <SetUp()> _
    Public Overrides Sub MyTestInitialize()
        MyBase.MyTestInitialize()
        target = Me.SwfWindow(TestApp.Form1_Form1).SwfComboBox(TestApp.ComboBox1)
    End Sub

    '''<summary>
    '''A test for SetSelectItem
    '''</summary>
    <Test()> _
    Public Sub SetSelectItemTest()
        Dim ItemNumber As Integer = 1
        Dim expected As Boolean = True
        Dim actual As Boolean
        actual = target.SetSelectItem(ItemNumber)
        Verify.AreEqual(expected, actual)
    End Sub

    '''<summary>
    '''A test for IsComboBox
    '''</summary>
    <Test()> _
    Public Sub IsComboBoxTest()
        Dim expected As Boolean = True
        Dim actual As Boolean
        actual = target.IsComboBox()
        Verify.AreEqual(expected, actual)
    End Sub

    '''<summary>
    '''A test for GetSelectedItemNumber
    '''</summary>
    <Test()> _
    Public Sub GetSelectedItemNumberTest()
        Dim expected As Integer = 1
        Dim actual As Integer
        target.SetSelectItem(expected)
        System.Threading.Thread.Sleep(200)
        actual = target.GetSelectedItemNumber()
        Verify.AreEqual(expected, actual)
    End Sub

    '''<summary>
    '''A test for GetItemCount
    '''</summary>
    <Test()> _
    Public Sub GetItemCountTest()
        Dim expected As Long = 3
        Dim actual As Long
        actual = target.GetItemCount()
        Verify.AreEqual(expected, actual)
    End Sub

    '''<summary>
    '''A test for GetItemByIndex
    '''</summary>
    <Test()> _
    Public Sub GetItemByIndexTest()
        Dim Index As Integer = 0
        Dim expected As String = "item1"
        Dim actual As String
        actual = target.GetItemByIndex(Index)
        Verify.AreEqual(expected, actual)
    End Sub

    '''<summary>
    '''A test for GetAllItems
    '''</summary>
    <Test()> _
    Public Sub GetAllItemsTest()
        Dim expected As String = "item3"
        Dim actual() As String
        actual = target.GetAllItems
        Verify.AreEqual(expected, actual(2))
    End Sub
End Class
