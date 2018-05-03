Imports NUnit.Framework
Imports UIControls

'''<summary>
'''This is a test class for SwfListBoxTest and is intended
'''to contain all SwfListBoxTest Unit Tests
'''</summary>
<TestFixture()> _
Public Class SwfListBoxTest
    Inherits DotNetTests

    Private Shared target As SwfListBox

    <SetUp()> _
    Public Overrides Sub MyTestInitialize()
        MyBase.MyTestInitialize()
        target = SwfWindow(TestApp.Form1_Form1).SwfListBox(TestApp.ListBox1)
    End Sub

    '''<summary>
    '''A test for SetSelectedItem
    '''</summary>
    <Test()> _
    Public Sub SetSelectedItemTest()
        Dim itemNumber As Integer = 0
        Dim expected As Boolean = True
        Dim actual As Boolean
        actual = target.SetSelectedItem(itemNumber)
        Verify.AreEqual(expected, actual)
        Verify.AreEqual(itemNumber, target.GetSelectedItemNumber())
    End Sub

    '''<summary>
    '''A test for IsListBox
    '''</summary>
    <Test()> _
    Public Sub IsListBoxTest()
        Dim expected As Boolean = True
        Dim actual As Boolean
        actual = target.IsListBox
        Verify.AreEqual(expected, actual)
    End Sub

    '''<summary>
    '''A test for GetSelectedItems
    '''</summary>
    <Test()> _
    Public Sub GetSelectedItemsTest()
        Dim expected() As Integer = {-1}
        Dim actual() As Integer
        target.SetSelectedItem(0)
        actual = target.GetSelectedItems()
        Verify.AreEqual(expected(0), actual(0))
    End Sub

    '''<summary>
    '''A test for GetSelectedItemNumber
    '''</summary>
    <Test()> _
    Public Sub GetSelectedItemNumberTest()
        Dim expected As Integer = 0
        Dim actual As Integer
        target.SetSelectedItem(0)
        actual = target.GetSelectedItemNumber()
        Verify.AreEqual(expected, actual)
    End Sub

    '''<summary>
    '''A test for GetSelectCount
    '''</summary>
    <Test()> _
    Public Sub GetSelectCountTest()
        Dim expected As Integer = -1
        Dim actual As Integer
        actual = target.GetSelectCount()
        Verify.AreEqual(expected, actual)
    End Sub

    '''<summary>
    '''A test for GetItemCount
    '''</summary>
    <Test()> _
    Public Sub GetItemCountTest()
        Dim expected As Integer = 0
        Dim actual As Integer
        actual = target.GetItemCount()
        Verify.AreNotEqual(expected, actual)
    End Sub

    '''<summary>
    '''A test for GetItemByIndex
    '''</summary>
    <Test()> _
    Public Sub GetItemByIndexTest()
        Dim index As Integer = 0
        Dim expected As String = String.Empty
        Dim actual As String
        actual = target.GetItemByIndex(index)
        Verify.AreNotEqual(expected, actual)
    End Sub

    '''<summary>
    '''A test for GetAllItems
    '''</summary>
    <Test()> _
    Public Sub GetAllItemsTest()
        Dim expected() As String = {}
        Dim actual() As String
        actual = target.GetAllItems()
        Verify.AreNotEqual(expected.GetLength(0), actual.GetLength(0))
    End Sub
End Class
