Imports winAPI.API
Imports winAPI.NativeFunctions
Imports System.Runtime.InteropServices
Imports System.Text
Imports System.ComponentModel
Imports System
Imports System.Collections
Imports System.Windows

Friend Class TreeViewWindowsAPI
    Private Shared WindowsFunctions As APIControls.IndependentWindowsFunctionsv1

    Public Sub New(ByVal wf As APIControls.IndependentWindowsFunctionsv1)
        WindowsFunctions = wf
    End Sub

    Public Function IsTreeView(ByVal hWnd As IntPtr) As Boolean
        If (WindowsFunctions.GetClassNameNoDotNet(hWnd).ToUpperInvariant().IndexOf("TREEVIEW") <> -1) Then
            Return True
        End If
        Return False
    End Function

    Public Function GetItemCount(ByVal Hwnd As IntPtr) As Integer
        Return WinAPI.API.SendMessage(Hwnd, WinAPI.API.TreeViewMessages.TVM_GETCOUNT, IntPtr.Zero, IntPtr.Zero).ToInt32()
    End Function

#Region "Local APIs seem to be required for unknown reason"

    <DllImport("user32.dll")> _
    Private Shared Function SendMessage(ByVal window As IntPtr, ByVal message As Integer, ByVal wParam As IntPtr, ByVal lParam As IntPtr) As Integer
    End Function

    <DllImport("user32")> _
    Private Shared Function GetWindowThreadProcessId(ByVal hWnd As IntPtr, ByRef lpwdProcessID As Integer) As IntPtr
    End Function

    <DllImport("kernel32")> _
    Private Shared Function OpenProcess(ByVal dwDesiredAccess As UInteger, ByVal bInheritHandle As Boolean, ByVal dwProcessId As Integer) As IntPtr
    End Function

    <DllImport("kernel32")> _
    Private Shared Function VirtualAllocEx(ByVal hProcess As IntPtr, ByVal lpAddress As IntPtr, ByVal dwSize As Integer, ByVal flAllocationType As UInteger, ByVal flProtect As UInteger) As IntPtr
    End Function

    <DllImport("kernel32")> _
    Private Shared Function VirtualFreeEx(ByVal hProcess As IntPtr, ByVal lpAddress As IntPtr, ByVal dwSize As Integer, ByVal dwFreeType As UInteger) As Boolean
    End Function

    <DllImport("kernel32")> _
    Private Shared Function WriteProcessMemory(ByVal hProcess As IntPtr, ByVal lpBaseAddress As IntPtr, ByRef buffer As TVITEM, ByVal dwSize As Integer, ByVal lpNumberOfBytesWritten As IntPtr) As Boolean
    End Function

    <DllImport("kernel32")> _
    Private Shared Function ReadProcessMemory(ByVal hProcess As IntPtr, ByVal lpBaseAddress As IntPtr, ByVal lpBuffer As IntPtr, ByVal dwSize As Integer, ByVal lpNumberOfBytesRead As IntPtr) As Boolean
    End Function

    <DllImport("kernel32")> _
    Private Shared Function CloseHandle(ByVal hObject As IntPtr) As Boolean
    End Function
#End Region


    'Const PROCESS_ALL_ACCESS As UInteger = CUInt((983040L Or 1048576L Or &HFFF))
    'Const TVIF_TEXT As Integer = &H1
    'Const MEM_COMMIT As UInteger = &H1000
    'Const MEM_RELEASE As UInteger = &H8000
    'Const PAGE_READWRITE As UInteger = &H4

    Private Const TVIF_TEXT As Integer = 1
    Private Const TVIF_PARAM As Integer = 4
    Private Const TVIF_STATE As Integer = 8
    Private Const PROCESS_ALL_ACCESS As UInteger = CInt((983040 Or 1048576 Or 4095))
    Private Const MEM_COMMIT As UInteger = 4096
    Private Const MEM_RELEASE As UInteger = 32768
    Private Const PAGE_READWRITE As UInteger = 4

    Private Const TVM_GETITEM As Integer = &H110C
    Private Const TVM_SETITEM As Integer = &H110D
    Private Const TV_FIRST As Integer = &H1100
    Private Const TVM_GETNEXTITEM As Integer = (TV_FIRST + 10)

    Private Const TVGN_ROOT As Integer = &H0
    Private Const TVGN_NEXT As Integer = &H1
    Private Const TVGN_CHILD As Integer = &H4

    Private Const MY_MAXLVITEMTEXT As Integer = 260

    Private ItemsAdded As New System.Collections.Generic.List(Of IntPtr)()

    <StructLayout(LayoutKind.Sequential, CharSet:=CharSet.Auto)> _
    Private Structure TVITEM
        Public mask As Integer
        Public hItem As IntPtr
        Public state As Integer
        Public stateMask As Integer
        Public pszText As IntPtr
        Public cchTextMax As Integer
        Public iImage As Integer
        Public iSelectedImage As Integer
        Public cChildren As Integer
        Public lParam As IntPtr
    End Structure

    <StructLayout(LayoutKind.Sequential)> _
    Public Structure TV_ITEM
        Public mask As Integer
        Public hItem As Integer
        Public state As Integer
        Public stateMask As Integer
        Public pszText As IntPtr
        'if string, must be preallocated
        Public cchTextMax As Integer
        Public iImage As Integer
        Public iSelectedImage As Integer
        Public cChildren As Integer
        Public lParam As Integer
        Public iIntegral As Integer
    End Structure

