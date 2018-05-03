''' <summary>
''' Provided to replace the default reporting so that you can disable all internal
''' reporting.
''' </summary>
''' <remarks></remarks>
Public Class StubbedReport
    Implements IReport

    ''' <summary>
    ''' Returns 0.
    ''' </summary>
    ''' <returns>Returns 0.</returns>
    Public ReadOnly Property CurrentRun() As Integer Implements IReport.CurrentRun
        Get
            Return 0
        End Get
    End Property

    ''' <summary>
    ''' Returns 0.
    ''' </summary>
    ''' <returns>Returns 0.</returns>
    Public ReadOnly Property EnableAll() As Byte Implements IReport.EnableAll
        Get
            Return 0
        End Get
    End Property

    ''' <summary>
    ''' Returns 0.
    ''' </summary>
    ''' <returns>Returns 0.</returns>
    Public ReadOnly Property EnableAllButInfo() As Byte Implements IReport.EnableAllButInfo
        Get
            Return 0
        End Get
    End Property

    ''' <summary>
    ''' Returns 0.
    ''' </summary>
    ''' <returns>Returns 0.</returns>
    Public ReadOnly Property EnableErrorsAndWarnings() As Byte Implements IReport.EnableErrorsAndWarnings
        Get
            Return 0
        End Get
    End Property

    ''' <summary>
    ''' Returns 0.
    ''' </summary>
    ''' <returns>Returns 0.</returns>
    Public ReadOnly Property EnableErrorsOnly() As Byte Implements IReport.EnableErrorsOnly
        Get
            Return 0
        End Get
    End Property

    ''' <summary>
    ''' Returns 0.
    ''' </summary>
    ''' <returns>Returns 0.</returns>
    Public ReadOnly Property EnableInfoOnly() As Byte Implements IReport.EnableInfoOnly
        Get
            Return 0
        End Get
    End Property

    ''' <summary>
    ''' Returns 0.
    ''' </summary>
    ''' <returns>Returns 0.</returns>
    Public ReadOnly Property EnableNothing() As Byte Implements IReport.EnableNothing
        Get
            Return 0
        End Get
    End Property

    ''' <summary>
    ''' Returns 0.
    ''' </summary>
    ''' <returns>Returns 0.</returns>
    Public ReadOnly Property Fail() As Byte Implements IReport.Fail
        Get
            Return 0
        End Get
    End Property

    ''' <summary>
    ''' Returns 0.
    ''' </summary>
    ''' <returns>Returns 0.</returns>
    Public Property Filter() As Byte Implements IReport.Filter
        Get
            Return 0
        End Get
        Set(ByVal value As Byte)

        End Set
    End Property

    ''' <summary>
    ''' Returns 0.
    ''' </summary>
    ''' <returns>Returns 0.</returns>
    Public ReadOnly Property Info() As Byte Implements IReport.Info
        Get
            Return 0
        End Get
    End Property

    ''' <summary>
    ''' Does nothing.
    ''' </summary>
    Public Function NextRun() As Boolean Implements IReport.NextRun
        Return False
    End Function

    ''' <summary>
    ''' Does nothing.
    ''' </summary>
    Public Sub NextStep(Optional ByVal NextStepName As String = "") Implements IReport.NextStep
    End Sub

    ''' <summary>
    ''' Does nothing.
    ''' </summary>
    Public Sub NextTest(Optional ByVal NextTestName As String = "", Optional ByVal NextStepName As String = "") Implements IReport.NextTest
    End Sub

    ''' <summary>
    ''' Returns 0.
    ''' </summary>
    ''' <returns>Returns 0.</returns>
    Public ReadOnly Property Pass() As Byte Implements IReport.Pass
        Get
            Return 0
        End Get
    End Property

    ''' <summary>
    ''' Does nothing.
    ''' </summary>
    Public Sub RecordEvent(ByVal TypeOfMessage As Byte, ByVal MainMessage As String, Optional ByVal AdditionalDetails As String = "") Implements IReport.RecordEvent
    End Sub

    ''' <summary>
    ''' Does nothing.
    ''' </summary>
    Public Sub RecordEventAssert(ByVal AssertValue As Boolean, ByVal TypeOfMessage As Byte, ByVal MainMessage As String, Optional ByVal AdditionalDetails As String = "") Implements IReport.RecordEventAssert
    End Sub

    ''' <summary>
    ''' Returns an empty string.
    ''' </summary>
    ''' <returns>Returns an empty string.</returns>
    Public Property ReportConnectionString() As String Implements IReport.ReportConnectionString
        Get
            Return String.Empty
        End Get
        Set(ByVal value As String)
        End Set
    End Property

    ''' <summary>
    ''' Returns 0.
    ''' </summary>
    ''' <returns>Returns 0.</returns>
    Public Property Runs() As Integer Implements IReport.Runs
        Get
            Return 0
        End Get
        Set(ByVal value As Integer)
        End Set
    End Property

    ''' <summary>
    ''' Returns an empty string.
    ''' </summary>
    ''' <returns>Returns an empty string.</returns>
    Public Property StepName() As String Implements IReport.StepName
        Get
            Return String.Empty
        End Get
        Set(ByVal value As String)
        End Set
    End Property

    ''' <summary>
    ''' Returns 0.
    ''' </summary>
    ''' <returns>Returns 0.</returns>
    Public ReadOnly Property StepNumber() As Byte Implements IReport.StepNumber
        Get
            Return 0
        End Get
    End Property

    ''' <summary>
    ''' Returns an empty string.
    ''' </summary>
    ''' <returns>Returns an empty string.</returns>
    Public Property TestName() As String Implements IReport.TestName
        Get
            Return String.Empty
        End Get
        Set(ByVal value As String)
        End Set
    End Property

    ''' <summary>
    ''' Returns 0.
    ''' </summary>
    ''' <returns>Returns 0.</returns>
    Public ReadOnly Property TestNumber() As Integer Implements IReport.TestNumber
        Get
            Return 0
        End Get
    End Property

    ''' <summary>
    ''' Returns 0.
    ''' </summary>
    ''' <returns>Returns 0.</returns>
    Public ReadOnly Property Warning() As Byte Implements IReport.Warning
        Get
            Return 0
        End Get
    End Property
End Class