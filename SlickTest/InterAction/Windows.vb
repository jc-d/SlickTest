
'Imports winAPI.API
'Imports winAPI.NativeFunctions
'Imports System.Runtime.InteropServices
'Imports System.Text
'Imports System.ComponentModel
'Imports System




'Public Class Windowsv1
'#Const isAbs = 2 'Set to 1 if you want to use absolute values
'    Protected Friend HandlesList() As IntPtr
'    Private MessageID As Integer = 0
'    Private possibleHandles As System.Collections.Generic.List(Of IntPtr)
'    Public UseDotNet As Boolean = False
'    Protected Friend HandleCount As Integer = 0
'    Private Shared WindowsFunctions As New APIControls.IndependentWindowsFunctionsv1()

'    Private Function BuildList(ByVal TopWindowhwnd As IntPtr, ByVal hwnd As IntPtr) As IntPtr()
'        If (hwnd = 0) Then
'            '    'elminates all possible non-windows right off the bat.
'            '    'turns out sometimes windows appear invisible even when they shouldn't.
'            '    'Win32Window.IsWindowVisible(handle) And
'            For Each handle As IntPtr In EnumerateWindows.GetHandles()
'                possibleHandles.Add(handle)
'            Next
'            HandlesList = possibleHandles.ToArray()
'            possibleHandles.Clear()
'        Else
'            For Each handle As IntPtr In EnumerateWindows.GetChildHandles(TopWindowhwnd)
'                possibleHandles.Add(handle)
'            Next
'            HandlesList = possibleHandles.ToArray()
'            possibleHandles.Clear()
'        End If
'        'If (hwnd <> 0 And isAParent(hwnd, TopWindowhwnd) = True) Then
'        'handlesList = Win32Window.GetChildHandles(TopWindowhwnd)
'        'For Each handle As IntPtr In Win32Window.GetHandles()
'        ' If getParent(handle) = 0 Then
'        'possibleHandles.Add(handle)
'        ' End If
'        'Next
'        'handlesList = possibleHandles.ToArray()
'        'possibleHandles.Clear()
'        'Else
'        ' End If
'        'Return handlesList
'        Return HandlesList
'    End Function


'    ''' <summary>
'    ''' 
'    ''' </summary>
'    ''' <param name="text"></param>
'    ''' <param name="hwnd"></param>
'    ''' <param name="AppHwnd"></param>
'    ''' <remarks></remarks>
'    Public Sub SendTextByHandler(ByVal text As String, ByVal hwnd As IntPtr, ByVal AppHwnd As IntPtr)
'        WindowsFunctions.Window.SetToForeGround(AppHwnd)
'        WinAPI.API.SetFocus(hwnd)
'        Keyboard.SendKeys(text)
'    End Sub

'    ''' <summary>
'    ''' Builds a description from any point in the list of hwnds.
'    ''' </summary>
'    ''' <param name="TopWindowhwnd">The actual window or parent object</param>
'    ''' <param name="hwnd">The object you wish to create the description for.  
'    ''' Pass the parent object in agian, if you want that for the description
'    ''' created.</param>
'    ''' <returns>returns the description created.</returns>
'    ''' <remarks></remarks>
'    Public Function CreateDescription(ByVal TopWindowhwnd As IntPtr, ByVal hwnd As IntPtr) As Description
'        possibleHandles = New System.Collections.Generic.List(Of IntPtr)
'        'Console.WriteLine("Process: TopWindow Hwnd = " & TopWindowhwnd.ToString() & _
'        '            "hwnd = " & hwnd.ToString())
'        HandlesList = BuildList(TopWindowhwnd, hwnd)

'        If (hwnd = 0) Then
'            hwnd = TopWindowhwnd
'            'Else
'            '    Console.WriteLine("hwnd= " & _
'            '    hwnd.ToString() & " TopWindowhwnd= " & TopWindowhwnd.ToString() & _
'            '    " getParent(hwnd) = " & GetParent(hwnd).ToString())
'        End If

'        'System.Console.WriteLine("handlesList size = " & HandlesList.Length.ToString)


'        Dim desc As Description = New Description()
'        Dim retDesc As Description = New Description()
'        Dim type As String = ""

'        '//////////////////////ClassName//////////////////////
'        type = "name"
'        desc.Add(type, GetClassName(hwnd))
'        Dim str As String
'        Dim test As Boolean = False
'        For Each handle As IntPtr In HandlesList
'            If (handle.Equals(hwnd)) Then
'                test = True
'            End If
'        Next
'        'Console.WriteLine("********** TEST: " & test)
'        For Each handle As IntPtr In HandlesList
'            str = GetClassName(handle)
'            If (str.Equals(desc.Name)) Then
'                possibleHandles.Add(handle)
'            End If
'        Next

'        If (possibleHandles.Count >= 1) Then
'            retDesc.Add(type, GetClassName(hwnd))
'            If (possibleHandles.Count = 1) Then
'                Return retDesc
'            End If
'            HandlesList = possibleHandles.ToArray()
'            possibleHandles.Clear()
'        Else
'            desc = retDesc 'set back to old good state.
'        End If

'        '//////////////////////Value//////////////////////
'        type = "value"
'        desc.Add(type, WindowsFunctions.GetText(hwnd))
'        For Each handle As IntPtr In HandlesList
'            If (WindowsFunctions.GetText(handle).Equals(desc.Value)) Then
'                possibleHandles.Add(handle)
'            End If
'        Next

'        If (possibleHandles.Count >= 1) Then
'            retDesc.Add(type, WindowsFunctions.GetText(hwnd))
'            If (possibleHandles.Count = 1) Then
'                'special case:  name/value is the most common
'                'type of recording so, I would rather take in less data rather than
'                'more.  If name/value did it, check if just value could do it.
'                'slower, but better.
'                possibleHandles.Clear()
'                If (hwnd = 0) Then
'                    hwnd = TopWindowhwnd
'                End If
'                HandlesList = BuildList(TopWindowhwnd, hwnd)
'                For Each handle As IntPtr In HandlesList
'                    If (WindowsFunctions.GetText(handle).Equals(desc.Value)) Then
'                        possibleHandles.Add(handle)
'                    End If
'                Next
'                If (possibleHandles.Count = 1) Then
'                    If (possibleHandles(0) = hwnd) Then
'                        'it's possible it picked up a 
'                        '"empty text" or something silly
'                        'like that...

'                        'great, text only worked :D
'                        retDesc = New Description()
'                        retDesc.Add(type, WindowsFunctions.GetText(hwnd))
'                        'else text alone did not work
'                    End If
'                End If
'                Return retDesc
'            End If
'            HandlesList = possibleHandles.ToArray()
'            possibleHandles.Clear()
'        Else
'            desc = retDesc 'set back to old good state.
'        End If
'        '///////////////////////Width/////////////////////////
'        type = "width"
'        desc.Add(type, WindowsFunctions.GetLocation(hwnd).Width.ToString())
'        For Each handle As IntPtr In HandlesList
'            If (WindowsFunctions.GetLocation(handle).Width.Equals(desc.Width)) Then
'                possibleHandles.Add(handle)
'            End If
'        Next

'        If (possibleHandles.Count >= 1) Then
'#If isAbs = 1 Then
'            retDesc.Add(type, Mouse.RelativeToAbsCoordX(IndependentWindowsFunctions.GetLocation(hwnd).Width))
'#Else
'            retDesc.Add(type, WindowsFunctions.GetLocation(hwnd).Width.ToString())
'#End If
'            If (possibleHandles.Count = 1) Then
'                Return retDesc
'            End If
'            HandlesList = possibleHandles.ToArray()
'            possibleHandles.Clear()
'        Else
'            desc = retDesc 'set back to old good state.
'        End If

'        '///////////////////////Height////////////////////////
'        type = "height"
'        desc.Add(type, WindowsFunctions.GetLocation(hwnd).Height.ToString())
'        For Each handle As IntPtr In HandlesList
'            If (WindowsFunctions.GetLocation(handle).Height.Equals(desc.Height)) Then
'                possibleHandles.Add(handle)
'            End If
'        Next

'        If (possibleHandles.Count >= 1) Then
'#If isAbs = 1 Then
'            retDesc.Add(type, Mouse.RelativeToAbsCoordY(IndependentWindowsFunctions.GetLocation(hwnd).Height))
'#Else
'            retDesc.Add(type, WindowsFunctions.GetLocation(hwnd).Height.ToString())
'#End If
'            If (possibleHandles.Count = 1) Then
'                Return retDesc
'            End If
'            HandlesList = possibleHandles.ToArray()
'            possibleHandles.Clear()
'        Else
'            desc = retDesc 'set back to old good state.
'        End If

'        '///////////////////////Top////////////////////////
'        type = "top"
'        Dim rec As System.Drawing.Rectangle = WindowsFunctions.GetLocation(hwnd)
'        desc.Add(type, rec.Top.ToString())
'        For Each handle As IntPtr In HandlesList
'            If (WindowsFunctions.GetLocation(handle).Top.Equals(rec.Top)) Then
'                possibleHandles.Add(handle)
'            End If
'        Next

'        If (possibleHandles.Count >= 1) Then
'#If isAbs = 1 Then
'            retDesc.Add(type, Mouse.RelativeToAbsCoordY(rec.Top))
'#Else
'            retDesc.Add(type, rec.Top.ToString())
'#End If

'            If (possibleHandles.Count = 1) Then
'                Return retDesc
'            End If
'            HandlesList = possibleHandles.ToArray()
'            possibleHandles.Clear()
'        Else
'            desc = retDesc 'set back to old good state.
'        End If

