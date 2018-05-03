Imports APIControls
''' <summary>
''' A WebElement is a generic object that allows for some basic 
''' abilities you can perform on ANY object, be it a WebTable 
''' or a WebSpan. 
''' </summary>
''' <remarks></remarks>
Public Class WebElement
    Inherits AbstractWebObject

#Region "Constructor"
    ''' <summary>
    ''' Creates a WebElement object.  
    ''' Do not directly create this object, instead use New UIControls.InterAct().[Object].WebElement([desc]).[Method]
    ''' </summary>
    ''' <param name="desc"></param>
    ''' <remarks></remarks>
    Protected Friend Sub New(ByVal desc As String)
        currentRectangle = New System.Drawing.Rectangle(0, 0, 0, 0)
        description = New System.Collections.Generic.List(Of APIControls.IDescription)
        Dim aDesc As UIControls.Description = UIControls.Description.Create()
        description.Add(aDesc)
        If (readDescription(desc) = False) Then
            Throw New InvalidExpressionException("Description not valid: " & desc)
            'error
        End If
        Init()
    End Sub

    Protected Friend Overridable Sub Init()
        HtmlTags.Add("*")
    End Sub

    Public Overrides ReadOnly Property ListOfSupportedHtmlTags() As System.Collections.Generic.List(Of String)
        Get
            Return HtmlTags
        End Get
    End Property

    Protected Friend Sub New(ByVal desc As APIControls.IDescription, ByVal descObj As System.Collections.Generic.List(Of APIControls.IDescription)) ', ByVal Values As System.Collections.Specialized.StringCollection, ByVal Names As System.Collections.Specialized.StringCollection)
        Me.description = descObj
        description.Add(desc)
        Init()
    End Sub

    ''' <summary>
    ''' Creates a WebElement object.  
    ''' Do not directly create this object, instead use New UIControls.InterAct().[Object].WebElement([desc]).[Method]
    ''' </summary>
    ''' <remarks></remarks>
    Protected Friend Sub New()
        'this is a do nothing case, to ignore errors...
        Init()
    End Sub

    ''' <summary>
    ''' Creates a WebElement object.  
    ''' Do not directly create this object, instead use New UIControls.InterAct().[Object].WebElement([desc]).[Method]
    ''' </summary>
    ''' <param name="desc"></param>
    ''' <remarks></remarks>
    Protected Friend Sub New(ByVal desc As APIControls.IDescription)
        description = New System.Collections.Generic.List(Of APIControls.IDescription)
        description.Add(desc)
        Init()
    End Sub

    ''' <summary>
    ''' Creates a WebElement object.  
    ''' Do not directly create this object, instead use New UIControls.InterAct().[Object].WebElement([desc]).[Method]
    ''' </summary>
    ''' <param name="desc"></param>
    ''' <remarks></remarks>
    Protected Friend Sub New(ByVal desc As System.Collections.Generic.List(Of APIControls.IDescription))
        Me.description = desc
        Init()
    End Sub

    Protected Friend Sub New(ByVal desc As UIControls.Description, ByVal descObj As System.Collections.Generic.List(Of APIControls.IDescription), ByVal reporter As IReport) ', ByVal Values As System.Collections.Specialized.StringCollection, ByVal Names As System.Collections.Specialized.StringCollection)
        Me.reporter = reporter
        Me.description = descObj
        description.Add(desc)
        Init()
    End Sub
#End Region

#Region "Private/Protected"
    Friend Function BuildWebElement(ByVal desc As Object) As WebElement
        Return DirectCast(BuildElement(desc, ElementTypes.Element), WebElement)
    End Function

    Friend Function BuildElement(ByVal desc As Object, ByVal type As AbstractWebObject.ElementTypes) As AbstractWebObject
        Return BuildElement(desc, type, Me.reporter, Me.description)
    End Function

    Friend Shared Function BuildElement(ByVal desc As Object, ByVal type As AbstractWebObject.ElementTypes, ByVal reporter As IReport, ByVal description As System.Collections.Generic.List(Of APIControls.IDescription)) As AbstractWebObject
        If (reporter Is Nothing) Then
            Log.LogData("Reporter Dead!")
            Throw New SlickTestUIException("Reporter Dead!")
        End If

        Try
            Dim descriptionValue As UIControls.Description
            If (TypeOf desc Is String) Then
                descriptionValue = UIControls.Description.Create(desc.ToString())
            Else
                If (TypeOf desc Is UIControls.Description) Then
                    descriptionValue = DirectCast(desc, UIControls.Description)
                Else
                    Throw New InvalidCastException("Description was not a valid type.  Type: " & desc.GetType().ToString())
                End If
            End If
            Select Case type
                Case ElementTypes.Span
                    Return New WebSpan(descriptionValue, description, reporter)
                Case ElementTypes.ComboBox
                    Return New WebComboBox(descriptionValue, description, reporter)
                Case ElementTypes.Div
                    Return New WebDiv(descriptionValue, description, reporter)
                Case ElementTypes.TableRow
                    Return New WebTableRow(descriptionValue, description, reporter)
                Case ElementTypes.TableCell
                    Return New WebTableCell(descriptionValue, description, reporter)
                Case ElementTypes.Table
                    Return New WebTable(descriptionValue, description, reporter)
                Case ElementTypes.Element
                    Return New WebElement(descriptionValue, description, reporter)
                Case ElementTypes.List
                    Return New WebList(descriptionValue, description, reporter)
                Case ElementTypes.ListItem
                    Return New WebListItem(descriptionValue, description, reporter)
                Case ElementTypes.Link
                    Return New WebLink(descriptionValue, description, reporter)
                Case ElementTypes.Checkbox
                    Return New WebCheckbox(descriptionValue, description, reporter)
                Case ElementTypes.GenericInput
                    Return New WebGenericInput(descriptionValue, description, reporter)
                Case ElementTypes.Image
                    Return New WebImage(descriptionValue, description, reporter)
                Case ElementTypes.Button
                    Return New WebButton(descriptionValue, description, reporter)
                Case ElementTypes.TextBox
                    Return New WebTextBox(descriptionValue, description, reporter)
                Case ElementTypes.RadioButton
                    Return New WebRadioButton(descriptionValue, description, reporter)
                Case Else
                    Throw New SlickTestUIException("Invalid type: " & type.ToString())
            End Select
        Catch ex As Exception
            Throw
        End Try
    End Function

    Private Function GetFullDescription() As UIControls.Description
        ExistsWithException()
        Return UIControls.Description.ConvertApiToUiDescription(Me.CurrentElement.CreateFullDescription())
    End Function

#End Region

#Region "Factories"

    ''' <summary>
    ''' Creates a WebElement object.
    ''' </summary>
    ''' <param name="desc">A description of the inner WebElement.</param>
    ''' <returns>Returns the WebElement.</returns>
    ''' <remarks>This allows the automation to go down the object tree.  For example:
    ''' New UIControls.InterAct().IEWebBrowser(MyWindow).WebElement(MyElement).Click()</remarks>
    Public Function WebElement(ByVal desc As Object) As WebElement
        Return BuildWebElement(desc)
    End Function

    ''' <summary>
    ''' Creates a WebTable object.
    ''' </summary>
    ''' <param name="desc">A description of the WebTable.</param>
    ''' <returns>Returns the WebElement.</returns>
    ''' <remarks>This allows the automation to go down the object tree.  For example:
    ''' New UIControls.InterAct().IEWebBrowser(MyWindow).WebTable(MyWebTable).Click()</remarks>
    Public Function WebTable(ByVal desc As Object) As WebTable
        Return DirectCast(BuildElement(desc, ElementTypes.Table), WebTable)
    End Function

    ''' <summary>
    ''' Creates a WebTableRow object.
    ''' </summary>
    ''' <param name="desc">A description of the WebTableRow.</param>
    ''' <returns>Returns the WebTableRow.</returns>
    ''' <remarks>This allows the automation to go down the object tree.  For example:
    ''' New UIControls.InterAct().IEWebBrowser(MyWindow).WebTableRow(MyTableRow).Click()</remarks>
    Public Function WebTableRow(ByVal desc As Object) As WebTableRow
        Return DirectCast(BuildElement(desc, ElementTypes.TableRow), WebTableRow)
    End Function

    ''' <summary>
    ''' Creates a WebTableCell object.
    ''' </summary>
    ''' <param name="desc">A description of the WebTableCell.</param>
    ''' <returns>Returns the WebTableRow.</returns>
    ''' <remarks>This allows the automation to go down the object tree.  For example:
    ''' New UIControls.InterAct().IEWebBrowser(MyWindow).WebTableCell(MyTableCell).Click()</remarks>
    Public Function WebTableCell(ByVal desc As Object) As WebTableCell
        Return DirectCast(BuildElement(desc, ElementTypes.TableCell), WebTableCell)
    End Function

    ''' <summary>
    ''' Creates a WebComboBox object.
    ''' </summary>
    ''' <param name="desc">A description of the WebComboBox.</param>
    ''' <returns>Returns the WebComboBox.</returns>
    ''' <remarks>This allows the automation to go down the object tree.  For example:
    ''' New UIControls.InterAct().IEWebBrowser(MyWindow).WebTableCell(MyComboBox).Click()</remarks>
    Public Function WebComboBox(ByVal desc As Object) As WebComboBox
        Return DirectCast(BuildElement(desc, ElementTypes.ComboBox), WebComboBox)
    End Function

    ''' <summary>
    ''' Creates a WebSpan object.
    ''' </summary>
    ''' <param name="desc">A description of the WebSpan.</param>
    ''' <returns>Returns the WebSpan.</returns>
    ''' <remarks>This allows the automation to go down the object tree.  For example:
    ''' New UIControls.InterAct().IEWebBrowser(MyWindow).WebSpan(MySpan).Click()</remarks>
    Public Function WebSpan(ByVal desc As Object) As WebSpan
        Return DirectCast(BuildElement(desc, ElementTypes.Span), WebSpan)
    End Function

    ''' <summary>
    ''' Creates a WebDiv object.
    ''' </summary>
    ''' <param name="desc">A description of the WebDiv.</param>
    ''' <returns>Returns the WebDiv.</returns>
    ''' <remarks>This allows the automation to go down the object tree.  For example:
    ''' New UIControls.InterAct().IEWebBrowser(MyWindow).WebDiv(MyDiv).Click()</remarks>
    Public Function WebDiv(ByVal desc As Object) As WebDiv
        Return DirectCast(BuildElement(desc, ElementTypes.Div), WebDiv)
    End Function

    ''' <summary>
    ''' Creates a WebList object.
    ''' </summary>
    ''' <param name="desc">A description of the WebList.</param>
    ''' <returns>Returns the WebList.</returns>
    ''' <remarks>This allows the automation to go down the object tree.  For example:
    ''' New UIControls.InterAct().IEWebBrowser(MyWindow).WebList(MyWebList).Click()</remarks>
    Public Function WebList(ByVal desc As Object) As WebList
        Return DirectCast(BuildElement(desc, ElementTypes.List), WebList)
    End Function

    ''' <summary>
    ''' Creates a WebListItem object.
    ''' </summary>
    ''' <param name="desc">A description of the WebListItem.</param>
    ''' <returns>Returns the WebListItem.</returns>
    ''' <remarks>This allows the automation to go down the object tree.  For example:
    ''' New UIControls.InterAct().IEWebBrowser(MyWindow).WebListItem(MyWebListItem).Click()</remarks>
    Public Function WebListItem(ByVal desc As Object) As WebListItem
        Return DirectCast(BuildElement(desc, ElementTypes.ListItem), WebListItem)
    End Function

    ''' <summary>
    ''' Creates a WebLink object.
    ''' </summary>
    ''' <param name="desc">A description of the WebLink.</param>
    ''' <returns>Returns the WebLink.</returns>
    ''' <remarks>This allows the automation to go down the object tree.  For example:
    ''' New UIControls.InterAct().IEWebBrowser(MyWindow).WebLink(MyWebLink).Click()</remarks>
    Public Function WebLink(ByVal desc As Object) As WebLink
        Return DirectCast(BuildElement(desc, ElementTypes.Link), WebLink)
    End Function

    ''' <summary>
    ''' Creates a WebCheckbox object.
    ''' </summary>
    ''' <param name="desc">A description of the WebCheckbox.</param>
    ''' <returns>Returns the WebCheckbox.</returns>
    ''' <remarks>This allows the automation to go down the object tree.  For example:
    ''' New UIControls.InterAct().IEWebBrowser(MyWindow).WebCheckbox(MyWebCheckbox).Click()</remarks>
    Public Function WebCheckbox(ByVal desc As Object) As WebCheckbox
        Return DirectCast(BuildElement(desc, ElementTypes.Checkbox), WebCheckbox)
    End Function

    ''' <summary>
    ''' Creates a WebGenericInput object.  This includes checkbox, button, hidden
    ''' image, password, radio, reset, submit and text types.
    ''' </summary>
    ''' <param name="desc">A description of the WebGenericInput.</param>
    ''' <returns>Returns the WebGenericInput.</returns>
    ''' <remarks>This allows the automation to go down the object tree.  For example:
    ''' New UIControls.InterAct().IEWebBrowser(MyWindow).WebGenericInput(MyWebCheckbox).Click()</remarks>
    Public Function WebGenericInput(ByVal desc As Object) As WebGenericInput
        Return DirectCast(BuildElement(desc, ElementTypes.GenericInput), WebGenericInput)
    End Function

    ''' <summary>
    ''' Creates a WebImage object.  This includes both the img and input tag types.
    ''' </summary>
    ''' <param name="desc">A description of the WebImage.</param>
    ''' <returns>Returns the WebImage.</returns>
    ''' <remarks>This allows the automation to go down the object tree.  For example:
    ''' New UIControls.InterAct().IEWebBrowser(MyWindow).WebImage(MyWebImage).Click()</remarks>
    Public Function WebImage(ByVal desc As Object) As WebImage
        Return DirectCast(BuildElement(desc, ElementTypes.Image), WebImage)
    End Function

    ''' <summary>
    ''' Creates a WebButton object.  This includes both the button and input tag types.
    ''' </summary>
    ''' <param name="desc">A description of the WebButton.</param>
    ''' <returns>Returns the WebButton.</returns>
    ''' <remarks>This allows the automation to go down the object tree.  For example:
    ''' New UIControls.InterAct().IEWebBrowser(MyWindow).WebButton(MyWebButton).Click()</remarks>
    Public Function WebButton(ByVal desc As Object) As WebButton
        Return DirectCast(BuildElement(desc, ElementTypes.Button), WebButton)
    End Function

    ''' <summary>
    ''' Creates a WebTextBox object.  This includes both the textarea and input tag types.
    ''' </summary>
    ''' <param name="desc">A description of the WebTextBox.</param>
    ''' <returns>Returns the WebTextBox.</returns>
    ''' <remarks>This allows the automation to go down the object tree.  For example:
    ''' New UIControls.InterAct().IEWebBrowser(MyWindow).WebTextBox(MyWebTextBox).Click()</remarks>
    Public Function WebTextBox(ByVal desc As Object) As WebTextBox
        Return DirectCast(BuildElement(desc, ElementTypes.TextBox), WebTextBox)
    End Function

    ''' <summary>
    ''' Creates a WebRadioButton object.
    ''' </summary>
    ''' <param name="desc">A description of the WebRadioButton.</param>
    ''' <returns>Returns the WebRadioButton.</returns>
    ''' <remarks>This allows the automation to go down the object tree.  For example:
    ''' New UIControls.InterAct().IEWebBrowser(MyWindow).WebRadioButton(MyRadioButton).Click()</remarks>
    Public Function WebRadioButton(ByVal desc As Object) As WebRadioButton
        Return DirectCast(BuildElement(desc, ElementTypes.RadioButton), WebRadioButton)
    End Function

