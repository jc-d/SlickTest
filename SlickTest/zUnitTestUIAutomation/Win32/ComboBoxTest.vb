Imports NUnit.Framework
Imports UIControls



'''<summary>
'''This is a test class for ComboBoxTest and is intended
'''to contain all ComboBoxTest Unit Tests
'''</summary>
<TestFixture()> _
Public Class ComboBoxTest


#Region "Additional test attributes"
    Private Shared p As System.Diagnostics.Process
    Private Shared target As ComboBox

    Public Shared Sub Kill()
        For Each proc In System.Diagnostics.Process.GetProcessesByName("Notepad")
            proc.Kill()
        Next
    End Sub

    'You can use the following additional attributes as you write your tests:
    '
    'Use ClassInitialize to run code before running the first test in the class
    <TestFixtureSetUp()> _
    Public Shared Sub MyClassInitialize()
    End Sub

    'Use ClassCleanup to run code after all tests in a class have run
    <TestFixtureTearDown()> _
    Public Shared Sub MyClassCleanup()
    End Sub

    'Use TestInitialize to run code before running each test
    <SetUp()> _
    Public Sub MyTestInitialize()
        Kill()
        p = Diagnostics.Process.Start(System.IO.Path.GetFullPath("..\..\..\zUnitTestUIAutomation\Programs\notepad.exe"))
        System.Threading.Thread.Sleep(2000)
        Dim Description As UIControls.Description = UIControls.Description.Create( _
        "ControlType:=""combobox"";;controlid:=""1140""", False)

        Dim a As UIControls.InterAct = New UIControls.InterAct()
        Dim desc As String = "hwnd:=""" & p.MainWindowHandle.ToString() & """"
        a.Window(desc).SetActive()
        a.Window(desc).Menu.SelectMenuItem(New String() {"Format", "Font..."})
        System.Threading.Thread.Sleep(250)
        target = a.Window("name:=""#32770"";;value:=""Font""").ComboBox(Description)
        a = Nothing
    End Sub

    'Use TestCleanup to run code after each test has run
    <TearDown()> _
    Public Sub MyTestCleanup()
        Kill()
    End Sub
    '
#End Region


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
        Verify.AreEqual(target.GetItemByIndex(ItemNumber), target.GetSelectedItemText())
    End Sub

    '''<summary>
    '''A test for IsComboBox
    '''</summary>
    <Test()> _
    Public Sub IsComboBoxTest()
        Dim expected As Boolean = True
        Dim actual As Boolean = target.IsComboBox()
        Verify.AreEqual(expected, actual)
    End Sub

    '''<summary>
    '''A test for GetSelectedItemNumber
    '''</summary>
    <Test()> _
    Public Sub GetSelectedItemNumberTest()

        Dim expected As Integer = 2
        Dim actual As Integer
        target.SetSelectItem(expected)
        actual = target.GetSelectedItemNumber()
        Verify.AreEqual(expected, actual)
    End Sub

    '''<summary>
    '''A test for GetItemCount
    '''</summary>
    <Test()> _
    Public Sub GetItemCountTest()
        Dim expected As Long = 5
        Dim actual As Long
        actual = target.GetItemCount()
        Verify.IsTrue(expected <= actual)
    End Sub

    '''<summary>
    '''A test for GetItemByIndex
    '''</summary>
    <Test()> _
    Public Sub GetItemByIndexTest()

        Dim Index As Integer = 0
        Dim expected As String = "Western"
        Dim actual As String
        actual = target.GetItemByIndex(Index)
        Verify.AreEqual(expected, actual)
    End Sub

    '''<summary>
    '''A test for GetAllItems
    '''</summary>
    <Test()> _
    Public Sub GetAllItemsTest()
        Dim expected As String = "Greek"
        Dim actual() As String
        actual = target.GetAllItems()
        Dim list As New System.Text.StringBuilder()
        For Each item As String In actual
            list.Append(item)
        Next
        Verify.IsTrue(list.ToString().Contains(expected))
    End Sub
End Class