'        '///////////////////////left////////////////////////
'        type = "left"
'        desc.Add(type, rec.Left.ToString())
'        For Each handle As IntPtr In HandlesList
'            If (WindowsFunctions.GetLocation(handle).Left.Equals(rec.Left)) Then
'                possibleHandles.Add(handle)
'            End If
'        Next

'        If (possibleHandles.Count >= 1) Then
'#If isAbs = 1 Then
'            retDesc.Add(type, Mouse.RelativeToAbsCoordX(rec.Left))
'#Else
'            retDesc.Add(type, rec.Left.ToString())
'#End If
'            If (possibleHandles.Count = 1) Then
'                Return retDesc
'            End If
'            HandlesList = possibleHandles.ToArray()
'            possibleHandles.Clear()
'        Else
'            desc = retDesc 'set back to old good state.
'        End If

'        '///////////////////////right////////////////////////
'        type = "right"
'        desc.Add(type, rec.Right.ToString())
'        For Each handle As IntPtr In HandlesList
'            If (GetClassName(handle).Equals(rec.Right)) Then
'                possibleHandles.Add(handle)
'            End If
'        Next

'        If (possibleHandles.Count >= 1) Then
'#If isAbs = 1 Then
'            retDesc.Add(type, Mouse.RelativeToAbsCoordX(rec.Right))
'#Else
'            retDesc.Add(type, rec.Right.ToString())
'#End If
'            If (possibleHandles.Count = 1) Then
'                Return retDesc
'            End If
'            HandlesList = possibleHandles.ToArray()
'            possibleHandles.Clear()
'        Else
'            desc = retDesc 'set back to old good state.
'        End If

'        '///////////////////////bottom////////////////////////
'        type = "bottom"
'        desc.Add(type, rec.Bottom.ToString())
'        For Each handle As IntPtr In HandlesList

'            If (WindowsFunctions.GetLocation(handle).Bottom.Equals(desc.Bottom)) Then
'                possibleHandles.Add(handle)
'            End If
'        Next

'        If (possibleHandles.Count >= 1) Then
'#If isAbs = 1 Then
'            retDesc.Add(type, Mouse.RelativeToAbsCoordY(rec.Bottom))
'#Else
'            retDesc.Add(type, rec.Bottom.ToString())
'#End If
'            If (possibleHandles.Count = 1) Then
'                Return retDesc
'            End If
'            HandlesList = possibleHandles.ToArray()
'            possibleHandles.Clear()
'        Else
'            desc = retDesc 'set back to old good state.
'        End If
'        '///////////////////////window type////////////////////////
'        type = "windowtype"
'        desc.Add(type, GetObjectTypeAsString(hwnd))


'        For Each handle As IntPtr In HandlesList
'            If (GetObjectTypeAsString(handle) = desc.WindowType()) Then
'                possibleHandles.Add(handle)
'            End If
'        Next

'        If (possibleHandles.Count >= 1) Then
'            retDesc.Add(type, IsWindow(hwnd).ToString())
'            If (possibleHandles.Count = 1) Then
'                Return retDesc
'            End If
'            HandlesList = possibleHandles.ToArray()
'            possibleHandles.Clear()
'        Else
'            desc = retDesc 'set back to old good state.
'        End If
'        '///////////////////////hwnd////////////////////////
'        type = "hwnd"
'        desc.Add(type, hwnd.ToString())
'        For Each handle As IntPtr In HandlesList
'            If (handle.ToString().Equals(desc.Hwnd)) Then
'                possibleHandles.Add(handle)
'            End If
'        Next

'        If (possibleHandles.Count >= 1) Then
'            retDesc.Add(type, hwnd.ToString())
'            If (possibleHandles.Count = 1) Then
'                Return retDesc
'            End If
'            'Else
'            'desc = retDesc 'set back to old good state.
'        End If
'        'handlesList = possibleHandles.ToArray()
'        'possibleHandles.Clear()
'        Dim tmp As New Description()
'        tmp.Add(type, hwnd.ToString)
'        Return tmp
'        Throw New Exception("Could not generate a description for hwnd: " & hwnd.ToString())

'    End Function

'    Public Function SearchForObj(ByVal desc As APIControls.Description, ByVal hwnd As IntPtr) As IntPtr
'        Dim TmpHwnd As IntPtr = 0
'        possibleHandles = New System.Collections.Generic.List(Of IntPtr)
'        Dim tmpText As String = ""
'        If (desc.Count = 0) Then
'            Throw New Exception("No information provided in description")
'        End If
'        HandleCount = 1
'        If (hwnd <> IntPtr.Zero) Then
'            HandlesList = EnumerateWindows.GetChildHandles(hwnd)
'            If (desc.Contains(Description.DescriptionData.Hwnd) = True) Then 'handle search first, 
'                'since it's a special and boolean case.. if the handle is not
'                'found then the description is wrong.
'                For Each handle As IntPtr In HandlesList
'                    If (handle.Equals(desc.Hwnd)) Then
'                        Return handle
'                    End If
'                Next
'                HandleCount = 0
'                Return 0
'            End If
'        Else
'            'elminates all possible non-windows right off the bat.
'            'If (SearchVisibleOnly = True) Then
'            'search handles.
'            If (desc.Contains(Description.DescriptionData.Hwnd) = True) Then 'handle search first, 
'                'since it's a special and boolean case.. if the handle is not
'                'found then the description is wrong.
'                'System.Console.WriteLine(desc.Hwnd.ToString)
'                For Each handle As IntPtr In EnumerateWindows.GetHandles()

'                    If (handle.Equals(desc.Hwnd)) Then
'                        Return handle
'                    End If
'                    For Each subHandle As IntPtr In EnumerateWindows.GetChildHandles(handle) 'Sub handles are not included in handles list
'                        If (subHandle.Equals(desc.Hwnd)) Then
'                            Return subHandle
'                        End If
'                    Next
'                Next
'                HandleCount = 0
'                Return 0
'            Else

'                For Each handle As IntPtr In EnumerateWindows.GetHandles()
'                    'If Win32Window.IsWindowVisible(handle) And getParent(handle) = 0 Then
'                    'If getParent(handle) = 0 Then
'                    possibleHandles.Add(handle)
'                    ' End If
'                Next
'            End If
'            HandlesList = possibleHandles.ToArray()
'            possibleHandles.Clear()
'        End If

'        'search name if needed
'        If (desc.Contains(Description.DescriptionData.Name) = True) Then
'            For Each handle As IntPtr In HandlesList
'                tmpText = GetClassName(handle)
'                'If tmpText <> "" Then Console.WriteLine("cls = " & tmpText)
'                If (desc.WildCard = False) Then
'                    If tmpText = desc.Name Then
'                        possibleHandles.Add(handle)
'                        'Console.WriteLine("*** FOUND cls = " & tmpText)
'                    End If
'                Else
'                    If tmpText Like desc.Name Then
'                        possibleHandles.Add(handle)
'                    End If
'                End If
'            Next
'            HandlesList = possibleHandles.ToArray()
'            possibleHandles.Clear()
'        End If

'        If (HandlesList.Length = 0) Then
'            HandleCount = 0
'            Return 0
'        End If

'        If (desc.Contains(Description.DescriptionData.Value) = True) Then
'            For Each handle As IntPtr In HandlesList
'                tmpText = WindowsFunctions.GetText(handle)
'                'If tmpText <> "" Then Console.WriteLine("text = " & tmpText)
'                If (desc.WildCard = False) Then
'                    If tmpText = desc.Value Then
'                        possibleHandles.Add(handle)
'                        'Console.WriteLine("*** FOUND text = " & tmpText)
'                    End If
'                Else
'                    If tmpText Like desc.Value Then
'                        possibleHandles.Add(handle)
'                    End If
'                End If
'            Next
'            HandlesList = possibleHandles.ToArray()
'            possibleHandles.Clear()
'        End If

'        If (HandlesList.Length = 0) Then
'            HandleCount = 0
'            Return 0
'        End If
'        'seach location/size
'        Dim rectangle As System.Drawing.Rectangle = New System.Drawing.Rectangle()
'        If (desc.Location <> rectangle.Empty) Then
'            Dim results As Boolean = False
'            For Each handle As IntPtr In HandlesList
'                rectangle = WindowsFunctions.GetLocation(handle)
'                results = True

'                If (desc.Contains(Description.DescriptionData.Left) = True) Then
'                    If (rectangle.Left <> desc.Left) Then
'                        results = False
'                    End If
'                End If
'                If (desc.Contains(Description.DescriptionData.Right) = True) Then
'                    If (rectangle.Right <> desc.Right) Then
'                        results = False
'                    End If
'                End If
'                If (desc.Contains(Description.DescriptionData.Top) = True) Then
'                    If (rectangle.Top <> desc.Top) Then
'                        results = False
'                    End If
'                End If
'                If (desc.Contains(Description.DescriptionData.Bottom) = True) Then
'                    If (rectangle.Bottom <> desc.Bottom) Then
'                        results = False
'                    End If
'                End If
'                'size
'                If (desc.Contains(Description.DescriptionData.Width) = True) Then
'                    If (rectangle.Width <> desc.Width) Then
'                        results = False
'                    End If
'                End If
'                If (desc.Contains(Description.DescriptionData.Height) = True) Then
'                    If (rectangle.Height <> desc.Height) Then
'                        results = False
'                    End If
'                End If

