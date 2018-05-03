Imports NUnit.Framework
Imports UIControls

'''<summary>
'''This is a test class for ListBoxTest and is intended
'''to contain all ListBoxTest Unit Tests
'''</summary>
<TestFixture()> _
Public Class ListBoxTest

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
        target = a.Window("name:=""#32770"";;value:=""Font"""). _
        ComboBox("name:=""ComboBox"";;nearbylabel:=""Font:"";;controlid=""1136"""). _
        ListBox("name:=""ComboLBox""")

        a = Nothing
    End Sub

    'Use TestCleanup to run code after each test has run
    <TearDown()> _
    Public Sub MyTestCleanup()
        Kill()
    End Sub
    '
#End Region


    ''''<summary>
    ''''A test for SetSelectedItem
    ''''</summary>
    '<Test()> _
    'Public Sub SetSelectedItemTest()

    '    Dim ItemNumber As Integer = 0 ' TODO: Initialize to an appropriate value
    '    Dim expected As Boolean = False ' TODO: Initialize to an appropriate value
    '    Dim actual As Boolean
    '    actual = target.SetSelectedItem(ItemNumber)
    '    Verify.AreEqual(expected, actual)
    'End Sub

    '''<summary>
    '''A test for IsListBox
    '''</summary>
    <Test()> _
    Public Sub IsListBoxTest()
        Dim expected As Boolean = True
        Dim actual As Boolean
        actual = target.IsListBox()
        Verify.AreEqual(expected, actual)
    End Sub

    '''<summary>
    '''A test for GetSelectedItems
    '''</summary>
    <Test()> _
    Public Sub GetSelectedItemsTest()

        Dim expected As Integer = -1
        Dim actual() As Integer
        actual = target.GetSelectedItems()
        Verify.AreEqual(expected, actual(0))
    End Sub

    '''<summary>
    '''A test for GetSelectedItemNumber
    '''</summary>
    <Test()> _
    Public Sub GetSelectedItemNumberTest()

        Dim expected As Integer = 0
        Dim actual As Integer
        actual = target.GetSelectedItemNumber()
        Verify.IsTrue(expected < actual)
    End Sub

    '''<summary>
    '''A test for GetSelectCount
    '''</summary>
    <Test()> _
    Public Sub GetSelectCountTest()
        Dim expected As Integer = -1 'Not Multi-select
        Dim actual As Integer
        actual = target.GetSelectCount()
        Verify.AreEqual(expected, actual)
    End Sub

    '''<summary>
    '''A test for GetItemCount
    '''</summary>
    <Test()> _
    Public Sub GetItemCountTest()
        Dim expectedStart As Integer = 130 'I don't have a good dynamic way to get this #.
        Dim expectedEnd As Integer = 240
        Dim actual As Integer
        actual = target.GetItemCount()
        Verify.IsWithin(expectedStart, expectedEnd, actual)
    End Sub

    '''<summary>
    '''A test for GetItemByIndex
    '''</summary>
    <Test()> _
    Public Sub GetItemByIndexTest()
        Dim expected As String = "Arial"
        Dim actual As String = String.Empty

        For index As Integer = 0 To target.GetItemCount()
            actual = target.GetItemByIndex(index)
            If (expected = actual) Then
                Verify.AreEqual(expected, actual)
                Return
            End If
        Next

        Verify.AreEqual(expected, actual)
    End Sub

    '''<summary>
    '''A test for GetAllItems
    '''</summary>
    <Test()> _
    Public Sub GetAllItemsTest()

        Dim expected As String = "Arial"
        Dim actual() As String
        actual = target.GetAllItems()
        For Each actualString As String In actual
            If (expected = actualString) Then
                Verify.AreEqual(expected, actualString)
                Return
            Else
                Console.WriteLine("failed match: " & actualString & " compared to " & expected)
            End If
        Next
        Throw New Exception("Unable to find item " & expected)
    End Sub
End Class