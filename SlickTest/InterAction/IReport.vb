''' <summary>
''' The interface for Slick Test's reporting.  This allows a user to 
''' replace Slick Test's reporter with their own.  A few limitations
''' involving the usage of canceling a test. 
''' </summary>
''' <remarks></remarks>
Public Interface IReport
    ''' <summary>
    ''' The number of runs you want to go through.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property Runs() As Integer
    ''' <summary>
    ''' The current run the report is on.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ReadOnly Property CurrentRun() As Integer
    ''' <summary>
    ''' The step in the test.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ReadOnly Property StepNumber() As Byte
    ''' <summary>
    ''' The name of the test.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property TestName() As String
    ''' <summary>
    ''' The name of the step.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property StepName() As String
    ''' <summary>
    ''' The test number that you are on.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ReadOnly Property TestNumber() As Integer
    ''' <summary>
    ''' Places the report on the next step in the test.
    ''' </summary>
    ''' <param name="NextStepName">The name of the next step, if a name is given.</param>
    ''' <remarks></remarks>
    Sub NextStep(Optional ByVal NextStepName As String = "")
    ''' <summary>
    ''' Places the report on the next test, first step.
    ''' </summary>
    ''' <param name="NextTestName">The name of the next test, if a name is given.</param>
    ''' <param name="NextStepName">The name of the next step, if a name is given.</param>
    ''' <remarks></remarks>
    Sub NextTest(Optional ByVal NextTestName As String = "", Optional ByVal NextStepName As String = "")
    ''' <summary>
    ''' Starts the next run.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function NextRun() As Boolean
    ''' <summary>
    ''' The value given for a warning in a test.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ReadOnly Property Warning() As Byte
    ''' <summary>
    ''' The value given for a fail or error in a test.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ReadOnly Property Fail() As Byte
    ''' <summary>
    ''' The value given for debugging information in a test.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ReadOnly Property Info() As Byte
    ''' <summary>
    ''' The value given for a pass in a test.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ReadOnly Property Pass() As Byte
    ''' <summary>
    ''' The value given to the filter to allow all test results into the report.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ReadOnly Property EnableAll() As Byte
    ''' <summary>
    ''' The value given to the filter to allow fail and warnings into the test results report.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ReadOnly Property EnableErrorsAndWarnings() As Byte
    ''' <summary>
    ''' The value given to the filter to allow info into the test results report.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ReadOnly Property EnableInfoOnly() As Byte
    ''' <summary>
    ''' The value given to the filter to allow errors into the test results report.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ReadOnly Property EnableErrorsOnly() As Byte
    ''' <summary>
    ''' The value given to the filter to allow nothing into the test results report.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ReadOnly Property EnableNothing() As Byte
    ''' <summary>
    ''' The value given to the filter to allow all but info into the test results report.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ReadOnly Property EnableAllButInfo() As Byte
    ''' <summary>
    ''' The filter to provide or prevent test results from entering the database.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property Filter() As Byte
    ''' <summary>
    ''' Records the results.
    ''' </summary>
    ''' <param name="TypeOfMessage">The message type (i.e. Pass,Fail, etc.)</param>
    ''' <param name="MainMessage">The message you wish to record.</param>
    ''' <param name="AdditionalDetails">Any addtional details to record if any are provided.</param>
    ''' <remarks></remarks>
    Sub RecordEvent(ByVal TypeOfMessage As Byte, ByVal MainMessage As String, Optional ByVal AdditionalDetails As String = "")
    ''' <summary>
    ''' Records the results.
    ''' </summary>
    ''' <param name="AssertValue">A value tested at run time.  If true, the message will not be
    ''' recorded.</param>
    ''' <param name="TypeOfMessage">The message type (i.e. Pass,Fail, etc.)</param>
    ''' <param name="MainMessage">The message you wish to record.</param>
    ''' <param name="AdditionalDetails">Any addtional details to record if any are provided.</param>
    ''' <remarks></remarks>
    Sub RecordEventAssert(ByVal AssertValue As Boolean, ByVal TypeOfMessage As Byte, ByVal MainMessage As String, Optional ByVal AdditionalDetails As String = "")
    ''' <summary>
    ''' Provides a network connection string.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property ReportConnectionString() As String
End Interface