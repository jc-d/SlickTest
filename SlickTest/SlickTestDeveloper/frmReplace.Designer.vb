<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmReplace
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmReplace))
        Me.btnFind = New System.Windows.Forms.Button
        Me.chkMatchCase = New System.Windows.Forms.CheckBox
        Me.txtSearchTerm = New System.Windows.Forms.TextBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.txtReplacementText = New System.Windows.Forms.TextBox
        Me.Label2 = New System.Windows.Forms.Label
        Me.btnReplace = New System.Windows.Forms.Button
        Me.btnReplaceAll = New System.Windows.Forms.Button
        Me.ChkStringReverse = New System.Windows.Forms.CheckBox
        Me.SuspendLayout()
        '
        'btnFind
        '
        Me.btnFind.Location = New System.Drawing.Point(8, 119)
        Me.btnFind.Name = "btnFind"
        Me.btnFind.Size = New System.Drawing.Size(75, 21)
        Me.btnFind.TabIndex = 4
        Me.btnFind.Text = "&Find"
        Me.btnFind.UseVisualStyleBackColor = True
        '
        'chkMatchCase
        '
        Me.chkMatchCase.AutoSize = True
        Me.chkMatchCase.Location = New System.Drawing.Point(251, 135)
        Me.chkMatchCase.Name = "chkMatchCase"
        Me.chkMatchCase.Size = New System.Drawing.Size(83, 17)
        Me.chkMatchCase.TabIndex = 3
        Me.chkMatchCase.Text = "Match Case"
        Me.chkMatchCase.UseVisualStyleBackColor = True
        '
        'txtSearchTerm
        '
        Me.txtSearchTerm.Location = New System.Drawing.Point(15, 25)
        Me.txtSearchTerm.Multiline = True
        Me.txtSearchTerm.Name = "txtSearchTerm"
        Me.txtSearchTerm.Size = New System.Drawing.Size(321, 32)
        Me.txtSearchTerm.TabIndex = 1
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(12, 9)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(232, 13)
        Me.Label1.TabIndex = 5
        Me.Label1.Text = "Search Term (press ctrl+enter to add new lines):"
        '
        'txtReplacementText
        '
        Me.txtReplacementText.Location = New System.Drawing.Point(16, 76)
        Me.txtReplacementText.Multiline = True
        Me.txtReplacementText.Name = "txtReplacementText"
        Me.txtReplacementText.Size = New System.Drawing.Size(320, 32)
        Me.txtReplacementText.TabIndex = 2
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(13, 60)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(258, 13)
        Me.Label2.TabIndex = 10
        Me.Label2.Text = "Replacement Text (press ctrl+enter to add new lines):"
        '
        'btnReplace
        '
        Me.btnReplace.Location = New System.Drawing.Point(89, 119)
        Me.btnReplace.Name = "btnReplace"
        Me.btnReplace.Size = New System.Drawing.Size(75, 21)
        Me.btnReplace.TabIndex = 6
        Me.btnReplace.Text = "&Replace"
        Me.btnReplace.UseVisualStyleBackColor = True
        '
        'btnReplaceAll
        '
        Me.btnReplaceAll.Location = New System.Drawing.Point(170, 119)
        Me.btnReplaceAll.Name = "btnReplaceAll"
        Me.btnReplaceAll.Size = New System.Drawing.Size(75, 21)
        Me.btnReplaceAll.TabIndex = 7
        Me.btnReplaceAll.Text = "Replace &All"
        Me.btnReplaceAll.UseVisualStyleBackColor = True
        '
        'ChkStringReverse
        '
        Me.ChkStringReverse.AutoSize = True
        Me.ChkStringReverse.Location = New System.Drawing.Point(251, 114)
        Me.ChkStringReverse.Name = "ChkStringReverse"
        Me.ChkStringReverse.Size = New System.Drawing.Size(77, 17)
        Me.ChkStringReverse.TabIndex = 11
        Me.ChkStringReverse.Text = "Search Up"
        Me.ChkStringReverse.UseVisualStyleBackColor = True
        '
        'frmReplace
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(353, 153)
        Me.Controls.Add(Me.ChkStringReverse)
        Me.Controls.Add(Me.btnReplaceAll)
        Me.Controls.Add(Me.btnReplace)
        Me.Controls.Add(Me.txtReplacementText)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.btnFind)
        Me.Controls.Add(Me.chkMatchCase)
        Me.Controls.Add(Me.txtSearchTerm)
        Me.Controls.Add(Me.Label1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmReplace"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Replace Text"
        Me.TopMost = True
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents btnFind As System.Windows.Forms.Button
    Friend WithEvents chkMatchCase As System.Windows.Forms.CheckBox
    Friend WithEvents txtSearchTerm As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtReplacementText As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents btnReplace As System.Windows.Forms.Button
    Friend WithEvents btnReplaceAll As System.Windows.Forms.Button
    Friend WithEvents ChkStringReverse As System.Windows.Forms.CheckBox
End Class
