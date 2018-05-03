Friend Interface IElement
    ReadOnly Property Text() As String
    ReadOnly Property Right() As Integer
    ReadOnly Property Left() As Integer
    ReadOnly Property Top() As Integer
    ReadOnly Property Bottom() As Integer
    ReadOnly Property Height() As Integer
    ReadOnly Property Width() As Integer
    ReadOnly Property Attribute(ByVal AttributeName As String) As String
    ReadOnly Property OuterText() As String
    ReadOnly Property InnerHTML() As String
    ReadOnly Property OuterHTML() As String
    ReadOnly Property Id() As String
    ReadOnly Property Title() As String
    ReadOnly Property TagName() As String
    ReadOnly Property Enabled() As Boolean
End Interface
