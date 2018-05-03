'Updated On: 8/23/08

''' <summary>
''' A window is just a specialized WinObject, and it performs everything a WinObject.
''' </summary>
''' <remarks></remarks>
Public Class Window
    Inherits WinObject

#Region "Constructors"

    ''' <summary>
    ''' Creates a WinObject object.  
    ''' Do not directly create this object, instead use InterAct.[Object].WinObject([desc]).[Method]
    ''' </summary>
    ''' <param name="desc"></param>
    ''' <remarks></remarks>
    Public Sub New(ByVal desc As String)
        MyBase.New(desc)
    End Sub
    'Protected Friend Sub New(ByVal desc As String)
    '    MyBase.New(desc)
    'End Sub

    Protected Friend Sub New(ByVal desc As APIControls.IDescription, ByVal descObj As System.Collections.Generic.List(Of APIControls.IDescription)) ', ByVal Values As System.Collections.Specialized.StringCollection, ByVal Names As System.Collections.Specialized.StringCollection)
        MyBase.New(desc, descObj)
    End Sub

    ''' <summary>
    ''' Creates a Window object.  
    ''' Do not directly create this object, instead use InterAct.[Object].Window([desc]).[Method]
    ''' </summary>
    ''' <remarks></remarks>
    Protected Friend Sub New()
        MyBase.New()
        'this is a do nothing case, to ignore errors...
    End Sub

    ''' <summary>
    ''' Creates a Window object.  
    ''' Do not directly create this object, instead use InterAct.[Object].Window([desc]).[Method]
    ''' </summary>
    ''' <param name="desc"></param>
    ''' <remarks></remarks>
    Protected Friend Sub New(ByVal desc As APIControls.IDescription)
        MyBase.New(desc)
    End Sub

    ''' <summary>
    ''' Creates a Window object.  
    ''' Do not directly create this object, instead use InterAct.[Object].Window([desc]).[Method]
    ''' </summary>
    ''' <param name="desc"></param>
    ''' <remarks></remarks>
    Protected Friend Sub New(ByVal desc As System.Collections.Generic.List(Of APIControls.IDescription))
        MyBase.New(desc)
    End Sub

    Protected Friend Sub New(ByVal desc As String, ByVal descObj As System.Collections.Generic.List(Of APIControls.IDescription)) ', ByVal Values As System.Collections.Specialized.StringCollection, ByVal Names As System.Collections.Specialized.StringCollection)
        MyBase.New(desc, descObj)
    End Sub

#End Region

#Region "Private/Protected Methods"
    Private Function GetMainWindowHwnd() As IntPtr
        If (Me.currentWindowHwnd = IntPtr.Zero) Then
            Return WindowsFunctions.SearchForObj(description(0), IntPtr.Zero)
        Else
            Return Me.currentWindowHwnd
        End If
    End Function

#End Region

    ''' <summary>
    ''' Close sends a close command to the window, but does not confirm the window is closed..
    ''' </summary>
    ''' <remarks>Close may fail to close the window if a message box, such as "Save Changes to the document?"
    ''' appears.</remarks>
    Public Sub Close()
        WindowsFunctions.CloseWindow(New IntPtr(Me.Hwnd))
    End Sub

    ''' <summary>
    ''' CloseAll sends a close command to all the windows with the description given.  This does not confirm the window is closed.
    ''' </summary>
    ''' <remarks>CloseAll may fail to close one or more windows if a message box, such as a "Save Changes to the document?"
    ''' appears from the app.</remarks>
    Public Sub CloseAll()
        For Each ObjHwnd As IntPtr In Me.GetObjectHwnds()
            WindowsFunctions.CloseWindow(ObjHwnd)
        Next
    End Sub

    ''' <summary>
    ''' Checks a window to see if it has a maximize button.
    ''' </summary>
    ''' <returns>Returns true if it has a maximize button and false
    ''' if it does not or if the window could not be found.</returns>
    ''' <remarks></remarks>
    Public Function HasMaximizeButton() As Boolean
        ExistsWithException()
        Try
            If (WindowsFunctions.Window.IsWindow(currentHwnd)) Then
                Return WindowsFunctions.Window.HasMax(currentHwnd)
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    ''' <summary>
    ''' Checks a window to see if it has a minimize button.
    ''' </summary>
    ''' <returns>Returns true if it has a minimize button and false
    ''' if it does not or if the window could not be found.</returns>
    ''' <remarks></remarks>
    Public Function HasMinimizeButton() As Boolean
        ExistsWithException()
        Try
            If (WindowsFunctions.Window.IsWindow(currentHwnd)) Then
                Return WindowsFunctions.Window.HasMin(currentHwnd)
            End If
        Catch ex As Exception
            Throw ex
        End Try
        Return False
    End Function

    ''' <summary>
    ''' Checks a window to see if it has a title bar.
    ''' </summary>
    ''' <returns>Returns true if it has a title bar and false
    ''' if it does not or if the window could not be found.</returns>
    ''' <remarks></remarks>
    Public Function HasTitleBar() As Boolean
        ExistsWithException()
        Try
            If (WindowsFunctions.Window.IsWindow(currentHwnd)) Then
                Return WindowsFunctions.Window.HasTitleBar(currentHwnd)
            End If
        Catch ex As Exception
            Throw ex
        End Try
        Return False
    End Function

    ''' <summary>
    ''' Checks a window to see if it has a border.
    ''' </summary>
    ''' <returns>Returns true if it has a border and false
    ''' if it does not or if the window could not be found.</returns>
    ''' <remarks></remarks>
    Public Function HasBorder() As Boolean
        ExistsWithException()
        Try
            If (WindowsFunctions.Window.IsWindow(currentHwnd)) Then
                Return WindowsFunctions.Window.HasBorder(currentHwnd)
            End If
        Catch ex As Exception
            Throw ex
        End Try
        Return False
    End Function

    ''' <summary>
    ''' Returns the state of the window.  The possible states are
    ''' minimized, maximized and normal.
    ''' </summary>
    ''' <returns>Returns the Window state.</returns>
    ''' <remarks></remarks>
    Public Function GetWindowState() As System.Windows.Forms.FormWindowState
        Dim wp As New WinAPI.API.WINDOWPLACEMENT()
        wp.length = System.Runtime.InteropServices.Marshal.SizeOf(wp)
        WinAPI.API.GetWindowPlacement(New IntPtr(Me.Hwnd()), wp)
        Select Case wp.showCmd Mod 4
            Case 2
                Return Windows.Forms.FormWindowState.Minimized
            Case 3
                Return Windows.Forms.FormWindowState.Maximized
            Case Else
                Return Windows.Forms.FormWindowState.Normal
        End Select
        Return Windows.Forms.FormWindowState.Normal
    End Function

    ''' <summary>
    ''' Sets the windows state to maximized, minimized or normal.
    ''' </summary>
    ''' <param name="State">The state you are going to change the window to.</param>
    ''' <remarks></remarks>
    Public Sub SetWindowState(ByVal State As System.Windows.Forms.FormWindowState)
        Dim Command As Integer = 1
        Select Case State
            Case Windows.Forms.FormWindowState.Maximized
                Command = 3
            Case Windows.Forms.FormWindowState.Minimized
                Command = 2
            Case Windows.Forms.FormWindowState.Normal
                Command = 1
            Case Else
                Throw New SlickTestUIException("Unable to set to state " & State.ToString())
        End Select
        WinAPI.API.ShowWindow(New IntPtr(Me.Hwnd()), Command)
    End Sub

    ''' <summary>
    ''' Sets a window active.
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub SetActive()
        Me.AppActivateByHwnd(Me.Hwnd)
    End Sub

    ''' <summary>
    ''' Gets the currently active window's Hwnd.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks>Provided to test to see what window is currently active.</remarks>
    Public Shared Function GetActiveWindow() As IntPtr
        Return WindowsFunctions.Window.GetActiveWindow()
    End Function

    ''' <summary>
    ''' Moves a window to a certain location onto the screen.
    ''' </summary>
    ''' <param name="X">The X location to move the window to.</param>
    ''' <param name="Y">The Y location to move the window to.</param>
    ''' <remarks>This method will set the window to the normal state.</remarks>
    Public Sub Move(ByVal X As Integer, ByVal Y As Integer)
        Dim HwndVal As IntPtr = New IntPtr(Me.Hwnd())
        WinAPI.API.ShowWindow(HwndVal, 1) ' 1 = normal
        If (WinAPI.API.SetWindowPos(HwndVal, WinAPI.API.SetWindowPosZOrder.HWND_TOP, X, Y, 0, 0, WinAPI.API.SetWindowPosFlags.SWP_NOSIZE) = 0) Then
            Throw New SlickTestUIException("Unable to move window.")
        End If
    End Sub

    ''' <summary>
    ''' Resizes a window.
    ''' </summary>
    ''' <param name="Width">The Width of the window.</param>
    ''' <param name="Height">The Height of the window.</param>
    ''' <remarks>This method will set the window to the normal state.</remarks>
    Public Sub SetSize(ByVal Width As Integer, ByVal Height As Integer)
        Dim HwndVal As IntPtr = New IntPtr(Me.Hwnd())
        WinAPI.API.ShowWindow(HwndVal, 1) ' 1 = normal
        If (WinAPI.API.SetWindowPos(HwndVal, WinAPI.API.SetWindowPosZOrder.HWND_TOP, 0, 0, Width, Height, WinAPI.API.SetWindowPosFlags.SWP_NOMOVE) = 0) Then
            Throw New SlickTestUIException("Unable to resize window.")
        End If
    End Sub

    ''' <summary>
    ''' Resizes a window.
    ''' </summary>
    ''' <param name="Size">The size of the window.</param>
    ''' <remarks>This method will set the window to the normal state.</remarks>
    Public Sub SetSize(ByVal Size As System.Drawing.Size)
        SetSize(size.Width, size.Height)
    End Sub

    ''' <summary>
    ''' Gets the entire data set for all objects found within the given window.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks>This method maybe take a while.</remarks>
    Public Function DumpWindowData() As String
        Dim window As New System.Text.StringBuilder(10000)
        Dim items As System.Collections.Generic.Dictionary(Of String, System.Collections.Generic.Dictionary(Of String, String))
        items = DumpWindowDataAsDictionary()

        For Each item As String In items.Keys
            window.AppendLine(item)
            For Each description As String In items(item).Keys
                window.AppendLine(vbTab & description & "='" & items(item)(description) & "'")
            Next
        Next
        Return window.ToString()
    End Function

    ''' <summary>
    ''' Gets the entire data set for all objects found within the given WebElement.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks>This method maybe take a while.</remarks>
    Public Function DumpWindowDataAsDictionary() As System.Collections.Generic.Dictionary(Of String, System.Collections.Generic.Dictionary(Of String, String))
        Dim itemCount As Integer = 1
        Dim items As New System.Collections.Generic.Dictionary(Of String, System.Collections.Generic.Dictionary(Of String, String))
        ExistsWithException()

        Dim MainDesc As APIControls.Description = WindowsFunctions.CreateDescriptionFromHwnd(New IntPtr(Me.Hwnd))

        items = BuildDescribingAsDictionary(items, MainDesc, itemCount)
        Dim ChildDescriptions As Description() = Me.GetChildDescriptions()
        If (ChildDescriptions Is Nothing) Then Return items 'window.ToString()

        For Each desc As Description In ChildDescriptions
            itemCount = itemCount + 1
            items = BuildDescribingAsDictionary(items, desc, itemCount)
            Me.description.RemoveRange(1, Me.description.Count - 1)
        Next
        Return items
    End Function

    Private Function BuildDescribingAsDictionary(ByRef internalDictionary As System.Collections.Generic.Dictionary(Of String, System.Collections.Generic.Dictionary(Of String, String)), ByVal desc As APIControls.Description, ByVal itemCount As Integer) As System.Collections.Generic.Dictionary(Of String, System.Collections.Generic.Dictionary(Of String, String))
        Dim valueToAddToList As New System.Collections.Generic.Dictionary(Of String, String)
        internalDictionary.Add("Item # " & itemCount, valueToAddToList)

        For Each item As APIControls.Description.DescriptionData In [Enum].GetValues(GetType(APIControls.Description.DescriptionData))
            If (desc.Contains(item)) Then
                valueToAddToList.Add(desc.GetItemName(item), desc.GetItemValue(item))
            End If
        Next

        valueToAddToList.Add("Style", Me.GetStyle().ToString())
        valueToAddToList.Add("StyleEx", Me.GetStyleEx().ToString())

        Dim hwndDescription As String = "hwnd:=""" & desc.Hwnd.ToString() & """"
        Select Case desc.WindowType().ToLowerInvariant()
            Case "listbox"
                Dim LstBox As UIControls.ListBox = ListBox(hwndDescription)
                If (LstBox Is Nothing) Then Return internalDictionary
                valueToAddToList.Add("ItemCount", LstBox.GetItemCount().ToString())
                If (LstBox.GetItemCount() > 1) Then
                    valueToAddToList.Add("(Sample)Item(0)", LstBox.GetItemByIndex(0))
                End If
                valueToAddToList.Add("SelectCount", LstBox.GetSelectCount().ToString())
                valueToAddToList.Add("SelectedItemNumber", LstBox.GetSelectedItemNumber().ToString())
                valueToAddToList.Add("IsEnabled", LstBox.IsEnabled().ToString())

            Case "listview"
                Dim LstView As UIControls.ListView = ListView(hwndDescription)
                If (LstView Is Nothing) Then Return internalDictionary
                valueToAddToList.Add("ColumnCount", LstView.GetColumnCount().ToString())
                If (LstView.GetColumnCount() >= 1) Then
                    valueToAddToList.Add("Values", LstView.GetAllFormatted())
                End If
                valueToAddToList.Add("RowCount", LstView.GetRowCount().ToString())
                valueToAddToList.Add("IsEnabled", LstView.IsEnabled().ToString())

            Case "combobox"
                Dim CmbBox As UIControls.ComboBox = ComboBox(hwndDescription)
                If (CmbBox Is Nothing) Then Return internalDictionary
                valueToAddToList.Add("ItemCount", CmbBox.GetItemCount().ToString())
                valueToAddToList.Add("SelectedItemNumber", CmbBox.GetSelectedItemNumber().ToString())
                If (CmbBox.GetItemCount() > 1) Then
                    valueToAddToList.Add("(Sample)Item(0)", CmbBox.GetItemByIndex(0))
                End If
                valueToAddToList.Add("IsEnabled", CmbBox.IsEnabled().ToString())

            Case "textbox"
                Dim TxtBox As UIControls.TextBox = TextBox(hwndDescription)
                If (TxtBox Is Nothing) Then Return internalDictionary
                valueToAddToList.Add("CurrentLineNumber", TxtBox.GetCurrentLineNumber().ToString())
                valueToAddToList.Add("IndexFromCaret", TxtBox.GetIndexFromCaret().ToString())
                valueToAddToList.Add("LineCount", TxtBox.GetLineCount().ToString())
                If (TxtBox.GetLineCount() > 1) Then
                    valueToAddToList.Add("(Sample)LineText(0)", TxtBox.GetLineText(0))
                End If
                valueToAddToList.Add("IsEnabled", TxtBox.IsEnabled().ToString())

            Case "button"
                'Nothing special
                Dim Buton As UIControls.Button = Button(hwndDescription)
                If (Buton Is Nothing) Then Return internalDictionary
                valueToAddToList.Add("IsEnabled", Buton.IsEnabled().ToString())
            Case "staticlabel"
                'Nothing special
                Dim StcLabel As UIControls.StaticLabel = StaticLabel(hwndDescription)
                If (StcLabel Is Nothing) Then Return internalDictionary
                valueToAddToList.Add("IsEnabled", StcLabel.IsEnabled().ToString())
            Case "checkbox"
                Dim ChkBox As UIControls.CheckBox = CheckBox(hwndDescription)
                If (ChkBox Is Nothing) Then Return internalDictionary
                valueToAddToList.Add("Is3State", ChkBox.Is3State().ToString())
                valueToAddToList.Add("Is3StateAuto", ChkBox.Is3StateAuto().ToString())
                valueToAddToList.Add("IsButton", ChkBox.IsButton().ToString())
                valueToAddToList.Add("IsCheckBox", ChkBox.IsCheckBox().ToString())
                valueToAddToList.Add("IsEnabled", ChkBox.IsEnabled().ToString())
                valueToAddToList.Add("Checked", ChkBox.GetCheckedString())
            Case "radiobutton"
                Dim RadButton As UIControls.RadioButton = RadioButton(hwndDescription)
                If (RadButton Is Nothing) Then Return internalDictionary
                valueToAddToList.Add("IsEnabled", RadButton.IsEnabled().ToString())
                valueToAddToList.Add("IsButton", RadButton.IsButton().ToString())
                valueToAddToList.Add("IsRadioButton", RadButton.IsRadioButton().ToString())
                valueToAddToList.Add("IsSelected", RadButton.GetSelected().ToString())
            Case "treeview"
                Dim TreView As UIControls.TreeView = TreeView(hwndDescription)
                If (TreView Is Nothing) Then Return internalDictionary
                valueToAddToList.Add("IsEnabled", TreView.IsEnabled().ToString())
                valueToAddToList.Add("SelectedText", TreView.GetSelectedText())
                valueToAddToList.Add("Count", TreView.GetItemCount().ToString())
            Case "tabcontrol"
                Dim TbCtrl As UIControls.TabControl = TabControl(hwndDescription)
                If (TbCtrl Is Nothing) Then Return internalDictionary
                valueToAddToList.Add("IsEnabled", TbCtrl.IsEnabled().ToString())
                valueToAddToList.Add("SelectedTabIndex", TbCtrl.GetSelectedTab().ToString())
                valueToAddToList.Add("TabCount", TbCtrl.GetTabCount().ToString())
            Case "window"
                Dim win As UIControls.Window = Me
                valueToAddToList.Add("HasBorder", win.HasBorder().ToString())
                valueToAddToList.Add("HasMaximizeButton", win.HasMaximizeButton().ToString())
                valueToAddToList.Add("HasMinimizeButton", win.HasMinimizeButton().ToString())
                valueToAddToList.Add("HasTitleBar", win.HasTitleBar().ToString())
                'valueToAddToList.Add("NumberOfSameWindows", win.GetObjectCount() )
                valueToAddToList.Add("HasMenu", win.Menu.ContainsMenu().ToString())
                If (win.Menu.ContainsMenu()) Then
                    valueToAddToList.Add("TopLevelMenuCount", win.Menu.GetTopLevelMenuCount().ToString())
                    valueToAddToList.Add("TopLevelMenuText", win.Menu.GetTopLevelMenuText())
                    valueToAddToList.Add("MenuCount", win.Menu.GetMenuCount().ToString())
                    valueToAddToList.Add("AllMenuText", win.Menu.GetText())
                End If
                valueToAddToList.Add("WindowState", win.GetWindowState().ToString())
            Case Else
                Dim WinObj As UIControls.WinObject = WinObject(hwndDescription)
                If (WinObj Is Nothing) Then Return internalDictionary
                valueToAddToList.Add("IsEnabled", WinObj.IsEnabled().ToString())
        End Select
        valueToAddToList.Add("ClientAreaRect", Me.GetClientAreaRect().ToString())

        Return internalDictionary
    End Function


End Class
