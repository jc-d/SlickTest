''' <summary>
''' Provides data for the WindowsHookLib.LLKeyboardHook.KeyDown or 
''' WindowsHookLib.LLKeyboardHook.KeyUp event. 
''' </summary>
Public Class LLKeyEventArgs

    Private _keyCode, _
    _modifiers, _keyData As Keys
    Private _handled As Boolean

    ''' <summary>
    ''' Gets a value indicating whether the ALT key was pressed.
    ''' </summary>
    Public ReadOnly Property Alt() As Boolean
        Get
            Return (Me._modifiers And Keys.Alt) <> 0
        End Get
    End Property

    ''' <summary>
    ''' Gets a value indication whether the CTRL key was pressed.
    ''' </summary>
    Public ReadOnly Property Control() As Boolean
        Get
            Return (Me._modifiers And Keys.Control) <> 0
        End Get
    End Property

    ''' <summary>
    ''' Gets a value indication whether the SHIFT key was pressed.
    ''' </summary>
    Public ReadOnly Property Shift() As Boolean
        Get
            Return (Me._modifiers And Keys.Shift) <> 0
        End Get
    End Property

    ''' <summary>
    ''' Gets or sets a value indicating whether the event was handled.
    ''' </summary>
    Public Property Handled() As Boolean
        Get
            Return Me._handled
        End Get
        Set(ByVal value As Boolean)
            Me._handled = value
        End Set
    End Property

    ''' <summary>
    ''' Gets the keyboard code for a KeyDown or KeyUp event. 
    ''' </summary>
    Public ReadOnly Property KeyCode() As Keys
        Get
            Return Me._keyCode
        End Get
    End Property

    ''' <summary>
    ''' Gets the keyboard value for a KeyDown or KeyUp event. 
    ''' </summary>
    Public ReadOnly Property KeyValue() As Integer
        Get
            Return Me._keyCode
        End Get
    End Property

    ''' <summary>
    ''' Gets the key data for a KeyDown or KeyUp event. 
    ''' </summary>
    Public ReadOnly Property KeyData() As Keys
        Get
            Return Me._keyData
        End Get
    End Property

    ''' <summary>
    ''' Gets the modifier flags for a KeyDown or KeyUp event. 
    ''' The flags indicate which combination of CTRL, SHFT, 
    ''' and ALT keys was pressed.
    ''' </summary>
    Public ReadOnly Property Modifiers() As Keys
        Get
            Return Me._modifiers
        End Get
    End Property

    Sub New(ByVal kCode As Keys, ByVal kData As Keys, ByVal kModifiers As Keys, ByVal kHandled As Boolean)
        Me._keyCode = kCode
        Me._modifiers = kModifiers
        Me._keyData = kData
        Me._handled = kHandled
    End Sub

End Class
