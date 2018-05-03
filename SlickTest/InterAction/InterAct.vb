'Slick Test Developer, Copyright (c) 2007-2010 Jeremy Carey-dressler
#Region "TODO"

'************************TODO:
'TreeView (somewhat done)
'ListView (somewhat done)
'ToolBar
'Tab (somewhat done)
'StatusBar
'Spinner
'Scroll
'Radio Button (somewhat done)
'Menu (somewhat done)
'Check Box (done)
'Calander (maybe)
'Datagrid (.net; maybe)
'Editor/Edit (Encrypted text for text boxes... is multi-line... etc)
'SWF = System.windows.forms

'Suggested feature set taken from pyautowin, a GNU python library:
'http://svn.openqa.org/fisheye/browse/pywinauto/version_0.1.1/controlactions.py?r=13&%252540annotateMode=age

#Region "No Review Required"
'#####Static
'^^DONE:^^
'#GetText
'<Not directly handled, but indirectly solved>
'#VerifyValue

'^^NOT DONE:^^
'^^NOT REVIEWED:^^
'#####CHECKBOX
'^^DONE:^^
'#Check
'#Uncheck
'#GetState
'#SetState
'#Toggle
'#VerifyValue
'#IsChecked
'^^NOT DONE:^^
'^^NOT REVIEWED:^^
'#Properties
'^^DONE:^^
'#bChecked
'#bValue
'^^NOT DONE:^^
'^^NOT REVIEWED:^^
'#
'#
#End Region
'######ANYWIN
'^^DONE:^^
'#GetHandle
'#Click
'#DoubleClick
'#GetChildren
'#GetName
'#GetClass
'#GetRect
'#PressKeys
'#PressMouse
'#ReleaseKeys
'#ReleaseMouse
'#MoveMouse 
'#TypeKeys
'#Exists
'#IsEnabled
'#CaptureBitmap
'#GetNativeClass

'<Not directly handled, but indirectly solved>
'#GetProperty
'#GetArrayProperty
'#Properties
'#GetPropertyList
'#VerifyBitmap  (Ignore)
'#VerifyEnabled
'#VerifyEverything  (Ignore)
'#VerifyText 
'#VerifyProperties  (Ignore)
'#SetArrayProperty  (Ignore)
'#GetBitmapCRC  (Ignore)
'#GetContents  (Ignore)
'#GetEverything  (Ignore)
'#SetProperty
'#MenuSelect 
'#PopupSelect
'#MultiClick  (Ignore)
'#VerifyActive  (Ignore)
'#IsActive  (Ignore)
'#GetIDGetIndex (Control ID works, and get index workds, both together?)
'

'^^NOT DONE:^^
'#ScrollIntoView
'#GetParent  (Ignore)
'#GetInputLanguage
'#InvokeJava 
'#SetInputLanguage
'#IsVisible 
'#GetHelpText
'^^NOT REVIEWED:^^
'#GetAppId
'#GetCaption'Not sure what this does
'#ClearTrap
'#GenerateDecl
'#GetTag 
'#InvokeMethods 
'#IsArrayProperty
'#IsDefined
'#IsOfClass
'#SetTrap
'#WaitBitmap

'^^DONE:^^
'#bExists
'#sName
'#Rect
'#hWnd
'#bEnabled
'#lwChildren
'#Class
'#sID
'^^NOT DONE:^^
'^^NOT REVIEWED:^^
'#bActive
'#AppId
'#sCaption
'#iIndex
'#wParent
'#WndTag
'#
'#
'######CONTROL
'^^DONE:^^
'^^NOT DONE:^^
'^^NOT REVIEWED:^^
'#GetPriorStatic
'#HasFocus
'#SetFocus
'#VerifyFocus
'#
'######BUTTON
'^^DONE:^^
'#Click
'^^NOT DONE:^^
'#IsPressed'Depressed?  Isn't this a checkbox?
'^^NOT REVIEWED:^^
'#IsIndeterminate
'#
'#

'#####MENUITEM
'^^DONE:^^
'#Pick (poorly done)
'^^NOT DONE:^^
'#Check
'#IsChecked
'#Uncheck
'#VerifyChecked
'^^NOT REVIEWED:^^
'#Properties
'^^DONE:^^
'^^NOT DONE:^^
'#bChecked
'^^NOT REVIEWED:^^
'#
'#
'#
'#####COMBOBOX
'^^DONE:^^
'#GetItemCount
'#GetItemText
'#GetSelIndex
'#SetText
'#Select
'#GetSelText

'<Not directly handled, but indirectly solved>
'#VerifyContents
'#VerifyValue
'#VerifyText
'#FindItem
'#GetText

'^^NOT DONE:^^
'#ClearText
'^^NOT REVIEWED:^^
'#GetContents
'#Properties
'^^DONE:^^
'#iItemCount
'#sValue
'^^NOT DONE:^^
'^^NOT REVIEWED:^^
'#lsContents
'#iValue
'#
'#####LISTBOX
'^^DONE:^^
'#GetItemCount
'#GetItemText
'#Select
'#GetSelIndex
'#GetSelText
'<Not directly handled, but indirectly solved>
'#BeginDrag'Not ideal, but it can be done.
'#EndDrag
'#VerifyContents
'#VerifyValue
'#GetContents
'#FindItem
'#IsMultiSel

'^^NOT DONE:^^
'#MultiSelect
'#MultiUnselect
'#IsExtendSel
'#GetMultiSelIndex
'#GetMultiSelText
'#SelectList
'#SelectRange
'#ExtendSelect
'#DoubleSelect
'^^NOT REVIEWED:^^
'#Properties
'^^DONE:^^
'#sValue
'#iItemCount
'#bIsMulti
'^^NOT DONE:^^
'#bIsExtend
'^^NOT REVIEWED:^^
'#lsContents
'#iValue
'#liValue
'#lsValue
'#
'#
'#####EDIT
'^^DONE:^^
'#SetText
'#GetText
'#IsItalic
'#GetFontName
'#GetFontSize
'#IsMultiText
'#GetContents

'<Not directly handled, but indirectly solved>
'#GetMultiText
'#ClearText
'#GetPosition//not column/row, just index.
'#VerifyPosition

