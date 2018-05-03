Friend Class WebTextBoxAPI
    Inherits WebElementAPI

    Public Sub New(ByVal element As Object)
        MyBase.New(element)
        If (IsTextBox(Me)) Then Return
        Throw New SlickTestAPIException("Element " & Me.ToString() & " is not a textbox.")
    End Sub

    Public Shared Function IsTextBox(ByVal element As WebElementAPI) As Boolean
        If (element Is Nothing) Then Return False
        For Each textTag As String In WebTagDefinitions.TextBoxTags
            If (textTag = element.TagName.ToLowerInvariant()) Then
                If (WebInputAPI.IsInput(element)) Then
                    Dim Type As String = element.Attribute("type")
                    If (Type = WebTagDefinitions.TextType OrElse _
                        Type = WebTagDefinitions.PasswordType) Then Return True
                End If
            Else
                Return True
            End If
        Next
        Return False
    End Function

    Private Function GetTextAreaTag() As String
        Return WebTagDefinitions.TextBoxTags(1)
    End Function

    Private Function IsTextArea() As Boolean
        Return GetTextAreaTag() = Me.TagName.ToLowerInvariant()
    End Function

    Public Function Columns() As Integer
        If (IsTextArea()) Then Return HTMLTextArea.cols
        Return 1
    End Function

    Public Function Rows() As Integer
        If (IsTextArea()) Then Return HTMLTextArea.rows
        Return -1
    End Function

    Public Function [ReadOnly]() As Boolean
        If (IsTextArea()) Then Return HTMLTextArea.readOnly
        Return Not Me.Enabled
    End Function

    Public Function Wrap() As String
        If (IsTextArea()) Then Return HTMLTextArea.wrap
        Return ""
    End Function

    Public Function Disabled() As Boolean
        If (IsTextArea()) Then Return HTMLTextArea.disabled
        Return Not Me.Enabled
    End Function

    Public Function Type() As String
        If (IsTextArea()) Then Return HTMLTextArea.type
        Return ""
    End Function

    Public Sub [Select]()
        If (IsTextArea()) Then
            HTMLTextArea.select()
        Else
            Me.SetActive()
        End If
    End Sub

    Public Sub ClearText()
        Me.SetText("")
    End Sub

    Public Overrides Sub TypeText(ByVal text As String, Optional ByVal typeSpeed As Integer = 30)
        ClearText()
        For Each c As Char In text
            TypeKey(c)
            System.Threading.Thread.Sleep(30)
        Next
    End Sub
    'To Use...
    Protected Friend ReadOnly Property HTMLInputTextElement() As mshtml.IHTMLInputTextElement
        Get
            Return DirectCast(HTMLElement, mshtml.IHTMLInputTextElement)
        End Get
    End Property


    Protected Friend ReadOnly Property HTMLTextContainer() As mshtml.IHTMLTextContainer
        Get
            Return DirectCast(HTMLElement, mshtml.IHTMLTextContainer)
        End Get
    End Property

    Protected Friend ReadOnly Property HTMLTxtRange() As mshtml.IHTMLTxtRange
        Get
            Return DirectCast(HTMLElement, mshtml.IHTMLTxtRange)
        End Get
    End Property


    Protected Friend ReadOnly Property HTMLTextElement() As mshtml.IHTMLTextElement
        Get
            Return DirectCast(HTMLElement, mshtml.IHTMLTextElement)
        End Get
    End Property

    Protected Friend ReadOnly Property HTMLTextArea() As mshtml.IHTMLTextAreaElement
        Get
            Return DirectCast(HTMLElement, mshtml.IHTMLTextAreaElement)
        End Get
    End Property
End Class
