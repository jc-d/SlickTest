''' <summary>
''' A WinObject is a generic object that allows for some basic abilities you can perform on 
''' ANY object, be it a window or a button.
''' </summary>
''' <remarks>WinObjects only provide very basic functionality, but are designed to allow a use to
''' do 80% of the simple automation most users would require.</remarks>
Public Class WinObject
    Inherits AbstractWinObject
    Protected Friend currentWindowHwnd As IntPtr

#Region "Win32 Specific"

    ''' <summary>
    ''' Creates a Menu object.
    ''' </summary>
    ''' <returns>Returns a menu.</returns>
    ''' <remarks></remarks>
    Public ReadOnly Property Menu() As Menu
        Get
            Dim internalMenu As New Menu(Me.description(0))
            internalMenu.reporter = Me.reporter
            Return internalMenu
        End Get
    End Property

#End Region

#Region "Constructors"
    ''' <summary>
    ''' Creates a WinObject object.  
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
        End If
    End Sub

    Protected Friend Sub New(ByVal desc As APIControls.IDescription, ByVal descObj As System.Collections.Generic.List(Of APIControls.IDescription)) ', ByVal Values As System.Collections.Specialized.StringCollection, ByVal Names As System.Collections.Specialized.StringCollection)
        setup()
        Me.description = descObj
        description.Add(desc)
    End Sub

    ''' <summary>
    ''' Creates a WinObject object.  
    ''' Do not directly create this object, instead use InterAct.[Object].WinObject([desc]).[Method]
    ''' </summary>
    ''' <remarks></remarks>
    Protected Friend Sub New()
        'this is a do nothing case, to ignore errors...
    End Sub

    ''' <summary>
    ''' Creates a WinObject object.  
    ''' Do not directly create this object, instead use InterAct.[Object].WinObject([desc]).[Method]
    ''' </summary>
    ''' <param name="desc"></param>
    ''' <remarks></remarks>
    Protected Friend Sub New(ByVal desc As APIControls.IDescription)
        setup()
        description = New System.Collections.Generic.List(Of APIControls.IDescription)
        description.Add(desc)
    End Sub

    ''' <summary>
    ''' Creates a WinObject object.  
    ''' Do not directly create this object, instead use InterAct.[Object].WinObject([desc]).[Method]
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

#Region "Factories"

    ''' <summary>
    ''' Creates a window object.
    ''' </summary>
    ''' <param name="desc">A description of the inner window.</param>
    ''' <returns>Returns a window object</returns>
    ''' <remarks>This allows the automation to go down the object tree.  For example:
    ''' New UIControls.InterAct().Window(MyWindow).Window(InnerWindow).Click</remarks>
    Public Function Window(ByVal desc As APIControls.IDescription) As Window
        Return BuildWindow(desc)
    End Function

    ''' <summary>
    ''' Creates a WinObject object.
    ''' </summary>
    ''' <param name="desc">A description of the inner WinObject.</param>
    ''' <returns>Returns the WinObject.</returns>
    ''' <remarks>This allows the automation to go down the object tree.  For example:
    ''' New UIControls.InterAct().Window(MyWindow).WinObject(MyButton).Click()</remarks>
    Public Function WinObject(ByVal desc As APIControls.IDescription) As WinObject
        Return BuildWinObject(desc)
    End Function

    ''' <summary>
    ''' Creates a TextBox object.
    ''' </summary>
    ''' <param name="desc">A description of the inner TextBox.</param>
    ''' <returns>Returns the TextBox.</returns>
    ''' <remarks>This allows the automation to go down the object tree.  For example:
    ''' New UIControls.InterAct().Window(MyWindow).TextBox(MyText).Click()</remarks>
    Public Function TextBox(ByVal desc As APIControls.IDescription) As TextBox
        Dim tmpwin As UIControls.Window = BuildWindow(desc)
        Return New TextBox(tmpwin)
    End Function

    ''' <summary>
    ''' Creates a Button object.
    ''' </summary>
    ''' <param name="desc">A description of the inner Button.</param>
    ''' <returns>Returns the Button.</returns>
    ''' <remarks>This allows the automation to go down the object tree.  For example:
    ''' New UIControls.InterAct().Window(MyWindow).Button(myButton).Click()</remarks>
    Public Function Button(ByVal desc As APIControls.IDescription) As Button
        Dim tmpwin As UIControls.Window = BuildWindow(desc)
        Return New Button(tmpwin)
    End Function

    ''' <summary>
    ''' Creates a CheckBox object.
    ''' </summary>
    ''' <param name="desc">A description of the inner CheckBox.</param>
    ''' <returns>Returns the CheckBox.</returns>
    ''' <remarks>This allows the automation to go down the object tree.  For example:
    ''' New UIControls.InterAct().Window(MyWindow).CheckBox(MyCheckBox).Click()</remarks>
    Public Function CheckBox(ByVal desc As APIControls.IDescription) As CheckBox
        Dim tmpwin As UIControls.Window = BuildWindow(desc)
        Return New CheckBox(tmpwin)
    End Function

    ''' <summary>
    ''' Creates a RadioButton object.
    ''' </summary>
    ''' <param name="desc">A description of the inner RadioButton.</param>
    ''' <returns>Returns the RadioButton.</returns>
    ''' <remarks>This allows the automation to go down the object tree.  For example:
    ''' New UIControls.InterAct().Window(MyWindow).RadioButton(MyRadioButton).Click()</remarks>
    Public Function RadioButton(ByVal desc As APIControls.IDescription) As RadioButton
        Dim tmpwin As UIControls.Window = BuildWindow(desc)
        Return New RadioButton(tmpwin)
    End Function

    ''' <summary>
    ''' Creates a StaticLabel object.
    ''' </summary>
    ''' <param name="desc">A description of the inner StaticLabel.</param>
    ''' <returns>Returns the StaticLabel.</returns>
    ''' <remarks>This allows the automation to go down the object tree.  For example:
    ''' New UIControls.InterAct().Window(MyWindow).StaticLabel(MyStaticLabel).Click()</remarks>
    Public Function StaticLabel(ByVal desc As APIControls.IDescription) As StaticLabel
        Dim tmpwin As UIControls.Window = BuildWindow(desc)
        Return New StaticLabel(tmpwin)
    End Function

    ''' <summary>
    ''' Creates a ListBox object.
    ''' </summary>
    ''' <param name="desc">A description of the ListBox object.</param>
    ''' <returns>Returns the ListBox.</returns>
    ''' <remarks>This allows the automation to go down the object tree.  For example:
    ''' New UIControls.InterAct().Window(MyWindow).ListBox(myListBox).Click()</remarks>
    Public Function ListBox(ByVal desc As APIControls.IDescription) As ListBox
        Dim tmpwin As UIControls.Window = BuildWindow(desc)
        Return New ListBox(tmpwin)
    End Function

    ''' <summary>
    ''' Creates a ListView object.
    ''' </summary>
    ''' <param name="desc">A description of the ListView object.</param>
    ''' <returns>Returns the ListView.</returns>
    ''' <remarks>This allows the automation to go down the object tree.  For example:
    ''' New UIControls.InterAct().Window(MyWindow).ListView(myListView).Click()</remarks>
    Public Function ListView(ByVal desc As APIControls.IDescription) As ListView
        Dim tmpwin As UIControls.Window = BuildWindow(desc)
        Return New ListView(tmpwin)
    End Function

    ''' <summary>
    ''' Creates a TreeView object.
    ''' </summary>
    ''' <param name="desc">A description of the TreeView object.</param>
    ''' <returns>Returns the TreeView.</returns>
    ''' <remarks>This allows the automation to go down the object tree.  For example:
    ''' New UIControls.InterAct().Window(MyWindow).TreeView(myTreeView).Click()</remarks>
    Public Function TreeView(ByVal desc As APIControls.IDescription) As TreeView
        Dim tmpwin As UIControls.Window = BuildWindow(desc)
        Return New TreeView(tmpwin)
    End Function

    ''' <summary>
    ''' Creates a TabControl object.
    ''' </summary>
    ''' <param name="desc">A description of the TabControl object.</param>
    ''' <returns>Returns the TabControl.</returns>
    ''' <remarks>This allows the automation to go down the object tree.  For example:
    ''' New UIControls.InterAct().Window(MyWindow).TabControl(myTabControl).Click()</remarks>
    Public Function TabControl(ByVal desc As APIControls.IDescription) As TabControl
        Dim tmpwin As UIControls.Window = BuildWindow(desc)
        Return New TabControl(tmpwin)
    End Function

    ''' <summary>
    ''' Creates a ComboBox object.
    ''' </summary>
    ''' <param name="desc">A description of the ComboBox object.</param>
    ''' <returns>Returns the ComboBox.</returns>
    ''' <remarks>This allows the automation to go down the object tree.  For example:
    ''' New UIControls.InterAct().Window(MyWindow).ComboBox(myComboBox).Click()</remarks>
    Public Function ComboBox(ByVal desc As APIControls.IDescription) As ComboBox
        Dim tmpwin As UIControls.Window = BuildWindow(desc)
        Return New ComboBox(tmpwin)
    End Function
#Region "Description"

#End Region
#Region "String"
    ''' <summary>
    ''' Creates a window object.
    ''' </summary>
    ''' <param name="desc">A description of the inner window.</param>
    ''' <returns>Returns a window object</returns>
    ''' <remarks>This allows the automation to go down the object tree.  For example:
    ''' New UIControls.InterAct().Window(MyWindow).Window(InnerWindow).Click</remarks>
    Public Function Window(ByVal desc As String) As Window
        Return BuildWindow(desc)
    End Function

    ''' <summary>
    ''' Creates a WinObject object.
    ''' </summary>
    ''' <param name="desc">A description of the inner WinObject.</param>
    ''' <returns>Returns the WinObject.</returns>
    ''' <remarks>This allows the automation to go down the object tree.  For example:
    ''' New UIControls.InterAct().Window(MyWindow).WinObject(MyButton).Click()</remarks>
    Public Function WinObject(ByVal desc As String) As WinObject
        Return BuildWinObject(desc)
    End Function

    ''' <summary>
    ''' Creates a TextBox object.
    ''' </summary>
    ''' <param name="desc">A description of the inner TextBox.</param>
    ''' <returns>Returns the TextBox.</returns>
    ''' <remarks>This allows the automation to go down the object tree.  For example:
    ''' New UIControls.InterAct().Window(MyWindow).TextBox(MyText).Click()</remarks>
    Public Function TextBox(ByVal desc As String) As TextBox
        Dim tmpwin As UIControls.Window = BuildWindow(desc)
        Return New TextBox(tmpwin)
    End Function

    ''' <summary>
    ''' Creates a Button object.
    ''' </summary>
    ''' <param name="desc">A description of the inner Button.</param>
    ''' <returns>Returns the Button.</returns>
    ''' <remarks>This allows the automation to go down the object tree.  For example:
    ''' New UIControls.InterAct().Window(MyWindow).Button(myButton).Click()</remarks>
    Public Function Button(ByVal desc As String) As Button
        Dim tmpwin As UIControls.Window = BuildWindow(desc)
        Return New Button(tmpwin)
    End Function

    ''' <summary>
    ''' Creates a CheckBox object.
    ''' </summary>
    ''' <param name="desc">A description of the inner CheckBox.</param>
    ''' <returns>Returns the CheckBox.</returns>
    ''' <remarks>This allows the automation to go down the object tree.  For example:
    ''' New UIControls.InterAct().Window(MyWindow).CheckBox(MyCheckBox).Click()</remarks>
    Public Function CheckBox(ByVal desc As String) As CheckBox
        Dim tmpwin As UIControls.Window = BuildWindow(desc)
        Return New CheckBox(tmpwin)
    End Function

    ''' <summary>
    ''' Creates a RadioButton object.
    ''' </summary>
    ''' <param name="desc">A description of the inner RadioButton.</param>
    ''' <returns>Returns the RadioButton.</returns>
    ''' <remarks>This allows the automation to go down the object tree.  For example:
    ''' New UIControls.InterAct().Window(MyWindow).RadioButton(MyRadioButton).Click()</remarks>
    Public Function RadioButton(ByVal desc As String) As RadioButton
        Dim tmpwin As UIControls.Window = BuildWindow(desc)
        Return New RadioButton(tmpwin)
    End Function

    ''' <summary>
    ''' Creates a StaticLabel object.
    ''' </summary>
    ''' <param name="desc">A description of the inner StaticLabel.</param>
    ''' <returns>Returns the StaticLabel.</returns>
    ''' <remarks>This allows the automation to go down the object tree.  For example:
    ''' New UIControls.InterAct().Window(MyWindow).StaticLabel(MyStaticLabel).Click()</remarks>
    Public Function StaticLabel(ByVal desc As String) As StaticLabel
        Dim tmpwin As UIControls.Window = BuildWindow(desc)
        Return New StaticLabel(tmpwin)
    End Function

    ''' <summary>
    ''' Creates a ListBox object.
    ''' </summary>
    ''' <param name="desc">A description of the ListBox object.</param>
    ''' <returns>Returns the ListBox.</returns>
    ''' <remarks>This allows the automation to go down the object tree.  For example:
    ''' New UIControls.InterAct().Window(MyWindow).ListBox(myListBox).Click()</remarks>
    Public Function ListBox(ByVal desc As String) As ListBox
        Dim tmpwin As UIControls.Window = BuildWindow(desc)
        Return New ListBox(tmpwin)
    End Function

    ''' <summary>
    ''' Creates a ListView object.
    ''' </summary>
    ''' <param name="desc">A description of the ListView object.</param>
    ''' <returns>Returns the ListView.</returns>
    ''' <remarks>This allows the automation to go down the object tree.  For example:
    ''' New UIControls.InterAct().Window(MyWindow).ListView(myListView).Click()</remarks>
    Public Function ListView(ByVal desc As String) As ListView
        Dim tmpwin As UIControls.Window = BuildWindow(desc)
        Return New ListView(tmpwin)
    End Function

    ''' <summary>
    ''' Creates a TreeView object.
    ''' </summary>
    ''' <param name="desc">A description of the TreeView object.</param>
    ''' <returns>Returns the TreeView.</returns>
    ''' <remarks>This allows the automation to go down the object tree.  For example:
    ''' New UIControls.InterAct().Window(MyWindow).TreeView(myTreeView).Click()</remarks>
    Public Function TreeView(ByVal desc As String) As TreeView
        Dim tmpwin As UIControls.Window = BuildWindow(desc)
        Return New TreeView(tmpwin)
    End Function

    ''' <summary>
    ''' Creates a TabControl object.
    ''' </summary>
    ''' <param name="desc">A description of the TabControl object.</param>
    ''' <returns>Returns the TabControl.</returns>
    ''' <remarks>This allows the automation to go down the object tree.  For example:
    ''' New UIControls.InterAct().Window(MyWindow).TabControl(myTabControl).Click()</remarks>
    Public Function TabControl(ByVal desc As String) As TabControl
        Dim tmpwin As UIControls.Window = BuildWindow(desc)
        Return New TabControl(tmpwin)
    End Function

    ''' <summary>
    ''' Creates a ComboBox object.
    ''' </summary>
    ''' <param name="desc">A description of the ComboBox object.</param>
    ''' <returns>Returns the ComboBox.</returns>
    ''' <remarks>This allows the automation to go down the object tree.  For example:
    ''' New UIControls.InterAct().Window(MyWindow).ComboBox(myComboBox).Click()</remarks>
    Public Function ComboBox(ByVal desc As String) As ComboBox
        Dim tmpwin As UIControls.Window = BuildWindow(desc)
        Return New ComboBox(tmpwin)
    End Function
#End Region
#End Region

#Region "Private/Protected methods"
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

    Private Function BuildWinObject(ByVal desc As Object) As WinObject
        Dim win As WinObject = Nothing
        If (Me.reporter Is Nothing) Then
            Log.LogData("Reporter Dead!") 'Should be handled better
            Throw New SlickTestUIException("Reporter dead")
        End If
        Try
            If (TypeOf desc Is String) Then
                win = New WinObject(desc.ToString(), Me.description) ', Values, Names)
            Else
                If (TypeOf desc Is UIControls.Description) Then
                    Dim tmpDesc As UIControls.Description = DirectCast(desc, UIControls.Description)
                    win = New WinObject(tmpDesc, Me.description)
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

    Protected Friend Function BuildWindow(ByVal desc As Object) As Window
        Dim win As Window = Nothing
        If (Me.reporter Is Nothing) Then
            Log.LogData("Reporter Dead!")
            Throw New SlickTestUIException("Reporter dead")
        End If
        Try

            If (TypeOf desc Is String) Then
                win = New Window(desc.ToString(), Me.description) ', Values, Names)
            Else
                If (TypeOf desc Is UIControls.Description) Then
                    Dim tmpDesc As UIControls.Description = DirectCast(desc, UIControls.Description)
                    win = New Window(tmpDesc, Me.description)
                Else
                    Throw New InvalidCastException("Description was not a valid type")
                End If
            End If
            win.reporter = Me.reporter
            Return win
        Catch ex As Exception
            Throw ex
        End Try
    End Function
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
            Throw ex
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
            Throw ex
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
    ''' <remarks></remarks>
    Public Function GetChildDescriptions() As UIControls.Description()
        Dim tmpDescs As New System.Collections.Generic.List(Of UIControls.Description)
        ExistsWithException()
        Try
            For Each handle As IntPtr In APIControls.EnumerateWindows.GetChildHandles(currentHwnd)
                If (handle = IntPtr.Zero) Then Continue For
                Dim Desc As APIControls.Description = WindowsFunctions.CreateDescriptionFromHwnd(handle, True)
                Dim Desc2 As UIControls.Description = UIControls.Description.ConvertApiToUiDescription(Desc)
                Dim TmpDesc As UIControls.Description = UIControls.Description.Create()
                For Each item As UIControls.Description.DescriptionData In [Enum].GetValues(GetType(UIControls.Description.DescriptionData))
                    If (Desc2.Contains(item) = True) Then
                        TmpDesc.Add(Desc.GetItemName(DirectCast(item, APIControls.Description.DescriptionData)), Desc2.GetItemValue(item))
                    End If
                Next
                If (TmpDesc.Hwnd = IntPtr.Zero) Then Continue For
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
        APIControls.InternalKeyboard.SendChars(text, New IntPtr(Me.Hwnd))
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