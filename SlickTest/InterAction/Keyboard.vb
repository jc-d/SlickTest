Imports winAPI.API
''' <summary>
''' Provides the user with control over the keyboard.
''' </summary>
''' <remarks></remarks>
Public Class Keyboard

    ''' <summary>
    ''' Performs very similar functionality to System.Windows.Forms.SendKeys.Send(),
    ''' however it is slightly different.  It is designed to have less security issues
    ''' compared to the .net supplied functionality.
    ''' </summary>
    ''' <param name="keys">A string of keys or key symbols you wish to send via the keyboard.</param>
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
    ''' </table>
    ''' 
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
    Public Shared Sub SendKeys(ByVal keys As String)
        APIControls.InternalKeyboard.SendKeys(keys)
    End Sub

    ''' <summary>
    ''' This performs direct key presses to a specific window object but
    ''' only supports basic text with no special key support.
    ''' </summary>
    ''' <param name="keys">A string of keys you wish to send via the keyboard.</param>
    ''' <param name="hwnd">The window you wish to send to.</param>
    ''' <remarks></remarks>
    Public Shared Sub SendKeys(ByVal keys() As Char, ByVal hwnd As IntPtr)
        APIControls.InternalKeyboard.SendChars(keys, hwnd)
    End Sub

    ''' <summary>
    ''' This performs direct key presses to a specific window object but
    ''' only supports basic text with no special key support.
    ''' </summary>
    ''' <param name="keys">A string of keys you wish to send via the keyboard.</param>
    ''' <param name="hwnd">The window you wish to send to.</param>
    ''' <remarks>WARNING: Not all special characters are supported. It is suggested you
    ''' limit the character set to ASCII.</remarks>
    Public Shared Sub SendKeys(ByVal keys As String, ByVal hwnd As IntPtr)
        APIControls.InternalKeyboard.SendChars(keys, hwnd)
    End Sub

    Protected Friend Sub New()

    End Sub

End Class