#Region "Search by Text"
    Public Function GetItemHwndByExactText(ByVal Hwnd As IntPtr, ByVal ItemText As String) As IntPtr
        Dim Item As IntPtr = New IntPtr(WinAPI.API.SendMessage(Hwnd, WinAPI.API.TreeViewMessages.TVM_GETNEXTITEM, WinAPI.API.TreeViewItemSelFlags.TVGN_ROOT, IntPtr.Zero))
        ItemsAdded.Clear()
        Return Me.GetItemHwndByText(Hwnd, Item, ItemText, False, 0)
    End Function

    Public Function GetItemHwndByLikeText(ByVal Hwnd As IntPtr, ByVal ItemText As String) As IntPtr
        Dim Item As IntPtr = New IntPtr(WinAPI.API.SendMessage(Hwnd, WinAPI.API.TreeViewMessages.TVM_GETNEXTITEM, WinAPI.API.TreeViewItemSelFlags.TVGN_ROOT, IntPtr.Zero))
        ItemsAdded.Clear()
        Return Me.GetItemHwndByText(Hwnd, Item, ItemText, True, 0)
    End Function

    Public Function GetItemIndexByExactText(ByVal Hwnd As IntPtr, ByVal ItemText As String) As Integer
        Dim Item As IntPtr = New IntPtr(WinAPI.API.SendMessage(Hwnd, WinAPI.API.TreeViewMessages.TVM_GETNEXTITEM, WinAPI.API.TreeViewItemSelFlags.TVGN_ROOT, IntPtr.Zero))
        Dim Index As Integer
        ItemsAdded.Clear()
        If (Me.GetItemHwndByText(Hwnd, Item, ItemText, False, Index) <> IntPtr.Zero) Then Return Index
        Return -1
    End Function

    Public Function GetItemIndexByLikeText(ByVal Hwnd As IntPtr, ByVal ItemText As String) As Integer
        Dim Item As IntPtr = New IntPtr(WinAPI.API.SendMessage(Hwnd, WinAPI.API.TreeViewMessages.TVM_GETNEXTITEM, WinAPI.API.TreeViewItemSelFlags.TVGN_ROOT, IntPtr.Zero))
        Dim Index As Integer
        ItemsAdded.Clear()
        If (Me.GetItemHwndByText(Hwnd, Item, ItemText, True, Index) <> IntPtr.Zero) Then Return Index
        Return -1
    End Function

    Private Function DoCompare(ByVal Txt1 As String, ByVal Txt2 As String, ByVal UseLike As Boolean) As Boolean
        If (UseLike = True) Then
            If (Txt1 Like Txt2) Then
                Return True
            End If
        Else
            If (Txt1 = Txt2) Then
                Return True
            End If
        End If
        Return False
    End Function

    Private Function GetItemHwndByText(ByVal Hwnd As IntPtr, ByVal CurrentSearchItem As IntPtr, ByVal ItemText As String, ByVal UseLike As Boolean, ByRef CurrentIndex As Integer) As IntPtr
        Dim Item As IntPtr = CurrentSearchItem
        Dim Child As IntPtr = IntPtr.Zero
        Dim tmpSuccess As Boolean = False
        Dim tmpHwnd As IntPtr = IntPtr.Zero
        While (Item <> IntPtr.Zero)
            If (ItemsAdded.Contains(Item) = False) Then
                If (DoCompare(GetText(Hwnd, Item), ItemText, UseLike)) Then
                    Return Item
                End If
                CurrentIndex += 1
                ItemsAdded.Add(Item)
            End If
            Child = New IntPtr(WinAPI.NativeFunctions.SendMessage(Hwnd, WinAPI.API.TreeViewMessages.TVM_GETNEXTITEM, New IntPtr(WinAPI.API.TreeViewItemSelFlags.TVGN_CHILD), Item))
            While (Child <> IntPtr.Zero)
                If (ItemsAdded.Contains(Child) = False) Then
                    If (DoCompare(GetText(Hwnd, Child), ItemText, UseLike)) Then
                        Return Child
                    End If
                    CurrentIndex += 1
                    ItemsAdded.Add(Child)
                    tmpHwnd = GetItemHwndByText(Hwnd, Child, ItemText, UseLike, CurrentIndex)
                    If (tmpHwnd <> IntPtr.Zero) Then Return tmpHwnd 'we found it!
                End If
                Child = New IntPtr(WinAPI.NativeFunctions.SendMessage(Hwnd, WinAPI.API.TreeViewMessages.TVM_GETNEXTITEM, New IntPtr(WinAPI.API.TreeViewItemSelFlags.TVGN_NEXT), Child))
            End While
            Item = New IntPtr(WinAPI.NativeFunctions.SendMessage(Hwnd, WinAPI.API.TreeViewMessages.TVM_GETNEXTITEM, New IntPtr(WinAPI.API.TreeViewItemSelFlags.TVGN_NEXT), Item))
        End While
        Return IntPtr.Zero 'No Find :(
    End Function
