Friend Class WebElementAPI
    Implements IElement
    Protected Friend Property HTMLElement() As mshtml.IHTMLElement
        Get
            Return InternalHTMLElement
        End Get
        Set(ByVal value As mshtml.IHTMLElement)
            InternalHTMLElement = value
        End Set
    End Property

    Protected Friend ReadOnly Property SubElementsCollection() As mshtml.IHTMLElementCollection
        Get
            Return DirectCast(HTMLElement.all, mshtml.IHTMLElementCollection)
        End Get
    End Property

    Private InternalHTMLElement As mshtml.IHTMLElement
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

    Friend ReadOnly Property DOMNode() As mshtml.IHTMLDOMNode
        Get
            Return DirectCast(InternalHTMLElement, mshtml.IHTMLDOMNode)
        End Get
    End Property

    Friend ReadOnly Property DOMNode2() As mshtml.IHTMLDOMNode2
        Get
            Return DirectCast(InternalHTMLElement, mshtml.IHTMLDOMNode2)
        End Get
    End Property

    ''' <summary>
    ''' Takes IHTMLElement
    ''' </summary>
    ''' <param name="element">Takes IHTMLElement</param>
    ''' <remarks></remarks>
    Friend Sub New(ByVal element As Object)
        If (TypeOf element Is WebElementAPI) Then
            HTMLElement = DirectCast(element, WebElementAPI).HTMLElement
        Else
            HTMLElement = DirectCast(element, mshtml.IHTMLElement)
        End If
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
            Dim t As String = HTMLElement.parentElement.innerHTML
            If (t Is Nothing) Then Return ""
            Return t
        End Get
    End Property

    ''' <summary>
    ''' Returns the index of the element.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks>html is 0, etc.</remarks>
    Public ReadOnly Property Index() As Integer Implements IElement.Index
        Get
            Return Me.HTMLElement.sourceIndex
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
    ''' Gets a value indicating whether this Element is enabled.
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
    ''' Gets the language that is used in the  as specified in the HTML.
    ''' The parser uses this property to determine how to display language-specific
    ''' choices for quotations, numbers, and so on.
    ''' </summary>
    ''' <value></value>
    Public ReadOnly Property LanguageToUse() As String
        Get
            Dim tmpElement As mshtml.IHTMLElement = HTMLElement
            Dim t As String
            Do
                t = tmpElement.lang
                If (Not t Is Nothing) Then Return t
                tmpElement = tmpElement.parentElement
            Loop While (Not tmpElement Is Nothing)
            Return String.Empty
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
    ''' in this element).  If no text is found, use the value if one exists
    ''' </summary>
    ''' <value>The innertext or value.</value>
    Public Overridable ReadOnly Property Text() As String Implements IElement.Text
        Get
            Dim t As String = HTMLElement.innerText
            If (t Is Nothing) Then
                t = Value
                If (t Is Nothing) Then Return ""
            End If
            Return t
        End Get
    End Property

    ''' <summary>
    ''' Sets the text directly but does not simulate key presses.  All previously
    ''' entered text is lost.
    ''' </summary>
    ''' <param name="text">The text to set.</param>
    ''' <remarks></remarks>
    Public Sub SetText(ByVal Text As String)
        Me.HTMLElement.innerText = Text
    End Sub

    ''' <summary>
    ''' Sets the text directly but does not simulate key presses.  All previously
    ''' entered text is kept.
    ''' </summary>
    ''' <param name="text">The text to set.</param>
    ''' <remarks></remarks>
    Public Sub AppendText(ByVal Text As String)
        SetText(Me.Text & Text)
    End Sub

    Public Function GetParent() As WebElementAPI
        Try
            If (Me.HTMLElement.parentElement Is Nothing) Then Return Nothing
            Dim e As New WebElementAPI(Me.HTMLElement.parentElement)
            Return e
        Catch ex As Exception
            'something bad happened.
        End Try
        Return Nothing
    End Function

    ''' <summary>
    ''' Scrolls object into view.
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub ScrollIntoView()
        Me.HTMLElement.scrollIntoView(True)
    End Sub

    Protected Friend Shared Function ConvertIHtmlElementCollectionToElementArray(ByVal ElementCollection As Object) As WebElementAPI()
        Dim col As mshtml.IHTMLElementCollection = DirectCast(ElementCollection, mshtml.IHTMLElementCollection)
        If (col.length = 0) Then Return Nothing

        Dim e(col.length - 1) As WebElementAPI
        For i As Integer = 0 To col.length - 1
            e(i) = New WebElementAPI(DirectCast(col.item(i), mshtml.IHTMLElement))
        Next
        Return e
    End Function

    Public Function GetAllChildren() As WebElementAPI()
        Try
            If (Me.htmlElement2.canHaveChildren = False) Then Return Nothing
            Return ConvertIHtmlElementCollectionToElementArray(Me.HTMLElement.all)
        Catch ex As Exception
            'something bad happened.
        End Try
        Return Nothing
    End Function

    Public Function GetChildrenCount() As Integer
        Return SubElementsCollection.length
    End Function

    Public Function GetElementsByTag(ByVal tag As String) As WebElementAPI()
        Return ConvertIHtmlElementCollectionToElementArray(htmlElement2.getElementsByTagName(tag))
    End Function

    Public Function GetElementsByName(ByVal name As String) As WebElementAPI()
        Dim l As New System.Collections.Generic.List(Of WebElementAPI)

        For Each e As mshtml.IHTMLElement In SubElementsCollection()
            Try

                Dim tmpName As Object = e.getAttribute("name")
                If (tmpName.ToString() = name) Then
                    l.Add(New WebElementAPI(e))
                End If
            Catch ex As Exception
            End Try
        Next
        Return l.ToArray()
    End Function

    Public Function ActualX() As Integer
        Dim x As Integer = 0
        x = Me.Left
        Dim e As WebElementAPI = Me.GetParent()
        While (e IsNot Nothing)
            x += e.Left
            e = e.GetParent()
        End While
        Return x
    End Function

    Public Function ActualY() As Integer
        Dim y As Integer = 0
        y = Me.Top
        Dim e As WebElementAPI = Me.GetParent()
        While (e IsNot Nothing)
            y += e.Top
            e = e.GetParent()
        End While
        Return y
    End Function

    Public Function GetChildren() As WebElementAPI()
        Try
            If (Me.htmlElement2.canHaveChildren = False) Then Return Nothing
            eCollect = DirectCast(Me.HTMLElement.children, mshtml.IHTMLElementCollection)
            If (eCollect.length = 0) Then Return Nothing
            Dim e(eCollect.length - 1) As WebElementAPI
            For i As Integer = 0 To eCollect.length - 1
                e(i) = New WebElementAPI(DirectCast(eCollect.item(i), mshtml.IHTMLElement))
            Next
            Return e
        Catch ex As Exception
            'something bad happened.
        End Try
        Return Nothing
    End Function

    Public ReadOnly Property Name() As String Implements IElement.Name
        Get
            Try
                Return Me.Attribute("name")
            Catch ex As Exception
                Return String.Empty
            End Try
        End Get
    End Property

    Public ReadOnly Property Value() As String Implements IElement.Value
        Get
            Try
                Return Me.Attribute("value")
            Catch ex As Exception
                Return String.Empty
            End Try
        End Get
    End Property

    Public Sub SetValue(ByVal Name As String, ByVal Value As String)
        Me.HTMLElement.setAttribute(Name, Value)
    End Sub

    Public Function NextElement() As WebElementAPI
        If (DOMNode.nextSibling Is Nothing) Then Return Nothing
        Try
            Dim e As New WebElementAPI(DOMNode.nextSibling)
            Return e
        Catch ex As Exception
            'Me.TabIndex()'Find by tab index?
            Return Nothing
        End Try
    End Function

    Public Function PreviousElement() As WebElementAPI
        If (DOMNode.previousSibling Is Nothing) Then Return Nothing
        Try
            Dim e As New WebElementAPI(DOMNode.previousSibling)
            Return e
        Catch ex As Exception
            'Me.TabIndex()'Find by tab index?
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' Creates a mostly full description.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks>If name or id are blank they are automatically excluded</remarks>
    Public Function CreateFullDescription() As APIControls.Description
        Dim d As New APIControls.Description()
        d.Add(Description.DescriptionData.Bottom, Me.Bottom.ToString())
        d.Add(Description.DescriptionData.Top, Me.Top.ToString())
        d.Add(Description.DescriptionData.Left, Me.Left.ToString())
        d.Add(Description.DescriptionData.Right, Me.Right.ToString())
        d.Add(Description.DescriptionData.Height, Me.Height.ToString())
        d.Add(Description.DescriptionData.Width, Me.Width.ToString())
        If (Me.Name <> "") Then d.Add(Description.DescriptionData.Name, Me.Name)
        d.Add(Description.DescriptionData.WebValue, Me.Value)
        If (Me.Id <> "") Then d.Add(Description.DescriptionData.WebID, Me.Id)
        d.Add(Description.DescriptionData.WebInnerHTML, Me.InnerHtml)
        d.Add(Description.DescriptionData.WebOuterHTML, Me.OuterHtml)
        d.Add(Description.DescriptionData.WebTag, Me.TagName)
        d.Add(Description.DescriptionData.WebText, Me.Text)
        d.Add(Description.DescriptionData.Index, Me.Index.ToString())
        d.Add(Description.DescriptionData.ControlType, Me.GetWebType())
        Return d
    End Function

    ''' <summary>
    ''' Creates a the most useful parts of a full description.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks>If name or id are blank they are automatically excluded</remarks>
    Public Function CreateReasonableDescription() As APIControls.Description
        Dim d As New APIControls.Description()
        If (Me.Name <> "") Then d.Add(Description.DescriptionData.Name, Me.Name)
        d.Add(Description.DescriptionData.WebValue, Me.Value)
        If (Me.Id <> "") Then d.Add(Description.DescriptionData.WebID, Me.Id)
        d.Add(Description.DescriptionData.WebTag, Me.TagName)
        d.Add(Description.DescriptionData.Index, Me.Index.ToString())
        Return d
    End Function

    Public Function AccessKey() As String
        Return htmlElement2.accessKey
    End Function

    Public Function IsTabStop() As Boolean
        Dim retVal As Boolean = TabIndex() < 0
        If (retVal) Then Return True
        Dim tag As String = TagName.ToUpperInvariant()
        If (TabIndex() > 0) Then Return False 'negative is ignored.

        Select Case tag
            Case "A", "BODY", "BUTTON", "FRAME", "IFRAME", "IMG", "INPUT", "OBJECT", "SELECT", "TEXTAREA", "ISINDEX"
                Return True
            Case Else
                Return False
        End Select
    End Function

    Public Function TabIndex() As Short
        Return htmlElement2.tabIndex
    End Function

    Public Sub Blur()
        htmlElement2.blur()
    End Sub

    Public Sub Focus()
        htmlElement2.focus()
    End Sub

    Public Function TagUrn() As String
        Return htmlElement2.tagUrn
    End Function

    Public Function ScopeName() As String
        Return htmlElement2.scopeName
    End Function

    Public Function IsMultiLine() As Boolean
        Return htmlElement3.isMultiLine()
    End Function

    Public Sub SetActive()
        htmlElement3.setActive()
    End Sub

    ''' <summary>
    ''' The type used to get the web type used in SlickTest.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetWebType() As String
        Return WebTagDefinitions.GetWebType(Me.TagName)
    End Function

    Private Function GetWebTypeSmart() As String
        Return WebTagDefinitions.GetTagSmart(Me.TagName, Me.Attribute("type"))
    End Function

    Public Const EventKeyCode As String = "KeyCode"
    Public Function ExecuteEvent(ByVal EventName As String, ByVal EventPropertyList As System.Collections.Generic.Dictionary(Of String, String)) As Boolean
        Dim obj As Object = Nothing
        Dim EventObject As mshtml.IHTMLEventObj = DirectCast(HTMLElement.document, mshtml.IHTMLDocument4).CreateEventObject(obj)
        Dim EventPropertyValue As String = String.Empty
        If (EventName.ToUpperInvariant() = "ONKEYPRESS" AndAlso WebTextBoxAPI.IsTextBox(Me) = True) Then
            EventPropertyValue = Me.Value + EventPropertyList(EventKeyCode)
            Me.SetValue("value", EventPropertyValue)
        End If
        For Each propertyName As String In EventPropertyList.Keys
            Dim value As String = EventPropertyList(propertyName)(0)
            Select Case propertyName
                Case EventKeyCode
                    EventObject.keyCode = Convert.ToInt32(Char.GetNumericValue(value(0)))
                Case Else
                    DirectCast(EventObject, mshtml.IHTMLEventObj2).setAttribute(propertyName, value, 0)
            End Select
        Next
        Return htmlElement3.FireEvent(EventName, DirectCast(EventObject, Object))
    End Function

    Public Function KeyDown(ByVal c As Char) As Boolean
        Return ExecuteEvent("onKeyDown", NewKeyboardEvent(c))
    End Function

    Private Shared Function NewKeyboardEvent(ByVal c As Char) As System.Collections.Generic.Dictionary(Of String, String)
        Dim x As New System.Collections.Generic.Dictionary(Of String, String)()
        x.Add(EventKeyCode, c.ToString())
        Return x
    End Function

    Public Function KeyUp(ByVal c As Char) As Boolean
        Return ExecuteEvent("onKeyUp", NewKeyboardEvent(c))
    End Function

    Public Function KeyPress(ByVal c As Char) As Boolean
        Return ExecuteEvent("onKeyPress", NewKeyboardEvent(c))
    End Function

    Public Overridable Sub TypeText(ByVal text As String, Optional ByVal typeSpeed As Integer = 30)
        For Each c As Char In text
            TypeKey(c)
            System.Threading.Thread.Sleep(typeSpeed)
        Next
    End Sub

    Protected Function TypeKey(ByVal c As Char) As Boolean
        If (KeyDown(c)) Then
            If (KeyPress(c)) Then
                If (KeyUp(c)) Then
                    Return True
                End If
            End If
        End If
        Return False
    End Function

    'Public Overrides Equals()
    Public Function AreSame(ByVal element As WebElementAPI) As Boolean
        If (Me.Index <> element.Index) Then
            Return False
        End If
        If (Me.ClassName <> element.ClassName) Then
            Return False
        End If
        If (Me.Id <> element.Id) Then
            Return False
        End If
        If (Me.InnerHtml <> element.InnerHtml) Then
            Return False
        End If
        If (Me.TagName <> element.TagName) Then
            Return False
        End If
        If (Me.Name <> element.Name) Then
            Return False
        End If
        Return True
    End Function

