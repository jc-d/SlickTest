#Const IncludeWeb = 2 'set to 1 to enable web

#If (IncludeWeb = 1) Then
Public Class IEWebBrowser
    Inherits Window

#Region "Private/Protected"

    Private Const CENTER As Integer = -1
    Private Const FULL_SIZE As Integer = -1
    Private Const LeftButton As Integer = 0
    Private Shared WithEvents IETimer As New System.Timers.Timer()
    Private Shared IETimerHwnd As IntPtr = IntPtr.Zero

    Private Function BuildWebElement(ByVal desc As Object) As WebElement
        Dim win As WebElement = Nothing
        If (Me.reporter Is Nothing) Then
            Log.LogData("Reporter Dead!")
            Throw New SlickTestUIException("Reporter Dead!")
        End If
        Try

            If (TypeOf desc Is String) Then
                win = New WebElement(desc.ToString(), Me.description) ', Values, Names)
            Else
                If (TypeOf desc Is UIControls.Description) Then
                    Dim tmpDesc As UIControls.Description = DirectCast(desc, UIControls.Description)
                    win = New WebElement(tmpDesc, Me.description)
                Else
                    Throw New InvalidCastException("Description was not a valid type")
                End If
            End If
            win.reporter = Me.reporter
            Return win
        Catch ex As Exception
            Throw
        End Try
    End Function

    Protected Function GetIEHwnd() As IntPtr
        If (New IntPtr(Me.Hwnd) <> IntPtr.Zero) Then Return New IntPtr(Me.Hwnd)
        Dim i As IntPtr = WindowsFunctions.SearchForObj(description(0), IntPtr.Zero)
        Return i
    End Function

    Private Shared Sub GetIE(ByVal Hwnd As IntPtr)
        GetIETimer(Hwnd)
        Dim Timeout As Integer = 0
        While (IETimer.Interval = 1)
            System.Threading.Thread.Sleep(40)
            Timeout += 1
            If (Timeout = 20) Then Return
        End While
    End Sub

    Private Shared Sub GetIETimer(ByVal Hwnd As IntPtr)
        IETimerHwnd = Hwnd
        IETimer.Interval = 1
        IETimer.Enabled = True
        IETimer.Start()
    End Sub

    Private Shared Sub IETimer_Elapsed(ByVal sender As Object, ByVal e As System.Timers.ElapsedEventArgs) Handles IETimer.Elapsed
        IETimer.Stop()
        If (IE.TakeOverIESearch(IETimerHwnd) = False) Then
            IETimer.Interval = 2
            Throw New SlickTestUIException("Unable to find IE.")
        End If
        IETimer.Interval = 2
    End Sub

    Private Sub GetIE()
        If (New IntPtr(Me.Hwnd) <> IntPtr.Zero) Then 'already have Hwnd
            GetIE(New IntPtr(Me.Hwnd))
            Return
        End If
        Me.currentHwnd = GetIEHwnd()
        GetIE(New IntPtr(Me.Hwnd))
    End Sub
