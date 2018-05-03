Imports winAPI.API
Imports winAPI.NativeFunctions
Imports System.Runtime.InteropServices
Imports System.Text
Imports System.ComponentModel
Imports System
Imports System.Collections

Friend Class StaticLabelWindowsAPI
    Private Shared WindowsFunctions As APIControls.IndependentWindowsFunctionsv1
    Public Sub New(ByVal wf As APIControls.IndependentWindowsFunctionsv1)
        WindowsFunctions = wf
    End Sub

    Function IsStaticLabel(ByVal hwnd As IntPtr) As Boolean
        If (GenericMethodsUIAutomation.IsWPFOrCustom(hwnd) = True) Then
            Return WindowsFunctions.WpfStaticLabel.IsStaticLabel(hwnd)
        End If

        Dim cn As String = WindowsFunctions.GetClassNameNoDotNet(hwnd)
        If (cn.ToLowerInvariant.Contains("static") = True) Then
            Return True
        End If
        Return False
    End Function

End Class