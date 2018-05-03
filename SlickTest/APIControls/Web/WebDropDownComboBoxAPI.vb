Friend Class WebDropDownComboBoxAPI
    Inherits WebElementAPI

    Public Sub New(ByVal element As Object)
        MyBase.New(element)
        If (IsDropDownComboBox(Me)) Then Return
        Throw New SlickTestAPIException("Element " & Me.ToString() & " is not a Combobox.")
    End Sub

    Public Shared Function IsDropDownComboBox(ByVal element As WebElementAPI) As Boolean
        If (element Is Nothing) Then Return False
        For Each tableTag As String In WebTagDefinitions.DropdownComboBoxTags
            If (tableTag = element.TagName.ToLowerInvariant()) Then
                Return True
            End If
        Next
        Return False
    End Function

    Protected Friend ReadOnly Property HTMLSelect() As mshtml.IHTMLSelectElement
        Get
            Return DirectCast(HTMLElement, mshtml.IHTMLSelectElement)
        End Get
    End Property

    Protected Friend ReadOnly Property HTMLSelect2() As mshtml.IHTMLSelectElement2
        Get
            Return DirectCast(HTMLElement, mshtml.IHTMLSelectElement2)
        End Get
    End Property

    Protected Friend ReadOnly Property HTMLSelect4() As mshtml.IHTMLSelectElement4
        Get
            Return DirectCast(HTMLElement, mshtml.IHTMLSelectElement4)
        End Get
    End Property

    'Protected Friend ReadOnly Property HTMLSelect() As mshtml.IHTMLSelectElement
    '    Get
    '        Return DirectCast(HTMLElement, mshtml.IHTMLSelectElement)
    '    End Get
    'End Property

    Public Function IsDisabled() As Boolean
        Return HTMLSelect.disabled
    End Function

    Public Function Length() As Integer
        Return HTMLSelect.length
    End Function

    Public Function AllowMultiSelect() As Boolean
        Return HTMLSelect.multiple
    End Function

    Public Function SelectedIndex() As Integer
        Return HTMLSelect.selectedIndex
    End Function

    Public Function Size() As Integer
        Return HTMLSelect.size
    End Function

    Public Function Type() As String
        Return HTMLSelect.type
    End Function

    Public Function SelectValue() As String
        Return HTMLSelect.value
    End Function

    Public Function GetItem(ByVal IdOrName As String) As WebElementAPI
        Return New WebElementAPI(HTMLSelect4.namedItem(IdOrName))
    End Function

    Public Function Item(ByVal index As Integer) As WebElementAPI
        Return New WebElementAPI(HTMLSelect.item(, index))
    End Function

End Class
