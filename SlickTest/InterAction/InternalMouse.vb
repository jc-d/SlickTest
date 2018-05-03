Imports System.Drawing
Imports WinAPI.API
Imports System.Runtime.InteropServices

''' <summary>
''' Gives the user direct control over the mouse, but very
''' little control based upon objects.
''' </summary>
''' <remarks></remarks>
Friend Class InternalMouse

#Region "Structures"
    Protected Friend Structure WINDOWPLACEMENT
        Dim Length As Long
        Dim flags As Long
        Dim showCmd As Long
        Dim ptMinPosition As POINTAPI
        Dim ptMaxPosition As POINTAPI
        Dim rcNormalPosition As RECT
    End Structure

    ''' <summary>
    ''' A RECT structure as required by the user32.dll api.
    ''' </summary>
    <Serializable(), StructLayout(LayoutKind.Sequential)> _
    Protected Friend Structure RECT
        Public Left As Integer
        Public Top As Integer
        Public Right As Integer
        Public Bottom As Integer

        Public Sub New(ByVal left_ As Integer, ByVal top_ As Integer, ByVal right_ As Integer, ByVal bottom_ As Integer)
            Left = left_
            Top = top_
            Right = right_
            Bottom = bottom_
        End Sub

        Public ReadOnly Property Height() As Integer
            Get
                Return Bottom - Top
            End Get
        End Property
        Public ReadOnly Property Width() As Integer
            Get
                Return Right - Left
            End Get
        End Property
        Public ReadOnly Property Size() As Size
            Get
                Return New Size(Width, Height)
            End Get
        End Property

        Public ReadOnly Property Location() As POINT
            Get
                Return New POINT(Left, Top)
            End Get
        End Property

        ' Handy method for converting to a System.Drawing.Rectangle
        Public Function ToRectangle() As Rectangle
            Return Rectangle.FromLTRB(Left, Top, Right, Bottom)
        End Function

        Public Shared Function FromRectangle(ByVal rectangle As Rectangle) As RECT
            Return New RECT(rectangle.Left, rectangle.Top, rectangle.Right, rectangle.Bottom)
        End Function

        Public Overloads Overrides Function GetHashCode() As Integer
            Return Left Xor ((Top << 13) Or (Top >> &H13)) Xor ((Width << &H1A) Or (Width >> 6)) Xor ((Height << 7) Or (Height >> &H19))
        End Function

#Region "Operator overloads"

        Public Shared Widening Operator CType(ByVal rect As RECT) As Rectangle
            Return rect.ToRectangle()
        End Operator

        Public Shared Widening Operator CType(ByVal rect As Rectangle) As RECT
            Return FromRectangle(rect)
        End Operator

