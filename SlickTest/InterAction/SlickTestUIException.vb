''' <summary>
''' Exceptions that occur in the front end of the Slick Test APIs.
''' </summary>
''' <remarks></remarks>
Public Class SlickTestUIException
    Inherits APIControls.SlickTestAPIException

    ''' <summary>
    ''' Creates a new exception.
    ''' </summary>
    ''' <param name="exMessage">Exception message to be raised.</param>
    ''' <remarks></remarks>
    Public Sub New(ByVal exMessage As String)
        MyBase.New(exMessage)
    End Sub

    ''' <summary>
    ''' Creates a new exception.
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub New()
        MyBase.New()
    End Sub

    ''' <summary>
    ''' Creates a new exception.
    ''' </summary>
    ''' <param name="exMessage">Exception message to be raised.</param>
    ''' <param name="innerException">Inner exception.</param>
    ''' <remarks></remarks>
    Public Sub New(ByVal exMessage As String, ByVal innerException As Exception)
        MyBase.New(exMessage, innerException)
    End Sub

    ''' <summary>
    ''' Creates a new exception.
    ''' </summary>
    ''' <param name="exMessage">Exception message to be raised.</param>
    ''' <param name="innerException">Inner exception.</param>
    ''' <param name="reporter">Reporter to record error.</param>
    ''' <remarks></remarks>
    Public Sub New(ByVal exMessage As String, ByVal innerException As Exception, ByVal reporter As IReport)
        MyBase.New(exMessage, innerException)
        reporter.RecordEvent(reporter.Fail, exMessage, "Error: " + Me.ToString())
    End Sub
End Class
