Imports winAPI.API
Imports winAPI.NativeFunctions
Imports System.Runtime.InteropServices
Imports System.Text
Imports System.ComponentModel
Imports System
Imports System.Collections

Friend Class WindowWindowsAPI
    Private Shared WindowsFunctions As APIControls.IndependentWindowsFunctionsv1
    Public Sub New(ByVal wf As APIControls.IndependentWindowsFunctionsv1)
        WindowsFunctions = wf
    End Sub

    Public Function IsWindow(ByVal hwnd As IntPtr) As Boolean
        If (GenericMethodsUIAutomation.IsWPFOrCustom(hwnd) = True) Then
            Return WindowsFunctions.WpfWindow.IsWindow(hwnd)
        End If

        If (WindowsFunctions.GetParent(hwnd) = IntPtr.Zero) Then
            Return True
        Else
            Return False
        End If
    End Function

    ''' <summary>
    ''' This sets the application to the front.  This will both ensure the application 
    ''' isn't minimized and that the application is on top.
    ''' </summary>
    ''' <param name="hwnd"></param>
    ''' <remarks></remarks>
    Public Sub SetToForeGround(ByVal hwnd As IntPtr)
        'Console.WriteLine("SetToForeGround: " & hwnd.ToString())
        If IntPtr.Zero.Equals(hwnd) Then
            Exit Sub
        End If

        If (TopWindow.IsIconic(hwnd) = True) Then
            'Console.WriteLine("It's minimized.")
            Dim tw As TopWindow = New TopWindow()
            tw.ShowWindow(hwnd)
        End If
        Try
            'Console.WriteLine("Bring to top...")
            If (BringWindowToTop(hwnd) = True) Then Return
        Catch ex As Exception
        End Try
        'Console.WriteLine("Brint to top failed.  AppActivating")
        Try
            AppActivate(WindowsFunctions.GetAllText(hwnd))
        Catch ex As Exception
        End Try
    End Sub

    Public Function GetActiveWindow() As IntPtr
        Return TopWindow.GetCurrentlyActiveWindowHandle()
    End Function

    <DllImport("user32", EntryPoint:="BringWindowToTop")> _
    Private Shared Function BringWindowToTop(ByVal hwnd As IntPtr) As Boolean
    End Function

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="hwnd"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function HasMax(ByVal hwnd As IntPtr) As Boolean
        'Public Function HasMax(ByVal hwnd As Long) As Boolean
        Dim Style As Int64
        Style = WinAPI.API.GetWindowLongAsInt64(hwnd, GWL.STYLE)
        If ((Style And WinAPI.API.WS.WS_MAXIMIZEBOX) = WinAPI.API.WS.WS_MAXIMIZEBOX) = True Then
            HasMax = True
        Else
            HasMax = False
        End If
    End Function

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="hwnd"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function HasMin(ByVal hwnd As IntPtr) As Boolean
        Dim Style As Int64
        Style = WinAPI.API.GetWindowLongAsInt64(hwnd, GWL.STYLE)

        If ((Style And WinAPI.API.WS.WS_MINIMIZEBOX) = WinAPI.API.WS.WS_MINIMIZEBOX) = True Then
            Return True
        Else
            Return False
        End If
    End Function

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="hwnd"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function HasTitleBar(ByVal hwnd As IntPtr) As Boolean
        Dim Style As Int64
        Style = WinAPI.API.GetWindowLongAsInt64(hwnd, GWL.STYLE)
        If ((Style And WinAPI.API.WS.WS_DLGFRAME) = WinAPI.API.WS.WS_DLGFRAME = True) Then
            HasTitleBar = True
        Else
            HasTitleBar = False
        End If
    End Function
    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="hwnd"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function HasBorder(ByVal hwnd As IntPtr) As Boolean
        Dim Style As Int64

        Style = WinAPI.API.GetWindowLongAsInt64(hwnd, GWL.STYLE)
        If ((Style And WinAPI.API.WS.WS_BORDER) = WinAPI.API.WS.WS_BORDER = True) Then
            HasBorder = True
        Else
            HasBorder = False
        End If
    End Function
End Class
