Imports winAPI.API
Imports System.Runtime.InteropServices
#Const IncludeWeb = 2 'set to 1 to enable web

Public Class ObjSpy
    Private Property description() As APIControls.IDescription
        Get
            Return InternalDescription
        End Get
        Set(ByVal value As APIControls.IDescription)
            If (Not value Is Nothing) Then
                If (InternalDescription Is Nothing) Then InternalDescription = New APIControls.Description()
                For Each item As APIControls.Description.DescriptionData In [Enum].GetValues(GetType(APIControls.Description.DescriptionData))
                    If (value.Contains(item) = True) Then
                        InternalDescription.Add(item, value.GetItemValue(item))
                    End If
                Next
            End If
        End Set
    End Property

    Public Event HidingForm()

    Private InternalDescription As APIControls.IDescription

    Public descriptions As System.Collections.Generic.List(Of APIControls.Description)
    Friend WithEvents Keys As HandleInput.MouseAndKeys
    Private RedHighlight As LayerForm
    Private PerformScanWindow As Boolean = False
    Private Shared WindowsFunctions As New APIControls.IndependentWindowsFunctionsv1()
    Private xExtern, yExtern As Integer
    Private MainHwnd As IntPtr
    Private Completed As Boolean
    Private Shared GlobalCursor As New GlobalMouseCursor()
    Private Shared CursorPath As String
    Private CodeView As CodeViewer
    Private highlightSpiedItem As UIControls.RedHighlight = Nothing

    Public Sub New()
        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        RedHighlight = New LayerForm(Me.Handle)
        MouseTimer.Stop()
        CursorPath = System.IO.Path.GetDirectoryName(System.Windows.Forms.Application.ExecutablePath) & "\"
        CursorPath += "harrow.cur"
        Me.ShowInTaskbar = False
        Me.ShowInTaskbar = True
        ' Add any initialization after the InitializeComponent() call.
    End Sub

    Sub BuildWindowTree(ByVal hChild As IntPtr)
        If (hChild.Equals(IntPtr.Zero)) Then
            'Return Strings
            Return
        End If
        Dim desc As APIControls.Description = WindowsFunctions.CreateDescriptionFromHwnd(hChild)
        descriptions.Add(desc)
        BuildWindowTree(GetParent(hChild))
    End Sub

#Region "Web Stuff"

