Imports APIControls

Public Class WebRadioButton
    Inherits WebElement

#Region "Constructor"
    ''' <summary>
    ''' Creates a WebRadioButton object.  
    ''' Do not directly create this object, instead use UIControls.[Object].WebRadioButton([desc]).[Method]
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
    ''' Creates a WebRadioButton object.  
    ''' Do not directly create this object, instead use UIControls.[Object].WebRadioButton([desc]).[Method]
    ''' </summary>
    ''' <remarks></remarks>
    Protected Friend Sub New()
        'this is a do nothing case, to ignore errors...
        Init()
    End Sub

    ''' <summary>
    ''' Creates a WebRadioButton object.  
    ''' Do not directly create this object, instead use InterAct.[Object].WebRadioButton([desc]).[Method]
    ''' </summary>
    ''' <param name="desc"></param>
    ''' <remarks></remarks>
    Protected Friend Sub New(ByVal desc As APIControls.IDescription)
        description = New System.Collections.Generic.List(Of APIControls.IDescription)
        description.Add(desc)
        Init()
    End Sub

    ''' <summary>
    ''' Creates a WebRadioButton object.  
    ''' Do not directly create this object, instead use InterAct.[Object].WebRadioButton([desc]).[Method]
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
        HtmlTags = APIControls.WebTagDefinitions.InputTags
    End Sub
#End Region

    ''' <summary>
    ''' Determines if the radiobutton is read only.
    ''' </summary>
    ''' <returns>Returns true if the radiobutton is set to read only.</returns>
    ''' <remarks></remarks>
    Public Function IsReadOnly() As Boolean
        ExistsWithException()
        Return GetRadioAPI().IsReadOnly()
    End Function

    ''' <summary>
    ''' Determines if the radiobutton is selected.
    ''' </summary>
    ''' <returns>Returns true if the radiobutton is selected.</returns>
    ''' <remarks></remarks>
    Public Function IsSelected() As Boolean
        ExistsWithException()
        Return GetRadioAPI().IsChecked()
    End Function


    Private Function GetRadioAPI() As WebRadioButtonAPI
        Return New WebRadioButtonAPI(Me.CurrentElement)
    End Function
End Class
