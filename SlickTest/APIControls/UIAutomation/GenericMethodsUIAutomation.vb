Imports System.Windows.Automation

Public Class GenericMethodsUIAutomation

    Protected Function GetAutomationElement(ByVal hwnd As IntPtr) As System.Windows.Automation.AutomationElement
        Return System.Windows.Automation.AutomationElement.FromHandle(hwnd)
    End Function

    Protected Function GetAutomationElement(ByVal point As System.Drawing.Point) As System.Windows.Automation.AutomationElement
        Return System.Windows.Automation.AutomationElement.FromPoint(New System.Windows.Point(point.X, point.Y))
    End Function

    Protected Function GetPattern(ByVal hwnd As IntPtr, ByVal pattern As AutomationPattern) As System.Windows.Automation.BasePattern
        Return DirectCast(GetAutomationElement(hwnd).GetCurrentPattern(pattern), BasePattern)
    End Function

    Protected Function GetPattern(ByVal element As AutomationElement, ByVal pattern As AutomationPattern) As System.Windows.Automation.BasePattern
        Return DirectCast(element.GetCurrentPattern(pattern), BasePattern)
    End Function

    Protected Function AndProperties(ByVal prop1 As Condition, ByVal prop2 As Condition) As AndCondition
        Dim props() As Condition = {prop1, prop2}
        Return New AndCondition(props)
    End Function

    Protected Function AndProperties(ByVal props() As Condition) As AndCondition
        Return New AndCondition(props)
    End Function

    Protected Function OrProperties(ByVal prop1 As PropertyCondition, ByVal prop2 As PropertyCondition) As OrCondition
        Dim props() As PropertyCondition = {prop1, prop2}
        Return New OrCondition(props)
    End Function

    Protected Function CreateNotCondition(ByVal propertyname As AutomationProperty, ByVal value As Object) As NotCondition
        Return New NotCondition(New PropertyCondition(propertyname, value))
    End Function

    Public Shared Function GetWindow(ByVal hwnd As IntPtr) As IntPtr
        Dim p As IntPtr = WinAPI.API.GetParent(hwnd)
        If (p = IntPtr.Zero) Then Return hwnd
        Return GetWindow(p)
    End Function

    Private Shared tw As TreeWalker

    Public Shared Function GetWindow(ByVal ae As AutomationElement) As AutomationElement
        tw = TreeWalker.ControlViewWalker

        If (ae Is Nothing) Then Return Nothing
        Dim aep As AutomationElement = GetParentElement(ae)
        If (aep Is Nothing) Then Return ae

        If (aep.Current.ControlType.Id = ControlType.Window.Id) Then Return aep
        Return GetWindow(aep)
    End Function

    Public Shared Function GetParentElement(ByVal ae As AutomationElement) As AutomationElement
        tw = TreeWalker.ControlViewWalker

        If (ae Is Nothing) Then Return Nothing
        Return tw.GetParent(ae)
    End Function

    Public Shared Function IsWPF(ByVal hwnd As IntPtr) As Boolean
        Return IndependentWindowsFunctionsv1.GetClassNameGlobal(GetWindow(hwnd)).StartsWith("HwndWrapper[")

        'Return AutomationElement.FromHandle(GetWindow(hwnd)).Cached.FrameworkId = "WPF"
        'Return False
    End Function

    Public Shared Function IsWPF(ByVal element As AutomationElement) As Boolean
        Dim hwnd As IntPtr
        If (WinAPI.API.Is64Bit()) Then
            hwnd = New IntPtr(GetWindow(element).Current.NativeWindowHandle)
        Else
            hwnd = System.Diagnostics.Process.GetProcessById(GetWindow(element).Current.ProcessId).MainWindowHandle
        End If

        Return IndependentWindowsFunctionsv1.GetClassNameGlobal(hwnd).StartsWith("HwndWrapper[")
        'This is probably slower.... TODO test this.
        'Return element.Current.FrameworkId = "WPF"
    End Function

    Public Shared Function IsCustom(ByVal hwnd As IntPtr) As Boolean
        Return False 'For future usage?
        'Return AutomationElement.FromHandle(GetWindow(hwnd)).Cached.FrameworkId = ""
    End Function

    Public Shared Function IsWPFOrCustom(ByVal hwnd As IntPtr) As Boolean
        Dim handle As IntPtr = GetWindow(hwnd)
        If (IsWPF(handle)) Then Return True
        If (IsCustom(handle)) Then Return True
        Return False
    End Function

    Protected Function GetWPFCanidatesFromHwndAndBelow(ByVal hwnd As IntPtr) As AutomationElementCollection
        Dim ae As AutomationElement = GetAutomationElement(hwnd)
        If (ae Is Nothing) Then Return Nothing

        Dim notHeaderItem As NotCondition = CreateNotCondition(AutomationElement.ControlTypeProperty, ControlType.HeaderItem)
        Dim notTabItem As NotCondition = CreateNotCondition(AutomationElement.ControlTypeProperty, ControlType.TabItem)
        Dim notDataItem As NotCondition = CreateNotCondition(AutomationElement.ControlTypeProperty, ControlType.DataItem)
        Dim notListItem As NotCondition = CreateNotCondition(AutomationElement.ControlTypeProperty, ControlType.ListItem)
        Dim notMenuItem As NotCondition = CreateNotCondition(AutomationElement.ControlTypeProperty, ControlType.MenuItem)
        Dim notTreeItem As NotCondition = CreateNotCondition(AutomationElement.ControlTypeProperty, ControlType.TreeItem)
        Dim WPFOnly As New PropertyCondition(AutomationElement.FrameworkIdProperty, "WPF")
        Dim CustomOnly As New PropertyCondition(AutomationElement.FrameworkIdProperty, "")

        Dim SumTotalConditions As AndCondition = AndProperties( _
        New Condition() {notHeaderItem, notTabItem, notDataItem, _
                        notListItem, notMenuItem, notTreeItem, _
                                OrProperties(WPFOnly, CustomOnly)})

        Dim aec As System.Windows.Automation.AutomationElementCollection = ae.FindAll(Windows.Automation.TreeScope.Subtree, SumTotalConditions)

        Return aec
    End Function
End Class
