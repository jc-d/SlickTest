Imports Microsoft.VisualStudio.TestTools.UnitTesting
Imports UIControls

'''<summary>
'''This is a test class for SwfListBoxTest and is intended
'''to contain all SwfListBoxTest Unit Tests
'''</summary>
<TestClass()> _
Public Class SwfListBoxTest
    Inherits DotNetTests

    Private Shared target As SwfListBox

    <TestInitialize()> _
    Public Overrides Sub MyTestInitialize()
        MyBase.MyTestInitialize()
        target = SwfWindow(TestApp.Form1_Form1).SwfListBox(TestApp.ListBox1)
    End Sub

    '''<summary>
    '''A test for SetSelectedItem
    '''</summary>
    <TestMethod()> _
    Public Sub SetSelectedItemTest()
        Dim itemNumber As Integer = 0
        Dim expected As Boolean = True
        Dim actual As Boolean
        actual = target.SetSelectedItem(itemNumber)
        Assert.AreEqual(expected, actual)
        Assert.AreEqual(itemNumber, target.GetSelectedItemNumber())
    End Sub

    '''<summary>
    '''A test for IsListBox
    '''</summary>
    <TestMethod()> _
    Public Sub IsListBoxTest()
        Dim expected As Boolean = True
        Dim actual As Boolean
        actual = target.IsListBox
        Assert.AreEqual(expected, actual)
    End Sub

    '''<summary>
    '''A test for GetSelectedItems
    '''</summary>
    <TestMethod()> _
    Public Sub GetSelectedItemsTest()
        Dim expected() As Integer = {-1}
        Dim actual() As Integer
        target.SetSelectedItem(0)
        actual = target.GetSelectedItems()
        Assert.AreEqual(expected(0), actual(0))
    End Sub

    '''<summary>
    '''A test for GetSelectedItemNumber
    '''</summary>
    <TestMethod()> _
    Public Sub GetSelectedItemNumberTest()
        Dim expected As Integer = 0
        Dim actual As Integer
        target.SetSelectedItem(0)
        actual = target.GetSelectedItemNumber()
        Assert.AreEqual(expected, actual)
    End Sub

    '''<summary>
    '''A test for GetSelectCount
    '''</summary>
    <TestMethod()> _
    Public Sub GetSelectCountTest()
        Dim expected As Integer = -1
        Dim actual As Integer
        actual = target.GetSelectCount()
        Assert.AreEqual(expected, actual)
    End Sub

    '''<summary>
    '''A test for GetItemCount
    '''</summary>
    <TestMethod()> _
    Public Sub GetItemCountTest()
        Dim expected As Integer = 0
        Dim actual As Integer
        actual = target.GetItemCount()
        Assert.AreNotEqual(expected, actual)
    End Sub

    '''<summary>
    '''A test for GetItemByIndex
    '''</summary>
    <TestMethod()> _
    Public Sub GetItemByIndexTest()
        Dim index As Integer = 0
        Dim expected As String = String.Empty
        Dim actual As String
        actual = target.GetItemByIndex(index)
        Assert.AreNotEqual(expected, actual)
    End Sub

    '''<summary>
    '''A test for GetAllItems
    '''</summary>
    <TestMethod()> _
    Public Sub GetAllItemsTest()
        Dim expected() As String = {}
        Dim actual() As String
        actual = target.GetAllItems()
        Assert.AreNotEqual(expected.GetLength(0), actual.GetLength(0))
    End Sub
End Class
