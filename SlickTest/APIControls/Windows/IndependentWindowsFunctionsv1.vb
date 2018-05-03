Imports winAPI.API
Imports winAPI.NativeFunctions
Imports System.Runtime.InteropServices
Imports System.Text
Imports System.ComponentModel
Imports System
Imports System.Diagnostics
Imports System.Collections
Imports System.Runtime.CompilerServices
Imports System.Windows.Automation

<Assembly: InternalsVisibleTo("InterAction"), _
Assembly: InternalsVisibleTo("SlickTestDeveloper"), _
Assembly: InternalsVisibleTo("HandleInput")> 
<Assembly: CLSCompliantAttribute(True)> 
Friend Class IndependentWindowsFunctionsv1
    Inherits IndependentUIAutomation

    Friend Sub New()
    End Sub

    'Public Declare Function GetWindowRect Lib "user32.dll" (ByVal hWnd As Integer, ByRef lpRect As RECT) As Int32
    <DllImport("user32.dll")> _
    Public Shared Function GetWindowRect(ByVal hWnd As IntPtr, ByRef lpRect As RECT) As Boolean
    End Function

    <DllImport("user32.dll")> _
    Private Shared Function GetClientRect(ByVal hWnd As System.IntPtr, ByRef lpRECT As RECT) As Boolean
    End Function


    'Private Declare Function SendMessage Lib "user32" Alias "SendMessageA" (ByVal hWnd As IntPtr, ByVal msg As Integer, ByVal wp As Integer, ByVal lp As Integer) As Integer
    Public Structure RECT
        Public Left As Int32
        Public Top As Int32
        Public Right As Int32
        Public Bottom As Int32
    End Structure

    Public TextBox As New TextBoxWindowsAPI(Me)
    Public ListBox As New ListBoxWindowsAPI(Me)
    Public ListView As New ListViewWindowsAPI(Me)
    Public TabControl As New TabControlWindowsAPI(Me)
    Public ComboBox As New ComboBoxWindowsAPI(Me)
    Public Button As New ButtonWindowsAPI(Me)
    Public StaticLabel As New StaticLabelWindowsAPI(Me)
    Public Window As New WindowWindowsAPI(Me)
    Public TreeView As New TreeViewWindowsAPI(Me)

    Public WpfTextBox As New TextBoxUIAutomation()
    Public WpfListBox As New ListBoxUIAutomation()
    Public WpfListView As New ListViewUIAutomation()
    Public WpfTabControl As New TabControlUIAutomation()
    Public WpfComboBox As New ComboBoxUIAutomation()
    Public WpfButton As New ButtonUIAutomation()
    Public WpfStaticLabel As New StaticLabelUIAutomation()
    Public WpfWindow As New WindowUIAutomation()
    Public WpfTreeView As New TreeViewUIAutomation()

