Imports winAPI.API
Imports winAPI.NativeFunctions
Imports System.Runtime.InteropServices
Imports System.Text
Imports System.ComponentModel
Imports System
Imports System.Collections
Friend Class EnumerateWindows
    Public Shared isRunningThreaded As Boolean = False
    Private Shared sPatternName As String
    Private Shared sPatternValue As String
    Private Shared hFind As IntPtr
    Private Shared WindowHandles As New ArrayList
    Private Shared ChildHandles As New ArrayList
    Private Shared ParentHandle As IntPtr = IntPtr.Zero
    Private Shared WindowsFunctions As New APIControls.IndependentWindowsFunctionsv1()

    Private Shared Function EnumWinProc(ByVal hwnd As IntPtr, ByVal lParam As Integer) As Boolean
        'If IsWindowVisible(hwnd) And GetParent(hwnd) = 0 Then
        If GetParent(hwnd) = IntPtr.Zero Then
            If (sPatternValue <> "" Or sPatternName <> "") Then
                Dim sName As String = WindowsFunctions.GetAllText(hwnd)
                'Dim sValue As String = WindowsFunctions.GetAllText(hwnd)
                'If (sName <> "") Then Console.WriteLine("title: " & sName)
                If lParam = 0 Then
                    sName = sName.ToUpper()
                    'sValue = sValue.ToUpper()
                End If
                If sName Like sPatternName Then
                    'If sValue Like sPatternValue Then
                    'Console.WriteLine("found: " & sName)
                    hFind = hwnd
                    Return False
                    'End If
                End If
            End If
        Else
            Return False
        End If
        Return True
    End Function

    Public Shared Function FindWindowWild(ByVal sWildName As String, ByVal sWildValue As String, Optional ByVal bMatchCase As Boolean = True) As IntPtr
        sPatternName = sWildName
        sPatternValue = sPatternName
        hFind = IntPtr.Zero
        If Not bMatchCase Then
            sPatternName = sPatternName.ToUpper()
            sPatternValue = sPatternValue.ToUpper()
        End If
        isRunningThreaded = True
        WinAPI.NativeFunctions.EnumWindows(AddressOf EnumWinProc, New IntPtr(Convert.ToInt32(bMatchCase)))
        isRunningThreaded = False
        Return hFind
    End Function

    Public Shared Function GetHandles() As IntPtr()
        ParentHandle = IntPtr.Zero
        isRunningThreaded = True
        WinAPI.NativeFunctions.EnumWindows(AddressOf EnumWindowsProc, Nothing)
        Dim winHandles(WindowHandles.Count - 1) As IntPtr
        WindowHandles.CopyTo(winHandles)
        WindowHandles.Clear()
        isRunningThreaded = False
        Return winHandles
    End Function

    Private Shared Function EnumWindowsProc(ByVal handle As IntPtr, ByVal parameter As Integer) As Boolean
        If (ParentHandle.Equals(IntPtr.Zero)) Then
            WindowHandles.Add(handle)
        Else
            Dim tmp As IntPtr = WindowsFunctions.GetParent(handle)
            'If (tmp.Equals(IntPtr.Zero) = False) Then Console.WriteLine("Handle: " & handle.ToString & ", " & WindowsFunctions.GetClassName(handle) & ", " & WindowsFunctions.GetText(handle))
            While (tmp.Equals(IntPtr.Zero) = False)
                If (ParentHandle.Equals(tmp)) Then
                    ChildHandles.Add(handle)
                    Return True
                End If
                tmp = WindowsFunctions.GetParent(tmp)
            End While
        End If
        Return True
    End Function

    Public Shared Function GetChildHandles(ByVal handle As IntPtr) As IntPtr()
        'winAPI.NativeFunctions.EnumChildWindows(handle, AddressOf HandleChildCallback, Nothing)
        ParentHandle = handle
        isRunningThreaded = True
        'winAPI.NativeFunctions.EnumWindows(AddressOf EnumWindowsProc, Nothing)
        WinAPI.NativeFunctions.EnumChildWindows(handle, AddressOf HandleChildCallback, Nothing)
        Dim cHandles(ChildHandles.Count - 1) As IntPtr
        ChildHandles.CopyTo(cHandles)
        ChildHandles.Clear()
        ParentHandle = IntPtr.Zero
        isRunningThreaded = False
        Return cHandles
    End Function

    Public Shared Function isChildDirectlyConnectedToParent(ByVal parent As IntPtr, ByVal child As IntPtr) As Boolean
        Dim tmpParents() As IntPtr = GetChildHandles(parent)
        For Each item As IntPtr In tmpParents
            If (item.Equals(child)) Then Return True
        Next
        Return False
    End Function

    Public Shared Sub Hide(ByVal title As String)
        For Each handle As IntPtr In GetHandles()
            If WindowsFunctions.GetAllText(handle).Contains(title) Then
                WinAPI.NativeFunctions.ShowWindow(handle, SW.HIDE)
            End If
        Next
    End Sub

    Private Shared Function HandleChildCallback(ByVal handle As IntPtr, ByVal parameter As Integer) As Boolean
        ChildHandles.Add(handle)
        Return True
    End Function

End Class

#Region "Old Code"

'Public Shared Function GetTitle(ByVal hWnd As IntPtr) As String
'    Return WindowsFunctions.GetAllText(hWnd)
'End Function