#End Region

#Region "Search by Index"

    Public Function GetItemHwndByIndex(ByVal Hwnd As IntPtr, ByVal SearchItemIndex As Integer) As IntPtr
        If (Me.GetItemCount(Hwnd) <= SearchItemIndex) Then Return IntPtr.Zero
        If (SearchItemIndex < 0) Then  Return IntPtr.Zero
        Dim Item As IntPtr = New IntPtr(WinAPI.API.SendMessage(Hwnd, WinAPI.API.TreeViewMessages.TVM_GETNEXTITEM, WinAPI.API.TreeViewItemSelFlags.TVGN_ROOT, IntPtr.Zero))
        ItemsAdded.Clear()
        Return Me.GetItemHwndByIndex(Hwnd, Item, SearchItemIndex, 0)
    End Function

    Private Function GetItemHwndByIndex(ByVal Hwnd As IntPtr, ByVal CurrentSearchItem As IntPtr, ByVal SearchItemIndex As Integer, ByRef CurrentIndex As Integer) As IntPtr
        Dim Item As IntPtr = CurrentSearchItem
        Dim Child As IntPtr = IntPtr.Zero
        Dim tmpSuccess As Boolean = False
        Dim tmpHwnd As IntPtr = IntPtr.Zero
        While (Item <> IntPtr.Zero)
            If (ItemsAdded.Contains(Item) = False) Then
                If (SearchItemIndex = CurrentIndex) Then
                    Return Item
                End If
                CurrentIndex += 1
                ItemsAdded.Add(Item)
            End If
            Child = New IntPtr(WinAPI.NativeFunctions.SendMessage(Hwnd, WinAPI.API.TreeViewMessages.TVM_GETNEXTITEM, New IntPtr(WinAPI.API.TreeViewItemSelFlags.TVGN_CHILD), Item))
            While (Child <> IntPtr.Zero)
                If (ItemsAdded.Contains(Child) = False) Then
                    If (SearchItemIndex = CurrentIndex) Then
                        Return Child
                    End If
                    CurrentIndex += 1
                    ItemsAdded.Add(Child)
                    tmpHwnd = GetItemHwndByIndex(Hwnd, Child, SearchItemIndex, CurrentIndex)
                    If (tmpHwnd <> IntPtr.Zero) Then Return tmpHwnd 'we found it!
                End If
                Child = New IntPtr(WinAPI.NativeFunctions.SendMessage(Hwnd, WinAPI.API.TreeViewMessages.TVM_GETNEXTITEM, New IntPtr(WinAPI.API.TreeViewItemSelFlags.TVGN_NEXT), Child))
            End While
            Item = New IntPtr(WinAPI.NativeFunctions.SendMessage(Hwnd, WinAPI.API.TreeViewMessages.TVM_GETNEXTITEM, New IntPtr(WinAPI.API.TreeViewItemSelFlags.TVGN_NEXT), Item))
        End While
        Return IntPtr.Zero 'No Find :(
    End Function


