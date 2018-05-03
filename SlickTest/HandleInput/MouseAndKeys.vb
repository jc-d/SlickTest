Imports System
Imports System.IO
Imports System.Windows.Forms
Imports System.Runtime.InteropServices
Imports winAPI.API
<CLSCompliant(True)> _
Public Class MouseAndKeys
    Inherits MouseOnlyHandler

#Const isAbs = 2 '1 means you are using absolute positioning.
    '//winapis
    'Public Declare Function WindowFromPoint Lib "user32" (ByVal xPoint As Integer, ByVal yPoint As Integer) As IntPtr
    Private Property keyBuffer() As String
        Get
            Return keyBufferVal
        End Get
        Set(ByVal value As String)
            keyBufferVal = value
        End Set
    End Property
    Private keyBufferVal As System.String

    Protected Overrides ReadOnly Property ShiftState()
        Get
            If (isHooked = False) Then
                Throw New Exception("Keyboard hooks are not present when trying to read shift state.")
            End If
            Try
                Return _mgr_keyboard.ShiftKeyDown()
            Catch ex As Exception
                Return False
            End Try
        End Get
    End Property

    Protected Overrides ReadOnly Property CtrlState()
        Get
            If (isHooked = False) Then
                Throw New Exception("Keyboard hooks are not present when trying to read ctrl state.")
            End If
            Try
                Return _mgr_keyboard.CtrlKeyDown()
            Catch ex As Exception
                Return False
            End Try
        End Get
    End Property

    Protected Overrides ReadOnly Property AltState()
        Get
            If (isHooked = False) Then
                Throw New Exception("Keyboard hooks are not present when trying to read alt state.")
            End If
            Try
                Return _mgr_keyboard.AltKeyDown()
            Catch ex As Exception
                Return False
            End Try
        End Get
    End Property

    Private WindowsFunctions As New APIControls.IndependentWindowsFunctionsv1
    Private WithEvents _mgr_keyboard As WindowsHookLib.LLKeyboardHook
    Public Event TypingWindowChanged(ByVal action As String, ByVal Text As String, ByVal PreviousHwnd As IntPtr, ByVal OverrideErrors As Boolean)

    Public Sub New()
        MyBase.New()
        _mgr_keyboard = New WindowsHookLib.LLKeyboardHook
    End Sub

    Public Overrides Sub StartHandlers()
        MyBase.StartHandlers()
        keyBuffer = ""
        Try
            _mgr_keyboard.InstallHook()
        Catch ex As Exception
            System.Windows.Forms.MessageBox.Show("Unable to connect to the keyboard!  Error data: " & ex.ToString())
        End Try
    End Sub

    Public Overrides Sub StopHandlers()
        MyBase.StopHandlers()
        Try
            If (isHooked = True) Then
                _mgr_keyboard.RemoveHook()
            End If
        Catch ex As Exception
            System.Windows.Forms.MessageBox.Show("Unable to disconnect to the keyboard!  Error data: " & ex.ToString())
        End Try
        isHooked = False
    End Sub

    Private Function getClassName(ByVal hwnd As IntPtr) As String
        Return WindowsFunctions.GetClassName(hwnd)
    End Function

    Private Function getWindowText(ByVal hwnd As IntPtr) As String
        Return WindowsFunctions.GetAllText(hwnd)
    End Function

    Public Function GetKeys() As String
        Dim tempKey As String = keyBuffer
        keyBuffer = ""
        Return tempKey
    End Function

    Private Function SimpleModify(ByVal s As String) As String
        If (s.Length > 1) Then
            If AltState = True Then
                s = "%" + s
            End If
            If CtrlState = True Then
                s = "^" + s
            End If
            Return s
        Else
            Dim tmp As String
            Dim modified As Boolean = False
            tmp = "{" + s + "}"
            If AltState = True Then
                tmp = "%" + tmp
                modified = True
            End If
            If CtrlState = True Then
                tmp = "^" + tmp
                modified = True
            End If
            If (modified = True) Then
                Return tmp
            Else
                Return s
            End If
        End If
    End Function

    Private Function Modify(ByVal s As String) As String
        If ShiftState = True Then
            s = "+" + s
        End If
        Return SimpleModify(s)
    End Function

    Function FindKey(ByVal str As System.Windows.Forms.Keys) As String
        Dim temp As String = ""
        '[Enum].Format() '(GetType(System.Windows.Forms.Keys), str, "")
        temp = System.Enum.GetName(GetType(System.Windows.Forms.Keys), str).ToLower
        If temp.Length > 1 Then
            Select Case temp
                Case "controlkey"
                    Return ""
                Case "menu"
                    Return ""
                Case "shiftkey"
                    Return ""
                Case "up"
                    temp = Modify("{UP}")
                    ' break 
                Case "down"
                    temp = Modify("{DOWN}")
                    ' break 
                Case "left"
                    temp = Modify("{LEFT}")
                    ' break 
                Case "right"
                    temp = Modify("{RIGHT}")
                    ' break 
                Case "space"
                    temp = " "
                    ' break 
                Case "home"
                    temp = Modify("{HOME}")
                    ' break 
                Case "~"
                    temp = "{~}"
                Case "apps"
                    temp = Modify("{CONTEXTMENU}")
                Case "tab"
                    temp = Modify("{TAB}")
                    ' break 
                Case "escape"
                    temp = Modify("{ESCAPE}")
                    ' break 
                Case "return"
                    temp = Modify("{ENTER}")
                    ' break 
                Case "back"
                    temp = Modify("{BS}")
                Case "lwin"
                    temp = "{LWIN}"
                Case "oemcomma"
                    If ShiftState = False Then
                        temp = ","
                    Else
                        temp = "<"
                    End If
                    ' break 
                Case "oemsemicolon"
                    If ShiftState = False Then
                        temp = ";"
                    Else
                        temp = ":"
                    End If
                    ' break 
                Case "d1"
                    If ShiftState = False Then
                        temp = "1"
                    Else
                        temp = "!"
                    End If
                    ' break 
                Case "d2"
                    If ShiftState = False Then
                        temp = "2"
                    Else
                        temp = "@"
                    End If
                    ' break 
                Case "d3"
                    If ShiftState = False Then
                        temp = "3"
                    Else
                        temp = "#"
                    End If
                Case "d4"
                    If ShiftState = False Then
                        temp = "4"
                    Else
                        temp = "$"
                    End If
                    ' break 
                Case "d5"
                    If ShiftState = False Then
                        temp = "5"
                    Else
                        temp = "%"
                    End If
                    ' break 
                Case "d6"
                    If ShiftState = False Then
                        temp = "6"
                    Else
                        temp = "^"
                    End If
                    ' break 
                Case "d7"
                    If ShiftState = False Then
                        temp = "7"
                    Else
                        temp = "&"
                    End If
                    ' break 
                Case "d8"
                    If ShiftState = False Then
                        temp = "8"
                    Else
                        temp = "*"
                    End If
                    ' break 
                Case "d9"
                    If ShiftState = False Then
                        temp = "9"
                    Else
                        temp = "("
                    End If
                    ' break 
                Case "d0"
                    If ShiftState = False Then
                        temp = "0"
                    Else
                        temp = ")"
                    End If
                    ' break 

                Case "capslock"
                    temp = ""
                    ' break 
                Case "multiply"
                    temp = "*"
                    ' break 
                Case "divide"
                    temp = "/"
                    ' break 
                Case "end"
                    temp = Modify("{END}")
                    ' break 
                Case "pagedown"
                    temp = Modify("{PGDN}")
                    ' break 
                Case "pageup"
                    temp = Modify("{PGUP}")
                    ' break 
                Case "f1"
                    temp = "{F1}"
                Case "f2"
                    temp = "{F2}"
                Case "f3"
                    temp = "{F3}"
                Case "f4"
                    temp = "{F4}"
                Case "f5"
                    temp = "{F5}"
                Case "f6"
                    temp = "{F6}"
                Case "f7"
                    temp = "{F7}"
                Case "f8"
                    temp = "{F8}"
                Case "f9"
                    temp = "{F9}"
                Case "f10"
                    temp = "{F10}"
                Case "f11"
                    temp = "{F11}"
                Case "f12"
                    temp = "{F12}"
                Case "insert"
                    temp = Modify("{INSERT}")
                Case "scroll"
                    temp = "{SCROLLLOCK}"
                Case "prior"
                    temp = Modify("{PGUP}")
                Case "numlock"
                    temp = "{NUMLOCK}"
                Case "pause"
                    temp = "{BREAK}"
                Case "lwin"
                    temp = ""
                Case "enter"
                    temp = "{ENTER}"
                Case "rwin"
                    temp = ""
                Case "subtract"
                    temp = "-"
                Case "decimal"
                    temp = "."
                Case "add"
                    temp = "{+}"
                Case "delete"
                    temp = Modify("{DEL}")
                Case "oemplus"
                    If ShiftState = False Then
                        temp = "="
                    Else
                        temp = "{+}"
                    End If
                    ' break 
                Case "oemminus"
                    If ShiftState = False Then
                        temp = "-"
                    Else
                        temp = "_"
                    End If
                    ' break 
                Case "oemtilde"
                    If ShiftState = False Then
                        temp = "`"
                    Else
                        temp = "{~}"
                    End If
                    ' break 
                Case "oemquestion"
                    If ShiftState = False Then
                        temp = "/"
                    Else
                        temp = "?"
                    End If
                Case "oem2"
                    If ShiftState = False Then
                        temp = "/"
                    Else
                        temp = "?"
                    End If
                Case "oemquotes"
                    If ShiftState = False Then
                        temp = "'"
                    Else
                        temp = """"""
                    End If
                    ' break 
                Case "oempipe"
                    If ShiftState = False Then
                        temp = "\"
                    Else
                        temp = "|"
                    End If
                    ' break 
                Case "oemopenbrackets"
                    If ShiftState = False Then
                        temp = "["
                    Else
                        temp = "{"
                    End If
                    ' break 
                Case "oemclosebrackets"
                    If ShiftState = False Then
                        temp = "]"
                    Else
                        temp = "}"
                    End If
                    ' break
                Case Else
                    If Not (temp.IndexOf("numpad") = -1) Then
                        temp = temp.Replace("numpad", "")
                    End If
                    If Not (temp.IndexOf("period") = -1) Then
                        temp = "."
                    End If
                    If Not (temp.IndexOf("oem") = -1) Then
                        Try
                            temp = Convert.ToChar(str - 128) 'last try
                        Catch ex As Exception
                            temp = ""
                        End Try

                    End If

            End Select
        Else
            temp = SimpleModify(temp)
        End If
        If ShiftState = False Then
            Return temp.ToLower
        Else
            Return temp.ToUpper
        End If
    End Function

    Protected Overrides Sub HandleKeyBoard(Optional ByVal OverrideErrors As Boolean = False)
        If (keyBuffer <> "") Then
            Dim str As String = ProcessWindow.BuildWindowTree(CurrentHwnd, "")
            Dim myBuffer As String = String.Empty
            Dim TmpKeyBuffer As String = keyBuffer
            If (str.IndexOf(vbNewLine) <> -1) Then
                myBuffer += ProcessWindow.ProccessStrs(str, 0, 0)
                keyBuffer = "" 'dump keys
            ElseIf (OverrideErrors = True) Then 'we're logging it even with errors.
                keyBuffer = "" 'dump keys
            End If
            RaiseEvent TypingWindowChanged(myBuffer, TmpKeyBuffer, CurrentHwnd, OverrideErrors)

            CurrentHwnd = IntPtr.Zero
        End If
    End Sub

    Private Sub _mgr_KeyDown(ByVal sender As Object, ByVal e As WindowsHookLib.LLKeyEventArgs) Handles _mgr_keyboard.KeyDown
        If (IsRecording = False) Then Return
        If (TopWindow.GetActiveWindowHandle = Me.hwndOfApp) Then Return
        Dim ti As WinAPI.API.GUITHREADINFO
        ti = WinAPI.API.GetThreadInfo(0) '0= active window thread.

        Dim tmphwnd As IntPtr = ti.hwndFocus

        'Console.WriteLine("ti.cbSize = " & ti.cbSize.ToString() & " ti.flags = " & ti.flags.ToString() & _
        '" ti.hwndActive = " & ti.hwndActive.ToString() & " ti.hwndCapred = " & ti.hwndCapred.ToString() & _
        '" ti.hwndCapture = " & ti.hwndCapture.ToString() & " ti.hwndFocus =" & ti.hwndFocus.ToString() & _
        '" ti.hwndMenuOwner = " & ti.hwndMenuOwner.ToString() & " ti.hwndMoveSize = " & ti.hwndMoveSize.ToString() & _
        '"ti.rcCaret = " & ti.rcCaret.ToString())

        'Console.WriteLine("tmphwnd = " & tmphwnd.ToString())
        If (tmphwnd <> Me.CurrentHwnd OrElse tmphwnd = IntPtr.Zero) Then
            HandleKeyBoard()
            CurrentHwnd = tmphwnd
        End If
        keyBuffer += FindKey(e.KeyCode)
    End Sub

    Public Overrides Sub CloseHandles()
        MyBase.CloseHandles()
        Me._mgr_keyboard.Dispose()
    End Sub
End Class