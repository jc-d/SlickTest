Friend Class Element
    Implements IElement
    Private HTMLElement As mshtml.IHTMLElement
    Private ElementObject As Object
    Private Shared eCollect As mshtml.IHTMLElementCollection

    Friend ReadOnly Property htmlElement2() As mshtml.IHTMLElement2
        Get
            Return DirectCast(HTMLElement, mshtml.IHTMLElement2)
        End Get
    End Property

    Friend ReadOnly Property htmlElement3() As mshtml.IHTMLElement3
        Get
            Return DirectCast(HTMLElement, mshtml.IHTMLElement3)
        End Get
    End Property

    Friend Sub New(ByVal element As Object)
        HTMLElement = DirectCast(element, mshtml.IHTMLElement)
        ElementObject = element
    End Sub

    Public Sub Click()
        Me.HTMLElement.click()
    End Sub

    ''' <summary>
    ''' Gets the inner HTML of this element.
    ''' </summary>
    ''' <value>The inner HTML.</value>
    Public ReadOnly Property InnerHtml() As String Implements IElement.InnerHTML
        Get
            Dim t As String = HTMLElement.innerHTML
            If (t Is Nothing) Then Return ""
            Return t
        End Get
    End Property

    ''' <summary>
    ''' Gets the outer text.
    ''' </summary>
    ''' <value>The outer text.</value>
    Public ReadOnly Property OuterText() As String Implements IElement.OuterText
        Get
            Dim t As String = HTMLElement.outerText
            If (t Is Nothing) Then Return ""
            Return t
        End Get
    End Property

    ''' <summary>
    ''' Gets the outer HTML.
    ''' </summary>
    ''' <value>The outer HTML.</value>
    Public ReadOnly Property OuterHtml() As String Implements IElement.OuterHTML
        Get
            Dim t As String = HTMLElement.outerHTML
            If (t Is Nothing) Then Return ""
            Return t
        End Get
    End Property

    ''' <summary>
    ''' Gets the tag name of this element.
    ''' </summary>
    ''' <value>The name of the tag.</value>
    Public ReadOnly Property TagName() As String Implements IElement.TagName
        Get
            Try
                Dim t As String = HTMLElement.tagName
                If (t Is Nothing) Then Return ""
                Return t
            Catch ex As Exception
                Return ""
            End Try
        End Get
    End Property

    ''' <summary>
    ''' Gets the title.
    ''' </summary>
    ''' <value>The title.</value>
    Public ReadOnly Property Title() As String Implements IElement.Title
        Get
            Dim t As String = HTMLElement.title
            If (t Is Nothing) Then Return ""
            Return t
        End Get
    End Property

    ''' <summary>
    ''' Gets the name of the stylesheet class assigned to this element (if any).
    ''' </summary>
    ''' <value>The name of the class.</value>
    Public ReadOnly Property ClassName() As String
        Get
            Dim t As String = HTMLElement.className
            If (t Is Nothing) Then Return ""
            Return t
        End Get
    End Property

    ''' <summary>
    ''' Gets a value indicating whether this <see cref="Element"/> is enabled.
    ''' </summary>
    ''' <value><c>true</c> if enabled; otherwise, <c>false</c>.</value>
    Public ReadOnly Property Enabled() As Boolean Implements IElement.Enabled
        Get
            Return Not htmlElement3.disabled
        End Get
    End Property

    ''' <summary>
    ''' Gets the id of this element as specified in the HTML.
    ''' </summary>
    ''' <value>The id.</value>
    Public ReadOnly Property Id() As String Implements IElement.Id
        Get
            Dim t As String = HTMLElement.id
            If (t Is Nothing) Then Return ""
            Return t

        End Get
    End Property


    ''' <summary>
    ''' Gets the attribute of this element as specified in the HTML.
    ''' </summary>
    ''' <value>The attribute.</value>
    Public ReadOnly Property Attribute(ByVal AttributeName As String) As String Implements IElement.Attribute
        Get
            Try
                Dim t As String = HTMLElement.getAttribute(AttributeName).ToString
                If (t Is Nothing) Then Return ""
                Return t
            Catch ex As Exception

            End Try
            Return ""
        End Get
    End Property

    ''' <summary>
    ''' Gets the Width of the element specified in the HTML.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property Width() As Integer Implements IElement.Width
        Get
            Return HTMLElement.offsetWidth
        End Get
    End Property

    ''' <summary>
    ''' Gets the Height of the element specified in the HTML.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property Height() As Integer Implements IElement.Height
        Get
            Return HTMLElement.offsetHeight
        End Get
    End Property

    ''' <summary>
    ''' Gets the Top of the element specified in the HTML.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property Top() As Integer Implements IElement.Top
        Get
            Return HTMLElement.offsetTop
        End Get
    End Property

    ''' <summary>
    ''' Gets the Bottom of the element specified in the HTML.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property Bottom() As Integer Implements IElement.Bottom
        Get
            Return HTMLElement.offsetTop + HTMLElement.offsetHeight
        End Get
    End Property

    ''' <summary>
    ''' Gets the Left of the element specified in the HTML.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property Left() As Integer Implements IElement.Left
        Get
            Return HTMLElement.offsetLeft
        End Get
    End Property

    ''' <summary>
    ''' Gets the Right of the element specified in the HTML.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property Right() As Integer Implements IElement.Right
        Get
            Return HTMLElement.offsetLeft + HTMLElement.offsetWidth
        End Get
    End Property

    ''' <summary>
    ''' Gets the innertext of this element (and the innertext of all the elements contained
    ''' in this element).
    ''' </summary>
    ''' <value>The innertext.</value>
    Public Overridable ReadOnly Property Text() As String Implements IElement.Text
        Get
            Dim t As String = HTMLElement.innerText
            If (t Is Nothing) Then Return ""
            Return t
        End Get
    End Property

    Public Function GetParent() As Element
        Try
            If (Me.HTMLElement.parentElement Is Nothing) Then Return Nothing
            Dim e As New Element(Me.HTMLElement.parentElement)
            Return e
        Catch ex As Exception
            'something bad happened.
        End Try
        Return Nothing
    End Function
    Public Function GetAllChildren() As Element()
        Try
            If (Me.htmlElement2.canHaveChildren = False) Then Return Nothing
            eCollect = DirectCast(Me.HTMLElement.all, mshtml.IHTMLElementCollection)
            If (eCollect.length = 0) Then Return Nothing
            Dim e(eCollect.length - 1) As Element
            For i As Integer = 0 To eCollect.length - 1
                e(i) = New Element(DirectCast(eCollect.item(i), mshtml.IHTMLElement))
            Next
            Return e
        Catch ex As Exception
            'something bad happened.
        End Try
        Return Nothing
    End Function

    Protected Friend Function GetAllChildrenVElement() As VElement()
        Try
            If (Me.htmlElement2.canHaveChildren = False) Then Return Nothing
            eCollect = DirectCast(Me.HTMLElement.all, mshtml.IHTMLElementCollection)
            If (eCollect.length = 0) Then Return Nothing
            Dim e(eCollect.length - 1) As VElement
            For i As Integer = 0 To eCollect.length - 1
                e(i) = New VElement(DirectCast(eCollect.item(i), mshtml.IHTMLElement))
            Next
            Return e
        Catch ex As Exception
            'something bad happened.
        End Try
        Return Nothing
    End Function

    Public Function ActualX() As Integer
        Dim x As Integer = 0
        x = Me.Left
        Dim e As Element = Me.GetParent()
        While (e IsNot Nothing)
            x += e.Left
            e = e.GetParent()
        End While
        Return x
    End Function
    Public Function ActualY() As Integer
        Dim y As Integer = 0
        y = Me.Top
        Dim e As Element = Me.GetParent()
        While (e IsNot Nothing)
            y += e.Top
            e = e.GetParent()
        End While
        Return y
    End Function

    Public Function GetChildren() As Element()
        Try
            'System.Console.WriteLine("***Getting Element Children at: " & Now.TimeOfDay.ToString())
            'System.Console.WriteLine("***+Getting Element Children Collection at: " & Now.TimeOfDay.ToString())
            If (Me.htmlElement2.canHaveChildren = False) Then Return Nothing
            eCollect = DirectCast(Me.HTMLElement.children, mshtml.IHTMLElementCollection)
            'System.Console.WriteLine("***+Getting Element Children Collection completed at: " & Now.TimeOfDay.ToString())
            If (eCollect.length = 0) Then Return Nothing
            Dim e(eCollect.length - 1) As Element
            For i As Integer = 0 To eCollect.length - 1
                e(i) = New Element(DirectCast(eCollect.item(i), mshtml.IHTMLElement))
            Next
            'System.Console.WriteLine("***Getting Element Children completed at: " & Now.TimeOfDay.ToString())
            Return e
        Catch ex As Exception
            'something bad happened.
        End Try
        Return Nothing
    End Function


End Class