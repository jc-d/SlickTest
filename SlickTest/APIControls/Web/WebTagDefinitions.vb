Public Class WebTagDefinitions

    Public Shared Function GetWebType(ByVal WebTag As String) As String
        Return GetTagSmart(WebTag, "")
    End Function

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="WebTag"></param>
    ''' <param name="WebType">Type is only used for input tag</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function GetTagSmart(ByVal WebTag As String, ByVal WebType As String) As String
        Dim tag As String = WebTag.ToLowerInvariant()

        For Each t As String In APIControls.WebTagDefinitions.TableCellTags
            If (t = tag) Then Return "WebTableCell"
        Next
        For Each t As String In APIControls.WebTagDefinitions.TableRowTags
            If (t = tag) Then Return "WebTableRow"
        Next
        For Each t As String In APIControls.WebTagDefinitions.TableTags
            If (t = tag) Then Return "WebTable"
        Next
        For Each t As String In APIControls.WebTagDefinitions.InputTags
            If (t = tag) Then
                Select Case WebType.ToLowerInvariant()
                    Case WebTagDefinitions.ButtonType, WebTagDefinitions.SubmitButtonType, WebTagDefinitions.ResetButtonType
                        Return "WebButton"
                    Case WebTagDefinitions.CheckboxType
                        Return "WebComboBox"
                    Case WebTagDefinitions.ImageType
                        Return "WebImage"
                    Case WebTagDefinitions.RadioButtonType
                        Return "WebRadioButton"
                    Case WebTagDefinitions.PasswordType, WebTagDefinitions.TextType
                        Return "WebTextBox"
                    Case Else
                        Return "WebGenericInput"
                End Select
            End If
        Next
        For Each t As String In APIControls.WebTagDefinitions.SpanTags
            If (t = tag) Then Return "WebSpan"
        Next
        For Each t As String In APIControls.WebTagDefinitions.DivTags
            If (t = tag) Then Return "WebDiv"
        Next
        For Each t As String In APIControls.WebTagDefinitions.LinkTags
            If (t = tag) Then Return "WebLink"
        Next
        For Each t As String In APIControls.WebTagDefinitions.ListItemTags
            If (t = tag) Then Return "WebListItem"
        Next
        For Each t As String In APIControls.WebTagDefinitions.ListTags
            If (t = tag) Then Return "WebList"
        Next
        For Each t As String In APIControls.WebTagDefinitions.DropdownComboBoxTags
            If (t = tag) Then Return "WebComboBox"
        Next
        For Each t As String In APIControls.WebTagDefinitions.LabelTags
            If (t = tag) Then Return "WebLabel"
        Next
        For Each t As String In APIControls.WebTagDefinitions.ImageTags
            If (t = tag) Then Return "WebImage"
        Next
        For Each t As String In APIControls.WebTagDefinitions.FormTags
            If (t = tag) Then Return "WebForm"
        Next
        For Each t As String In APIControls.WebTagDefinitions.ButtonTags
            If (t = tag) Then Return "WebButton"
        Next
        For Each t As String In APIControls.WebTagDefinitions.TextBoxTags
            If (t = tag) Then Return "WebTextBox"
        Next
        'For Each t As String In APIControls.WebTagDefinitions.AreaTags
        '    If (t = tag) Then Return "WebArea"
        'Next
        Return "WebElement"
    End Function

    Private Shared Table As New System.Collections.Generic.List(Of String)(9)
    Public Shared ReadOnly Property TableTags() As System.Collections.Generic.List(Of String)
        Get
            If (Table.Count = 0) Then
                Table.Add("table")
                'Table.Add("col")
                'Table.Add("colgroup")
                Table.Add("thead")
                Table.Add("tfoot")
                Table.Add("tbody")
            End If
            Return Table
        End Get
    End Property

    Private Shared TableRow As New System.Collections.Generic.List(Of String)(1)
    Public Shared ReadOnly Property TableRowTags() As System.Collections.Generic.List(Of String)
        Get
            If (TableRow.Count = 0) Then
                TableRow.Add("tr")
                TableRow.Add("caption")
            End If
            Return TableRow
        End Get
    End Property

    Private Shared TableCell As New System.Collections.Generic.List(Of String)(2)
    Public Shared ReadOnly Property TableCellTags() As System.Collections.Generic.List(Of String)
        Get
            If (TableCell.Count = 0) Then
                TableCell.Add("td")
                TableCell.Add("th")
            End If
            Return TableCell
        End Get
    End Property

    Private Shared DropdownComboBox As New System.Collections.Generic.List(Of String)(1)
    Public Shared ReadOnly Property DropdownComboBoxTags() As System.Collections.Generic.List(Of String)
        Get
            If (DropdownComboBox.Count = 0) Then
                DropdownComboBox.Add("select")
            End If
            Return DropdownComboBox
        End Get
    End Property

    Private Shared TextBox As New System.Collections.Generic.List(Of String)(2)
    Public Shared ReadOnly Property TextBoxTags() As System.Collections.Generic.List(Of String)
        Get
            If (TextBox.Count = 0) Then
                TextBox.Add("input")
                TextBox.Add("textarea")
            End If
            Return TextBox
        End Get
    End Property

    Public Const RadioButtonType As String = "radio"
    Public Const CheckboxType As String = "checkbox"
    Public Const ButtonType As String = "button"
    Public Const SubmitButtonType As String = "submit"
    Public Const ImageType As String = "image"
    Public Const ResetButtonType As String = "reset"
    Public Const TextType As String = "text"
    Public Const PasswordType As String = "password"
    Public Const FileUploadType As String = "file"

    Private Shared Input As New System.Collections.Generic.List(Of String)(1)
    Public Shared ReadOnly Property InputTags() As System.Collections.Generic.List(Of String)
        Get
            If (Input.Count = 0) Then
                Input.Add("input")
            End If
            Return Input
        End Get
    End Property

    Private Shared Div As New System.Collections.Generic.List(Of String)(1)
    Public Shared ReadOnly Property DivTags() As System.Collections.Generic.List(Of String)
        Get
            If (Div.Count = 0) Then
                Div.Add("div")
            End If
            Return Div
        End Get
    End Property

    Private Shared Span As New System.Collections.Generic.List(Of String)(1)
    Public Shared ReadOnly Property SpanTags() As System.Collections.Generic.List(Of String)
        Get
            If (Span.Count = 0) Then
                Span.Add("span")
            End If
            Return Span
        End Get
    End Property


    Private Shared List As New System.Collections.Generic.List(Of String)(1)
    Public Shared ReadOnly Property ListTags() As System.Collections.Generic.List(Of String)
        Get
            If (List.Count = 0) Then
                List.Add("ul")
            End If
            Return List
        End Get
    End Property

    Private Shared ListItems As New System.Collections.Generic.List(Of String)(3)
    Public Shared ReadOnly Property ListItemTags() As System.Collections.Generic.List(Of String)
        Get
            If (ListItems.Count = 0) Then
                ListItems.Add("li")
                ListItems.Add("ul")
                ListItems.Add("ol")
            End If
            Return ListItems
        End Get
    End Property

    Private Shared Link As New System.Collections.Generic.List(Of String)(1)
    Public Shared ReadOnly Property LinkTags() As System.Collections.Generic.List(Of String)
        Get
            If (Link.Count = 0) Then
                Link.Add("a")
            End If
            Return Link
        End Get
    End Property

    Private Shared Image As New System.Collections.Generic.List(Of String)(2)
    Public Shared ReadOnly Property ImageTags() As System.Collections.Generic.List(Of String)
        Get
            If (Image.Count = 0) Then
                Image.Add("img")
                Image.Add("input")
            End If
            Return Image
        End Get
    End Property

    'Private Shared Area As New System.Collections.Generic.List(Of String)(1)
    'Public Shared ReadOnly Property AreaTags() As System.Collections.Generic.List(Of String)
    '    Get
    '        If (Area.Count = 0) Then
    '            Area.Add("area")
    '        End If
    '        Return Area
    '    End Get
    'End Property

    Private Shared Form As New System.Collections.Generic.List(Of String)(1)
    Public Shared ReadOnly Property FormTags() As System.Collections.Generic.List(Of String)
        Get
            If (Form.Count = 0) Then
                Form.Add("area")
            End If
            Return Form
        End Get
    End Property

    Private Shared Label As New System.Collections.Generic.List(Of String)(1)
    Public Shared ReadOnly Property LabelTags() As System.Collections.Generic.List(Of String)
        Get
            If (Label.Count = 0) Then
                Label.Add("label")
            End If
            Return Label
        End Get
    End Property

    Private Shared Button As New System.Collections.Generic.List(Of String)(1)
    Public Shared ReadOnly Property ButtonTags() As System.Collections.Generic.List(Of String)
        Get
            If (Button.Count = 0) Then
                Button.Add("button")
                Button.Add("input")
            End If
            Return Button
        End Get
    End Property

    'Paragraph??

End Class