'Public Shared Sub Show(ByVal title As String)
'    For Each handle As IntPtr In GetHandles()
'        If GetTitle(handle).Contains(title) Then
'            WinAPI.NativeFunctions.ShowWindow(handle, SW.RESTORE)
'        End If
'    Next
'End Sub

'Public Shared Function SearchChildHandlesWild(ByVal handle As IntPtr, ByVal Name As String, ByVal Value As String) As IntPtr
'    If (handle = IntPtr.Zero) Then
'        Return IntPtr.Zero 'return zero if zero given
'    End If
'    Dim cls As String = ""
'    If (Value <> "") Then
'        Dim txt As String = ""
'        Dim results As Boolean
'        For Each childhandle As IntPtr In GetChildHandles(handle)
'            txt = WindowsFunctions.GetAllText(childhandle)
'            'If txt <> "" Then Console.WriteLine("txt = " & txt)
'            If txt <> "" AndAlso txt Like Value Then
'                results = True
'            Else
'                results = False
'            End If
'            If (results = True) Then
'                cls = WindowsFunctions.GetClassName(childhandle)
'                'If cls <> "" Then Console.WriteLine("cls = " & cls)
'                If cls <> "" AndAlso cls Like Name Then
'                    'Console.WriteLine("Found Item: " & "txt = " & txt & "cls = " & cls)
'                    Return childhandle
'                End If
'            End If
'        Next
'    Else
'        For Each childhandle As IntPtr In GetChildHandles(handle)
'            cls = WindowsFunctions.GetClassName(childhandle)
'            If cls <> "" AndAlso cls Like Name Then
'                Return childhandle
'            End If
'        Next
'    End If
'End Function

'Public Shared Function SearchChildHandles(ByVal handle As IntPtr, ByVal Name As String, ByVal Value As String) As IntPtr
'    'System.Console.WriteLine("Now searching in SearchChildHandles for " + handle.ToString() + " - " + Name + " - " + Value)
'    If (handle = IntPtr.Zero) Then
'        Return IntPtr.Zero 'return zero if zero given
'    End If
'    Dim cls As String = ""
'    If (Value <> "") Then
'        'System.Console.WriteLine("Searching with values!")
'        Dim txt As String = ""
'        Dim results As Boolean
'        For Each childhandle As IntPtr In GetChildHandles(handle)
'            txt = WindowsFunctions.GetAllText(childhandle)
'            'If txt <> "" Then Console.WriteLine("txt = " & txt)
'            If txt = Value Then
'                results = True
'            Else
'                results = False
'            End If
'            If (results = True) Then
'                cls = WindowsFunctions.GetClassName(childhandle)
'                'If cls <> "" Then Console.WriteLine("cls = " & cls)
'                If cls <> "" AndAlso cls = Name Then
'                    'If (IsWindowVisible(childhandle)) Then
'                    'Console.WriteLine("Found Item: " & "txt = " & txt & "cls = " & cls)
'                    Return childhandle
'                    'End If
'                End If
'            End If
'        Next
'    Else
'        'System.Console.WriteLine("Searching withOUT values!")
'        For Each childhandle As IntPtr In GetChildHandles(handle)
'            cls = WindowsFunctions.GetClassName(childhandle)
'            'If cls <> "" Then Console.WriteLine("cls = " & cls)
'            If cls = Name Then
'                'If (IsWindowVisible(childhandle)) Then
'                'Console.WriteLine("Found Item: cls = " & cls)
'                Return childhandle
'                'End If
'            End If
'        Next
'    End If
'    Return IntPtr.Zero
'End Function

'Public Shared Function Exists(ByVal title As String, ByVal text As String) As Boolean
'    Dim handle, childhandle As IntPtr
'    Dim currentTitle, currentChildTitle As String
'    For Each handle In GetHandles()
'        currentTitle = GetTitle(handle)
'        If currentTitle <> "" AndAlso currentTitle.Contains(title) Then
'            For Each childhandle In GetChildHandles(handle)
'                currentChildTitle = GetChildText(childhandle)
'                If currentChildTitle <> "" AndAlso currentChildTitle.Contains(text) Then
'                    Return True
'                End If
'            Next
'        End If
'    Next
'End Function

'Public Shared Function GetHandle(ByVal title As String) As IntPtr
'    For Each handle As IntPtr In GetHandles()
'        If GetTitle(handle).Contains(title) Then
'            Return handle
'        End If
'    Next
'End Function

'Public Shared Sub Close(ByVal title As String)
'    Dim handle As IntPtr
'    Dim currentTitle As String

'    For Each handle In GetHandles()
'        currentTitle = GetTitle(handle)
'        If currentTitle <> "" AndAlso currentTitle.Contains(title) Then
'            WinAPI.NativeFunctions.SendMessage(handle, WM.CLOSE, IntPtr.Zero, IntPtr.Zero)
'        End If
'    Next
'End Sub

'Public Shared Function GetChildText(ByVal handle As IntPtr) As String
'    Dim ip As IntPtr = Marshal.AllocHGlobal(1000)
'    WinAPI.NativeFunctions.SendMessage(handle, WM.GETTEXT, New IntPtr(1000), ip)
'    Dim ret As String = Marshal.PtrToStringAuto(ip)
'    Marshal.FreeHGlobal(ip)
'    Return ret
'End Function


#End Region
