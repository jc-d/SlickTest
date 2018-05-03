Imports winAPI.API
Imports winAPI.NativeFunctions
Imports System.Runtime.InteropServices
Imports System.Text
Imports System.ComponentModel
Imports System
Imports System.Collections
Imports System.Runtime.CompilerServices
<Assembly: InternalsVisibleTo("InterAction"), Assembly: InternalsVisibleTo("SlickTestDeveloper")> 
Friend Class ListBoxWindowsAPI
    Private Shared WindowsFunctions As APIControls.IndependentWindowsFunctionsv1

    Friend Sub New(ByVal wf As APIControls.IndependentWindowsFunctionsv1)
        WindowsFunctions = wf
    End Sub

    Public Function IsListBox(ByVal hWnd As IntPtr) As Boolean
        Dim lCount As Long
        'lCount = WinAPI.NativeFunctions.SendMessage(hWnd, LB_GETCOUNT, 0&, 0&)
        lCount = WinAPI.NativeFunctions.SendMessage(hWnd, LB_GETCOUNT, IntPtr.Zero, IntPtr.Zero)
        If (lCount = LB_ERR) Then
            Return False
        ElseIf (lCount = 0) Then
            If (WindowsFunctions.GetClassNameNoDotNet(hWnd).ToLowerInvariant.IndexOf(".listbox.") <> -1) Then
                Return True
            End If
            Return False
        End If
        Return True
    End Function

    'Public Shared Function GetListBoxItems1(ByVal lhWnd As IntPtr) As String()
    '    Dim lCount As Long, lLen As Long, N As Long
    '    Dim sItems() As String = {""}

    '    lCount = ListBoxItemsCount(lhWnd)
    '    If (lCount = LB_ERR) Or (lCount = 0) Then
    '        Return sItems
    '    End If
    '    ReDim sItems(lCount - 1)
    '    Dim sBuilder As System.Text.StringBuilder
    '    For N = 0 To lCount - 1
    '        sBuilder = New System.Text.StringBuilder()
    '        lLen = WinAPI.NativeFunctions.SendMessage(lhWnd, LB_GETTEXTLEN, N, 0&)
    '        sBuilder.Capacity = lLen + 1
    '        WinAPI.NativeFunctions.SendMessage(lhWnd, LB_GETTEXT, N, sBuilder)
    '        sItems(N) = sBuilder.ToString()
    '    Next N
    '    GetListBoxItems = sItems
    'End Function


    Public Function GetListBoxSelectedItems(ByVal lhWnd As IntPtr) As Integer()
        'Dim iNumItems As Long = ListBoxItemsCount(lhWnd)
        Dim iNumItems As Integer = ListBoxCount(lhWnd)
        Dim iItems() As Integer = {-1}
        If iNumItems > 0 Then
            ReDim iItems(iNumItems - 1)
            'If (WinAPI.NativeFunctions.SendMessage(lhWnd, LB_GETSELITEMS, iNumItems, _
            '  sItems(0)) = -1) Then
            '    Dim fail() As String = {""}
            '    Return fail
            'End If
            'This implementation SUCKS!  But it was the only way I could get it to work.
            Dim state As Boolean = False
            Dim CurrentItem As Integer = 0
            For i As Integer = 0 To iItems.GetLength(0) - 1
                iItems(i) = -1
            Next
            For i As Integer = 0 To Me.ListBoxCount(lhWnd) + 1
                state = (WinAPI.NativeFunctions.SendMessage(lhWnd, WinAPI.API.ListBoxMessages.LB_GETSEL, New IntPtr(i), IntPtr.Zero) <> 0)
                If (state = True) Then
                    iItems(CurrentItem) = i
                    CurrentItem += 1
                End If
            Next
        End If
        Return iItems
    End Function

    Public Function GetListBoxSelectedItem(ByVal hwnd As IntPtr) As Integer
        'if you have no selected items or you have a multi-select box then error out
        'If (WinAPI.NativeFunctions.SendMessage(hwnd, LB_GETSELCOUNT, 0, 0) <> 1) Then
        If (WinAPI.NativeFunctions.SendMessage(hwnd, LB_GETSELCOUNT, IntPtr.Zero, IntPtr.Zero) <> 1) Then
            'Return WinAPI.NativeFunctions.SendMessage(hwnd, LB_GETCURSEL, 0, 0)
            Return WinAPI.NativeFunctions.SendMessage(hwnd, LB_GETCURSEL, IntPtr.Zero, IntPtr.Zero)
        Else
            Return -1
        End If
    End Function

    Public Function SelectListBoxItem(ByVal hwnd As IntPtr, ByVal item As Integer) As Boolean
        Dim i As Integer = ListBoxItemsCount(hwnd)
        If (i > item) Then
            If (item >= 0) Then
                WinAPI.NativeFunctions.SendMessage(hwnd, LB_SETCURSEL, New IntPtr(item), IntPtr.Zero)
                'WinAPI.NativeFunctions.SendMessage(hwnd, LB_SETCURSEL, item, 0)
                Return True
            End If
        End If
        Return False

    End Function

    Public Function GetListBoxItem(ByVal index As Integer, ByVal hWnd As IntPtr) As String
        Dim sBuilder As System.Text.StringBuilder
        sBuilder = New System.Text.StringBuilder()
        'Dim lLen As Integer = WinAPI.NativeFunctions.SendMessage(hWnd, LB_GETTEXTLEN, index, 0&)
        Dim lLen As Integer = WinAPI.NativeFunctions.SendMessage(hWnd, LB_GETTEXTLEN, New IntPtr(index), IntPtr.Zero)
        sBuilder.Capacity = lLen + 1 '+1 for null char after string.  Not sure if this is needed.
        'winAPI.NativeFunctions.SendMessage(hWnd, LB_GETTEXT, index, sBuilder)
        WinAPI.API.SendMessage(hWnd, LB_GETTEXT, index, sBuilder)
        Return sBuilder.ToString()
    End Function

    Public Function GetListBoxItems(ByVal lhWnd As IntPtr) As String()
        'Dim lCount As Long, lLen As Long, N As Long
        Dim lCount As Integer, lLen As Integer, N As Integer
        Dim sItems() As String = {""}

        lCount = ListBoxItemsCount(lhWnd)
        If (lCount = LB_ERR) Or (lCount = 0) Then
            Return sItems
        End If
        ReDim sItems(lCount - 1)
        Dim sBuilder As System.Text.StringBuilder
        For N = 0 To lCount - 1
            sBuilder = New System.Text.StringBuilder()
            'lLen = WinAPI.NativeFunctions.SendMessage(lhWnd, LB_GETTEXTLEN, N, 0&)
            lLen = WinAPI.NativeFunctions.SendMessage(lhWnd, LB_GETTEXTLEN, New IntPtr(N), IntPtr.Zero)
            sBuilder.Capacity = lLen + 1
            WinAPI.API.SendMessage(lhWnd, LB_GETTEXT, N, sBuilder)
            'winAPI.NativeFunctions.SendMessage(lhWnd, LB_GETTEXT, N, sBuilder)
            sItems(N) = sBuilder.ToString()
        Next N
        GetListBoxItems = sItems
    End Function

    Public Function ListBoxCount(ByVal Hwnd As IntPtr) As Integer
        Return WinAPI.NativeFunctions.SendMessage(Hwnd, WinAPI.API.LB_GETSELCOUNT, IntPtr.Zero, IntPtr.Zero)
    End Function

    'Public Function ListBoxItemsCount(ByVal lhWnd As IntPtr) As Long
    Public Function ListBoxItemsCount(ByVal lhWnd As IntPtr) As Integer
        'Return WinAPI.NativeFunctions.SendMessage(lhWnd, LB_GETCOUNT, 0&, 0&)
        Return WinAPI.NativeFunctions.SendMessage(lhWnd, LB_GETCOUNT, IntPtr.Zero, IntPtr.Zero)
    End Function

End Class
