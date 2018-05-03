Imports winAPI.API
Imports winAPI.NativeFunctions
Imports System.Runtime.InteropServices
Imports System.Text
Imports System.ComponentModel
Imports System
Imports System.Collections
Imports System.Runtime.CompilerServices
<Assembly: InternalsVisibleTo("InterAction"), Assembly: InternalsVisibleTo("SlickTestDeveloper")> 
Friend Class ComboBoxWindowsAPI
    Private Shared WindowsFunctions As APIControls.IndependentWindowsFunctionsv1

    Public Function IsComboBox(ByVal hWnd As IntPtr) As Boolean
        If (GenericMethodsUIAutomation.IsWPFOrCustom(hWnd) = True) Then
            Return WindowsFunctions.WpfComboBox.IsComboBox(hWnd)
        End If

        Dim lCount As Long
        'lCount = WinAPI.NativeFunctions.SendMessage(hWnd, CB_GETCOUNT, 0&, 0&)
        lCount = WinAPI.NativeFunctions.SendMessage(hWnd, CB_GETCOUNT, IntPtr.Zero, IntPtr.Zero)
        If (lCount = CB_ERR) Then
            Return False
        ElseIf (lCount = 0) Then
            Dim ClassName As String = WindowsFunctions.GetClassNameNoDotNet(hWnd).ToLowerInvariant
            If (ClassName.Contains("combobox") = True) Then 'Should include "ComboBoxEx32"
                Return True
            End If
            Return False
        End If
        Return True
    End Function
    Friend Sub New(ByVal wf As APIControls.IndependentWindowsFunctionsv1)
        WindowsFunctions = wf
    End Sub

    'Public Function ComboBoxItemsCount(ByVal lhWnd As IntPtr) As Long
    '    Return WinAPI.NativeFunctions.SendMessage(lhWnd, CB_GETCOUNT, 0&, 0&)
    'End Function


    '/////////
    'Public Function GetComboBoxSelectedItems(ByVal lhWnd As IntPtr) As String()
    '    Dim iNumItems As Long = ListBoxItemsCount(lhWnd)
    '    Dim sItems() As String = {""}
    '    If iNumItems > 0 Then
    '        ReDim sItems(iNumItems - 1)
    '        WinAPI.NativeFunctions.SendMessage(lhWnd, CB_GETSELITEMS, iNumItems, _
    '          sItems(0))
    '    End If
    '    Return sItems
    'End Function

    Public Function GetComboBoxSelectedItem(ByVal hwnd As IntPtr) As Integer
        'if you have no selected items or you have a multi-select box then error out
        'If (WinAPI.NativeFunctions.SendMessage(hwnd, CB_GETSELCOUNT, 0, 0) <> 1) Then
        Return WinAPI.NativeFunctions.SendMessage(hwnd, CB_GETCURSEL, IntPtr.Zero, IntPtr.Zero)
        'Else
        'Return -1
        'End If
    End Function

    Public Function GetComboBoxItem(ByVal index As Integer, ByVal hWnd As IntPtr) As String
        Dim sBuilder As System.Text.StringBuilder
        sBuilder = New System.Text.StringBuilder()
        'Dim lLen As Integer = WinAPI.NativeFunctions.SendMessage(hWnd, CB_GETLBTEXTLEN, index, 0&)
        Dim lLen As Integer = WinAPI.NativeFunctions.SendMessage(hWnd, CB_GETLBTEXTLEN, New IntPtr(index), IntPtr.Zero)
        sBuilder.Capacity = lLen + 1 '+1 for null char after string.  Not sure if this is needed.
        'winAPI.NativeFunctions.SendMessage(hWnd, LB_GETTEXT, index, sBuilder)
        WinAPI.API.SendMessage(hWnd, CB_GETLBTEXT, index, sBuilder)
        Return sBuilder.ToString()
    End Function

    Public Function SelectComboBoxItem(ByVal hwnd As IntPtr, ByVal item As Integer) As Boolean
        Dim i As Integer = ComboBoxItemsCount(hwnd)
        If (i >= item And item > 0) Then
            WinAPI.NativeFunctions.SendMessage(hwnd, CB_SETCURSEL, New IntPtr(item), IntPtr.Zero)
            'WinAPI.NativeFunctions.SendMessage(hwnd, CB_SETCURSEL, item, 0)
            Return True
        Else
            Return False
        End If
    End Function

    Public Function GetComboBoxItems(ByVal lhWnd As IntPtr) As String()
        'Dim lCount As Long, lLen As Long, N As Long
        Dim lCount As Integer, lLen As Integer, N As Integer
        Dim sItems() As String = {""}

        lCount = ComboBoxItemsCount(lhWnd)
        If (lCount = LB_ERR) Or (lCount = 0) Then
            Return sItems
        End If
        ReDim sItems(lCount - 1)
        Dim sBuilder As System.Text.StringBuilder
        For N = 0 To lCount - 1
            sBuilder = New System.Text.StringBuilder()
            'lLen = WinAPI.NativeFunctions.SendMessage(lhWnd, CB_GETLBTEXTLEN, N, 0&)
            lLen = WinAPI.NativeFunctions.SendMessage(lhWnd, CB_GETLBTEXTLEN, New IntPtr(N), IntPtr.Zero)
            sBuilder.Capacity = lLen + 1
            WinAPI.API.SendMessage(lhWnd, CB_GETLBTEXT, N, sBuilder)
            'winAPI.NativeFunctions.SendMessage(lhWnd, LB_GETTEXT, N, sBuilder)
            sItems(N) = sBuilder.ToString()
        Next N
        GetComboBoxItems = sItems
    End Function


    'Public Function ComboBoxItemsCount(ByVal lhWnd As IntPtr) As Long
    Public Function ComboBoxItemsCount(ByVal lhWnd As IntPtr) As Integer
        'Return WinAPI.NativeFunctions.SendMessage(lhWnd, LB_GETCOUNT, 0&, 0&)
        'Return WinAPI.NativeFunctions.SendMessage(lhWnd, LB_GETCOUNT, IntPtr.Zero, IntPtr.Zero)
        Return WinAPI.NativeFunctions.SendMessage(lhWnd, CB_GETCOUNT, IntPtr.Zero, IntPtr.Zero)
    End Function

End Class

