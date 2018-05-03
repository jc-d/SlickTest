Imports System.runtime.InteropServices

Public Class Kernel32

#Region " ERROR PROVIDER APIS "

    <DllImport("kernel32", EntryPoint:="GetLastError")> _
    Public Shared Function GetLastError() As Integer
    End Function

    <DllImport("kernel32", EntryPoint:="FormatMessageA")> _
    Public Shared Function FormatMessage(ByVal dwFlags As Integer, _
    ByVal lpSource As Integer, ByVal dwMessageId As Integer, _
    ByVal dwLanguageId As Integer, ByRef lpBuffer As String, _
    ByVal nSize As Integer, ByVal Arguments As Integer) As Integer
    End Function

#End Region

#Region "CONSTANTS"

    Public Const FORMAT_MESSAGE_ALLOCATE_BUFFER As Integer = &H100
    Public Const FORMAT_MESSAGE_IGNORE_INSERTS As Integer = &H200
    Public Const FORMAT_MESSAGE_FROM_SYSTEM As Integer = &H1000
    Public Const LANG_NEUTRAL As Integer = &H0

#End Region

End Class
