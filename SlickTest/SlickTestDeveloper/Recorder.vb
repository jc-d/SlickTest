Public Class Recorder
#Const isAbs = 2 'Set to 1 if you want absolute coordinates

    Private IsRecording As Boolean
    Private TempText As String
    Private StopClicked As Boolean
    Private TextTimerInt As Integer
    Private Title As String
    Private FirstAppActivated As Boolean
    Private description As APIControls.Description
    Private ActiveWindow As APIControls.TopWindow
    Private RecordTabControlNormalSize As System.Drawing.Size
    Public ReturnText As String
    Public ReturnClass As String
    Private ObjectIdenifier As String = "Me."
    Private UpdateText As New System.Collections.Generic.List(Of String)
    Private RecorderSleepTime As Integer

    Public Property ObjectRecorderID() As String
        Get
            Return ObjectIdenifier
        End Get
        Set(ByVal value As String)
            ObjectIdenifier = value
            If (ObjectIdenifier <> "") Then
                If (ObjectIdenifier.EndsWith(".") = False) Then
                    ObjectIdenifier += "."
                End If
            End If
        End Set
    End Property

#Region "Stupid Regex for txt-to-desc object"


    ''' <summary>
    '''  Regular expression built for C# on: Sat, Jul 21, 2007, 01:34:59 AM
    '''  Using Expresso Version: 3.0.2745, http://www.ultrapico.com
    '''  
    '''  A description of the regular expression:
    '''  
    '''  [StartDesc]: A named capture group. [\w+(\(\")]
    '''      [1]: A numbered capture group. [\(\"]
    '''          \(\"
    '''              Literal (
    '''              Literal "
    '''  [KeyValue]: A named capture group. [((\w*\:\=)(\"\")((\s*\w*\s*)*)(\"\"\"?)(\;?\;?))*]
    '''      [2]: A numbered capture group. [(\w*\:\=)(\"\")((\s*\w*\s*)*)(\"\"\"?)(\;?\;?)], any number of repetitions
    '''          (\w*\:\=)(\"\")((\s*\w*\s*)*)(\"\"\"?)(\;?\;?)
    '''              [3]: A numbered capture group. [\w*\:\=]
    '''                  \w*\:\=
    '''                      Alphanumeric, any number of repetitions
    '''                      Literal :
    '''                      Literal =
    '''              [4]: A numbered capture group. [\"\"]
    '''                  \"\"
    '''                      Literal "
    '''                      Literal "
    '''              [5]: A numbered capture group. [(\s*\w*\s*)*]
    '''                  [6]: A numbered capture group. [\s*\w*\s*], any number of repetitions
    '''                      \s*\w*\s*
    '''                          Whitespace, any number of repetitions
    '''                          Alphanumeric, any number of repetitions
    '''                          Whitespace, any number of repetitions
    '''              [7]: A numbered capture group. [\"\"\"?]
    '''                  \"\"\"?
    '''                      Literal "
    '''                      Literal "
    '''                      Literal ", zero or one repetitions
    '''              [8]: A numbered capture group. [\;?\;?]
    '''                  \;?\;?
    '''                      Literal ;, zero or one repetitions
    '''                      Literal ;, zero or one repetitions
    '''  [Close]: A named capture group. [\)]
    '''      Literal )
    '''  
    ''' (?'StartDesc'\w+\(\"")(?'KeyValue'(\w*\:\=\""\""((\s*\w*\s*)*)\""\""\""?\;?\;?)*)(?'Close'\))
    ''' </summary>
    ''' 
    Private Shared VBNetRegex As New System.Text.RegularExpressions.Regex( _
    "(?<StartDesc>\w+\(\"")(?<KeyValue>(\w*\:\=\""\""([^""]*|\""?\""?[!#$%&'()\[\]*+,-.\""/:;<=>?@\\^_`{|}~A-Za-z0-9\s\t\-]*\""?\""?)\""\""\""?\;?\;?)*)(?<Close>\))", _
    System.Text.RegularExpressions.RegexOptions.Multiline Or System.Text.RegularExpressions.RegexOptions.Compiled)

    Private Shared CSharpRegex As New System.Text.RegularExpressions.Regex( _
    "(?<StartDesc>\w+\(@\"")(?<KeyValue>(\w*\:\=\""\""([^""]*|\""?\""?[!#$%&'()\[\]*+,-.\""/:;<=>?@\\^_`{|}~A-Za-z0-9\s\t\-]*\""?\""?)\""\""\""?\;?\;?)*)(?<Close>\))", _
    System.Text.RegularExpressions.RegexOptions.Multiline Or System.Text.RegularExpressions.RegexOptions.Compiled)


    'Private Shared regex As New System.Text.RegularExpressions.Regex( _
    '"(?<StartDesc>\w+\(\"")(?<KeyValue>(\w*\:\=\""\""([^""]*|\""?\""?[!#$%&'()\[\]*+,-./:;<=>?@\\^_`{|}~A-Za-z0-9\s\t\-]*\""?\""?)\""\""\""?\;?\;?)*)(?<Close>\))", _
    'System.Text.RegularExpressions.RegexOptions.Multiline Or System.Text.RegularExpressions.RegexOptions.Compiled)
    'Private Shared regex As New System.Text.RegularExpressions.Regex( _
    '"(?<StartDesc>\w+\(\"")(?<KeyValue>(\w*\:\=.*\""\)\.)*)", _
    'System.Text.RegularExpressions.RegexOptions.Singleline Xor System.Text.RegularExpressions.RegexOptions.Compiled)
    'KNOWN BUG: This will not catch/convert descriptions with quotes in them.  Everything else should work AFAIK.

#End Region

    Public Function ConvertCode(ByVal DescriptionClassName As String, ByVal CodeClassName As String, Optional ByVal ConvertToDescription As Boolean = True)
        If (ConvertToDescription = True) Then
            ConvertDescription()
        End If
        Dim CommentCode As String = "'"
        Dim Region As String = "#Region"
        Dim EndRegion As String = "#End Region"
        Dim PublicClass As String = "Public Class "
        Dim EndClass As String = "End Class"
        Dim PublicSub As String = "Public Sub New()"
        Dim EndSub As String = "End Sub"

        Dim InheritsFrom As String = vbNewLine & vbTab & "Inherits UIControls.InterAct"
        Dim AfterClassNameWrapper As String = ""

        If (SlickTestDev.CurrentLanguage = CodeTranslator.Languages.CSharp) Then
            PublicClass = PublicClass.ToLower()
            Region = Region.ToLower()
            EndRegion = EndRegion.ToLower().Replace(" ", "")
            EndClass = "}"
            PublicSub = "public " & CodeClassName & "(){"
            EndSub = "}"
            CommentCode = "//"
            InheritsFrom = ": UIControls.InterAct"
            AfterClassNameWrapper = "{"
        End If

        Dim DescriptionsText As String = Me.DescriptionTextBox.Text.Replace(vbNewLine & vbNewLine, vbNewLine).Replace(vbNewLine, vbNewLine & vbTab)
        If (DescriptionsText.Length > 5) Then
            DescriptionsText = CommentCode & "Description Code Below" & vbNewLine & vbTab & DescriptionsText & vbNewLine
        End If
        Dim Code As String = String.Empty

        '''''''Appends the DescriptionClassName to each of the objects.
        Dim TmpRecordText As String = Me.RecorderTextBox.Text.Replace(vbNewLine, vbNewLine & vbTab & vbTab)
        If (ConvertToDescription = True) Then
            TmpRecordText = TmpRecordText.Replace("Window(", "Window(" & DescriptionClassName & ".")
            TmpRecordText = TmpRecordText.Replace("TextBox(", "TextBox(" & DescriptionClassName & ".")
            TmpRecordText = TmpRecordText.Replace("RadioButton(", "RadioButton(" & DescriptionClassName & ".")
            TmpRecordText = TmpRecordText.Replace("ListBox(", "ListBox(" & DescriptionClassName & ".")
            TmpRecordText = TmpRecordText.Replace("WinObject(", "WinObject(" & DescriptionClassName & ".")
            TmpRecordText = TmpRecordText.Replace("ComboBox(", "ComboBox(" & DescriptionClassName & ".")
            TmpRecordText = TmpRecordText.Replace("Button(", "Button(" & DescriptionClassName & ".")
            TmpRecordText = TmpRecordText.Replace("CheckBox(", "CheckBox(" & DescriptionClassName & ".")
            TmpRecordText = TmpRecordText.Replace("StaticLabel(", "StaticLabel(" & DescriptionClassName & ".")
            TmpRecordText = TmpRecordText.Replace("TabControl(", "TabControl(" & DescriptionClassName & ".")


            TmpRecordText = TmpRecordText.Replace("WebElement(", "WebElement(" & DescriptionClassName & ".")
            TmpRecordText = TmpRecordText.Replace("ListView(", "ListView(" & DescriptionClassName & ".")
            TmpRecordText = TmpRecordText.Replace("TreeView(", "TreeView(" & DescriptionClassName & ".")
            ''''''''''''''''''''''''''''''Fix all Items which were not correctly converted.
            Dim DescClassNameDotQuote As String = DescriptionClassName & "."""
            TmpRecordText = TmpRecordText.Replace("Window(" & DescClassNameDotQuote, "Window(""")
            TmpRecordText = TmpRecordText.Replace("TextBox(" & DescClassNameDotQuote, "TextBox(""")
            TmpRecordText = TmpRecordText.Replace("RadioButton(" & DescClassNameDotQuote, "RadioButton(""")
            TmpRecordText = TmpRecordText.Replace("ListBox(" & DescClassNameDotQuote, "ListBox(""")
            TmpRecordText = TmpRecordText.Replace("TreeView(" & DescClassNameDotQuote, "TreeView(""")
            TmpRecordText = TmpRecordText.Replace("ListView(" & DescClassNameDotQuote, "ListView(""")
            TmpRecordText = TmpRecordText.Replace("WinObject(" & DescClassNameDotQuote, "WinObject(""")
            TmpRecordText = TmpRecordText.Replace("ComboBox(" & DescClassNameDotQuote, "ComboBox(""")
            TmpRecordText = TmpRecordText.Replace("Button(" & DescClassNameDotQuote, "Button(""")
            TmpRecordText = TmpRecordText.Replace("CheckBox(" & DescClassNameDotQuote, "CheckBox(""")
            TmpRecordText = TmpRecordText.Replace("StaticLabel(" & DescClassNameDotQuote, "StaticLabel(""")
            TmpRecordText = TmpRecordText.Replace("TabControl(" & DescClassNameDotQuote, "TabControl(""")


            TmpRecordText = TmpRecordText.Replace("WebElement(" & DescClassNameDotQuote, "WebElement(""")
        End If

        If (DescriptionsText.Length <> 0) Then
            If (DescriptionClassName <> CodeClassName) Then

                Code = vbNewLine & Region & " ""Description (" & DescriptionClassName & ") was generated via the recorder...""" & vbNewLine & _
                PublicClass & DescriptionClassName & AfterClassNameWrapper & vbNewLine & _
                vbTab & DescriptionsText.Substring(0, Math.Max(DescriptionsText.Length - 3, 0)) & _
                EndClass & vbNewLine & EndRegion & vbNewLine

            End If
        End If

        Dim Code2 As String = String.Empty
        Dim RegionCode As String = Region & " ""Recorded code (" & CodeClassName & ") was generated via the recorder..."""
        If (DescriptionClassName <> CodeClassName) Then

            Code2 = vbNewLine & RegionCode & vbNewLine & _
            PublicClass & CodeClassName & InheritsFrom & AfterClassNameWrapper & vbNewLine & _
            vbTab & PublicSub & vbNewLine & _
            vbTab & CommentCode & "Recorded Code Below" & vbNewLine & _
            TmpRecordText & vbNewLine & _
            vbTab & EndSub & vbNewLine & EndClass & vbNewLine & EndRegion

        Else

            Code2 = vbNewLine & RegionCode & vbNewLine & _
            PublicClass & CodeClassName & InheritsFrom & AfterClassNameWrapper & vbNewLine & _
            vbTab & DescriptionsText.Substring(0, Math.Max(DescriptionsText.Length - 3, 0)) & vbNewLine & _
            vbTab & PublicSub & vbNewLine & _
            vbTab & CommentCode & "Recorded Code Below" & vbNewLine & _
            TmpRecordText & vbNewLine & _
            vbTab & EndSub & vbNewLine & EndClass & vbNewLine & EndRegion

        End If
        Return Code & Code2
    End Function

    Private Sub CodeIntoEditorButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CodeIntoEditorButton.Click
        Me.TopMost = False
        Dim DescriptionClassName As String = InputBox("Enter an original class name to use for the descriptions.  " & _
        "Note that this will also require that all descriptions are converted to objects.  " & _
        "This process can not be undone.", SlickTestDev.MsgBoxTitle, "MyApplication")

        Dim CodeClassName As String = InputBox("Enter an original class name to use for the test code.", _
        SlickTestDev.MsgBoxTitle, "MyTestClass")
        Dim ConvertCodeToDescriptions As Boolean = True
        If (MessageBox.Show("Would you like to have the code converted to descriptions?", _
                            SlickTestDev.MsgBoxTitle, MessageBoxButtons.YesNo) _
            = Windows.Forms.DialogResult.No) Then
            ConvertCodeToDescriptions = False
        End If

        Me.TopMost = True
        If (DescriptionClassName <> "" AndAlso IsNumeric(DescriptionClassName) = False) Then
            If (CodeClassName <> "" AndAlso IsNumeric(CodeClassName) = False) Then
                Me.ReturnText = ConvertCode(DescriptionClassName, CodeClassName, ConvertCodeToDescriptions)
                Me.ReturnClass = CodeClassName
                My.Forms.SlickTestDev.ProjectCurrentlySaved = False
                Me.Close()
                Return
            End If
        End If
        'Invalid entry.
        MessageBox.Show("Invalid description or code class name. " & _
                        CopyCodeError, SlickTestDev.MsgBoxTitle)
    End Sub

    'Private Function GetKeys() As String
    '    'Dim temp As String = Keys.GetKeys
    '    'Console.WriteLine("temp=" + temp)
    '    'GetKeys = temp
    '    Return ""
    'End Function

    Private Sub Record()
        TextTimerInt = 0
        'GetBufferedInfo()
        'DumpText()
        Me.CodeIntoClipboardButton.Enabled = Not Me.CodeIntoClipboardButton.Enabled
        Me.CodeIntoEditorButton.Enabled = Not Me.CodeIntoEditorButton.Enabled
        Me.ConvertDescriptionsButton.Enabled = Not Me.ConvertDescriptionsButton.Enabled
        If (IsRecording = False) Then
            Me.RecordTabControl.Size = New System.Drawing.Size(Me.RecordTabControl.Size.Width, 100)
            Me.RecorderTextBox.Size = New System.Drawing.Size(Me.RecordTabControl.Size.Width - 14, Me.RecordTabControl.Size.Height - 55)
            Me.MinimumSize = New System.Drawing.Size(Me.Width, RecordTabControl.Size.Height + 75)
            Me.Size = New System.Drawing.Size(Me.Width, RecordTabControl.Size.Height + 75)
            Me.RecordButton.Text = Me.RecordButton.Text.Replace("Start", "Stop")
            Me.RecordButton.BackColor = Color.Red
            Keys.IsRecording = True
            FirstAppActivated = False
            Keys.StartHandlers()
            Me.Location = New System.Drawing.Point( _
            System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Width - Me.Size.Width, _
            System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Height - Me.Size.Height)
        Else
            Me.RecordButton.Text = Me.RecordButton.Text.Replace("Stop", "Start")
            Me.RecordButton.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(0, Byte), Integer))
            Me.RecordTabControl.Size = Me.RecordTabControlNormalSize
            Me.Size = New System.Drawing.Size(Me.Width, RecordTabControl.Size.Height + 75)
            Me.RecorderTextBox.Size = New System.Drawing.Size(Me.RecordTabControl.Size.Width - 14, Me.RecordTabControl.Size.Height - 35)
            Me.Focus()
            Keys.IsRecording = False
            Keys.StopHandlers()
            Me.Location = New System.Drawing.Point( _
            System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Width - Me.Size.Width, _
            System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Height - Me.Size.Height)
        End If
        IsRecording = Not IsRecording
    End Sub

    Private Sub RecordButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RecordButton.Click
        Record()
    End Sub

    'Private Sub GetBufferedInfo()
    '    TempText = TempText & GetKeys()
    '    Dim AppTemp As String = GetApp()
    '    If (AppTemp <> "") Then
    '        If (AppTemp <> Title) Then
    '            Title = AppTemp
    '            DumpText()
    '        End If
    '    End If
    '    If (StopClicked = True) Then
    '        DumpText()
    '    End If
    'End Sub

    'Private Function GetApp() As String
    '    Return ActiveWindow.GetActiveWindow()
    'End Function

    'Private Sub DumpText()
    '    If TempText <> "" Then
    '        RecorderTextBox.Text += vbNewLine & ObjectRecorderID & "SendInput(""" & TempText & """)" & Pause()
    '        TempText = ""
    '    End If
    'End Sub

    Private Function Pause() As String
        If (RecorderSleepTime > 0) Then
            If (SlickTestDev.CurrentLanguage = CodeTranslator.Languages.VBNet) Then
                Return vbNewLine & "System.Threading.Thread.Sleep(" & RecorderSleepTime & ")"
            Else
                Return vbNewLine & "System.Threading.Thread.Sleep(" & RecorderSleepTime & ");"
            End If
        End If
        Return ""
    End Function

    Private Const IEWebBrowserString As String = "IEWebBrowser"

    Private Sub Keys_MouseAction(ByVal action As String, ByVal type As HandleInput.MouseAndKeys.MouseActionType, ByVal x As Integer, ByVal y As Integer) Handles Keys.MouseAction
        If (action <> "") Then
            'GetBufferedInfo()
            'DumpText()
#If isAbs = 1 Then
            x = InterAction.Mouse.RelativeToAbsCoordX(x)
            y = InterAction.Mouse.RelativeToAbsCoordY(y)
#End If
            'If (type <> HandleInput.Keys.MouseActionType.SendText OrElse type <> HandleInput.Keys.MouseActionType.SendTextObj) Then
            '    If (Me.IsDoubleClick = TriState.False) Then
            '        Me.IsDoubleClick = TriState.True
            '    Else
            '        Me.IsDoubleClick = TriState.False
            '    End If
            'End If
            If (type = HandleInput.MouseAndKeys.MouseActionType.DoubleClickObjL OrElse _
            type = HandleInput.MouseAndKeys.MouseActionType.DoubleClickObjR) Then
                UpdateText.Clear()
            Else
                For Each str As String In UpdateText
                    Me.RecorderTextBox.Text += str
                Next
                UpdateText.Clear()
            End If
            Select Case type
                Case HandleInput.MouseAndKeys.MouseActionType.ClickXY
                    'SendInput looks ugly for the user...
                    'UpdateText.Add(vbNewLine & ObjectRecorderID & "SendInput(""" & action & """)")
                    UpdateText.Add(vbNewLine & ObjectRecorderID & "Mouse." & action)
                Case HandleInput.MouseAndKeys.MouseActionType.DoubleClickXY
                    UpdateText.Add(vbNewLine & ObjectRecorderID & "Mouse." & action & _
                    vbNewLine & ObjectIdenifier & "Mouse." & action)
                Case HandleInput.MouseAndKeys.MouseActionType.ClickObjL
                    If (My.Settings.Recorder_Absolute__Coordinates = True) Then
                        x = UIControls.Mouse.RelativeToAbsCoordX(x)
                        y = UIControls.Mouse.RelativeToAbsCoordY(y)
                        UpdateText.Add(vbNewLine & ObjectRecorderID & action & _
                        "Click(" & ObjectRecorderID & "Mouse.AbsToRelativeCoordX(" & x & "," & _
                        System.Windows.Forms.Screen.PrimaryScreen.Bounds.Width & ")," & _
                        "" & ObjectRecorderID & "Mouse.AbsToRelativeCoordY(" & y & "," & _
                        System.Windows.Forms.Screen.PrimaryScreen.Bounds.Height & ")" & ")")
                    Else
                        If (action.StartsWith(IEWebBrowserString) AndAlso action.Contains("WinObject(""") = False) Then
                            UpdateText.Add(vbNewLine & ObjectRecorderID & action & "Click(" & ")")
                        Else
                            UpdateText.Add(vbNewLine & ObjectRecorderID & action & "Click(" & x & "," & y & ")")
                        End If
                    End If
                Case HandleInput.MouseAndKeys.MouseActionType.ClickObjR
                    If (My.Settings.Recorder_Absolute__Coordinates = True) Then
                        x = UIControls.Mouse.RelativeToAbsCoordX(x)
                        y = UIControls.Mouse.RelativeToAbsCoordY(y)
                        UpdateText.Add(vbNewLine & ObjectRecorderID & action & _
                        "RightClick(" & ObjectRecorderID & "Mouse.AbsToRelativeCoordX(" & x & "," & _
                        System.Windows.Forms.Screen.PrimaryScreen.Bounds.Width & ")," & _
                        "" & ObjectRecorderID & "Mouse.AbsToRelativeCoordY(" & y & "," & _
                        System.Windows.Forms.Screen.PrimaryScreen.Bounds.Height & ")" & ")")
                    Else
                        If (action.StartsWith(IEWebBrowserString) AndAlso action.Contains("WinObject(""") = False) Then
                            UpdateText.Add(vbNewLine & ObjectRecorderID & action & "RightClick(" & ")")
                        Else
                            UpdateText.Add(vbNewLine & ObjectRecorderID & action & "RightClick(" & x & "," & y & ")")
                        End If
                    End If
                Case HandleInput.MouseAndKeys.MouseActionType.DoubleClickObjL
                    If (My.Settings.Recorder_Absolute__Coordinates = True) Then
                        x = UIControls.Mouse.RelativeToAbsCoordX(x)
                        y = UIControls.Mouse.RelativeToAbsCoordY(y)

                        UpdateText.Add(vbNewLine & ObjectRecorderID & action & _
                        "Click(" & ObjectRecorderID & "Mouse.AbsToRelativeCoordX(" & x & "," & _
                        System.Windows.Forms.Screen.PrimaryScreen.Bounds.Width & ")," & _
                        "" & ObjectRecorderID & "Mouse.AbsToRelativeCoordY(" & y & "," & _
                        System.Windows.Forms.Screen.PrimaryScreen.Bounds.Height & ")" & ")")
                    Else
                        If (action.StartsWith(IEWebBrowserString) AndAlso action.Contains("WinObject(""") = False) Then
                            UpdateText.Add(vbNewLine & ObjectRecorderID & action & "Click(" & ")")
                        Else
                            UpdateText.Add(vbNewLine & ObjectRecorderID & action & "Click(" & x & "," & y & ")")
                        End If
                    End If
                Case Else
                    Throw New Exception(type.ToString() & " is not currently supported")
            End Select
          
            'ElseIf (type = HandleInput.Keys.MouseActionType.ClickObjR) Then
            '        RecorderTextBox.Text += vbNewLine & ObjectRecorderID & action & "RightClick(" & x & "," & y & ")" + _
            '        vbNewLine & ObjectRecorderID & action & "RightClick(" & x & "," & y & ")" & Pause()
            '    End If
            'If (type <> HandleInput.Keys.MouseActionType.SendText OrElse type <> HandleInput.Keys.MouseActionType.SendTextObj) Then
            '    Dim TmpText As String = UpdateText.Item(UpdateText.Count - 1)
            '    UpdateText.Remove(TmpText)
            '    If (Me.IsDoubleClick = TriState.True) Then
            '        UpdateText.Add(TmpText & TmpText & Pause())
            '    Else
            '        UpdateText.Add(TmpText & Pause())
            '    End If
            'End If
            Dim sb As New System.Text.StringBuilder()

            For Each str As String In UpdateText
                sb.Append(str)
            Next
            If (SlickTestDev.CurrentLanguage = CodeTranslator.Languages.CSharp) Then
                Dim Code As String = CodeTranslator.FixVBStringToCSharpVbLikeString(sb.ToString())
                Me.RecorderTextBox.Text += CodeTranslator.Convert(CodeTranslator.Languages.VBNet, CodeTranslator.Languages.CSharp, Code)
            Else
                Me.RecorderTextBox.Text += sb.ToString()
            End If
            UpdateText.Clear()
            UpdateText.Add(Pause())
            Try
                RecorderTextBox.ActiveTextAreaControl.Caret.Position = RecorderTextBox.Document.OffsetToPosition(RecorderTextBox.Text.Length - 2)
                RecorderTextBox.ActiveTextAreaControl.ScrollToCaret()
            Catch ex As Exception
                'Happens at first click... which is fine for now...
            End Try
            Try
                If (Me.RecorderTextBox.ActiveTextAreaControl.HScrollBar IsNot Nothing) Then
                    Me.RecorderTextBox.ActiveTextAreaControl.HScrollBar.Value = 0
                End If
            Catch ex As Exception
            End Try
        End If
    End Sub

    Public Sub New(ByVal Keys As HandleInput.MouseAndKeys)
        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        IsRecording = False
        Me.Keys = Keys
        Me.XYRecordingCheckBox.Checked = My.Settings.Recorder_Default__XY__Record__Style
        Me.Keys.RecordXY = My.Settings.Recorder_Default__XY__Record__Style

        Title = ""
        TempText = ""
        Me.Keys.hwndOfApp = Me.Handle
        IsRecording = False
        ActiveWindow = New APIControls.TopWindow
        Me.TopMost = True
        RecordTabControlNormalSize = Me.RecordTabControl.Size
        'Text box 1.
        If (My.Settings.UI_Highlight__Edit__Line = True) Then
            Me.RecorderTextBox.LineViewerStyle = ICSharpCode.TextEditor.Document.LineViewerStyle.FullRow
        Else
            Me.RecorderTextBox.LineViewerStyle = ICSharpCode.TextEditor.Document.LineViewerStyle.None
        End If
        Me.RecorderTextBox.ShowTabs = My.Settings.UI_Show__Tab__Markers
        Me.RecorderTextBox.ShowLineNumbers = My.Settings.UI_Show__Line__Numbers
        RecorderTextBox.Font = My.Settings.UI_Font
        RecorderTextBox.ShowSpaces = My.Settings.UI_Show__Space__Markers
        RecorderTextBox.ShowEOLMarkers = My.Settings.UI_Show__New__Line__Markers
        RecorderTextBox.ShowMatchingBracket = My.Settings.UI_Show__Matching__Brackets
        'Text Box 2
        If (My.Settings.UI_Highlight__Edit__Line = True) Then
            Me.DescriptionTextBox.LineViewerStyle = ICSharpCode.TextEditor.Document.LineViewerStyle.FullRow
        Else
            Me.DescriptionTextBox.LineViewerStyle = ICSharpCode.TextEditor.Document.LineViewerStyle.None
        End If
        Me.DescriptionTextBox.ShowTabs = My.Settings.UI_Show__Tab__Markers
        Me.DescriptionTextBox.ShowLineNumbers = My.Settings.UI_Show__Line__Numbers
        DescriptionTextBox.Font = My.Settings.UI_Font
        DescriptionTextBox.ShowSpaces = My.Settings.UI_Show__Space__Markers
        DescriptionTextBox.ShowEOLMarkers = My.Settings.UI_Show__New__Line__Markers
        DescriptionTextBox.ShowMatchingBracket = My.Settings.UI_Show__Matching__Brackets
        RecorderSleepTime = My.Settings.Recorder_Default__Sleep__Time__MS
        Me.ObjectRefTextBox.Text = ObjectIdenifier
        Me.SleepTimeMaskedTextBox.Text = RecorderSleepTime
        ObjectIdenifier = My.Settings.Recorder_Default__Object__Reference
        Me.ObjectRefTextBox.Text = ObjectIdenifier
        Me.TotalDescriptionLengthMaskedTextBox.Text = My.Settings.Recorder_Default__Total__Description__Length
    End Sub

    Private Sub Recorder_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        If (Me.IsRecording = True) Then
            Me.Record()
        End If
        My.Forms.SlickTestDev.WindowState = FormWindowState.Normal
    End Sub

    Private Const CopyCodeError As String = "  Class name can not be empty or just a number."

    Private Sub CodeIntoClipboardButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CodeIntoClipboardButton.Click
        System.Windows.Forms.Clipboard.Clear()
        Me.TopMost = False
        Dim DescriptionClassName As String = InputBox("Enter an original class name to use for the descriptions.  " & _
        "Note that this will also require that all descriptions are converted to objects.  " & _
        "This process can not be undone.", SlickTestDev.MsgBoxTitle, "MyApplication")

        Dim CodeClassName As String = InputBox("Enter an original class name to use for the test code.", _
        SlickTestDev.MsgBoxTitle, "MyTestClass")

        Dim ConvertCodeToDescriptions As Boolean = True
        If (MessageBox.Show("Would you like to have the code converted to descriptions?", _
                            SlickTestDev.MsgBoxTitle, MessageBoxButtons.YesNo) _
            = Windows.Forms.DialogResult.No) Then
            ConvertCodeToDescriptions = False
        End If


        Me.TopMost = True

        Try
            Dim TmpSetStr As String = String.Empty
            If (DescriptionClassName <> "" AndAlso IsNumeric(DescriptionClassName) = False) Then
                If (CodeClassName <> "" AndAlso IsNumeric(CodeClassName) = False) Then
                    TmpSetStr = ConvertCode(DescriptionClassName, CodeClassName, ConvertCodeToDescriptions)
                Else
                    Throw New Exception("Code class name is invalid." & CopyCodeError) 'Invalid entry.
                End If
            Else
                Throw New Exception("Description class name is invalid." & CopyCodeError) 'Invalid entry.
            End If
            System.Windows.Forms.Clipboard.SetText(TmpSetStr)
        Catch ex As Exception
            UIControls.Alert.Show("Warning: The text was not copied to clipboard.  Error Message: " & ex.Message(), , SlickTestDev.MsgBoxTitle)
        End Try
    End Sub

    Private DescriptionStringLengthLimit As Integer = 35

    'Good test:
    'Me.Window("Value:=""Legend of Zelda Medley - Yahoo! Video - Microsoft Internet Explorer"";;Name:=""IEFrame""").Click(23705,640)
    'Threading.Thread.Sleep(500)
    Sub ConvertDescription()
        Dim CommentForLanguage As String
        If (SlickTestDev.CurrentLanguage = CodeTranslator.Languages.CSharp) Then
            CommentForLanguage = "//"
        Else
            CommentForLanguage = "'"
        End If
        'At this point a regular expression is run on the recorded data.
        Dim descHelper As DescriptionHelper
        Dim descHelperParent As DescriptionHelper
        Dim ms As System.Text.RegularExpressions.MatchCollection
        Dim strDescriptionCollection As New System.Collections.Generic.Dictionary(Of String, DescriptionHelper)
        Dim strRecordString As String = String.Empty
        Dim strDescriptionName As String = String.Empty
        Dim RecordedDescriptionTextParent As String = String.Empty
        For Each line As String In Me.RecorderTextBox.Text.Split(vbNewLine)
            If (SlickTestDev.CurrentLanguage = CodeTranslator.Languages.VBNet) Then
                ms = VBNetRegex.Matches(line)
            Else
                ms = CSharpRegex.Matches(line)
            End If
            For i As Integer = 0 To ms.Count - 1
                description = New APIControls.Description()
                descHelper = New DescriptionHelper() 'A new description helper is built.
                strRecordString = ms.Item(i).Groups.Item("KeyValue").ToString() 'We get the "KeyValue" out of the regex.
                strRecordString = strRecordString.Substring(0, strRecordString.Length - 1) 'We remove the last few characters, '").' (an extra quote, a ) and a .).
                If (strDescriptionCollection.ContainsKey(strRecordString) = False) Then 'If this hasn't already been recorded, we put it in the collection.
                    If (readDescription(strRecordString) = True) Then 'Attempt to read the description.
                        RecordHelper.description = description
                        strDescriptionName = RecordHelper.SmartNameBuilder(DescriptionStringLengthLimit)
                        descHelper.DescriptionName = DoSearch(strDescriptionName, strDescriptionCollection)
                        descHelper.Description = RecordHelper.BuildDescription(descHelper.DescriptionName)
                        strDescriptionCollection.Add(strRecordString, descHelper)
                    End If
                End If
            Next

            'For Each RecordedDescriptionText As String In strDescriptionCollection.Keys
            Dim RecordedDescriptionText As String = String.Empty
            For counter As Integer = 0 To ms.Count - 1
                RecordedDescriptionText = ms.Item(counter).Groups.Item("KeyValue").ToString()
                RecordedDescriptionText = RecordedDescriptionText.Substring(0, RecordedDescriptionText.Length - 1)
                If (strDescriptionCollection.ContainsKey(RecordedDescriptionText) = True) Then
                    descHelper = strDescriptionCollection.Item(RecordedDescriptionText)
                    If (descHelper.Processed = False) Then
                        descHelper.Processed = True
                        Dim ParentsName As String
                        If (counter > 0) Then
                            RecordedDescriptionTextParent = ms.Item(counter - 1).Groups.Item("KeyValue").ToString()
                            RecordedDescriptionTextParent = RecordedDescriptionTextParent.Substring(0, RecordedDescriptionTextParent.Length - 1)
                            If (strDescriptionCollection.ContainsKey(RecordedDescriptionTextParent) = True) Then
                                descHelperParent = strDescriptionCollection.Item(RecordedDescriptionTextParent)
                                ParentsName = CommentForLanguage & "Parent's name: " & descHelperParent.DescriptionName
                            Else
                                ParentsName = CommentForLanguage & "Parent's name: Unprocessed description."
                            End If
                        Else
                            ParentsName = CommentForLanguage & "Top level object"
                        End If
                        Me.DescriptionTextBox.Text += descHelper.Description & ParentsName & vbNewLine  'put description in text box.
                        Me.RecorderTextBox.Text = Me.RecorderTextBox.Text.Replace("""" & RecordedDescriptionText & """", descHelper.DescriptionName) 'replace old version of descripiton with new description.
                        strDescriptionCollection.Item(RecordedDescriptionText) = descHelper
                    End If
                End If
            Next
        Next
        If (SlickTestDev.CurrentLanguage = CodeTranslator.Languages.CSharp) Then
            Me.RecorderTextBox.Text = Me.RecorderTextBox.Text.Replace("@", "")
        End If
    End Sub

    Private Sub ConvertDescriptionsButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ConvertDescriptionsButton.Click
        ConvertDescription()
    End Sub

    Protected Function DoSearch(ByVal strDescriptionName As String, ByVal strDescriptionCollection As System.Collections.Generic.Dictionary(Of String, DescriptionHelper), Optional ByVal counter As Integer = 1) As String
        Dim tmpstrDescriptionName As String = strDescriptionName.ToLower
        For Each descHelp As DescriptionHelper In strDescriptionCollection.Values
            If (descHelp.DescriptionName.ToLower = tmpstrDescriptionName) Then 'names match, get a new name
                Dim Index As Integer = strDescriptionName.LastIndexOf("_")
                If (Index >= 0) Then
                    If (IsNumeric(strDescriptionName.Substring(Index + 1)) = True) Then
                        strDescriptionName = strDescriptionName.Substring(0, Index)
                    End If
                    Return DoSearch(strDescriptionName & "_" & (counter + 1).ToString(), strDescriptionCollection, counter + 1)
                Else
                    Return DoSearch(strDescriptionName & "_" & (counter + 1).ToString(), strDescriptionCollection, counter + 1)
                End If
            End If
        Next
        Return strDescriptionName
    End Function

    Protected Function readDescription(ByVal desc As String) As Boolean
        Dim index As Integer = desc.IndexOf(":=")
        If (index = -1) Then
            Return False
        End If
        Dim type As String = desc.Substring(0, index).ToLower().Trim()
        Dim tmpValue As String = ""
        index = desc.IndexOf("""")
        Dim index2 As Integer = desc.IndexOf(";;", index + 1)
        If (index2 = -1) Then
            If (desc.Substring(desc.Length - 1) = """") Then
                index2 = desc.Length '+ 1
            Else
                index2 = desc.Length + 1
            End If
        End If
        tmpValue = desc.Substring(index + 2, index2 - (4 + index))
        description.Add(type, tmpValue)
        If (index2 > desc.Length) Then
            index2 = desc.Length
        End If
        If (desc.IndexOf(":=", index2) <> -1) Then
            Return readDescription(desc.Substring(index2 + 2))
        Else
            Return True
        End If
        '"name:=""blah"";;value:="blah"
    End Function

    Private Sub Recorder_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Me.Location = New System.Drawing.Point( _
        System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Width - Me.Size.Width, _
        System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Height - Me.Size.Height)
        Me.RecorderTextBox.SetHighlightStyleByExt(SlickTestDev.LanguageExt)
        Me.DescriptionTextBox.SetHighlightStyleByExt(SlickTestDev.LanguageExt)
    End Sub

    'Private Sub DoubleClickTimer_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DoubleClickTimer.Tick
    '    Me.IsDoubleClick = TriState.UseDefault
    '    Me.DoubleClickTimer.Enabled = False
    '    Console.WriteLine("Entering Tick")
    '    For Each str As String In UpdateText
    '        Console.WriteLine("String: " & str & "||")
    '        Me.RecorderTextBox.Text += str
    '    Next
    '    UpdateText.Clear()
    '    Me.DoubleClickTimer.Enabled = True
    'End Sub

    Private Sub ObjectRefTextBox_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ObjectRefTextBox.TextChanged
        ObjectRecorderID = ObjectRefTextBox.Text
    End Sub

    Private Sub SleepTimeMaskedTextBox_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SleepTimeMaskedTextBox.TextChanged
        Try
            RecorderSleepTime = Convert.ToInt32(Me.SleepTimeMaskedTextBox.Text)
        Catch ex As Exception
            Me.SleepTimeMaskedTextBox.Text = RecorderSleepTime
        End Try

    End Sub

    Private Sub Keys_TypingWindowChanged(ByVal Action As String, ByVal Text As String, ByVal PreviousHwnd As System.IntPtr, ByVal OverrideErrors As Boolean) Handles Keys.TypingWindowChanged
        If (Action <> "") Then
            If (Text <> "") Then
                If (Action.StartsWith(IEWebBrowserString) AndAlso Action.Contains("WinObject(""") = False) Then
                    RecorderTextBox.Text += vbNewLine & ObjectRecorderID & Action & "SetText(""" & Text & """)" & Pause()
                Else
                    RecorderTextBox.Text += vbNewLine & ObjectRecorderID & Action & "TypeKeys(""" & Text & """)" & Pause()
                End If
            End If
        ElseIf (OverrideErrors = True) Then
            RecorderTextBox.Text += vbNewLine & ObjectRecorderID & "SendInput(""" & Text & """)" & Pause()
        End If
    End Sub

    Private Sub XYRecordingCheckBox_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles XYRecordingCheckBox.CheckedChanged
        Me.Keys.RecordXY = Me.XYRecordingCheckBox.Checked
    End Sub

    Private Sub TotalDescriptionLengthMaskedTextBox_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TotalDescriptionLengthMaskedTextBox.Leave
        Dim DescriptionLengthMax As Integer = 25
        Try
            DescriptionLengthMax = Convert.ToInt32(Me.TotalDescriptionLengthMaskedTextBox.Text)
            If (DescriptionLengthMax > 255 OrElse DescriptionLengthMax < 20) Then
                Throw New Exception("The description length was greater than the maximum or less than the minimum.")
            End If

        Catch ex As Exception
            System.Windows.Forms.MessageBox.Show("Description Length invalid.  Error: " & ex.Message, SlickTestDev.MsgBoxTitle)
            DescriptionLengthMax = 25
            Me.TotalDescriptionLengthMaskedTextBox.Text = DescriptionLengthMax.ToString()
        End Try
        DescriptionStringLengthLimit = DescriptionLengthMax
    End Sub
End Class

Public Class DescriptionHelper
    Public Description As String
    Public DescriptionName As String
    Public Processed As Boolean = False
End Class