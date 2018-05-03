<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ObjSpy
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
        Me.components = New System.ComponentModel.Container
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(ObjSpy))
        Me.SpyObjectsTreeView = New System.Windows.Forms.TreeView
        Me.MouseLocationTextBox = New System.Windows.Forms.TextBox
        Me.ClassNameTextBox = New System.Windows.Forms.TextBox
        Me.MouseTimer = New System.Windows.Forms.Timer(Me.components)
        Me.MouseFollowButton = New System.Windows.Forms.CheckBox
        Me.ClickScannerButton = New System.Windows.Forms.Button
        Me.SpyDataViewer = New System.Windows.Forms.DataGridView
        Me.DataName = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Value = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.CopyDescriptionButton = New System.Windows.Forms.Button
        Me.SpyClickTimer = New System.Windows.Forms.Timer(Me.components)
        Me.SpyToolTip = New System.Windows.Forms.ToolTip(Me.components)
        Me.ScanWindowButton = New System.Windows.Forms.Button
        Me.NextClickTimer = New System.Windows.Forms.Timer(Me.components)
        Me.HighlightCheckBox = New System.Windows.Forms.CheckBox
        Me.BuildWebTimer = New System.Windows.Forms.Timer(Me.components)
        Me.PerformScanAndUpdateAfterEventTimer = New System.Windows.Forms.Timer(Me.components)
        Me.TreeViewContextMenuStrip = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.HighlightToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.UnhighlightToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator
        Me.DumpWindowToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.TextToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.XmlToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        CType(Me.SpyDataViewer, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TreeViewContextMenuStrip.SuspendLayout()
        Me.SuspendLayout()
        '
        'SpyObjectsTreeView
        '
        Me.SpyObjectsTreeView.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.SpyObjectsTreeView.Location = New System.Drawing.Point(3, 2)
        Me.SpyObjectsTreeView.Name = "SpyObjectsTreeView"
        Me.SpyObjectsTreeView.Size = New System.Drawing.Size(319, 193)
        Me.SpyObjectsTreeView.TabIndex = 0
        '
        'MouseLocationTextBox
        '
        Me.MouseLocationTextBox.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.MouseLocationTextBox.Location = New System.Drawing.Point(3, 201)
        Me.MouseLocationTextBox.Name = "MouseLocationTextBox"
        Me.MouseLocationTextBox.Size = New System.Drawing.Size(76, 20)
        Me.MouseLocationTextBox.TabIndex = 2
        '
        'ClassNameTextBox
        '
        Me.ClassNameTextBox.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ClassNameTextBox.Location = New System.Drawing.Point(85, 201)
        Me.ClassNameTextBox.Name = "ClassNameTextBox"
        Me.ClassNameTextBox.Size = New System.Drawing.Size(237, 20)
        Me.ClassNameTextBox.TabIndex = 3
        '
        'MouseTimer
        '
        Me.MouseTimer.Enabled = True
        Me.MouseTimer.Interval = 400
        '
        'MouseFollowButton
        '
        Me.MouseFollowButton.Location = New System.Drawing.Point(261, 227)
        Me.MouseFollowButton.Name = "MouseFollowButton"
        Me.MouseFollowButton.Size = New System.Drawing.Size(62, 22)
        Me.MouseFollowButton.TabIndex = 4
        Me.MouseFollowButton.Text = "Follow"
        Me.SpyToolTip.SetToolTip(Me.MouseFollowButton, "Allows a user a quick preview of various objects by following the mouse.")
        Me.MouseFollowButton.UseVisualStyleBackColor = True
        '
        'ClickScannerButton
        '
        Me.ClickScannerButton.Location = New System.Drawing.Point(3, 225)
        Me.ClickScannerButton.Name = "ClickScannerButton"
        Me.ClickScannerButton.Size = New System.Drawing.Size(174, 22)
        Me.ClickScannerButton.TabIndex = 5
        Me.ClickScannerButton.Text = "Scan object on click"
        Me.SpyToolTip.SetToolTip(Me.ClickScannerButton, "To spy layered items, hold down ctrl and it will ignore that click")
        Me.ClickScannerButton.UseVisualStyleBackColor = True
        '
        'SpyDataViewer
        '
        Me.SpyDataViewer.AllowUserToAddRows = False
        Me.SpyDataViewer.AllowUserToDeleteRows = False
        Me.SpyDataViewer.AllowUserToResizeRows = False
        Me.SpyDataViewer.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.SpyDataViewer.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCellsExceptHeaders
        Me.SpyDataViewer.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.SpyDataViewer.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.DataName, Me.Value})
        Me.SpyDataViewer.Location = New System.Drawing.Point(3, 254)
        Me.SpyDataViewer.MultiSelect = False
        Me.SpyDataViewer.Name = "SpyDataViewer"
        Me.SpyDataViewer.ReadOnly = True
        Me.SpyDataViewer.RowHeadersWidth = 15
        Me.SpyDataViewer.RowTemplate.Height = 24
        Me.SpyDataViewer.Size = New System.Drawing.Size(318, 260)
        Me.SpyDataViewer.TabIndex = 6
        '
        'DataName
        '
        Me.DataName.HeaderText = "Data Name"
        Me.DataName.MinimumWidth = 60
        Me.DataName.Name = "DataName"
        Me.DataName.ReadOnly = True
        '
        'Value
        '
        Me.Value.HeaderText = "Data Value"
        Me.Value.Name = "Value"
        Me.Value.ReadOnly = True
        Me.Value.Width = 305
        '
        'CopyDescriptionButton
        '
        Me.CopyDescriptionButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.CopyDescriptionButton.Enabled = False
        Me.CopyDescriptionButton.Location = New System.Drawing.Point(3, 520)
        Me.CopyDescriptionButton.Name = "CopyDescriptionButton"
        Me.CopyDescriptionButton.Size = New System.Drawing.Size(158, 21)
        Me.CopyDescriptionButton.TabIndex = 7
        Me.CopyDescriptionButton.Text = "Copy description into viewer"
        Me.SpyToolTip.SetToolTip(Me.CopyDescriptionButton, "The description generated isn't the ""least descriptive"" but rather, it features a" & _
                "ll attributes of the object")
        Me.CopyDescriptionButton.UseVisualStyleBackColor = True
        '
        'SpyClickTimer
        '
        '
        'ScanWindowButton
        '
        Me.ScanWindowButton.Anchor = System.Windows.Forms.AnchorStyles.Bottom
        Me.ScanWindowButton.Location = New System.Drawing.Point(167, 520)
        Me.ScanWindowButton.Name = "ScanWindowButton"
        Me.ScanWindowButton.Size = New System.Drawing.Size(154, 21)
        Me.ScanWindowButton.TabIndex = 8
        Me.ScanWindowButton.Text = "Scan all objects into viewer"
        Me.SpyToolTip.SetToolTip(Me.ScanWindowButton, "Hold down alt when clicking to ensure that you get descriptions from the top wind" & _
                "ow down.")
        Me.ScanWindowButton.UseVisualStyleBackColor = True
        '
        'NextClickTimer
        '
        Me.NextClickTimer.Enabled = True
        Me.NextClickTimer.Interval = 200
        '
        'HighlightCheckBox
        '
        Me.HighlightCheckBox.AutoSize = True
        Me.HighlightCheckBox.Location = New System.Drawing.Point(183, 230)
        Me.HighlightCheckBox.Name = "HighlightCheckBox"
        Me.HighlightCheckBox.Size = New System.Drawing.Size(67, 17)
        Me.HighlightCheckBox.TabIndex = 9
        Me.HighlightCheckBox.Text = "Highlight"
        Me.HighlightCheckBox.UseVisualStyleBackColor = True
        '
        'BuildWebTimer
        '
        Me.BuildWebTimer.Interval = 40
        '
        'PerformScanAndUpdateAfterEventTimer
        '
        Me.PerformScanAndUpdateAfterEventTimer.Interval = 1
        '
        'TreeViewContextMenuStrip
        '
        Me.TreeViewContextMenuStrip.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.HighlightToolStripMenuItem, Me.UnhighlightToolStripMenuItem, Me.ToolStripSeparator1, Me.DumpWindowToolStripMenuItem})
        Me.TreeViewContextMenuStrip.Name = "TreeViewContextMenuStrip"
        Me.TreeViewContextMenuStrip.Size = New System.Drawing.Size(153, 98)
        '
        'HighlightToolStripMenuItem
        '
        Me.HighlightToolStripMenuItem.Name = "HighlightToolStripMenuItem"
        Me.HighlightToolStripMenuItem.Size = New System.Drawing.Size(152, 22)
        Me.HighlightToolStripMenuItem.Text = "Highlight"
        '
        'UnhighlightToolStripMenuItem
        '
        Me.UnhighlightToolStripMenuItem.Name = "UnhighlightToolStripMenuItem"
        Me.UnhighlightToolStripMenuItem.Size = New System.Drawing.Size(152, 22)
        Me.UnhighlightToolStripMenuItem.Text = "Unhighlight"
        '
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        Me.ToolStripSeparator1.Size = New System.Drawing.Size(149, 6)
        '
        'DumpWindowToolStripMenuItem
        '
        Me.DumpWindowToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.TextToolStripMenuItem, Me.XmlToolStripMenuItem})
        Me.DumpWindowToolStripMenuItem.Name = "DumpWindowToolStripMenuItem"
        Me.DumpWindowToolStripMenuItem.Size = New System.Drawing.Size(152, 22)
        Me.DumpWindowToolStripMenuItem.Text = "Dump Window"
        '
        'TextToolStripMenuItem
        '
        Me.TextToolStripMenuItem.Name = "TextToolStripMenuItem"
        Me.TextToolStripMenuItem.Size = New System.Drawing.Size(152, 22)
        Me.TextToolStripMenuItem.Text = "Text"
        '
        'XmlToolStripMenuItem
        '
        Me.XmlToolStripMenuItem.Name = "XmlToolStripMenuItem"
        Me.XmlToolStripMenuItem.Size = New System.Drawing.Size(152, 22)
        Me.XmlToolStripMenuItem.Text = "Xml"
        '
        'ObjSpy
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(323, 544)
        Me.Controls.Add(Me.HighlightCheckBox)
        Me.Controls.Add(Me.ScanWindowButton)
        Me.Controls.Add(Me.CopyDescriptionButton)
        Me.Controls.Add(Me.SpyDataViewer)
        Me.Controls.Add(Me.ClickScannerButton)
        Me.Controls.Add(Me.MouseFollowButton)
        Me.Controls.Add(Me.ClassNameTextBox)
        Me.Controls.Add(Me.MouseLocationTextBox)
        Me.Controls.Add(Me.SpyObjectsTreeView)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.Name = "ObjSpy"
        Me.Text = "Object Spy"
        CType(Me.SpyDataViewer, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TreeViewContextMenuStrip.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents SpyObjectsTreeView As System.Windows.Forms.TreeView
    Friend WithEvents MouseLocationTextBox As System.Windows.Forms.TextBox
    Friend WithEvents ClassNameTextBox As System.Windows.Forms.TextBox
    Friend WithEvents MouseTimer As System.Windows.Forms.Timer
    Friend WithEvents MouseFollowButton As System.Windows.Forms.CheckBox
    Friend WithEvents ClickScannerButton As System.Windows.Forms.Button
    Friend WithEvents SpyDataViewer As System.Windows.Forms.DataGridView
    Friend WithEvents CopyDescriptionButton As System.Windows.Forms.Button
    Friend WithEvents SpyClickTimer As System.Windows.Forms.Timer
    Friend WithEvents SpyToolTip As System.Windows.Forms.ToolTip
    Friend WithEvents NextClickTimer As System.Windows.Forms.Timer
    Friend WithEvents ScanWindowButton As System.Windows.Forms.Button
    Friend WithEvents HighlightCheckBox As System.Windows.Forms.CheckBox
    Friend WithEvents BuildWebTimer As System.Windows.Forms.Timer
    Friend WithEvents DataName As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Value As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents PerformScanAndUpdateAfterEventTimer As System.Windows.Forms.Timer
    Friend WithEvents TreeViewContextMenuStrip As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents HighlightToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents DumpWindowToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents UnhighlightToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents TextToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents XmlToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
End Class
