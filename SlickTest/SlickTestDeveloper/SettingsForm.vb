''' <history>
''' 12-12-2005  Added support for Enum types (pre-defined enum classes) - thanks Peter!
'''             Added Padding support for the viewing pane (so if a vertical scrollbar
'''               appears, a horizontal one doesn't).
''' 12-09-2005  Added support for FireFox2 view
''' 12-01-2005  Added support to easily add images for Category names (FireFox view)
''' 11-30-2005  Added support for FireFox1 view
''' 11-16-2005  Added support for TabPage view
''' 11-15-2005  Released
''' Taken from: http://www.codeproject.com/KB/dialog/OptionsLib.aspx
''' WARNING: At present, no license has been provided for this code.
''' </history>

Public Class OptionsForm

    Private Settings As New SettingInfoCollection
    Private _OptionStyle As OptionsStyle = OptionsStyle.FireFox1
    Private _ImgList As New Hashtable

    ''' <summary>
    ''' Determines the style of the Settings dialog box
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum OptionsStyle
        ''' <summary>
        ''' Displays the dialog box in TabPage mode (like Word)
        ''' </summary>
        ''' <remarks></remarks>
        TabPages = 0
        ''' <summary>
        ''' Displays the dialog box in TreeView mode (like Visual Studio)
        ''' </summary>
        ''' <remarks></remarks>
        TreeView = 1
        ''' <summary>
        ''' Displays the dialog box in a mode similar to FireFox (pre 1.5)
        ''' </summary>
        ''' <remarks></remarks>
        FireFox1 = 2
        ''' <summary>
        ''' Displays the dialog box in a mode similar to FireFox (post 1.5)
        ''' </summary>
        ''' <remarks></remarks>
        FireFox2 = 3
    End Enum

    ''' <summary>
    ''' Changes the style the dialog box is displayed in.
    ''' </summary>
    ''' <value></value>
    ''' <returns>OptionsStyle</returns>
    Public Property Style() As OptionsStyle
        Get
            Return _OptionStyle
        End Get
        Set(ByVal value As OptionsStyle)
            'If value = OptionsStyle.FireFox2 Then MsgBox("FireFox2 not supported yet.") : value = OptionsStyle.FireFox1
            _OptionStyle = value
        End Set
    End Property

    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Load the SaveOnExit value
        chkSaveOnExit.Checked = My.Application.SaveMySettingsOnExit
        'Setup the Setting property object for grabing all the settings
        Dim sp As System.Configuration.SettingsProperty = Nothing

        'Cycle through each setting and add it if appropriate.
        For Each sp In My.Settings.Properties
            'If the name doesn't have an underscore - we can't assign a category so we can't change it.
            'If the setting is ApplicationScoped - we aren't able to change it at runtime.
            'Check also if there is support on this form for the System.Type the setting is.
            If sp.Name.IndexOf("_") <> -1 AndAlso IsUserScope(sp) AndAlso IsAllowedType(sp) Then
                'Passed the tests create a new SettingInfo object
                Dim newSetting As New SettingInfo
                'Load the settings data into the object
                newSetting.LoadData(sp.Name)
                'Add the object to the collection
                Settings.Add(newSetting)
            End If
        Next

        'Sort the settings by category - makes the TreeView look nice
        quickSort(Settings)
        'Load appropriately for the style we are using
        Select Case Me.Style
            Case OptionsStyle.TabPages
                'Hide the FireFox panes
                pnlFFPane.Visible = False
                pnlFFBar.Visible = False
                'Hide the TreeView mode settings controls
                tvCategories.Visible = False
                tlp.Visible = False
                'Show the TabControl
                tcMain.Visible = True
                tcMain.TabIndex = 0
                'Load the settings into the TabControl
                LoadTabControl()
            Case OptionsStyle.TreeView
                'Hide the TabControl
                tcMain.Visible = False
                'Hide the FireFox panes
                pnlFFPane.Visible = False
                pnlFFBar.Visible = False
                'Show the TreeView
                tvCategories.Visible = True
                tvCategories.TabIndex = 0
                tlp.Visible = True
                'Load the settings into the TreeView
                LoadTreeView()
            Case OptionsStyle.FireFox1
                'Hide the TabControl
                tcMain.Visible = False
                'Hide the TreeView
                tvCategories.Visible = False
                'Hide the FireFox bar
                pnlFFBar.Visible = False
                'Show the FireFox pane
                pnlFFPane.Visible = True
                tlp.Visible = True
                'Load the settings into the FireFox Pane
                LoadFireFoxPane()
            Case OptionsStyle.FireFox2
                'Hide the TabControl
                tcMain.Visible = False
                'Hide the TreeView
                tvCategories.Visible = False
                'Hide the FireFox pane
                pnlFFPane.Visible = False
                'Show the FireFox bar
                pnlFFBar.Visible = True
                Dim rgt As Integer = tlp.Right
                tlp.Width = rgt - 12
                tlp.Left = 12
                tlp.Top = pnlFFBar.Bottom
                tlp.Height = (cmdOK.Top - 10) - pnlFFBar.Bottom
                tlp.Visible = True
                'Load the settings into the FireFox Bar
                LoadFireFoxBar()
        End Select
    End Sub

#Region " Image Functions "
    Public Sub ImageAdd(ByVal key As String, ByVal img As Image)
        _ImgList.Add(key, img)
    End Sub
    Public Sub ImageRemove(ByVal key As String)
        _ImgList.Remove(key)
    End Sub
    Public Function ImageGet(ByVal key As String) As Image
        Return DirectCast(_ImgList.Item(key), Image)
    End Function
#End Region

#Region " Sorting Functions "

    ''' <summary>
    ''' Bootstrap starts the quick sort method of sorting a SettingInfoCollection.  Sorts alphabetically by category.
    ''' </summary>
    ''' <param name="col">The SettingInfoCollection you wish to sort.</param>
    Private Sub quickSort(ByRef col As SettingInfoCollection)
        'Call the actual sorting method
        SortIt(col, 0, col.Count - 1)
    End Sub
    ''' <summary>
    ''' QuickSort subroutine that does the sorting for a SettingInfoCollection.
    ''' </summary>
    ''' <param name="SortArray">The SettingInfoCollection to sort.</param>
    ''' <param name="First">The starting element of the selection you wish to sort (usually 0).</param>
    ''' <param name="Last">Then final element of the selection you wish to sort (usually the Count - 1 of the collection).</param>
    ''' <remarks>Called by the quickSort subroutine</remarks>
    Private Sub SortIt(ByRef SortArray As SettingInfoCollection, ByVal First As Integer, ByVal Last As Integer)
        'Copied and modified code to support sorting of the SettingInfoCollection
        'using strings
        Dim Low As Integer, High As Integer
        Dim Temp As SettingInfo = Nothing
        Dim List_Separator As SettingInfo = Nothing
        Low = First
        High = Last
        List_Separator = SortArray(Convert.ToInt32((First + Last) / 2)).Clone
        Do
            Do While (SortArray(Low).Category < List_Separator.Category)
                Low += 1
            Loop
            Do While (SortArray(High).Category > List_Separator.Category)
                High -= 1
            Loop
            If (Low <= High) Then
                Temp = SortArray(Low).Clone
                SortArray.SetItem(Low, SortArray(High))
                SortArray.SetItem(High, Temp)
                Low += 1
                High -= 1
            End If
        Loop While (Low <= High)
        If (First < High) Then SortIt(SortArray, First, High)
        If (Low < Last) Then SortIt(SortArray, Low, Last)
    End Sub

#End Region

