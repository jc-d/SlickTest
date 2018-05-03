'Slick Test Developer, Copyright (c) 2007-2009 Jeremy Carey-dressler
Imports APIControls
''' <summary>
''' This is where all the unreported logging is peformed.  This just writes all Log information
''' straight to a text file or the console.  The log is for things not intended for the offical
''' automation results, but instead designed to provide users the ability to keep track or tail
''' basic information.
''' </summary>
''' <remarks></remarks>
Public Class Log

    ''' <summary>
    ''' Enables or disables logging.
    ''' </summary>
    ''' <remarks></remarks>
    Public Shared WriteDebugInfo As Boolean = True
    ''' <summary>
    ''' Location for the log file.
    ''' </summary>
    ''' <remarks>This is by default saves to C:\temp.log</remarks>
    Public Shared FileLogLocation As String = "C:\temp.log"
    Private Shared SW As System.IO.StreamWriter
    ''' <summary>
    ''' If this is set to 0 then the file will never be deleted.
    ''' </summary>
    ''' <remarks></remarks>
    Public Shared FileSizeBeforeDeleteInKB As Long = 12400000

    ''' <summary>
    ''' The filter for logging.  The higher the number, the less information that will appear in the log.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks>Typically uses a convention between 1 and 10, where 1 is the most inclusive, and
    ''' 10 is the most exclusive in what is logged.</remarks>
    Public Shared Property DefaultFilter() As Byte
        Get
            Return PrivateFilter
        End Get
        Set(ByVal value As Byte)
            If (value > 0 AndAlso value <= 10) Then
                PrivateFilter = value
            Else
                Throw New SlickTestUIException("Filter value " & value & " is invalid.")
            End If
        End Set
    End Property
    ''' <summary>
    ''' If this is set to false, then it will automatically write to the console.
    ''' </summary>
    ''' <remarks>If the console is set to not appear in your project, then
    ''' the data will still be written to the stream, but will not be visible 
    ''' to the user.</remarks>
    Public Shared StoreLogLocation As LogLocation = LogLocation.ToConsole
    Private Shared InUse As Boolean = False
    Private Shared PrivateFilter As Byte = 1
    Private Shared PrivateDefaultFilter As Byte = 1



    ''' <summary>
    ''' Location that the log will write to.
    ''' </summary>
    ''' <remarks>This allows the logger to write to a file, the console (command window) or both.</remarks>
    Public Enum LogLocation As Byte
        ''' <summary>
        ''' Log only to a file, based upon the file location given.
        ''' </summary>
        ''' <remarks></remarks>
        ToFile
        ''' <summary>
        ''' Log only to the console (command window).  If you shut off the console in 
        ''' your project, you will never see this data.  
        ''' </summary>
        ''' <remarks></remarks>
        ToConsole
        ''' <summary>
        ''' Logs both to the file, based upon the location give and the console
        ''' (command window).
        ''' </summary>
        ''' <remarks></remarks>
        ToFileAndConsole
    End Enum

    ''' <summary>
    ''' The filter for logging.  The higher the number, the less information that will appear in the log.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks>Typically uses a convention between 1 and 10, where 1 is the most inclusive, and
    ''' 10 is the most exclusive in what is logged.</remarks>
    Public Shared Property Filter() As Byte
        Get
            Return PrivateFilter
        End Get
        Set(ByVal value As Byte)
            If (value > 0 AndAlso value <= 10) Then
                PrivateFilter = value
            Else
                Throw New SlickTestUIException("Filter value " & value & " is invalid.")
            End If
        End Set
    End Property

    ''' <summary>
    ''' All information sent to log info will be logged, based upon the default filter value.  
    ''' It will not log info if WriteDebugInfo is set to false.
    ''' </summary>
    ''' <param name="LogInfo">Information to be logged.</param>
    ''' <exception cref="Exception">When the user is unable to access the file, an exception is thrown.</exception>
    ''' <remarks>Deletes logs after the log gets to be about a certain size.<p/>
    ''' <p/>
    ''' If the logger fails to log due to an error, it will provide a messagebox,
    ''' as that information can't be logged.<p/>
    ''' <p/>
    ''' If the file is already being opened, (due to multithreading, not because another 
    ''' app is using it) then the logger will keep trying to log the information
    ''' when it can.<p/>
    ''' </remarks>
    Public Shared Sub LogData(ByVal LogInfo As String)
        DoLogging.LogData(LogInfo, DefaultFilter)
    End Sub

    ''' <summary>
    ''' All information sent to log info will be logged, regardless of filter value.  
    ''' It will not log info if WriteDebugInfo is set to false.
    ''' </summary>
    ''' <param name="LogInfo">Information to be logged</param>
    ''' <exception cref="Exception">When the user is unable to access the file, an exception is thrown.</exception>
    ''' <remarks>Deletes logs after the log gets to be about a certain size.<p/>
    ''' <p/>
    ''' If the logger fails to log due to an error, it will provide a messagebox,
    ''' as that information can't be logged.<p/>
    ''' <p/>
    ''' If the file is already being opened, (due to multithreading, not because another 
    ''' app is using it) then the logger will keep trying to log the information
    ''' when it can.<p/>
    ''' </remarks>
    Public Shared Sub Log(ByVal LogInfo As String)
        DoLogging.LogData(LogInfo)
    End Sub

    ''' <summary>
    ''' Logs the information, if and only if the current filter is lower than the filter 
    ''' given for logging the information.
    ''' </summary>
    ''' <param name="LogInfo">Information to be logged.</param>
    ''' <param name="PriorityFilter">Filter value typically goes between 1 and 10, although 
    ''' it is not technically limited to those values.  The higher the value, the more likely it is 
    ''' to be logged.</param>
    ''' <remarks></remarks>
    Public Shared Sub LogData(ByVal LogInfo As String, ByVal PriorityFilter As Byte)
        DoLogging.LogData(LogInfo, PriorityFilter)
    End Sub

    Protected Friend Sub New()

    End Sub

    Private Class DoLogging
        Public Shared Sub LogData(ByVal LogInfo As String, ByVal PriorityFilter As Byte)
            If (PriorityFilter >= Filter) Then
                LogData(LogInfo)
            End If
        End Sub
        Public Shared Sub LogData(ByVal LogInfo As String)
            If (InUse = True) Then 'just wait to see if access is possible
                Dim timer As Integer = 0
                While (InUse = True)
                    System.Threading.Thread.Sleep(4)
                    timer += 1
                    If (timer > 200) Then
                        Throw New SlickTestAPIException("Logger unable to access log file.  Log attempt was for: " & LogInfo)
                    End If
                End While
            End If
            InUse = True
            Try
                If (StoreLogLocation = LogLocation.ToConsole Or StoreLogLocation = LogLocation.ToFileAndConsole) Then
                    Console.WriteLine(LogInfo)
                End If
                If (WriteDebugInfo = True) Then
                    If (StoreLogLocation = LogLocation.ToFile Or StoreLogLocation = LogLocation.ToFileAndConsole) Then
                        SW = New System.IO.StreamWriter(FileLogLocation, True)
                        SW.WriteLine(LogInfo)
                        SW.Close()
                        If (FileSizeBeforeDeleteInKB <> 0) Then
                            If (Microsoft.VisualBasic.FileSystem.FileLen(FileLogLocation) > FileSizeBeforeDeleteInKB) Then
                                System.IO.File.Delete(FileLogLocation)
                            End If
                            InUse = False
                        End If
                    End If
                End If
            Catch ex As Exception
                Throw ex
                'Throw Exception("Logger unable to access log file.  Log attempt was for: " & LogInfo)
                'MessageBox.Show("Exception occured while trying to log.  Please write " & _
                '"this exception down as it will not be logged. Exception: " & ex.ToString & vbNewLine & _
                '"This was attempting to log the following: " & LogInfo, "", "LogError", System.Windows.Forms.MessageBoxIcon.Error, _
                'System.Windows.Forms.MessageBoxDefaultButton.Button1, 10, False, False)
            End Try
            InUse = False
        End Sub

    End Class
End Class