#End Region

    Private Function GetSelectedItemHwnd(ByVal Hwnd As IntPtr) As IntPtr
        Dim Item As IntPtr = New IntPtr(WinAPI.API.SendMessage(Hwnd, WinAPI.API.TreeViewMessages.TVM_GETNEXTITEM, WinAPI.API.TreeViewItemSelFlags.TVGN_CARET, IntPtr.Zero))
        Return Item
    End Function

    Public Function GetSelectedItemText(ByVal Hwnd As IntPtr) As String
        Dim Item As IntPtr = New IntPtr(WinAPI.API.SendMessage(Hwnd, WinAPI.API.TreeViewMessages.TVM_GETNEXTITEM, WinAPI.API.TreeViewItemSelFlags.TVGN_CARET, IntPtr.Zero))
        Return GetText(Hwnd, Item)
    End Function

    Public Function GetAllItemsText(ByVal Hwnd As IntPtr) As System.Collections.Generic.List(Of String)
        Dim Item As IntPtr = New IntPtr(WinAPI.API.SendMessage(Hwnd, WinAPI.API.TreeViewMessages.TVM_GETNEXTITEM, WinAPI.API.TreeViewItemSelFlags.TVGN_ROOT, IntPtr.Zero))
        Dim StrCol As New System.Collections.Generic.List(Of String)()
        ItemsAdded.Clear()
        Me.GetAllItemsText(Hwnd, Item, StrCol)
        Return StrCol
    End Function

    Private Sub GetAllItemsText(ByVal Hwnd As IntPtr, ByVal CurrentSearchItem As IntPtr, ByRef StrCol As System.Collections.Generic.List(Of String))
        Dim Item As IntPtr = CurrentSearchItem
        Dim Child As IntPtr = IntPtr.Zero
        Dim tmpHwnd As IntPtr = IntPtr.Zero
        While (Item <> IntPtr.Zero)
            If (ItemsAdded.Contains(Item) = False) Then
                StrCol.Add(Me.GetText(Hwnd, Item))
                ItemsAdded.Add(Item)
            End If
            Child = New IntPtr(WinAPI.NativeFunctions.SendMessage(Hwnd, WinAPI.API.TreeViewMessages.TVM_GETNEXTITEM, New IntPtr(WinAPI.API.TreeViewItemSelFlags.TVGN_CHILD), Item))

            While (Child <> IntPtr.Zero)
                If (ItemsAdded.Contains(Child) = False) Then
                    ItemsAdded.Add(Child)
                    StrCol.Add(Me.GetText(Hwnd, Child))
                    GetAllItemsText(Hwnd, Child, StrCol)
                End If
                Child = New IntPtr(WinAPI.NativeFunctions.SendMessage(Hwnd, WinAPI.API.TreeViewMessages.TVM_GETNEXTITEM, New IntPtr(WinAPI.API.TreeViewItemSelFlags.TVGN_NEXT), Child))
            End While
            Item = New IntPtr(WinAPI.NativeFunctions.SendMessage(Hwnd, WinAPI.API.TreeViewMessages.TVM_GETNEXTITEM, New IntPtr(WinAPI.API.TreeViewItemSelFlags.TVGN_NEXT), Item))
        End While
    End Sub

    Public Function GetText(ByVal Hwnd As IntPtr, ByVal Item As IntPtr) As String
        Return ReadTreeViewItem(Hwnd, Item)
    End Function