#End Region
    End Structure

    ''' <summary>
    ''' A POINT structure as required by the user32.dll api.
    ''' </summary>
    <StructLayout(LayoutKind.Sequential)> _
    Public Structure POINT
        Public X As Integer
        Public Y As Integer

        Public Sub New(ByVal x As Integer, ByVal y As Integer)
            Me.X = x
            Me.Y = y
        End Sub

        Public Shared Widening Operator CType(ByVal p As POINT) As System.Drawing.Point
            Return New System.Drawing.Point(p.X, p.Y)
        End Operator

        Public Shared Widening Operator CType(ByVal p As System.Drawing.Point) As POINT
            Return New POINT(p.X, p.Y)
        End Operator
    End Structure

    Structure MOUSEINPUT
        Public dx As Integer
        Public dy As Integer
        Public mouseData As UInteger
        Public dwFlags As UInteger
        Public time As UInteger
        Public dwExtraInfo As IntPtr
    End Structure

    Structure KEYBDINPUT
        Public wVk As UShort
        Public wScan As UShort
        Public dwFlags As UInteger
        Public time As UInteger
        Public dwExtraInfo As IntPtr
    End Structure

    Structure HARDWAREINPUT
        Public uMsg As Integer
        Public wParamL As Short
        Public wParamH As Short
    End Structure

    <StructLayout(LayoutKind.Explicit)> _
    Structure MOUSEKEYBDHARDWAREINPUT
        <FieldOffset(0)> _
        Public mi As MOUSEINPUT

        <FieldOffset(0)> _
        Public ki As KEYBDINPUT

        <FieldOffset(0)> _
        Public hi As HARDWAREINPUT
    End Structure

    Structure INPUT
        Public type As Integer
        Public mkhi As MOUSEKEYBDHARDWAREINPUT
    End Structure

#End Region

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
    ''' <returns>Returns true if successful.</returns>
    ''' <remarks>Some applications are known to not work well (such as winamp) with this
    ''' "smart" method of clicking.</remarks>
    Protected Friend Shared Function ClickByHwnd(ByVal HWnd As IntPtr) As Boolean
        If (HWnd.ToInt64() <= 0) Then
            Return False
        End If
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


    Protected Friend Shared Sub DragAndDrop(ByVal StartPoint As Drawing.Point, ByVal EndPoint As Drawing.Point, ByVal Delay As Boolean)
        GotoXY(StartPoint.X, StartPoint.Y)
        LeftDown()
        System.Threading.Thread.Sleep(200)
        GotoXY(EndPoint.X, EndPoint.Y, Delay)
        LeftUp()
    End Sub

    Protected Friend Shared Sub DragAndDropMiddle(ByVal StartPoint As Drawing.Point, ByVal EndPoint As Drawing.Point, ByVal Delay As Boolean)
        GotoXY(StartPoint.X, StartPoint.Y)
        RightDown()
        System.Threading.Thread.Sleep(200)
        GotoXY(EndPoint.X, EndPoint.Y, Delay)
        RightUp()
    End Sub

    Protected Friend Shared Sub DragAndDropRight(ByVal StartPoint As Drawing.Point, ByVal EndPoint As Drawing.Point, ByVal Delay As Boolean)
        GotoXY(StartPoint.X, StartPoint.Y)
        MiddleDown()
        System.Threading.Thread.Sleep(200)
        GotoXY(EndPoint.X, EndPoint.Y, Delay)
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
    ''' <remarks>I believe the X/Y cords are relative.</remarks>
    Public Shared Sub GotoXY(ByVal X As Integer, ByVal Y As Integer, Optional ByVal IsDelayed As Boolean = False)
        'System.Windows.Forms.Cursor.Position = New System.Drawing.Point(X, Y)
        DoMouseMove(X, Y, IsDelayed)
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
    ''' <param name="xMove">A relative position</param>
    ''' <param name="yMove">A relative position</param>
    ''' <remarks></remarks>
    Private Shared Sub MoveMouse(ByVal xMove As Integer, ByVal yMove As Integer)
        'mouse_event(MOUSEEVENTF.MOVE Or MOUSEEVENTF.ABSOLUTE, xMove, yMove, 0, 0)
        Dim screenRect As New RECT()
        GetWindowRect(GetDesktopWindow(), screenRect)
        Dim oddOffset_height As Double = If((screenRect.Height Mod 4 = 0), 1.5, 2.0R)
        Dim oddOffset_width As Double = If((screenRect.Width Mod 4 = 0), 1.5, 2.0R)
        Dim mouseInput As INPUT() = New INPUT(0) {}
        mouseInput(0).type = INPUT_MOUSE
        mouseInput(0).mkhi.mi.dx = CInt((CDbl(xMove) * ((CDbl(SCREEN_BUFF) / screenRect.Width)) + oddOffset_width))
        mouseInput(0).mkhi.mi.dy = CInt(((CDbl(yMove) * (CDbl(SCREEN_BUFF) / screenRect.Height)) + oddOffset_height))
        mouseInput(0).mkhi.mi.mouseData = 0
        mouseInput(0).mkhi.mi.time = 0
        mouseInput(0).mkhi.mi.dwFlags = Convert.ToUInt32(MOUSEEVENTF.MOVE Or MOUSEEVENTF.ABSOLUTE)
        SendInput(1, mouseInput, Marshal.SizeOf(mouseInput(0)))
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