'                If (results = True) Then
'                    possibleHandles.Add(handle)
'                End If
'            Next
'            HandlesList = possibleHandles.ToArray()
'            possibleHandles.Clear()
'        End If
'        If (HandlesList.Length = 0) Then
'            HandleCount = 0
'            Return 0
'        End If
'        'search isWindow
'        If (desc.Contains(Description.DescriptionData.WindowType) = True) Then
'            For Each handle As IntPtr In HandlesList
'                If (GetObjectTypeAsString(handle).ToLower = desc.WindowType) Then
'                    possibleHandles.Add(handle)
'                End If
'            Next
'            HandlesList = possibleHandles.ToArray()
'            possibleHandles.Clear()
'        End If

'        If (HandlesList.Length = 0) Then
'            HandleCount = 0
'            Return 0
'        End If
'        '//////////// search completed.
'        If (HandlesList.Length = 0) Then
'            HandleCount = 0
'            Return 0
'        Else
'            If (HandlesList.Length > 1) Then 'too many handles
'                HandleCount = HandlesList.Length
'                Throw New Exception("More than one item with the description " & desc.ToString() & " was found.")
'            Else
'                Return HandlesList(0)
'            End If
'        End If

'    End Function

'    Public Function GetObjectTypeAsString(ByVal hwnd As IntPtr) As String
'        Dim retVal As String
'        If (WindowsFunctions.ListBox.IsListBox(hwnd)) Then
'            retVal = "ListBox"
'        ElseIf (WindowsFunctions.ComboBox.IsComboBox(hwnd)) Then
'            retVal = "ComboBox"
'        ElseIf (WindowsFunctions.TextBox.IsTextBox(hwnd)) Then
'            retVal = "TextBox"
'        ElseIf (WindowsFunctions.Button.IsButton(hwnd)) Then
'            retVal = "Button"
'        ElseIf (WindowsFunctions.StaticLabel.IsStaticLabel(hwnd)) Then
'            retVal = "StaticLabel"
'        ElseIf (WindowsFunctions.Button.IsCheckBox(hwnd)) Then
'            retVal = "CheckBox"
'        ElseIf (WindowsFunctions.Button.IsRadioButton(hwnd)) Then
'            retVal = "RadioButton"
'        ElseIf (IsWindow(hwnd)) Then
'            retVal = "Window"
'        Else
'            retVal = "WinObject"
'        End If
'        Return retVal
'    End Function

'    'Public Sub GetClassInfo(ByVal hwnd As IntPtr)
'    '    Dim WC As WndClassEX = Nothing
'    '    WC.cbSize = Len(WC)
'    '    Dim str As String = ""
'    '    WinAPI.API.GetClassInfoEx(hwnd, str, WC)
'    '    str = WC.lpszClassName
'    '    If (str.IndexOf(".") <> -1) Then
'    '        'must be .net non-sense...
'    '        If (str.ToLowerInvariant.IndexOf(".listbox.") <> -1) Then
'    '            str = "LISTBOX"
'    '        Else
'    '            If (str.ToLowerInvariant.IndexOf(".button.") <> -1) Then
'    '                str = "BUTTON"
'    '            Else
'    '                If (str.ToLowerInvariant.IndexOf(".static.") <> -1) Then
'    '                    str = "STATIC"
'    '                Else
'    '                    If (str.ToLowerInvariant.IndexOf(".systabcontrol32.") <> -1) Then
'    '                        str = "SYSTABCONTROL32"
'    '                    Else
'    '                        If (str.ToLowerInvariant.IndexOf(".tooltips_class32.") <> -1) Then
'    '                            str = "TOOLTIPS_CLASS32"
'    '                        End If
'    '                    End If
'    '                End If
'    '            End If
'    '        End If
'    '    End If
'    'End Sub

'    '/////////
'    Public Function SearchForApp(ByVal AppTitle As String, ByVal AppClassName As String) As IntPtr
'        If (AppTitle = "") Then
'            AppTitle = vbNullString
'        End If
'        If (AppClassName = "") Then
'            AppClassName = vbNullString
'        End If
'        Return FindWindow(AppClassName, AppTitle)
'    End Function

'    Public Function SearchForObjInApp(ByVal AppTitle As String, ByVal hwnd As IntPtr, ByVal ClassName As String) As IntPtr
'        Dim Value As String = ""
'        Dim TitleHwnd As IntPtr = FindWindow(vbNullString, AppTitle)
'        Dim TmpHwnd As IntPtr = 0
'        If (TitleHwnd = 0) Then
'            Return 0
'        End If
'        If (ClassName.Contains("::") = True) Then
'            Value = ClassName.Split("::").GetValue(2)
'            ClassName = ClassName.Split("::").GetValue(0)
'        End If
'        If (hwnd = TitleHwnd) Then
'            TmpHwnd = FindWindowEx(hwnd, IntPtr.Zero, ClassName, Value)
'        Else
'            TmpHwnd = FindWindowEx(hwnd, TitleHwnd, ClassName, Value)
'        End If
'        Return TmpHwnd
'    End Function

'    Public Function SearchForObjInApp(ByVal AppTitle As String, ByVal ClassName As String, ByVal Value As String) As IntPtr
'        Dim TitleHwnd As IntPtr = FindWindow(vbNullString, AppTitle)
'        If (TitleHwnd = 0) Then
'            Return 0
'        End If
'        Return FindWindowEx(TitleHwnd, IntPtr.Zero, ClassName, Value)
'    End Function

'    Public Function SearchForObjInApp(ByVal FirstPointer As IntPtr, ByVal ClassName As String, ByVal Value As String) As IntPtr
'        Return SearchForObjInApp(FirstPointer, IntPtr.Zero, ClassName, Value)
'    End Function

'    Public Function SearchForObjInApp(ByVal FirstPointer As IntPtr, ByVal NextPointer As IntPtr, ByVal ClassName As String, ByVal Value As String) As IntPtr
'        Return FindWindowEx(FirstPointer, NextPointer, ClassName, Value)
'    End Function

'    Public Function GetClassName(ByVal hWnd As IntPtr) As String
'        Try
'            If (UseDotNet = False) Then Return WindowsFunctions.GetClassNameNoDotNet(hWnd)
'            Dim str As String = WindowsFunctions.GetClassNameNoDotNet(hWnd)
'            If (str.ToLowerInvariant.IndexOf("windowsform") = -1) Then Return str
'            Return GetDotNetClassName(hWnd)
'        Catch ex As Exception
'            Return ""
'        End Try
'    End Function

'    Public Function GetDotNetClassName(ByVal hWnd As IntPtr) As String
'        Return APIControls.DotNetNames.GetWinFormsId(hWnd)
'    End Function

'    Public Function GetControlName(ByVal hwnd As IntPtr) As String
'        If (MessageID = 0) Then
'            MessageID = WinAPI.API.RegisterWindowMessage("WM_GETCONTROLNAME")
'        End If
'        Dim bytearray As String = Space(65535)
'        Dim size As Integer = 65535
'        'Dim s As System.Text.StringBuilder = New System.Text.StringBuilder()
'        's.Capacity = 65535
'        Dim retLen As Integer = WinAPI.NativeFunctions.SendMessage(hwnd, MessageID, size, bytearray)
'        Return retLen.ToString()
'        'Return s.ToString()
'    End Function
'End Class




'Friend Class EnumerateWindows
'    Public Shared isRunningThreaded As Boolean = False
'    Private Shared sPatternName As String
'    Private Shared sPatternValue As String
'    Private Shared hFind As IntPtr
'    Private Shared WindowHandles As New ArrayList
'    Private Shared ChildHandles As New ArrayList
'    Private Shared ParentHandle As IntPtr = 0
'    Private Shared WindowsFunctions As New APIControls.IndependentWindowsFunctionsv1()
'    Private Shared Windowsv1 As New Windowsv1()

'    Private Shared Function EnumWinProc(ByVal hwnd As IntPtr, ByVal lParam As Integer) As Boolean
'        'If IsWindowVisible(hwnd) And GetParent(hwnd) = 0 Then
'        If GetParent(hwnd) = 0 Then
'            If (sPatternValue <> "" Or sPatternName <> "") Then
'                Dim sName As String = GetTitle(hwnd)
'                Dim sValue As String = WindowsFunctions.GetText(hwnd)
'                'If (sName <> "") Then Console.WriteLine("title: " & sName)
'                If lParam = 0 Then
'                    sName = sName.ToUpper()
'                    sValue = sValue.ToUpper()
'                End If
'                If sName Like sPatternName Then
'                    If sValue Like sPatternValue Then
'                        'Console.WriteLine("found: " & sName)
'                        hFind = hwnd
'                        Return False
'                    End If
'                End If
'            End If
'        Else
'            Return False
'        End If
'        Return True
'    End Function

'    '===========================================================
'    ' Name: Public Function FindWindowWild
'    ' Input: 
'    '   ByVal  sWildName As String
'    '   ByVal   sWildValue As String
'    '   ByVal  Optional  bMatchCase As Boolean = True
'    ' Output:  As IntPtr
'    ' Purpose:
'    ' Remarks:
'    '   Example Code: 
'    '===========================================================
'    Public Shared Function FindWindowWild(ByVal sWildName As String, ByVal sWildValue As String, Optional ByVal bMatchCase As Boolean = True) As IntPtr
'        sPatternName = sWildName
'        sPatternValue = sPatternName
'        hFind = IntPtr.Zero
'        If Not bMatchCase Then
'            sPatternName = sPatternName.ToUpper()
'            sPatternValue = sPatternValue.ToUpper()
'        End If
'        isRunningThreaded = True
'        WinAPI.NativeFunctions.EnumWindows(AddressOf EnumWinProc, bMatchCase)
'        isRunningThreaded = False
'        Return hFind
'    End Function

