'Imports System

'Imports NUnit.Framework

'Imports UIControls



''''<summary>
''''This is a test class for ReportTest and is intended
''''to contain all ReportTest Unit Tests
''''</summary>
'<TestFixture()>  _
'Public Class ReportTest


'

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
'    'Public Shared Sub MyClassInitialize()
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
'    <Test()> _
'    Public Sub WarningTest()
'        Dim actual As Byte
'        actual = Report.Warning
'        Verify.Inconclusive("Verify the correctness of this test method.")
'    End Sub

'    '''<summary>
'    '''A test for TestNumber
'    '''</summary>
'    <Test()> _
'    Public Sub TestNumberTest()
'        Dim actual As Byte
'        actual = Report.TestNumber
'        Verify.Inconclusive("Verify the correctness of this test method.")
'    End Sub

'    '''<summary>
'    '''A test for TestName
'    '''</summary>
'    <Test()> _
'    Public Sub TestNameTest()
'        Dim expected As String = String.Empty ' TODO: Initialize to an appropriate value
'        Dim actual As String
'        Report.TestName = expected
'        actual = Report.TestName
'        Verify.AreEqual(expected, actual)
'        Verify.Inconclusive("Verify the correctness of this test method.")
'    End Sub

'    '''<summary>
'    '''A test for StepNumber
'    '''</summary>
'    <Test()> _
'    Public Sub StepNumberTest()
'        Dim actual As Byte
'        actual = Report.StepNumber
'        Verify.Inconclusive("Verify the correctness of this test method.")
'    End Sub

'    '''<summary>
'    '''A test for StepName
'    '''</summary>
'    <Test()> _
'    Public Sub StepNameTest()
'        Dim expected As String = String.Empty ' TODO: Initialize to an appropriate value
'        Dim actual As String
'        Report.StepName = expected
'        actual = Report.StepName
'        Verify.AreEqual(expected, actual)
'        Verify.Inconclusive("Verify the correctness of this test method.")
'    End Sub

'    '''<summary>
'    '''A test for Runs
'    '''</summary>
'    <Test()> _
'    Public Sub RunsTest()
'        Dim expected As Integer = 0 ' TODO: Initialize to an appropriate value
'        Dim actual As Integer
'        Report.Runs = expected
'        actual = Report.Runs
'        Verify.AreEqual(expected, actual)
'        Verify.Inconclusive("Verify the correctness of this test method.")
'    End Sub

'    '''<summary>
'    '''A test for Pass
'    '''</summary>
'    <Test()> _
'    Public Sub PassTest()
'        Dim actual As Byte
'        actual = Report.Pass
'        Verify.Inconclusive("Verify the correctness of this test method.")
'    End Sub

'    '''<summary>
'    '''A test for Info
'    '''</summary>
'    <Test()> _
'    Public Sub InfoTest()
'        Dim actual As Byte
'        actual = Report.Info
'        Verify.Inconclusive("Verify the correctness of this test method.")
'    End Sub

'    '''<summary>
'    '''A test for Filter
'    '''</summary>
'    <Test()> _
'    Public Sub FilterTest()
'        Dim target As Report = New Report ' TODO: Initialize to an appropriate value
'        Dim expected As Byte = 0 ' TODO: Initialize to an appropriate value
'        Dim actual As Byte
'        target.Filter = expected
'        actual = target.Filter
'        Verify.AreEqual(expected, actual)
'        Verify.Inconclusive("Verify the correctness of this test method.")
'    End Sub

'    '''<summary>
'    '''A test for Fail
'    '''</summary>
'    <Test()> _
'    Public Sub FailTest()
'        Dim actual As Byte
'        actual = Report.Fail
'        Verify.Inconclusive("Verify the correctness of this test method.")
'    End Sub

'    '''<summary>
'    '''A test for EnableNothing
'    '''</summary>
'    <Test()> _
'    Public Sub EnableNothingTest()
'        Dim actual As Byte
'        actual = Report.EnableNothing
'        Verify.Inconclusive("Verify the correctness of this test method.")
'    End Sub

'    '''<summary>
'    '''A test for EnableInfoOnly
'    '''</summary>
'    <Test()> _
'    Public Sub EnableInfoOnlyTest()
'        Dim actual As Byte
'        actual = Report.EnableInfoOnly
'        Verify.Inconclusive("Verify the correctness of this test method.")
'    End Sub

