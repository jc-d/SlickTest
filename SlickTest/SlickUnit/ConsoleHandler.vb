'http://bytes.com/topic/visual-basic-net/answers/384371-getstdhandle-net
Public Class ConsoleHandler
    Private Shared StdOutputStream As System.IO.StringWriter = Nothing
    Private Shared ForwardStreamWriter As System.IO.StreamWriter = Nothing
    Private Shared ConsoleOutWriter As System.IO.TextWriter = Nothing
    Private Shared Index As Long = 0

    Public Shared Sub AttachConsoleProcess()
        If (StdOutputStream Is Nothing) Then
            ConsoleOutWriter = Console.Out
            ForwardStreamWriter = New System.IO.StreamWriter(System.Console.OpenStandardOutput())
            ForwardStreamWriter.AutoFlush = True
            Console.SetOut(ForwardStreamWriter)
            StdOutputStream = New System.IO.StringWriter()
            Console.SetOut(StdOutputStream)
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

    Public Shared Function GetLatestText() As String
        Dim txt As String = String.Empty
        If (Not StdOutputStream Is Nothing) Then
            StdOutputStream.Flush()
            ForwardStreamWriter.Flush()
            txt = StdOutputStream.ToString()
            Dim tmpIndex As Long = Index
            If (Index > txt.Length) Then
                Index = txt.Length
                Return txt
            End If
            Index = txt.Length
            Return txt.Substring(tmpIndex)
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
        Index = 0
        Return txt
    End Function

End Class


