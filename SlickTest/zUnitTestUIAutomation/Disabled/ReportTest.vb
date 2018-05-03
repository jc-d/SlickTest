'Imports System

'Imports Microsoft.VisualStudio.TestTools.UnitTesting

'Imports UIControls



''''<summary>
''''This is a test class for ReportTest and is intended
''''to contain all ReportTest Unit Tests
''''</summary>
'<TestClass()> _
'Public Class ReportTest


'    Private testContextInstance As TestContext

'    '''<summary>
'    '''Gets or sets the test context which provides
'    '''information about and functionality for the current test run.
'    '''</summary>
'    Public Property TestContext() As TestContext
'        Get
'            Return testContextInstance
'        End Get
'        Set(ByVal value As TestContext)
'            testContextInstance = Value
'        End Set
'    End Property

'#Region "Additional test attributes"
'    '
'    'You can use the following additional attributes as you write your tests:
'    '
'    'Use ClassInitialize to run code before running the first test in the class
'    '<ClassInitialize()>  _
'    'Public Shared Sub MyClassInitialize(ByVal testContext As TestContext)
'    'End Sub
'    '
'    'Use ClassCleanup to run code after all tests in a class have run
'    '<ClassCleanup()>  _
'    'Public Shared Sub MyClassCleanup()
'    'End Sub
'    '
'    'Use TestInitialize to run code before running each test
'    '<TestInitialize()>  _
'    'Public Sub MyTestInitialize()
'    'End Sub
'    '
'    'Use TestCleanup to run code after each test has run
'    '<TestCleanup()>  _
'    'Public Sub MyTestCleanup()
'    'End Sub
'    '
'#End Region


'    '''<summary>
'    '''A test for Warning
'    '''</summary>
'    <TestMethod()> _
'    Public Sub WarningTest()
'        Dim actual As Byte
'        actual = Report.Warning
'        Assert.Inconclusive("Verify the correctness of this test method.")
'    End Sub

'    '''<summary>
'    '''A test for TestNumber
'    '''</summary>
'    <TestMethod()> _
'    Public Sub TestNumberTest()
'        Dim actual As Byte
'        actual = Report.TestNumber
'        Assert.Inconclusive("Verify the correctness of this test method.")
'    End Sub

'    '''<summary>
'    '''A test for TestName
'    '''</summary>
'    <TestMethod()> _
'    Public Sub TestNameTest()
'        Dim expected As String = String.Empty ' TODO: Initialize to an appropriate value
'        Dim actual As String
'        Report.TestName = expected
'        actual = Report.TestName
'        Assert.AreEqual(expected, actual)
'        Assert.Inconclusive("Verify the correctness of this test method.")
'    End Sub

'    '''<summary>
'    '''A test for StepNumber
'    '''</summary>
'    <TestMethod()> _
'    Public Sub StepNumberTest()
'        Dim actual As Byte
'        actual = Report.StepNumber
'        Assert.Inconclusive("Verify the correctness of this test method.")
'    End Sub

'    '''<summary>
'    '''A test for StepName
'    '''</summary>
'    <TestMethod()> _
'    Public Sub StepNameTest()
'        Dim expected As String = String.Empty ' TODO: Initialize to an appropriate value
'        Dim actual As String
'        Report.StepName = expected
'        actual = Report.StepName
'        Assert.AreEqual(expected, actual)
'        Assert.Inconclusive("Verify the correctness of this test method.")
'    End Sub

'    '''<summary>
'    '''A test for Runs
'    '''</summary>
'    <TestMethod()> _
'    Public Sub RunsTest()
'        Dim expected As Integer = 0 ' TODO: Initialize to an appropriate value
'        Dim actual As Integer
'        Report.Runs = expected
'        actual = Report.Runs
'        Assert.AreEqual(expected, actual)
'        Assert.Inconclusive("Verify the correctness of this test method.")
'    End Sub

'    '''<summary>
'    '''A test for Pass
'    '''</summary>
'    <TestMethod()> _
'    Public Sub PassTest()
'        Dim actual As Byte
'        actual = Report.Pass
'        Assert.Inconclusive("Verify the correctness of this test method.")
'    End Sub

'    '''<summary>
'    '''A test for Info
'    '''</summary>
'    <TestMethod()> _
'    Public Sub InfoTest()
'        Dim actual As Byte
'        actual = Report.Info
'        Assert.Inconclusive("Verify the correctness of this test method.")
'    End Sub

'    '''<summary>
'    '''A test for Filter
'    '''</summary>
'    <TestMethod()> _
'    Public Sub FilterTest()
'        Dim target As Report = New Report ' TODO: Initialize to an appropriate value
'        Dim expected As Byte = 0 ' TODO: Initialize to an appropriate value
'        Dim actual As Byte
'        target.Filter = expected
'        actual = target.Filter
'        Assert.AreEqual(expected, actual)
'        Assert.Inconclusive("Verify the correctness of this test method.")
'    End Sub

