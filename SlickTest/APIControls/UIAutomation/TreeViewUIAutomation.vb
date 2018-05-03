Imports System.Windows.Automation

Public Class TreeViewUIAutomation
    Inherits IndependentUIAutomation

    Public Function IsTreeView(ByVal hwnd As IntPtr) As Boolean
        Return WpfGetControlType(hwnd).Id = ControlType.Tree.Id
    End Function

    Public Function IsTreeView(ByVal element As AutomationElement) As Boolean
        Return WpfGetControlType(element).Id = ControlType.Tree.Id
    End Function
End Class
