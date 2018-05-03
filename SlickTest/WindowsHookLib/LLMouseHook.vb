Imports System.Drawing
Imports System.Reflection
Imports System.Windows.Forms
Imports System.runtime.InteropServices

Public Class LLMouseHook
    Implements IDisposable

#Region " EVENT HANDLERS "
    Public Event StateChanged(ByVal sender As Object, ByVal e As StateChangedEventArgs)
    Public Event MouseDown(ByVal sender As IntPtr, ByVal e As LLMouseEventArgs)
    Public Event MouseMove(ByVal sender As IntPtr, ByVal e As LLMouseEventArgs)
    Public Event MouseUp(ByVal sender As IntPtr, ByVal e As LLMouseEventArgs)
    Public Event MouseWheel(ByVal sender As IntPtr, ByVal e As LLMouseEventArgs)
    Public Event MouseClick(ByVal sender As IntPtr, ByVal e As LLMouseEventArgs)
    Public Event MouseDoubleClick(ByVal sender As IntPtr, ByVal e As LLMouseEventArgs)
#End Region

#Region " CLASS VARIABLES "

    <MarshalAs(UnmanagedType.FunctionPtr)> _
    Private _mouseProc As User32.WMCallBack
    Private Shared _hMouseHook As IntPtr
    Private _e As LLMouseEventArgs
    Private _rectangle As New Rectangle
    Private _hwnd As IntPtr
    Private _bottons As MouseButtons

#End Region

