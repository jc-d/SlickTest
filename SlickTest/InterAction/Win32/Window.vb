'Updated On: 8/23/08

''' <summary>
''' A window is just a specialized WinObject, and so it performs everything a WinObject.
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
        WinAPI.API.GetWindowPlacement(Me.Hwnd(), wp)
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
        WinAPI.API.ShowWindow(Me.Hwnd(), Command)
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
        Dim HwndVal As IntPtr = Me.Hwnd()
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
        Dim HwndVal As IntPtr = Me.Hwnd()
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
        Dim itemCount As Integer = 1
        Dim MainDesc As APIControls.Description = WindowsFunctions.CreateDescriptionFromHwnd(New IntPtr(Me.Hwnd))
        window.AppendLine(BuildDescribingString(MainDesc, itemCount))
        Dim ChildDescriptions As Description() = Me.GetChildDescriptions()
        For Each desc As Description In ChildDescriptions
            itemCount = itemCount + 1
            window.AppendLine(BuildDescribingString(desc, itemCount))
            Me.description.RemoveRange(1, Me.description.Count - 1)
            'For i As Integer = 1 To Me.description.Count - 1 'We have to clear the caching so that it doesn't build up.
            '    Me.description.RemoveAt(i)
            '    i = 1
            'Next
        Next
        Return window.ToString()
    End Function

    Private Function BuildDescribingString(ByVal desc As APIControls.Description, ByVal itemCount As Integer) As String
        Dim valueToAddToList As New System.Text.StringBuilder(300)
        valueToAddToList.AppendLine("Item # " & itemCount)

        For Each item As APIControls.Description.DescriptionData In [Enum].GetValues(GetType(APIControls.Description.DescriptionData))
            If (desc.Contains(item)) Then
                valueToAddToList.AppendLine(vbTab & desc.GetItemName(item) & "='" & desc.GetItemValue(item) & "'")
            End If
        Next
        valueToAddToList.AppendLine(vbTab & "Style='" & Me.GetStyle() & "'")
        valueToAddToList.AppendLine(vbTab & "StyleEx='" & Me.GetStyleEx() & "'")

        Dim hwndDescription As String = "hwnd:=""" & desc.Hwnd.ToString() & """"
        Select Case desc.WindowType().ToLowerInvariant()
            Case "listbox"
                Dim LstBox As UIControls.ListBox = ListBox(hwndDescription)
                If (LstBox Is Nothing) Then Return valueToAddToList.ToString()
                valueToAddToList.AppendLine(vbTab & "ItemCount='" & LstBox.GetItemCount() & "'")
                If (LstBox.GetItemCount() > 1) Then
                    valueToAddToList.AppendLine(vbTab & "(Sample)Item(0)='" & LstBox.GetItemByIndex(0) & "'")
                End If
                valueToAddToList.AppendLine(vbTab & "SelectCount='" & LstBox.GetSelectCount() & "'")
                valueToAddToList.AppendLine(vbTab & "SelectedItemNumber='" & LstBox.GetSelectedItemNumber() & "'")
                valueToAddToList.AppendLine(vbTab & "IsEnabled='" & LstBox.IsEnabled() & "'")

            Case "listview"
                Dim LstView As UIControls.ListView = ListView(hwndDescription)
                If (LstView Is Nothing) Then Return valueToAddToList.ToString()
                valueToAddToList.AppendLine(vbTab & "ColumnCount='" & LstView.GetColumnCount() & "'")
                If (LstView.GetColumnCount() >= 1) Then
                    valueToAddToList.AppendLine(vbTab & "Values='" & LstView.GetAllFormatted() & "'")
                End If
                valueToAddToList.AppendLine(vbTab & "RowCount='" & LstView.GetRowCount() & "'")
                valueToAddToList.AppendLine(vbTab & "IsEnabled='" & LstView.IsEnabled() & "'")

            Case "combobox"
                Dim CmbBox As UIControls.ComboBox = ComboBox(hwndDescription)
                If (CmbBox Is Nothing) Then Return valueToAddToList.ToString()
                valueToAddToList.AppendLine(vbTab & "ItemCount='" & CmbBox.GetItemCount() & "'")
                valueToAddToList.AppendLine(vbTab & "SelectedItemNumber='" & CmbBox.GetSelectedItemNumber() & "'")
                If (CmbBox.GetItemCount() > 1) Then
                    valueToAddToList.AppendLine(vbTab & "(Sample)Item(0)='" & CmbBox.GetItemByIndex(0) & "'")
                End If
                valueToAddToList.AppendLine(vbTab & "IsEnabled='" & CmbBox.IsEnabled() & "'")

            Case "textbox"
                Dim TxtBox As UIControls.TextBox = TextBox(hwndDescription)
                If (TxtBox Is Nothing) Then Return valueToAddToList.ToString()
                valueToAddToList.AppendLine(vbTab & "CurrentLineNumber='" & TxtBox.GetCurrentLineNumber() & "'")
                valueToAddToList.AppendLine(vbTab & "IndexFromCaret='" & TxtBox.GetIndexFromCaret() & "'")
                valueToAddToList.AppendLine(vbTab & "LineCount='" & TxtBox.GetLineCount() & "'")
                If (TxtBox.GetLineCount() > 1) Then
                    valueToAddToList.AppendLine(vbTab & "(Sample)LineText(0)='" & TxtBox.GetLineText(0) & "'")
                End If
                valueToAddToList.AppendLine(vbTab & "IsEnabled='" & TxtBox.IsEnabled() & "'")

            Case "button"
                'Nothing special
                Dim Buton As UIControls.Button = Button(hwndDescription)
                If (Buton Is Nothing) Then Return valueToAddToList.ToString()
                valueToAddToList.AppendLine(vbTab & "IsEnabled='" & Buton.IsEnabled() & "'")
            Case "staticlabel"
                'Nothing special
                Dim StcLabel As UIControls.StaticLabel = StaticLabel(hwndDescription)
                If (StcLabel Is Nothing) Then Return valueToAddToList.ToString()
                valueToAddToList.AppendLine(vbTab & "IsEnabled='" & StcLabel.IsEnabled() & "'")
            Case "checkbox"
                Dim ChkBox As UIControls.CheckBox = CheckBox(hwndDescription)
                If (ChkBox Is Nothing) Then Return valueToAddToList.ToString()
                valueToAddToList.AppendLine(vbTab & "Is3State='" & ChkBox.Is3State() & "'")
                valueToAddToList.AppendLine(vbTab & "Is3StateAuto='" & ChkBox.Is3StateAuto() & "'")
                valueToAddToList.AppendLine(vbTab & "IsButton='" & ChkBox.IsButton() & "'")
                valueToAddToList.AppendLine(vbTab & "IsCheckBox='" & ChkBox.IsCheckBox() & "'")
                valueToAddToList.AppendLine(vbTab & "IsEnabled='" & ChkBox.IsEnabled() & "'")
                valueToAddToList.AppendLine(vbTab & "Checked='" & ChkBox.GetCheckedString() & "'")
            Case "radiobutton"
                Dim RadButton As UIControls.RadioButton = RadioButton(hwndDescription)
                If (RadButton Is Nothing) Then Return valueToAddToList.ToString()
                valueToAddToList.AppendLine(vbTab & "IsEnabled='" & RadButton.IsEnabled() & "'")
                valueToAddToList.AppendLine(vbTab & "IsButton='" & RadButton.IsButton() & "'")
                valueToAddToList.AppendLine(vbTab & "IsRadioButton='" & RadButton.IsRadioButton() & "'")
                valueToAddToList.AppendLine(vbTab & "IsSelected='" & RadButton.GetSelected() & "'")
            Case "treeview"
                Dim TreView As UIControls.TreeView = TreeView(hwndDescription)
                If (TreView Is Nothing) Then Return valueToAddToList.ToString()
                valueToAddToList.AppendLine(vbTab & "IsEnabled='" & TreView.IsEnabled() & "'")
                valueToAddToList.AppendLine(vbTab & "SelectedText='" & TreView.GetSelectedText() & "'")
                valueToAddToList.AppendLine(vbTab & "Count='" & TreView.GetItemCount() & "'")
            Case "tabcontrol"
                Dim TbCtrl As UIControls.TabControl = TabControl(hwndDescription)
                If (TbCtrl Is Nothing) Then Return valueToAddToList.ToString()
                valueToAddToList.AppendLine(vbTab & "IsEnabled='" & TbCtrl.IsEnabled() & "'")
                valueToAddToList.AppendLine(vbTab & "SelectedTabIndex='" & TbCtrl.GetSelectedTab() & "'")
                valueToAddToList.AppendLine(vbTab & "TabCount='" & TbCtrl.GetTabCount() & "'")
            Case "window"
                Dim win As UIControls.Window = Me
                valueToAddToList.AppendLine(vbTab & "HasBorder='" & win.HasBorder() & "'")
                valueToAddToList.AppendLine(vbTab & "HasMaximizeButton='" & win.HasMaximizeButton() & "'")
                valueToAddToList.AppendLine(vbTab & "HasMinimizeButton='" & win.HasMinimizeButton() & "'")
                valueToAddToList.AppendLine(vbTab & "HasTitleBar='" & win.HasTitleBar() & "'")
                'valueToAddToList.AppendLine(vbTab & "NumberOfSameWindows='" & win.GetObjectCount() & "'")
                valueToAddToList.AppendLine(vbTab & "HasMenu='" & win.Menu.ContainsMenu() & "'")
                If (win.Menu.ContainsMenu()) Then
                    valueToAddToList.AppendLine(vbTab & "TopLevelMenuCount='" & win.Menu.GetTopLevelMenuCount() & "'")
                    valueToAddToList.AppendLine(vbTab & "TopLevelMenuText='" & win.Menu.GetTopLevelMenuText() & "'")
                    valueToAddToList.AppendLine(vbTab & "MenuCount='" & win.Menu.GetMenuCount() & "'")
                    valueToAddToList.AppendLine(vbTab & "AllMenuText='" & win.Menu.GetText() & "'")
                End If
                valueToAddToList.AppendLine(vbTab & "WindowState='" & win.GetWindowState().ToString() & "'")
            Case Else
                Dim WinObj As UIControls.WinObject = WinObject(hwndDescription)
                If (WinObj Is Nothing) Then Return valueToAddToList.ToString()
                valueToAddToList.AppendLine(vbTab & "IsEnabled='" & WinObj.IsEnabled() & "'")
        End Select
        valueToAddToList.AppendLine(vbTab & "ClientAreaRect='" & Me.GetClientAreaRect().ToString() & "'")

        Return valueToAddToList.ToString()
    End Function

End Class
