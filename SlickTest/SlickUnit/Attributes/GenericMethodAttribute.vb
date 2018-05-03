Public MustInherit Class GenericMethodAttribute
    Inherits Attribute

    Private description As String
    Private name As String
    Private lastChangedTime As String

    Public ReadOnly Property Notes() As String
        Get
            Return description
        End Get
    End Property

    Public ReadOnly Property Author() As String
        Get
            Return name
        End Get
    End Property

    Public Property LastChanged() As String
        Get
            Return lastChangedTime
        End Get
        Set(ByVal Value As String)
            lastChangedTime = Value
        End Set
    End Property

    Sub New(ByVal author As String, ByVal notes As String)
        name = author
        description = notes
    End Sub

    Sub New()
    End Sub

End Class
