'Slick Test Developer, Copyright (c) 2007-2010 Jeremy Carey-dressler
Imports APIControls

''' <summary>
''' This is where all the unreported logging is peformed.  This just writes all Log information
''' straight to a text file or the console.  The log is for things not intended for the offical
''' automation results, but instead designed to provide users the ability to keep track or tail
''' basic information.
''' </summary>
''' <remarks></remarks>
Public Class Log
    Private Shared Logger As ILog = New GenericLog()
    Private Shared SW As System.IO.StreamWriter

    ''' <summary>
    ''' Enables or disables logging.
    ''' </summary>
    ''' <remarks></remarks>
    Public Shared Property WriteDebugInfo() As Boolean
        Get
            Return Logger.WriteDebugInfo
        End Get
        Set(ByVal value As Boolean)
            Logger.WriteDebugInfo = value
        End Set
    End Property

    ''' <summary>
    ''' Location for the log file.
    ''' </summary>
    ''' <remarks>This is by default saves to C:\temp.log</remarks>
    Public Shared Property FileLogLocation() As String
        Get
            Return Logger.FileLogLocation
        End Get
        Set(ByVal value As String)
            Logger.FileLogLocation = value
        End Set
    End Property

    ''' <summary>
    ''' Rollover size in KB.  If this is set to 0 
    ''' then the file will never be deleted.
    ''' </summary>
    ''' <remarks></remarks>
    Public Shared Property FileSizeBeforeDeleteInKB() As Long
        Get
            Return Logger.FileSizeBeforeDeleteInKB
        End Get
        Set(ByVal value As Long)
            Logger.FileSizeBeforeDeleteInKB = value
        End Set
    End Property

    ''' <summary>
    ''' The filter for logging.  The higher the number, the less information that will appear in the log.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks>Typically uses a convention between 1 and 10, where 1 is the most inclusive, and
    ''' 10 is the most exclusive in what is logged.</remarks>
    Public Shared Property DefaultFilter() As Byte
        Get
            Return Logger.DefaultFilter
        End Get
        Set(ByVal value As Byte)
            Logger.DefaultFilter = value
        End Set
    End Property

    ''' <summary>
    ''' If this is set to false, then it will automatically write to the console.
    ''' </summary>
    ''' <remarks>If the console is set to not appear in your project, then
    ''' the data will still be written to the stream, but will not be visible 
    ''' to the user.</remarks>
    Public Shared Property StoreLogLocation() As LogLocation
        Get
            Return Logger.StoreLogLocation
        End Get
        Set(ByVal value As LogLocation)
            Logger.StoreLogLocation = value
        End Set
    End Property

    ''' <summary>
    ''' The filter for logging.  The higher the number, the less information that will appear in the log.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks>Typically uses a convention between 1 and 10, where 1 is the most inclusive, and
    ''' 10 is the most exclusive in what is logged.</remarks>
    Public Shared Property Filter() As Byte
        Get
            Return Logger.Filter
        End Get
        Set(ByVal value As Byte)
            Logger.Filter = value
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
        Logger.LogData(LogInfo, DefaultFilter)
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
        Logger.LogData(LogInfo)
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
        Logger.LogData(LogInfo, PriorityFilter)
    End Sub

    Protected Friend Sub New()
    End Sub

    ''' <summary>
    ''' Allows the user to override the logger and user their own logger.
    ''' </summary>
    ''' <param name="MyCustomLogger">Replacement Logger</param>
    ''' <remarks></remarks>
    Public Shared Sub OverrideLogger(ByVal MyCustomLogger As ILog)
        If (MyCustomLogger Is Nothing) Then Throw New SlickTestAPIException("CustomLogger is not initialized.")
        Logger = MyCustomLogger
    End Sub

End Class

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
    ''' <summary>
    ''' Stores to no location.
    ''' </summary>
    ''' <remarks></remarks>
    ToNone
    ''' <summary>
    ''' Not currently supported, but provided for users who override the logger.
    ''' </summary>
    ''' <remarks></remarks>
    ToDatabase
End Enum
