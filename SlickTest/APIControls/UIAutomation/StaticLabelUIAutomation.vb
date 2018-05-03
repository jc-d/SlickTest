Imports System.Windows.Automation

Public Class StaticLabelUIAutomation
    Inherits IndependentUIAutomation

    Public Function IsStaticLabel(ByVal hwnd As IntPtr) As Boolean
        Return (WpfGetControlType(hwnd).Id = ControlType.Text.Id)
    End Function

    Public Function IsStaticLabel(ByVal element As AutomationElement) As Boolean
        Return (WpfGetControlType(element).Id = ControlType.Text.Id)
    End Function

End Class
