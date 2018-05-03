Imports WinAPI.API

''' <summary>
''' The following styles can be specified wherever a window style is required. 
''' </summary>
''' <remarks>NOTE: Some of this information was provided by Microsoft's(tm) MSDN.</remarks>
Public Class StyleExtendedInfo

    Protected Friend Sub New()

    End Sub

    ''' <summary>
    ''' Tests the windows object style value to see if it includes the WS_Value.
    ''' </summary>
    ''' <param name="StyleValue">The style value returned from a windows object.</param>
    ''' <param name="WS_Value">The WS value you that is checked to see if it is contained in the windows object.</param>
    ''' <returns></returns>
    ''' <remarks>Example usage: StyleExtendedInfo.ContainsValue(Window(MyWindow).GetStyleEx(),StyleExtendedInfo.WS_EX_TOPMOST)
    ''' <p />This will return true if the window contains the value "WS_EX_TOPMOST" (the internal windows
    ''' value that control's the object's topmost property).</remarks>
    Public Function ContainsValue(ByVal StyleValue As Int64, ByVal WS_Value As Int64) As Boolean
        Return ((StyleValue And WS_Value) = WS_Value)
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
        For Each item As WS_EX In [Enum].GetValues(GetType(WS_EX))
            Try
                If (ContainsValue(StyleValue, Convert.ToInt32(item)) = True) Then
                    Name = [Enum].GetName(GetType(WS_EX), item)
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
    ''' Specifies that a window created with this style accepts drag-drop files.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property WS_EX_ACCEPTFILES() As Integer
        Get
            Return Convert.ToInt32(WS_EX.WS_EX_ACCEPTFILES)
        End Get
    End Property

    ''' <summary>
    ''' Forces a top-level window onto the taskbar when the window is visible.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property WS_EX_APPWINDOW() As Integer
        Get
            Return Convert.ToInt32(WS_EX.WS_EX_APPWINDOW)
        End Get
    End Property

    ''' <summary>
    ''' Specifies that a window has a border with a sunken edge.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property WS_EX_CLIENTEDGE() As Integer
        Get
            Return Convert.ToInt32(WS_EX.WS_EX_CLIENTEDGE)
        End Get
    End Property

    ''' <summary>
    ''' <B>Windows XP</B>: Paints all descendants of a window in bottom-to-top painting order 
    ''' using double-buffering. For more information, see Remarks. This cannot be used 
    ''' if the window has a class style of either CS_OWNDC or CS_CLASSDC.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property WS_EX_COMPOSITED() As Integer
        Get
            Return Convert.ToInt32(WS_EX.WS_EX_COMPOSITED)
        End Get
    End Property

    ''' <summary>
    ''' Includes a question mark in the title bar of the window. When the user clicks the question mark,
    ''' the cursor changes to a question mark with a pointer. If the user then clicks a child window,
    '''  the child receives a WM_HELP message. The child window should pass the message to the parent
    '''  window procedure, which should call the WinHelp function using the HELP_WM_HELP command.
    '''  The Help application displays a pop-up window that typically contains help for the child window.
    ''' <p/>WS_EX_CONTEXTHELP cannot be used with the WS_MAXIMIZEBOX or WS_MINIMIZEBOX styles.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property WS_EX_CONTEXTHELP() As Integer
        Get
            Return Convert.ToInt32(WS_EX.WS_EX_CONTEXTHELP)
        End Get
    End Property

    ''' <summary>
    ''' The window itself contains child windows that should take part in dialog box navigation. 
    ''' If this style is specified, the dialog manager recurses into children of this window when
    ''' performing navigation operations such as handling the TAB key, an arrow key, or a keyboard
    ''' mnemonic.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property WS_EX_CONTROLPARENT() As Integer
        Get
            Return Convert.ToInt32(WS_EX.WS_EX_CONTROLPARENT)
        End Get
    End Property

    ''' <summary>
    ''' Creates a window that has a double border; the window can, 
    ''' optionally, be created with a title bar by specifying the WS_CAPTION 
    ''' style in the dwStyle parameter.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property WS_EX_DLGMODALFRAME() As Integer
        Get
            Return Convert.ToInt32(WS_EX.WS_EX_DLGMODALFRAME)
        End Get
    End Property

    ''' <summary>
    ''' <B>Windows 2000/XP</B>: Creates a layered window. Note that this cannot be used for 
    ''' child windoWS_EX.WS_ Also, this cannot be used if the window has a class style of 
    ''' either CS_OWNDC or CS_CLASSDC.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property WS_EX_LAYERED() As Integer
        Get
            Return Convert.ToInt32(WS_EX.WS_EX_LAYERED)
        End Get
    End Property

    ''' <summary>
    ''' <B>Arabic and Hebrew versions of Windows 98/Me, Windows 2000/XP</B>: Creates a window whose 
    ''' horizontal origin is on the right edge. Increasing horizontal values advance to the left.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property WS_EX_LAYOUTRTL() As Integer
        Get
            Return Convert.ToInt32(WS_EX.WS_EX_LAYOUTRTL)
        End Get
    End Property

    ''' <summary>
    ''' Creates a window that has generic left-aligned properties. This is the default.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property WS_EX_LEFT() As Integer
        Get
            Return Convert.ToInt32(WS_EX.WS_EX_LEFT)
        End Get
    End Property

    ''' <summary>
    ''' If the shell language is Hebrew, Arabic, or another language that supports reading order 
    ''' alignment, the vertical scroll bar (if present) is to the left of the client area. For other 
    ''' languages, the style is ignored.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property WS_EX_LEFTSCROLLBAR() As Integer
        Get
            Return Convert.ToInt32(WS_EX.WS_EX_LEFTSCROLLBAR)
        End Get
    End Property

    ''' <summary>
    ''' The window text is displayed using left-to-right reading-order properties. 
    ''' This is the default.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property WS_EX_LTRREADING() As Integer
        Get
            Return Convert.ToInt32(WS_EX.WS_EX_LTRREADING)
        End Get
    End Property

    ''' <summary>
    ''' Creates a multiple-document interface (MDI) child window.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property WS_EX_MDICHILD() As Integer
        Get
            Return Convert.ToInt32(WS_EX.WS_EX_MDICHILD)
        End Get
    End Property

    ''' <summary>
    ''' <B>Windows 2000/XP</B>: A top-level window created with this style does not become the
    '''  foreground window when the user clicks it. The system does not bring this
    '''  window to the foreground when the user minimizes or closes the foreground window. 
    ''' To activate the window, use the SetActiveWindow or SetForegroundWindow function.
    ''' The window does not appear on the taskbar by default. To force the window to appear on 
    ''' the taskbar, use the WS_EX_APPWINDOW style.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property WS_EX_NOACTIVATE() As Integer
        Get
            Return Convert.ToInt32(WS_EX.WS_EX_NOACTIVATE)
        End Get
    End Property

    ''' <summary>
    ''' <B>Windows 2000/XP</B>: A window created with this style does not pass its 
    ''' window layout to its child windoWS_EX.WS_
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property WS_EX_NOINHERITLAYOUT() As Integer
        Get
            Return Convert.ToInt32(WS_EX.WS_EX_NOINHERITLAYOUT)
        End Get
    End Property

    ''' <summary>
    ''' Specifies that a child window created with this style does not send the 
    ''' WM_PARENTNOTIFY message to its parent window when it is created or destroyed.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property WS_EX_NOPARENTNOTIFY() As Integer
        Get
            Return Convert.ToInt32(WS_EX.WS_EX_NOPARENTNOTIFY)
        End Get
    End Property

    ''' <summary>
    ''' Combines the WS_EX_CLIENTEDGE and WS_EX_WINDOWEDGE styles.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property WS_EX_OVERLAPPEDWINDOW() As Integer
        Get
            Return Convert.ToInt32(WS_EX.WS_EX_OVERLAPPEDWINDOW)
        End Get
    End Property

    ''' <summary>
    ''' Combines the WS_EX_WINDOWEDGE, WS_EX_TOOLWINDOW, and WS_EX_TOPMOST styles.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property WS_EX_PALETTEWINDOW() As Integer
        Get
            Return Convert.ToInt32(WS_EX.WS_EX_PALETTEWINDOW)
        End Get
    End Property

    ''' <summary>
    ''' The window has generic "right-aligned" properties. This depends on the window 
    ''' class. This style has an effect only if the shell language is Hebrew, Arabic, or
    '''  another language that supports reading-order alignment; otherwise, the style is 
    ''' ignored.
    ''' Using the WS_EX_RIGHT style for static or edit controls has the same effect as using the 
    ''' SS_RIGHT or ES_RIGHT style, respectively. Using this style with button controls has the 
    ''' same effect as using BS_RIGHT and BS_RIGHTBUTTON styles.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property WS_EX_RIGHT() As Integer
        Get
            Return Convert.ToInt32(WS_EX.WS_EX_RIGHT)
        End Get
    End Property

    ''' <summary>
    ''' Vertical scroll bar (if present) is to the right of the client area. 
    ''' This is the default.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property WS_EX_RIGHTSCROLLBAR() As Integer
        Get
            Return Convert.ToInt32(WS_EX.WS_EX_RIGHTSCROLLBAR)
        End Get
    End Property

    ''' <summary>
    ''' If the shell language is Hebrew, Arabic, or another language that 
    ''' supports reading-order alignment, the window text is displayed using 
    ''' right-to-left reading-order properties. For other languages, the style 
    ''' is ignored.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property WS_EX_RTLREADING() As Integer
        Get
            Return Convert.ToInt32(WS_EX.WS_EX_RTLREADING)
        End Get
    End Property

    ''' <summary>
    ''' Creates a window with a three-dimensional border style intended to be used 
    ''' for items that do not accept user input.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property WS_EX_STATICEDGE() As Integer
        Get
            Return Convert.ToInt32(WS_EX.WS_EX_STATICEDGE)
        End Get
    End Property

    ''' <summary>
    ''' Creates a tool window; that is, a window intended to be used as a floating toolbar. 
    ''' A tool window has a title bar that is shorter than a normal title bar, and the window 
    ''' title is drawn using a smaller font. A tool window does not appear in the taskbar or 
    ''' in the dialog that appears when the user presses ALT+TAB. If a tool window has a system menu, 
    ''' its icon is not displayed on the title bar. However, you can display the system menu by 
    ''' right-clicking or by typing ALT+SPACE.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property WS_EX_TOOLWINDOW() As Integer
        Get
            Return Convert.ToInt32(WS_EX.WS_EX_TOOLWINDOW)
        End Get
    End Property

    ''' <summary>
    ''' Specifies that a window created with this style should be placed above 
    ''' all non-topmost windows and should stay above them, even when the window is 
    ''' deactivated. To add or remove this style, use the SetWindowPos function.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property WS_EX_TOPMOST() As Integer
        Get
            Return Convert.ToInt32(WS_EX.WS_EX_TOPMOST)
        End Get
    End Property

    ''' <summary>
    ''' Specifies that a window created with this style should not be painted until siblings beneath 
    ''' the window (that were created by the same thread) have been painted. The window appears 
    ''' transparent because the bits of underlying sibling windows have already been painted.
    ''' To achieve transparency without these restrictions, use the SetWindowRgn function.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property WS_EX_TRANSPARENT() As Integer
        Get
            Return Convert.ToInt32(WS_EX.WS_EX_TRANSPARENT)
        End Get
    End Property

    ''' <summary>
    ''' Specifies that a window has a border with a raised edge.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property WS_EX_WINDOWEDGE() As Integer
        Get
            Return Convert.ToInt32(WS_EX.WS_EX_WINDOWEDGE)
        End Get
    End Property

End Class
