Imports System.runtime.InteropServices

Public Class User32

    Public Delegate Function WMCallBack(ByVal nCode As Integer, _
    ByVal wParam As Integer, ByRef lParam As MSLLHOOKSTRUCT) As Integer

    Public Delegate Function WKCallBack(ByVal nCode As Integer, _
    ByVal wParam As Integer, ByRef lParam As KBDLLHOOKSTRUCT) As Integer

#Region " USER32 API SHARED FUNCTIONS "

    <DllImport("user32", EntryPoint:="SetWindowsHookEx")> _
    Public Shared Function SetWindowsHookEx( _
    ByVal idHook As Integer, ByVal HookProc As WMCallBack, _
    ByVal hInstance As IntPtr, ByVal wParam As Integer) As IntPtr
    End Function

    <DllImport("user32", EntryPoint:="SetWindowsHookEx")> _
    Public Shared Function SetWindowsHookEx( _
    ByVal idHook As Integer, ByVal HookProc As WKCallBack, _
    ByVal hInstance As IntPtr, ByVal wParam As Integer) As IntPtr
    End Function

    <DllImport("user32", EntryPoint:="UnhookWindowsHookEx")> _
    Public Shared Function UnhookWindowsHookEx(ByVal hook As IntPtr) As Boolean
    End Function

    <DllImport("user32", EntryPoint:="CallNextHookEx")> _
    Public Shared Function CallNextHookEx( _
    ByVal idHook As IntPtr, ByVal nCode As Integer, _
    ByVal wParam As Integer, ByRef lParam As MSLLHOOKSTRUCT) As Integer
    End Function

    <DllImport("user32", EntryPoint:="CallNextHookEx")> _
    Public Shared Function CallNextHookEx( _
    ByVal idHook As IntPtr, ByVal nCode As Integer, _
    ByVal wParam As Integer, ByRef lParam As KBDLLHOOKSTRUCT) As Integer
    End Function

    'Modified for Slick Test... WindowFromPoint(x,y) is unsupported by Windows 7
    '<DllImport("user32", EntryPoint:="WindowFromPoint")> _
    'Public Shared Function WindowFromPoint( _
    'ByVal xPoint As Integer, ByVal yPoint As Integer) As IntPtr
    'End Function

    Public Shared Function WindowFromPoint(ByVal xPoint As Integer, ByVal yPoint As Integer) As IntPtr
        Return WindowFromPoint(New POINT(xPoint, yPoint))
    End Function

    <DllImport("user32", EntryPoint:="WindowFromPoint")> _
    Protected Friend Shared Function WindowFromPoint(ByVal p As POINT) As IntPtr
    End Function

#End Region

#Region " CONSTANTS "

    '===================== Mouse ====================

    Public Const WH_MOUSE As Integer = 7
    Public Const WH_MOUSE_LL As Integer = 14
    Public Const HC_ACTION As Integer = 0
    Public Const WM_MOUSEMOVE As Integer = &H200
    Public Const WM_LBUTTONDOWN As Integer = &H201
    Public Const WM_LBUTTONUP As Integer = &H202
    Public Const WM_LBUTTONDBLCLK As Integer = &H203
    Public Const WM_RBUTTONDOWN As Integer = &H204
    Public Const WM_RBUTTONUP As Integer = &H205
    Public Const WM_RBUTTONDBLCLK As Integer = &H206
    Public Const WM_MBUTTONDOWN As Integer = &H207
    Public Const WM_MBUTTONUP As Integer = &H208
    Public Const WM_MBUTTONDBLCLK As Integer = &H209
    Public Const WM_MOUSEWHEEL As Integer = &H20A
    Public Const WM_XBUTTONDOWN As Integer = &H20B
    Public Const WM_XBUTTONUP As Integer = &H20C
    Public Const WM_XBUTTONDBLCLK As Integer = &H20D
    Public Const WM_NCXBUTTONDOWN As Integer = &HAB
    Public Const WM_NCXBUTTONUP As Integer = &HAC
    Public Const WM_NCXBUTTONDBLCLK As Integer = &HAD
    '=================================================

    '==================== Keyboard ===================

    Public Const WH_KEYBOARD As Integer = 2
    Public Const WH_KEYBOARD_LL As Integer = 13
    Public Const WM_KEYDOWN As Integer = &H100
    Public Const WM_KEYUP As Integer = &H101
    Public Const WM_SYSKEYDOWN As Integer = &H104
    Public Const WM_SYSKEYUP As Integer = &H105
    Public Const LLKHF_EXTENDED As Integer = &H1&
    Public Const LLKHF_INJECTED As Integer = &H10&
    Public Const LLKHF_ALTDOWN As Integer = &H20&
    Public Const LLKHF_UP As Integer = &H80&
    Public Const VK_TAB As Integer = &H9
    Public Const VK_CONTROL As Integer = &H11
    Public Const VK_ESCAPE As Integer = &H1B

    '=================================================
#End Region

#Region " STRUCTURES "
    Public Structure MSLLHOOKSTRUCT
        Public pt As POINT
        Public mouseData As Integer
        Public flags As Integer
        Public time As Integer
        Public extra As Integer
    End Structure

    Public Structure KBDLLHOOKSTRUCT
        Public vkCode As Integer
        Public scanCode As Integer
        Public flags As Integer
        Public time As Integer
        Public dwExtraInfo As Integer
    End Structure

#End Region

End Class
