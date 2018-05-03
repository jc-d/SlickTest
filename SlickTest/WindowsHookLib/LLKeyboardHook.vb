Imports System.runtime.InteropServices

Public Class LLKeyboardHook
    Implements IDisposable

#Region " EVENT HANDLERS "
    Public Event StateChanged(ByVal sender As Object, ByVal e As StateChangedEventArgs)
    Public Event KeyDown(ByVal sender As Object, ByVal e As LLKeyEventArgs)
    Public Event KeyUp(ByVal sender As Object, ByVal e As LLKeyEventArgs)
#End Region

#Region " CLASS VARIABLES "

    <MarshalAs(UnmanagedType.FunctionPtr)> _
    Private _keyboardProc As User32.WKCallBack
    Private Shared _hKeyboardHook As IntPtr
    Private _modifiers As Keys
    Private _keyData As Keys
    Private _handledKeys As New List(Of Integer)

#End Region

#Region " PUBLIC PROPERTIES "

    ''' <summary>
    ''' Gets a Boolean value indicating if the ALT key is down.
    ''' </summary>
    Public ReadOnly Property AltKeyDown() As Boolean
        Get
            Return (Me._modifiers And Keys.Alt) <> 0
        End Get
    End Property

    ''' <summary>
    ''' Gets a Boolean value indicating if the CTRL key is down.
    ''' </summary>
    Public ReadOnly Property CtrlKeyDown() As Boolean
        Get
            Return (Me._modifiers And Keys.Control) <> 0
        End Get
    End Property

    ''' <summary>
    ''' Gets a Boolean value indicating if the SHIFT key is down.
    ''' </summary>
    Public ReadOnly Property ShiftKeyDown() As Boolean
        Get
            Return (Me._modifiers And Keys.Shift) <> 0
        End Get
    End Property

#End Region

#Region " ON EVENT "

    Private Sub OnStateChanged()
        Dim e As New StateChangedEventArgs(Me)
        RaiseEvent StateChanged(Me, e)
    End Sub

    Private Function OnKeyUp(ByRef lParam As User32.KBDLLHOOKSTRUCT) As LLKeyEventArgs

        Dim keyCode As Keys = CType(lParam.vkCode, Keys)
        If keyCode = Keys.LMenu Or keyCode = Keys.RMenu Then
            Me._modifiers = Me._modifiers Xor Keys.Alt
            keyCode = Keys.Menu
        ElseIf keyCode = Keys.LControlKey Or keyCode = Keys.RControlKey Then
            Me._modifiers = Me._modifiers Xor Keys.Control
            keyCode = Keys.ControlKey
        ElseIf keyCode = Keys.LShiftKey Or keyCode = Keys.RShiftKey Then
            Me._modifiers = Me._modifiers Xor Keys.Shift
            keyCode = Keys.ShiftKey
        End If
        Me._keyData = keyCode Or Me._modifiers
        Dim kHankled As Boolean = Me._handledKeys.Contains(CInt(keyCode))
        Dim e As New LLKeyEventArgs(keyCode, Me._keyData, Me._modifiers, kHankled)
        RaiseEvent KeyUp(Me, e)
        Return e

    End Function

    Private Function OnKeyDown(ByRef lParam As User32.KBDLLHOOKSTRUCT) As LLKeyEventArgs

        Dim keyCode As Keys = CType(lParam.vkCode, Keys)
        If keyCode = Keys.LMenu Or keyCode = Keys.RMenu Then
            Me._modifiers = Me._modifiers Or Keys.Alt
            keyCode = Keys.Menu
        ElseIf keyCode = Keys.LControlKey Or keyCode = Keys.RControlKey Then
            Me._modifiers = Me._modifiers Or Keys.Control
            keyCode = Keys.ControlKey
        ElseIf keyCode = Keys.LShiftKey Or keyCode = Keys.RShiftKey Then
            Me._modifiers = Me._modifiers Or Keys.Shift
            keyCode = Keys.ShiftKey
        End If
        Me._keyData = keyCode Or Me._modifiers
        Dim e As New LLKeyEventArgs(keyCode, Me._keyData, Me._modifiers, False)
        RaiseEvent KeyDown(Me, e)
        Return e

    End Function

#End Region

#Region " PUBLIC METHODES "

    'This sub processes all the keyboard messages and passes to the other windows
    Private Function LowLevelKeyboardProc(ByVal nCode As Integer, ByVal wParam As Integer, ByRef lParam As User32.KBDLLHOOKSTRUCT) As Integer
        If (nCode = User32.HC_ACTION) Then
            Select Case True
                Case wParam = User32.WM_KEYDOWN Or wParam = User32.WM_SYSKEYDOWN
                    Dim e As LLKeyEventArgs = Me.OnKeyDown(lParam)
                    If e.Handled Then
                        If Not Me._handledKeys.Contains(e.KeyValue) Then
                            Me._handledKeys.Add(e.KeyValue)
                        End If
                        Return 1
                    End If
                Case wParam = User32.WM_KEYUP Or wParam = User32.WM_SYSKEYUP
                    Dim e As LLKeyEventArgs = Me.OnKeyUp(lParam)
                    Me._handledKeys.Remove(e.KeyValue)
                    If e.Handled Then
                        Return 1
                    End If
            End Select
        End If
        Return User32.CallNextHookEx(LLKeyboardHook._hKeyboardHook, nCode, wParam, lParam)
    End Function

    ''' <summary>
    ''' Installs the keyboard hook for this application. 
    ''' Only one low-level keyboard hook can be installed at a time.
    ''' </summary>
    ''' <param name="throwEx">A Boolean value indicating if the
    ''' method should throw an exception on unsuccessful install. 
    ''' It is set to True by default.</param>
    ''' <returns>Returns a Boolean value that determines 
    ''' if the hook installation was successful.</returns>
    Public Function InstallHook(Optional ByVal throwEx As Boolean = True) As Boolean
        Dim success As Boolean = True
        If LLKeyboardHook._hKeyboardHook = IntPtr.Zero Then
            Me._keyboardProc = New User32.WKCallBack(AddressOf LowLevelKeyboardProc)
            Dim hinstDLL As IntPtr = Marshal.GetHINSTANCE( _
            Reflection.Assembly.GetExecutingAssembly().GetModules()(0))
            LLKeyboardHook._hKeyboardHook = User32.SetWindowsHookEx( _
            User32.WH_KEYBOARD_LL, Me._keyboardProc, hinstDLL, 0)
            If LLKeyboardHook._hKeyboardHook = IntPtr.Zero Then
                success = False
                If throwEx Then
                    Dim message As String = "Failed to install the keyboard hook!" _
                    & Environment.NewLine & Me.GetLastApiError
                    Dim ex As New Exception(message)
                    ex.Source = "LLKeyboardHook"
                    Throw ex
                End If
            Else
                Me.OnStateChanged()
            End If
        End If
        Return success
    End Function

    ''' <summary>
    ''' Removes the keyboard hook for this application.
    ''' </summary>
    ''' <param name="throwEx">A Boolean value indicating if the
    ''' method should throw an exception on unsuccessful remove.
    ''' It is set to True by default.</param>
    ''' <returns>Returns a Boolean value that determines 
    ''' if the hook uninstalled successfully.</returns>
    ''' <remarks></remarks>
    Public Function RemoveHook(Optional ByVal throwEx As Boolean = True) As Boolean
        Dim success As Boolean = True
        If Not LLKeyboardHook._hKeyboardHook = IntPtr.Zero Then
            If Not User32.UnhookWindowsHookEx(LLKeyboardHook._hKeyboardHook) Then
                success = False
                If throwEx Then
                    Dim message As String = "Failed to remove the keyboard hook!" _
                    & Environment.NewLine & Me.GetLastApiError
                    Dim ex As New Exception(message)
                    ex.Source = "LLKeyboardHook"
                    Throw ex
                End If
            Else
                LLKeyboardHook._hKeyboardHook = IntPtr.Zero
                Me.OnStateChanged()
            End If
        End If
        Return success
    End Function

    ''' <summary>
    ''' Gets the state of the hook.
    ''' </summary>
    Public Function GetState() As WindowsHookLib.HookState
        If LLKeyboardHook._hKeyboardHook = IntPtr.Zero Then
            Return HookState.Uninstalled
        Else
            Return HookState.Installed
        End If
    End Function

    ''' <summary>
    ''' Gets the last Api error message.
    ''' </summary>
    Public Function GetLastApiError() As String
        Dim message As String = Nothing
        Dim eCode As Integer = Kernel32.GetLastError()
        If eCode > 0 Then
            Kernel32.FormatMessage( _
            Kernel32.FORMAT_MESSAGE_ALLOCATE_BUFFER _
            Or Kernel32.FORMAT_MESSAGE_FROM_SYSTEM _
            Or Kernel32.FORMAT_MESSAGE_IGNORE_INSERTS, _
            0, eCode, Kernel32.LANG_NEUTRAL, message, 0, 0)
        End If
        Return message
    End Function

#End Region

#Region " DISPOSE "

    ''' <summary>
    ''' Unhooks the keyboard and sets all the class 
    ''' private variables to point to nothing. If the keyboard 
    ''' is not hooked it doesn’t throw an exception.
    ''' </summary>
    Public Sub Dispose() Implements System.IDisposable.Dispose
        Dispose(True)
    End Sub

    Protected Overrides Sub finalize()
        If Me.GetState = HookState.Installed AndAlso Not disposed Then
            Dim message As String = _
            "Dispose or remove the hook before exiting the application!"
            Dim ex As New Exception(message)
            ex.Source = "LLMouseHook"
            Throw ex
        End If
        Dispose(False)
    End Sub

    Protected disposed As Boolean

    Protected Overridable Sub dispose(ByVal disposing As Boolean)
        If disposed Then Exit Sub
        If disposing Then
            disposed = True
        End If
        Me.CleanUp()
    End Sub

    Private Sub CleanUp()
        Me.RemoveHook()
        LLKeyboardHook._hKeyboardHook = Nothing
        Me._keyboardProc = Nothing
    End Sub

#End Region

End Class