#End Region

    ''' <summary>
    ''' Closes the web browser.
    ''' </summary>
    ''' <returns>Returns true if the web browser is successfully closed.</returns>
    ''' <remarks></remarks>
    Public Shadows Function Close() As Boolean
        Try
            GetIE()
            Return IE.Close()
        Catch ex As Exception
               Throw
        End Try
        Return False
    End Function

    ''' <summary>
    ''' Presses the Refresh button, which reloads the contents of the webpage.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function Refresh() As Boolean
        Try
            GetIE()
            Return IE.Refresh()
        Catch ex As Exception
                Throw
        End Try
        Return False
    End Function

    ''' <summary>
    ''' Presses the back button.
    ''' </summary>
    ''' <returns>Returns true if the action was successful.</returns>
    ''' <remarks>If back is pressed and there is no page to go
    ''' back to, this function may throw an exception or log an error.</remarks>
    Public Function Back() As Boolean
        Try
            GetIE()
            Return IE.Back()
        Catch ex As Exception
                Throw
        End Try
        Return False
    End Function

    ''' <summary>
    ''' Presses the forward button.
    ''' </summary>
    ''' <returns>Returns true if the action was successful.</returns>
    ''' <remarks>If forward is pressed and there is no page to go
    ''' forward to, this function may throw an exception or log an error.</remarks>
    Public Function Forward() As Boolean
        Try
            GetIE()
            Return IE.Forward()
        Catch ex As Exception
                Throw
        End Try
        Return False
    End Function

    ''' <summary>
    ''' Returns the browser to "home", which is set by the user.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GoHome() As Boolean
        Try
            GetIE()
            Return IE.Home()
        Catch ex As Exception
                Throw
        End Try
        Return False
    End Function

    ''' <summary>
    ''' Gets the current URL in the web browser.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetURL() As String
        Try
            GetIE()
            Return IE.URL()
        Catch ex As Exception
                Throw
        End Try
        Return ""
    End Function

    ''' <summary>
    ''' Gets the current webpage's text.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetPageText() As String
        Try
            GetIE()
            Return IE.PageText()
        Catch ex As Exception
                Throw
        End Try
        Return ""
    End Function

    ''' <summary>
    ''' Gets the current webpage's html.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetPageHTML() As String
        Try
            GetIE()
            Return IE.PageHTML()
        Catch ex As Exception
                Throw
        End Try
        Return ""
    End Function
    ''' <summary>
    ''' Waits till the web browser indicates it has completely loaded.
    ''' </summary>
    ''' <param name="Timeout">Time out in Ms.  If the browser never makes it to the completed
    ''' state in time, then it will exit out of the function and return false</param>
    ''' <returns>Returns true if it enters a completed state before the timeout.</returns>
    ''' <remarks>If the browser is closed or crashes while this function is running,
    ''' it will return false.</remarks>
    Public Function WaitTillCompleted(Optional ByVal Timeout As Integer = 10000) As Boolean
        Try
            GetIE()
            While (IE.State <> APIControls.InternetExplorer.IEState.Complete)
                If (Timeout < 0) Then Return False
                System.Threading.Thread.Sleep(200)
                Timeout -= 210 'approximately the amount of time it 
                'takes to process the state and the pause time.
                'This could be better, but it's good enough for now.
            End While
        Catch ex As Exception
            Return False 'something bad happened, like the browser got closed.
        End Try
        Threading.Thread.Sleep(1000) 'wait one more second, as it takes a while to render what was "loaded"
    End Function

    ''' <summary>
    ''' Returns the current state of the web browser.
    ''' </summary>
    ''' <returns>The following values maybe returned:
    ''' 1 = Complete
    ''' 2 = Interactive
    ''' 3 = Loaded
    ''' 4 = Loading
    ''' 5 = Uninitialized
    ''' 6 = Unknown</returns>
    ''' <remarks></remarks>
    Public Function GetState() As Byte
        Return IE.State
    End Function

    ''' <summary>
    ''' Sets the URL in a instance of Internet Explorer.
    ''' </summary>
    ''' <param name="NewURL">The URL you wish to set.</param>
    ''' <returns>Returns true, if it is successful in setting the URL.</returns>
    ''' <remarks>This does not ensure that the </remarks>
    Public Function SetURL(ByVal NewURL As String) As Boolean
        Try
            GetIE()
            IE.URL = NewURL
            Return True
        Catch ex As Exception
                Throw
        End Try
        Return False
    End Function

#Region "Constructors"
    ''' <summary>
    ''' Creates a WinObject object.  
    ''' Do not directly create this object, instead use InterAct.[Object].WinObject([desc]).[Method]
    ''' </summary>
    ''' <param name="desc"></param>
    ''' <remarks></remarks>
    Protected Friend Sub New(ByVal desc As String)
        description = New System.Collections.Generic.List(Of APIControls.IDescription)
        Dim aDesc As UIControls.Description = UIControls.Description.Create()
        description.Add(aDesc)
        If (readDescription(desc) = False) Then
            Throw New InvalidExpressionException("Description not valid: " & desc)
            'error
        End If
    End Sub


    Protected Friend Sub New(ByVal desc As APIControls.IDescription, ByVal descObj As System.Collections.Generic.List(Of APIControls.IDescription)) ', ByVal Values As System.Collections.Specialized.StringCollection, ByVal Names As System.Collections.Specialized.StringCollection)
        Me.description = descObj
        description.Add(desc)
    End Sub

    ''' <summary>
    ''' Creates a WebBrowser object.  
    ''' Do not directly create this object, instead use InterAct.[Object].WebBrowser([desc]).[Method]
    ''' </summary>
    ''' <remarks></remarks>
    Protected Friend Sub New()
        'this is a do nothing case, to ignore errors...
    End Sub

    Protected Friend Sub New(ByVal desc As String, ByVal descObj As System.Collections.Generic.List(Of APIControls.IDescription)) ', ByVal Values As System.Collections.Specialized.StringCollection, ByVal Names As System.Collections.Specialized.StringCollection)
        Me.description = descObj
        Dim aDesc As UIControls.Description = UIControls.Description.Create()
        description.Add(aDesc)
        If (readDescription(desc) = False) Then
            Throw New InvalidExpressionException("Description not valid: " & desc)
            'error
        End If
    End Sub

    ''' <summary>
    ''' Creates a WebBrowser object.  
    ''' Do not directly create this object, instead use InterAct.WebBrowser([desc]).[Method]
    ''' </summary>
    ''' <param name="desc"></param>
    ''' <remarks></remarks>
    Protected Friend Sub New(ByVal desc As APIControls.IDescription)
        description = New System.Collections.Generic.List(Of APIControls.IDescription)
        description.Add(desc)
    End Sub

#End Region

    ''' <summary>
    ''' Creates a WebElement object.
    ''' </summary>
    ''' <param name="desc">A description of the inner WebElement.</param>
    ''' <returns>returns the WinObject.</returns>
    ''' <remarks>This allows the automation to go down the object tree.  For example:
    ''' InterAct.WebBrowser(MyWindow).WebElement(MyButton).Click()</remarks>
    Public Function WebElement(ByVal desc As Object) As WebElement
        Return BuildWebElement(desc)
    End Function

End Class
#End If