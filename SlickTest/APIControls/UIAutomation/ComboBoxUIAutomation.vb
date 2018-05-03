Imports System.Windows.Automation

Public Class ComboBoxUIAutomation
    Inherits IndependentUIAutomation

    Public Function IsComboBox(ByVal hwnd As IntPtr) As Boolean
        Return WpfGetControlType(hwnd).Id = ControlType.ComboBox.Id
    End Function

    Public Function IsComboBox(ByVal element As AutomationElement) As Boolean
        Return WpfGetControlType(element).Id = ControlType.ComboBox.Id
    End Function
End Class
