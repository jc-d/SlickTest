<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ProjectSelect
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
        Dim ListViewGroup1 As System.Windows.Forms.ListViewGroup = New System.Windows.Forms.ListViewGroup("Projects", System.Windows.Forms.HorizontalAlignment.Left)
        Dim ListViewGroup2 As System.Windows.Forms.ListViewGroup = New System.Windows.Forms.ListViewGroup("ListViewGroup", System.Windows.Forms.HorizontalAlignment.Left)
        Dim ListViewItem1 As System.Windows.Forms.ListViewItem = New System.Windows.Forms.ListViewItem(New String() {"Library Project", "Typical Project", "Simple Project"}, 0)
        Dim ListViewItem2 As System.Windows.Forms.ListViewItem = New System.Windows.Forms.ListViewItem("Record and Go", "MouseMove2.bmp")
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(ProjectSelect))
        Me.ProjectSelectListView = New System.Windows.Forms.ListView
        Me.Projects = New System.Windows.Forms.ColumnHeader("Utilities-128x128.png")
        Me.ImageList1 = New System.Windows.Forms.ImageList(Me.components)
        Me.SaveFileDialog1 = New System.Windows.Forms.SaveFileDialog
        Me.BrowseButton = New System.Windows.Forms.Button
        Me.OpenFileDialog1 = New System.Windows.Forms.OpenFileDialog
        Me.OkButton = New System.Windows.Forms.Button
        Me.LanguageDropDown = New System.Windows.Forms.ComboBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.SuspendLayout()
        '
        'ProjectSelectListView
        '
        Me.ProjectSelectListView.BackColor = System.Drawing.Color.Silver
        Me.ProjectSelectListView.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.Projects})
        Me.ProjectSelectListView.ForeColor = System.Drawing.Color.Black
        Me.ProjectSelectListView.GridLines = True
        ListViewGroup1.Header = "Projects"
        ListViewGroup1.Name = "Projects"
        ListViewGroup2.Header = "ListViewGroup"
        ListViewGroup2.Name = "ListViewGroup1"
        Me.ProjectSelectListView.Groups.AddRange(New System.Windows.Forms.ListViewGroup() {ListViewGroup1, ListViewGroup2})
        ListViewItem1.ToolTipText = "Library projects provide the ability to include code for multiple projects."
        Me.ProjectSelectListView.Items.AddRange(New System.Windows.Forms.ListViewItem() {ListViewItem1, ListViewItem2})
        Me.ProjectSelectListView.LargeImageList = Me.ImageList1
        Me.ProjectSelectListView.Location = New System.Drawing.Point(12, 12)
        Me.ProjectSelectListView.MultiSelect = False
        Me.ProjectSelectListView.Name = "ProjectSelectListView"
        Me.ProjectSelectListView.Size = New System.Drawing.Size(329, 131)
        Me.ProjectSelectListView.SmallImageList = Me.ImageList1
        Me.ProjectSelectListView.TabIndex = 0
        Me.ProjectSelectListView.UseCompatibleStateImageBehavior = False
        '
        'Projects
        '
        Me.Projects.Text = "Project Options"
        Me.Projects.Width = 95
        '
        'ImageList1
        '
        Me.ImageList1.ImageStream = CType(resources.GetObject("ImageList1.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.ImageList1.TransparentColor = System.Drawing.Color.Transparent
        Me.ImageList1.Images.SetKeyName(0, "Utilities-128x128.png")
        Me.ImageList1.Images.SetKeyName(1, "MouseMove2.bmp")
        '
        'BrowseButton
        '
        Me.BrowseButton.Location = New System.Drawing.Point(12, 149)
        Me.BrowseButton.Name = "BrowseButton"
        Me.BrowseButton.Size = New System.Drawing.Size(75, 23)
        Me.BrowseButton.TabIndex = 1
        Me.BrowseButton.Text = "Browse"
        Me.BrowseButton.UseVisualStyleBackColor = True
        '
        'OpenFileDialog1
        '
        Me.OpenFileDialog1.DefaultExt = "stp"
        Me.OpenFileDialog1.FileName = "Slick Test Project Select"
        Me.OpenFileDialog1.Filter = "Slick Test Project|*.stp"
        '
        'OkButton
        '
        Me.OkButton.Location = New System.Drawing.Point(265, 149)
        Me.OkButton.Name = "OkButton"
        Me.OkButton.Size = New System.Drawing.Size(75, 23)
        Me.OkButton.TabIndex = 2
        Me.OkButton.Text = "Ok"
        Me.OkButton.UseVisualStyleBackColor = True
        '
        'LanguageDropDown
        '
        Me.LanguageDropDown.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.LanguageDropDown.FormattingEnabled = True
        Me.LanguageDropDown.Location = New System.Drawing.Point(151, 150)
        Me.LanguageDropDown.Name = "LanguageDropDown"
        Me.LanguageDropDown.Size = New System.Drawing.Size(108, 21)
        Me.LanguageDropDown.TabIndex = 3
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(91, 153)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(58, 13)
        Me.Label1.TabIndex = 4
        Me.Label1.Text = "Language:"
        '
        'ProjectSelect
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(352, 178)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.LanguageDropDown)
        Me.Controls.Add(Me.OkButton)
        Me.Controls.Add(Me.BrowseButton)
        Me.Controls.Add(Me.ProjectSelectListView)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "ProjectSelect"
        Me.Text = "Project Select"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents ProjectSelectListView As System.Windows.Forms.ListView
    Friend WithEvents Projects As System.Windows.Forms.ColumnHeader
    Friend WithEvents ImageList1 As System.Windows.Forms.ImageList
    Friend WithEvents SaveFileDialog1 As System.Windows.Forms.SaveFileDialog
    Friend WithEvents BrowseButton As System.Windows.Forms.Button
    Friend WithEvents OpenFileDialog1 As System.Windows.Forms.OpenFileDialog
    Friend WithEvents OkButton As System.Windows.Forms.Button
    Friend WithEvents LanguageDropDown As System.Windows.Forms.ComboBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
End Class
