Public Class NUnitRunner
    Inherits GenericRunner

    Public Overrides Function CreateTestResult() As GenericTestResults
        Return New SlickUnitTestResults()
    End Function

    Public Overrides Sub InitTypes(ByVal Framework As Framework) 'TODO find out NUnit types
        Dim ImportsType As String = "NUnit.Framework."
        SetupTestType = Framework.GetTypeByString(ImportsType & "SetUpAttribute")
        CleanupTestType = Framework.GetTypeByString(ImportsType & "TearDownAttribute")
        ClassAttributeToRunType = Framework.GetTypeByString(ImportsType & "TestFixtureAttribute")
        SetupClassType = Framework.GetTypeByString(ImportsType & "TestFixtureSetUpAttribute")
        TestType = Framework.GetTypeByString(ImportsType & "TestAttribute")
        CleanupClassType = Framework.GetTypeByString(ImportsType & "TestFixtureTearDownAttribute")
        UnitTestExceptionType = Framework.GetTypeByString(ImportsType & "AssertionException")
        IgnoreTestType = Framework.GetTypeByString(ImportsType & "IgnoreAttribute")
        ExpectedExceptionType = Framework.GetTypeByString(ImportsType & "ExpectedExceptionAttribute")
    End Sub

    Public Sub New(ByVal Framework As Framework)
        MyBase.New(Framework)
    End Sub

    Public Sub New()
        MyBase.New()
    End Sub
End Class