Friend Class VElement
    Implements IElement

    Private IsEnabled As Boolean
    Private TextVals As New System.Collections.Generic.Dictionary(Of String, String)
    Private Loc As New System.Drawing.Rectangle

    Public Sub New(ByVal elem As IElement)
        TextVals.Add("attribute:name", elem.Attribute("name"))
        Loc = New System.Drawing.Rectangle(elem.Left, elem.Top, elem.Width, elem.Height)
        IsEnabled = elem.Enabled
        TextVals.Add("id", elem.Id)
        TextVals.Add("innerhtml", elem.InnerHTML)
        TextVals.Add("outerhtml", elem.OuterHTML)
        TextVals.Add("outertext", elem.OuterText)
        TextVals.Add("tagname", elem.TagName)
        TextVals.Add("text", elem.Text)
        TextVals.Add("title", elem.Title)
    End Sub

    Public Sub New(ByVal elem1 As Object)
        Dim elem As mshtml.IHTMLElement = DirectCast(elem1, mshtml.IHTMLElement)
        Dim attr As String = String.Empty
        Try
            attr = elem.getAttribute("name").ToString()
            If (attr Is Nothing) Then attr = String.Empty
        Catch ex As Exception
        End Try

        TextVals.Add("attribute:name", attr)
        Loc = New System.Drawing.Rectangle(elem.offsetLeft, elem.offsetTop, elem.offsetWidth, elem.offsetHeight)
        IsEnabled = Not DirectCast(elem, mshtml.IHTMLElement3).isDisabled
        TextVals.Add("id", elem.id)
        TextVals.Add("innerhtml", elem.innerHTML)
        TextVals.Add("outerhtml", elem.outerHTML)
        TextVals.Add("outertext", elem.outerText)
        TextVals.Add("tagname", elem.tagName)
        TextVals.Add("text", elem.innerText)
        TextVals.Add("title", elem.title)
    End Sub

    Public ReadOnly Property Text() As String Implements IElement.Text
        Get
            Return TextVals.Item("text")
        End Get
    End Property
    Public ReadOnly Property Right() As Integer Implements IElement.Right
        Get
            Return Loc.Right
        End Get
    End Property
    Public ReadOnly Property Left() As Integer Implements IElement.Left
        Get
            Return Loc.Left
        End Get
    End Property
    Public ReadOnly Property Top() As Integer Implements IElement.Top
        Get
            Return Loc.Top
        End Get
    End Property
    Public ReadOnly Property Bottom() As Integer Implements IElement.Bottom
        Get
            Return Loc.Bottom
        End Get
    End Property
    Public ReadOnly Property Height() As Integer Implements IElement.Height
        Get
            Return Loc.Height
        End Get
    End Property
    Public ReadOnly Property Width() As Integer Implements IElement.Width
        Get
            Return Loc.Width
        End Get
    End Property
    Public ReadOnly Property Attribute(ByVal AttributeName As String) As String Implements IElement.Attribute
        Get
            Return TextVals.Item("attribute:" & AttributeName.ToLower())
        End Get
    End Property
    Public ReadOnly Property OuterText() As String Implements IElement.OuterText
        Get
            Return TextVals.Item("outertext")
        End Get
    End Property
    Public ReadOnly Property InnerHTML() As String Implements IElement.InnerHTML
        Get
            Return TextVals.Item("innerhtml")
        End Get
    End Property
    Public ReadOnly Property OuterHTML() As String Implements IElement.OuterHTML
        Get
            Return TextVals.Item("outerhtml")
        End Get
    End Property
    Public ReadOnly Property Id() As String Implements IElement.Id
        Get
            Return TextVals.Item("id")
        End Get
    End Property
    Public ReadOnly Property Title() As String Implements IElement.Title
        Get
            Return TextVals.Item("title")
        End Get
    End Property
    Public ReadOnly Property TagName() As String Implements IElement.TagName
        Get
            Return TextVals.Item("tagname")
        End Get
    End Property
    Public ReadOnly Property Enabled() As Boolean Implements IElement.Enabled
        Get
            Return IsEnabled
        End Get
    End Property
End Class