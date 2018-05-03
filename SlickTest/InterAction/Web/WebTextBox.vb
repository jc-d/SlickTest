Imports APIControls

Public Class WebTextBox
    Inherits WebElement

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
        description.Add(aDesc)
        If (readDescription(desc) = False) Then
            Throw New InvalidExpressionException("Description not valid: " & desc)
            'error
        End If
        Init()
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
    ''' Do not directly create this object, instead use InterAct.[Object].WebElement([desc]).[Method]
    ''' </summary>
    ''' <remarks></remarks>
    Protected Friend Sub New()
        'this is a do nothing case, to ignore errors...
        Init()
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
        Init()
    End Sub

    ''' <summary>
    ''' Creates a WebElement object.  
    ''' Do not directly create this object, instead use InterAct.[Object].WebElement([desc]).[Method]
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

    Protected Friend Overrides Sub Init()
        HtmlTags = APIControls.WebTagDefinitions.TextBoxTags
    End Sub
#End Region

    Private Function GetWebTextBoxAPI() As WebTextBoxAPI
        Return New WebTextBoxAPI(Me.CurrentElement)
    End Function

    ''' <summary>
    ''' Selects only textarea otherwise it only set's the text field active.
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub SetSelect()
        ExistsWithException()
        GetWebTextBoxAPI().Select()
    End Sub

    ''' <summary>
    ''' Detects readonly for textarea, otherwise it returns is disabled.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function IsReadOnly() As Boolean
        ExistsWithException()
        Return GetWebTextBoxAPI().ReadOnly()
    End Function

    ''' <summary>
    ''' Returns the number of rows in a textarea or -1 if not a textarea.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetRowCount() As Integer
        ExistsWithException()
        Return GetWebTextBoxAPI().Rows()
    End Function

    ''' <summary>
    ''' Returns the number of columns in a textarea or 1 if not a textarea.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetColumnCount() As Integer
        ExistsWithException()
        Return GetWebTextBoxAPI().Columns()
    End Function

    Public Function GetWrap() As String
        ExistsWithException()
        Return GetWebTextBoxAPI().Wrap()
    End Function

    Public Function TextType() As String
        ExistsWithException()
        Return GetWebTextBoxAPI().Type()
    End Function

    ''' <summary>
    ''' First clears all text and then types the text out.
    ''' </summary>
    ''' <param name="text"></param>
    ''' <remarks></remarks>
    Public Overloads Sub TypeText(ByVal text As String)
        ExistsWithException()
        GetWebTextBoxAPI().TypeText(text)
    End Sub


End Class
