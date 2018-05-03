<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class VerifyDescription
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(VerifyDescription))
        Me.DescriptionText = New TextEditor.TextEditorBox
        Me.StatusStrip1 = New System.Windows.Forms.StatusStrip
        Me.ToolStripStatusLabel1 = New System.Windows.Forms.ToolStripStatusLabel
        Me.EnterAsExecuteTimer = New System.Windows.Forms.Timer(Me.components)
        Me.ToolStripSplitButton1 = New System.Windows.Forms.ToolStripSplitButton
        Me.SeeExactCodeToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.RunToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.StatusStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'DescriptionText
        '
        Me.DescriptionText.IsReadOnly = False
        Me.DescriptionText.Location = New System.Drawing.Point(-1, 0)
        Me.DescriptionText.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.DescriptionText.Name = "DescriptionText"
        Me.DescriptionText.Size = New System.Drawing.Size(671, 49)
        Me.DescriptionText.TabIndex = 0
        Me.DescriptionText.Text = "TextEditorBox1"
        '
        'StatusStrip1
        '
        Me.StatusStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripSplitButton1, Me.ToolStripStatusLabel1})
        Me.StatusStrip1.Location = New System.Drawing.Point(0, 26)
        Me.StatusStrip1.Name = "StatusStrip1"
        Me.StatusStrip1.Size = New System.Drawing.Size(653, 22)
        Me.StatusStrip1.TabIndex = 1
        Me.StatusStrip1.Text = "StatusStrip1"
        '
        'ToolStripStatusLabel1
        '
        Me.ToolStripStatusLabel1.AutoToolTip = True
        Me.ToolStripStatusLabel1.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Left
        Me.ToolStripStatusLabel1.BorderStyle = System.Windows.Forms.Border3DStyle.RaisedOuter
        Me.ToolStripStatusLabel1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
        Me.ToolStripStatusLabel1.Name = "ToolStripStatusLabel1"
        Me.ToolStripStatusLabel1.Size = New System.Drawing.Size(30, 17)
        Me.ToolStripStatusLabel1.Text = "test"
        '
        'EnterAsExecuteTimer
        '
        Me.EnterAsExecuteTimer.Interval = 40
        '
        'ToolStripSplitButton1
        '
        Me.ToolStripSplitButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
        Me.ToolStripSplitButton1.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.SeeExactCodeToolStripMenuItem, Me.RunToolStripMenuItem})
        Me.ToolStripSplitButton1.Image = CType(resources.GetObject("ToolStripSplitButton1.Image"), System.Drawing.Image)
        Me.ToolStripSplitButton1.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripSplitButton1.Name = "ToolStripSplitButton1"
        Me.ToolStripSplitButton1.Size = New System.Drawing.Size(90, 20)
        Me.ToolStripSplitButton1.Text = "Options (Run)"
        '
        'SeeExactCodeToolStripMenuItem
        '
        Me.SeeExactCodeToolStripMenuItem.Name = "SeeExactCodeToolStripMenuItem"
        Me.SeeExactCodeToolStripMenuItem.Size = New System.Drawing.Size(219, 22)
        Me.SeeExactCodeToolStripMenuItem.Text = "See Last Compiled Exact Code"
        '
        'RunToolStripMenuItem
        '
        Me.RunToolStripMenuItem.Name = "RunToolStripMenuItem"
        Me.RunToolStripMenuItem.Size = New System.Drawing.Size(219, 22)
        Me.RunToolStripMenuItem.Text = "Run"
        '
        'VerifyDescription
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(653, 48)
        Me.Controls.Add(Me.StatusStrip1)
        Me.Controls.Add(Me.DescriptionText)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimumSize = New System.Drawing.Size(661, 75)
        Me.Name = "VerifyDescription"
        Me.Text = "Test Description"
        Me.StatusStrip1.ResumeLayout(False)
        Me.StatusStrip1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents DescriptionText As TextEditor.TextEditorBox
    Friend WithEvents StatusStrip1 As System.Windows.Forms.StatusStrip
    Friend WithEvents ToolStripStatusLabel1 As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents EnterAsExecuteTimer As System.Windows.Forms.Timer
    Friend WithEvents ToolStripSplitButton1 As System.Windows.Forms.ToolStripSplitButton
    Friend WithEvents SeeExactCodeToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents RunToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
End Class