#Region "Taken From Andy Dopieralski"
    ''' <summary>
    ''' This is the int size of the Windows screen buffer for all screens (2^16).
    ''' </summary>
    Const SCREEN_BUFF As Long = 65535
    ' Input type. Mouse = 0, key = 1, other = 2.
    Const INPUT_MOUSE As Integer = 0
    Const INPUT_KEYBOARD As Integer = 1
    Const INPUT_HARDWARE As Integer = 2
    'Message to send to the buffer
    Const KEYEVENTF_EXTENDEDKEY As UInteger = &H1
    Const KEYEVENTF_KEYUP As UInteger = &H2
    Const KEYEVENTF_UNICODE As UInteger = &H4
    Const KEYEVENTF_SCANCODE As UInteger = &H8
    Const XBUTTON1 As UInteger = &H1
    Const XBUTTON2 As UInteger = &H2

    ' Move mouse to X, Y over time on the hypotenuse.
    Private Shared Sub DoMouseMove(ByVal X As Integer, ByVal Y As Integer, ByVal delay As Boolean)
        Dim currentPos As POINT
        GetCursorPos(currentPos)
        Dim moveTo As New POINT(X, Y)
        Dim run As Integer = Math.Abs(currentPos.X - moveTo.X)
        Dim rise As Integer = Math.Abs(currentPos.Y - moveTo.Y)
        Dim dragPathLength As Integer = Convert.ToInt32(Math.Sqrt(Math.Pow(run, 2) + Math.Pow(rise, 2)))
        If False = delay OrElse dragPathLength = 0 Then
            MoveMouse(moveTo.X, moveTo.Y)
        Else

            Dim counter As Integer = If((rise > run), rise, run)
            Dim starting As Integer = If((rise > run), currentPos.Y, currentPos.X)
            Dim xmodifier As Integer = If((currentPos.X < moveTo.X), 1, -1)
            Dim ymodifier As Integer = If((currentPos.Y < moveTo.Y), 1, -1)
            Dim slope As Double = (xmodifier * ymodifier) * CDbl(rise) / run
            Dim newX As Integer = currentPos.X
            Dim newY As Integer = currentPos.Y
            For i As Integer = 0 To counter

                If xmodifier * ymodifier > 0 Then
                    If rise > run Then
                        newX = currentPos.X + (xmodifier * CInt((CDbl(i) / slope)))
                        newY = currentPos.Y + (ymodifier * i)
                    Else
                        newX = currentPos.X + (xmodifier * i)
                        newY = currentPos.Y + (ymodifier * CInt((CDbl(i) * slope)))
                    End If
                Else
                    If rise > run Then
                        newX = currentPos.X - (xmodifier * CInt((CDbl(i) / slope)))
                        newY = currentPos.Y + (ymodifier * i)
                    Else
                        newX = currentPos.X + (xmodifier * i)
                        newY = currentPos.Y - (ymodifier * CInt((CDbl(i) * slope)))
                    End If
                End If
                MoveMouse(newX, newY)
                If delay Then
                    System.Threading.Thread.Sleep(1)
                End If
            Next

            GetCursorPos(currentPos)
            If currentPos.X <> moveTo.X OrElse currentPos.Y <> moveTo.Y Then
                MoveMouse(moveTo.X, moveTo.Y)
            End If
            GetCursorPos(currentPos)

            'Console.WriteLine(("Ending position: " & currentPos.X & ", ") + currentPos.Y)
        End If
    End Sub

#End Region

    <DllImport("user32.dll", SetLastError:=True, CharSet:=CharSet.Unicode)> _
    Public Shared Function SendInput(ByVal nInputs As UInteger, ByVal pInputs As INPUT(), ByVal cbSize As Integer) As UInteger
    End Function
    <DllImport("user32.dll")> _
    Public Shared Function GetCursorPos(ByRef lpPoint As POINT) As <MarshalAs(UnmanagedType.Bool)> Boolean
    End Function
    <DllImport("user32.dll", SetLastError:=True)> _
    Public Shared Function GetDesktopWindow() As IntPtr
    End Function
    <DllImport("user32.dll")> _
    Public Shared Function GetWindowRect(ByVal hwnd As IntPtr, ByRef lpRect As RECT) As Boolean
    End Function





End Class
