Imports Microsoft.VisualStudio.TestTools.UnitTesting
Imports UIControls


'''<summary>
'''This is a test class for SwfComboBoxTest and is intended
'''to contain all SwfComboBoxTest Unit Tests
'''</summary>
<TestClass()> _
Public Class SwfComboBoxTest
    Inherits DotNetTests

    Public target As SwfComboBox

    <TestInitialize()> _
    Public Overrides Sub MyTestInitialize()
        MyBase.MyTestInitialize()
        target = Me.SwfWindow(TestApp.Form1_Form1).SwfComboBox(TestApp.ComboBox1)
    End Sub

    '''<summary>
    '''A test for SetSelectItem
    '''</summary>
    <TestMethod()> _
    Public Sub SetSelectItemTest()
        Dim ItemNumber As Integer = 1
        Dim expected As Boolean = True
        Dim actual As Boolean
        actual = target.SetSelectItem(ItemNumber)
        Assert.AreEqual(expected, actual)
    End Sub

    '''<summary>
    '''A test for IsComboBox
    '''</summary>
    <TestMethod()> _
    Public Sub IsComboBoxTest()
        Dim expected As Boolean = True
        Dim actual As Boolean
        actual = target.IsComboBox()
        Assert.AreEqual(expected, actual)
    End Sub

    '''<summary>
    '''A test for GetSelectedItemNumber
    '''</summary>
    <TestMethod()> _
    Public Sub GetSelectedItemNumberTest()
        Dim expected As Integer = 1
        Dim actual As Integer
        target.SetSelectItem(expected)
        System.Threading.Thread.Sleep(200)
        actual = target.GetSelectedItemNumber()
        Assert.AreEqual(expected, actual)
    End Sub

    '''<summary>
    '''A test for GetItemCount
    '''</summary>
    <TestMethod()> _
    Public Sub GetItemCountTest()
        Dim expected As Long = 3
        Dim actual As Long
        actual = target.GetItemCount()
        Assert.AreEqual(expected, actual)
    End Sub

    '''<summary>
    '''A test for GetItemByIndex
    '''</summary>
    <TestMethod()> _
    Public Sub GetItemByIndexTest()
        Dim Index As Integer = 0
        Dim expected As String = "item1"
        Dim actual As String
        actual = target.GetItemByIndex(Index)
        Assert.AreEqual(expected, actual)
    End Sub

    '''<summary>
    '''A test for GetAllItems
    '''</summary>
    <TestMethod()> _
    Public Sub GetAllItemsTest()
        Dim expected As String = "item3"
        Dim actual() As String
        actual = target.GetAllItems
        Assert.AreEqual(expected, actual(2))
    End Sub
End Class
