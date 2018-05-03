Imports Microsoft.VisualStudio.TestTools.UnitTesting

Imports UIControls



'''<summary>
'''This is a test class for KeyboardTest and is intended
'''to contain all KeyboardTest Unit Tests
'''</summary>
<TestClass()> _
Public Class KeyboardTest


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
    Public Shared Sub CloseAll(Optional ByVal name As String = "calc")
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

    Public Shared Sub Init()
        CloseAll("notepad")
        p = Diagnostics.Process.Start("notepad.exe")
        System.Threading.Thread.Sleep(2000)
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
        Try
            CloseAll("notepad")
        Catch ex As Exception
            'Who cares.
        End Try
    End Sub
    '
#End Region


    '''<summary>
    '''A test for SendKeys
    '''</summary>
    <TestMethod()> _
    Public Sub SendKeysTest()
        Dim keys As String = "zzxcvbnm,./';lkjhgfdsaqwertyuiop[]\=-0987654321`{~}~~!@#${%}{^}&{*}()_{+}|{{}|{}}POIUYTREWQASDFGHJKL:""?><MNBVCXZ"
        AppActivate(p.Id)
        System.Threading.Thread.Sleep(1000)
        Keyboard.SendKeys(keys)
        System.Threading.Thread.Sleep(1000)
        Dim s As String = "{LEFT}"
        Dim sb As New System.Text.StringBuilder()
        For i = 0 To keys.Length + 2
            sb.Append(s)
        Next
        Keyboard.SendKeys("+(" + sb.ToString() + ")")
        System.Threading.Thread.Sleep(1000)
        Keyboard.SendKeys("^(x)")
        Assert.AreEqual(keys.Replace("~~", vbNewLine & vbNewLine).Replace("{{}", "<REPLACER>"). _
                        Replace("{}}", "<REPLACEL>").Replace("{", "").Replace("}", ""). _
                        Replace("<REPLACER>", "{"). _
                        Replace("<REPLACEL>", "}"), New UIControls.Clipboard().Text)
        System.Threading.Thread.Sleep(1000)
        Keyboard.SendKeys("^{v}")
        System.Threading.Thread.Sleep(1000)
        Keyboard.SendKeys("%{f4}")
        System.Threading.Thread.Sleep(1000)
        Keyboard.SendKeys("{RIGHT}")
        System.Threading.Thread.Sleep(1000)
        Keyboard.SendKeys("~")
        System.Threading.Thread.Sleep(1000)
        Assert.AreEqual(p.HasExited, True)
    End Sub

End Class
