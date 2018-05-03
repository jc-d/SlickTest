<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Recorder
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Recorder))
        Me.RecorderTextBox = New TextEditor.TextEditorBox
        Me.RecordTabControl = New System.Windows.Forms.TabControl
        Me.TabPage1 = New System.Windows.Forms.TabPage
        Me.TabPage2 = New System.Windows.Forms.TabPage
        Me.DescriptionTextBox = New TextEditor.TextEditorBox
        Me.TabPage3 = New System.Windows.Forms.TabPage
        Me.TotalDescriptionLengthMaskedTextBox = New System.Windows.Forms.MaskedTextBox
        Me.Label3 = New System.Windows.Forms.Label
        Me.XYRecordingCheckBox = New System.Windows.Forms.CheckBox
        Me.Label2 = New System.Windows.Forms.Label
        Me.ObjectRefTextBox = New System.Windows.Forms.TextBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.SleepTimeMaskedTextBox = New System.Windows.Forms.MaskedTextBox
        Me.SleepTimeLabel = New System.Windows.Forms.Label
        Me.InfoLabel = New System.Windows.Forms.Label
        Me.CodeIntoEditorButton = New System.Windows.Forms.Button
        Me.CodeIntoClipboardButton = New System.Windows.Forms.Button
        Me.ConvertDescriptionsButton = New System.Windows.Forms.Button
        Me.RecordButton = New System.Windows.Forms.Button
        Me.DoubleClickTimer = New System.Windows.Forms.Timer(Me.components)
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.RecordTabControl.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        Me.TabPage2.SuspendLayout()
        Me.TabPage3.SuspendLayout()
        Me.SuspendLayout()
        '
        'RecorderTextBox
        '
        Me.RecorderTextBox.IsReadOnly = False
        Me.RecorderTextBox.Location = New System.Drawing.Point(0, 3)
        Me.RecorderTextBox.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.RecorderTextBox.Name = "RecorderTextBox"
        Me.RecorderTextBox.ShowSpaces = True
        Me.RecorderTextBox.ShowTabs = True
        Me.RecorderTextBox.Size = New System.Drawing.Size(513, 277)
        Me.RecorderTextBox.TabIndex = 0
        '
        'RecordTabControl
        '
        Me.RecordTabControl.Controls.Add(Me.TabPage1)
        Me.RecordTabControl.Controls.Add(Me.TabPage2)
        Me.RecordTabControl.Controls.Add(Me.TabPage3)
        Me.RecordTabControl.Location = New System.Drawing.Point(3, 0)
        Me.RecordTabControl.Name = "RecordTabControl"
        Me.RecordTabControl.SelectedIndex = 0
        Me.RecordTabControl.Size = New System.Drawing.Size(527, 307)
        Me.RecordTabControl.TabIndex = 1
        '
        'TabPage1
        '
        Me.TabPage1.Controls.Add(Me.RecorderTextBox)
        Me.TabPage1.Location = New System.Drawing.Point(4, 22)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage1.Size = New System.Drawing.Size(519, 281)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "Record"
        Me.TabPage1.UseVisualStyleBackColor = True
        '
        'TabPage2
        '
        Me.TabPage2.Controls.Add(Me.DescriptionTextBox)
        Me.TabPage2.Location = New System.Drawing.Point(4, 22)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage2.Size = New System.Drawing.Size(519, 281)
        Me.TabPage2.TabIndex = 1
        Me.TabPage2.Text = "Descriptions"
        Me.TabPage2.UseVisualStyleBackColor = True
        '
        'DescriptionTextBox
        '
        Me.DescriptionTextBox.IsReadOnly = False
        Me.DescriptionTextBox.Location = New System.Drawing.Point(0, 3)
        Me.DescriptionTextBox.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.DescriptionTextBox.Name = "DescriptionTextBox"
        Me.DescriptionTextBox.ShowSpaces = True
        Me.DescriptionTextBox.ShowTabs = True
        Me.DescriptionTextBox.Size = New System.Drawing.Size(513, 281)
        Me.DescriptionTextBox.TabIndex = 1
        '
        'TabPage3
        '
        Me.TabPage3.Controls.Add(Me.TotalDescriptionLengthMaskedTextBox)
        Me.TabPage3.Controls.Add(Me.Label3)
        Me.TabPage3.Controls.Add(Me.XYRecordingCheckBox)
        Me.TabPage3.Controls.Add(Me.Label2)
        Me.TabPage3.Controls.Add(Me.ObjectRefTextBox)
        Me.TabPage3.Controls.Add(Me.Label1)
        Me.TabPage3.Controls.Add(Me.SleepTimeMaskedTextBox)
        Me.TabPage3.Controls.Add(Me.SleepTimeLabel)
        Me.TabPage3.Controls.Add(Me.InfoLabel)
        Me.TabPage3.Location = New System.Drawing.Point(4, 22)
        Me.TabPage3.Name = "TabPage3"
        Me.TabPage3.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage3.Size = New System.Drawing.Size(519, 281)
        Me.TabPage3.TabIndex = 2
        Me.TabPage3.Text = "Recording Options"
        Me.TabPage3.UseVisualStyleBackColor = True
        '
        'TotalDescriptionLengthMaskedTextBox
        '
        Me.TotalDescriptionLengthMaskedTextBox.Location = New System.Drawing.Point(182, 214)
        Me.TotalDescriptionLengthMaskedTextBox.Mask = "000"
        Me.TotalDescriptionLengthMaskedTextBox.Name = "TotalDescriptionLengthMaskedTextBox"
        Me.TotalDescriptionLengthMaskedTextBox.Size = New System.Drawing.Size(101, 20)
        Me.TotalDescriptionLengthMaskedTextBox.TabIndex = 8
        Me.TotalDescriptionLengthMaskedTextBox.Text = "35"
        Me.ToolTip1.SetToolTip(Me.TotalDescriptionLengthMaskedTextBox, "Maximum 255, minimum 20")
        Me.TotalDescriptionLengthMaskedTextBox.ValidatingType = GetType(Integer)
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(27, 217)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(111, 13)
        Me.Label3.TabIndex = 7
        Me.Label3.Text = "Total Variable Length:"
        Me.ToolTip1.SetToolTip(Me.Label3, "Maximum 255, minimum 20")
        '
        'XYRecordingCheckBox
        '
        Me.XYRecordingCheckBox.AutoSize = True
        Me.XYRecordingCheckBox.Location = New System.Drawing.Point(182, 179)
        Me.XYRecordingCheckBox.Name = "XYRecordingCheckBox"
        Me.XYRecordingCheckBox.Size = New System.Drawing.Size(97, 17)
        Me.XYRecordingCheckBox.TabIndex = 6
        Me.XYRecordingCheckBox.Text = "X/Y Recording"
        Me.XYRecordingCheckBox.UseVisualStyleBackColor = True
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(27, 184)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(134, 13)
        Me.Label2.TabIndex = 5
        Me.Label2.Text = "Record Using Coordinates:"
        Me.ToolTip1.SetToolTip(Me.Label2, "Sleep time between clicks, in milliseconds")
        '
        'ObjectRefTextBox
        '
        Me.ObjectRefTextBox.Location = New System.Drawing.Point(182, 140)
        Me.ObjectRefTextBox.Name = "ObjectRefTextBox"
        Me.ObjectRefTextBox.Size = New System.Drawing.Size(101, 20)
        Me.ObjectRefTextBox.TabIndex = 4
        Me.ToolTip1.SetToolTip(Me.ObjectRefTextBox, "The object that references the automation.  Example: Me.<automation> or MyObject." & _
                "<automation>")
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(27, 143)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(94, 13)
        Me.Label1.TabIndex = 3
        Me.Label1.Text = "Object Reference:"
        Me.ToolTip1.SetToolTip(Me.Label1, "Sleep time between clicks, in milliseconds")
        '
        'SleepTimeMaskedTextBox
        '
        Me.SleepTimeMaskedTextBox.Location = New System.Drawing.Point(182, 95)
        Me.SleepTimeMaskedTextBox.Mask = "00000"
        Me.SleepTimeMaskedTextBox.Name = "SleepTimeMaskedTextBox"
        Me.SleepTimeMaskedTextBox.Size = New System.Drawing.Size(101, 20)
        Me.SleepTimeMaskedTextBox.TabIndex = 2
        Me.SleepTimeMaskedTextBox.ValidatingType = GetType(Integer)
        '
        'SleepTimeLabel
        '
        Me.SleepTimeLabel.AutoSize = True
        Me.SleepTimeLabel.Location = New System.Drawing.Point(27, 98)
        Me.SleepTimeLabel.Name = "SleepTimeLabel"
        Me.SleepTimeLabel.Size = New System.Drawing.Size(63, 13)
        Me.SleepTimeLabel.TabIndex = 1
        Me.SleepTimeLabel.Text = "Sleep Time:"
        Me.ToolTip1.SetToolTip(Me.SleepTimeLabel, "Sleep time between clicks, in milliseconds")
        '
        'InfoLabel
        '
        Me.InfoLabel.AutoEllipsis = True
        Me.InfoLabel.Location = New System.Drawing.Point(103, 24)
        Me.InfoLabel.Name = "InfoLabel"
        Me.InfoLabel.Size = New System.Drawing.Size(281, 72)
        Me.InfoLabel.TabIndex = 0
        Me.InfoLabel.Text = resources.GetString("InfoLabel.Text")
        '
        'CodeIntoEditorButton
        '
        Me.CodeIntoEditorButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.CodeIntoEditorButton.Location = New System.Drawing.Point(264, 325)
        Me.CodeIntoEditorButton.Name = "CodeIntoEditorButton"
        Me.CodeIntoEditorButton.Size = New System.Drawing.Size(90, 34)
        Me.CodeIntoEditorButton.TabIndex = 6
        Me.CodeIntoEditorButton.Text = "Code Into Editor"
        Me.CodeIntoEditorButton.UseVisualStyleBackColor = True
        '
        'CodeIntoClipboardButton
        '
        Me.CodeIntoClipboardButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.CodeIntoClipboardButton.Location = New System.Drawing.Point(168, 325)
        Me.CodeIntoClipboardButton.Name = "CodeIntoClipboardButton"
        Me.CodeIntoClipboardButton.Size = New System.Drawing.Size(90, 34)
        Me.CodeIntoClipboardButton.TabIndex = 5
        Me.CodeIntoClipboardButton.Text = "Code Into Clipboard"
        Me.CodeIntoClipboardButton.UseVisualStyleBackColor = True
        '
        'ConvertDescriptionsButton
        '
        Me.ConvertDescriptionsButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.ConvertDescriptionsButton.Location = New System.Drawing.Point(437, 325)
        Me.ConvertDescriptionsButton.Name = "ConvertDescriptionsButton"
        Me.ConvertDescriptionsButton.Size = New System.Drawing.Size(90, 34)
        Me.ConvertDescriptionsButton.TabIndex = 4
        Me.ConvertDescriptionsButton.Text = "Convert Descriptions"
        Me.ConvertDescriptionsButton.UseVisualStyleBackColor = True
        '
        'RecordButton
        '
        Me.RecordButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.RecordButton.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.RecordButton.Location = New System.Drawing.Point(7, 325)
        Me.RecordButton.Name = "RecordButton"
        Me.RecordButton.Size = New System.Drawing.Size(90, 34)
        Me.RecordButton.TabIndex = 7
        Me.RecordButton.Text = "Start Recording"
        Me.RecordButton.UseVisualStyleBackColor = False
        '
        'DoubleClickTimer
        '
        Me.DoubleClickTimer.Interval = 40
        '
        'Recorder
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(532, 362)
        Me.Controls.Add(Me.RecordButton)
        Me.Controls.Add(Me.CodeIntoEditorButton)
        Me.Controls.Add(Me.CodeIntoClipboardButton)
        Me.Controls.Add(Me.ConvertDescriptionsButton)
        Me.Controls.Add(Me.RecordTabControl)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "Recorder"
        Me.Text = "Recorder"
        Me.RecordTabControl.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        Me.TabPage2.ResumeLayout(False)
        Me.TabPage3.ResumeLayout(False)
        Me.TabPage3.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents RecorderTextBox As TextEditor.TextEditorBox
    Friend WithEvents RecordTabControl As System.Windows.Forms.TabControl
    Friend WithEvents TabPage1 As System.Windows.Forms.TabPage
    Friend WithEvents TabPage2 As System.Windows.Forms.TabPage
    Friend WithEvents DescriptionTextBox As TextEditor.TextEditorBox
    Friend WithEvents CodeIntoEditorButton As System.Windows.Forms.Button
    Friend WithEvents CodeIntoClipboardButton As System.Windows.Forms.Button
    Friend WithEvents ConvertDescriptionsButton As System.Windows.Forms.Button
    Friend WithEvents RecordButton As System.Windows.Forms.Button
    Friend WithEvents Keys As HandleInput.MouseAndKeys
    Friend WithEvents DoubleClickTimer As System.Windows.Forms.Timer
    Friend WithEvents TabPage3 As System.Windows.Forms.TabPage
    Friend WithEvents InfoLabel As System.Windows.Forms.Label
    Friend WithEvents ObjectRefTextBox As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    Friend WithEvents SleepTimeMaskedTextBox As System.Windows.Forms.MaskedTextBox
    Friend WithEvents SleepTimeLabel As System.Windows.Forms.Label
    Friend WithEvents XYRecordingCheckBox As System.Windows.Forms.CheckBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents TotalDescriptionLengthMaskedTextBox As System.Windows.Forms.MaskedTextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
End Class