#Region " Loading Functions "
    ''' <summary>
    ''' Checks whether the dialog supports a given type for editing.
    ''' </summary>
    ''' <param name="se">A SettingsProperty object as found in the My namespace.</param>
    ''' <returns>Boolean: True editing is supported, false editing is not supported.</returns>
    Private Function IsAllowedType(ByVal se As System.Configuration.SettingsProperty) As Boolean
        'Build an array of allowed System.Types that this form supports.
        Dim allowTypes() As Type = {GetType(Boolean), GetType(Byte), GetType(Char), GetType(Date), _
                                    GetType(Decimal), GetType(Double), GetType(Integer), GetType(Long), _
                                    GetType(SByte), GetType(Short), GetType(Single), GetType(String), _
                                    GetType(Color), GetType(Font), _
                                    GetType(UInteger), GetType(ULong), GetType(UShort)}
        'If the type of the property is found in the array - the type is supported.
        If Array.IndexOf(allowTypes, se.PropertyType) <> -1 Then Return True
        '###Peter Spiegler(12/12/2005)###  enum type
        If (se.PropertyType.FullName = "System.Collections.Specialized.StringCollection") Then Return True

        If (se.PropertyType.BaseType.Name = "Enum") Then Return True
        'The type must not be supported if it got here
        Return False
    End Function
    ''' <summary>
    ''' Checks whether the Setting is writable or not.
    ''' </summary>
    ''' <param name="sp">A SettingsProperty as specified in the My namespace.</param>
    ''' <returns>Boolean: True setting is writable, false setting is read-only.</returns>
    Private Function IsUserScope(ByVal sp As System.Configuration.SettingsProperty) As Boolean
        'The scope of the setting is stored in the Attributes of the setting
        'we must cycle through all settings and search for an ApplicationScopedSettingAttribute
        'or a UserScopedSettingAttribute
        For Each o As Object In sp.Attributes.Values
            'If we find an ApplicationScopedSettingAttribute the setting isn't UserScoped so return false
            If TypeOf (o) Is System.Configuration.ApplicationScopedSettingAttribute Then Return False
            'If we find a UserScopedSettingAttribute then the setting is UserScoped so return true
            If TypeOf (o) Is System.Configuration.UserScopedSettingAttribute Then Return True
        Next
        'If we didn't find either of them, it isn't UserScoped (and I'm not sure what it is) so return false
        Return False
    End Function

    ''' <summary>
    ''' Cycles through eact setting and makes sure the category is in the TreeView
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadTreeView()
        'If there are no settings - do not try loading the treeview
        If Settings.Count = 0 Then Exit Sub
        'Cycle through all of the loaded settings
        For Each si As SettingInfo In Settings
            'Add the categories to the root of the TreeView
            AddCat(tvCategories.Nodes, si.Category.Split("."c), 0)
        Next
    End Sub
    ''' <summary>
    ''' Adds categories/sub-categories to a TreeView.
    ''' </summary>
    ''' <param name="parent_nodes">The TreeNodeCollection to which you wish to add the category/sub-category</param>
    ''' <param name="fields">An array of categories/sub-categories.  Index 0 is the Category everything else is a sub-category of the index before it.</param>
    ''' <param name="field_num">The index with which to the current TreeNodeCollection level. For a TreeNode.Nodes object this would most likely be 0.</param>
    Private Sub AddCat(ByVal parent_nodes As TreeNodeCollection, ByVal fields() As String, ByVal field_num As Integer)
        'If there were no fields passed - we can't add anything to the TreeView so exit the sub
        'Also exit if we reached then end of the field list
        If field_num > fields.GetUpperBound(0) Then Exit Sub

        Dim found_field As Boolean
        'Check each node of the parent to see if it category/sub-category already exists
        For Each child_node As TreeNode In parent_nodes
            If child_node.Text = fields(field_num) Then
                'It exists so check this node for the next sub-category
                AddCat(child_node.Nodes, fields, field_num + 1)
                found_field = True
            End If
        Next child_node

        'If we didn't find the node - then add it
        If Not found_field Then
            Dim new_node As TreeNode = parent_nodes.Add(fields(field_num))
            'Check the next sub-category in the field list
            AddCat(new_node.Nodes, fields, field_num + 1)
        End If
    End Sub

    ''' <summary>
    ''' Cycles through all the Main categories, creating a TabPage and loading the control into it.
    ''' </summary>
    Private Sub LoadTabControl()
        Dim cat As SettingCategoryCollection = Settings.GetCategories
        For Each str As SettingCategory In cat
            Dim tp As New TabPage
            tp.Padding = New Padding(0, 0, 18, 0) 'Added 12/12/2005
            tp.Text = str.MainCat
            tp.Tag = str.SubCats
            tp.UseVisualStyleBackColor = True
            LoadTabPageContents(tp.Text, tp)
            tcMain.TabPages.Add(tp)
        Next
    End Sub
    ''' <summary>
    ''' Loads the settings editing controls into a TabPage.
    ''' </summary>
    ''' <param name="cat">The Main category of the controls you wish to add.</param>
    ''' <param name="tp">A reference to the TabPage you wish to add the controls to.</param>
    Private Sub LoadTabPageContents(ByVal cat As String, ByRef tp As TabPage)
        'Create a new TableLayoutPanel to layout the controls for the settings
        Dim ntlp As New TableLayoutPanel
        ntlp.Name = cat
        ntlp.Dock = DockStyle.Fill
        ntlp.ColumnCount = 2
        ntlp.ColumnStyles.Add(New ColumnStyle(SizeType.AutoSize))
        ntlp.ColumnStyles.Add(New ColumnStyle(SizeType.AutoSize))
        ntlp.AutoSizeMode = Windows.Forms.AutoSizeMode.GrowAndShrink
        ntlp.AutoScroll = True
        'Add the new TableLayoutPanel to the TabPage
        tp.Controls.Add(ntlp)
        'Reset the TabIndex for the dynamic controls
        Dim tbIdx As Integer = 1
        'Get all the settings for this TabPage
        Dim sets() As SettingInfo = Settings.GetByTabCategory(cat)
        'If there aren't any we must exit
        If sets Is Nothing Then Exit Sub
        'Some declaration to make things easier in the loop
        Dim lbl As Label = Nothing
        Dim dc1 As DockStyle = DockStyle.Fill
        Dim dc2 As DockStyle = DockStyle.None
        Dim x As RowStyle
        'Cycle through this category's settings
        For i As Integer = 0 To UBound(sets)
            lbl = New Label
            x = New RowStyle
            x.SizeType = SizeType.AutoSize
            lbl.AutoSize = True
            'If we've reached the end of the settings - don't use the Fill DockStyle
            If i = UBound(sets) Then lbl.Dock = dc2 Else lbl.Dock = dc1
            lbl.TextAlign = ContentAlignment.MiddleLeft
            If TypeOf (sets(i).Value) Is Boolean Then 'If the setting is a boolean use a checkbox for editing
                'Setup the label
                lbl.Text = sets(i).Name
                'Add the label
                ntlp.Controls.Add(lbl)
                ntlp.RowStyles.Add(x)
                'Setup the checkbox
                Dim chk As New CheckBox
                chk.Name = sets(i).TrueName
                chk.Tag = GetTypeAbbr(sets(i).Value.GetType)
                chk.Checked = Convert.ToBoolean(sets(i).Value)
                chk.TabIndex = tbIdx
                'Setup the checkbox help string - for after clicking on the question mark button on the form
                hp1.SetHelpString(chk, "Toggles between true and false for this setting.")
                hp1.SetShowHelp(chk, True)
                'Add the handler for the checkbox, and add it to the form
                AddHandler chk.LostFocus, AddressOf checkbox_handler
                ntlp.Controls.Add(chk)
            ElseIf TypeOf (sets(i).Value) Is Date Then 'If the setting is a date use a datetimepicker for editing
                'Setup the label
                lbl.Text = sets(i).Name
                'Add the label
                ntlp.Controls.Add(lbl)
                ntlp.RowStyles.Add(x)
                'Setup the datetimepicker
                Dim dtp As New DateTimePicker
                dtp.Name = sets(i).TrueName
                dtp.Tag = GetTypeAbbr(sets(i).Value.GetType)
                dtp.Value = Convert.ToDateTime(sets(i).Value)
                dtp.Anchor = DirectCast(AnchorStyles.Left + AnchorStyles.Right + AnchorStyles.Top, System.Windows.Forms.AnchorStyles)
                dtp.TabIndex = tbIdx
                'Setup the datetimepicker help string - for after clicking on the question mark button on the form
                hp1.SetHelpString(dtp, "Drop-down that allows you to choose a date for this setting.")
                hp1.SetShowHelp(dtp, True)
                'Add the handler for the datetimepicker, and add it to the form
                AddHandler dtp.LostFocus, AddressOf datetimepicker_handler
                ntlp.Controls.Add(dtp)
            ElseIf IsTxtType(sets(i).Value.GetType) Then 'If the setting is a type we edit with a textbox use a textbox for editing
                'Setup the label
                lbl.Text = sets(i).Name
                'Add the label
                ntlp.Controls.Add(lbl)
                ntlp.RowStyles.Add(x)
                'Setup the textbox
                Dim txt As New TextBox
                txt.Name = sets(i).TrueName
                'If the name contains the word Password - make the textbox mask the characters
                If sets(i).Name.ToLower.Contains("password") Then txt.PasswordChar = "*"c
                txt.Tag = GetTypeAbbr(sets(i).Value.GetType)
                txt.Text = sets(i).Value.ToString()
                txt.Anchor = DirectCast(AnchorStyles.Left + AnchorStyles.Right + AnchorStyles.Top, AnchorStyles)
                txt.TabIndex = tbIdx
                'Setup the textbox help string - for after clicking on the question mark button on the form
                'Uses GetTypeName for type specific messages
                hp1.SetHelpString(txt, "Text entry field that allows you to enter " & GetTypeName(sets(i).Value.GetType) & " value for this setting.")
                hp1.SetShowHelp(txt, True)
                'Add the handler for the textbox, and add it to the form
                AddHandler txt.LostFocus, AddressOf textbox_handler
                ntlp.Controls.Add(txt)
            ElseIf TypeOf (sets(i).Value) Is Color Then 'If the setting is a color use a button with no text for editing
                'Setup the label
                lbl.Text = sets(i).Name
                'Add the label
                ntlp.Controls.Add(lbl)
                ntlp.RowStyles.Add(x)
                'Setup the button
                Dim cmd As New Button
                cmd.Name = sets(i).TrueName
                cmd.Tag = GetTypeAbbr(sets(i).Value.GetType)
                cmd.BackColor = DirectCast(sets(i).Value, Color)
                cmd.TabIndex = tbIdx
                'Setup the button help string - for after clicking on the question mark button on the form
                hp1.SetHelpString(cmd, "Button that allows you to choose a color for this setting.")
                hp1.SetShowHelp(cmd, True)
                'Add the handlers for the button, and add it to the form
                AddHandler cmd.Click, AddressOf button_click
                AddHandler cmd.LostFocus, AddressOf button_handler
                ntlp.Controls.Add(cmd)
            ElseIf TypeOf (sets(i).Value) Is Font Then 'If the setting is a font use a button with the font name for editing
                'Setup the label
                lbl.Text = sets(i).Name
                'Add the label
                ntlp.Controls.Add(lbl)
                ntlp.RowStyles.Add(x)
                'Setup the button
                Dim cmd As New Button
                cmd.AutoSize = True
                cmd.Name = sets(i).TrueName
                cmd.Tag = GetTypeAbbr(sets(i).Value.GetType)
                cmd.Text = CType(sets(i).Value, Font).Name
                cmd.Font = DirectCast(sets(i).Value, Font)
                cmd.UseVisualStyleBackColor = True
                cmd.TabIndex = tbIdx
                'Setup the button help string - for after clicking on the question mark button on the form
                hp1.SetHelpString(cmd, "Button that allows you to choose a font for this setting.")
                hp1.SetShowHelp(cmd, True)
                'Add the handlers for the button, and add it to the form
                AddHandler cmd.Click, AddressOf button_click
                AddHandler cmd.LostFocus, AddressOf button_handler
                ntlp.Controls.Add(cmd)
                '###Peter Spiegler(12/12/2005)###  enum type
            ElseIf TypeOf (sets(i).Value) Is [Enum] Then 'If the setting is a enum type we fill a combobox
                'Setup the label
                lbl.Text = sets(i).Name
                'Add the label
                ntlp.Controls.Add(lbl)
                ntlp.RowStyles.Add(x)
                'Setup the ComboBox
                Dim cbo As New ComboBox
                cbo.DropDownStyle = ComboBoxStyle.DropDownList
                cbo.Name = sets(i).TrueName
                cbo.Tag = GetTypeAbbr(sets(i).Value.GetType)
                For Each o As Object In [Enum].GetValues(sets(i).Value.GetType)
                    cbo.Items.Add(o)
                Next
                cbo.SelectedIndex = Convert.ToInt32(sets(i).Value)
                cbo.Anchor = DirectCast(AnchorStyles.Left + AnchorStyles.Right + AnchorStyles.Top, AnchorStyles)
                cbo.TabIndex = tbIdx
                'Setup the help string - for after clicking on the question mark button on the form
                hp1.SetHelpString(cbo, "ComboBox that allows you to enter values of type " & sets(i).Value.GetType.FullName & "  for this setting.")
                hp1.SetShowHelp(cbo, True)
                'Add the handler for the Combobox, and add it to the form
                AddHandler cbo.LostFocus, AddressOf enum_handler
                ntlp.Controls.Add(cbo)
            ElseIf TypeOf (sets(i).Value) Is System.Collections.Specialized.StringCollection Then
                'deal with string collections
                'Setup the label
                lbl.Text = sets(i).Name
                'Add the label
                ntlp.Controls.Add(lbl)
                ntlp.RowStyles.Add(x)
                'Setup the textbox
                Dim txt As New TextBox
                txt.Name = sets(i).TrueName
                txt.Multiline = True
                txt.Tag = GetTypeAbbr(GetType(String))
                For Each value As String In sets(i).Value
                    If (txt.Text = String.Empty) Then
                        txt.Text += value
                    Else
                        txt.Text += vbNewLine & value
                    End If
                Next
                txt.Size = New Size(txt.Size.Width, txt.Size.Height * 3)
                'txt.Text = txt.Text.TrimStart(vbNewLine)
                txt.Anchor = DirectCast(AnchorStyles.Left + AnchorStyles.Right + AnchorStyles.Top, AnchorStyles)
                txt.TabIndex = tbIdx
                txt.ScrollBars = ScrollBars.Vertical
                'Setup the textbox help string - for after clicking on the question mark button on the form
                'Uses GetTypeName for type specific messages
                hp1.SetHelpString(txt, "Text entry field that allows you to enter a list of values for this setting.  Enter one item per line.")
                hp1.SetShowHelp(txt, True)
                'Add the handler for the textbox, and add it to the form
                AddHandler txt.LostFocus, AddressOf textbox_handler
                ntlp.Controls.Add(txt)
            End If
            'Don't keep the label at autosize - remember it's most likely docked
            lbl.AutoSize = False
            'Increment the TabIndex
            tbIdx += 1
        Next
        'Update the TabIndex of controls already on the form
        chkSaveOnExit.TabIndex = tbIdx + 1
        cmdOK.TabIndex = tbIdx + 2
        cmdCancel.TabIndex = tbIdx + 3
        cmdApply.TabIndex = tbIdx + 4
    End Sub

    ''' <summary>
    ''' Cycles through all the Main categories - adding it to the FireFox pane (with Icons where appropriate)
    ''' </summary>
    ''' <remarks>Added Nov. 29, 2005</remarks>
    Private Sub LoadFireFoxPane()
        Dim cat As SettingCategoryCollection = Settings.GetCategories
        For Each str As SettingCategory In cat
            Dim fi As New FFItem
            If _ImgList.ContainsKey(str.MainCat) Then
                fi.Image = DirectCast(_ImgList.Item(str.MainCat), Image)
            End If
            fi.Text = str.MainCat
            fi.Tag = str.SubCats
            FireFoxPane1.Controls.Add(fi)
        Next
        FireFoxPane1.SetSelectedIndex(0)
        FireFoxPanes_SelectedIndexChanged(FireFoxPane1, New System.EventArgs)
    End Sub

    ''' <summary>
    ''' Cycles through all the Main categories - adding it to the FireFox bar (with Icons where appropriate)
    ''' </summary>
    ''' <remarks>Added Dec. 9, 2005</remarks>
    Private Sub LoadFireFoxBar()
        Dim cat As SettingCategoryCollection = Settings.GetCategories
        For Each str As SettingCategory In cat
            Dim fi As New FFItem
            If _ImgList.ContainsKey(str.MainCat) Then
                fi.Image = DirectCast(_ImgList.Item(str.MainCat), Image)
            End If
            fi.Text = str.MainCat
            fi.Tag = str.SubCats
            FireFoxBar1.Controls.Add(fi)
        Next
        FireFoxBar1.SetSelectedIndex(0)
        FireFoxPanes_SelectedIndexChanged(FireFoxBar1, New System.EventArgs)
    End Sub

