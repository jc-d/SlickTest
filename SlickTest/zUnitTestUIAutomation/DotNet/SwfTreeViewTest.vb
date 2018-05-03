Imports NUnit.Framework
Imports UIControls

'''<summary>
'''This is a test class for SwfTreeViewTest and is intended
'''to contain all SwfTreeViewTest Unit Tests
'''</summary>
<TestFixture()> _
Public Class SwfTreeViewTest
    Inherits DotNetTests

    Public target As SwfTreeView

    <SetUp()> _
    Public Overrides Sub MyTestInitialize()
        MyBase.MyTestInitialize()
        target = SwfWindow(TestApp.Form1_Form1).SwfTreeView(TestApp.TreeView1)
    End Sub


    '''<summary>
    '''A test for SetSelectedItem
    '''</summary>
    <Test()> _
    Public Sub SetSelectedItemTest1()
        Dim Text As String = "item1"
        target.SetSelectedItem(Text)
        Verify.AreEqual(Text, target.GetSelectedText())
    End Sub

    '''<summary>
    '''A test for SetSelectedItem
    '''</summary>
    <Test()> _
    Public Sub SetSelectedItemTest()
        Dim Index As Integer = 0
        target.SetSelectedItem(Index)
        Verify.AreEqual(Index, target.GetSelectedItemNumber())
    End Sub

    '''<summary>
    '''A test for IsTreeView
    '''</summary>
    <Test()> _
    Public Sub IsTreeViewTest()
        Dim expected As Boolean = True
        Dim actual As Boolean
        actual = target.IsTreeView()
        Verify.AreEqual(expected, actual)
    End Sub

    '''<summary>
    '''A test for GetSelectedText
    '''</summary>
    <Test()> _
    Public Sub GetSelectedTextTest()
        Dim expected As String = String.Empty
        Dim actual As String
        target.SetSelectedItem(0)
        actual = target.GetSelectedText()
        Verify.AreNotEqual(expected, actual)
    End Sub

    '''<summary>
    '''A test for GetSelectedItemNumber
    '''</summary>
    <Test()> _
    Public Sub GetSelectedItemNumberTest()
        Dim expected As Integer = 0
        target.SetSelectedItem(expected)
        Dim actual As Integer
        actual = target.GetSelectedItemNumber()
        Verify.AreEqual(expected, actual)
    End Sub

    '''<summary>
    '''A test for GetItemText
    '''</summary>
    <Test()> _
    Public Sub GetItemTextTest()
        Dim Index As Integer = 0
        Dim expected As String = "item1"
        Dim actual As String
        actual = target.GetItemText(Index)
        Verify.AreEqual(expected, actual)
    End Sub

    '''<summary>
    '''A test for GetItemIndex
    '''</summary>
    <Test()> _
    Public Sub GetItemIndexTest1()
        Dim Text As String = "*m1"
        Dim UseWildCard As Boolean = True
        Dim expected As Integer = 0
        Dim actual As Integer
        actual = target.GetItemIndex(Text, UseWildCard)
        Verify.AreEqual(expected, actual)
    End Sub

    '''<summary>
    '''A test for GetItemIndex
    '''</summary>
    <Test()> _
    Public Sub GetItemIndexTest()
        Dim Text As String = "item3"
        Dim expected As Integer = 3
        Dim actual As Integer
        actual = target.GetItemIndex(Text)
        Verify.AreEqual(expected, actual)
    End Sub

    '''<summary>
    '''A test for GetItemCount
    '''</summary>
    <Test()> _
    Public Sub GetItemCountTest()
        Dim expected As Integer = 4
        Dim actual As Integer
        actual = target.GetItemCount()
        Verify.AreEqual(expected, actual)
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

    '''<summary>
    '''A test for ExpandItems
    '''</summary>
    <Test()> _
    Public Sub ExpandItemsTest()
        target.CollapseItem(0)
        Dim ItemsText() As String = {"item1"}
        target.ExpandItems(ItemsText)
        Dim exception As Exception = Nothing
        Try
            target.SetSelectedItem(1)
        Catch ex As Exception
            exception = ex
        End Try
        Verify.AreEqual(Nothing, exception)
    End Sub

    '''<summary>
    '''A test for ExpandItem
    '''</summary>
    <Test()> _
    Public Sub ExpandItemTest1()
        target.CollapseItem(0)
        Dim ItemText As String = "item1"
        target.ExpandItem(ItemText)
        Dim exception As Exception = Nothing
        Try
            target.SetSelectedItem(1)
        Catch ex As Exception
            exception = ex
        End Try
        Verify.AreEqual(Nothing, exception)
    End Sub

    '''<summary>
    '''A test for ExpandItem
    '''</summary>
    <Test()> _
    Public Sub ExpandItemTest()
        target.CollapseItem(0)
        Dim ItemIndex As Integer = 0
        target.ExpandItem(ItemIndex)
        Dim exception As Exception = Nothing
        Try
            target.SetSelectedItem(1)
        Catch ex As Exception
            exception = ex
        End Try
        Verify.AreEqual(Nothing, exception)
    End Sub

    '''<summary>
    '''A test for CollapseItems
    '''</summary>
    <Test()> _
    Public Sub CollapseItemsTest()
        Dim ItemsText() As String = {"item1"}
        target.CollapseItems(ItemsText)
        Dim exception As Exception = Nothing
        Try
            target.SetSelectedItem(1)
        Catch ex As Exception
            exception = ex
        End Try
        Verify.AreNotEqual(Nothing, exception)
    End Sub

    '''<summary>
    '''A test for CollapseItem
    '''</summary>
    <Test()> _
    Public Sub CollapseItemTest1()
        Dim ItemText As String = "item1"
        target.CollapseItem(ItemText)
        Dim exception As Exception = Nothing
        Try
            target.SetSelectedItem(1)
        Catch ex As Exception
            Exception = ex
        End Try
        Verify.AreNotEqual(Nothing, Exception)
    End Sub

    '''<summary>
    '''A test for CollapseItem
    '''</summary>
    <Test()> _
    Public Sub CollapseItemTest()
        Dim ItemIndex As Integer = 0
        target.CollapseItem(ItemIndex)
        Dim exception As Exception = Nothing
        Try
            target.SetSelectedItem(1)
        Catch ex As Exception
            exception = ex
        End Try
        Verify.AreNotEqual(Nothing, exception)
    End Sub
End Class
