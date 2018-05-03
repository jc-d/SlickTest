﻿Imports APIControls

Public Class WebList
    Inherits WebElement

#Region "Constructor"
    ''' <summary>
    ''' Creates a WebElement object.  
    ''' Do not directly create this object, instead use UIControls.[Object].WebElement([desc]).[Method]
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
    ''' Do not directly create this object, instead use UIControls.[Object].WebElement([desc]).[Method]
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
        HtmlTags = APIControls.WebTagDefinitions.ListTags
    End Sub
#End Region

    Private Function GetWebListAPI() As WebListAPI
        Return New WebListAPI(Me.CurrentElement)
    End Function

    ''' <summary>
    ''' A Boolean value indicating whether the list should be compacted by 
    ''' removing extra space between list objects.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetCompact() As Boolean
        ExistsWithException()
        Return GetWebListAPI.Compact
    End Function

    Public Function GetListItem(ByVal index As Integer) As WebListItem
        ExistsWithException()
        Dim item As WebListItemAPI = GetWebListAPI().GetListItem(index)
        Dim desc As IDescription = UIControls.Description.ConvertApiToUiDescription( _
               item.CreateFullDescription())
        Dim foundElement As WebListItem = DirectCast(BuildElement(desc, ElementTypes.ListItem), WebListItem)

        Return foundElement
    End Function

    Public Function GetListItem(ByVal text As String) As WebListItem
        ExistsWithException()
        Dim item As WebListItemAPI = GetWebListAPI().GetListItem(text)
        Dim desc As IDescription = UIControls.Description.ConvertApiToUiDescription( _
               item.CreateFullDescription())
        Dim foundElement As WebListItem = DirectCast(BuildElement(desc, ElementTypes.ListItem), WebListItem)

        Return foundElement
    End Function
End Class
