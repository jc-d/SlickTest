Imports WinAPI.API
'http://msdn.microsoft.com/en-us/library/bb775149(VS.85).aspx

''' <summary>
''' The following styles can be specified wherever a ListBox style is required. 
''' </summary>
''' <remarks>NOTE: Some of this information was provided by Microsoft's(tm) MSDN.
''' </remarks>
Public Class ListBoxStyle

    Protected Friend Sub New()
    End Sub

    ''' <summary>
    ''' Tests the windows object style value to see if it includes the LBS_Value.
    ''' </summary>
    ''' <param name="StyleValue">The style value returned from a windows object.</param>
    ''' <param name="LBS_Value">The LBS value you that is checked to see if it is contained in the windows object.</param>
    ''' <returns></returns>
    ''' <remarks>Example usage: StyleInfo.ListBoxStyle.ContainsValue(Window(MyWindow).GetStyle(),StyleInfo.ListBoxStyle.LBS_SORT)
    ''' <p />This will return true if the window contains the value "LBS_Value".</remarks>
    Public Function ContainsValue(ByVal StyleValue As Int64, ByVal LBS_Value As Int64) As Boolean
        Return ((StyleValue And LBS_Value) = LBS_Value)
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
        For Each item As ListBoxStyles In [Enum].GetValues(GetType(ListBoxStyles))
            Try
                If (ContainsValue(StyleValue, item) = True) Then
                    Name = [Enum].GetName(GetType(ListBoxStyles), item).TrimStart("n"c)
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
    ''' Notifies the parent window with an input message whenever the 
    ''' user clicks or double-clicks a string in the list box.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property LBS_NOTIFY() As Integer
        Get
            Return Convert.ToInt32(ListBoxStyles.LBS_NOTIFY)
        End Get
    End Property

    ''' <summary>
    ''' Sorts strings in the list box alphabetically.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property LBS_SORT() As Integer
        Get
            Return Convert.ToInt32(ListBoxStyles.LBS_SORT)
        End Get
    End Property

    ''' <summary>
    ''' Specifies that the list box's appearance is not updated when changes are made.
    ''' To change the redraw state of the control, use the WM_SETREDRAW message.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property LBS_NOREDRAW() As Integer
        Get
            Return Convert.ToInt32(ListBoxStyles.LBS_NOREDRAW)
        End Get
    End Property

    ''' <summary>
    ''' Turns string selection on or off each time the user clicks or double-clicks 
    ''' a string in the list box. The user can select any number of strings.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property LBS_MULTIPLESEL() As Integer
        Get
            Return Convert.ToInt32(ListBoxStyles.LBS_MULTIPLESEL)
        End Get
    End Property

    ''' <summary>
    ''' Specifies that the owner of the list box is responsible for drawing its contents 
    ''' and that the items in the list box are the same height. The owner window receives a 
    ''' WM_MEASUREITEM message when the list box is created and a WM_DRAWITEM message when a 
    ''' visual aspect of the list box has changed.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property LBS_OWNERDRAWFIXED() As Integer
        Get
            Return Convert.ToInt32(ListBoxStyles.LBS_OWNERDRAWFIXED)
        End Get
    End Property

    ''' <summary>
    ''' Specifies that the owner of the list box is responsible for drawing its 
    ''' contents and that the items in the list box are variable in height. The 
    ''' owner window receives a WM_MEASUREITEM message for each item in the combo 
    ''' box when the combo box is created and a WM_DRAWITEM message when a visual 
    ''' aspect of the combo box has changed.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property LBS_OWNERDRAWVARIABLE() As Integer
        Get
            Return Convert.ToInt32(ListBoxStyles.LBS_OWNERDRAWVARIABLE)
        End Get
    End Property

    ''' <summary>
    ''' Specifies that a list box contains items consisting of strings. The list 
    ''' box maintains the memory and addresses for the strings so that the application 
    ''' can use the LB_GETTEXT message to retrieve the text for a particular item. By default, 
    ''' all list boxes except owner-drawn list boxes have this style. You can create an owner-drawn 
    ''' list box either with or without this style.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property LBS_HASSTRINGS() As Integer
        Get
            Return Convert.ToInt32(ListBoxStyles.LBS_HASSTRINGS)
        End Get
    End Property

    ''' <summary>
    ''' Enables a list box to recognize and expand tab characters when drawing its strings. 
    ''' You can use the LB_SETTABSTOPS message to specify tab stop positions. The default 
    ''' tab positions are 32 dialog template units apart. Dialog template units are the 
    ''' device-independent units used in dialog box templates. To convert measurements from 
    ''' dialog template units to screen units (pixels), use the MapDialogRect function.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property LBS_USETABSTOPS() As Integer
        Get
            Return Convert.ToInt32(ListBoxStyles.LBS_USETABSTOPS)
        End Get
    End Property

    ''' <summary>
    ''' Specifies that the size of the list box is exactly the size specified by the 
    ''' application when it created the list box. Normally, the system sizes a list 
    ''' box so that the list box does not display partial items.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property LBS_NOINTEGRALHEIGHT() As Integer
        Get
            Return Convert.ToInt32(ListBoxStyles.LBS_NOINTEGRALHEIGHT)
        End Get
    End Property

    ''' <summary>
    ''' Specifies a multi-columnn list box that is scrolled horizontally. 
    ''' The LB_SETCOLUMNWIDTH message sets the width of the columns.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property LBS_MULTICOLUMN() As Integer
        Get
            Return Convert.ToInt32(ListBoxStyles.LBS_MULTICOLUMN)
        End Get
    End Property

    ''' <summary>
    ''' Specifies that the owner of the list box receives WM_VKEYTOITEM messages whenever 
    ''' the user presses a key and the list box has the input focus. This enables an application 
    ''' to perform special processing on the keyboard input.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property LBS_WANTKEYBOARDINPUT() As Integer
        Get
            Return Convert.ToInt32(ListBoxStyles.LBS_WANTKEYBOARDINPUT)
        End Get
    End Property

    ''' <summary>
    ''' Allows multiple items to be selected by using the SHIFT 
    ''' key and the mouse or special key combinations.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property LBS_EXTENDEDSEL() As Integer
        Get
            Return Convert.ToInt32(ListBoxStyles.LBS_EXTENDEDSEL)
        End Get
    End Property

    ''' <summary>
    ''' Shows a disabled vertical scroll bar for the list box when the box does not 
    ''' contain enough items to scroll. If you do not specify this style, the scroll 
    ''' bar is hidden when the list box does not contain enough items.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property LBS_DISABLENOSCROLL() As Integer
        Get
            Return Convert.ToInt32(ListBoxStyles.LBS_DISABLENOSCROLL)
        End Get
    End Property

    ''' <summary>
    ''' Specifies a no-data list box. Specify this style when the count of items in the list 
    ''' box will exceed one thousand. A no-data list box must also have the LBS_OWNERDRAWFIXED 
    ''' style, but must not have the LBS_SORT or LBS_HASSTRINGS style. 
    ''' A no-data list box resembles an owner-drawn list box except that it contains 
    ''' no string or bitmap data for an item. Commands to add, insert, or delete an item always 
    ''' ignore any specified item data; requests to find a string within the list box always fail. 
    ''' The system sends the WM_DRAWITEM message to the owner window when an item must be drawn. 
    ''' The itemID member of the DRAWITEMSTRUCT structure passed with the WM_DRAWITEM message 
    ''' specifies the line number of the item to be drawn. A no-data list box does not 
    ''' send a WM_DELETEITEM message.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property LBS_NODATA() As Integer
        Get
            Return Convert.ToInt32(ListBoxStyles.LBS_NODATA)
        End Get
    End Property

    ''' <summary>
    ''' Specifies that the list box contains items that can be viewed but not selected. 
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property LBS_NOSEL() As Integer
        Get
            Return Convert.ToInt32(ListBoxStyles.LBS_NOSEL)
        End Get
    End Property

    ''' <summary>
    ''' Sorts strings in the list box alphabetically. The parent window receives an input 
    ''' message whenever the user clicks or double-clicks a string. The list box has borders 
    ''' on all sides.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property LBS_STANDARD() As Integer
        Get
            Return Convert.ToInt32(ListBoxStyles.LBS_STANDARD)
        End Get
    End Property

End Class
