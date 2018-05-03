Imports APIControls

''' <summary>
''' The Abstract Window is the set of abilities all object types have.
''' </summary>
''' <remarks>The Abstract WinObject can't be created, but is the base
''' for all Slick Test Developer Windows Objects.</remarks>
Public MustInherit Class AbstractWinObject
    Implements IAbstractWinObject
#Const IsAbs = 2 'set to 1 for abs position values
#Const IncludeWeb = 2 'set to 1 to enable web

    'Protected Friend useWildCards As Boolean
    Friend Shared TakePicturesBeforeClicks As Boolean = False
    Friend Shared TakePicturesAfterClicks As Boolean = False
    Friend Shared TakePicturesBeforeTyping As Boolean = False
    Friend Shared TakePicturesAfterTyping As Boolean = False
    Protected Friend Shared TakePictureLocationFolder As String = ".\"
    Friend Const PICTUREBEFORECLICK As String = "just before clicking"
    Friend Const PICTUREAFTERCLICK As String = "just after clicking"
    Friend Const PICTUREAFTERTYPE As String = "just after typing"
    Friend Const PICTUREBEFORETYPE As String = "just before typing"

    Protected Friend description As System.Collections.Generic.List(Of APIControls.IDescription)
    Protected Friend currentRectangle As System.Drawing.Rectangle
    Protected Friend currentHwnd As IntPtr
    Protected Friend reporter As UIControls.IReport
    Protected Friend lastWindowFoundInfo As String
    Friend Shared WindowsFunctions As New APIControls.IndependentWindowsFunctionsv1()
    'protected friend?
    Private Shared Timeout As Int32 = 20
    Private currentWindowHwnd As IntPtr
    Private Const CENTER As Integer = -1
    Private Const FULL_SIZE As Integer = -1
    Private Const LEFTBUTTON As Integer = 0
    Private Shared Rand As System.Random = New Random()
#If (IncludeWeb = 1) Then
    Friend Shared IE As New APIControls.InternetExplorer()
#End If


#Region "Private/Protected methods"

    ''' <summary>
    ''' Not used.
    ''' </summary>
    ''' <remarks></remarks>
    Protected Friend Sub New()
    End Sub


    ''' <summary>
    ''' The amount of time to wait for an item to appear (exist).
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Protected Friend Shared Property ExistTimeout() As Integer
        Get
            Return Timeout
        End Get
        Set(ByVal value As Integer)
            Timeout = value
        End Set
    End Property

    Friend Shared Property ImageLocation() As String
        Get
            Return AbstractWinObject.TakePictureLocationFolder
        End Get
        Set(ByVal value As String)
            If (value.EndsWith("\") = False) Then
                TakePictureLocationFolder = value & "\"
            Else
                TakePictureLocationFolder = value
            End If
            If (TakePictureLocationFolder.ToLower.EndsWith("images\") = False) Then
                TakePictureLocationFolder += "Images\"
                If (System.IO.Directory.Exists(TakePictureLocationFolder) = False) Then
                    System.IO.Directory.CreateDirectory(TakePictureLocationFolder)
                End If
            Else
                If (System.IO.Directory.Exists(TakePictureLocationFolder) = False) Then
                    System.IO.Directory.CreateDirectory(TakePictureLocationFolder)
                End If
            End If
        End Set
    End Property

    ''' <summary>
    ''' Checks to see if a Slick Test Windows Object is found, and if it is not,
    ''' the it will throw an exception.  This will not wait for the window to
    ''' appear.
    ''' </summary>
    ''' <remarks>If there is a process already searching for windows,
    ''' then this will wait for that to complete.</remarks>
    Protected Sub ExistsWithException()
        Try
            While (APIControls.EnumerateWindows.isRunningThreaded = True)
                Threading.Thread.Sleep(2)
            End While
            If (Exists(ExistTimeout) = False) Then
                Throw New SlickTestUIException("Failed to find object: " & lastWindowFoundInfo)
            End If
        Catch ex As Exception
            Throw
        End Try
    End Sub

    ''' <summary>
    ''' Returns the control ID of anything but a top level window.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks>Returns 0 if it is unable to retrieve the control ID.</remarks>
    Public Function GetControlID() As Integer
        Return WindowsFunctions.GetControlID(Me.Hwnd)
    End Function

    Private Function TestExists() As Boolean
        Me.currentWindowHwnd = IntPtr.Zero 'set to 0 so method works correctly
        Try
            currentHwnd = GetMainWindowHwnd()
            Me.currentWindowHwnd = currentHwnd
            For i As Integer = 1 To description.Count - 1
                lastWindowFoundInfo = description(i - 1).ToString()
                If (currentHwnd.Equals(IntPtr.Zero)) Then
                    Return False
                End If
                currentHwnd = WindowsFunctions.SearchForObj(description(i), currentHwnd)
            Next
            If (currentHwnd = IntPtr.Zero) Then
                lastWindowFoundInfo = description(description.Count - 1).ToString()
                Return False
            Else
                Me.currentRectangle = WindowsFunctions.GetLocation(currentHwnd)
                If (currentRectangle.IsEmpty) Then 'Note: This could get wrong data IF the objects are minimized.
                    Return False
                End If
                Return True
            End If
        Catch ex As Exception
            If (WindowsFunctions.HandleCount >= 1) Then
                Return True 'Exception was due to more than one item, no exception need be recorded.
            End If
            Dim ReportedText As String = "Failed to perform requested action: " & Me.ToString()
            Throw
        End Try
    End Function

    Private Function GetMainWindowHwnd() As IntPtr
        If (Me.currentWindowHwnd = IntPtr.Zero) Then
            Dim tmpWild As Boolean = description(0).WildCard
            Dim tmp As IntPtr = WindowsFunctions.SearchForObj(description(0), IntPtr.Zero)
            Me.currentWindowHwnd = tmp
            If (Me.currentWindowHwnd = IntPtr.Zero) Then
                Log.LogData("Main Hwnd is zero... will continue searching.", 2)
            End If
            description(0).WildCard = tmpWild
            Return tmp
        Else
            Return Me.currentWindowHwnd
        End If
    End Function

    Protected Friend ReadOnly Property Hwnd() As Int64 ' As Integer
        Get
            Return GetHwnd()
        End Get
    End Property

    Protected Friend ReadOnly Property Name() As String
        Get
            Return GetName()
        End Get
    End Property

