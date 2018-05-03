Public Class ScreenCapture
#Const IsAbs = 2 'set to 1 for abs position values
    Private point1 As System.Drawing.Point
    Private point1Rel As System.Drawing.Point
    Private point2 As System.Drawing.Point
    Private saveFileLocation As String
    Private Shared ScreenShot As UIControls.Screenshot
    Private Shared WindowsFunctions As New APIControls.IndependentWindowsFunctionsv1()
    Friend WithEvents Keys As HandleInput.MouseOnlyHandler
    Private Const MouseCaptureText As String = "Mouse Capture"
    Private Const MouseTopLeftCorner As String = "Top-Left Corner"
    Private Const DefaultFileExtension As String = "bmp"
    Private FileExtension As String = DefaultFileExtension

    Public Sub New()
        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        point1 = New Point()
        point2 = New Point()
        point1Rel = New Point()
        ScreenShot = New UIControls.Screenshot()
        saveFileLocation = ""
        Keys = New HandleInput.MouseOnlyHandler()
    End Sub

    Private Sub UpdateFileExtension(ByVal File As String)
        If (File.Contains(".")) Then
            FileExtension = System.IO.Path.GetExtension(File).Replace(".", "")
            Dim tmpFileExtension As String = FileExtension.ToUpperInvariant()
            Dim success As Boolean = False
            For Each ext As String In New String() {"PNG", "JPEG", "BMP", "JPG", "TIF", "WMF"}
                If (tmpFileExtension = ext) Then
                    success = True
                End If
            Next
            If (Not success) Then
                FileExtension = DefaultFileExtension
            End If
        Else
            FileExtension = DefaultFileExtension
        End If
    End Sub


    Public Sub SC()
        Dim File As String = _
       InputBox("After you press ok, you will have 10 seconds to put the " + _
       "app you want to have a screenshot on  top.  Enter a folder path and file name you wish to use for this screenshot!", _
       SlickTestDev.MsgBoxTitle, "C:\")

        If (File.Length < 3) Then
            Return 'cancel pressed or invalid file length
        End If

        If (System.IO.File.Exists(File) = True) Then
            System.Windows.Forms.MessageBox.Show("Error, file already exists!", _
                                                 SlickTestDev.MsgBoxTitle)
            Return
        End If
        UpdateFileExtension(File)
        Dim startTime As Date = Date.Now
        Dim endTime As Date = Date.Now

        While (endTime.Subtract(startTime).Seconds < 10)
            System.Threading.Thread.Sleep(10)
            Application.DoEvents()
            endTime = Date.Now
        End While

        Dim ptr As Integer = WinAPI.API.GetForegroundWindow()
        Dim str As String = HandleInput.ProcessWindow.BuildWindowTree(New IntPtr(ptr), "")
        If (str.IndexOf(vbNewLine) <> -1) Then
            Me.CodeTextBox.Text = HandleInput.ProcessWindow.ProccessStrs(str, -1, -1)
        End If
        If (SlickTestDev.CurrentLanguage = CodeTranslator.Languages.CSharp) Then
            Me.CodeTextBox.Text += "CaptureImage(""C:\test." & FileExtension & """);"
            Me.CodeTextBox.Text = CodeTranslator.FixVBStringToCSharpVbLikeString(Me.CodeTextBox.Text)
        Else
            Me.CodeTextBox.Text += "CaptureImage(""C:\test." & FileExtension & """)"
        End If
        If (ScreenShot.TopWindow(File) = False) Then
            System.Windows.Forms.MessageBox.Show( _
            "Failed to capture top window.", SlickTestDev.MsgBoxTitle)
        Else
            If (File.ToLower.EndsWith(FileExtension) = False) Then File += "." & FileExtension
            System.Windows.Forms.MessageBox.Show("Screenshot captured at: " & File, _
                                                 SlickTestDev.MsgBoxTitle)
        End If
    End Sub

    Private Sub TopAppButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TopAppButton.Click
        SC()
    End Sub

    Private Sub GetScreenByMouseButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GetScreenByMouseButton.Click
        If (GetScreenByMouseButton.Text = MouseCaptureText) Then
            saveFileLocation = _
            InputBox("After you press ok, you will need to put the mouse in the upper left hand corner" & _
            " of the location you want to take a screenshot then right click (using the mouse).  " & _
            "Repeat the same steps for the Bottom-Right Corner.  Enter a folder path and file name you wish to use for this screenshot!", _
            SlickTestDev.MsgBoxTitle, "C:\")

            If (saveFileLocation.Length < 3) Then
                Return 'cancel pressed or invalid file length
            End If
            UpdateFileExtension(saveFileLocation)

            If (System.IO.Path.GetFileName(saveFileLocation) = System.IO.Path.GetFileNameWithoutExtension(saveFileLocation)) Then
                saveFileLocation += "." & FileExtension
            End If

            If (System.IO.File.Exists(saveFileLocation) = True) Then
                System.Windows.Forms.MessageBox.Show( _
                "Error: file already exists! File name: " & _
                saveFileLocation, SlickTestDev.MsgBoxTitle)
                Return
            End If

            Me.TopMost = True
            Me.TopAppButton.Enabled = False
            If (Not Keys.IsSystemHooked) Then Keys.StartHandlers()
            GetScreenByMouseButton.Text = MouseTopLeftCorner

        Else
            Keys_MouseEvent(System.Windows.Forms.Cursor.Position.X, System.Windows.Forms.Cursor.Position.Y, HandleInput.MouseOnlyHandler.MouseActionType.ClickObjR)
        End If
    End Sub

    Function IsNumber(ByVal num As String) As Boolean
        For Each item As Char In num
            If (Char.IsDigit(item) = False) Then
                Return False
            End If
        Next
        If (num.Length = 0) Then Return False
        Return True
    End Function

    Private Sub ScreenCapture_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        Keys.StopHandlers()
        Keys.CloseHandles()
    End Sub

    Private Sub Keys_MouseEvent(ByVal x As Integer, ByVal y As Integer, ByVal type As HandleInput.MouseOnlyHandler.MouseActionType) Handles Keys.MouseEvent
        If (type = HandleInput.MouseOnlyHandler.MouseActionType.ClickObjR) Then
            If (GetScreenByMouseButton.Text = MouseTopLeftCorner) Then
                GetScreenByMouseButton.Text = "Bottom-Right Corner"
                point1.X = x
                point1.Y = y
                Dim rec As Rectangle = WindowsFunctions.GetLocation(WinAPI.API.WindowFromPoint(point1.X, point1.Y))
                point1Rel.X = point1.X - rec.X
                point1Rel.Y = point1.Y - rec.Y
            Else
                GetScreenByMouseButton.Text = MouseCaptureText
                point2.X = x
                point2.Y = y
                Dim ptr2 As Integer = CType(WinAPI.API.WindowFromPoint(point2.X, point2.Y), Integer)
                Dim ptr1 As Integer = CType(WinAPI.API.WindowFromPoint(point1.X, point1.Y), Integer)
                Dim str2 As String = HandleInput.ProcessWindow.BuildWindowTree(New IntPtr(ptr2), "")
                Dim str1 As String = HandleInput.ProcessWindow.BuildWindowTree(New IntPtr(ptr1), "")
                Dim count As Integer = 0
                Dim RunSmartCapture As Boolean = True
                If (ptr2 = ptr1) Then
                    If (str2.IndexOf(vbNewLine) = -1) Then
                        RunSmartCapture = False
                    End If
                End If

                If (ptr2 = ptr1) Then
                    If (str1.IndexOf(vbNewLine) = -1) Then
                        RunSmartCapture = False
                    End If
                End If
                If (ptr2 <> ptr1) Then
                    Dim strs1() As String = str1.Split(vbNewLine)
                    Dim strs2() As String = str2.Split(vbNewLine)
                    Dim matchfound As Boolean = False
                    For count = (strs1.GetLength(0) - 1) To 0 Step -1
                        If (strs2.GetLength(0) <= count) Then
                            count = (strs2.GetLength(0) - 1)
                        End If
                        If (strs1(count) = strs2(count)) Then
                            If (IsNumber(strs1(count))) Then
                                matchfound = True
                                Exit For
                            End If
                        Else
                            Exit For
                        End If
                    Next
                    count = count - 1
                    If (matchfound = True) Then
                        RunSmartCapture = True
                    Else
                        count = -1
                        RunSmartCapture = False
                    End If
                Else
                    RunSmartCapture = True
                End If
                If (RunSmartCapture) Then
                    'point1 = point1Rel
                    Dim rec As Rectangle = WindowsFunctions.GetLocation(New IntPtr(ptr1))
                    'point2.X = point2.X - rec.X
                    'point2.Y = point2.Y - rec.Y

                    Dim h As Integer = (point2.Y - point1.Y)
                    If (h < 0) Then h = (point1.Y - point2.Y)
                    Dim w As Integer = (point2.X - point1.X)
                    If (w < 0) Then w = (point1.X - point2.X)

                    Me.CodeTextBox.Text = HandleInput.ProcessWindow.ProccessStrs(str2, -1, -1, count)
#If IsAbs = 1 Then
                point1.X = InterAction.Mouse.RelativeToAbsCoordX(point1.X)
                point2.X = InterAction.Mouse.RelativeToAbsCoordX(point2.X)
                point1.Y = InterAction.Mouse.RelativeToAbsCoordX(point1.Y)
                point2.Y = InterAction.Mouse.RelativeToAbsCoordX(point2.Y)
                h = InterAction.Mouse.RelativeToAbsCoordX(h)
                w = InterAction.Mouse.RelativeToAbsCoordX(w)
#End If
                    Try
                        ScreenShot.CaptureScreenArea(saveFileLocation, New System.Drawing.Rectangle(point1.X, point1.Y, w, h))
                    Catch ex As Exception
                        System.Windows.Forms.MessageBox.Show("Unable to capture Image: " & ex.ToString())
                    End Try
                    If (SlickTestDev.CurrentLanguage = CodeTranslator.Languages.VBNet) Then
                        Me.CodeTextBox.Text += "CaptureImage(""C:\test." & FileExtension & """, New System.Drawing.Rectangle(" & point1.X & "," & _
                        point1.Y & "," & w & "," & h & "))"
                    Else
                        Me.CodeTextBox.Text += "CaptureImage(""C:\test." & FileExtension & """, new System.Drawing.Rectangle(" & point1.X & "," & _
                        point1.Y & "," & w & "," & h & "));"
                        Me.CodeTextBox.Text = CodeTranslator.FixVBStringToCSharpVbLikeString(Me.CodeTextBox.Text)
                    End If
                Else
                    Dim h As Integer = (point2.Y - point1.Y)
                    If (h < 0) Then h = (point1.Y - point2.Y)
                    Dim w As Integer = (point2.X - point1.X)
                    If (w < 0) Then w = (point1.X - point2.X)
                    Try
#If IsAbs = 1 Then
                    point1.X = InterAction.Mouse.RelativeToAbsCoordX(point1.X)
                    point2.X = InterAction.Mouse.RelativeToAbsCoordX(point2.X)
                    point1.Y = InterAction.Mouse.RelativeToAbsCoordX(point1.Y)
                    point2.Y = InterAction.Mouse.RelativeToAbsCoordX(point2.Y)
                    h = InterAction.Mouse.RelativeToAbsCoordX(h)
                    w = InterAction.Mouse.RelativeToAbsCoordX(w)
#End If
                        ScreenShot.CaptureScreenArea(saveFileLocation, New System.Drawing.Rectangle(point1.X, point1.Y, w, h))
                    Catch ex As Exception
                        System.Windows.Forms.MessageBox.Show("Unable to capture Image: " & ex.ToString())
                    End Try
                    If (SlickTestDev.CurrentLanguage = CodeTranslator.Languages.VBNet) Then
                        Me.CodeTextBox.Text = "Me.Screenshot.CaptureScreenArea(""C:\test." & FileExtension & """, New System.Drawing.Rectangle(" & _
                        point1.X & ", " & point1.Y & "," & w & "," & h & "))"
                    Else
                        Me.CodeTextBox.Text = "Screenshot.CaptureScreenArea(""C:\test." & FileExtension & """, new System.Drawing.Rectangle(" & _
                        point1.X & ", " & point1.Y & "," & w & "," & h & "));"
                        Me.CodeTextBox.Text = CodeTranslator.FixVBStringToCSharpVbLikeString(Me.CodeTextBox.Text)
                    End If
                End If
                Keys.StopHandlers()
                Me.TopAppButton.Enabled = True
                Me.TopMost = False
            End If
        End If
    End Sub
End Class
