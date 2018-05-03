Imports System
Imports NUnit.Framework
Imports UIControls



'''<summary>
'''This is a test class for WinObjectTest and is intended
'''to contain all WinObjectTest Unit Tests
'''</summary>
<TestFixture()> _
Public Class WinObjectTest

#Region "Additional test attributes"
    Private Shared p As System.Diagnostics.Process
    Private Shared hwnd As IntPtr
    Private Shared target As WinObject

    Public Shared Sub CloseAll(Optional ByVal name As String = "calc")
        For Each pro As System.Diagnostics.Process In System.Diagnostics.Process.GetProcessesByName(name)
            pro.CloseMainWindow()
        Next
        System.Threading.Thread.Sleep(100)
    End Sub

    Public Shared Sub Init()
        p = Diagnostics.Process.Start(System.IO.Path.GetFullPath("..\..\..\zUnitTestUIAutomation\Programs\calc.exe"))
        hwnd = p.MainWindowHandle
        Dim a As UIControls.InterAct
        a = New UIControls.InterAct()
        target = a.Window("name:=""" & "SciCalc" & """")
        System.Threading.Thread.Sleep(100)
    End Sub

    '
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
        Init()
    End Sub

    'Use TestCleanup to run code after each test has run
    <TearDown()> _
    Public Sub MyTestCleanup()
        CloseAll()
    End Sub
    '
#End Region


    '''<summary>
    '''A test for Menu
    '''</summary>
    <Test()> _
    Public Sub MenuTest()
        Dim actual As Menu
        actual = target.Menu
        Verify.AreEqual(actual.ContainsMenu(), True)
    End Sub

    '''<summary>
    '''A test for GetIndex
    '''</summary>
    <Test()> _
    Public Sub GetIndexTest()
        Dim actual As Integer
        actual = target.GetIndex()
        Verify.AreEqual(0, actual)
    End Sub

    ''''<summary>
    ''''A test for WinObject
    ''''</summary>
    '<Test()> _
    'Public Sub WinObjectTest()

    '    Dim desc As Object = Nothing ' TODO: Initialize to an appropriate value
    '    Dim expected As WinObject = Nothing ' TODO: Initialize to an appropriate value
    '    Dim actual As WinObject
    '    actual = target.WinObject(desc)
    '    Verify.AreEqual(expected, actual)
    '    Verify.Inconclusive("Verify the correctness of this test method.")
    'End Sub

    ''''<summary>
    ''''A test for Window
    ''''</summary>
    '<Test()> _
    'Public Sub WindowTest()

    '    Dim desc As Object = Nothing ' TODO: Initialize to an appropriate value
    '    Dim expected As Window = Nothing ' TODO: Initialize to an appropriate value
    '    Dim actual As Window
    '    actual = target.Window(desc)
    '    Verify.AreEqual(expected, actual)
    '    Verify.Inconclusive("Verify the correctness of this test method.")
    'End Sub

    '''<summary>
    '''A test for TypeKeys
    '''</summary>
    <Test()> _
    Public Sub TypeKeysTest()

        Dim Text As String = "1234"
        System.Threading.Thread.Sleep(100)
        target.TypeKeys(Text)
        System.Threading.Thread.Sleep(100)
        Verify.AreEqual("1234. ", target.TextBox("Name:=""Edit""").GetText())
    End Sub

    ''''<summary>
    ''''A test for TreeView
    ''''</summary>
    '<Test()> _
    'Public Sub TreeViewTest()
    '    Dim desc As Object = Nothing ' TODO: Initialize to an appropriate value
    '    Dim expected As TreeView = Nothing ' TODO: Initialize to an appropriate value
    '    Dim actual As TreeView
    '    actual = target.TreeView(desc)
    '    Verify.AreEqual(expected, actual)
    '    Verify.Inconclusive("Verify the correctness of this test method.")
    'End Sub

    ''''<summary>
    ''''A test for TextBox
    ''''</summary>
    '<Test()> _
    'Public Sub TextBoxTest()

    '    Dim desc As Object = Nothing ' TODO: Initialize to an appropriate value
    '    Dim expected As TextBox = Nothing ' TODO: Initialize to an appropriate value
    '    Dim actual As TextBox
    '    actual = target.TextBox(desc)
    '    Verify.AreEqual(expected, actual)
    '    Verify.Inconclusive("Verify the correctness of this test method.")
    'End Sub

    ''''<summary>
    ''''A test for TabControl
    ''''</summary>
    '<Test()> _
    'Public Sub TabControlTest()

    '    Dim desc As Object = Nothing ' TODO: Initialize to an appropriate value
    '    Dim expected As TabControl = Nothing ' TODO: Initialize to an appropriate value
    '    Dim actual As TabControl
    '    actual = target.TabControl(desc)
    '    Verify.AreEqual(expected, actual)
    '    Verify.Inconclusive("Verify the correctness of this test method.")
    'End Sub

    ''''<summary>
    ''''A test for StaticLabel
    ''''</summary>
    '<Test()> _
    'Public Sub StaticLabelTest()

    '    Dim desc As Object = Nothing ' TODO: Initialize to an appropriate value
    '    Dim expected As StaticLabel = Nothing ' TODO: Initialize to an appropriate value
    '    Dim actual As StaticLabel
    '    actual = target.StaticLabel(desc)
    '    Verify.AreEqual(expected, actual)
    '    Verify.Inconclusive("Verify the correctness of this test method.")
    'End Sub

    'This maybe a bad idea?
    ''''<summary>
    ''''A test for SetText
    ''''</summary>
    '<Test()> _
    'Public Sub SetTextTest()

    '    Dim text As String = String.Empty ' TODO: Initialize to an appropriate value
    '    target.SetText(text)
    '    Verify.Inconclusive("A method that does not return a value cannot be verified.")
    'End Sub

    ''''<summary>
    ''''A test for RadioButton
    ''''</summary>
    '<Test()> _
    'Public Sub RadioButtonTest()

    '    Dim desc As Object = Nothing ' TODO: Initialize to an appropriate value
    '    Dim expected As RadioButton = Nothing ' TODO: Initialize to an appropriate value
    '    Dim actual As RadioButton
    '    actual = target.RadioButton(desc)
    '    Verify.AreEqual(expected, actual)
    '    Verify.Inconclusive("Verify the correctness of this test method.")
    'End Sub

    ''''<summary>
    ''''A test for ListView
    ''''</summary>
    '<Test()> _
    'Public Sub ListViewTest()

    '    Dim desc As Object = Nothing ' TODO: Initialize to an appropriate value
    '    Dim expected As ListView = Nothing ' TODO: Initialize to an appropriate value
    '    Dim actual As ListView
    '    actual = target.ListView(desc)
    '    Verify.AreEqual(expected, actual)
    '    Verify.Inconclusive("Verify the correctness of this test method.")
    'End Sub

    ''''<summary>
    ''''A test for ListBox
    ''''</summary>
    '<Test()> _
    'Public Sub ListBoxTest()

    '    Dim desc As Object = Nothing ' TODO: Initialize to an appropriate value
    '    Dim expected As ListBox = Nothing ' TODO: Initialize to an appropriate value
    '    Dim actual As ListBox
    '    actual = target.ListBox(desc)
    '    Verify.AreEqual(expected, actual)
    '    Verify.Inconclusive("Verify the correctness of this test method.")
    'End Sub

    '''<summary>
    '''A test for GetText
    '''</summary>
    <Test()> _
    Public Sub GetTextTest()
        Dim expected As String = "Calculator"
        Dim actual As String
        actual = target.GetText
        Verify.AreEqual(expected, actual)
    End Sub

    ''''<summary>
    ''''A test for GetMainWindowHwnd
    ''''</summary>
    '<Test(), _
    ' DeploymentItem("InterAction.dll")> _
    'Public Sub GetMainWindowHwndTest()
    '    Dim target As WinObject_Accessor = New WinObject_Accessor ' TODO: Initialize to an appropriate value
    '    Dim expected As IntPtr = p.MainWindowHandle
    '    Dim actual As IntPtr
    '    actual = target.GetMainWindowHwnd
    '    Verify.AreEqual(expected, actual)
    'End Sub

    '''<summary>
    '''A test for GetControlName
    '''</summary>
    <Test()> _
    Public Sub GetControlNameTest()

        Dim expected As String = "SciCalc"
        Dim actual As String
        actual = target.GetControlName()
        Verify.AreEqual(expected, actual)
    End Sub

    '''<summary>
    '''A test for GetChildDescriptions
    '''</summary>
    <Test()> _
    Public Sub GetChildDescriptionsTest()
        Dim actual() As Description
        actual = target.GetChildDescriptions
        Verify.AreEqual(78, actual.Count)
    End Sub

    ''''<summary>
    ''''A test for ComboBox
    ''''</summary>
    '<Test()> _
    'Public Sub ComboBoxTest()

    '    Dim desc As Object = Nothing ' TODO: Initialize to an appropriate value
    '    Dim expected As ComboBox = Nothing ' TODO: Initialize to an appropriate value
    '    Dim actual As ComboBox
    '    actual = target.ComboBox(desc)
    '    Verify.AreEqual(expected, actual)
    '    Verify.Inconclusive("Verify the correctness of this test method.")
    'End Sub

    '''<summary>
    '''A test for CloseParent
    '''</summary>
    <Test()> _
    Public Sub CloseParentTest()
        System.Threading.Thread.Sleep(2000)
        target.CloseParent()
        System.Threading.Thread.Sleep(2000)
        Verify.AreEqual(False, target.Exists(5))
        Init()
    End Sub

    ''''<summary>
    ''''A test for CheckBox
    ''''</summary>
    '<Test()> _
    'Public Sub CheckBoxTest()

    '    Dim desc As Object = Nothing ' TODO: Initialize to an appropriate value
    '    Dim expected As CheckBox = Nothing ' TODO: Initialize to an appropriate value
    '    Dim actual As CheckBox
    '    actual = target.CheckBox(desc)
    '    Verify.AreEqual(expected, actual)
    '    Verify.Inconclusive("Verify the correctness of this test method.")
    'End Sub

    ''''<summary>
    ''''A test for Button
    ''''</summary>
    '<Test()> _
    'Public Sub ButtonTest()

    '    Dim desc As Object = Nothing ' TODO: Initialize to an appropriate value
    '    Dim expected As Button = Nothing ' TODO: Initialize to an appropriate value
    '    Dim actual As Button
    '    actual = target.Button(desc)
    '    Verify.AreEqual(expected, actual)
    '    Verify.Inconclusive("Verify the correctness of this test method.")
    'End Sub

    ''''<summary>
    ''''A test for BuildWinObject
    ''''</summary>
    '<Test(), _
    ' DeploymentItem("InterAction.dll")> _
    'Public Sub BuildWinObjectTest()
    '    Dim target As WinObject_Accessor = New WinObject_Accessor ' TODO: Initialize to an appropriate value
    '    Dim desc As Object = Nothing ' TODO: Initialize to an appropriate value
    '    Dim expected As WinObject = Nothing ' TODO: Initialize to an appropriate value
    '    Dim actual As WinObject
    '    actual = target.BuildWinObject(desc)
    '    Verify.AreEqual(expected, actual)
    '    Verify.Inconclusive("Verify the correctness of this test method.")
    'End Sub

    ''''<summary>
    ''''A test for BuildWindow
    ''''</summary>
    '<Test()> _
    'Public Sub BuildWindowTest()

    '    Dim desc As Object = Nothing ' TODO: Initialize to an appropriate value
    '    Dim expected As Window = Nothing ' TODO: Initialize to an appropriate value
    '    Dim actual As Window
    '    actual = target.BuildWindow(desc)
    '    Verify.AreEqual(expected, actual)
    '    Verify.Inconclusive("Verify the correctness of this test method.")
    'End Sub

End Class
