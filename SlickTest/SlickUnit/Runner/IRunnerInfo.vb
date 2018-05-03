Public Interface IRunnerInfo
    Property Framework() As SlickUnit.Framework
    Property ClassTypeFilter() As System.Type
    Property TestAttributeFilter() As System.Type
    Property ClassTypeAttributeFilter() As System.Type
    Property ExactMethodName() As String
    Property LikeMethodName() As System.Collections.Generic.List(Of String)
    Property NotLikeMethodName() As System.Collections.Generic.List(Of String)
    Property Test() As MethodAndTypeInfo
    Property ClassInstance() As Object
    Property ExactCase() As Boolean
    Property AutomaticallyAttachConsoleHandler() As Boolean
    Property FilteredNamespace() As String
    Function CreateCopy(ByVal InfoToCopy As IRunnerInfo) As IRunnerInfo

End Interface