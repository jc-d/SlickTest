Imports WinAPI.API

''' <summary>
''' The following styles can be specified wherever a TreeView style is required. 
''' </summary>
''' <remarks>NOTE: Some of this information was provided by Microsoft's(tm) MSDN.</remarks>
Public Class TreeViewStyle

    Protected Friend Sub New()
    End Sub

    ''' <summary>
    ''' Tests the windows object style value to see if it includes the TVS_Value.
    ''' </summary>
    ''' <param name="StyleValue">The style value returned from a windows object.</param>
    ''' <param name="TVS_Value">The TVS value you that is checked to see if it is contained in the windows object.</param>
    ''' <returns></returns>
    ''' <remarks>Example usage: StyleInfo.TreeViewStyle.ContainsValue(Window(MyWindow).GetStyle(),StyleInfo.TreeViewStyle.TVS_HASBUTTONS)
    ''' <p />This will return true if the window contains the value "TVS_HASBUTTONS".</remarks>
    Public Function ContainsValue(ByVal StyleValue As Int64, ByVal TVS_Value As Int64) As Boolean
        Return ((StyleValue And TVS_Value) = TVS_Value)
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
        For Each item As TreeViewStyles In [Enum].GetValues(GetType(TreeViewStyles))
            Try
                If (ContainsValue(StyleValue, item) = True) Then
                    Name = [Enum].GetName(GetType(TreeViewStyles), item).TrimStart("n"c)
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
    '    For Each item As TreeViewStyles In [Enum].GetValues(GetType(TreeViewStyles))
    '        temp = String.Empty
    '        Name = [Enum].GetName(GetType(TreeViewStyles), item)
    '        temp = Property_txt & "" & Name.TrimStart("n"c).ToUpper() & "() As Integer" & vbNewLine & _
    '        "Get" & vbNewLine & _
    '        "Return " & "TreeViewStyles" & "." & Name.ToUpper() & vbNewLine & _
    '        "End Get" & vbNewLine & _
    '        "End Property"
    '        RetVal += temp & vbNewLine & vbNewLine
    '    Next
    '    System.Windows.Forms.Clipboard.SetText(RetVal)
    '    Return RetVal
    'End Function

    ''' <summary>
    ''' Displays plus (+) and minus (-) buttons next to parent items. 
    ''' The user clicks the buttons to expand or collapse a parent item's 
    ''' list of child items. To include buttons with items at the root of 
    ''' the tree view, TVS_LINESATROOT must also be specified. 
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property TVS_HASBUTTONS() As Integer
        Get
            Return TreeViewStyles.TVS_HASBUTTONS
        End Get
    End Property

    ''' <summary>
    ''' Uses lines to show the hierarchy of items. 
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property TVS_HASLINES() As Integer
        Get
            Return TreeViewStyles.TVS_HASLINES
        End Get
    End Property

    ''' <summary>
    ''' Uses lines to link items at the root of the tree-view control. 
    ''' This value is ignored if TVS_HASLINES is not also specified. 
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property TVS_LINESATROOT() As Integer
        Get
            Return TreeViewStyles.TVS_LINESATROOT
        End Get
    End Property

    ''' <summary>
    ''' Allows the user to edit the labels of tree-view items. 
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property TVS_EDITLABELS() As Integer
        Get
            Return TreeViewStyles.TVS_EDITLABELS
        End Get
    End Property

    ''' <summary>
    ''' Prevents the tree-view control from sending TVN_BEGINDRAG 
    ''' notification messages. 
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property TVS_DISABLEDRAGDROP() As Integer
        Get
            Return TreeViewStyles.TVS_DISABLEDRAGDROP
        End Get
    End Property

    ''' <summary>
    ''' Causes a selected item to remain selected when the tree-view 
    ''' control loses focus. 
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property TVS_SHOWSELALWAYS() As Integer
        Get
            Return TreeViewStyles.TVS_SHOWSELALWAYS
        End Get
    End Property

    ''' <summary>
    ''' Version 4.70. Causes text to be displayed from right-to-left (RTL). 
    ''' Usually, windows display text left-to-right (LTR). Windows can be 
    ''' mirrored to display languages such as Hebrew or Arabic that read 
    ''' RTL. Typically, tree-view text is displayed in the same direction 
    ''' as the text in its parent window. If TVS_RTLREADING is set, 
    ''' tree-view text reads in the opposite direction from the text in 
    ''' the parent window. 
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property TVS_RTLREADING() As Integer
        Get
            Return TreeViewStyles.TVS_RTLREADING
        End Get
    End Property

    ''' <summary>
    ''' Version 4.70. Disables tooltips. 
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property TVS_NOTOOLTIPS() As Integer
        Get
            Return TreeViewStyles.TVS_NOTOOLTIPS
        End Get
    End Property

    ''' <summary>
    ''' Version 4.70. Enables check boxes for items in a tree-view control. 
    ''' A check box is displayed only if an image is associated with the item. 
    ''' When set to this style, the control effectively uses DrawFrameControl 
    ''' to create and set a state image list containing two images. State 
    ''' image 1 is the unchecked box and state image 2 is the checked box. 
    ''' Setting the state image to zero removes the check box altogether. 
    ''' For more information, see Working with state image indexes. 
    ''' 
    ''' Version 5.80. Displays a check box even if no image is associated with 
    ''' the item.
    ''' 
    ''' Once a tree-view control is created with this style, the style cannot 
    ''' be removed. Instead, you must destroy the control and create a new one 
    ''' in its place. Destroying the tree-view control does not destroy the 
    ''' check box state image list. You must destroy it explicitly. Get the 
    ''' handle to the state image list by sending the tree-view control a 
    ''' TVM_GETIMAGELIST message. Then destroy the image list with 
    ''' ImageList_Destroy.
    ''' 
    ''' If you want to use this style, you must set the TVS_CHECKBOXES 
    ''' style with SetWindowLong after you create the treeview control, 
    ''' and before you populate the tree. Otherwise, the checkboxes might 
    ''' appear unchecked, depending on timing issues.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property TVS_CHECKBOXES() As Integer
        Get
            Return TreeViewStyles.TVS_CHECKBOXES
        End Get
    End Property

    ''' <summary>
    ''' Version 4.70. Enables hot tracking in a tree-view control. 
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property TVS_TRACKSELECT() As Integer
        Get
            Return TreeViewStyles.TVS_TRACKSELECT
        End Get
    End Property

    ''' <summary>
    ''' Version 4.71. Causes the item being selected to expand and the item being 
    ''' unselected to collapse upon selection in the tree view. If the mouse is 
    ''' used to single-click the selected item and that item is closed, it will 
    ''' be expanded. If the user holds down the CTRL key while selecting an item, 
    ''' the item being unselected will not be collapsed. 
    ''' 
    ''' Version 5.80. Causes the item being selected to expand and the item being 
    ''' unselected to collapse upon selection in the tree view. If the user holds 
    ''' down the CTRL key while selecting an item, the item being unselected will 
    ''' not be collapsed.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property TVS_SINGLEEXPAND() As Integer
        Get
            Return TreeViewStyles.TVS_SINGLEEXPAND
        End Get
    End Property

    ''' <summary>
    ''' Version 4.71. Obtains tooltip information by sending the 
    ''' TVN_GETINFOTIP notification.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property TVS_INFOTIP() As Integer
        Get
            Return TreeViewStyles.TVS_INFOTIP
        End Get
    End Property

    ''' <summary>
    ''' Version 4.71. Enables full-row selection in the tree view. 
    ''' The entire row of the selected item is highlighted, and clicking 
    ''' anywhere on an item's row causes it to be selected. This style 
    ''' cannot be used in conjunction with the TVS_HASLINES style. 
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property TVS_FULLROWSELECT() As Integer
        Get
            Return TreeViewStyles.TVS_FULLROWSELECT
        End Get
    End Property

    ''' <summary>
    ''' Version 4.71. Disables both horizontal and vertical scrolling in 
    ''' the control. The control will not display any scroll bars. 
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property TVS_NOSCROLL() As Integer
        Get
            Return TreeViewStyles.TVS_NOSCROLL
        End Get
    End Property

    ''' <summary>
    ''' Version 4.71 Sets the height of the items to an odd height with the 
    ''' TVM_SETITEMHEIGHT message. By default, the height of items must be 
    ''' an even value. 
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property TVS_NONEVENHEIGHT() As Integer
        Get
            Return TreeViewStyles.TVS_NONEVENHEIGHT
        End Get
    End Property



End Class
