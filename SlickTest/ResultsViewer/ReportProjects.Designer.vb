<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ReportProjects
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(ReportProjects))
        Me.OkButton = New System.Windows.Forms.Button
        Me.ProjectGroupBox = New System.Windows.Forms.GroupBox
        Me.ExternalReportsCheckBox = New System.Windows.Forms.CheckBox
        Me.Label2 = New System.Windows.Forms.Label
        Me.PCComboBox = New System.Windows.Forms.ComboBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.ProjectSelectComboBox = New System.Windows.Forms.ComboBox
        Me.CancelProjectSelectButton = New System.Windows.Forms.Button
        Me.TestGroupBox = New System.Windows.Forms.GroupBox
        Me.Label3 = New System.Windows.Forms.Label
        Me.TestResultsComboBox = New System.Windows.Forms.ComboBox
        Me.ProjectGroupBox.SuspendLayout()
        Me.TestGroupBox.SuspendLayout()
        Me.SuspendLayout()
        '
        'OkButton
        '
        Me.OkButton.Location = New System.Drawing.Point(9, 244)
        Me.OkButton.Name = "OkButton"
        Me.OkButton.Size = New System.Drawing.Size(75, 23)
        Me.OkButton.TabIndex = 0
        Me.OkButton.Text = "OK"
        Me.OkButton.UseVisualStyleBackColor = True
        '
        'ProjectGroupBox
        '
        Me.ProjectGroupBox.Controls.Add(Me.ExternalReportsCheckBox)
        Me.ProjectGroupBox.Controls.Add(Me.Label2)
        Me.ProjectGroupBox.Controls.Add(Me.PCComboBox)
        Me.ProjectGroupBox.Controls.Add(Me.Label1)
        Me.ProjectGroupBox.Controls.Add(Me.ProjectSelectComboBox)
        Me.ProjectGroupBox.Location = New System.Drawing.Point(12, 12)
        Me.ProjectGroupBox.Name = "ProjectGroupBox"
        Me.ProjectGroupBox.Size = New System.Drawing.Size(242, 145)
        Me.ProjectGroupBox.TabIndex = 1
        Me.ProjectGroupBox.TabStop = False
        Me.ProjectGroupBox.Text = "Project information"
        '
        'ExternalReportsCheckBox
        '
        Me.ExternalReportsCheckBox.AutoSize = True
        Me.ExternalReportsCheckBox.Location = New System.Drawing.Point(6, 114)
        Me.ExternalReportsCheckBox.Name = "ExternalReportsCheckBox"
        Me.ExternalReportsCheckBox.Size = New System.Drawing.Size(104, 17)
        Me.ExternalReportsCheckBox.TabIndex = 5
        Me.ExternalReportsCheckBox.Text = "External Reports"
        Me.ExternalReportsCheckBox.UseVisualStyleBackColor = True
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(6, 64)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(24, 13)
        Me.Label2.TabIndex = 3
        Me.Label2.Text = "PC:"
        '
        'PCComboBox
        '
        Me.PCComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.PCComboBox.FormattingEnabled = True
        Me.PCComboBox.Location = New System.Drawing.Point(6, 87)
        Me.PCComboBox.Name = "PCComboBox"
        Me.PCComboBox.Size = New System.Drawing.Size(230, 21)
        Me.PCComboBox.TabIndex = 2
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(6, 16)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(43, 13)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "Project:"
        '
        'ProjectSelectComboBox
        '
        Me.ProjectSelectComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ProjectSelectComboBox.FormattingEnabled = True
        Me.ProjectSelectComboBox.Location = New System.Drawing.Point(6, 40)
        Me.ProjectSelectComboBox.Name = "ProjectSelectComboBox"
        Me.ProjectSelectComboBox.Size = New System.Drawing.Size(230, 21)
        Me.ProjectSelectComboBox.TabIndex = 0
        '
        'CancelProjectSelectButton
        '
        Me.CancelProjectSelectButton.Location = New System.Drawing.Point(179, 244)
        Me.CancelProjectSelectButton.Name = "CancelProjectSelectButton"
        Me.CancelProjectSelectButton.Size = New System.Drawing.Size(75, 23)
        Me.CancelProjectSelectButton.TabIndex = 2
        Me.CancelProjectSelectButton.Text = "Cancel"
        Me.CancelProjectSelectButton.UseVisualStyleBackColor = True
        '
        'TestGroupBox
        '
        Me.TestGroupBox.Controls.Add(Me.Label3)
        Me.TestGroupBox.Controls.Add(Me.TestResultsComboBox)
        Me.TestGroupBox.Location = New System.Drawing.Point(12, 163)
        Me.TestGroupBox.Name = "TestGroupBox"
        Me.TestGroupBox.Size = New System.Drawing.Size(242, 75)
        Me.TestGroupBox.TabIndex = 3
        Me.TestGroupBox.TabStop = False
        Me.TestGroupBox.Text = "Test Information"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(3, 16)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(69, 13)
        Me.Label3.TabIndex = 3
        Me.Label3.Text = "Test Results:"
        '
        'TestResultsComboBox
        '
        Me.TestResultsComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.TestResultsComboBox.FormattingEnabled = True
        Me.TestResultsComboBox.Items.AddRange(New Object() {"*", "Pass", "Fail", "Warning"})
        Me.TestResultsComboBox.Location = New System.Drawing.Point(6, 36)
        Me.TestResultsComboBox.Name = "TestResultsComboBox"
        Me.TestResultsComboBox.Size = New System.Drawing.Size(230, 21)
        Me.TestResultsComboBox.TabIndex = 2
        '
        'ReportProjects
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(266, 275)
        Me.Controls.Add(Me.TestGroupBox)
        Me.Controls.Add(Me.CancelProjectSelectButton)
        Me.Controls.Add(Me.ProjectGroupBox)
        Me.Controls.Add(Me.OkButton)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "ReportProjects"
        Me.Text = "Project Select"
        Me.ProjectGroupBox.ResumeLayout(False)
        Me.ProjectGroupBox.PerformLayout()
        Me.TestGroupBox.ResumeLayout(False)
        Me.TestGroupBox.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents OkButton As System.Windows.Forms.Button
    Friend WithEvents ProjectGroupBox As System.Windows.Forms.GroupBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents PCComboBox As System.Windows.Forms.ComboBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents ProjectSelectComboBox As System.Windows.Forms.ComboBox
    Friend WithEvents CancelProjectSelectButton As System.Windows.Forms.Button
    Friend WithEvents ExternalReportsCheckBox As System.Windows.Forms.CheckBox
    Friend WithEvents TestGroupBox As System.Windows.Forms.GroupBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents TestResultsComboBox As System.Windows.Forms.ComboBox
End Class