#If IncludeWeb = 1 Then

    Sub BuildWebTree(ByVal MainWindow As IntPtr, ByVal x As Integer, ByVal y As Integer) ', ByVal Strings As String) ' As String
        If (MainWindow.Equals(IntPtr.Zero)) Then
            'Return Strings
            Return
        End If
        Try
            'If (RedHighlight.IE IsNot Nothing) Then
            '    IE = RedHighlight.IE
            'Else
            'Dim ElemRec As System.Drawing.Rectangle = IE.GetElementLocation(x, y)
            'End If
            Dim element As APIControls.Element = GetIEElement(MainWindow, x, y)
            Dim ieDesc As APIControls.Description = WindowsFunctions.CreateDescriptionFromHwnd(MainWindow)
            BuildWebTreeRecursion(element)
            descriptions.Add(ieDesc)
        Catch ex As Exception
        End Try
    End Sub

    Private Function GetIEElement(ByRef MainWindow As IntPtr, ByVal x As Integer, ByVal y As Integer) As APIControls.Element
        Dim IE As New APIControls.InternetExplorer()
        If (WindowsFunctions.IsWebPartIE(MainWindow)) Then
            IE.TakeOverIESearch(MainWindow)
            Return IE.GetElement(x, y)
        End If
        Return Nothing
    End Function

    Private Sub BuildWebTreeRecursion(ByVal elem As APIControls.Element)
        If (elem Is Nothing) Then Return
        Dim desc As APIControls.Description = GetWebDescription(elem)
        Dim SubElem As APIControls.Element = Nothing
        Try
            SubElem = elem.GetParent()
        Catch ex As Exception
            SubElem = Nothing
        End Try
        If (SubElem Is Nothing) Then Return
        descriptions.Add(desc)
        BuildWebTreeRecursion(SubElem)
    End Sub

    Private Function GetWebDescription(ByVal e As APIControls.Element) As APIControls.Description
        Dim desc As New APIControls.Description()
        Try
            desc.Add(APIControls.Description.DescriptionData.Width, e.Width.ToString())
            desc.Add(APIControls.Description.DescriptionData.Height, e.Height.ToString())
            desc.Add(APIControls.Description.DescriptionData.Top, e.Top.ToString())
            desc.Add(APIControls.Description.DescriptionData.Right, e.Right.ToString())
            desc.Add(APIControls.Description.DescriptionData.Left, e.Left.ToString())
            desc.Add(APIControls.Description.DescriptionData.Bottom, e.Bottom.ToString())
            desc.Add(APIControls.Description.DescriptionData.WebTag, e.TagName)
            desc.Add(APIControls.Description.DescriptionData.WebText, e.Text)
            desc.Add(APIControls.Description.DescriptionData.WebTitle, e.Title)
            desc.Add(APIControls.Description.DescriptionData.WebInnerHTML, e.InnerHtml)
            desc.Add(APIControls.Description.DescriptionData.WebOuterHTML, e.OuterHtml)
        Catch ex As Exception
        End Try
        Try
            desc.Add(APIControls.Description.DescriptionData.WebID, e.Id.ToString())
        Catch ex As Exception
            'likely to fail
        End Try
        Try
            desc.Add(APIControls.Description.DescriptionData.Name, e.Attribute("Name"))
        Catch ex As Exception
            'likely to fail
        End Try
        Return desc
    End Function

    Private Sub BuildWebTimer_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BuildWebTimer.Tick
        Me.BuildWebTimer.Enabled = False
        BuildWebTree(Me.MainHwnd, Me.xExtern, Me.yExtern)
        UpdateTreeViewForWeb()
        Completed = True
    End Sub

    Private Sub UpdateTreeViewForWeb()
        Dim PreviousHwnd As Int64
        Try
            PreviousHwnd = CType(descriptions(descriptions.Count - 1).Hwnd, Int64)
        Catch ex As Exception
            Return
        End Try
        Dim Parentnode As TreeNode = Nothing
        For i As Integer = descriptions.Count - 1 To 0 Step -1
            Dim wTextStr As String = String.Empty

            If (i = descriptions.Count - 1) Then
                'IE instance.
                wTextStr = descriptions(i).Value
                If (wTextStr Is Nothing) Then wTextStr = String.Empty
                If wTextStr.Length <> 0 Then
                    wTextStr = descriptions(i).Name & " - """ & wTextStr & """"
                Else
                    wTextStr = descriptions(i).Name
                End If
                wTextStr = (wTextStr & " - hWnd: " & descriptions(i).Hwnd.ToString()).Replace(vbNewLine, "")
            Else
                wTextStr = "Tag: <" & descriptions(i).WebTag & "> ID: " & descriptions(i).WebID & _
                " Name: " & descriptions(i).Name & " Value: " & descriptions(i).WebValue
            End If
            Dim str As String = wTextStr
            'This needs to be fixed so that it works the same way record does.

            If (Parentnode Is Nothing) Then
                SpyObjectsTreeView.Nodes.Add(str.Replace(vbCr, ""))
                SpyObjectsTreeView.Nodes(0).Tag = descriptions(i)
                Parentnode = SpyObjectsTreeView.Nodes(0)
            Else
                Parentnode.Nodes.Add(str.Replace(vbCr, ""))
                Parentnode.Nodes(0).Tag = descriptions(i)
                Parentnode = Parentnode.Nodes(0)
            End If
            PreviousHwnd = CType(descriptions(i).Hwnd, Int64)
            'addNodes(TreeView1.Nodes(0), strs, strs.Length - 3)
        Next
        descriptions.Clear()
        SpyObjectsTreeView.Nodes(0).ExpandAll()
        Dim Node As TreeNode
        Node = SpyObjectsTreeView.Nodes(0)
        For i As Integer = 1 To SpyObjectsTreeView.GetNodeCount(True) - 1
            Node = Node.Nodes(0)
        Next
        Dim searchVal As Integer = Node.Text.IndexOf("hWnd: ")
        If (searchVal = -1) Then Return
        Dim hwnd As IntPtr = IntPtr.Zero
        Try
            hwnd = New IntPtr(Convert.ToInt64(Node.Text.Substring(searchVal + "hWnd: ".Length)))
        Catch ex As Exception
            Return
        End Try
        Dim desc As APIControls.Description = WindowsFunctions.CreateDescriptionFromHwnd(hwnd)
        Me.SpyDataViewer.Rows.Clear()
        For Each item As APIControls.Description.DescriptionData In [Enum].GetValues(GetType(APIControls.Description.DescriptionData))
            If (desc.Contains(item) = True) Then
                Dim str1() As String = {desc.GetItemName(item), desc.GetItemValue(item)}
                'If (str1(1) <> "") Then 'a value must be present in order to list it.
                'Not needed due to the use of contains.
                Me.SpyDataViewer.Rows.Add(str1)
                'End If
            End If
        Next
    End Sub


#End If
#End Region

    Private Sub ObjSpy_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        TurnOffAndHide()
        e.Cancel = True
        Try
            Interaction.AppActivate(System.Diagnostics.Process.GetCurrentProcess.Id)
        Catch ex As Exception
        End Try
    End Sub

    Public Sub TurnOffAndHide()
        Try
            GlobalCursor.ResetCursor()
        Catch ex As Exception
        End Try
        Me.HideHighlightSpiedItem()
        Me.SpyObjectsTreeView.Nodes.Clear()
        Me.MouseTimer.Stop()
        Me.SpyClickTimer.Stop()
        Me.NextClickTimer.Stop()
        Me.RedHighlight.Hide()
        Me.ClassNameTextBox.Text = ""
        Me.MouseLocationTextBox.Text = ""
        Me.MouseFollowButton.Checked = False
        CopyDescriptionButton.Enabled = False
        description = New APIControls.Description
        Me.SpyDataViewer.Rows.Clear()
        'Me.Owner.Show()
        RaiseEvent HidingForm()
        If (Me.Visible = False) Then
        Else
            Me.Hide()
        End If
    End Sub

#Region "Win APIs"

    <DllImport("user32", EntryPoint:="GetWindowRect")> _
    Private Shared Function GetWindowRect(ByVal hwnd As IntPtr, _
    ByVal lpRect As RECT) As Integer
    End Function

    <StructLayout(LayoutKind.Sequential)> _
    Public Class RECT
        Public Left As Integer
        Public Top As Integer
        Public Right As Integer
        Public Bottom As Integer
    End Class
#End Region

    Private Sub ScanAndUpdateTreeView()
        descriptions = New System.Collections.Generic.List(Of APIControls.Description)
        SpyObjectsTreeView.Nodes().Clear()
        'Dim str As String = BuildWindowTree(WindowFromPoint(interact.Mouse.GetCurrentX, interact.Mouse.GetCurrentY), "")
        Dim x, y As Integer
        x = UIControls.Mouse.CurrentX
        y = UIControls.Mouse.CurrentY
        Dim WinHwnd As IntPtr = WindowFromPoint(x, y)
#If IncludeWeb = 1 Then
        If (WindowsFunctions.IsWebPartIEHTML(WinHwnd) = True) Then


            'If (WindowsFunctions.GetClassName(WinHwnd) = "Internet Explorer_Server") Then
            Dim cRect As New RECT
            GetWindowRect(WinHwnd, cRect)
            'Dim rec As New System.Drawing.Rectangle()
            'rec = Rectangle.Empty
            'rec.X = cRect.Left
            'rec.Y = cRect.Top
            y = y - (cRect.Top)
            x = x - (cRect.Left)

            xExtern = x
            yExtern = y
            MainHwnd = WinHwnd
            Completed = False
            Me.BuildWebTimer.Enabled = True
            Me.BuildWebTimer.Start()
            'While (Completed = False)
            '    System.Threading.Thread.Sleep(10)
            'End While
            'BuildWebTree(WinHwnd, x, y) 'temp. code

        Else
#End If
        BuildWindowTree(WinHwnd)
        UpdateTreeViewForWindows()
#If IncludeWeb = 1 Then
        End If
#End If

        Me.Activate()
    End Sub

    Private Sub UpdateTreeViewForWindows()
        Dim PreviousHwnd As Int64
        Try
            PreviousHwnd = descriptions(descriptions.Count - 1).Hwnd.ToInt64()
        Catch ex As Exception
            Return
        End Try
        If (Not PreviousHwnd.Equals(0)) Then
            Dim Parentnode As TreeNode = Nothing
            For i As Integer = descriptions.Count - 1 To 0 Step -1
                Dim wTextStr As String = String.Empty
                wTextStr = descriptions(i).Value
                If (wTextStr Is Nothing) Then wTextStr = String.Empty
                If wTextStr.Length <> 0 Then
                    wTextStr = descriptions(i).Name & " - """ & wTextStr & """"
                Else
                    wTextStr = descriptions(i).Name
                End If
                Dim str As String = (wTextStr & " - hWnd: " & descriptions(i).Hwnd.ToString()).Replace(vbNewLine, "")
                'This needs to be fixed so that it works the same way record does.
                If (New IntPtr(PreviousHwnd) <> descriptions(i).Hwnd) Then
                    If (descriptions(i).Hwnd <> IntPtr.Zero) Then
                        If (APIControls.EnumerateWindows.isChildDirectlyConnectedToParent(New IntPtr(PreviousHwnd), descriptions(i).Hwnd) = False) Then
                            SpyObjectsTreeView.Nodes().Clear()
                            Parentnode = Nothing 'TreeView1.Nodes(0)
                        End If
                    End If
                End If
                If (Parentnode Is Nothing) Then
                    SpyObjectsTreeView.Nodes.Add(str.Replace(vbCr, ""))
                    Parentnode = SpyObjectsTreeView.Nodes(0)
                Else
                    If (Not descriptions(i).Hwnd.Equals(IntPtr.Zero)) Then 'no zero int ptrs
                        Parentnode.Nodes.Add(str.Replace(vbCr, ""))
                        Parentnode = Parentnode.Nodes(0)
                    End If
                End If
                PreviousHwnd = descriptions(i).Hwnd.ToInt64()
                Parentnode.Tag = PreviousHwnd
            Next
            descriptions.Clear()
            SpyObjectsTreeView.Nodes(0).ExpandAll()
            Dim Node As TreeNode
            Node = SpyObjectsTreeView.Nodes(0)
            For i As Integer = 1 To SpyObjectsTreeView.GetNodeCount(True) - 1
                Node = Node.Nodes(0)
            Next
            Dim searchVal As Integer = Node.Text.IndexOf("hWnd: ")
            If (searchVal = -1) Then Return
            Dim hwnd As IntPtr = IntPtr.Zero
            Try
                hwnd = New IntPtr(Convert.ToInt64(Node.Text.Substring(searchVal + "hWnd: ".Length)))
            Catch ex As Exception
                Return
            End Try
            Dim desc As APIControls.Description = WindowsFunctions.CreateDescriptionFromHwnd(hwnd)
            Me.SpyDataViewer.Rows.Clear()
            For Each item As APIControls.Description.DescriptionData In [Enum].GetValues(GetType(APIControls.Description.DescriptionData))
                If (desc.Contains(item) = True) Then
                    Dim str1() As String = {desc.GetItemName(item), desc.GetItemValue(item)}
                    'If (str1(1) <> "") Then 'a value must be present in order to list it.
                    'Not needed due to the use of contains.
                    Me.SpyDataViewer.Rows.Add(str1)
                    'End If
                End If
            Next
            Me.description = desc
            Dim values As Dictionary(Of String, String) = DumpDataFromWindows(desc, Me.TreeNodeToHwnd(Me.SpyObjectsTreeView.Nodes(0)))
            For Each item As String In values.Keys
                Dim str() As String = {item, values(item)}
                Me.SpyDataViewer.Rows.Add(str)
            Next

            If (Not Me.SpyDataViewer.SortOrder = SortOrder.None) Then
                If (Me.SpyDataViewer.SortOrder = SortOrder.Ascending) Then
                    Me.SpyDataViewer.Sort(Me.SpyDataViewer.SortedColumn, System.ComponentModel.ListSortDirection.Ascending)
                Else
                    Me.SpyDataViewer.Sort(Me.SpyDataViewer.SortedColumn, System.ComponentModel.ListSortDirection.Descending)
                End If
                Me.SpyDataViewer.Refresh()
            End If
        End If
    End Sub

    Private Sub AddNodes(ByVal Parentnode As TreeNode, ByVal items() As String, ByVal num As Integer)
        If (num >= 0) Then
            Parentnode.Nodes.Add(items(num).Replace(vbCr, ""))
            AddNodes(Parentnode.Nodes(0), items, num - 1)
        End If
    End Sub

    Private Sub MouseTimer_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MouseTimer.Tick
        Dim cX As Integer = UIControls.Mouse.CurrentX
        Dim cY As Integer = UIControls.Mouse.CurrentY
        Dim wTextStr As String
        Me.MouseLocationTextBox.Text = cX & ", " & cY
        Dim tmp As IntPtr = WindowFromPoint(cX, cY)
#If IncludeWeb = 1 Then
        If (Not WindowsFunctions.IsWebPartIEHTML(tmp)) Then
#End If
        wTextStr = WindowsFunctions.GetAllText(tmp)
        If wTextStr.Length <> 0 Then
            wTextStr = WindowsFunctions.GetClassName(tmp) & " - """ & wTextStr & """"
        Else
            wTextStr = WindowsFunctions.GetClassName(tmp)
        End If
        Me.ClassNameTextBox.Text = wTextStr & " - hWnd: " & tmp.ToString()
#If IncludeWeb = 1 Then
        Else
            Dim elem As APIControls.Element = GetIEElement(tmp, cX, cY)
            If (elem Is Nothing) Then
                Me.ClassNameTextBox.Text = ""
                Return
            End If
            Dim desc As APIControls.Description = GetWebDescription(elem)

            wTextStr = "Tag: <" & desc.WebTag & "> ID: " & desc.WebID & _
            " Name: " & desc.Name & " Value: " & desc.WebValue

            Me.ClassNameTextBox.Text = wTextStr
        End If
#End If
    End Sub

    Private Const DotNetClassName As String = "windowsform"

    Private Sub TreeView1_NodeMouseClick(ByVal sender As Object, ByVal e As System.Windows.Forms.TreeNodeMouseClickEventArgs) Handles SpyObjectsTreeView.NodeMouseClick
        If (e.Button = Windows.Forms.MouseButtons.Left) Then
            Me.ClassNameTextBox.Text = e.Node.Text
            'Dim searchVal As Integer = e.Node.Text.IndexOf("hWnd: ")
            'If (searchVal = -1) Then Return
            'Dim hwnd As IntPtr = IntPtr.Zero
            'Try
            '    hwnd = New IntPtr(Convert.ToInt32(e.Node.Text.Substring(searchVal + "hWnd: ".Length)))
            '    If (hwnd = IntPtr.Zero) Then Return
            'Catch ex As Exception
            '    Return
            'End Try
            Dim hwnd As IntPtr = TreeNodeToHwnd(e.Node)
            Dim desc As APIControls.Description
            If (hwnd <> IntPtr.Zero) Then
                desc = WindowsFunctions.CreateDescriptionFromHwnd(hwnd)
            Else
                'Must be web
                desc = DirectCast(e.Node.Tag, APIControls.Description)
            End If
            description = desc

            Me.SpyDataViewer.Rows.Clear()
            For Each item As APIControls.Description.DescriptionData In [Enum].GetValues(GetType(APIControls.Description.DescriptionData))
                If (desc.Contains(item) = True) Then
                    Dim str() As String = {desc.GetItemName(item), desc.GetItemValue(item)}
                    Me.SpyDataViewer.Rows.Add(str)
                End If
            Next
            If (hwnd <> IntPtr.Zero) Then
                Dim values As Dictionary(Of String, String) = DumpDataFromWindows(desc, Me.TreeNodeToHwnd(Me.SpyObjectsTreeView.Nodes(0)))
                For Each item As String In values.Keys
                    Dim str() As String = {item, values(item)}
                    Me.SpyDataViewer.Rows.Add(str)
                Next
            End If
            Me.SpyDataViewer.Sort(Me.SpyDataViewer.Columns(0), System.ComponentModel.ListSortDirection.Ascending)
        Else
            Me.TreeViewContextMenuStrip.Show(Me.SpyObjectsTreeView, e.Location)
            Me.SpyObjectsTreeView.SelectedNode = Me.SpyObjectsTreeView.GetNodeAt(e.Location)
        End If
    End Sub

    Private Sub ClickScannerButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ClickScannerButton.Click
        Me.HideHighlightSpiedItem()
        GlobalCursor.SetCursor(ObjSpy.CursorPath)
        PerformScanWindow = False
        Me.SpyClickTimer.Start()
        Me.NextClickTimer.Start()
    End Sub

    Private Sub DataGridView1_CellClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles SpyDataViewer.CellClick
        If (Not Me.SpyDataViewer.CurrentCell Is Nothing) Then
            Me.ClassNameTextBox.Text = Me.SpyDataViewer.CurrentCell.Value.ToString()
        End If
    End Sub

    Private Sub CopyDescriptionButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CopyDescriptionButton.Click
        RecordHelper.description = Me.description
        Dim str As String = RecordHelper.BuildDescription(RecordHelper.SmartNameBuilder(My.Settings.Recorder_Default__Total__Description__Length))
        If (Not String.IsNullOrEmpty(str)) Then
            If (CodeView Is Nothing OrElse CodeView.IsClosed = True) Then
                CodeView = New CodeViewer(True)
            End If
            CodeView.CodeTextBox.Text = str
            CodeView.ShowDialog(Me)
        End If
    End Sub

    Private Sub ObjSpy_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Me.MinimumSize = Me.Size
        Me.NextClickTimer.Stop()
        Me.RedHighlight.Hide()
        description = New APIControls.Description
        If (Me.HighlightCheckBox.Checked = True) Then
            NextClickTimer.Start()
        End If
    End Sub

#Region "GetChildInfo -- Taken from WinObject and AbstractWinObject"

    ''' <summary>
    ''' Gets the entire list of children objects of the WinObject.  The list
    ''' is not in any particular order.
    ''' </summary>
    ''' <returns>Returns an array of descriptions that are children of
    ''' the current window.</returns>
    ''' <remarks>This converts the list into generic objects, rather than
    ''' description objects for scripting related reasons.</remarks>
    Public Function GetChildDescriptions(ByVal Description As APIControls.Description) As APIControls.Description()
        Dim tmpDescs As System.Collections.Generic.List(Of APIControls.Description)
        tmpDescs = New System.Collections.Generic.List(Of APIControls.Description)
        Dim Hwnd As IntPtr = WindowsFunctions.SearchForObj(Description, IntPtr.Zero)
        If (Hwnd = IntPtr.Zero) Then
            Return tmpDescs.ToArray()
        End If
        Try
            For Each handle As IntPtr In APIControls.EnumerateWindows.GetChildHandles(Hwnd)
                tmpDescs.Add(WindowsFunctions.CreateDescriptionFromHwnd(handle))
            Next
        Catch ex As Exception
            Return tmpDescs.ToArray()
        End Try
        Return tmpDescs.ToArray()
    End Function

#End Region

    Protected Shared Function DoSearch(ByVal strDescriptionName As String, ByVal strDescriptionCollection As System.Collections.Generic.List(Of String), Optional ByVal counter As Integer = 1) As String
        Dim tmpstrDescriptionName As String = strDescriptionName
        Dim TmpName As String = ""
        For i As Integer = 0 To strDescriptionCollection.Count - 2 'excludes the latest add
            If (strDescriptionCollection.Item(i).Equals(tmpstrDescriptionName, StringComparison.OrdinalIgnoreCase)) Then 'names match, get a new name
                'If (strDescriptionName.Contains("VsChannel") = True) Then
                '    strDescriptionName = strDescriptionName
                'End If
                Dim Index As Integer = strDescriptionName.LastIndexOf("_")
                If (Index >= 0) Then
                    If (IsNumeric(strDescriptionName.Substring(Index + 1)) = True) Then
                        strDescriptionName = strDescriptionName.Substring(0, Index)
                    End If
                    TmpName = strDescriptionName & "_" & (counter + 1).ToString()
                    If (TmpName.Contains("[Static]") = True) Then
                        If (TmpName.Equals("[Static]") = False) Then
                            TmpName = TmpName.Replace("[Static]", "Static")
                        End If
                    End If
                    Return DoSearch(TmpName, strDescriptionCollection, counter + 1)
                Else
                    TmpName = strDescriptionName & "_" & (counter + 1).ToString()
                    If (TmpName.Contains("[Static]") = True) Then
                        If (TmpName.Equals("[Static]") = False) Then
                            TmpName = TmpName.Replace("[Static]", "Static")
                        End If
                    End If
                    Return DoSearch(TmpName, strDescriptionCollection, counter + 1)
                End If
            End If
        Next
        'If (strDescriptionName.StartsWith("[") = True) Then
        '    If (strDescriptionName.Equals("[Static]") = False) Then
        '        strDescriptionName = strDescriptionName.Replace("[Static]", "Static")
        '    End If
        'End If
        strDescriptionCollection.Item(strDescriptionCollection.Count - 1) = strDescriptionName
        Return strDescriptionName
    End Function

    Private Sub SpyClickTimer_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SpyClickTimer.Tick
        Me.SpyClickTimer.Stop() 'Provides enough time for the click of the button not to
        'be the recorded click.
        Keys.ObjectSpy = True
    End Sub

    Public Sub ShowApp()
        Me.Show()
        'MouseFollowButton.Checked = True
        'MouseTimer.Start()
        If (Me.HighlightCheckBox.Checked = True) Then
            NextClickTimer.Start()
        End If
    End Sub

    Private Sub NextClickTimer_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles NextClickTimer.Tick
        If (Me.HighlightCheckBox.Checked = True) Then
            RedHighlight.DoDraw()
        End If
    End Sub

    Private Sub ScanWindowButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ScanWindowButton.Click
        Me.HideHighlightSpiedItem()
        PerformScanWindow = True
        Me.SpyClickTimer.Start()
        Me.NextClickTimer.Start()
    End Sub

    Private Sub TreeView1_AfterSelect(ByVal sender As System.Object, ByVal e As System.Windows.Forms.TreeViewEventArgs) Handles SpyObjectsTreeView.AfterSelect
        If (SpyObjectsTreeView.SelectedNode IsNot Nothing) Then
            CopyDescriptionButton.Enabled = True
        Else
            CopyDescriptionButton.Enabled = False
        End If
    End Sub

    Private Sub MouseFollowButton_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MouseFollowButton.CheckedChanged
        Me.HideHighlightSpiedItem()
        If (Me.MouseFollowButton.Checked = False) Then
            'Stop Mouse Follow
            MouseTimer.Stop()
        Else
            'Start Mouse Follow
            MouseTimer.Start()
        End If
    End Sub

    Private Sub HighlightCheckBox_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles HighlightCheckBox.CheckedChanged
        Me.HideHighlightSpiedItem()
        If (Me.NextClickTimer.Enabled = False) Then
            If (Me.HighlightCheckBox.Checked = True) Then
                Me.NextClickTimer.Start()
                If (Me.RedHighlight IsNot Nothing) Then
                    Me.RedHighlight.Location = New System.Drawing.Point(1, 1)
                    Me.RedHighlight.Size = New System.Drawing.Size(1, 1)
                    Me.RedHighlight.Visible = True
                End If
            Else
                Me.NextClickTimer.Stop()
                If (Me.RedHighlight IsNot Nothing) Then
                    Me.RedHighlight.Visible = False
                End If
            End If
        Else
            If (HighlightCheckBox.Checked = False) Then
                Me.TopMost = False
                If (Me.RedHighlight IsNot Nothing) Then
                    Me.RedHighlight.Visible = False
                End If
            Else
                Me.TopMost = True
            End If
        End If
    End Sub

#Region "Object spy and object collection"

    Private Class Keys_ObjSpyState
        Public x As Integer
        Public y As Integer
        Public AltState As Boolean
        Public Sub New(ByVal x As Integer, ByVal y As Integer, ByVal AltState As Boolean)
            Me.x = x
            Me.y = y
            Me.AltState = AltState
        End Sub
    End Class
    Private KeysObjSpyState As Keys_ObjSpyState
    Private Sub Keys_ObjSpy(ByVal x As Integer, ByVal y As Integer, ByVal AltState As Boolean) Handles Keys.ObjSpy
        KeysObjSpyState = New Keys_ObjSpyState(x, y, AltState)
        Me.PerformScanAndUpdateAfterEventTimer.Enabled = True
        Me.PerformScanAndUpdateAfterEventTimer.Start()
        'Me.Cursor = Cursors.Default
        GlobalCursor.ResetCursor()
    End Sub

    Private Sub PerformScanAndUpdateAfterEventTimer_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PerformScanAndUpdateAfterEventTimer.Tick
        Me.PerformScanAndUpdateAfterEventTimer.Stop()
        Me.PerformScanAndUpdateAfterEventTimer.Enabled = False
        Dim x, y As Integer
        Dim AltState As Boolean = KeysObjSpyState.AltState 'ugly, but acceptable.
        x = KeysObjSpyState.x
        y = KeysObjSpyState.y
        'NextClickTimer.Stop()
        'Me.RedHighlight.Hide()
        If (PerformScanWindow = False) Then
            ScanAndUpdateTreeView() 'object spy and update ui
        Else 'records descriptions dynamically.
            descriptions = New System.Collections.Generic.List(Of APIControls.Description)
            Dim hwnd As IntPtr = WindowFromPoint(x, y)
            If (WindowsFunctions.IsWebPartIEHTML(New IntPtr(hwnd.ToInt64()))) Then
                MessageBox.Show("IE is unsupported by this feature.  ", SlickTestDev.MsgBoxTitle, _
                                MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If

            '//////////////////////////////////////////////////////////////////////////////////
            If (AltState = True) Then
                BuildWindowTree(hwnd)
                Dim PreviousHwnd As Int64 = descriptions(descriptions.Count - 1).Hwnd.ToInt64()
                hwnd = New IntPtr(PreviousHwnd)
                For i As Integer = descriptions.Count - 1 To 0 Step -1

                    'This needs to be fixed so that it works the same way record does.
                    If (New IntPtr(PreviousHwnd) <> descriptions(i).Hwnd) Then
                        If (APIControls.EnumerateWindows.isChildDirectlyConnectedToParent(New IntPtr(PreviousHwnd), descriptions(i).Hwnd) = False) Then
                            If (descriptions.Count >= i + 1) Then
                                hwnd = descriptions(i + 1).Hwnd
                                Exit For
                            End If
                        End If
                    End If
                    PreviousHwnd = descriptions(i).Hwnd.ToInt64()
                Next
            End If
            descriptions.Clear()

            '//////////////////////////////////////////////////////////////////////////////////
            Dim desc As New APIControls.Description()

            'desc = InterAction.Windows.CreateDescription(hwnd, hwnd) 'this doesn't work so well...
            'desc.Add("hwnd", hwnd.ToString())
            desc = WindowsFunctions.CreateDescriptionFromHwnd(hwnd)

            descriptions.Add(desc)
            descriptions.AddRange(GetChildDescriptions(descriptions.Item(0))) 'not sure this will work.



            Dim str As New System.Text.StringBuilder((descriptions.Count * 120) + 1) 'Give a guess at the string size.
            Dim Names As New System.Collections.Generic.List(Of String)

            'include the tree up, but not searching everywhere.
            If (descriptions.Count = 1) Then
                descriptions.Clear()
                BuildWindowTree(desc.Hwnd)
                descriptions.Reverse()
                For i As Integer = 0 To descriptions.Count - 1
                    desc = descriptions.Item(i)
                    If (i <> 0) Then 'Produces a filtered description.
                        desc = WindowsFunctions.CreateDescription(hwnd, desc.Hwnd)
                    Else 'Produces a filtered description.
                        hwnd = desc.Hwnd 'set to top window
                        desc = WindowsFunctions.CreateDescription(desc.Hwnd, IntPtr.Zero) 'for main window
                    End If
                    Dim DescStrLocation As String = ""
                    RecordHelper.description = desc

                    Names.Add(RecordHelper.SmartNameBuilder(My.Settings.Recorder_Default__Total__Description__Length))
                    str.Append( _
                    RecordHelper.BuildDescription( _
                    DoSearch(Names.Item(Names.Count - 1), Names)) & vbNewLine)
                Next

            Else
                For i As Integer = 0 To descriptions.Count - 1
                    desc = descriptions.Item(i)
                    If (i <> 0) Then 'Produces a filtered description.
                        desc = WindowsFunctions.CreateDescription(hwnd, desc.Hwnd)
                    Else 'Produces a filtered description.
                        desc = WindowsFunctions.CreateDescription(hwnd, IntPtr.Zero) 'for main window
                    End If
                    Dim DescStrLocation As String = ""
                    RecordHelper.description = desc

                    Names.Add(RecordHelper.SmartNameBuilder(40)) 'A little longer than average, but more helpful names... I hope.
                    str.Append( _
                    RecordHelper.BuildDescription( _
                    DoSearch(Names.Item(Names.Count - 1), Names)) & vbNewLine)
                Next
            End If
            If (str.Length <> 0) Then
                If (CodeView Is Nothing OrElse CodeView.IsClosed = True) Then
                    CodeView = New CodeViewer(True)
                End If
                CodeView.CodeTextBox.Text = str.ToString()
                CodeView.ShowDialog(Me)
            End If
        End If
    End Sub
#End Region

#Region "Global Cursor"
    Private Class GlobalMouseCursor

        Private Declare Auto Function LoadCursor Lib "user32.dll" (ByVal hInst As IntPtr, ByVal iconId As Integer) As IntPtr
        Private Declare Auto Function LoadCursorFromFile Lib "user32.dll" (ByVal lpFileName As String) As IntPtr
        Private Declare Function SetSystemCursor Lib "user32.dll" (ByVal hCur As IntPtr, ByVal id As Integer) As Boolean
        Private Const IDC_APPSTARTING As Integer = 32650
        Private Const IDC_ARROW As Integer = 32512
        Private Const IDC_CROSS As Integer = 32515
        Private Const IDC_HELP As Integer = 32651
        Private Const IDC_IBEAM As Integer = 32513
        Private Const IDC_ICON As Integer = 32641
        Private Const IDC_NO As Integer = 32648
        Private Const IDC_SIZE As Integer = 32640
        Private Const IDC_SIZEALL As Integer = 32646
        Private Const IDC_SIZENESW As Integer = 32643
        Private Const IDC_SIZENS As Integer = 32645
        Private Const IDC_SIZENWSE As Integer = 32642
        Private Const IDC_SIZEWE As Integer = 32644
        Private Const IDC_UPARROW As Integer = 32516
        Private Const IDC_WAIT As Integer = 32514
        Private OCR_NORMAL As Integer = 32512
        Private oldCursor As IntPtr = IntPtr.Zero
        Private newCursor As IntPtr = IntPtr.Zero
        Private cursorSet As Boolean = False

        Public Sub New()
        End Sub

        Public Sub SetCursor(ByVal fileName As String)
            If Me.cursorSet Then Me.ResetCursor()
            Me.oldCursor = New Cursor(GlobalMouseCursor.LoadCursor(Nothing, GlobalMouseCursor.IDC_ARROW)).CopyHandle
            Me.newCursor = GlobalMouseCursor.LoadCursorFromFile(fileName)
            If Not Me.newCursor.Equals(IntPtr.Zero) Then
                GlobalMouseCursor.SetSystemCursor(Me.newCursor, Me.OCR_NORMAL)
            End If
            Me.cursorSet = True
        End Sub

        Public Sub ResetCursor()
            If Not Me.oldCursor.Equals(IntPtr.Zero) Then GlobalMouseCursor.SetSystemCursor(Me.oldCursor, Me.OCR_NORMAL)
        End Sub

    End Class

#End Region

    Private Sub HideHighlightSpiedItem()
        If (Not (highlightSpiedItem Is Nothing)) Then highlightSpiedItem.Hide()
    End Sub

    Private Function TreeNodeToHwnd(ByVal node As TreeNode) As IntPtr
        If (TypeOf node.Tag Is UIControls.Description OrElse TypeOf node.Tag Is APIControls.Description) Then
            Return IntPtr.Zero
        End If
        Return New IntPtr(Convert.ToInt64(node.Tag))
    End Function

    Private Sub HighlightToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles HighlightToolStripMenuItem.Click
        Dim hwnd As IntPtr = TreeNodeToHwnd(Me.SpyObjectsTreeView.SelectedNode)
        Try
            WindowsFunctions.AppActivateByHwnd(hwnd)
            HideHighlightSpiedItem()
        Catch ex As Exception
            System.Windows.Forms.MessageBox.Show("Unable to highlight window because: " & ex.Message, SlickTestDev.MsgBoxTitle)
            Return
        End Try
        highlightSpiedItem = New UIControls.RedHighlight(hwnd)
        highlightSpiedItem.Show()
        highlightSpiedItem.DoDraw()
    End Sub

    Private Sub DumpWindowToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DumpWindowToolStripMenuItem.Click
        Dim windowResults As String
        Try
            Dim hwnd As IntPtr = TreeNodeToHwnd(Me.SpyObjectsTreeView.SelectedNode)
            Dim windowsDescription As UIControls.Description = UIControls.Description.Create()
            windowsDescription.Add(UIControls.Description.DescriptionData.Hwnd, hwnd.ToString())
            Dim w As New UIControls.Window(windowsDescription)
            w.reporter = New UIControls.StubbedReport()
            windowResults = w.DumpWindowData()
        Catch ex As Exception
            windowResults = ex.Message & vbNewLine & vbNewLine & "Details: " & vbNewLine & ex.ToString()
        End Try
        If (windowResults.Length <> 0) Then
            If (CodeView Is Nothing OrElse CodeView.IsClosed = True) Then
                CodeView = New CodeViewer(True)
            End If
            CodeView.CodeTextBox.Text = windowResults
            CodeView.ShowDialog(Me)
        End If
    End Sub

#Region "Dump Window Data (Duplication :()"
    Public Function DumpDataFromWindows(ByVal desc As APIControls.IDescription, ByVal TopHwnd As IntPtr) As Dictionary(Of String, String)
        Dim valueToAddToList As New Dictionary(Of String, String)
        Dim w As UIControls.AbstractWinObject = New UIControls.WinObject(desc)
        If (w Is Nothing) Then Return valueToAddToList
        w.reporter = New UIControls.StubbedReport
        Try

            valueToAddToList.Add("Style", w.GetStyle())
            valueToAddToList.Add("StyleEx", w.GetStyleEx())
            Dim window As UIControls.Window

            Dim UIType As String = w.GetObjectType().ToLowerInvariant().Replace("swf", "")
            If (UIType.Contains("window") OrElse TopHwnd = IntPtr.Zero) Then
                window = New UIControls.Window(desc) 'since w is a window, then just reuse the description.
            Else
                Dim parentHwnd As IntPtr = TopHwnd

                Dim winDescription As UIControls.Description = UIControls.Description.Create()
                winDescription.Add(APIControls.Description.DescriptionData.Hwnd, parentHwnd.ToString())
                window = New UIControls.Window(winDescription)
                window.reporter = New UIControls.StubbedReport
            End If
            UIControls.AbstractWinObject.ExistTimeout = 2
            Dim UIDesc As UIControls.Description
            Dim tmphwnd As IntPtr = desc.Hwnd
            desc.Reset()
            desc.Add(APIControls.Description.DescriptionData.Hwnd, tmphwnd.ToString())
            If (TypeOf desc Is APIControls.Description) Then
                UIDesc = UIControls.Description.ConvertApiToUiDescription(desc)
            Else
                UIDesc = desc
            End If

            Select Case UIType
                Case "listbox"
                    Dim LstBox As UIControls.ListBox = window.ListBox(UIDesc)
                    valueToAddToList.Add("ItemCount", LstBox.GetItemCount())
                    valueToAddToList.Add("SelectCount", LstBox.GetSelectCount())
                    valueToAddToList.Add("SelectedItemNumber", LstBox.GetSelectedItemNumber())
                    valueToAddToList.Add("IsEnabled", LstBox.IsEnabled())
                    valueToAddToList.Add("StyleInfo", UIControls.StyleInfo.ListBoxStyle.ValueInfo(LstBox.GetStyle()))

                Case "listview"
                    Dim LstView As UIControls.ListView = window.ListView(UIDesc)
                    valueToAddToList.Add("ColumnCount", LstView.GetColumnCount())
                    If (LstView.GetColumnCount() >= 1) Then
                        valueToAddToList.Add("Values", LstView.GetAllFormatted())
                    End If
                    valueToAddToList.Add("RowCount", LstView.GetRowCount())
                    valueToAddToList.Add("IsEnabled", LstView.IsEnabled())
                    valueToAddToList.Add("StyleInfo", UIControls.StyleInfo.ListViewStyle.ValueInfo(LstView.GetStyle()))

                Case "combobox"
                    Dim CmbBox As UIControls.ComboBox = window.ComboBox(UIDesc)
                    valueToAddToList.Add("ItemCount", CmbBox.GetItemCount())
                    valueToAddToList.Add("SelectedItemNumber", CmbBox.GetSelectedItemNumber())
                    valueToAddToList.Add("IsEnabled", CmbBox.IsEnabled())
                    valueToAddToList.Add("StyleInfo", UIControls.StyleInfo.ComboBoxStyle.ValueInfo(CmbBox.GetStyle()))

                Case "textbox"
                    Dim TxtBox As UIControls.TextBox = window.TextBox(UIDesc)
                    valueToAddToList.Add("CurrentLineNumber", TxtBox.GetCurrentLineNumber())
                    valueToAddToList.Add("IndexFromCaret", TxtBox.GetIndexFromCaret())
                    valueToAddToList.Add("LineCount", TxtBox.GetLineCount())
                    valueToAddToList.Add("IsEnabled", TxtBox.IsEnabled())
                    valueToAddToList.Add("StyleInfo", UIControls.StyleInfo.TextBoxStyle.ValueInfo(TxtBox.GetStyle()))

                Case "button"
                    'Nothing special
                    Dim Buton As UIControls.Button = window.Button(UIDesc)
                    valueToAddToList.Add("IsEnabled", Buton.IsEnabled())
                    valueToAddToList.Add("StyleInfo", UIControls.StyleInfo.ButtonStyle.ValueInfo(Buton.GetStyle()))

                Case "staticlabel"
                    'Nothing special
                    Dim StcLabel As UIControls.StaticLabel = window.StaticLabel(UIDesc)
                    valueToAddToList.Add("IsEnabled", StcLabel.IsEnabled())

                Case "checkbox"
                    Dim ChkBox As UIControls.CheckBox = window.CheckBox(UIDesc)
                    valueToAddToList.Add("Is3State", ChkBox.Is3State())
                    valueToAddToList.Add("Is3StateAuto", ChkBox.Is3StateAuto())
                    valueToAddToList.Add("IsButton", ChkBox.IsButton())
                    valueToAddToList.Add("IsCheckBox", ChkBox.IsCheckBox())
                    valueToAddToList.Add("IsEnabled", ChkBox.IsEnabled())
                    valueToAddToList.Add("Checked", ChkBox.GetCheckedString())
                    valueToAddToList.Add("StyleInfo", UIControls.StyleInfo.CheckBoxStyle.ValueInfo(ChkBox.GetStyle()))

                Case "radiobutton"
                    Dim RadButton As UIControls.RadioButton = window.RadioButton(UIDesc)
                    valueToAddToList.Add("IsEnabled", RadButton.IsEnabled())
                    valueToAddToList.Add("IsButton", RadButton.IsButton())
                    valueToAddToList.Add("IsRadioButton", RadButton.IsRadioButton())
                    valueToAddToList.Add("IsSelected", RadButton.GetSelected())
                    valueToAddToList.Add("StyleInfo", UIControls.StyleInfo.RadioButtonStyle.ValueInfo(RadButton.GetStyle()))

                Case "treeview"
                    Dim TreView As UIControls.TreeView = window.TreeView(UIDesc)
                    valueToAddToList.Add("IsEnabled", TreView.IsEnabled())
                    valueToAddToList.Add("SelectedText", TreView.GetSelectedText())
                    valueToAddToList.Add("Count", TreView.GetItemCount())
                    valueToAddToList.Add("StyleInfo", UIControls.StyleInfo.TreeViewStyle.ValueInfo(TreView.GetStyle()))

                Case "tabcontrol"
                    Dim TbCtrl As UIControls.TabControl = window.TabControl(UIDesc)
                    valueToAddToList.Add("IsEnabled", TbCtrl.IsEnabled())
                    valueToAddToList.Add("SelectedTabIndex", TbCtrl.GetSelectedTab())
                    valueToAddToList.Add("TabCount", TbCtrl.GetTabCount())
                    valueToAddToList.Add("StyleInfo", UIControls.StyleInfo.TabStyle.ValueInfo(TbCtrl.GetStyle()))

                Case "window"
                    Dim win As UIControls.Window

                    If (window.Hwnd = desc.Hwnd) Then
                        win = window
                    Else
                        win = window.Window(UIDesc)
                    End If
                    If (win.GetName().ToLowerInvariant.IndexOf("windowsform") = -1) Then
                        valueToAddToList.Add("HasBorder", win.HasBorder())
                        valueToAddToList.Add("HasMaximizeButton", win.HasMaximizeButton())
                        valueToAddToList.Add("HasMinimizeButton", win.HasMinimizeButton())
                        valueToAddToList.Add("HasTitleBar", win.HasTitleBar())
                        'valueToAddToList.Add("NumberOfSameWindows", win.GetObjectCount())
                        valueToAddToList.Add("HasMenu", win.Menu.ContainsMenu())
                        If (win.Menu.ContainsMenu()) Then
                            valueToAddToList.Add("TopLevelMenuCount", win.Menu.GetTopLevelMenuCount())
                            valueToAddToList.Add("TopLevelMenuText", win.Menu.GetTopLevelMenuText())
                            valueToAddToList.Add("MenuCount", win.Menu.GetMenuCount())
                            valueToAddToList.Add("AllMenuText", win.Menu.GetText())
                        End If
                    End If

                    valueToAddToList.Add("WindowState", win.GetWindowState().ToString())
                Case Else
                    valueToAddToList.Add("IsEnabled", w.IsEnabled())
            End Select
            valueToAddToList.Add("StyleInfoGeneric", UIControls.InterAct.StyleInfo.ValueInfo(w.GetStyle()))
            valueToAddToList.Add("StyleExInfoGeneric", UIControls.InterAct.StyleExtendedInfo.ValueInfo(w.GetStyleEx()))
            valueToAddToList.Add("ClientAreaRect", w.GetClientAreaRect().ToString())

            If (Not String.IsNullOrEmpty(w.Name)) Then
                Dim tmpClassName As String = w.Name
                If (tmpClassName.ToLowerInvariant().IndexOf(DotNetClassName) <> -1) Then
                    Dim str() As String = {"InternalName", tmpClassName}
                    If (Not String.IsNullOrEmpty(str(1))) Then
                        Me.SpyDataViewer.Rows.Add(str)
                    End If
                End If
            End If
        Catch ex As Exception
            MessageBox.Show("Failed to get some description details." & vbNewLine & vbNewLine & "Error Details: " & ex.ToString(), SlickTestDev.MsgBoxTitle, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
        Return valueToAddToList
    End Function
#End Region

    Private Sub UnhighlightToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles UnhighlightToolStripMenuItem.Click
        If (highlightSpiedItem IsNot Nothing) Then Me.highlightSpiedItem.Hide()
    End Sub
End Class


Public Class LayerForm
    Inherits UIControls.RedHighlight

#Region " API DECLARATIONS "
    '<DllImport("user32", EntryPoint:="WindowFromPoint")> _
    'Private Shared Function WindowFromPoint(ByVal xPoint As Integer, _
    'ByVal yPoint As Integer) As IntPtr
    'End Function
    <DllImport("user32", EntryPoint:="GetForegroundWindow")> _
    Private Shared Function GetForegroundWindow() As IntPtr
    End Function
    <DllImport("user32", EntryPoint:="GetAncestor")> _
    Private Shared Function GetAncestor(ByVal hwnd As IntPtr, _
    ByVal gaFlags As Integer) As IntPtr
    End Function

#Region " CONSTANTS "
    Const GA_PARENT As Integer = 1
    Const GA_ROOT As Integer = 2
    Const GA_ROOTOWNER As Integer = 3
#End Region


#End Region

    Dim prec As Rectangle
    Private Shared WindowsFunctions As New APIControls.IndependentWindowsFunctionsv1()
#If (IncludeWeb = 1) Then
    Friend IE As APIControls.InternetExplorer
#End If
    Private Element As APIControls.Element


    Public Sub New(ByVal SpyHandle As IntPtr)
        MyBase.New(SpyHandle)
        prec = New Rectangle(0, 0, 0, 0)
        Me.Show()
    End Sub

    Private Sub LayerForm_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Me.Paint
        Dim p As New Pen(Color.Red, 3)
        e.Graphics.DrawRectangle(p, 0, 0, Me.Width, Me.Height)
        'System.Console.WriteLine("* At draw-- Height: " & Me.Height & " Width: " & Me.Width)
        p.Dispose()
    End Sub

    Protected Overrides Sub DoDraw()
        Me.Visible = True
        Dim hwnd As IntPtr = IntPtr.Zero
        Dim TopHwnd As IntPtr = IntPtr.Zero

        hwnd = WindowFromPoint(Control.MousePosition.X, Control.MousePosition.Y)

        TopHwnd = GetAncestor(hwnd, GA_ROOT)
        If TopHwnd = SpyHandle Or TopHwnd = Me.Handle Or TopHwnd = IntPtr.Zero Then
            'System.Console.WriteLine("Rect is bad...")
            prec = rec
            'rec = Rectangle.Empty
            'Me.Size = New Size(0, 0)
            Me.Refresh()
            Return
        End If
        BringWindowToTop(Me.Handle)
        rec = GetRectangle(hwnd)

#If IncludeWeb = 1 Then

        'If (WindowsFunctions.GetClassName(hwnd) = "Internet Explorer_Server") Then
        If (WindowsFunctions.IsWebPartIEHTML(hwnd)) Then
            Dim point As System.Drawing.Point = Windows.Forms.Cursor.Position
            Dim x, y As Int32
            x = point.X
            y = point.Y
            y = y - (rec.Top + 2)
            x = x - (rec.Left + 2)
            IE = New APIControls.InternetExplorer()
            Dim IEHwnd As IntPtr = hwnd
            If (WindowsFunctions.IsWebPartIE(IEHwnd) = True) Then
                If (IE.TakeOverIESearch(IEHwnd) = True) Then
                    Dim ElemRec As System.Drawing.Rectangle = IE.GetElementLocation(x, y)
                    rec = ElemRec
                    'System.Console.WriteLine("ObjectSpy says element location: " & rec.X & ", " & rec.Y & "; Width: " & rec.Width & " Height: " & rec.Height)
                    'rec = New Rectangle(ElemRec.X + rec.X, _
                    '                    ElemRec.Y + rec.Y, ElemRec.Width, _
                    '                    ElemRec.Height)
                    '                    System.Console.WriteLine(rec.Location)
                End If
            End If
        End If
#End If
        If prec <> rec Then
            Me.SetPlacement(rec)
        End If
        prec = rec
    End Sub

End Class