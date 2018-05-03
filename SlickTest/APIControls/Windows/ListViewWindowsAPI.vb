Imports winAPI.API
Imports winAPI.NativeFunctions
Imports System.Runtime.InteropServices
Imports System.Text
Imports System.ComponentModel
Imports System
Imports System.Collections

Friend Class ListViewWindowsAPI
    Private Shared WindowsFunctions As APIControls.IndependentWindowsFunctionsv1

    Public Sub New(ByVal wf As APIControls.IndependentWindowsFunctionsv1)
        WindowsFunctions = wf
    End Sub

    Public Function IsListView(ByVal hWnd As IntPtr) As Boolean
        If (GenericMethodsUIAutomation.IsWPFOrCustom(hWnd) = True) Then
            Return WindowsFunctions.WpfListView.IsListView(hWnd)
        End If

        If (GetItemCount(hWnd) > 0) Then
            Return True
        Else
            If (WindowsFunctions.GetClassNameNoDotNet(hWnd).ToLowerInvariant.IndexOf("listview") <> -1) Then
                Return True
            End If
            Return False
        End If
    End Function

    Public Function GetColumns(ByVal hwnd As IntPtr) As String()
        Dim ColCount As Integer = GetColumnCount(hwnd) - 1
        Dim Cols(ColCount) As String
        For iCol As Integer = 0 To ColCount
            Cols(iCol) = GetColumnName(hwnd, iCol)
        Next
        Return Cols
    End Function

    Public Function FindColumnNumber(ByVal hwnd As IntPtr, ByVal ColumnNameWildCard As String) As Integer
        For ColumnNumber As Integer = 0 To Me.GetColumnCount(hwnd) - 1
            If (Me.GetColumnName(hwnd, ColumnNumber) Like ColumnNameWildCard) Then
                Return ColumnNumber
            End If
        Next
        Throw New SlickTestAPIException("Unable to find column with the text '" & ColumnNameWildCard & "'.")
    End Function

    Public Function FindRowNumber(ByVal hwnd As IntPtr, ByVal RowTextWildCard As String) As Integer
        For RowNumber As Integer = 0 To Me.GetItemCount(hwnd) - 1
            For Each Row As String In Me.GetRow(hwnd, RowNumber)
                If (Row Like RowTextWildCard) Then
                    Return RowNumber
                End If
            Next
        Next
        Throw New SlickTestAPIException("Unable to find row with the text '" & RowTextWildCard & "'.")
    End Function

    Public Function GetItemCount(ByVal hwnd As IntPtr) As Integer
        Return WinAPI.NativeFunctions.SendMessage(hwnd, ListViewMessages.LVM_GETITEMCOUNT, IntPtr.Zero, IntPtr.Zero)
    End Function

    Public Function GetColumnCount(ByVal hwnd As IntPtr) As Integer
        Dim HeaderHwnd As IntPtr = New IntPtr(WinAPI.NativeFunctions.SendMessage(hwnd, ListViewMessages.LVM_GETHEADER, IntPtr.Zero, IntPtr.Zero))
        Return WinAPI.NativeFunctions.SendMessage(HeaderHwnd, HeaderMessages.HDM_GETITEMCOUNT, IntPtr.Zero, IntPtr.Zero)
    End Function

    Public Function GetColumnName(ByVal hwnd As IntPtr, ByVal Column As Integer) As String
        If (GetColumnCount(hwnd) >= Column) Then
            Dim HeaderHwnd As IntPtr = New IntPtr(WinAPI.NativeFunctions.SendMessage(hwnd, ListViewMessages.LVM_GETHEADER, IntPtr.Zero, IntPtr.Zero))
            Return ReadListViewHeader(HeaderHwnd, Column)
        Else
            Throw New SlickTestAPIException("Invalid column number provided.  Object contains only " & GetColumnCount(hwnd) & " columns which is less than " & Column & ".")
        End If
    End Function

    Public Function GetRow(ByVal hwnd As IntPtr, ByVal Row As Integer) As String()
        Dim ColCount As Integer = GetColumnCount(hwnd)
        Dim str(ColCount) As String
        For i As Integer = 0 To ColCount
            str(i) = ReadListViewItem(hwnd, Row, i)
        Next
        Return str
    End Function

    Public Function GetEntireList(ByVal hwnd As IntPtr) As String(,)
        Dim ColCount As Integer = GetColumnCount(hwnd)
        Dim RowCount As Integer = GetItemCount(hwnd)

        Dim str(RowCount, ColCount) As String
        If (RowCount <= 0) Then Return str
        For iCol As Integer = 0 To ColCount
            For iRow As Integer = 0 To RowCount
                str(iRow, iCol) = ReadListViewItem(hwnd, iRow, iCol)
            Next
        Next
        Return str
    End Function

    Public Function GetEntireListForPrinting(ByVal hwnd As IntPtr) As String
        Dim List(,) As String = GetEntireList(hwnd)
        Dim RetStr As New System.Text.StringBuilder((List.GetUpperBound(0) + List.GetUpperBound(1)) * 20)
        RetStr.Append("Columns: ")

        For iCol As Integer = 0 To List.GetUpperBound(1) - 1
            RetStr.Append(GetColumnName(hwnd, iCol) & ", ")
        Next
        Try
            RetStr.Remove(RetStr.Length - 2, 2)
            RetStr.Append(vbNewLine)
        Catch ex As Exception
            'don't care
        End Try
        If (List.GetUpperBound(0) >= 0) Then
            RetStr.Append("Row 1: ")
        Else
            Return RetStr.ToString()
        End If

        For iRow As Integer = 0 To List.GetUpperBound(0) - 1
            If (iRow <> 0) Then
                RetStr.Append(vbNewLine & "Row " & iRow + 1 & ": ")
            End If
            For iCol As Integer = 0 To List.GetUpperBound(1)
                RetStr.Append(List(iRow, iCol) & ", ")
            Next
            RetStr.Remove(RetStr.Length - 2, 2)
        Next
        Return RetStr.ToString()
    End Function

    Public Function GetSelectedItemCount(ByVal hwnd As IntPtr) As Integer
        Return WinAPI.NativeFunctions.SendMessage(hwnd, ListViewMessages.LVM_GETSELECTEDCOUNT, IntPtr.Zero, IntPtr.Zero)
    End Function

    Public Sub SetColumnWidth(ByVal hwnd As IntPtr, ByVal Column As Integer, ByVal ColumnSizeInPixels As Integer)
        Dim RetVal As Integer
        Try
            RetVal = WinAPI.API.SendMessageTimeoutInt(hwnd, ListViewMessages.LVM_SETCOLUMNWIDTH, Column, ColumnSizeInPixels)
        Catch ex As Exception
            Throw New SlickTestAPIException("Unable to resize column.")
        End Try
        If (Convert.ToBoolean(RetVal) = False) Then
            Throw New SlickTestAPIException("Unable to resize column.")
        End If
    End Sub

    Public Function GetColumnWidth(ByVal hwnd As IntPtr, ByVal Column As Integer) As Integer
        Try
            Return WinAPI.API.SendMessageTimeoutInt(hwnd, ListViewMessages.LVM_GETCOLUMNWIDTH, Column, 0)
        Catch ex As Exception
            Throw New SlickTestAPIException("Unable to get column size.")
        End Try
    End Function

    Public Function GetSelectedItems(ByVal hwnd As IntPtr) As Integer()
        Dim Count As Integer = GetSelectedItemCount(hwnd) - 1
        Dim RetVal(Count) As Integer
        Dim Item As Integer = -1
        Dim Counter As Integer = 0
        While (Counter < Count + 1)
            Try
                Item = WinAPI.API.SendMessageTimeoutInt(hwnd, ListViewMessages.LVM_GETNEXTITEM, Item, ListViewNotifyItem.LVNI_SELECTED)
            Catch ex As Exception
                Throw New SlickTestAPIException("Unable to get selected items.")
            End Try
            RetVal(Counter) = Item
            Counter += 1
        End While
        Return RetVal
    End Function

    Public Sub SetSelectedItems(ByVal hwnd As IntPtr, ByVal Items() As Integer)
        For Each item As Integer In Items
            SetSelectedItem(hwnd, item, True)
        Next
        'If (WinAPI.API.SendMessageTimeoutInt(hwnd, ListViewMessages.LVM_SETITEMSTATE, Items(Count), ListViewNotifyItem.LVNI_SELECTED, Results) = False) Then
        '    Throw New Exception("Unable to set selected items.")
        'End If       
    End Sub

    Public Sub SelectAll(ByVal hwnd As IntPtr)
        SetSelectedItem(hwnd, -1, True)
        'Dim Results As Integer
        'If (WinAPI.API.SendMessageTimeoutInt(hwnd, ListViewMessages.LVM_SETITEMSTATE, -1, ListViewNotifyItem.LVNI_SELECTED, Results) = False) Then
        '    Throw New Exception("Unable to set selected items.")
        'End If
    End Sub

    Public Sub UnselectAll(ByVal hwnd As IntPtr)
        SetSelectedItem(hwnd, -1, False)
    End Sub