'    '===========================================================
'    ' Name: Public Function GetHandles
'    ' Input: [None]
'    ' Output:  As IntPtr()
'    ' Purpose:
'    ' Remarks:
'    '   Example Code: 
'    '===========================================================
'    Public Shared Function GetHandles() As IntPtr()
'        ParentHandle = IntPtr.Zero
'        isRunningThreaded = True
'        WinAPI.NativeFunctions.EnumWindows(AddressOf EnumWindowsProc, Nothing)
'        Dim winHandles(WindowHandles.Count - 1) As IntPtr
'        WindowHandles.CopyTo(winHandles)
'        WindowHandles.Clear()
'        isRunningThreaded = False
'        Return winHandles
'    End Function


'    Private Shared Function EnumWindowsProc(ByVal handle As IntPtr, ByVal parameter As Integer) As Boolean
'        If (ParentHandle.Equals(IntPtr.Zero)) Then
'            WindowHandles.Add(handle)
'        Else
'            Dim tmp As IntPtr = WindowsFunctions.getParent(handle)
'            If (tmp.Equals(IntPtr.Zero) = False) Then Console.WriteLine("Handle: " & handle.ToString & ", " & Windowsv1.GetClassName(handle) & ", " & WindowsFunctions.GetText(handle))
'            While (tmp.Equals(IntPtr.Zero) = False)
'                If (ParentHandle.Equals(tmp)) Then
'                    ChildHandles.Add(handle)
'                    Return True
'                End If
'                tmp = WindowsFunctions.getParent(tmp)
'            End While
'        End If
'        Return True
'    End Function

'    '===========================================================
'    ' Name: Public Function GetChildHandles
'    ' Input: 
'    '   ByVal  handle As IntPtr
'    ' Output:  As IntPtr()
'    ' Purpose:
'    ' Remarks:
'    '   Example Code: 
'    '===========================================================
'    Public Shared Function GetChildHandles(ByVal handle As IntPtr) As IntPtr()
'        'winAPI.NativeFunctions.EnumChildWindows(handle, AddressOf HandleChildCallback, Nothing)
'        ParentHandle = handle
'        isRunningThreaded = True
'        'winAPI.NativeFunctions.EnumWindows(AddressOf EnumWindowsProc, Nothing)
'        WinAPI.NativeFunctions.EnumChildWindows(handle, AddressOf HandleChildCallback, Nothing)
'        Dim cHandles(ChildHandles.Count - 1) As IntPtr
'        ChildHandles.CopyTo(cHandles)
'        ChildHandles.Clear()
'        ParentHandle = IntPtr.Zero
'        isRunningThreaded = False
'        Return cHandles
'    End Function

'    '===========================================================
'    ' Name: Public Function isChildDirectlyConnectedToParent
'    ' Input: 
'    '   ByVal  parent As IntPtr
'    '   ByVal   child As IntPtr
'    ' Output:  As Boolean
'    ' Purpose:
'    ' Remarks:
'    '   Example Code: 
'    '===========================================================
'    Public Shared Function isChildDirectlyConnectedToParent(ByVal parent As IntPtr, ByVal child As IntPtr) As Boolean
'        Dim tmpParents() As IntPtr = GetChildHandles(parent)
'        For Each item As IntPtr In tmpParents
'            If (item.Equals(child)) Then Return True
'        Next
'        Return False
'    End Function

'    '===========================================================
'    ' Name: Public Sub Show
'    ' Input: 
'    '   ByVal  title As String
'    ' Output: 
'    ' Purpose:
'    ' Remarks:
'    '   Example Code: 
'    '===========================================================
'    Public Shared Sub Show(ByVal title As String)
'        For Each handle As IntPtr In GetHandles()
'            If GetTitle(handle).Contains(title) Then
'                WinAPI.NativeFunctions.ShowWindow(handle, SW.RESTORE)
'            End If
'        Next
'    End Sub

'    '===========================================================
'    ' Name: Public Function GetTitle
'    ' Input: 
'    '   ByVal  hWnd As IntPtr
'    ' Output:  As String
'    ' Purpose:
'    ' Remarks:
'    '   Example Code: 
'    '===========================================================
'    Public Shared Function GetTitle(ByVal hWnd As IntPtr) As String
'        Return WindowsFunctions.GetText(hWnd)
'    End Function

'    '===========================================================
'    ' Name: Public Sub Hide
'    ' Input: 
'    '   ByVal  title As String
'    ' Output: 
'    ' Purpose:
'    ' Remarks:
'    '   Example Code: 
'    '===========================================================
'    Public Shared Sub Hide(ByVal title As String)
'        For Each handle As IntPtr In GetHandles()
'            If GetTitle(handle).Contains(title) Then
'                WinAPI.NativeFunctions.ShowWindow(handle, SW.HIDE)
'            End If
'        Next
'    End Sub

'    '===========================================================
'    ' Name: Public Function SearchChildHandlesWild
'    ' Input: 
'    '   ByVal  handle As IntPtr
'    '   ByVal   Name As String
'    '   ByVal   Value As String
'    ' Output:  As IntPtr
'    ' Purpose:
'    ' Remarks:
'    '   Example Code: 
'    '===========================================================
'    Public Shared Function SearchChildHandlesWild(ByVal handle As IntPtr, ByVal Name As String, ByVal Value As String) As IntPtr
'        If (handle = IntPtr.Zero) Then
'            Return IntPtr.Zero 'return zero if zero given
'        End If
'        Dim cls As String = ""
'        If (Value <> "") Then
'            Dim txt As String = ""
'            Dim results As Boolean
'            For Each childhandle As IntPtr In GetChildHandles(handle)
'                txt = WindowsFunctions.GetText(childhandle)
'                'If txt <> "" Then Console.WriteLine("txt = " & txt)
'                If txt <> "" AndAlso txt Like Value Then
'                    results = True
'                Else
'                    results = False
'                End If
'                If (results = True) Then
'                    cls = Windowsv1.GetClassName(childhandle)
'                    'If cls <> "" Then Console.WriteLine("cls = " & cls)
'                    If cls <> "" AndAlso cls Like Name Then
'                        Console.WriteLine("Found Item: " & "txt = " & txt & "cls = " & cls)
'                        Return childhandle
'                    End If
'                End If
'            Next
'        Else
'            For Each childhandle As IntPtr In GetChildHandles(handle)
'                cls = Windowsv1.GetClassName(childhandle)
'                If cls <> "" AndAlso cls Like Name Then
'                    Return childhandle
'                End If
'            Next
'        End If
'    End Function

'    '===========================================================
'    ' Name: Public Function SearchChildHandles
'    ' Input: 
'    '   ByVal  handle As IntPtr
'    '   ByVal   Name As String
'    '   ByVal   Value As String
'    ' Output:  As IntPtr
'    ' Purpose:
'    ' Remarks:
'    '   Example Code: 
'    '===========================================================
'    Public Shared Function SearchChildHandles(ByVal handle As IntPtr, ByVal Name As String, ByVal Value As String) As IntPtr
'        'System.Console.WriteLine("Now searching in SearchChildHandles for " + handle.ToString() + " - " + Name + " - " + Value)
'        If (handle = IntPtr.Zero) Then
'            Return IntPtr.Zero 'return zero if zero given
'        End If
'        Dim cls As String = ""
'        If (Value <> "") Then
'            'System.Console.WriteLine("Searching with values!")
'            Dim txt As String = ""
'            Dim results As Boolean
'            For Each childhandle As IntPtr In GetChildHandles(handle)
'                txt = WindowsFunctions.GetText(childhandle)
'                If txt <> "" Then Console.WriteLine("txt = " & txt)
'                If txt = Value Then
'                    results = True
'                Else
'                    results = False
'                End If
'                If (results = True) Then
'                    cls = Windowsv1.GetClassName(childhandle)
'                    If cls <> "" Then Console.WriteLine("cls = " & cls)
'                    If cls <> "" AndAlso cls = Name Then
'                        'If (IsWindowVisible(childhandle)) Then
'                        'Console.WriteLine("Found Item: " & "txt = " & txt & "cls = " & cls)
'                        Return childhandle
'                        'End If
'                    End If
'                End If
'            Next
'        Else
'            'System.Console.WriteLine("Searching withOUT values!")
'            For Each childhandle As IntPtr In GetChildHandles(handle)
'                cls = Windowsv1.GetClassName(childhandle)
'                If cls <> "" Then Console.WriteLine("cls = " & cls)
'                If cls = Name Then
'                    'If (IsWindowVisible(childhandle)) Then
'                    'Console.WriteLine("Found Item: cls = " & cls)
'                    Return childhandle
'                    'End If
'                End If
'            Next
'        End If
'        Return 0
'    End Function

'    Public Shared Function Exists(ByVal title As String, ByVal text As String) As Boolean
'        Dim handle, childhandle As IntPtr
'        Dim currentTitle, currentChildTitle As String
'        For Each handle In GetHandles()
'            currentTitle = GetTitle(handle)
'            If currentTitle <> "" AndAlso currentTitle.Contains(title) Then
'                For Each childhandle In GetChildHandles(handle)
'                    currentChildTitle = GetChildText(childhandle)
'                    If currentChildTitle <> "" AndAlso currentChildTitle.Contains(text) Then
'                        Return True
'                    End If
'                Next
'            End If
'        Next
'    End Function

