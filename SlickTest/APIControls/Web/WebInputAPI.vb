Friend Class WebInputAPI
    Inherits WebElementAPI

    Public Sub New(ByVal element As Object)
        MyBase.New(element)
        If (IsInput(Me)) Then Return
        Throw New SlickTestAPIException("Element " & Me.ToString() & " is not a Input.")
    End Sub

    Public Shared Function IsInput(ByVal element As WebElementAPI) As Boolean
        If (element Is Nothing) Then Return False
        For Each tableTag As String In WebTagDefinitions.InputTags
            If (tableTag = element.TagName.ToLowerInvariant()) Then
                Return True
            End If
        Next
        Return False
    End Function

    Protected Friend ReadOnly Property HTMLInputElement() As mshtml.IHTMLInputElement
        Get
            Return DirectCast(HTMLElement, mshtml.IHTMLInputElement)
        End Get
    End Property

    Protected Friend ReadOnly Property HTMLInputElement2() As mshtml.IHTMLInputElement2
        Get
            Return DirectCast(HTMLElement, mshtml.IHTMLInputElement2)
        End Get
    End Property

    Public Function IsChecked() As Boolean
        Return HTMLInputElement.checked
    End Function

    Public Function IsComplete() As Boolean
        Return HTMLInputElement.complete
    End Function

    Public Function IsDefaultChecked() As Boolean
        Return HTMLInputElement.defaultChecked
    End Function

    Public Function IsDisabled() As Boolean
        Return HTMLInputElement.disabled
    End Function
    ''' <summary>
    ''' When the indeterminate state is used for a checkbox, it behaves as a 
    ''' three-state control. A checkbox cannot be changed to indeterminate 
    ''' state through the user interface, this state can only be set 
    ''' from scripts. This state can be used to force the user to check or 
    ''' uncheck a checkbox.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function IsIndeterminate() As Boolean
        Return HTMLInputElement.indeterminate
    End Function

    Public Function IsReadOnly() As Boolean
        Return HTMLInputElement.readOnly
    End Function

    'same as checked, not needed.
    'Public Function Status() As Boolean
    '    Return HTMLInputElement.status
    'End Function

    Public Function Accept() As String
        Return HTMLInputElement2.accept
    End Function

    Public Overridable Function UseMap() As String
        Return HTMLInputElement2.useMap
    End Function

    Public Overridable Function Align() As String
        Return HTMLInputElement.align
    End Function

    Public Overridable Function Alt() As String
        Return HTMLInputElement.alt
    End Function

    Public Function DynSrc() As String
        Return HTMLInputElement.dynsrc
    End Function

    Public Function LowResolutionSrc() As String
        Return HTMLInputElement.lowsrc
    End Function

    Public Function ReadyState() As String
        Return HTMLInputElement.readyState
    End Function

    Public Function Src() As String
        Return HTMLInputElement.src
    End Function

    Public Function Start() As String
        Return HTMLInputElement.Start
    End Function

    Public Function Type() As String
        Return HTMLInputElement.type
    End Function

    Public Function InputValue() As String
        Return HTMLInputElement.value
    End Function

    Public Function Vrml() As String
        Return HTMLInputElement.vrml
    End Function

    Public Function InputHeight() As Integer
        Return HTMLInputElement.height
    End Function

    Public Function InputWidth() As Integer
        Return HTMLInputElement.width
    End Function

    Public Function HSpace() As Integer
        Return HTMLInputElement.hspace
    End Function

    Public Function MaxLength() As Integer
        Return HTMLInputElement.maxLength
    End Function

    Public Function Size() As Integer
        Return HTMLInputElement.size
    End Function

    Public Overridable Function VSpace() As Integer
        Return HTMLInputElement.vspace
    End Function

    Public Function Border() As Object
        Return HTMLInputElement.border
    End Function

    Public Sub SetSelect() 'for radio buttons?
        HTMLInputElement.select()
    End Sub

    Public Function SetChecked(ByVal CheckStateToSet As Boolean) As Boolean
        Try
            If (Me.IsReadOnly()) Then Return False
        Catch ex As Exception
        End Try
        If (CheckStateToSet) Then
            If (Not IsChecked()) Then
                Me.Click()
                Return True
            End If
        Else
            If (IsChecked()) Then
                Me.Click()
                Return True
            End If
        End If
        Return False
    End Function

    Public Function GetInputType() As String
        Return Me.Attribute("type").ToLowerInvariant()
    End Function
End Class
