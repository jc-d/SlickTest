Imports winAPI.API
Imports winAPI.NativeFunctions
Imports System.Runtime.InteropServices
Imports System.Text
Imports System.ComponentModel
Imports System
Imports System.Collections
Imports System.Runtime.CompilerServices
<Assembly: InternalsVisibleTo("InterAction")> 
<CLSCompliantAttribute(True)> _
Friend Class ButtonWindowsAPI
    Private Shared WindowsFunctions As APIControls.IndependentWindowsFunctionsv1

    Friend Sub New(ByVal wf As APIControls.IndependentWindowsFunctionsv1)
        WindowsFunctions = wf
    End Sub

    Function IsButton(ByVal hwnd As IntPtr) As Boolean
        Dim cn As String = WindowsFunctions.GetClassNameNoDotNet(hwnd)
        If (cn.ToLowerInvariant().Contains("button") = True) Then
            Dim IsCheck As Boolean = IsCheckBox(hwnd)
            Dim IsRadio As Boolean = IsRadioButton(hwnd)
            If (IsCheck = True Xor IsRadio = True) Then 'if and only if it is only a check box or only a radio box.  Otherwise, assume button.
                Return False
            End If
            Return True
        End If
        Return False
    End Function

    Function IsRadioButton(ByVal hwnd As IntPtr) As Boolean

        If (ContainsValue(WindowsFunctions.GetWindowsStyle(hwnd), BS.RADIOBUTTON) = True OrElse _
        ContainsValue(WindowsFunctions.GetWindowsStyle(hwnd), BS.AUTORADIOBUTTON) = True) Then
            If (WindowsFunctions.IsDotNet(hwnd) = False) Then
                If (ContainsValue(WindowsFunctions.GetWindowsStyle(hwnd), BS.AUTOCHECKBOX) = True OrElse _
                    ContainsValue(WindowsFunctions.GetWindowsStyle(hwnd), BS.CHECKBOX) = True) Then
                    Return False
                End If
            Else 'is dot net
                If (WindowsFunctions.GetDotNetClassName(hwnd).ToUpperInvariant().Contains("CHECKBOX")) Then
                    Return False
                End If
            End If
            Dim cn As String = WindowsFunctions.GetClassNameNoDotNet(hwnd)
            If (cn.ToLowerInvariant().Contains("button") = True) Then
                Return True
            End If
        End If
        Return False
    End Function

    Function IsCheckBox(ByVal hwnd As IntPtr) As Boolean
        If (ContainsValue(WindowsFunctions.GetWindowsStyle(hwnd), BS.CHECKBOX) = True OrElse _
        ContainsValue(WindowsFunctions.GetWindowsStyle(hwnd), BS.AUTOCHECKBOX) = True) Then
            If (WindowsFunctions.IsDotNet(hwnd) = False) Then
                If (ContainsValue(WindowsFunctions.GetWindowsStyle(hwnd), BS.RADIOBUTTON) = True OrElse _
                   ContainsValue(WindowsFunctions.GetWindowsStyle(hwnd), BS.AUTORADIOBUTTON) = True) Then
                    Return False
                End If
            Else 'is dot net
                If (WindowsFunctions.GetDotNetClassName(hwnd).ToUpperInvariant().Contains("RADIO")) Then
                    Return False
                End If
            End If
            Dim cn As String = WindowsFunctions.GetClassNameNoDotNet(hwnd)
            If (cn.ToLowerInvariant().Contains("button") = True) Then
                Return True
            End If
            End If
        Return False
    End Function

    ''' <summary>
    ''' Tests the windows object style value to see if it includes the XX_Value.
    ''' </summary>
    ''' <param name="StyleValue">The style value returned from a windows object.</param>
    ''' <param name="XX_Value">The BS value you that is checked to see if it is contained in the windows object.</param>
    ''' <returns></returns>
    ''' <remarks>Example usage: StyleInfo.ContainsValue(Window(MyWindow).GetStyle.ButtonStyle.GetStyle(),StyleInfo.ButtonStyle.WS_VISIBLE)
    ''' <p />This will return true if the window contains the value "WS_VISIBLE" (the internal windows
    ''' value that control's the object's visibility).</remarks>
    Private Function ContainsValue(ByVal StyleValue As Long, ByVal XX_Value As Long) As Boolean
        Return ((StyleValue And XX_Value) = XX_Value)
    End Function

End Class