'    '===========================================================
'    ' Name: Public Function GetHandle
'    ' Input: 
'    '   ByVal  title As String
'    ' Output:  As IntPtr
'    ' Purpose:
'    ' Remarks:
'    '   Example Code: 
'    '===========================================================
'    Public Shared Function GetHandle(ByVal title As String) As IntPtr
'        For Each handle As IntPtr In GetHandles()
'            If GetTitle(handle).Contains(title) Then
'                Return handle
'            End If
'        Next
'    End Function


'    '===========================================================
'    ' Name: Public Sub Close
'    ' Input: 
'    '   ByVal  title As String
'    ' Output: 
'    ' Purpose:
'    ' Remarks:
'    '   Example Code: 
'    '===========================================================
'    Public Shared Sub Close(ByVal title As String)
'        Dim handle As IntPtr
'        Dim currentTitle As String

'        For Each handle In GetHandles()
'            currentTitle = GetTitle(handle)
'            If currentTitle <> "" AndAlso currentTitle.Contains(title) Then
'                WinAPI.NativeFunctions.SendMessage(handle, WM.CLOSE, IntPtr.Zero, IntPtr.Zero)
'            End If
'        Next
'    End Sub

'    Private Shared Function HandleChildCallback(ByVal handle As IntPtr, ByVal parameter As Integer) As Boolean
'        ChildHandles.Add(handle)
'        Return True
'    End Function

'    '===========================================================
'    ' Name: Public Function GetChildText
'    ' Input: 
'    '   ByVal  handle As IntPtr
'    ' Output:  As String
'    ' Purpose:
'    ' Remarks:
'    '   Example Code: 
'    '===========================================================
'    Public Shared Function GetChildText(ByVal handle As IntPtr) As String
'        Dim ip As IntPtr = Marshal.AllocHGlobal(1000)
'        WinAPI.NativeFunctions.SendMessage(handle, WM.GETTEXT, New IntPtr(1000), ip)
'        Dim ret As String = Marshal.PtrToStringAuto(ip)
'        Marshal.FreeHGlobal(ip)
'        Return ret
'    End Function

'End Class





'Public Class Windows
'#Const isAbs = 2 'Set to 1 if you want to use absolute values
'    Private Shared HandlesList() As IntPtr
'    Private Shared MessageID As Integer = 0
'    Private Shared possibleHandles As System.Collections.Generic.List(Of IntPtr)
'    Public Shared UseDotNet As Boolean = False
'    Protected Friend Shared HandleCount As Integer = 0
'    Private Shared WindowsFunctions As New InterAction.IndependentWindowsFunctionsv1()


'    Private Shared Function BuildList(ByVal TopWindowhwnd As IntPtr, ByVal hwnd As IntPtr) As IntPtr()
'        If (hwnd = 0) Then
'            '    'elminates all possible non-windows right off the bat.
'            '    'turns out sometimes windows appear invisible even when they shouldn't.
'            '    'Win32Window.IsWindowVisible(handle) And
'            For Each handle As IntPtr In EnumerateWindows.GetHandles()
'                possibleHandles.Add(handle)
'            Next
'            HandlesList = possibleHandles.ToArray()
'            possibleHandles.Clear()
'        Else
'            For Each handle As IntPtr In EnumerateWindows.GetChildHandles(TopWindowhwnd)
'                possibleHandles.Add(handle)
'            Next
'            HandlesList = possibleHandles.ToArray()
'            possibleHandles.Clear()
'        End If
'        'If (hwnd <> 0 And isAParent(hwnd, TopWindowhwnd) = True) Then
'        'handlesList = Win32Window.GetChildHandles(TopWindowhwnd)
'        'For Each handle As IntPtr In Win32Window.GetHandles()
'        ' If getParent(handle) = 0 Then
'        'possibleHandles.Add(handle)
'        ' End If
'        'Next
'        'handlesList = possibleHandles.ToArray()
'        'possibleHandles.Clear()
'        'Else
'        ' End If
'        'Return handlesList
'        Return HandlesList
'    End Function


'    Public Shared Sub SetText(ByVal text As String, ByVal hwnd As IntPtr)
'        WinAPI.API.SendMessageStr(hwnd, WM_SETTEXT, 0&, text)
'    End Sub

'    ''' <summary>
'    ''' 
'    ''' </summary>
'    ''' <param name="text"></param>
'    ''' <param name="hwnd"></param>
'    ''' <param name="AppHwnd"></param>
'    ''' <remarks></remarks>
'    Public Shared Sub SendTextByHandler(ByVal text As String, ByVal hwnd As IntPtr, ByVal AppHwnd As IntPtr)
'        WindowsFunctions.SetToForeGround(AppHwnd)
'        WinAPI.API.SetFocus(hwnd)
'        Keyboard.SendKeys(text)
'    End Sub

'    ''' <summary>
'    ''' 
'    ''' </summary>
'    ''' <param name="text"></param>
'    ''' <param name="hwnd"></param>
'    ''' <remarks></remarks>
'    Public Shared Sub AppendText(ByVal text As String, ByVal hwnd As IntPtr)
'        Dim SelectionStart As Integer, SelectionEnd As Integer
'        WinAPI.API.SendMessageByRef(hwnd, EM_GETSEL, SelectionStart, SelectionEnd)
'        WinAPI.API.SendMessageStr(hwnd, EM_REPLACESEL, False, text)
'    End Sub

'    ''' <summary>
'    ''' Builds a description from any point in the list of hwnds.
'    ''' </summary>
'    ''' <param name="TopWindowhwnd">The actual window or parent object</param>
'    ''' <param name="hwnd">The object you wish to create the description for.  
'    ''' Pass the parent object in agian, if you want that for the description
'    ''' created.</param>
'    ''' <returns>returns the description created.</returns>
'    ''' <remarks></remarks>
'    Public Shared Function CreateDescription(ByVal TopWindowhwnd As IntPtr, ByVal hwnd As IntPtr) As Description
'        possibleHandles = New System.Collections.Generic.List(Of IntPtr)
'        Console.WriteLine("Process: TopWindow Hwnd = " & TopWindowhwnd.ToString() & _
'                    "hwnd = " & hwnd.ToString())
'        HandlesList = BuildList(TopWindowhwnd, hwnd)

'        If (hwnd = 0) Then
'            hwnd = TopWindowhwnd
'        Else
'            Console.WriteLine("hwnd= " & _
'            hwnd.ToString() & " TopWindowhwnd= " & TopWindowhwnd.ToString() & _
'            " getParent(hwnd) = " & getParent(hwnd).ToString())
'        End If

'        'System.Console.WriteLine("handlesList size = " & HandlesList.Length.ToString)


'        Dim desc As Description = New Description()
'        Dim retDesc As Description = New Description()
'        Dim type As String = ""

'        '//////////////////////ClassName//////////////////////
'        type = "name"
'        desc.Add(type, GetClassName(hwnd))
'        Dim str As String
'        Dim test As Boolean = False
'        For Each handle As IntPtr In HandlesList
'            If (handle.Equals(hwnd)) Then
'                test = True
'            End If
'        Next
'        Console.WriteLine("********** TEST: " & test)
'        For Each handle As IntPtr In HandlesList
'            str = GetClassName(handle)
'            If (str.Equals(desc.Name)) Then
'                possibleHandles.Add(handle)
'            End If
'        Next

'        If (possibleHandles.Count >= 1) Then
'            retDesc.Add(type, GetClassName(hwnd))
'            If (possibleHandles.Count = 1) Then
'                Return retDesc
'            End If
'            HandlesList = possibleHandles.ToArray()
'            possibleHandles.Clear()
'        Else
'            desc = retDesc 'set back to old good state.
'        End If

'        '//////////////////////Value//////////////////////
'        type = "value"
'        desc.Add(type, WindowsFunctions.GetText(hwnd))
'        For Each handle As IntPtr In HandlesList
'            If (WindowsFunctions.GetText(handle).Equals(desc.Value)) Then
'                possibleHandles.Add(handle)
'            End If
'        Next

'        If (possibleHandles.Count >= 1) Then
'            retDesc.Add(type, WindowsFunctions.GetText(hwnd))
'            If (possibleHandles.Count = 1) Then
'                'special case:  name/value is the most common
'                'type of recording so, I would rather take in less data rather than
'                'more.  If name/value did it, check if just value could do it.
'                'slower, but better.
'                possibleHandles.Clear()
'                If (hwnd = 0) Then
'                    hwnd = TopWindowhwnd
'                End If
'                HandlesList = BuildList(TopWindowhwnd, hwnd)
'                For Each handle As IntPtr In HandlesList
'                    If (WindowsFunctions.GetText(handle).Equals(desc.Value)) Then
'                        possibleHandles.Add(handle)
'                    End If
'                Next
'                If (possibleHandles.Count = 1) Then
'                    If (possibleHandles(0) = hwnd) Then
'                        'it's possible it picked up a 
'                        '"empty text" or something silly
'                        'like that...

'                        'great, text only worked :D
'                        retDesc = New Description()
'                        retDesc.Add(type, WindowsFunctions.GetText(hwnd))
'                        'else text alone did not work
'                    End If
'                End If
'                Return retDesc
'            End If
'            HandlesList = possibleHandles.ToArray()
'            possibleHandles.Clear()
'        Else
'            desc = retDesc 'set back to old good state.
'        End If
'        '///////////////////////Width/////////////////////////
'        type = "width"
'        desc.Add(type, WindowsFunctions.GetLocation(hwnd).Width.ToString())
'        For Each handle As IntPtr In HandlesList
'            If (WindowsFunctions.GetLocation(handle).Width.Equals(desc.Width)) Then
'                possibleHandles.Add(handle)
'            End If
'        Next