#Region "Style"
    Public Function StyleBackground() As String
        Return HTMLElement.style.background
    End Function

    Public Function StyleBackgroundAttachment() As String
        Return HTMLElement.style.backgroundAttachment
    End Function

    Public Function StyleBackgroundImage() As String
        Return HTMLElement.style.backgroundImage
    End Function

    Public Function StyleBackgroundPosition() As String
        Return HTMLElement.style.backgroundPosition
    End Function

    Public Function StyleBorder() As String
        Return HTMLElement.style.border
    End Function

    Public Function StyleBorderColor() As String
        Return HTMLElement.style.borderColor
    End Function

    Public Function StyleBorderStyle() As String
        Return HTMLElement.style.borderStyle
    End Function

    Public Function StyleCssText() As String
        Return HTMLElement.style.cssText
    End Function

    Public Function StyleDisplay() As String
        Return HTMLElement.style.display
    End Function

    Public Function StyleFont() As String
        Return HTMLElement.style.font
    End Function

    Public Function StyleFontSize() As String
        Dim val As Object = HTMLElement.style.fontSize
        If (val Is Nothing) Then Return Nothing
        Return val.ToString()
    End Function

    Public Function StyleFontStyle() As String
        Return HTMLElement.style.fontStyle
    End Function

    Public Function StyleFontWeight() As String
        Return HTMLElement.style.fontWeight
    End Function

    Public Function StyleMargin() As String
        Return HTMLElement.style.margin
    End Function

    Public Function StyleOverflow() As String
        Return HTMLElement.style.overflow
    End Function

    Public Function StylePadding() As String
        Return HTMLElement.style.padding
    End Function

    Public Function StylePosition() As String
        Return HTMLElement.style.position
    End Function

    Public Function StyleVisibility() As String
        Return HTMLElement.style.visibility
    End Function

    Public Function StyleTextAlign() As String
        Return HTMLElement.style.textAlign
    End Function

    Public Function StyleZIndex() As String
        Return Convert.ToString(HTMLElement.style.zIndex)
    End Function

    Public Function VerticalAlign() As String
        Return Convert.ToString(HTMLElement.style.verticalAlign)
    End Function
#End Region
End Class