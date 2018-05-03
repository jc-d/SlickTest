Imports Microsoft.VisualStudio.TestTools.UnitTesting
Imports UIControls



'''<summary>
'''This is a test class for ComboBoxTest and is intended
'''to contain all ComboBoxTest Unit Tests
'''</summary>
<TestClass()> _
Public Class ComboBoxTest


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
    Private Shared target As ComboBox

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
        target = a.Window("name:=""#32770"";;value:=""Font""").ComboBox(Description)
        a = Nothing
    End Sub

    'Use TestCleanup to run code after each test has run
    <TestCleanup()> _
    Public Sub MyTestCleanup()
        Kill()
    End Sub
    '
#End Region


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
        Assert.AreEqual(target.GetItemByIndex(ItemNumber), target.GetSelectedItemText())
    End Sub

    '''<summary>
    '''A test for IsComboBox
    '''</summary>
    <TestMethod()> _
    Public Sub IsComboBoxTest()
        Dim expected As Boolean = True
        Dim actual As Boolean = target.IsComboBox()
        Assert.AreEqual(expected, actual)
    End Sub

    '''<summary>
    '''A test for GetSelectedItemNumber
    '''</summary>
    <TestMethod()> _
    Public Sub GetSelectedItemNumberTest()

        Dim expected As Integer = 2
        Dim actual As Integer
        target.SetSelectItem(expected)
        actual = target.GetSelectedItemNumber()
        Assert.AreEqual(expected, actual)
    End Sub

    '''<summary>
    '''A test for GetItemCount
    '''</summary>
    <TestMethod()> _
    Public Sub GetItemCountTest()
        Dim expected As Long = 5
        Dim actual As Long
        actual = target.GetItemCount()
        Assert.IsTrue(expected <= actual)
    End Sub

    '''<summary>
    '''A test for GetItemByIndex
    '''</summary>
    <TestMethod()> _
    Public Sub GetItemByIndexTest()

        Dim Index As Integer = 0
        Dim expected As String = "Western"
        Dim actual As String
        actual = target.GetItemByIndex(Index)
        Assert.AreEqual(expected, actual)
    End Sub

    '''<summary>
    '''A test for GetAllItems
    '''</summary>
    <TestMethod()> _
    Public Sub GetAllItemsTest()
        Dim expected As String = "Greek"
        Dim actual() As String
        actual = target.GetAllItems()
        Dim list As New System.Text.StringBuilder()
        For Each item As String In actual
            list.Append(item)
        Next
        Assert.IsTrue(list.ToString().Contains(expected))
    End Sub
End Class