#Region "MS UIAutomation"

    Public Sub ExpandCollapseItem(ByVal Hwnd As IntPtr, ByVal Item As String, ByVal Expand As Boolean) 'As IntPtr
        If (Hwnd = IntPtr.Zero) Then Throw New SlickTestAPIException("Unable to find TreeView.")
        Dim e As Automation.AutomationElement
        ' If (SubHwnd = IntPtr.Zero) Then
        e = Automation.AutomationElement.FromHandle(Hwnd)
        'Else
        'e = Automation.AutomationElement.FromHandle(SubHwnd)
        'End If


        Dim NameCondition As Automation.PropertyCondition = New Automation.PropertyCondition( _
        Automation.AutomationElement.NameProperty, Item)
        Dim e1 As Automation.AutomationElement = _
        e.FindFirst(Automation.TreeScope.Subtree, NameCondition)
        If (e1 Is Nothing) Then Throw New SlickTestAPIException("Unable to find the item '" & Item & "'.")

        Dim ec As Automation.ExpandCollapsePattern = _
         DirectCast(e1.GetCurrentPattern(Automation.ExpandCollapsePattern.Pattern), Automation.ExpandCollapsePattern)
        If (ec Is Nothing) Then Throw New SlickTestAPIException("Unable to expand or collapse the item '" & Item & "'.")
        If (Expand = True) Then
            ec.Expand()
        Else
            ec.Collapse()
        End If
    End Sub
    'TODO: Test this method.
    Public Sub SelectVisibleItem(ByVal Hwnd As IntPtr, ByVal Item As String, ByVal SelectItem As Boolean) 'As IntPtr
        If (Hwnd = IntPtr.Zero) Then Throw New SlickTestAPIException("Unable to find TreeView.")
        Dim e As Automation.AutomationElement
        ' If (SubHwnd = IntPtr.Zero) Then
        e = Automation.AutomationElement.FromHandle(Hwnd)
        'Else
        'e = Automation.AutomationElement.FromHandle(SubHwnd)
        'End If

        Dim NameCondition As Automation.PropertyCondition = New Automation.PropertyCondition( _
        Automation.AutomationElement.NameProperty, Item)
        Dim e1 As Automation.AutomationElement = _
        e.FindFirst(Automation.TreeScope.Subtree, NameCondition)
        If (e1 Is Nothing) Then Throw New SlickTestAPIException("Unable to find the item '" & Item & "'.")

        Dim si As Automation.SelectionItemPattern = _
         DirectCast(e1.GetCurrentPattern(Automation.SelectionItemPattern.Pattern), Automation.SelectionItemPattern)
        If (si Is Nothing) Then Throw New SlickTestAPIException("Unable to select or unselect the item '" & Item & "'.")
        If (SelectItem = True) Then
            si.Select()
        Else
            si.RemoveFromSelection()
        End If
    End Sub


    Public Sub ExpandCollapseItem(ByVal Hwnd As IntPtr, ByVal Index As Integer, ByVal Expand As Boolean)
        Dim TmpHwnd As IntPtr = GetItemHwndByIndex(Hwnd, Index)
        If (TmpHwnd = IntPtr.Zero) Then Throw New SlickTestAPIException("Unable to find item with the index '" & Index & "'.")
        Dim Txt As String = GetText(Hwnd, TmpHwnd)
        ExpandCollapseItem(Hwnd, Txt, Expand)
    End Sub