#Region " ON EVENT "

    Private Sub OnStateChanged()
        Dim e As New StateChangedEventArgs(Me)
        RaiseEvent StateChanged(Me, e)
    End Sub

    Private Function OnMouseMove(ByVal wParam As Integer, ByVal lParam As User32.MSLLHOOKSTRUCT, ByVal hwnd As IntPtr) As LLMouseEventArgs
        Dim e As New LLMouseEventArgs(Me._bottons, 0, lParam.pt.X, lParam.pt.Y, 0, False)
        RaiseEvent MouseMove(hwnd, e)
        Return e
    End Function

    Private Sub OnMouseDown(ByVal wParam As Integer, ByVal lParam As User32.MSLLHOOKSTRUCT, ByVal hwnd As IntPtr)
        Dim btn As MouseButtons
        Static time As Integer = 0
        Select Case True
            Case wParam = User32.WM_LBUTTONDOWN
                btn = MouseButtons.Left
            Case wParam = User32.WM_RBUTTONDOWN
                btn = MouseButtons.Right
            Case wParam = User32.WM_MBUTTONDOWN
                btn = MouseButtons.Middle
            Case wParam = User32.WM_XBUTTONDOWN Or wParam = User32.WM_NCXBUTTONDOWN
                Dim hiWord As Integer = BitConverter.ToInt16 _
                (BitConverter.GetBytes(lParam.mouseData), 2)
                If hiWord = 1 Then
                    btn = MouseButtons.XButton1
                ElseIf hiWord = 2 Then
                    btn = MouseButtons.XButton2
                End If
        End Select
        If (lParam.time - time <= SystemInformation.DoubleClickTime) _
        AndAlso (Me._e.Button = btn) AndAlso (lParam.pt.X >= Me._rectangle.X _
        AndAlso lParam.pt.X <= Me._rectangle.Right) AndAlso (lParam.pt.Y >= Me._rectangle.Y _
        AndAlso lParam.pt.Y <= Me._rectangle.Bottom) AndAlso (Me._hwnd = hwnd) _
        AndAlso Me._e.Clicks <> 2 Then
            Me._e = New LLMouseEventArgs(btn, 2, lParam.pt.X, lParam.pt.Y, 0, False)
            RaiseEvent MouseDown(hwnd, Me._e)
        Else
            Me._e = New LLMouseEventArgs(btn, 1, lParam.pt.X, lParam.pt.Y, 0, False)
            RaiseEvent MouseDown(hwnd, Me._e)
        End If
        Me._bottons = Me._bottons Xor btn
        time = lParam.time
        Me._hwnd = hwnd
        Me._rectangle = New Rectangle(CInt(lParam.pt.X - (Me._rectangle.Width / 2)), _
        CInt(lParam.pt.Y - (Me._rectangle.Height / 2)), Me._rectangle.Width, _
        Me._rectangle.Height)
    End Sub

    Private Sub OnMouseUp(ByVal wParam As Integer, ByVal lParam As User32.MSLLHOOKSTRUCT, ByVal hwnd As IntPtr)
        Dim btn As MouseButtons
        Select Case True
            Case wParam = User32.WM_LBUTTONUP
                btn = MouseButtons.Left
            Case wParam = User32.WM_RBUTTONUP
                btn = MouseButtons.Right
            Case wParam = User32.WM_MBUTTONUP
                btn = MouseButtons.Middle
            Case wParam = User32.WM_XBUTTONUP Or wParam = User32.WM_NCXBUTTONUP
                Dim hiWord As Integer = BitConverter.ToInt16 _
                (BitConverter.GetBytes(lParam.mouseData), 2)
                If hiWord = 1 Then
                    btn = MouseButtons.XButton1
                ElseIf hiWord = 2 Then
                    btn = MouseButtons.XButton2
                End If
        End Select
        If Me._e.Button = btn AndAlso Me._hwnd = hwnd AndAlso Me._e.Clicks <> 2 Then
            Me._e = New LLMouseEventArgs(btn, 1, lParam.pt.X, lParam.pt.Y, 0, Me._e.Handled)
            Me.OnMouseClick(hwnd)
        ElseIf Me._e.Button = btn AndAlso Me._hwnd = hwnd AndAlso Me._e.Clicks = 2 Then
            Me.OnMouseDoubleClick(hwnd)
        End If
        Dim e As LLMouseEventArgs = New LLMouseEventArgs(btn, 1, lParam.pt.X, lParam.pt.Y, 0, Me._e.Handled)
        RaiseEvent MouseUp(hwnd, e)
        Me._e = New LLMouseEventArgs(btn, Me._e.Clicks, lParam.pt.X, lParam.pt.Y, 0, e.Handled)
        Me._bottons = Me._bottons Xor btn
    End Sub

    Private Sub OnMouseClick(ByVal hwnd As IntPtr)
        RaiseEvent MouseClick(hwnd, Me._e)
    End Sub

    Private Sub OnMouseDoubleClick(ByVal hwnd As IntPtr)
        RaiseEvent MouseDoubleClick(hwnd, Me._e)
    End Sub

    Private Function OnMouseWheel(ByVal wParam As Integer, ByVal lParam As User32.MSLLHOOKSTRUCT, ByVal hwnd As IntPtr) As LLMouseEventArgs
        Dim hiWord As Integer
        hiWord = BitConverter.ToInt16(BitConverter.GetBytes(lParam.mouseData), 2)
        Dim e As New LLMouseEventArgs(MouseButtons.None, 0, lParam.pt.X, lParam.pt.Y, hiWord, False)
        RaiseEvent MouseWheel(hwnd, e)
        Return e
    End Function

#End Region

