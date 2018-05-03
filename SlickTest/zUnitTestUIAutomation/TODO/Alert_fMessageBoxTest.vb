Imports System
Imports System.Drawing
Imports System.Windows.Forms
Imports NUnit.Framework
Imports UIControls



''''<summary>
''''This is a test class for Alert_fMessageBoxTest and is intended
''''to contain all Alert_fMessageBoxTest Unit Tests
''''</summary>
'<TestFixture()>  _
'Public Class Alert_fMessageBoxTest


'

'    '''<summary>
'    '''Gets or sets the test context which provides
'    '''information about and functionality for the current test run.
'    '''</summary>
'    Public Property TestContext() As TestContext
'        Get
'            Return testContextInstance
'        End Get
'        Set(ByVal value As TestContext)
'            testContextInstance = Value
'        End Set
'    End Property

'#Region "Additional test attributes"
'    '
'    'You can use the following additional attributes as you write your tests:
'    '
'    'Use ClassInitialize to run code before running the first test in the class
'    '<ClassInitialize()>  _
'    'Public Shared Sub MyClassInitialize()
'    'End Sub
'    '
'    'Use ClassCleanup to run code after all tests in a class have run
'    '<ClassCleanup()>  _
'    'Public Shared Sub MyClassCleanup()
'    'End Sub
'    '
'    'Use TestInitialize to run code before running each test
'    '<TestInitialize()>  _
'    'Public Sub MyTestInitialize()
'    'End Sub
'    '
'    'Use TestCleanup to run code after each test has run
'    '<TestCleanup()>  _
'    'Public Sub MyTestCleanup()
'    'End Sub
'    '
'#End Region


'    '''<summary>
'    '''A test for Timer1
'    '''</summary>
'    <Test(), _
'     DeploymentItem("InterAction.dll")> _
'    Public Sub Timer1Test()
'        Dim target As Alert_Accessor.fMessageBox = New Alert_Accessor.fMessageBox ' TODO: Initialize to an appropriate value
'        Dim expected As Timer = Nothing ' TODO: Initialize to an appropriate value
'        Dim actual As Timer
'        target.Timer1 = expected
'        actual = target.Timer1
'        Verify.AreEqual(expected, actual)
'        Verify.Inconclusive("Verify the correctness of this test method.")
'    End Sub

'    '''<summary>
'    '''A test for TimeOut
'    '''</summary>
'    <Test(), _
'     DeploymentItem("InterAction.dll")> _
'    Public Sub TimeOutTest()
'        Dim target As Alert_Accessor.fMessageBox = New Alert_Accessor.fMessageBox ' TODO: Initialize to an appropriate value
'        Dim expected As Integer = 0 ' TODO: Initialize to an appropriate value
'        Dim actual As Integer
'        target.TimeOut = expected
'        actual = target.TimeOut
'        Verify.AreEqual(expected, actual)
'        Verify.Inconclusive("Verify the correctness of this test method.")
'    End Sub

'    '''<summary>
'    '''A test for Shortcuts
'    '''</summary>
'    <Test(), _
'     DeploymentItem("InterAction.dll")> _
'    Public Sub ShortcutsTest()
'        Dim target As Alert_Accessor.fMessageBox = New Alert_Accessor.fMessageBox ' TODO: Initialize to an appropriate value
'        Dim expected As Boolean = False ' TODO: Initialize to an appropriate value
'        target.Shortcuts = expected
'        Verify.Inconclusive("Write-only properties cannot be verified.")
'    End Sub

'    '''<summary>
'    '''A test for RichTextBox1
'    '''</summary>
'    <Test(), _
'     DeploymentItem("InterAction.dll")> _
'    Public Sub RichTextBox1Test()
'        Dim target As Alert_Accessor.fMessageBox = New Alert_Accessor.fMessageBox ' TODO: Initialize to an appropriate value
'        Dim expected As RichTextBox = Nothing ' TODO: Initialize to an appropriate value
'        Dim actual As RichTextBox
'        target.RichTextBox1 = expected
'        actual = target.RichTextBox1
'        Verify.AreEqual(expected, actual)
'        Verify.Inconclusive("Verify the correctness of this test method.")
'    End Sub

