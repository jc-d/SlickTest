Imports Microsoft.VisualStudio.TestTools.UnitTesting
Imports UIControls

'''<summary>
'''This is a test class for ListBoxTest and is intended
'''to contain all ListBoxTest Unit Tests
'''</summary>
<TestClass()> _
Public Class ListBoxTest

    Private testContextInstance As TestContext

    '''<summary>
    '''Gets or sets the test context which provides
    '''information about and functionality for the current test run.
    '''</summary>
    Public Property TestContext() As TestContext
        Get
            Return testContextInstance
        End Get
        Set(ByVal value As TestContext)
            testContextInstance = Value
        End Set
    End Property

#Region "Additional test attributes"
    Private Shared p As System.Diagnostics.Process
    Private Shared target As ListBox

    Public Shared Sub Kill()
        For Each proc In System.Diagnostics.Process.GetProcessesByName("Notepad")
            proc.Kill()
        Next
    End Sub

    'You can use the following additional attributes as you write your tests:
    '
    'Use ClassInitialize to run code before running the first test in the class
    <ClassInitialize()> _
    Public Shared Sub MyClassInitialize(ByVal testContext As TestContext)
    End Sub

    'Use ClassCleanup to run code after all tests in a class have run
    <ClassCleanup()> _
    Public Shared Sub MyClassCleanup()
    End Sub

    'Use TestInitialize to run code before running each test
    <TestInitialize()> _
    Public Sub MyTestInitialize()
        Kill()
        p = Diagnostics.Process.Start("Notepad.exe")
        System.Threading.Thread.Sleep(2000)
        Dim Description As UIControls.Description = UIControls.Description.Create( _
        "windowtype:=""combobox"";;controlid:=""1140""", False)

        Dim a As UIControls.InterAct = New UIControls.InterAct()
        Dim desc As String = "hwnd:=""" & p.MainWindowHandle.ToString() & """"
        a.Window(desc).SetActive()
        a.Window(desc).Menu.SelectMenuItem(New String() {"Format", "Font..."})
        System.Threading.Thread.Sleep(250)
        target = a.Window("name:=""#32770"";;value:=""Font"""). _
        ComboBox("name:=""ComboBox"";;nearbylabel:=""Font:"";;controlid=""1136"""). _
        ListBox("name:=""ComboLBox""")

        a = Nothing
    End Sub

    'Use TestCleanup to run code after each test has run
    <TestCleanup()> _
    Public Sub MyTestCleanup()
        Kill()
    End Sub
    '
#End Region


    ''''<summary>
    ''''A test for SetSelectedItem
    ''''</summary>
    '<TestMethod()> _
    'Public Sub SetSelectedItemTest()

    '    Dim ItemNumber As Integer = 0 ' TODO: Initialize to an appropriate value
    '    Dim expected As Boolean = False ' TODO: Initialize to an appropriate value
    '    Dim actual As Boolean
    '    actual = target.SetSelectedItem(ItemNumber)
    '    Assert.AreEqual(expected, actual)
    'End Sub

    '''<summary>
    '''A test for IsListBox
    '''</summary>
    <TestMethod()> _
    Public Sub IsListBoxTest()
        Dim expected As Boolean = True
        Dim actual As Boolean
        actual = target.IsListBox()
        Assert.AreEqual(expected, actual)
    End Sub

    '''<summary>
    '''A test for GetSelectedItems
    '''</summary>
    <TestMethod()> _
    Public Sub GetSelectedItemsTest()

        Dim expected As Integer = -1
        Dim actual() As Integer
        actual = target.GetSelectedItems()
        Assert.AreEqual(expected, actual(0))
    End Sub

    '''<summary>
    '''A test for GetSelectedItemNumber
    '''</summary>
    <TestMethod()> _
    Public Sub GetSelectedItemNumberTest()

        Dim expected As Integer = 0
        Dim actual As Integer
        actual = target.GetSelectedItemNumber()
        Assert.IsTrue(expected < actual)
    End Sub

    '''<summary>
    '''A test for GetSelectCount
    '''</summary>
    <TestMethod()> _
    Public Sub GetSelectCountTest()
        Dim expected As Integer = -1 'Not Multi-select
        Dim actual As Integer
        actual = target.GetSelectCount()
        Assert.AreEqual(expected, actual)
    End Sub

    '''<summary>
    '''A test for GetItemCount
    '''</summary>
    <TestMethod()> _
    Public Sub GetItemCountTest()
        Dim expected As Integer = 130 'I don't have a good dynamic way to get this #.
        Dim actual As Integer
        actual = target.GetItemCount()
        Console.WriteLine(expected & " <= " & actual)
        Assert.IsTrue(expected <= actual)
    End Sub

    '''<summary>
    '''A test for GetItemByIndex
    '''</summary>
    <TestMethod()> _
    Public Sub GetItemByIndexTest()
        Dim expected As String = "Arial"
        Dim actual As String = String.Empty

        For index As Integer = 0 To target.GetItemCount()
            actual = target.GetItemByIndex(index)
            If (expected = actual) Then
                Assert.AreEqual(expected, actual)
                Return
            End If
        Next

        Assert.AreEqual(expected, actual)
    End Sub

    '''<summary>
    '''A test for GetAllItems
    '''</summary>
    <TestMethod()> _
    Public Sub GetAllItemsTest()

        Dim expected As String = "Arial"
        Dim actual() As String
        actual = target.GetAllItems()
        For Each actualString As String In actual
            If (expected = actualString) Then
                Assert.AreEqual(expected, actualString)
                Return
            Else
                Console.WriteLine("failed match: " & actualString & " compared to " & expected)
            End If
        Next
        Throw New Exception("Unable to find item " & expected)
    End Sub
End Class