#Const UseAttributes = 2 'set to 1 to enable attributes
#If (UseAttributes = 1) Then


Imports System
''' <summary>
''' Attributes that will allow test cases to be run in a certain order.
''' </summary>
''' <remarks>Attributes are currently not used, but is designed for future usage.</remarks>
<AttributeUsage(AttributeTargets.Class Or AttributeTargets.Method, AllowMultiple:=True)> _
Public Class TestCaseAttributes
    Private Priority As UIControls.AutomationSettings.TestCasePriority = UIControls.AutomationSettings.TestCasePriority.MediumPriority
    Private Name As String = String.Empty

    Public ReadOnly Property TestCasePriority() As UIControls.AutomationSettings.TestCasePriority
        Get
            Return Priority
        End Get
    End Property

    Public ReadOnly Property TestCaseName() As String
        Get
            Return Name
        End Get
    End Property
    ''' <summary>
    ''' Creates a TestCase rather than just running the default constructor.  This allows Slick Test to run
    ''' each test case individually.
    ''' </summary>
    ''' <param name="TestCaseName">The name or a simple description of the test case inside the project</param>
    ''' <param name="TestCasePriority">The priority of the Test Case.</param>
    ''' <remarks>Defaults to normal priority.</remarks>
    Public Sub New(ByVal TestCaseName As String, ByVal TestCasePriority As UIControls.AutomationSettings.TestCasePriority)
        Priority = TestCasePriority
        Name = TestCaseName
    End Sub

    ''' <summary>
    ''' Creates a TestCase rather than just running the default constructor.  This allows Slick Test to run
    ''' each test case individually.
    ''' </summary>
    ''' <param name="TestCaseName">The name or a simple description of the test case inside the project</param>
    ''' <remarks>Defaults to normal priority.</remarks>
    Public Sub New(ByVal TestCaseName As String)
        Name = TestCaseName
    End Sub

End Class
#End If