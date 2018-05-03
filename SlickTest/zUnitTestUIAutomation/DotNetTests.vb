Imports System.Drawing
Imports System
Imports System.Collections.Specialized
Imports Microsoft.VisualStudio.TestTools.UnitTesting
Imports UIControls

Public Class DotNetTests
    Inherits UIControls.InterAct

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
            testContextInstance = value
        End Set
    End Property

#Region "Additional test attributes"
    Private Shared p As System.Diagnostics.Process
    Public Shared Sub CloseAll(Optional ByVal name As String = "DotNetTest")
        For Each pro As System.Diagnostics.Process In System.Diagnostics.Process.GetProcessesByName(name)
            pro.CloseMainWindow()
        Next
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

    Public Shared Sub Init()
        Dim Path As String = System.IO.Path.GetFullPath("..\..\..\zUnitTestUIAutomation\DotNetTest.exe")
        p = Diagnostics.Process.Start(Path)
        System.Threading.Thread.Sleep(2000)
    End Sub

    'Use TestInitialize to run code before running each test
    <TestInitialize()> _
    Public Overridable Sub MyTestInitialize()
        Init()
        Assert.IsTrue(SwfWindow(TestApp.Form1_Form1).Exists)
    End Sub

    'Use TestCleanup to run code after each test has run
    <TestCleanup()> _
    Public Sub MyTestCleanup()
        CloseAll()
    End Sub
    '
#End Region
End Class

#Region "Description (TestApp) was generated via the recorder..."
Public Class TestApp
    'Description Code Below
    Public Shared Form1_Form1 As UIControls.Description = UIControls.Description.Create("name:=""Form1"";;value:=""Form1""") 'Top level object
    Public Shared CheckBox1 As UIControls.Description = UIControls.Description.Create("name:=""CheckBox1""") 'Parent's name: Form1_Form1
    Public Shared GroupBox2 As UIControls.Description = UIControls.Description.Create("name:=""GroupBox2""") 'Parent's name: Form1_Form1
    Public Shared RadioButton1 As UIControls.Description = UIControls.Description.Create("name:=""RadioButton1""") 'Parent's name: GroupBox2
    Public Shared RadioButton2 As UIControls.Description = UIControls.Description.Create("name:=""RadioButton2""") 'Parent's name: GroupBox2
    Public Shared TreeView1 As UIControls.Description = UIControls.Description.Create("name:=""TreeView1""") 'Parent's name: Form1_Form1
    Public Shared GroupBox1 As UIControls.Description = UIControls.Description.Create("name:=""GroupBox1""") 'Parent's name: Form1_Form1
    Public Shared ComboBox1 As UIControls.Description = UIControls.Description.Create("name:=""ComboBox1""") 'Parent's name: GroupBox1
    Public Shared Edit As UIControls.Description = UIControls.Description.Create("name:=""Edit""") 'Parent's name: ComboBox1
    Public Shared CheckedListBox1 As UIControls.Description = UIControls.Description.Create("name:=""CheckedListBox1""") 'Parent's name: GroupBox1
    Public Shared ListBox1 As UIControls.Description = UIControls.Description.Create("name:=""ListBox1""") 'Parent's name: GroupBox1
    Public Shared ListView1 As UIControls.Description = UIControls.Description.Create("name:=""ListView1""") 'Parent's name: Form1_Form1
    Public Shared TextBox1 As UIControls.Description = UIControls.Description.Create("name:=""TextBox1""") 'Parent's name: Form1_Form1
    Public Shared TextBox2 As UIControls.Description = UIControls.Description.Create("name:=""TextBox2""") 'Parent's name: Form1_Form1
    Public Shared TabControl1 As UIControls.Description = UIControls.Description.Create("name:=""TabControl1""") 'Parent's name: Form1_Form1
    Public Shared TabPage2 As UIControls.Description = UIControls.Description.Create("name:=""TabPage2""") 'Parent's name: TabControl1
    Public Shared Button4 As UIControls.Description = UIControls.Description.Create("name:=""Button4""") 'Parent's name: TabPage2
    Public Shared Button1 As UIControls.Description = UIControls.Description.Create("name:=""Button1""") 'Parent's name: Form1_Form1
    Public Shared Button2 As UIControls.Description = UIControls.Description.Create("name:=""Button2""") 'Parent's name: Form1_Form1
    Public Shared RichTextBox1 As UIControls.Description = UIControls.Description.Create("name:=""RichTextBox1""") 'Parent's name: Form1_Form1
    Public Shared Label1 As UIControls.Description = UIControls.Description.Create("name:=""Label1""") 'Parent's name: Form1_Form1
End Class
#End Region

