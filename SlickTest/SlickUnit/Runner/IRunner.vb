Public Interface IRunner

    Property SetupTestType() As System.Type
    Property CleanupTestType() As System.Type
    Property ClassAttributeToRunType() As System.Type
    Property SetupClassType() As System.Type
    Property TestAttributeType() As System.Type
    Property CleanupClassType() As System.Type
    Property UnitTestExceptionType() As System.Type
    Property IgnoreTestType() As System.Type
    Property ExpectedExceptionType() As System.Type
    Sub InitTypes(ByVal Framework As Framework)

    Property TestResults() As System.Collections.Generic.List(Of GenericTestResults)

    Sub ExecuteTest(ByVal Info As IRunnerInfo)
    Sub SetupTest(ByVal Info As IRunnerInfo)
    Sub CleanupTest(ByVal Info As IRunnerInfo)

    Function SetupClass(ByVal Info As IRunnerInfo) As Object
    Sub CleanupClass(ByVal Info As IRunnerInfo)
    Sub RunTestsInClass(ByVal Info As IRunnerInfo)
    Sub RunTestInClass(ByVal Info As IRunnerInfo)
    Sub RunTestsInNamespace(ByVal Info As IRunnerInfo)
End Interface