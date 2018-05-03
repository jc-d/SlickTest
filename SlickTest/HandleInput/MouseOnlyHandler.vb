Imports System.Windows.Forms

Public Class MouseOnlyHandler

    Protected mouseChecker As Integer
    Protected mouseX As Integer
    Protected mouseY As Integer
    Protected CurrentHwnd As IntPtr = IntPtr.Zero
    Public ObjectSpy As Boolean
    Public RecordXY As Boolean
    Public IsRecording As Boolean
    Public hwndOfApp As IntPtr
    Protected isHooked As Boolean
    Friend TopWindow As APIControls.TopWindow
    Private WindowsFunctions As New APIControls.IndependentWindowsFunctionsv1
    Private WithEvents _mouse_mgr As WindowsHookLib.LLMouseHook
    Public Event MouseAction(ByVal action As String, ByVal type As MouseActionType, ByVal x As Integer, ByVal y As Integer)
    Public Event ObjSpy(ByVal x As Integer, ByVal y As Integer, ByVal AltPressed As Boolean)
    Public Event MouseEvent(ByVal x As Integer, ByVal y As Integer, ByVal type As MouseActionType)

    Public ReadOnly Property IsSystemHooked() As Boolean
        Get
            Return isHooked
        End Get
    End Property

    Protected Overridable Sub RaiseEventMouseAction(ByVal action As String, ByVal type As MouseActionType, ByVal x As Integer, ByVal y As Integer)
        RaiseEvent MouseAction(action, type, x, y)
    End Sub

    Protected Overridable Sub RaiseEventObjSpy(ByVal x As Integer, ByVal y As Integer, ByVal AltPressed As Boolean)
        RaiseEvent ObjSpy(x, y, AltPressed)
    End Sub

    Protected Overridable ReadOnly Property ShiftState()
        Get
            Return False
        End Get
    End Property

    Protected Overridable ReadOnly Property CtrlState()
        Get
            Return False
        End Get
    End Property

    Protected Overridable ReadOnly Property AltState()
        Get
            Return False
        End Get
    End Property

    Public Enum MouseActionType
        ClickXY
        DoubleClickXY
        ClickObjL
        ClickObjR
        ClickTypeUnknown
        DoubleClickObjL
        DoubleClickObjR
        SendText
        SendTextObj
    End Enum

    Public Sub New()
        RecordXY = True
        TopWindow = New APIControls.TopWindow()
        IsRecording = False
        hwndOfApp = 0
        _mouse_mgr = New WindowsHookLib.LLMouseHook
        isHooked = False
    End Sub

    Public Overridable Sub StartHandlers()
        mouseChecker = 0
        isHooked = True
        Try
            _mouse_mgr.InstallHook()
        Catch ex As Exception
            System.Windows.Forms.MessageBox.Show("Unable to connect to the mouse!  Error data: " & ex.ToString())
        End Try
    End Sub

    Private Sub _mouse_mgr_MouseDoubleClick(ByVal sender As System.IntPtr, ByVal e As WindowsHookLib.LLMouseEventArgs) Handles _mouse_mgr.MouseDoubleClick
        _mouse_mgr_MouseActivity(sender, e)
    End Sub

    Private Sub _mouse_mgr_MouseActivity(ByVal sender As System.IntPtr, ByVal e As WindowsHookLib.LLMouseEventArgs) Handles _mouse_mgr.MouseClick
        If (e.Button <> MouseButtons.None) Then
            Dim action As MouseActionType = MouseActionType.ClickTypeUnknown
            If (e.Button = MouseButtons.Right) Then
                action = MouseActionType.ClickObjR
            End If
            If (e.Button = MouseButtons.Left) Then
                action = MouseActionType.ClickObjL
            End If
            RaiseEvent MouseEvent(e.X, e.Y, action)
        End If
        If (IsRecording = False) Then
            If (ObjectSpy = True) Then
                If e.Button <> MouseButtons.None Then
                    If (Me.CtrlState = True) Then
                        Return
                    End If
                    ObjectSpy = False
                    'e.Handled = True'causes weird side effects with IE, maybe other apps too.
                    RaiseEventObjSpy(e.X, e.Y, AltState)
                End If
            End If
            Return
        End If
        Dim hWnd As IntPtr
        HandleKeyBoard(True)
        If e.Button <> MouseButtons.None Then
            Dim parent As IntPtr = WindowsFunctions.GetHwndByXY(e.X, e.Y)
            Dim parent2 As IntPtr = IntPtr.Zero
            hWnd = parent
            While (parent <> parent2)
                If (parent = Me.hwndOfApp) Then Return
                parent2 = parent
                parent = WinAPI.API.GetParent(parent)
            End While
        End If
        Dim myBuffer As String = ""
        Dim x As Integer = e.X
        Dim y As Integer = e.Y
        If e.Button = MouseButtons.Left Then
            If (RecordXY = True) Then
