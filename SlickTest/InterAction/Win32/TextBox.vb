Imports System.Windows.Automation
Imports APIControls

''' <summary>
''' A TextBox is just a specialized WinObject, and so it 
''' performs everything a WinObject does.
''' </summary>
''' <remarks></remarks>
Public NotInheritable Class TextBox
    Inherits WinObject

#Region "Constructors"

    ''' <summary>
    ''' Creates a TextBox object.  
    ''' Do not directly create this object, instead use InterAct.[Object].TextBox([desc]).[Method]
    ''' </summary>
    ''' <param name="desc"></param>
    ''' <remarks></remarks>
    Protected Friend Sub New(ByVal desc As String)
        MyBase.New(desc)
    End Sub

    Protected Friend Sub New(ByVal desc As APIControls.IDescription, ByVal descObj As System.Collections.Generic.List(Of APIControls.IDescription)) ', ByVal Values As System.Collections.Specialized.StringCollection, ByVal Names As System.Collections.Specialized.StringCollection)
        MyBase.New(desc, descObj)
    End Sub

    Protected Friend Sub New()
        MyBase.New()
        'this is a do nothing case, to ignore errors...
    End Sub

    ''' <summary>
    ''' Creates a TextBox object.  
    ''' Do not directly create this object, instead use InterAct.[Object].TextBox([desc]).[Method]
    ''' </summary>
    ''' <param name="desc"></param>
    ''' <remarks></remarks>
    Protected Friend Sub New(ByVal desc As APIControls.IDescription)
        MyBase.New(desc)
    End Sub

    ''' <summary>
    ''' Creates a TextBox object.  
    ''' Do not directly create this object, instead use InterAct.[Object].TextBox([desc]).[Method]
    ''' </summary>
    ''' <param name="desc"></param>
    ''' <remarks></remarks>
    Protected Friend Sub New(ByVal desc As System.Collections.Generic.List(Of APIControls.IDescription))
        MyBase.New(desc)
    End Sub

    Protected Friend Sub New(ByVal desc As String, ByVal descObj As System.Collections.Generic.List(Of APIControls.IDescription)) ', ByVal Values As System.Collections.Specialized.StringCollection, ByVal Names As System.Collections.Specialized.StringCollection)
        MyBase.New(desc, descObj)
    End Sub

    Protected Friend Sub New(ByVal win As Window)
        Me.description = win.description
        Me.currentHwnd = win.currentHwnd
        Me.reporter = win.reporter
        Me.currentRectangle = win.currentRectangle
    End Sub
#End Region

    ''' <summary>
    ''' The line that the cursor is currently on in the text box.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetCurrentLineNumber() As Integer
        ExistsWithException()
        Return WindowsFunctions.TextBox.GetLineNumber(Me.currentHwnd) + 1
    End Function

    ''' <summary>
    ''' The total number of lines in the text box.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks>WARNING: This line count includes line created via word wrap.</remarks>
    Public Function GetLineCount() As Integer
        ExistsWithException()
        Return WindowsFunctions.TextBox.GetLineCount(Me.currentHwnd)
    End Function

    ''' <summary>
    ''' The number of characters in a certain line.
    ''' </summary>
    ''' <param name="LineNumber">The line number that you wish to get the line length.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetLineLength(ByVal LineNumber As Integer) As Integer
        ExistsWithException()
        Return WindowsFunctions.TextBox.GetLineLength(Me.currentHwnd, LineNumber - 1)
    End Function

    ''' <summary>
    ''' The text for a given line in a text box.
    ''' </summary>
    ''' <param name="LineNumber">The line number that you wish to get the text.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetLineText(ByVal LineNumber As Integer) As String
        ExistsWithException()
        Return WindowsFunctions.TextBox.GetLineText(Me.currentHwnd, LineNumber - 1)
    End Function

    ''' <summary>
    ''' The cursors location in the text box starting from 0.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetIndexFromCaret() As Integer
        ExistsWithException()
        Return WindowsFunctions.TextBox.GetCaretLineIndex(Me.currentHwnd)
    End Function

    ''' <summary>
    ''' Tests to see if the text box is a multi-line text box or a single line text box.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property IsMultiline() As Boolean
        Get
            Return UIControls.StyleInfo.TextBoxStyle.ContainsValue(Me.GetStyle(), UIControls.StyleInfo.TextBoxStyle.ES_MULTILINE)
        End Get
    End Property

#Region "UI Automation (MS) based"

    Private Const INVALID As String = "System.__ComObject"

    ''' <summary>
    ''' Determines if any of the text in the TextBox is italized.
    ''' </summary>
    ''' <value></value>
    ''' <returns>Returns true if any text is italized.</returns>
    ''' <remarks></remarks>
    Public ReadOnly Property IsItalic() As Boolean
        Get
            Dim targetTextPattern As TextPattern = GetTextPattern()

            Dim textAttribute As Object = targetTextPattern.DocumentRange.GetAttributeValue(TextPattern.IsItalicAttribute)
            If (textAttribute.ToString() = INVALID) Then
                ' Returns MixedAttributeValue if the value of the 
                ' specified attribute varies over the text range. 
                Return True

            ElseIf (Not TypeOf textAttribute Is Boolean) Then
                Throw New SlickTestUIException("IsItalic attribute does not appear to be supported for this object.")
            Else
                Return Convert.ToBoolean(textAttribute)
            End If
        End Get
    End Property

    ''' <summary>
    ''' Determines if any of the text in the TextBox is read only.
    ''' </summary>
    ''' <value></value>
    ''' <returns>Returns true if any text is set to read only.</returns>
    ''' <remarks></remarks>
    Public ReadOnly Property IsReadOnly() As Boolean
        Get
            Dim targetTextPattern As TextPattern = GetTextPattern()

            Dim textAttribute As Object = targetTextPattern.DocumentRange.GetAttributeValue(TextPattern.IsReadOnlyAttribute)
            If (textAttribute.ToString() = INVALID) Then
                ' Returns MixedAttributeValue if the value of the 
                ' specified attribute varies over the text range. 
                Return True
            ElseIf (Not TypeOf textAttribute Is Boolean) Then
                Throw New SlickTestUIException("IsReadOnly attribute does not appear to be supported for this object.")
            Else
                Return Convert.ToBoolean(textAttribute)
            End If
        End Get
    End Property

    ''' <summary>
    ''' Gets the font name from a TextBox.
    ''' </summary>
    ''' <value></value>
    ''' <returns>The font name is returned if it can be determined.</returns>
    ''' <remarks>Returns "Mixed" if their is more than one font in the TextBox</remarks>
    Public ReadOnly Property FontName() As String
        Get
            Dim targetTextPattern As TextPattern = GetTextPattern()

            Dim textAttribute As Object = targetTextPattern.DocumentRange.GetAttributeValue(TextPattern.FontNameAttribute)
            If (textAttribute.ToString() = INVALID) Then
                ' Returns MixedAttributeValue if the value of the 
                ' specified attribute varies over the text range. 
                Return "Mixed"
            ElseIf (Not TypeOf textAttribute Is String) Then
                Throw New SlickTestUIException("FontName attribute does not appear to be supported for this object.")
            Else
                Return textAttribute.ToString()
            End If
        End Get
    End Property

    ''' <summary>
    ''' Gets the font size from a TextBox.
    ''' </summary>
    ''' <value></value>
    ''' <returns>The font size is returned if it can be determined.</returns>
    ''' <remarks>Returns 0.0 if more than one font size is used in the TextBox.</remarks>
    Public ReadOnly Property FontSize() As Double
        Get
            Dim targetTextPattern As TextPattern = GetTextPattern()

            Dim textAttribute As Object = targetTextPattern.DocumentRange.GetAttributeValue(TextPattern.FontSizeAttribute)

            If (textAttribute.ToString() = INVALID) Then
                ' Returns MixedAttributeValue if the value of the 
                ' specified attribute varies over the text range. 
                Return 0.0
            ElseIf (Not TypeOf textAttribute Is Double) Then
                Throw New SlickTestUIException("FontSize attribute does not appear to be supported for this object.")
            Else
                Return Convert.ToDouble(textAttribute)
            End If
        End Get
    End Property

    Private Function GetTextPattern() As TextPattern
        Dim TmpHwnd As System.IntPtr = New IntPtr(Me.Hwnd)
        Try
            Dim targetTextElement As System.Windows.Automation.AutomationElement
            targetTextElement = AutomationElement.FromHandle(TmpHwnd)
            Dim targetTextPattern As TextPattern = _
            DirectCast(targetTextElement.GetCurrentPattern( _
            TextPattern.Pattern), TextPattern)
            If (targetTextPattern Is Nothing) Then
                ' Target control doesn't support TextPattern.
                Throw New SlickTestAPIException("Object does not appear to be a valid TextBox.")
            End If
            Return targetTextPattern
        Catch ex As Exception
            Throw New SlickTestUIException("An unexpected error occurred when attempting to get attributes from a TextBox.", ex)
        End Try


    End Function

#End Region


    'Public Function SetCaretIndex(ByVal Index As Integer) As Integer
    '    If (ExistsWithException() = False) Then
    '        Return -1
    '    End If
    '    WindowsFunctions.SetLineIndex(Me.currentHwnd, Index)
    '    Return 1
    'End Function


#Region "Win32 specific"

    ''' <summary>
    ''' Tests to see if the text box is a passworded text box.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property IsPasswordBox() As Boolean
        Get
            ExistsWithException()
            UIControls.StyleInfo.TextBoxStyle.ContainsValue(Me.GetStyle(), UIControls.StyleInfo.TextBoxStyle.ES_PASSWORD)
        End Get
    End Property

#End Region

    ''' <summary>
    ''' Determines if the object is a TextBox.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function IsTextBox() As Boolean
        Return WindowsFunctions.TextBox.IsTextBox(New IntPtr(Me.Hwnd()))
    End Function


End Class

#Region "Old code"
'' <summary>
'' Appends the current text with additional text given.
'' </summary>
'' <param name="text">The additional text to append to the end of the current text.</param>
'' <returns></returns>
'' <remarks>Currently a test function.  This may not remain in the final version</remarks>
'Public Function AppendText(ByVal text As String) As Boolean
'    If (ExistsWithException() = False) Then
'        Return False
'    End If
'    Try
'        WindowsFunctions.AppendText(text, currentHwnd)
'    Catch ex As Exception
'        Report(UIControls.Report.Fail, "Failed to perform requested action", _
'            "Error: " & ex.Message)
'        If (IgnoreInternalErrors = False) Then
'            Throw
'        End If
'        Return False
'    End Try
'    Return True
'End Function

#End Region