#End Region

#Region " TreeView Handling "
    ''' <summary>
    ''' Clears all the setting controls that were created at runtime from the window.
    ''' </summary>
    Private Sub ClearOptions()
        'Cleans up the dynamically added controls for the settings
        Dim ctrl As Control
        'While there are still dynamic controls on the form - remove them
        While tlp.Controls.Count > 0
            'Set an object equal to a reference of the control
            ctrl = tlp.Controls.Item(0)
            'Remove the handlers based on the type of the control
            If TypeOf (ctrl) Is Button Then
                RemoveHandler ctrl.Click, AddressOf button_click
                RemoveHandler ctrl.LostFocus, AddressOf button_handler
            ElseIf TypeOf (ctrl) Is CheckBox Then
                RemoveHandler ctrl.LostFocus, AddressOf checkbox_handler
            ElseIf TypeOf (ctrl) Is DateTimePicker Then
                RemoveHandler ctrl.LostFocus, AddressOf datetimepicker_handler
            ElseIf TypeOf (ctrl) Is TextBox Then
                RemoveHandler ctrl.LostFocus, AddressOf textbox_handler
            ElseIf TypeOf (ctrl) Is ComboBox Then
                RemoveHandler ctrl.LostFocus, AddressOf enum_handler
            End If
            'Remove the control from the form
            ctrl.Dispose()
        End While
    End Sub
    ''' <summary>
    ''' Determines whether the type passed would use a textbox or not.
    ''' </summary>
    ''' <param name="typ">A system type that you wish to check.</param>
    ''' <returns>Boolean: True the type does use a textbox, false it doesn't use a textbox.</returns>
    Private Function IsTxtType(ByVal typ As Type) As Boolean
        'If the type passed is one of these we use a textbox to edit it
        If GetType(Byte) Is typ Or _
           GetType(Char) Is typ Or _
           GetType(Decimal) Is typ Or _
           GetType(Double) Is typ Or _
           GetType(Integer) Is typ Or _
           GetType(Long) Is typ Or _
           GetType(SByte) Is typ Or _
           GetType(Short) Is typ Or _
           GetType(Single) Is typ Or _
           GetType(String) Is typ Or _
           GetType(UInteger) Is typ Or _
           GetType(ULong) Is typ Or _
           GetType(UShort) Is typ Then
            Return True
        End If
        'We don't use a textbox on this type.
        Return False
    End Function
    Private Sub tvCategories_AfterSelect(ByVal sender As System.Object, ByVal e As System.Windows.Forms.TreeViewEventArgs) Handles tvCategories.AfterSelect
        'A category was selected - clear the current display
        ClearOptions()
        tlp.RowStyles.Clear()
        tlp.RowCount = 1
        'Reset the TabIndex for the dynamic controls
        Dim tbIdx As Integer = 1
        'Get all the settings that match this category
        Dim sets() As SettingInfo = Settings.GetByCategory(tvCategories.SelectedNode.FullPath)
        'If there aren't any we must exit
        If sets Is Nothing Then Exit Sub
        'Some declaration to make things easier in the loop
        Dim lbl As Label = Nothing
        Dim dc1 As DockStyle = DockStyle.Fill
        Dim dc2 As DockStyle = DockStyle.None
        Dim x As RowStyle
        'Cycle through this category's settings
        For i As Integer = 0 To UBound(sets)
            lbl = New Label
            x = New RowStyle
            x.SizeType = SizeType.AutoSize
            lbl.AutoSize = True
            'If we've reached the end of the settings - don't use the Fill DockStyle.
            If i = UBound(sets) Then lbl.Dock = dc2 Else lbl.Dock = dc1
            lbl.TextAlign = ContentAlignment.MiddleLeft
            If TypeOf (sets(i).Value) Is Boolean Then 'If the setting is a boolean use a checkbox for editing
                'Setup the label
                lbl.Text = sets(i).Name
                'Add the label
                tlp.Controls.Add(lbl)
                tlp.RowStyles.Add(x)
                'Setup the checkbox
                Dim chk As New CheckBox
                chk.Name = sets(i).TrueName
                chk.Tag = GetTypeAbbr(sets(i).Value.GetType)
                chk.Checked = Convert.ToBoolean(sets(i).Value)
                chk.TabIndex = tbIdx
                'Setup the checkbox help string - for after clicking on the question mark button on the form
                hp1.SetHelpString(chk, "Toggles between true and false for this setting.")
                hp1.SetShowHelp(chk, True)
                'Add the handler for the checkbox, and add it to the form
                AddHandler chk.LostFocus, AddressOf checkbox_handler
                tlp.Controls.Add(chk)
            ElseIf TypeOf (sets(i).Value) Is Date Then 'If the setting is a date use a datetimepicker for editing
                'Setup the label
                lbl.Text = sets(i).Name
                'Add the label
                tlp.Controls.Add(lbl)
                tlp.RowStyles.Add(x)
                'Setup the datetimepicker
                Dim dtp As New DateTimePicker
                dtp.Name = sets(i).TrueName
                dtp.Tag = GetTypeAbbr(sets(i).Value.GetType)
                dtp.Value = Convert.ToDateTime(sets(i).Value)
                dtp.Anchor = DirectCast(AnchorStyles.Left + AnchorStyles.Right + AnchorStyles.Top, AnchorStyles)
                dtp.TabIndex = tbIdx
                'Setup the datetimepicker help string - for after clicking on the question mark button on the form
                hp1.SetHelpString(dtp, "Drop-down that allows you to choose a date for this setting.")
                hp1.SetShowHelp(dtp, True)
                'Add the handler for the datetimepicker, and add it to the form
                AddHandler dtp.LostFocus, AddressOf datetimepicker_handler
                tlp.Controls.Add(dtp)
            ElseIf IsTxtType(sets(i).Value.GetType) Then 'If the setting is a type we edit with a textbox use a textbox for editing
                'Setup the label
                lbl.Text = sets(i).Name
                'Add the label
                tlp.Controls.Add(lbl)
                tlp.RowStyles.Add(x)
                'Setup the textbox
                Dim txt As New TextBox
                txt.Name = sets(i).TrueName
                'If the name contains the word Password - make the textbox mask the characters
                If sets(i).Name.ToLower.Contains("password") Then txt.PasswordChar = "*"c
                txt.Tag = GetTypeAbbr(sets(i).Value.GetType)
                txt.Text = sets(i).Value.ToString()
                txt.Anchor = DirectCast(AnchorStyles.Left + AnchorStyles.Right + AnchorStyles.Top, AnchorStyles)
                txt.TabIndex = tbIdx
                'Setup the textbox help string - for after clicking on the question mark button on the form
                'Uses GetTypeName for type specific messages
                hp1.SetHelpString(txt, "Text entry field that allows you to enter " & GetTypeName(sets(i).Value.GetType) & " value for this setting.")
                hp1.SetShowHelp(txt, True)
                'Add the handler for the textbox, and add it to the form
                AddHandler txt.LostFocus, AddressOf textbox_handler
                tlp.Controls.Add(txt)
            ElseIf TypeOf (sets(i).Value) Is Color Then 'If the setting is a color use a button with no text for editing
                'Setup the label
                lbl.Text = sets(i).Name
                'Add the label
                tlp.Controls.Add(lbl)
                tlp.RowStyles.Add(x)
                'Setup the button
                Dim cmd As New Button
                cmd.Name = sets(i).TrueName
                cmd.Tag = GetTypeAbbr(sets(i).Value.GetType)
                cmd.BackColor = DirectCast(sets(i).Value, Color)
                cmd.TabIndex = tbIdx
                'Setup the button help string - for after clicking on the question mark button on the form
                hp1.SetHelpString(cmd, "Button that allows you to choose a color for this setting.")
                hp1.SetShowHelp(cmd, True)
                'Add the handlers for the button, and add it to the form
                AddHandler cmd.Click, AddressOf button_click
                AddHandler cmd.LostFocus, AddressOf button_handler
                tlp.Controls.Add(cmd)
            ElseIf TypeOf (sets(i).Value) Is Font Then 'If the setting is a font use a button with the font name for editing
                'Setup the label
                lbl.Text = sets(i).Name
                'Add the label
                tlp.Controls.Add(lbl)
                tlp.RowStyles.Add(x)
                'Setup the button
                Dim cmd As New Button
                cmd.AutoSize = True
                cmd.Name = sets(i).TrueName
                cmd.Tag = GetTypeAbbr(sets(i).Value.GetType)
                cmd.Text = CType(sets(i).Value, Font).Name
                cmd.Font = DirectCast(sets(i).Value, Font)
                cmd.UseVisualStyleBackColor = True
                cmd.TabIndex = tbIdx
                'Setup the button help string - for after clicking on the question mark button on the form
                hp1.SetHelpString(cmd, "Button that allows you to choose a font for this setting.")
                hp1.SetShowHelp(cmd, True)
                'Add the handlers for the button, and add it to the form
                AddHandler cmd.Click, AddressOf button_click
                AddHandler cmd.LostFocus, AddressOf button_handler
                tlp.Controls.Add(cmd)
                '###Peter Spiegler(12/12/2005)### enum type
            ElseIf TypeOf (sets(i).Value) Is [Enum] Then 'If the setting is a enum type we fill a combobox
                'Setup the label
                lbl.Text = sets(i).Name
                'Add the label
                tlp.Controls.Add(lbl)
                tlp.RowStyles.Add(x)
                'Setup the ComboBox
                Dim cbo As New ComboBox
                cbo.DropDownStyle = ComboBoxStyle.DropDownList
                cbo.Name = sets(i).TrueName
                cbo.Tag = GetTypeAbbr(sets(i).Value.GetType) 'not necessary, but everywhere else so...
                For Each o As Object In [Enum].GetValues(sets(i).Value.GetType)
                    cbo.Items.Add(o)
                Next
                cbo.SelectedIndex = Convert.ToInt32(sets(i).Value)
                cbo.Anchor = DirectCast(AnchorStyles.Left + AnchorStyles.Right + AnchorStyles.Top, AnchorStyles)
                cbo.TabIndex = tbIdx
                'Setup the help string - for after clicking on the question mark button on the form
                hp1.SetHelpString(cbo, "ComboBox that allows you to enter values of type " & sets(i).Value.GetType.FullName & "  for this setting.")
                hp1.SetShowHelp(cbo, True)
                'Add the handler for the Combobox, and add it to the form
                AddHandler cbo.LostFocus, AddressOf enum_handler
                tlp.Controls.Add(cbo)
            End If
            'Don't keep the label at autosize - remember it's most likely docked
            lbl.AutoSize = False
            'Increment the TabIndex
            tbIdx += 1
        Next
        'Update the TabIndex of controls already on the form
        chkSaveOnExit.TabIndex = tbIdx + 1
        cmdOK.TabIndex = tbIdx + 2
        cmdCancel.TabIndex = tbIdx + 3
        cmdApply.TabIndex = tbIdx + 4
    End Sub
    ''' <summary>
    ''' Gets the four letter type abbreviation for the passed type.
    ''' </summary>
    ''' <param name="typ">A system type that you wish to get the four letter type for.</param>
    ''' <returns>String containing the four letter abbrieviation for the type.</returns>
    ''' <remarks>Some are 3 letters with a space in front to make it four characters.</remarks>
    Private Function GetTypeAbbr(ByVal typ As Type) As String
        'Get a four character string to represent the type
        'This goes in the tag of the dynamic control and helps
        'the handlers determine what type to check for in textboxes
        'and buttons.
        If GetType(Boolean) Is typ Then
            Return "bool"
        ElseIf GetType(Byte) Is typ Then
            Return "byte"
        ElseIf GetType(Char) Is typ Then
            Return "char"
        ElseIf GetType(Date) Is typ Then
            Return "date"
        ElseIf GetType(Decimal) Is typ Then
            Return " dec"
        ElseIf GetType(Double) Is typ Then
            Return " dbl"
        ElseIf GetType(Integer) Is typ Then
            Return " int"
        ElseIf GetType(Long) Is typ Then
            Return "long"
        ElseIf GetType(SByte) Is typ Then
            Return "sbyt"
        ElseIf GetType(Short) Is typ Then
            Return "shrt"
        ElseIf GetType(Single) Is typ Then
            Return "sngl"
        ElseIf GetType(String) Is typ Then
            Return " str"
        ElseIf GetType(Color) Is typ Then
            Return " clr"
        ElseIf GetType(Font) Is typ Then
            Return "font"
        ElseIf GetType(UInteger) Is typ Then
            Return "uint"
        ElseIf GetType(ULong) Is typ Then
            Return "ulng"
        ElseIf GetType(UShort) Is typ Then
            Return "usht"
        End If
        Return Nothing
    End Function
    ''' <summary>
    ''' Gets a string containing the type name with a describing article (a, an, the) for display in a MsgBox
    ''' </summary>
    ''' <param name="typ">A system type that you wish to get the name of.</param>
    ''' <returns>String containing the name of the type and an article for describing it.</returns>
    Private Function GetTypeName(ByVal typ As Type) As String
        'Gets a string for the message box error message when trying to determine
        'if a setting was set properly
        If GetType(Boolean) Is typ Then
            Return "a Boolean"
        ElseIf GetType(Byte) Is typ Then
            Return "a Byte"
        ElseIf GetType(Char) Is typ Then
            Return "a Character"
        ElseIf GetType(Date) Is typ Then
            Return "a Date"
        ElseIf GetType(Decimal) Is typ Then
            Return "a Decimal (numeric)"
        ElseIf GetType(Double) Is typ Then
            Return "a Double (numeric)"
        ElseIf GetType(Integer) Is typ Then
            Return "an Integer (numeric)"
        ElseIf GetType(Long) Is typ Then
            Return "a Long (numeric)"
        ElseIf GetType(SByte) Is typ Then
            Return "a Short Byte"
        ElseIf GetType(Short) Is typ Then
            Return "a Short (numeric)"
        ElseIf GetType(Single) Is typ Then
            Return "a Single (numeric)"
        ElseIf GetType(String) Is typ Then
            Return "a String (text)"
        ElseIf GetType(Color) Is typ Then
            Return "a Color"
        ElseIf GetType(Font) Is typ Then
            Return "a Font"
        ElseIf GetType(UInteger) Is typ Then
            Return "an Unsigned Integer (numeric)"
        ElseIf GetType(ULong) Is typ Then
            Return "an Unsigned Long (numeric)"
        ElseIf GetType(UShort) Is typ Then
            Return "an Unsigned Short (numeric)"
        End If
        Return Nothing
    End Function
#End Region

#Region " FireFoxPane Handling "
    Private Sub FireFoxPanes_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles FireFoxPane1.SelectedIndexChanged, FireFoxBar1.SelectedIndexChanged
        'A category was selected - clear the current display
        ClearOptions()
        tlp.RowStyles.Clear()
        tlp.RowCount = 1
        'Reset the TabIndex for the dynamic controls
        Dim tbIdx As Integer = 1
        'Get all the settings that match this category
        Dim sets() As SettingInfo = Settings.GetByTabCategory(sender.GetItem(sender.SelectedIndex).Text)
        'If there aren't any we must exit
        If sets Is Nothing Then Exit Sub
        'Some declaration to make things easier in the loop
        Dim lbl As Label = Nothing
        Dim dc1 As DockStyle = DockStyle.Fill
        Dim dc2 As DockStyle = DockStyle.None
        Dim x As RowStyle
        'Cycle through this category's settings
        For i As Integer = 0 To UBound(sets)
            lbl = New Label
            x = New RowStyle
            x.SizeType = SizeType.AutoSize
            lbl.AutoSize = True
            'If we've reached the end of the settings - don't use the Fill DockStyle.
            If i = UBound(sets) Then lbl.Dock = dc2 Else lbl.Dock = dc1
            lbl.TextAlign = ContentAlignment.MiddleLeft
            If TypeOf (sets(i).Value) Is Boolean Then 'If the setting is a boolean use a checkbox for editing
                'Setup the label
                lbl.Text = sets(i).Name
                'Add the label
                tlp.Controls.Add(lbl)
                tlp.RowStyles.Add(x)
                'Setup the checkbox
                Dim chk As New CheckBox
                chk.Name = sets(i).TrueName
                chk.Tag = GetTypeAbbr(sets(i).Value.GetType)
                chk.Checked = Convert.ToBoolean(sets(i).Value)
                chk.TabIndex = tbIdx
                'Setup the checkbox help string - for after clicking on the question mark button on the form
                hp1.SetHelpString(chk, "Toggles between true and false for this setting.")
                hp1.SetShowHelp(chk, True)
                'Add the handler for the checkbox, and add it to the form
                AddHandler chk.LostFocus, AddressOf checkbox_handler
                tlp.Controls.Add(chk)
            ElseIf TypeOf (sets(i).Value) Is Date Then 'If the setting is a date use a datetimepicker for editing
                'Setup the label
                lbl.Text = sets(i).Name
                'Add the label
                tlp.Controls.Add(lbl)
                tlp.RowStyles.Add(x)
                'Setup the datetimepicker
                Dim dtp As New DateTimePicker
                dtp.Name = sets(i).TrueName
                dtp.Tag = GetTypeAbbr(sets(i).Value.GetType)
                dtp.Value = Convert.ToDateTime(sets(i).Value)
                dtp.Anchor = DirectCast(AnchorStyles.Left + AnchorStyles.Right + AnchorStyles.Top, AnchorStyles)
                dtp.TabIndex = tbIdx
                'Setup the datetimepicker help string - for after clicking on the question mark button on the form
                hp1.SetHelpString(dtp, "Drop-down that allows you to choose a date for this setting.")
                hp1.SetShowHelp(dtp, True)
                'Add the handler for the datetimepicker, and add it to the form
                AddHandler dtp.LostFocus, AddressOf datetimepicker_handler
                tlp.Controls.Add(dtp)
            ElseIf IsTxtType(sets(i).Value.GetType) Then 'If the setting is a type we edit with a textbox use a textbox for editing
                'Setup the label
                lbl.Text = sets(i).Name
                'Add the label
                tlp.Controls.Add(lbl)
                tlp.RowStyles.Add(x)
                'Setup the textbox
                Dim txt As New TextBox
                txt.Name = sets(i).TrueName
                'If the name contains the word Password - make the textbox mask the characters
                If sets(i).Name.ToLower.Contains("password") Then txt.PasswordChar = "*"c
                txt.Tag = GetTypeAbbr(sets(i).Value.GetType)
                txt.Text = sets(i).Value.ToString()
                txt.Anchor = DirectCast(AnchorStyles.Left + AnchorStyles.Right + AnchorStyles.Top, AnchorStyles)
                txt.TabIndex = tbIdx
                'Setup the textbox help string - for after clicking on the question mark button on the form
                'Uses GetTypeName for type specific messages
                hp1.SetHelpString(txt, "Text entry field that allows you to enter " & GetTypeName(sets(i).Value.GetType) & " value for this setting.")
                hp1.SetShowHelp(txt, True)
                'Add the handler for the textbox, and add it to the form
                AddHandler txt.LostFocus, AddressOf textbox_handler
                tlp.Controls.Add(txt)
            ElseIf TypeOf (sets(i).Value) Is Color Then 'If the setting is a color use a button with no text for editing
                'Setup the label
                lbl.Text = sets(i).Name
                'Add the label
                tlp.Controls.Add(lbl)
                tlp.RowStyles.Add(x)
                'Setup the button
                Dim cmd As New Button
                cmd.Name = sets(i).TrueName
                cmd.Tag = GetTypeAbbr(sets(i).Value.GetType)
                cmd.BackColor = DirectCast(sets(i).Value, Color)
                cmd.TabIndex = tbIdx
                'Setup the button help string - for after clicking on the question mark button on the form
                hp1.SetHelpString(cmd, "Button that allows you to choose a color for this setting.")
                hp1.SetShowHelp(cmd, True)
                'Add the handlers for the button, and add it to the form
                AddHandler cmd.Click, AddressOf button_click
                AddHandler cmd.LostFocus, AddressOf button_handler
                tlp.Controls.Add(cmd)
            ElseIf TypeOf (sets(i).Value) Is Font Then 'If the setting is a font use a button with the font name for editing
                'Setup the label
                lbl.Text = sets(i).Name
                'Add the label
                tlp.Controls.Add(lbl)
                tlp.RowStyles.Add(x)
                'Setup the button
                Dim cmd As New Button
                cmd.AutoSize = True
                cmd.Name = sets(i).TrueName
                cmd.Tag = GetTypeAbbr(sets(i).Value.GetType)
                cmd.Text = CType(sets(i).Value, Font).Name
                cmd.Font = DirectCast(sets(i).Value, Font)
                cmd.UseVisualStyleBackColor = True
                cmd.TabIndex = tbIdx
                'Setup the button help string - for after clicking on the question mark button on the form
                hp1.SetHelpString(cmd, "Button that allows you to choose a font for this setting.")
                hp1.SetShowHelp(cmd, True)
                'Add the handlers for the button, and add it to the form
                AddHandler cmd.Click, AddressOf button_click
                AddHandler cmd.LostFocus, AddressOf button_handler
                tlp.Controls.Add(cmd)
                '###Peter Spiegler(12/12/2005)### enum type
            ElseIf TypeOf (sets(i).Value) Is [Enum] Then 'If the setting is a enum type we fill a combobox
                'Setup the label
                lbl.Text = sets(i).Name
                'Add the label
                tlp.Controls.Add(lbl)
                tlp.RowStyles.Add(x)
                'Setup the ComboBox
                Dim cbo As New ComboBox
                cbo.DropDownStyle = ComboBoxStyle.DropDownList
                cbo.Name = sets(i).TrueName
                cbo.Tag = GetTypeAbbr(sets(i).Value.GetType) 'not necessary, but everywhere else so...
                For Each o As Object In [Enum].GetValues(sets(i).Value.GetType)
                    cbo.Items.Add(o)
                Next
                cbo.SelectedIndex = Convert.ToInt32(sets(i).Value)
                cbo.Anchor = DirectCast(AnchorStyles.Left + AnchorStyles.Right + AnchorStyles.Top, AnchorStyles)
                cbo.TabIndex = tbIdx
                'Setup the help string - for after clicking on the question mark button on the form
                hp1.SetHelpString(cbo, "ComboBox that allows you to enter values of type " & sets(i).Value.GetType.FullName & "  for this setting.")
                hp1.SetShowHelp(cbo, True)
                'Add the handler for the Combobox, and add it to the form
                AddHandler cbo.LostFocus, AddressOf enum_handler
                tlp.Controls.Add(cbo)
            End If
            'Don't keep the label at autosize - remember it's most likely docked
            lbl.AutoSize = False
            'Increment the TabIndex
            tbIdx += 1
        Next
        'Update the TabIndex of controls already on the form
        chkSaveOnExit.TabIndex = tbIdx + 1
        cmdOK.TabIndex = tbIdx + 2
        cmdCancel.TabIndex = tbIdx + 3
        cmdApply.TabIndex = tbIdx + 4
    End Sub
#End Region

#Region " Property Handlers "
    'checkbox, datetimepicker, textbox, button, and enum(thanks Peter)
    'Handlers do not apply the values they set - just update the collection
    Private Sub checkbox_handler(ByVal sender As Object, ByVal e As System.EventArgs)
        'Checkboxes can only be boolean - update the setting in the collection
        Settings.SetValueByTrueName(sender.Name.ToString(), sender.Checked)
    End Sub
    Private Sub datetimepicker_handler(ByVal sender As Object, ByVal e As System.EventArgs)
        'the datetimepicker can only be a date - so update the collection
        Settings.SetValueByTrueName(sender.Name.ToString(), sender.Value)
    End Sub
    Private Sub textbox_handler(ByVal sender As Object, ByVal e As System.EventArgs)
        'byte, char, decimal, double, integer, long, sbyte, short, single, string, uinteger, ulong, ushort
        'is a list of everything that a textbox could contain
        'check the value in the textbox to make sure it
        'is of the correct type before adding it to the
        'collection - if it's not the correct type tell the
        'user so and select the offending value
        If CType(sender.Tag, String) = GetTypeAbbr(GetType(Byte)) Then
            Dim x As Byte = Nothing
            If Byte.TryParse(sender.Text.ToString(), x) Then
                Settings.SetValueByTrueName(sender.Name.ToString(), x)
            Else
                MsgBox("Could not convert " & sender.Text & " to a Byte.", MsgBoxStyle.Exclamation)
                sender.Focus()
                sender.SelectAll()
            End If
        ElseIf CType(sender.Tag, String) = GetTypeAbbr(GetType(Char)) Then
            Dim x As Char = Nothing
            If Char.TryParse(sender.Text.ToString(), x) Then
                Settings.SetValueByTrueName(sender.Name.ToString(), x)
            Else
                MsgBox("Could not convert " & sender.Text & " to a Char.", MsgBoxStyle.Exclamation)
                sender.Focus()
                sender.SelectAll()
            End If
        ElseIf CType(sender.Tag, String) = GetTypeAbbr(GetType(Decimal)) Then
            Dim x As Decimal = Nothing
            If Decimal.TryParse(sender.Text.ToString(), x) Then
                Settings.SetValueByTrueName(sender.Name.ToString(), x)
            Else
                MsgBox("Could not convert " & sender.Text & " to a Decimal.", MsgBoxStyle.Exclamation)
                sender.Focus()
                sender.SelectAll()
            End If
        ElseIf CType(sender.Tag, String) = GetTypeAbbr(GetType(Double)) Then
            Dim x As Double = Nothing
            If Double.TryParse(sender.Text.ToString(), x) Then
                Settings.SetValueByTrueName(sender.Name.ToString(), x)
            Else
                MsgBox("Could not convert " & sender.Text & " to a Double.", MsgBoxStyle.Exclamation)
                sender.Focus()
                sender.SelectAll()
            End If
        ElseIf CType(sender.Tag, String) = GetTypeAbbr(GetType(Integer)) Then
            Dim x As Integer = Nothing
            If Integer.TryParse(sender.Text.ToString(), x) Then
                Settings.SetValueByTrueName(sender.Name.ToString(), x)
            Else
                MsgBox("Could not convert " & sender.Text & " to a Integer.", MsgBoxStyle.Exclamation)
                sender.Focus()
                sender.SelectAll()
            End If
        ElseIf CType(sender.Tag, String) = GetTypeAbbr(GetType(Long)) Then
            Dim x As Long = Nothing
            If Long.TryParse(sender.Text.ToString(), x) Then
                Settings.SetValueByTrueName(sender.Name.ToString(), x)
            Else
                MsgBox("Could not convert " & sender.Text & " to a Long.", MsgBoxStyle.Exclamation)
                sender.Focus()
                sender.SelectAll()
            End If
        ElseIf CType(sender.Tag, String) = GetTypeAbbr(GetType(SByte)) Then
            Dim x As SByte = Nothing
            If SByte.TryParse(sender.Text.ToString(), x) Then
                Settings.SetValueByTrueName(sender.Name.ToString(), x)
            Else
                MsgBox("Could not convert " & sender.Text & " to a SByte.", MsgBoxStyle.Exclamation)
                sender.Focus()
                sender.SelectAll()
            End If
        ElseIf CType(sender.Tag, String) = GetTypeAbbr(GetType(Short)) Then
            Dim x As Short = Nothing
            If Short.TryParse(sender.Text.ToString(), x) Then
                Settings.SetValueByTrueName(sender.Name.ToString(), x)
            Else
                MsgBox("Could not convert " & sender.Text & " to a Short.", MsgBoxStyle.Exclamation)
                sender.Focus()
                sender.SelectAll()
            End If
        ElseIf CType(sender.Tag, String) = GetTypeAbbr(GetType(Single)) Then
            Dim x As Single = Nothing
            If Single.TryParse(sender.Text.ToString(), x) Then
                Settings.SetValueByTrueName(sender.Name.ToString(), x)
            Else
                MsgBox("Could not convert " & sender.Text & " to a Single.", MsgBoxStyle.Exclamation)
                sender.Focus()
                sender.SelectAll()
            End If
        ElseIf CType(sender.Tag, String) = GetTypeAbbr(GetType(String)) Then
            Settings.SetValueByTrueName(sender.Name.ToString(), sender.Text)
        ElseIf CType(sender.Tag, String) = GetTypeAbbr(GetType(UInteger)) Then
            Dim x As UInteger = Nothing
            If UInteger.TryParse(sender.Text.ToString(), x) Then
                Settings.SetValueByTrueName(sender.Name.ToString(), x)
            Else
                MsgBox("Could not convert " & sender.Text & " to a UInteger.", MsgBoxStyle.Exclamation)
                sender.Focus()
                sender.SelectAll()
            End If
        ElseIf CType(sender.Tag, String) = GetTypeAbbr(GetType(ULong)) Then
            Dim x As ULong = Nothing
            If ULong.TryParse(sender.Text.ToString(), x) Then
                Settings.SetValueByTrueName(sender.Name.ToString(), x)
            Else
                MsgBox("Could not convert " & sender.Text & " to a ULong.", MsgBoxStyle.Exclamation)
                sender.Focus()
                sender.SelectAll()
            End If
        ElseIf CType(sender.Tag, String) = GetTypeAbbr(GetType(UShort)) Then
            Dim x As UShort = Nothing
            If UShort.TryParse(sender.Text.ToString(), x) Then
                Settings.SetValueByTrueName(sender.Name.ToString(), x)
            Else
                MsgBox("Could not convert " & sender.Text & " to a UShort.", MsgBoxStyle.Exclamation)
                sender.Focus()
                sender.SelectAll()
            End If
        End If
    End Sub
    Private Sub button_click(ByVal sender As Object, ByVal e As System.EventArgs)
        'Buttons can contain either a Font, or a color
        'check which it is and bring up the appropriate dialog
        'box for editing
        If CType(sender.Tag, String) = GetTypeAbbr(GetType(Color)) Then
            dlgColor.Color = DirectCast(sender.BackColor, Color)
            If dlgColor.ShowDialog = Windows.Forms.DialogResult.OK Then sender.BackColor = dlgColor.Color
        ElseIf CType(sender.Tag, String) = GetTypeAbbr(GetType(Font)) Then
            dlgFont.Font = CType(sender, Button).Font
            If dlgFont.ShowDialog = Windows.Forms.DialogResult.OK Then
                sender.Font = dlgFont.Font
                sender.Text = dlgFont.Font.Name
            End If
        End If
    End Sub
    Private Sub button_handler(ByVal sender As Object, ByVal e As System.EventArgs)
        'Buttons can contain either a Font, or a color
        'check which it is to make sure we're updating
        'the correct value
        If CType(sender.Tag, String) = GetTypeAbbr(GetType(Color)) Then
            Settings.SetValueByTrueName(sender.Name.ToString(), sender.BackColor)
        ElseIf CType(sender.Tag, String) = GetTypeAbbr(GetType(Font)) Then
            Settings.SetValueByTrueName(sender.Name.ToString(), sender.Font)
        End If
    End Sub
    '###Peter Spiegler(12/12/2005)###  enum types
    Private Sub enum_handler(ByVal sender As Object, ByVal e As System.EventArgs)
        Settings.SetEnumByTrueName(sender.Name.ToString(), sender.text.ToString())
    End Sub
#End Region

#Region " Button Clicks "

    Private Sub cmdApply_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdApply.Click
        ApplySettings() 'Apply the settings don't save, let the application save when it exits
    End Sub

    Private Sub cmdOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdOK.Click
        '###Peter Spiegler(12/12/2005)###
        'if cmdOK_Click is called by pressing Return/Enter key set the focus to cmdOK, so that LostFocus is called
        ' and the  value is copied to settings
        cmdOK.Focus()

        ApplySettings() 'Apply the settings don't save, let the application save when it exits
        'Tell the dialog it can close and everything is ok
        Me.DialogResult = Windows.Forms.DialogResult.OK
    End Sub
    ''' <summary>
    ''' Applies the settings in the SettingInfoCollection to the actual setting in the My namespace.
    ''' </summary>
    Private Sub ApplySettings()
        'Cycle through each setting that we could edit and
        'update the real setting contained in the My namespace
        For Each si As SettingInfo In Settings
            My.Settings.Item(si.TrueName) = si.Value
        Next
        'Update the SaveOnExit value
        My.Application.SaveMySettingsOnExit = chkSaveOnExit.Checked
    End Sub

