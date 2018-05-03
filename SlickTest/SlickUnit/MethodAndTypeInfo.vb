Imports System.Runtime.Serialization
Imports System.Runtime.Remoting.Lifetime

<Serializable()> _
Public Class MethodAndTypeInfo
    Inherits MarshalByRefObject
    'Implements ISerializable

    Public Method As System.Reflection.MethodInfo
    Public Type As System.Type
    Public ClassInstance As Object

    Public Function MethodContainsAttribute(ByVal t As Type) As Boolean
        Return MethodContainsAttribute(Method, t)
    End Function

    Public Shared Function MethodContainsAttribute(ByVal Method As System.Reflection.MethodInfo, ByVal t As Type) As Boolean
        If (Method Is Nothing) Then Return False

        For Each at As Object In Method.GetCustomAttributes(True)
            If (at.GetType().FullName = t.FullName) Then
                Return True
            End If
        Next
        Return False
    End Function

    Public Shared Function AnyMethodContainsAttribute(ByVal Methods As MethodAndTypeInfo(), ByVal t As Type) As Boolean
        If (Methods Is Nothing) Then Return False
        For Each m In Methods
            If (Not m Is Nothing) Then
                If (MethodContainsAttribute(m.Method, t)) Then Return True
            End If
        Next
        Return False
    End Function

    Public Shared Function AllMethodsContainsAttribute(ByVal Methods As MethodAndTypeInfo(), ByVal t As Type) As Boolean
        If (Methods Is Nothing) Then Return False
        For Each m In Methods
            If (m Is Nothing) Then Return False
            If (MethodContainsAttribute(m.Method, t) = False) Then Return False
        Next
        Return True
    End Function

    Public Sub New(ByVal MyMethod As System.Reflection.MethodInfo, ByVal MyType As System.Type)
        Me.Method = MyMethod
        Me.Type = MyType
    End Sub

    'Protected Sub New( _
    '  ByVal info As System.Runtime.Serialization.SerializationInfo, _
    '  ByVal context As System.Runtime.Serialization.StreamingContext)

    '    Method = DirectCast(info.GetValue("Method", GetType(System.Reflection.MethodInfo)), System.Reflection.MethodInfo)
    '    Type = DirectCast(info.GetValue("Type", GetType(System.Type)), System.Type)
    '    ClassInstance = info.GetValue("ClassInstance", GetType(System.Collections.Generic.List(Of String)))
    'End Sub

    'Public Sub GetObjectData(ByVal info As SerializationInfo, ByVal context As StreamingContext) Implements ISerializable.GetObjectData
    '    info.AddValue("Method", Method)
    '    info.AddValue("Type", Type)
    '    info.AddValue("ClassInstance", ClassInstance)
    'End Sub

    Public Overrides Function InitializeLifetimeService() As Object
        Dim lease As ILease = DirectCast(MyBase.InitializeLifetimeService(), ILease)
        If (lease.CurrentState = LeaseState.Initial) Then
            lease.InitialLeaseTime = TimeSpan.FromDays(45)
            lease.RenewOnCallTime = TimeSpan.FromDays(45)
            lease.SponsorshipTimeout = TimeSpan.FromDays(45)
        End If
        Return lease
    End Function

End Class