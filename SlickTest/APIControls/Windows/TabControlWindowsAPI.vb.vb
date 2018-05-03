Imports winAPI.API
Imports winAPI.NativeFunctions
Imports System.Runtime.InteropServices
Imports System.Text
Imports System.ComponentModel
Imports System
Imports System.Collections

Friend Class TabControlWindowsAPI
    Private Shared WindowsFunctions As APIControls.IndependentWindowsFunctionsv1
    Public Sub New(ByVal wf As APIControls.IndependentWindowsFunctionsv1)
        WindowsFunctions = wf
    End Sub

    Function IsTabControl(ByVal hwnd As IntPtr) As Boolean
        If (GenericMethodsUIAutomation.IsWPFOrCustom(hwnd) = True) Then
            Return WindowsFunctions.WpfTabControl.IsTabControl(hwnd)
        End If
        Dim cn As String = WindowsFunctions.GetClassNameNoDotNet(hwnd)
        If (cn.ToLowerInvariant.Contains("systabcontrol") = True) Then
            Return True
        End If
        Return False
    End Function

    Sub SelectTab(ByVal hwnd As IntPtr, ByVal index As Integer)
        WinAPI.API.SendMessage(hwnd, TabControlMessages.TCM_SETCURFOCUS, index, IntPtr.Zero)
        System.Threading.Thread.Sleep(1) 'give time for refresh of screen.
        WinAPI.API.SendMessage(hwnd, TabControlMessages.TCM_SETCURSEL, index, IntPtr.Zero)
    End Sub

    Function GetTabCount(ByVal hwnd As IntPtr) As Integer
        Return WinAPI.API.SendMessage(hwnd, TabControlMessages.TCM_GETITEMCOUNT, IntPtr.Zero, IntPtr.Zero).ToInt32()
    End Function

    Function GetSelectedTab(ByVal hwnd As IntPtr) As Integer
        Return WinAPI.API.SendMessage(hwnd, TabControlMessages.TCM_GETCURSEL, IntPtr.Zero, IntPtr.Zero).ToInt32()
    End Function

    <Runtime.InteropServices.DllImport("user32.dll", EntryPoint:="SendMessage")> _
        Private Shared Function SendMessageRECT(ByVal hWnd As IntPtr, ByVal msg As Int32, ByVal wParam As Int32, ByRef lParam As WinAPI.API.RECT) As Boolean
    End Function

    Function GetRECT(ByVal hwnd As IntPtr, ByVal index As Integer) As System.Drawing.Rectangle
        Dim APIRect As RECT
        If (SendMessageRECT(hwnd, TabControlMessages.TCM_GETITEMRECT, index, APIRect) = False) Then
            Throw New SlickTestAPIException("Unable to get the rectangle of tab with the index of '" & index & "'")
        End If
        Dim Rect As New System.Drawing.Rectangle(Convert.ToInt32(APIRect.left), _
        Convert.ToInt32(APIRect.top), Convert.ToInt32(APIRect.right - APIRect.left), _
        Convert.ToInt32(APIRect.bottom - APIRect.top))
        Return Rect
    End Function

End Class