#End Region

End Class

''' <summary>
''' The categories for the settings - used for loading FFPane and TabPages
''' </summary>
''' <remarks></remarks>
Public Class SettingCategory
    Public MainCat As String
    Public SubCats As String
End Class
''' <summary>
''' A collection of SettingCategory object - used for returning the Main and SubCategories separately
''' </summary>
''' <remarks></remarks>
Public Class SettingCategoryCollection
    Inherits CollectionBase

    ''' <summary>
    ''' Adds a Setting
    ''' </summary>
    ''' <param name="item">A SettingCategory Object</param>
    ''' <remarks></remarks>
    Public Sub Add(ByVal item As SettingCategory)
        List.Add(item)
    End Sub
    ''' <summary>
    ''' Returns a specific element of the collection by position. Read-only.
    ''' </summary>
    ''' <param name="index">A numeric expression that specifies the position of an element of the collection. Index must be a number from 0 through the value of the Collection's Count Property.</param>
    ''' <returns>A SettingCategory object from the position specified.</returns>
    Default ReadOnly Property Item(ByVal index As Integer) As SettingCategory
        Get
            Return CType(List.Item(index), SettingCategory)
        End Get
    End Property

    ''' <summary>
    ''' Returns the position of a category and it's subcategory within the collection.
    ''' </summary>
    ''' <param name="name">The name of the main category</param>
    ''' <param name="subc">The subcategory(ies).  If multiple use dot-notation to separate the sub-categories</param>
    ''' <returns>An integer that represent the index of the position of the category/subcategory pair</returns>
    Public Function IndexOf(ByVal name As String, ByVal subc As String) As Integer
        For i As Integer = 0 To Count - 1
            If CType(List.Item(i), SettingCategory).MainCat = name And CType(List.Item(i), SettingCategory).SubCats = subc Then Return i
        Next
        Return -1
    End Function
