Imports WinAPI.API

''' <summary>
''' The following styles can be specified wherever a ComboBox style is required. 
''' </summary>
''' <remarks>NOTE: Some of this information was provided by Microsoft's(tm) MSDN.</remarks>
Public Class ComboBoxStyle

    Protected Friend Sub New()
    End Sub

    ''' <summary>
    ''' Tests the windows object style value to see if it includes the CBS_Value.
    ''' </summary>
    ''' <param name="StyleValue">The style value returned from a windows object.</param>
    ''' <param name="CBS_Value">The CBS value you that is checked to see if it is contained in the windows object.</param>
    ''' <returns></returns>
    ''' <remarks>Example usage: StyleInfo.ComboBoxStyle.ContainsValue(Window(MyWindow).GetStyle(),StyleInfo.ComboBoxStyle.CBS_SIMPLE)
    ''' <p />This will return true if the window contains the value "CBS_SIMPLE".</remarks>
    Public Function ContainsValue(ByVal StyleValue As Int64, ByVal CBS_Value As Int64) As Boolean
        Return ((StyleValue And CBS_Value) = CBS_Value)
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
        For Each item As BS In [Enum].GetValues(GetType(ComboBoxStyles))
            Try
                If (ContainsValue(StyleValue, item) = True) Then
                    Name = [Enum].GetName(GetType(ComboBoxStyles), item).TrimStart("n"c)
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
    '    For Each item As ComboBoxStyles In [Enum].GetValues(GetType(ComboBoxStyles))
    '        temp = String.Empty
    '        Name = [Enum].GetName(GetType(ComboBoxStyles), item)
    '        temp = Property_txt & "" & Name.TrimStart("n"c).ToUpper() & "() As Integer" & vbNewLine & _
    '        "Get" & vbNewLine & _
    '        "Return " & "ComboBoxStyles" & "." & Name.ToUpper() & vbNewLine & _
    '        "End Get" & vbNewLine & _
    '        "End Property"
    '        RetVal += temp & vbNewLine & vbNewLine
    '    Next
    '    System.Windows.Forms.Clipboard.SetText(RetVal)
    '    Return RetVal
    'End Function

    ''' <summary>
    ''' Displays the list box at all times. The current selection in 
    ''' the list box is displayed in the edit control.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property CBS_SIMPLE() As Integer
        Get
            Return ComboBoxStyles.CBS_SIMPLE
        End Get
    End Property

    ''' <summary>
    ''' Similar to CBS_SIMPLE, except that the list box is not displayed 
    ''' unless the user selects an icon next to the edit control.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property CBS_DROPDOWN() As Integer
        Get
            Return ComboBoxStyles.CBS_DROPDOWN
        End Get
    End Property

    ''' <summary>
    ''' Similar to CBS_DROPDOWN, except that the edit control is replaced by a 
    ''' static text item that displays the current selection in the list box.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property CBS_DROPDOWNLIST() As Integer
        Get
            Return ComboBoxStyles.CBS_DROPDOWNLIST
        End Get
    End Property

    ''' <summary>
    ''' Specifies that the owner of the list box is responsible for drawing its 
    ''' contents and that the items in the list box are all the same height. 
    ''' The owner window receives a WM_MEASUREITEM message when the combo box 
    ''' is created and a WM_DRAWITEM message when a visual aspect of the combo 
    ''' box has changed.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property CBS_OWNERDRAWFIXED() As Integer
        Get
            Return ComboBoxStyles.CBS_OWNERDRAWFIXED
        End Get
    End Property

    ''' <summary>
    ''' Specifies that the owner of the list box is responsible for drawing its 
    ''' contents and that the items in the list box are variable in height. 
    ''' The owner window receives a WM_MEASUREITEM message for each item in 
    ''' the combo box when you create the combo box and a WM_DRAWITEM message 
    ''' when a visual aspect of the combo box has changed.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property CBS_OWNERDRAWVARIABLE() As Integer
        Get
            Return ComboBoxStyles.CBS_OWNERDRAWVARIABLE
        End Get
    End Property

    ''' <summary>
    ''' Automatically scrolls the text in an edit control to the right when 
    ''' the user types a character at the end of the line. If this style is 
    ''' not set, only text that fits within the rectangular boundary is allowed.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property CBS_AUTOHSCROLL() As Integer
        Get
            Return ComboBoxStyles.CBS_AUTOHSCROLL
        End Get
    End Property

    ''' <summary>
    ''' Converts text entered in the combo box edit control from the Windows 
    ''' character set to the OEM character set and then back to the Windows 
    ''' character set. This ensures proper character conversion when the 
    ''' application calls the CharToOem function to convert a Windows string 
    ''' in the combo box to OEM characters. This style is most useful for 
    ''' combo boxes that contain file names and applies only to combo boxes 
    ''' created with the CBS_SIMPLE or CBS_DROPDOWN style.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property CBS_OEMCONVERT() As Integer
        Get
            Return ComboBoxStyles.CBS_OEMCONVERT
        End Get
    End Property

    ''' <summary>
    ''' Automatically sorts strings added to the list box.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property CBS_SORT() As Integer
        Get
            Return ComboBoxStyles.CBS_SORT
        End Get
    End Property

    ''' <summary>
    ''' Specifies that an owner-drawn combo box contains items consisting 
    ''' of strings. The combo box maintains the memory and address for the 
    ''' strings so the application can use the CBS_GETLBTEXT message to retrieve 
    ''' the text for a particular item.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property CBS_HASSTRINGS() As Integer
        Get
            Return ComboBoxStyles.CBS_HASSTRINGS
        End Get
    End Property

    ''' <summary>
    ''' Specifies that the size of the combo box is exactly the size 
    ''' specified by the application when it created the combo box. Normally, 
    ''' the system sizes a combo box so that it does not display partial items.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property CBS_NOINTEGRALHEIGHT() As Integer
        Get
            Return ComboBoxStyles.CBS_NOINTEGRALHEIGHT
        End Get
    End Property

    ''' <summary>
    ''' Shows a disabled vertical scroll bar in the list box when the box 
    ''' does not contain enough items to scroll. Without this style, the 
    ''' scroll bar is hidden when the list box does not contain enough items.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property CBS_DISABLENOSCROLL() As Integer
        Get
            Return ComboBoxStyles.CBS_DISABLENOSCROLL
        End Get
    End Property

    ''' <summary>
    ''' Converts to uppercase all text in both the selection field and the list.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property CBS_UPPERCASE() As Integer
        Get
            Return ComboBoxStyles.CBS_UPPERCASE
        End Get
    End Property

    ''' <summary>
    ''' Converts to lowercase all text in both the selection field and the list.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property CBS_LOWERCASE() As Integer
        Get
            Return ComboBoxStyles.CBS_LOWERCASE
        End Get
    End Property
End Class
