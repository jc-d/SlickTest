Imports WinAPI.API

' TODO: BS_PUSHBUTTON,  
' ComCtl32.dll version 6, Vista only: BS_SPLITBUTTON, BS_DEFSPLITBUTTON, BS_COMMANDLINK, BS_DEFCOMMANDLINK
' http://msdn2.microsoft.com/en-us/library/ms673347.aspx
' You can access this through StyleInfo.ButtonStyle

''' <summary>
''' The following styles can be specified wherever a Button style is required. 
''' </summary>
''' <remarks>NOTE: Some of this information was provided by Microsoft's(tm) MSDN.</remarks>
Public Class ButtonStyle

    Protected Friend Sub New()

    End Sub

    ''' <summary>
    ''' Creates a small, empty check box with text. By default, the text is displayed to 
    ''' the right of the check box. To display the text to the left of the check box, combine 
    ''' this flag with the BS_LEFTTEXT style (or with the equivalent BS_RIGHTBUTTON style).
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property BS_CHECKBOX() As Integer
        Get
            Return BS.CHECKBOX
        End Get
    End Property

    ''' <summary>
    ''' Specifies that the button displays text.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property BS_TEXT() As Integer
        Get
            Return BS.TEXT
        End Get
    End Property

    ''' <summary>
    ''' Creates a push button that behaves like a BS_PUSHBUTTON style button, but 
    ''' has a distinct appearance. If the button is in a dialog box, the user can 
    ''' select the button by pressing the ENTER key, even when the button does not 
    ''' have the input focus. This style is useful for enabling the user to quickly 
    ''' select the most likely (default) option.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property BS_DEFPUSHBUTTON() As Integer
        Get
            Return BS.DEFPUSHBUTTON
        End Get
    End Property

    ''' <summary>
    ''' Creates a button that is the same as a check box, except that the check 
    ''' state automatically toggles between checked and cleared each time the 
    ''' user selects the check box.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property BS_AUTOCHECKBOX() As Integer
        Get
            Return BS.AUTOCHECKBOX
        End Get
    End Property

    ''' <summary>
    ''' Creates a small circle with text. By default, the text is displayed to the 
    ''' right of the circle. To display the text to the left of the circle, combine 
    ''' this flag with the BS_LEFTTEXT style (or with the equivalent BS_RIGHTBUTTON 
    ''' style). Use radio buttons for groups of related, but mutually exclusive choices.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property BS_RADIOBUTTON() As Integer
        Get
            Return BS.RADIOBUTTON
        End Get
    End Property

    ''' <summary>
    ''' Creates a button that is the same as a check box, except that the box can be 
    ''' grayed as well as checked or cleared. Use the grayed state to show that the 
    ''' state of the check box is not determined.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property BS_3STATE() As Integer
        Get
            Return BS.n3STATE
        End Get
    End Property

    ''' <summary>
    ''' Creates a button that is the same as a three-state check box, except that 
    ''' the box changes its state when the user selects it. The state cycles through 
    ''' checked, indeterminate, and cleared.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property BS_AUTO3STATE() As Integer
        Get
            Return BS.AUTO3STATE
        End Get
    End Property

    ''' <summary>
    ''' Creates a rectangle in which other controls can be grouped. Any text 
    ''' associated with this style is displayed in the rectangle's upper left corner.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property BS_GROUPBOX() As Integer
        Get
            Return BS.GROUPBOX
        End Get
    End Property

    ''' <summary>
    ''' Obsolete, but provided for compatibility with 16-bit versions of 
    ''' Windows. Applications should use BS_OWNERDRAW instead.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property BS_USERBUTTON() As Integer
        Get
            Return BS.USERBUTTON
        End Get
    End Property

    ''' <summary>
    ''' Creates a button that is the same as a radio button, except that when the 
    ''' user selects it, the system automatically sets the button's check state to 
    ''' checked and automatically sets the check state for all other buttons in the 
    ''' same group to cleared.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property BS_AUTORADIOBUTTON() As Integer
        Get
            Return BS.AUTORADIOBUTTON
        End Get
    End Property

    ''' <summary>
    ''' Creates an owner-drawn button. The owner window receives a WM_DRAWITEM 
    ''' message when a visual aspect of the button has changed. Do not combine 
    ''' the BS_OWNERDRAW style with any other button styles.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property BS_OWNERDRAW() As Integer
        Get
            Return BS.OWNERDRAW
        End Get
    End Property

    ''' <summary>
    ''' Positions a radio button's circle or a check box's square on the 
    ''' right side of the button rectangle. Same as the BS_LEFTTEXT style.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property BS_RIGHTBUTTON() As Integer
        Get
            Return BS.RIGHTBUTTON
        End Get
    End Property

    ''' <summary>
    ''' Places text on the left side of the radio button or check box when 
    ''' combined with a radio button or check box style. Same as the 
    ''' BS_RIGHTBUTTON style.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property BS_LEFTTEXT() As Integer
        Get
            Return BS.LEFTTEXT
        End Get
    End Property

    ''' <summary>
    ''' Specifies that the button displays an icon. See the Remarks 
    ''' section for its interaction with BS_BITMAP.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks><table>
    ''' <tr>
    ''' <th>BS_ICON or BS_BITMAP set?</th>
    ''' <th>BM_SETIMAGE called?</th>
    ''' <th>Result</th>
    ''' </tr>
    ''' <tr><td>Yes</td><td>Yes</td><td>Show icon only.</td></tr>
    ''' <tr><td>No</td><td>Yes</td><td>Show icon and text.</td></tr>
    ''' <tr><td>Yes</td><td>No</td><td>Show text only.</td></tr>
    ''' <tr><td>No</td><td>No</td><td>Show text only</td></tr>
    ''' </table>
    ''' </remarks>
    Public ReadOnly Property BS_ICON() As Integer
        Get
            Return BS.ICON
        End Get
    End Property

    ''' <summary>
    ''' Specifies that the button displays a bitmap. See the Remarks 
    ''' section for its interaction with BS_ICON.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks><table>
    ''' <tr>
    ''' <th>BS_ICON or BS_BITMAP set?</th>
    ''' <th>BM_SETIMAGE called?</th>
    ''' <th>Result</th>
    ''' </tr>
    ''' <tr><td>Yes</td><td>Yes</td><td>Show icon only.</td></tr>
    ''' <tr><td>No</td><td>Yes</td><td>Show icon and text.</td></tr>
    ''' <tr><td>Yes</td><td>No</td><td>Show text only.</td></tr>
    ''' <tr><td>No</td><td>No</td><td>Show text only</td></tr>
    ''' </table>
    ''' </remarks>
    Public ReadOnly Property BS_BITMAP() As Integer
        Get
            Return BS.BITMAP
        End Get
    End Property

    ''' <summary>
    ''' Left-justifies the text in the button rectangle. However, if the button 
    ''' is a check box or radio button that does not have the BS_RIGHTBUTTON 
    ''' style, the text is left justified on the right side of the check box 
    ''' or radio button.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property BS_LEFT() As Integer
        Get
            Return BS.LEFT
        End Get
    End Property

    ''' <summary>
    ''' Right-justifies text in the button rectangle. However, if the button is 
    ''' a check box or radio button that does not have the BS_RIGHTBUTTON style, 
    ''' the text is right justified on the right side of the check box or radio 
    ''' button.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property BS_RIGHT() As Integer
        Get
            Return BS.RIGHT
        End Get
    End Property

    ''' <summary>
    ''' Centers text horizontally in the button rectangle.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property BS_CENTER() As Integer
        Get
            Return BS.CENTER
        End Get
    End Property

    ''' <summary>
    ''' Places text at the top of the button rectangle.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property BS_TOP() As Integer
        Get
            Return BS.TOP
        End Get
    End Property

    ''' <summary>
    ''' Places text at the bottom of the button rectangle.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property BS_BOTTOM() As Integer
        Get
            Return BS.BOTTOM
        End Get
    End Property

    ''' <summary>
    ''' Places text in the middle (vertically) of the button rectangle.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property BS_VCENTER() As Integer
        Get
            Return BS.VCENTER
        End Get
    End Property

    ''' <summary>
    ''' Makes a button (such as a check box, three-state check box, or 
    ''' radio button) look and act like a push button. The button looks 
    ''' raised when it isn't pushed or checked, and sunken when it is 
    ''' pushed or checked.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property BS_PUSHLIKE() As Integer
        Get
            Return BS.PUSHLIKE
        End Get
    End Property

    ''' <summary>
    ''' Wraps the button text to multiple lines if the text string is 
    ''' too long to fit on a single line in the button rectangle.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property BS_MULTILINE() As Integer
        Get
            Return BS.MULTILINE
        End Get
    End Property

    ''' <summary>
    ''' Enables a button to send BN_KILLFOCUS and BN_SETFOCUS notification messages to 
    ''' its parent window. Note that buttons send the BN_CLICKED notification message 
    ''' regardless of whether it has this style. To get BN_DBLCLK notification messages, 
    ''' the button must have the BS_RADIOBUTTON or BS_OWNERDRAW style.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property BS_NOTIFY() As Integer
        Get
            Return BS.NOTIFY
        End Get
    End Property

    ''' <summary>
    ''' Specifies that the button is two-dimensional; it does not 
    ''' use the default shading to create a 3-D image.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property BS_FLAT() As Integer
        Get
            Return BS.FLAT
        End Get
    End Property

    ''' <summary>
    ''' Tests the windows object style value to see if it includes the BS_Value.
    ''' </summary>
    ''' <param name="StyleValue">The style value returned from a windows object.</param>
    ''' <param name="BS_Value">The BS value you that is checked to see if it is contained in the windows object.</param>
    ''' <returns></returns>
    ''' <remarks>Example usage: StyleInfo.ButtonStyle.ContainsValue(Window(MyWindow).GetStyle.ButtonStyle.GetStyle(),StyleInfo.ButtonStyle.BS_FLAT)
    ''' <p />This will return true if the window contains the value "BS_FLAT".</remarks>
    Public Function ContainsValue(ByVal StyleValue As Int64, ByVal BS_Value As Int64) As Boolean
        Return ((StyleValue And BS_Value) = BS_Value)
    End Function

    ''' <summary>
    ''' Provides a string with all the values that the StyleValue provided has set.
    ''' </summary>
    ''' <param name="StyleValue">The style value returned from a windows object.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ValueInfo(ByVal StyleValue As Int64) As String
        Dim RetVal As New System.Text.StringBuilder(100)
        Dim Name As String = String.Empty
        Dim AddItems As New System.Collections.ArrayList()
        For Each item As BS In [Enum].GetValues(GetType(BS))
            Try
                If (ContainsValue(StyleValue, item) = True) Then
                    Name = [Enum].GetName(GetType(BS), item).TrimStart("n"c)
                    If (AddItems.Contains(item) = False) Then
                        If (AddItems.Count <> 0) Then
                            RetVal.Append(" - " & Name)
                        Else
                            RetVal.Append(Name)
                        End If
                        AddItems.Add(item)
                    End If
                End If
            Catch ex As Exception
                'Nothing to worry about, there are a few uint values, and nothing I can do about em.
            End Try
        Next
        Return RetVal.ToString().TrimEnd(" "c)
    End Function

End Class