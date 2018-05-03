'Updated On: 8/23/08

''' <summary>
''' A SwfWinObject is a generic object that allows for some basic abilities you 
''' can perform on ANY object, be it a SwfWindow or a SwfButton.
''' </summary>
''' <remarks>SwfWinObjects only provide very basic functionality, but are designed to allow a use to
''' do 80% of the simple automation most users would require.</remarks>
Public Class SwfWinObject
    Inherits AbstractWinObject
    Protected Friend currentWindowHwnd As IntPtr


#Region "Constructors"
    ''' <summary>
    ''' Creates a SwfWinObject object.  
    ''' Do not directly create this object, instead use InterAct.[Object].WinObject([desc]).[Method]
    ''' </summary>
    ''' <param name="desc"></param>
    ''' <remarks></remarks>
    Protected Friend Sub New(ByVal desc As String)
        setup()
        currentRectangle = New System.Drawing.Rectangle(0, 0, 0, 0)
        description = New System.Collections.Generic.List(Of APIControls.IDescription)
        Dim aDesc As UIControls.Description = UIControls.Description.Create()
        description.Add(aDesc)
        If (readDescription(desc) = False) Then
            Throw New InvalidExpressionException("Description not valid: " & desc)
            'error
        End If
    End Sub


    Protected Friend Sub New(ByVal desc As APIControls.IDescription, ByVal descObj As System.Collections.Generic.List(Of APIControls.IDescription)) ', ByVal Values As System.Collections.Specialized.StringCollection, ByVal Names As System.Collections.Specialized.StringCollection)
        setup()
        Me.description = descObj
        description.Add(desc)
    End Sub

    ''' <summary>
    ''' Creates a SwfWinObject object.  
    ''' Do not directly create this object, instead use InterAct.[Object].SwfWinObject([desc]).[Method]
    ''' </summary>
    ''' <remarks></remarks>
    Protected Friend Sub New()
        'this is a do nothing case, to ignore errors...
    End Sub

    ''' <summary>
    ''' Creates a SwfWinObject object.  
    ''' Do not directly create this object, instead use InterAct.[Object].SwfWinObject([desc]).[Method]
    ''' </summary>
    ''' <param name="desc"></param>
    ''' <remarks></remarks>
    Protected Friend Sub New(ByVal desc As APIControls.IDescription)
        setup()
        description = New System.Collections.Generic.List(Of APIControls.IDescription)
        description.Add(desc)
    End Sub

    ''' <summary>
    ''' Creates a SwfWinObject object.  
    ''' Do not directly create this object, instead use InterAct.[Object].SwfWinObject([desc]).[Method]
    ''' </summary>
    ''' <param name="desc"></param>
    ''' <remarks></remarks>
    Protected Friend Sub New(ByVal desc As System.Collections.Generic.List(Of APIControls.IDescription))
        setup()
        Me.description = desc
    End Sub

    Protected Friend Sub New(ByVal desc As String, ByVal descObj As System.Collections.Generic.List(Of APIControls.IDescription)) ', ByVal Values As System.Collections.Specialized.StringCollection, ByVal Names As System.Collections.Specialized.StringCollection)
        setup()
        Me.description = descObj
        Dim aDesc As UIControls.Description = UIControls.Description.Create()
        description.Add(aDesc)
        If (readDescription(desc) = False) Then
            Throw New InvalidExpressionException("Description not valid: " & desc)
            'error
        End If
    End Sub

#End Region

#Region "Factories - Dot Net Specific."
#Region "Description"
    ''' <summary>
    ''' Creates a SwfWindow object.
    ''' </summary>
    ''' <param name="desc">A description of the inner SwfWindow.</param>
    ''' <returns>returns a SwfWindow object</returns>
    ''' <remarks>This allows the automation to go down the object tree.  For example:
    ''' InterAct.SwfWindow(MyWindow).SwfWindow(InnerWindow).Click</remarks>
    Public Function SwfWindow(ByVal desc As APIControls.IDescription) As SwfWindow
        Return BuildWindow(desc)
    End Function

    ''' <summary>
    ''' Creates a SwfWinObject object.
    ''' </summary>
    ''' <param name="desc">A description of the inner SwfWinObject.</param>
    ''' <returns>returns the SwfWinObject.</returns>
    ''' <remarks>This allows the automation to go down the object tree.  For example:
    ''' InterAct.SwfWindow(MyWindow).SwfWinObject(MyButton).Click()</remarks>
    Public Function SwfWinObject(ByVal desc As APIControls.IDescription) As SwfWinObject
        Return BuildSwfWinObject(desc)
    End Function

    ''' <summary>
    ''' Creates a SwfTextBox object.
    ''' </summary>
    ''' <param name="desc">A description of the inner SwfTextBox.</param>
    ''' <returns>returns the SwfTextBox.</returns>
    ''' <remarks>This allows the automation to go down the object tree.  For example:
    ''' InterAct.SwfWindow(MyWindow).SwfTextBox(MyText).Click()</remarks>
    Public Function SwfTextBox(ByVal desc As APIControls.IDescription) As SwfTextBox
        Dim tmpwin As UIControls.SwfWindow = BuildWindow(desc)
        Return New SwfTextBox(tmpwin)
    End Function

    ''' <summary>
    ''' Creates a SwfButton object.
    ''' </summary>
    ''' <param name="desc">A description of the inner SwfButton.</param>
    ''' <returns>returns the SwfButton.</returns>
    ''' <remarks>This allows the automation to go down the object tree.  For example:
    ''' InterAct.SwfWindow(MyWindow).SwfButton(myButton).Click()</remarks>
    Public Function SwfButton(ByVal desc As APIControls.IDescription) As SwfButton
        Dim tmpwin As UIControls.SwfWindow = BuildWindow(desc)
        Return New SwfButton(tmpwin)
    End Function

    ''' <summary>
    ''' Creates a SwfCheckBox object.
    ''' </summary>
    ''' <param name="desc">A description of the inner SwfCheckBox.</param>
    ''' <returns>returns the SwfCheckBox.</returns>
    ''' <remarks>This allows the automation to go down the object tree.  For example:
    ''' InterAct.SwfWindow(MyWindow).SwfCheckBox(MyCheckBox).Click()</remarks>
    Public Function SwfCheckBox(ByVal desc As APIControls.IDescription) As SwfCheckBox
        Dim tmpwin As UIControls.SwfWindow = BuildWindow(desc)
        Return New SwfCheckBox(tmpwin)
    End Function

    ''' <summary>
    ''' Creates a SwfRadioButton object.
    ''' </summary>
    ''' <param name="desc">A description of the inner SwfRadioButton.</param>
    ''' <returns>returns the SwfRadioButton.</returns>
    ''' <remarks>This allows the automation to go down the object tree.  For example:
    ''' InterAct.SwfWindow(MyWindow).SwfRadioButton(MyRadioButton).Click()</remarks>
    Public Function SwfRadioButton(ByVal desc As APIControls.IDescription) As SwfRadioButton
        Dim tmpwin As UIControls.SwfWindow = BuildWindow(desc)
        Return New SwfRadioButton(tmpwin)
    End Function

    ''' <summary>
    ''' Creates a SwfStaticLabel object.
    ''' </summary>
    ''' <param name="desc">A description of the inner SwfStaticLabel.</param>
    ''' <returns>returns the SwfStaticLabel.</returns>
    ''' <remarks>This allows the automation to go down the object tree.  For example:
    ''' InterAct.SwfWindow(MyWindow).SwfStaticLabel(MyStaticLabel).Click()</remarks>
    Public Function SwfStaticLabel(ByVal desc As APIControls.IDescription) As SwfStaticLabel
        Dim tmpwin As UIControls.SwfWindow = BuildWindow(desc)
        Return New SwfStaticLabel(tmpwin)
    End Function

    ''' <summary>
    ''' Creates a SwfListBox object.
    ''' </summary>
    ''' <param name="desc">A description of the SwfListBox object.</param>
    ''' <returns>returns the SwfListBox.</returns>
    ''' <remarks>This allows the automation to go down the object tree.  For example:
    ''' InterAct.SwfWindow(MyWindow).SwfListBox(myListBox).Click()</remarks>
    Public Function SwfListBox(ByVal desc As APIControls.IDescription) As SwfListBox
        Dim tmpwin As UIControls.SwfWindow = BuildWindow(desc)
        Return New SwfListBox(tmpwin)
    End Function

    ''' <summary>
    ''' Creates a SwfListView object.
    ''' </summary>
    ''' <param name="desc">A description of the SwfListView object.</param>
    ''' <returns>returns the SwfListView.</returns>
    ''' <remarks>This allows the automation to go down the object tree.  For example:
    ''' InterAct.SwfWindow(MyWindow).SwfListView(myListView).Click()</remarks>
    Public Function SwfListView(ByVal desc As APIControls.IDescription) As SwfListView
        Dim tmpwin As UIControls.SwfWindow = BuildWindow(desc)
        Return New SwfListView(tmpwin)
    End Function

    ''' <summary>
    ''' Creates a SwfTreeView object.
    ''' </summary>
    ''' <param name="desc">A description of the SwfTreeView object.</param>
    ''' <returns>returns the SwfTreeView.</returns>
    ''' <remarks>This allows the automation to go down the object tree.  For example:
    ''' InterAct.SwfWindow(MyWindow).SwfTreeView(myTreeView).Click()</remarks>
    Public Function SwfTreeView(ByVal desc As APIControls.IDescription) As SwfTreeView
        Dim tmpwin As UIControls.SwfWindow = BuildWindow(desc)
        Return New SwfTreeView(tmpwin)
    End Function

    ''' <summary>
    ''' Creates a SwfTabControl object.
    ''' </summary>
    ''' <param name="desc">A description of the SwfTabControl object.</param>
    ''' <returns>returns the SwfTabControl.</returns>
    ''' <remarks>This allows the automation to go down the object tree.  For example:
    ''' InterAct.SwfWindow(MyWindow).SwfTabControl(myTabControl).Click()</remarks>
    Public Function SwfTabControl(ByVal desc As APIControls.IDescription) As SwfTabControl
        Dim tmpwin As UIControls.SwfWindow = BuildWindow(desc)
        Return New SwfTabControl(tmpwin)
    End Function

    ''' <summary>
    ''' Creates a SwfComboBox object.
    ''' </summary>
    ''' <param name="desc">A description of the SwfComboBox object.</param>
    ''' <returns>returns the SwfComboBox.</returns>
    ''' <remarks>This allows the automation to go down the object tree.  For example:
    ''' InterAct.SwfWindow(MyWindow).SwfComboBox(myComboBox).Click()</remarks>
    Public Function SwfComboBox(ByVal desc As APIControls.IDescription) As SwfComboBox
        Dim tmpwin As UIControls.SwfWindow = BuildWindow(desc)
        Return New SwfComboBox(tmpwin)
    End Function
#End Region
#Region "String"
    ''' <summary>
    ''' Creates a SwfWindow object.
    ''' </summary>
    ''' <param name="desc">A description of the inner SwfWindow.</param>
    ''' <returns>returns a SwfWindow object</returns>
    ''' <remarks>This allows the automation to go down the object tree.  For example:
    ''' InterAct.SwfWindow(MyWindow).SwfWindow(InnerWindow).Click</remarks>
    Public Function SwfWindow(ByVal desc As String) As SwfWindow
        Return BuildWindow(desc)
    End Function


    ''' <summary>
    ''' Creates a SwfWinObject object.
    ''' </summary>
    ''' <param name="desc">A description of the inner SwfWinObject.</param>
    ''' <returns>returns the SwfWinObject.</returns>
    ''' <remarks>This allows the automation to go down the object tree.  For example:
    ''' InterAct.SwfWindow(MyWindow).SwfWinObject(MyButton).Click()</remarks>
    Public Function SwfWinObject(ByVal desc As String) As SwfWinObject
        Return BuildSwfWinObject(desc)
    End Function

    ''' <summary>
    ''' Creates a SwfTextBox object.
    ''' </summary>
    ''' <param name="desc">A description of the inner SwfTextBox.</param>
    ''' <returns>returns the SwfTextBox.</returns>
    ''' <remarks>This allows the automation to go down the object tree.  For example:
    ''' InterAct.SwfWindow(MyWindow).SwfTextBox(MyText).Click()</remarks>
    Public Function SwfTextBox(ByVal desc As String) As SwfTextBox
        Dim tmpwin As UIControls.SwfWindow = BuildWindow(desc)
        Return New SwfTextBox(tmpwin)
    End Function

    ''' <summary>
    ''' Creates a SwfButton object.
    ''' </summary>
    ''' <param name="desc">A description of the inner SwfButton.</param>
    ''' <returns>returns the SwfButton.</returns>
    ''' <remarks>This allows the automation to go down the object tree.  For example:
    ''' InterAct.SwfWindow(MyWindow).SwfButton(myButton).Click()</remarks>
    Public Function SwfButton(ByVal desc As String) As SwfButton
        Dim tmpwin As UIControls.SwfWindow = BuildWindow(desc)
        Return New SwfButton(tmpwin)
    End Function

    ''' <summary>
    ''' Creates a SwfCheckBox object.
    ''' </summary>
    ''' <param name="desc">A description of the inner SwfCheckBox.</param>
    ''' <returns>returns the SwfCheckBox.</returns>
    ''' <remarks>This allows the automation to go down the object tree.  For example:
    ''' InterAct.SwfWindow(MyWindow).SwfCheckBox(MyCheckBox).Click()</remarks>
    Public Function SwfCheckBox(ByVal desc As String) As SwfCheckBox
        Dim tmpwin As UIControls.SwfWindow = BuildWindow(desc)
        Return New SwfCheckBox(tmpwin)
    End Function

    ''' <summary>
    ''' Creates a SwfRadioButton object.
    ''' </summary>
    ''' <param name="desc">A description of the inner SwfRadioButton.</param>
    ''' <returns>returns the SwfRadioButton.</returns>
    ''' <remarks>This allows the automation to go down the object tree.  For example:
    ''' InterAct.SwfWindow(MyWindow).SwfRadioButton(MyRadioButton).Click()</remarks>
    Public Function SwfRadioButton(ByVal desc As String) As SwfRadioButton
        Dim tmpwin As UIControls.SwfWindow = BuildWindow(desc)
        Return New SwfRadioButton(tmpwin)
    End Function

    ''' <summary>
    ''' Creates a SwfStaticLabel object.
    ''' </summary>
    ''' <param name="desc">A description of the inner SwfStaticLabel.</param>
    ''' <returns>returns the SwfStaticLabel.</returns>
    ''' <remarks>This allows the automation to go down the object tree.  For example:
    ''' InterAct.SwfWindow(MyWindow).SwfStaticLabel(MyStaticLabel).Click()</remarks>
    Public Function SwfStaticLabel(ByVal desc As String) As SwfStaticLabel
        Dim tmpwin As UIControls.SwfWindow = BuildWindow(desc)
        Return New SwfStaticLabel(tmpwin)
    End Function

    ''' <summary>
    ''' Creates a SwfListBox object.
    ''' </summary>
    ''' <param name="desc">A description of the SwfListBox object.</param>
    ''' <returns>returns the SwfListBox.</returns>
    ''' <remarks>This allows the automation to go down the object tree.  For example:
    ''' InterAct.SwfWindow(MyWindow).SwfListBox(myListBox).Click()</remarks>
    Public Function SwfListBox(ByVal desc As String) As SwfListBox
        Dim tmpwin As UIControls.SwfWindow = BuildWindow(desc)
        Return New SwfListBox(tmpwin)
    End Function

    ''' <summary>
    ''' Creates a SwfListView object.
    ''' </summary>
    ''' <param name="desc">A description of the SwfListView object.</param>
    ''' <returns>returns the SwfListView.</returns>
    ''' <remarks>This allows the automation to go down the object tree.  For example:
    ''' InterAct.SwfWindow(MyWindow).SwfListView(myListView).Click()</remarks>
    Public Function SwfListView(ByVal desc As String) As SwfListView
        Dim tmpwin As UIControls.SwfWindow = BuildWindow(desc)
        Return New SwfListView(tmpwin)
    End Function

    ''' <summary>
    ''' Creates a SwfTreeView object.
    ''' </summary>
    ''' <param name="desc">A description of the SwfTreeView object.</param>
    ''' <returns>returns the SwfTreeView.</returns>
    ''' <remarks>This allows the automation to go down the object tree.  For example:
    ''' InterAct.SwfWindow(MyWindow).SwfTreeView(myTreeView).Click()</remarks>
    Public Function SwfTreeView(ByVal desc As String) As SwfTreeView
        Dim tmpwin As UIControls.SwfWindow = BuildWindow(desc)
        Return New SwfTreeView(tmpwin)
    End Function

    ''' <summary>
    ''' Creates a SwfTabControl object.
    ''' </summary>
    ''' <param name="desc">A description of the SwfTabControl object.</param>
    ''' <returns>returns the SwfTabControl.</returns>
    ''' <remarks>This allows the automation to go down the object tree.  For example:
    ''' InterAct.SwfWindow(MyWindow).SwfTabControl(myTabControl).Click()</remarks>
    Public Function SwfTabControl(ByVal desc As String) As SwfTabControl
        Dim tmpwin As UIControls.SwfWindow = BuildWindow(desc)
        Return New SwfTabControl(tmpwin)
    End Function

    ''' <summary>
    ''' Creates a SwfComboBox object.
    ''' </summary>
    ''' <param name="desc">A description of the SwfComboBox object.</param>
    ''' <returns>returns the SwfComboBox.</returns>
    ''' <remarks>This allows the automation to go down the object tree.  For example:
    ''' InterAct.SwfWindow(MyWindow).SwfComboBox(myComboBox).Click()</remarks>
    Public Function SwfComboBox(ByVal desc As String) As SwfComboBox
        Dim tmpwin As UIControls.SwfWindow = BuildWindow(desc)
        Return New SwfComboBox(tmpwin)
    End Function

#End Region
#End Region

#Region "Private/Protected methods - some .net specific code"
    Private Sub setup()
        lastWindowFoundInfo = ""
    End Sub

    Private Function GetMainWindowHwnd() As IntPtr
        If (Me.currentWindowHwnd = IntPtr.Zero) Then
            Return WindowsFunctions.SearchForObj(description(0), IntPtr.Zero)
        Else
            Return Me.currentWindowHwnd
        End If
    End Function
    'TODO: Fix reporting of dead reporters
#Region "Dot Net Specific"


    Private Function BuildSwfWinObject(ByVal desc As Object) As SwfWinObject
        Dim win As SwfWinObject = Nothing
        If (Me.reporter Is Nothing) Then
            Log.LogData("Reporter Dead!") 'Should be handled better
            Throw New SlickTestUIException("Reporter Dead!")
        End If
        Try
            If (TypeOf desc Is String) Then
                win = New SwfWinObject(desc.ToString(), Me.description) ', Values, Names)
            Else
                If (TypeOf desc Is UIControls.Description) Then
                    Dim tmpDesc As UIControls.Description = DirectCast(desc, UIControls.Description)
                    win = New SwfWinObject(tmpDesc, Me.description)
                Else
                    Throw New InvalidCastException("Window description was not a valid type.")
                End If
            End If
            win.reporter = Me.reporter
            Return win
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Protected Friend Function BuildWindow(ByVal desc As Object) As SwfWindow
        Dim win As SwfWindow = Nothing
        If (Me.reporter Is Nothing) Then
            Log.LogData("Reporter Dead!")
            Throw New SlickTestUIException("Reporter Dead!")
        End If
        Try

            If (TypeOf desc Is String) Then
                win = New SwfWindow(desc.ToString(), Me.description) ', Values, Names)
            Else
                If (TypeOf desc Is UIControls.Description) Then
                    Dim tmpDesc As UIControls.Description = DirectCast(desc, UIControls.Description)
                    win = New SwfWindow(tmpDesc, Me.description)
                Else
                    Throw New InvalidCastException("Description was not a valid type")
                End If
            End If
            '//////
            win.reporter = Me.reporter
            Return win
        Catch ex As Exception
            Throw
        End Try
    End Function
#End Region
#End Region


    ''' <summary>
    ''' Closes the main window (or top window) in a object.
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub CloseParent()
        Dim a As New UIControls.Window(description(0)) 'Probably should use some sorta "get parent" tool instead.
        a.Close()
        a = Nothing
    End Sub

    ''' <summary>
    ''' Current text in the WinObject, if it can be gotten.
    ''' </summary>
    ''' <returns>Returns all the text found in the object.  Returns "" if it is unable to find 
    ''' the object.</returns>
    ''' <remarks>WinObjects may perform differently than other types of objects when getting text,
    ''' as WinObjects only performs a generic text gathering method.  This will generally work for
    ''' buttons and windows, but will not work for more complex windows objects.</remarks>
    Public Function GetText() As String
        ExistsWithException()
        Try
            Return WindowsFunctions.GetAllText(currentHwnd)
        Catch ex As Exception
            Throw
        End Try
    End Function

    ''' <summary>
    ''' Types text into a text box or other window's object.
    ''' See <see cref="UIControls.Keyboard.SendKeys">SendKeys</see>.
    ''' </summary>
    ''' <param name="Text">The text you wish to type.</param>
    ''' <remarks></remarks>
    ''' <seealso cref="UIControls.Keyboard.SendKeys"/>
    Public Sub TypeKeys(ByVal Text As String)
        ExistsWithException()
        If (AbstractWinObject.TakePicturesBeforeTyping = True) Then
            Me.CaptureImageForUserInteraction(ImageLocation & GetPictureFileName(), PICTUREBEFORETYPE)
        End If
        Try
            WindowsFunctions.SendTextByHandler(Text, currentHwnd)
        Catch ex As Exception
            'Report(UIControls.Report.Fail, "Failed to perform requested action", _
            '    "Error: " & ex.Message)
            'If (IgnoreInternalErrors = False) Then
            Throw ex
            'End If
            'Return False
        End Try
        If (AbstractWinObject.TakePicturesAfterTyping = True) Then
            Me.CaptureImageForUserInteraction(ImageLocation & GetPictureFileName(), PICTUREAFTERTYPE)
        End If
    End Sub

    ''' <summary>
    ''' Returns the control name if the object is found
    ''' </summary>
    ''' <returns>Returns the name of the control, if the object can be found.  Returns an empty string, if the
    ''' object can't be found.</returns>
    ''' <remarks></remarks>
    Public Function GetControlName() As String
        ExistsWithException()
        Try
            'This seems WRONG...???
            'Return WindowsFunctions.GetControlName(currentHwnd)
            Return WindowsFunctions.GetClassName(currentHwnd)
        Catch ex As Exception
            'Report(UIControls.Report.Fail, "Failed to perform requested action", _
            '    "Error: " & ex.Message)
            Throw
        End Try
    End Function

    ''' <summary>
    ''' Gets the entire list of children objects of the WinObject.  The list
    ''' is not in any particular order.
    ''' </summary>
    ''' <returns>Returns an array of descriptions that are children of
    ''' the current window.</returns>
    ''' <remarks>This converts the list into generic objects, rather than
    ''' description objects for scripting related reasons.</remarks>
    Public Function GetChildDescriptions() As UIControls.Description()
        Dim tmpDescs As New System.Collections.Generic.List(Of UIControls.Description)
        ExistsWithException()
        Try
            For Each handle As IntPtr In APIControls.EnumerateWindows.GetChildHandles(currentHwnd)
                Dim Desc As APIControls.Description = WindowsFunctions.CreateDescriptionFromHwnd(handle)
                Dim Desc2 As UIControls.Description = UIControls.Description.ConvertApiToUiDescription(Desc)
                Dim TmpDesc As UIControls.Description = UIControls.Description.Create()
                For Each item As UIControls.Description.DescriptionData In [Enum].GetValues(GetType(UIControls.Description.DescriptionData))
                    If (Desc2.Contains(item) = True) Then
                        TmpDesc.Add(Desc.GetItemName(item), Desc2.GetItemValue(item))
                    End If
                Next
                TmpDesc.WildCard = Desc.WildCard
                tmpDescs.Add(TmpDesc)
            Next
        Catch ex As Exception
            Throw ex
        End Try
        Return tmpDescs.ToArray()
    End Function


    ''' <summary>
    ''' Sets the text to a specific value, erasing any previous text.
    ''' </summary>
    ''' <param name="text">The text you wish to set for the WinObject.</param>
    ''' <remarks>Not all WinObjects show text or can have text set to them. Any
    ''' errors involving setting text are ignored.</remarks>
    Public Sub SetText(ByVal text As String)
        ExistsWithException()
        Try
            WindowsFunctions.SetText(text, currentHwnd)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    ''' <summary>
    ''' Appends the current text with additional text given.
    ''' </summary>
    ''' <param name="text">The additional text to append to the end of the
    ''' current text.</param>
    ''' <remarks>Not all WinObjects show text or can have text set to them.
    ''' This currently only supports ASCII text.</remarks>
    Public Sub AppendText(ByVal text As String)
        ExistsWithException()
        APIControls.InternalKeyboard.SendChars(text, currentHwnd)
    End Sub


    ''' <summary>
    ''' Returns the index of the WinObject.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks>If this is peformed on a window, the index will always be zero.</remarks>
    Public ReadOnly Property GetIndex() As Integer
        Get
            Dim hwnd As IntPtr = New IntPtr(Me.Hwnd())
            Dim val As Integer = WindowsFunctions.FindIndex(GetMainWindowHwnd(), hwnd)
            If (val = -1) Then
                val = 0
            End If
            Return val
        End Get
    End Property

End Class
