Imports System.Windows.Automation
Imports System.Windows.Automation.Text

Public Class IndependentUIAutomation
    Inherits GenericMethodsUIAutomation

#Region "Hwnd"

    Public Function WpfIsEnabled(ByVal hwnd As IntPtr) As Boolean
        Return GetAutomationElement(hwnd).Current.IsEnabled
    End Function

    Public Function WpfGetText(ByVal hwnd As IntPtr) As String
        Return WpfGetTextPatternRange(hwnd).GetText(-1)
    End Function

    Public Function WpfGetClassName(ByVal hwnd As IntPtr) As String
        Return GetAutomationElement(hwnd).Current.ClassName
    End Function

    Public Function WpfGetTextPatternRange(ByVal hwnd As IntPtr) As TextPatternRange
        Return DirectCast(GetPattern(hwnd, TextPattern.Pattern), TextPattern).DocumentRange
    End Function

    Public Function WpfGetLocation(ByVal hwnd As IntPtr) As System.Drawing.Rectangle
        Dim rect As Windows.Rect = GetAutomationElement(hwnd).Current.BoundingRectangle
        Dim rectangle As New System.Drawing.Rectangle( _
        Convert.ToInt32(rect.X), Convert.ToInt32(rect.Y), _
        Convert.ToInt32(rect.Width), Convert.ToInt32(rect.Height))
        Return rectangle
    End Function

    Public Function WpfGetControlType(ByVal hwnd As IntPtr) As ControlType
        Return GetAutomationElement(hwnd).Current.ControlType
    End Function

    Public Function WpfGetName(ByVal hwnd As IntPtr) As String
        Return GetAutomationElement(hwnd).Current.Name
    End Function

    Public Sub WpfClick(ByVal hwnd As IntPtr)
        DirectCast(GetPattern(hwnd, InvokePattern.Pattern), InvokePattern).Invoke()
    End Sub
#End Region

#Region "AutomationElement"
    Public Function WpfIsEnabled(ByVal element As AutomationElement) As Boolean
        Return element.Current.IsEnabled
    End Function

    Public Function WpfGetText(ByVal element As AutomationElement) As String
        Return WpfGetTextPatternRange(element).GetText(-1)
    End Function

    Public Function WpfGetClassName(ByVal element As AutomationElement) As String
        Return element.Current.ClassName
    End Function

    Public Function WpfGetTextPatternRange(ByVal element As AutomationElement) As TextPatternRange
        Return DirectCast(GetPattern(element, TextPattern.Pattern), TextPattern).DocumentRange
    End Function

    Public Function WpfGetLocation(ByVal element As AutomationElement) As System.Drawing.Rectangle
        Dim rect As Windows.Rect = element.Current.BoundingRectangle
        Dim rectangle As New System.Drawing.Rectangle( _
        Convert.ToInt32(rect.X), Convert.ToInt32(rect.Y), _
        Convert.ToInt32(rect.Width), Convert.ToInt32(rect.Height))
        Return rectangle
    End Function

    Public Function WpfGetControlType(ByVal element As AutomationElement) As ControlType
        Return element.Current.ControlType
    End Function

    Public Function WpfGetName(ByVal element As AutomationElement) As String
        Return element.Current.Name
    End Function

    Public Sub WpfClick(ByVal element As AutomationElement)
        DirectCast(GetPattern(element, InvokePattern.Pattern), InvokePattern).Invoke()
    End Sub
#End Region

End Class