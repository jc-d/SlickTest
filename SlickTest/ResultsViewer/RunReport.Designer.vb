<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class RunReport
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(RunReport))
        Me.ExternalReportCheckBox = New System.Windows.Forms.CheckBox
        Me.ProjectComboBox = New System.Windows.Forms.ComboBox
        Me.StatusStrip1 = New System.Windows.Forms.StatusStrip
        Me.ReportTextBox = New System.Windows.Forms.WebBrowser
        Me.ReportViewGroupBox = New System.Windows.Forms.GroupBox
        Me.BuildAndShowButton = New System.Windows.Forms.Button
        Me.MenuStrip1 = New System.Windows.Forms.MenuStrip
        Me.FileToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.OpenDatabaseToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator
        Me.SaveAsHTMLToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.SaveToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.SaveAsCSVToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripSeparator4 = New System.Windows.Forms.ToolStripSeparator
        Me.EmailReportToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripSeparator2 = New System.Windows.Forms.ToolStripSeparator
        Me.PrintToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.PrintPreviewToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripSeparator3 = New System.Windows.Forms.ToolStripSeparator
        Me.CloseToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.DatabaseToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.ClearDatabaseToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.GetLastSQLCommandToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.CompactDatabaseToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.ShrinkDatabaseToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.ViewToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.ProjectFiltersToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.RefreshReportsListToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.SaveFileDialog1 = New System.Windows.Forms.SaveFileDialog
        Me.TestResultsTextBox = New System.Windows.Forms.TextBox
        Me.TestResultsComboBox = New System.Windows.Forms.ComboBox
        Me.EditResultsButton = New System.Windows.Forms.Button
        Me.TestResultsGroupBox = New System.Windows.Forms.GroupBox
        Me.SaveResultsButton = New System.Windows.Forms.Button
        Me.Label2 = New System.Windows.Forms.Label
        Me.Label1 = New System.Windows.Forms.Label
        Me.OpenFileDialog1 = New System.Windows.Forms.OpenFileDialog
        Me.ReportViewGroupBox.SuspendLayout()
        Me.MenuStrip1.SuspendLayout()
        Me.TestResultsGroupBox.SuspendLayout()
        Me.SuspendLayout()
        '
        'ExternalReportCheckBox
        '
        Me.ExternalReportCheckBox.AutoSize = True
        Me.ExternalReportCheckBox.Location = New System.Drawing.Point(257, 18)
        Me.ExternalReportCheckBox.Name = "ExternalReportCheckBox"
        Me.ExternalReportCheckBox.Size = New System.Drawing.Size(134, 17)
        Me.ExternalReportCheckBox.TabIndex = 0
        Me.ExternalReportCheckBox.Text = "Show External Reports"
        Me.ExternalReportCheckBox.UseVisualStyleBackColor = True
        '
        'ProjectComboBox
        '
        Me.ProjectComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ProjectComboBox.FormattingEnabled = True
        Me.ProjectComboBox.Location = New System.Drawing.Point(6, 19)
        Me.ProjectComboBox.Name = "ProjectComboBox"
        Me.ProjectComboBox.Size = New System.Drawing.Size(245, 21)
        Me.ProjectComboBox.TabIndex = 1
        '
        'StatusStrip1
        '
        Me.StatusStrip1.Location = New System.Drawing.Point(0, 436)
        Me.StatusStrip1.Name = "StatusStrip1"
        Me.StatusStrip1.Size = New System.Drawing.Size(495, 22)
        Me.StatusStrip1.TabIndex = 3
        Me.StatusStrip1.Text = "StatusStrip1"
        '
        'ReportTextBox
        '
        Me.ReportTextBox.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ReportTextBox.Location = New System.Drawing.Point(12, 27)
        Me.ReportTextBox.Name = "ReportTextBox"
        Me.ReportTextBox.Size = New System.Drawing.Size(471, 202)
        Me.ReportTextBox.TabIndex = 4
        '
        'ReportViewGroupBox
        '
        Me.ReportViewGroupBox.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.ReportViewGroupBox.Controls.Add(Me.ExternalReportCheckBox)
        Me.ReportViewGroupBox.Controls.Add(Me.ProjectComboBox)
        Me.ReportViewGroupBox.Location = New System.Drawing.Point(12, 383)
        Me.ReportViewGroupBox.Name = "ReportViewGroupBox"
        Me.ReportViewGroupBox.Size = New System.Drawing.Size(411, 50)
        Me.ReportViewGroupBox.TabIndex = 6
        Me.ReportViewGroupBox.TabStop = False
        Me.ReportViewGroupBox.Text = "Report View"
        '
        'BuildAndShowButton
        '
        Me.BuildAndShowButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.BuildAndShowButton.Location = New System.Drawing.Point(429, 383)
        Me.BuildAndShowButton.Name = "BuildAndShowButton"
        Me.BuildAndShowButton.Size = New System.Drawing.Size(58, 50)
        Me.BuildAndShowButton.TabIndex = 7
        Me.BuildAndShowButton.Text = "Build && Show Report"
        Me.BuildAndShowButton.UseVisualStyleBackColor = True
        '
        'MenuStrip1
        '
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.FileToolStripMenuItem, Me.DatabaseToolStripMenuItem, Me.ViewToolStripMenuItem})
        Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.Size = New System.Drawing.Size(495, 24)
        Me.MenuStrip1.TabIndex = 9
        Me.MenuStrip1.Text = "MenuStrip1"
        '
        'FileToolStripMenuItem
        '
        Me.FileToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.OpenDatabaseToolStripMenuItem, Me.ToolStripSeparator1, Me.SaveAsHTMLToolStripMenuItem, Me.SaveToolStripMenuItem, Me.SaveAsCSVToolStripMenuItem, Me.ToolStripSeparator4, Me.EmailReportToolStripMenuItem, Me.ToolStripSeparator2, Me.PrintToolStripMenuItem, Me.PrintPreviewToolStripMenuItem, Me.ToolStripSeparator3, Me.CloseToolStripMenuItem})
        Me.FileToolStripMenuItem.Name = "FileToolStripMenuItem"
        Me.FileToolStripMenuItem.Size = New System.Drawing.Size(35, 20)
        Me.FileToolStripMenuItem.Text = "&File"
        '
        'OpenDatabaseToolStripMenuItem
        '
        Me.OpenDatabaseToolStripMenuItem.Name = "OpenDatabaseToolStripMenuItem"
        Me.OpenDatabaseToolStripMenuItem.Size = New System.Drawing.Size(160, 22)
        Me.OpenDatabaseToolStripMenuItem.Text = "&Open Database"
        '
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        Me.ToolStripSeparator1.Size = New System.Drawing.Size(157, 6)
        '
        'SaveAsHTMLToolStripMenuItem
        '
        Me.SaveAsHTMLToolStripMenuItem.Name = "SaveAsHTMLToolStripMenuItem"
        Me.SaveAsHTMLToolStripMenuItem.Size = New System.Drawing.Size(160, 22)
        Me.SaveAsHTMLToolStripMenuItem.Text = "Save as &HTML"
        '
        'SaveToolStripMenuItem
        '
        Me.SaveToolStripMenuItem.Name = "SaveToolStripMenuItem"
        Me.SaveToolStripMenuItem.Size = New System.Drawing.Size(160, 22)
        Me.SaveToolStripMenuItem.Text = "Save as &TXT"
        '
        'SaveAsCSVToolStripMenuItem
        '
        Me.SaveAsCSVToolStripMenuItem.Name = "SaveAsCSVToolStripMenuItem"
        Me.SaveAsCSVToolStripMenuItem.Size = New System.Drawing.Size(160, 22)
        Me.SaveAsCSVToolStripMenuItem.Text = "Save as C&SV"
        '
        'ToolStripSeparator4
        '
        Me.ToolStripSeparator4.Name = "ToolStripSeparator4"
        Me.ToolStripSeparator4.Size = New System.Drawing.Size(157, 6)
        '
        'EmailReportToolStripMenuItem
        '
        Me.EmailReportToolStripMenuItem.Name = "EmailReportToolStripMenuItem"
        Me.EmailReportToolStripMenuItem.Size = New System.Drawing.Size(160, 22)
        Me.EmailReportToolStripMenuItem.Text = "&Email Report"
        '
        'ToolStripSeparator2
        '
        Me.ToolStripSeparator2.Name = "ToolStripSeparator2"
        Me.ToolStripSeparator2.Size = New System.Drawing.Size(157, 6)
        '
        'PrintToolStripMenuItem
        '
        Me.PrintToolStripMenuItem.Name = "PrintToolStripMenuItem"
        Me.PrintToolStripMenuItem.Size = New System.Drawing.Size(160, 22)
        Me.PrintToolStripMenuItem.Text = "&Print"
        '
        'PrintPreviewToolStripMenuItem
        '
        Me.PrintPreviewToolStripMenuItem.Name = "PrintPreviewToolStripMenuItem"
        Me.PrintPreviewToolStripMenuItem.Size = New System.Drawing.Size(160, 22)
        Me.PrintPreviewToolStripMenuItem.Text = "Print Preview"
        '
        'ToolStripSeparator3
        '
        Me.ToolStripSeparator3.Name = "ToolStripSeparator3"
        Me.ToolStripSeparator3.Size = New System.Drawing.Size(157, 6)
        '
        'CloseToolStripMenuItem
        '
        Me.CloseToolStripMenuItem.Name = "CloseToolStripMenuItem"
        Me.CloseToolStripMenuItem.Size = New System.Drawing.Size(160, 22)
        Me.CloseToolStripMenuItem.Text = "&Close"
        '
        'DatabaseToolStripMenuItem
        '
        Me.DatabaseToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ClearDatabaseToolStripMenuItem, Me.GetLastSQLCommandToolStripMenuItem, Me.CompactDatabaseToolStripMenuItem, Me.ShrinkDatabaseToolStripMenuItem})
        Me.DatabaseToolStripMenuItem.Name = "DatabaseToolStripMenuItem"
        Me.DatabaseToolStripMenuItem.Size = New System.Drawing.Size(65, 20)
        Me.DatabaseToolStripMenuItem.Text = "Database"
        '
        'ClearDatabaseToolStripMenuItem
        '
        Me.ClearDatabaseToolStripMenuItem.Name = "ClearDatabaseToolStripMenuItem"
        Me.ClearDatabaseToolStripMenuItem.Size = New System.Drawing.Size(197, 22)
        Me.ClearDatabaseToolStripMenuItem.Text = "&Clear Database"
        '
        'GetLastSQLCommandToolStripMenuItem
        '
        Me.GetLastSQLCommandToolStripMenuItem.Name = "GetLastSQLCommandToolStripMenuItem"
        Me.GetLastSQLCommandToolStripMenuItem.Size = New System.Drawing.Size(197, 22)
        Me.GetLastSQLCommandToolStripMenuItem.Text = "Get Last SQL Command"
        Me.GetLastSQLCommandToolStripMenuItem.Visible = False
        '
        'CompactDatabaseToolStripMenuItem
        '
        Me.CompactDatabaseToolStripMenuItem.Name = "CompactDatabaseToolStripMenuItem"
        Me.CompactDatabaseToolStripMenuItem.Size = New System.Drawing.Size(197, 22)
        Me.CompactDatabaseToolStripMenuItem.Text = "Compact Database"
        '
        'ShrinkDatabaseToolStripMenuItem
        '
        Me.ShrinkDatabaseToolStripMenuItem.Name = "ShrinkDatabaseToolStripMenuItem"
        Me.ShrinkDatabaseToolStripMenuItem.Size = New System.Drawing.Size(197, 22)
        Me.ShrinkDatabaseToolStripMenuItem.Text = "Shrink Database"
        '
        'ViewToolStripMenuItem
        '
        Me.ViewToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ProjectFiltersToolStripMenuItem, Me.RefreshReportsListToolStripMenuItem})
        Me.ViewToolStripMenuItem.Name = "ViewToolStripMenuItem"
        Me.ViewToolStripMenuItem.Size = New System.Drawing.Size(41, 20)
        Me.ViewToolStripMenuItem.Text = "&View"
        '
        'ProjectFiltersToolStripMenuItem
        '
        Me.ProjectFiltersToolStripMenuItem.Name = "ProjectFiltersToolStripMenuItem"
        Me.ProjectFiltersToolStripMenuItem.Size = New System.Drawing.Size(202, 22)
        Me.ProjectFiltersToolStripMenuItem.Text = "&Project Filters"
        '
        'RefreshReportsListToolStripMenuItem
        '
        Me.RefreshReportsListToolStripMenuItem.Name = "RefreshReportsListToolStripMenuItem"
        Me.RefreshReportsListToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F5
        Me.RefreshReportsListToolStripMenuItem.Size = New System.Drawing.Size(202, 22)
        Me.RefreshReportsListToolStripMenuItem.Text = "Refresh Reports List"
        '
        'SaveFileDialog1
        '
        Me.SaveFileDialog1.DefaultExt = "txt"
        '
        'TestResultsTextBox
        '
        Me.TestResultsTextBox.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TestResultsTextBox.Enabled = False
        Me.TestResultsTextBox.Location = New System.Drawing.Point(7, 35)
        Me.TestResultsTextBox.Multiline = True
        Me.TestResultsTextBox.Name = "TestResultsTextBox"
        Me.TestResultsTextBox.Size = New System.Drawing.Size(344, 101)
        Me.TestResultsTextBox.TabIndex = 10
        '
        'TestResultsComboBox
        '
        Me.TestResultsComboBox.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TestResultsComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.TestResultsComboBox.Enabled = False
        Me.TestResultsComboBox.FormattingEnabled = True
        Me.TestResultsComboBox.Items.AddRange(New Object() {"Pass", "Fail", "Warning"})
        Me.TestResultsComboBox.Location = New System.Drawing.Point(357, 35)
        Me.TestResultsComboBox.Name = "TestResultsComboBox"
        Me.TestResultsComboBox.Size = New System.Drawing.Size(112, 21)
        Me.TestResultsComboBox.TabIndex = 11
        '
        'EditResultsButton
        '
        Me.EditResultsButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.EditResultsButton.Location = New System.Drawing.Point(357, 62)
        Me.EditResultsButton.Name = "EditResultsButton"
        Me.EditResultsButton.Size = New System.Drawing.Size(111, 34)
        Me.EditResultsButton.TabIndex = 12
        Me.EditResultsButton.Text = "Edit Results"
        Me.EditResultsButton.UseVisualStyleBackColor = True
        '
        'TestResultsGroupBox
        '
        Me.TestResultsGroupBox.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TestResultsGroupBox.Controls.Add(Me.SaveResultsButton)
        Me.TestResultsGroupBox.Controls.Add(Me.Label2)
        Me.TestResultsGroupBox.Controls.Add(Me.Label1)
        Me.TestResultsGroupBox.Controls.Add(Me.EditResultsButton)
        Me.TestResultsGroupBox.Controls.Add(Me.TestResultsComboBox)
        Me.TestResultsGroupBox.Controls.Add(Me.TestResultsTextBox)
        Me.TestResultsGroupBox.Location = New System.Drawing.Point(11, 232)
        Me.TestResultsGroupBox.Name = "TestResultsGroupBox"
        Me.TestResultsGroupBox.Size = New System.Drawing.Size(475, 142)
        Me.TestResultsGroupBox.TabIndex = 13
        Me.TestResultsGroupBox.TabStop = False
        Me.TestResultsGroupBox.Text = "Test Results"
        '
        'SaveResultsButton
        '
        Me.SaveResultsButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.SaveResultsButton.Enabled = False
        Me.SaveResultsButton.Location = New System.Drawing.Point(357, 102)
        Me.SaveResultsButton.Name = "SaveResultsButton"
        Me.SaveResultsButton.Size = New System.Drawing.Size(111, 34)
        Me.SaveResultsButton.TabIndex = 15
        Me.SaveResultsButton.Text = "Update Results"
        Me.SaveResultsButton.UseVisualStyleBackColor = True
        '
        'Label2
        '
        Me.Label2.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(354, 16)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(76, 13)
        Me.Label2.TabIndex = 14
        Me.Label2.Text = "Overall Result:"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(6, 16)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(96, 13)
        Me.Label1.TabIndex = 13
        Me.Label1.Text = "Result Description:"
        '
        'OpenFileDialog1
        '
        Me.OpenFileDialog1.DefaultExt = "sdf"
        Me.OpenFileDialog1.Filter = "Database|*.sdf"
        '
        'RunReport
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(495, 458)
        Me.Controls.Add(Me.TestResultsGroupBox)
        Me.Controls.Add(Me.BuildAndShowButton)
        Me.Controls.Add(Me.ReportViewGroupBox)
        Me.Controls.Add(Me.ReportTextBox)
        Me.Controls.Add(Me.StatusStrip1)
        Me.Controls.Add(Me.MenuStrip1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MainMenuStrip = Me.MenuStrip1
        Me.MinimumSize = New System.Drawing.Size(503, 345)
        Me.Name = "RunReport"
        Me.Text = "Report Viewer"
        Me.ReportViewGroupBox.ResumeLayout(False)
        Me.ReportViewGroupBox.PerformLayout()
        Me.MenuStrip1.ResumeLayout(False)
        Me.MenuStrip1.PerformLayout()
        Me.TestResultsGroupBox.ResumeLayout(False)
        Me.TestResultsGroupBox.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents ExternalReportCheckBox As System.Windows.Forms.CheckBox
    Friend WithEvents ProjectComboBox As System.Windows.Forms.ComboBox
    Friend WithEvents StatusStrip1 As System.Windows.Forms.StatusStrip
    Friend WithEvents ReportTextBox As System.Windows.Forms.WebBrowser
    Friend WithEvents ReportViewGroupBox As System.Windows.Forms.GroupBox
    Friend WithEvents BuildAndShowButton As System.Windows.Forms.Button
    Friend WithEvents MenuStrip1 As System.Windows.Forms.MenuStrip
    Friend WithEvents FileToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents SaveToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents CloseToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents SaveFileDialog1 As System.Windows.Forms.SaveFileDialog
    Friend WithEvents DatabaseToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ClearDatabaseToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents TestResultsTextBox As System.Windows.Forms.TextBox
    Friend WithEvents TestResultsComboBox As System.Windows.Forms.ComboBox
    Friend WithEvents EditResultsButton As System.Windows.Forms.Button
    Friend WithEvents TestResultsGroupBox As System.Windows.Forms.GroupBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents SaveResultsButton As System.Windows.Forms.Button
    Friend WithEvents GetLastSQLCommandToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents SaveAsCSVToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents EmailReportToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ViewToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ProjectFiltersToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents CompactDatabaseToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ShrinkDatabaseToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents OpenDatabaseToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents OpenFileDialog1 As System.Windows.Forms.OpenFileDialog
    Friend WithEvents RefreshReportsListToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents ToolStripSeparator2 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents PrintToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator3 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents PrintPreviewToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents SaveAsHTMLToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator4 As System.Windows.Forms.ToolStripSeparator
End Class