'    '''<summary>
'    '''A test for PictureBox1
'    '''</summary>
'    <Test(), _
'     DeploymentItem("InterAction.dll")> _
'    Public Sub PictureBox1Test()
'        Dim target As Alert_Accessor.fMessageBox = New Alert_Accessor.fMessageBox ' TODO: Initialize to an appropriate value
'        Dim expected As PictureBox = Nothing ' TODO: Initialize to an appropriate value
'        Dim actual As PictureBox
'        target.PictureBox1 = expected
'        actual = target.PictureBox1
'        Verify.AreEqual(expected, actual)
'        Verify.Inconclusive("Verify the correctness of this test method.")
'    End Sub

'    '''<summary>
'    '''A test for Message
'    '''</summary>
'    <Test(), _
'     DeploymentItem("InterAction.dll")> _
'    Public Sub MessageTest()
'        Dim target As Alert_Accessor.fMessageBox = New Alert_Accessor.fMessageBox ' TODO: Initialize to an appropriate value
'        Dim expected As String = String.Empty ' TODO: Initialize to an appropriate value
'        Dim actual As String
'        target.Message = expected
'        actual = target.Message
'        Verify.AreEqual(expected, actual)
'        Verify.Inconclusive("Verify the correctness of this test method.")
'    End Sub

'    '''<summary>
'    '''A test for lblTimer
'    '''</summary>
'    <Test(), _
'     DeploymentItem("InterAction.dll")> _
'    Public Sub lblTimerTest()
'        Dim target As Alert_Accessor.fMessageBox = New Alert_Accessor.fMessageBox ' TODO: Initialize to an appropriate value
'        Dim expected As Label = Nothing ' TODO: Initialize to an appropriate value
'        Dim actual As Label
'        target.lblTimer = expected
'        actual = target.lblTimer
'        Verify.AreEqual(expected, actual)
'        Verify.Inconclusive("Verify the correctness of this test method.")
'    End Sub

'    '''<summary>
'    '''A test for Language
'    '''</summary>
'    <Test(), _
'     DeploymentItem("InterAction.dll")> _
'    Public Sub LanguageTest()
'        Dim target As Alert_Accessor.fMessageBox = New Alert_Accessor.fMessageBox ' TODO: Initialize to an appropriate value
'        Dim expected As Alert_Accessor.fMessageBox.enuLanguage = Nothing ' TODO: Initialize to an appropriate value
'        Dim actual As Alert_Accessor.fMessageBox.enuLanguage
'        target.Language = expected
'        actual = target.Language
'        Verify.AreEqual(expected, actual)
'        Verify.Inconclusive("Verify the correctness of this test method.")
'    End Sub

'    '''<summary>
'    '''A test for Icon
'    '''</summary>
'    <Test(), _
'     DeploymentItem("InterAction.dll")> _
'    Public Sub IconTest()
'        Dim target As Alert_Accessor.fMessageBox = New Alert_Accessor.fMessageBox ' TODO: Initialize to an appropriate value
'        Dim expected As MessageBoxIcon = New MessageBoxIcon ' TODO: Initialize to an appropriate value
'        target.Icon = expected
'        Verify.Inconclusive("Write-only properties cannot be verified.")
'    End Sub

'    '''<summary>
'    '''A test for DefaultButton
'    '''</summary>
'    <Test(), _
'     DeploymentItem("InterAction.dll")> _
'    Public Sub DefaultButtonTest()
'        Dim target As Alert_Accessor.fMessageBox = New Alert_Accessor.fMessageBox ' TODO: Initialize to an appropriate value
'        Dim expected As MessageBoxDefaultButton = New MessageBoxDefaultButton ' TODO: Initialize to an appropriate value
'        Dim actual As MessageBoxDefaultButton
'        target.DefaultButton = expected
'        actual = target.DefaultButton
'        Verify.AreEqual(expected, actual)
'        Verify.Inconclusive("Verify the correctness of this test method.")
'    End Sub