'    '''<summary>
'    '''A test for EnableErrorsOnly
'    '''</summary>
'    <Test()> _
'    Public Sub EnableErrorsOnlyTest()
'        Dim actual As Byte
'        actual = Report.EnableErrorsOnly
'        Verify.Inconclusive("Verify the correctness of this test method.")
'    End Sub

'    '''<summary>
'    '''A test for EnableErrorsAndWarnings
'    '''</summary>
'    <Test()> _
'    Public Sub EnableErrorsAndWarningsTest()
'        Dim actual As Byte
'        actual = Report.EnableErrorsAndWarnings
'        Verify.Inconclusive("Verify the correctness of this test method.")
'    End Sub

'    '''<summary>
'    '''A test for EnableAllButInfo
'    '''</summary>
'    <Test()> _
'    Public Sub EnableAllButInfoTest()
'        Dim actual As Byte
'        actual = Report.EnableAllButInfo
'        Verify.Inconclusive("Verify the correctness of this test method.")
'    End Sub

'    '''<summary>
'    '''A test for EnableAll
'    '''</summary>
'    <Test()> _
'    Public Sub EnableAllTest()
'        Dim actual As Byte
'        actual = Report.EnableAll
'        Verify.Inconclusive("Verify the correctness of this test method.")
'    End Sub

'    '''<summary>
'    '''A test for CurrentRun
'    '''</summary>
'    <Test()> _
'    Public Sub CurrentRunTest()
'        Dim actual As Integer
'        actual = Report.CurrentRun
'        Verify.Inconclusive("Verify the correctness of this test method.")
'    End Sub

'    '''<summary>
'    '''A test for UpdateTestResults
'    '''</summary>
'    <Test(), _
'     DeploymentItem("InterAction.dll")> _
'    Public Sub UpdateTestResultsTest()
'        Dim target As Report_Accessor = New Report_Accessor ' TODO: Initialize to an appropriate value
'        Dim reporterType As Byte = 0 ' TODO: Initialize to an appropriate value
'        Dim expected As Boolean = False ' TODO: Initialize to an appropriate value
'        Dim actual As Boolean
'        actual = target.UpdateTestResults(reporterType)
'        Verify.AreEqual(expected, actual)
'        Verify.Inconclusive("Verify the correctness of this test method.")
'    End Sub

'    '''<summary>
'    '''A test for StartReport
'    '''</summary>
'    <Test(), _
'     DeploymentItem("InterAction.dll")> _
'    Public Sub StartReportTest()
'        Dim target As Report_Accessor = New Report_Accessor ' TODO: Initialize to an appropriate value
'        target.StartReport()
'        Verify.Inconclusive("A method that does not return a value cannot be verified.")
'    End Sub

'    '''<summary>
'    '''A test for ResetReporter
'    '''</summary>
'    <Test(), _
'     DeploymentItem("InterAction.dll")> _
'    Public Sub ResetReporterTest()
'        Dim target As Report_Accessor = New Report_Accessor ' TODO: Initialize to an appropriate value
'        target.ResetReporter()
'        Verify.Inconclusive("A method that does not return a value cannot be verified.")
'    End Sub

'    '''<summary>
'    '''A test for RecordEvent
'    '''</summary>
'    <Test()> _
'    Public Sub RecordEventTest()
'        Dim target As Report = New Report ' TODO: Initialize to an appropriate value
'        Dim TypeOfMessage As Byte = 0 ' TODO: Initialize to an appropriate value
'        Dim MainMessage As String = String.Empty ' TODO: Initialize to an appropriate value
'        Dim AdditionalDetails As String = String.Empty ' TODO: Initialize to an appropriate value
'        target.RecordEvent(TypeOfMessage, MainMessage, AdditionalDetails)
'        Verify.Inconclusive("A method that does not return a value cannot be verified.")
'    End Sub

'    '''<summary>
'    '''A test for NextTest
'    '''</summary>
'    <Test()> _
'    Public Sub NextTestTest()
'        Dim target As Report = New Report ' TODO: Initialize to an appropriate value
'        Dim NextTestName As String = String.Empty ' TODO: Initialize to an appropriate value
'        Dim NextStepName As String = String.Empty ' TODO: Initialize to an appropriate value
'        target.NextTest(NextTestName, NextStepName)
'        Verify.Inconclusive("A method that does not return a value cannot be verified.")
'    End Sub