#Region "Read Listview item - NOTE: This all must be in the same class (assembly?) to work, or so it appears."

    Const LVM_GETITEM As Integer = 4101
    Const LVM_SETITEM As Integer = 4102
    Const LVIF_TEXT As Integer = 1
    Const PROCESS_ALL_ACCESS As UInteger = CInt((983040 Or 1048576 Or 4095))
    Const MEM_COMMIT As UInteger = 4096
    Const MEM_RELEASE As UInteger = 32768
    Const PAGE_READWRITE As UInteger = 4
    Public Const HDI_FORMAT As Integer = 4
    Public Const HDI_TEXT As Integer = 2
    Private Const HDF_LEFT As Integer = 0
    Private Const HDF_STRING As Integer = 16384
    Private Const HDF_SORTUP As Integer = 1024
    Private Const HDF_SORTDOWN As Integer = 512
    Private Const LVM_FIRST As Integer = 4096
    Private Const LVM_GETHEADER As Integer = (LVM_FIRST + 31)
    Private Const HDM_FIRST As Integer = 4608
    Private Const HDM_GETITEM As Integer = HDM_FIRST + 11
    Private Const HDM_SETITEM As Integer = HDM_FIRST + 12

    'Declare this structure first
    <System.Runtime.InteropServices.StructLayout(LayoutKind.Sequential, CharSet:=CharSet.Ansi)> _
    Private Structure HDITEM
        Public mask As UInteger
        Public cxy As Integer
        Public pszText As IntPtr
        Public hbm As IntPtr
        Public cchTextMax As Integer
        Public fmt As Integer
        Public lParam As IntPtr
        Public iImage As Integer
        Public iOrder As Integer
        Public type As UInteger
        Public pvFilter As IntPtr
    End Structure


    <DllImport("user32.dll")> _
    Private Shared Function SendMessage(ByVal hWnd As IntPtr, ByVal msg As Int32, ByVal wParam As Int32, ByVal lParam As IntPtr) As Boolean
    End Function

    <DllImport("user32.dll", EntryPoint:="SendMessage")> _
    Private Shared Function SendMessageHDItem(ByVal hWnd As IntPtr, ByVal msg As Int32, ByVal wParam As Int32, ByRef lParam As HDITEM) As Boolean
    End Function

    <DllImport("user32.dll", EntryPoint:="SendMessage")> _
    Private Shared Function SendMessageLVItem(ByVal hWnd As IntPtr, ByVal msg As Int32, ByVal wParam As Int32, ByRef lParam As LV_ITEM) As Boolean
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
    Private Shared Function WriteProcessMemory(ByVal hProcess As IntPtr, ByVal lpBaseAddress As IntPtr, ByRef buffer As LV_ITEM, ByVal dwSize As Integer, ByVal lpNumberOfBytesWritten As IntPtr) As Boolean
    End Function

    <DllImport("kernel32")> _
    Private Shared Function WriteProcessMemory(ByVal hProcess As IntPtr, ByVal lpBaseAddress As IntPtr, ByRef buffer As HDITEM, ByVal dwSize As Integer, ByVal lpNumberOfBytesWritten As IntPtr) As Boolean
    End Function

    <DllImport("kernel32")> _
    Private Shared Function ReadProcessMemory(ByVal hProcess As IntPtr, ByVal lpBaseAddress As IntPtr, ByVal lpBuffer As IntPtr, ByVal dwSize As Integer, ByVal lpNumberOfBytesRead As IntPtr) As Boolean
    End Function

    <DllImport("kernel32")> _
    Private Shared Function CloseHandle(ByVal hObject As IntPtr) As Boolean
    End Function

    <StructLayout(LayoutKind.Sequential)> _
    Private Structure LV_ITEM
        Public mask As Integer 'As UInteger
        Public iItem As Integer
        Public iSubItem As Integer
        Public state As Integer 'As UInteger
        Public stateMask As Integer 'As UInteger
        Public pszText As IntPtr
        Public cchTextMax As Integer
        Public iImage As Integer
    End Structure

    Public Shared Sub SetSelectedItem(ByVal hWnd As IntPtr, ByVal item As Integer, ByVal SelectItems As Boolean)
        Const dwBufferSize As Integer = 1024
        'WinAPI.API.ListViewItemState.LVIS_SELECTED
        Dim dwProcessID As Integer
        Dim lvItem As LV_ITEM
        Dim retval As Boolean = False
        Dim bSuccess As Boolean
        Dim hProcess As IntPtr = IntPtr.Zero
        Dim lpRemoteBuffer As IntPtr = IntPtr.Zero
        Dim lpLocalBuffer As IntPtr = IntPtr.Zero
        Dim threadId As IntPtr = IntPtr.Zero

        Try
            lvItem = New LV_ITEM()
            lpLocalBuffer = Marshal.AllocHGlobal(dwBufferSize)
            ' Get the process id owning the window
            threadId = GetWindowThreadProcessId(hWnd, dwProcessID)
            If (threadId = IntPtr.Zero) OrElse (dwProcessID = 0) Then
                Throw New ApplicationException("Failed to access process.")
            End If

            ' Open the process with all access
            hProcess = OpenProcess(PROCESS_ALL_ACCESS, False, dwProcessID)
            If hProcess = IntPtr.Zero Then
                Throw New ApplicationException("Failed to access process.")
            End If

            ' Allocate a buffer in the remote process
            lpRemoteBuffer = VirtualAllocEx(hProcess, IntPtr.Zero, _
                                            Marshal.SizeOf(GetType(LV_ITEM)), MEM_COMMIT, PAGE_READWRITE)
            If lpRemoteBuffer = IntPtr.Zero Then
                Throw New SystemException("Failed to allocate memory in remote process.")
            End If

            ' Fill in the LVITEM struct, this is in your own process
            ' Set the pszText member to somewhere in the remote buffer,
            ' For the example I used the address imediately following the LVITEM stuct
            lvItem.mask = WinAPI.API.ListViewItemFlags.LVIF_STATE
            If (SelectItems) Then
                lvItem.state = WinAPI.API.ListViewItemState.LVIS_SELECTED '15?
            Else
                lvItem.state = 0
            End If
            lvItem.stateMask = WinAPI.API.ListViewItemState.LVIS_SELECTED Xor WinAPI.API.ListViewItemState.LVIS_FOCUSED
            lvItem.iItem = item
            'lvItem.pszText = CType((lpRemoteBuffer.ToInt32() + Marshal.SizeOf(GetType(LV_ITEM))), IntPtr)
            'lvItem.cchTextMax = 256

            ' Copy the local LVITEM to the remote buffer
            bSuccess = WriteProcessMemory(hProcess, lpRemoteBuffer, lvItem, Marshal.SizeOf(GetType(LV_ITEM)), IntPtr.Zero)
            If Not bSuccess Then
                Throw New SystemException("Failed to write to process memory.")
            End If

            ' Send the message to the remote window with the address of the remote buffer
            SendMessage(hWnd, ListViewMessages.LVM_SETITEMSTATE, item, lpRemoteBuffer)

            ' Read the struct back from the remote process into local buffer
            bSuccess = ReadProcessMemory(hProcess, lpRemoteBuffer, lpLocalBuffer, dwBufferSize, IntPtr.Zero)
            If Not bSuccess Then
                Throw New SystemException("Failed to read from process memory.")
            End If

            ' At this point the lpLocalBuffer contains the returned LV_ITEM structure
            ' the next line extracts the text from the buffer into a managed string
            'retval = Marshal.PtrToStringAnsi(CType((lpLocalBuffer.ToInt32() + Marshal.SizeOf(GetType(LV_ITEM))), IntPtr))
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

    End Sub

    Public Shared Function ReadListViewItem(ByVal hWnd As IntPtr, ByVal item As Integer, Optional ByVal ColumnNumber As Integer = 0) As String
        Const dwBufferSize As Integer = 1024

        Dim dwProcessID As Integer
        Dim lvItem As LV_ITEM
        Dim retval As String
        Dim bSuccess As Boolean
        Dim hProcess As IntPtr = IntPtr.Zero
        Dim lpRemoteBuffer As IntPtr = IntPtr.Zero
        Dim lpLocalBuffer As IntPtr = IntPtr.Zero
        Dim threadId As IntPtr = IntPtr.Zero

        Try
            lvItem = New LV_ITEM()
            lpLocalBuffer = Marshal.AllocHGlobal(dwBufferSize)
            ' Get the process id owning the window
            threadId = GetWindowThreadProcessId(hWnd, dwProcessID)
            If (threadId = IntPtr.Zero) OrElse (dwProcessID = 0) Then
                Throw New SlickTestAPIException("Failed to access process.")
            End If

            ' Open the process with all access
            hProcess = OpenProcess(PROCESS_ALL_ACCESS, False, dwProcessID)
            If hProcess = IntPtr.Zero Then
                Throw New SlickTestAPIException("Failed to access process.")
            End If

            ' Allocate a buffer in the remote process
            lpRemoteBuffer = VirtualAllocEx(hProcess, IntPtr.Zero, dwBufferSize, MEM_COMMIT, PAGE_READWRITE)
            If lpRemoteBuffer = IntPtr.Zero Then
                Throw New SlickTestAPIException("Failed to allocate memory in remote process.")
            End If

            ' Fill in the LVITEM struct, this is in your own process
            ' Set the pszText member to somewhere in the remote buffer,
            ' For the example I used the address imediately following the LVITEM stuct
            lvItem.mask = LVIF_TEXT
            lvItem.iItem = item
            If (WinAPI.API.Is64Bit()) Then
                lvItem.pszText = CType((lpRemoteBuffer.ToInt64() + Marshal.SizeOf(GetType(LV_ITEM))), IntPtr)
            Else
                lvItem.pszText = CType((lpRemoteBuffer.ToInt32() + Marshal.SizeOf(GetType(LV_ITEM))), IntPtr)
            End If
            lvItem.cchTextMax = 256
            lvItem.iSubItem = ColumnNumber

            ' Copy the local LVITEM to the remote buffer
            bSuccess = WriteProcessMemory(hProcess, lpRemoteBuffer, lvItem, Marshal.SizeOf(GetType(LV_ITEM)), IntPtr.Zero)
            If Not bSuccess Then
                Throw New SlickTestAPIException("Failed to write to process memory.")
            End If

            ' Send the message to the remote window with the address of the remote buffer
            SendMessage(hWnd, LVM_GETITEM, 0, lpRemoteBuffer)

            ' Read the struct back from the remote process into local buffer
            bSuccess = ReadProcessMemory(hProcess, lpRemoteBuffer, lpLocalBuffer, dwBufferSize, IntPtr.Zero)
            If Not bSuccess Then
                Throw New SlickTestAPIException("Failed to read from process memory.")
            End If

            ' At this point the lpLocalBuffer contains the returned LV_ITEM structure
            ' the next line extracts the text from the buffer into a managed string
            If (WinAPI.API.Is64Bit()) Then
                retval = Marshal.PtrToStringAnsi(CType((lpLocalBuffer.ToInt64() + Marshal.SizeOf(GetType(LV_ITEM))), IntPtr))
            Else
                retval = Marshal.PtrToStringAnsi(CType((lpLocalBuffer.ToInt32() + Marshal.SizeOf(GetType(LV_ITEM))), IntPtr))
            End If
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
        Return retval
    End Function

    Public Shared Function ReadListViewHeader(ByVal hWnd As IntPtr, ByVal ColumnNumber As Integer) As String
        Const dwBufferSize As Integer = 1024

        Dim dwProcessID As Integer
        Dim hdItem As HDITEM
        Dim retval As String
        Dim bSuccess As Boolean
        Dim hProcess As IntPtr = IntPtr.Zero
        Dim lpRemoteBuffer As IntPtr = IntPtr.Zero
        Dim lpLocalBuffer As IntPtr = IntPtr.Zero
        Dim threadId As IntPtr = IntPtr.Zero

        Try
            'lvItem = New HDITEM()
            'lvItem.mask = HDI_FORMAT

            'SendMessageHDItem(hWnd, HDM_GETITEM, 0, lvItem)

            lpLocalBuffer = Marshal.AllocHGlobal(dwBufferSize)
            ' Get the process id owning the window
            threadId = GetWindowThreadProcessId(hWnd, dwProcessID)
            If (threadId = IntPtr.Zero) OrElse (dwProcessID = 0) Then
                Throw New ArgumentException("Unable to communicate with the Windows Object.")
            End If

            ' Open the process with all access
            hProcess = OpenProcess(PROCESS_ALL_ACCESS, False, dwProcessID)
            If hProcess = IntPtr.Zero Then
                Throw New ApplicationException("Failed to access process.")
            End If

            ' Allocate a buffer in the remote process
            lpRemoteBuffer = VirtualAllocEx(hProcess, IntPtr.Zero, dwBufferSize, MEM_COMMIT, PAGE_READWRITE)
            If lpRemoteBuffer = IntPtr.Zero Then
                Throw New SystemException("Failed to allocate memory in remote process.")
            End If

            ' Fill in the LVITEM struct, this is in your own process
            ' Set the pszText member to somewhere in the remote buffer,
            ' For the example I used the address imediately following the LVITEM stuct
            hdItem.mask = HDI_TEXT
            hdItem.fmt = HDF_STRING Xor HDF_LEFT
            If (WinAPI.API.Is64Bit()) Then
                hdItem.pszText = CType((lpRemoteBuffer.ToInt64() + Marshal.SizeOf(GetType(HDITEM))), IntPtr)

            Else
                hdItem.pszText = CType((lpRemoteBuffer.ToInt32() + Marshal.SizeOf(GetType(HDITEM))), IntPtr)

            End If
            hdItem.cchTextMax = 256

            ' Copy the local LVITEM to the remote buffer
            bSuccess = WriteProcessMemory(hProcess, lpRemoteBuffer, hdItem, Marshal.SizeOf(GetType(HDITEM)), IntPtr.Zero)
            If Not bSuccess Then
                Throw New SystemException("Failed to write to process memory.")
            End If

            ' Send the message to the remote window with the address of the remote buffer
            If (SendMessage(hWnd, HDM_GETITEM, ColumnNumber, lpRemoteBuffer) = False) Then
                Throw New SlickTestAPIException("Failed to get column " & ColumnNumber & ".")
            End If

            ' Read the struct back from the remote process into local buffer
            bSuccess = ReadProcessMemory(hProcess, lpRemoteBuffer, lpLocalBuffer, dwBufferSize, IntPtr.Zero)
            If Not bSuccess Then
                Throw New SystemException("Failed to read from process memory.")
            End If

            'At this point the lpLocalBuffer contains the returned HDITEM structure
            'the next line extracts the text from the buffer into a managed string
            If (WinAPI.API.Is64Bit()) Then
                retval = Marshal.PtrToStringAuto(CType((lpLocalBuffer.ToInt64() + Marshal.SizeOf(GetType(HDITEM))), IntPtr))
            Else
                retval = Marshal.PtrToStringAuto(CType((lpLocalBuffer.ToInt32() + Marshal.SizeOf(GetType(HDITEM))), IntPtr))

            End If
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
        Return retval
    End Function

#End Region

#Region "Old"
    'Public Shared Sub SetSelectedItem(ByVal hWnd As IntPtr, ByVal item As Integer, ByVal SelectItems As Boolean)
    'Dim lvItem As New LV_ITEM()
    '' Fill in the LVITEM struct, this is in your own process
    '' Set the pszText member to somewhere in the remote buffer,
    '' For the example I used the address imediately following the LVITEM stuct
    'lvItem.mask = WinAPI.API.ListViewItemFlags.LVIF_STATE
    'If (SelectItems = True) Then
    '    lvItem.state = 0 '&HF 'WinAPI.API.ListViewItemState.LVIS_SELECTED Xor ListViewItemState.LVIS_FOCUSED
    'End If
    'lvItem.stateMask = WinAPI.API.ListViewItemState.LVIS_SELECTED Xor ListViewItemState.LVIS_FOCUSED
    'lvItem.iItem = item
    'SendMessageLVItem(hWnd, ListViewMessages.LVM_SETITEMSTATE, item, lvItem)
    'End Sub
#End Region

End Class
