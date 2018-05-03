Imports System.CodeDom.Compiler

Public Class CSharpCompiler
    Inherits AbstractCompiler

    Private codeProvider As Microsoft.CSharp.CSharpCodeProvider

    Public Overrides Function Compile(ByVal Execute As Boolean, ByVal Args As String) As Boolean
        codeProvider = New Microsoft.CSharp.CSharpCodeProvider()

        For Each asm As String In IncludedAssemblies
            If Not Parameters.ReferencedAssemblies.Contains(asm) Then
                Parameters.ReferencedAssemblies.Add(asm)
            End If
        Next
        Parameters.IncludeDebugInformation = CompileWithDebugInformation
        Parameters.OutputAssembly = ExecutablePath + Me.ExecutableName
        Parameters.TreatWarningsAsErrors = TreatWarningsAsErrors
        Parameters.MainClass = StartingClass
        Dim counter As Integer = 0

        Parameters.CompilerOptions += Me.AdditionalCompilerOptions

        If CreateDLL = True Then
            Parameters.CompilerOptions += " /target:library"
        End If

        Parameters.CompilerOptions = Parameters.CompilerOptions.TrimStart(" "c)

        While IsRunning = True
            System.Threading.Thread.Sleep(1000)
            counter += 1
            If counter > 10 Then
                Throw New Exception("Unable to excute command.  Another proccess" & _
                                    " is already running and has not exited after a reasonable amount" & _
                                    " of time.")
            End If
        End While
        Try
            If System.IO.File.Exists(Parameters.OutputAssembly) = True Then
                System.IO.File.Delete(Parameters.OutputAssembly)
            End If
        Catch
            Throw New Exception("Unable to delete previously compiled assembly.  Is " & _
                                System.IO.Path.GetFullPath(Parameters.OutputAssembly) & _
                                " currently running?")
        End Try

        If CompileMethod = CompileType.FromFiles Then
            Results = codeProvider.CompileAssemblyFromFile(Parameters, FilePaths.ToArray())
        Else
            Results = codeProvider.CompileAssemblyFromSource(Parameters, SourceCode.ToArray())
        End If

        If Results.Errors.Count > 0 Then
            Dim i As Integer = 0
            For i = 0 To Results.Errors.Count - 1
                If (Results.Errors.Item(i) IsNot Nothing) Then
                    Me.Errors.Add("In '" & Results.Errors.Item(i).FileName & _
                    "' ( Line:" & Results.Errors.Item(i).Line & ", Col:" & _
                    Results.Errors.Item(i).Column & ") error number " & _
                    Results.Errors.Item(i).ErrorNumber & " occurred.  Description: " & _
                    Results.Errors.Item(i).ErrorText)
                End If
                i += 1
            Next
            Return False
        Else
            If Execute = True Then
                StartDateTime = System.DateTime.Now
                If ShowConsoleUI = True Then
                    Dim p As New System.Diagnostics.ProcessStartInfo()
                    p.FileName = Results.PathToAssembly
                    p.Arguments = Args
                    Me.UserProcess = System.Diagnostics.Process.Start(p)
                    Me.UserProcess.EnableRaisingEvents = True
                    AddHandler UserProcess.Exited, AddressOf UserProcess_Exited
                Else
                    Dim p As New System.Diagnostics.ProcessStartInfo()
                    p.CreateNoWindow = True
                    p.FileName = Results.PathToAssembly
                    p.Arguments = Args
                    p.WindowStyle = ProcessWindowStyle.Hidden
                    Me.UserProcess = System.Diagnostics.Process.Start(p)
                    Me.UserProcess.EnableRaisingEvents = True
                    AddHandler UserProcess.Exited, AddressOf UserProcess_Exited
                End If
            End If
            Return True
        End If
    End Function

    Public Overrides Property OptionExplicit() As Boolean
        Get
            Return False
        End Get
        Set(ByVal value As Boolean)
            'Ignored
        End Set
    End Property

    Public Overrides Property OptionStrict() As Boolean
        Get
            Return False
        End Get
        Set(ByVal value As Boolean)
            'Ignored
        End Set
    End Property
End Class