#Region "Clicking code"

    Private Sub DoDrop(ByVal isLeft As Integer, ByVal X As Integer, ByVal Y As Integer)
        Try
#If IsAbs = 1 Then
            If (x <> AbstractWindow.CENTER) Then
                X = InterAction.InternalMouse.AbsToRelativeCoordX(x)
            End If
            If (y <> AbstractWindow.CENTER) Then
                Y = InterAction.InternalMouse.AbsToRelativeCoordX(y)
            End If
#Else
            'do nothing
#End If
            ExistsWithException()
            PerformDragDrop(isLeft, False, X, Y)
        Catch ex As Exception
            Throw
        End Try
    End Sub

    Private Sub DoDrag(ByVal isLeft As Integer, ByVal X As Integer, ByVal Y As Integer) 'As Boolean
        Try
#If IsAbs = 1 Then
            If (x <> AbstractWindow.CENTER) Then
                X = InterAction.InternalMouse.AbsToRelativeCoordX(x)
            End If
            If (y <> AbstractWindow.CENTER) Then
                Y = InterAction.InternalMouse.AbsToRelativeCoordX(y)
            End If
#Else
#End If
            DoMoveMouseTo(X, Y)
            currentRectangle = WindowsFunctions.GetLocation(currentHwnd)
            If (currentRectangle.Left < 0 And currentRectangle.Right < 0 And _
            currentRectangle.Top < 0 And currentRectangle.Bottom < 0) Then
                Throw New InvalidProgramException("Failed bring window to front.")
            Else
                If (X = -1 And Y = -1) Then
                    PerformDragDrop(isLeft, True, Convert.ToInt32(((currentRectangle.Left + currentRectangle.Right) / 2)), _
                    Convert.ToInt32(((currentRectangle.Top + currentRectangle.Bottom) / 2)))
                Else
                    If ((currentRectangle.Width - X) > 0 And (currentRectangle.Height - Y) > 0) Then
                        PerformDragDrop(isLeft, True, X + currentRectangle.Left, Y + currentRectangle.Top)
                    Else
                        Throw New SlickTestAPIException("X or Y cordinate is beyond the size of the object")
                    End If
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub PerformDragDrop(ByVal isLeft As Integer, ByVal isMouseDown As Boolean, ByVal X As Integer, ByVal Y As Integer)
        If (X <> -1 And Y <> -1) Then
            UIControls.InternalMouse.GotoXY(X, Y)
        ElseIf (X <> -1 Or Y <> -1) Then
            If (X = -1) Then
                UIControls.InternalMouse.GotoXY(UIControls.InternalMouse.CurrentX, Y)
            Else
                UIControls.InternalMouse.GotoXY(X, UIControls.InternalMouse.CurrentY)
            End If
        End If
        If (isLeft = LEFTBUTTON) Then
            If (isMouseDown = True) Then
                UIControls.InternalMouse.LeftDown()
            Else
                If (UIControls.InternalMouse.isMouseLeftDown = True) Then
                    UIControls.InternalMouse.LeftUp()
                End If
            End If
        Else
            If (isMouseDown = True) Then
                UIControls.InternalMouse.RightDown()
            Else
                If (UIControls.InternalMouse.isMouseRightDown = True) Then
                    UIControls.InternalMouse.RightUp()
                End If
            End If
        End If
    End Sub

    Private Sub DoClick(ByVal isLeft As Boolean, ByVal X As Integer, ByVal Y As Integer)
#If IsAbs = 1 Then
        If (x <> AbstractWindow.CENTER) Then
            If (x < 50) Then
                X += 50
            End If
            X = InterAction.InternalMouse.AbsToRelativeCoordX(x)
        End If
        If (y <> AbstractWindow.CENTER) Then
            If (y < 50) Then
                Y += 50
            End If
            Y = InterAction.InternalMouse.AbsToRelativeCoordX(y)
        End If
