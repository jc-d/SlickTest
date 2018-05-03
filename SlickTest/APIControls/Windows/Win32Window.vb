Imports WinAPI.API

Friend Class Win32Window
    Public Declare Function IsWindowVisible& Lib "user32" (ByVal hwnd As IntPtr)
    Public Declare Function GetParent Lib "user32" (ByVal hwnd As IntPtr) As Integer

    Public Shared Sub Show(ByVal handle As IntPtr)
        WinAPI.NativeFunctions.ShowWindow(handle, SW.RESTORE)
    End Sub

    Public Shared Sub Hide(ByVal handle As IntPtr)
        WinAPI.NativeFunctions.ShowWindow(handle, SW.HIDE)
    End Sub
End Class