#Const isAbs = 2 'Set to 1 if you want to use absolute values
    Public HandlesList() As IntPtr
    Private MessageID As Integer = 0
    Private possibleHandles As System.Collections.Generic.List(Of IntPtr)
    Public HandleCount As Integer = 0

    Public Sub CloseWindow(ByVal hWnd As IntPtr)
        WinAPI.NativeFunctions.PostMessage(hWnd, WinAPI.API.WM.CLOSE, IntPtr.Zero, IntPtr.Zero)
    End Sub

    Public Sub CloseWindow(ByVal element As AutomationElement)
        System.Diagnostics.Process.GetProcessById(element.Current.ProcessId).CloseMainWindow() 'Close?  Kill?
    End Sub

    Public Function GetRadioButtonState(ByVal Hwnd As IntPtr) As Integer
        If (IsDotNet(Hwnd) OrElse IsCustom(Hwnd)) Then
            Return Button.GetRadioButtonState(Hwnd)
        Else
            Return WinAPI.NativeFunctions.SendMessage(Hwnd, WinAPI.API.BM.BM_GETCHECK, IntPtr.Zero, IntPtr.Zero)
        End If
    End Function

    Public Function GetRadioButtonState(ByVal element As AutomationElement) As Integer
        Return WpfButton.GetRadioButtonState(element)
    End Function

    Public Function GetCheckBoxState(ByVal Hwnd As IntPtr) As Integer
        Dim Custom As Boolean = IsCustom(Hwnd)
        If (IsDotNet(Hwnd) OrElse Custom) Then
            If (Button.IsCheckBox(Hwnd) OrElse _
                Custom AndAlso WpfButton.IsCheckbox(Hwnd)) Then
                Return Button.GetCheckBoxState(Hwnd)
            End If
        Else
            Return WinAPI.NativeFunctions.SendMessage(Hwnd, WinAPI.API.BM.BM_GETCHECK, IntPtr.Zero, IntPtr.Zero)
        End If
    End Function

    Public Function SetCheckBoxState(ByVal Hwnd As IntPtr, ByVal state As Integer) As Integer
        If (IsDotNet(Hwnd) OrElse IsWPFOrCustom(Hwnd)) Then
            Return Button.SetCheckBoxState(Hwnd, state)
        Else
            Return WinAPI.NativeFunctions.SendMessage(Hwnd, WinAPI.API.BM.BM_SETCHECK, New IntPtr(state), IntPtr.Zero)
        End If
    End Function

    Public Function IsEnabled(ByVal Hwnd As IntPtr) As Boolean
        Return WinAPI.API.IsWindowEnabled(Hwnd)
    End Function

    Public Function GetControlID(ByVal hwnd As IntPtr) As Integer
        Try
            Dim id As Integer = WinAPI.API.GetDlgCtrlID(hwnd)
            If (id < 0) Then Return 0
            Return id
        Catch ex As Exception
            Return 0
        End Try
    End Function

    Public Function GetWindowsStyle(ByVal hwnd As IntPtr) As Int64
        Return GetWindowLongAsInt64(hwnd, WinAPI.API.GWL.STYLE)
    End Function

    Public Function GetEXWindowsStyle(ByVal hwnd As IntPtr) As Int64
        Return GetWindowLongAsInt64(hwnd, WinAPI.API.GWL.EXSTYLE)
    End Function

    Public Function GetWindowsID(ByVal hwnd As IntPtr) As Int64
        Return GetWindowLongAsInt64(hwnd, WinAPI.API.GWL.ID)
    End Function

    Public Function GetProcessName(ByVal element As AutomationElement) As String
        Dim PID As Integer = element.Current.ProcessId
        For Each p As System.Diagnostics.Process In System.Diagnostics.Process.GetProcesses()
            If (p.Id = PID) Then
                Return p.ProcessName
            End If
        Next
        Return ""
    End Function

    Public Function GetProcessName(ByVal handle As System.IntPtr) As String
        Dim PID As Integer = GetProcessID(handle)
        For Each p As System.Diagnostics.Process In System.Diagnostics.Process.GetProcesses()
            If (p.Id = PID) Then
                Return p.ProcessName
            End If
        Next
        Return ""
    End Function

    Private TmpUIAutoElement As System.Windows.Automation.AutomationElement

    Public Function GetNearByLabel(ByVal handle As System.IntPtr) As String
        Try
            TmpUIAutoElement = System.Windows.Automation.AutomationElement.FromHandle(handle).Current.LabeledBy
            If (TmpUIAutoElement Is Nothing) Then Return String.Empty
            Return TmpUIAutoElement.Current.Name
        Catch ex As Exception
            Return String.Empty
        End Try
    End Function

    Public Function GetNearByLabel(ByVal element As AutomationElement) As String
        Try
            TmpUIAutoElement = element.Current.LabeledBy
            If (TmpUIAutoElement Is Nothing) Then Return String.Empty
            Return TmpUIAutoElement.Current.Name
        Catch ex As Exception
            Return String.Empty
        End Try
    End Function

    ''' <summary>
    ''' Creates description from a hwnd value.
    ''' </summary>
    ''' <param name="hwnd">hwnd value to create the description</param>
    ''' <returns>A description object with all values prefilled.</returns>
    ''' <remarks>This is helpful for dynamically creating descriptions.
    ''' This will not work for web sites as they do not use hwnds.</remarks>
    Public Function CreateDescriptionFromHwnd(ByVal hwnd As IntPtr, Optional ByVal OverrideWebHtml As Boolean = False) As APIControls.Description
        Dim desc As New APIControls.Description()

        If (OverrideWebHtml = True OrElse IsWebPartIEHTML(hwnd) = False) Then
            Try
                desc.Add("hwnd", hwnd.ToString())
                desc.Add("value", Me.GetAllWindowText(hwnd))
                desc.Add("value", Me.GetAllWindowText(hwnd))
                desc.Add("name", Me.GetClassName(hwnd))
                desc.Add("nearbylabel", Me.GetNearByLabel(hwnd))


                desc.Add("processname", Me.GetProcessName(hwnd))
                Dim parent As IntPtr = Me.GetTopParent(hwnd)
                If (parent <> hwnd) Then desc.Add("index", Me.FindIndex(parent, hwnd).ToString())
                Dim rect As System.Drawing.Rectangle = Me.GetLocation(hwnd)
#If isAbs = 1 Then
                desc.Add("top", Mouse.RelativeToAbsCoordY(rect.Top))
                desc.Add("left", Mouse.RelativeToAbsCoordX(rect.Left))
                desc.Add("right", Mouse.RelativeToAbsCoordX(rect.Right))
                desc.Add("bottom", Mouse.RelativeToAbsCoordY(rect.Bottom))
                desc.Add("height", Mouse.RelativeToAbsCoordY(rect.Height))
                desc.Add("width", Mouse.RelativeToAbsCoordX(rect.Width))
#Else
                desc.Add("top", rect.Top.ToString())
                desc.Add("left", rect.Left.ToString())
                desc.Add("right", rect.Right.ToString())
                desc.Add("bottom", rect.Bottom.ToString())
                desc.Add("height", rect.Height.ToString())
                desc.Add("width", rect.Width.ToString())
#End If
                desc.Add("ControlType", Me.GetObjectTypeAsString(hwnd).ToLower())
                desc.Add("controlid", Me.GetControlID(hwnd).ToString())
            Catch ex As Exception
                Return desc
            End Try

        End If
        Return desc
    End Function

    Public Function GetProcessID(ByVal handle As System.IntPtr) As Integer
        Dim PID As Int32
        'Dim NotSureWhatItIs As Integer= WinAPI.API.GetWindowThreadProcessId(handle.ToInt32(), PID)
        WinAPI.API.GetWindowThreadProcessIdWrapper(handle, PID)
        Return PID
    End Function

    Public Function GetProcessID(ByVal element As AutomationElement) As Integer
        Return element.Current.ProcessId
    End Function

    Public Function GetClassNameNoDotNet(ByVal element As AutomationElement) As String
        Return element.Current.ClassName
    End Function

    Public Function GetClassNameNoDotNet(ByVal hWnd As IntPtr) As String
        Return GetClassNameGlobal(hWnd)
    End Function

    Protected Friend Shared Function GetClassNameGlobal(ByVal hWnd As IntPtr) As String
        Dim pClsName As System.Text.StringBuilder = New System.Text.StringBuilder(256)
        Try
            WinAPI.API.GetClassName(hWnd, pClsName, pClsName.Capacity)
            Return pClsName.ToString()
        Catch ex As Exception
            Return ""
        End Try
    End Function

    Public Sub SetText(ByVal text As String, ByVal hwnd As IntPtr)
        If (IsDotNet(hwnd) = True) Then
            WinAPI.API.SendMessageStr(hwnd, WM_SETTEXT, 0&, "")
            For Each c As Char In text 'this is a lame fix, but I'll call it good.
                AppendText(c.ToString(), hwnd)
            Next
        Else
            WinAPI.API.SendMessageStr(hwnd, WM_SETTEXT, 0&, text)
        End If
    End Sub

    Private Function IsAParent(ByVal child As IntPtr, ByVal parent As IntPtr) As Boolean
        Dim tmp As IntPtr = GetParent(child)
        While (Not tmp.Equals(IntPtr.Zero))
            If (parent.Equals(tmp)) Then
                Return True
            End If
            tmp = GetParent(tmp)
        End While
        Return False
    End Function

    ''' <summary>
    ''' Appends the current text with additional text given.
    ''' </summary>
    ''' <param name="text">The additional text to append to the end of the current text.</param>
    ''' <remarks>Currently a test function.  This may not remain in the final version</remarks>
    Public Sub AppendText(ByVal text As String, ByVal hwnd As IntPtr)
        Dim SelectionStart As Integer, SelectionEnd As Integer
        WinAPI.API.SendMessageTimeoutInt(hwnd, EM_GETSEL, SelectionStart, SelectionEnd)
        'WinAPI.API.SendMessageByRef(hwnd, EM_GETSEL, SelectionStart, SelectionEnd)
        'WinAPI.API.SendMessageStr(hwnd, EM_REPLACESEL, False, text)
        WinAPI.API.SendMessageStr(hwnd, EM_REPLACESEL, 0, text)
    End Sub
    'https://svn.enthought.com/enthought/browser/trunk/src/lib/enthought/guitest/win32/winGuiAuto.py?rev=6543
    '    # Set the current selection range, depending on append flag 
    'if append: 
    '        win32gui.SendMessage(hwnd, win32con.EM_SETSEL, -1, 0) 
    '     else: 
    '         win32gui.SendMessage(hwnd, win32con.EM_SETSEL, 0, -1) 
    '# Send the text 
    '     win32gui.SendMessage(hwnd, win32con.EM_REPLACESEL, True, os.linesep.join(text))

    Public Function GetParent(ByVal hwnd As IntPtr) As IntPtr
        Return WinAPI.API.GetParent(hwnd)
    End Function

    Public Function GetParent(ByVal element As AutomationElement) As AutomationElement
        Return GenericMethodsUIAutomation.GetParentElement(element)
    End Function

    ''' <summary>
    ''' This attempts to get all the windows text, rather than the first 256 characters.
    ''' </summary>
    ''' <param name="WindowHandle"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetAllWindowText(ByVal WindowHandle As IntPtr) As String
        'Dim ptrRet As IntPtr
        Dim ptrLength As IntPtr
        'get length for buffer... 
        ptrLength = New IntPtr(WinAPI.NativeFunctions.SendMessage(WindowHandle, WM_GETTEXTLENGTH, IntPtr.Zero, IntPtr.Zero))
        'create buffer for return value... 
        Dim sbText As New System.Text.StringBuilder(ptrLength.ToInt32 + 1)
        'get window text... 
        'ptrRet = WinAPI.API.SendMessageTimeout(WindowHandle, WM_GETTEXT, ptrLength.ToInt32 + 1, sbText)
        WinAPI.API.SendMessageTimeout(WindowHandle, WM_GETTEXT, ptrLength.ToInt32 + 1, sbText)
        'get return value... 
        If (sbText.Length = 0) Then
            Try
                Return System.Windows.Automation.AutomationElement.FromHandle(WindowHandle).Current.Name
            Catch ex As Exception
                'we don't care.
            End Try
        End If

        Return sbText.ToString
    End Function

    Public Function GetAllWindowText(ByVal element As AutomationElement) As String
        Return WpfGetText(element)
    End Function


    Public Function GetAllWindowTextSmart(ByVal WindowHandle As IntPtr) As String
        Return GetAllText(WindowHandle)
    End Function

    Public Function GetAllWindowTextSmart(ByVal element As AutomationElement) As String
        Return WpfGetText(element)
    End Function

    Public Function GetAllText(ByVal element As AutomationElement) As String
        Return WpfGetText(element)
    End Function

    Public Function GetAllText(ByVal WindowHandle As IntPtr) As String
        'Dim ptrRet As IntPtr
        Dim ptrLength As IntPtr
        'get length for buffer... 
        'ptrLength = New IntPtr(WinAPI.NativeFunctions.SendMessage(WindowHandle, WM_GETTEXTLENGTH, IntPtr.Zero, IntPtr.Zero))
        Try
            Dim len As Integer = WinAPI.API.SendMessageTimeoutInt(WindowHandle, WM_GETTEXTLENGTH, 0, 0)
            ptrLength = New IntPtr(len)
            Dim sbText As New System.Text.StringBuilder(ptrLength.ToInt32 + 1)
            WinAPI.API.SendMessageTimeout(WindowHandle, WM_GETTEXT, ptrLength.ToInt32 + 1, sbText)
            If (sbText.Length <> 0) Then Return sbText.ToString()
        Catch ex As Exception
        End Try
        Dim retText As String
        Try
            retText = DirectCast(AutomationElement.FromHandle(WindowHandle).GetCurrentPattern(TextPattern.Pattern), TextPattern).DocumentRange.GetText(-1)
            Return retText
        Catch ex As Exception
            'we don't care.
            Return String.Empty
        End Try

        'get return value... 
    End Function

    Public Function GetText(ByVal hwnd As IntPtr) As String
        'Dim pClsName As System.Text.StringBuilder = New System.Text.StringBuilder(256)
        'GetWindowText(hwnd, pClsName, pClsName.Capacity)
        'GetText = pClsName.ToString()
        Return GetAllText(hwnd)
    End Function

    Public Function GetText(ByVal element As AutomationElement) As String
        Return WpfGetText(element)
    End Function

    Public Function GetHwndByXY(ByVal x As Integer, ByVal y As Integer) As IntPtr
        Return WindowFromPoint(x, y)
    End Function

    Public Function GetAutomationElementByXY(ByVal x As Integer, ByVal y As Integer) As AutomationElement
        Return GetAutomationElement(New System.Drawing.Point(x, y))
    End Function

    Public Function GetLocation(ByVal hwnd As IntPtr) As System.Drawing.Rectangle
        If (GenericMethodsUIAutomation.IsCustom(hwnd)) Then
            Return WpfGetLocation(hwnd)
        End If
        Dim rec As RECT
        If hwnd <> IntPtr.Zero And WinAPI.API.IsWindow(hwnd) Then
            If GetWindowRect(hwnd, rec) <> False Then
                Return New System.Drawing.Rectangle(rec.Left, rec.Top, rec.Right - rec.Left, rec.Bottom - rec.Top)
            End If
        End If
        Return New System.Drawing.Rectangle(0, 0, 0, 0)
    End Function

    Public Function GetLocation(ByVal element As AutomationElement) As System.Drawing.Rectangle
        Return WpfGetLocation(element)
    End Function

    Public Function GetClientLocation(ByVal hwnd As IntPtr) As System.Drawing.Rectangle
        Dim rec As RECT
        If hwnd <> IntPtr.Zero And WinAPI.API.IsWindow(hwnd) Then
            If GetClientRect(hwnd, rec) <> False Then
                Return New System.Drawing.Rectangle(rec.Left, rec.Top, rec.Right - rec.Left, rec.Bottom - rec.Top)
            End If
        End If
        Return New System.Drawing.Rectangle(0, 0, 0, 0)
    End Function
    '////////////////////////////////////////////////////////


    Private Function BuildList(ByVal TopWindowhwnd As IntPtr, ByVal hwnd As IntPtr) As IntPtr()
        If (hwnd = IntPtr.Zero) Then
            '    'elminates all possible non-windows right off the bat.
            '    'turns out sometimes windows appear invisible even when they shouldn't.
            '    'Win32Window.IsWindowVisible(handle) And
            For Each handle As IntPtr In EnumerateWindows.GetHandles()
                possibleHandles.Add(handle)
            Next
            HandlesList = possibleHandles.ToArray()
            possibleHandles.Clear()
        Else
            For Each handle As IntPtr In EnumerateWindows.GetChildHandles(TopWindowhwnd)
                possibleHandles.Add(handle)
            Next
            HandlesList = possibleHandles.ToArray()
            possibleHandles.Clear()
        End If
        'If (hwnd <> 0 And isAParent(hwnd, TopWindowhwnd) = True) Then
        'handlesList = Win32Window.GetChildHandles(TopWindowhwnd)
        'For Each handle As IntPtr In Win32Window.GetHandles()
        ' If getParent(handle) = 0 Then
        'possibleHandles.Add(handle)
        ' End If
        'Next
        'handlesList = possibleHandles.ToArray()
        'possibleHandles.Clear()
        'Else
        ' End If
        'Return handlesList
        Return HandlesList
    End Function


    ''' <summary>
    ''' Sends text/keys to a handle.
    ''' </summary>
    ''' <param name="text">The text you wish to send</param>
    ''' <param name="hwnd">The hwnd you wish to send to</param>
    ''' <remarks></remarks>
    Public Sub SendTextByHandler(ByVal text As String, ByVal hwnd As IntPtr)
        Me.AppActivateByHwnd(hwnd)
        WinAPI.API.SendMessage(hwnd, WinAPI.API.WM.ACTIVATE, 1, IntPtr.Zero)
        WinAPI.API.SendMessage(hwnd, WinAPI.API.WM.SETFOCUS, 1, IntPtr.Zero)
        Dim InternalPID As Int32 = System.Diagnostics.Process.GetCurrentProcess().Id()
        Dim SendTextPID As Int32
        WinAPI.API.GetWindowThreadProcessIdWrapper(hwnd, SendTextPID)
        Try
            WinAPI.API.AttachThreadInput(InternalPID, SendTextPID, 1)
        Catch ex As Exception
            'Ignore this.
        End Try
        WinAPI.API.SetFocus(hwnd)
        InternalKeyboard.SendKeys(text)
        Try
            WinAPI.API.AttachThreadInput(InternalPID, SendTextPID, 0)
        Catch ex As Exception
            'Ignore this.
        End Try
    End Sub

    Sub AppActivateByHwnd(ByVal Hwnd As IntPtr)
        Dim handle As IntPtr = Me.GetTopParent(Hwnd)
        If (TopWindow.GetCurrentlyActiveWindowHandle() = handle) Then
            Return
        End If
        Dim procId As Integer = Me.GetProcessID(handle)
        If (procId <= 0) Then
            Dim title As String = FindParentTitle(Hwnd)
            If (String.IsNullOrEmpty(title)) Then
                Throw New SlickTestAPIException("Unable to get a title or process id.  Possibly the handle is invalid.")
            Else
                Microsoft.VisualBasic.AppActivate(title)
            End If
        End If
        Microsoft.VisualBasic.AppActivate(procId)
    End Sub

    Sub AppActivateByHwnd(ByVal element As AutomationElement)
        Dim pid As Integer = element.Current.ProcessId
        If (pid <= 0) Then
            Throw New SlickTestAPIException("Unable to get a process id.")
        End If

        If (GetProcessID(TopWindow.GetCurrentlyActiveWindowHandle()) = pid) Then
            Return
        End If

        Microsoft.VisualBasic.AppActivate(pid)
    End Sub

    Private Function FindParentTitle(ByVal hChild As IntPtr) As String
        Return Me.GetAllWindowTextSmart(GetTopParent(hChild))
    End Function

    Public Function GetTopParent(ByVal hChild As IntPtr) As IntPtr
        If (GetParent(hChild).Equals(IntPtr.Zero)) Then
            Return hChild
        End If
        Return GetTopParent(GetParent(hChild))
    End Function

    ' <summary>
    ' Builds a description from any point in the list of hwnds.
    ' </summary>
    ' <param name="TopWindowhwnd">The actual window or parent object</param>
    ' <param name="hwnd">The object you wish to create the description for.  
    ' Pass the parent object in agian, if you want that for the description
    ' created.</param>
    ' <returns>Returns the description created.</returns>
    ' <remarks></remarks>
    Public Function CreateDescription(ByVal TopWindowhwnd As IntPtr, ByVal hwnd As IntPtr) As Description
        possibleHandles = New System.Collections.Generic.List(Of IntPtr)
        'Console.WriteLine("Process: TopWindow Hwnd = " & TopWindowhwnd.ToString() & _
        '            "hwnd = " & hwnd.ToString())
        HandlesList = BuildList(TopWindowhwnd, hwnd)

        If (hwnd = IntPtr.Zero) Then
            hwnd = TopWindowhwnd
            'Else
            '    Console.WriteLine("hwnd= " & _
            '    hwnd.ToString() & " TopWindowhwnd= " & TopWindowhwnd.ToString() & _
            '    " getParent(hwnd) = " & GetParent(hwnd).ToString())
        End If

        'System.Console.WriteLine("handlesList size = " & HandlesList.Length.ToString)


        Dim desc As Description = New Description()
        Dim retDesc As Description = New Description()
        Dim type As String = ""
        Dim HwndCountPreCheck As Integer = HandlesList.Length

        '//////////////////////ClassName//////////////////////
        If (GetClassName(hwnd) <> "") Then
            type = "name"
            desc.Add(type, GetClassName(hwnd))
            Dim str As String

            For Each handle As IntPtr In HandlesList
                str = GetClassName(handle)
                If (str.Equals(desc.Name)) Then
                    possibleHandles.Add(handle)
                End If
            Next

            If (possibleHandles.Count >= 1) Then
                retDesc.Add(type, GetClassName(hwnd))
                If (possibleHandles.Count = 1) Then
                    Return retDesc
                End If
                If (HwndCountPreCheck = possibleHandles.Count) Then
                    retDesc.Remove(type)
                    desc = retDesc 'set back to old good state.
                Else
                    HandlesList = possibleHandles.ToArray()
                End If
                possibleHandles.Clear()
            Else
                desc = retDesc 'set back to old good state.
            End If
            HwndCountPreCheck = HandlesList.Length
        End If

        '//////////////////////Value//////////////////////
        type = "value"
        desc.Add(type, Me.GetAllWindowTextSmart(hwnd))
        For Each handle As IntPtr In HandlesList
            If (Me.GetAllWindowTextSmart(handle).Equals(desc.Value)) Then
                possibleHandles.Add(handle)
            End If
        Next

        If (possibleHandles.Count >= 1) Then
            retDesc.Add(type, GetAllWindowTextSmart(hwnd))
            If (possibleHandles.Count = 1) Then
                Return retDesc
            End If
            If (HwndCountPreCheck = possibleHandles.Count) Then
                retDesc.Remove(type)
                desc = retDesc 'set back to old good state.
            Else
                HandlesList = possibleHandles.ToArray()
            End If
            possibleHandles.Clear()
        Else
            desc = retDesc 'set back to old good state.
        End If
        HwndCountPreCheck = HandlesList.Length

        '//////////////////////ClassName2 (empty class name; This should almost never occur)//////////////////////
        If (GetClassName(hwnd) = "") Then
            type = "name"
            desc.Add(type, GetClassName(hwnd))

            Dim str As String

            For Each handle As IntPtr In HandlesList
                str = GetClassName(handle)
                If (str.Equals(desc.Name)) Then
                    possibleHandles.Add(handle)
                End If
            Next


            If (possibleHandles.Count >= 1) Then
                retDesc.Add(type, GetClassName(hwnd))
                If (possibleHandles.Count = 1) Then
                    Return retDesc
                End If
                If (HwndCountPreCheck = possibleHandles.Count) Then
                    retDesc.Remove(type)
                    desc = retDesc 'set back to old good state.
                Else
                    HandlesList = possibleHandles.ToArray()
                End If
                possibleHandles.Clear()
            Else
                desc = retDesc 'set back to old good state.
            End If
            HwndCountPreCheck = HandlesList.Length
        End If

        '//////////////////////Index//////////////////////
        If (hwnd <> TopWindowhwnd) Then
            type = "index"
            desc.Add(type, Me.FindIndex(GetTopParent(TopWindowhwnd), hwnd).ToString())
            For Each handle As IntPtr In HandlesList
                If (Me.FindIndex(Me.GetTopParent(handle), handle) = desc.Index) Then
                    possibleHandles.Add(handle)
                End If
            Next

            If (possibleHandles.Count >= 1) Then
                retDesc.Add(type, Me.FindIndex(TopWindowhwnd, hwnd).ToString())
                If (possibleHandles.Count = 1) Then
                    Return retDesc
                End If
                If (HwndCountPreCheck = possibleHandles.Count) Then
                    retDesc.Remove(type)
                    desc = retDesc 'set back to old good state.
                Else
                    HandlesList = possibleHandles.ToArray()
                End If
                possibleHandles.Clear()
            Else
                desc = retDesc 'set back to old good state.
            End If
            HwndCountPreCheck = HandlesList.Length
        End If


        '//////////////////////ProcessName//////////////////////
        Dim pName As String = Me.GetProcessName(hwnd)
        Dim PID As Integer = Me.GetProcessID(hwnd)
        If (pName <> "") Then
            type = "processname"
            desc.Add(type, pName)
            'due to speed, this one gets heavy optimization

            Dim Proc As System.Diagnostics.Process = Nothing
            Proc = System.Diagnostics.Process.GetProcessById(PID)

            If (Proc IsNot Nothing) Then 'We didn't find a process :(
                'By iterating the processes first, this becomes a more effienct loop.
                For Each handle As IntPtr In HandlesList
                    If (Me.GetProcessID(handle) = PID) Then
                        possibleHandles.Add(handle)
                    End If
                Next
            End If
        End If

        If (possibleHandles.Count >= 1) Then
            retDesc.Add(type, pName)
            If (possibleHandles.Count = 1) Then
                Return retDesc
            End If
            If (HwndCountPreCheck = possibleHandles.Count) Then
                retDesc.Remove(type)
                desc = retDesc 'set back to old good state.
            Else
                HandlesList = possibleHandles.ToArray()
            End If
            possibleHandles.Clear()
        Else
            desc = retDesc 'set back to old good state.
        End If
        HwndCountPreCheck = HandlesList.Length

        '///////////////////////window type////////////////////////
        type = "ControlType"
        desc.Add(type, GetObjectTypeAsString(hwnd))

        For Each handle As IntPtr In HandlesList
            If (GetObjectTypeAsString(handle).ToLower() = desc.WindowType()) Then
                possibleHandles.Add(handle)
            End If
        Next

        If (possibleHandles.Count >= 1) Then
            retDesc.Add(type, GetObjectTypeAsString(hwnd))
            If (possibleHandles.Count = 1) Then
                Return retDesc
            End If
            If (HwndCountPreCheck = possibleHandles.Count) Then
                retDesc.Remove(type)
                desc = retDesc 'set back to old good state.
            Else
                HandlesList = possibleHandles.ToArray()
            End If
            possibleHandles.Clear()
        Else
            desc = retDesc 'set back to old good state.
        End If
        HwndCountPreCheck = HandlesList.Length

        '///////////////////////near by label type////////////////////////
        'this is near the end of the list because it is likely costly to use, but
        'it also is a very effective method.
        type = "nearbylabel"
        desc.Add(type, Me.GetNearByLabel(hwnd).ToLowerInvariant())

        For Each handle As IntPtr In HandlesList
            If (GetNearByLabel(handle).ToLowerInvariant() = desc.NearByLabel()) Then
                possibleHandles.Add(handle)
            End If
        Next

        If (possibleHandles.Count >= 1) Then
            retDesc.Add(type, GetNearByLabel(hwnd))
            If (possibleHandles.Count = 1) Then
                Return retDesc
            End If
            If (HwndCountPreCheck = possibleHandles.Count) Then
                retDesc.Remove(type)
                desc = retDesc 'set back to old good state.
            Else
                HandlesList = possibleHandles.ToArray()
            End If
            possibleHandles.Clear()
        Else
            desc = retDesc 'set back to old good state.
        End If
        HwndCountPreCheck = HandlesList.Length

        '///////////////////////control id type////////////////////////
        'this is near the end of the list because it is likely costly to use, but
        'it also is a very effective method.
        type = "controlid"
        desc.Add(type, Me.GetControlID(hwnd).ToString())
        If (desc.ControlID = hwnd.ToInt32()) Then
            If (desc.ControlID <> 0) Then 'we don't even consider it for anything with 0 as a ctrl id as that is invalid and/or a window.
                For Each handle As IntPtr In HandlesList
                    If (GetControlID(handle) = desc.ControlID()) Then
                        possibleHandles.Add(handle)
                    End If
                Next

                If (possibleHandles.Count >= 1) Then
                    retDesc.Add(type, GetControlID(hwnd).ToString())
                    If (possibleHandles.Count = 1) Then
                        Return retDesc
                    End If
                    If (HwndCountPreCheck = possibleHandles.Count) Then
                        retDesc.Remove(type)
                        desc = retDesc 'set back to old good state.
                    Else
                        HandlesList = possibleHandles.ToArray()
                    End If
                    possibleHandles.Clear()
                Else
                    desc = retDesc 'set back to old good state.
                End If
                HwndCountPreCheck = HandlesList.Length
            End If
        End If
        'Height before width, as many things expand width wise,
        'but fewer things expand height wise (examples:
        ' * single line text boxes
        ' * Toolbars
        ' * Tabs
        ' With that said, I suppose the reverse could be true
        ' too... That is to say, more things will have the
        ' same height, but not width.  But then both are
        ' include, doing the same amount of recording damage.
        '///////////////////////Height////////////////////////
        type = "height"
        desc.Add(type, Me.GetLocation(hwnd).Height.ToString())
        For Each handle As IntPtr In HandlesList
            If (Me.GetLocation(handle).Height.Equals(desc.Height)) Then
                possibleHandles.Add(handle)
            End If
        Next

        If (possibleHandles.Count >= 1) Then
#If isAbs = 1 Then
                retDesc.Add(type, Mouse.RelativeToAbsCoordY(IndependentWindowsFunctions.GetLocation(hwnd).Height))
#Else
            retDesc.Add(type, Me.GetLocation(hwnd).Height.ToString())
#End If
            If (possibleHandles.Count = 1) Then
                Return retDesc
            End If
            If (HwndCountPreCheck = possibleHandles.Count) Then
                retDesc.Remove(type)
                desc = retDesc 'set back to old good state.
            Else
                HandlesList = possibleHandles.ToArray()
            End If
            possibleHandles.Clear()
        Else
            desc = retDesc 'set back to old good state.
        End If
        HwndCountPreCheck = HandlesList.Length

        '///////////////////////Width/////////////////////////
        type = "width"
        desc.Add(type, Me.GetLocation(hwnd).Width.ToString())
        For Each handle As IntPtr In HandlesList
            If (Me.GetLocation(handle).Width.Equals(desc.Width)) Then
                possibleHandles.Add(handle)
            End If
        Next


        If (possibleHandles.Count >= 1) Then
#If isAbs = 1 Then
                retDesc.Add(type, Mouse.RelativeToAbsCoordX(IndependentWindowsFunctions.GetLocation(hwnd).Width))
#Else
            retDesc.Add(type, Me.GetLocation(hwnd).Width.ToString())
#End If
            If (possibleHandles.Count = 1) Then
                Return retDesc
            End If
            If (HwndCountPreCheck = possibleHandles.Count) Then
                retDesc.Remove(type)
                desc = retDesc 'set back to old good state.
            Else
                HandlesList = possibleHandles.ToArray()
            End If
            possibleHandles.Clear()
        Else
            desc = retDesc 'set back to old good state.
        End If
        HwndCountPreCheck = HandlesList.Length


        '///////////////////////Top////////////////////////
        type = "top"
        Dim rec As System.Drawing.Rectangle = Me.GetLocation(hwnd)
        desc.Add(type, rec.Top.ToString())
        For Each handle As IntPtr In HandlesList
            If (Me.GetLocation(handle).Top.Equals(rec.Top)) Then
                possibleHandles.Add(handle)
            End If
        Next


        If (possibleHandles.Count >= 1) Then
#If isAbs = 1 Then
                retDesc.Add(type, Mouse.RelativeToAbsCoordY(rec.Top))
#Else
            retDesc.Add(type, rec.Top.ToString())
#End If
            If (possibleHandles.Count = 1) Then
                Return retDesc
            End If
            If (HwndCountPreCheck = possibleHandles.Count) Then
                retDesc.Remove(type)
                desc = retDesc 'set back to old good state.
            Else
                HandlesList = possibleHandles.ToArray()
            End If
            possibleHandles.Clear()
        Else
            desc = retDesc 'set back to old good state.
        End If
        HwndCountPreCheck = HandlesList.Length

        '///////////////////////left////////////////////////
        type = "left"
        desc.Add(type, rec.Left.ToString())
        For Each handle As IntPtr In HandlesList
            If (Me.GetLocation(handle).Left.Equals(rec.Left)) Then
                possibleHandles.Add(handle)
            End If
        Next

        If (possibleHandles.Count >= 1) Then
#If isAbs = 1 Then
                retDesc.Add(type, Mouse.RelativeToAbsCoordX(rec.Left))
#Else
            retDesc.Add(type, rec.Left.ToString())
#End If
            If (possibleHandles.Count = 1) Then
                Return retDesc
            End If
            If (HwndCountPreCheck = possibleHandles.Count) Then
                retDesc.Remove(type)
                desc = retDesc 'set back to old good state.
            Else
                HandlesList = possibleHandles.ToArray()
            End If
            possibleHandles.Clear()
        Else
            desc = retDesc 'set back to old good state.
        End If
        HwndCountPreCheck = HandlesList.Length

        '///////////////////////right////////////////////////
        type = "right"
        desc.Add(type, rec.Right.ToString())
        For Each handle As IntPtr In HandlesList
            If (GetLocation(handle).Right.Equals(rec.Right)) Then
                possibleHandles.Add(handle)
            End If
        Next

        If (possibleHandles.Count >= 1) Then
#If isAbs = 1 Then
                retDesc.Add(type, Mouse.RelativeToAbsCoordX(rec.Right))
#Else
            retDesc.Add(type, rec.Right.ToString())
#End If
            If (possibleHandles.Count = 1) Then
                Return retDesc
            End If
            If (HwndCountPreCheck = possibleHandles.Count) Then
                retDesc.Remove(type)
                desc = retDesc 'set back to old good state.
            Else
                HandlesList = possibleHandles.ToArray()
            End If
            possibleHandles.Clear()
        Else
            desc = retDesc 'set back to old good state.
        End If
        HwndCountPreCheck = HandlesList.Length

        '///////////////////////bottom////////////////////////
        type = "bottom"
        desc.Add(type, rec.Bottom.ToString())
        For Each handle As IntPtr In HandlesList

            If (Me.GetLocation(handle).Bottom.Equals(desc.Bottom)) Then
                possibleHandles.Add(handle)
            End If
        Next


        If (possibleHandles.Count >= 1) Then
#If isAbs = 1 Then
                retDesc.Add(type, Mouse.RelativeToAbsCoordY(rec.Bottom))
#Else
            retDesc.Add(type, rec.Bottom.ToString())
#End If
            If (possibleHandles.Count = 1) Then
                Return retDesc
            End If
            If (HwndCountPreCheck = possibleHandles.Count) Then
                retDesc.Remove(type)
                desc = retDesc 'set back to old good state.
            Else
                HandlesList = possibleHandles.ToArray()
            End If
            possibleHandles.Clear()
        Else
            desc = retDesc 'set back to old good state.
        End If
        HwndCountPreCheck = HandlesList.Length

        '///////////////////////hwnd////////////////////////
        type = "hwnd"
        desc.Add(type, hwnd.ToString())
        For Each handle As IntPtr In HandlesList
            If (handle.ToString().Equals(desc.Hwnd.ToString())) Then
                possibleHandles.Add(handle)
            End If
        Next

        If (possibleHandles.Count >= 1) Then
            retDesc.Add(type, hwnd.ToString())
            If (possibleHandles.Count = 1) Then
                Return retDesc
            End If
        End If
        Dim tmp As New Description()
        tmp.Add(type, hwnd.ToString)
        Return tmp
        'It's better to return a reasonably correct description than non description at all.
        'Throw New SlickTestAPIException("Could not generate a description for hwnd: " & hwnd.ToString())
    End Function

    ''' <summary>
    ''' Attempts to find the index by searching through a list of hwnds in a reasonable method.
    ''' </summary>
    ''' <param name="TopHwnd"></param>
    ''' <param name="SearchHwnd"></param>
    ''' <returns>Returns -1 if you are searching for the window or 
    ''' if the searched hwnd.</returns>
    ''' <remarks>Index should be reproducable if nothing is changed.</remarks>
    Public Function FindIndex(ByVal TopHwnd As IntPtr, ByVal SearchHwnd As IntPtr) As Integer
        If (TopHwnd = SearchHwnd) Then Return -1
        Dim ParentHwnd As IntPtr = IntPtr.Zero
        Dim IsSuccessful As Boolean = False
        Dim ct As Integer = 1
        If (SearchHwnd = IntPtr.Zero) Then Return -1
        If (TopHwnd = IntPtr.Zero) Then Return 0
        '''''''''''''
        Dim Children As IntPtr() = APIControls.EnumerateWindows.GetChildHandles(TopHwnd)
        For Each child As IntPtr In Children
            If (child = SearchHwnd) Then
                Return ct
            End If
            ct += 1
        Next
        ''''''''''''''
        'Do
        '    ParentHwnd = New IntPtr(FindWindowEx(TopHwnd, ParentHwnd, Nothing, Nothing))
        '    If (ParentHwnd <> IntPtr.Zero) Then
        '        ct += 1
        '        DebugData(ParentHwnd)
        '    End If
        '    If (ParentHwnd = SearchHwnd) Then Return ct
        '    ct = SearchSubIndex(ParentHwnd, SearchHwnd, ct, IsSuccessful)
        '    If (IsSuccessful = True) Then
        '        Return ct
        '    End If
        'Loop While (ParentHwnd <> IntPtr.Zero)

        'ParentHwnd = WinAPI.API.GetWindow(TopHwnd, WinAPI.API.GW_CHILD)
        'If (ParentHwnd <> IntPtr.Zero) Then
        '    ct += 1
        '    DebugData(ParentHwnd)
        'End If
        'If (ParentHwnd = SearchHwnd) Then Return ct
        'ct = SearchSubIndex(ParentHwnd, SearchHwnd, ct, IsSuccessful)
        'If (IsSuccessful = True) Then
        '    Return ct
        'End If
        'WriteFile("******************************************")
        'Throw New Exception("Unable to get index.  TopHwnd: " & TopHwnd.ToString() & _
        '" SearchHwnd: " & SearchHwnd.ToString())
        Return 0
    End Function

    'Private Function SearchSubIndex(ByVal ParentHwnd As IntPtr, ByVal SearchHwnd As IntPtr, ByVal Count As Integer, ByRef IsSuccessful As Boolean) As Integer
    '    Dim TmpHwnd As IntPtr = New IntPtr(2) 'Just a madeup non zero value
    '    Dim TmpChildren As IntPtr = IntPtr.Zero
    '    While (TmpHwnd <> SearchHwnd AndAlso TmpHwnd <> IntPtr.Zero)
    '        TmpHwnd = WinAPI.API.GetWindow(ParentHwnd, WinAPI.API.GW_CHILD)
    '        DebugData(TmpHwnd)
    '        If (TmpHwnd <> IntPtr.Zero) Then
    '            Count += 1
    '            If (TmpHwnd.Equals(SearchHwnd) = True) Then
    '                IsSuccessful = True
    '                Return Count
    '            End If
    '            TmpChildren = New IntPtr(FindWindowEx(ParentHwnd, TmpHwnd, Nothing, Nothing))
    '            If (TmpChildren <> IntPtr.Zero) Then
    '                DebugData(TmpChildren)

    '                Count += 1
    '                If (TmpChildren.Equals(SearchHwnd) = True) Then
    '                    IsSuccessful = True
    '                    Return Count
    '                End If
    '                Count = SearchSubIndex(TmpChildren, SearchHwnd, Count, IsSuccessful)
    '                If (IsSuccessful = True) Then
    '                    Return Count
    '                End If
    '                ParentHwnd = TmpHwnd
    '            Else
    '                ParentHwnd = TmpHwnd
    '            End If
    '        End If
    '    End While
    '    Return Count
    'End Function

    Public Function SearchForObj(ByVal desc As IDescription, ByVal hwnd As IntPtr) As IntPtr
        Dim TmpHwnd As IntPtr = IntPtr.Zero
        possibleHandles = New System.Collections.Generic.List(Of IntPtr)
        Dim tmpText As String = ""
        If (desc.Count = 0) Then
            Throw New SlickTestAPIException("No information provided in description.")
        End If
        HandleCount = 1
        If (hwnd <> IntPtr.Zero) Then
            HandlesList = EnumerateWindows.GetChildHandles(hwnd)
            If (desc.Contains(Description.DescriptionData.Hwnd) = True) Then 'handle search first, 
                'since it's a special and boolean case.. if the handle is not
                'found then the description is wrong.
                For Each handle As IntPtr In HandlesList
                    If (handle.Equals(desc.Hwnd)) Then
                        Return handle
                    End If
                Next
                If (desc.Hwnd = hwnd) Then Return hwnd
                HandleCount = 0
                Return IntPtr.Zero
            End If
        Else
            'elminates all possible non-windows right off the bat.
            'If (SearchVisibleOnly = True) Then
            'search handles.
            If (desc.Contains(Description.DescriptionData.Hwnd) = True) Then 'handle search first, 
                'since it's a special and boolean case.. if the handle is not
                'found then the description is wrong.
                'System.Console.WriteLine(desc.Hwnd.ToString)
                For Each handle As IntPtr In EnumerateWindows.GetHandles()

                    If (handle.Equals(desc.Hwnd)) Then
                        Return handle
                    End If
                    For Each subHandle As IntPtr In EnumerateWindows.GetChildHandles(handle) 'Sub handles are not included in handles list
                        If (subHandle.Equals(desc.Hwnd)) Then
                            Return subHandle
                        End If
                    Next
                Next
                HandleCount = 0
                Return IntPtr.Zero
            Else

                For Each handle As IntPtr In EnumerateWindows.GetHandles()
                    'If Win32Window.IsWindowVisible(handle) And getParent(handle) = 0 Then
                    'If getParent(handle) = 0 Then
                    If (possibleHandles.Contains(handle) = False) Then possibleHandles.Add(handle)
                    For Each subHandle As IntPtr In EnumerateWindows.GetChildHandles(handle) 'Sub handles are not included in handles list
                        If (possibleHandles.Contains(handle) = False) Then possibleHandles.Add(handle)
                    Next
                    ' End If
                Next
            End If
            HandlesList = possibleHandles.ToArray()
            possibleHandles.Clear()
        End If

        'search name if needed
        If (desc.Contains(Description.DescriptionData.Name) = True) Then
            For Each handle As IntPtr In HandlesList
                tmpText = GetClassName(handle)
                'If tmpText <> "" Then Console.WriteLine("cls = " & tmpText)
                If (desc.WildCard = False) Then
                    If tmpText = desc.Name Then
                        possibleHandles.Add(handle)
                        'Console.WriteLine("*** FOUND cls = " & tmpText)
                    End If
                Else
                    If tmpText Like desc.Name Then
                        possibleHandles.Add(handle)
                    End If
                End If
            Next
            HandlesList = possibleHandles.ToArray()
            possibleHandles.Clear()
        End If

        If (HandlesList.Length = 0) Then
            HandleCount = 0
            Return IntPtr.Zero
        End If

        If (desc.Contains(Description.DescriptionData.Value) = True) Then
            For Each handle As IntPtr In HandlesList
                tmpText = Me.GetAllWindowTextSmart(handle)
                'If tmpText <> "" Then Console.WriteLine("text = " & tmpText)
                If (desc.WildCard = False) Then
                    If tmpText = desc.Value Then
                        possibleHandles.Add(handle)
                        'Console.WriteLine("*** FOUND text = " & tmpText)
                    End If
                Else
                    If tmpText Like desc.Value Then
                        possibleHandles.Add(handle)
                    End If
                End If
            Next
            HandlesList = possibleHandles.ToArray()
            possibleHandles.Clear()
        End If

        If (HandlesList.Length = 0) Then
            HandleCount = 0
            Return IntPtr.Zero
        End If

        'search for process name
        If (desc.Contains(Description.DescriptionData.ProcessName) = True) Then
            For Each handle As IntPtr In HandlesList
                tmpText = Me.GetProcessName(handle)
                'If tmpText <> "" Then Console.WriteLine("text = " & tmpText)
                If (desc.WildCard = False) Then
                    If tmpText = desc.ProcessName Then
                        possibleHandles.Add(handle)
                        'Console.WriteLine("*** FOUND text = " & tmpText)
                    End If
                Else
                    If tmpText Like desc.ProcessName Then
                        possibleHandles.Add(handle)
                    End If
                End If
            Next
            HandlesList = possibleHandles.ToArray()
            possibleHandles.Clear()
        End If

        If (HandlesList.Length = 0) Then
            HandleCount = 0
            Return IntPtr.Zero
        End If

        'search for index
        If (desc.Contains(Description.DescriptionData.Index) = True) Then
            Dim parent As IntPtr
            Dim index As Integer
            For Each handle As IntPtr In HandlesList
                'parent = Me.GetParent(handle)
                parent = Me.GetTopParent(handle)
                index = Me.FindIndex(parent, handle)
                If index = desc.Index Then
                    possibleHandles.Add(handle)
                End If
            Next
            HandlesList = possibleHandles.ToArray()
            possibleHandles.Clear()
        End If

        If (HandlesList.Length = 0) Then
            HandleCount = 0
            Return IntPtr.Zero
        End If

        'search for controlID
        If (desc.Contains(Description.DescriptionData.ControlID) = True) Then
            Dim cntrlId As Integer
            For Each handle As IntPtr In HandlesList
                cntrlId = Me.GetControlID(handle)
                If cntrlId = desc.ControlID Then
                    possibleHandles.Add(handle)
                End If
            Next
            HandlesList = possibleHandles.ToArray()
            possibleHandles.Clear()
        End If

        If (HandlesList.Length = 0) Then
            HandleCount = 0
            Return IntPtr.Zero
        End If

        'seach location/size
        Dim rectangle As System.Drawing.Rectangle = New System.Drawing.Rectangle()
        rectangle = Drawing.Rectangle.Empty
        rectangle.X = -1
        rectangle.Y = -1
        If (desc.Location <> rectangle) Then
            Dim results As Boolean = False
            For Each handle As IntPtr In HandlesList
                rectangle = Me.GetLocation(handle)
                results = True

                If (desc.Contains(Description.DescriptionData.Left) = True) Then
                    If (rectangle.Left <> desc.Left) Then
                        results = False
                    End If
                End If
                If (desc.Contains(Description.DescriptionData.Right) = True) Then
                    If (rectangle.Right <> desc.Right) Then
                        results = False
                    End If
                End If
                If (desc.Contains(Description.DescriptionData.Top) = True) Then
                    If (rectangle.Top <> desc.Top) Then
                        results = False
                    End If
                End If
                If (desc.Contains(Description.DescriptionData.Bottom) = True) Then
                    If (rectangle.Bottom <> desc.Bottom) Then
                        results = False
                    End If
                End If
                'size
                If (desc.Contains(Description.DescriptionData.Width) = True) Then
                    If (rectangle.Width <> desc.Width) Then
                        results = False
                    End If
                End If
                If (desc.Contains(Description.DescriptionData.Height) = True) Then
                    If (rectangle.Height <> desc.Height) Then
                        results = False
                    End If
                End If

                If (results = True) Then
                    possibleHandles.Add(handle)
                End If
            Next
            HandlesList = possibleHandles.ToArray()
            possibleHandles.Clear()
        End If
        If (HandlesList.Length = 0) Then
            HandleCount = 0
            Return IntPtr.Zero
        End If

        'search Window Type
        If (desc.Contains(Description.DescriptionData.ControlType) = True) Then
            For Each handle As IntPtr In HandlesList
                If (GetObjectTypeAsString(handle).ToLower = desc.WindowType) Then
                    possibleHandles.Add(handle)
                End If
            Next
            HandlesList = possibleHandles.ToArray()
            possibleHandles.Clear()
        End If

        If (HandlesList.Length = 0) Then
            HandleCount = 0
            Return IntPtr.Zero
        End If

        'search for near by labels.
        If (desc.Contains(Description.DescriptionData.NearByLabel) = True) Then

            For Each handle As IntPtr In HandlesList
                Try
                    tmpText = System.Windows.Automation.AutomationElement.FromHandle(handle).Current.LabeledBy.Current.Name 'See Me.GetNearByLabel()
                Catch ex As Exception
                    Continue For 'fail, don't compare.
                End Try
                If (desc.WildCard = False) Then
                    If tmpText = desc.NearByLabel Then
                        possibleHandles.Add(handle)
                    End If
                Else
                    If tmpText Like desc.NearByLabel Then
                        possibleHandles.Add(handle)
                    End If
                End If
            Next
            HandlesList = possibleHandles.ToArray()
            possibleHandles.Clear()
        End If

        If (HandlesList.Length = 0) Then
            HandleCount = 0
            Return IntPtr.Zero
        End If

        '//////////// search completed.
        If (HandlesList.Length = 0) Then
            HandleCount = 0
            Return IntPtr.Zero
        Else
            If (HandlesList.Length > 1) Then 'too many handles
                HandleCount = HandlesList.Length
                Throw New SlickTestAPIException("More than one item with the description " & desc.ToString() & " was found.")
            Else
                Return HandlesList(0)
            End If
        End If

    End Function

    Public Function GetObjectTypeAsString(ByVal hwnd As IntPtr) As String
        Dim retVal As String
        If (Me.ListBox.IsListBox(hwnd)) Then
            retVal = "ListBox"
        ElseIf (Me.ListView.IsListView(hwnd)) Then
            retVal = "ListView"
        ElseIf (Me.ComboBox.IsComboBox(hwnd)) Then
            retVal = "ComboBox"
        ElseIf (Me.TextBox.IsTextBox(hwnd)) Then
            retVal = "TextBox"
        ElseIf (Me.Button.IsButton(hwnd)) Then
            retVal = "Button"
        ElseIf (Me.StaticLabel.IsStaticLabel(hwnd)) Then
            retVal = "StaticLabel"
        ElseIf (Me.Button.IsCheckBox(hwnd)) Then
            retVal = "CheckBox"
        ElseIf (Me.Button.IsRadioButton(hwnd)) Then
            retVal = "RadioButton"
        ElseIf (Me.TreeView.IsTreeView(hwnd)) Then
            retVal = "TreeView"
        ElseIf (Me.TabControl.IsTabControl(hwnd)) Then
            retVal = "TabControl"
        ElseIf (Window.IsWindow(CType(hwnd, IntPtr))) Then

            If (IsWebPartIE(hwnd) = True) Then
                retVal = "IEWebBrowser"
            Else
                retVal = "Window"
            End If
        Else
            retVal = "WinObject"
        End If
        Return retVal
    End Function

    Public Function SearchForApp(ByVal AppTitle As String, ByVal AppClassName As String) As IntPtr
        If (AppTitle = "") Then
            AppTitle = vbNullString
        End If
        If (AppClassName = "") Then
            AppClassName = vbNullString
        End If
        Return FindWindow(AppClassName, AppTitle)
    End Function

    Public Function SearchForObjInApp(ByVal AppTitle As String, ByVal hwnd As IntPtr, ByVal ClassName As String) As IntPtr
        Dim Value As String = ""
        Dim TitleHwnd As IntPtr = FindWindow(vbNullString, AppTitle)
        Dim TmpHwnd As IntPtr = IntPtr.Zero
        If (TitleHwnd = IntPtr.Zero) Then
            Return IntPtr.Zero
        End If
        'If (ClassName.Contains("::") = True) Then
        '    Value = ClassName.Split("::").GetValue(2).ToString()
        '    ClassName = ClassName.Split("::").GetValue(0).ToString()
        'End If
        If (hwnd = TitleHwnd) Then
            TmpHwnd = New IntPtr(FindWindowEx(hwnd, IntPtr.Zero, ClassName, Value))
        Else
            TmpHwnd = New IntPtr(FindWindowEx(hwnd, TitleHwnd, ClassName, Value))
        End If
        Return TmpHwnd
    End Function

    Public Function SearchForObjInApp(ByVal AppTitle As String, ByVal ClassName As String, ByVal Value As String) As IntPtr
        Dim TitleHwnd As IntPtr = FindWindow(vbNullString, AppTitle)
        If (TitleHwnd = IntPtr.Zero) Then
            Return IntPtr.Zero
        End If
        Return New IntPtr(FindWindowEx(TitleHwnd, IntPtr.Zero, ClassName, Value))
    End Function

    Public Function SearchForObjInApp(ByVal FirstPointer As IntPtr, ByVal ClassName As String, ByVal Value As String) As IntPtr
        Return SearchForObjInApp(FirstPointer, IntPtr.Zero, ClassName, Value)
    End Function

    Public Function SearchForObjInApp(ByVal FirstPointer As IntPtr, ByVal NextPointer As IntPtr, ByVal ClassName As String, ByVal Value As String) As IntPtr
        Return New IntPtr(FindWindowEx(FirstPointer, NextPointer, ClassName, Value))
    End Function
    Private Const DotNetNameWindowsForms As String = "windowsform"

    Public Function IsDotNet(ByVal hwnd As IntPtr) As Boolean
        Dim str As String = GetClassNameNoDotNet(hwnd)
        If (str.ToLowerInvariant.IndexOf(DotNetNameWindowsForms) = -1) Then Return False
        Return True
    End Function

    Public Function GetClassName(ByVal element As AutomationElement) As String
        Return element.Current.ClassName
    End Function

    Public Function GetClassName(ByVal hWnd As IntPtr) As String
        If (GenericMethodsUIAutomation.IsCustom(hWnd)) Then
            Return WpfGetClassName(hWnd)
        End If
        Try
            Dim str As String = Me.GetClassNameNoDotNet(hWnd)
            If (str.ToLowerInvariant.IndexOf(DotNetNameWindowsForms) = -1) Then Return str
            Return GetDotNetClassName(hWnd)
        Catch ex As Exception
            Return ""
        End Try
    End Function

    Public Function IsWebPartIE(ByRef Hwnd As IntPtr) As Boolean
        Dim tmpRef As IntPtr = Hwnd
        While (IsWebPartExactlyIE(tmpRef) = False)
            tmpRef = GetParent(tmpRef)
            If (tmpRef = IntPtr.Zero) Then
                Return False
            End If
        End While
        Hwnd = tmpRef
        Return True
    End Function

    Public Function IsWebPartExactlyIE(ByVal Hwnd As IntPtr) As Boolean
        Return (GetClassName(Hwnd) = "IEFrame")
    End Function

    Public Function IsWebPartIEHTML(ByVal Hwnd As IntPtr) As Boolean
        Return (GetClassName(Hwnd) = "Internet Explorer_Server")
    End Function

    Public Function GetDotNetClassName(ByVal hWnd As IntPtr) As String
        Return APIControls.DotNetNames.GetWinFormsId(hWnd)
    End Function
End Class
