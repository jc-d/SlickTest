Imports System.Windows.Automation

Public Class ListViewUIAutomation
    Inherits IndependentUIAutomation

    Public Function IsListView(ByVal hwnd As IntPtr) As Boolean
        Return WpfGetControlType(hwnd).Id = ControlType.DataGrid.Id
    End Function

    Public Function IsListView(ByVal element As AutomationElement) As Boolean
        Return WpfGetControlType(element).Id = ControlType.DataGrid.Id
    End Function
End Class