End Class
''' <summary>
''' An object to hold an indvidual settings data
''' </summary>
Public Class SettingInfo

    Private _Name As String
    ''' <summary>
    ''' The display name of the setting.
    ''' </summary>
    ''' <value>The display name for the setting</value>
    ''' <returns>The display name for the setting</returns>
    Public Property Name() As String
        Get
            Return _Name
        End Get
        Set(ByVal value As String)
            _Name = value
        End Set
    End Property

    Private _Category As String
    ''' <summary>
    ''' The category to which the setting belongs.
    ''' </summary>
    ''' <value>The category to which the setting belongs</value>
    ''' <returns>The dot seperated category/sub-category to which the setting belongs</returns>
    Public ReadOnly Property Category() As String
        Get
            Return _Category
        End Get
    End Property
    ''' <summary>
    ''' Sets the category based on an array of the categories/sub-categories.
    ''' </summary>
    ''' <param name="values">The category/sub-category list.  Index 0 is the category and the sub-categories follow.</param>
    Public Sub SetCategory(ByVal values() As String)
        'The category should contain a dot separated list of values - implode does this nicely
        _Category = implode(".", values)
    End Sub
    ''' <summary>
    ''' Sets the category to the string provided.
    ''' </summary>
    ''' <param name="value">The actual dot separated category/sub-category list. (ex Category1.SubCat1.SubCat2)</param>
    Public Sub SetCategory(ByVal value As String)
        'If the value is already in dot separated format just updated (doesn't check)
        _Category = value
    End Sub
    ''' <summary>
    ''' Takes an array of values and concatenates them separating each item in the array with the value in chr.
    ''' </summary>
    ''' <param name="chr">A character or string which separates the values in "values".</param>
    ''' <param name="values">An array of strings to be concatenated and separated by chr.</param>
    ''' <returns>A string containing all the values in the array provided concatenated and separated by the value in chr.</returns>
    Private Function implode(ByVal chr As String, ByVal values() As String) As String
        'Taken from the php function implode
        'Setup an empty string for building
        Dim tmp As String = Nothing
        'Cycle through the array
        For Each str As String In values
            'Add the current value and the separator to the string
            tmp &= str & chr
        Next
        'The result has an extra delimiter on the end remove it
        tmp = tmp.TrimEnd(chr)
        'Return the finished result
        Return tmp
    End Function

    ''' <summary>
    ''' Loads a My.Settings setting name into the current SettingInfo object.
    ''' </summary>
    ''' <param name="str">A My.Settings name in the following format (Category_SubCategory1_SubCategory2_etc..._Setting__Name_SortIndex).</param>
    ''' <remarks>A single underscore separates the categories, name, and sort index.  A double underscore signifies a space.  Not providing a sort index, will give the setting a sort index of -1.</remarks>
    Public Sub LoadData(ByVal str As String)
        'Replace the double underscore with a space to allow the window
        'to show multi-word Settings names
        str = str.Replace("__", " ")
        'Check if the last value (separated by underscores) is numeric
        If MyNumeric(str.Substring(str.LastIndexOf("_") + 1)) Then
            'It is numeric - so use it as the SortIndex
            _Sort = Integer.Parse(str.Substring(str.LastIndexOf("_") + 1))
            'Remove it from the string
            str = str.Substring(0, str.LastIndexOf("_"))
        Else
            'Not numeric assign default SortIndex of -1
            _Sort = -1
        End If
        'Assign the name of the setting - should be that last value in the string
        _Name = str.Substring(str.LastIndexOf("_") + 1)
        'Get the category string (don't include the name)
        Dim cat As String = str.Substring(0, str.LastIndexOf("_"))
        'Assign the category (SetCategory takes a string array so use split)
        SetCategory(cat.Split("_"))
        'Get the value from the My namespace and assign it
        _Value = My.Settings.Item(TrueName)
    End Sub
    ''' <summary>
    ''' Checks if the string is a pure numeric value.
    ''' </summary>
    ''' <param name="val">A string containing the value to be checked.</param>
    ''' <returns>True if the value is numeric, false if the value isn't numeric.</returns>
    ''' <remarks>Cycles through each character in the value passed and checks if it is one of the following: 1234567890.  If it's not the value isn't numeric and it returns false.</remarks>
    Private Function MyNumeric(ByVal val As String) As Boolean
        'Create a string array for valid characters of a numeric string
        Dim chr() As String = {"1", "2", "3", "4", "5", "6", "7", "8", "9", "0"}
        'Check each character in the passed value
        For i As Integer = 0 To val.Length - 1
            If Array.IndexOf(chr, val.Substring(i, 1)) = -1 Then Return False 'Not a number
        Next
        'Successfully made it through the test - it is a number
        Return True
    End Function

    Private _Sort As Integer
    ''' <summary>
    ''' The SortIndex of the setting name.  Determines where the setting will be display on the form once it's category is selected.
    ''' </summary>
    ''' <value>Integer</value>
    ''' <returns>Integer</returns>
    Public Property SortIndex() As Integer
        Get
            Return _Sort
        End Get
        Set(ByVal value As Integer)
            _Sort = value
        End Set
    End Property

    Private _Value As Object
    ''' <summary>
    ''' The value of the setting.  Could be any object type from boolean to font.
    ''' </summary>
    ''' <value>Object containing the settings value.</value>
    ''' <returns>Object containing the settings value.</returns>
    Public Property Value() As Object
        Get
            Return _Value
        End Get
        Set(ByVal value As Object)
            _Value = value
        End Set
    End Property

    ''' <summary>
    ''' The true name of the setting.  Computes the original setting name from the category, name, and sort index.
    ''' </summary>
    ''' <returns>String containing the true setting name.</returns>
    Public ReadOnly Property TrueName() As String
        'Rebuilds the original setting name as specified in the My namespace
        Get
            'Create temporary string and give it the value of the category replacing the periods with underscores
            'Also add the name onto the end of it
            Dim tmp As String = _Category.Replace(".", "_") & "_" & _Name
            'If there is a valid SortIndex - add that to the end as well
            If _Sort <> -1 Then tmp &= "_" & SortIndex
            'Return a value with no spaces (use double underscores instead)
            Return tmp.Replace(" ", "__")
        End Get
    End Property

    ''' <summary>
    ''' Clones the current SettingInfo object to another variable.
    ''' </summary>
    ''' <returns>A copy of the current SettingInfo object.</returns>
    Public Function Clone() As SettingInfo
        'Build a copy of this SettingInfo and return it
        Dim tmp As New SettingInfo
        tmp.Name = _Name
        tmp.SetCategory(_Category)
        tmp.SortIndex = _Sort
        tmp.Value = _Value
        Return tmp
    End Function