'        If (possibleHandles.Count >= 1) Then
'#If isAbs = 1 Then
'            retDesc.Add(type, Mouse.RelativeToAbsCoordX(IndependentWindowsFunctions.GetLocation(hwnd).Width))
'#Else
'            retDesc.Add(type, WindowsFunctions.GetLocation(hwnd).Width.ToString())
'#End If
'            If (possibleHandles.Count = 1) Then
'                Return retDesc
'            End If
'            HandlesList = possibleHandles.ToArray()
'            possibleHandles.Clear()
'        Else
'            desc = retDesc 'set back to old good state.
'        End If

'        '///////////////////////Height////////////////////////
'        type = "height"
'        desc.Add(type, WindowsFunctions.GetLocation(hwnd).Height.ToString())
'        For Each handle As IntPtr In HandlesList
'            If (WindowsFunctions.GetLocation(handle).Height.Equals(desc.Height)) Then
'                possibleHandles.Add(handle)
'            End If
'        Next

'        If (possibleHandles.Count >= 1) Then
'#If isAbs = 1 Then
'            retDesc.Add(type, Mouse.RelativeToAbsCoordY(IndependentWindowsFunctions.GetLocation(hwnd).Height))
'#Else
'            retDesc.Add(type, WindowsFunctions.GetLocation(hwnd).Height.ToString())
'#End If
'            If (possibleHandles.Count = 1) Then
'                Return retDesc
'            End If
'            HandlesList = possibleHandles.ToArray()
'            possibleHandles.Clear()
'        Else
'            desc = retDesc 'set back to old good state.
'        End If

'        '///////////////////////Top////////////////////////
'        type = "top"
'        Dim rec As System.Drawing.Rectangle = WindowsFunctions.GetLocation(hwnd)
'        desc.Add(type, rec.Top.ToString())
'        For Each handle As IntPtr In HandlesList
'            If (WindowsFunctions.GetLocation(handle).Top.Equals(rec.Top)) Then
'                possibleHandles.Add(handle)
'            End If
'        Next

'        If (possibleHandles.Count >= 1) Then
'#If isAbs = 1 Then
'            retDesc.Add(type, Mouse.RelativeToAbsCoordY(rec.Top))
'#Else
'            retDesc.Add(type, rec.Top.ToString())
'#End If

'            If (possibleHandles.Count = 1) Then
'                Return retDesc
'            End If
'            HandlesList = possibleHandles.ToArray()
'            possibleHandles.Clear()
'        Else
'            desc = retDesc 'set back to old good state.
'        End If

'        '///////////////////////left////////////////////////
'        type = "left"
'        desc.Add(type, rec.Left.ToString())
'        For Each handle As IntPtr In HandlesList
'            If (WindowsFunctions.GetLocation(handle).Left.Equals(rec.Left)) Then
'                possibleHandles.Add(handle)
'            End If
'        Next

'        If (possibleHandles.Count >= 1) Then
'#If isAbs = 1 Then
'            retDesc.Add(type, Mouse.RelativeToAbsCoordX(rec.Left))
'#Else
'            retDesc.Add(type, rec.Left.ToString())
'#End If
'            If (possibleHandles.Count = 1) Then
'                Return retDesc
'            End If
'            HandlesList = possibleHandles.ToArray()
'            possibleHandles.Clear()
'        Else
'            desc = retDesc 'set back to old good state.
'        End If

'        '///////////////////////right////////////////////////
'        type = "right"
'        desc.Add(type, rec.Right.ToString())
'        For Each handle As IntPtr In HandlesList
'            If (GetClassName(handle).Equals(rec.Right)) Then
'                possibleHandles.Add(handle)
'            End If
'        Next

'        If (possibleHandles.Count >= 1) Then
'#If isAbs = 1 Then
'            retDesc.Add(type, Mouse.RelativeToAbsCoordX(rec.Right))
'#Else
'            retDesc.Add(type, rec.Right.ToString())
'#End If
'            If (possibleHandles.Count = 1) Then
'                Return retDesc
'            End If
'            HandlesList = possibleHandles.ToArray()
'            possibleHandles.Clear()
'        Else
'            desc = retDesc 'set back to old good state.
'        End If

'        '///////////////////////bottom////////////////////////
'        type = "bottom"
'        desc.Add(type, rec.Bottom.ToString())
'        For Each handle As IntPtr In HandlesList

'            If (WindowsFunctions.GetLocation(handle).Bottom.Equals(desc.Bottom)) Then
'                possibleHandles.Add(handle)
'            End If
'        Next

'        If (possibleHandles.Count >= 1) Then
'#If isAbs = 1 Then
'            retDesc.Add(type, Mouse.RelativeToAbsCoordY(rec.Bottom))
'#Else
'            retDesc.Add(type, rec.Bottom.ToString())
'#End If
'            If (possibleHandles.Count = 1) Then
'                Return retDesc
'            End If
'            HandlesList = possibleHandles.ToArray()
'            possibleHandles.Clear()
'        Else
'            desc = retDesc 'set back to old good state.
'        End If
'        '///////////////////////isWindow////////////////////////
'        type = "iswindow"
'        desc.Add(type, IsWindow(hwnd).ToString())

'        For Each handle As IntPtr In HandlesList
'            If (IsWindow(handle)) Then
'                possibleHandles.Add(handle)
'            End If
'        Next

'        If (possibleHandles.Count >= 1) Then
'            retDesc.Add(type, IsWindow(hwnd).ToString())
'            If (possibleHandles.Count = 1) Then
'                Return retDesc
'            End If
'            HandlesList = possibleHandles.ToArray()
'            possibleHandles.Clear()
'        Else
'            desc = retDesc 'set back to old good state.
'        End If
'        '///////////////////////isWindow////////////////////////
'        type = "islistbox"
'        desc.Add(type, IsListBox(hwnd).ToString())

'        For Each handle As IntPtr In HandlesList
'            If (IsListBox(handle)) Then
'                possibleHandles.Add(handle)
'            End If
'        Next

'        If (possibleHandles.Count >= 1) Then
'            retDesc.Add(type, IsListBox(hwnd).ToString())
'            If (possibleHandles.Count = 1) Then
'                Return retDesc
'            End If
'            HandlesList = possibleHandles.ToArray()
'            possibleHandles.Clear()
'        Else
'            desc = retDesc 'set back to old good state.
'        End If
'        '///////////////////////hwnd////////////////////////
'        type = "hwnd"
'        desc.Add(type, hwnd.ToString())
'        For Each handle As IntPtr In HandlesList
'            If (handle.ToString().Equals(desc.Hwnd)) Then
'                possibleHandles.Add(handle)
'            End If
'        Next

'        If (possibleHandles.Count >= 1) Then
'            retDesc.Add(type, hwnd.ToString())
'            If (possibleHandles.Count = 1) Then
'                Return retDesc
'            End If
'            'Else
'            'desc = retDesc 'set back to old good state.
'        End If
'        'handlesList = possibleHandles.ToArray()
'        'possibleHandles.Clear()
'        Dim tmp As New Description()
'        tmp.Add(type, hwnd.ToString)
'        Return tmp
'        Throw New Exception("Could not generate a description for hwnd: " & hwnd.ToString())

'    End Function


'    '===========================================================
'    ' Name: Public Function SearchForObj
'    ' Input: 
'    '   ByVal  desc As APIControls.Description
'    '   ByVal   hwnd As IntPtr
'    ' Output:  As IntPtr
'    ' Purpose:
'    ' Remarks:
'    '   Example Code: 
'    '===========================================================
'    Public Shared Function SearchForObj(ByVal desc As APIControls.Description, ByVal hwnd As IntPtr) As IntPtr
'        Dim TmpHwnd As IntPtr = 0
'        possibleHandles = New System.Collections.Generic.List(Of IntPtr)
'        Dim tmpText As String = ""
'        If (desc.Count = 0) Then
'            Throw New Exception("No information provided in description")
'        End If
'        HandleCount = 1
'        If (hwnd <> IntPtr.Zero) Then
'            HandlesList = EnumerateWindows.GetChildHandles(hwnd)
'            If (desc.Contains(Description.DescriptionData.Hwnd) = True) Then 'handle search first, 
'                'since it's a special and boolean case.. if the handle is not
'                'found then the description is wrong.
'                For Each handle As IntPtr In HandlesList
'                    If (handle.Equals(desc.Hwnd)) Then
'                        Return handle
'                    End If
'                Next
'                HandleCount = 0
'                Return 0
'            End If
'        Else
'            'elminates all possible non-windows right off the bat.
'            'If (SearchVisibleOnly = True) Then
'            'search handles.
'            If (desc.Contains(Description.DescriptionData.Hwnd) = True) Then 'handle search first, 
'                'since it's a special and boolean case.. if the handle is not
'                'found then the description is wrong.
'                'System.Console.WriteLine(desc.Hwnd.ToString)
'                For Each handle As IntPtr In EnumerateWindows.GetHandles()

'                    If (handle.Equals(desc.Hwnd)) Then
'                        Return handle
'                    End If
'                    For Each subHandle As IntPtr In EnumerateWindows.GetChildHandles(handle) 'Sub handles are not included in handles list
'                        If (subHandle.Equals(desc.Hwnd)) Then
'                            Return subHandle
'                        End If
'                    Next
'                Next
'                HandleCount = 0
'                Return 0
'            Else

