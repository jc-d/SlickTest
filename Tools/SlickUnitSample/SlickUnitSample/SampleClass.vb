Imports SlickUnit

<TestFixture()> _
Public Class SampleClass
    Private x As String = "Not Inited"
    Private ProjectFilePath As String = System.IO.Directory.GetCurrentDirectory() & "\" & "SlickUnitSample.stp"
    <Test()> _
    Public Sub Test2()
        Dim s As String = x
        Writer.WriteLine("Test2 - " & s)
    End Sub
    <Test()> _
    Public Sub Test1()
        Writer.WriteLine("Test1")
    End Sub
    <Test()> _
    Public Sub DirectXAccess()
        Writer.WriteLine("DirectXAccess; x = " & x)
    End Sub
    <Test()> _
     Public Sub UsingReporter()
        Writer.WriteLine("Attempting to load project file " & ProjectFilePath)
        'Dim i As New UIControls.InterAct(ProjectFilePath)
        'i.Report.RecordEvent(i.Report.Pass, "This is a test...", "A report should appear.")

    End Sub
    <SetUp()> _
    Public Sub MyTestInitialize()
        Writer.WriteLine("Setup Test; x gets set")
        x = "Setup"
        Writer.WriteLine("x set.")
    End Sub

    <TestFixtureSetUp()> _
    Public Sub MyClassInitialize()
        Writer.WriteLine("MyClassInitialize")
        ProjectFilePath = System.IO.Directory.GetCurrentDirectory() & "\" & "SlickUnitSample.stp"
        Writer.WriteLine(System.IO.Directory.GetCurrentDirectory() & "\" & "SlickUnitSample.stp")
        Writer.WriteLine("Filepath: " & ProjectFilePath)
        Writer.WriteLine("Does file exist? " & System.IO.File.Exists(ProjectFilePath))
        'Writer.WriteLine("Load Project File? " & UIControls.AutomationSettings.ProjectAlreadyLoaded)first run false, second run true...  shared data is not dropped.

        Dim i As New UIControls.InterAct(ProjectFilePath)
        Writer.WriteLine("Path loaded: " & UIControls.AutomationSettings.LoadedProjectFile)
        Writer.WriteLine("Reporter Location: " & i.Report.ToString())
        'Writer.WriteLine("ReportDatabasePath: " & UIControls.AutomationSettings.Project.ReportDatabasePath)
        i.Report.RecordEvent(i.Report.Pass, "This is a test... NOW: " & System.DateTime.Now, "A report should appear.")

        Writer.WriteLine("/Class Setup")
    End Sub

    <TearDown()> _
    Public Sub MyTestCleanup()
        Writer.WriteLine("Test Cleanup")
    End Sub

    <TestFixtureTearDown()> _
    Public Sub MyClassCleanup()
        Writer.WriteLine("Class Cleanup")
    End Sub



End Class

Public Class Writer
    Public Shared Sub WriteLine(ByVal s As String)
        Console.WriteLine(s)
        'SaveTextToFile(s, "C:\Temp\test.txt")
    End Sub

    Public Shared Function SaveTextToFile(ByVal strData As String, _
    ByVal FullPath As String) As Boolean

        Dim bAns As Boolean = False
        Dim objReader As System.IO.StreamWriter
        Try
            objReader = New System.IO.StreamWriter(FullPath, True)
            objReader.WriteLine(System.DateTime.Now & " - " & strData)
            objReader.Close()
            bAns = True
        Catch Ex As Exception

        End Try
        Return bAns
    End Function
End Class


<TestFixture()> _
Public Class SampleClassWithAsserts
    Private x As String = "Not Inited"
    Private Const constVal As String = "Setup"
    <Test()> _
    Public Sub Test2FailedAssert()
        Dim s As String = x
        Writer.WriteLine("Test2FailedAssert - " & s)
        Assert.AreEqual(1, 2)
    End Sub
    <Test()> _
    Public Sub Test1AreNotSame()
        Writer.WriteLine("Test1")
        Assert.AreNotSame(1, 2)
    End Sub
    <Test()> _
    Public Sub DirectXAccess()
        Writer.WriteLine("x = " & x)
        Assert.AreSame(constVal, x)
    End Sub

    <SetUp()> _
    Public Sub MyTestInitialize()
        x = constVal
        Writer.WriteLine("Setup Test; x gets set")
    End Sub

    <TestFixtureSetUp()> _
    Public Sub MyClassInitialize()
        Writer.WriteLine("Class Setup")
    End Sub

    <TearDown()> _
    Public Sub MyTestCleanup()
        Writer.WriteLine("Test Cleanup")
    End Sub

    <TestFixtureTearDown()> _
    Public Sub MyClassCleanup()
        Writer.WriteLine("Class Cleanup")
    End Sub

End Class
