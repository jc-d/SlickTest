Imports winAPI.API
Imports System.Runtime.InteropServices

Imports System.Runtime.CompilerServices
<Assembly: InternalsVisibleTo("InterAction"), Assembly: CLSCompliantAttribute(True), _
Assembly: InternalsVisibleTo("SlickTestDeveloper")> 
Friend Class ProcessWindow
    Friend Shared WindowsFunctions As New APIControls.IndependentWindowsFunctionsv1()

    Private Shared IE As New APIControls.InternetExplorer()
    Public Shared WebX As Integer
    Public Shared WebY As Integer
    Private Shared RetStr As String = String.Empty
    'Public Shared Windowsv1 As New InterAction.Windowsv1()
    Private Const Window As String = "Window("""
    Private Const WinObject As String = "WinObject("""
    Private Const ListBox As String = "ListBox("""
    Private Const ListView As String = "ListView("""
    Private Const TabControl As String = "TabControl("""
    Private Const ComboBox As String = "ComboBox("""
    Private Const TextBox As String = "TextBox("""
    Private Const TreeView As String = "TreeView("""
    Private Const Button As String = "Button("""
    Private Const CheckBox As String = "CheckBox("""
    Private Const RadioButton As String = "RadioButton("""
    Private Const StaticLabel As String = "StaticLabel("""
    Private Const SWF As String = "Swf"
    Public Const Web As String = "Web"
    Public Const WPF As String = "Wpf"

    Public Const WebElement As String = "WebElement("""
    Public Const WebTable As String = "WebTable("""
    Public Const WebTableRow As String = "WebTableRow("""
    Public Const WebTableCell As String = "WebTableCell("""
    Public Const WebSpan As String = "WebSpan("""
    Public Const WebCheckbox As String = "WebCheckBox("""
    Public Const WebButton As String = "WebButton("""
    Public Const WebDiv As String = "WebDiv("""
    Public Const WebLink As String = "WebLink("""
    Public Const WebGenericInput As String = "WebGenericInput("""
    Public Const WebImage As String = "WebImage("""
    Public Const WebList As String = "WebList("""
    Public Const WebTextBox As String = "WebTextBox("""
    Public Const WebComboBox As String = "WebComboBox("""
    Public Const WebRadioButton As String = "WebRadioButton("""



    Public Const IEWebBrower As String = "IEWebBrowser("""
    Public Shared IsWebJustProcessed As Boolean = False
    Private Shared WithEvents IETimer As New System.Timers.Timer()
    Private Shared IETimerHwnd As IntPtr = IntPtr.Zero
    Private Shared WinItems() As String = {Window, WinObject, ListBox, ListView, ComboBox, _
    TextBox, Button, CheckBox, RadioButton, StaticLabel, TabControl, TreeView, IEWebBrower, _
    SWF & Window, SWF & WinObject, SWF & ListBox, SWF & ListView, SWF & ComboBox, _
    SWF & TextBox, SWF & Button, SWF & CheckBox, SWF & RadioButton, SWF & StaticLabel, _
    SWF & TabControl, SWF & TreeView, WPF & Window, WPF & WinObject, WPF & ListBox, _
    WPF & ListView, WPF & ComboBox, WPF & TextBox, WPF & Button, WPF & CheckBox, _
    WPF & RadioButton, WPF & StaticLabel, WPF & TabControl, WPF & TreeView, _
    WebElement, WebTable, WebTableCell, WebTableRow, WebCheckbox, WebButton, _
    WebDiv, WebLink, WebGenericInput, WebImage, WebList, WebTextBox, _
    WebComboBox, WebSpan, WebRadioButton}

    '**************************This is where the update for recording clicks is needed for Internet Explorer
    Public Shared Function ProccessStrs(ByVal str As String, ByVal XCoord As Int32, ByVal YCoord As Int32, Optional ByVal counter As Integer = -1) As String
        Dim strs() As String = str.Split(vbNewLine)
        Dim newStr As String = ""
        'Dim int As Int32 = 0
        Dim tmpInt As Int32
        Dim FirstTime As Boolean = True
        Dim TopWindow As IntPtr = 0
        Dim NextParent As IntPtr = 0
        IsWebJustProcessed = False
        Dim TopWinHwnds As New System.Collections.Generic.List(Of IntPtr)
        Dim MainWinHwnds As New System.Collections.Generic.List(Of IntPtr)
        Dim Hwnds As New System.Collections.Generic.List(Of IntPtr)
        'Dim InfoLog As String = String.Empty
        'Console.WriteLine("hWnds: ")
        For i As Integer = strs.Length - 1 To 0 Step -1
            If (strs(i).Length > 0) Then
                If (Integer.TryParse(strs(i), tmpInt) = True) Then
                    Hwnds.Add(tmpInt)
                End If
            End If
        Next

        If (WindowsFunctions.IsWebPartExactlyIE(Hwnds.Item(0)) = True AndAlso XCoord <> -1 AndAlso YCoord <> -1) Then 'recording shows this is IE
            'Dang it!  It's IE... Time to check if it involves the web...
            If ((Hwnds.Count = 3 AndAlso WindowsFunctions.IsWebPartIEHTML(Hwnds.Item(2)) = True) _
            OrElse (Hwnds.Count = 4 AndAlso WindowsFunctions.IsWebPartIEHTML(Hwnds.Item(3)) = True) _
            OrElse (Hwnds.Count = 5 AndAlso WindowsFunctions.IsWebPartIEHTML(Hwnds.Item(4)) = True)) Then
                'Grumble... gotta do a lot of special cases here.
                'Grab IE's main window, like normal.
                newStr = CreateWindowString(Hwnds.Item(0), IntPtr.Zero)
                'The rest of this counts on IE being good and giving good data.
                Dim p As System.Drawing.Point = APIControls.InternetExplorer.GetPointByInternalIE(Hwnds.Item(Hwnds.Count - 1), XCoord, YCoord)
                WebX = p.X 'XCoord - rec.Left
                WebY = p.Y 'YCoord - rec.Top
                Try
                    GetIE(Hwnds.Item(0))
                    IsWebJustProcessed = True
                Catch ex As Exception
                    Return newStr 'Return this or an error string?
                End Try
                newStr += RetStr
                Return newStr
            End If
        End If

        For Each i As Integer In Hwnds
            tmpInt = i

            If (FirstTime = True) Then
                TopWinHwnds.Add(New IntPtr(tmpInt))
                'TopWindow = New IntPtr(tmpInt)
                MainWinHwnds.Add(IntPtr.Zero)
                'int = 0
                FirstTime = False
            Else
                NextParent = MainWinHwnds.Item(MainWinHwnds.Count - 1) 'int
                If (APIControls.EnumerateWindows.isChildDirectlyConnectedToParent(NextParent, tmpInt) = False) Then
                    newStr = ""
                    TopWinHwnds.Add(New IntPtr(tmpInt))
                    'TopWindow = tmpInt
                    'int = 0
                    MainWinHwnds.Add(IntPtr.Zero)
                Else
                    MainWinHwnds.Add(New IntPtr(tmpInt))
                    TopWinHwnds.Add(TopWinHwnds.Item(TopWinHwnds.Count - 1))
                    'int = tmpInt
                End If
            End If
            'in order to deal with the "parent stubs" that occur
            'such as filedialog boxes, I simply remove all the
            'recorded parents automagically.
            'InfoLog += "Top: " & TopWindow.ToString() & " Inner: " & int.ToString & vbNewLine
            'newStr += CreateWindowString(TopWindow, int) ', FirstTime)
            ''**
            newStr += CreateWindowString(TopWinHwnds.Item(TopWinHwnds.Count - 1), MainWinHwnds.Item(MainWinHwnds.Count - 1)) ', FirstTime)
            ''**
            'int = tmpInt
            MainWinHwnds.Item(MainWinHwnds.Count - 1) = tmpInt
        Next
        'Console.WriteLine(InfoLog)
        'MsgBox(InfoLog)
        Console.WriteLine(newStr)
        Return newStr
    End Function