'^^NOT DONE:^^
'#GetMultiSelText
'#IsBold
'#IsRichText
'#IsUnderline
'#VerifySelRange
'#VerifySelText
'#VerifyValue
'#GetSelRange
'#GetSelText
'#SetMultiText
'#SetPosition
'#SetSelRange
'^^NOT REVIEWED:^^

'#Properties
'^^DONE:^^
'#sValue
'#bIsMulti
'#lsValue

'^^NOT DONE:^^
'^^NOT REVIEWED:^^
'#
'#
'#####LISTVIEW
'^^DONE:^^
'#GetColumnName
'#GetColumnCount
'#GetItemText
'#GetSelIndex 
'#GetContents 
'#GetMultiSelText
'#GetSelText 
'#GetMultiSelIndex
'#FindItem
'#Select
'#SelectList
'#SelectRange
'#ExtendSelect
'#MultiSelect

'<Not directly handled, but indirectly solved>
'#BeginDrag
'#EndDrag
'#MultiUnselect
'#IsMultiSel

'^^NOT DONE:^^
'^^NOT REVIEWED:^^
'#DoubleSelect
'#ExposeItem
'#GetItemImageState 
'#GetItemImageIndex 
'#GetItemRect
'#GetView
'#method
'#(ListView) 
'#IsExtendSel
'#PressItem
'#ReleaseItem
'#VerifyContents
'#VerifyValue
'#
'#
'#####TREEVIEW
'^^DONE:^^
'#GetItemText
'#FindItem
'#GetItemCount
'#GetSelText
'#Expand
'#Collapse
'#Select

'<Not directly handled, but indirectly solved>
'#BeginDrag
'#EndDrag
'#VerifyContents
'#VerifyValue

'^^NOT DONE:^^
'#GetSelIndex
'#GetSubItemCount
'#GetSubItems 
'#DoubleSelect
'#IsItemEditable
'#IsItemExpandable
'#IsItemExpanded
'#MultiSelect
'#MultiUnselect
'#SelectList
'#GetItemRect
'^^NOT REVIEWED:^^
'#ExposeItem
'#ExtendSelect
'#GetContents
'#GetItemImageIndex
'#GetItemImageState 
'#GetItemLevel
'#PressItem
'#ReleaseItem
'#

#End Region

#Const UseAttributes = 2 'set to 1 to enable attributes

