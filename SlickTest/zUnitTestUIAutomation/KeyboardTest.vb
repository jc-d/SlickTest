Imports NUnit.Framework

Imports UIControls



'''<summary>
'''This is a test class for KeyboardTest and is intended
'''to contain all KeyboardTest Unit Tests
'''</summary>
<TestFixture()> _
Public Class KeyboardTest

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
    <TestFixtureSetUp()> _
    Public Shared Sub MyClassInitialize()

    End Sub

    Public Shared Sub Init()
        CloseAll("notepad")
        p = Diagnostics.Process.Start(System.IO.Path.GetFullPath("..\..\..\zUnitTestUIAutomation\Programs\notepad.exe"))
        System.Threading.Thread.Sleep(2000)
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
    <Test()> _
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
        Verify.AreEqual(keys.Replace("~~", vbNewLine & vbNewLine).Replace("{{}", "<REPLACER>"). _
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
        Verify.AreEqual(p.HasExited, True)
    End Sub

End Class