'    '''<summary>
'    '''A test for Fail
'    '''</summary>
'    <TestMethod()> _
'    Public Sub FailTest()
'        Dim actual As Byte
'        actual = Report.Fail
'        Assert.Inconclusive("Verify the correctness of this test method.")
'    End Sub

'    '''<summary>
'    '''A test for EnableNothing
'    '''</summary>
'    <TestMethod()> _
'    Public Sub EnableNothingTest()
'        Dim actual As Byte
'        actual = Report.EnableNothing
'        Assert.Inconclusive("Verify the correctness of this test method.")
'    End Sub

'    '''<summary>
'    '''A test for EnableInfoOnly
'    '''</summary>
'    <TestMethod()> _
'    Public Sub EnableInfoOnlyTest()
'        Dim actual As Byte
'        actual = Report.EnableInfoOnly
'        Assert.Inconclusive("Verify the correctness of this test method.")
'    End Sub

'    '''<summary>
'    '''A test for EnableErrorsOnly
'    '''</summary>
'    <TestMethod()> _
'    Public Sub EnableErrorsOnlyTest()
'        Dim actual As Byte
'        actual = Report.EnableErrorsOnly
'        Assert.Inconclusive("Verify the correctness of this test method.")
'    End Sub

'    '''<summary>
'    '''A test for EnableErrorsAndWarnings
'    '''</summary>
'    <TestMethod()> _
'    Public Sub EnableErrorsAndWarningsTest()
'        Dim actual As Byte
'        actual = Report.EnableErrorsAndWarnings
'        Assert.Inconclusive("Verify the correctness of this test method.")
'    End Sub

'    '''<summary>
'    '''A test for EnableAllButInfo
'    '''</summary>
'    <TestMethod()> _
'    Public Sub EnableAllButInfoTest()
'        Dim actual As Byte
'        actual = Report.EnableAllButInfo
'        Assert.Inconclusive("Verify the correctness of this test method.")
'    End Sub

'    '''<summary>
'    '''A test for EnableAll
'    '''</summary>
'    <TestMethod()> _
'    Public Sub EnableAllTest()
'        Dim actual As Byte
'        actual = Report.EnableAll
'        Assert.Inconclusive("Verify the correctness of this test method.")
'    End Sub

'    '''<summary>
'    '''A test for CurrentRun
'    '''</summary>
'    <TestMethod()> _
'    Public Sub CurrentRunTest()
'        Dim actual As Integer
'        actual = Report.CurrentRun
'        Assert.Inconclusive("Verify the correctness of this test method.")
'    End Sub

'    '''<summary>
'    '''A test for UpdateTestResults
'    '''</summary>
'    <TestMethod(), _
'     DeploymentItem("InterAction.dll")> _
'    Public Sub UpdateTestResultsTest()
'        Dim target As Report_Accessor = New Report_Accessor ' TODO: Initialize to an appropriate value
'        Dim reporterType As Byte = 0 ' TODO: Initialize to an appropriate value
'        Dim expected As Boolean = False ' TODO: Initialize to an appropriate value
'        Dim actual As Boolean
'        actual = target.UpdateTestResults(reporterType)
'        Assert.AreEqual(expected, actual)
'        Assert.Inconclusive("Verify the correctness of this test method.")
'    End Sub

'    '''<summary>
'    '''A test for StartReport
'    '''</summary>
'    <TestMethod(), _
'     DeploymentItem("InterAction.dll")> _
'    Public Sub StartReportTest()
'        Dim target As Report_Accessor = New Report_Accessor ' TODO: Initialize to an appropriate value
'        target.StartReport()
'        Assert.Inconclusive("A method that does not return a value cannot be verified.")
'    End Sub

'    '''<summary>
'    '''A test for ResetReporter
'    '''</summary>
'    <TestMethod(), _
'     DeploymentItem("InterAction.dll")> _
'    Public Sub ResetReporterTest()
'        Dim target As Report_Accessor = New Report_Accessor ' TODO: Initialize to an appropriate value
'        target.ResetReporter()
'        Assert.Inconclusive("A method that does not return a value cannot be verified.")
'    End Sub

'    '''<summary>
'    '''A test for RecordEvent
'    '''</summary>
'    <TestMethod()> _
'    Public Sub RecordEventTest()
'        Dim target As Report = New Report ' TODO: Initialize to an appropriate value
'        Dim TypeOfMessage As Byte = 0 ' TODO: Initialize to an appropriate value
'        Dim MainMessage As String = String.Empty ' TODO: Initialize to an appropriate value
'        Dim AdditionalDetails As String = String.Empty ' TODO: Initialize to an appropriate value
'        target.RecordEvent(TypeOfMessage, MainMessage, AdditionalDetails)
'        Assert.Inconclusive("A method that does not return a value cannot be verified.")
'    End Sub