''' <summary>
''' Allows user to perform various actions including: <p/>
''' <ul>
''' <li>Send Input (both mouse and keyboard).</li>
''' <li>Take a screenshot.</li>
''' <li>Compare Two Images</li>
''' <li>Record data in reporter.</li>
''' <li>Set/Get Text from a clipboard, clear clipboard.</li>
''' <li>Activate a window</li>
''' <li>Create a Window, SWFWindow.</li>
''' </ul>
''' </summary>
''' <remarks></remarks>
Public Class InterAct
    Private Shared myMouse As Mouse
    Private Shared myScreenShot As Screenshot
    Private Shared myClipboard As Clipboard
    Private Shared InternalStyleInfo As New StyleInfo()
    Private Shared InternalExtendedStyleInfo As New StyleExtendedInfo()
    Private Shared AutoSettings As AutomationSettings
    Private Shared CurrentDomain As AppDomain
    Protected Friend Shared WithEvents RunningTimer As New Timers.Timer(10 * 60000)
    Private Shared aReport As IReport = Nothing
    Private WindowsFunctions As New APIControls.IndependentWindowsFunctionsv1()

#Region "Properties/Methods"
#Region "Reporter Stuff"
    ''' <summary>
    ''' Provided for convenience for reporting Pass.
    ''' </summary>
    ''' <remarks></remarks>
    Public ReadOnly Property Pass() As Byte
        Get
            Return Report.Pass
        End Get
    End Property

    ''' <summary>
    ''' Provided for convenience for reporting Fail.
    ''' </summary>
    ''' <remarks></remarks>
    Public ReadOnly Property Fail() As Byte
        Get
            Return Report.Fail
        End Get
    End Property

    ''' <summary>
    ''' Provided for convenience for reporting Warning.
    ''' </summary>
    ''' <remarks></remarks>
    Public ReadOnly Property Warning() As Byte
        Get
            Return Report.Warning
        End Get
    End Property

    ''' <summary>
    ''' Provided for convenience for reporting Info.
    ''' </summary>
    ''' <remarks></remarks>
    Public ReadOnly Property Info() As Byte
        Get
            Return Report.Info
        End Get
    End Property

    ''' <summary>
    ''' Provides access to the reporter to allow users to append results to the report.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property Report() As IReport
        Get
            RunTimeReset()
            Return aReport
        End Get
    End Property

    ''' <summary>
    ''' Records the results.  This is provided as a shortcut to Report.RecordEvent(...)
    ''' </summary>
    ''' <param name="TypeOfMessage">The message type (i.e. Pass,Fail, etc.)</param>
    ''' <param name="MainMessage">The message you wish to record.</param>
    ''' <param name="AdditionalDetails">Any addtional details to record if any are provided.</param>
    ''' <remarks></remarks>
    Public Sub RecordEvent(ByVal TypeOfMessage As Byte, ByVal MainMessage As String, Optional ByVal AdditionalDetails As String = "")
        Report.RecordEvent(TypeOfMessage, MainMessage, AdditionalDetails)
    End Sub

    ''' <summary>
    ''' Provides the ability to replace the reporter with your own reporter.
    ''' The replacement reporter must implement IReport.
    ''' </summary>
    ''' <param name="reporter">A replacement reporter.</param>
    ''' <remarks>If a connection string isn't already set, this 
    ''' will set the connection string based upon the project settings.</remarks>
    Public Sub OverrideReporter(ByRef reporter As IReport)
        If (reporter Is Nothing) Then
            Return
        End If
        RunTimeReset()
        If (String.IsNullOrEmpty(reporter.ReportConnectionString)) Then
            reporter.ReportConnectionString = aReport.ReportConnectionString
        End If
        aReport = reporter
    End Sub
#End Region
#Region "Style"
    ''' <summary>
    ''' Allows for the testing of the style information.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared ReadOnly Property StyleInfo() As StyleInfo
        Get
            RunTimeReset()
            Return InternalStyleInfo
        End Get
    End Property
    ''' <summary>
    ''' Allows for the testing of extended style information. 
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared ReadOnly Property StyleExtendedInfo() As StyleExtendedInfo
        Get
            RunTimeReset()
            Return InternalExtendedStyleInfo
        End Get
    End Property
#End Region

    ''' <summary>
    ''' Automation settings affecting runtime.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property Settings() As AutomationSettings
        Get
            RunTimeReset()
            Return AutoSettings
        End Get
    End Property

    '' <summary>
    '' Creates a messagebox, which is useful for pausing the automation with a
    '' debug message.  By default, the messagebox is setup 
    '' </summary>
    '' <value></value>
    '' <returns></returns>
    '' <remarks></remarks>
    'Public ReadOnly Property MessageBox() As MessageBox
    '    Get
    '        RunTimeReset()
    '        Return myMessageBox
    '    End Get
    'End Property

    'Public ReadOnly Property Log() As Log
    '    Get
    '        RunTimeReset()
    '        Return myLog
    '    End Get
    'End Property

    ''' <summary>
    ''' Allows the user to capture and process screenshots.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property Screenshot() As Screenshot
        Get
            RunTimeReset()
            Return myScreenShot
        End Get
    End Property

    ''' <summary>
    ''' Provides simple clipboard support for text.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property Clipboard() As Clipboard
        Get
            RunTimeReset()
            Return myClipboard
        End Get
    End Property

    ''' <summary>
    ''' Provides simple control over the mouse, without requiring any windows objects.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property Mouse() As Mouse
        Get
            RunTimeReset()
            Return myMouse
        End Get
    End Property

    ''' <summary>
    ''' Performs very similar functionality to System.Windows.Forms.SendKeys.Send(),
    ''' however it is slightly different.  It is designed to have less security issues
    ''' compared to the .net supplied functionality.  It also allows a user to control
    ''' the mouse as well as the keyboard using a single string.
    ''' </summary>
    ''' <param name="Input">A string of keys, key symbols or mouse actions you wish to send via the keyboard or mouse.</param>
    ''' <exception cref="ArgumentException">When the string passed in isn't valid</exception>
    ''' <exception cref="Exception">When Windows refuses or is unable to process the keys.
    ''' This maybe a security issue.</exception>
    ''' <remarks>
    ''' In order to be mostly compatable with SendKeys, there is support for all of the
    ''' keys that SendKeys provides, as well as a few additional keys.  There are also
    ''' some special limitations to this version of send keys.  <p/>
    ''' <p/>
    ''' Each key is represented by one or more characters. To specify a single keyboard 
    ''' character, use the character itself. For example, to represent the letter A, 
    ''' pass in the string "A" to the method. To represent more than one character, 
    ''' append each additional character to the one preceding it. To represent the 
    ''' letters A, B, and C, specify the parameter as "ABC".
    ''' <p/>
    ''' <table width="100%">
    ''' <tr>
    ''' <th><p>Special Key</p></th> 
    ''' <th><p>Symbolic Code</p></th> 
    ''' </tr>
    ''' <tr> 
    ''' <td><p>BACKSPACE</p></td> 
    ''' <td><p>{BACKSPACE}, {BS}, or {BKSP}</p></td> 
    ''' </tr>
    ''' <tr> 
    ''' <td><p>BREAK (currently unsupported)</p></td> 
    ''' <td><p>{BREAK}</p></td> 
    ''' </tr>
    ''' <tr>
    ''' <td><p>CAPS LOCK</p></td> 
    ''' <td><p>{CAPSLOCK}</p></td> 
    ''' </tr>
    ''' <tr> 
    ''' <td><p>DEL or DELETE</p> 
    ''' </td> 
    ''' <td><p>{DELETE} or {DEL}</p></td> 
    ''' </tr>
    ''' <tr> 
    ''' <td><p>DOWN ARROW</p></td> 
    ''' <td><p>{DOWN}</p></td> 
    ''' </tr>
    ''' <tr> 
    ''' <td><p>END</p></td> 
    ''' <td><p>{END}</p></td> 
    ''' </tr>
    ''' <tr> 
    ''' <td><p>ENTER</p></td> 
    ''' <td><p>{ENTER}or ~ </p></td> 
    ''' </tr>
    ''' <tr> 
    ''' <td><p>ESC </p> </td> 
    ''' <td> <p>{ESC} </p> </td> 
    ''' </tr>
    ''' <tr> 
    ''' <td> <p>HELP </p> </td> 
    ''' <td> <p>{HELP} </p> </td> 
    ''' </tr>
    ''' <tr> 
    ''' <td> <p>HOME </p> </td> 
    ''' <td> <p>{HOME} </p> </td> 
    ''' </tr>
    ''' <tr> 
    ''' <td> <p>INS or INSERT </p> </td> 
    ''' <td> <p>{INSERT} or {INS} </p> </td> 
    ''' </tr>
    ''' <tr> 
    ''' <td> <p>LEFT ARROW </p> </td> 
    ''' <td> <p>{LEFT} </p> </td> 
    ''' </tr>
    ''' <tr> 
    ''' <td> <p>NUM LOCK </p> </td> 
    ''' <td> <p>{NUMLOCK} </p> </td> 
    ''' </tr>
    ''' <tr> 
    ''' <td> <p>PAGE DOWN </p> </td> 
    ''' <td> <p>{PGDN} </p> </td> 
    ''' </tr>
    ''' <tr> 
    ''' <td> <p>PAGE UP </p> </td> 
    ''' <td> <p>{PGUP} </p> </td> 
    ''' </tr>
    ''' <tr> 
    ''' <td> <p>PRINT SCREEN </p> </td> 
    ''' <td> <p>{PRTSC}</p> </td> 
    ''' </tr>
    ''' <tr> 
    ''' <td> <p>RIGHT ARROW </p> </td> 
    ''' <td> <p>{RIGHT} </p> </td> 
    ''' </tr>
    ''' <tr> 
    ''' <td> <p>SCROLL LOCK </p> </td> 
    ''' <td> <p>{SCROLLLOCK} </p> </td> 
    ''' </tr>
    ''' <tr> 
    ''' <td> <p>TAB </p> </td> 
    ''' <td> <p>{TAB} </p> </td> 
    ''' </tr>
    ''' <tr> 
    ''' <td> <p>UP ARROW </p> </td> 
    ''' <td> <p>{UP} </p> 
    ''' </td>
    ''' <tr>
    '''<td><p>LEFT WINDOWS KEY</p></td>
    '''<td><p>{LWIN}</p></td>
    '''</tr>
    '''<tr>
    '''<td><p>RIGHT WINDOWS KEY</p></td>
    '''<td><p>{RWIN}</p></td>
    '''</tr> 
    ''' </tr>
    ''' <tr>
    '''<td><p>CONTEXT MENU KEY</p></td>
    '''<td><p>{CONTEXTMENU}</p></td>
    '''</tr> 
    ''' <tr> 
    ''' <td> <p>F1 </p> </td> 
    ''' <td> <p>{F1} </p> </td> 
    ''' </tr>
    ''' <tr> 
    ''' <td> <p>F2 </p> </td> 
    ''' <td> <p>{F2} </p> </td> 
    ''' </tr>
    ''' <tr> 
    ''' <td> <p>F3 </p> </td> 
    ''' <td> <p>{F3} </p> </td> 
    ''' </tr>
    ''' <tr> 
    ''' <td> <p>F4 </p> </td> 
    ''' <td> <p>{F4} </p> </td> 
    ''' </tr>
    ''' <tr> 
    ''' <td> <p>F5 </p> </td> 
    ''' <td> <p>{F5} </p> </td> 
    ''' </tr>
    ''' <tr> 
    ''' <td> <p>F6 </p> </td> 
    ''' <td> <p>{F6} </p> </td> 
    ''' </tr>
    ''' <tr> 
    ''' <td> <p>F7 </p> </td> 
    ''' <td> <p>{F7} </p> </td> 
    ''' </tr>
    ''' <tr> 
    ''' <td> <p>F8 </p> </td> 
    ''' <td> <p>{F8} </p> </td> 
    ''' </tr>
    ''' <tr> 
    ''' <td> <p>F9 </p> </td> 
    ''' <td> <p>{F9} </p> </td> 
    ''' </tr>
    ''' <tr> 
    ''' <td> <p>F10 </p> </td> 
    ''' <td> <p>{F10} </p> </td> 
    ''' </tr>
    ''' <tr> 
    ''' <td> <p>F11 </p> </td> 
    ''' <td> <p>{F11} </p> </td> 
    ''' </tr>
    ''' <tr> 
    ''' <td> <p>F12 </p> </td> 
    ''' <td> <p>{F12} </p> </td> 
    ''' </tr>
    ''' <tr> 
    ''' <td> <p>F13 </p> </td> 
    ''' <td> <p>{F13} </p> </td> 
    ''' </tr>
    ''' <tr> 
    ''' <td> <p>F14 </p> </td> 
    ''' <td> <p>{F14} </p> </td> 
    ''' </tr>
    ''' <tr> 
    ''' <td> <p>F15 </p> </td> 
    ''' <td> <p>{F15} </p> </td> 
    ''' </tr>
    ''' <tr> 
    ''' <td> <p>F16 </p> </td> 
    ''' <td> <p>{F16} </p> </td> 
    ''' </tr>
    ''' <tr> 
    ''' <td> <p>Keypad add </p> </td> 
    ''' <td> <p>{ADD} </p> </td> 
    ''' </tr>
    ''' <tr> 
    ''' <td> <p>Keypad subtract </p> </td> 
    ''' <td> <p>{SUBTRACT} </p> </td> 
    ''' </tr>
    ''' <tr> 
    ''' <td> <p>Keypad multiply </p> </td> 
    ''' <td> <p>{MULTIPLY} </p> </td> 
    ''' </tr>
    ''' <tr> 
    ''' <td> <p>Keypad divide </p> </td> 
    ''' <td> <p>{DIVIDE} </p> </td> 
    ''' </tr>
    ''' <tr> 
    ''' <td> <p>Left Click </p> </td> 
    ''' <td> <p>{LCLICK(x,y)} </p> </td> 
    ''' </tr>
    ''' <tr> 
    ''' <td> <p>Right Click </p> </td> 
    ''' <td> <p>{RCLICK(x,y)} </p> </td> 
    ''' </tr>
    ''' <tr> 
    ''' <td> <p>Move mouse </p> </td> 
    ''' <td> <p>{GOTOXY(x,y)} </p> </td> 
    ''' </tr>
    ''' </table>
    ''' There are additional modifier keys that can be used to change the 
    ''' functionality of the other keystrokes.  For example, you might press
    ''' Ctrl+C to perform a copy command.  In order to do that you provide 
    ''' the special character (^) and then the letter you wish to press.<p/>
    ''' <p/>
    ''' Example Usage: <p/>
    ''' SendKeys("^C")
    ''' 
    ''' <table width="50%">
    ''' <tr> <th> <p>Key </p> </th> 
    ''' <th> <p>Code </p> </th> 
    ''' </tr>
    ''' <tr> 
    ''' <td> <p>SHIFT </p> </td> 
    ''' <td> <p>+ </p> </td> 
    ''' </tr>
    ''' <tr> 
    ''' <td> <p>WINDOW (LOGO)</p> </td> 
    ''' <td> <p>* </p> </td> 
    ''' </tr>
    ''' <tr> 
    ''' <td> <p>CTRL </p> </td> 
    ''' <td> <p>^ </p> </td> 
    ''' </tr>
    ''' <tr> 
    ''' <td><p>ALT </p> </td> 
    ''' <td> <p>% </p> </td> 
    ''' <p/>Limitations:<p/>
    ''' 1. If you use parentheses in order to apply modifier buttons (such as shift and ctrl) on
    ''' multiple button presses you must close both the shift and ctrl Parentheses at the same time.
    ''' Example of correct usage:<p/>
    ''' +(^(HI))<p/>
    ''' Example of incorrect usage:<p/>
    ''' +(%(HE)H)<p/>
    ''' <p/>
    ''' </tr>
    ''' </table>
    ''' </remarks>
    Public Function SendInput(ByVal Input As String) As Boolean
        If (Me.SendInputPrivate(Input) = False) Then
            Throw New SlickTestUIException("Unable to process SendInput for the following string: " & Input)
        End If
    End Function

    ''' <summary>
    ''' Starts a program and attempts to define the window with either a 
    ''' hwnd or a process name if the hwnd is not avalible via the process
    ''' object.
    ''' </summary>
    ''' <param name="Program">Program name or path.</param>
    ''' <param name="WaitForReady">How long to wait for input idle.  By Default it is set to 7 seconds.</param>
    ''' <returns>A description with either a hwnd or a procress name</returns>
    ''' <remarks></remarks>
    Public Function StartProgram(ByVal Program As String, Optional ByVal WaitForReady As Integer = 7) As UIControls.Description
        Dim p As Process = System.Diagnostics.Process.Start(Program)
        Dim i As Integer = 0
        Do
            System.Threading.Thread.Sleep(500)
            If (i = 10) Then
                Throw New SlickTestUIException("Unable to start program: " & Program)
            End If
            i += 1
        Loop While (p Is Nothing)
        p.WaitForInputIdle(1000 * WaitForReady)
        Dim desc As UIControls.Description = Description.Create()

        Dim hwnd As IntPtr = System.Diagnostics.Process.GetProcessById(p.Id).MainWindowHandle
        If (hwnd <> IntPtr.Zero) Then
            desc.Add(UIControls.Description.DescriptionData.Hwnd, hwnd.ToString())
        Else
            desc.Add(APIControls.Description.DescriptionData.ProcessName, p.ProcessName.Replace(".exe", ""))
        End If
        Return desc
    End Function

    ''' <summary>
    ''' Starts a program and attempts to define the window with either a 
    ''' hwnd or a process name if the hwnd is not avalible via the process
    ''' object.
    ''' </summary>
    ''' <param name="ProcessStartInfo">Information to start a program.</param>
    ''' <param name="WaitForReady">How long to wait for input idle.  By Default it is set to 7 seconds.</param>
    ''' <returns>A description with either a hwnd or a procress name</returns>
    ''' <remarks></remarks>
    Public Function StartProgram(ByVal ProcessStartInfo As System.Diagnostics.ProcessStartInfo, Optional ByVal WaitForReady As Integer = 7) As UIControls.Description
        Dim p As Process = System.Diagnostics.Process.Start(ProcessStartInfo)
        Dim i As Integer = 0
        Do
            System.Threading.Thread.Sleep(500)
            If (i = 10) Then
                Throw New SlickTestUIException("Unable to start program: " & ProcessStartInfo.FileName)
            End If
            i += 1
        Loop While (p Is Nothing)
        p.WaitForInputIdle(1000 * WaitForReady)
        Dim desc As UIControls.Description = Description.Create()
        Dim hwnd As IntPtr = System.Diagnostics.Process.GetProcessById(p.Id).MainWindowHandle
        If (hwnd <> IntPtr.Zero) Then
            desc.Add(UIControls.Description.DescriptionData.Hwnd, hwnd.ToString())
        Else
            desc.Add(APIControls.Description.DescriptionData.ProcessName, p.ProcessName.Replace(".exe", ""))
        End If

        Return desc
    End Function