'    '''<summary>
'    '''A test for Buttons
'    '''</summary>
'    <Test(), _
'     DeploymentItem("InterAction.dll")> _
'    Public Sub ButtonsTest()
'        Dim target As Alert_Accessor.fMessageBox = New Alert_Accessor.fMessageBox ' TODO: Initialize to an appropriate value
'        Dim expected As MessageBoxButtons = New MessageBoxButtons ' TODO: Initialize to an appropriate value
'        Dim actual As MessageBoxButtons
'        target.Buttons = expected
'        actual = target.Buttons
'        Verify.AreEqual(expected, actual)
'        Verify.Inconclusive("Verify the correctness of this test method.")
'    End Sub

'    '''<summary>
'    '''A test for Button3
'    '''</summary>
'    <Test(), _
'     DeploymentItem("InterAction.dll")> _
'    Public Sub Button3Test()
'        Dim target As Alert_Accessor.fMessageBox = New Alert_Accessor.fMessageBox ' TODO: Initialize to an appropriate value
'        Dim expected As Button = Nothing ' TODO: Initialize to an appropriate value
'        Dim actual As Button
'        target.Button3 = expected
'        actual = target.Button3
'        Verify.AreEqual(expected, actual)
'        Verify.Inconclusive("Verify the correctness of this test method.")
'    End Sub

'    '''<summary>
'    '''A test for Button2
'    '''</summary>
'    <Test(), _
'     DeploymentItem("InterAction.dll")> _
'    Public Sub Button2Test()
'        Dim target As Alert_Accessor.fMessageBox = New Alert_Accessor.fMessageBox ' TODO: Initialize to an appropriate value
'        Dim expected As Button = Nothing ' TODO: Initialize to an appropriate value
'        Dim actual As Button
'        target.Button2 = expected
'        actual = target.Button2
'        Verify.AreEqual(expected, actual)
'        Verify.Inconclusive("Verify the correctness of this test method.")
'    End Sub

'    '''<summary>
'    '''A test for Button1
'    '''</summary>
'    <Test(), _
'     DeploymentItem("InterAction.dll")> _
'    Public Sub Button1Test()
'        Dim target As Alert_Accessor.fMessageBox = New Alert_Accessor.fMessageBox ' TODO: Initialize to an appropriate value
'        Dim expected As Button = Nothing ' TODO: Initialize to an appropriate value
'        Dim actual As Button
'        target.Button1 = expected
'        actual = target.Button1
'        Verify.AreEqual(expected, actual)
'        Verify.Inconclusive("Verify the correctness of this test method.")
'    End Sub

'    '''<summary>
'    '''A test for Timer1_Tick
'    '''</summary>
'    <Test(), _
'     DeploymentItem("InterAction.dll")> _
'    Public Sub Timer1_TickTest()
'        Dim target As Alert_Accessor.fMessageBox = New Alert_Accessor.fMessageBox ' TODO: Initialize to an appropriate value
'        Dim sender As Object = Nothing ' TODO: Initialize to an appropriate value
'        Dim e As EventArgs = Nothing ' TODO: Initialize to an appropriate value
'        target.Timer1_Tick(sender, e)
'        Verify.Inconclusive("A method that does not return a value cannot be verified.")
'    End Sub

'    '''<summary>
'    '''A test for SetMessageSize
'    '''</summary>
'    <Test(), _
'     DeploymentItem("InterAction.dll")> _
'    Public Sub SetMessageSizeTest()
'        Dim target As Alert_Accessor.fMessageBox = New Alert_Accessor.fMessageBox ' TODO: Initialize to an appropriate value
'        target.SetMessageSize()
'        Verify.Inconclusive("A method that does not return a value cannot be verified.")
'    End Sub

'    '''<summary>
'    '''A test for SetFormSize
'    '''</summary>
'    <Test(), _
'     DeploymentItem("InterAction.dll")> _
'    Public Sub SetFormSizeTest()
'        Dim target As Alert_Accessor.fMessageBox = New Alert_Accessor.fMessageBox ' TODO: Initialize to an appropriate value
'        target.SetFormSize()
'        Verify.Inconclusive("A method that does not return a value cannot be verified.")
'    End Sub

