<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ScreenCapture
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(ScreenCapture))
        Me.TopAppButton = New System.Windows.Forms.Button
        Me.GetScreenByMouseButton = New System.Windows.Forms.Button
        Me.CodeTextBox = New System.Windows.Forms.TextBox
        Me.HelpTextBox = New System.Windows.Forms.TextBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.SuspendLayout()
        '
        'TopAppButton
        '
        Me.TopAppButton.Location = New System.Drawing.Point(10, 114)
        Me.TopAppButton.Name = "TopAppButton"
        Me.TopAppButton.Size = New System.Drawing.Size(101, 29)
        Me.TopAppButton.TabIndex = 0
        Me.TopAppButton.Text = "Top App"
        Me.TopAppButton.UseVisualStyleBackColor = True
        '
        'GetScreenByMouseButton
        '
        Me.GetScreenByMouseButton.Location = New System.Drawing.Point(117, 114)
        Me.GetScreenByMouseButton.Name = "GetScreenByMouseButton"
        Me.GetScreenByMouseButton.Size = New System.Drawing.Size(114, 29)
        Me.GetScreenByMouseButton.TabIndex = 1
        Me.GetScreenByMouseButton.Text = "Mouse Capture"
        Me.GetScreenByMouseButton.UseVisualStyleBackColor = True
        '
        'CodeTextBox
        '
        Me.CodeTextBox.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.CodeTextBox.Location = New System.Drawing.Point(10, 162)
        Me.CodeTextBox.Multiline = True
        Me.CodeTextBox.Name = "CodeTextBox"
        Me.CodeTextBox.Size = New System.Drawing.Size(222, 99)
        Me.CodeTextBox.TabIndex = 2
        '
        'HelpTextBox
        '
        Me.HelpTextBox.Enabled = False
        Me.HelpTextBox.Location = New System.Drawing.Point(10, 3)
        Me.HelpTextBox.MaxLength = 1000
        Me.HelpTextBox.Multiline = True
        Me.HelpTextBox.Name = "HelpTextBox"
        Me.HelpTextBox.ReadOnly = True
        Me.HelpTextBox.Size = New System.Drawing.Size(222, 105)
        Me.HelpTextBox.TabIndex = 3
        Me.HelpTextBox.Text = resources.GetString("HelpTextBox.Text")
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(7, 146)
        Me.Label1.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(35, 13)
        Me.Label1.TabIndex = 4
        Me.Label1.Text = "Code:"
        '
        'ScreenCapture
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(241, 271)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.HelpTextBox)
        Me.Controls.Add(Me.CodeTextBox)
        Me.Controls.Add(Me.GetScreenByMouseButton)
        Me.Controls.Add(Me.TopAppButton)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "ScreenCapture"
        Me.Text = "Screen Capture Assistant"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents TopAppButton As System.Windows.Forms.Button
    Friend WithEvents GetScreenByMouseButton As System.Windows.Forms.Button
    Friend WithEvents CodeTextBox As System.Windows.Forms.TextBox
    Friend WithEvents HelpTextBox As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
End Class