#End Region

#Region "Constructors"

    ''' <summary>
    ''' Create a Interact object.  The project file (stp) is loaded when this
    ''' object is initialized.  Appends a error handler to the current application
    ''' domain.  Performs various initializations and creates a report object 
    ''' for reporting.  If the project file has incorrect or invalid information
    ''' with reguards to the report object then the object will fail to initialize.
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub New()
        Dim File As String
        'I believe excutable path will return the app, not the DLL running
        File = System.IO.Path.GetDirectoryName(System.Windows.Forms.Application.ExecutablePath) & "\" & _
        System.IO.Path.GetFileNameWithoutExtension(System.Windows.Forms.Application.ExecutablePath) & ".stp"
        PreInit(File)
    End Sub

    ''' <summary>
    ''' Create a Interact object.  The project file (stp) is loaded when this
    ''' object is initialized.  Appends a error handler to the current application
    ''' domain.  Performs various initializations and creates a report object 
    ''' for reporting.  If the project file has incorrect or invalid information
    ''' with reguards to the report object then the object will fail to initialize.
    ''' </summary>
    ''' <param name="ProjectFile">The file generated by Slick Test that has a file extension of .stp.</param>
    ''' <remarks></remarks>
    Public Sub New(ByVal ProjectFile As String)
        PreInit(ProjectFile)
    End Sub

    ''' <summary>
    ''' Create a Interact object.  The project file (stp) is loaded when this
    ''' object is initialized.  Appends a error handler to the current application
    ''' domain.  Performs various initializations and creates a report object 
    ''' for reporting.  If the project file has incorrect or invalid information
    ''' with reguards to the report object then the object will fail to initialize.
    ''' </summary>
    ''' <param name="ProjectFile">The file generated by Slick Test that has a file extension of .stp.</param>
    ''' <param name="LoadReporter">Provided so you can use your own reporter.  The Report
    ''' will still be initialized, but it will use a stubbed class.  If you have already
    ''' loaded a report, this will be completely ignored using the previously loaded report.</param>
    ''' <remarks></remarks>
    Public Sub New(ByVal ProjectFile As String, ByVal LoadReporter As Boolean)
        PreInit(ProjectFile, LoadReporter)
    End Sub

    Private Sub PreInit(ByVal ProjectFile As String, Optional ByVal LoadRealReporter As Boolean = True)
        If (ProjectFile.EndsWith(".stp", StringComparison.InvariantCultureIgnoreCase) = False) Then
            Throw New SlickTestUIException("Invalid project file '" & ProjectFile & "'.  File must end with extension '.stp'")
        End If

        Try
            CurrentDomain = AppDomain.CurrentDomain()
            AddHandler CurrentDomain.UnhandledException, AddressOf ExceptionHandler
        Catch ex As Exception
            'should always work, but if not, meh.
        End Try
        Try
            If (System.IO.File.Exists(ProjectFile) = True) Then
                If (Not UIControls.AutomationSettings.ProjectAlreadyLoaded) Then
                    Log.Log("Loading project: " & ProjectFile)
                    UIControls.AutomationSettings.Project.LoadProject(ProjectFile)
                    AutoSettings = New AutomationSettings(System.IO.Path.GetDirectoryName(ProjectFile))
                End If
            Else
                Log.Log("FAILED TO Loading project: " & ProjectFile)
                'Report.RecordEvent(Report.Fail, "FAILED TO Loading project: '" & File & _
                '"'.  Without this file the execution can't continue.")

                Throw New SlickTestUIException("FAILED TO Loading project: '" & ProjectFile & _
                "'.  Without this file the execution can't continue.")
            End If
        Catch ex As Exception
            Log.Log("FAILED TO START INTERACT!")
            UIControls.Alert.Show("Bad stuff going on! EX:" & ex.ToString())
            Throw ex
        End Try
        Constructor(LoadRealReporter)
    End Sub
#End Region

#Region "Private/Protected Functions"

    Private Sub ExceptionHandler(ByVal sender As Object, ByVal args As UnhandledExceptionEventArgs)
        Dim e As Exception = DirectCast(args.ExceptionObject, Exception)
        Report.RecordEvent(Report.Fail, "An unhandled exception occured while running " & _
        Report.TestName, "Exception Details: " & e.ToString())
        If (UIControls.AutomationSettings.Project.CompileAsDebug = False) Then
            System.Diagnostics.Process.GetCurrentProcess.Kill()
        Else
            Log.Log("Unhandled exception occurred.  UI will close in 10 seconds.")
            Log.Log("Exception: " + e.ToString())
            System.Threading.Thread.Sleep(10000)
        End If
    End Sub

    Private Sub Constructor(ByVal LoadRealReporter As Boolean)
        Try
            myMouse = New Mouse
            myScreenShot = New Screenshot()
            myClipboard = New Clipboard()
            If (aReport Is Nothing) Then 'make sure not already loaded.
                If (LoadRealReporter = False) Then
                    aReport = New StubbedReport()
                Else
                    'UIControls.MessageBox.Show("Project.LastOpenedFile: " & Project.LastOpenedFile)
                    If (System.IO.File.Exists(UIControls.AutomationSettings.Project.ReportDatabasePath) OrElse _
                        UIControls.AutomationSettings.Project.ProjectName <> "" OrElse UIControls.AutomationSettings.Project.LastOpenedFile <> "") Then
                        aReport = New Report(UIControls.AutomationSettings.Project.GUID, _
                        UIControls.AutomationSettings.Project.ReportDatabasePath, _
                        UIControls.AutomationSettings.Project.ProjectName, _
                        UIControls.AutomationSettings.Project.IsOfficialRun)
                        aReport.ReportConnectionString = AutomationSettings.ExternalReportDatabaseConnectionString
                    Else
                        Throw New SlickTestUIException("Unable to load information to create a report object.")
                    End If
                End If
            End If
            RunTimeReset()
        Catch ex As Exception

        End Try

    End Sub

    ''' <summary>
    ''' Resets the timer.
    ''' </summary>
    ''' <remarks></remarks>
    Protected Friend Shared Sub RunTimeReset()
        RunningTimer.Stop()
        If (RunningTimer.Interval = 0) Then
            Return
        End If
        RunningTimer.Start()
        RunningTimer.Enabled = True
    End Sub

    Private Function ProcessInput(ByVal Input As String, ByVal SearchString As String, ByVal Index As Integer) As System.Drawing.Point
        Dim tempInput As String = ""
        RunTimeReset()

        tempInput = Input.Substring(Index + SearchString.Length)
        If (tempInput.Chars(0) <> """") Then 'X/Y
            Return GetXYVals(Input, SearchString, Index)
        Else 'Uses windows names
            Return GetDescription(Input, SearchString, Index)
        End If
    End Function

    Private Function GetDescription(ByVal Input As String, ByVal SearchString As String, ByVal Index As Integer) As System.Drawing.Point
        RunTimeReset()

        Dim hwnd As IntPtr = IntPtr.Zero
        Dim tempInput As String = ""
        Dim p As System.Drawing.Point = New System.Drawing.Point(0, 0)

        tempInput = Input.Substring(Index + SearchString.Length)
        Dim Counter As Integer = 0
        Do
            Counter = Counter + 1
        Loop While (tempInput.Chars(Counter) <> ",")
        Dim AppTitle As String = Space(256)
        AppTitle = tempInput.Substring(1, Counter - 2)
        '"title","obj,stuff"
        hwnd = WinAPI.API.FindWindow(vbNullString, AppTitle)
        If (hwnd = IntPtr.Zero) Then
            Return p
        End If
        Dim quoteStart As Int32 = tempInput.Substring(Counter + 1).IndexOf("""")
        Counter = quoteStart + 2 + Counter 'skip quote

        If (quoteStart <> -1) Then

            Dim endloop As Boolean = False
            Dim ClassName As String = Space(256)
            Dim firstSearch As Boolean = True
            'Dim loopStart As Integer = 0
            Do
                ClassName = ""
                Do
                    ClassName += tempInput.Chars(Counter)
                    Counter = Counter + 1
                    If (tempInput.Length = Counter) Then
                        Return p
                    End If
                Loop While (tempInput.Chars(Counter) <> "," And tempInput.Chars(Counter) <> """")

                If (tempInput.Chars(Counter) = """") Then
                    quoteStart = Counter + 1
                    If (tempInput.Substring(quoteStart).IndexOf("""") = -1) Then
                        endloop = True
                    End If
                Else
                    endloop = True
                End If
                'ClassName = tempInput.Substring(loopStart, Counter - 1)

                If (firstSearch = True) Then
                    'hwnd = UIControls.InternalMouse.searchAllWindows(AppTitle, hwnd, ClassName)
                    hwnd = WindowsFunctions.SearchForObjInApp(AppTitle, hwnd, ClassName)
                Else
                    hwnd = WindowsFunctions.SearchForObjInApp(AppTitle, hwnd, ClassName)
                End If

                firstSearch = False
                If (hwnd <> IntPtr.Zero) Then
                    If (UIControls.InternalMouse.ClickByHwnd(hwnd) = True) Then
                        Return New System.Drawing.Point(-1, -1)
                    Else
                        Return New System.Drawing.Point(0, 0)
                    End If
                End If
            Loop While (endloop = False)
        Else
            If (UIControls.InternalMouse.ClickByHwnd(hwnd) = True) Then
                Return New System.Drawing.Point(-1, -1)
            Else
                Return New System.Drawing.Point(0, 0)
            End If
        End If
        If (UIControls.InternalMouse.ClickByHwnd(hwnd) = True) Then
            Return New System.Drawing.Point(-1, -1)
        Else
            Return New System.Drawing.Point(0, 0)
        End If
    End Function

    Private Function GetXYVals(ByVal Input As String, ByVal SearchString As String, ByVal Index As Integer) As System.Drawing.Point
        RunTimeReset()

        Dim X, Y As Integer
        X = 0
        Y = 0
        Dim tempInput As String = Input.Substring(Index + SearchString.Length)
        Dim Counter As Integer = -1
        Do
            Counter = Counter + 1
        Loop While (tempInput.Chars(Counter) <> ",")
        Dim LastPortionOfClick As Integer
        'WriteLog("SubStrPerIndex + SearchString.Length= : " & Input.Substring(Index + SearchString.Length))

        X = CInt(tempInput.Substring(0, Counter))
        'WriteLog("X = " & X)
        LastPortionOfClick = tempInput.Substring(Counter + 1).IndexOf(")")
        Y = CInt(tempInput.Substring(Counter + 1, LastPortionOfClick))
        'WriteLog("Y = " & Y)
        Return New System.Drawing.Point(X, Y)
    End Function

    Private Shared Sub RunningTimer_Elapsed(ByVal sender As Object, ByVal e As System.Timers.ElapsedEventArgs) Handles RunningTimer.Elapsed
        aReport.RecordEvent(aReport.Fail, "Automation timed out.", "Automation timed out after " & ((RunningTimer.Interval / 1000) / 60) & " minutes.")
        'System.Reflection.Assembly.GetExecutingAssembly.getn()
        'Diagnostics.Process.GetProcessesByName(System.Reflection.Assembly.GetExecutingAssembly.GetName.Name)
        Process.GetCurrentProcess.CloseMainWindow()
        Process.GetCurrentProcess.Close()
    End Sub

    Private Function SendInputPrivate(ByVal Input As String) As Boolean
        RunTimeReset()
        If (Input.Length <= 0) Then
            Exit Function
        End If
        'WriteLog("******************************2:" & Input.Length)
        SendInputPrivate = True
        Dim LIndex As Integer = Input.IndexOf("{LCLICK(")
        Dim RIndex As Integer = Input.IndexOf("{RCLICK(")
        Dim GotoXY As Integer = Input.IndexOf("{GOTOXY(")
        Dim FoundClick As Boolean = False
        Dim SearchString As String = ""
        Dim tempInput As String = ""
        Dim Index As Integer
        Dim p As System.Drawing.Point = New System.Drawing.Point(0, 0)
        If (LIndex <> -1 Or RIndex <> -1 Or GotoXY <> -1) Then
            FoundClick = True
            If (LIndex > RIndex) Then
                If (LIndex > GotoXY) Then
                    Index = LIndex
                    SearchString = "{LCLICK("
                Else
                    Index = GotoXY
                    SearchString = "{GOTOXY("
                End If
            Else
                If (RIndex > GotoXY) Then
                    Index = RIndex
                    SearchString = "{RCLICK("
                Else
                    Index = GotoXY
                    SearchString = "{GOTOXY("
                End If
            End If
            'WriteLog("******************************3")
            'WriteLog("Searching in: " & Input)
            'WriteLog("Searching for: " & SearchString)
            'WriteLog("Index = : " & Index)
            'WriteLog("SubStrPerIndex = : " & Input.Substring(Index))
            p = ProcessInput(Input, SearchString, Index)
            tempInput = Input.Substring(0, Index)
            'WriteLog("tempInput2 - " & tempInput)
            Dim str As String = SearchString

            Index = Input.IndexOf(str)
            Index = Input.IndexOf(")}", Index + 2)

            'WriteLog("FinalIndex = " & Index)
        Else
            tempInput = Input
        End If
        Try
            Keyboard.SendKeys(tempInput)
        Catch ex As Exception
            Return False
        End Try

        If (FoundClick = True) Then
            Dim click As Boolean = True
            If (p.X = 0) Then
                If (p.Y = 0) Then
                    SendInputPrivate = False
                    click = False
                End If
            End If
            If (p.X = -1) Then
                If (p.Y = -1) Then
                    click = False 'already clicked...
                End If
            End If
            If (click <> False) Then
                If (SearchString.IndexOf("L") <> -1) Then
                    UIControls.InternalMouse.LeftClickXY(p.X, p.Y)
                Else
                    UIControls.InternalMouse.RightClickXY(p.X, p.Y)
                End If
            End If
            Return SendInputPrivate(Input.Substring(Index))
        End If
    End Function

    Private Sub SetupWinObjects()
        RunTimeReset()
    End Sub


#End Region

    'NOTE: I don't see a reason SlickTest should allow users to use anything but a window or a 
    'winobject first, and then any sub object as required.

#Region "//////////**********Win 32 code***********\\\\\\\\\\\\"
    ''' <summary>
    ''' A Window type of Microsoft Windows object, inherits from WinObject.
    ''' </summary>
    ''' <param name="description">A text description.</param>
    ''' <returns>A Window object which allows you to perform various actions on the 
    ''' Window object or any objects below it.</returns>
    ''' <remarks></remarks>
    Public Function Window(ByVal description As String) As Window
        Dim win As Window
        SetupWinObjects()

        Try
            win = New Window(description.ToString())
            win.reporter = Me.Report
        Catch ex As Exception
            Throw New SlickTestUIException("Unable to create a SlickTest object.", ex, Report)
        End Try
        Return win
    End Function

    ''' <summary>
    ''' A Window type of Microsoft Windows object, inherits from WinObject.
    ''' </summary>
    ''' <param name="description">A description object.</param>
    ''' <returns>A Window object which allows you to perform various actions on the 
    ''' Window object or any objects below it.</returns>
    ''' <remarks></remarks>
    Public Function Window(ByVal description As UIControls.Description) As Window
        Dim win As Window
        SetupWinObjects()
        Try
            win = New Window(description)
            win.reporter = Me.Report
        Catch ex As Exception
            Throw New SlickTestUIException("Unable to create a SlickTest object.", ex, Report)
        End Try
        Return win
    End Function

    ''' <summary>
    ''' A Window type of Microsoft Windows object, inherits from WinObject.
    ''' </summary>
    ''' <returns>A Window object which allows you to perform various actions on the 
    ''' Window object or any objects below it.</returns>
    ''' <remarks>Throws an exception if no active window can be found.</remarks>
    Public Function ActiveWindow() As Window
        Dim hwnd As IntPtr = UIControls.Window.GetActiveWindow()
        Dim description As UIControls.Description = UIControls.Description.Create()
        If (hwnd = IntPtr.Zero) Then
            Throw New SlickTestUIException("No active window could be found.")
        End If
        description.Add("hwnd", hwnd.ToString())
        Return Window(description)
    End Function
#End Region

#Region "//////////**********Dot net code***********\\\\\\\\\\\\"
    ''' <summary>
    ''' A SwfWindow type of Microsoft Windows object, inherits from WinObject.
    ''' </summary>
    ''' <param name="description">A text description.</param>
    ''' <returns>A Window object which allows you to perform various actions on the 
    ''' Window object or any objects below it.</returns>
    ''' <remarks></remarks>
    Public Function SwfWindow(ByVal description As String) As SwfWindow
        RunTimeReset()

        Dim win As SwfWindow
        Try
            win = New SwfWindow(description)
            win.reporter = Me.Report
        Catch ex As Exception
            Throw New SlickTestUIException("Unable to create a SlickTest object.", ex, Report)
        End Try
        Return win
    End Function

    ''' <summary>
    ''' A SwfWindow type of Microsoft Windows object, inherits from WinObject.
    ''' </summary>
    ''' <param name="description">A description object.</param>
    ''' <returns>A Window object which allows you to perform various actions on the 
    ''' Window object or any objects below it.</returns>
    ''' <remarks></remarks>
    Public Function SwfWindow(ByVal description As UIControls.Description) As SwfWindow
        RunTimeReset()

        Dim win As SwfWindow

        Try
            win = New SwfWindow(description)
            win.reporter = Me.Report
        Catch ex As Exception
            Throw New SlickTestUIException("Unable to create a SlickTest object.", ex, Report)
        End Try
        Return win
    End Function
#End Region

#Region "//////////**********Web code***********\\\\\\\\\\\\"


    ''' <summary>
    ''' A IEWebBrowser type of Microsoft Windows object, inherits from WinObject.
    ''' </summary>
    ''' <param name="description">A text description.</param>
    ''' <returns>A IEWebBrowser object which allows you to perform various actions on the 
    ''' IEWebBrowser object or any objects below it.</returns>
    ''' <remarks></remarks>
    Public Function IEWebBrowser(ByVal description As String) As IEWebBrowser
        SetupWinObjects()
        Dim win As IEWebBrowser
        Try
            win = New IEWebBrowser(description)
            win.reporter = Me.Report
        Catch ex As Exception
            Throw New SlickTestUIException("Unable to create a SlickTest object.", ex, Report)
        End Try
        Return win
    End Function

    ''' <summary>
    ''' A IEWebBrowser type of Microsoft Windows object, inherits from WinObject.
    ''' </summary>
    ''' <param name="description">A description object.</param>
    ''' <returns>A IEWebBrowser object which allows you to perform various actions on the 
    ''' IEWebBrowser object or any objects below it.</returns>
    ''' <remarks></remarks>
    Public Function IEWebBrowser(ByVal description As UIControls.Description) As IEWebBrowser
        SetupWinObjects()
        Dim win As IEWebBrowser
        Try
            win = New IEWebBrowser(description)
            win.reporter = Me.Report
        Catch ex As Exception
            Throw New SlickTestUIException("Unable to create a SlickTest object.", ex, Report)
        End Try
        Return win
    End Function

#End Region

#Region "AppActivate"
    ''' <summary>
    ''' Activates an App via the PID value.
    ''' </summary>
    ''' <param name="AppID">Process ID</param>
    ''' <remarks>This is just a simple wrapper for Microsoft.VisualBasic.AppActivate
    ''' </remarks>
    Public Sub AppActivate(ByVal AppID As Integer)
        RunTimeReset()
        Microsoft.VisualBasic.AppActivate(AppID)
    End Sub

    ''' <summary>
    ''' Activates an App via the App Title Text.
    ''' </summary>
    ''' <param name="AppTitle">A partial or complete title of the app you wish to activate</param>
    ''' <remarks>This is just a simple wrapper for Microsoft.VisualBasic.AppActivate
    ''' </remarks>
    Public Sub AppActivate(ByVal AppTitle As String)
        RunTimeReset()
        Microsoft.VisualBasic.AppActivate(AppTitle)
    End Sub

    ''' <summary>
    ''' Activates an App by finding the window containing a certain Hwnd
    ''' </summary>
    ''' <param name="Hwnd">A Hwnd inside of the app you wish to activate</param>
    ''' <remarks>This is just a simple wrapper for Microsoft.VisualBasic.AppActivate
    ''' </remarks>
    Public Sub AppActivateByHwnd(ByVal Hwnd As Int64)
        RunTimeReset()
        WindowsFunctions.AppActivateByHwnd(New IntPtr(Hwnd))
    End Sub

    ''' <summary>
    ''' Activates an App by finding the window containing a certain Hwnd
    ''' </summary>
    ''' <param name="Hwnd">A Hwnd inside of the app you wish to activate</param>
    ''' <remarks>This is just a simple wrapper for Microsoft.VisualBasic.AppActivate
    ''' </remarks>
    Public Sub AppActivateByHwnd(ByVal Hwnd As IntPtr)
        RunTimeReset()
        WindowsFunctions.AppActivateByHwnd(Hwnd)
    End Sub
#End Region


End Class


