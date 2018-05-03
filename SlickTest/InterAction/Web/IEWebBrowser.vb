''' <summary>
''' WebBrowser object for Internet Explorer.
''' </summary>
''' <remarks></remarks>
Public Class IEWebBrowser
    Inherits Window

#Region "Private/Protected"

    Private Const CENTER As Integer = -1
    Private Const FULL_SIZE As Integer = -1
    Private Const LeftButton As Integer = 0
    Private Shared WithEvents IETimer As New System.Timers.Timer()
    Private Shared IETimerHwnd As IntPtr = IntPtr.Zero

    Protected Function GetIEHwnd() As IntPtr
        If (New IntPtr(Me.Hwnd) <> IntPtr.Zero) Then Return New IntPtr(Me.Hwnd)
        Dim i As IntPtr = WindowsFunctions.SearchForObj(description(0), IntPtr.Zero)
        Return i
    End Function

    Private Shared Sub GetIE(ByVal Hwnd As IntPtr)
        While (Not APIControls.InternetExplorer.TakeOverCompleted = True)
            System.Threading.Thread.Sleep(20)
        End While

        GetIETimer(Hwnd)
        Dim Timeout As Integer = 0
        While (IETimer.Interval = 1)
            System.Threading.Thread.Sleep(200)
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
        Dim takeOverIEResult As Boolean

        SyncLock (IE)
            takeOverIEResult = IE.TakeOverIESearch(IETimerHwnd)
        End SyncLock

        If (takeOverIEResult = False) Then
            IETimer.Interval = 2
            Throw New SlickTestUIException("Unable to find Internet Explorer.")
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

    Private Function ElementBuilder(ByVal desc As Object, ByVal type As AbstractWebObject.ElementTypes) As AbstractWebObject
        Return UIControls.WebElement.BuildElement(desc, type, Me.reporter, Me.description)
    End Function

    Private Function ConvertDescription(ByVal Description As Object) As Description
        If (TypeOf description Is String) Then
            Return UIControls.Description.Create(Description.ToString())
        Else
            If (TypeOf Description Is UIControls.Description) Then
                Return DirectCast(Description, UIControls.Description)
            Else
                Throw New InvalidCastException("Description was not a valid type.  Type: " & Description.GetType().ToString())
            End If
        End If
    End Function