'    '''<summary>
'    '''A test for NextTest
'    '''</summary>
'    <TestMethod()> _
'    Public Sub NextTestTest()
'        Dim target As Report = New Report ' TODO: Initialize to an appropriate value
'        Dim NextTestName As String = String.Empty ' TODO: Initialize to an appropriate value
'        Dim NextStepName As String = String.Empty ' TODO: Initialize to an appropriate value
'        target.NextTest(NextTestName, NextStepName)
'        Assert.Inconclusive("A method that does not return a value cannot be verified.")
'    End Sub

'    '''<summary>
'    '''A test for NextStep
'    '''</summary>
'    <TestMethod()> _
'    Public Sub NextStepTest()
'        Dim target As Report = New Report ' TODO: Initialize to an appropriate value
'        Dim NextStepName As String = String.Empty ' TODO: Initialize to an appropriate value
'        target.NextStep(NextStepName)
'        Assert.Inconclusive("A method that does not return a value cannot be verified.")
'    End Sub

'    '''<summary>
'    '''A test for NextRun
'    '''</summary>
'    <TestMethod()> _
'    Public Sub NextRunTest()
'        Dim expected As Boolean = False ' TODO: Initialize to an appropriate value
'        Dim actual As Boolean
'        actual = Report.NextRun
'        Assert.AreEqual(expected, actual)
'        Assert.Inconclusive("Verify the correctness of this test method.")
'    End Sub

'    '''<summary>
'    '''A test for GetTime
'    '''</summary>
'    <TestMethod(), _
'     DeploymentItem("InterAction.dll")> _
'    Public Sub GetTimeTest()
'        Dim target As Report_Accessor = New Report_Accessor ' TODO: Initialize to an appropriate value
'        Dim expected As String = String.Empty ' TODO: Initialize to an appropriate value
'        Dim actual As String
'        actual = target.GetTime
'        Assert.AreEqual(expected, actual)
'        Assert.Inconclusive("Verify the correctness of this test method.")
'    End Sub

'    '''<summary>
'    '''A test for GetSQLRunName
'    '''</summary>
'    <TestMethod(), _
'     DeploymentItem("InterAction.dll")> _
'    Public Sub GetSQLRunNameTest()
'        Dim expected As String = String.Empty ' TODO: Initialize to an appropriate value
'        Dim actual As String
'        actual = Report_Accessor.GetSQLRunName
'        Assert.AreEqual(expected, actual)
'        Assert.Inconclusive("Verify the correctness of this test method.")
'    End Sub

'    '''<summary>
'    '''A test for GetSQLRunLineName
'    '''</summary>
'    <TestMethod(), _
'     DeploymentItem("InterAction.dll")> _
'    Public Sub GetSQLRunLineNameTest()
'        Dim expected As String = String.Empty ' TODO: Initialize to an appropriate value
'        Dim actual As String
'        actual = Report_Accessor.GetSQLRunLineName
'        Assert.AreEqual(expected, actual)
'        Assert.Inconclusive("Verify the correctness of this test method.")
'    End Sub

'    '''<summary>
'    '''A test for FixSQLStr
'    '''</summary>
'    <TestMethod(), _
'     DeploymentItem("InterAction.dll")> _
'    Public Sub FixSQLStrTest()
'        Dim target As Report_Accessor = New Report_Accessor ' TODO: Initialize to an appropriate value
'        Dim Str As String = String.Empty ' TODO: Initialize to an appropriate value
'        Dim expected As String = String.Empty ' TODO: Initialize to an appropriate value
'        Dim actual As String
'        actual = target.FixSQLStr(Str)
'        Assert.AreEqual(expected, actual)
'        Assert.Inconclusive("Verify the correctness of this test method.")
'    End Sub

'    '''<summary>
'    '''A test for Report Constructor
'    '''</summary>
'    <TestMethod()> _
'    Public Sub ReportConstructorTest1()
'        Dim GUID As Guid = New Guid ' TODO: Initialize to an appropriate value
'        Dim ReportFilePath As String = String.Empty ' TODO: Initialize to an appropriate value
'        Dim ProjectName As String = String.Empty ' TODO: Initialize to an appropriate value
'        Dim RunTypeOfficial As Boolean = False ' TODO: Initialize to an appropriate value
'        Dim target As Report = New Report(GUID, ReportFilePath, ProjectName, RunTypeOfficial)
'        Assert.Inconclusive("TODO: Implement code to verify target")
'    End Sub

'    '''<summary>
'    '''A test for Report Constructor
'    '''</summary>
'    <TestMethod()> _
'    Public Sub ReportConstructorTest()
'        Dim target As Report = New Report
'        Assert.Inconclusive("TODO: Implement code to verify target")
'    End Sub
'End Class
