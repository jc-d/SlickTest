'Author: Arman Ghazanchyan
'Created date: 11/02/2006
'Last updated: 04/27/2007
'This license can be found via the following link: http://www.codeproject.com/info/cpol10.aspx
Imports System.runtime.InteropServices
Imports System.Drawing
'Class methods adjusted to work better with api.  Removed lots of general API connections.
Friend Class Capture

#Region " API DECLARATIONS "

#Region " ERROR PROVIDER APIS "
    <DllImport("kernel32", EntryPoint:="GetLastError")> _
    Private Shared Function GetLastError() As Integer
    End Function
    <DllImport("kernel32", EntryPoint:="FormatMessageA")> _
    Private Shared Function FormatMessage(ByVal dwFlags As Integer, _
    ByVal lpSource As Integer, ByVal dwMessageId As Integer, _
    ByVal dwLanguageId As Integer, ByRef lpBuffer As String, _
    ByVal nSize As Integer, ByVal Arguments As Integer) As Integer
    End Function

#Region "CONSTANTS"
    Const FORMAT_MESSAGE_ALLOCATE_BUFFER As Integer = &H100
    Const FORMAT_MESSAGE_IGNORE_INSERTS As Integer = &H200
    Const FORMAT_MESSAGE_FROM_SYSTEM As Integer = &H1000
    Const LANG_NEUTRAL As Integer = &H0
#End Region

#End Region

    <DllImport("user32", EntryPoint:="GetWindowRect")> _
    Private Shared Function GetWindowRect(ByVal hwnd As IntPtr, _
    ByVal lpRect As RECT) As Integer
    End Function
    <DllImport("user32", EntryPoint:="ReleaseDC")> _
    Private Shared Function ReleaseDC(ByVal hwnd As IntPtr, _
    ByVal hdc As IntPtr) As Integer
    End Function
    '<DllImport("user32", EntryPoint:="WindowFromPoint")> _
    'Private Shared Function WindowFromPoint(ByVal xPoint As Integer, _
    'ByVal yPoint As Integer) As IntPtr
    'End Function
    <DllImport("user32", EntryPoint:="GetForegroundWindow")> _
    Private Shared Function GetForegroundWindow() As IntPtr
    End Function
    <DllImport("user32", EntryPoint:="GetWindowDC")> _
    Private Shared Function GetWindowDC(ByVal hwnd As IntPtr) As IntPtr
    End Function
    <DllImport("user32", EntryPoint:="GetDesktopWindow")> _
    Private Shared Function GetDesktopWindow() As IntPtr
    End Function
    <DllImport("gdi32", EntryPoint:="BitBlt")> _
    Private Shared Function BitBlt(ByVal hDestDC As IntPtr, ByVal x As Integer, _
    ByVal y As Integer, ByVal nWidth As Integer, ByVal nHeight As Integer, _
    ByVal hSrcDC As IntPtr, ByVal xSrc As Integer, ByVal ySrc As Integer, _
    ByVal dwRop As Integer) As Integer
    End Function

#Region "CONSTANTS"

    Private Const CAPTUREBLT As Integer = &H40000000
    Private Const BLACKNESS As Integer = &H42
    Private Const DSTINVERT As Integer = &H550009
    Private Const MERGECOPY As Integer = &HC000CA
    Private Const MERGEPAINT As Integer = &HBB0226
    Private Const NOTSRCCOPY As Integer = &H330008
    Private Const NOTSRCERASE As Integer = &H1100A6
    Private Const PATCOPY As Integer = &HF00021
    Private Const PATINVERT As Integer = &H5A0049
    Private Const PATPAINT As Integer = &HFB0A09
    Private Const SRCAND As Integer = &H8800C6
    Private Const SRCCOPY As Integer = &HCC0020
    Private Const SRCERASE As Integer = &H440328
    Private Const SRCINVERT As Integer = &H660046
    Private Const SRCPAINT As Integer = &HEE0086
    Private Const WHITENESS As Integer = &HFF0062

#End Region

#Region " STRUCTURE "
    <StructLayout(LayoutKind.Sequential)> _
        Private Class RECT
        Public Left As Integer
        Public Top As Integer
        Public Right As Integer
        Public Bottom As Integer
    End Class

    Private Structure POINTAPI
        Dim X As Integer
        Dim Y As Integer
    End Structure

#End Region

#End Region

