Imports System

Imports System.Collections.Generic

Imports Microsoft.VisualStudio.TestTools.UnitTesting

Imports UIControls



'''<summary>
'''This is a test class for WinObjectTest and is intended
'''to contain all WinObjectTest Unit Tests
'''</summary>
<TestClass()> _
Public Class WinObjectTest


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
    Private Shared hwnd As IntPtr
    Private Shared target As WinObject

    Public Shared Sub CloseAll(Optional ByVal name As String = "calc")
        For Each pro As System.Diagnostics.Process In System.Diagnostics.Process.GetProcessesByName(name)
            pro.CloseMainWindow()
        Next
        System.Threading.Thread.Sleep(100)
    End Sub

    Public Shared Sub Init()
        p = Diagnostics.Process.Start("calc.exe")
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
        Init()
    End Sub

    'Use TestCleanup to run code after each test has run
    <TestCleanup()> _
    Public Sub MyTestCleanup()
        CloseAll()
    End Sub
    '
#End Region


    '''<summary>
    '''A test for Menu
    '''</summary>
    <TestMethod()> _
    Public Sub MenuTest()
        Dim actual As Menu
        actual = target.Menu
        Assert.AreEqual(actual.ContainsMenu(), True)
    End Sub

    '''<summary>
    '''A test for GetIndex
    '''</summary>
    <TestMethod()> _
    Public Sub GetIndexTest()
        Dim actual As Integer
        actual = target.GetIndex()
        Assert.AreEqual(0, actual)
    End Sub

    ''''<summary>
    ''''A test for WinObject
    ''''</summary>
    '<TestMethod()> _
    'Public Sub WinObjectTest()

    '    Dim desc As Object = Nothing ' TODO: Initialize to an appropriate value
    '    Dim expected As WinObject = Nothing ' TODO: Initialize to an appropriate value
    '    Dim actual As WinObject
    '    actual = target.WinObject(desc)
    '    Assert.AreEqual(expected, actual)
    '    Assert.Inconclusive("Verify the correctness of this test method.")
    'End Sub

    ''''<summary>
    ''''A test for Window
    ''''</summary>
    '<TestMethod()> _
    'Public Sub WindowTest()

    '    Dim desc As Object = Nothing ' TODO: Initialize to an appropriate value
    '    Dim expected As Window = Nothing ' TODO: Initialize to an appropriate value
    '    Dim actual As Window
    '    actual = target.Window(desc)
    '    Assert.AreEqual(expected, actual)
    '    Assert.Inconclusive("Verify the correctness of this test method.")
    'End Sub

    '''<summary>
    '''A test for TypeKeys
    '''</summary>
    <TestMethod()> _
    Public Sub TypeKeysTest()

        Dim Text As String = "1234"
        System.Threading.Thread.Sleep(100)
        target.TypeKeys(Text)
        System.Threading.Thread.Sleep(100)
        Assert.AreEqual("1234. ", target.TextBox("Name:=""Edit""").GetText())
    End Sub

    ''''<summary>
    ''''A test for TreeView
    ''''</summary>
    '<TestMethod()> _
    'Public Sub TreeViewTest()
    '    Dim desc As Object = Nothing ' TODO: Initialize to an appropriate value
    '    Dim expected As TreeView = Nothing ' TODO: Initialize to an appropriate value
    '    Dim actual As TreeView
    '    actual = target.TreeView(desc)
    '    Assert.AreEqual(expected, actual)
    '    Assert.Inconclusive("Verify the correctness of this test method.")
    'End Sub

    ''''<summary>
    ''''A test for TextBox
    ''''</summary>
    '<TestMethod()> _
    'Public Sub TextBoxTest()

    '    Dim desc As Object = Nothing ' TODO: Initialize to an appropriate value
    '    Dim expected As TextBox = Nothing ' TODO: Initialize to an appropriate value
    '    Dim actual As TextBox
    '    actual = target.TextBox(desc)
    '    Assert.AreEqual(expected, actual)
    '    Assert.Inconclusive("Verify the correctness of this test method.")
    'End Sub

    ''''<summary>
    ''''A test for TabControl
    ''''</summary>
    '<TestMethod()> _
    'Public Sub TabControlTest()

    '    Dim desc As Object = Nothing ' TODO: Initialize to an appropriate value
    '    Dim expected As TabControl = Nothing ' TODO: Initialize to an appropriate value
    '    Dim actual As TabControl
    '    actual = target.TabControl(desc)
    '    Assert.AreEqual(expected, actual)
    '    Assert.Inconclusive("Verify the correctness of this test method.")
    'End Sub

    ''''<summary>
    ''''A test for StaticLabel
    ''''</summary>
    '<TestMethod()> _
    'Public Sub StaticLabelTest()

    '    Dim desc As Object = Nothing ' TODO: Initialize to an appropriate value
    '    Dim expected As StaticLabel = Nothing ' TODO: Initialize to an appropriate value
    '    Dim actual As StaticLabel
    '    actual = target.StaticLabel(desc)
    '    Assert.AreEqual(expected, actual)
    '    Assert.Inconclusive("Verify the correctness of this test method.")
    'End Sub

    'This maybe a bad idea?
    ''''<summary>
    ''''A test for SetText
    ''''</summary>
    '<TestMethod()> _
    'Public Sub SetTextTest()

    '    Dim text As String = String.Empty ' TODO: Initialize to an appropriate value
    '    target.SetText(text)
    '    Assert.Inconclusive("A method that does not return a value cannot be verified.")
    'End Sub

    ''''<summary>
    ''''A test for RadioButton
    ''''</summary>
    '<TestMethod()> _
    'Public Sub RadioButtonTest()

    '    Dim desc As Object = Nothing ' TODO: Initialize to an appropriate value
    '    Dim expected As RadioButton = Nothing ' TODO: Initialize to an appropriate value
    '    Dim actual As RadioButton
    '    actual = target.RadioButton(desc)
    '    Assert.AreEqual(expected, actual)
    '    Assert.Inconclusive("Verify the correctness of this test method.")
    'End Sub

    ''''<summary>
    ''''A test for ListView
    ''''</summary>
    '<TestMethod()> _
    'Public Sub ListViewTest()

    '    Dim desc As Object = Nothing ' TODO: Initialize to an appropriate value
    '    Dim expected As ListView = Nothing ' TODO: Initialize to an appropriate value
    '    Dim actual As ListView
    '    actual = target.ListView(desc)
    '    Assert.AreEqual(expected, actual)
    '    Assert.Inconclusive("Verify the correctness of this test method.")
    'End Sub

    ''''<summary>
    ''''A test for ListBox
    ''''</summary>
    '<TestMethod()> _
    'Public Sub ListBoxTest()

    '    Dim desc As Object = Nothing ' TODO: Initialize to an appropriate value
    '    Dim expected As ListBox = Nothing ' TODO: Initialize to an appropriate value
    '    Dim actual As ListBox
    '    actual = target.ListBox(desc)
    '    Assert.AreEqual(expected, actual)
    '    Assert.Inconclusive("Verify the correctness of this test method.")
    'End Sub

    '''<summary>
    '''A test for GetText
    '''</summary>
    <TestMethod()> _
    Public Sub GetTextTest()
        Dim expected As String = "Calculator"
        Dim actual As String
        actual = target.GetText
        Assert.AreEqual(expected, actual)
    End Sub

    ''''<summary>
    ''''A test for GetMainWindowHwnd
    ''''</summary>
    '<TestMethod(), _
    ' DeploymentItem("InterAction.dll")> _
    'Public Sub GetMainWindowHwndTest()
    '    Dim target As WinObject_Accessor = New WinObject_Accessor ' TODO: Initialize to an appropriate value
    '    Dim expected As IntPtr = p.MainWindowHandle
    '    Dim actual As IntPtr
    '    actual = target.GetMainWindowHwnd
    '    Assert.AreEqual(expected, actual)
    'End Sub

    '''<summary>
    '''A test for GetControlName
    '''</summary>
    <TestMethod()> _
    Public Sub GetControlNameTest()

        Dim expected As String = "SciCalc"
        Dim actual As String
        actual = target.GetControlName()
        Assert.AreEqual(expected, actual)
    End Sub

    '''<summary>
    '''A test for GetChildDescriptions
    '''</summary>
    <TestMethod()> _
    Public Sub GetChildDescriptionsTest()
        Dim actual() As Description
        actual = target.GetChildDescriptions
        Assert.AreEqual(78, actual.Count)
    End Sub

    ''''<summary>
    ''''A test for ComboBox
    ''''</summary>
    '<TestMethod()> _
    'Public Sub ComboBoxTest()

    '    Dim desc As Object = Nothing ' TODO: Initialize to an appropriate value
    '    Dim expected As ComboBox = Nothing ' TODO: Initialize to an appropriate value
    '    Dim actual As ComboBox
    '    actual = target.ComboBox(desc)
    '    Assert.AreEqual(expected, actual)
    '    Assert.Inconclusive("Verify the correctness of this test method.")
    'End Sub

    '''<summary>
    '''A test for CloseParent
    '''</summary>
    <TestMethod()> _
    Public Sub CloseParentTest()
        System.Threading.Thread.Sleep(2000)
        target.CloseParent()
        System.Threading.Thread.Sleep(2000)
        Assert.AreEqual(False, target.Exists(5))
        Init()
    End Sub

    ''''<summary>
    ''''A test for CheckBox
    ''''</summary>
    '<TestMethod()> _
    'Public Sub CheckBoxTest()

    '    Dim desc As Object = Nothing ' TODO: Initialize to an appropriate value
    '    Dim expected As CheckBox = Nothing ' TODO: Initialize to an appropriate value
    '    Dim actual As CheckBox
    '    actual = target.CheckBox(desc)
    '    Assert.AreEqual(expected, actual)
    '    Assert.Inconclusive("Verify the correctness of this test method.")
    'End Sub

    ''''<summary>
    ''''A test for Button
    ''''</summary>
    '<TestMethod()> _
    'Public Sub ButtonTest()

    '    Dim desc As Object = Nothing ' TODO: Initialize to an appropriate value
    '    Dim expected As Button = Nothing ' TODO: Initialize to an appropriate value
    '    Dim actual As Button
    '    actual = target.Button(desc)
    '    Assert.AreEqual(expected, actual)
    '    Assert.Inconclusive("Verify the correctness of this test method.")
    'End Sub

    ''''<summary>
    ''''A test for BuildWinObject
    ''''</summary>
    '<TestMethod(), _
    ' DeploymentItem("InterAction.dll")> _
    'Public Sub BuildWinObjectTest()
    '    Dim target As WinObject_Accessor = New WinObject_Accessor ' TODO: Initialize to an appropriate value
    '    Dim desc As Object = Nothing ' TODO: Initialize to an appropriate value
    '    Dim expected As WinObject = Nothing ' TODO: Initialize to an appropriate value
    '    Dim actual As WinObject
    '    actual = target.BuildWinObject(desc)
    '    Assert.AreEqual(expected, actual)
    '    Assert.Inconclusive("Verify the correctness of this test method.")
    'End Sub

    ''''<summary>
    ''''A test for BuildWindow
    ''''</summary>
    '<TestMethod()> _
    'Public Sub BuildWindowTest()

    '    Dim desc As Object = Nothing ' TODO: Initialize to an appropriate value
    '    Dim expected As Window = Nothing ' TODO: Initialize to an appropriate value
    '    Dim actual As Window
    '    actual = target.BuildWindow(desc)
    '    Assert.AreEqual(expected, actual)
    '    Assert.Inconclusive("Verify the correctness of this test method.")
    'End Sub

End Class