End Class
''' <summary>
''' A collection of SettingInfo objects
''' </summary>
Public Class SettingInfoCollection
    Inherits CollectionBase

    ''' <summary>
    ''' Adds a SettingInfo object to the collection.
    ''' </summary>
    ''' <param name="item">A SettingInfo object</param>
    Public Sub Add(ByVal item As SettingInfo)
        'Add the item to the list
        List.Add(item)
    End Sub

    ''' <summary>
    ''' Returns a specific element of the collection by position. Read-only.
    ''' </summary>
    ''' <param name="index">A numeric expression that specifies the position of an element of the collection. Index must be a number from 0 through the value of the Collection's Count Property.</param>
    ''' <returns>A SettingInfo object from the position specified.</returns>
    Default Public ReadOnly Property Item(ByVal index As Integer) As SettingInfo
        Get
            'Return the selected item (make sure it is the correct type)
            Return CType(List.Item(index), SettingInfo)
        End Get
    End Property

    ''' <summary>
    ''' Sets the values of a SettingInfo object already in the collection.
    ''' </summary>
    ''' <param name="index">A numeric expression that specifies the position of an element you wish to change in the collection. Index must be a number from 0 through the value of the Collection's Count Property.</param>
    ''' <param name="value">A SettingInfo object that contains the values you wish to set to the element specified by Index.</param>
    Public Sub SetItem(ByVal index As Integer, ByVal value As SettingInfo)
        'Update an item already in the collection, must do it one value at a time
        'otherwise we just get a reference
        CType(List.Item(index), SettingInfo).SetCategory(value.Category)
        CType(List.Item(index), SettingInfo).Name = value.Name
        CType(List.Item(index), SettingInfo).Value = value.Value
        CType(List.Item(index), SettingInfo).SortIndex = value.SortIndex
    End Sub

    ''' <summary>
    ''' Get's all the SettingInfo objects in the collection that have the same category as the one specified.
    ''' </summary>
    ''' <param name="category">A string expression that specifies the category of the elements to retrieve.  Category must be in the dot separated format used by the SettingInfo objects (ex. Category.SubCategory.SubCat2)</param>
    ''' <returns>An array of SettingInfo objects that match the category specified.</returns>
    Public Function GetByCategory(ByVal category As String) As SettingInfo()
        'Create and empty array used for return the found data
        Dim tmp() As SettingInfo = Nothing
        'An integer to store the current array index
        Dim iT As Integer = 0
        'A boolean to tell us if we need to sort it
        Dim sortIt As Boolean = False
        'Cycle through all the settings
        For si As Integer = 0 To Count - 1
            'Does the category match?
            If CType(List.Item(si), SettingInfo).Category = category Then
                'Yes - Initialize current array itemj
                ReDim Preserve tmp(iT)
                'If the SortIndex isn't default (-1) then we need to sort the entire array when done
                If CType(List.Item(si), SettingInfo).SortIndex <> -1 Then sortIt = True
                'Copy the item to the array
                tmp(iT) = List.Item(si)
                'Increment the index
                iT += 1
            End If
        Next
        'Do we need to sort it?
        If sortIt Then
            'Yes - call the quicksort subroutine for this array
            Me.SortIt(tmp, 0, UBound(tmp))
        End If
        'Return the sorted (if necessary) array
        Return tmp
    End Function

    ''' <summary>
    ''' Get's all the SettingInfo objects in the collection that have the same category as the one specified.
    ''' </summary>
    ''' <param name="cat">A string expression that specifies the category of the elements to retrieve.  Category must be in the dot separated format used by the SettingInfo objects (ex. Category.SubCategory.SubCat2)</param>
    ''' <returns>An array of SettingInfo objects that match the category specified.</returns>
    ''' <remarks>Used only for the TabPages as the category could be condensed from the real form.</remarks>
    Public Function GetByTabCategory(ByVal cat As String) As SettingInfo()
        'Create and empty array used for return the found data
        Dim tmp() As SettingInfo = Nothing
        'An integer to store the current array index
        Dim iT As Integer = 0
        'A boolean to tell us if we need to sort it
        Dim sortIt As Boolean = False
        'Cycle through all the settings
        For si As Integer = 0 To Count - 1
            'Does the category match?
            If CType(List.Item(si), SettingInfo).Category.StartsWith(cat) Then
                'Yes - Initialize current array itemj
                ReDim Preserve tmp(iT)
                'If the SortIndex isn't default (-1) then we need to sort the entire array when done
                If CType(List.Item(si), SettingInfo).SortIndex <> -1 Then sortIt = True
                'Copy the item to the array
                tmp(iT) = List.Item(si)
                'Increment the index
                iT += 1
            End If
        Next
        'Do we need to sort it?
        If sortIt Then
            'Yes - call the quicksort subroutine for this array
            Me.SortIt(tmp, 0, UBound(tmp))
        End If
        'Return the sorted (if necessary) array
        Return tmp
    End Function

    Public Function GetCategories() As SettingCategoryCollection
        Dim tmp As New SettingCategoryCollection
        Dim add As SettingCategory
        Dim name As String
        Dim subn As String
        For Each si As SettingInfo In List
            name = Nothing
            subn = Nothing
            If si.Category.IndexOf(".") = -1 Then name = si.Category Else name = si.Category.Substring(0, si.Category.IndexOf("."))
            If si.Category.IndexOf(".") = -1 Then subn = Nothing Else subn = si.Category.Substring(si.Category.IndexOf("."))
            If tmp.IndexOf(name, subn) = -1 Then
                add = New SettingCategory
                add.MainCat = name
                add.SubCats = subn
                tmp.Add(add)
            End If
        Next
        Return tmp
    End Function

    ''' <summary>
    ''' Sorts an array of SettingInfo objects by their SortIndex.
    ''' </summary>
    ''' <param name="SortArray">A reference to the array you wish to sort.</param>
    ''' <param name="First">The starting element of the selection you wish to sort (usually 0).</param>
    ''' <param name="Last">Then final element of the selection you wish to sort (usually the upper bound of the array).</param>
    ''' <remarks>Called from GetByCategory</remarks>
    Private Sub SortIt(ByRef SortArray() As SettingInfo, ByVal First As Integer, ByVal Last As Integer)
        'Copied and modified from the SortIt method in the form
        Dim Low As Integer, High As Integer
        Dim Temp As SettingInfo = Nothing
        Dim List_Separator As SettingInfo = Nothing
        Low = First
        High = Last
        List_Separator = SortArray(Convert.ToInt32((First + Last) / 2)).Clone
        Do
            Do While (SortArray(Low).SortIndex < List_Separator.SortIndex)
                Low += 1
            Loop
            Do While (SortArray(High).SortIndex > List_Separator.SortIndex)
                High -= 1
            Loop
            If (Low <= High) Then
                Temp = SortArray(Low).Clone
                SortArray(Low) = SortArray(High).Clone
                SortArray(High) = Temp.Clone
                Low += 1
                High -= 1
            End If
        Loop While (Low <= High)
        If (First < High) Then SortIt(SortArray, First, High)
        If (Low < Last) Then SortIt(SortArray, Low, Last)
    End Sub

    ''' <summary>
    ''' Sets the value of an SettingInfo object already in the collection by it's true name.
    ''' </summary>
    ''' <param name="name">The name of the setting as specified in the My.Settings object.</param>
    ''' <param name="value">The value you wish to set to the specified setting.</param>
    ''' <remarks>Called by the control handlers when the controls lose focus.</remarks>
    Public Sub SetValueByTrueName(ByVal name As String, ByVal value As Object)
        'Loop through all items in the list
        For i As Integer = 0 To Count - 1
            'Does the item's TrueName match the name passed?
            If CType(List(i), SettingInfo).TrueName = name Then
                'Yes - then update the value and exit
                If (TypeOf (CType(List(i), SettingInfo).Value) Is System.Collections.Specialized.StringCollection) Then
                    Dim strings() As String = value.ToString.Split(vbNewLine)
                    Dim strList As New System.Collections.Specialized.StringCollection()

                    For Each str As String In strings
                        If (strList.Count = 0) Then
                            strList.Add(str)
                        Else
                            strList.Add(str.Substring(Math.Min(str.Length, 1), str.Length - 1))
                        End If
                    Next
                    CType(List(i), SettingInfo).Value = strList
                Else
                    CType(List(i), SettingInfo).Value = value
                End If


                Exit Sub
            End If
        Next
    End Sub

    '###Peter Spiegler(12/12/2005)###  enum types
    ''' <summary>
    ''' Sets the value of a SettingInfo object already in the collection using it's true name, where the value is of EnumType
    ''' </summary>
    ''' <param name="name">The name of the setting as specified the the My.Settings object.</param>
    ''' <param name="EnumText">The enumeration name you wish to set the value to.</param>
    ''' <remarks>Called by the control handlers when the controls lose focus.</remarks>
    Public Sub SetEnumByTrueName(ByVal name As String, ByVal EnumText As String)
        'Loop through all items in the list
        For i As Integer = 0 To Count - 1
            'Does the item's TrueName match the name passed?
            If CType(List(i), SettingInfo).TrueName = name Then
                'Yes - then update the value and exit
                CType(List(i), SettingInfo).Value = [Enum].Parse(CType(List(i), SettingInfo).Value.GetType, EnumText)
                Exit Sub
            End If
        Next
    End Sub
End Class

''' <summary>
''' Represents a panel that dynamically lays out FFItems in a column similar to the FireFox options dialog.
''' </summary>
Public Class FireFoxPane
    Inherits System.Windows.Forms.TableLayoutPanel

