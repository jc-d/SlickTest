Imports System.Windows.Automation

Public Class WindowUIAutomation
    Inherits IndependentUIAutomation

    Public Function IsWindow(ByVal hwnd As IntPtr) As Boolean
        Return WpfGetControlType(hwnd).Id = ControlType.Window.Id
    End Function

    Public Function IsWindow(ByVal element As AutomationElement) As Boolean
        Return WpfGetControlType(element).Id = ControlType.Window.Id
    End Function
End Class
