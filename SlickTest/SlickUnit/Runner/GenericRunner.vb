Imports System.Runtime.Remoting.Lifetime

Public MustInherit Class GenericRunner
    Inherits MarshalByRefObject
    Implements SlickUnit.IRunner

    Private InternalSetupTestType As System.Type
    Public Property SetupTestType() As System.Type Implements IRunner.SetupTestType
        Get
            Return InternalSetupTestType
        End Get
        Protected Set(ByVal value As System.Type)
            InternalSetupTestType = value
        End Set
    End Property

    Private InternalCleanupTestType As System.Type
    Public Property CleanupTestType() As System.Type Implements IRunner.CleanupTestType
        Get
            Return InternalCleanupTestType
        End Get
        Protected Set(ByVal value As System.Type)
            InternalCleanupTestType = value
        End Set
    End Property

    Private InternalClassToRunType As System.Type
    Public Property ClassAttributeToRunType() As System.Type Implements IRunner.ClassAttributeToRunType
        Get
            Return InternalClassToRunType
        End Get
        Protected Set(ByVal value As System.Type)
            InternalClassToRunType = value
        End Set
    End Property

    Private InternalSetupClassType As System.Type
    Public Property SetupClassType() As System.Type Implements IRunner.SetupClassType
        Get
            Return InternalSetupClassType
        End Get
        Protected Set(ByVal value As System.Type)
            InternalSetupClassType = value
        End Set
    End Property

    Private InternalTestType As System.Type
    Public Property TestType() As System.Type Implements IRunner.TestAttributeType
        Get
            Return InternalTestType
        End Get
        Protected Set(ByVal value As System.Type)
            InternalTestType = value
        End Set
    End Property

    Private InternalCleanupClassType As System.Type
    Public Property CleanupClassType() As System.Type Implements IRunner.CleanupClassType
        Get
            Return InternalCleanupClassType
        End Get
        Protected Set(ByVal value As System.Type)
            InternalCleanupClassType = value
        End Set
    End Property

    Private InternalUnitTestExceptionType As System.Type
    Public Property UnitTestExceptionType() As System.Type Implements IRunner.UnitTestExceptionType
        Get
            Return InternalUnitTestExceptionType
        End Get
        Protected Set(ByVal value As System.Type)
            InternalUnitTestExceptionType = value
        End Set
    End Property

    Private InternalIgnoreTestType As System.Type
    Public Property IgnoreTestType() As System.Type Implements IRunner.IgnoreTestType
        Get
            Return InternalIgnoreTestType
        End Get
        Protected Set(ByVal value As System.Type)
            InternalIgnoreTestType = value
        End Set
    End Property

    Private InternalExpectedExceptionType As System.Type 'TODO Implement.
    Public Property ExpectedExceptionType() As System.Type Implements IRunner.ExpectedExceptionType
        Get
            Return InternalExpectedExceptionType
        End Get
        Protected Set(ByVal value As System.Type)
            InternalExpectedExceptionType = value
        End Set
    End Property

    Protected CurrentResult As GenericTestResults

    Private InternalRequiresAllSetupCleanup As Boolean = False
    Public Property RequiresAllSetupCleanup() As Boolean
        Get
            Return InternalRequiresAllSetupCleanup
        End Get
        Set(ByVal value As Boolean)
            InternalRequiresAllSetupCleanup = value
        End Set
    End Property
    Public MustOverride Sub InitTypes(ByVal Framework As Framework) Implements IRunner.InitTypes
    Public MustOverride Function CreateTestResult() As GenericTestResults

    Private Results As System.Collections.Generic.List(Of GenericTestResults)
    Public Property TestResults() As System.Collections.Generic.List(Of GenericTestResults) Implements IRunner.TestResults
        Get
            Return Results
        End Get
        Set(ByVal value As System.Collections.Generic.List(Of GenericTestResults))
            Results = value
        End Set
    End Property

    Protected Function CreateClassInstance(ByVal Framework As SlickUnit.Framework, ByVal TestClassType As System.Type) As Object
        If (Framework.UseInternalDomain) Then
            Return AppDomain.CurrentDomain.CreateInstance(Framework.GetAsm(TestClassType).FullName, TestClassType.FullName, False, Reflection.BindingFlags.CreateInstance, Nothing, Nothing, Nothing, Nothing, System.AppDomain.CurrentDomain.Evidence) 'Framework.CurrentAppDomain().Evidence)
        Else
            Return Framework.GetAsm(TestClassType).CreateInstance(TestClassType.FullName, False, Reflection.BindingFlags.CreateInstance, Nothing, Nothing, Nothing, Nothing)
        End If
    End Function

    Public Sub CleanupClass(ByVal Info As IRunnerInfo) Implements IRunner.CleanupClass
        Try
            'InvokeMethod(Framework, TestClassTypeFilter, ClassToRunType, CleanupClassType, Nothing, ClassInstance, False)
            Info.TestAttributeFilter = CleanupClassType
            CurrentResult.StartClassCleanupTime = DateTime.Now
            InvokeMethod(Info, RequiresAllSetupCleanup)
        Catch ex As Exception
            CurrentResult.TestResult = SlickUnit.TestResult.FailInClassCleanup
            CurrentResult.ExceptionResults.Add(ex)
        End Try
        CurrentResult.EndClassCleanupTime = DateTime.Now
    End Sub

    Public Sub CleanupTest(ByVal Info As IRunnerInfo) Implements IRunner.CleanupTest
        Try
            'InvokeMethod(Framework, TestClassTypeFilter, ClassToRunType, CleanupTestType, Nothing, Test.ClassInstance, False)
            Info.TestAttributeFilter = CleanupTestType
            CurrentResult.StartTestCleanupTime = DateTime.Now
            InvokeMethod(Info, RequiresAllSetupCleanup)
        Catch ex As Exception
            CurrentResult.TestName = Info.Test.Method.Name
            CurrentResult.ClassName = Info.Test.Method.ReflectedType.Name
            CurrentResult.Namespace = Info.Test.Method.ReflectedType.Namespace

            CurrentResult.TestResult = SlickUnit.TestResult.FailInTestCleanup
            CurrentResult.ExceptionResults.Add(ex)
        End Try
        CurrentResult.EndTestCleanupTime = DateTime.Now
    End Sub

    Public Sub RunTestsInClass(ByVal Info As IRunnerInfo) Implements IRunner.RunTestsInClass
        'ByVal Framework As SlickUnit.Framework, ByVal TestClassTypeFilter As System.Type
        Info.Framework.LoadDlls()
        Info.TestAttributeFilter = TestType
        Dim methods As System.Collections.Generic.List(Of MethodAndTypeInfo) = _
        Info.Framework.GetValidMethodAndTypeInfo(Info)

        Dim o As Object = Nothing
        CurrentResult = CreateTestResult()
        If (Info.AutomaticallyAttachConsoleHandler) Then ConsoleHandler.AttachConsoleProcess()
        If (MethodAndTypeInfo.AllMethodsContainsAttribute(methods.ToArray(), Me.IgnoreTestType)) Then Return
        If (Info.ClassTypeFilter Is Nothing) Then
            If (methods.Count <= 0) Then Throw New Exception("ClassName Invalid.  Verify class exists.")
            Info.ClassTypeFilter = Info.Framework.GetTypeByString(methods(0).Method.ReflectedType.Namespace & "." & methods(0).Method.ReflectedType.Name)
        End If
        o = SetupClass(Info)
        If (CurrentResult.ExceptionResults.Count <> 0) Then
            Dim method As System.Reflection.MethodInfo = Nothing
            If (Not Info.Test Is Nothing) Then
                method = Info.Test.Method
            Else
                If (methods.Count > 0) Then
                    method = methods(0).Method
                End If
            End If
            If (Not method Is Nothing) Then
                CurrentResult.Namespace = method.ReflectedType.Namespace
                CurrentResult.ClassName = method.ReflectedType.Name
            End If

            TestResults.Add(CurrentResult)
            Return
        End If
        Info.ClassInstance = o
        For Each method As MethodAndTypeInfo In methods

            Info.Test = method
            Info.Test.ClassInstance = o
            If (MethodAndTypeInfo.MethodContainsAttribute(method.Method, IgnoreTestType)) Then
                If (Info.AutomaticallyAttachConsoleHandler) Then
                    CurrentResult.ConsoleOutput = ConsoleHandler.DetachConsoleProcess()
                Else
                    CurrentResult.ConsoleOutput = ""
                End If
                CurrentResult.TestResult = TestResult.Ignore
                CurrentResult.TestName = Info.Test.Method.Name
                CurrentResult.ClassName = Info.Test.Method.ReflectedType.Name
                CurrentResult.Namespace = Info.Test.Method.ReflectedType.Namespace


                TestResults.Add(CurrentResult)
                If (Info.AutomaticallyAttachConsoleHandler) Then ConsoleHandler.AttachConsoleProcess()
                CurrentResult = CreateTestResult()
                Continue For
            End If
            ExecuteTest(Info)
            CurrentResult.ConsoleOutput = ConsoleHandler.DetachConsoleProcess()
            TestResults.Add(CurrentResult)
            If (Info.AutomaticallyAttachConsoleHandler) Then ConsoleHandler.AttachConsoleProcess()
            CurrentResult = CreateTestResult()
        Next
        CurrentResult = TestResults(TestResults.Count - 1) 'TODO figure out way to handle this better.
        Try
            CleanupClass(Info)
        Catch ex As Exception
            'CurrentResult.TestOrClassName = methods(l - 1).Type.Namespace + "." + methods(l - 1).Type.Name
            CurrentResult.TestResult = SlickUnit.TestResult.FailInClassCleanup
            CurrentResult.ExceptionResults.Add(ex)
        End Try
        If (Info.AutomaticallyAttachConsoleHandler) Then CurrentResult.ConsoleOutput += ConsoleHandler.DetachConsoleProcess()
    End Sub

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <returns>returns true if test is ignored</returns>
    ''' <remarks></remarks>
    Private Function HandledIgnored(ByVal Info As IRunnerInfo) As Boolean
        If (Not Info.Test Is Nothing) Then
            If (Info.Test.MethodContainsAttribute(IgnoreTestType)) Then
                If (Info.AutomaticallyAttachConsoleHandler) Then CurrentResult.ConsoleOutput = ConsoleHandler.DetachConsoleProcess()
                CurrentResult.TestName = Info.Test.Method.Name
                CurrentResult.ClassName = Info.Test.Method.ReflectedType.Name
                CurrentResult.Namespace = Info.Test.Method.ReflectedType.Namespace

                CurrentResult.TestResult = TestResult.Ignore
                TestResults.Add(CurrentResult)
                Return True
            End If
        End If
        Return False
    End Function

    Public Sub RunTestInClass(ByVal Info As IRunnerInfo) Implements IRunner.RunTestInClass
        'ByVal Framework As SlickUnit.Framework, ByVal TestClassTypeFilter As System.Type, ByVal Test As MethodAndTypeInfo
        Info.Framework.LoadDlls()
        CurrentResult = CreateTestResult()
        If (Info.AutomaticallyAttachConsoleHandler) Then ConsoleHandler.AttachConsoleProcess()
        If (HandledIgnored(Info) = True) Then Return 'test is ignored
        Info.ClassInstance = SetupClass(Info)
        If (CurrentResult.ExceptionResults.Count <> 0) Then
            TestResults.Add(CurrentResult)
            Return
        End If

        If (Not Info.Test Is Nothing) Then Info.Test.ClassInstance = Info.ClassInstance
        ExecuteTest(Info)
        'TestResults.Add(CurrentResult)
        'Only Cleanup is a seperate
        'CurrentResult = CreateTestResult()
        Try
            CleanupClass(Info)
        Catch ex As Exception
            'CurrentResult.TestOrClassName = Info.Test.Type.Namespace + "." + Info.Test.Type.Name
            CurrentResult.TestResult = SlickUnit.TestResult.FailInClassCleanup
            CurrentResult.ExceptionResults.Add(ex)
        End Try
        If (Info.AutomaticallyAttachConsoleHandler) Then
            CurrentResult.ConsoleOutput = ConsoleHandler.DetachConsoleProcess()
        Else
            CurrentResult.ConsoleOutput = ""
        End If
        TestResults.Add(CurrentResult)
    End Sub

    Public Sub RunTestsInNamespace(ByVal Info As IRunnerInfo) Implements IRunner.RunTestsInNamespace
        Dim newInfo As IRunnerInfo = Info.CreateCopy(Info)
        For Each method As SlickUnit.MethodAndTypeInfo In Info.Framework.GetValidMethodAndTypeInfo(Info)
            newInfo.ExactCase = True
            newInfo.ExactMethodName = method.Method.Name
            newInfo.Test = method
            newInfo.ClassTypeFilter = method.Type
            If (HandledIgnored(newInfo) = True) Then Return
            Me.RunTestInClass(newInfo)
        Next
    End Sub

    Public Sub ExecuteTest(ByVal Info As IRunnerInfo) Implements IRunner.ExecuteTest
        Console.WriteLine("Setup Test: " & Info.Test.Method.Name)
        SetupTest(Info)
        Info.TestAttributeFilter = Nothing
        Try
            Dim fastInvoker As FastInvokeHandler = FastMethodInvoker.GetMethodInvoker(Info.Test.Method)
            CurrentResult.StartTestTime = DateTime.Now
            fastInvoker(Info.ClassInstance, Nothing)
        Catch ex As Exception
            If (UnitTestExceptionType Is Nothing OrElse UnitTestExceptionType Is ex.GetType()) Then
                CurrentResult.TestResult = SlickUnit.TestResult.Fail
            Else
                CurrentResult.TestResult = SlickUnit.TestResult.FailByException
            End If
            CurrentResult.ExceptionResults.Add(ex)
        End Try
        CurrentResult.EndTestTime = DateTime.Now

        CurrentResult.TestName = Info.Test.Method.Name
        CurrentResult.ClassName = Info.Test.Method.ReflectedType.Name
        CurrentResult.Namespace = Info.Test.Method.ReflectedType.Namespace

        CurrentResult.TestResult = SlickUnit.TestResult.Pass

        CleanupTest(Info)
    End Sub

    Public Function SetupClass(ByVal Info As IRunnerInfo) As Object Implements IRunner.SetupClass
        Try

            Dim o As Object = CreateClassInstance(Info.Framework, Info.ClassTypeFilter)
            Info.ClassInstance = o
            If (Not Info.Test Is Nothing) Then Info.Test.ClassInstance = o
            'InvokeMethod(Framework, TestClassTypeFilter, ClassToRunType, SetupClassType, Nothing, o, False)
            Info.TestAttributeFilter = SetupClassType
            CurrentResult.StartClassSetupTime = DateTime.Now
            InvokeMethod(Info, RequiresAllSetupCleanup)
            CurrentResult.EndClassSetupTime = DateTime.Now

            Return o
        Catch ex As Exception
            CurrentResult.ExceptionResults.Add(ex)
            CurrentResult.TestResult = TestResult.FailByException
            Return Nothing
        End Try
    End Function

    Public Sub SetupTest(ByVal Info As IRunnerInfo) Implements IRunner.SetupTest
        Try
            Info.TestAttributeFilter = SetupTestType
            'InvokeMethod(Framework, TestClassTypeFilter, ClassToRunType, SetupTestType, Nothing, Test.ClassInstance, False)
            CurrentResult.StartTestSetupTime = DateTime.Now
            InvokeMethod(Info, RequiresAllSetupCleanup)
        Catch ex As Exception
            CurrentResult.TestName = Info.Test.Method.Name
            CurrentResult.ClassName = Info.Test.Method.ReflectedType.Name
            CurrentResult.Namespace = Info.Test.Method.ReflectedType.Namespace

            CurrentResult.TestResult = SlickUnit.TestResult.FailInTestSetup
            CurrentResult.ExceptionResults.Add(ex)
        End Try
        CurrentResult.EndTestSetupTime = DateTime.Now
    End Sub

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="RunnerInfo"></param>
    ''' <param name="IsRequired"></param>
    ''' <remarks>Uses Framework, TestClassTypeFilter, TestAttributeFilter,
    ''' MethodName (of some form), ClassInstance, IsRequired</remarks>
    Protected Sub InvokeMethod(ByVal RunnerInfo As IRunnerInfo, ByVal IsRequired As Boolean)
        Dim m As MethodAndTypeInfo = GetMethod(RunnerInfo, IsRequired)
        If (m Is Nothing) Then Return
        Dim fastInvoker As FastInvokeHandler = FastMethodInvoker.GetMethodInvoker(m.Method)
        'fastInvoker.DynamicInvoke(Nothing)
        fastInvoker.Invoke(RunnerInfo.ClassInstance, Nothing)
        ' fastInvoker(RunnerInfo.ClassInstance, Nothing)
    End Sub

    Protected Function GetMethod(ByVal Framework As SlickUnit.Framework, ByVal TestClassTypeFilter As System.Type, ByVal ClassTypeAttributeFilter As System.Type, ByVal TestAttributeFilter As System.Type, ByVal MethodName As String, ByVal IsRequired As Boolean) As MethodAndTypeInfo
        Dim m As MethodAndTypeInfo() = Framework.GetValidMethodAndTypeInfo(TestAttributeFilter, ClassTypeAttributeFilter, TestClassTypeFilter, MethodName).ToArray()
        If (m.Length <> 1 AndAlso IsRequired = True) Then
            Throw New Exception("To many or too few methods found.")
        Else
            If (IsRequired = False AndAlso m.Length <> 1) Then
                Return Nothing
            End If
        End If
        Return m(0)
    End Function

    Protected Function GetMethod(ByVal Info As IRunnerInfo, ByVal IsRequired As Boolean) As MethodAndTypeInfo
        Dim m As MethodAndTypeInfo() = _
        Info.Framework.GetValidMethodAndTypeInfo(Info.TestAttributeFilter, _
                                                 Info.ClassTypeAttributeFilter, _
                                                 Info.ClassTypeFilter, _
                                                 Info.ExactMethodName).ToArray()
        If (m.Length <> 1 AndAlso IsRequired = True) Then
            Throw New Exception("To many or too few methods found.")
        Else
            If (IsRequired = False AndAlso m.Length <> 1) Then
                Return Nothing
            End If
        End If
        Return m(0)
    End Function

    Public Sub New(ByVal Framework As Framework)
        InitTypes(Framework)
        TestResults = New System.Collections.Generic.List(Of GenericTestResults)()
    End Sub

    Public Sub New()
        TestResults = New System.Collections.Generic.List(Of GenericTestResults)()
    End Sub

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