#Region " Properties "

    Private _SelectedIndex As Integer = 0
    ''' <summary>
    ''' Gets the zero-based index of the currently selected item in a FireFoxPane
    ''' </summary>
    ''' <returns>An integer containing the zero-based index of the currently selected item in a FireFoxPane</returns>
    Public ReadOnly Property SelectedIndex() As Integer
        Get
            Return _SelectedIndex
        End Get
    End Property
    ''' <summary>
    ''' Sets currently selected item in a FireFox pane with a zero-based index, without triggering SelectedIndexChanged
    ''' </summary>
    ''' <param name="value">The zero-based index of the item you want to select</param>
    Public Sub SetSelectedIndex(ByVal value As Integer)
        If value < 0 Or value >= Me.Controls.Count Then Throw New IndexOutOfRangeException : Exit Sub
        Dim fitem As FFItem = CType(Me.GetControlFromPosition(0, SelectedIndex), FFItem)
        fitem.Selected = False
        _SelectedIndex = value
        fitem = CType(Me.GetControlFromPosition(0, SelectedIndex), FFItem)
        fitem.Selected = True
    End Sub
    ''' <summary>
    ''' Sets the currently selected item in a FireFox pane with a zero-based index, will trigger SelectedIndexChanged
    ''' </summary>
    ''' <param name="val">The zero-based index of the item you want to select</param>
    Private Sub SetSelIndex(ByVal val As Integer)
        If val < 0 Or val >= Me.Controls.Count Then Throw New IndexOutOfRangeException : Exit Sub
        Dim blnRaise As Boolean = False
        blnRaise = Not (_SelectedIndex = val)
        _SelectedIndex = val
        If blnRaise Then RaiseEvent SelectedIndexChanged(Me, New System.EventArgs())
    End Sub

#End Region

    ''' <summary>
    ''' Occurs when the FireFoxPane.SelectedIndex property has changed.
    ''' </summary>
    Public Event SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs)

    ''' <summary>
    ''' Gets the item at the specified zero-based index.
    ''' </summary>
    ''' <param name="index">An integer representing the item in the FireFoxPane you wish to retrieve. Zero-based index.</param>
    ''' <returns>A FireFoxItem</returns>
    Public Function GetItem(ByVal index As Integer) As FFItem
        If index < 0 Or index >= Me.Controls.Count Then Throw New IndexOutOfRangeException : Return New FFItem()
        Return CType(Me.GetControlFromPosition(0, index), FFItem)
    End Function

    ''' <summary>
    ''' Initializes a new instance of the FireFoxPane class.
    ''' </summary>
    Public Sub New()
        Me.BackColor = Color.White
        HScroll = False
    End Sub

    ''' <summary>
    ''' Raises the Paint event for the control.
    ''' </summary>
    ''' <param name="e">A System.Windows.Forms.PaintEventArgs that contains the event data.</param>
    Protected Overrides Sub OnPaint(ByVal e As System.Windows.Forms.PaintEventArgs)
        'Paint like normal
        MyBase.OnPaint(e)
    End Sub
    ''' <summary>
    ''' Draws the XP style border around the FireFoxPane.
    ''' </summary>
    ''' <param name="g">A System.Drawing.Graphics object that contains the graphic surface to draw on.</param>
    Private Sub Draw_Border(ByRef g As Graphics)
        '124,159,181
        Dim brdrColor As Color = Color.FromArgb(124, 159, 181)
        g.DrawRectangle(New Pen(brdrColor), 0, 0, Me.Width - 1, Me.Height - 1)
    End Sub

    ''' <summary>
    ''' Raises the FireFoxPane.ControlAdded event.
    ''' </summary>
    ''' <param name="e">A System.Windows.Forms.ControlEventArgs that contains the event data.</param>
    Protected Overrides Sub OnControlAdded(ByVal e As System.Windows.Forms.ControlEventArgs)
        'Update any anchor styles so already added items will float
        If Me.Controls.Count <> 0 Then Me.Controls.Item(Me.Controls.Count - 1).Anchor = AnchorStyles.None
        'Add the control
        MyBase.OnControlAdded(e)
        'Set this controls anchor so it doesn't float away from the other items
        e.Control.Anchor = AnchorStyles.Top
        'Give it a name, so as not to confuse anything that might be confused by no names at all
        e.Control.Name = "FFItem" & Me.Controls.Count
        'Set the Index of the item equal to the row number
        CType(e.Control, FFItem).Index = Me.Controls.Count - 1
        'Add the handler to allow the panel to work with it
        AddHandler e.Control.Click, AddressOf InnerControlClicked
        'If its not an FFItem we don't want it
        If Not TypeOf (e.Control) Is FFItem Then
            Me.Controls.Remove(e.Control)
        End If
    End Sub
    ''' <summary>
    ''' Raises the FireFoxPane.ControlRemoved event.
    ''' </summary>
    ''' <param name="e">A System.Windows.Forms.ControlEventArgs that contains the event data.</param>
    Protected Overrides Sub OnControlRemoved(ByVal e As System.Windows.Forms.ControlEventArgs)
        'Remove the handler that was added
        RemoveHandler e.Control.Click, AddressOf InnerControlClicked
        'Remove the control
        MyBase.OnControlRemoved(e)
        'Update any anchorstyles if necessary
        If Me.Controls.Count <> 0 Then Me.Controls.Item(Me.Controls.Count - 1).Anchor = AnchorStyles.Top
        'Update the SelectedIndex, if the last one was selected and it's index changed
        If SelectedIndex + 1 > Me.Controls.Count Then SetSelectedIndex(Me.Controls.Count - 1)
        'Update the indices (required if an item was removed from between other items)
        For i As Integer = 0 To Me.RowCount - 1
            CType(Me.GetControlFromPosition(0, i), FFItem).Index = i
        Next
    End Sub

    ''' <summary>
    ''' Receives a call when the cell should be refreshed.
    ''' </summary>
    ''' <param name="e">A System.Windows.Forms.TableLayoutCellPaintEventArgs that provides data for the event.</param>
    ''' <remarks>Commented out the painting on 11-29-2005 when the item itself started doing it.</remarks>
    Protected Overrides Sub OnCellPaint(ByVal e As System.Windows.Forms.TableLayoutCellPaintEventArgs)
        MyBase.OnCellPaint(e)
        ' If currently selected - paint the color on top of everything else
        If e.Row = SelectedIndex Then
            'Dim br As New SolidBrush(Color.FromArgb(150, 129, 159, 181))
            'e.Graphics.FillRectangle(br, e.CellBounds)
            'Dim ctrl As Control = Me.GetControlFromPosition(e.Column, e.Row)
            'ctrl.CreateGraphics.FillRectangle(br, e.CellBounds)
        End If
    End Sub

    ''' <summary>
    ''' Receives a call when an Item in a cell is clicked.
    ''' </summary>
    ''' <param name="sender">A reference to the FFItem that was clicked.</param>
    ''' <param name="e">A System.EventArgs that contains data for the event.</param>
    ''' <remarks>Yea right, on the comments for e - does make it look official though :)</remarks>
    Private Sub InnerControlClicked(ByVal sender As Object, ByVal e As System.EventArgs)
        'get the row
        Dim rw As Integer = CType(sender, FFItem).Index
        If rw <> SelectedIndex Then
            'change it
            Dim fitem As FFItem = CType(Me.GetControlFromPosition(0, SelectedIndex), FFItem)
            fitem.Selected = False
            CType(sender, FFItem).Selected = True
            SetSelIndex(rw)
        End If
    End Sub

End Class