'                For Each handle As IntPtr In EnumerateWindows.GetHandles()
'                    'If Win32Window.IsWindowVisible(handle) And getParent(handle) = 0 Then
'                    'If getParent(handle) = 0 Then
'                    possibleHandles.Add(handle)
'                    ' End If
'                Next
'            End If
'            HandlesList = possibleHandles.ToArray()
'            possibleHandles.Clear()
'        End If



'        'search name if needed
'        If (desc.Contains(Description.DescriptionData.Name) = True) Then
'            For Each handle As IntPtr In HandlesList
'                tmpText = Windows.GetClassName(handle)
'                'If tmpText <> "" Then Console.WriteLine("cls = " & tmpText)
'                If (desc.WildCard = False) Then
'                    If tmpText = desc.Name Then
'                        possibleHandles.Add(handle)
'                        'Console.WriteLine("*** FOUND cls = " & tmpText)
'                    End If
'                Else
'                    If tmpText Like desc.Name Then
'                        possibleHandles.Add(handle)
'                    End If
'                End If
'            Next
'            HandlesList = possibleHandles.ToArray()
'            possibleHandles.Clear()
'        End If

'        If (HandlesList.Length = 0) Then
'            HandleCount = 0
'            Return 0
'        End If

'        If (desc.Contains(Description.DescriptionData.Value) = True) Then
'            For Each handle As IntPtr In HandlesList
'                tmpText = WindowsFunctions.GetText(handle)
'                'If tmpText <> "" Then Console.WriteLine("text = " & tmpText)
'                If (desc.WildCard = False) Then
'                    If tmpText = desc.Value Then
'                        possibleHandles.Add(handle)
'                        'Console.WriteLine("*** FOUND text = " & tmpText)
'                    End If
'                Else
'                    If tmpText Like desc.Value Then
'                        possibleHandles.Add(handle)
'                    End If
'                End If
'            Next
'            HandlesList = possibleHandles.ToArray()
'            possibleHandles.Clear()
'        End If

'        If (HandlesList.Length = 0) Then
'            HandleCount = 0
'            Return 0
'        End If
'        'seach location/size
'        Dim rectangle As System.Drawing.Rectangle = New System.Drawing.Rectangle()
'        If (desc.Location <> rectangle.Empty) Then
'            Dim results As Boolean = False
'            For Each handle As IntPtr In HandlesList
'                rectangle = WindowsFunctions.GetLocation(handle)
'                results = True

'                If (desc.Contains(Description.DescriptionData.Left) = True) Then
'                    If (rectangle.Left <> desc.Left) Then
'                        results = False
'                    End If
'                End If
'                If (desc.Contains(Description.DescriptionData.Right) = True) Then
'                    If (rectangle.Right <> desc.Right) Then
'                        results = False
'                    End If
'                End If
'                If (desc.Contains(Description.DescriptionData.Top) = True) Then
'                    If (rectangle.Top <> desc.Top) Then
'                        results = False
'                    End If
'                End If
'                If (desc.Contains(Description.DescriptionData.Bottom) = True) Then
'                    If (rectangle.Bottom <> desc.Bottom) Then
'                        results = False
'                    End If
'                End If
'                'size
'                If (desc.Contains(Description.DescriptionData.Width) = True) Then
'                    If (rectangle.Width <> desc.Width) Then
'                        results = False
'                    End If
'                End If
'                If (desc.Contains(Description.DescriptionData.Height) = True) Then
'                    If (rectangle.Height <> desc.Height) Then
'                        results = False
'                    End If
'                End If

'                If (results = True) Then
'                    possibleHandles.Add(handle)
'                End If
'            Next
'            HandlesList = possibleHandles.ToArray()
'            possibleHandles.Clear()
'        End If
'        If (HandlesList.Length = 0) Then
'            HandleCount = 0
'            Return 0
'        End If
'        'search isWindow
'        If (desc.Contains(Description.DescriptionData.IsWindow) = True) Then
'            For Each handle As IntPtr In HandlesList
'                If (IsWindow(handle)) Then
'                    possibleHandles.Add(handle)
'                End If
'            Next
'            HandlesList = possibleHandles.ToArray()
'            possibleHandles.Clear()
'        End If

'        If (HandlesList.Length = 0) Then
'            HandleCount = 0
'            Return 0
'        End If

'        'search islistbox
'        If (desc.Contains(Description.DescriptionData.IsListBox) = True) Then
'            For Each handle As IntPtr In HandlesList
'                If (IsListBox(handle)) Then
'                    possibleHandles.Add(handle)
'                End If
'            Next
'            HandlesList = possibleHandles.ToArray()
'            possibleHandles.Clear()
'        End If

'        '//////////// search completed.
'        'If (SearchVisibleOnly = True And handlesList.Length = 0) Then
'        '    SearchVisibleOnly = False
'        '    Dim handle As IntPtr = SearchForObj(desc, hwnd)
'        '    If (handle.Equals(IntPtr.Zero) = False) Then
'        '        possibleHandles.Add(handle)
'        '        handlesList = possibleHandles.ToArray()
'        '    End If
'        'End If
'        'SearchVisibleOnly = True
'        If (HandlesList.Length = 0) Then
'            HandleCount = 0
'            Return 0
'        Else
'            If (HandlesList.Length > 1) Then 'too many handles
'                HandleCount = HandlesList.Length
'                Throw New Exception("More than one item with the description " & desc.ToString() & " was found.")
'            Else
'                Return HandlesList(0)
'            End If
'        End If

'    End Function


'    '===========================================================
'    ' Name: Public Function isListBox
'    ' Input: 
'    '   ByVal  lhWnd As IntPtr
'    ' Output:  As Boolean
'    ' Purpose:
'    ' Remarks:
'    '   Example Code: 
'    '===========================================================
'    Public Shared Function IsListBox(ByVal lhWnd As IntPtr) As Boolean
'        Dim lCount As Long
'        lCount = WinAPI.NativeFunctions.SendMessage(lhWnd, LB_GETCOUNT, 0&, 0&)
'        If (lCount = LB_ERR) Then
'            Return False
'        ElseIf (lCount = 0) Then
'            If (WindowsFunctions.GetClassNameNoDotNet(lhWnd).ToLowerInvariant.IndexOf(".listbox.") <> -1) Then
'                Return True
'            End If
'            Return False
'        End If
'        Return True
'    End Function
'    '===========================================================
'    ' Name: Public Function GetListBoxItem
'    ' Input: 
'    '   ByVal  index As Integer
'    '   ByVal  hWnd As IntPtr
'    ' Output:  As String
'    ' Purpose:
'    ' Remarks:
'    '   Example Code: 
'    '===========================================================
'    Public Shared Function GetListBoxItem(ByVal index As Long, ByVal hWnd As IntPtr) As String
'        Dim sBuilder As System.Text.StringBuilder
'        sBuilder = New System.Text.StringBuilder()
'        Dim lLen As Integer = WinAPI.NativeFunctions.SendMessage(hWnd, LB_GETTEXTLEN, index, 0&)
'        sBuilder.Capacity = lLen + 1 '+1 for null char after string.  Not sure if this is needed.
'        'winAPI.NativeFunctions.SendMessage(hWnd, LB_GETTEXT, index, sBuilder)
'        WinAPI.API.SendMessage(hWnd, LB_GETTEXT, index, sBuilder)
'        Return sBuilder.ToString()
'    End Function

'    '===========================================================
'    ' Name: Public Sub getClassInfo
'    ' Input: 
'    '   ByVal  hwnd As IntPtr
'    ' Output: 
'    ' Purpose:
'    ' Remarks:
'    '   Example Code: 
'    '===========================================================
'    Public Shared Sub getClassInfo(ByVal hwnd As IntPtr)
'        Dim WC As WndClassEX = Nothing
'        WC.cbSize = Len(WC)
'        Dim str As String = ""
'        WinAPI.API.GetClassInfoEx(hwnd, str, WC)
'        str = WC.lpszClassName
'        If (str.IndexOf(".") <> -1) Then
'            'must be .net non-sense...
'            If (str.ToLowerInvariant.IndexOf(".listbox.") <> -1) Then
'                str = "LISTBOX"
'            Else
'                If (str.ToLowerInvariant.IndexOf(".button.") <> -1) Then
'                    str = "BUTTON"
'                Else
'                    If (str.ToLowerInvariant.IndexOf(".static.") <> -1) Then
'                        str = "STATIC"
'                    Else
'                        If (str.ToLowerInvariant.IndexOf(".systabcontrol32.") <> -1) Then
'                            str = "SYSTABCONTROL32"
'                        Else
'                            If (str.ToLowerInvariant.IndexOf(".tooltips_class32.") <> -1) Then
'                                str = "TOOLTIPS_CLASS32"
'                            End If
'                        End If
'                    End If
'                End If
'            End If
'        End If
'    End Sub

'    '===========================================================
'    ' Name: Public Function ListBoxItemsCount
'    ' Input: 
'    '   ByVal  lhWnd As IntPtr
'    ' Output:  As Long
'    ' Purpose:
'    ' Remarks:
'    '   Example Code: 
'    '===========================================================
'    Public Shared Function ListBoxItemsCount(ByVal lhWnd As IntPtr) As Long
'        Return WinAPI.NativeFunctions.SendMessage(lhWnd, LB_GETCOUNT, 0&, 0&)
'    End Function

