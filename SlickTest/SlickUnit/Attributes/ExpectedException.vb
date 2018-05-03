''' <summary>
''' Expecting an for a test method exception.
''' </summary>
''' <remarks></remarks>
<AttributeUsage(AttributeTargets.Method)> _
Public Class ExpectedException
    Inherits Attribute

    Private _type As System.Type

    Public ReadOnly Property Type() As System.Type
        Get
            Return _type
        End Get
    End Property

    Sub New(ByVal type As String)
        _type = GetType(Type)
    End Sub

    Sub New(ByVal type As System.Type)
        _type = type
    End Sub
End Class
