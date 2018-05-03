Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.CodeDom
Imports System.CodeDom.Compiler
Imports Microsoft.VisualBasic
Imports System.Diagnostics

Public Interface ICompiler
    Event ExecutionComplete(ByVal ExitCode As Integer, ByVal StartDateTime As DateTime)

    ReadOnly Property ErrorResults() As CompilerErrorCollection
    Property CompileMethod() As CompileType
    Property OptionExplicit() As Boolean
    Property OptionStrict() As Boolean
    Property ShowConsoleUI() As Boolean
    Property CreateDLL() As Boolean
    Property CompileWithDebugInformation() As Boolean
    Property TreatWarningsAsErrors() As Boolean
    ReadOnly Property IsRunning() As Boolean
    Property IncludedAssemblies() As System.Collections.Generic.List(Of String)
    ReadOnly Property Errors() As System.Collections.Generic.List(Of String)
    Property FilePaths() As System.Collections.Generic.List(Of String)
    Property SourceCode() As System.Collections.Generic.List(Of String)
    Property ExecutableName() As String
    Property AdditionalCompilerOptions() As String
    Property StartingClass() As String
    Property ExecutablePath() As String
    ReadOnly Property ExecutableFilePath() As String
    Function KillProcess() As Boolean
    Sub Reset()
    Function Compile(ByVal Execute As Boolean, ByVal Args As String) As Boolean
End Interface

Public Enum CompileType As Integer
    FromFiles
    SourceCode
End Enum