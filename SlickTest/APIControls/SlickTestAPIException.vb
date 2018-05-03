''' <summary>
''' Internal exceptions that occur in the backend of the Slick Test APIs.
''' </summary>
''' <remarks></remarks>
Public Class SlickTestAPIException
    Inherits Exception

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

End Class
