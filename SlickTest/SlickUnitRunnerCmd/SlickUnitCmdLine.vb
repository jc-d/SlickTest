Imports SlickUnit

Module SlickUnitCmdLine
    Private Framework As SlickUnit.Framework
    Private Runner As IRunner
    Public Data As New SetupData()
    Private Info As SlickUnit.IRunnerInfo

    Public WithEvents timer As New Timers.Timer(10)

    Public Sub Main(ByVal Args As String())

        For Each value As String In Args
            If (ProcessArgs(value) = False) Then Return
        Next

        Framework = New Framework()
        Framework.AutomaticallyAttachConsoleHandler = False
        Framework.UseInternalDomain = False
        Framework.Init()
        Framework.InitDefaultExcludeItems()

        For Each file As String In Data.SearchedDllFiles
            Framework.AddDll(file, True)
        Next

        For Each file As String In Data.NotSearchedDllFiles
            Framework.AddDll(file, False)
        Next
        Info = New SlickUnit.RunnerInfo()
        Info.NotLikeMethodName = New System.Collections.Generic.List(Of String)
        Info.LikeMethodName = New System.Collections.Generic.List(Of String)


        For Each item As String In Data.Exclude.Split("|"c)
            If (Not String.IsNullOrEmpty(item)) Then
                Framework.ExcludeFilter.Add(item)
                Info.NotLikeMethodName.Add(item)
            End If
        Next

        For Each item As String In Data.Include.Split("|"c)
            If (Not String.IsNullOrEmpty(item)) Then
                Framework.OnlyIncludeFilter.Add(item)
                Info.LikeMethodName.Add(item)
            End If
        Next
        'file cleanup
        If (Not String.IsNullOrEmpty(Data.OutputFile) AndAlso System.IO.File.Exists(Data.OutputFile)) Then
            System.IO.File.Delete(Data.OutputFile)
        End If
        If (Not String.IsNullOrEmpty(Data.JustResultsFile) AndAlso System.IO.File.Exists(Data.JustResultsFile)) Then
            System.IO.File.Delete(Data.JustResultsFile)
        End If

        If (Framework.SearchedDllLocations.Count = 0) Then
            WriteData("No Dlls were listed.  Unable to to find any tests.")
            Return
        End If

        Framework.LoadDlls()

        If (Data.GetFramework() = SetupData.FrameworkType.SlickUnit) Then
            Runner = New SlickRunner(Framework)
        Else
            Runner = New NUnitRunner(Framework)
        End If
        If (Data.DisplayMethod) Then
            WriteData(Framework.GetValidMethodStrings(Runner.TestAttributeType, Runner.ClassAttributeToRunType))
        Else
            Dim t As Type

            Info.Framework = Framework
            Info.ExactCase = False
            If (ShouldCaptureInFile()) Then
                Info.AutomaticallyAttachConsoleHandler = True
            Else
                Info.AutomaticallyAttachConsoleHandler = False
            End If
            Info.ExactCase = Data.UseExactName
            Runner.InitTypes(Framework)
            timer.Start()
            Dim Text As String = Data.BaseNameSpaceOrClassOrMethod
            Select Case Data.TypeOfBase
                Case SetupData.BaseType.MethodName
                    t = Framework.GetTypeByString(Text.Substring(0, Text.LastIndexOf(".")))
                    Dim TestType As System.Type = Runner.TestAttributeType
                    Dim TestFixtureType As System.Type = Runner.ClassAttributeToRunType
                    Dim method As SlickUnit.MethodAndTypeInfo = Framework.GetValidMethodAndTypeInfo(TestType, TestFixtureType, t, Text.Substring(Text.LastIndexOf(".") + 1))(0)
                    Info.ClassTypeFilter = t
                    Info.Test = method
                    Runner.RunTestInClass(Info)
                    HandleResults(Runner.TestResults)
                Case SetupData.BaseType.ClassName
                    t = Framework.GetTypeByString(Text)
                    Info.ClassTypeFilter = t
                    Runner.RunTestsInClass(Info)
                    HandleResults(Runner.TestResults)
                Case SetupData.BaseType.NameSpaceName
                    'TODO
                    Info.ClassTypeAttributeFilter = Runner.ClassAttributeToRunType
                    Info.TestAttributeFilter = Runner.TestAttributeType
                    Info.FilteredNamespace = Data.BaseNameSpaceOrClassOrMethod
                    Runner.RunTestsInNamespace(Info)
                    Framework.Dispose()
                    HandleResults(Runner.TestResults)
                Case SetupData.BaseType.All
                    Dim MIList As System.Collections.Generic.List(Of MethodAndTypeInfo) = _
                    Framework.GetValidMethodAndTypeInfo(Runner.TestAttributeType, Runner.ClassAttributeToRunType)
                    Dim TypesRun As New System.Collections.Generic.List(Of String)()
                    For Each mi As MethodAndTypeInfo In MIList
                        If (TypesRun.Contains(mi.Type.FullName) = False) Then
                            TypesRun.Add(mi.Type.FullName)
                            Info.ClassTypeFilter = mi.Type
                            Info.ClassTypeAttributeFilter = Runner.ClassAttributeToRunType
                            Info.TestAttributeFilter = Runner.TestAttributeType
                            Runner.RunTestsInClass(Info)
                        End If
                    Next
                    Framework.Dispose()
                    HandleResults(Runner.TestResults)
            End Select
        End If
        timer_Elapsed(Nothing, Nothing) 'clear the buffer
        timer.Stop() 'stop the console grabber call.
        If (Data.Wait) Then
            Console.WriteLine("Press any key to continue.")
            Console.ReadKey()
        End If
    End Sub

    Public Sub HandleResults(ByVal Results As System.Collections.Generic.List(Of GenericTestResults))
        If (String.IsNullOrEmpty(Data.Xml)) Then
            WriteData("*************** Results *****************" & vbNewLine, True)

            For Each tr As GenericTestResults In Results
                WriteData(tr.ToString(), True)
                WriteData("********************************" & vbNewLine, True)
            Next
        Else
            Dim stream As New System.IO.FileStream(Data.Xml, IO.FileMode.Create, IO.FileAccess.Write, IO.FileShare.Delete)
            Dim xml As New System.Xml.XmlTextWriter(stream, System.Text.Encoding.UTF8)
            xml.Formatting = System.Xml.Formatting.Indented
            xml.Indentation = 4
            xml.WriteStartDocument(True)
            xml.WriteStartElement("TestResults")
            Dim NameSpaceAndCurrentClass As String = String.Empty
            Dim count As Integer = 0
            For Each tr As GenericTestResults In Results
                If (NameSpaceAndCurrentClass <> tr.NamespaceAndClassName OrElse count = 0) Then
                    If (count <> 0) Then xml.WriteEndElement()

                    NameSpaceAndCurrentClass = tr.NamespaceAndClassName

                    xml.WriteStartElement("TestResultsForAClass")
                    xml.WriteAttributeString("ClassName", tr.ClassName)

                    Dim elementCount As Integer = FindEndOfClassInResultsIndex(Results, NameSpaceAndCurrentClass, count)
                    xml.WriteAttributeString("TotalTimeTakenInClass", (Results(elementCount).EndClassCleanupTime - Results(count).StartTestSetupTime).ToString())

                    xml.WriteAttributeString("StartClassSetupTime", Results(count).StartClassSetupTime)
                    xml.WriteAttributeString("EndClassSetupTime", Results(count).EndClassSetupTime)
                    xml.WriteAttributeString("StartClassCleanupTime", Results(elementCount).StartClassCleanupTime)
                    xml.WriteAttributeString("EndClassCleanupTime", Results(elementCount).EndClassCleanupTime)
                    xml.WriteAttributeString("Namespace", tr.Namespace)

                End If
                xml.WriteStartElement("Test")
                xml.WriteAttributeString("Name", tr.TestName)
                xml.WriteAttributeString("Namespace", tr.Namespace)
                xml.WriteAttributeString("ClassName", tr.ClassName)
                xml.WriteAttributeString("FullName", tr.FullName)

                xml.WriteStartElement("TestResult")
                xml.WriteString(tr.TestResultString)
                xml.WriteEndElement()

                xml.WriteStartElement("ConsoleOutput")
                xml.WriteCData(tr.ConsoleOutput)
                xml.WriteEndElement()

                xml.WriteStartElement("ExceptionResults")
                xml.WriteCData(tr.ExceptionResultsString())
                xml.WriteEndElement()

                xml.WriteStartElement("StartTestTime")
                xml.WriteString(tr.StartTestTime)
                xml.WriteEndElement()

                xml.WriteStartElement("EndTestTime")
                xml.WriteString(tr.EndTestTime)
                xml.WriteEndElement()

                xml.WriteStartElement("StartTestSetupTime")
                xml.WriteString(tr.StartTestSetupTime)
                xml.WriteEndElement()

                xml.WriteStartElement("EndTestSetupTime")
                xml.WriteString(tr.EndTestSetupTime)
                xml.WriteEndElement()

                xml.WriteStartElement("StartTestCleanupTime")
                xml.WriteString(tr.StartTestCleanupTime)
                xml.WriteEndElement()

                xml.WriteStartElement("EndTestCleanupTime")
                xml.WriteString(tr.EndTestCleanupTime)
                xml.WriteEndElement()

                xml.WriteEndElement()
                count += 1
            Next

            xml.WriteEndElement()

            xml.WriteEndElement()
            xml.WriteEndDocument()
            xml.Flush()
            xml.Close()
        End If
    End Sub

    Private Function FindEndOfClassInResultsIndex(ByVal Results As System.Collections.Generic.List(Of GenericTestResults), ByVal namespaceAndClass As String, ByVal startIndex As Integer) As Integer
        For i As Integer = startIndex + 1 To Results.Count - 1
            If (Results(i).NamespaceAndClassName <> namespaceAndClass) Then Return i - 1
        Next
        Return Results.Count - 1
    End Function

    Public Sub SetupDirectoryCache()
        Dim DirectoryCachePath As String = Framework.GetCacheDirectory()
        Dim path As String = System.IO.Path.GetDirectoryName(DirectoryCachePath)
        Dim name As String = "SlickUnit.dll"

        If (path.EndsWith("\") = False) Then path += "\"
        path += "..\"
        If (System.IO.Directory.Exists(DirectoryCachePath) = False) Then
            Try
                System.IO.Directory.CreateDirectory(DirectoryCachePath)
            Catch ex As Exception
                WriteData("Unable to create folder " & DirectoryCachePath & ".  Please create folder manually.  " & _
                                                     "Slick Test may need to be restarted after creating the folder in order to get the system to work.")
            End Try
        End If
        Try
            System.IO.File.Copy(path & name, DirectoryCachePath & name, False)
        Catch ex As Exception
            WriteData(ex.ToString())
        End Try
    End Sub

    Public Function ShouldCaptureInFile() As Boolean
        Dim ShouldCapture As Boolean = Not String.IsNullOrEmpty(Data.OutputFile)
        If (ShouldCapture) Then Return True
        Return Not String.IsNullOrEmpty(Data.Xml)
    End Function

    Public Sub WriteData(ByVal text As String, Optional ByVal isResults As Boolean = False)
        If (ShouldCaptureInFile()) Then
            If (String.IsNullOrEmpty(Data.Xml)) Then
                If (isResults AndAlso Not String.IsNullOrEmpty(Data.JustResultsFile)) Then
                    WriteFile(Data.JustResultsFile, text)
                Else
                    WriteFile(Data.OutputFile, text)
                End If
            Else
                Console.WriteLine(text) 'Is this right?
            End If
        Else
            Console.WriteLine(text)
        End If
    End Sub

    Public Sub WriteFile(ByVal filename As String, ByVal text As String)
        WriteFileUnsafe(filename, text)
    End Sub

    Private Sub WriteFileUnsafe(ByVal filename As String, ByVal text As String)
        If (String.IsNullOrEmpty(text)) Then Return
        Dim sw As New System.IO.StreamWriter(filename, True, System.Text.Encoding.UTF8)
        sw.Write(text)
        sw.Flush()
        sw.Close()
        sw.Dispose()
    End Sub

    Public Function ProcessArgs(ByVal arg As String) As Boolean

        Dim tmpArg As String = arg.ToUpperInvariant()
        Dim argValue As String = String.Empty
        If (tmpArg.Contains(":") AndAlso tmpArg.Trim().LastIndexOf(":") <> 1) Then
            argValue = arg.Substring(arg.IndexOf(":"))
            tmpArg = tmpArg.Substring(0, arg.IndexOf(":"))
        End If
        If (tmpArg.StartsWith("/")) Then
            tmpArg = "-" + tmpArg.Substring(1)
        End If

        If (argValue.StartsWith(":")) Then
            argValue = argValue.Substring(1)
        End If
        Select Case tmpArg
            Case "-?"
                Console.WriteLine("Args supported: " & vbNewLine & _
                                                  "-Out:<aFile> - The output that is normally shown on the console may be redirected to a file. This includes output created by the test program as well as what SlickUnit itself creates." & vbNewLine & _
                                                  "-Results:<aFile> - Outputs just the results to a file (-out can be used, but the results will not be included in that file)." & vbNewLine & _
                                                  "(Not supported) -Err:<aFile> - The command redirects standard error output to the StdErr.txt file.  Currently not supported." & vbNewLine & _
                                                  "-Include:<NameSpaceOrClassOrTest> - Filters using wild card statement and splits on |." & vbNewLine & _
                                                  "-Exclude:<NameSpaceOrClassOrTest> - Filters using wild card statement and splits on |." & vbNewLine & _
                                                  "-Xml:File - Provides an XML file with the " & vbNewLine & _
                                                  "-ExactName - Searches for the exact name." & vbNewLine & _
                                                  "-AdditionalDlls:<aFile.dll> - Dlls that are not searched, but need to be included." & vbNewLine & _
                                                  "-Display - Prints out all methods in all searched dlls.  All filters are ignored." & vbNewLine & _
                                                  "-Wait - Waits for a key after the program has been run." & vbNewLine & _
                                                  "-RunInNamespace:<NameSpace> - Full or partial namespace for filtering.  Can't be used with RunInTest,RunInClass." & vbNewLine & _
                                                  "-RunInClass:<Class> - Full path, starting with the namespace, then the class.  Can't be used with RunInTest,RunInNamespace." & vbNewLine & _
                                                  "-RunInTest:<Test> - Full path, starting with the namespace, then the class, then the test.  Can't be used with RunInNamespace,RunInClass." & vbNewLine & _
                                                  "-Include:<NameSpaceOrClassOrTest> - Filters using wild card statement and splits on |." & vbNewLine & _
                                                  "aFile.dll")
                Return False
            Case "-OUT"
                Data.OutputFile = argValue
            Case "-RESULTS"
                Data.JustResultsFile = argValue
            Case "-ERR"
                WriteData("Currently not supported.")
                Data.ErrorFile = argValue
            Case "-INCLUDE"
                Data.Include = argValue
            Case "-EXCLUDE"
                Data.Exclude = argValue
            Case "-XML"
                Data.Xml = argValue
            Case "-WAIT"
                Data.Wait = True
            Case "-FRAMEWORK"
                Data.FrameWork = argValue.ToUpperInvariant().Trim()
            Case "-DISPLAY"
                Data.DisplayMethod = True
            Case "-RUNINNAMESPACE"
                Data.BaseNameSpaceOrClassOrMethod = argValue
                Data.TypeOfBase = SetupData.BaseType.NameSpaceName
            Case "-RUNINCLASS"
                Data.BaseNameSpaceOrClassOrMethod = argValue
                Data.TypeOfBase = SetupData.BaseType.ClassName
            Case "-RUNINTEST"
                Data.BaseNameSpaceOrClassOrMethod = argValue
                Data.TypeOfBase = SetupData.BaseType.MethodName
            Case "-EXACTNAME"
                Data.UseExactName = True
            Case "-ADDITIONALDLLS"
                If (argValue.EndsWith("DLL")) Then
                    Data.NotSearchedDllFiles.Add(argValue)
                End If
            Case Else
                If (tmpArg.EndsWith("DLL")) Then
                    Data.SearchedDllFiles.Add(tmpArg)
                Else
                    System.Console.WriteLine("Arg is not supported: " & arg)
                End If
        End Select
        Return True
    End Function

    Private Sub timer_Elapsed(ByVal sender As Object, ByVal e As System.Timers.ElapsedEventArgs) Handles timer.Elapsed
        timer.Stop()
        WriteData(SlickUnit.ConsoleHandler.GetLatestText())
        timer.Start()
    End Sub
End Module

Public Class SetupData
    Public OutputFile As String
    Public ErrorFile As String
    Public SearchedDllFiles As System.Collections.Generic.List(Of String)
    Public NotSearchedDllFiles As System.Collections.Generic.List(Of String)

    Public Include As String
    Public Exclude As String
    Public Xml As String
    Public Wait As Boolean
    Public FrameWork As String
    Public DisplayMethod As Boolean
    Public BaseNameSpaceOrClassOrMethod As String
    Public TypeOfBase As BaseType
    Public UseExactName As Boolean
    Public JustResultsFile As String

    Public Enum BaseType
        MethodName
        ClassName
        NameSpaceName
        All
    End Enum
    <Serializable()> _
    Public Enum FrameworkType
        NUnit
        SlickUnit
    End Enum

    Public Function GetFramework() As FrameworkType
        Select Case (FrameWork)
            Case "NUNIT"
                Return FrameworkType.NUnit
            Case Else
                Return FrameworkType.SlickUnit
        End Select
    End Function

    Public Sub New()
        SearchedDllFiles = New System.Collections.Generic.List(Of String)()
        NotSearchedDllFiles = New System.Collections.Generic.List(Of String)()
        Wait = False
        Include = ""
        Exclude = ""
        DisplayMethod = False
        BaseNameSpaceOrClassOrMethod = ""
        TypeOfBase = BaseType.All
        UseExactName = True
    End Sub

End Class
