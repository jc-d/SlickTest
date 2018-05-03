Imports System.Windows.Automation

Public Class TextBoxUIAutomation
    Inherits IndependentUIAutomation

    Public Function IsTextBox(ByVal hwnd As IntPtr) As Boolean
        Return WpfGetControlType(hwnd).Id = ControlType.Edit.Id
    End Function

    Public Function IsTextBox(ByVal element As AutomationElement) As Boolean
        Return WpfGetControlType(element).Id = ControlType.Edit.Id
    End Function

End Class
