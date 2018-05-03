Imports System.Windows.Automation

Public Class ListBoxUIAutomation
    Inherits IndependentUIAutomation

    Public Function IsListBox(ByVal hwnd As IntPtr) As Boolean
        Return WpfGetControlType(hwnd).Id = ControlType.List.Id
    End Function

    Public Function IsListBox(ByVal element As AutomationElement) As Boolean
        Return WpfGetControlType(element).Id = ControlType.List.Id
    End Function
End Class
