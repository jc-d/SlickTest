Imports WinAPI.API
''' <summary>
''' The following styles can be specified wherever a window style is required. 
''' After the control has been created, these styles cannot be modified, except as noted.
''' <p/><B>WARNING:</B> Most style information for SWF (.net) windows uncertain as the windows
''' are managed and may have different behavior than normal windows.  It is recommended you
''' only use this for Win32 objects.
''' </summary>
''' <remarks>NOTE: Some of this information was provided by Microsoft's(tm) MSDN.</remarks>
Public Class StyleInfo

    Protected Friend Sub New()
    End Sub

    Private Shared ButtonStyleInfo As New ButtonStyle()
    Private Shared TextBoxStyleInfo As New TextBoxStyle()
    Private Shared ComboBoxStyleInfo As New ComboBoxStyle()
    Private Shared TreeViewStyleInfo As New TreeViewStyle()
    Private Shared ListViewStyleInfo As New ListViewStyle()
    Private Shared TabStyleInfo As New TabStyle()
    Private Shared ListBoxStyleInfo As New ListBoxStyle()

    ''' <summary>
    ''' Provides style information for list box.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared ReadOnly Property ListBoxStyle() As ListBoxStyle
        Get
            Return ListBoxStyleInfo
        End Get
    End Property


    ''' <summary>
    ''' Provides style information for tabs.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared ReadOnly Property TabStyle() As TabStyle
        Get
            Return TabStyleInfo
        End Get
    End Property

    ''' <summary>
    ''' Provides style information for list views.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared ReadOnly Property ListViewStyle() As ListViewStyle
        Get
            Return ListViewStyleInfo
        End Get
    End Property

    ''' <summary>
    ''' Provides style information for tree views.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared ReadOnly Property TreeViewStyle() As TreeViewStyle
        Get
            Return TreeViewStyleInfo
        End Get
    End Property

    ''' <summary>
    ''' Provides style information for combo boxes.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared ReadOnly Property ComboBoxStyle() As ComboBoxStyle
        Get
            Return ComboBoxStyleInfo
        End Get
    End Property

    ''' <summary>
    ''' Provides style information for textboxes.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared ReadOnly Property TextBoxStyle() As TextBoxStyle
        Get
            Return TextBoxStyleInfo
        End Get
    End Property

    ''' <summary>
    ''' Provides style information for button.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared ReadOnly Property ButtonStyle() As ButtonStyle
        Get
            Return ButtonStyleInfo
        End Get
    End Property

    ''' <summary>
    ''' Provides style information for checkbox.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared ReadOnly Property CheckBoxStyle() As ButtonStyle
        Get
            Return ButtonStyle
        End Get
    End Property

    ''' <summary>
    ''' Provides style information for radiobutton.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared ReadOnly Property RadioButtonStyle() As ButtonStyle
        Get
            Return ButtonStyle
        End Get
    End Property

    ''' <summary>
    ''' Creates a window that has a thin-line border.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property WS_BORDER() As Integer
        Get
            Return Convert.ToInt32(WS.WS_BORDER)
        End Get
    End Property

    ''' <summary>
    ''' Creates a window that has a title bar (includes the WS_BORDER style).
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property WS_CAPTION() As Integer
        Get
            Return Convert.ToInt32(WS.WS_CAPTION)
        End Get
    End Property

    ''' <summary>
    ''' Creates a child window. A window with this style cannot have a menu bar. 
    ''' This style cannot be used with the WS_POPUP style.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property WS_CHILD() As Integer
        Get
            Return Convert.ToInt32(WS.WS_CHILD)
        End Get
    End Property

    ''' <summary>
    ''' Same as the WS_CHILD style.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property WS_CHILDWINDOW() As Integer
        Get
            Return Convert.ToInt32(WS.WS_CHILDWINDOW)
        End Get
    End Property

    ''' <summary>
    ''' Excludes the area occupied by child windows when drawing occurs within the 
    ''' parent window. This style is used when creating the parent window.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property WS_CLIPCHILDREN() As Integer
        Get
            Return Convert.ToInt32(WS.WS_CLIPCHILDREN)
        End Get
    End Property

    ''' <summary>
    ''' Clips child windows relative to each other; that is, when a particular child 
    ''' window receives a WM_PAINT message, the WS_CLIPSIBLINGS style clips all other 
    ''' overlapping child windows out of the region of the child window to be updated. 
    ''' If WS_CLIPSIBLINGS is not specified and child windows overlap, it is possible, 
    ''' when drawing within the client area of a child window, to draw within the client 
    ''' area of a neighboring child window.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property WS_CLIPSIBLINGS() As Integer
        Get
            Return Convert.ToInt32(WS.WS_CLIPSIBLINGS)
        End Get
    End Property

    ''' <summary>
    ''' Creates a window that is initially disabled. A disabled window cannot receive input from the user. 
    ''' To change this after a window has been created, use EnableWindow.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property WS_DISABLED() As Integer
        Get
            Return Convert.ToInt32(WS.WS_DISABLED)
        End Get
    End Property

    ''' <summary>
    ''' Creates a window that has a border of a style typically used 
    ''' with dialog boxes. A window with this style cannot have a title bar.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property WS_DLGFRAME() As Integer
        Get
            Return Convert.ToInt32(WS.WS_DLGFRAME)
        End Get
    End Property

    ''' <summary>
    ''' Specifies the first control of a group of controls. The group consists of this first control and 
    ''' all controls defined after it, up to the next control with the WS_GROUP style. The first control in 
    ''' each group usually has the WS_TABSTOP style so that the user can move from group to group. 
    ''' The user can subsequently change the keyboard focus from one control in the group to the next 
    ''' control in the group by using the direction keys.You can turn this style on and off to change 
    ''' dialog box navigation. To change this style after a window has been created, use SetWindowLong.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property WS_GROUP() As Integer
        Get
            Return Convert.ToInt32(WS.WS_GROUP)
        End Get
    End Property

    ''' <summary>
    ''' Creates a window that has a horizontal scroll bar.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property WS_HSCROLL() As Integer
        Get
            Return Convert.ToInt32(WS.WS_HSCROLL)
        End Get
    End Property

    ''' <summary>
    ''' Creates a window that is initially minimized. Same as the WS_MINIMIZE style.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property WS_ICONIC() As Integer
        Get
            Return Convert.ToInt32(WS.WS_ICONIC)
        End Get
    End Property

    ''' <summary>
    ''' Creates a window that is initially maximized.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property WS_MAXIMIZE() As Integer
        Get
            Return Convert.ToInt32(WS.WS_MAXIMIZE)
        End Get
    End Property

    ''' <summary>
    ''' Creates a window that has a maximize button. Cannot be combined with 
    ''' the WS_EX_CONTEXTHELP style. The WS_SYSMENU style must also be specified.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property WS_MAXIMIZEBOX() As Integer
        Get
            Return Convert.ToInt32(WS.WS_MAXIMIZEBOX)
        End Get
    End Property

    ''' <summary>
    ''' Creates a window that is initially minimized. Same as the WS_ICONIC style.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property WS_MINIMIZE() As Integer
        Get
            Return Convert.ToInt32(WS.WS_MINIMIZE)
        End Get
    End Property

    ''' <summary>
    ''' Creates a window that has a minimize button. Cannot be combined with the 
    ''' WS_EX_CONTEXTHELP style. The WS_SYSMENU style must also be specified.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property WS_MINIMIZEBOX() As Integer
        Get
            Return Convert.ToInt32(WS.WS_MINIMIZEBOX)
        End Get
    End Property

    ''' <summary>
    ''' Creates an overlapped window. An overlapped window has a title bar and a border.
    '''  Same as the WS_TILED style.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property WS_OVERLAPPED() As Integer
        Get
            Return Convert.ToInt32(WS.WS_OVERLAPPED)
        End Get
    End Property

    ''' <summary>
    ''' Creates an overlapped window with the WS_OVERLAPPED, 
    ''' WS_CAPTION, WS_SYSMENU, WS_THICKFRAME, WS_MINIMIZEBOX, and WS_MAXIMIZEBOX 
    ''' styles. Same as the WS_TILEDWINDOW style.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property WS_OVERLAPPEDWINDOW() As Integer
        Get
            Return Convert.ToInt32(WS.WS_OVERLAPPEDWINDOW)
        End Get
    End Property

    ''' <summary>
    ''' Creates a pop-up window. This style cannot be used with the WS_CHILD style.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks>WARNING: This property may not correctly function with other automation functionality.</remarks>
    Public ReadOnly Property WS_POPUP() As Long
        Get
            Return WS.WS_POPUP
        End Get
    End Property

    ''' <summary>
    ''' Creates a pop-up window with WS_BORDER, WS_POPUP, and WS_SYSMENU styles. 
    ''' The WS_CAPTION and WS_POPUPWINDOW styles must be combined to make the window menu visible.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks>WARNING: This property may not correctly function with other automation functionality.</remarks>
    Public ReadOnly Property WS_POPUPWINDOW() As Long
        Get
            Return WS.WS_POPUPWINDOW
        End Get
    End Property

    ''' <summary>
    ''' Creates a window that has a sizing border. Same as the WS_THICKFRAME style.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property WS_SIZEBOX() As Integer
        Get
            Return Convert.ToInt32(WS.WS_SIZEBOX)
        End Get
    End Property

    ''' <summary>
    ''' Creates a window that has a window menu on its title bar. The WS_CAPTION 
    ''' style must also be specified.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property WS_SYSMENU() As Integer
        Get
            Return Convert.ToInt32(WS.WS_SYSMENU)
        End Get
    End Property

    ''' <summary>
    ''' Specifies a control that can receive the keyboard focus when the user presses the TAB key.
    '''  Pressing the TAB key changes the keyboard focus to the next control with the WS_TABSTOP style. 
    ''' You can turn this style on and off to change dialog box navigation. To change this style after a 
    ''' window has been created, use SetWindowLong.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property WS_TABSTOP() As Integer
        Get
            Return Convert.ToInt32(WS.WS_TABSTOP)
        End Get
    End Property

    ''' <summary>
    ''' Creates a window that has a sizing border. Same as the WS_SIZEBOX style.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property WS_THICKFRAME() As Integer
        Get
            Return Convert.ToInt32(WS.WS_THICKFRAME)
        End Get
    End Property

    ''' <summary>
    ''' Creates an overlapped window. An overlapped window 
    ''' has a title bar and a border. Same as the WS_OVERLAPPED style.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property WS_TILED() As Integer
        Get
            Return Convert.ToInt32(WS.WS_TILED)
        End Get
    End Property

    ''' <summary>
    ''' Creates an overlapped window with the WS_OVERLAPPED, WS_CAPTION, WS_SYSMENU, 
    ''' WS_THICKFRAME, WS_MINIMIZEBOX, and WS_MAXIMIZEBOX styles. Same as the 
    ''' WS_OVERLAPPEDWINDOW style.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property WS_TILEDWINDOW() As Integer
        Get
            Return Convert.ToInt32(WS.WS_TILEDWINDOW)
        End Get
    End Property

    ''' <summary>
    ''' Creates a window that is initially visible.
    ''' This style can be turned on and off by using ShowWindow or SetWindowPos.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property WS_VISIBLE() As Integer
        Get
            Return Convert.ToInt32(WS.WS_VISIBLE)
        End Get
    End Property

    ''' <summary>
    ''' Creates a window that has a vertical scroll bar.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property WS_VSCROLL() As Integer
        Get
            Return Convert.ToInt32(WS.WS_VSCROLL)
        End Get
    End Property

    ''' <summary>
    ''' Tests the windows object style value to see if it includes the WS_Value.
    ''' </summary>
    ''' <param name="StyleValue">The style value returned from a windows object.</param>
    ''' <param name="WS_Value">The WS value you that is checked to see if it is contained in the windows object.</param>
    ''' <returns></returns>
    ''' <remarks>Example usage: StyleInfo.ContainsValue(Window(MyWindow).GetStyle(),StyleInfo.WS_VISIBLE)
    ''' <p />This will return true if the window contains the value "WS_VISIBLE" (the internal windows
    ''' value that control's the object's visibility).</remarks>
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
        For Each item As WS In [Enum].GetValues(GetType(WS))
            Try
                If (ContainsValue(StyleValue, Convert.ToInt32(item)) = True) Then
                    Name = [Enum].GetName(GetType(WS), item)
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