#If isAbs = 1 Then
                myBuffer += "LeftClick(" & InterAction.Mouse.RelativeToAbsCoordX(e.X) & "," & InterAction.Mouse.RelativeToAbsCoordX(e.Y) & ")"
#Else
                myBuffer += "LeftClick(" & e.X & "," & e.Y & ")"
#End If

                If (e.Clicks = 2) Then
                    RaiseEventMouseAction(myBuffer, MouseActionType.DoubleClickXY, x, y)
                Else
                    RaiseEventMouseAction(myBuffer, MouseActionType.ClickXY, x, y)
                End If
            Else

                Dim quote As String = """"
                Dim str As String = ProcessWindow.BuildWindowTree(hWnd, "")
                If (str.IndexOf(vbNewLine) <> -1) Then
                    myBuffer += ProcessWindow.ProccessStrs(str, x, y)
                End If
                If (ProcessWindow.IsWebJustProcessed = False) Then
                    Dim loc As System.Drawing.Rectangle = WindowsFunctions.GetLocation(hWnd)
                    x = x - loc.X
                    y = y - loc.Y
                Else
                    x = ProcessWindow.WebX
                    y = ProcessWindow.WebY
                End If
                If (e.Clicks = 2) Then
                    RaiseEventMouseAction(myBuffer, MouseActionType.DoubleClickObjL, x, y)
                Else
                    RaiseEventMouseAction(myBuffer, MouseActionType.ClickObjL, x, y)
                End If

            End If

            myBuffer = ""
        End If
        If e.Button = MouseButtons.Right Then
            If (RecordXY = True) Then
#If isAbs = 1 Then
                myBuffer += "RightClick(" & InterAction.Mouse.RelativeToAbsCoordX(e.X) & "," & InterAction.Mouse.RelativeToAbsCoordX(e.Y) & ")"
#Else
                myBuffer += "RightClick(" & e.X & "," & e.Y & ")"
#End If
                If (e.Clicks = 2) Then
                    RaiseEventMouseAction(myBuffer, MouseActionType.DoubleClickXY, x, y)
                Else
                    RaiseEventMouseAction(myBuffer, MouseActionType.ClickXY, x, y)
                End If
            Else
                Dim quote As String = """"
                Dim str As String = ProcessWindow.BuildWindowTree(hWnd, "")
                If (str.IndexOf(vbNewLine) <> -1) Then
                    myBuffer += ProcessWindow.ProccessStrs(str, x, y)
                End If
                If (ProcessWindow.IsWebJustProcessed = False) Then
                    Dim loc As System.Drawing.Rectangle = WindowsFunctions.GetLocation(hWnd)
                    x = x - loc.X
                    y = y - loc.Y
                Else
                    x = ProcessWindow.WebX
                    y = ProcessWindow.WebY
                End If
                If (e.Clicks = 2) Then
                    RaiseEventMouseAction(myBuffer, MouseActionType.DoubleClickObjR, x, y)
                Else

                    RaiseEventMouseAction(myBuffer, MouseActionType.ClickObjR, x, y)
                End If
            End If
            myBuffer = ""
        End If
    End Sub

    Public Function MouseCheck(ByVal x As Integer, ByVal y As Integer) As String
        Dim retVal As String = ""
        If ProximityPlusOrMinus(x, mouseX, 2) = True Then
            If ProximityPlusOrMinus(y, mouseY, 2) = True Then
                mouseChecker += 1
                If mouseChecker >= 20 Then
                    retVal = "{GOTOXY(" & x & "," & y & ")}"
                    mouseChecker = 0
                End If
            Else
                mouseChecker = 0
            End If
        Else
            mouseChecker = 0
        End If
        mouseX = x
        mouseY = y
        Return retVal
    End Function

    Public Overridable Function ProximityPlusOrMinus(ByVal Location1 As Integer, ByVal Location2 As Integer, ByVal HowClose As Integer) As Boolean
        If Location1 + HowClose > Location2 Then
            If Location2 >= Location1 Then
                Return True
            End If
        End If
        If Location1 - HowClose < Location2 Then
            If Location2 <= Location1 Then
                Return True
            End If
        End If
        Return False
    End Function

    Public Overridable Sub CloseHandles()
        Me._mouse_mgr.Dispose()
    End Sub

    Public Overridable Sub StopHandlers()
        Try
            If (isHooked = True) Then
                _mouse_mgr.RemoveHook()
            End If
        Catch ex As Exception
            System.Windows.Forms.MessageBox.Show("Unable to disconnect to the mouse!  Error data: " & ex.ToString())
        End Try
        isHooked = False
    End Sub

    Protected Overridable Sub HandleKeyBoard(Optional ByVal OverrideErrors As Boolean = False)
    End Sub

End Class