#End Region

    ''' <summary>
    ''' Closes the web browser.
    ''' </summary>
    ''' <returns>Returns true if the web browser is successfully closed.</returns>
    ''' <remarks></remarks>
    Public Shadows Function Close() As Boolean
        GetIE()
        Return IE.Close()
    End Function

    ''' <summary>
    ''' Presses the Refresh button, which reloads the contents of the webpage.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function Refresh() As Boolean
        GetIE()
        Return IE.Refresh()
    End Function

    ''' <summary>
    ''' Presses the back button.
    ''' </summary>
    ''' <returns>Returns true if the action was successful.</returns>
    ''' <remarks>If back is pressed and there is no page to go
    ''' back to, this function may throw an exception or log an error.</remarks>
    Public Function Back() As Boolean
        GetIE()
        Return IE.Back()
    End Function

    ''' <summary>
    ''' Presses the forward button.
    ''' </summary>
    ''' <returns>Returns true if the action was successful.</returns>
    ''' <remarks>If forward is pressed and there is no page to go
    ''' forward to, this function may throw an exception or log an error.</remarks>
    Public Function Forward() As Boolean
        GetIE()
        Return IE.Forward()
    End Function

    ''' <summary>
    ''' Returns the browser to "home", which is set by the user.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GoHome() As Boolean
        GetIE()
        Return IE.Home()
    End Function

    ''' <summary>
    ''' Gets the current URL in the web browser.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetURL() As String
        GetIE()
        Return IE.URL()
    End Function

    ''' <summary>
    ''' Gets the current webpage's text.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetPageText() As String
        GetIE()
        Return IE.PageText()
    End Function

    ''' <summary>
    ''' Gets the current webpage's html.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetPageHTML() As String
        GetIE()
        Return IE.PageHTML()
    End Function

    ''' <summary>
    ''' Waits until the web browser indicates it has completely loaded.
    ''' </summary>
    ''' <param name="Timeout">Time out in Seconds.  If the browser never makes it to the completed
    ''' state in time, then it will exit out of the function and return false</param>
    ''' <returns>Returns true if it enters a completed state before the timeout.</returns>
    ''' <remarks>If the browser is closed or crashes while this function is running,
    ''' it will return false.</remarks>
    Public Function WaitTillCompleted(Optional ByVal Timeout As Integer = 10) As Boolean
        Dim currentTime As System.DateTime = System.DateTime.Now()
        Try
            GetIE()
            While (IE.State <> APIControls.InternetExplorer.IEState.Complete)

                System.Threading.Thread.Sleep(200)
                If ((System.DateTime.Now - currentTime).Seconds > Timeout) Then
                    Return False
                End If
            End While
        Catch ex As Exception
            Return False 'something bad happened, like the browser got closed.
        End Try
        Threading.Thread.Sleep(1000) 'wait one more second, as it takes a while to render what was "loaded"
        Return True
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
        GetIE()
        Return Convert.ToByte(IE.State)
    End Function

    ''' <summary>
    ''' Gets the number of items found in the browser.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks>Excludes elements in a iframe.</remarks>
    Public Function GetWebElementCount() As Integer
        GetIE()
        Return IE.GetBodyParent().GetChildrenCount() + 1
    End Function

    ''' <summary>
    ''' Gets the status text.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks>Typically shown at the bottom left side 
    ''' of the browser</remarks>
    Public Function GetStatusText() As String
        GetIE()
        Return IE.GetStatus()
    End Function

    ''' <summary>
    ''' Sets the URL in a instance of Internet Explorer.
    ''' </summary>
    ''' <param name="NewURL">The URL you wish to set.</param>
    ''' <param name="waitTimeOut">The time you wish to wait, defaulted to 20 seconds</param>
    ''' <returns>Returns true, if the URL is set and the page loads.</returns>
    ''' <remarks>This will wait up to 20 seconds for the page to load</remarks>
    Public Function SetURL(ByVal NewURL As String, Optional ByVal waitTimeOut As Integer = 20) As Boolean
        GetIE()
        IE.URL = NewURL
        Return Me.WaitTillCompleted(waitTimeOut)
    End Function

    ''' <summary>
    ''' Sets the URL in a instance of Internet Explorer.
    ''' </summary>
    ''' <param name="NewURL">The URL you wish to set.</param>
    ''' <remarks>This will wait up to 20 seconds for the page to load</remarks>
    Public Sub SetURLNoWait(ByVal NewURL As String)
        GetIE()
        IE.URL = NewURL
    End Sub

    ''' <summary>
    ''' Finds a element based upon the description given and then creates descriptions for all of the elements below
    ''' that element.
    ''' </summary>
    ''' <param name="DescriptionForFilter">Description to do filtering with.</param>
    ''' <returns>A array of descriptions based upon the first element found that matches the filter.</returns>
    ''' <remarks></remarks>
    Public Function GetWebDescriptionsWithFilter(ByVal DescriptionForFilter As Object) As UIControls.Description()
        Dim list As New System.Collections.Generic.List(Of UIControls.Description)()
        Dim descriptionValue As UIControls.Description = ConvertDescription(DescriptionForFilter)
        GetIE()
        Dim elems As APIControls.WebElementAPI() = IE.FindElements(descriptionValue, Nothing)
        If (elems Is Nothing) Then Return Nothing
        For Each element As APIControls.WebElementAPI In elems
            list.Add(UIControls.Description.ConvertApiToUiDescription(element.CreateFullDescription()))
        Next
        Return list.ToArray()
    End Function

    ''' <summary>
    ''' Gets descriptions for all elements like a certain description.
    ''' </summary>
    ''' <param name="DescriptionForFilter">Description to do filtering with.</param>
    ''' <returns>A array of descriptions based upon the the filter given.</returns>
    ''' <remarks></remarks>
    Public Function GetAllElementsLikeDescription(ByVal DescriptionForFilter As Object) As UIControls.Description()
        Dim list As New System.Collections.Generic.List(Of UIControls.Description)()
        Dim descriptionValue As UIControls.Description = ConvertDescription(DescriptionForFilter)
        GetIE()

        Dim elems As System.Collections.Generic.List(Of APIControls.WebElementAPI) = IE.RecursiveFindElementsSearch(descriptionValue)
        If (elems Is Nothing) Then Return Nothing

        For Each element As APIControls.WebElementAPI In elems
            list.Add(UIControls.Description.ConvertApiToUiDescription(element.CreateFullDescription()))
        Next
        Return list.ToArray()
    End Function

    ''' <summary>
    ''' Gets the number of elements that are like a certain description.
    ''' </summary>
    ''' <param name="DescriptionForFilter">Description to do filtering with.</param>
    ''' <returns>A array of descriptions based upon the the filter given.</returns>
    ''' <remarks></remarks>
    Public Function GetNumberOfElementsLikeDescription(ByVal DescriptionForFilter As Object) As Integer
        Dim descriptionValue As UIControls.Description = ConvertDescription(DescriptionForFilter)
        GetIE()
        Dim elems As System.Collections.Generic.List(Of APIControls.WebElementAPI) = IE.RecursiveFindElementsSearch(descriptionValue)
        If (elems Is Nothing) Then Return Nothing
        Return elems.Count
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

#Region "Factories"
    ''' <summary>
    ''' Creates a WebElement object.
    ''' </summary>
    ''' <param name="desc">A description of the inner WebElement.</param>
    ''' <returns>Returns the WinObject.</returns>
    ''' <remarks>This allows the automation to go down the object tree.  For example:
    ''' New UIControls.InterAct().IEWebBrowser(MyWindow).WebElement(MyButton).Click()</remarks>
    Public Function WebElement(ByVal desc As Object) As WebElement
        Return DirectCast(ElementBuilder(desc, AbstractWebObject.ElementTypes.Element), WebElement)
    End Function

    ''' <summary>
    ''' Creates a WebElement object.
    ''' </summary>
    ''' <param name="desc">A description of the inner WebElement.</param>
    ''' <returns>Returns the WebElement.</returns>
    ''' <remarks>This allows the automation to go down the object tree.  For example:
    ''' New UIControls.InterAct().IEWebBrowser(MyWindow).WebElement(MyElement).Click()</remarks>
    Public Function WebTable(ByVal desc As Object) As WebTable
        Return DirectCast(ElementBuilder(desc, AbstractWebObject.ElementTypes.Table), WebTable)
    End Function

    ''' <summary>
    ''' Creates a WebTableRow object.
    ''' </summary>
    ''' <param name="desc">A description of the WebTableRow.</param>
    ''' <returns>Returns the WebTableRow.</returns>
    ''' <remarks>This allows the automation to go down the object tree.  For example:
    ''' New UIControls.InterAct().IEWebBrowser(MyWindow).WebTableRow(MyTableRow).Click()</remarks>
    Public Function WebTableRow(ByVal desc As Object) As WebTableRow
        Return DirectCast(ElementBuilder(desc, AbstractWebObject.ElementTypes.TableRow), WebTableRow)
    End Function

    ''' <summary>
    ''' Creates a WebTableCell object.
    ''' </summary>
    ''' <param name="desc">A description of the WebTableCell.</param>
    ''' <returns>Returns the WebTableRow.</returns>
    ''' <remarks>This allows the automation to go down the object tree.  For example:
    ''' New UIControls.InterAct().IEWebBrowser(MyWindow).WebTableCell(MyTableCell).Click()</remarks>
    Public Function WebTableCell(ByVal desc As Object) As WebTableCell
        Return DirectCast(ElementBuilder(desc, AbstractWebObject.ElementTypes.TableCell), WebTableCell)
    End Function

    ''' <summary>
    ''' Creates a WebComboBox object.
    ''' </summary>
    ''' <param name="desc">A description of the WebComboBox.</param>
    ''' <returns>Returns the WebComboBox.</returns>
    ''' <remarks>This allows the automation to go down the object tree.  For example:
    ''' New UIControls.InterAct().IEWebBrowser(MyWindow).WebComboBox(MyComboBox).Click()</remarks>
    Public Function WebComboBox(ByVal desc As Object) As WebComboBox
        Return DirectCast(ElementBuilder(desc, AbstractWebObject.ElementTypes.ComboBox), WebComboBox)
    End Function

    ''' <summary>
    ''' Creates a WebDiv object.
    ''' </summary>
    ''' <param name="desc">A description of the WebDiv.</param>
    ''' <returns>Returns the WebDiv.</returns>
    ''' <remarks>This allows the automation to go down the object tree.  For example:
    ''' New UIControls.InterAct().IEWebBrowser(MyWindow).WebDiv(MyDiv).Click()</remarks>
    Public Function WebDiv(ByVal desc As Object) As WebDiv
        Return DirectCast(ElementBuilder(desc, AbstractWebObject.ElementTypes.Div), WebDiv)
    End Function

    ''' <summary>
    ''' Creates a WebSpan object.
    ''' </summary>
    ''' <param name="desc">A description of the WebSpan.</param>
    ''' <returns>Returns the WebSpan.</returns>
    ''' <remarks>This allows the automation to go down the object tree.  For example:
    ''' New UIControls.InterAct().IEWebBrowser(MyWindow).WebSpan(MySpan).Click()</remarks>
    Public Function WebSpan(ByVal desc As Object) As WebSpan
        Return DirectCast(ElementBuilder(desc, AbstractWebObject.ElementTypes.Span), WebSpan)
    End Function

    ''' <summary>
    ''' Creates a WebList object.
    ''' </summary>
    ''' <param name="desc">A description of the WebList.</param>
    ''' <returns>Returns the WebList.</returns>
    ''' <remarks>This allows the automation to go down the object tree.  For example:
    ''' New UIControls.InterAct().IEWebBrowser(MyWindow).WebList(MyWebList).Click()</remarks>
    Public Function WebList(ByVal desc As Object) As WebList
        Return DirectCast(ElementBuilder(desc, AbstractWebObject.ElementTypes.List), WebList)
    End Function

    ''' <summary>
    ''' Creates a WebListItem object.
    ''' </summary>
    ''' <param name="desc">A description of the WebListItem.</param>
    ''' <returns>Returns the WebListItem.</returns>
    ''' <remarks>This allows the automation to go down the object tree.  For example:
    ''' New UIControls.InterAct().IEWebBrowser(MyWindow).WebListItem(MyWebListItem).Click()</remarks>
    Public Function WebListItem(ByVal desc As Object) As WebListItem
        Return DirectCast(ElementBuilder(desc, AbstractWebObject.ElementTypes.ListItem), WebListItem)
    End Function

    ''' <summary>
    ''' Creates a WebLink object.
    ''' </summary>
    ''' <param name="desc">A description of the WebLink.</param>
    ''' <returns>Returns the WebLink.</returns>
    ''' <remarks>This allows the automation to go down the object tree.  For example:
    ''' New UIControls.InterAct().IEWebBrowser(MyWindow).WebLink(MyWebLink).Click()</remarks>
    Public Function WebLink(ByVal desc As Object) As WebLink
        Return DirectCast(ElementBuilder(desc, AbstractWebObject.ElementTypes.Link), WebLink)
    End Function

    ''' <summary>
    ''' Creates a WebCheckbox object.
    ''' </summary>
    ''' <param name="desc">A description of the WebCheckbox.</param>
    ''' <returns>Returns the WebCheckbox.</returns>
    ''' <remarks>This allows the automation to go down the object tree.  For example:
    ''' New UIControls.InterAct().IEWebBrowser(MyWindow).WebCheckbox(MyWebCheckbox).Click()</remarks>
    Public Function WebCheckbox(ByVal desc As Object) As WebCheckbox
        Return DirectCast(ElementBuilder(desc, AbstractWebObject.ElementTypes.Checkbox), WebCheckbox)
    End Function

    ''' <summary>
    ''' Creates a WebGenericInput object.  This includes checkbox, button, hidden
    ''' image, password, radio, reset, submit and text types.
    ''' </summary>
    ''' <param name="desc">A description of the WebGenericInput.</param>
    ''' <returns>Returns the WebGenericInput.</returns>
    ''' <remarks>This allows the automation to go down the object tree.  For example:
    ''' New UIControls.InterAct().IEWebBrowser(MyWindow).WebGenericInput(MyWebCheckbox).Click()</remarks>
    Public Function WebGenericInput(ByVal desc As Object) As WebGenericInput
        Return DirectCast(ElementBuilder(desc, AbstractWebObject.ElementTypes.GenericInput), WebGenericInput)
    End Function

    ''' <summary>
    ''' Creates a WebImage object.
    ''' </summary>
    ''' <param name="desc">A description of the WebImage.</param>
    ''' <returns>Returns the WebImage.</returns>
    ''' <remarks>This allows the automation to go down the object tree.  For example:
    ''' New UIControls.InterAct().IEWebBrowser(MyWindow).WebImage(MyWebImage).Click()</remarks>
    Public Function WebImage(ByVal desc As Object) As WebImage
        Return DirectCast(ElementBuilder(desc, AbstractWebObject.ElementTypes.Image), WebImage)
    End Function

    ''' <summary>
    ''' Creates a WebButton object.
    ''' </summary>
    ''' <param name="desc">A description of the WebButton.</param>
    ''' <returns>Returns the WebButton.</returns>
    ''' <remarks>This allows the automation to go down the object tree.  For example:
    ''' New UIControls.InterAct().IEWebBrowser(MyWindow).WebButton(MyWebButton).Click()</remarks>
    Public Function WebButton(ByVal desc As Object) As WebButton
        Return DirectCast(ElementBuilder(desc, AbstractWebObject.ElementTypes.Button), WebButton)
    End Function

    ''' <summary>
    ''' Creates a WebRadioButton object.
    ''' </summary>
    ''' <param name="desc">A description of the WebRadioButton.</param>
    ''' <returns>Returns the WebRadioButton.</returns>
    ''' <remarks>This allows the automation to go down the object tree.  For example:
    ''' New UIControls.InterAct().IEWebBrowser(MyWindow).WebRadioButton(MyWebRadioButton).Click()</remarks>
    Public Function WebRadioButton(ByVal desc As Object) As WebRadioButton
        Return DirectCast(ElementBuilder(desc, AbstractWebObject.ElementTypes.RadioButton), WebRadioButton)
    End Function

    ''' <summary>
    ''' Creates a WebTextBox object.
    ''' </summary>
    ''' <param name="desc">A description of the WebTextBox.</param>
    ''' <returns>Returns the WebTextBox.</returns>
    ''' <remarks>This allows the automation to go down the object tree.  For example:
    ''' New UIControls.InterAct().IEWebBrowser(MyWindow).WebButton(MyWebTextBox).Click()</remarks>
    Public Function WebTextBox(ByVal desc As Object) As WebTextBox
        Return DirectCast(ElementBuilder(desc, AbstractWebObject.ElementTypes.TextBox), WebTextBox)
    End Function
#End Region

End Class