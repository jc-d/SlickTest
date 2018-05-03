<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class AddDLLs
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
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel
        Me.CancelButton = New System.Windows.Forms.Button
        Me.OK_Button = New System.Windows.Forms.Button
        Me.AddButton = New System.Windows.Forms.Button
        Me.AsmListView = New System.Windows.Forms.ListView
        Me.NameColumnHeader = New System.Windows.Forms.ColumnHeader
        Me.VersionColumnHeader = New System.Windows.Forms.ColumnHeader
        Me.LanguageColumnHeader = New System.Windows.Forms.ColumnHeader
        Me.PathColumnHeader = New System.Windows.Forms.ColumnHeader
        Me.BrowseButton = New System.Windows.Forms.Button
        Me.OpenFileDialog = New System.Windows.Forms.OpenFileDialog
        Me.Label1 = New System.Windows.Forms.Label
        Me.BackgroundWorker1 = New System.ComponentModel.BackgroundWorker
        Me.TableLayoutPanel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TableLayoutPanel1.ColumnCount = 2
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel1.Controls.Add(Me.CancelButton, 1, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.OK_Button, 0, 0)
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(277, 283)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 1
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 29.0!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(146, 29)
        Me.TableLayoutPanel1.TabIndex = 0
        '
        'CancelButton
        '
        Me.CancelButton.Location = New System.Drawing.Point(76, 3)
        Me.CancelButton.Name = "CancelButton"
        Me.CancelButton.Size = New System.Drawing.Size(67, 23)
        Me.CancelButton.TabIndex = 3
        Me.CancelButton.Text = "Cancel"
        Me.CancelButton.UseVisualStyleBackColor = True
        '
        'OK_Button
        '
        Me.OK_Button.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.OK_Button.Location = New System.Drawing.Point(3, 3)
        Me.OK_Button.Name = "OK_Button"
        Me.OK_Button.Size = New System.Drawing.Size(67, 23)
        Me.OK_Button.TabIndex = 0
        Me.OK_Button.Text = "OK"
        '
        'AddButton
        '
        Me.AddButton.Location = New System.Drawing.Point(207, 286)
        Me.AddButton.Name = "AddButton"
        Me.AddButton.Size = New System.Drawing.Size(67, 23)
        Me.AddButton.TabIndex = 1
        Me.AddButton.Text = "Add"
        Me.AddButton.UseVisualStyleBackColor = True
        '
        'AsmListView
        '
        Me.AsmListView.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.NameColumnHeader, Me.VersionColumnHeader, Me.LanguageColumnHeader, Me.PathColumnHeader})
        Me.AsmListView.FullRowSelect = True
        Me.AsmListView.GridLines = True
        Me.AsmListView.Location = New System.Drawing.Point(12, 11)
        Me.AsmListView.Name = "AsmListView"
        Me.AsmListView.Size = New System.Drawing.Size(412, 264)
        Me.AsmListView.Sorting = System.Windows.Forms.SortOrder.Ascending
        Me.AsmListView.TabIndex = 1
        Me.AsmListView.UseCompatibleStateImageBehavior = False
        Me.AsmListView.View = System.Windows.Forms.View.Details
        '
        'NameColumnHeader
        '
        Me.NameColumnHeader.Text = "Name"
        Me.NameColumnHeader.Width = 331
        '
        'VersionColumnHeader
        '
        Me.VersionColumnHeader.Text = "Version"
        Me.VersionColumnHeader.Width = 116
        '
        'LanguageColumnHeader
        '
        Me.LanguageColumnHeader.Text = "Culture"
        Me.LanguageColumnHeader.Width = 88
        '
        'PathColumnHeader
        '
        Me.PathColumnHeader.Text = "Path"
        '
        'BrowseButton
        '
        Me.BrowseButton.Location = New System.Drawing.Point(12, 286)
        Me.BrowseButton.Name = "BrowseButton"
        Me.BrowseButton.Size = New System.Drawing.Size(67, 23)
        Me.BrowseButton.TabIndex = 2
        Me.BrowseButton.Text = "Browse"
        Me.BrowseButton.UseVisualStyleBackColor = True
        '
        'OpenFileDialog
        '
        Me.OpenFileDialog.DefaultExt = "dll"
        Me.OpenFileDialog.InitialDirectory = ".\"
        Me.OpenFileDialog.Title = "Add DLL files from "
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 36.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(26, 103)
        Me.Label1.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(405, 55)
        Me.Label1.TabIndex = 3
        Me.Label1.Text = "LOADING DLLs..."
        '
        'BackgroundWorker1
        '
        Me.BackgroundWorker1.WorkerSupportsCancellation = True
        '
        'AddDLLs
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(435, 324)
        Me.Controls.Add(Me.AddButton)
        Me.Controls.Add(Me.BrowseButton)
        Me.Controls.Add(Me.AsmListView)
        Me.Controls.Add(Me.TableLayoutPanel1)
        Me.Controls.Add(Me.Label1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "AddDLLs"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Add DLLs"
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents OK_Button As System.Windows.Forms.Button
    Friend WithEvents AsmListView As System.Windows.Forms.ListView
    Friend WithEvents AddButton As System.Windows.Forms.Button
    Friend WithEvents BrowseButton As System.Windows.Forms.Button
    Friend WithEvents OpenFileDialog As System.Windows.Forms.OpenFileDialog
    Friend WithEvents NameColumnHeader As System.Windows.Forms.ColumnHeader
    Friend WithEvents VersionColumnHeader As System.Windows.Forms.ColumnHeader
    Friend WithEvents LanguageColumnHeader As System.Windows.Forms.ColumnHeader
    Friend Shadows WithEvents CancelButton As System.Windows.Forms.Button
    Friend WithEvents PathColumnHeader As System.Windows.Forms.ColumnHeader
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents BackgroundWorker1 As System.ComponentModel.BackgroundWorker

End Class