#Region "web"




    Private Shared Sub GetIE(ByVal Hwnd As IntPtr)
        'System.Console.WriteLine("GetIE with IntPtr")
        GetIETimer(Hwnd)
        Dim Timeout As Integer = 0
        While (IETimer.Interval = 1)
            System.Threading.Thread.Sleep(40)
            'Timeout += 1
            'If (Timeout = 20) Then Return
        End While
    End Sub
    Private Shared Sub GetIETimer(ByVal Hwnd As IntPtr)
        'System.Console.WriteLine("Starting IETimer")
        IETimerHwnd = Hwnd
        IETimer.Interval = 1
        IETimer.Enabled = True
        IETimer.Start()
    End Sub

    Private Shared Sub IETimer_Elapsed(ByVal sender As Object, ByVal e As System.Timers.ElapsedEventArgs) Handles IETimer.Elapsed
        IETimer.Stop()
        Console.WriteLine("IE Reading started: " & Now.TimeOfDay.ToString())
        Dim takeOverIEResult As Boolean
        SyncLock (IE)
            takeOverIEResult = IE.TakeOverIESearch(IETimerHwnd)
        End SyncLock

        If (takeOverIEResult = False) Then
            IETimer.Interval = 2
            Throw New Exception("Unable to find Internet Explorer.")
        End If
        IE.Stop()

        Dim elem As APIControls.WebElementAPI = IE.GetElement(WebX, WebY)
        Console.WriteLine("*-*IE Element To description: " & Now.TimeOfDay.ToString())
        Dim desc As APIControls.Description = IE.FindGoodDescription(elem, True)
        Console.WriteLine("*-*IE Element To description Done: " & Now.TimeOfDay.ToString())
        If (desc IsNot Nothing) Then
            RetStr = CreateWebString(desc, elem.TagName)
        Else
            RetStr = ""
        End If
        Dim rec As System.Drawing.Rectangle = IE.GetElementLocation(elem)
        WebX = rec.X - WebX
        WebY = rec.Y - WebY
        Dim ActualX As Integer = elem.ActualX
        Dim ActualY As Integer = elem.ActualY
        Console.WriteLine("Point of test element is [test function]: " + IE.TestGetElementPoint(ActualX, ActualY).ToString())

        Console.WriteLine("Point of 0,0 is [test function]: " + IE.TestGetElementPoint(0, 0).ToString())
        Console.WriteLine("IE located at: " & IE.GetIELocation().ToString())
        IETimer.Interval = 2
        Console.WriteLine("IE Reading ending: " & Now.TimeOfDay.ToString())
        elem.Click()
    End Sub

    Public Shared Function CreateWebString(ByVal desc As APIControls.Description, ByVal TagName As String) As String
        Dim quote As String = """"
        quote += quote
        Dim retVal As String = APIControls.WebTagDefinitions.GetWebType(TagName) & "("""

        Dim firstTimeInLoop As Boolean = True

        For Each item As APIControls.Description.DescriptionData In [Enum].GetValues(GetType(APIControls.Description.DescriptionData))
            'Newer method, may bust with newline fix
            Dim TmpRetVal As String
            If (desc.Contains(item) = True) Then
                If (item = APIControls.Description.DescriptionData.WebValue OrElse item = APIControls.Description.DescriptionData.WebText) Then
                    Dim tmp As String = desc.GetItemValue(item)
                    If (tmp.Length = APIControls.InternetExplorer.RecorderTextLength) Then
                        If tmp.EndsWith("*") Then
                            desc.WildCard = True
                        End If
                    End If
                End If
                If (firstTimeInLoop = True) Then
                    firstTimeInLoop = False
                    TmpRetVal = desc.GetItemName(item) & ":=" & quote & _
                    desc.GetItemValue(item).Replace("""", quote)

                Else
                    TmpRetVal = ";;" & desc.GetItemName(item) & ":=" & quote & _
                    desc.GetItemValue(item).Replace("""", quote)

                End If
                retVal += TmpRetVal. _
                Replace(vbNewLine, """" & " & vbNewLine & " & """"). _
                Replace(vbCr, """" & " & vbCr & " & """"). _
                Replace(vbLf, """" & " & vbLf & " & """") & quote
            End If
            'Old simple method, could break with new lines
            'If (desc.Contains(item) = True) Then
            '    If (firstTimeInLoop = True) Then
            '        firstTimeInLoop = False
            '        retVal += desc.GetItemName(item) & ":=" & quote & _
            '        desc.GetItemValue(item).Replace("""", quote) & quote
            '    Else
            '        retVal += ";;" & desc.GetItemName(item) & ":=" & quote & _
            '        desc.GetItemValue(item).Replace("""", quote) & quote
            '    End If
            'End If
        Next
        For Each item As String In WinItems
            If (retVal = item) Then 'failure... This is really bad :(
                Return ""
            End If
        Next
        Return retVal & """)." 'Good, it worked
    End Function

#End Region


    'order = false for the first window since name and value are swapped.
    'this is done due to windows, I guess :/
    Public Shared Function CreateWindowString(ByVal TophWnd As IntPtr, ByVal ihWnd As Int32) As String ', ByVal order As Boolean) As String

        Dim quote As String = """"
        Dim hWnd As IntPtr = ihWnd
        quote += quote
        Dim retVal As String
        'This method is _SLOW_ but accurate....
        Dim desc As APIControls.Description = WindowsFunctions.CreateDescription(TophWnd, hWnd)

        If (WindowsFunctions.ListBox.IsListBox(hWnd)) Then
            retVal = ListBox
        ElseIf (WindowsFunctions.ListView.IsListView(hWnd)) Then
            retVal = ListView
        ElseIf (WindowsFunctions.ComboBox.IsComboBox(hWnd)) Then
            retVal = ComboBox
        ElseIf (WindowsFunctions.TextBox.IsTextBox(hWnd)) Then
            retVal = TextBox
        ElseIf (WindowsFunctions.Button.IsButton(hWnd)) Then
            retVal = Button
        ElseIf (WindowsFunctions.TabControl.IsTabControl(hWnd)) Then
            retVal = TabControl
        ElseIf (WindowsFunctions.StaticLabel.IsStaticLabel(hWnd)) Then
            retVal = StaticLabel
        ElseIf (WindowsFunctions.Button.IsCheckBox(hWnd)) Then
            retVal = CheckBox
        ElseIf (WindowsFunctions.Button.IsRadioButton(hWnd)) Then
            retVal = RadioButton
        ElseIf (WindowsFunctions.TreeView.IsTreeView(hWnd)) Then
            retVal = TreeView
        ElseIf (WindowsFunctions.Window.IsWindow(hWnd)) Then
            'If (desc.Name.Equals("IEFrame") = True) Then 'window type is webbrowser
            Dim isWeb As Boolean = False

            If (hWnd = IntPtr.Zero) Then
                isWeb = WindowsFunctions.IsWebPartExactlyIE(TophWnd)
            Else
                isWeb = WindowsFunctions.IsWebPartExactlyIE(hWnd)
            End If
            If (isWeb = True) Then
                retVal = IEWebBrower
            Else
                retVal = Window
            End If
        Else
            retVal = WinObject
        End If
        If (hWnd = IntPtr.Zero) Then
            If (WindowsFunctions.GetClassNameNoDotNet(TophWnd).Contains("WindowsForms10") = True) Then
                retVal = SWF + retVal
            End If
        Else
            If (WindowsFunctions.GetClassNameNoDotNet(hWnd).Contains("WindowsForms10") = True) Then
                retVal = SWF + retVal
            End If
        End If

        If (desc.Contains(APIControls.Description.DescriptionData.Name)) = True Then
            If (desc.Name.Equals("IEFrame") = True) Then
                'This is a instance of IE
                If (hWnd = IntPtr.Zero) Then 'it's a top window
                    desc.Add(APIControls.Description.DescriptionData.Value, WindowsFunctions.GetAllText(TophWnd)) 'The name of the window is automatically included.
                Else 'it's a subwindow... how?  window managers, imbedded html, etc.
                    desc.Add(APIControls.Description.DescriptionData.Value, WindowsFunctions.GetAllText(hWnd)) 'The name of the window is automatically included.
                End If

                'Do more web based stuff here.
                'retVal = Web + retVal
            End If
        End If

        Dim firstTimeInLoop As Boolean = True
        If (desc.Value IsNot Nothing) Then
            If (desc.Value.Length > 80) Then
                desc.Add(APIControls.Description.DescriptionData.Value, desc.Value.Substring(0, 50) & "*")
                desc.WildCard = True
            End If
            If (desc.Value.Contains(vbNewLine)) Then
                desc.Add(APIControls.Description.DescriptionData.Value, desc.Value.Replace(vbNewLine, "*"))
                desc.WildCard = True
            End If
            If (desc.Value.Contains(vbCr)) Then
                desc.Add(APIControls.Description.DescriptionData.Value, desc.Value.Replace(vbCr, "*"))
                desc.WildCard = True
            End If
            If (desc.Value.Contains("""")) Then
                desc.Add(APIControls.Description.DescriptionData.Value, desc.Value.Replace("""", quote))
            End If
        End If
        If (desc.Name IsNot Nothing) Then
            If (desc.Name.Length > 120) Then
                desc.Add(APIControls.Description.DescriptionData.Name, desc.Name.Substring(0, 75) & "*")
                desc.WildCard = True
            End If
            If (desc.Name.Contains(vbNewLine)) Then
                desc.Add(APIControls.Description.DescriptionData.Name, desc.Name.Replace(vbNewLine, "*"))
                desc.WildCard = True
            End If
            If (desc.Name.StartsWith("ATL:")) Then
                desc.Add(APIControls.Description.DescriptionData.Name, "ATL:*")
                desc.WildCard = True
            End If
            If (desc.Name.ToLower.StartsWith("afx:")) Then
                desc.Add(APIControls.Description.DescriptionData.Name, desc.Name.Substring(0, "Afx:".Length) & "*")
                desc.WildCard = True
            End If
        End If
        For Each item As APIControls.Description.DescriptionData In [Enum].GetValues(GetType(APIControls.Description.DescriptionData))
            If (desc.Contains(item) = True) Then
                If (firstTimeInLoop = True) Then
                    firstTimeInLoop = False
                    'retVal += desc.GetItemName(item) & ":=" & quote & _
                    'desc.GetItemValue(item).Replace(vbNewLine, "& vbNewLine &" & """").Replace("""", quote) & quote
                    retVal += desc.GetItemName(item) & ":=" & quote & _
                    desc.GetItemValue(item) & quote
                Else
                    'retVal += ";;" & desc.GetItemName(item) & ":=" & quote & _
                    'desc.GetItemValue(item).Replace(vbNewLine, " & vbNewLine & " & """").Replace("""", quote) & quote
                    retVal += ";;" & desc.GetItemName(item) & ":=" & quote & _
                    desc.GetItemValue(item) & quote
                End If
            End If
        Next
        For Each item As String In WinItems
            If (retVal = item) Then 'failure... This is really bad :(
                Return ""
            End If
        Next
        If (desc.WildCard = True) Then

            retVal += ";;" & desc.GetItemName(APIControls.Description.DescriptionData.WildCard) & ":=" & _
            quote & desc.GetItemValue("wildcard").Replace("""", quote) & quote
        End If
        Return retVal & """)." 'Good, it worked
    End Function
    '**************************This is where the update for recording clicks is needed for Internet Explorer
    Shared Function BuildWindowTree(ByVal hChild As IntPtr, ByVal Strings As String) As String
        If (hChild = 0) Then
            Return Strings
        End If
        Strings += hChild.ToString() & vbNewLine
        Strings = BuildWindowTree(GetParent(hChild), Strings & vbCr)
        Return Strings
    End Function
End Class
