Imports WinAPI.API
'TODO: Add ES_NUMBER: Allows only digits to be entered into the edit control.

''' <summary>
''' The following styles can be specified wherever a TextBox style is required. 
''' </summary>
''' <remarks>NOTE: Some of this information was provided by Microsoft's(tm) MSDN.
''' </remarks>
Public Class TextBoxStyle

    Protected Friend Sub New()

    End Sub

    ''' <summary>
    ''' Tests the windows object style value to see if it includes the ES_Value.
    ''' </summary>
    ''' <param name="StyleValue">The style value returned from a windows object.</param>
    ''' <param name="ES_Value">The ES value you that is checked to see if it is contained in the windows object.</param>
    ''' <returns></returns>
    ''' <remarks>Example usage: StyleInfo.ContainsValue(Window(MyWindow).GetStyle.TextStyle.GetStyle(),StyleInfo.TextStyle.ES_LEFT)
    ''' <p />This will return true if the window contains the value "ES_LEFT".</remarks>
    Public Function ContainsValue(ByVal StyleValue As Int64, ByVal ES_Value As Int64) As Boolean
        Return ((StyleValue And ES_Value) = ES_Value)
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
        For Each item As BS In [Enum].GetValues(GetType(WinAPI.API.ES))
            Try
                If (ContainsValue(StyleValue, item) = True) Then
                    Name = [Enum].GetName(GetType(ES), item).TrimStart("n"c)
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

    'Public Function TEMP_WriteCode() As String
    '    Dim Property_txt As String = "Public ReadOnly Property "
    '    Dim RetVal As String = String.Empty
    '    Dim Name As String = String.Empty
    '    Dim temp As String
    '    For Each item As ES In [Enum].GetValues(GetType(ES))
    '        temp = String.Empty
    '        Name = [Enum].GetName(GetType(ES), item)
    '        temp = Property_txt & "ES_" & Name.TrimStart("n"c).ToUpper() & "() As Integer" & vbNewLine & _
    '        "Get" & vbNewLine & _
    '        "Return " & "ES" & "." & Name.ToUpper() & vbNewLine & _
    '        "End Get" & vbNewLine & _
    '        "End Property"
    '        RetVal += temp & vbNewLine & vbNewLine
    '    Next
    '    System.Windows.Forms.Clipboard.SetText(RetVal)
    '    Return RetVal
    'End Function

    ''' <summary>
    ''' Left-aligns text in a single-line or multiline edit control.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property ES_LEFT() As Integer
        Get
            Return ES.LEFT
        End Get
    End Property

    ''' <summary>
    ''' Centers text in a single-line or multiline edit control.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property ES_CENTER() As Integer
        Get
            Return ES.CENTER
        End Get
    End Property

    ''' <summary>
    ''' Right-aligns text in a single-line or multiline edit control.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property ES_RIGHT() As Integer
        Get
            Return ES.RIGHT
        End Get
    End Property

    ''' <summary>
    '''  Designates a multiple-line edit control. (The default is single line.) 
    ''' If the ES_AUTOVSCROLL style is specified, the edit control shows as many 
    ''' lines as possible and scrolls vertically when the user presses the ENTER 
    ''' key. If ES_AUTOVSCROLL is not given, the edit control shows as many lines 
    ''' as possible and beeps if ENTER is pressed when no more lines can be displayed. 
    ''' If the ES_AUTOHSCROLL style is specified, the multiple-line edit control 
    ''' automatically scrolls horizontally when the caret goes past the right edge 
    ''' of the control. To start a new line, the user must press ENTER. If ES_AUTOHSCROLL 
    ''' is not given, the control automatically wraps words to the beginning of the next 
    ''' line when necessary; a new line is also started if ENTER is pressed. The position 
    ''' of the wordwrap is determined by the window size. If the window size changes, the 
    ''' wordwrap position changes and the text is redisplayed. Multiple-line edit controls 
    ''' can have scroll bars. An edit control with scroll bars processes its own scroll-bar 
    ''' messages. Edit controls without scroll bars scroll as described above and process 
    ''' any scroll messages sent by the parent window.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property ES_MULTILINE() As Integer
        Get
            Return ES.MULTILINE
        End Get
    End Property

    ''' <summary>
    ''' Converts all characters to uppercase as they are typed into the edit control.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property ES_UPPERCASE() As Integer
        Get
            Return ES.UPPERCASE
        End Get
    End Property

    ''' <summary>
    '''  Converts all characters to lowercase as they are typed into the edit control.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property ES_LOWERCASE() As Integer
        Get
            Return ES.LOWERCASE
        End Get
    End Property

    ''' <summary>
    ''' Displays all characters as an asterisk (*) as they are typed into the edit control. 
    ''' An application can use the SetPasswordChar member function to change the character 
    ''' that is displayed.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property ES_PASSWORD() As Integer
        Get
            Return ES.PASSWORD
        End Get
    End Property

    ''' <summary>
    ''' Automatically scrolls text up one page when the user presses ENTER on the last line.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property ES_AUTOVSCROLL() As Integer
        Get
            Return ES.AUTOVSCROLL
        End Get
    End Property

    ''' <summary>
    ''' Automatically scrolls text to the right by 10 characters when the user types a 
    ''' character at the end of the line. When the user presses the ENTER key, the control 
    ''' scrolls all text back to position 0.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property ES_AUTOHSCROLL() As Integer
        Get
            Return ES.AUTOHSCROLL
        End Get
    End Property

    ''' <summary>
    ''' Normally, an edit control hides the selection when the 
    ''' control loses the input focus and inverts the selection when 
    ''' the control receives the input focus. Specifying ES_NOHIDESEL 
    ''' deletes this default action.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property ES_NOHIDESEL() As Integer
        Get
            Return ES.NOHIDESEL
        End Get
    End Property

    ''' <summary>
    ''' Text entered in the edit control is converted from the ANSI character 
    ''' set to the OEM character set and then back to ANSI. This ensures proper 
    ''' character conversion when the application calls the AnsiToOem Windows 
    ''' function to convert an ANSI string in the edit control to OEM characters. 
    ''' This style is most useful for edit controls that contain filenames.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property ES_OEMCONVERT() As Integer
        Get
            Return ES.OEMCONVERT
        End Get
    End Property

    ''' <summary>
    ''' Prevents the user from entering or editing text in the edit control. 
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property ES_READONLY() As Integer
        Get
            Return ES.READONLY
        End Get
    End Property

    ''' <summary>
    ''' Specifies that a carriage return be inserted when the user presses the 
    ''' ENTER key while entering text into a multiple-line edit control in a 
    ''' dialog box. Without this style, pressing the ENTER key has the same 
    ''' effect as pressing the dialog boxs default pushbutton. This style has 
    ''' no effect on a single-line edit control.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property ES_WANTRETURN() As Integer
        Get
            Return ES.WANTRETURN
        End Get
    End Property
End Class
