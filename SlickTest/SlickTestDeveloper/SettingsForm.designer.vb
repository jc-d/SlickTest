<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class OptionsForm
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing AndAlso components IsNot Nothing Then
            components.Dispose()
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.cmdApply = New System.Windows.Forms.Button
        Me.cmdCancel = New System.Windows.Forms.Button
        Me.tlp = New System.Windows.Forms.TableLayoutPanel
        Me.dlgColor = New System.Windows.Forms.ColorDialog
        Me.dlgFont = New System.Windows.Forms.FontDialog
        Me.chkSaveOnExit = New System.Windows.Forms.CheckBox
        Me.hp1 = New System.Windows.Forms.HelpProvider
        Me.tvCategories = New System.Windows.Forms.TreeView
        Me.cmdOK = New System.Windows.Forms.Button
        Me.tcMain = New System.Windows.Forms.TabControl
        Me.Label1 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.Label3 = New System.Windows.Forms.Label
        Me.Label4 = New System.Windows.Forms.Label
        Me.Label5 = New System.Windows.Forms.Label
        Me.Label6 = New System.Windows.Forms.Label
        Me.Label7 = New System.Windows.Forms.Label
        Me.Label8 = New System.Windows.Forms.Label
        Me.Label9 = New System.Windows.Forms.Label
        Me.Label10 = New System.Windows.Forms.Label
        Me.Label11 = New System.Windows.Forms.Label
        Me.Label12 = New System.Windows.Forms.Label
        Me.pnlFFBar = New System.Windows.Forms.Panel
        Me.pnlFFPane = New System.Windows.Forms.Panel
        Me.FireFoxPane1 = New SlickTestDeveloper.FireFoxPane
        Me.FireFoxBar1 = New SlickTestDeveloper.FireFoxBar
        Me.pnlFFBar.SuspendLayout()
        Me.pnlFFPane.SuspendLayout()
        Me.SuspendLayout()
        '
        'cmdApply
        '
        Me.hp1.SetHelpString(Me.cmdApply, "Applies the changes without closing the window.")
        Me.cmdApply.Location = New System.Drawing.Point(495, 283)
        Me.cmdApply.Name = "cmdApply"
        Me.hp1.SetShowHelp(Me.cmdApply, True)
        Me.cmdApply.Size = New System.Drawing.Size(75, 23)
        Me.cmdApply.TabIndex = 1
        Me.cmdApply.Text = "Apply"
        Me.cmdApply.UseVisualStyleBackColor = True
        Me.cmdApply.Visible = False
        '
        'cmdCancel
        '
        Me.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.hp1.SetHelpString(Me.cmdCancel, "Closes the dialog box without applying the changes.")
        Me.cmdCancel.Location = New System.Drawing.Point(414, 283)
        Me.cmdCancel.Name = "cmdCancel"
        Me.hp1.SetShowHelp(Me.cmdCancel, True)
        Me.cmdCancel.Size = New System.Drawing.Size(75, 23)
        Me.cmdCancel.TabIndex = 2
        Me.cmdCancel.Text = "Cancel"
        Me.cmdCancel.UseVisualStyleBackColor = True
        '
        'tlp
        '
        Me.tlp.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.tlp.AutoScroll = True
        Me.tlp.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.tlp.ColumnCount = 2
        Me.tlp.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle)
        Me.tlp.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle)
        Me.hp1.SetHelpString(Me.tlp, "This pane contains the setting you are able to change from the selected category." & _
                "")
        Me.tlp.Location = New System.Drawing.Point(180, 12)
        Me.tlp.Name = "tlp"
        Me.tlp.Padding = New System.Windows.Forms.Padding(0, 0, 18, 0)
        Me.tlp.RowCount = 1
        Me.tlp.RowStyles.Add(New System.Windows.Forms.RowStyle)
        Me.tlp.RowStyles.Add(New System.Windows.Forms.RowStyle)
        Me.tlp.RowStyles.Add(New System.Windows.Forms.RowStyle)
        Me.hp1.SetShowHelp(Me.tlp, True)
        Me.tlp.Size = New System.Drawing.Size(390, 265)
        Me.tlp.TabIndex = 4
        '
        'chkSaveOnExit
        '
        Me.chkSaveOnExit.AutoSize = True
        Me.hp1.SetHelpString(Me.chkSaveOnExit, "Determines if the application will save your settings when you close the applicat" & _
                "ion.")
        Me.chkSaveOnExit.Location = New System.Drawing.Point(12, 287)
        Me.chkSaveOnExit.Name = "chkSaveOnExit"
        Me.hp1.SetShowHelp(Me.chkSaveOnExit, True)
        Me.chkSaveOnExit.Size = New System.Drawing.Size(124, 17)
        Me.chkSaveOnExit.TabIndex = 5
        Me.chkSaveOnExit.Text = "Save settings on exit"
        Me.chkSaveOnExit.UseVisualStyleBackColor = True
        '
        'tvCategories
        '
        Me.tvCategories.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.hp1.SetHelpString(Me.tvCategories, "Contains the categories of settings you may change.  Select a category to see the" & _
                " settings in the panel on the right.")
        Me.tvCategories.Location = New System.Drawing.Point(12, 12)
        Me.tvCategories.Name = "tvCategories"
        Me.tvCategories.PathSeparator = "."
        Me.hp1.SetShowHelp(Me.tvCategories, True)
        Me.tvCategories.Size = New System.Drawing.Size(162, 265)
        Me.tvCategories.TabIndex = 0
        '
        'cmdOK
        '
        Me.hp1.SetHelpString(Me.cmdOK, "Applies the settings and closes the dialog box.")
        Me.cmdOK.Location = New System.Drawing.Point(333, 283)
        Me.cmdOK.Name = "cmdOK"
        Me.hp1.SetShowHelp(Me.cmdOK, True)
        Me.cmdOK.Size = New System.Drawing.Size(75, 23)
        Me.cmdOK.TabIndex = 3
        Me.cmdOK.Text = "OK"
        Me.cmdOK.UseVisualStyleBackColor = True
        '
        'tcMain
        '
        Me.tcMain.Location = New System.Drawing.Point(12, 12)
        Me.tcMain.Name = "tcMain"
        Me.tcMain.SelectedIndex = 0
        Me.tcMain.Size = New System.Drawing.Size(558, 269)
        Me.tcMain.TabIndex = 6
        '
        'Label1
        '
        Me.Label1.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(80, 11)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(39, 13)
        Me.Label1.TabIndex = 2
        Me.Label1.Text = "Label1"
        '
        'Label2
        '
        Me.Label2.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(80, 46)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(39, 13)
        Me.Label2.TabIndex = 3
        Me.Label2.Text = "Label2"
        '
        'Label3
        '
        Me.Label3.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(80, 81)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(39, 13)
        Me.Label3.TabIndex = 4
        Me.Label3.Text = "Label3"
        '
        'Label4
        '
        Me.Label4.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(80, 116)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(39, 13)
        Me.Label4.TabIndex = 5
        Me.Label4.Text = "Label4"
        '
        'Label5
        '
        Me.Label5.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(80, 151)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(39, 13)
        Me.Label5.TabIndex = 6
        Me.Label5.Text = "Label5"
        '
        'Label6
        '
        Me.Label6.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(80, 186)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(39, 13)
        Me.Label6.TabIndex = 7
        Me.Label6.Text = "Label6"
        '
        'Label7
        '
        Me.Label7.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(80, 221)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(39, 13)
        Me.Label7.TabIndex = 8
        Me.Label7.Text = "Label7"
        '
        'Label8
        '
        Me.Label8.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(80, 256)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(39, 13)
        Me.Label8.TabIndex = 9
        Me.Label8.Text = "Label8"
        '
        'Label9
        '
        Me.Label9.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(80, 291)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(39, 13)
        Me.Label9.TabIndex = 10
        Me.Label9.Text = "Label9"
        '
        'Label10
        '
        Me.Label10.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(77, 326)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(45, 13)
        Me.Label10.TabIndex = 11
        Me.Label10.Text = "Label10"
        '
        'Label11
        '
        Me.Label11.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.Label11.AutoSize = True
        Me.Label11.Location = New System.Drawing.Point(77, 361)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(45, 13)
        Me.Label11.TabIndex = 12
        Me.Label11.Text = "Label11"
        '
        'Label12
        '
        Me.Label12.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.Label12.AutoSize = True
        Me.Label12.Location = New System.Drawing.Point(77, 396)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(45, 13)
        Me.Label12.TabIndex = 13
        Me.Label12.Text = "Label12"
        '
        'pnlFFBar
        '
        Me.pnlFFBar.BackColor = System.Drawing.Color.FromArgb(CType(CType(124, Byte), Integer), CType(CType(159, Byte), Integer), CType(CType(181, Byte), Integer))
        Me.pnlFFBar.Controls.Add(Me.FireFoxBar1)
        Me.pnlFFBar.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlFFBar.Location = New System.Drawing.Point(0, 0)
        Me.pnlFFBar.Name = "pnlFFBar"
        Me.pnlFFBar.Size = New System.Drawing.Size(582, 58)
        Me.pnlFFBar.TabIndex = 9
        '
        'pnlFFPane
        '
        Me.pnlFFPane.BackColor = System.Drawing.Color.FromArgb(CType(CType(124, Byte), Integer), CType(CType(159, Byte), Integer), CType(CType(181, Byte), Integer))
        Me.pnlFFPane.Controls.Add(Me.FireFoxPane1)
        Me.pnlFFPane.Location = New System.Drawing.Point(12, 12)
        Me.pnlFFPane.Name = "pnlFFPane"
        Me.pnlFFPane.Size = New System.Drawing.Size(164, 271)
        Me.pnlFFPane.TabIndex = 10
        '
        'FireFoxPane1
        '
        Me.FireFoxPane1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.FireFoxPane1.AutoScroll = True
        Me.FireFoxPane1.BackColor = System.Drawing.Color.White
        Me.FireFoxPane1.ColumnCount = 1
        Me.FireFoxPane1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.FireFoxPane1.Location = New System.Drawing.Point(1, 1)
        Me.FireFoxPane1.Name = "FireFoxPane1"
        Me.FireFoxPane1.RowCount = 3
        Me.FireFoxPane1.RowStyles.Add(New System.Windows.Forms.RowStyle)
        Me.FireFoxPane1.RowStyles.Add(New System.Windows.Forms.RowStyle)
        Me.FireFoxPane1.RowStyles.Add(New System.Windows.Forms.RowStyle)
        Me.FireFoxPane1.Size = New System.Drawing.Size(162, 269)
        Me.FireFoxPane1.TabIndex = 7
        '
        'FireFoxBar1
        '
        Me.FireFoxBar1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.FireFoxBar1.BackColor = System.Drawing.Color.White
        Me.FireFoxBar1.ColumnCount = 1
        Me.FireFoxBar1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle)
        Me.FireFoxBar1.GrowStyle = System.Windows.Forms.TableLayoutPanelGrowStyle.AddColumns
        Me.FireFoxBar1.Location = New System.Drawing.Point(1, 1)
        Me.FireFoxBar1.Name = "FireFoxBar1"
        Me.FireFoxBar1.RowCount = 1
        Me.FireFoxBar1.RowStyles.Add(New System.Windows.Forms.RowStyle)
        Me.FireFoxBar1.Size = New System.Drawing.Size(580, 56)
        Me.FireFoxBar1.TabIndex = 8
        '
        'OptionsForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.ClientSize = New System.Drawing.Size(582, 318)
        Me.Controls.Add(Me.pnlFFPane)
        Me.Controls.Add(Me.pnlFFBar)
        Me.Controls.Add(Me.chkSaveOnExit)
        Me.Controls.Add(Me.tlp)
        Me.Controls.Add(Me.cmdOK)
        Me.Controls.Add(Me.cmdCancel)
        Me.Controls.Add(Me.cmdApply)
        Me.Controls.Add(Me.tvCategories)
        Me.Controls.Add(Me.tcMain)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.HelpButton = True
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "OptionsForm"
        Me.ShowIcon = False
        Me.Text = "Options"
        Me.pnlFFBar.ResumeLayout(False)
        Me.pnlFFPane.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents tvCategories As System.Windows.Forms.TreeView
    Friend WithEvents cmdApply As System.Windows.Forms.Button
    Friend WithEvents cmdCancel As System.Windows.Forms.Button
    Friend WithEvents cmdOK As System.Windows.Forms.Button
    Friend WithEvents tlp As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents dlgColor As System.Windows.Forms.ColorDialog
    Friend WithEvents dlgFont As System.Windows.Forms.FontDialog
    Friend WithEvents chkSaveOnExit As System.Windows.Forms.CheckBox
    Friend WithEvents hp1 As System.Windows.Forms.HelpProvider
    Friend WithEvents tcMain As System.Windows.Forms.TabControl
    Friend WithEvents FireFoxPane1 As FireFoxPane
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents FireFoxBar1 As FireFoxBar
    Friend WithEvents pnlFFBar As System.Windows.Forms.Panel
    Friend WithEvents pnlFFPane As System.Windows.Forms.Panel

End Class