#Else
#End If
        Try
            DoMoveMouseTo(X, Y)
            currentRectangle = WindowsFunctions.GetLocation(currentHwnd)
            If (currentRectangle.Left < 0 And currentRectangle.Right < 0 And _
            currentRectangle.Top < 0 And currentRectangle.Bottom < 0) Then
                Throw New SlickTestUIException("Failed bring window to front.  Command: " & Me.ToString())
            Else
                If (X = -1 And Y = -1) Then
                    PerformClick(isLeft, Convert.ToInt32(((currentRectangle.Left + currentRectangle.Right) / 2)), _
                    Convert.ToInt32(((currentRectangle.Top + currentRectangle.Bottom) / 2)))
                Else
                    If ((currentRectangle.Width - X) > 0 And (currentRectangle.Height - Y) > 0) Then
                        PerformClick(isLeft, X + currentRectangle.Left, Y + currentRectangle.Top)
                    Else
                        Throw New SlickTestUIException("X or Y cordinate is beyond the size of the object.")
                    End If
                End If
            End If
        Catch ex As Exception
            Throw
        End Try
    End Sub

    Private Sub PerformClick(ByVal isLeft As Boolean, ByVal X As Integer, ByVal Y As Integer)
        If (TakePicturesBeforeClicks = True) Then
            Me.CaptureImageForUserInteraction(ImageLocation & GetPictureFileName(), PICTUREBEFORECLICK)
        End If
        If (isLeft = True) Then
            UIControls.InternalMouse.LeftClickXY(X, Y)
        Else
            UIControls.InternalMouse.RightClickXY(X, Y)
        End If
        If (TakePicturesAfterClicks = True) Then
            Me.CaptureImageForUserInteraction(ImageLocation & GetPictureFileName(), PICTUREAFTERCLICK)
        End If
    End Sub

    Protected Friend Sub CaptureImageForUserInteraction(ByVal Path As String, ByVal Action As String)
        Dim x As New UIControls.Screenshot()
        x.ImageType = System.Drawing.Imaging.ImageFormat.Png
        Try
            Me.AppActivate()
            Me.AppActivateByHwnd(Hwnd)
            System.Threading.Thread.Sleep(40)
            x.CaptureControl(Path, WindowsFunctions.GetTopParent(New IntPtr(Hwnd())))
        Catch ex As Exception
            If (Action = PICTUREAFTERCLICK) Then 'This could happen since window might be closed.
                x.CaptureDesktop(Path)
                Report(reporter.Info, "Unable to capture window after clicking.  Desktop captured instead.", "")
                'Else
                'Log.Log("Exception occured when capturing image: " & ex.ToString())
            End If
        End Try
        Report(reporter.Info, "<IMG src=""" & Path & """>", "Image captured " & Action & ": " & Path)
    End Sub

    Protected Friend Function GetPictureFileName() As String
        Dim s As String = _
        Convert.ToChar(Rand.Next(65, 90)) & _
        Convert.ToChar(Rand.Next(65, 90)) & _
        Convert.ToChar(Rand.Next(65, 90)) & _
        Convert.ToChar(Rand.Next(65, 90)) & _
        Convert.ToChar(Rand.Next(65, 90)) & _
        Convert.ToChar(Rand.Next(65, 90)) & _
        Convert.ToChar(Rand.Next(65, 90)) & _
       Rand.Next(10, 9876540).ToString() '& _
        Dim n As String = WindowsFunctions.GetClassName(New IntPtr(Me.Hwnd))
        If (n.Length = 0) Then
            s = s & ".png"
        Else
            n = n.Substring(0, Math.Min(n.Length, 60))
            s = s & "_" & n & ".png"
        End If

        Return RemoveChars(s, "\/:?""<>|")
    End Function

    Private Shared Function RemoveChars(ByVal s As String, ByVal RemoveCharsList As String)
        For Each c As Char In RemoveCharsList.ToCharArray
            s = s.Replace(c, "")
        Next
        Return s
    End Function

#End Region

    Private Sub WriteNotes(ByVal str As String)
        'reporter.RecordEvent(UIControls.Report.Info, str)
    End Sub

    Private Function AppActivate() As Boolean
        currentRectangle = WindowsFunctions.GetLocation(currentHwnd)
        WriteNotes("AppActivate: currentRectangle: " & currentRectangle.ToString())
        If (currentRectangle.Left <= 0 And currentRectangle.Right <= 0 And _
        currentRectangle.Top <= 0 And currentRectangle.Bottom <= 0) Then
            WriteNotes("AppActivate: activating...")
            WindowsFunctions.Window.SetToForeGround(GetMainWindowHwnd()) 'only bring things to the front if needed.
            System.Threading.Thread.Sleep(40)
            Me.currentRectangle = WindowsFunctions.GetLocation(currentHwnd)
        End If
        WriteNotes("AppActivate: currentRectangle: " & currentRectangle.ToString())
        If (currentRectangle.Left <= 0 And currentRectangle.Right <= 0 And _
            currentRectangle.Top <= 0 And currentRectangle.Bottom <= 0) Then
            WriteNotes("AppActivate: activating...")
            AppActivateByHwnd(Me.Hwnd)
            System.Threading.Thread.Sleep(40)
            Me.currentRectangle = WindowsFunctions.GetLocation(currentHwnd)
        End If
        WriteNotes("AppActivate: currentRectangle: " & currentRectangle.ToString())
        If (currentRectangle.Left <= 0 And currentRectangle.Right <= 0 And _
            currentRectangle.Top <= 0 And currentRectangle.Bottom <= 0) Then
            Return False
        End If
        Return True
    End Function

    Protected Friend Sub AppActivateByHwnd(ByVal Hwnd As Int64)
        WindowsFunctions.AppActivateByHwnd(New IntPtr(Hwnd))
    End Sub

    Private Function FindParentTitle(ByVal hChild As IntPtr) As String
        If (WindowsFunctions.GetParent(hChild).Equals(IntPtr.Zero)) Then
            Return WindowsFunctions.GetAllText(hChild)
        End If
        Return FindParentTitle(WindowsFunctions.GetParent(hChild))
    End Function

    ''' <summary>
    ''' Move the mouse to a certain X/Y location within the object.
    ''' </summary>
    ''' <param name="x">Optional X As Integer = CENTER</param>
    ''' <param name="y">Optional Y As Integer = CENTER</param>
    ''' <remarks>This will only allow you to move the mouse within the object.</remarks>
    Private Sub DoMoveMouseTo(Optional ByVal X As Integer = CENTER, Optional ByVal Y As Integer = CENTER)
        ExistsWithException()
        Try
            WriteNotes("About to AppActivate")
            If (AppActivate() = False) Then
                Throw New InvalidProgramException("Failed bring window to front.  Command: " & Me.ToString)
            Else
                WriteNotes("AppActivate worked.  Starting GotoXY")
                If (X = CENTER Or Y = CENTER) Then
                    If (X = CENTER And Y = CENTER) Then
                        WriteNotes("1")
                        UIControls.InternalMouse.GotoXY(Convert.ToInt32(((currentRectangle.Left + currentRectangle.Right) / 2)), _
                        Convert.ToInt32(((currentRectangle.Top + currentRectangle.Bottom) / 2)))
                    ElseIf (X = CENTER) Then
                        WriteNotes("2")
                        If ((currentRectangle.Height - Y) >= 0) Then
                            UIControls.InternalMouse.GotoXY(Convert.ToInt32(((currentRectangle.Left + currentRectangle.Right) / 2)), _
                            (Y + currentRectangle.Top))
                        Else
                            WriteNotes("3")
                            Throw New SlickTestUIException("X or Y cordinate is beyond the size of the object: " & _
                            description(description.Count - 1).ToString())
                        End If
                    Else
                        WriteNotes("4")
                        WriteNotes("currentRectangle: " & currentRectangle.ToString())
                        If ((currentRectangle.Width - X) >= 0) Then
                            UIControls.InternalMouse.GotoXY((X + currentRectangle.Left), _
                            Convert.ToInt32(((currentRectangle.Top + currentRectangle.Bottom) / 2)))
                        Else
                            WriteNotes("5")
                            Throw New SlickTestUIException("X or Y cordinate is beyond the size of the object: " & _
                            description(description.Count - 1).ToString())
                        End If
                    End If
                Else
                    WriteNotes("6")
                    If ((currentRectangle.Width - X) >= 0 And (currentRectangle.Height - Y) >= 0) Then
                        UIControls.InternalMouse.GotoXY(X + currentRectangle.Left, Y + currentRectangle.Top)
                    Else
                        WriteNotes("7 " & description.Count)
                        Throw New SlickTestUIException("X or Y cordinate is beyond the size of the object: " & _
                        description(description.Count - 1).ToString() & ".  Object Size: " & currentRectangle.ToString())
                    End If
                End If
            End If
            WriteNotes("Completed GotoXY.  ObjectAtAbs...")
            If (IsObjectAtAbsLocation(UIControls.InternalMouse.CurrentAbsX, UIControls.InternalMouse.CurrentAbsY) = False) Then
                WriteNotes("SetToForeGround(currentWindowHwnd) " & currentWindowHwnd.ToString())
                WindowsFunctions.Window.SetToForeGround(currentWindowHwnd) 'only bring things to the front if needed.
                Me.AppActivateByHwnd(Me.Hwnd)
                If (IsObjectAtAbsLocation(UIControls.InternalMouse.CurrentAbsX, UIControls.InternalMouse.CurrentAbsY) = False) Then
                    Throw New SlickTestUIException("Failed to find or bring window to front.  Command: " & Me.ToString())
                End If
            End If
        Catch ex As Exception
            Throw
        End Try
    End Sub

    Protected Friend Function readDescription(ByVal desc As String) As Boolean
        Dim index As Integer = desc.IndexOf(":=")
        If (index = -1) Then
            Return False
        End If
        Dim type As String = desc.Substring(0, index).ToLower().Trim()
        Dim tmpValue As String = ""
        index = desc.IndexOf("""")
        Dim index2 As Integer = desc.IndexOf(";;", index + 1)
        If (index2 = -1) Then
            If (desc.Substring(desc.Length - 1) = """") Then
                index2 = desc.Length '+ 1
            Else
                index2 = desc.Length + 1
            End If
        End If
        tmpValue = desc.Substring(index + 1, index2 - (2 + index))
        description.Item(description.Count - 1).Add(type, tmpValue)
        If (index2 > desc.Length) Then
            index2 = desc.Length
        End If
        If (desc.IndexOf(":=", index2) <> -1) Then
            Return readDescription(desc.Substring(index2 + 2))
        Else
            Return True
        End If
        '"name:=""blah"";;value:="blah"
    End Function

    Protected Friend Sub Report(ByVal Type As Short, ByVal MajorMessage As String, ByVal MinorMessage As String)
        reporter.RecordEvent(Type, MajorMessage, MinorMessage)
    End Sub

#End Region

    ''' <summary>
    ''' Gets the process name of any object.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks>If the process is unknown it will return "".</remarks>
    Public Function GetProcessName() As String
        Return WindowsFunctions.GetProcessName(New IntPtr(Me.Hwnd()))
    End Function

    'exist is _HEAVILY_ used, so it must be made fast.
    'No waste if possible.
    ''' <summary>
    ''' Test to see if an object can be found in a certain amount of time.
    ''' </summary>
    ''' <param name="time">The time to see if the object is found.</param>
    ''' <returns>Returns true if the object was found, false if it was not.</returns>
    ''' <remarks>
    ''' This method, when passing in a time, does not ensure a
    ''' window will be found if the window appears and disappears
    ''' quickly.  In those case, it is recommended you create 
    ''' your own loop and perform a exists(0)<p/><p/>
    '''   Example Code: <p/>
    ''' if(Me.Window(MyWindow).Exists(10)=true) then<p/>
    '''   MsgBox "Window was found with in 10 seconds"<p/>
    ''' else<p/>
    '''   MsgBox "After 10 seconds, the window was never found"<p/>
    ''' end if<p/>
    ''' </remarks>
    Public Function Exists(ByVal Time As Integer) As Boolean Implements IAbstractWinObject.Exists
        Dim currentTime As System.DateTime = System.DateTime.Now()
        If (Time <> 0) Then
            Do
                If (TestExists() = True) Then
                    Return True
                End If
                System.Threading.Thread.Sleep(200)
            Loop While ((System.DateTime.Now - currentTime).Seconds < Time)
        Else
            Return TestExists()
        End If
        Return False
    End Function
    'Public Function Exists(ByVal Time As Integer) As Boolean Implements IAbstractWinObject.Exists
    '    System.Console.WriteLine("Test Exists Started: " & System.DateTime.Now)
    '    Dim timer As Integer = Time * 1000 'set to milliseconds
    '    If (Time <> 0) Then
    '        Do
    '            System.Console.WriteLine("Test Exists Loop Started: " & System.DateTime.Now)

    '            If (TestExists() = True) Then
    '                Return True
    '            End If
    '            System.Threading.Thread.Sleep(200)
    '            System.Console.WriteLine("Test Exists End: " & System.DateTime.Now)

    '            timer -= 210 'add a little for processing time ;).. 
    '            'note: This could be done much better, but good enough for now
    '        Loop While (timer > 0)
    '    Else
    '        Return TestExists()
    '    End If
    '    Return False
    'End Function

    ''' <summary>
    ''' Test to see if an object can be found without rechecking multiple times..
    ''' </summary>
    ''' <returns>Returns true if the object was found, false if it was not.</returns>
    ''' <remarks>
    ''' This method, when passing in a time, does not ensure a
    ''' window will be found if the window appears and disappears
    ''' quickly.  In those case, it is recommended you create 
    ''' your own loop and perform a Exists(0)<p/><p/>
    '''   Example Code: <p/>
    ''' if(Me.Window(MyWindow).Exists()=true) then<p/>
    '''   MsgBox "Window was found."<p/>
    ''' else<p/>
    '''   MsgBox "After checking once, the window was not found"<p/>
    ''' end if<p/>
    ''' </remarks>
    Public Function Exists() As Boolean Implements IAbstractWinObject.Exists
        Return Exists(0)
    End Function

    ''' <summary>
    ''' Gets the number of objects that contain the description provided.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks>This is helpful for trying to identify if you have more than one
    ''' window with the same description open at the same time.</remarks>
    Public Function GetObjectCount() As Integer Implements IAbstractWinObject.WinObjectCount
        Try
            Exists()
        Catch ex As Exception
        End Try
        Return WindowsFunctions.HandleCount
    End Function

    ''' <summary>
    ''' Gets an array of Hwnd of the objects that have the same description as the one provided.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks>This is helpful to identify the windows that exist
    ''' window with the same description open at the same time.</remarks>
    Public Function GetObjectHwnds() As IntPtr() Implements IAbstractWinObject.WinObjectHwnds
        Try
            Exists()
        Catch ex As Exception
        End Try
        Return WindowsFunctions.HandlesList
    End Function

    ''' <summary>
    ''' Returns the Window Handle (Hwnd) value.
    ''' </summary>
    ''' <value></value>
    ''' <returns>Returns 0 if object can't be found.</returns>
    ''' <remarks></remarks>
    Public ReadOnly Property GetHwnd() As Int64 Implements IAbstractWinObject.Hwnd
        Get
            If (Exists(ExistTimeout) = True) Then
                Return Me.currentHwnd.ToInt64()
            Else
                Return 0
            End If
        End Get
    End Property
#Region "Location"

    ''' <summary>
    ''' Provides the left point of the object.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks>This may return 0 if the application is currently minimized.</remarks>
    Public ReadOnly Property GetLeft() As Integer Implements IAbstractWinObject.Left
        Get
            ExistsWithException()
#If IsAbs = 1 Then
            Return Mouse.RelativeToAbsCoordX(currentRectangle.Left)
#Else
            Return currentRectangle.Left
#End If
        End Get
    End Property

    ''' <summary>
    ''' Provides the right point of the object.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks>This may return 0 if the application is currently minimized.</remarks>
    Public ReadOnly Property GetRight() As Integer Implements IAbstractWinObject.Right
        Get
            ExistsWithException()
#If IsAbs = 1 Then
            Return Mouse.RelativeToAbsCoordX(currentRectangle.Right)
#Else
            Return currentRectangle.Right
#End If
        End Get
    End Property

    ''' <summary>
    ''' Provides the top point of the object.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks>This may return 0 if the application is currently minimized.</remarks>
    Public ReadOnly Property GetTop() As Integer Implements IAbstractWinObject.Top
        Get
            ExistsWithException()
#If IsAbs = 1 Then
            Return Mouse.RelativeToAbsCoordX(currentRectangle.Top)
#Else
            Return currentRectangle.Top
#End If
        End Get
    End Property

    ''' <summary>
    ''' Provides the bottom point of the object.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks>This may return 0 if the application is currently minimized.</remarks>
    Public ReadOnly Property GetBottom() As Integer Implements IAbstractWinObject.Bottom
        Get
            ExistsWithException()
#If IsAbs = 1 Then
            Return Mouse.RelativeToAbsCoordX(currentRectangle.Bottom)
#Else
            Return currentRectangle.Bottom
#End If
        End Get
    End Property

    ''' <summary>
    ''' Returns the width of the object.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks>This may return 0 if the application is currently minimized.</remarks>
    Public ReadOnly Property GetWidth() As Integer Implements IAbstractWinObject.Width
        Get
            ExistsWithException()
#If IsAbs = 1 Then
            Return Mouse.RelativeToAbsCoordX(currentRectangle.Width)
#Else
            Return currentRectangle.Width
#End If
        End Get
    End Property

    ''' <summary>
    ''' Return the height of the object.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks>This may return 0 if the application is currently minimized.</remarks>
    Public ReadOnly Property GetHeight() As Integer Implements IAbstractWinObject.Height
        Get
            ExistsWithException()
#If IsAbs = 1 Then
            Return Mouse.RelativeToAbsCoordX(currentRectangle.Height)
#Else
            Return currentRectangle.Height
#End If
        End Get
    End Property
#End Region

    'Is the object at the absolute location of X,Y 
    'NOTE: InterAct.InterAction.InternalMouse.currentX,InterAct.InterAction.InternalMouse.currentY should get you X/Y
    'cords
    ''' <summary>
    ''' Tests to see if an object is at a certain absolute location.
    ''' </summary>
    ''' <param name="x">The absolute X location.</param>
    ''' <param name="y">The absolute Y location.</param>
    ''' <returns>Returns true if object is under the X/Y location given.</returns>
    ''' <remarks></remarks>
    Public Function IsObjectAtAbsLocation(ByVal X As Integer, ByVal Y As Integer) As Boolean Implements IAbstractWinObject.IsObjectAtAbsLocation
        ExistsWithException()
#If IsAbs <> 1 Then
        Dim hwnd As IntPtr = WindowsFunctions.GetHwndByXY(UIControls.InternalMouse.AbsToRelativeCoordX(X), UIControls.InternalMouse.AbsToRelativeCoordY(Y))
#Else
        Dim hwnd As IntPtr = WindowsFunctions.GetHwndByXY(x, y)
#End If
        While (hwnd <> IntPtr.Zero)
            If (hwnd = Me.currentHwnd) Then
                Return True
            End If
            hwnd = WindowsFunctions.GetParent(hwnd)
        End While
        Return False
    End Function

    ''' <summary>
    ''' Clicks on a certain location within an object.
    ''' </summary>
    ''' <param name="x">Clicks inside the object at the X-coordinate given</param>
    ''' <param name="y">Clicks inside the object at the Y-coordinate given</param>
    ''' <remarks>This method only performs a left click.</remarks>
    Public Sub Click(ByVal X As Integer, ByVal Y As Integer) Implements IAbstractWinObject.Click
        DoClick(True, X, Y)
    End Sub

    ''' <summary>
    ''' Clicks on the center of an object.
    ''' </summary>
    ''' <remarks>This method only performs a left click.</remarks>
    Public Sub Click() Implements IAbstractWinObject.Click
        Click(CENTER, CENTER)
    End Sub

    ''' <summary>
    ''' Clicks twice on a certain location within an object.
    ''' </summary>
    ''' <param name="x">Clicks inside the object at the X-coordinate given</param>
    ''' <param name="y">Clicks inside the object at the Y-coordinate given</param>
    ''' <remarks>This method only performs a left click.</remarks>
    Public Sub DoubleClick(ByVal X As Integer, ByVal Y As Integer)
        DoClick(True, X, Y)
        DoClick(True, X, Y)
    End Sub

    ''' <summary>
    ''' Clicks on the center of an object twice.
    ''' </summary>
    ''' <remarks>This method only performs a left click.</remarks>
    Public Sub DoubleClick()
        DoubleClick(CENTER, CENTER)
    End Sub

    ''' <summary>
    ''' Clicks on a certain location within an object.
    ''' </summary>
    ''' <param name="x">Clicks inside the object at the X-coordinate given</param>
    ''' <param name="y">Clicks inside the object at the Y-coordinate given</param>
    ''' <remarks>This method only performs a right click.</remarks>
    Public Sub RightClick(ByVal X As Integer, ByVal Y As Integer) Implements IAbstractWinObject.RightClick
        DoClick(False, X, Y)
    End Sub

    ''' <summary>
    ''' Clicks on a certain location within an object.
    ''' </summary>
    ''' <remarks>This method only performs a right click.</remarks>
    Public Sub RightClick() Implements IAbstractWinObject.RightClick
        RightClick(CENTER, CENTER)
    End Sub

    ''' <summary>
    ''' Presses a mouse button down on the object at a X/Y location
    ''' </summary>
    ''' <param name="X">The X-Coordinate that the mouse will be pressed at.</param>
    ''' <param name="Y">The Y-Coordinate that the mouse will be pressed at.</param>
    ''' <param name="Button">0 sets the mouse click to left, 1 sets the mouse click to right.</param>
    ''' <remarks>This is useful for a drag/drop scenerio or when you want to 
    ''' hold down a button for a certain about of time.</remarks>
    Public Sub MouseDown(ByVal X As Integer, ByVal Y As Integer, ByVal Button As Integer) Implements IAbstractWinObject.MouseDown
        DoDrag(LEFTBUTTON, X, Y)
    End Sub

    ''' <summary>
    ''' Presses the left mouse button down on the object at a X/Y location
    ''' </summary>
    ''' <param name="X">The X-Coordinate that the mouse will be pressed at.</param>
    ''' <param name="Y">The Y-Coordinate that the mouse will be pressed at.</param>
    ''' <remarks>This is useful for a drag/drop scenerio or when you want to 
    ''' hold down a button for a certain about of time.</remarks>
    Public Sub MouseDown(ByVal X As Integer, ByVal Y As Integer) Implements IAbstractWinObject.MouseDown
        MouseDown(X, Y, LEFTBUTTON)
    End Sub

    ''' <summary>
    ''' Presses the left mouse button down on the object at the center location.
    ''' </summary>
    ''' <remarks>This is useful for a drag/drop scenerio or when you want to 
    ''' hold down a button for a certain about of time.</remarks>
    Public Sub MouseDown() Implements IAbstractWinObject.MouseDown
        MouseDown(CENTER, CENTER, LEFTBUTTON)
    End Sub

    ''' <summary>
    ''' Releases a mouse button already pressed down at a certain X/Y location.
    ''' </summary>
    ''' <param name="X">The X-Coordinate that the mouse will be released at.</param>
    ''' <param name="Y">The Y-Coordinate that the mouse will be released at.</param>
    ''' <param name="Button">0 sets the mouse click to left, 1 sets the mouse click to right.</param>
    ''' <remarks>This is useful for a drag/drop scenerio or when you want 
    ''' to hold down a button for a certain about of time.</remarks>
    Public Sub MouseUp(ByVal X As Integer, ByVal Y As Integer, ByVal Button As Integer) Implements IAbstractWinObject.MouseUp
        DoDrop(Button, X, Y)
    End Sub

    ''' <summary>
    ''' Releases the left mouse button already pressed down at a certain X/Y location.
    ''' </summary>
    ''' <param name="X">The X-Coordinate that the mouse will be released at.</param>
    ''' <param name="Y">The Y-Coordinate that the mouse will be released at.</param>
    ''' <remarks>This is useful for a drag/drop scenerio or when you want 
    ''' to hold down a button for a certain about of time.</remarks>
    Public Sub MouseUp(ByVal X As Integer, ByVal Y As Integer) Implements IAbstractWinObject.MouseUp
        MouseUp(X, Y, LEFTBUTTON)
    End Sub

    ''' <summary>
    ''' Releases the left mouse button in the center of the object. 
    ''' NOTE: Dragging may cause the actual location to not appear to be the center of the object.
    ''' </summary>
    ''' <remarks>This is useful for a drag/drop scenerio or when you want 
    ''' to hold down a button for a certain about of time.</remarks>
    Public Sub MouseUp() Implements IAbstractWinObject.MouseUp
        MouseUp(CENTER, CENTER, LEFTBUTTON)
    End Sub

    ''' <summary>
    ''' This click simulates a click by sending a message
    ''' to the object rather than physically click on the object.
    ''' </summary>
    ''' <remarks>
    ''' The advantage to this is that the app can be off screen
    ''' or even minimized and the click will still be performed.<BR/>
    ''' The disadvantage is that there is not way to click on a
    ''' particular part of the object.<BR/>
    ''' Example Code: <BR/>
    ''' InterActObject.Window(myWindow).SmartClick()
    ''' </remarks>
    Public Sub SmartClick() Implements IAbstractWinObject.SmartClick
        'this click does not need to know anything about the window or the window location.
        'it doesn't even need the app maximized.
        ExistsWithException()
        Try
            UIControls.InternalMouse.ClickByHwnd(currentHwnd)
        Catch ex As Exception
            Throw
        End Try
    End Sub

    ''' <summary>
    ''' Determines if the Windows Object is enabled or is disabled.
    ''' </summary>
    ''' <returns>Returns true if the Windows Object is enabled</returns>
    ''' <remarks></remarks>
    Public Function IsEnabled() As Boolean Implements IAbstractWinObject.IsEnabled
        ExistsWithException()
        Return WindowsFunctions.IsEnabled(New IntPtr(Me.Hwnd))
    End Function

    ''' <summary>
    ''' Gets the windows style value for the object.  Returns -1 if the object could
    ''' not be found.
    ''' </summary>
    ''' <returns>The current object style.</returns>
    ''' <remarks></remarks>
    Public Function GetStyle() As Int64 Implements IAbstractWinObject.GetStyle
        ExistsWithException()
        Return WindowsFunctions.GetWindowsStyle(New IntPtr(Me.Hwnd))
    End Function

    ''' <summary>
    ''' Gets the windows style extended value for the object.  Returns -1 if the object could
    ''' not be found.
    ''' </summary>
    ''' <returns>The current extended object style.</returns>
    ''' <remarks></remarks>
    Public Function GetStyleEx() As Int64 Implements IAbstractWinObject.GetStyleEx
        ExistsWithException()
        Return WindowsFunctions.GetEXWindowsStyle(New IntPtr(Me.Hwnd))
    End Function

    ''' <summary>
    ''' Move the mouse to a certain X/Y location within the object.
    ''' </summary>
    ''' <param name="x">Moves the mouse's X-coordinate to a certain location inside the object.</param>
    ''' <param name="y">Moves the mouse's Y-coordinate to a certain location inside the object.</param>
    ''' <remarks>This will only allow you to move the mouse within the object.</remarks>
    Public Sub MoveMouseTo(ByVal X As Integer, ByVal Y As Integer) Implements IAbstractWinObject.MoveMouseTo
#If IsAbs = 1 Then
        If (x <> AbstractWindow.CENTER) Then
            X = InterAction.InternalMouse.AbsToRelativeCoordX(x)
        End If
        If (y <> AbstractWindow.CENTER) Then
            Y = InterAction.InternalMouse.AbsToRelativeCoordX(y)
        End If
#End If
        DoMoveMouseTo(X, Y)
    End Sub

    ''' <summary>
    ''' Move the mouse to the center X/Y location within the object.
    ''' </summary>
    ''' <remarks>This will only allow you to move the mouse within the object.</remarks>
    Public Sub MoveMouseTo() Implements IAbstractWinObject.MoveMouseTo
        MoveMouseTo(CENTER, CENTER)
    End Sub

    ''' <summary>
    ''' This allows a user to capture an image of the object or a part of the object.
    ''' </summary>
    ''' <param name="FilePath">The path to save the file to.</param>
    ''' <remarks>
    '''   <BR/>Example Code:<BR/>
    '''   bSuccess = Me.Window(myWindow).CaptureImage("""C:\myFile""")<BR/>
    ''' </remarks>
    Public Sub CaptureImage(ByVal FilePath As String) Implements IAbstractWinObject.CaptureImage
        CaptureImage(FilePath, New System.Drawing.Rectangle(0, 0, FULL_SIZE, FULL_SIZE))
    End Sub

    ''' <summary>
    ''' This allows a user to capture an image of the object or a part of the object.
    ''' </summary>
    ''' <param name="FilePath">The path to save the file to.</param>
    ''' <param name="Rect">The rectangle you wish to capture.</param>
    ''' <remarks>
    '''   <BR/>Example Code:<BR/>
    '''   bSuccess = Me.Window(myWindow).CaptureImage("""C:\myFile""", New System.Drawing.Rectangle(1,1,10,10))<BR/>
    ''' </remarks>
    Public Sub CaptureImage(ByVal FilePath As String, ByVal Rect As System.Drawing.Rectangle) Implements IAbstractWinObject.CaptureImage
        Try
            Dim Success As Boolean = False
            Me.AppActivate()
            Me.AppActivateByHwnd(Me.Hwnd())
            If (Rect.X = 0 And Rect.Y = 0 And Rect.Width = FULL_SIZE And Rect.Height = FULL_SIZE) Then
                Dim x As New UIControls.Screenshot()
                Success = x.CaptureControl(FilePath, Hwnd)
            Else
                Dim y As New UIControls.Screenshot()
                Success = y.CaptureControl(FilePath, Rect, Hwnd)
            End If
            If (Success = False) Then
                Throw New SlickTestUIException("Image '" & FilePath & "' was not captured successfully.")
            End If
        Catch ex As Exception
            Report(reporter.Fail, "Failed to perform requested action: " & Me.ToString, _
                "Error: " & ex.Message)
            Throw
        End Try
    End Sub

    ''' <summary>
    ''' Returns the entire location in string format.
    ''' </summary>
    ''' <returns>returns a string format of the location.</returns>
    ''' <remarks></remarks>
    Public Function GetLocation() As String Implements IAbstractWinObject.GetLocation
        ExistsWithException()
        Dim tmp As System.Drawing.Rectangle = WindowsFunctions.GetLocation(currentHwnd)
        Return tmp.ToString()
    End Function

    ''' <summary>
    ''' Gets the client area (the area that does not include scroll bars).
    ''' </summary>
    ''' <returns>Return a recta</returns>
    ''' <remarks></remarks>
    Public Function GetClientAreaRect() As System.Drawing.Rectangle
        ExistsWithException()
        Return WindowsFunctions.GetClientLocation(currentHwnd)
    End Function

    ''' <summary>
    ''' Returns the entire location.
    ''' </summary>
    ''' <returns>returns the rectangle of the object.</returns>
    ''' <remarks></remarks>
    Public Function GetLocationRect() As System.Drawing.Rectangle
        ExistsWithException()
        Return WindowsFunctions.GetLocation(currentHwnd)
    End Function

    ''' <summary>
    ''' Provides the specific object type slick test sees.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetObjectType() As String
        Return WindowsFunctions.GetObjectTypeAsString(Hwnd)
    End Function

    ''' <summary>
    ''' Returns a string of the current description.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Overrides Function ToString() As String Implements IAbstractWinObject.ToString
        'Return MyBase.ToString()
        Dim s As New System.Text.StringBuilder()
        For i As Integer = 0 To description.Count - 1
            s.Append(description(i).ToString())
        Next
        Return s.ToString()
    End Function

    ''' <summary>
    ''' Places a red ring around the object for about 3 seconds.
    ''' </summary>
    ''' <remarks>This should have no affect on the system but will bring the window to the top.</remarks>
    Public Sub Highlight()
        Dim hwnd As IntPtr = New IntPtr(Me.Hwnd())
        Dim FlashHighlighter As New FlashHighlighter()
        FlashHighlighter.Start(hwnd, 7, 240)
        Do Until FlashHighlighter.IsCompleted
            System.Windows.Forms.Application.DoEvents()
            System.Threading.Thread.Sleep(200)
        Loop
        FlashHighlighter.Hide()
    End Sub

#Region "Not For .net"

    ''' <summary>
    ''' Returns the object's name.
    ''' </summary>
    ''' <value></value>
    ''' <returns>This will NOT return the dotnet name of the window.</returns>
    ''' <remarks></remarks>
    Public ReadOnly Property GetName() As String Implements IAbstractWinObject.Name
        Get
            ExistsWithException()
            Return WindowsFunctions.GetClassNameNoDotNet(New IntPtr(Me.Hwnd))
        End Get
    End Property

#End Region

End Class