'    '''<summary>
'    '''A test for NextStep
'    '''</summary>
'    <Test()> _
'    Public Sub NextStepTest()
'        Dim target As Report = New Report ' TODO: Initialize to an appropriate value
'        Dim NextStepName As String = String.Empty ' TODO: Initialize to an appropriate value
'        target.NextStep(NextStepName)
'        Verify.Inconclusive("A method that does not return a value cannot be verified.")
'    End Sub

'    '''<summary>
'    '''A test for NextRun
'    '''</summary>
'    <Test()> _
'    Public Sub NextRunTest()
'        Dim expected As Boolean = False ' TODO: Initialize to an appropriate value
'        Dim actual As Boolean
'        actual = Report.NextRun
'        Verify.AreEqual(expected, actual)
'        Verify.Inconclusive("Verify the correctness of this test method.")
'    End Sub

'    '''<summary>
'    '''A test for GetTime
'    '''</summary>
'    <Test(), _
'     DeploymentItem("InterAction.dll")> _
'    Public Sub GetTimeTest()
'        Dim target As Report_Accessor = New Report_Accessor ' TODO: Initialize to an appropriate value
'        Dim expected As String = String.Empty ' TODO: Initialize to an appropriate value
'        Dim actual As String
'        actual = target.GetTime
'        Verify.AreEqual(expected, actual)
'        Verify.Inconclusive("Verify the correctness of this test method.")
'    End Sub

'    '''<summary>
'    '''A test for GetSQLRunName
'    '''</summary>
'    <Test(), _
'     DeploymentItem("InterAction.dll")> _
'    Public Sub GetSQLRunNameTest()
'        Dim expected As String = String.Empty ' TODO: Initialize to an appropriate value
'        Dim actual As String
'        actual = Report_Accessor.GetSQLRunName
'        Verify.AreEqual(expected, actual)
'        Verify.Inconclusive("Verify the correctness of this test method.")
'    End Sub

'    '''<summary>
'    '''A test for GetSQLRunLineName
'    '''</summary>
'    <Test(), _
'     DeploymentItem("InterAction.dll")> _
'    Public Sub GetSQLRunLineNameTest()
'        Dim expected As String = String.Empty ' TODO: Initialize to an appropriate value
'        Dim actual As String
'        actual = Report_Accessor.GetSQLRunLineName
'        Verify.AreEqual(expected, actual)
'        Verify.Inconclusive("Verify the correctness of this test method.")
'    End Sub

'    '''<summary>
'    '''A test for FixSQLStr
'    '''</summary>
'    <Test(), _
'     DeploymentItem("InterAction.dll")> _
'    Public Sub FixSQLStrTest()
'        Dim target As Report_Accessor = New Report_Accessor ' TODO: Initialize to an appropriate value
'        Dim Str As String = String.Empty ' TODO: Initialize to an appropriate value
'        Dim expected As String = String.Empty ' TODO: Initialize to an appropriate value
'        Dim actual As String
'        actual = target.FixSQLStr(Str)
'        Verify.AreEqual(expected, actual)
'        Verify.Inconclusive("Verify the correctness of this test method.")
'    End Sub

'    '''<summary>
'    '''A test for Report Constructor
'    '''</summary>
'    <Test()> _
'    Public Sub ReportConstructorTest1()
'        Dim GUID As Guid = New Guid ' TODO: Initialize to an appropriate value
'        Dim ReportFilePath As String = String.Empty ' TODO: Initialize to an appropriate value
'        Dim ProjectName As String = String.Empty ' TODO: Initialize to an appropriate value
'        Dim RunTypeOfficial As Boolean = False ' TODO: Initialize to an appropriate value
'        Dim target As Report = New Report(GUID, ReportFilePath, ProjectName, RunTypeOfficial)
'        Verify.Inconclusive("TODO: Implement code to verify target")
'    End Sub

'    '''<summary>
'    '''A test for Report Constructor
'    '''</summary>
'    <Test()> _
'    Public Sub ReportConstructorTest()
'        Dim target As Report = New Report
'        Verify.Inconclusive("TODO: Implement code to verify target")
'    End Sub
'End Class
