Imports System.Windows.Forms
Imports System.Reflection

Public Class frmProjectSettings

    Private Project As CurrentProjectData
    Public Sub New(ByRef pj As CurrentProjectData)
        'pg1.SelectedObject = MyIni.Sections

        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        ' Add any initialization after the InitializeComponent() call.
        Project = pj
        Project.SaveProject(Project.LoadLocation)
        For Each section As XmlSettings.IniSection In Project.MyIni.Sections
            If ("ProjectSettings" = section.Name) Then
                section.DisplayInPG = True
            Else
                section.DisplayInPG = False
            End If
        Next
        SettingsPropertyGrid.SelectedObject = Project.MyIni.Sections
        SettingsPropertyGrid.ExpandAllGridItems()
    End Sub

    Private Sub SetDescriptionWindowSize(ByVal size As Integer)
        Dim controlsProp As PropertyInfo = SettingsPropertyGrid.[GetType]().GetProperty("Controls")
        Dim cc As Control.ControlCollection = DirectCast(controlsProp.GetValue(SettingsPropertyGrid, Nothing), Control.ControlCollection)
        For Each c As Control In cc
            Dim controlType As Type = c.[GetType]()
            If controlType.Name = "DocComment" Then
                Dim linesProperty As PropertyInfo = controlType.GetProperty("Lines")
                linesProperty.SetValue(c, size, Nothing)
                Dim userSizedField As FieldInfo = controlType.BaseType.GetField("userSized", BindingFlags.Instance Or BindingFlags.NonPublic)
                userSizedField.SetValue(c, True)
                Exit For
            End If
        Next
    End Sub

    Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK_Button.Click
        Dim BF As System.Collections.Generic.List(Of String) = Project.BuildFiles
        Dim PjName As String = Project.ProjectName
        Project.MyIni.Sections = SettingsPropertyGrid.SelectedObject
        Project.MyIni.Save(Project.LoadLocation)
        Project.LoadProject(Project.LoadLocation)
        If (PjName <> Project.ProjectName) Then
            Project.GUID = System.Guid.NewGuid()
        End If
        Project.BuildFiles = BF
        Project.SaveProject(Project.LoadLocation) 'lots of saving and loading, but this will work.
        Me.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.Close()
    End Sub

    Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel_Button.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub

    Private Sub SettingsPropertyGrid_SelectedGridItemChanged(ByVal sender As System.Object, ByVal e As System.Windows.Forms.SelectedGridItemChangedEventArgs) Handles SettingsPropertyGrid.SelectedGridItemChanged
        SetDescriptionWindowSize(7)
    End Sub
End Class
