Imports winAPI.API
''' <summary>
''' Gives the user direct control over the mouse, but very
''' little control based upon objects.
''' </summary>
''' <remarks></remarks>
Friend Class InternalMouse

    Protected Friend Structure POINTAPI
        Public X As Int16 'Long
        Public Y As Int16 'Long
    End Structure

    Protected Friend Structure WINDOWPLACEMENT
        Dim Length As Long
        Dim flags As Long
        Dim showCmd As Long
        Dim ptMinPosition As POINTAPI
        Dim ptMaxPosition As POINTAPI
        Dim rcNormalPosition As RECT
    End Structure

    Protected Friend Structure RECT
        Public Left As Int32
        Public Top As Int32
        Public Right As Int32
        Public Bottom As Int32
    End Structure

    Protected Friend Declare Function GetCursorPos Lib "user32" (ByVal lpPoint As POINTAPI) As Long
    '/////////////////////////////////////////////////
    Private order As Integer
    Private ClassName As String
    Private hWnd As IntPtr
    Protected Friend Shared isMouseRightDown As Boolean = False
    Protected Friend Shared isMouseLeftDown As Boolean = False
    Protected Friend Shared isMouseMiddleDown As Boolean = False

    ''' <summary>
    ''' Returns the absolute X value of the current mouse position.
    ''' </summary>
    ''' <returns>An absolute value, between 0 and 65535.</returns>
    ''' <remarks></remarks>
    Public Shared Function CurrentAbsX() As Integer
        CurrentAbsX = RelativeToAbsCoordX(CurrentX)
    End Function

    ''' <summary>
    ''' Returns the absolute Y value of the current mouse position.
    ''' </summary>
    ''' <returns>An absolute value, between 0 and 65535.</returns>
    ''' <remarks></remarks>
    Public Shared Function CurrentAbsY() As Integer
        CurrentAbsY = RelativeToAbsCoordY(CurrentY)
    End Function


    ''' <summary>
    ''' Returns the relative X value of the current mouse position.
    ''' </summary>
    ''' <returns>A cord which is a relative value (relative to the resolution)</returns>
    ''' <remarks></remarks>
    Public Shared Function CurrentX() As Integer
        CurrentX = System.Windows.Forms.Cursor.Position.X
    End Function

    ''' <summary>
    ''' Returns the relative Y value of the current mouse position.
    ''' </summary>
    ''' <returns>A cord which is a relative value (relative to the resolution)</returns>
    ''' <remarks></remarks>
    Public Shared Function CurrentY() As Integer
        CurrentY = System.Windows.Forms.Cursor.Position.Y
    End Function

    Public Shared Function AbsToRelativeCoordY(ByVal Coord As Integer, Optional ByVal ScreenHeight As Integer = -1) As Integer
        Dim screen As Integer
        If (ScreenHeight = -1) Then
            screen = System.Windows.Forms.Screen.PrimaryScreen.Bounds.Height
        Else
            screen = ScreenHeight
        End If
        Return Convert.ToInt32((Coord / 65535) * screen)
    End Function

    Public Shared Function RelativeToAbsCoordY(ByVal Coord As Integer, Optional ByVal ScreenHeight As Integer = -1) As Integer
        Dim screen As Integer
        If (ScreenHeight = -1) Then
            screen = System.Windows.Forms.Screen.PrimaryScreen.Bounds.Height
        Else
            screen = ScreenHeight
        End If
        Return Convert.ToInt32((65535 * Coord) / screen)
    End Function

    Public Shared Function AbsToRelativeCoordX(ByVal Coord As Integer, Optional ByVal ScreenWidth As Integer = -1) As Integer
        Dim screen As Integer
        If (ScreenWidth = -1) Then
            screen = System.Windows.Forms.Screen.PrimaryScreen.Bounds.Width
        Else
            screen = ScreenWidth
        End If
        Return Convert.ToInt32((Coord / 65535) * screen)
    End Function

    Public Shared Function RelativeToAbsCoordX(ByVal Coord As Integer, Optional ByVal ScreenWidth As Integer = -1) As Integer
        Dim screen As Integer
        If (ScreenWidth = -1) Then
            screen = System.Windows.Forms.Screen.PrimaryScreen.Bounds.Width
        Else
            screen = ScreenWidth
        End If
        Return Convert.ToInt32((65535 * Coord) / screen)
    End Function

    ''' <summary>
    ''' Clicks on a Hwnd value, if the Hwnd value is greater than 0.
    ''' </summary>
    ''' <param name="HWnd">the Windows Hwnd value.</param>
    ''' <returns>returns true if successful.</returns>
    ''' <remarks>Some applications are known to not work well (such as winamp) with this
    ''' "smart" method of clicking.</remarks>
    Protected Friend Shared Function ClickByHwnd(ByVal HWnd As IntPtr) As Boolean
        If (HWnd.ToInt64() <= 0) Then
            Return False
        End If
        'SendMessage(HWnd, WM_LBUTTONDOWN, 0, IntPtr.Zero)
        ' System.Threading.Thread.Sleep(200)
        'send the left mouse button "up" message to the button... 
        'SendMessage(HWnd, WM_LBUTTONUP, 0, IntPtr.Zero)
        'System.Threading.Thread.Sleep(200)
        'send the button state message to the button, telling it to handle its events... 
        'SendMessage(HWnd, BM_SETSTATE, 1, IntPtr.Zero)
        Try
            SendMessage(HWnd, BM_CLICK, 0, IntPtr.Zero)
        Catch ex As Exception
            Return False
        End Try
        Return True
    End Function

    ''' <summary>
    ''' Use the left mouse button to click at a certain X/Y location.
    ''' </summary>
    ''' <param name="X">The X location.</param>
    ''' <param name="Y">The Y location.</param>
    ''' <remarks>I believe the X/Y cords are relative???</remarks>
    Protected Friend Shared Sub LeftClickXY(ByVal X As Integer, ByVal Y As Integer)
        GotoXY(X, Y)
        LeftClick()
    End Sub

    
    Protected Friend Shared Sub DragAndDrop(ByVal StartPoint As Drawing.Point, ByVal EndPoint As Drawing.Point)
        GotoXY(StartPoint.X, StartPoint.Y)
        LeftDown()
        System.Threading.Thread.Sleep(200)
        GotoXY(EndPoint.X, EndPoint.Y)
        LeftUp()
    End Sub

    Protected Friend Shared Sub DragAndDropMiddle(ByVal StartPoint As Drawing.Point, ByVal EndPoint As Drawing.Point)
        GotoXY(StartPoint.X, StartPoint.Y)
        RightDown()
        System.Threading.Thread.Sleep(200)
        GotoXY(EndPoint.X, EndPoint.Y)
        RightUp()
    End Sub

    Protected Friend Shared Sub DragAndDropRight(ByVal StartPoint As Drawing.Point, ByVal EndPoint As Drawing.Point)
        GotoXY(StartPoint.X, StartPoint.Y)
        MiddleDown()
        System.Threading.Thread.Sleep(200)
        GotoXY(EndPoint.X, EndPoint.Y)
        MiddleUp()
    End Sub

    ''' <summary>
    ''' Use the right mouse button to click at a certain X/Y location.
    ''' </summary>
    ''' <param name="X">The X location.</param>
    ''' <param name="Y">The Y location.</param>
    ''' <remarks>I believe the X/Y cords are relative???</remarks>
    Protected Friend Shared Sub RightClickXY(ByVal X As Integer, ByVal Y As Integer)
        GotoXY(X, Y)
        RightClick()
    End Sub

    ''' <summary>
    ''' Use the middle mouse button to click at a certain X/Y location.
    ''' </summary>
    ''' <param name="X">The X location.</param>
    ''' <param name="Y">The Y location.</param>
    ''' <remarks>I believe the X/Y cords are relative???</remarks>
    Protected Friend Shared Sub MiddleClickXY(ByVal X As Integer, ByVal Y As Integer)
        GotoXY(X, Y)
        MiddleClick()
    End Sub

    ''' <summary>
    ''' Moves the mouse to a certain X/Y location
    ''' </summary>
    ''' <param name="X">The X location.</param>
    ''' <param name="Y">The Y location.</param>
    ''' <remarks>I believe the X/Y cords are relative???</remarks>
    Public Shared Sub GotoXY(ByVal X As Integer, ByVal Y As Integer)
        System.Windows.Forms.Cursor.Position = New System.Drawing.Point(X, Y)
    End Sub

    ''' <summary>
    ''' Presses the left mouse button down and then up at its current
    ''' location.
    ''' </summary>
    ''' <remarks></remarks>
    Protected Friend Shared Sub LeftClick()
        LeftDown()
        LeftUp()
    End Sub

    ''' <summary>
    ''' Presses the left mouse button down at its current
    ''' location.
    ''' </summary>
    ''' <remarks></remarks>
    Protected Friend Shared Sub LeftDown()
        isMouseLeftDown = True
        mouse_event(MOUSEEVENTF.LEFTDOWN, 0, 0, 0, 0)
    End Sub


    ''' <summary>
    ''' Lift's the left mouse button up at its current
    ''' location.
    ''' </summary>
    ''' <remarks></remarks>
    Protected Friend Shared Sub LeftUp()
        isMouseLeftDown = False
        mouse_event(MOUSEEVENTF.LEFTUP, 0, 0, 0, 0)
    End Sub

    ''' <summary>
    ''' Presses the middle mouse button down and then up at its current
    ''' location.
    ''' </summary>
    ''' <remarks></remarks>
    Protected Friend Shared Sub MiddleClick()
        MiddleDown()
        MiddleUp()
    End Sub

    ''' <summary>
    ''' Presses the middle mouse button down at its current
    ''' location.
    ''' </summary>
    ''' <remarks></remarks>
    Protected Friend Shared Sub MiddleDown()
        isMouseMiddleDown = True
        mouse_event(MOUSEEVENTF.MIDDLEDOWN, 0, 0, 0, 0)
    End Sub

    ''' <summary>
    ''' Lift's the middle mouse button up at its current
    ''' location.
    ''' </summary>
    ''' <remarks></remarks>
    Protected Friend Shared Sub MiddleUp()
        isMouseMiddleDown = False
        mouse_event(MOUSEEVENTF.MIDDLEUP, 0, 0, 0, 0)
    End Sub

    ''' <summary>
    ''' Moves the mouse to a new location
    ''' </summary>
    ''' <param name="xMove">An absolute value, between 0 and 65535</param>
    ''' <param name="yMove">An absolute value, between 0 and 65535</param>
    ''' <remarks></remarks>
    Public Shared Sub MoveMouse(ByVal xMove As Integer, ByVal yMove As Integer)
        mouse_event(MOUSEEVENTF.MOVE Or MOUSEEVENTF.ABSOLUTE, xMove, yMove, 0, 0)
    End Sub
    ''' <summary>
    ''' Presses the right mouse button down and then up at its current
    ''' location.
    ''' </summary>
    ''' <remarks></remarks>
    Protected Friend Shared Sub RightClick()
        RightDown()
        RightUp()
    End Sub

    ''' <summary>
    ''' Presses the right mouse button down at its current
    ''' location.
    ''' </summary>
    ''' <remarks></remarks>
    Protected Friend Shared Sub RightDown()
        isMouseRightDown = True
        mouse_event(MOUSEEVENTF.RIGHTDOWN, 0, 0, 0, 0)
    End Sub

    ''' <summary>
    ''' Lift's the right mouse button up at its current
    ''' location.
    ''' </summary>
    ''' <remarks></remarks>
    Protected Friend Shared Sub RightUp()
        isMouseRightDown = False
        mouse_event(MOUSEEVENTF.RIGHTUP, 0, 0, 0, 0)
    End Sub


    Private Enum MOUSEEVENTF
        MOVE = 1 '; /* mouse move */
        LEFTDOWN = 2 '; /* left button down */
        LEFTUP = 4 '; /* left button up */
        RIGHTDOWN = &H8 '; /* right button down */
        RIGHTUP = &H10 '; /* right button up */
        MIDDLEDOWN = &H20 '; /* middle button down */
        MIDDLEUP = &H40 '; /* middle button up */
        XDOWN = &H80 '; /* x button down */
        XUP = &H100 ' ; /* x button down */
        WHEEL = &H800 ' ; /* wheel button rolled */
        VIRTUALDESK = &H4000 '; /* map to entire virtual desktop */
        ABSOLUTE = &H8000 '; /* absolute move */
    End Enum

    'Public Structure MOUSEINPUT
    '    Public dx As Integer
    '    Public dy As Integer
    '    Public mouseData As Integer
    '    Public dwFlags As Integer
    '    Public time As Integer
    '    Public dwExtraInfo As IntPtr
    'End Structure

    'Public Structure KEYBDINPUT
    '    Public wVk As Short ' according to msdn, these are word (short)
    '    Public wScan As Short
    '    Public dwFlags As Integer
    '    Public time As Integer
    '    Public dwExtraInfo As IntPtr
    'End Structure

    'Public Structure HARDWAREINPUT
    '    Public uMsg As Integer
    '    Public wParamL As Short
    '    Public wParamH As Short
    'End Structure

    '<Runtime.InteropServices.StructLayout(Runtime.InteropServices.LayoutKind.Explicit)> _
    'Public Structure INPUT_UNION
    '    <Runtime.InteropServices.FieldOffset(0)> _
    '    Public mi As MOUSEINPUT
    '    <Runtime.InteropServices.FieldOffset(0)> _
    '    Public ki As KEYBDINPUT
    '    <Runtime.InteropServices.FieldOffset(0)> _
    '    Public hi As HARDWAREINPUT
    'End Structure

    'Public Structure INPUT_TYPE
    '    Public dwType As Integer
    '    Public union As INPUT_UNION
    'End Structure

    'Public Declare Function SendInput Lib "user32.dll" _
    '(ByVal nInputs As Integer, _
    'ByRef pInputs As INPUT_TYPE, _
    'ByVal cbSize As Integer) As Integer

    ''Then... You can use it like this:

    'Public Sub PressKey(ByVal KeyCode As Short)
    '    'special handler for mouse buttons
    '    Select Case KeyCode
    '        Case vbKeyRButton
    '            PressMouseButton(MOUSEEVENTF_RIGHTDOWN)
    '        Case vbKeyLButton
    '            PressMouseButton(MOUSEEVENTF_LEFTDOWN)
    '        Case vbKeyMButton
    '            PressMouseButton(MOUSEEVENTF_MIDDLEDOWN)
    '        Case Else 'now do regular keyboard things
    '            Dim InputEvent As INPUT_TYPE ' holds information about each event

    '            ' Press the key
    '            With InputEvent
    '                .dwType = INPUT_KEYBOARD
    '                .union.ki.wVk = KeyCode ' the key to release
    '                .union.ki.wScan = 0 ' not needed
    '                .union.ki.dwFlags = 0 ' press the key down
    '                .union.ki.time = 0 ' use the default
    '                .union.ki.dwExtraInfo = New IntPtr(0) ' not needed
    '            End With

    '            ' Now that all the information for the two input events has been placed
    '            ' into the array, finally send it into the input stream.
    '            Dim X As Integer
    '            X = SendInput(1, InputEvents(0), Len(InputEvents(0)))

    '    End Select
    'End Sub


    '////////////////////////////////////////////////////////

    ' Declare Function SendInput Lib "user32.dll" (ByVal cInputs As Integer, ByRef pInputs As Input, ByVal cbSize As Integer) As Integer

    ''<StructLayout(LayoutKind.Explicit)> _
    ' Structure INPUT
    '    Dim dwType As Integer
    '    Dim mouseInput As mouseInput
    '    Dim keyboardInput As KEYBDINPUT
    '    Dim hardwareInput As hardwareInput
    'End Structure

    ''<StructLayout(LayoutKind.Explicit)> _
    ' Structure KEYBDINPUT
    '    '<FieldOffset(0)> 
    '    Public wVk As Short
    '    '<FieldOffset(2)> 
    '    Public wScan As Short
    '    '<FieldOffset(4)> 
    '    Public dwFlags As Integer
    '    '<FieldOffset(8)> 
    '    Public time As Integer
    '    '<FieldOffset(12)> 
    '    Public dwExtraInfo As IntPtr
    'End Structure

    ''<StructLayout(LayoutKind.Explicit)> _
    ' Structure HARDWAREINPUT
    '    '<FieldOffset(0)> 
    '    Public uMsg As Integer
    '    '<FieldOffset(4)> 
    '    Public wParamL As Short
    '    '<FieldOffset(6)>
    '    Public wParamH As Short
    'End Structure

    ''<StructLayout(LayoutKind.Explicit)> _
    ' Structure MOUSEINPUT
    '    '<FieldOffset(0)>
    '    Public dx As Integer
    '    '<FieldOffset(4)> 
    '    Public dy As Integer
    '    '<FieldOffset(8)> 
    '    Public mouseData As Integer
    '    '<FieldOffset(12)> 
    '    Public dwFlags As Integer
    '    '<FieldOffset(16)> 
    '    Public time As Integer
    '    '<FieldOffset(20)> 
    '    Public dwExtraInfo As IntPtr
    'End Structure
    'Const INPUT_MOUSE As Integer = 0
    'Const INPUT_KEYBOARD As Integer = 1
    'Const INPUT_HARDWARE As Integer = 2

    'Function AbsoluteClickXY(ByVal X As Integer, ByVal Y As Integer, ByVal Button As Integer)
    '    DoMouse(NativeMethods.MOUSEEVENTF.LEFTDOWN, New System.Drawing.Point(0, 0))
    '    'System.Windows.Forms.Cursor.Position = New System.Drawing.Point(X, Y)
    'End Function

    ' Sub DoMouse(ByVal flags As NativeMethods.MOUSEEVENTF, ByVal newPoint As System.Drawing.Point)
    '    Dim input As New NativeMethods.INPUT
    '    Dim mi As New NativeMethods.MOUSEINPUT
    '    input.dwType = NativeMethods.InputType.Mouse
    '    input.mi = mi
    '    input.mi.dwExtraInfo = IntPtr.Zero
    '    ' mouse co-ords: top left is (0,0), bottom right is (65535, 65535)
    '    ' convert screen co-ord to mouse co-ords...

    '    input.mi.dx = newPoint.X * (65535 / System.Windows.Forms.Screen.PrimaryScreen.Bounds.Width)
    '    input.mi.dy = newPoint.Y * (65535 / System.Windows.Forms.Screen.PrimaryScreen.Bounds.Height)
    '    input.mi.time = 0
    '    input.mi.mouseData = 0  ' can be used for WHEEL event see msdn
    '    input.mi.dwFlags = flags

    '    Dim cbSize As Integer = System.Runtime.InteropServices.Marshal.SizeOf(GetType(NativeMethods.INPUT))
    '    Dim result As Integer = NativeMethods.SendInput(1, input, cbSize)
    '    If result = 0 Then Debug.WriteLine(System.Runtime.InteropServices.Marshal.GetLastWin32Error)
    'End Sub

End Class
