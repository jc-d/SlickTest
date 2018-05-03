Imports System.Windows.Automation

Public Class TabControlUIAutomation
    Inherits IndependentUIAutomation

    Public Function IsTabControl(ByVal hwnd As IntPtr) As Boolean
        Return WpfGetControlType(hwnd).Id = ControlType.Tab.Id
    End Function

    Public Function IsTabControl(ByVal element As AutomationElement) As Boolean
        Return WpfGetControlType(element).Id = ControlType.Tab.Id
    End Function

End Class