#Region " CLASS METHODS "

    ''' <summary>
    ''' Captures the image of the Desktop Window. 
    ''' Returns a bitmap if the function succeeded, 
    ''' otherwise returns nothing.
    ''' </summary>
    Public Shared Function FullScreen() As Bitmap
        Dim hwnd As IntPtr
        Dim wRect As New RECT
        'Get the handle of the Desktop Window.
        hwnd = GetDesktopWindow()
        'Get the window rectangle.
        If hwnd <> IntPtr.Zero Then
            If GetWindowRect(hwnd, wRect) <> 0 Then
                Dim rect As New Rectangle(wRect.Left, wRect.Top, _
                wRect.Right - wRect.Left, wRect.Bottom - wRect.Top)
                Return Capture.ScreenRectangle(rect)
            Else
                Throw New SlickTestUIException(Capture.ApiError)
            End If
        End If
        Return Nothing
    End Function

    ''' <summary>
    ''' Captures the image of an active window from the screen. Returns 
    ''' a bitmap if the function succeeded, otherwise returns nothing.
    ''' </summary>
    Public Shared Function ActiveWindow() As Bitmap
        Dim hwnd As IntPtr
        Dim wRect As New RECT
        'Get the handle of the selected (active) window.
        hwnd = GetForegroundWindow()
        'Get the window rectangle.
        If hwnd <> IntPtr.Zero Then
            If GetWindowRect(hwnd, wRect) <> 0 Then
                Dim rect As New Rectangle(wRect.Left, wRect.Top, _
                wRect.Right - wRect.Left, wRect.Bottom - wRect.Top)
                Return Capture.ScreenRectangle(rect)
            Else
                Throw New SlickTestUIException(Capture.ApiError)
            End If
        End If
        Return Nothing
    End Function

    ''' <summary>
    ''' Captures the image of a window from the screen specified by a handle. 
    ''' Returns a bitmap if the function succeeded, otherwise returns nothing.
    ''' </summary>
    ''' <param name="hwnd">The handle to the window whose 
    ''' image should be captured.</param>
    Public Shared Function Window(ByVal hwnd As IntPtr) As Bitmap
        Dim wRect As New RECT
        'Get the window rectangle.
        If hwnd <> IntPtr.Zero Then
            If GetWindowRect(hwnd, wRect) <> 0 Then
                Dim rect As New Rectangle(wRect.Left, wRect.Top, _
                wRect.Right - wRect.Left, wRect.Bottom - wRect.Top)
                Return Capture.ScreenRectangle(rect)
            Else
                Throw New SlickTestUIException(Capture.ApiError)
            End If
        End If
        Return Nothing
    End Function

    ''' <summary>
    ''' Captures an image of a control/container within a 
    ''' window. Returns a bitmap if the function 
    ''' succeeded, otherwise returns nothing.
    ''' </summary>
    ''' <param name="p">A point within the control in the screen coordinates.</param>
    Public Overloads Shared Function Control(ByVal p As Point) As Bitmap
        Dim hwnd As IntPtr
        Dim wRect As New RECT
        'Get the handle of a window from a point.
        hwnd = WinAPI.API.WindowFromPoint(p.X, p.Y)
        'Get the window area.
        If hwnd <> IntPtr.Zero Then
            If GetWindowRect(hwnd, wRect) <> 0 Then
                Dim rect As New Rectangle(wRect.Left, wRect.Top, _
                wRect.Right - wRect.Left, wRect.Bottom - wRect.Top)
                Return Capture.ScreenRectangle(rect)
            Else
                Throw New SlickTestUIException(Capture.ApiError)
            End If
        End If
        Return Nothing
    End Function

    ''' <summary>
    ''' Captures an image of a control/container within a 
    ''' window. Returns a bitmap if the function 
    ''' succeeded, otherwise returns nothing.
    ''' </summary>
    ''' <param name="hwnd">The handle of the control.</param>
    Public Overloads Shared Function Control(ByVal hwnd As IntPtr) As Bitmap
        Dim wRect As New RECT
        'Get the window area.
        If hwnd <> IntPtr.Zero Then
            If GetWindowRect(hwnd, wRect) <> 0 Then
                Dim rect As New Rectangle(wRect.Left, wRect.Top, _
                wRect.Right - wRect.Left, wRect.Bottom - wRect.Top)
                Return Capture.ScreenRectangle(rect)
            Else
                Throw New SlickTestUIException(Capture.ApiError)
            End If
        End If
        Return Nothing
    End Function

    ''' <summary>
    ''' Captures a rectangle image from the screen. 
    ''' Returns a bitmap if the function succeeded, 
    ''' otherwise returns nothing.
    ''' </summary>
    ''' <param name="imageRect">A rectangle within the screen 
    ''' coordinates whose image should be captured.</param>
    Public Shared Function ScreenRectangle(ByVal imageRect As Rectangle) As Bitmap
        If imageRect.Width > 0 AndAlso imageRect.Height > 0 Then
            'Get the handle device context for the entire screen.
            Dim wHdc As IntPtr = GetWindowDC(IntPtr.Zero)
            'Create graphics object from bitmap.
            Dim g As Graphics
            Dim img As New Bitmap(imageRect.Width, imageRect.Height)
            img.MakeTransparent()
            g = Graphics.FromImage(img)
            'Get handle device context from the graphics object.
            Dim gHdc As IntPtr = g.GetHdc()
            'Copy the screen to the bitmap.
            If BitBlt(gHdc, 0, 0, imageRect.Width, imageRect.Height, _
            wHdc, imageRect.X, imageRect.Y, SRCCOPY Or CAPTUREBLT) = 0 Then
                Throw New SlickTestUIException(Capture.ApiError)
            End If
            'Release handles to device contexts.
            g.ReleaseHdc(gHdc)
            ReleaseDC(IntPtr.Zero, wHdc)
            g.Dispose()
            Return img
        End If
        Return Nothing
    End Function

    ''' <summary>
    ''' Gets the last api error occurred in the api 
    ''' dll and returns the error message as a string.
    ''' </summary>
    Public Shared Function ApiError() As String
        Dim message As String = ""
        Dim eCode As Integer = GetLastError()
        If eCode > 0 Then
            FormatMessage(FORMAT_MESSAGE_ALLOCATE_BUFFER Or _
            FORMAT_MESSAGE_FROM_SYSTEM Or FORMAT_MESSAGE_IGNORE_INSERTS, _
            0, eCode, LANG_NEUTRAL, message, 0, 0)
        End If
        Return message
    End Function

#End Region

End Class
