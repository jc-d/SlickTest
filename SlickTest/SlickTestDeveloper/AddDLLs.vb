Imports System.Windows.Forms

Public Class AddDLLs

    Private FilesToAdd As New System.Collections.Generic.List(Of String)()
    Private ExcludeList As New System.Collections.Generic.List(Of String)()
    Private ExcludeListDLLInfo As New System.Collections.Generic.List(Of String)()
    Private lvwColumnSorter As ListViewColumnSorter
    Private Shared KillProgram As Boolean = False
    Private Shared ItemsToAdd As New System.Collections.ArrayList()


    Private DisplayItemToFilePath As New System.Collections.Specialized.StringDictionary()

    Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK_Button.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.Close()
    End Sub

    Private Sub AddDLLs_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        KillProgram = True
        Me.BackgroundWorker1.CancelAsync()
        GC.Collect()
    End Sub

    Private Sub AddDLLs_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        lvwColumnSorter = New ListViewColumnSorter()
        Me.AsmListView.ListViewItemSorter = lvwColumnSorter
        Me.AsmListView.Hide()
        For Each file As String In Me.ExcludeList
            Dim asmInfo As System.Reflection.AssemblyName
            Try
                asmInfo = System.Reflection.AssemblyName.GetAssemblyName(file)
                Me.ExcludeListDLLInfo.Add(asmInfo.Name & " - " & asmInfo.Version.ToString & " - " & asmInfo.CultureInfo.DisplayName)
            Catch ex As Exception
            End Try
        Next
        Me.BackgroundWorker1.RunWorkerAsync()
        Me.AsmListView.Columns(0).Width = 331
        Me.AsmListView.Columns(1).Width = 105
        Me.AsmListView.Columns(2).Width = 88
        Me.AsmListView.Columns(3).Width = 280
        'AsmListBox.Sorted = True
    End Sub

    ' This delegate enables asynchronous calls for setting
    ' the text property on a TextBox control.
    Delegate Sub SetTextCallback(ByVal [text] As String)

    Private Sub SetText(ByVal [text] As String)

        ' InvokeRequired required compares the thread ID of the
        ' calling thread to the thread ID of the creating thread.
        ' If these threads are different, it returns true.
        Try
            If Me.Label1.InvokeRequired Then
                Dim d As New SetTextCallback(AddressOf SetText)
                Me.Invoke(d, New Object() {[text]})
            Else
                Me.Label1.Text = [text]
            End If
        Catch ex As Exception
        End Try

    End Sub

    Private Shared counter As Integer = 0

    Private Sub SearchFiles(ByVal AsmPath As String)
        For Each path As String In System.IO.Directory.GetDirectories(AsmPath)
            SearchFiles(path)
            counter = counter + 1
            If ((counter Mod 60) = 0) Then
                SetText(Me.Label1.Text.Replace("...", "") + ".")
            End If
        Next
        Dim strInfo As String
        For Each file As String In System.IO.Directory.GetFiles(AsmPath)
            If (KillProgram = True) Then Return

            counter = counter + 1
            If ((counter Mod 60) = 0) Then
                SetText(Me.Label1.Text.Replace("...", "") + ".")
            End If

            If (file.ToLower.EndsWith(".dll") = True) Then
                file = file.Replace("/", "\")
                If (DisplayItemToFilePath.ContainsValue(file) = False) Then
                    If (ExcludeList.Contains(file) = False) Then
                        Dim asmInfo As System.Reflection.AssemblyName
                        Try
                            asmInfo = System.Reflection.AssemblyName.GetAssemblyName(file)
                            strInfo = asmInfo.Name & " - " & asmInfo.Version.ToString & " - " & asmInfo.CultureInfo.DisplayName
                            If (Me.ExcludeListDLLInfo.Contains(strInfo) = False) Then
                                DisplayItemToFilePath.Add(strInfo, file)
                                Dim Items() As String = {asmInfo.Name, asmInfo.Version.ToString(), asmInfo.CultureInfo.DisplayName, file}
                                ItemsToAdd.Add(Items)
                            End If
                        Catch ex As Exception

                        End Try
                    End If
                End If
            End If
        Next
    End Sub

    Public Sub Exclude(ByVal ExcludeList As String())
        Me.ExcludeList.Clear()
        Me.ExcludeList.AddRange(ExcludeList)
    End Sub

    Private Sub AddButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AddButton.Click
        If (Me.AsmListView.SelectedItems.Count = 0) Then Return
        For Each item As System.Windows.Forms.ListViewItem In Me.AsmListView.SelectedItems
            Dim tmp As String = item.SubItems(0).Text & " - " & item.SubItems(1).Text & " - " & item.SubItems(2).Text
            FilesToAdd.Add(DisplayItemToFilePath(tmp))
            Me.AsmListView.Items.Remove(item)
        Next
    End Sub

    Private Sub BrowseButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BrowseButton.Click
        If (Me.OpenFileDialog.ShowDialog() = Windows.Forms.DialogResult.OK) Then
            Dim asmInfo As System.Reflection.AssemblyName
            Try
                asmInfo = System.Reflection.AssemblyName.GetAssemblyName(Me.OpenFileDialog.FileName)
                FilesToAdd.Add(Me.OpenFileDialog.FileName)
            Catch ex As Exception
                System.Windows.Forms.MessageBox.Show("Unable to load: " & Me.OpenFileDialog.FileName & ".  " & _
                "This may note be .net assembly or a COM enabled DLL.")
            End Try
        End If
    End Sub

    Public Function FilesAdded() As String()
        Return FilesToAdd.ToArray()
    End Function

    Private Sub AsmListView_ColumnClick(ByVal sender As Object, ByVal e As System.Windows.Forms.ColumnClickEventArgs) Handles AsmListView.ColumnClick
        ' Determine if the clicked column is already the column that is 
        ' being sorted.
        If (e.Column = lvwColumnSorter.SortColumn) Then
            ' Reverse the current sort direction for this column.
            If (lvwColumnSorter.Order = SortOrder.Ascending) Then
                lvwColumnSorter.Order = SortOrder.Descending
            Else
                lvwColumnSorter.Order = SortOrder.Ascending
            End If
        Else
            ' Set the column number that is to be sorted; default to ascending.
            lvwColumnSorter.SortColumn = e.Column
            lvwColumnSorter.Order = SortOrder.Ascending
        End If

        ' Perform the sort with these new sort options.
        Me.AsmListView.Sort()
    End Sub
#Region "List View"
    Private Class ListViewColumnSorter
        Implements System.Collections.IComparer

        Private ColumnToSort As Integer
        Private OrderOfSort As SortOrder
        Private ObjectCompare As CaseInsensitiveComparer

        Public Sub New()
            ' Initialize the column to '0'.
            ColumnToSort = 0

            ' Initialize the sort order to 'none'.
            OrderOfSort = SortOrder.None

            ' Initialize the CaseInsensitiveComparer object.
            ObjectCompare = New CaseInsensitiveComparer()
        End Sub

        Public Function Compare(ByVal x As Object, ByVal y As Object) As Integer Implements IComparer.Compare
            Dim compareResult As Integer
            Dim listviewX As ListViewItem
            Dim listviewY As ListViewItem

            ' Cast the objects to be compared to ListViewItem objects.
            listviewX = CType(x, ListViewItem)
            listviewY = CType(y, ListViewItem)

            ' Compare the two items.
            compareResult = ObjectCompare.Compare(listviewX.SubItems(ColumnToSort).Text, listviewY.SubItems(ColumnToSort).Text)

            ' Calculate the correct return value based on the object 
            ' comparison.
            If (OrderOfSort = SortOrder.Ascending) Then
                ' Ascending sort is selected, return typical result of 
                ' compare operation.
                Return compareResult
            ElseIf (OrderOfSort = SortOrder.Descending) Then
                ' Descending sort is selected, return negative result of 
                ' compare operation.
                Return (-compareResult)
            Else
                ' Return '0' to indicate that they are equal.
                Return 0
            End If
        End Function

        Public Property SortColumn() As Integer
            Set(ByVal Value As Integer)
                ColumnToSort = Value
            End Set

            Get
                Return ColumnToSort
            End Get
        End Property

        Public Property Order() As SortOrder
            Set(ByVal Value As SortOrder)
                OrderOfSort = Value
            End Set
            Get
                Return OrderOfSort
            End Get
        End Property
    End Class
#End Region
    Private Sub CancelButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CancelButton.Click
        Me.DialogResult = Windows.Forms.DialogResult.Cancel
        FilesToAdd.Clear()
        Me.Close()
    End Sub

    Private Sub BackgroundWorker1_DoWork(ByVal sender As System.Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles BackgroundWorker1.DoWork
        Dim AsmPath As String = Environment.GetEnvironmentVariable("WINDIR")
        If (AsmPath.EndsWith("\") = False) Then AsmPath += "\"
        If (AsmPath.EndsWith("\\") = False) Then AsmPath.Substring(0, AsmPath.Length - 1)
        AsmPath += "assembly\"
        SearchFiles(AsmPath)
    End Sub

    Private Sub BackgroundWorker1_RunWorkerCompleted(ByVal sender As Object, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles BackgroundWorker1.RunWorkerCompleted
        For Each Item() As String In AddDLLs.ItemsToAdd
            AsmListView.Items.Add(New System.Windows.Forms.ListViewItem(Item))
            If ((counter Mod 100) = 0) Then
                SetText(Me.Label1.Text.Replace("...", "") + ".")
            End If
        Next
        lvwColumnSorter.SortColumn = 0
        lvwColumnSorter.Order = SortOrder.Ascending
        Me.AsmListView.Sort()
        Me.AsmListView.Show()
    End Sub

#Region "old code"
    'If (file.ToLower.EndsWith(".ini") = True) Then
    '    Dim FileInfo() As String = System.IO.File.ReadAllLines(file)
    '    Dim DisplayName As String = ""
    '    Dim Version As String = ""
    '    Dim FilePath As String = ""
    '    Dim Culture As String = ""
    '    For Each line As String In FileInfo
    '        If (line.Contains("URL=file:///") = True) Then
    '            FilePath = line.Substring("URL=file:///".Length)
    '        End If
    '        If (line.Contains("DisplayName=") = True) Then
    '            DisplayName = line.Substring(line.IndexOf("DisplayName=") + "DisplayName=".Length)
    '            DisplayName = DisplayName.Substring(0, DisplayName.IndexOf(","))
    '        End If
    '        If (line.Contains("Version=") = True) Then
    '            Version = line.Substring(line.IndexOf("Version=") + "Version=".Length)
    '            Version = Version.Substring(0, Version.IndexOf(","))
    '        End If
    '        If (line.Contains("Culture=") = True) Then
    '            Culture = line.Substring(line.IndexOf("Culture=") + "Culture=".Length)
    '            Culture = Culture.Substring(0, Culture.IndexOf(","))
    '        End If
    '    Next

    '    If (FilePath <> "") Then
    '        FilePath = FilePath.Replace("/", "\")
    '        If (DisplayItemToFilePath.ContainsValue(FilePath) = False) Then
    '            If (ExcludeList.Contains(FilePath) = False) Then
    '                strInfo = DisplayName & " - " & Version & " - " & Culture
    '                Try
    '                    DisplayItemToFilePath.Add(strInfo, FilePath)
    '                    'AsmListView.Items.Add(strInfo)

    '                    Dim Items() As String = {DisplayName, Version, Culture, FilePath}
    '                    AsmListView.Items.Add(New System.Windows.Forms.ListViewItem(Items))

    '                Catch ex As Exception
    '                    'could have multiples with the same signature... ignore em.
    '                End Try
    '            End If
    '        End If
    '    End If
    'End If
#End Region


End Class