#Region " CLASS METHODS "

    'Default class constructor.
    Public Sub New()
        Me._e = New LLMouseEventArgs(MouseButtons.None, 0, 0, 0, 0, False)
        Me._rectangle = New Rectangle(0, 0, SystemInformation _
        .DoubleClickSize.Width, SystemInformation.DoubleClickSize.Height)
    End Sub

    'This sub processes all the mouse messages and passes to the other windows
    Private Function LowLevelMouseProc(ByVal nCode As Integer, ByVal wParam As Integer, ByRef lParam As User32.MSLLHOOKSTRUCT) As Integer
        If (nCode >= User32.HC_ACTION) Then
            Select Case True
                Case wParam = User32.WM_MOUSEMOVE
                    If Me.OnMouseMove( _
                    wParam, lParam, User32.WindowFromPoint( _
                    lParam.pt.X, lParam.pt.Y)).Handled Then
                        Return 1
                    End If
                Case wParam = User32.WM_LBUTTONDOWN Or wParam = User32.WM_RBUTTONDOWN _
                Or wParam = User32.WM_MBUTTONDOWN Or wParam = User32.WM_XBUTTONDOWN Or _
                wParam = User32.WM_NCXBUTTONDOWN
                    Me.OnMouseDown(wParam, lParam, User32.WindowFromPoint(lParam.pt.X, lParam.pt.Y))
                    If Me._e.Handled Then
                        Return 1
                    End If
                Case wParam = User32.WM_LBUTTONUP Or wParam = User32.WM_RBUTTONUP _
                Or wParam = User32.WM_MBUTTONUP Or wParam = User32.WM_XBUTTONUP Or _
                wParam = User32.WM_NCXBUTTONUP
                    Me.OnMouseUp(wParam, lParam, User32.WindowFromPoint(lParam.pt.X, lParam.pt.Y))
                    If Me._e.Handled Then
                        Me._e.Handled = False
                        Return 1
                    End If
                Case wParam = User32.WM_MOUSEWHEEL
                    If Me.OnMouseWheel( _
                    wParam, lParam, User32.WindowFromPoint( _
                    lParam.pt.X, lParam.pt.Y)).Handled Then
                        Return 1
                    End If
            End Select
        End If
        Return User32.CallNextHookEx(LLMouseHook._hMouseHook, nCode, wParam, lParam)
    End Function

    ''' <summary>
    ''' Installs the mouse hook for this application. 
    ''' Only one low-level mouse hook can be installed at a time.
    ''' </summary>
    ''' <param name="throwEx">A Boolean value indicating if the
    ''' method should throw an exception on unsuccessful install.
    ''' It is set to True by default.</param>
    ''' <returns>Returns a Boolean value that determines 
    ''' if the hook installation was successful.</returns>
    Public Function InstallHook(Optional ByVal throwEx As Boolean = True) As Boolean
        Dim success As Boolean = True
        If LLMouseHook._hMouseHook = IntPtr.Zero Then
            Me._mouseProc = New User32.WMCallBack(AddressOf LowLevelMouseProc)
            Dim hinstDLL As IntPtr = Marshal.GetHINSTANCE( _
            Reflection.Assembly.GetExecutingAssembly().GetModules()(0))
            LLMouseHook._hMouseHook = User32.SetWindowsHookEx( _
            User32.WH_MOUSE_LL, Me._mouseProc, hinstDLL, 0)
            If LLMouseHook._hMouseHook = IntPtr.Zero Then
                success = False
                If throwEx Then
                    Dim message As String = "Failed to install the mouse hook!" _
                    & Environment.NewLine & Me.GetLastApiError
                    Dim ex As New Exception(message)
                    ex.Source = "LLMouseHook"
                    Throw ex
                End If
            Else
                Me.OnStateChanged()
            End If
        End If
        Return success
    End Function

    ''' <summary>
    ''' Removes the mouse hook for this application.
    ''' </summary>
    ''' <param name="throwEx">A Boolean value indicating if the
    ''' method should throw an exception on unsuccessful remove. 
    ''' It is set to True by default.</param>
    ''' <returns>Returns a Boolean value that determines 
    ''' if the hook uninstalled successfully.</returns>
    ''' <remarks></remarks>
    Public Function RemoveHook(Optional ByVal throwEx As Boolean = True) As Boolean
        Dim success As Boolean = True
        If Not LLMouseHook._hMouseHook = IntPtr.Zero Then
            If Not User32.UnhookWindowsHookEx(LLMouseHook._hMouseHook) Then
                success = False
                If throwEx Then
                    Dim message As String = "Failed to remove the mouse hook!" _
                    & Environment.NewLine & Me.GetLastApiError
                    Dim ex As New Exception(message)
                    ex.Source = "LLMouseHook"
                    Throw ex
                End If
            Else
                LLMouseHook._hMouseHook = IntPtr.Zero
                Me.OnStateChanged()
            End If
        End If
        Return success
    End Function

    ''' <summary>
    ''' Gets the state of the object.
    ''' </summary>
    Public Function GetState() As WindowsHookLib.HookState
        If LLMouseHook._hMouseHook = IntPtr.Zero Then
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
    ''' Removes the mouse hook and sets all the class 
    ''' private variables to point to nothing. If the mouse 
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
        Me._e = Nothing
        LLMouseHook._hMouseHook = Nothing
        Me._mouseProc = Nothing
        Me._rectangle = Nothing
        Me._hwnd = Nothing
        Me._bottons = Nothing
    End Sub

#End Region

End Class

