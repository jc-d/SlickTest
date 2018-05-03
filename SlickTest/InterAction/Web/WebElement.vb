#Const IncludeWeb = 2 'set to 1 to enable web

#If (IncludeWeb = 1) Then
Public Class WebElement
    Inherits AbstractWebObject

#Region "Constructor"
    ''' <summary>
    ''' Creates a WebElement object.  
    ''' Do not directly create this object, instead use InterAct.[Object].WebElement([desc]).[Method]
    ''' </summary>
    ''' <param name="desc"></param>
    ''' <remarks></remarks>
    Protected Friend Sub New(ByVal desc As String)
        currentRectangle = New System.Drawing.Rectangle(0, 0, 0, 0)
        description = New System.Collections.Generic.List(Of APIControls.IDescription)
        Dim aDesc As UIControls.Description = UIControls.Description.Create()
        Description.Add(aDesc)
        If (readDescription(desc) = False) Then
            Throw New InvalidExpressionException("Description not valid: " & desc)
            'error
        End If
    End Sub

    Protected Friend Sub New(ByVal desc As APIControls.IDescription, ByVal descObj As System.Collections.Generic.List(Of APIControls.IDescription)) ', ByVal Values As System.Collections.Specialized.StringCollection, ByVal Names As System.Collections.Specialized.StringCollection)
        Me.description = descObj
        Description.Add(desc)
    End Sub

    ''' <summary>
    ''' Creates a WebElement object.  
    ''' Do not directly create this object, instead use InterAct.[Object].WebElement([desc]).[Method]
    ''' </summary>
    ''' <remarks></remarks>
    Protected Friend Sub New()
        'this is a do nothing case, to ignore errors...
    End Sub

    ''' <summary>
    ''' Creates a WebElement object.  
    ''' Do not directly create this object, instead use InterAct.[Object].WebElement([desc]).[Method]
    ''' </summary>
    ''' <param name="desc"></param>
    ''' <remarks></remarks>
    Protected Friend Sub New(ByVal desc As APIControls.IDescription)
        description = New System.Collections.Generic.List(Of APIControls.IDescription)
        description.Add(desc)
    End Sub

    ''' <summary>
    ''' Creates a WebElement object.  
    ''' Do not directly create this object, instead use InterAct.[Object].WebElement([desc]).[Method]
    ''' </summary>
    ''' <param name="desc"></param>
    ''' <remarks></remarks>
    Protected Friend Sub New(ByVal desc As System.Collections.Generic.List(Of APIControls.IDescription))
        Me.description = desc
    End Sub

    Protected Friend Sub New(ByVal desc As String, ByVal descObj As System.Collections.Generic.List(Of APIControls.IDescription)) ', ByVal Values As System.Collections.Specialized.StringCollection, ByVal Names As System.Collections.Specialized.StringCollection)
        Me.description = descObj
        Dim aDesc As UIControls.Description = UIControls.Description.Create()
        Description.Add(aDesc)
        If (readDescription(desc) = False) Then
            Throw New InvalidExpressionException("Description not valid: " & desc)
            'error
        End If
    End Sub
#End Region

#Region "Private/Protected"
    Private Function BuildWebElement(ByVal desc As Object) As WebElement
        Dim web As WebElement = Nothing
        If (Me.reporter Is Nothing) Then
            Log.LogData("Reporter Dead!")
            Throw New SlickTestUIException("Reporter Dead!")
        End If
        Try
            If (TypeOf desc Is String) Then
                web = New WebElement(desc.ToString(), Me.description)
            Else
                If (TypeOf desc Is UIControls.Description) Then
                    Dim tmpDesc As UIControls.Description = DirectCast(desc, UIControls.Description)
                    web = New WebElement(tmpDesc, Me.description)
                Else
                    Throw New InvalidCastException("Description was not a valid type")
                End If
            End If
            '//////
            web.reporter = Me.reporter
            Return web
        Catch ex As Exception
            Throw
        End Try
    End Function
#End Region

    ''' <summary>
    ''' Creates a WebElement object.
    ''' </summary>
    ''' <param name="desc">A description of the inner WebElement.</param>
    ''' <returns>returns the WebElement.</returns>
    ''' <remarks>This allows the automation to go down the object tree.  For example:
    ''' InterAct.WebElement(MyWindow).WebElement(MyButton).Click()</remarks>
    Public Function WebElement(ByVal desc As Object) As WebElement
        Return BuildWebElement(desc)
    End Function

    ''' <summary>
    ''' Click a element.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function Click() As Boolean
        If (Exists(0) = True) Then
            'Console.WriteLine("About to click...")
            Me.CurrentElement.Click()
            Return True
        End If
        Return False
    End Function

    ''' <summary>
    ''' Click a point inside the element, if possible??
    ''' </summary>
    ''' <param name="X">Currently ignored.</param>
    ''' <param name="Y">Currently ignored.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function Click(ByVal X As Integer, ByVal Y As Integer) As Boolean
        If (Exists(0) = True) Then
            'Console.WriteLine("About to click...")
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
    ''' <remarks></remarks>
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
    ''' Get the OuterText of an element.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetParentElement() As WebElement
        ExistsWithException()
        Return BuildWebElement(UIControls.Description.ConvertApiToUiDescription( _
                        IE.FindGoodDescription(Me.CurrentElement.GetParent())))
    End Function

    ''' <summary>
    ''' Returns the left point of the element.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ReadOnly Property Left() As Integer
        Get
            ExistsWithException()
            Return Me.CurrentElement.Left
        End Get
    End Property

    ''' <summary>
    ''' Returns the right point of the element.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ReadOnly Property Right() As Integer
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
    ReadOnly Property Top() As Integer
        Get
            ExistsWithException()
            Return Me.CurrentElement.Top
        End Get
    End Property

    ''' <summary>
    ''' Returns the bottom point of the element.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ReadOnly Property Bottom() As Integer
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
    ReadOnly Property Width() As Integer
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
    ReadOnly Property Height() As Integer
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
    ReadOnly Property ClassName() As String
        Get
            ExistsWithException()
            Return Me.CurrentElement.ClassName
        End Get
    End Property
End Class
#End If