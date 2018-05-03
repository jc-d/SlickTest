Imports WinAPI.API

''' <summary>
''' The following styles can be specified wherever a ListView style is required. 
''' </summary>
''' <remarks>NOTE: Some of this information was provided by Microsoft's(tm) MSDN.
''' </remarks>
Public Class ListViewStyle

    Protected Friend Sub New()

    End Sub

    ''' <summary>
    ''' Tests the windows object style value to see if it includes the LVS_Value.
    ''' </summary>
    ''' <param name="StyleValue">The style value returned from a windows object.</param>
    ''' <param name="LVS_Value">The LVS value you that is checked to see if it is contained in the windows object.</param>
    ''' <returns></returns>
    ''' <remarks>Example usage: StyleInfo.ContainsValue(Window(MyWindow).GetStyle.ListViewStyle.GetStyle(),StyleInfo.ListViewStyle.LVS_ALIGNLEFT)
    ''' <p />This will return true if the window contains the value "LVS_ALIGNLEFT".</remarks>
    Public Function ContainsValue(ByVal StyleValue As Int64, ByVal LVS_Value As Int64) As Boolean
        Return ((StyleValue And LVS_Value) = LVS_Value)
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
        For Each item As ListViewStyles In [Enum].GetValues(GetType(WinAPI.API.ListViewStyles))
            Try
                If (ContainsValue(StyleValue, item) = True) Then
                    Name = [Enum].GetName(GetType(ListViewStyles), item).TrimStart("n"c)
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
    '    For Each item As ListViewStyles In [Enum].GetValues(GetType(ListViewStyles))
    '        temp = String.Empty
    '        Name = [Enum].GetName(GetType(ListViewStyles), item)
    '        temp = Property_txt & "" & Name.TrimStart("n"c).ToUpper() & "() As Integer" & vbNewLine & _
    '        "Get" & vbNewLine & _
    '        "Return " & "ListViewStyles" & "." & Name.ToUpper() & vbNewLine & _
    '        "End Get" & vbNewLine & _
    '        "End Property"
    '        RetVal += temp & vbNewLine & vbNewLine
    '    Next
    '    System.Windows.Forms.Clipboard.SetText(RetVal)
    '    Return RetVal
    'End Function

    ''' <summary>
    ''' This style specifies icon view. Same as LVS_ALIGNTOP.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property LVS_ICON() As Integer
        Get
            Return ListViewStyles.LVS_ALIGNTOP
        End Get
    End Property

    ''' <summary>
    ''' This style specifies list view. Same as LVS_TYPEMASK.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property LVS_LIST() As Integer
        Get
            Return ListViewStyles.LVS_TYPEMASK
        End Get
    End Property

    ''' <summary>
    ''' Items are aligned with the top of the list-view 
    ''' control in icon and small icon view. 
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property LVS_ALIGNTOP() As Integer
        Get
            Return ListViewStyles.LVS_ALIGNTOP
        End Get
    End Property

    ''' <summary>
    ''' This style specifies report view. When using the LVS_REPORT style 
    ''' with a list-view control, the first column is always left-aligned. 
    ''' You cannot use LVCFMT_RIGHT to change this alignment.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property LVS_REPORT() As Integer
        Get
            Return ListViewStyles.LVS_REPORT
        End Get
    End Property

    ''' <summary>
    ''' This style specifies small icon view. 
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property LVS_SMALLICON() As Integer
        Get
            Return ListViewStyles.LVS_SMALLICON
        End Get
    End Property

    ''' <summary>
    ''' Determines the control's current window style. 
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property LVS_TYPEMASK() As Integer
        Get
            Return ListViewStyles.LVS_TYPEMASK
        End Get
    End Property

    ''' <summary>
    ''' Only one item at a time can be selected. By default, 
    ''' multiple items may be selected. 
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property LVS_SINGLESEL() As Integer
        Get
            Return ListViewStyles.LVS_SINGLESEL
        End Get
    End Property

    ''' <summary>
    ''' The selection, if any, is always shown, even if 
    ''' the control does not have the focus.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property LVS_SHOWSELALWAYS() As Integer
        Get
            Return ListViewStyles.LVS_SHOWSELALWAYS
        End Get
    End Property

    ''' <summary>
    ''' Item indexes are sorted based on item text in ascending order.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property LVS_SORTASCENDING() As Integer
        Get
            Return ListViewStyles.LVS_SORTASCENDING
        End Get
    End Property

    ''' <summary>
    ''' Item indexes are sorted based on item text in descending order. 
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property LVS_SORTDESCENDING() As Integer
        Get
            Return ListViewStyles.LVS_SORTDESCENDING
        End Get
    End Property

    ''' <summary>
    ''' The image list will not be deleted when the control is destroyed. 
    ''' This style enables the use of the same image lists with multiple 
    ''' list-view controls. 
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property LVS_SHAREIMAGELISTS() As Integer
        Get
            Return ListViewStyles.LVS_SHAREIMAGELISTS
        End Get
    End Property

    ''' <summary>
    ''' Item text is displayed on a single line in icon view. By default, 
    ''' item text may wrap in icon view. 
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property LVS_NOLABELWRAP() As Integer
        Get
            Return ListViewStyles.LVS_NOLABELWRAP
        End Get
    End Property

    ''' <summary>
    ''' Icons are automatically kept arranged in icon and small icon view.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property LVS_AUTOARRANGE() As Integer
        Get
            Return ListViewStyles.LVS_AUTOARRANGE
        End Get
    End Property

    ''' <summary>
    ''' Item text can be edited in place. The parent window must 
    ''' process the LVN_ENDLABELEDIT notification message. 
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property LVS_EDITLABELS() As Integer
        Get
            Return ListViewStyles.LVS_EDITLABELS
        End Get
    End Property

    ''' <summary>
    ''' The owner window can paint items in report view. 
    ''' The list-view control sends a WM_DRAWITEM message to paint each item; 
    ''' it does not send separate messages for each subitem. The iItemData 
    ''' member of the DRAWITEMSTRUCT structure contains the item data for 
    ''' the specified list-view item. 
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property LVS_OWNERDRAWFIXED() As Integer
        Get
            Return ListViewStyles.LVS_OWNERDRAWFIXED
        End Get
    End Property

    ''' <summary>
    ''' Items are left-aligned in icon and small icon view. 
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property LVS_ALIGNLEFT() As Integer
        Get
            Return ListViewStyles.LVS_ALIGNLEFT
        End Get
    End Property

    ''' <summary>
    ''' The control's current alignment. 
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property LVS_ALIGNMASK() As Integer
        Get
            Return ListViewStyles.LVS_ALIGNMASK
        End Get
    End Property

    ''' <summary>
    ''' Version 4.70. This style specifies a virtual list-view control.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property LVS_OWNERDATA() As Integer
        Get
            Return ListViewStyles.LVS_OWNERDATA
        End Get
    End Property

    ''' <summary>
    ''' Scrolling is disabled. All items must be within the client area. 
    ''' This style is not compatible with the LVS_LIST or LVS_REPORT styles. 
    ''' See Knowledge Base Article Q137520 for further discussion.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property LVS_NOSCROLL() As Integer
        Get
            Return ListViewStyles.LVS_NOSCROLL
        End Get
    End Property

    ''' <summary>
    ''' Column headers are not displayed in report view. By default, 
    ''' columns have headers in report view. 
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property LVS_NOCOLUMNHEADER() As Integer
        Get
            Return ListViewStyles.LVS_NOCOLUMNHEADER
        End Get
    End Property

    ''' <summary>
    ''' Column headers do not work like buttons. This style can be used 
    ''' if clicking a column header in report view does not carry out 
    ''' an action, such as sorting.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property LVS_NOSORTHEADER() As Integer
        Get
            Return ListViewStyles.LVS_NOSORTHEADER
        End Get
    End Property

    ''' <summary>
    ''' Determines the window styles that control item alignment 
    ''' and header appearance and behavior. 
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property LVS_TYPESTYLEMASK() As Integer
        Get
            Return ListViewStyles.LVS_TYPESTYLEMASK
        End Get
    End Property

End Class
