<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class GotoLine
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
        Me.GoToLineButton = New System.Windows.Forms.Button
        Me.LineNumberTextBox = New System.Windows.Forms.MaskedTextBox
        Me.LineNumberLabel = New System.Windows.Forms.Label
        Me.SuspendLayout()
        '
        'GoToLineButton
        '
        Me.GoToLineButton.Location = New System.Drawing.Point(1, 39)
        Me.GoToLineButton.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.GoToLineButton.Name = "GoToLineButton"
        Me.GoToLineButton.Size = New System.Drawing.Size(63, 28)
        Me.GoToLineButton.TabIndex = 1
        Me.GoToLineButton.Text = "Go"
        Me.GoToLineButton.UseVisualStyleBackColor = True
        '
        'LineNumberTextBox
        '
        Me.LineNumberTextBox.BeepOnError = True
        Me.LineNumberTextBox.Location = New System.Drawing.Point(72, 43)
        Me.LineNumberTextBox.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.LineNumberTextBox.Mask = "#########"
        Me.LineNumberTextBox.Name = "LineNumberTextBox"
        Me.LineNumberTextBox.Size = New System.Drawing.Size(77, 22)
        Me.LineNumberTextBox.TabIndex = 0
        '
        'LineNumberLabel
        '
        Me.LineNumberLabel.AutoSize = True
        Me.LineNumberLabel.Location = New System.Drawing.Point(-3, 11)
        Me.LineNumberLabel.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.LineNumberLabel.Name = "LineNumberLabel"
        Me.LineNumberLabel.Size = New System.Drawing.Size(133, 17)
        Me.LineNumberLabel.TabIndex = 2
        Me.LineNumberLabel.Text = "Go To Line Number"
        '
        'GotoLine
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(165, 75)
        Me.Controls.Add(Me.LineNumberLabel)
        Me.Controls.Add(Me.LineNumberTextBox)
        Me.Controls.Add(Me.GoToLineButton)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "GotoLine"
        Me.ShowIcon = False
        Me.ShowInTaskbar = False
        Me.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Go To Line"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents GoToLineButton As System.Windows.Forms.Button
    Friend WithEvents LineNumberTextBox As System.Windows.Forms.MaskedTextBox
    Friend WithEvents LineNumberLabel As System.Windows.Forms.Label
End Class
