Public Class SlickRunner
    Inherits GenericRunner
    Implements SlickUnit.IRunner

    Public Overrides Sub InitTypes(ByVal Framework As Framework)
        SetupTestType = GetType(SetUp)
        CleanupTestType = GetType(TearDown)
        ClassAttributeToRunType = GetType(TestFixture)
        SetupClassType = GetType(TestFixtureSetUp)
        TestType = GetType(Test)
        CleanupClassType = GetType(TestFixtureTearDown)
        UnitTestExceptionType = GetType(SlickUnit.SlickUnitException)
        IgnoreTestType = GetType(Ignore)
        ExpectedExceptionType = GetType(ExpectedException)
    End Sub

    Public Overrides Function CreateTestResult() As GenericTestResults
        Return New SlickUnitTestResults()
    End Function

    Public Sub New(ByVal Framework As Framework)
        MyBase.New(Framework)
    End Sub

    Public Sub New()
        MyBase.New()
    End Sub
End Class


