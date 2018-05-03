Imports winAPI.API
Imports System

Friend Class TextBoxWindowsAPI
    Private Declare Function SendMessage Lib "user32.dll" Alias "SendMessageA" (ByVal window As IntPtr, ByVal msg As Integer, ByVal wParam As IntPtr, ByVal lParam As IntPtr) As Integer
    Private Shared WindowsFunctions As APIControls.IndependentWindowsFunctionsv1

    Public Sub New(ByVal wf As APIControls.IndependentWindowsFunctionsv1)
        WindowsFunctions = wf
    End Sub

    Public Function IsTextBox(ByVal hwnd As IntPtr) As Boolean
        If (GenericMethodsUIAutomation.IsWPFOrCustom(hwnd) = True) Then
            Return WindowsFunctions.WpfTextBox.IsTextBox(hwnd)
        End If
        Dim cn As String = WindowsFunctions.GetClassNameNoDotNet(hwnd)
        If (cn.ToLowerInvariant.Contains("edit") = True) Then
            Return True
        End If
        Return False
    End Function

    Public Function GetCaretLineIndex(ByVal hwnd As IntPtr) As Integer
        Return GetLineIndexForLine(hwnd, -1)
    End Function
    Public Function GetLineIndexForLine(ByVal hwnd As IntPtr, ByVal lineNumber As Integer) As Integer
        Return SendMessage(hwnd, EM_LINEINDEX, New IntPtr(lineNumber), IntPtr.Zero)
    End Function

    Public Sub SetLineIndex(ByVal hwnd As IntPtr, ByVal Index As Integer)
        SendMessage(hwnd, EM_LINEINDEX, New IntPtr(-1), New IntPtr(Index))
    End Sub

    Public Function GetLineLength(ByVal hwnd As IntPtr, ByVal lineNumber As Integer) As Integer
        Dim CharCount As Integer = GetLineIndexForLine(hwnd, lineNumber)
        Return SendMessage(hwnd, EM_LINELENGTH, New IntPtr(CharCount), IntPtr.Zero)
    End Function

    ''' <summary>
    ''' This attempts to get all the windows text, rather than the first 256 characters.
    ''' </summary>
    ''' <param name="WindowHandle"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetAllWindowText(ByVal WindowHandle As IntPtr) As String
        Dim ptrRet As IntPtr
        Dim ptrLength As Integer
        'get length for buffer... 
        ptrLength = WinAPI.NativeFunctions.SendMessage(WindowHandle, WM_GETTEXTLENGTH, IntPtr.Zero, IntPtr.Zero)
        'create buffer for return value... 
        Dim sbText As New System.Text.StringBuilder(ptrLength + 1)
        'get window text... 
        ptrRet = WinAPI.API.SendMessage(WindowHandle, WM_GETTEXT, ptrLength + 1, sbText)
        'get return value... 
        Return sbText.ToString
    End Function

    Public Function GetLineText(ByVal hwnd As IntPtr, ByVal lineNumber As Integer) As String
        Dim CharCount As Integer = GetLineIndexForLine(hwnd, lineNumber)
        If CharCount > -1 Then
            Dim Length As Integer = SendMessage(hwnd, EM_LINELENGTH, New IntPtr(CharCount), IntPtr.Zero)
            Dim Text As String = GetAllWindowText(hwnd)
            Return (Text.Substring(CharCount, Length))
        Else
            Return String.Empty
        End If
    End Function

    Public Function GetLineCount(ByVal hwnd As IntPtr) As Integer 'EM_GETLINECOUNT
        Return SendMessage(hwnd, EM_GETLINECOUNT, IntPtr.Zero, IntPtr.Zero)
    End Function


    Public Function GetLineNumber(ByVal hwnd As IntPtr) As Integer
        Return SendMessage(hwnd, EM_LINEFROMCHAR, New IntPtr(GetCaretLineIndex(hwnd)), IntPtr.Zero)
        'Dim i As Integer, j As Integer
        ''Dim lParam As Integer, wParam As Integer
        'Dim k, k1 As Integer

        'i = WinAPI.NativeFunctions.SendMessage(hwnd, EM_GETSEL, 0, 0)
        'j = i / 2 ^ 16
        ''******Caret**********byte

        'k1 = WinAPI.NativeFunctions.SendMessage(hwnd, EM_GETLINE, 0, 0) '**********
        'LineNumber = WinAPI.NativeFunctions.SendMessage(hwnd, EM_LINEFROMCHAR, 0, 0) '**********
        'LineNumber = LineNumber + 1
        ''SendMessage(hwnd, EM_LINEINDEX, -1, 0)
        'k = WinAPI.NativeFunctions.SendMessage(hwnd, EM_LINEINDEX, -1, 0)
        ''*****caret**********byte
        'ColumnNumber = j - k + 1
        'Return New System.Drawing.Point(ColNo, LineNo)
    End Function
End Class
