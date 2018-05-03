Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.CodeDom
Imports System.CodeDom.Compiler
Imports Microsoft.VisualBasic
Imports System.Diagnostics

Public MustInherit Class AbstractCompiler
    Implements ICompiler

    Protected StartDateTime As DateTime
    Public MustOverride Function Compile(ByVal Execute As Boolean, ByVal Args As String) As Boolean Implements ICompiler.Compile
    Public MustOverride Property OptionExplicit() As Boolean Implements ICompiler.OptionExplicit
    Public MustOverride Property OptionStrict() As Boolean Implements ICompiler.OptionStrict
    Protected Parameters As System.CodeDom.Compiler.CompilerParameters

    Public Overridable Sub Reset() Implements ICompiler.Reset
        ErrorMsgs = New System.Collections.Generic.List(Of String)()
        IncludedAssemblies = New System.Collections.Generic.List(Of String)()
        FilePaths = New System.Collections.Generic.List(Of String)()
        ExecutablePath = ".\"
        ExecutableName = "Out.exe"
        StartingClass = ""
        DebugInfo = False
        CreateDLL = False
        Parameters = New CompilerParameters()
        Parameters.GenerateExecutable = True
        Parameters.CompilerOptions = "/optimize"
        CompileMethod = CompileType.FromFiles
        CompilerOptions = ""
    End Sub

    Protected Shared HasInit As Boolean = False

    Protected Sub Init()
        If (Not HasInit) Then
            UserProcess = New Process()
            Me.UserProcess.EnableRaisingEvents = True
            AddHandler UserProcess.Exited, AddressOf UserProcess_Exited
        End If
    End Sub

    Public Event ExecutionComplete(ByVal ExitCode As Integer, ByVal StartDateTime As Date) Implements ICompiler.ExecutionComplete

    Public Property CompileMethod() As CompileType Implements ICompiler.CompileMethod
        Get
            Return CompilerMethodType
        End Get
        Set(ByVal value As CompileType)
            CompilerMethodType = value
        End Set
    End Property
    Private CompilerMethodType As CompileType

    

    Private CompilerOptions As String
    Public Property AdditionalCompilerOptions() As String Implements ICompiler.AdditionalCompilerOptions
        Get
            If (String.IsNullOrEmpty(CompilerOptions)) Then Return ""
            Return " " + CompilerOptions
        End Get
        Set(ByVal value As String)
            CompilerOptions = value
        End Set
    End Property

    Protected Results As CompilerResults
    Public ReadOnly Property ErrorResults() As CompilerErrorCollection Implements ICompiler.ErrorResults
        Get
            Return Results.Errors
        End Get
    End Property

    Private ErrorMsgs As System.Collections.Generic.List(Of String)
    Public ReadOnly Property Errors() As System.Collections.Generic.List(Of String) Implements ICompiler.Errors
        Get
            Return ErrorMsgs
        End Get
    End Property

    Private IncludeAsm As System.Collections.Generic.List(Of String)
    Public Property IncludedAssemblies() As System.Collections.Generic.List(Of String) Implements ICompiler.IncludedAssemblies
        Get
            Return IncludeAsm
        End Get
        Set(ByVal value As System.Collections.Generic.List(Of String))
            IncludeAsm = value
        End Set
    End Property

    Private FileLocation As System.Collections.Generic.List(Of String)
    Public Property FilePaths() As System.Collections.Generic.List(Of String) Implements ICompiler.FilePaths
        Get
            Return FileLocation
        End Get
        Set(ByVal value As System.Collections.Generic.List(Of String))
            FileLocation = value
        End Set
    End Property

    Private ConsoleVisible As Boolean
    Public Property ShowConsoleUI() As Boolean Implements ICompiler.ShowConsoleUI
        Get
            Return ConsoleVisible
        End Get
        Set(ByVal value As Boolean)
            ConsoleVisible = value
        End Set
    End Property

    Private BuildDLL As Boolean
    Public Property CreateDLL() As Boolean Implements ICompiler.CreateDLL
        Get
            Return BuildDLL
        End Get
        Set(ByVal value As Boolean)
            BuildDLL = value
        End Set
    End Property

    Private Source As System.Collections.Generic.List(Of String)
    Public Property SourceCode() As System.Collections.Generic.List(Of String) Implements ICompiler.SourceCode
        Get
            Return Source
        End Get
        Set(ByVal value As System.Collections.Generic.List(Of String))
            Source = value
        End Set
    End Property

    Private DebugInfo As Boolean
    Public Property CompileWithDebugInformation() As Boolean Implements ICompiler.CompileWithDebugInformation
        Get
            Return DebugInfo
        End Get
        Set(ByVal value As Boolean)
            DebugInfo = value
        End Set
    End Property

    Private WarningsAsErrors As Boolean
    Public Property TreatWarningsAsErrors() As Boolean Implements ICompiler.TreatWarningsAsErrors
        Get
            Return WarningsAsErrors
        End Get
        Set(ByVal value As Boolean)
            WarningsAsErrors = value
        End Set
    End Property

    Private OutputExeName As String
    Public Property ExecutableName() As String Implements ICompiler.ExecutableName
        Get
            Return ConvertFromExeToDll(OutputExeName, CreateDLL)
        End Get
        Set(ByVal value As String)
            If value.ToLowerInvariant().EndsWith(".exe") = False Then
                If value.ToLowerInvariant().EndsWith(".dll") = False Then
                    OutputExeName = value + ".exe"
                    Return
                End If
            End If
            OutputExeName = value
        End Set
    End Property

    Private Shared Function ConvertFromExeToDll(ByVal FileName As String, ByVal CreateDLL As Boolean)
        If (CreateDLL) Then
            If (FileName.ToLowerInvariant().EndsWith(".exe")) Then
                FileName = FileName.Substring(0, FileName.Length - 4) & ".dll"
            End If
        Else 'create exe
            If (FileName.ToLowerInvariant().EndsWith(".dll")) Then
                FileName = FileName.Substring(0, FileName.Length - 4) & ".exe"
            End If
        End If
        Return FileName
    End Function

    Private MainClass As String
    Public Property StartingClass() As String Implements ICompiler.StartingClass
        Get
            Return MainClass
        End Get
        Set(ByVal value As String)
            MainClass = value
        End Set
    End Property

    Private OutputExePath As String
    Public Property ExecutablePath() As String Implements ICompiler.ExecutablePath
        Get
            Return OutputExePath
        End Get
        Set(ByVal value As String)
            If System.IO.Directory.Exists(value) = True Then
                If value.EndsWith("\") = False Then
                    OutputExePath = value + "\"
                Else
                    OutputExePath = value
                End If
            Else
                Throw New Exception("Invalid directory location.  Directory must " & _
                                    "exist before setting the Executable Path.")
            End If
        End Set
    End Property

    Public ReadOnly Property ExecutableFilePath() As String Implements ICompiler.ExecutableFilePath
        Get
            If ExecutablePath.EndsWith("\") = False Then
                ExecutablePath += "\"
            End If

            Return ExecutablePath & ExecutableName
        End Get
    End Property

    Protected UserProcess As System.Diagnostics.Process

    Protected Sub UserProcess_Exited(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim ExitResult As Integer = 0
        Try
            ExitResult = UserProcess.ExitCode
        Catch ex As Exception
        End Try
        RaiseEvent ExecutionComplete(ExitResult, StartDateTime)
    End Sub

    Public ReadOnly Property IsRunning() As Boolean Implements ICompiler.IsRunning
        Get
            If UserProcess IsNot Nothing Then
                Try
                    Return Not UserProcess.HasExited
                Catch
                End Try
            End If
            Return False
        End Get
    End Property

    Public Function KillProcess() As Boolean Implements ICompiler.KillProcess
        If UserProcess IsNot Nothing Then
            Try
                UserProcess.Kill()
            Catch
            End Try
            Return True
        End If
        Return False
    End Function

End Class