#End Region

    ''' <summary>
    ''' Click a element.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function Click() As Boolean
        If (Exists(0) = True) Then
            Me.CurrentElement.Click()
            Return True
        End If
        Return False
    End Function

    ''' <summary>
    ''' Gets the text of a element.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetText() As String
        ExistsWithException()
        Return Me.CurrentElement.Text
    End Function

    ''' <summary>
    ''' Gets the title of an element, if a title exists.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetTitle() As String
        ExistsWithException()
        Return Me.CurrentElement.Title
    End Function

    ''' <summary>
    ''' Gets the tag name of an element.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetTagName() As String
        ExistsWithException()
        Return Me.CurrentElement.TagName
    End Function

    ''' <summary>
    ''' Gets the Id of an element.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetId() As String
        ExistsWithException()
        Return Me.CurrentElement.Id
    End Function

    ''' <summary>
    ''' Gets the langauge the browser is using.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks>This will check up the inheritance to get the current
    ''' language if set in any tag.</remarks>
    Public Function GetCurrentLanguage() As String
        ExistsWithException()
        Return Me.CurrentElement.LanguageToUse
    End Function

    ''' <summary>
    ''' Scrolls object into view, with the object at the top of the
    ''' page, if possible.
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub ScrollIntoView()
        ExistsWithException()
        Me.CurrentElement.ScrollIntoView()
    End Sub

    ''' <summary>
    ''' Gets the Index of an element.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetIndex() As Integer
        ExistsWithException()
        Return Me.CurrentElement.Index
    End Function

    ''' <summary>
    ''' Sets focus to a given element.
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub SetFocus()
        ExistsWithException()
        Me.CurrentElement.Focus()
    End Sub

    ''' <summary>
    ''' Get tab index.
    ''' </summary>
    ''' <remarks></remarks>
    Public Function GetTabIndex() As Short
        ExistsWithException()
        Return Me.CurrentElement.TabIndex()
    End Function

    ''' <summary>
    ''' Gets the access key.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks>The shortcut key.</remarks>
    Public Function GetAccessKey() As String
        ExistsWithException()
        Return Me.CurrentElement.AccessKey
    End Function

    ''' <summary>
    ''' Determines if the element is a tab stop.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks>If true, you will stop on the element on tab.</remarks>
    Public Function IsTabStop() As Boolean
        ExistsWithException()
        Return Me.CurrentElement.IsTabStop
    End Function

    ''' <summary>
    ''' Causes a event like tabbing out/away from the element.
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub Blur()
        ExistsWithException()
        Me.CurrentElement.Blur()
    End Sub

    ''' <summary>
    ''' Gets whether an element is enabled or not.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function IsEnabled() As Boolean
        ExistsWithException()
        Return Me.CurrentElement.Enabled
    End Function

    ''' <summary>
    ''' Get the InnerHtml of an element.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetInnerHtml() As String
        ExistsWithException()
        Return Me.CurrentElement.InnerHtml
    End Function

    ''' <summary>
    ''' Get the OuterHtml of an element.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks>This actually uses the parent's InnerHtml since
    ''' that typically is more useful for testing.</remarks>
    Public Function GetOuterHtml() As String
        ExistsWithException()
        Return Me.CurrentElement.OuterHtml
    End Function

    ''' <summary>
    ''' Get the OuterText of an element.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetOuterText() As String
        ExistsWithException()
        Return Me.CurrentElement.OuterText
    End Function

    ''' <summary>
    ''' Get the Parent of an element.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetParentElement() As WebElement
        ExistsWithException()

        Dim desc As IDescription = UIControls.Description.ConvertApiToUiDescription( _
                       Me.CurrentElement.GetParent().CreateReasonableDescription())
        Dim parentElement As WebElement = BuildWebElement(desc)

        parentElement.description.RemoveRange(1, parentElement.description.Count - 1) 'since we are building a new fully described
        'element we must clear out the description or the child element will cause detection problems.
        parentElement.description.Add(desc)
        Return parentElement
    End Function

    ''' <summary>
    ''' Gets the style for the element.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetStyleInfo() As WebStyle
        ExistsWithException()
        Return New WebStyle(Me.CurrentElement)
    End Function

    ''' <summary>
    ''' Get the next element if element is a sibling.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetNextElement() As WebElement
        ExistsWithException()
        Dim e As WebElementAPI = Me.CurrentElement.NextElement()
        If (e Is Nothing) Then Return Nothing

        Dim desc As IDescription = UIControls.Description.ConvertApiToUiDescription( _
                       e.CreateReasonableDescription())
        Dim nextElement As WebElement = BuildWebElement(desc)

        nextElement.description.RemoveRange(1, nextElement.description.Count - 1) 'since we are building a new fully described
        'element we must clear out the description or the child element will cause detection problems.
        nextElement.description.Add(desc)
        Return nextElement
    End Function

    ''' <summary>
    ''' Get the previous element if element is a sibling.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetPreviousElement() As WebElement
        ExistsWithException()
        Dim e As WebElementAPI = Me.CurrentElement.PreviousElement()
        If (e Is Nothing) Then Return Nothing

        Dim desc As IDescription = UIControls.Description.ConvertApiToUiDescription( _
                       e.CreateReasonableDescription())
        Dim previousElement As WebElement = BuildWebElement(desc)

        previousElement.description.RemoveRange(1, previousElement.description.Count - 1) 'since we are building a new fully described
        'element we must clear out the description or the child element will cause detection problems.
        previousElement.description.Add(desc)
        Return previousElement
    End Function

    ''' <summary>
    ''' Returns the left point of the element.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property Left() As Integer
        Get
            ExistsWithException()
            Return Me.CurrentElement.ActualX
        End Get
    End Property

    ''' <summary>
    ''' Returns the right point of the element.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property Right() As Integer
        Get
            ExistsWithException()
            Return Me.CurrentElement.Right
        End Get
    End Property

    ''' <summary>
    ''' Returns the top point of the element.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property Top() As Integer
        Get
            ExistsWithException()
            Return Me.CurrentElement.ActualY
        End Get
    End Property

    ''' <summary>
    ''' Returns the bottom point of the element.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property Bottom() As Integer
        Get
            ExistsWithException()
            Return Me.CurrentElement.Bottom
        End Get
    End Property

    ''' <summary>
    ''' Returns the width of the element.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property Width() As Integer
        Get
            ExistsWithException()
            Return Me.CurrentElement.Width
        End Get
    End Property

    ''' <summary>
    ''' Returns the height of the element.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property Height() As Integer
        Get
            ExistsWithException()
            Return Me.CurrentElement.Height
        End Get
    End Property

    ''' <summary>
    ''' Gets the class name of an element.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property ClassName() As String
        Get
            ExistsWithException()
            Return Me.CurrentElement.ClassName
        End Get
    End Property

    ''' <summary>
    ''' Returns the URN for the namespace.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks>Typically this is not used.</remarks>
    Public ReadOnly Property TagUrn() As String
        Get
            ExistsWithException()
            Return Me.CurrentElement.TagUrn
        End Get
    End Property

    ''' <summary>
    ''' Namespace prefix that is used with the custom tags. 
    ''' This namespace is declared in the document by using the XMLNS attribute of the html element.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks>Typically this is not used.</remarks>
    Public ReadOnly Property ScopeName() As String
        Get
            ExistsWithException()
            Return Me.CurrentElement.ScopeName
        End Get
    End Property

    ''' <summary>
    ''' Sets the text for a given element.
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub SetText(ByVal Text As String)
        ExistsWithException()
        Me.CurrentElement.SetText(Text)
    End Sub

    ''' <summary>
    ''' Appends the text for a given element.
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub AppendText(ByVal Text As String)
        ExistsWithException()
        Me.CurrentElement.AppendText(Text)
    End Sub

    ''' <summary>
    ''' Gets the type of web element this is.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function WebType() As String
        ExistsWithException()
        Return APIControls.WebTagDefinitions.GetWebType(Me.CurrentElement.TagName)
    End Function

    ''' <summary>
    ''' Types the text out.
    ''' </summary>
    ''' <param name="text"></param>
    ''' <remarks></remarks>
    Public Sub TypeText(ByVal Text As String)
        ExistsWithException()
        Me.CurrentElement.TypeText(Text)
    End Sub

    ''' <summary>
    ''' Gets the current value, if a value is set.
    ''' </summary>
    ''' <remarks>Typically the same as GetText, but not always.</remarks>
    Public Function GetValue() As String
        ExistsWithException()
        Return Me.CurrentElement.Value
    End Function

    ''' <summary>
    ''' Gets a attribute based upon its name.
    ''' </summary>
    ''' <param name="AttributeName">Name of attribute</param>
    ''' <remarks></remarks>
    Public Function GetAttribute(ByVal AttributeName As String) As String
        ExistsWithException()
        Return CurrentElement.Attribute(AttributeName)
    End Function

    ''' <summary>
    ''' Gets the entire list of children objects of the WebElements.  The list
    ''' is not in any particular order.
    ''' </summary>
    ''' <returns>Returns an array of descriptions that are children of
    ''' the current window.</returns>
    ''' <remarks></remarks>
    Public Function GetChildDescriptions() As UIControls.Description()
        ExistsWithException()

        Dim list As New System.Collections.Generic.List(Of UIControls.Description)()
        Dim elems As WebElementAPI() = IE.FindElements(ElementDescriptionToFindInExists(), ElementDescriptionsForFormerElements())
        If (elems Is Nothing) Then Return Nothing
        For Each element As WebElementAPI In elems
            list.Add(UIControls.Description.ConvertApiToUiDescription(element.CreateFullDescription()))
        Next
        Return list.ToArray()
    End Function

    ''' <summary>
    ''' Gets the entire data set for all objects found within the given WebElement.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks>This method maybe take a while.</remarks>
    Public Function DumpWindowData() As String
        Dim window As New System.Text.StringBuilder(10000)
        Dim items As System.Collections.Generic.Dictionary(Of String, System.Collections.Generic.Dictionary(Of String, String))
        items = DumpWindowDataAsDictionary()

        For Each item As String In items.Keys
            window.AppendLine(item)
            For Each description As String In items(item).Keys
                window.AppendLine(vbTab & description & "='" & items(item)(description) & "'")
            Next
        Next
        Return window.ToString()
    End Function

    ''' <summary>
    ''' Gets the entire data set for all objects found within the given WebElement.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks>This method maybe take a while.</remarks>
    Public Function DumpWindowDataAsDictionary() As System.Collections.Generic.Dictionary(Of String, System.Collections.Generic.Dictionary(Of String, String))
        Dim itemCount As Integer = 1
        Dim items As New System.Collections.Generic.Dictionary(Of String, System.Collections.Generic.Dictionary(Of String, String))
        ExistsWithException()

        Dim MainDesc As APIControls.Description = Me.GetFullDescription()
        items = BuildDescribingAsDictionary(items, MainDesc, itemCount)
        Dim ChildDescriptions As Description() = Me.GetChildDescriptions()
        If (ChildDescriptions Is Nothing) Then Return items 'window.ToString()

        For Each desc As Description In ChildDescriptions
            itemCount = itemCount + 1
            items = BuildDescribingAsDictionary(items, desc, itemCount)
            Me.description.RemoveRange(1, Me.description.Count - 1)
        Next
        Return items
    End Function

    Private Function BuildDescribingAsDictionary(ByRef internalDictionary As System.Collections.Generic.Dictionary(Of String, System.Collections.Generic.Dictionary(Of String, String)), ByVal desc As APIControls.Description, ByVal itemCount As Integer) As System.Collections.Generic.Dictionary(Of String, System.Collections.Generic.Dictionary(Of String, String))
        Dim valueToAddToList As New System.Collections.Generic.Dictionary(Of String, String)
        internalDictionary.Add("Item # " & itemCount, valueToAddToList)

        For Each item As APIControls.Description.DescriptionData In [Enum].GetValues(GetType(APIControls.Description.DescriptionData))
            If (desc.Contains(item)) Then
                valueToAddToList.Add(desc.GetItemName(item), desc.GetItemValue(item))
            End If
        Next

        Dim description1 As UIControls.Description = UIControls.Description.Create()
        description1.Add(APIControls.Description.DescriptionData.Index, desc.Index.ToString())
        description1.Add(APIControls.Description.DescriptionData.WebTag, desc.WebTag)

        If (desc.Contains(APIControls.Description.DescriptionData.WebID) AndAlso Not String.IsNullOrEmpty(desc.WebID)) Then
            description1.Add(APIControls.Description.DescriptionData.WebID, desc.WebID)
        End If

        If (desc.Contains(APIControls.Description.DescriptionData.Name) AndAlso Not String.IsNullOrEmpty(desc.Name)) Then
            description1.Add(APIControls.Description.DescriptionData.Name, desc.Name)
        End If

        description1.Add(APIControls.Description.DescriptionData.WildCard, desc.WildCard.ToString())

        Dim tmpBrowserHwnd As IntPtr = description(0).Hwnd
        If (WindowsFunctions.IsWebPartExactlyIE(tmpBrowserHwnd) = False) Then Return internalDictionary
        Dim browserDescription As IDescription = Me.description(0)

        Dim browser As IEWebBrowser = New IEWebBrowser(browserDescription)
        browser.reporter = New StubbedReport()
        Dim absWebObj As WebElement

        Select Case desc.WindowType().ToLowerInvariant()
            Case "webtablecell"
                Dim TblCl As UIControls.WebTableCell = browser.WebTableCell(description1)
                If (TblCl Is Nothing) Then Return internalDictionary
                absWebObj = TblCl
                valueToAddToList.Add("ColumnSpan", TblCl.GetColumnSpan().ToString())
                valueToAddToList.Add("RowSpan", TblCl.GetRowSpan().ToString())
                valueToAddToList.Add("ParentRow.Text", TblCl.GetParentRow().GetText())

            Case "webtablerow"
                Dim TblRw As UIControls.WebTableRow = browser.WebTableRow(description1)
                If (TblRw Is Nothing) Then Return internalDictionary
                absWebObj = TblRw

                valueToAddToList.Add("RowIndex", TblRw.GetRowIndex().ToString())
                valueToAddToList.Add("CellCount", TblRw.GetCellCount().ToString())
                valueToAddToList.Add("CharOffset", TblRw.GetCharOffset())

            Case "webtable"
                Dim Tbl As UIControls.WebTable = browser.WebTable(description1)
                If (Tbl Is Nothing) Then Return internalDictionary
                absWebObj = Tbl

                valueToAddToList.Add("RowCount", Tbl.GetRowCount().ToString())
                valueToAddToList.Add("TBodiesCount", Tbl.GetTBodiesCount().ToString())
                valueToAddToList.Add("TFootRowCount", Tbl.GetTFootRowCount().ToString())
                valueToAddToList.Add("THeadRowCount", Tbl.GetTHeadRowCount().ToString())

            Case "webimage"
                Dim WinObj As UIControls.WebElement = browser.WebElement(description1)
                If (WinObj Is Nothing) Then Return internalDictionary
                absWebObj = WinObj

            Case "webspan"
                Dim WinObj As UIControls.WebElement = browser.WebElement(description1)
                If (WinObj Is Nothing) Then Return internalDictionary
                absWebObj = WinObj

            Case "webdiv"
                Dim WinObj As UIControls.WebElement = browser.WebElement(description1)
                If (WinObj Is Nothing) Then Return internalDictionary
                absWebObj = WinObj

            Case "weblink"
                Dim WinObj As UIControls.WebElement = browser.WebElement(description1)
                If (WinObj Is Nothing) Then Return internalDictionary
                absWebObj = WinObj

            Case "weblistitem"
                Dim WinObj As UIControls.WebElement = browser.WebElement(description1)
                If (WinObj Is Nothing) Then Return internalDictionary
                absWebObj = WinObj

            Case "weblist"
                Dim WinObj As UIControls.WebElement = browser.WebElement(description1)
                If (WinObj Is Nothing) Then Return internalDictionary
                absWebObj = WinObj

            Case "weblabel"
                Dim WinObj As UIControls.WebElement = browser.WebElement(description1)
                If (WinObj Is Nothing) Then Return internalDictionary
                absWebObj = WinObj

            Case "webimage"
                Dim WinObj As UIControls.WebElement = browser.WebElement(description1)
                If (WinObj Is Nothing) Then Return internalDictionary
                absWebObj = WinObj

            Case "webform"
                Dim WinObj As UIControls.WebElement = browser.WebElement(description1)
                If (WinObj Is Nothing) Then Return internalDictionary
                absWebObj = WinObj

            Case "webbutton"
                Dim WinObj As UIControls.WebElement = browser.WebElement(description1)
                If (WinObj Is Nothing) Then Return internalDictionary
                absWebObj = WinObj

            Case "webarea"
                Dim WinObj As UIControls.WebElement = browser.WebElement(description1)
                If (WinObj Is Nothing) Then Return internalDictionary
                absWebObj = WinObj

            Case "webelement"
                Dim WinObj As UIControls.WebElement = browser.WebElement(description1)
                If (WinObj Is Nothing) Then Return internalDictionary
                absWebObj = WinObj

            Case "webcombobox"
                Dim CmbBox As UIControls.WebComboBox = browser.WebComboBox(description1)
                If (CmbBox Is Nothing) Then Return internalDictionary
                absWebObj = CmbBox

                valueToAddToList.Add("ItemCount", CmbBox.GetItemCount().ToString())
                valueToAddToList.Add("Type", CmbBox.Type())
                valueToAddToList.Add("SelectedItemNumber", CmbBox.GetSelectedIndex().ToString())
                valueToAddToList.Add("SelectedValue", CmbBox.SelectedValue())
                valueToAddToList.Add("Size", CmbBox.Size().ToString())
                valueToAddToList.Add("AllowMultiItemSelect", CmbBox.AllowMultiItemSelect().ToString())
                valueToAddToList.Add("IsDisabled", CmbBox.IsDisabled().ToString())

                If (CmbBox.GetItemCount() >= 1) Then
                    valueToAddToList.Add("(Sample)Item(0).Text", CmbBox.GetItem(0).GetText())
                End If

                'ComboBox

            Case "webtextbox"
                Dim TxtBox As UIControls.WebTextBox = browser.WebTextBox(description1)
                If (TxtBox Is Nothing) Then Return internalDictionary
                valueToAddToList.Add("Wrap", TxtBox.GetWrap())
                valueToAddToList.Add("Rowcount", TxtBox.GetRowCount().ToString())
                valueToAddToList.Add("ColumnCount", TxtBox.GetColumnCount().ToString())
                valueToAddToList.Add("TextType", TxtBox.TextType())
                valueToAddToList.Add("IsReadOnly", TxtBox.IsReadOnly().ToString())
                absWebObj = TxtBox

                'TextBox

            Case "webbutton"
                'Nothing special
                Dim Buton As UIControls.WebButton = browser.WebButton(description1)
                If (Buton Is Nothing) Then Return internalDictionary
                valueToAddToList.Add("ButtonType", Buton.GetButtonType().ToString())
                absWebObj = Buton

                'WebButton

            Case "webcheckbox"
                Dim ChkBox As UIControls.WebCheckbox = browser.WebCheckbox(description1)
                If (ChkBox Is Nothing) Then Return internalDictionary
                absWebObj = ChkBox

                valueToAddToList.Add("IsIndeterminate", ChkBox.IsIndeterminate().ToString())
                valueToAddToList.Add("IsReadOnly", ChkBox.IsReadOnly().ToString())
                valueToAddToList.Add("IsChecked", ChkBox.GetChecked().ToString())
                'WebCheckBox
            Case "webgenericinput"
                Dim GnrInput As UIControls.WebGenericInput = browser.WebGenericInput(description1)
                If (GnrInput Is Nothing) Then Return internalDictionary
                absWebObj = GnrInput

                valueToAddToList.Add("Alt", GnrInput.GetAlt())
                valueToAddToList.Add("Align", GnrInput.GetAlign())
                valueToAddToList.Add("IsChecked/Selected", GnrInput.GetChecked().ToString())
                'WebGenericInput

            Case Else
                Dim WinObj As UIControls.WebElement = browser.WebElement(description1)
                If (WinObj Is Nothing) Then Return internalDictionary
                absWebObj = WinObj

        End Select

        If (absWebObj Is Nothing) Then
            Return internalDictionary
        End If
        valueToAddToList.Add("IsEnabled", absWebObj.IsEnabled().ToString())
        valueToAddToList.Add("IsTabStop", absWebObj.IsTabStop().ToString())
        valueToAddToList.Add("TabIndex", absWebObj.GetTabIndex().ToString())
        valueToAddToList.Add("CssText", absWebObj.GetStyleInfo.GetCssText())
        valueToAddToList.Add("Title", absWebObj.GetTitle())
        valueToAddToList.Add("Index", absWebObj.GetIndex().ToString())
        valueToAddToList.Add("AccessKey", absWebObj.GetAccessKey())
        valueToAddToList.Add("ClassName", absWebObj.ClassName())

        Return internalDictionary
    End Function


End Class