''' <summary>
''' Provides data for the WindowsHookLib.LLMouseHook:
''' MouseDown, MouseUp, MouseMove, MouseWheel, 
''' MouseClick and MouseDoubleClick event.  
''' </summary>
Public Class LLMouseEventArgs
    Inherits MouseEventArgs

    Private _mHandled As Boolean

    ''' <summary>
    ''' Gets or sets a value indicating whether the event was handled.
    ''' </summary>
    Public Property Handled() As Boolean
        Get
            Return Me._mHandled
        End Get
        Set(ByVal value As Boolean)
            Me._mHandled = value
        End Set
    End Property

    Sub New(ByVal button As MouseButtons, ByVal clicks As Integer, _
    ByVal x As Integer, ByVal y As Integer, ByVal delta As Integer, _
    ByVal mHandled As Boolean)
        MyBase.New(button, clicks, x, y, delta)
        Me._mHandled = mHandled
    End Sub
End Class

