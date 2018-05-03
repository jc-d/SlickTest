Public Class ErrorConsoleHandler

    Private Shared StdOutputStream As System.IO.StringWriter = Nothing
    Private Shared ForwardStreamWriter As System.IO.StreamWriter = Nothing
    Private Shared ConsoleOutWriter As System.IO.TextWriter = Nothing

    Public Shared Sub AttachConsoleErrorProcess()
        If (StdOutputStream Is Nothing) Then
            ConsoleOutWriter = Console.Error
            ForwardStreamWriter = New System.IO.StreamWriter(System.Console.OpenStandardError())
            ForwardStreamWriter.AutoFlush = True
            Console.SetError(ForwardStreamWriter)
            StdOutputStream = New System.IO.StringWriter() 'System.Console.OpenStandardOutput())
            Console.SetError(StdOutputStream)
        End If
    End Sub

    Public Shared Function GetCurrentText() As String
        Dim txt As String = String.Empty
        If (Not StdOutputStream Is Nothing) Then
            StdOutputStream.Flush()
            ForwardStreamWriter.Flush()
            txt = StdOutputStream.ToString()
        End If
        Return txt
    End Function

    Public Shared Function DetachConsoleProcess() As String
        Dim txt As String = GetCurrentText()
        If (Not StdOutputStream Is Nothing) Then
            StdOutputStream.Close()
            ForwardStreamWriter.Close()
            StdOutputStream.Dispose()
            ForwardStreamWriter.Dispose()
            StdOutputStream = Nothing
            ForwardStreamWriter = Nothing
            Console.SetOut(ConsoleOutWriter)
        End If
        Return txt
    End Function

End Class
