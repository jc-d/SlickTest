
Public Enum HookState As Short
    Uninstalled = 0
    Installed = 1
End Enum

''' <summary>
''' Provides data for the WindowsHookLib.LLMouseHook.StateChanged event.
''' </summary>
Public Class StateChangedEventArgs
    Inherits System.EventArgs

    Dim _state As HookState

    Sub New(ByVal llmh As LLMouseHook)
        Me._state = llmh.GetState
    End Sub

    Sub New(ByVal llkh As LLKeyboardHook)
        Me._state = llkh.GetState
    End Sub

    ''' <summary>
    ''' Gets a value indicating whether the mouse hook is installed. 
    ''' </summary>
    ReadOnly Property State() As HookState
        Get
            Return Me._state
        End Get
    End Property

End Class
