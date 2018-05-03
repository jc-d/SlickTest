<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmFind
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmFind))
        Me.Label1 = New System.Windows.Forms.Label
        Me.txtSearchTerm = New System.Windows.Forms.TextBox
        Me.chkMatchCase = New System.Windows.Forms.CheckBox
        Me.btnFind = New System.Windows.Forms.Button
        Me.ChkStringReverse = New System.Windows.Forms.CheckBox
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(13, 12)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(71, 13)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Search Term:"
        '
        'txtSearchTerm
        '
        Me.txtSearchTerm.Location = New System.Drawing.Point(16, 29)
        Me.txtSearchTerm.Name = "txtSearchTerm"
        Me.txtSearchTerm.Size = New System.Drawing.Size(252, 20)
        Me.txtSearchTerm.TabIndex = 1
        '
        'chkMatchCase
        '
        Me.chkMatchCase.AutoSize = True
        Me.chkMatchCase.Location = New System.Drawing.Point(16, 62)
        Me.chkMatchCase.Name = "chkMatchCase"
        Me.chkMatchCase.Size = New System.Drawing.Size(83, 17)
        Me.chkMatchCase.TabIndex = 4
        Me.chkMatchCase.Text = "Match Case"
        Me.chkMatchCase.UseVisualStyleBackColor = True
        '
        'btnFind
        '
        Me.btnFind.Location = New System.Drawing.Point(275, 29)
        Me.btnFind.Name = "btnFind"
        Me.btnFind.Size = New System.Drawing.Size(75, 21)
        Me.btnFind.TabIndex = 2
        Me.btnFind.Text = "&Find"
        Me.btnFind.UseVisualStyleBackColor = True
        '
        'ChkStringReverse
        '
        Me.ChkStringReverse.AutoSize = True
        Me.ChkStringReverse.Location = New System.Drawing.Point(105, 62)
        Me.ChkStringReverse.Name = "ChkStringReverse"
        Me.ChkStringReverse.Size = New System.Drawing.Size(77, 17)
        Me.ChkStringReverse.TabIndex = 5
        Me.ChkStringReverse.Text = "Search Up"
        Me.ChkStringReverse.UseVisualStyleBackColor = True
        '
        'frmFind
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(361, 102)
        Me.Controls.Add(Me.ChkStringReverse)
        Me.Controls.Add(Me.btnFind)
        Me.Controls.Add(Me.chkMatchCase)
        Me.Controls.Add(Me.txtSearchTerm)
        Me.Controls.Add(Me.Label1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmFind"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Find Text"
        Me.TopMost = True
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtSearchTerm As System.Windows.Forms.TextBox
    Friend WithEvents chkMatchCase As System.Windows.Forms.CheckBox
    Friend WithEvents btnFind As System.Windows.Forms.Button
    Friend WithEvents ChkStringReverse As System.Windows.Forms.CheckBox
End Class
