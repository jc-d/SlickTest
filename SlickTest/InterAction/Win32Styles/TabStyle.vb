'http://msdn.microsoft.com/en-us/library/bb760549(VS.85).aspx
Imports WinAPI.API

''' <summary>
''' The following styles can be specified wherever a Tab style is required. 
''' </summary>
''' <remarks>NOTE: Some of this information was provided by Microsoft's(tm) MSDN.
''' </remarks>
Public Class TabStyle

    Protected Friend Sub New()
    End Sub

    ''' <summary>
    ''' Tests the windows object style value to see if it includes the TCS_Value.
    ''' </summary>
    ''' <param name="StyleValue">The style value returned from a windows object.</param>
    ''' <param name="TCS_Value">The TCS value you that is checked to see if it is contained in the windows object.</param>
    ''' <returns></returns>
    ''' <remarks>Example usage: StyleInfo.TabStyle.ContainsValue(Window(MyWindow).GetStyle(),StyleInfo.TabStyle.TCS_BOTTOM )
    ''' <p />This will return true if the window contains the value "TCS_Value".</remarks>
    Public Function ContainsValue(ByVal StyleValue As Int64, ByVal TCS_Value As Int64) As Boolean
        Return ((StyleValue And TCS_Value) = TCS_Value)
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
        For Each item As TabControlStyles In [Enum].GetValues(GetType(TabControlStyles))
            Try
                If (ContainsValue(StyleValue, item) = True) Then
                    Name = [Enum].GetName(GetType(TabControlStyles), item).TrimStart("n"c)
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

    ''' <summary>
    ''' Tabs appear as tabs, and a border is drawn around the display area. 
    ''' This style is the default. Same as TCS_SINGLELINE.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property TCS_TABS() As Integer
        Get
            Return TabControlStyles.TCS_SINGLELINE
        End Get
    End Property

    ''' <summary>
    ''' The width of each tab is increased, if necessary, so that each row of tabs fills 
    ''' the entire width of the tab control. This window style is ignored unless the 
    ''' TCS_MULTILINE style is also specified. Same as TCS_SINGLELINE.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property TCS_RIGHTJUSTIFY() As Integer
        Get
            Return TabControlStyles.TCS_SINGLELINE
        End Get
    End Property

    ''' <summary>
    ''' Version 4.70. Tabs appear vertically on the right side of controls that use the 
    ''' TCS_VERTICAL style. This value equals TCS_BOTTOM. This style is not supported if 
    ''' you use visual styles. Same as TCS_BOTTOM.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property TCS_RIGHT() As Integer
        Get
            Return TabControlStyles.TCS_BOTTOM
        End Get
    End Property

    ''' <summary>
    ''' Only one row of tabs is displayed. The user can scroll to see more tabs, 
    ''' if necessary. This style is the default.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property TCS_SINGLELINE() As Integer
        Get
            Return TabControlStyles.TCS_SINGLELINE
        End Get
    End Property

    ''' <summary>
    ''' Version 4.70. Unneeded tabs scroll to the opposite side of the control when a tab is selected.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property TCS_SCROLLOPPOSITE() As Integer
        Get
            Return TabControlStyles.TCS_SCROLLOPPOSITE
        End Get
    End Property

    ''' <summary>
    ''' Version 4.70. Tabs appear at the bottom of the control. This value equals 
    ''' TCS_RIGHT. This style is not supported if you use ComCtl32.dll version 6.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property TCS_BOTTOM() As Integer
        Get
            Return TabControlStyles.TCS_BOTTOM
        End Get
    End Property

    ''' <summary>
    ''' Version 4.70. Multiple tabs can be selected by holding down the CTRL 
    ''' key when clicking. This style must be used with the TCS_BUTTONS style.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property TCS_MULTISELECT() As Integer
        Get
            Return TabControlStyles.TCS_MULTISELECT
        End Get
    End Property

    ''' <summary>
    ''' Version 4.71. Selected tabs appear as being indented into the background while 
    ''' other tabs appear as being on the same plane as the background. This style 
    ''' only affects tab controls with the TCS_BUTTONS style.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property TCS_FLATBUTTONS() As Integer
        Get
            Return TabControlStyles.TCS_FLATBUTTONS
        End Get
    End Property

    ''' <summary>
    ''' Icons are aligned with the left edge of each fixed-width tab. 
    ''' This style can only be used with the TCS_FIXEDWIDTH style.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property TCS_FORCEICONLEFT() As Integer
        Get
            Return TabControlStyles.TCS_FORCEICONLEFT
        End Get
    End Property

    ''' <summary>
    ''' Labels are aligned with the left edge of each fixed-width tab; that is, the 
    ''' label is displayed immediately to the right of the icon instead of being 
    ''' centered. This style can only be used with the TCS_FIXEDWIDTH style, and 
    ''' it implies the TCS_FORCEICONLEFT style.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property TCS_FORCELABELLEFT() As Integer
        Get
            Return TabControlStyles.TCS_FORCELABELLEFT
        End Get
    End Property

    ''' <summary>
    ''' Version 4.70. Items under the pointer are automatically highlighted. You can 
    ''' check whether or not hot tracking is enabled by calling SystemParametersInfo. 
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property TCS_HOTTRACK() As Integer
        Get
            Return TabControlStyles.TCS_HOTTRACK
        End Get
    End Property

    ''' <summary>
    ''' Version 4.70. Tabs appear at the left side of the control, with tab text displayed 
    ''' vertically. This style is valid only when used with the TCS_MULTILINE style. To 
    ''' make tabs appear on the right side of the control, also use the TCS_RIGHT style. 
    ''' This style is not supported if you use ComCtl32.dll version 6.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property TCS_VERTICAL() As Integer
        Get
            Return TabControlStyles.TCS_VERTICAL
        End Get
    End Property

    ''' <summary>
    ''' Tabs appear as buttons, and no border is drawn around the display area.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property TCS_BUTTONS() As Integer
        Get
            Return TabControlStyles.TCS_BUTTONS
        End Get
    End Property

    ''' <summary>
    ''' Multiple rows of tabs are displayed, if necessary, so all tabs are visible at once.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property TCS_MULTILINE() As Integer
        Get
            Return TabControlStyles.TCS_MULTILINE
        End Get
    End Property

    ''' <summary>
    ''' All tabs are the same width. This style cannot be combined with the TCS_RIGHTJUSTIFY style.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property TCS_FIXEDWIDTH() As Integer
        Get
            Return TabControlStyles.TCS_FIXEDWIDTH
        End Get
    End Property

    ''' <summary>
    ''' Rows of tabs will not be stretched to fill the entire width of the control. This style is the default.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property TCS_RAGGEDRIGHT() As Integer
        Get
            Return TabControlStyles.TCS_RAGGEDRIGHT
        End Get
    End Property

    ''' <summary>
    ''' The tab control receives the input focus when clicked.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property TCS_FOCUSONBUTTONDOWN() As Integer
        Get
            Return TabControlStyles.TCS_FOCUSONBUTTONDOWN
        End Get
    End Property

    ''' <summary>
    ''' The parent window is responsible for drawing tabs.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property TCS_OWNERDRAWFIXED() As Integer
        Get
            Return TabControlStyles.TCS_OWNERDRAWFIXED
        End Get
    End Property

    ''' <summary>
    ''' The tab control has a tooltip control associated with it. 
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property TCS_TOOLTIPS() As Integer
        Get
            Return TabControlStyles.TCS_TOOLTIPS
        End Get
    End Property

    ''' <summary>
    ''' The tab control does not receive the input focus when clicked.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property TCS_FOCUSNEVER() As Integer
        Get
            Return TabControlStyles.TCS_FOCUSNEVER
        End Get
    End Property
End Class
