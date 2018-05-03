''' <summary>
''' Menus are attached to windows.  Due to a menu's unusually broken up structure,
''' you have to define the path you wish to follow.  For example, if a menu
''' has File->Exit, you will have to define both file and exit in order to
''' select exit.
''' </summary>
''' <remarks></remarks>
Public NotInheritable Class Menu 'User can't create their own menu object.
    Inherits WinObject

#Region "Constructors"

    Protected Friend Sub New(ByVal desc As APIControls.IDescription)
        MyBase.New(desc)
    End Sub

#End Region

#Region "Private Methods and Classes"

    Private Function GetMenuLocationStr(ByVal MenuLocation() As String) As String
        Dim RetStr As String = String.Empty
        For i As Integer = 0 To MenuLocation.Length
            If (i <> 0) Then
                RetStr = RetStr & "->" & MenuLocation(i)
            Else
                RetStr = MenuLocation(i)
            End If
        Next
        Return RetStr
    End Function

    Private Function MenuCount(ByVal Hwnd As IntPtr) As Integer
        Return WinAPI.API.GetMenuItemCount(Hwnd)
    End Function

    Private Function CreateMenuStructure(ByVal Hwnd As IntPtr) As System.Collections.Generic.List(Of MenuStructure)
        Dim sc As New System.Collections.Generic.List(Of MenuStructure)
        InternalCreateMenuStructure(Hwnd, 0, sc, "")
        Return sc
    End Function

    Private Sub InternalCreateMenuStructure(ByVal Hwnd As IntPtr, ByVal MenuLevel As Integer, ByRef Menus As System.Collections.Generic.List(Of MenuStructure), ByVal ParentMenu As String)
        If (Hwnd <> IntPtr.Zero) Then
            For i As Integer = 0 To MenuCount(Hwnd)
                Menus.Add(New MenuStructure(GetMenuText(Hwnd, i), i, MenuLevel, Hwnd, ParentMenu))
            Next
            Dim SubHwnd As IntPtr = IntPtr.Zero

            For i As Integer = 0 To MenuCount(Hwnd)
                SubHwnd = WinAPI.API.GetSubMenu(Hwnd, i)
                If (SubHwnd <> IntPtr.Zero) Then
                    Dim FatherMenu As String = GetMenuText(Hwnd, i)
                    InternalCreateMenuStructure(SubHwnd, MenuLevel + 1, Menus, FatherMenu)
                End If
            Next
        End If
    End Sub

    Private Function GetMenuText(ByVal Hwnd As IntPtr, ByVal Index As Integer) As String
        Dim str As New System.Text.StringBuilder(256)
        WinAPI.API.GetMenuString(Hwnd, Index, str, 255, WinAPI.API.MF_BYPOSITION)
        Return str.ToString()
    End Function

    Private Class MenuStructure
        Public ReadOnly Property MenuText() As String
            Get
                If (FullMenuText.Contains(vbTab)) Then
                    Return FullMenuText.Split(vbTab(0))(0)
                Else
                    Return FullMenuText
                End If
            End Get
        End Property
        Public ReadOnly Property ShortCutText() As String
            Get
                If (FullMenuText.Contains(vbTab)) Then
                    Return FullMenuText.Split(vbTab(0))(1)
                End If
                Return ""
            End Get
        End Property
        Public FullMenuText As String
        Public MenuNumber As Integer
        Public MenuSubLevel As Integer
        Public Handle As IntPtr
        Public ParentText As String

        Public Sub New(ByVal Text As String, ByVal Number As Integer, ByVal MenuSubLevel As Integer, ByVal Handle As IntPtr, ByVal AssociatedFunction As String)
            FullMenuText = Text
            MenuNumber = Number
            Me.MenuSubLevel = MenuSubLevel
            Me.Handle = Handle
            Me.ParentText = AssociatedFunction
        End Sub

        Public Overrides Function ToString() As String
            Return MenuNumber & "||" & MenuSubLevel & "||" & MenuText & "||" & ParentText
        End Function

        Public Function GetMenuID() As Integer
            Return WinAPI.API.GetMenuItemID(Handle, MenuNumber)
        End Function
    End Class

    Private Sub WriteDebugMenuStructure(ByVal sc As System.Collections.Generic.List(Of MenuStructure))
        Debug.WriteLine("*****************************")
        For Each ms As MenuStructure In sc
            Debug.WriteLine(ms.ToString())
        Next
        Debug.WriteLine("*****************************")
    End Sub

    Private Function PerformCommand(ByVal MenuLocation() As String) As Integer
        Dim SelectMenuHwnd As IntPtr = New IntPtr(Me.Hwnd())
        Dim SplitMenus() As String = MenuLocation 'GetMenuLocationSplit(MenuLocation, SplitChar)
        Dim sc As System.Collections.Generic.List(Of MenuStructure) = CreateMenuStructure(SelectMenuHwnd)
        Dim WorkOnSubLevel As Integer = 0
        For i As Integer = 0 To sc.Count - 1
            If (sc.Item(i).MenuSubLevel = WorkOnSubLevel) Then
                If (SplitMenus.Length - 1 < WorkOnSubLevel) Then
                    Throw New SlickTestUIException("Unable to find menu item '" & GetMenuLocationStr(MenuLocation) & "'")
                End If
                If (sc.Item(i).MenuText.Replace("&", "") Like SplitMenus(WorkOnSubLevel).Replace("&", "")) Then
                    If (SplitMenus.Length - 1 = WorkOnSubLevel) Then
                        Return GetMenuStateInfo(sc.Item(i))
                    Else
                        sc.Clear()
                        sc = CreateMenuStructure(SelectMenuHwnd)
                        i = 0
                    End If
                    WorkOnSubLevel += 1
                End If
            End If
        Next
        Throw New SlickTestUIException("Unable to find menu item '" & GetMenuLocationStr(MenuLocation) & "'")
    End Function

    Private Function GetMenuStateInfo(ByVal MenuItem As MenuStructure) As Integer
        Dim mif As New WinAPI.API.MENUITEMINFO()
        mif.cbSize = System.Runtime.InteropServices.Marshal.SizeOf(mif)
        mif.fMask = WinAPI.API.MIIM_STATE Xor WinAPI.API.MIIM_STRING Xor WinAPI.API.MIIM_ID Xor WinAPI.API.MIIM_CHECKMARKS
        mif.dwTypeData = New String(Chr(0), 256) 'New String(Chr(0), (mif.cch + 1))
        mif.fType = 0
        mif.cch = 255
        WinAPI.API.GetMenuItemInfo(MenuItem.Handle, MenuItem.MenuNumber, 1, mif)
        Return mif.fState
    End Function

    Private Function IsHighlighted(ByVal ms As MenuStructure) As Boolean
        Return ((WinAPI.API.MFS_HILITE And GetMenuStateInfo(ms)) = WinAPI.API.MFS_HILITE)
    End Function

    ''' <summary>
    ''' Returns the text for a menu item based upon index.
    ''' </summary>
    ''' <param name="Index">The menu index</param>
    ''' <returns>Only returns top level text.</returns>
    ''' <remarks>The exact menu text.</remarks>
    Private Function GetMenuText(ByVal Index As Integer) As String
        Dim str As New System.Text.StringBuilder(256)
        WinAPI.API.GetMenuString(New IntPtr(Me.Hwnd()), Index, str, 255, WinAPI.API.MF_BYPOSITION)
        Return str.ToString()
    End Function

    Private Function MenuItemsLike(ByVal textA As String, ByVal textB As String) As Boolean
        If (textA.ToUpperInvariant().Replace("&", "") Like textB.ToUpperInvariant().Replace("&", "")) Then
            Return True
        End If
        Return False
    End Function

#End Region

    ''' <summary>
    ''' Gets the text of all of the items below the item you searched for 
    ''' including the full text of the item's text you provide.
    ''' </summary>
    ''' <param name="Text">The text to search for.</param>
    ''' <returns></returns>
    ''' <remarks>This compare the text using like and ignores case and the
    ''' '&amp;' character.
    ''' '</remarks>
    Public Function GetMenuTextBelow(ByVal Text As String) As System.Collections.Generic.List(Of String)
        Dim CurrentMenuStructure As System.Collections.Generic.List(Of MenuStructure) = CreateMenuStructure(New IntPtr(Me.Hwnd()))
        Dim menuText As New System.Collections.Generic.List(Of String)
        Dim menuItemAdded As Boolean
        Do
            menuItemAdded = False
            For Each menuItem As MenuStructure In CurrentMenuStructure
                If (MenuItemsLike(menuItem.ParentText, Text)) Then
                    If (menuText.Count = 0) Then
                        menuText.Add(menuItem.ParentText)
                    End If
                    If (Not menuText.Contains(menuItem.FullMenuText)) Then
                        If (String.IsNullOrEmpty(menuItem.FullMenuText)) Then
                            menuText.Add(menuItem.FullMenuText)
                            menuItemAdded = True
                        End If
                    End If
                Else
                    For Each alreadyFoundMenuItem As String In menuText
                        If (String.IsNullOrEmpty(alreadyFoundMenuItem)) Then
                            Continue For
                        End If
                        If (MenuItemsLike(menuItem.ParentText, alreadyFoundMenuItem)) Then
                            If (Not menuText.Contains(menuItem.FullMenuText)) Then
                                menuText.Add(menuItem.FullMenuText)
                                menuItemAdded = True
                            End If
                        End If
                    Next
                End If
            Next
        Loop While (menuItemAdded)

        Return menuText
    End Function

    ''' <summary>
    ''' Returns the total number of menu items at the top level given window.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetTopLevelMenuCount() As Integer
        Return MenuCount(New IntPtr(Me.Hwnd()))
    End Function

    ''' <summary>
    ''' Returns the total number of menu items for a given window.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetMenuCount() As Integer
        Return CreateMenuStructure(New IntPtr(Me.Hwnd())).Count
    End Function

    ''' <summary>
    ''' Returns the entire text of all the menu items on the 
    ''' top level in the application.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetTopLevelMenuText() As String
        Dim NumberOfMenuItems As Integer = GetTopLevelMenuCount()
        Dim text As New System.Text.StringBuilder(NumberOfMenuItems * 10 + 1)
        For i As Integer = 0 To NumberOfMenuItems - 1
            text.Append(GetMenuText(i))
        Next
        Return text.ToString()
    End Function

    ''' <summary>
    ''' Returns the text of the entire menu structure.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shadows Function GetText() As String
        Dim CurrentMenuStructure As System.Collections.Generic.List(Of MenuStructure) = CreateMenuStructure(New IntPtr(Me.Hwnd()))
        Dim text As New System.Text.StringBuilder(CurrentMenuStructure.Count * 10 + 1)

        For Each Menu As MenuStructure In CurrentMenuStructure
            text.Append(Menu.FullMenuText)
        Next
        Return text.ToString()
    End Function

    ''' <summary>
    ''' User can't set text in a menu.
    ''' </summary>
    ''' <param name="text"></param>
    ''' <remarks>Throws a InvalidOperationException.</remarks>
    Public Shadows Sub SetText(ByVal text As String)
        Throw New InvalidOperationException("Use can not set text in a menu.")
    End Sub


    ''' <summary>
    ''' Returns the Hwnd of the menu if one exists.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks>If no Hwnd exists for the object, an exception 
    ''' will be thrown.</remarks>
    Public Shadows Function Hwnd() As Int64
        Dim MainHwnd As IntPtr = New IntPtr(MyBase.Hwnd())
        If (WinAPI.API.IsMenu(MainHwnd) <> 0) Then Return MainHwnd.ToInt64()
        Dim MenuHwnd As Int64 = WinAPI.API.GetMenu(MainHwnd).ToInt64()
        If (New IntPtr(MenuHwnd) = IntPtr.Zero) Then
            Throw New SlickTestUIException("No menu could be found for the parent of this object.")
        End If
        Return MenuHwnd
    End Function

    ''' <summary>
    ''' Tests to see if the object has a menu.
    ''' </summary>
    ''' <returns>Returns true if a menu is contained.</returns>
    ''' <remarks>This uses wild card string matching in order to attempt to find
    '''  the name of the menu item.</remarks>
    Public Function ContainsMenu() As Boolean
        Dim MainHwnd As IntPtr = New IntPtr(MyBase.Hwnd())
        If (WinAPI.API.IsMenu(MainHwnd) <> 0) Then Return True
        Dim MenuHwnd As Int64 = WinAPI.API.GetMenu(MainHwnd).ToInt64()
        If (New IntPtr(MenuHwnd) = IntPtr.Zero) Then
            Return False
        End If
        Return True
    End Function

    ''' <summary>
    ''' Tests to see if the object has a menu item with a specific name.
    ''' </summary>
    ''' <returns>Returns true if a menu is contained.</returns>
    ''' <remarks>This uses wild card string matching in order to find the 
    ''' name of the menu item.  Ignores the '&amp;' character.</remarks>
    Public Function ContainsMenuItem(ByVal ItemText As String) As Boolean
        Dim sc As System.Collections.Generic.List(Of MenuStructure) = CreateMenuStructure(New IntPtr(Me.Hwnd()))
        For Each ms As MenuStructure In sc
            If (ms.FullMenuText.Replace("&", "") Like ItemText.Replace("&", "")) Then
                Return True
            End If
        Next
        Return False
    End Function

    ''' <summary>
    ''' This allows users to get the exact text of a menu item or sub menu item
    ''' with wild cards.
    ''' </summary>
    ''' <param name="ItemText">The item you are attempting to get.</param>
    ''' <exception cref="Exception" >No item is found.</exception>
    ''' <returns>Returns the text of the menu item.  </returns>
    ''' <remarks>This uses wild card string matching in order to find the name of 
    ''' the menu item.  If no menu item is found, an exception is thrown. 
    ''' Ignores the '&amp;' character.</remarks>
    Public Shadows Function GetText(ByVal ItemText As String) As String
        Dim sc As System.Collections.Generic.List(Of MenuStructure) = CreateMenuStructure(New IntPtr(Me.Hwnd()))
        For Each ms As MenuStructure In sc
            If (ms.FullMenuText.Replace("&", "") Like ItemText.Replace("&", "")) Then
                Return ms.FullMenuText
            End If
        Next
        Throw New SlickTestUIException("Unable to find menu item '" & ItemText & "'")

    End Function

    ''' <summary>
    ''' Select the menu item(s) items.
    ''' </summary>
    ''' <param name="MenuLocation">The menu location, starting with the top menu
    ''' and working your way through the structure.  All names ignore '&amp;'.</param>
    ''' <param name="MouseButtonToOpenMenu">The button to use to select the menu item.
    ''' By default, the left button is used.</param>
    ''' <remarks>This will attempt to select both enabled and disabled menu items.</remarks>
    Public Sub SelectMenuItem(ByVal MenuLocation() As String, Optional ByVal MouseButtonToOpenMenu As System.Windows.Forms.MouseButtons = Windows.Forms.MouseButtons.Left)
        Dim SelectMenuHwnd As IntPtr = New IntPtr(Me.Hwnd())
        Dim SplitMenus() As String = MenuLocation 'GetMenuLocationSplit(MenuLocation, SplitChar)
        Dim sc As System.Collections.Generic.List(Of MenuStructure) = CreateMenuStructure(SelectMenuHwnd)
        Dim WorkOnSubLevel As Integer = 0
        Dim CenterX As Integer = 0
        Dim CenterY As Integer = 0
        Dim MyBaseHwnd As Int64 = MyBase.Hwnd
        Dim rec As New WinAPI.API.RECT
        Dim IsFirstItemInListSelected As Boolean = False
        Dim LastSuccessfulMenuItem As String = SplitMenus(0)
        Me.AppActivateByHwnd(MyBaseHwnd) 'Bring app to front.
        For i As Integer = 0 To sc.Count - 1
            If (sc.Item(i).MenuSubLevel = WorkOnSubLevel) Then
                If (SplitMenus.Length - 1 < WorkOnSubLevel) Then
                    Throw New SlickTestUIException("Unable to find menu item '" & GetMenuLocationStr(MenuLocation) & "'")
                End If
                If (sc.Item(i).MenuText.Replace("&", "") Like SplitMenus(WorkOnSubLevel).Replace("&", "")) Then
                    If (WorkOnSubLevel = 0) Then 'Click first
                        WinAPI.API.GetMenuItemRect(New IntPtr(MyBaseHwnd), sc(i).Handle, sc(i).MenuNumber, rec)
                        CenterX = Convert.ToInt32(rec.left + ((rec.right - rec.left) / 2))
                        CenterY = Convert.ToInt32(rec.top + ((rec.bottom - rec.top) / 2))
                        UIControls.Mouse.GotoXY(CenterX, CenterY)
                        System.Threading.Thread.Sleep(10)
                        'Console.WriteLine("CenterX: " & CenterX & " CenterY: " & CenterY & " left = " & rec.left & "; top = " & rec.top & "; bottom = " & rec.bottom & "; right = " & rec.right)
                        Select Case MouseButtonToOpenMenu
                            Case Windows.Forms.MouseButtons.Left
                                UIControls.Mouse.LeftClick(CenterX, CenterY)
                            Case Windows.Forms.MouseButtons.Middle
                                UIControls.Mouse.MiddleClick(CenterX, CenterY)
                            Case Windows.Forms.MouseButtons.Right
                                UIControls.Mouse.RightClick(CenterX, CenterY)
                            Case Else
                                Throw New SlickTestUIException("Unsupported mouse button was provided to open a menu.  Mouse button: " & MouseButtonToOpenMenu)
                        End Select
                        If (SplitMenus.Length > 1) Then
                            LastSuccessfulMenuItem = SplitMenus(WorkOnSubLevel + 1)
                        End If
                    Else
                        'Press the down key
                        'Me.Report(reporter.Info, "A. WorkOnSubLevel = " & WorkOnSubLevel, "")
                        'Me.Report(reporter.Info, "B. sc.Item(i).ParentText = " & sc.Item(i).ParentText, "")
                        'Me.Report(reporter.Info, "C. sc.Item(i).MenuText = " & sc.Item(i).MenuText, "")

                        For j As Integer = 0 To sc.Count - 1
                            'Me.Report(reporter.Info, "D.****************" & vbNewLine & "sc.Item(j).MenuSubLevel = " & sc.Item(j).MenuSubLevel, "")
                            'Me.Report(reporter.Info, "E. sc.Item(j).ParentText = " & sc.Item(j).ParentText, "")
                            'Me.Report(reporter.Info, "F. sc.Item(j).MenuText = " & sc.Item(j).MenuText, "")
                            'Me.Report(reporter.Info, "G. IsHighlighted(sc.Item(i)) = " & IsHighlighted(sc.Item(i)), "")
                            'Me.Report(reporter.Info, "H. SplitMenus.Length - 1 = " & (SplitMenus.Length - 1), "")
                            'Me.Report(reporter.Info, "I. i = " & i & " j = " & j, "")
                            If (sc.Item(j).MenuSubLevel = WorkOnSubLevel) Then
                                System.Threading.Thread.Sleep(2)
                                If (sc.Item(j).ParentText = sc.Item(i).ParentText) Then
                                    'If (IsFirstItemInListSelected = False) Then
                                    'order matters, see else statement
                                    'If (sc.Item(j).FullMenuText <> "") Then 'only non-breaks count
                                    '    Me.TypeKeys("{Down}")
                                    'End If
                                    'If (sc.Item(j).FullMenuText = sc.Item(i).FullMenuText) Then
                                    '    Exit For
                                    'End If
                                    If (IsHighlighted(sc.Item(i)) = True) Then
                                        Exit For
                                    Else
                                        Keyboard.SendKeys("{DOWN}")
                                        'Me.TypeKeys("{Down}")
                                    End If
                                    'Else
                                    'in the first case, the first item is not selected
                                    'If (sc.Item(j).FullMenuText = sc.Item(i).FullMenuText) Then
                                    'Exit For
                                    'End If
                                    'If (sc.Item(j).FullMenuText <> "") Then 'only non-breaks count
                                    'Me.TypeKeys("{Down}")
                                    'End If
                                    'End If
                                End If
                            End If
                        Next
                        If (SplitMenus.Length - 1 = WorkOnSubLevel) Then
                            'Me.TypeKeys("{ENTER}")
                            Keyboard.SendKeys("{ENTER}")
                            Return
                        Else
                            LastSuccessfulMenuItem = SplitMenus(WorkOnSubLevel + 1)
                            'Me.TypeKeys("{RIGHT}")
                            Keyboard.SendKeys("{RIGHT}")
                            IsFirstItemInListSelected = True
                        End If
                    End If
                    sc.Clear()
                    sc = CreateMenuStructure(SelectMenuHwnd)
                    i = 0
                    WorkOnSubLevel += 1
                End If
            End If
        Next
        Throw New SlickTestUIException("Unable to select menu item '" & LastSuccessfulMenuItem & "'.")
    End Sub

    'Public Sub GetInfo()
    '    Dim mif As New WinAPI.API.MENUITEMINFO()
    '    mif.cbSize = System.Runtime.InteropServices.Marshal.SizeOf(mif)
    '    Dim sc As System.Collections.Generic.List(Of MenuStructure) = CreateMenuStructure(Me.Hwnd)
    '    Dim empty As String = New String(Chr(0), 256)
    '    For i As Integer = 0 To sc.Count - 1
    '        mif.fMask = WinAPI.API.MIIM_STATE Xor WinAPI.API.MIIM_STRING Xor WinAPI.API.MIIM_ID Xor WinAPI.API.MIIM_CHECKMARKS
    '        mif.dwTypeData = empty 'New String(Chr(0), (mif.cch + 1))
    '        mif.fType = 0
    '        mif.cch = 255

    '        WinAPI.API.GetMenuItemInfo(sc.Item(i).Handle, sc(i).MenuNumber, True, mif)

    '        'Dim info As String = String.Empty
    '        'info = "cch: " & mif.cch & " dwItemData:" & mif.dwItemData & " dwTypeData: " & mif.dwTypeData
    '        'info += " fMask: " & mif.fMask & " fState: " & mif.fState & " fType: " & mif.fType & " hbmpChecked: " & mif.hbmpChecked
    '        'info += " hbmpUnchecked: " & mif.hbmpUnchecked & " hSubMenu: " & mif.hSubMenu & " wID: " & mif.wID
    '        'System.Console.WriteLine("Item: " & sc(i).MenuText.PadRight(30) & " " & info)

    '        Dim state As String = String.Empty
    '        state = "CHECKED = " & ((WinAPI.API.MFS_CHECKED And mif.fState) = WinAPI.API.MFS_CHECKED) & _
    '        " DISABLED = " & ((WinAPI.API.MFS_DISABLED And mif.fState) = WinAPI.API.MFS_DISABLED) & _
    '        " ENABLED = " & ((WinAPI.API.MFS_ENABLED And mif.fState) = WinAPI.API.MFS_ENABLED) & _
    '        " GRAYED = " & ((WinAPI.API.MFS_GRAYED And mif.fState) = WinAPI.API.MFS_GRAYED) & _
    '        " HILITE = " & ((WinAPI.API.MFS_HILITE And mif.fState) = WinAPI.API.MFS_HILITE) & _
    '        " UNCHECKED = " & ((WinAPI.API.MFS_UNCHECKED And mif.fState) = WinAPI.API.MFS_UNCHECKED) & _
    '        " UNHILITE = " & ((WinAPI.API.MFS_UNHILITE And mif.fState) = WinAPI.API.MFS_UNHILITE) & _
    '        " DEFAULT = " & ((WinAPI.API.MFS_DEFAULT And mif.fState) = WinAPI.API.MFS_DEFAULT)

    '        System.Console.WriteLine("Item: " & sc(i).MenuText.PadRight(30) & " " & state)
    '    Next
    'End Sub

    ''' <summary>
    ''' Checks if the sub element is enabled.
    ''' </summary>
    ''' <param name="MenuLocation">The menu location, starting with the top menu
    ''' and working your way through the structure.  All names ignore '&amp;'.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shadows Function IsEnabled(ByVal MenuLocation() As String) As Boolean
        Dim state As Integer = PerformCommand(MenuLocation)
        Return ((WinAPI.API.MFS_ENABLED And state) = WinAPI.API.MFS_ENABLED)
    End Function

    ''' <summary>
    ''' Checks if the subelement is checked.
    ''' </summary>
    ''' <param name="MenuLocation">The menu location, starting with the top menu
    ''' and working your way through the structure.  All names ignore '&amp;'.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function IsChecked(ByVal MenuLocation() As String) As Boolean
        Dim state As Integer = PerformCommand(MenuLocation)
        Return ((WinAPI.API.MFS_CHECKED And state) = WinAPI.API.MFS_CHECKED)
    End Function
End Class
