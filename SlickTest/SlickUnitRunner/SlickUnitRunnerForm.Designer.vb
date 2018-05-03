<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class SlickUnitRunnerForm
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(SlickUnitRunnerForm))
        Me.RunTestButton = New System.Windows.Forms.Button
        Me.TreeView = New System.Windows.Forms.TreeView
        Me.ContextMenuStrip1 = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.RunTestsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.OpenFileDialog1 = New System.Windows.Forms.OpenFileDialog
        Me.GroupBoxFilterBy = New System.Windows.Forms.GroupBox
        Me.UseExactTextCheckBox = New System.Windows.Forms.CheckBox
        Me.ExcludeFilterTextBox = New System.Windows.Forms.TextBox
        Me.IncludeFilterTextBox = New System.Windows.Forms.TextBox
        Me.LabelExclude = New System.Windows.Forms.Label
        Me.LabelInclude = New System.Windows.Forms.Label
        Me.StopTestsButton = New System.Windows.Forms.Button
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.TextBoxResults = New System.Windows.Forms.TextBox
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.ContextMenuStrip1.SuspendLayout()
        Me.GroupBoxFilterBy.SuspendLayout()
        Me.SuspendLayout()
        '
        'RunTestButton
        '
        Me.RunTestButton.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.RunTestButton.Location = New System.Drawing.Point(395, 561)
        Me.RunTestButton.Name = "RunTestButton"
        Me.RunTestButton.Size = New System.Drawing.Size(80, 59)
        Me.RunTestButton.TabIndex = 0
        Me.RunTestButton.Text = "Run Test(s)"
        Me.RunTestButton.UseVisualStyleBackColor = True
        '
        'TreeView
        '
        Me.TreeView.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TreeView.ContextMenuStrip = Me.ContextMenuStrip1
        Me.TreeView.Location = New System.Drawing.Point(4, 12)
        Me.TreeView.MinimumSize = New System.Drawing.Size(355, 350)
        Me.TreeView.Name = "TreeView"
        Me.TreeView.Size = New System.Drawing.Size(478, 350)
        Me.TreeView.TabIndex = 1
        '
        'ContextMenuStrip1
        '
        Me.ContextMenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.RunTestsToolStripMenuItem})
        Me.ContextMenuStrip1.Name = "ContextMenuStrip1"
        Me.ContextMenuStrip1.Size = New System.Drawing.Size(134, 26)
        '
        'RunTestsToolStripMenuItem
        '
        Me.RunTestsToolStripMenuItem.Name = "RunTestsToolStripMenuItem"
        Me.RunTestsToolStripMenuItem.Size = New System.Drawing.Size(133, 22)
        Me.RunTestsToolStripMenuItem.Text = "Run Test(s)"
        '
        'OpenFileDialog1
        '
        Me.OpenFileDialog1.Filter = "Dlls|*.dll|All files|*.*"
        Me.OpenFileDialog1.Multiselect = True
        Me.OpenFileDialog1.Title = "Slick Unit Runner -- Select DLLs."
        '
        'GroupBoxFilterBy
        '
        Me.GroupBoxFilterBy.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.GroupBoxFilterBy.Controls.Add(Me.UseExactTextCheckBox)
        Me.GroupBoxFilterBy.Controls.Add(Me.ExcludeFilterTextBox)
        Me.GroupBoxFilterBy.Controls.Add(Me.IncludeFilterTextBox)
        Me.GroupBoxFilterBy.Controls.Add(Me.LabelExclude)
        Me.GroupBoxFilterBy.Controls.Add(Me.LabelInclude)
        Me.GroupBoxFilterBy.Location = New System.Drawing.Point(6, 561)
        Me.GroupBoxFilterBy.Name = "GroupBoxFilterBy"
        Me.GroupBoxFilterBy.Size = New System.Drawing.Size(383, 118)
        Me.GroupBoxFilterBy.TabIndex = 2
        Me.GroupBoxFilterBy.TabStop = False
        Me.GroupBoxFilterBy.Text = "Filter By"
        '
        'UseExactTextCheckBox
        '
        Me.UseExactTextCheckBox.AutoSize = True
        Me.UseExactTextCheckBox.Location = New System.Drawing.Point(10, 71)
        Me.UseExactTextCheckBox.Name = "UseExactTextCheckBox"
        Me.UseExactTextCheckBox.Size = New System.Drawing.Size(99, 17)
        Me.UseExactTextCheckBox.TabIndex = 4
        Me.UseExactTextCheckBox.Text = "Use Exact Text"
        Me.UseExactTextCheckBox.UseVisualStyleBackColor = True
        '
        'ExcludeFilterTextBox
        '
        Me.ExcludeFilterTextBox.Location = New System.Drawing.Point(59, 45)
        Me.ExcludeFilterTextBox.Name = "ExcludeFilterTextBox"
        Me.ExcludeFilterTextBox.Size = New System.Drawing.Size(318, 20)
        Me.ExcludeFilterTextBox.TabIndex = 3
        Me.ToolTip1.SetToolTip(Me.ExcludeFilterTextBox, "Filters use wild cards and pipes (|) to allow for multiple searches. I.E. TestA o" & _
                "r TestB would be like this *TestA*|*TestB*")
        '
        'IncludeFilterTextBox
        '
        Me.IncludeFilterTextBox.Location = New System.Drawing.Point(59, 12)
        Me.IncludeFilterTextBox.Name = "IncludeFilterTextBox"
        Me.IncludeFilterTextBox.Size = New System.Drawing.Size(318, 20)
        Me.IncludeFilterTextBox.TabIndex = 2
        Me.ToolTip1.SetToolTip(Me.IncludeFilterTextBox, "Filters use wild cards and pipes (|) to allow for multiple searches. I.E. TestA o" & _
                "r TestB would be like this *TestA*|*TestB*")
        '
        'LabelExclude
        '
        Me.LabelExclude.AutoSize = True
        Me.LabelExclude.Location = New System.Drawing.Point(7, 51)
        Me.LabelExclude.Name = "LabelExclude"
        Me.LabelExclude.Size = New System.Drawing.Size(48, 13)
        Me.LabelExclude.TabIndex = 1
        Me.LabelExclude.Text = "Exclude:"
        '
        'LabelInclude
        '
        Me.LabelInclude.AutoSize = True
        Me.LabelInclude.Location = New System.Drawing.Point(7, 20)
        Me.LabelInclude.Name = "LabelInclude"
        Me.LabelInclude.Size = New System.Drawing.Size(45, 13)
        Me.LabelInclude.TabIndex = 0
        Me.LabelInclude.Text = "Include:"
        '
        'StopTestsButton
        '
        Me.StopTestsButton.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.StopTestsButton.Enabled = False
        Me.StopTestsButton.Location = New System.Drawing.Point(395, 626)
        Me.StopTestsButton.Name = "StopTestsButton"
        Me.StopTestsButton.Size = New System.Drawing.Size(80, 53)
        Me.StopTestsButton.TabIndex = 3
        Me.StopTestsButton.Text = "Stop Test(s)"
        Me.StopTestsButton.UseVisualStyleBackColor = True
        '
        'ToolTip1
        '
        Me.ToolTip1.AutoPopDelay = 15000
        Me.ToolTip1.InitialDelay = 500
        Me.ToolTip1.ReshowDelay = 100
        Me.ToolTip1.ToolTipIcon = System.Windows.Forms.ToolTipIcon.Info
        '
        'TextBoxResults
        '
        Me.TextBoxResults.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxResults.Location = New System.Drawing.Point(6, 368)
        Me.TextBoxResults.Multiline = True
        Me.TextBoxResults.Name = "TextBoxResults"
        Me.TextBoxResults.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.TextBoxResults.Size = New System.Drawing.Size(476, 187)
        Me.TextBoxResults.TabIndex = 4
        '
        'Timer1
        '
        Me.Timer1.Interval = 10
        '
        'SlickUnitRunnerForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(487, 691)
        Me.Controls.Add(Me.TextBoxResults)
        Me.Controls.Add(Me.TreeView)
        Me.Controls.Add(Me.StopTestsButton)
        Me.Controls.Add(Me.GroupBoxFilterBy)
        Me.Controls.Add(Me.RunTestButton)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MinimumSize = New System.Drawing.Size(390, 430)
        Me.Name = "SlickUnitRunnerForm"
        Me.Text = "Slick Unit Runner"
        Me.ContextMenuStrip1.ResumeLayout(False)
        Me.GroupBoxFilterBy.ResumeLayout(False)
        Me.GroupBoxFilterBy.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents RunTestButton As System.Windows.Forms.Button
    Friend WithEvents TreeView As System.Windows.Forms.TreeView
    Friend WithEvents OpenFileDialog1 As System.Windows.Forms.OpenFileDialog
    Friend WithEvents ContextMenuStrip1 As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents RunTestsToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents GroupBoxFilterBy As System.Windows.Forms.GroupBox
    Friend WithEvents LabelExclude As System.Windows.Forms.Label
    Friend WithEvents LabelInclude As System.Windows.Forms.Label
    Friend WithEvents ExcludeFilterTextBox As System.Windows.Forms.TextBox
    Friend WithEvents IncludeFilterTextBox As System.Windows.Forms.TextBox
    Friend WithEvents StopTestsButton As System.Windows.Forms.Button
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    Friend WithEvents UseExactTextCheckBox As System.Windows.Forms.CheckBox
    Friend WithEvents TextBoxResults As System.Windows.Forms.TextBox
    Friend WithEvents Timer1 As System.Windows.Forms.Timer

End Class
