Imports NUnit.Framework
Imports UIControls

Public Class WebTests

#Region "Additional test attributes"

    Protected SiteUrl As String = System.IO.Path.GetFullPath(".\WebPages\TableSample.html")
    Protected Shared SiteTitle As String = "Slick Test*"
    Protected i As UIControls.InterAct
    Protected Shared RunKill As Boolean = True
    Private internalDesc As Description

    Protected ReadOnly Property WebBrowser() As IEWebBrowser
        Get
            Return i.IEWebBrowser(internalDesc)
        End Get
    End Property
    '
    'You can use the following additional attributes as you write your tests:
    '
    'Use ClassInitialize to run code before running the first test in the class
    <TestFixtureSetUp()> _
    Public Overridable Sub MyClassInitialize()
        KillWebBrowsers(True)
    End Sub

    Protected Shared Sub KillWebBrowsers(ByVal WithWarning As Boolean)
        If (RunKill) Then
            For Each p As System.Diagnostics.Process In System.Diagnostics.Process.GetProcessesByName("iexplore")
                'If (p.MainWindowTitle.Contains(SiteTitle.Replace("*", ""))) Then
                If (WithWarning = True) Then UIControls.Alert.Show(SiteTitle & " browser will be closed in 20 seconds.", , , False)
                p.CloseMainWindow()
                p.WaitForExit(2000)
                'End If
            Next
        End If
    End Sub

    'Use TestInitialize to run code before running each test
    <SetUp()> _
    Public Overridable Sub MyTestInitialize()
        KillWebBrowsers(False)

        Console.WriteLine("Opening: " & SiteUrl & " with title " & SiteTitle)
        i = New UIControls.InterAct()
        i.OverrideReporter(New UIControls.StubbedReport)
        Dim s As New System.Diagnostics.ProcessStartInfo
        s.FileName = "iexplore.exe"
        s.Arguments = SiteUrl
        internalDesc = i.StartProgram(s)
        WebBrowser.WaitTillCompleted()
        Console.WriteLine("Start Time: " & System.DateTime.Now.ToLongTimeString())
    End Sub
    '
    'Use TestCleanup to run code after each test has run
    <TearDown()> _
    Public Sub MyTestCleanup()
        Console.WriteLine("End Time: " & System.DateTime.Now.ToLongTimeString())
        KillWebBrowsers(False)
    End Sub
    '
#End Region


End Class
