''' <summary>
''' This is where all the unreported logging is peformed.  This just writes all Log information
''' straight to a text file or the console.  The log is for things not intended for the offical
''' automation results, but instead designed to provide users the ability to keep track or tail
''' basic information.
''' </summary>
''' <remarks></remarks>
Public Interface ILog
    ''' <summary>
    ''' Enables or disables logging.
    ''' </summary>
    ''' <remarks></remarks>
    Property WriteDebugInfo() As Boolean

    ''' <summary>
    ''' Location for the log file.
    ''' </summary>
    ''' <remarks>This is by default saves to C:\temp.log</remarks>
    Property FileLogLocation() As String

    ''' <summary>
    ''' Rollover size in KB.  If this is set to 0 
    ''' then the file will never be deleted.
    ''' </summary>
    ''' <remarks></remarks>
    Property FileSizeBeforeDeleteInKB() As Long

    ''' <summary>
    ''' The filter for logging.  The higher the number, the less information that will appear in the log.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks>Typically uses a convention between 1 and 10, where 1 is the most inclusive, and
    ''' 10 is the most exclusive in what is logged.</remarks>
    Property DefaultFilter() As Byte

    ''' <summary>
    ''' If this is set to false, then it will automatically write to the console.
    ''' </summary>
    ''' <remarks>If the console is set to not appear in your project, then
    ''' the data will still be written to the stream, but will not be visible 
    ''' to the user.</remarks>
    Property StoreLogLocation() As LogLocation

    ''' <summary>
    ''' The filter for logging.  The higher the number, the less information that will appear in the log.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks>Typically uses a convention between 1 and 10, where 1 is the most inclusive, and
    ''' 10 is the most exclusive in what is logged.</remarks>
    Property Filter() As Byte

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
    Sub LogData(ByVal LogInfo As String)

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
    Sub Log(ByVal LogInfo As String)

    ''' <summary>
    ''' Logs the information, if and only if the current filter is lower than the filter 
    ''' given for logging the information.
    ''' </summary>
    ''' <param name="LogInfo">Information to be logged.</param>
    ''' <param name="PriorityFilter">Filter value typically goes between 1 and 10, although 
    ''' it is not technically limited to those values.  The higher the value, the more likely it is 
    ''' to be logged.</param>
    ''' <remarks></remarks>
    Sub LogData(ByVal LogInfo As String, ByVal PriorityFilter As Byte)
End Interface