Imports System.Windows.Automation

Public Class ButtonUIAutomation
    Inherits IndependentUIAutomation

    Public Function IsButton(ByVal hwnd As IntPtr) As Boolean
        Return WpfGetControlType(hwnd).Id = ControlType.Button.Id
    End Function

    Public Function IsRadioButton(ByVal hwnd As IntPtr) As Boolean
        Return WpfGetControlType(hwnd).Id = ControlType.RadioButton.Id
    End Function

    Public Function IsCheckbox(ByVal hwnd As IntPtr) As Boolean
        Return WpfGetControlType(hwnd).Id = ControlType.CheckBox.Id
    End Function

    Public Function IsButton(ByVal element As AutomationElement) As Boolean
        Return WpfGetControlType(element).Id = ControlType.Button.Id
    End Function

    Public Function IsRadioButton(ByVal element As AutomationElement) As Boolean
        Return WpfGetControlType(element).Id = ControlType.RadioButton.Id
    End Function

    Public Function IsCheckbox(ByVal element As AutomationElement) As Boolean
        Return WpfGetControlType(element).Id = ControlType.CheckBox.Id
    End Function

    Public Function GetRadioButtonState(ByVal element As AutomationElement) As Integer
        Dim SelectionItemPattern As Windows.Automation.SelectionItemPattern = _
        DirectCast(element.GetCurrentPattern( _
        Windows.Automation.SelectionItemPattern.Pattern),  _
        Windows.Automation.SelectionItemPattern)

        If (SelectionItemPattern.Current.IsSelected) Then Return WinAPI.API.BST_CHECKED
        Return WinAPI.API.BST_UNCHECKED
    End Function

    Public Function SetCheckBoxState(ByVal element As AutomationElement, ByVal state As Integer) As Integer
        Dim stateChangeCount As Integer = 0

        Dim TogglePattern As Windows.Automation.TogglePattern = _
              DirectCast(element.GetCurrentPattern(Windows.Automation.TogglePattern.Pattern),  _
              Windows.Automation.TogglePattern)

        While (state <> TogglePattern.Current.ToggleState)
            TogglePattern.Toggle()
            stateChangeCount += 1
            If (stateChangeCount = 6) Then
                Exit While
            End If
        End While
        If (stateChangeCount <> 6) Then Return GetCheckBoxState(element)
        Throw New SlickTestAPIException("Not a checkbox.")
    End Function

    Public Function GetCheckBoxState(ByVal element As AutomationElement) As Integer
        Dim TogglePattern As Windows.Automation.TogglePattern = _
        DirectCast(element.GetCurrentPattern(Windows.Automation.TogglePattern.Pattern),  _
        Windows.Automation.TogglePattern)

        Dim state As Windows.Automation.ToggleState = TogglePattern.Current.ToggleState
        Return state
    End Function

End Class