'    '===========================================================
'    ' Name: Public Function GetListBoxSelectedItems
'    ' Input: 
'    '   ByVal  lhWnd As IntPtr
'    ' Output:  As String()
'    ' Purpose:
'    ' Remarks:
'    '   Example Code: 
'    '===========================================================
'    Public Shared Function GetListBoxSelectedItems(ByVal lhWnd As IntPtr) As String()
'        Dim iNumItems As Long = ListBoxItemsCount(lhWnd)
'        Dim sItems() As String = {""}
'        If iNumItems > 0 Then
'            ReDim sItems(iNumItems - 1)
'            WinAPI.NativeFunctions.SendMessage(lhWnd, LB_GETSELITEMS, iNumItems, _
'              sItems(0))
'        End If
'        Return sItems
'    End Function

'    '===========================================================
'    ' Name: Public Function GetListBoxSelectedItem
'    ' Input: 
'    '   ByVal  hwnd As IntPtr
'    ' Output:  As Integer
'    ' Purpose:
'    ' Remarks:
'    '   Example Code: 
'    '===========================================================
'    Public Shared Function GetListBoxSelectedItem(ByVal hwnd As IntPtr) As Integer
'        'if you have no selected items or you have a multi-select box then error out
'        If (WinAPI.NativeFunctions.SendMessage(hwnd, LB_GETSELCOUNT, 0, 0) <> 1) Then
'            Return WinAPI.NativeFunctions.SendMessage(hwnd, LB_GETCURSEL, 0, 0)
'        Else
'            Return -1
'        End If
'    End Function

'    '===========================================================
'    ' Name: Public Function SelectListBoxItem
'    ' Input: 
'    '   ByVal  hwnd As IntPtr
'    '   ByVal   item As Integer
'    ' Output:  As Boolean
'    ' Purpose:
'    ' Remarks:
'    '   Example Code: 
'    '===========================================================
'    Public Shared Function SelectListBoxItem(ByVal hwnd As IntPtr, ByVal item As Integer) As Boolean
'        Dim i As Integer = ListBoxItemsCount(hwnd)
'        If (i >= item And item > 0) Then
'            WinAPI.NativeFunctions.SendMessage(hwnd, LB_SETCURSEL, item, 0)
'            Return True
'        Else
'            Return False
'        End If
'    End Function

'    '===========================================================
'    ' Name: Public Function GetListBoxItems
'    ' Input: 
'    '   ByVal  lhWnd As IntPtr
'    ' Output:  As String()
'    ' Purpose:
'    ' Remarks:
'    '   Example Code: 
'    '===========================================================
'    Public Shared Function GetListBoxItems(ByVal lhWnd As IntPtr) As String()
'        Dim lCount As Long, lLen As Long, N As Long
'        Dim sItems() As String = {""}

'        lCount = ListBoxItemsCount(lhWnd)
'        If (lCount = LB_ERR) Or (lCount = 0) Then
'            Return sItems
'        End If
'        ReDim sItems(lCount - 1)
'        Dim sBuilder As System.Text.StringBuilder
'        For N = 0 To lCount - 1
'            sBuilder = New System.Text.StringBuilder()
'            lLen = WinAPI.NativeFunctions.SendMessage(lhWnd, LB_GETTEXTLEN, N, 0&)
'            sBuilder.Capacity = lLen + 1
'            WinAPI.API.SendMessage(lhWnd, LB_GETTEXT, N, sBuilder)
'            'winAPI.NativeFunctions.SendMessage(lhWnd, LB_GETTEXT, N, sBuilder)
'            sItems(N) = sBuilder.ToString()
'        Next N
'        GetListBoxItems = sItems
'    End Function

'    '===========================================================
'    ' Name: Public Function SearchForApp
'    ' Input: 
'    '   ByVal  AppTitle As String
'    '   ByVal   AppClassName As String
'    ' Output:  As IntPtr
'    ' Purpose:
'    ' Remarks:
'    '   Example Code: 
'    '===========================================================
'    Public Shared Function SearchForApp(ByVal AppTitle As String, ByVal AppClassName As String) As IntPtr
'        If (AppTitle = "") Then
'            AppTitle = vbNullString
'        End If
'        If (AppClassName = "") Then
'            AppClassName = vbNullString
'        End If
'        Return FindWindow(AppClassName, AppTitle)
'    End Function

'    '===========================================================
'    ' Name: Public Function SearchForObjInApp
'    ' Input: 
'    '   ByVal  AppTitle As String
'    '   ByVal   hwnd As IntPtr
'    '   ByVal   ClassName As String
'    ' Output:  As IntPtr
'    ' Purpose:
'    ' Remarks:
'    '   Example Code: 
'    '===========================================================
'    Public Shared Function SearchForObjInApp(ByVal AppTitle As String, ByVal hwnd As IntPtr, ByVal ClassName As String) As IntPtr
'        Dim Value As String = ""
'        Dim TitleHwnd As IntPtr = FindWindow(vbNullString, AppTitle)
'        Dim TmpHwnd As IntPtr = 0
'        If (TitleHwnd = 0) Then
'            Return 0
'        End If
'        If (ClassName.Contains("::") = True) Then
'            Value = ClassName.Split("::").GetValue(2)
'            ClassName = ClassName.Split("::").GetValue(0)
'        End If
'        If (hwnd = TitleHwnd) Then
'            TmpHwnd = FindWindowEx(hwnd, IntPtr.Zero, ClassName, Value)
'        Else
'            TmpHwnd = FindWindowEx(hwnd, TitleHwnd, ClassName, Value)
'        End If
'        Return TmpHwnd
'    End Function

'    '===========================================================
'    ' Name: Public Function SearchForObjInApp
'    ' Input: 
'    '   ByVal  AppTitle As String
'    '   ByVal   ClassName As String
'    '   ByVal   Value As String
'    ' Output:  As IntPtr
'    ' Purpose:
'    ' Remarks:
'    '   Example Code: 
'    '===========================================================
'    Public Shared Function SearchForObjInApp(ByVal AppTitle As String, ByVal ClassName As String, ByVal Value As String) As IntPtr
'        Dim TitleHwnd As IntPtr = FindWindow(vbNullString, AppTitle)
'        If (TitleHwnd = 0) Then
'            Return 0
'        End If
'        Return FindWindowEx(TitleHwnd, IntPtr.Zero, ClassName, Value)
'    End Function

'    '===========================================================
'    ' Name: Public Function SearchForObjInApp
'    ' Input: 
'    '   ByVal  FirstPointer As IntPtr
'    '   ByVal   ClassName As String
'    '   ByVal   Value As String
'    ' Output:  As IntPtr
'    ' Purpose:
'    ' Remarks:
'    '   Example Code: 
'    '===========================================================
'    Public Shared Function SearchForObjInApp(ByVal FirstPointer As IntPtr, ByVal ClassName As String, ByVal Value As String) As IntPtr
'        Return SearchForObjInApp(FirstPointer, IntPtr.Zero, ClassName, Value)
'    End Function

'    '===========================================================
'    ' Name: Public Function SearchForObjInApp
'    ' Input: 
'    '   ByVal  FirstPointer As IntPtr
'    '   ByVal   NextPointer As IntPtr
'    '   ByVal   ClassName As String
'    '   ByVal   Value As String
'    ' Output:  As IntPtr
'    ' Purpose:
'    ' Remarks:
'    '   Example Code: 
'    '===========================================================
'    Public Shared Function SearchForObjInApp(ByVal FirstPointer As IntPtr, ByVal NextPointer As IntPtr, ByVal ClassName As String, ByVal Value As String) As IntPtr
'        Return FindWindowEx(FirstPointer, NextPointer, ClassName, Value)
'    End Function

'    '===========================================================
'    ' Name: Public Function GetClassName
'    ' Input: 
'    '   ByVal  hWnd As IntPtr
'    ' Output:  As String
'    ' Purpose:
'    ' Remarks:
'    '   Example Code: 
'    '===========================================================
'    Public Shared Function GetClassName(ByVal hWnd As IntPtr) As String
'        Try
'            If (UseDotNet = False) Then Return WindowsFunctions.GetClassNameNoDotNet(hWnd)
'            Dim str As String = WindowsFunctions.GetClassNameNoDotNet(hWnd)
'            If (str.ToLowerInvariant.IndexOf("windowsform") = -1) Then Return str
'            Return GetDotNetClassName(hWnd)
'        Catch ex As Exception
'            Return ""
'        End Try
'    End Function


'    '===========================================================
'    ' Name: Public Function GetDotNetClassName
'    ' Input: 
'    '   ByVal  hWnd As IntPtr
'    ' Output:  As String
'    ' Purpose:
'    ' Remarks:
'    '   Example Code: 
'    '===========================================================
'    Public Shared Function GetDotNetClassName(ByVal hWnd As IntPtr) As String
'        Return DotNetNames.GetWinFormsId(hWnd)
'    End Function


'    '===========================================================
'    ' Name: Public Function GetControlName
'    ' Input: 
'    '   ByVal  hwnd As IntPtr
'    ' Output:  As String
'    ' Purpose:
'    ' Remarks:
'    '   Example Code: 
'    '===========================================================
'    Public Shared Function GetControlName(ByVal hwnd As IntPtr) As String
'        If (MessageID = 0) Then
'            MessageID = WinAPI.API.RegisterWindowMessage("WM_GETCONTROLNAME")
'        End If
'        Dim bytearray As String = Space(65535)
'        Dim size As Integer = 65535
'        'Dim s As System.Text.StringBuilder = New System.Text.StringBuilder()
'        's.Capacity = 65535
'        Dim retLen As Integer = WinAPI.NativeFunctions.SendMessage(hwnd, MessageID, size, bytearray)
'        Return retLen.ToString()
'        'Return s.ToString()
'    End Function
'End Class
