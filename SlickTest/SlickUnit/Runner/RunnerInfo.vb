Imports System.Runtime.Remoting.Lifetime

Public Class RunnerInfo
    Inherits MarshalByRefObject
    Implements IRunnerInfo

    Public Sub New()
        InternalAutomaticallyAttachConsoleHandler = True
        FilteredNamespace = Nothing
    End Sub


    Private InternalFramework As SlickUnit.Framework
    Private InternalClassTypeFilter As System.Type
    Private InternalTestAttributeFilter As System.Type
    Private InternalClassTypeAttributeFilter As System.Type
    Private InternalExactMethodName As String
    Private InternalLikeMethodName As System.Collections.Generic.List(Of String)
    Private InternalNotLikeMethodName As System.Collections.Generic.List(Of String)
    Private InternalTest As MethodAndTypeInfo
    Private InternalClassInstance As Object
    Private InternalAutomaticallyAttachConsoleHandler As Boolean
    Private InternalFilteredNamespace As String
    Private InternalExactCase As Boolean

    Public Function CreateCopy(ByVal InfoToCopy As IRunnerInfo) As IRunnerInfo Implements IRunnerInfo.CreateCopy
        Dim CopyToCreate As New RunnerInfo()
        CopyToCreate.AutomaticallyAttachConsoleHandler = InfoToCopy.AutomaticallyAttachConsoleHandler
        CopyToCreate.ClassInstance = InfoToCopy.ClassInstance
        CopyToCreate.ClassTypeAttributeFilter = InfoToCopy.ClassTypeAttributeFilter
        CopyToCreate.ClassTypeFilter = InfoToCopy.ClassTypeFilter
        CopyToCreate.ExactCase = InfoToCopy.ExactCase
        CopyToCreate.ExactMethodName = InfoToCopy.ExactMethodName
        CopyToCreate.FilteredNamespace = InfoToCopy.FilteredNamespace
        CopyToCreate.Framework = InfoToCopy.Framework
        CopyToCreate.LikeMethodName = InfoToCopy.LikeMethodName
        CopyToCreate.NotLikeMethodName = InfoToCopy.NotLikeMethodName
        CopyToCreate.Test = InfoToCopy.Test
        CopyToCreate.TestAttributeFilter = InfoToCopy.TestAttributeFilter

        Return CopyToCreate
    End Function



    Public Property ExactMethodName() As String Implements IRunnerInfo.ExactMethodName
        Get
            Return InternalExactMethodName
        End Get
        Set(ByVal value As String)
            InternalExactMethodName = value
        End Set
    End Property

    Public Property ClassInstance() As Object Implements IRunnerInfo.ClassInstance
        Get
            Return InternalClassInstance
        End Get
        Set(ByVal value As Object)
            InternalClassInstance = value
        End Set
    End Property

    Public Property ExactCase() As Boolean Implements IRunnerInfo.ExactCase
        Get
            Return InternalExactCase
        End Get
        Set(ByVal value As Boolean)
            InternalExactCase = value
        End Set
    End Property

    Public Property AutomaticallyAttachConsoleHandler() As Boolean Implements IRunnerInfo.AutomaticallyAttachConsoleHandler
        Get
            Return InternalAutomaticallyAttachConsoleHandler
        End Get
        Set(ByVal value As Boolean)
            InternalAutomaticallyAttachConsoleHandler = value
        End Set
    End Property

    Public Property FilteredNamespace() As String Implements IRunnerInfo.FilteredNamespace
        Get
            Return InternalFilteredNamespace
        End Get
        Set(ByVal value As String)
            InternalFilteredNamespace = value
        End Set
    End Property

    Public Property ClassTypeAttributeFilter() As System.Type Implements IRunnerInfo.ClassTypeAttributeFilter
        Get
            Return InternalClassTypeAttributeFilter
        End Get
        Set(ByVal value As System.Type)
            InternalClassTypeAttributeFilter = value
        End Set
    End Property

    Public Property ClassTypeFilter() As System.Type Implements IRunnerInfo.ClassTypeFilter
        Get
            Return InternalClassTypeFilter
        End Get
        Set(ByVal value As System.Type)
            InternalClassTypeFilter = value
        End Set
    End Property

    Public Property Framework() As Framework Implements IRunnerInfo.Framework
        Get
            Return InternalFramework
        End Get
        Set(ByVal value As Framework)
            InternalFramework = value
        End Set
    End Property

    Public Property LikeMethodName() As System.Collections.Generic.List(Of String) Implements IRunnerInfo.LikeMethodName
        Get
            Return InternalLikeMethodName
        End Get
        Set(ByVal value As System.Collections.Generic.List(Of String))
            InternalLikeMethodName = value
        End Set
    End Property

    Public Property NotLikeMethodName() As System.Collections.Generic.List(Of String) Implements IRunnerInfo.NotLikeMethodName
        Get
            Return InternalNotLikeMethodName
        End Get
        Set(ByVal value As System.Collections.Generic.List(Of String))
            InternalNotLikeMethodName = value
        End Set
    End Property

    Public Property Test() As MethodAndTypeInfo Implements IRunnerInfo.Test
        Get
            Return InternalTest
        End Get
        Set(ByVal value As MethodAndTypeInfo)
            InternalTest = value
        End Set
    End Property

    Public Property TestAttributeFilter() As System.Type Implements IRunnerInfo.TestAttributeFilter
        Get
            Return InternalTestAttributeFilter
        End Get
        Set(ByVal value As System.Type)
            InternalTestAttributeFilter = value
        End Set
    End Property

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