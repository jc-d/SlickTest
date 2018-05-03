''' <summary>
''' The Abstract Web is the set of abilities all Web object types must have.
''' </summary>
''' <remarks></remarks>
Public MustInherit Class AbstractWebObject
    Protected Friend description As System.Collections.Generic.List(Of APIControls.IDescription)
    Protected Friend IEHwnd As New IntPtr(0) 'may not be needed.
    Protected Friend reporter As UIControls.IReport
    Protected Friend currentRectangle As System.Drawing.Rectangle
    Friend Shared WindowsFunctions As New APIControls.IndependentWindowsFunctionsv1()
    Private WithEvents IETimer As New System.Timers.Timer()
    Friend Shared IE As New APIControls.InternetExplorer()
    Friend CurrentElement As APIControls.WebElementAPI = Nothing
    Public MustOverride ReadOnly Property ListOfSupportedHtmlTags() As System.Collections.Generic.List(Of String)
    Protected Friend HtmlTags As New System.Collections.Generic.List(Of String)
    Friend Enum ElementTypes
        Element
        Table
        TableRow
        TableCell
        Div
        Span
        ComboBox
        ListItem
        List
        Link
        Checkbox
        GenericInput
        Image
        Button
        TextBox
        RadioButton
    End Enum

#Region "Constructor"

    ''' <summary>
    ''' Not used.
    ''' </summary>
    ''' <remarks></remarks>
    Protected Friend Sub New()
    End Sub

#End Region

#Region "Private/Protected"
    Private Sub GetIE(ByVal Hwnd As IntPtr)
        GetIETimer(Hwnd)
        Dim Timeout As Integer = 0
        While (IETimer.Interval = 1)
            System.Threading.Thread.Sleep(40)
            Timeout += 1
            If (Timeout = 20) Then Return
        End While
    End Sub

    Private Sub GetIETimer(ByVal Hwnd As IntPtr)
        IEHwnd = Hwnd
        IETimer.Interval = 1
        IETimer.Enabled = True
        IETimer.Start()
    End Sub

    Private Sub IETimer_Elapsed(ByVal sender As Object, ByVal e As System.Timers.ElapsedEventArgs) Handles IETimer.Elapsed
        IETimer.Stop()
        Dim takeOverIEResult As Boolean
        SyncLock (IE)
            takeOverIEResult = IE.TakeOverIESearch(IEHwnd)
        End SyncLock

        If (takeOverIEResult = False) Then
            IETimer.Interval = 2
            Throw New SlickTestUIException("Unable to find Internet Explorer.")
        End If
        IETimer.Interval = 2
    End Sub

    Protected Friend Sub GetIE()
        If (IEHwnd <> IntPtr.Zero) Then 'already have Hwnd
            GetIE(IEHwnd)
            Return
        End If

        IEHwnd = GetIEHwnd()
        GetIE(IEHwnd)
    End Sub

    Protected Function GetIEHwnd() As IntPtr
        If (IEHwnd <> IntPtr.Zero) Then Return IEHwnd
        Dim internalHwnd As IntPtr = WindowsFunctions.SearchForObj(description(0), IntPtr.Zero)
        Return internalHwnd
    End Function

    Private Function TestExists() As Boolean
        If (IEHwnd = IntPtr.Zero) Then
            'we need to get IE...
            GetIE()
        End If
        Try
            CurrentElement = Nothing
            CurrentElement = IE.FindElement(ElementDescriptionToFindInExists(), ElementDescriptionsForFormerElements())
            If (CurrentElement Is Nothing) Then
                Return False
            End If
            Return True
        Catch ex As Exception
            Log.LogData("Exception in TestExists For Web: " & ex.ToString(), 3)
        End Try
        Return False
    End Function

    Protected Function ElementDescriptionsForFormerElements() As APIControls.IDescription()
        If (description.Count - 3 < 0) Then Return Nothing
        Dim descs(description.Count - 3) As APIControls.IDescription
        For i As Integer = 1 To description.Count - 2
            descs(i - 1) = description(i)
        Next
        Return descs
    End Function

    Protected Function ElementDescriptionToFindInExists() As APIControls.IDescription
        Return description(description.Count - 1)
    End Function

    Protected Sub Report(ByVal Type As Byte, ByVal MajorMessage As String, ByVal MinorMessage As String)
        reporter.RecordEvent(Type, MinorMessage, MajorMessage)
    End Sub

    Protected Function readDescription(ByVal desc As String) As Boolean
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
            'index2 = desc.Length '+ 1
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

#End Region

    ''' <summary>
    ''' Returns a string of the current description.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Overrides Function ToString() As String
        Dim s As New System.Text.StringBuilder()
        For i As Integer = 0 To description.Count - 1
            s.Append(description(i).ToString())
        Next
        Return s.ToString
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
    ''' if(Me.WebElement(MyElement).Exists(10)=true) then<p/>
    '''   MsgBox "Window was found with in 10 seconds"<p/>
    ''' else<p/>
    '''   MsgBox "After 10 seconds, the window was never found"<p/>
    ''' end if<p/>
    ''' </remarks>
    Public Function Exists(ByVal Time As Integer) As Boolean
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

    ''' <summary>
    ''' Test to see if an object can be found in a certain amount of time.
    ''' </summary>
    ''' <returns>Returns true if the object was found, false if it was not.</returns>
    ''' <remarks>
    ''' This method, when passing in a time, does not ensure a
    ''' window will be found if the window appears and disappears
    ''' quickly.  In those case, it is recommended you create 
    ''' your own loop and perform a exists(0)<p/><p/>
    '''   Example Code: <p/>
    ''' if(Me.WebElement(MyElement).Exists(10)=true) then<p/>
    '''   MsgBox "Window was found with in 10 seconds"<p/>
    ''' else<p/>
    '''   MsgBox "After 10 seconds, the window was never found"<p/>
    ''' end if<p/>
    ''' </remarks>
    Public Function Exists() As Boolean
        Return Exists(0)
    End Function

    ''' <summary>
    ''' The amount of time to wait for an item to appear (exist).
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Protected Friend Shared Property ExistTimeout() As Integer
        Get
            Return AbstractWinObject.ExistTimeout()
        End Get
        Set(ByVal value As Integer)
            AbstractWinObject.ExistTimeout = value
        End Set
    End Property

    Public Sub ExistsWithException()
        If (Not CurrentElement Is Nothing) Then Return
        If (Exists(ExistTimeout) = False) Then
            Throw New SlickTestUIException("Failed to find object: " & ElementDescriptionToFindInExists().ToString())
        End If
    End Sub
End Class