''' <summary>
''' Represents a panel that dynamically lays out FFItems in a column similar to the FireFox (1.5+) options dialog.
''' </summary>
Public Class FireFoxBar
    Inherits System.Windows.Forms.TableLayoutPanel
#Region " Properties "

    Private _SelectedIndex As Integer = 0
    ''' <summary>
    ''' Gets the zero-based index of the currently selected item in a FireFoxPane
    ''' </summary>
    ''' <returns>An integer containing the zero-based index of the currently selected item in a FireFoxPane</returns>
    Public ReadOnly Property SelectedIndex() As Integer
        Get
            Return _SelectedIndex
        End Get
    End Property
    ''' <summary>
    ''' Sets currently selected item in a FireFox bar with a zero-based index, without triggering SelectedIndexChanged
    ''' </summary>
    ''' <param name="value">The zero-based index of the item you want to select</param>
    Public Sub SetSelectedIndex(ByVal value As Integer)
        If value < 0 Or value >= Me.Controls.Count Then Throw New IndexOutOfRangeException : Exit Sub
        Dim fitem As FFItem = CType(Me.GetControlFromPosition(SelectedIndex, 0), FFItem)
        fitem.Selected = False
        _SelectedIndex = value
        fitem = CType(Me.GetControlFromPosition(SelectedIndex, 0), FFItem)
        fitem.Selected = True
    End Sub
    ''' <summary>
    ''' Sets the currently selected item in a FireFox pane with a zero-based index, will trigger SelectedIndexChanged
    ''' </summary>
    ''' <param name="val">The zero-based index of the item you want to select</param>
    Private Sub SetSelIndex(ByVal val As Integer)
        If val < 0 Or val >= Me.Controls.Count Then Throw New IndexOutOfRangeException : Exit Sub
        Dim blnRaise As Boolean = False
        blnRaise = Not (_SelectedIndex = val)
        _SelectedIndex = val
        If blnRaise Then RaiseEvent SelectedIndexChanged(Me, New System.EventArgs())
    End Sub

#End Region

    ''' <summary>
    ''' Occurs when the FireFoxBar.SelectedIndex property has changed.
    ''' </summary>
    Public Event SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs)

    ''' <summary>
    ''' Gets the item at the specified zero-based index.
    ''' </summary>
    ''' <param name="index">An integer representing the item in the FireFoxBar you wish to retrieve. Zero-based index.</param>
    ''' <returns>A FireFoxItem</returns>
    Public Function GetItem(ByVal index As Integer) As FFItem
        If index < 0 Or index >= Me.Controls.Count Then Throw New IndexOutOfRangeException : Return New FFItem()
        Return CType(Me.GetControlFromPosition(index, 0), FFItem)
    End Function

    ''' <summary>
    ''' Initializes a new instance of the FireFoxPane class.
    ''' </summary>
    Public Sub New()
        Me.BackColor = Color.White
        HScroll = True
        VScroll = False
    End Sub

    ''' <summary>
    ''' Raises the Paint event for the control.
    ''' </summary>
    ''' <param name="e">A System.Windows.Forms.PaintEventArgs that contains the event data.</param>
    Protected Overrides Sub OnPaint(ByVal e As System.Windows.Forms.PaintEventArgs)
        'Paint like normal
        MyBase.OnPaint(e)
    End Sub
    ''' <summary>
    ''' Draws the XP style border around the FireFoxPane.
    ''' </summary>
    ''' <param name="g">A System.Drawing.Graphics object that contains the graphic surface to draw on.</param>
    Private Sub Draw_Border(ByRef g As Graphics)
        '124,159,181
        Dim brdrColor As Color = Color.FromArgb(124, 159, 181)
        g.DrawRectangle(New Pen(brdrColor), 0, 0, Me.Width - 1, Me.Height - 1)
    End Sub

    ''' <summary>
    ''' Raises the FireFoxBar.ControlAdded event.
    ''' </summary>
    ''' <param name="e">A System.Windows.Forms.ControlEventArgs that contains the event data.</param>
    Protected Overrides Sub OnControlAdded(ByVal e As System.Windows.Forms.ControlEventArgs)
        'Update any anchor styles so already added items will float
        If Me.Controls.Count <> 0 Then Me.Controls.Item(Me.Controls.Count - 1).Anchor = AnchorStyles.None
        'Add the control
        MyBase.OnControlAdded(e)
        'Set this controls anchor so it doesn't float away from the other items
        e.Control.Anchor = AnchorStyles.Left
        'Give it a name, so as not to confuse anything that might be confused by no names at all
        e.Control.Name = "FFItem" & Me.Controls.Count
        'Set the Index of the item equal to the row number
        CType(e.Control, FFItem).Index = Me.Controls.Count - 1
        'Add the handler to allow the panel to work with it
        AddHandler e.Control.Click, AddressOf InnerControlClicked
        'If its not an FFItem we don't want it
        If Not TypeOf (e.Control) Is FFItem Then
            Me.Controls.Remove(e.Control)
        End If
    End Sub
    ''' <summary>
    ''' Raises the FireFoxPane.ControlRemoved event.
    ''' </summary>
    ''' <param name="e">A System.Windows.Forms.ControlEventArgs that contains the event data.</param>
    Protected Overrides Sub OnControlRemoved(ByVal e As System.Windows.Forms.ControlEventArgs)
        'Remove the handler that was added
        RemoveHandler e.Control.Click, AddressOf InnerControlClicked
        'Remove the control
        MyBase.OnControlRemoved(e)
        'Update any anchorstyles if necessary
        If Me.Controls.Count <> 0 Then Me.Controls.Item(Me.Controls.Count - 1).Anchor = AnchorStyles.Left
        'Update the SelectedIndex, if the last one was selected and it's index changed
        If SelectedIndex + 1 > Me.Controls.Count Then SetSelectedIndex(Me.Controls.Count - 1)
        'Update the indices (required if an item was removed from between other items)
        For i As Integer = 0 To Me.ColumnCount - 1
            CType(Me.GetControlFromPosition(i, 0), FFItem).Index = i
        Next
    End Sub

    ''' <summary>
    ''' Receives a call when the cell should be refreshed.
    ''' </summary>
    ''' <param name="e">A System.Windows.Forms.TableLayoutCellPaintEventArgs that provides data for the event.</param>
    ''' <remarks>Commented out the painting on 11-29-2005 when the item itself started doing it.</remarks>
    Protected Overrides Sub OnCellPaint(ByVal e As System.Windows.Forms.TableLayoutCellPaintEventArgs)
        MyBase.OnCellPaint(e)
        ' If currently selected - paint the color on top of everything else
        If e.Row = SelectedIndex Then
            'Dim br As New SolidBrush(Color.FromArgb(150, 129, 159, 181))
            'e.Graphics.FillRectangle(br, e.CellBounds)
            'Dim ctrl As Control = Me.GetControlFromPosition(e.Column, e.Row)
            'ctrl.CreateGraphics.FillRectangle(br, e.CellBounds)
        End If
    End Sub

    ''' <summary>
    ''' Receives a call when an Item in a cell is clicked.
    ''' </summary>
    ''' <param name="sender">A reference to the FFItem that was clicked.</param>
    ''' <param name="e">A System.EventArgs that contains data for the event.</param>
    ''' <remarks>Yea right, on the comments for e - does make it look official though :)</remarks>
    Private Sub InnerControlClicked(ByVal sender As Object, ByVal e As System.EventArgs)
        'get the column
        Dim rw As Integer = CType(sender, FFItem).Index
        If rw <> SelectedIndex Then
            'change it
            Dim fitem As FFItem = CType(Me.GetControlFromPosition(SelectedIndex, 0), FFItem)
            fitem.Selected = False
            CType(sender, FFItem).Selected = True
            SetSelIndex(rw)
        End If
    End Sub

End Class
''' <summary>
''' Represents a category button for a FireFoxPane object - displays Icon and text similar to the FireFox options dialog.
''' </summary>
Public Class FFItem
    Inherits System.Windows.Forms.Control

    Private _Image As Image
    ''' <summary>
    ''' Gets or sets the image that the FFItem displays. Image size must be 32x32.
    ''' </summary>
    ''' <value>A 32x32 image</value>
    ''' <returns>A 32x32 image</returns>
    Public Property Image() As Image
        Get
            Return _Image
        End Get
        Set(ByVal value As Image)
            If value.Height <> 32 Or value.Width <> 32 Then
                Throw New Exception("Image size must be 32x32.")
                _Image = New Bitmap(32, 32)
                Exit Property
            End If
            _Image = value
            Me.Invalidate()
        End Set
    End Property

    Private _Text As String
    ''' <summary>
    ''' Gets or sets the text associated with this control.
    ''' </summary>
    Public Overrides Property Text() As String
        Get
            Return _Text
        End Get
        Set(ByVal value As String)
            _Text = value
            Dim txtWidth As Integer = Me.CreateGraphics.MeasureString(value, Me.Font, New PointF(0, 0), New StringFormat(StringFormatFlags.NoWrap)).Width
            Dim txtHeight As Integer = Me.CreateGraphics.MeasureString(value, Me.Font, New PointF(0, 0), New StringFormat(StringFormatFlags.NoWrap)).Height
            Me.Height = 32 + txtHeight
            Me.Invalidate()
        End Set
    End Property

    Private _Selected As Boolean = False
    ''' <summary>
    ''' Gets or sets whether this control has been Selected by the users or not.
    ''' </summary>
    <System.ComponentModel.Browsable(False)> _
    Public Property Selected() As Boolean
        Get
            Return _Selected
        End Get
        Set(ByVal value As Boolean)
            _Selected = value
            If value Then Highlighted = False
            Me.Invalidate()
        End Set
    End Property

    Private _Index As Integer
    ''' <summary>
    ''' Gets or sets the Index (row number) of this control.
    ''' </summary>
    Public Property Index() As Integer
        Get
            Return _Index
        End Get
        Set(ByVal value As Integer)
            _Index = value
        End Set
    End Property

    ''' <summary>
    ''' Specifies whether the mouse is over the control (used for Hot-tracking).
    ''' </summary>
    Private Highlighted As Boolean = False

    ''' <summary>
    ''' Initializes a new instance of the FFItem class with default settings.
    ''' </summary>
    Public Sub New()
        Me.New("", New Bitmap(32, 32))
    End Sub
    ''' <summary>
    ''' Initializes a new instance of the FFItem class with some default settings.
    ''' </summary>
    ''' <param name="txt">The text displayed by the control</param>
    Public Sub New(ByVal txt As String)
        Me.New(txt, New Bitmap(32, 32))
    End Sub
    ''' <summary>
    ''' Initializes a new instance of the FFItem class with some default settings.
    ''' </summary>
    ''' <param name="img">The 32x32 image displayed by the control.</param>
    Public Sub New(ByVal img As Image)
        Me.New("", img)
    End Sub
    ''' <summary>
    ''' Initializes a new instance of the FFItem class.
    ''' </summary>
    ''' <param name="txt">The text displayed by the control.</param>
    ''' <param name="img">The 32x32 image displayed by the control</param>
    ''' <remarks></remarks>
    Public Sub New(ByVal txt As String, ByVal img As Image)
        Text = txt
        Image = img
        SetStyle(ControlStyles.OptimizedDoubleBuffer, True)
        SetStyle(ControlStyles.UserPaint, True)
    End Sub

    ''' <summary>
    ''' Raises the Paint event for the control.
    ''' </summary>
    ''' <param name="e">A System.Windows.Forms.PaintEventArgs that contains the event data.</param>
    Protected Overrides Sub OnPaint(ByVal e As System.Windows.Forms.PaintEventArgs)
        Dim txtWidth As Integer = Me.CreateGraphics.MeasureString(Text, Me.Font, New PointF(0, 0), New StringFormat(StringFormatFlags.NoWrap)).Width
        If Selected Then
            Dim brsh As New SolidBrush(Color.FromArgb(100, 124, 159, 181))
            e.Graphics.FillRectangle(brsh, Me.ClientRectangle)
        ElseIf Highlighted Then
            Dim brsh As New SolidBrush(Color.FromArgb(50, 124, 159, 181))
            e.Graphics.FillRectangle(brsh, Me.ClientRectangle)
        Else
            e.Graphics.FillRectangle(New SolidBrush(Me.Parent.BackColor), Me.ClientRectangle)
        End If
        e.Graphics.DrawImage(Image, New PointF((Me.Width / 2) - (32 / 2), 0))
        e.Graphics.DrawString(Text, Me.Font, New SolidBrush(Me.ForeColor), (Me.Width / 2) - (txtWidth / 2), 33)
    End Sub
    ''' <summary>
    ''' Raises the ParentChanged event for the control.
    ''' </summary>
    ''' <param name="e">A System.EventArgs that contains the event data.</param>
    Protected Overrides Sub OnParentChanged(ByVal e As System.EventArgs)
        MyBase.OnParentChanged(e)
        If TypeOf (Me.Parent) Is FireFoxPane Then Me.Width = Parent.Width - 5 : Exit Sub
        Dim txtWidth As Integer = Convert.ToInt32(Me.CreateGraphics.MeasureString(Text, Me.Font, New PointF(0, 0), New StringFormat(StringFormatFlags.NoWrap)).Width)
        If TypeOf (Me.Parent) Is FireFoxBar Then
            If txtWidth < 32 Then txtWidth = 32
            Me.Width = txtWidth + 5
        End If
    End Sub

    ''' <summary>
    ''' Handles the MouseEnter event for the control (for hot-tracking).
    ''' </summary>
    ''' <param name="sender">The sender of the event (usually an instance of itself).</param>
    ''' <param name="e">A System.EventArgs that contains the event data.</param>
    Private Sub FFItem_MouseEnter(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.MouseEnter
        If Not Selected Then Highlighted = True : Me.Invalidate()
    End Sub
    ''' <summary>
    ''' Handles the MouseLeave event for the control (for hot-tracking).
    ''' </summary>
    ''' <param name="sender">The sender of the event (usually an instance of itself).</param>
    ''' <param name="e">A System.EventArgs that contains the event data.</param>
    Private Sub FFItem_MouseLeave(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.MouseLeave
        If Not Selected Then Highlighted = False : Me.Invalidate()
    End Sub

    ''' <summary>
    ''' Generates a System.Windows.Forms.Control.Click event for the FFItem.
    ''' </summary>
    Public Sub PerformClick()
        MyBase.OnClick(New System.EventArgs)
    End Sub
End Class