#End Region

    'TODO: Fix this.
    'Private Shared Function ReadTreeViewItem(ByVal hWnd As IntPtr) As String
    '    Const dwBufferSize As Integer = 1024

    '    Dim dwProcessID As Integer

    '    Dim retval As String = ""

    '    Dim hProcess As IntPtr = IntPtr.Zero
    '    Dim lpRemoteBuffer As IntPtr = IntPtr.Zero
    '    Dim lpLocalBuffer As IntPtr = IntPtr.Zero
    '    Dim threadId As IntPtr = IntPtr.Zero
    '    Dim item As Integer = SendMessage(hWnd, TVM_GETNEXTITEM, IntPtr.Zero, IntPtr.Zero)
    '    Try

    '        lpLocalBuffer = Marshal.AllocHGlobal(dwBufferSize)
    '        ' Get the process id owning the window
    '        threadId = GetWindowThreadProcessId(hWnd, dwProcessID)
    '        If (threadId = IntPtr.Zero) OrElse (dwProcessID = 0) Then
    '            Throw New SlickTestAPIException("Failed to access process.")
    '        End If

    '        ' Open the process with all access
    '        hProcess = OpenProcess(PROCESS_ALL_ACCESS, False, dwProcessID)
    '        If hProcess = IntPtr.Zero Then
    '            Throw New SlickTestAPIException("Failed to access process.")
    '        End If

    '        ' Allocate a buffer in the remote process
    '        lpRemoteBuffer = VirtualAllocEx(hProcess, IntPtr.Zero, dwBufferSize, MEM_COMMIT, PAGE_READWRITE)
    '        If lpRemoteBuffer = IntPtr.Zero Then
    '            Throw New SlickTestAPIException("Failed to allocate memory in remote process.")
    '        End If


    '        ExtractNode(hProcess, hWnd, item, lpLocalBuffer, lpRemoteBuffer, dwBufferSize)
    '    Finally
    '        If lpLocalBuffer <> IntPtr.Zero Then
    '            Marshal.FreeHGlobal(lpLocalBuffer)
    '        End If
    '        If lpRemoteBuffer <> IntPtr.Zero Then
    '            VirtualFreeEx(hProcess, lpRemoteBuffer, 0, MEM_RELEASE)
    '        End If
    '        If hProcess <> IntPtr.Zero Then
    '            CloseHandle(hProcess)
    '        End If
    '    End Try
    '    Return retval
    'End Function

    'Private Shared Sub ExtractNode(ByVal hProcess As IntPtr, ByVal hTreeview As IntPtr, ByVal iItem As Int64, ByVal lpLocalBuffer As IntPtr, ByVal lpRemoteBuffer As IntPtr, ByVal dwBufferSize As Integer)
    '    Dim bSuccess As Boolean
    '    Dim retval As String
    '    While iItem > 0
    '        Dim tvItem As New TVITEM()
    '        ' Fill in the TVITEM struct, this is in your own process
    '        ' Set the pszText member to somewhere in the remote buffer,
    '        ' For the example I used the address imediately following the TVITEM stuct
    '        tvItem.mask = TVIF_TEXT
    '        tvItem.hItem = New IntPtr(iItem)

    '        tvItem.pszText = New IntPtr((lpRemoteBuffer.ToInt64() + Marshal.SizeOf(GetType(TV_ITEM))))
    '        tvItem.cchTextMax = 255

    '        ' Copy the local TVITEM to the remote buffer
    '        bSuccess = WriteProcessMemory(hProcess, lpRemoteBuffer, tvItem, Marshal.SizeOf(GetType(TV_ITEM)), IntPtr.Zero)
    '        If Not bSuccess Then
    '            Throw New SystemException("Failed to write to process memory")
    '        End If

    '        ' Send the message to the remote window with the address of the remote buffer
    '        SendMessage(hTreeview, TVM_GETITEM, 0, lpRemoteBuffer)

    '        ' Read the struct back from the remote process into local buffer
    '        bSuccess = ReadProcessMemory(hProcess, lpRemoteBuffer, lpLocalBuffer, dwBufferSize, IntPtr.Zero)

    '        If Not bSuccess Then
    '            Throw New SystemException("Failed to read from process memory")
    '        End If

    '        ' At this point the lpLocalBuffer contains the returned TVITEM structure
    '        ' the next line extracts the text from the buffer into a managed string
    '        retval = Marshal.PtrToStringAnsi(New IntPtr((lpLocalBuffer.ToInt32() + Marshal.SizeOf(GetType(TVITEM)))))
    '        System.Windows.Forms.MessageBox.Show(retval)
    '        If iItem > 0 Then
    '            Dim iChildItem As Integer = SendMessage(hTreeview, TVM_GETNEXTITEM, New IntPtr(TVGN_CHILD), New IntPtr(iItem))
    '            If iChildItem > 0 Then
    '                ExtractNode(hProcess, hTreeview, iChildItem, lpLocalBuffer, lpRemoteBuffer, dwBufferSize)
    '            End If
    '        End If
    '        iItem = SendMessage(hTreeview, TVM_GETNEXTITEM, New IntPtr(TVGN_NEXT), New IntPtr(iItem))

    '    End While
    'End Sub

    'Public Sub SetSelectedItem(ByVal hWnd As IntPtr, ByVal itemIndex As Integer, ByVal SelectItems As Boolean)
    '    Const dwBufferSize As Integer = 1024
    '    Dim dwProcessID As Integer
    '    Dim tvItem As TVITEM
    '    Dim retval As Boolean = False
    '    Dim bSuccess As Boolean
    '    Dim hProcess As IntPtr = IntPtr.Zero
    '    Dim lpRemoteBuffer As IntPtr = IntPtr.Zero
    '    Dim lpLocalBuffer As IntPtr = IntPtr.Zero
    '    Dim threadId As IntPtr = IntPtr.Zero
    '    Dim RootItem As IntPtr = New IntPtr(WinAPI.API.SendMessage(hWnd, WinAPI.API.TreeViewMessages.TVM_GETNEXTITEM, WinAPI.API.TreeViewItemSelFlags.TVGN_ROOT, IntPtr.Zero))
    '    Dim Child As IntPtr = RootItem
    '    Dim CurrentIndex As Integer = 0
    '    If (itemIndex > 0) Then
    '        While (Child <> IntPtr.Zero)
    '            Child = New IntPtr(WinAPI.NativeFunctions.SendMessage(hWnd, WinAPI.API.TreeViewMessages.TVM_GETNEXTITEM, New IntPtr(WinAPI.API.TreeViewItemSelFlags.TVGN_CHILD), Child))
    '            CurrentIndex += 1
    '            If (itemIndex = CurrentIndex) Then
    '                Exit While
    '            End If
    '        End While
    '    End If
    '    Dim itemAsHwnd As IntPtr = Child

    '    If (itemAsHwnd = IntPtr.Zero) Then
    '        Throw New SlickTestAPIException("Unable to find item # " & itemIndex)
    '    End If

    '    Try
    '        tvItem = New TVITEM()
    '        lpLocalBuffer = Marshal.AllocHGlobal(dwBufferSize)
    '        ' Get the process id owning the window
    '        threadId = GetWindowThreadProcessId(hWnd, dwProcessID)
    '        If (threadId = IntPtr.Zero) OrElse (dwProcessID = 0) Then
    '            Throw New ApplicationException("Failed to access process.")
    '        End If

    '        ' Open the process with all access
    '        hProcess = OpenProcess(PROCESS_ALL_ACCESS, False, dwProcessID)
    '        If hProcess = IntPtr.Zero Then
    '            Throw New ApplicationException("Failed to access process.")
    '        End If

    '        ' Allocate a buffer in the remote process
    '        lpRemoteBuffer = VirtualAllocEx(hProcess, IntPtr.Zero, _
    '                                        Marshal.SizeOf(GetType(TV_ITEM)), _
    '                                        MEM_COMMIT, PAGE_READWRITE)
    '        If lpRemoteBuffer = IntPtr.Zero Then
    '            Throw New SystemException("Failed to allocate memory in remote process.")
    '        End If

    '        ' Fill in the tvItem struct, this is in your own process
    '        ' Set the pszText member to somewhere in the remote buffer,
    '        ' For the example I used the address imediately following the tvItem stuct
    '        tvItem.mask = WinAPI.API.TreeViewItemFlags.TVIF_STATE
    '        If (SelectItems) Then
    '            tvItem.state = WinAPI.API.TreeViewItemState.TVIS_SELECTED '15?
    '        Else
    '            tvItem.state = 0
    '        End If
    '        tvItem.stateMask = WinAPI.API.TreeViewItemState.TVIS_SELECTED
    '        tvItem.hItem = itemAsHwnd

    '        ' Copy the local tvItem to the remote buffer
    '        bSuccess = WriteProcessMemory(hProcess, lpRemoteBuffer, tvItem, Marshal.SizeOf(GetType(TV_ITEM)), IntPtr.Zero)
    '        If Not bSuccess Then
    '            Throw New SystemException("Failed to write to process memory.")
    '        End If

    '        ' Send the message to the remote window with the address of the remote buffer
    '        SendMessage(hWnd, TreeViewMessages.TVM_SELECTITEM, itemAsHwnd, lpRemoteBuffer)

    '        ' Read the struct back from the remote process into local buffer
    '        bSuccess = ReadProcessMemory(hProcess, lpRemoteBuffer, lpLocalBuffer, dwBufferSize, IntPtr.Zero)
    '        If Not bSuccess Then
    '            Throw New SystemException("Failed to read from process memory.")
    '        End If

    '        ' At this point the lpLocalBuffer contains the returned LV_ITEM structure
    '        ' the next line extracts the text from the buffer into a managed string
    '        'retval = Marshal.PtrToStringAnsi(CType((lpLocalBuffer.ToInt32() + Marshal.SizeOf(GetType(LV_ITEM))), IntPtr))
    '    Finally
    '        If lpLocalBuffer <> IntPtr.Zero Then
    '            Marshal.FreeHGlobal(lpLocalBuffer)
    '        End If
    '        If lpRemoteBuffer <> IntPtr.Zero Then
    '            VirtualFreeEx(hProcess, lpRemoteBuffer, 0, MEM_RELEASE)
    '        End If
    '        If hProcess <> IntPtr.Zero Then
    '            CloseHandle(hProcess)
    '        End If
    '    End Try

    'End Sub

    Private Shared Function ReadTreeViewItem(ByVal Hwnd As IntPtr, ByVal Item As IntPtr) As String
        Dim text As String = String.Empty
        Const dwBufferSize As Integer = 1024

        Dim dwProcessID As Integer
        Dim bSuccess As Boolean
        Dim hProcess As IntPtr = IntPtr.Zero
        Dim lpRemoteBuffer As IntPtr = IntPtr.Zero
        Dim lpLocalBuffer As IntPtr = IntPtr.Zero
        Dim threadId As IntPtr = IntPtr.Zero
        Dim tvItem As New TVITEM()
        Try
            lpLocalBuffer = Marshal.AllocHGlobal(dwBufferSize)
            ' Get the process id owning the window
            threadId = GetWindowThreadProcessId(Hwnd, dwProcessID)
            If (threadId = IntPtr.Zero) OrElse (dwProcessID = 0) Then
                Throw New System.ApplicationException("Failed to access process.  Process id: " & dwProcessID)
            End If

            ' Open the process with all access
            hProcess = OpenProcess(PROCESS_ALL_ACCESS, False, dwProcessID)
            If hProcess = IntPtr.Zero Then
                Throw New System.ApplicationException("Failed to access process.  Process id: " & dwProcessID)
            End If

            ' Allocate a buffer in the remote process
            lpRemoteBuffer = VirtualAllocEx(hProcess, IntPtr.Zero, dwBufferSize, MEM_COMMIT, PAGE_READWRITE)
            If lpRemoteBuffer = IntPtr.Zero Then
                Throw New SlickTestAPIException("Failed to allocate memory in remote process.")
            End If


            tvItem.hItem = Item
            tvItem.cchTextMax = dwBufferSize
            tvItem.mask = TVIF_TEXT
            If (WinAPI.API.Is64Bit()) Then
                tvItem.pszText = CType((lpRemoteBuffer.ToInt64() + Marshal.SizeOf(GetType(TVITEM))), IntPtr)
            Else
                tvItem.pszText = CType((lpRemoteBuffer.ToInt32() + Marshal.SizeOf(GetType(TVITEM))), IntPtr)
            End If

            'tvItem.stateMask = Convert.ToInt32(TreeViewItemState.TVIS_SELECTED)
            'ptrTvi = Marshal.AllocHGlobal(Marshal.SizeOf(tvItem))
            'Marshal.StructureToPtr(tvItem, ptrTvi, False)

            bSuccess = WriteProcessMemory(hProcess, lpRemoteBuffer, tvItem, Marshal.SizeOf(GetType(TVITEM)), IntPtr.Zero)

            If Not bSuccess Then
                Throw New SlickTestAPIException("Failed to write to process memory.")
            End If

            ' Send the message to the remote window with the address of the remote buffer
            'SendMessage(Hwnd, WinAPI.API.TreeViewMessages.TVM_GETITEMW, 0, lpRemoteBuffer)
            SendMessage(Hwnd, WinAPI.API.TreeViewMessages.TVM_GETITEMW, IntPtr.Zero, lpRemoteBuffer)

            ' Read the struct back from the remote process into local buffer
            bSuccess = ReadProcessMemory(hProcess, lpRemoteBuffer, lpLocalBuffer, dwBufferSize, IntPtr.Zero)
            If Not bSuccess Then
                Throw New SlickTestAPIException("Failed to read from process memory.")
            End If

            ' At this point the lpLocalBuffer contains the returned TVITEM structure
            ' the next line extracts the text from the buffer into a managed string
            If (WinAPI.API.Is64Bit()) Then
                text = Marshal.PtrToStringAuto(CType((lpLocalBuffer.ToInt64() + Marshal.SizeOf(GetType(TVITEM))), IntPtr))
            Else
                text = Marshal.PtrToStringAuto(CType((lpLocalBuffer.ToInt32() + Marshal.SizeOf(GetType(TVITEM))), IntPtr))
            End If
            'text = Marshal.PtrToStringAuto(tvItem.pszText)
        Catch ex As Exception
        Finally
            If lpLocalBuffer <> IntPtr.Zero Then
                Marshal.FreeHGlobal(lpLocalBuffer)
            End If
            If lpRemoteBuffer <> IntPtr.Zero Then
                VirtualFreeEx(hProcess, lpRemoteBuffer, 0, MEM_RELEASE)
            End If
            If hProcess <> IntPtr.Zero Then
                CloseHandle(hProcess)
            End If
        End Try
        Return text
    End Function

End Class