'    '''<summary>
'    '''A test for RichTextBox1_LinkClicked
'    '''</summary>
'    <Test(), _
'     DeploymentItem("InterAction.dll")> _
'    Public Sub RichTextBox1_LinkClickedTest()
'        Dim target As Alert_Accessor.fMessageBox = New Alert_Accessor.fMessageBox ' TODO: Initialize to an appropriate value
'        Dim sender As Object = Nothing ' TODO: Initialize to an appropriate value
'        Dim e As LinkClickedEventArgs = Nothing ' TODO: Initialize to an appropriate value
'        target.RichTextBox1_LinkClicked(sender, e)
'        Verify.Inconclusive("A method that does not return a value cannot be verified.")
'    End Sub

'    '''<summary>
'    '''A test for MessageBox_Load
'    '''</summary>
'    <Test(), _
'     DeploymentItem("InterAction.dll")> _
'    Public Sub MessageBox_LoadTest()
'        Dim target As Alert_Accessor.fMessageBox = New Alert_Accessor.fMessageBox ' TODO: Initialize to an appropriate value
'        Dim sender As Object = Nothing ' TODO: Initialize to an appropriate value
'        Dim e As EventArgs = Nothing ' TODO: Initialize to an appropriate value
'        target.MessageBox_Load(sender, e)
'        Verify.Inconclusive("A method that does not return a value cannot be verified.")
'    End Sub

'    '''<summary>
'    '''A test for MeasureString
'    '''</summary>
'    <Test(), _
'     DeploymentItem("InterAction.dll")> _
'    Public Sub MeasureStringTest()
'        Dim target As Alert_Accessor.fMessageBox = New Alert_Accessor.fMessageBox ' TODO: Initialize to an appropriate value
'        Dim pStr As String = String.Empty ' TODO: Initialize to an appropriate value
'        Dim pMaxWidth As Integer = 0 ' TODO: Initialize to an appropriate value
'        Dim pfont As Font = Nothing ' TODO: Initialize to an appropriate value
'        Dim expected As Size = New Size ' TODO: Initialize to an appropriate value
'        Dim actual As Size
'        actual = target.MeasureString(pStr, pMaxWidth, pfont)
'        Verify.AreEqual(expected, actual)
'        Verify.Inconclusive("Verify the correctness of this test method.")
'    End Sub

'    '''<summary>
'    '''A test for InitializeComponent
'    '''</summary>
'    <Test(), _
'     DeploymentItem("InterAction.dll")> _
'    Public Sub InitializeComponentTest()
'        Dim target As Alert_Accessor.fMessageBox = New Alert_Accessor.fMessageBox ' TODO: Initialize to an appropriate value
'        target.InitializeComponent()
'        Verify.Inconclusive("A method that does not return a value cannot be verified.")
'    End Sub

'    '''<summary>
'    '''A test for fMessageBox_MouseClick
'    '''</summary>
'    <Test(), _
'     DeploymentItem("InterAction.dll")> _
'    Public Sub fMessageBox_MouseClickTest()
'        Dim target As Alert_Accessor.fMessageBox = New Alert_Accessor.fMessageBox ' TODO: Initialize to an appropriate value
'        Dim sender As Object = Nothing ' TODO: Initialize to an appropriate value
'        Dim e As MouseEventArgs = Nothing ' TODO: Initialize to an appropriate value
'        target.fMessageBox_MouseClick(sender, e)
'        Verify.Inconclusive("A method that does not return a value cannot be verified.")
'    End Sub

'    '''<summary>
'    '''A test for Dispose
'    '''</summary>
'    <Test(), _
'     DeploymentItem("InterAction.dll")> _
'    Public Sub DisposeTest()
'        Dim target As Alert_Accessor.fMessageBox = New Alert_Accessor.fMessageBox ' TODO: Initialize to an appropriate value
'        Dim disposing As Boolean = False ' TODO: Initialize to an appropriate value
'        target.Dispose(disposing)
'        Verify.Inconclusive("A method that does not return a value cannot be verified.")
'    End Sub

'    '''<summary>
'    '''A test for fMessageBox Constructor
'    '''</summary>
'    <Test(), _
'     DeploymentItem("InterAction.dll")> _
'    Public Sub Alert_fMessageBoxConstructorTest()
'        Dim target As Alert_Accessor.fMessageBox = New Alert_Accessor.fMessageBox
'        Verify.Inconclusive("TODO: Implement code to verify target")
'    End Sub
'End Class
