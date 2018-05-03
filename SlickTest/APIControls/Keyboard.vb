Friend Class InternalKeyboard

    Protected Friend Const INPUT_KEYBOARD As Integer = 1
    Protected Friend Const KEYEVENTF_EXTENDEDKEY As Integer = 1
    Protected Friend Const KEYEVENTF_KEYUP As Integer = 2
    Protected Friend Const KEYEVENTF_UNICODE As Integer = 4
    Protected Friend Const KEYEVENTF_SCANCODE As Integer = 8

    Private Shared HoldShift As Boolean = False
    Private Shared HoldAlt As Boolean = False
    Private Shared HoldCtrl As Boolean = False
    Private Shared HoldLWin As Boolean = False
    Private Shared HoldRWin As Boolean = False
    Private Shared MyQueue As System.Collections.Generic.Queue(Of Char)

    <Runtime.InteropServices.DllImport("user32.dll", SetLastError:=True)> _
     Protected Friend Shared Function SendInput(ByVal nInputs As Integer, ByRef mi As INPUT, ByVal cbSize As Integer) As Integer
    End Function
    <Runtime.InteropServices.StructLayout(Runtime.InteropServices.LayoutKind.Sequential)> _
     Protected Friend Structure INPUT
        Public type As Integer
        Public union As INPUTUNION
    End Structure
    <Runtime.InteropServices.StructLayout(Runtime.InteropServices.LayoutKind.Sequential)> _
        Protected Friend Structure MOUSEINPUT
        Public dx As Integer
        Public dy As Integer
        Public mouseData As Integer
        Public dwFlags As Integer
        Public time As Integer
        Public dwExtraInfo As IntPtr
    End Structure
    <Runtime.InteropServices.StructLayout(Runtime.InteropServices.LayoutKind.Explicit)> _
    Protected Friend Structure INPUTUNION
        <Runtime.InteropServices.FieldOffset(0)> _
        Public mouseInput As MOUSEINPUT
        <Runtime.InteropServices.FieldOffset(0)> _
        Public keyboardInput As KEYBDINPUT
    End Structure
    <Runtime.InteropServices.StructLayout(Runtime.InteropServices.LayoutKind.Sequential)> _
    Protected Friend Structure KEYBDINPUT
        Public wVk As Short
        Public wScan As Short
        Public dwFlags As Integer
        Public time As Integer
        Public dwExtraInfo As IntPtr
    End Structure

    Protected Friend Enum VirtualKeys As Byte
        VK_LBUTTON = 1
        VK_CANCEL = 3
        VK_BACK = 8
        VK_TAB = 9
        VK_CLEAR = 12
        VK_RETURN = 13
        VK_SHIFT = 16
        VK_CONTROL = 17
        VK_MENU = 18 ' same as alt
        VK_CAPITAL = 20
        VK_ESCAPE = 27
        VK_SPACE = 32
        VK_PRIOR = 33
        VK_NEXT = 34
        VK_END = 35
        VK_HOME = 36
        VK_LEFT = 37
        VK_UP = 38
        VK_RIGHT = 39
        VK_DOWN = 40
        VK_SELECT = 41
        VK_EXECUTE = 43
        VK_SNAPSHOT = 44
        VK_INSERT = 45 'Insert added by me.
        VK_DELETE = 46 ' This was added by me.
        VK_HELP = 47
        VK_0 = 48
        VK_1 = 49
        VK_2 = 50
        VK_3 = 51
        VK_4 = 52
        VK_5 = 53
        VK_6 = 54
        VK_7 = 55
        VK_8 = 56
        VK_9 = 57
        VK_A = 65
        VK_B = 66
        VK_C = 67
        VK_D = 68
        VK_E = 69
        VK_F = 70
        VK_G = 71
        VK_H = 72
        VK_I = 73
        VK_J = 74
        VK_K = 75
        VK_L = 76
        VK_M = 77
        VK_N = 78
        VK_O = 79
        VK_P = 80
        VK_Q = 81
        VK_R = 82
        VK_S = 83
        VK_T = 84
        VK_U = 85
        VK_V = 86
        VK_W = 87
        VK_X = 88
        VK_Y = 89
        VK_Z = 90
        VK_APPS = 93 'Added by me, 
        VK_NUMPAD0 = 96
        VK_NUMPAD1 = 97
        VK_NUMPAD2 = 98
        VK_NUMPAD3 = 99
        VK_NUMPAD4 = 100
        VK_NUMPAD5 = 101
        VK_NUMPAD6 = 102
        VK_NUMPAD7 = 103
        VK_NUMPAD8 = 104
        VK_NUMPAD9 = 105
        VK_MULTIPLY = 106
        VK_ADD = 107
        VK_SEPARATOR = 108
        VK_SUBTRACT = 109
        VK_DECIMAL = 110
        VK_DIVIDE = 111
        VK_F1 = 112
        VK_F2 = 113
        VK_F3 = 114
        VK_F4 = 115
        VK_F5 = 116
        VK_F6 = 117
        VK_F7 = 118
        VK_F8 = 119
        VK_F9 = 120
        VK_F10 = 121
        VK_F11 = 122
        VK_F12 = 123
        VK_F13 = 124
        VK_F14 = 125
        VK_F15 = 126
        VK_F16 = 127
        VK_F17 = 128
        VK_F18 = 129
        VK_F19 = 130
        VK_F20 = 131
        VK_F21 = 132
        VK_F22 = 133
        VK_F23 = 134
        VK_F24 = 135
        VK_NUMBERLOCK = 144
        VK_SCROLLLOCK = 145 'Added by me
        VK_ATTN = 246
        VK_CRSEL = 247
        VK_EXSEL = 248
        VK_EREOF = 249
        VK_PLAY = 250
        VK_ZOOM = 251
        VK_NONAME = 252
        VK_PA1 = 253
        VK_OEM_CLEAR = 254
        VK_LWIN = 91
        VK_RWIN = 92
        VK_LSHIFT = 160
        VK_RSHIFT = 161
        VK_LCONTROL = 162
        VK_RCONTROL = 163
        VK_LMENU = 164
        VK_RMENU = 165
    End Enum

    Private Shared Function HandleBracket(ByVal MyQueue As System.Collections.Generic.Queue(Of Char), ByVal Keys As String) As System.Collections.Generic.Queue(Of Char)
        'System.Windows.Forms.Keys.Apps
        Dim ErrStr As String = "SendKeys formatting incorrect for: "
        If (MyQueue.Count <> 0) Then
            MyQueue.Dequeue()
            If (MyQueue.Peek = "{") Then
                MyQueue.Dequeue()
                If (MyQueue.Dequeue() <> "}") Then
                    Throw New ArgumentException(ErrStr & Keys)
                Else
                    InternalSendUnicodeString("{", 1, 10)
                    Return MyQueue
                End If
            End If
            If (MyQueue.Peek = "~") Then
                MyQueue.Dequeue()
                If (MyQueue.Dequeue() <> "}") Then
                    Throw New ArgumentException(ErrStr & Keys)
                Else
                    InternalSendUnicodeString("~", 1, 10)
                    Return MyQueue
                End If
            End If
            If (MyQueue.Peek = "(") Then
                MyQueue.Dequeue()
                If (MyQueue.Dequeue() <> "}") Then
                    Throw New ArgumentException(ErrStr & Keys)
                Else
                    InternalSendUnicodeString("(", 1, 10)
                    Return MyQueue
                End If
            End If
            If (MyQueue.Peek = ")") Then
                MyQueue.Dequeue()
                If (MyQueue.Dequeue() <> "}") Then
                    Throw New ArgumentException(ErrStr & Keys)
                Else
                    InternalSendUnicodeString(")", 1, 10)
                    Return MyQueue
                End If
            End If
            If (MyQueue.Peek = "+") Then
                MyQueue.Dequeue()
                If (MyQueue.Dequeue() <> "}") Then
                    Throw New ArgumentException(ErrStr & Keys)
                Else
                    InternalSendUnicodeString("+", 1, 10)
                    Return MyQueue
                End If
            End If
            If (MyQueue.Peek = "%") Then
                MyQueue.Dequeue()
                If (MyQueue.Dequeue() <> "}") Then
                    Throw New ArgumentException(ErrStr & Keys)
                Else
                    InternalSendUnicodeString("%", 1, 10)
                    Return MyQueue
                End If
            End If
            If (MyQueue.Peek = "^") Then
                MyQueue.Dequeue()
                If (MyQueue.Dequeue() <> "}") Then
                    Throw New ArgumentException(ErrStr & Keys)
                Else
                    InternalSendUnicodeString("^", 1, 10)
                    Return MyQueue
                End If
            End If
            If (MyQueue.Peek = "*") Then
                MyQueue.Dequeue()
                If (MyQueue.Dequeue() <> "}") Then
                    Throw New ArgumentException(ErrStr & Keys)
                Else
                    InternalSendUnicodeString("*", 1, 10)
                    Return MyQueue
                End If
            End If
        End If
        Dim strCommand As String = String.Empty
        While (MyQueue.Count <> 0)
            strCommand += Char.ToLower(MyQueue.Dequeue())
            If (strCommand.EndsWith("}") = True) Then
                Return MyQueue 'unsupported word...
            End If
            'completed:
            'backspace, enter,delete, up,down,left,right,windows key,f1-f24,
            'esc, tab, add, subtract, divide, multiply, home, insert, caps
            'scroll lock, end, pg down, pg up,  help, num lock, print screen
            'todo:
            'break.
            Select Case strCommand
                Case "contextmenu"
                    SendKeyboardInputVK(VirtualKeys.VK_APPS)
                    If (MyQueue.Dequeue() <> "}") Then
                        Throw New ArgumentException(ErrStr & Keys)
                    Else
                        Return MyQueue
                    End If
                Case "prtscr"
                    SendKeyboardInputVK(VirtualKeys.VK_SNAPSHOT)
                    If (MyQueue.Dequeue() <> "}") Then
                        Throw New ArgumentException(ErrStr & Keys)
                    Else
                        Return MyQueue
                    End If
                Case "numlock"
                    SendKeyboardInputVK(VirtualKeys.VK_NUMBERLOCK)
                    If (MyQueue.Dequeue() <> "}") Then
                        Throw New ArgumentException(ErrStr & Keys)
                    Else
                        Return MyQueue
                    End If
                Case "help"
                    SendKeyboardInputVK(VirtualKeys.VK_HELP)
                    If (MyQueue.Dequeue() <> "}") Then
                        Throw New ArgumentException(ErrStr & Keys)
                    Else
                        Return MyQueue
                    End If
                Case "pgup"
                    SendKeyboardInputVK(VirtualKeys.VK_PRIOR)
                    If (MyQueue.Dequeue() <> "}") Then
                        Throw New ArgumentException(ErrStr & Keys)
                    Else
                        Return MyQueue
                    End If
                Case "pgdn"
                    SendKeyboardInputVK(VirtualKeys.VK_NEXT)
                    If (MyQueue.Dequeue() <> "}") Then
                        Throw New ArgumentException(ErrStr & Keys)
                    Else
                        Return MyQueue
                    End If
                Case "end"
                    SendKeyboardInputVK(VirtualKeys.VK_END)
                    If (MyQueue.Dequeue() <> "}") Then
                        Throw New ArgumentException(ErrStr & Keys)
                    Else
                        Return MyQueue
                    End If
                Case "capslock"
                    SendKeyboardInputVK(VirtualKeys.VK_CAPITAL)
                    If (MyQueue.Dequeue() <> "}") Then
                        Throw New ArgumentException(ErrStr & Keys)
                    Else
                        Return MyQueue
                    End If
                Case "scrolllock"
                    SendKeyboardInputVK(VirtualKeys.VK_SCROLLLOCK)
                    If (MyQueue.Dequeue() <> "}") Then
                        Throw New ArgumentException(ErrStr & Keys)
                    Else
                        Return MyQueue
                    End If
                Case "home"
                    SendKeyboardInputVK(VirtualKeys.VK_HOME)
                    If (MyQueue.Dequeue() <> "}") Then
                        Throw New ArgumentException(ErrStr & Keys)
                    Else
                        Return MyQueue
                    End If
                Case "ins", "insert"
                    SendKeyboardInputVK(VirtualKeys.VK_INSERT)
                    If (MyQueue.Dequeue() <> "}") Then
                        Throw New ArgumentException(ErrStr & Keys)
                    Else
                        Return MyQueue
                    End If
                    'Case "insert"
                    '    SendKeyboardInputVK(VirtualKeys.VK_INSERT)
                    '    If (MyQueue.Dequeue() <> "}") Then
                    '        Throw New ArgumentException(ErrStr & Keys)
                    '    Else
                    '        Return MyQueue
                    '    End If
                Case "multiply"
                    SendKeyboardInputVK(VirtualKeys.VK_MULTIPLY)
                    If (MyQueue.Dequeue() <> "}") Then
                        Throw New ArgumentException(ErrStr & Keys)
                    Else
                        Return MyQueue
                    End If
                Case "divide"
                    SendKeyboardInputVK(VirtualKeys.VK_DIVIDE)
                    If (MyQueue.Dequeue() <> "}") Then
                        Throw New ArgumentException(ErrStr & Keys)
                    Else
                        Return MyQueue
                    End If
                Case "subtract"
                    SendKeyboardInputVK(VirtualKeys.VK_SUBTRACT)
                    If (MyQueue.Dequeue() <> "}") Then
                        Throw New ArgumentException(ErrStr & Keys)
                    Else
                        Return MyQueue
                    End If
                Case "add"
                    SendKeyboardInputVK(VirtualKeys.VK_ADD)
                    If (MyQueue.Dequeue() <> "}") Then
                        Throw New ArgumentException(ErrStr & Keys)
                    Else
                        Return MyQueue
                    End If
                Case "enter"
                    SendKeyboardInputVK(VirtualKeys.VK_RETURN)
                    If (MyQueue.Dequeue() <> "}") Then
                        Throw New ArgumentException(ErrStr & Keys)
                    Else
                        Return MyQueue
                    End If
                Case "bs", "backspace", "bksp"
                    SendKeyboardInputVK(VirtualKeys.VK_BACK)
                    If (MyQueue.Dequeue() <> "}") Then
                        Throw New ArgumentException(ErrStr & Keys)
                    Else
                        Return MyQueue
                    End If
                Case "del", "delete"
                    If (MyQueue.Peek() <> "e") Then
                        SendKeyboardInputVK(VirtualKeys.VK_DELETE)
                    Else
                        If (MyQueue.Dequeue() <> "}") Then
                            Throw New ArgumentException(ErrStr & Keys)
                        Else
                            Return MyQueue
                        End If
                    End If
                Case "up"
                    SendKeyboardInputVK(VirtualKeys.VK_UP)
                    If (MyQueue.Dequeue() <> "}") Then
                        Throw New ArgumentException(ErrStr & Keys)
                    Else
                        Return MyQueue
                    End If
                Case "down"
                    SendKeyboardInputVK(VirtualKeys.VK_DOWN)
                    If (MyQueue.Dequeue() <> "}") Then
                        Throw New ArgumentException(ErrStr & Keys)
                    Else
                        Return MyQueue
                    End If
                Case "left"
                    SendKeyboardInputVK(VirtualKeys.VK_LEFT)
                    If (MyQueue.Dequeue() <> "}") Then
                        Throw New ArgumentException(ErrStr & Keys)
                    Else
                        Return MyQueue
                    End If
                Case "right"
                    SendKeyboardInputVK(VirtualKeys.VK_RIGHT)
                    If (MyQueue.Dequeue() <> "}") Then
                        Throw New ArgumentException(ErrStr & Keys)
                    Else
                        Return MyQueue
                    End If
                Case "esc"
                    SendKeyboardInputVK(VirtualKeys.VK_ESCAPE)
                    If (MyQueue.Dequeue() <> "}") Then
                        Throw New ArgumentException(ErrStr & Keys)
                    Else
                        Return MyQueue
                    End If
                Case "tab"
                    SendKeyboardInputVK(VirtualKeys.VK_TAB)
                    If (MyQueue.Dequeue() <> "}") Then
                        Throw New ArgumentException(ErrStr & Keys)
                    Else
                        Return MyQueue
                    End If
                Case "f1"
                    If (Char.IsDigit(MyQueue.Peek()) = False) Then
                        SendKeyboardInputVK(VirtualKeys.VK_F1)
                        If (MyQueue.Dequeue() <> "}") Then
                            Throw New ArgumentException(ErrStr & Keys)
                        Else
                            Return MyQueue
                        End If
                    End If
                Case "f2"
                    If (Char.IsDigit(MyQueue.Peek()) = False) Then
                        SendKeyboardInputVK(VirtualKeys.VK_F2)
                        If (MyQueue.Dequeue() <> "}") Then
                            Throw New ArgumentException(ErrStr & Keys)
                        Else
                            Return MyQueue
                        End If
                    End If
                Case "f3"
                    SendKeyboardInputVK(VirtualKeys.VK_F3)
                    If (MyQueue.Dequeue() <> "}") Then
                        Throw New ArgumentException(ErrStr & Keys)
                    Else
                        Return MyQueue
                    End If
                Case "f4"
                    SendKeyboardInputVK(VirtualKeys.VK_F4)
                    If (MyQueue.Dequeue() <> "}") Then
                        Throw New ArgumentException(ErrStr & Keys)
                    Else
                        Return MyQueue
                    End If
                Case "f5"
                    SendKeyboardInputVK(VirtualKeys.VK_F5)
                    If (MyQueue.Dequeue() <> "}") Then
                        Throw New ArgumentException(ErrStr & Keys)
                    Else
                        Return MyQueue
                    End If
                Case "f6"
                    SendKeyboardInputVK(VirtualKeys.VK_F6)
                    If (MyQueue.Dequeue() <> "}") Then
                        Throw New ArgumentException(ErrStr & Keys)
                    Else
                        Return MyQueue
                    End If
                Case "f7"
                    SendKeyboardInputVK(VirtualKeys.VK_F7)
                    If (MyQueue.Dequeue() <> "}") Then
                        Throw New ArgumentException(ErrStr & Keys)
                    Else
                        Return MyQueue
                    End If
                Case "f8"
                    SendKeyboardInputVK(VirtualKeys.VK_F8)
                    If (MyQueue.Dequeue() <> "}") Then
                        Throw New ArgumentException(ErrStr & Keys)
                    Else
                        Return MyQueue
                    End If
                Case "f9"
                    SendKeyboardInputVK(VirtualKeys.VK_F9)
                    If (MyQueue.Dequeue() <> "}") Then
                        Throw New ArgumentException(ErrStr & Keys)
                    Else
                        Return MyQueue
                    End If
                Case "f10"
                    SendKeyboardInputVK(VirtualKeys.VK_F10)
                    If (MyQueue.Dequeue() <> "}") Then
                        Throw New ArgumentException(ErrStr & Keys)
                    Else
                        Return MyQueue
                    End If
                Case "f11"
                    SendKeyboardInputVK(VirtualKeys.VK_F11)
                    If (MyQueue.Dequeue() <> "}") Then
                        Throw New ArgumentException(ErrStr & Keys)
                    Else
                        Return MyQueue
                    End If
                Case "f12"
                    SendKeyboardInputVK(VirtualKeys.VK_F12)
                    If (MyQueue.Dequeue() <> "}") Then
                        Throw New ArgumentException(ErrStr & Keys)
                    Else
                        Return MyQueue
                    End If
                Case "lwin"
                    SendKeyboardInputVK(VirtualKeys.VK_LWIN)
                    If (MyQueue.Dequeue() <> "}") Then
                        Throw New ArgumentException(ErrStr & Keys)
                    Else
                        Return MyQueue
                    End If
                Case "rwin"
                    SendKeyboardInputVK(VirtualKeys.VK_RWIN)
                    If (MyQueue.Dequeue() <> "}") Then
                        Throw New ArgumentException(ErrStr & Keys)
                    Else
                        Return MyQueue
                    End If
            End Select
        End While
        Return MyQueue
    End Function

    Private Shared Function SendKeys(ByVal keys As String, ByVal MyQueue As System.Collections.Generic.Queue(Of Char)) As System.Collections.Generic.Queue(Of Char)
        Dim PeekChar As Char

        While (MyQueue.Count <> 0)
            PeekChar = MyQueue.Peek()
            Select Case Convert.ToString(PeekChar)
                Case "{"
                    MyQueue = HandleBracket(MyQueue, keys)
                Case "~"
                    MyQueue.Dequeue()
                    SendKeyboardInputVK(VirtualKeys.VK_RETURN)
                Case "+" 'Shift
                    MyQueue = Shift(keys, MyQueue)
                    If (MyQueue.Count <> 0) Then
                        If (MyQueue.Peek() = "(") Then MyQueue.Dequeue()
                    End If
                Case "%" 'Alt?
                    MyQueue = Alt(keys, MyQueue)
                    If (MyQueue.Count <> 0) Then
                        If (MyQueue.Peek() = "(") Then MyQueue.Dequeue()
                    End If
                Case "^" 'ctrl?
                    MyQueue = Ctrl(keys, MyQueue)
                    If (MyQueue.Count <> 0) Then
                        If (MyQueue.Peek() = "(") Then MyQueue.Dequeue()
                    End If
                Case "*" 'windows
                    MyQueue = Win(keys, MyQueue)
                    If (MyQueue.Count <> 0) Then
                        If (MyQueue.Peek() = "(") Then MyQueue.Dequeue()
                    End If
                Case ")"
                    If (HoldShift = True OrElse HoldCtrl = True OrElse HoldAlt = True OrElse HoldLWin = True) Then

                        While (MyQueue.Peek() = ")")
                            MyQueue.Dequeue()
                            If (MyQueue.Count = 0) Then
                                Exit While
                            End If
                        End While
                        If (HoldShift = True) Then
                            SendKeyboardInputVK(VirtualKeys.VK_SHIFT, False)
                        End If
                        If (HoldCtrl = True) Then
                            SendKeyboardInputVK(VirtualKeys.VK_CONTROL, False)
                        End If
                        If (HoldAlt = True) Then
                            SendKeyboardInputVK(VirtualKeys.VK_MENU, False)
                        End If
                        If (HoldLWin = True) Then
                            SendKeyboardInputVK(VirtualKeys.VK_LWIN, False)
                        End If
                        HoldCtrl = False
                        HoldShift = False
                        HoldAlt = False
                        HoldLWin = False
                    Else
                        SendUnicodeKeyboardInput(PeekChar, True)
                        SendUnicodeKeyboardInput(MyQueue.Dequeue(), False)
                    End If
                Case Else
                    'Console.WriteLine("**************")
                    Dim charAsByte As Byte = Convert.ToByte(PeekChar)
                    Dim ShouldHoldShift As Boolean = (charAsByte = Convert.ToByte(Char.ToUpper(PeekChar)))
                    'Console.WriteLine("Char: " & PeekChar & " Byte: " & _
                    '                 charAsByte & " HoldShift: " & ShouldHoldShift)

                    If ((charAsByte >= 48 AndAlso charAsByte <= 57) OrElse _
                        (charAsByte >= 65 AndAlso charAsByte <= 90) _
                        OrElse (charAsByte >= 97 AndAlso charAsByte <= 122)) Then
                        Dim TmpHoldShift As Boolean = False
                        If (Not HoldShift AndAlso ShouldHoldShift) Then
                            If (charAsByte >= 65 AndAlso charAsByte <= 90) Then
                                HoldShift = True
                                'Console.WriteLine("Pressing shift - " & PeekChar)
                                SendKeyboardInputVK(VirtualKeys.VK_SHIFT, True)
                                TmpHoldShift = True
                            End If
                        Else
                            If (ShouldHoldShift = False) Then
                                'Console.WriteLine("Uppering character - " & PeekChar)
                                charAsByte = Convert.ToByte(Char.ToUpper(PeekChar))
                            End If
                        End If
                        SendKeyboardInputVK(charAsByte, True)
                        SendKeyboardInputVK(Convert.ToByte(MyQueue.Dequeue()), False)
                        If (TmpHoldShift) Then
                            HoldShift = False
                            SendKeyboardInputVK(VirtualKeys.VK_SHIFT, False)
                        End If
                    Else
                        'Console.WriteLine("Sending Unicode - " & PeekChar & _
                        '                 " Byte: " & charAsByte)
                        SendUnicodeKeyboardInput(PeekChar, True)
                        SendUnicodeKeyboardInput(MyQueue.Dequeue(), False)
                    End If

            End Select
        End While
        'make sure to release all keys...
        If (HoldShift = True) Then
            SendKeyboardInputVK(VirtualKeys.VK_SHIFT, False)
        End If
        If (HoldCtrl = True) Then
            SendKeyboardInputVK(VirtualKeys.VK_CONTROL, False)
        End If
        If (HoldAlt = True) Then
            SendKeyboardInputVK(VirtualKeys.VK_MENU, False)
        End If
        If (HoldLWin = True) Then
            SendKeyboardInputVK(VirtualKeys.VK_LWIN, False)
        End If
        HoldCtrl = False
        HoldShift = False
        HoldAlt = False
        HoldLWin = False
        Return MyQueue
    End Function
    'These are for the windows key only
    'Private Const KEYEVENTF_EXTENDEDKEY As Long = &H1
    'Private Const KEYEVENTF_KEYUP As Long = &H2
    'Private Const VK_LWIN As Byte = &H5B
    'Private Declare Sub keybd_event Lib "user32" (ByVal bVk As Byte, ByVal bScan As Byte, ByVal dwFlags As Integer, ByVal dwExtraInfo As Integer)
    ' Simulate key press
    ' Simulate key release

    Private Shared Function Ctrl(ByVal keys As String, ByVal MyQueue As System.Collections.Generic.Queue(Of Char)) As System.Collections.Generic.Queue(Of Char)
        MyQueue.Dequeue()
        If (HoldCtrl = False) Then
            SendKeyboardInputVK(VirtualKeys.VK_CONTROL, True)
            If (MyQueue.Count = 0) Then
                SendKeyboardInputVK(VirtualKeys.VK_CONTROL, False)
                Return MyQueue
            End If
            If (MyQueue.Peek() = "(") Then
                HoldCtrl = True
                'hold down til ")"
                MyQueue.Dequeue()
            Else
                If (MyQueue.Peek() = "{") Then
                    MyQueue = HandleBracket(MyQueue, keys)
                    SendKeyboardInputVK(VirtualKeys.VK_CONTROL, False)
                    Return MyQueue
                Else
                    If (IsSpecialChar(MyQueue.Peek()) = False) Then
                        SendUnicodeKeyboardInput(MyQueue.Peek(), True)
                        SendUnicodeKeyboardInput(MyQueue.Dequeue(), False)
                        SendKeyboardInputVK(VirtualKeys.VK_CONTROL, False)
                    Else
                        'handle special chars
                        If (MyQueue.Peek() = "%") Then 'Alt
                            MyQueue.Dequeue() 'Alt will be handled here...
                            If (MyQueue.Count = 0 Or MyQueue.Peek() <> "(") Then
                                SendKeyboardInputVK(VirtualKeys.VK_MENU)
                                SendKeyboardInputVK(VirtualKeys.VK_CONTROL, False)
                                Return MyQueue
                            Else
                                HoldAlt = True
                                SendKeyboardInputVK(VirtualKeys.VK_MENU, True)
                                SendKeyboardInputVK(VirtualKeys.VK_CONTROL, False)
                                Return MyQueue
                            End If
                        End If
                        If (MyQueue.Peek() = "+") Then 'Shift
                            MyQueue.Dequeue() 'Shift will be handled here...
                            If (MyQueue.Count = 0 Or MyQueue.Peek() <> "(") Then
                                SendKeyboardInputVK(VirtualKeys.VK_SHIFT)
                                SendKeyboardInputVK(VirtualKeys.VK_CONTROL, False)
                                Return MyQueue
                            Else
                                HoldShift = True
                                SendKeyboardInputVK(VirtualKeys.VK_SHIFT, True)
                                SendKeyboardInputVK(VirtualKeys.VK_CONTROL, False)
                                Return MyQueue
                            End If
                        End If
                        If (MyQueue.Peek() = "*") Then 'Windows
                            MyQueue.Dequeue() 'Windows will be handled here...
                            If (MyQueue.Count = 0 Or MyQueue.Peek() <> "(") Then
                                SendKeyboardInputVK(VirtualKeys.VK_LWIN)
                                SendKeyboardInputVK(VirtualKeys.VK_CONTROL, False)
                                Return MyQueue
                            Else
                                HoldLWin = True
                                SendKeyboardInputVK(VirtualKeys.VK_LWIN, True)
                                SendKeyboardInputVK(VirtualKeys.VK_CONTROL, False)
                                Return MyQueue
                            End If
                        End If
                        If (MyQueue.Peek() = "~") Then 'Enter
                            MyQueue.Dequeue() 'Enter will be handled here...
                            SendKeyboardInputVK(VirtualKeys.VK_RETURN)
                            SendKeyboardInputVK(VirtualKeys.VK_CONTROL, False)
                            Return MyQueue
                        End If
                    End If
                End If
            End If
        End If
        Return MyQueue
    End Function

    Private Shared Function Win(ByVal keys As String, ByVal MyQueue As System.Collections.Generic.Queue(Of Char)) As System.Collections.Generic.Queue(Of Char)
        MyQueue.Dequeue()
        If (HoldLWin = False) Then
            SendKeyboardInputVK(VirtualKeys.VK_LWIN, True)
            'DownWinKey()
            If (MyQueue.Count = 0) Then
                SendKeyboardInputVK(VirtualKeys.VK_LWIN, False)
                'UpWinKey()
                Return MyQueue
            End If
            If (MyQueue.Peek() = "(") Then
                HoldLWin = True
                'hold down til ")"
                MyQueue.Dequeue()
            Else
                If (MyQueue.Peek() = "{") Then
                    MyQueue = HandleBracket(MyQueue, keys)
                    SendKeyboardInputVK(VirtualKeys.VK_LWIN, False)
                    'UpWinKey()
                    Return MyQueue
                Else
                    If (IsSpecialChar(MyQueue.Peek()) = False) Then
                        SendUnicodeKeyboardInput(MyQueue.Peek(), True)
                        SendUnicodeKeyboardInput(MyQueue.Dequeue(), False)
                        SendKeyboardInputVK(VirtualKeys.VK_LWIN, False)
                        'UpWinKey()
                    Else
                        'handle special chars
                        If (MyQueue.Peek() = "%") Then 'Alt
                            MyQueue.Dequeue() 'Alt will be handled here...
                            If (MyQueue.Count = 0 Or MyQueue.Peek() <> "(") Then
                                SendKeyboardInputVK(VirtualKeys.VK_MENU)
                                SendKeyboardInputVK(VirtualKeys.VK_LWIN, False)
                                'UpWinKey()
                                Return MyQueue
                            Else
                                HoldAlt = True
                                SendKeyboardInputVK(VirtualKeys.VK_MENU, True)
                                SendKeyboardInputVK(VirtualKeys.VK_LWIN, False)
                                'UpWinKey()
                                Return MyQueue
                            End If
                        End If
                        If (MyQueue.Peek() = "+") Then 'Shift
                            MyQueue.Dequeue() 'Shift will be handled here...
                            If (MyQueue.Count = 0 Or MyQueue.Peek() <> "(") Then
                                SendKeyboardInputVK(VirtualKeys.VK_SHIFT)
                                SendKeyboardInputVK(VirtualKeys.VK_LWIN, False)
                                'UpWinKey()
                                Return MyQueue
                            Else
                                HoldShift = True
                                SendKeyboardInputVK(VirtualKeys.VK_SHIFT, True)
                                SendKeyboardInputVK(VirtualKeys.VK_LWIN, False)
                                'UpWinKey()
                                Return MyQueue
                            End If
                        End If
                        If (MyQueue.Peek() = "^") Then 'Ctrl
                            MyQueue.Dequeue() 'ctrl will be handled here...
                            If (MyQueue.Count = 0 Or MyQueue.Peek() <> "(") Then
                                SendKeyboardInputVK(VirtualKeys.VK_CONTROL)
                                SendKeyboardInputVK(VirtualKeys.VK_LWIN, False)
                                'UpWinKey()
                                Return MyQueue
                            Else
                                HoldCtrl = True
                                SendKeyboardInputVK(VirtualKeys.VK_CONTROL, True)
                                SendKeyboardInputVK(VirtualKeys.VK_LWIN, False)
                                'UpWinKey()
                                Return MyQueue
                            End If
                        End If
                        If (MyQueue.Peek() = "~") Then 'Enter
                            MyQueue.Dequeue() 'Enter will be handled here...
                            SendKeyboardInputVK(VirtualKeys.VK_RETURN)
                            SendKeyboardInputVK(VirtualKeys.VK_LWIN, False)
                            'UpWinKey()
                            Return MyQueue
                        End If
                    End If
                End If
            End If
        End If
        Return MyQueue
    End Function

    Private Shared Function Alt(ByVal keys As String, ByVal MyQueue As System.Collections.Generic.Queue(Of Char)) As System.Collections.Generic.Queue(Of Char)
        MyQueue.Dequeue()
        If (HoldAlt = False) Then
            SendKeyboardInputVK(VirtualKeys.VK_MENU, True)
            If (MyQueue.Count = 0) Then
                SendKeyboardInputVK(VirtualKeys.VK_MENU, False)
                Return MyQueue
            End If
            If (MyQueue.Peek() = "(") Then
                HoldAlt = True
                MyQueue.Dequeue()
                'hold down til ")"
            Else
                If (MyQueue.Peek() = "{") Then
                    MyQueue = HandleBracket(MyQueue, keys)
                    SendKeyboardInputVK(VirtualKeys.VK_MENU, False)
                    Return MyQueue
                Else
                    If (IsSpecialChar(MyQueue.Peek()) = False) Then
                        SendUnicodeKeyboardInput(MyQueue.Peek(), True)
                        SendUnicodeKeyboardInput(MyQueue.Dequeue(), False)
                        SendKeyboardInputVK(VirtualKeys.VK_MENU, False)
                    Else
                        'handle special chars
                        If (MyQueue.Peek() = "^") Then 'Ctrl
                            MyQueue.Dequeue() 'ctrl will be handled here...
                            If (MyQueue.Count = 0 Or MyQueue.Peek() <> "(") Then
                                SendKeyboardInputVK(VirtualKeys.VK_CONTROL)
                                SendKeyboardInputVK(VirtualKeys.VK_MENU, False)
                                Return MyQueue
                            Else
                                HoldCtrl = True
                                SendKeyboardInputVK(VirtualKeys.VK_CONTROL, True)
                                SendKeyboardInputVK(VirtualKeys.VK_MENU, False)
                                Return MyQueue
                            End If
                        End If
                        If (MyQueue.Peek() = "*") Then 'Windows
                            MyQueue.Dequeue() 'Windows will be handled here...
                            If (MyQueue.Count = 0 Or MyQueue.Peek() <> "(") Then
                                SendKeyboardInputVK(VirtualKeys.VK_LWIN)
                                SendKeyboardInputVK(VirtualKeys.VK_MENU, False)
                                Return MyQueue
                            Else
                                HoldLWin = True
                                SendKeyboardInputVK(VirtualKeys.VK_LWIN, True)
                                SendKeyboardInputVK(VirtualKeys.VK_MENU, False)
                                Return MyQueue
                            End If
                        End If
                        If (MyQueue.Peek() = "+") Then 'Shift
                            MyQueue.Dequeue() 'Shift will be handled here...
                            If (MyQueue.Count = 0 Or MyQueue.Peek() <> "(") Then
                                SendKeyboardInputVK(VirtualKeys.VK_SHIFT)
                                SendKeyboardInputVK(VirtualKeys.VK_MENU, False)
                                Return MyQueue
                            Else
                                HoldShift = True
                                SendKeyboardInputVK(VirtualKeys.VK_SHIFT, True)
                                SendKeyboardInputVK(VirtualKeys.VK_MENU, False)
                                Return MyQueue
                            End If
                        End If
                        If (MyQueue.Peek() = "~") Then 'Enter
                            MyQueue.Dequeue() 'Enter will be handled here...
                            SendKeyboardInputVK(VirtualKeys.VK_RETURN)
                            SendKeyboardInputVK(VirtualKeys.VK_MENU, False)
                            Return MyQueue
                        End If
                    End If
                End If
            End If
        End If
        Return MyQueue
    End Function

    Private Shared Function Shift(ByVal keys As String, ByVal MyQueue As System.Collections.Generic.Queue(Of Char)) As System.Collections.Generic.Queue(Of Char)
        MyQueue.Dequeue()
        If (HoldShift = False) Then
            SendKeyboardInputVK(VirtualKeys.VK_SHIFT, True)
            If (MyQueue.Count = 0) Then
                SendKeyboardInputVK(VirtualKeys.VK_SHIFT, False)
                Return MyQueue
            End If
            If (MyQueue.Peek() = "(") Then
                HoldShift = True
                MyQueue.Dequeue()
                'hold down til ")"
            Else
                If (MyQueue.Peek() = "{") Then
                    MyQueue = HandleBracket(MyQueue, keys)
                    SendKeyboardInputVK(VirtualKeys.VK_SHIFT, False)
                    Return MyQueue
                Else
                    If (IsSpecialChar(MyQueue.Peek()) = False) Then
                        SendUnicodeKeyboardInput(MyQueue.Peek(), True)
                        SendUnicodeKeyboardInput(MyQueue.Dequeue(), False)
                        SendKeyboardInputVK(VirtualKeys.VK_SHIFT, False)
                    Else
                        'handle special chars
                        If (MyQueue.Peek() = "^") Then 'Ctrl
                            MyQueue.Dequeue() 'ctrl will be handled here...
                            If (MyQueue.Count = 0 Or MyQueue.Peek() <> "(") Then
                                SendKeyboardInputVK(VirtualKeys.VK_CONTROL)
                                SendKeyboardInputVK(VirtualKeys.VK_SHIFT, False)
                                Return MyQueue
                            Else
                                HoldCtrl = True
                                SendKeyboardInputVK(VirtualKeys.VK_CONTROL, True)
                                SendKeyboardInputVK(VirtualKeys.VK_SHIFT, False)
                                Return MyQueue
                            End If
                        End If
                        If (MyQueue.Peek() = "%") Then 'Alt
                            MyQueue.Dequeue() 'Alt will be handled here...
                            If (MyQueue.Count = 0 Or MyQueue.Peek() <> "(") Then
                                SendKeyboardInputVK(VirtualKeys.VK_MENU)
                                SendKeyboardInputVK(VirtualKeys.VK_SHIFT, False)
                                Return MyQueue
                            Else
                                HoldAlt = True
                                SendKeyboardInputVK(VirtualKeys.VK_MENU, True)
                                SendKeyboardInputVK(VirtualKeys.VK_SHIFT, False)
                                Return MyQueue
                            End If
                        End If
                        If (MyQueue.Peek() = "*") Then 'Windows
                            MyQueue.Dequeue() 'Windows will be handled here...
                            If (MyQueue.Count = 0 Or MyQueue.Peek() <> "(") Then
                                SendKeyboardInputVK(VirtualKeys.VK_LWIN)
                                SendKeyboardInputVK(VirtualKeys.VK_SHIFT, False)
                                Return MyQueue
                            Else
                                HoldLWin = True
                                SendKeyboardInputVK(VirtualKeys.VK_LWIN, True)
                                SendKeyboardInputVK(VirtualKeys.VK_SHIFT, False)
                                Return MyQueue
                            End If
                        End If
                        If (MyQueue.Peek() = "~") Then 'Enter
                            MyQueue.Dequeue() 'Enter will be handled here...
                            SendKeyboardInputVK(VirtualKeys.VK_RETURN)
                            SendKeyboardInputVK(VirtualKeys.VK_SHIFT, False)
                            Return MyQueue
                        End If
                    End If
                End If
            End If
        End If
        Return MyQueue
    End Function

    Private Shared Function IsSpecialChar(ByVal c As Char) As Boolean
        Select Case c.ToString()
            Case "+"
            Case "^"
            Case "%"
            Case "~"
            Case "*"
            Case Else
                Return False
        End Select
        Return True
    End Function

    Public Shared Sub SendKeys(ByVal keys As String)
        MyQueue = New System.Collections.Generic.Queue(Of Char)
        For Each c As Char In keys.ToCharArray()
            MyQueue.Enqueue(c)
        Next
        HoldShift = False
        HoldAlt = False
        HoldCtrl = False
        Try
            SendKeys(keys, MyQueue)
        Catch ex As ArgumentException
            Try
                System.Windows.Forms.SendKeys.Flush()
                If (HoldShift = True) Then
                    SendKeyboardInputVK(VirtualKeys.VK_SHIFT, False)
                End If
                If (HoldCtrl = True) Then
                    SendKeyboardInputVK(VirtualKeys.VK_CONTROL, False)
                End If
                If (HoldAlt = True) Then
                    SendKeyboardInputVK(VirtualKeys.VK_MENU, False)
                End If
                If (HoldLWin = True) Then
                    SendKeyboardInputVK(VirtualKeys.VK_LWIN, False)
                End If
                System.Windows.Forms.SendKeys.Flush()
            Catch ex2 As Exception
                System.Windows.Forms.SendKeys.Flush()
                Dim ex3 As New SlickTestAPIException("Unable to shut off Ctrl or Alt or Shift Key.", ex2)
                Throw ex3
            End Try
            Throw ex
        Catch ex1 As Exception
            Try
                If (HoldShift = True) Then
                    SendKeyboardInputVK(VirtualKeys.VK_SHIFT, False)
                End If
                If (HoldCtrl = True) Then
                    SendKeyboardInputVK(VirtualKeys.VK_CONTROL, False)
                End If
                If (HoldAlt = True) Then
                    SendKeyboardInputVK(VirtualKeys.VK_MENU, False)
                End If
                If (HoldLWin = True) Then
                    SendKeyboardInputVK(VirtualKeys.VK_LWIN, False)
                End If
                System.Windows.Forms.SendKeys.Flush()
            Catch ex2 As Exception
                System.Windows.Forms.SendKeys.Flush()
                Dim ex3 As New SlickTestAPIException("Unable to shut off Ctrl or Alt or Shift Key.", ex2)
                Throw ex3
            End Try
            Throw ex1
        End Try
    End Sub

    Public Shared Sub SendUnicodeKeyboardInput(ByVal key As Char, ByVal press As Boolean)
        If (AscW(key) > Byte.MaxValue) Then 'hopefully this intellegently sorts out unicode and non-unicode characters.
            Dim ki As New INPUT()
            ki.type = INPUT_KEYBOARD

            ki.union.keyboardInput.wVk = CShort(0)
            ki.union.keyboardInput.wScan = CShort(AscW(key))
            If (press) Then
                ki.union.keyboardInput.dwFlags = KEYEVENTF_UNICODE Or 0
            Else
                ki.union.keyboardInput.dwFlags = KEYEVENTF_UNICODE Or WinAPI.API.KEYEVENTF_KEYUP
            End If
            'ki.union.keyboardInput.dwFlags = KEYEVENTF_UNICODE Or (IIf(press, 0, WinAPI.API.KEYEVENTF_KEYUP))
            ki.union.keyboardInput.time = 0
            ki.union.keyboardInput.dwExtraInfo = New IntPtr(0)
            If 0 = SendInput(1, ki, Runtime.InteropServices.Marshal.SizeOf(ki)) Then
                Throw New SlickTestAPIException("Send Keys Error: " & Runtime.InteropServices.Marshal.GetLastWin32Error())
            End If
        Else
            If (HoldLWin = True OrElse HoldRWin = True) Then
                'SendKeyboardInputVK(CShort(AscW(key)), press) 'this is required to do things like 'windows key + d'
                SendKeyboardInputVK(CByte(AscW(key)), press) 'this is required to do things like 'windows key + d'
            Else
                Dim ki As New INPUT()
                ki.type = INPUT_KEYBOARD

                ki.union.keyboardInput.wVk = CShort(0)
                ki.union.keyboardInput.wScan = CShort(AscW(key))
                If (press) Then
                    ki.union.keyboardInput.dwFlags = KEYEVENTF_UNICODE Or 0
                Else
                    ki.union.keyboardInput.dwFlags = KEYEVENTF_UNICODE Or WinAPI.API.KEYEVENTF_KEYUP
                End If
                'ki.union.keyboardInput.dwFlags = KEYEVENTF_UNICODE Or (IIf(press, 0, WinAPI.API.KEYEVENTF_KEYUP))
                ki.union.keyboardInput.time = 0
                ki.union.keyboardInput.dwExtraInfo = New IntPtr(0)
                If 0 = SendInput(1, ki, Runtime.InteropServices.Marshal.SizeOf(ki)) Then
                    Throw New SlickTestAPIException("Send Keys Error: " & Runtime.InteropServices.Marshal.GetLastWin32Error())
                End If
            End If
        End If
    End Sub

    ''' <summary>
    ''' Injects a string of Unicode characters using simulated keyboard input
    ''' It should be noted that this overload just sends the whole string
    ''' with no pauses, depending on the recieving applications input processing
    ''' it may not be able to keep up with the speed, resulting in corruption or
    ''' loss of the input data.
    ''' </summary>
    ''' <param name="Data">The unicode string to be sent</param>
    Public Shared Sub SendUnicodeString(ByVal Data As String)
        InternalSendUnicodeString(data, -1, 0)
    End Sub

    ''' <summary>
    ''' Injects a string of Unicode characters using simulated keyboard input
    ''' with user defined timing.
    ''' </summary>
    ''' <param name="Data">The unicode string to be sent</param>
    ''' <param name="KeysToPressBetweenSleeps">How many characters to send between sleep calls</param>
    ''' <param name="SleepLength">How long, in milliseconds, to sleep for at each sleep call</param>
    Public Shared Sub SendUnicodeString(ByVal Data As String, ByVal KeysToPressBetweenSleeps As Integer, ByVal SleepLength As Integer)
        If KeysToPressBetweenSleeps < 1 Then
            Throw New ArgumentOutOfRangeException("The KeysToPressBetweenSleeps is < 1")
        End If

        If SleepLength < 0 Then
            Throw New ArgumentOutOfRangeException("The SleepLength is < 0")
        End If

        InternalSendUnicodeString(Data, KeysToPressBetweenSleeps, SleepLength)
    End Sub

    Friend Shared Sub SendKeyboardInputVK(ByVal vk As Byte, ByVal press As Boolean)
        'Newer keyboard system doesn't seem to support start button nearly as well.
        Dim ki As New INPUT()
        ki.type = INPUT_KEYBOARD
        ki.union.keyboardInput.wVk = vk
        ki.union.keyboardInput.wScan = 0

        Dim dwFlags As Integer = 0
        If (ki.union.keyboardInput.wScan > 0) Then
            dwFlags = dwFlags Or KEYEVENTF_SCANCODE
        End If
        If (Not press) Then
            dwFlags = dwFlags Or KEYEVENTF_KEYUP
        End If


        If (IsExtendedKey(vk)) Then
            'Console.WriteLine("VK " & vk & " IS an extended char.")
            ki.union.keyboardInput.dwFlags = dwFlags Or KEYEVENTF_EXTENDEDKEY
        Else
            'Console.WriteLine("VK " & vk & " IS NOT an extended char.")
            ki.union.keyboardInput.dwFlags = dwFlags
        End If
        ki.union.keyboardInput.time = 0
        ki.union.keyboardInput.dwExtraInfo = New IntPtr(0)
        If 0 = SendInput(1, ki, Runtime.InteropServices.Marshal.SizeOf(ki)) Then
            Throw New SlickTestAPIException("Send Keys Error: " & Runtime.InteropServices.Marshal.GetLastWin32Error())
        End If


    End Sub

    Friend Shared Function IsExtendedKey(ByVal key As Byte) As Boolean

        Return key = VirtualKeys.VK_MENU OrElse _
        key = VirtualKeys.VK_NUMBERLOCK OrElse _
        key = VirtualKeys.VK_INSERT OrElse key = VirtualKeys.VK_DELETE OrElse key = VirtualKeys.VK_HOME OrElse _
        key = VirtualKeys.VK_END OrElse key = VirtualKeys.VK_PRIOR OrElse key = VirtualKeys.VK_NEXT OrElse _
        key = VirtualKeys.VK_UP OrElse key = VirtualKeys.VK_DOWN OrElse key = VirtualKeys.VK_LEFT OrElse key = VirtualKeys.VK_RIGHT _
        OrElse key = VirtualKeys.VK_APPS OrElse key = VirtualKeys.VK_RWIN OrElse key = VirtualKeys.VK_LWIN _
        OrElse key = VirtualKeys.VK_RCONTROL OrElse key = VirtualKeys.VK_CONTROL OrElse _
        key = VirtualKeys.VK_LCONTROL


    End Function

    Friend Shared Sub SendKeyboardInputVK(ByVal vk As Byte)
        SendKeyboardInputVK(vk, True)
        System.Threading.Thread.Sleep(20)
        SendKeyboardInputVK(vk, False)
    End Sub

    ' Injects a string of Unicode characters using simulated keyboard input
    ' with user defined timing
    ' <param name="data">The unicode string to be sent</param>
    ' <param name="sleepFrequency">How many characters to send between sleep calls
    ' A sleepFrequency of -1 means to never sleep</param>
    ' <param name="sleepLength">How long, in milliseconds, to sleep for at each sleep call</param>
    Private Shared Sub InternalSendUnicodeString(ByVal data As String, ByVal sleepFrequency As Integer, ByVal sleepLength As Integer)
        Dim chardata As Char() = data.ToCharArray()
        Dim counter As Integer = -1

        For Each c As Char In chardata
            ' Every sleepFrequency characters, sleep for sleepLength ms to avoid overflowing the input buffer.
            counter += 1
            If counter > sleepFrequency Then
                counter = 0
                System.Threading.Thread.Sleep(sleepLength)
            End If

            SendUnicodeKeyboardInput(c, True)
            SendUnicodeKeyboardInput(c, False)
        Next
    End Sub


    Public Shared Sub SendChars(ByVal keys() As Char, ByVal hwnd As IntPtr)
        If (hwnd = IntPtr.Zero) Then
            Throw New SlickTestAPIException("Invalid hwnd provided.  hwnd is zero.")
        End If
        If (Not WinAPI.API.IsWindow(hwnd)) Then
            Throw New SlickTestAPIException("Invalid hwnd provided.  hwnd is not a window object: " & hwnd.ToString())
        End If
        For Each c As Char In keys
            NativeMethods.SendMessage(hwnd, WinAPI.API.WM_CHAR, New IntPtr(Convert.ToInt64(c)), IntPtr.Zero)
            System.Threading.Thread.Sleep(20)
        Next
    End Sub

    Public Shared Sub SendChars(ByVal keys As String, ByVal hwnd As IntPtr)
        If (hwnd = IntPtr.Zero) Then
            Throw New SlickTestAPIException("Invalid hwnd provided.  hwnd is zero.")
        End If
        If (Not WinAPI.API.IsWindow(hwnd)) Then
            Throw New SlickTestAPIException("Invalid hwnd provided.  hwnd is not a window object: " & hwnd.ToString())
        End If
        For Each c As Char In keys
            NativeMethods.SendMessage(hwnd, WinAPI.API.WM_CHAR, New IntPtr(Convert.ToInt64(c)), IntPtr.Zero)
            System.Threading.Thread.Sleep(20)
        Next

    End Sub

End Class
