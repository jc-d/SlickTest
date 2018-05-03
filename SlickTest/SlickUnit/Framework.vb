Imports System.Reflection
Imports System.Security
Imports System.Security.Policy
Imports System.IO
Imports System.Runtime.Remoting.Lifetime

<Serializable()> _
Public Class Framework
    Inherits MarshalByRefObject
    Implements IDisposable

#Region "Properties"
    Public AutomaticallyAttachConsoleHandler As Boolean = True

    Private InternalAppDomain As System.AppDomain
    Public Property CurrentAppDomain() As System.AppDomain
        Get
            If (UseInternalDomain) Then Return InternalAppDomain
            Return System.AppDomain.CurrentDomain
        End Get
        Private Set(ByVal value As System.AppDomain)
            InternalAppDomain = value
        End Set
    End Property

    Private InternalOnlyIncludeFilter As System.Collections.Generic.List(Of String)
    Public Property OnlyIncludeFilter() As System.Collections.Generic.List(Of String)
        Get
            Return InternalOnlyIncludeFilter
        End Get
        Set(ByVal value As System.Collections.Generic.List(Of String))
            InternalOnlyIncludeFilter = value
        End Set
    End Property

    Private InternalExcludeFilter As System.Collections.Generic.List(Of String)
    Public Property ExcludeFilter() As System.Collections.Generic.List(Of String)
        Get
            Return InternalExcludeFilter
        End Get
        Set(ByVal value As System.Collections.Generic.List(Of String))
            InternalExcludeFilter = value
        End Set
    End Property

    Private InternalDllLocations As System.Collections.Generic.List(Of String)
    Public Property SearchedDllLocations() As System.Collections.Generic.List(Of String)
        Get
            Return InternalDllLocations
        End Get
        Private Set(ByVal value As System.Collections.Generic.List(Of String))
            InternalDllLocations = value
        End Set
    End Property

    Private InternalNotSearchedDllAsm As System.Collections.Generic.List(Of System.Reflection.Assembly)
    Private Property NotSearchedDllAsms() As System.Collections.Generic.List(Of System.Reflection.Assembly)
        Get
            Return InternalNotSearchedDllAsm
        End Get
        Set(ByVal value As System.Collections.Generic.List(Of System.Reflection.Assembly))
            InternalNotSearchedDllAsm = value
        End Set
    End Property

    Private ReadOnly Property AllDllAsms() As System.Collections.Generic.List(Of System.Reflection.Assembly)
        Get
            Dim Asms As New System.Collections.Generic.List(Of System.Reflection.Assembly)
            If (Not InternalNotSearchedDllAsm Is Nothing) Then
                For Each asm As Assembly In InternalNotSearchedDllAsm
                    Asms.Add(asm)
                Next
            End If

            If (Not InternalDllAsm Is Nothing) Then
                For Each asm As Assembly In InternalDllAsm
                    Asms.Add(asm)
                Next
            End If
            Return Asms
        End Get
    End Property

    Private InternalNotSearchedDllLocations As System.Collections.Generic.List(Of String)
    Public Property NotSearchedDllLocations() As System.Collections.Generic.List(Of String)
        Get
            Return InternalNotSearchedDllLocations
        End Get
        Private Set(ByVal value As System.Collections.Generic.List(Of String))
            InternalNotSearchedDllLocations = value
        End Set
    End Property

    Public ReadOnly Property AllDllLocations() As System.Collections.Generic.List(Of String)
        Get
            Dim AllDlls As New System.Collections.Generic.List(Of String)
            If (Not InternalDllLocations Is Nothing) Then
                For Each Dll As String In InternalDllLocations
                    AllDlls.Add(Dll)
                Next
            End If

            If (Not InternalNotSearchedDllLocations Is Nothing) Then
                For Each Dll As String In InternalNotSearchedDllLocations
                    AllDlls.Add(Dll)
                Next
            End If
            Return AllDlls
        End Get
    End Property

    Private InternalAttributes As System.Collections.Generic.List(Of String)
    Private Property RequiredAttributes() As System.Collections.Generic.List(Of String)
        Get
            Return InternalAttributes
        End Get
        Set(ByVal value As System.Collections.Generic.List(Of String))
            InternalAttributes = value
        End Set
    End Property

    Private InternalDllAsm As System.Collections.Generic.List(Of System.Reflection.Assembly)
    Private Property SearchedDllAsms() As System.Collections.Generic.List(Of System.Reflection.Assembly)
        Get
            Return InternalDllAsm
        End Get
        Set(ByVal value As System.Collections.Generic.List(Of System.Reflection.Assembly))
            InternalDllAsm = value
        End Set
    End Property

    'Private CurrentAppDomain As AppDomain = Nothing
    Private AppDomainName As String = "" ' "SlickUnit - " ' & Guid.NewGuid().ToString()
    Private InternalWorkingDirectory As String = System.IO.Path.GetTempPath()

    Public Property WorkingDirectory() As String
        Get
            Return InternalWorkingDirectory
        End Get
        Set(ByVal value As String)
            If (SearchedDllAsms.Count <> 0) Then
                Throw New Exception("User can't override the working dirctory after loading dlls.")
            End If
            InternalWorkingDirectory = value
        End Set
    End Property

    Private InternalCancelTest As Boolean
    Public Property CancelTest() As Boolean
        Get
            Return InternalCancelTest
        End Get
        Set(ByVal value As Boolean)
            InternalCancelTest = value
        End Set
    End Property

    Private _internalInternalDomain As Boolean = True
    Public Property UseInternalDomain() As Boolean
        Get
            Return _internalInternalDomain
        End Get
        Set(ByVal value As Boolean)
            _internalInternalDomain = value
        End Set
    End Property
#End Region

    Public Sub AddDll(ByVal FilePath As String, Optional ByVal IsSearchedForExecutableTests As Boolean = True)
        If (System.IO.File.Exists(FilePath) = False) Then
            Throw New System.DllNotFoundException("Could not find: " & FilePath)
        End If
        Dim path As String = System.IO.Path.GetExtension(FilePath).ToUpperInvariant()
        If (path <> ".DLL" AndAlso path <> ".EXE") Then
            Throw New System.DllNotFoundException("Invalid file extension for file: " & FilePath)
        End If
        If (AllDllLocations.Contains(FilePath.ToUpper()) = False) Then
            If (IsSearchedForExecutableTests) Then
                SearchedDllLocations.Add(FilePath.ToUpper())
            Else
                NotSearchedDllLocations.Add(FilePath.ToUpper())
            End If
        End If
    End Sub

    Public Sub RemoveDll(ByVal FilePath As String)
        Dim TestPath As String = System.IO.Path.GetFullPath(FilePath.ToUpper())
        Dim IsInSearched As Boolean = False
        If (AllDllLocations.Contains(TestPath) = True) Then
            If (SearchedDllLocations.Contains(TestPath)) Then
                IsInSearched = True
                SearchedDllLocations.Remove(TestPath)
            Else
                NotSearchedDllLocations.Remove(TestPath)
            End If
        End If
        If (IsInSearched) Then
            For i As Integer = 0 To SearchedDllAsms.Count
                If (System.IO.Path.GetFullPath(SearchedDllAsms(i).FullName.ToUpper()) = TestPath) Then
                    SearchedDllAsms.Remove(SearchedDllAsms(i))
                    i -= 1
                End If
            Next
        Else
            For i As Integer = 0 To NotSearchedDllAsms.Count
                If (System.IO.Path.GetFullPath(NotSearchedDllAsms(i).FullName.ToUpper()) = TestPath) Then
                    NotSearchedDllAsms.Remove(NotSearchedDllAsms(i))
                    i -= 1
                End If
            Next
        End If
    End Sub

    Private Shared Function GetUniquePaths(ByVal listOfPaths As System.Collections.Generic.List(Of String)) As System.Collections.Generic.List(Of String)
        Dim newList As New System.Collections.Generic.List(Of String)()
        For Each item As String In listOfPaths
            If (newList.Contains(item) = False) Then newList.Add(item)
        Next
        Return newList
    End Function

    Private Shared Function GetUniqueDirs(ByVal listOfPaths As System.Collections.Generic.List(Of String)) As System.Collections.Generic.List(Of String)
        Dim newList As New System.Collections.Generic.List(Of String)()
        For Each item As String In listOfPaths
            If (newList.Contains(System.IO.Path.GetDirectoryName(item)) = False) Then newList.Add(System.IO.Path.GetDirectoryName(item))
        Next
        Return newList
    End Function

    Private Shared Function FlattenPaths(ByVal listOfPaths As System.Collections.Generic.List(Of String)) As String

        Dim sb As New System.Text.StringBuilder
        For Each Dir As String In listOfPaths
            sb.Append(Dir & "|")
        Next
        Return sb.ToString().TrimEnd("|"c)
    End Function

    Public Shared Function GetCacheDirectory() As String
        Return ".\TmpCache\" 'probably should come up with a better location
    End Function


    Public Sub LoadDlls()
        'Console.WriteLine("Loading DLLs...")
        Dim FullNameList As New System.Collections.Generic.List(Of String)
        Dim StoreDirectory As String = ".\"
        Dim StoreTo As String = GetCacheDirectory()
        If (UseInternalDomain) Then

            If (SearchedDllAsms.Count = 0) Then
            Else
                If (Not CurrentAppDomain Is Nothing) Then
                    System.AppDomain.Unload(CurrentAppDomain)
                End If
            End If
            If (SearchedDllLocations.Count <> 0) Then 'Only create if you have no Asm and do have some DLL locations.
                StoreDirectory = System.IO.Directory.GetCurrentDirectory() '& "\Execute"
                'Console.WriteLine("Creating a new domain.")
                Dim setup As AppDomainSetup = New AppDomainSetup()
                setup.ApplicationName = AppDomainName '& FlattenPaths(GetUniquePaths(Me.DllLocations))
                setup.ApplicationBase = StoreTo
                setup.ShadowCopyFiles = "true"
                setup.ShadowCopyDirectories = WorkingDirectory
                setup.CachePath = WorkingDirectory & System.IO.Path.GetRandomFileName() & "\"
                setup.DisallowBindingRedirects = False
                setup.DisallowCodeDownload = False
                setup.ConfigurationFile = String.Empty
                setup.DisallowApplicationBaseProbing = False

                If (System.IO.Directory.Exists(setup.CachePath)) Then
                    System.IO.Directory.Delete(setup.CachePath)
                End If
                If (System.IO.Directory.Exists(StoreTo) = False) Then
                    System.IO.Directory.CreateDirectory(StoreTo)
                End If

                For Each File As String In Me.SearchedDllLocations
                    Try
                        System.IO.File.Copy(File, StoreTo & System.IO.Path.GetFileName(File), True)
                    Catch ex As Exception

                    End Try
                Next
                Dim evidence As New Security.Policy.Evidence(AppDomain.CurrentDomain.Evidence)
                If (evidence.Count = 0) Then
                    Dim zone As New Zone(SecurityZone.MyComputer)
                    evidence.AddHost(zone)
                    Dim Assembly As Assembly = Assembly.GetExecutingAssembly()
                    Dim url As New Url(Assembly.CodeBase)
                    evidence.AddHost(url)
                    Dim hash As New Hash(Assembly)
                    evidence.AddHost(hash)
                End If

                CurrentAppDomain = System.AppDomain.CreateDomain(setup.ApplicationName, evidence, setup)

                If (Not System.IO.Directory.Exists(StoreDirectory)) Then
                    System.IO.Directory.CreateDirectory(StoreDirectory)
                End If

            End If
        End If

        For Each file As String In SearchedDllLocations
            If (System.IO.File.Exists(file)) Then
                Dim ContainsAsm As Boolean = False
                For Each Asm As System.Reflection.Assembly In SearchedDllAsms
                    If (System.IO.Path.GetFullPath(file).ToUpper() = System.IO.Path.GetFullPath(Asm.Location).ToUpper()) Then
                        ContainsAsm = True
                        Exit For
                    End If
                Next
                If (ContainsAsm = False) Then
                    SearchedDllAsms.Add(System.Reflection.Assembly.LoadFrom(file))
                    FullNameList.Add(SearchedDllAsms(SearchedDllAsms.Count - 1).FullName)
                End If
            End If
        Next
        'For Each File As String In Me.AllDllLocations
        '    Dim debugInfo As Byte() = LoadFile(System.IO.Path.GetFileNameWithoutExtension(File) & ".PDB")
        '    If (debugInfo Is Nothing) Then
        '        CurrentAppDomain.Load(LoadFile(File))

        '    Else
        '        CurrentAppDomain.Load(LoadFile(File), debugInfo)
        '    End If
        'Next

        If (UseInternalDomain) Then
            For Each name As String In FullNameList
                Try
                    CurrentAppDomain.Load(name)
                Catch e As System.Exception
                    System.Console.WriteLine("Failed to load name: " & name)
                End Try
            Next
        End If
    End Sub


    Private Shared Function LoadFile(ByVal filename As String) As Byte()
        If (System.IO.File.Exists(filename) = False) Then Return Nothing
        Dim fs As New FileStream(filename, FileMode.Open, FileAccess.Read, FileShare.ReadWrite)
        Dim buffer(CInt(fs.Length)) As Byte
        fs.Read(buffer, 0, buffer.Length)
        fs.Close()

        Return buffer
    End Function 'loadFile

    Public Function GetValidMethodStrings() As String
        Return GetValidMethodStrings(Nothing, Nothing)
    End Function

    Public Function GetValidMethodStrings(ByVal MethodTypeFilter As System.Type, ByVal ClassTypeFilter As System.Type) As String
        Dim list As New System.Text.StringBuilder()

        For Each m As MethodAndTypeInfo In GetValidMethodAndTypeInfo(MethodTypeFilter, ClassTypeFilter)
            Dim t As Type = m.Type
            Dim FullName As String = t.Namespace & "." & t.Name & "." & m.Method.Name
            list.Append(FullName)
            For Each pi As System.Reflection.ParameterInfo In m.Method.GetParameters()
                list.Append(" " & pi.Name & ",")
            Next

            list.AppendLine()
        Next
        Return list.ToString()
    End Function

    Public Function GetValidMethods() As System.Collections.Generic.List(Of System.Reflection.MethodInfo)
        Dim list As New System.Collections.Generic.List(Of System.Reflection.MethodInfo)
        For Each m As MethodAndTypeInfo In GetValidMethodAndTypeInfo()
            list.Add(m.Method)
        Next
        Return list
    End Function

    Public Function GetValidMethodAndTypeInfo() As System.Collections.Generic.List(Of MethodAndTypeInfo)
        Return GetValidMethodAndTypeInfo(Nothing, Nothing)
    End Function

    Public Function GetValidMethodAndTypeInfo(ByVal MethodAttributeTypeFilter As System.Type, ByVal ClassAttributeTypeFilter As System.Type) As System.Collections.Generic.List(Of MethodAndTypeInfo)
        Return GetValidMethodAndTypeInfo(MethodAttributeTypeFilter, ClassAttributeTypeFilter, Nothing, Nothing)
    End Function

    Public Function GetValidMethodAndTypeInfo(ByVal MethodAttributeTypeFilter As System.Type, ByVal ClassAttributeTypeFilter As System.Type, ByVal ClassTypeFilter As System.Type, ByVal ExactMethodName As String) As System.Collections.Generic.List(Of MethodAndTypeInfo)
        Dim Info As New RunnerInfo()
        Info.TestAttributeFilter = MethodAttributeTypeFilter
        Info.ClassTypeAttributeFilter = ClassAttributeTypeFilter
        Info.ClassTypeFilter = ClassTypeFilter
        Info.ExactMethodName = ExactMethodName
        Info.LikeMethodName = Nothing
        Info.NotLikeMethodName = Nothing
        Return GetValidMethodAndTypeInfo(Info)
    End Function

    Public Function GetValidMethodAndTypeInfo(ByVal Info As IRunnerInfo) As System.Collections.Generic.List(Of MethodAndTypeInfo)
        Dim list As New System.Collections.Generic.List(Of MethodAndTypeInfo)
        Dim domain As System.AppDomain = CurrentAppDomain
        Dim AlreadyCreatedPath As New System.Collections.Generic.List(Of String)

        If (domain Is Nothing OrElse domain.SetupInformation.ApplicationName <> "Framework") Then
            If (System.AppDomain.CurrentDomain.SetupInformation.ApplicationName = "Framework") Then
                domain = System.AppDomain.CurrentDomain
            End If
        End If
        For Each Asm As System.Reflection.Assembly In domain.GetAssemblies()
            If (Me.SearchedDllAsms.Contains(Asm) = False) Then Continue For
            For Each t As System.Type In Asm.GetExportedTypes()
                If (Not Info.ClassTypeAttributeFilter Is Nothing) Then
                    Dim SkipClass As Boolean = True
                    For Each at As Object In t.GetCustomAttributes(True)
                        If (Info.ClassTypeAttributeFilter.FullName = at.GetType().FullName) Then
                            If (Info.FilteredNamespace = Nothing OrElse t.Namespace Like Info.FilteredNamespace) Then
                                SkipClass = False
                                Exit For
                            End If
                        End If
                    Next
                    If (SkipClass) Then Continue For
                End If
                Dim mi As MethodInfo() = t.GetMethods()

                For Each m As MethodInfo In mi
                    If (m.IsPublic AndAlso m.Module.Assembly.Equals(Asm)) Then
                        If (Info.ClassTypeFilter Is Nothing OrElse Info.ClassTypeFilter.FullName = m.DeclaringType.FullName) Then
                            Dim FullName As String = t.Namespace & "." & t.Name & "." & m.Name
                            Dim Name As String = FullName.ToUpper()
                            If (ShouldIncludeMethod(FullName)) Then
                                Dim AddMethod As Boolean = False
                                If (Not Info.TestAttributeFilter Is Nothing) Then
                                    For Each at As Object In m.GetCustomAttributes(True)
                                        If (Info.TestAttributeFilter.FullName = at.GetType().FullName) Then
                                            AddMethod = True
                                            Exit For
                                        End If
                                    Next
                                Else
                                    AddMethod = True
                                End If
                                If (AlreadyCreatedPath.Contains(FullName) = False) Then
                                    AlreadyCreatedPath.Add(FullName)

                                    If (AddMethod) Then
                                        list.Add(New MethodAndTypeInfo(m, t))
                                    End If
                                End If
                            End If
                        End If
                    End If
                Next
            Next
        Next

        If (Not System.String.IsNullOrEmpty(Info.ExactMethodName)) Then
            For Each m As MethodAndTypeInfo In list

                If (String.Compare(m.Method.Name, Info.ExactMethodName, Not Info.ExactCase) = 0) Then
                    Dim l1 As New System.Collections.Generic.List(Of MethodAndTypeInfo)
                    l1.Add(m)
                    Return l1
                End If
            Next
        Else
            If (Info.NotLikeMethodName Is Nothing AndAlso Info.LikeMethodName Is Nothing) Then
                Return list
            End If
            If (Info.NotLikeMethodName.Count = 0 AndAlso Info.LikeMethodName.Count = 0) Then
                Return list
            End If
            Dim LikeMethodName As System.Collections.Generic.List(Of String)
            Dim NotLikeMethodName As System.Collections.Generic.List(Of String)

            If (Info.LikeMethodName Is Nothing) Then
                LikeMethodName = New System.Collections.Generic.List(Of String)
                LikeMethodName.Add("*")
            Else
                LikeMethodName = Info.LikeMethodName
            End If

            If (Info.NotLikeMethodName Is Nothing) Then
                NotLikeMethodName = New System.Collections.Generic.List(Of String)
                NotLikeMethodName.Add("~`""'!@#$%^&*()_") 'Invalid in any .net language, not ideal design
            Else
                NotLikeMethodName = Info.NotLikeMethodName
            End If

            Dim l1 As New System.Collections.Generic.List(Of MethodAndTypeInfo)

            For Each m As MethodAndTypeInfo In list
                Dim MethodName As String = m.Method.Name
                If (Info.ExactCase = False) Then MethodName = MethodName.ToUpperInvariant()
                For Each MethodNameLike As String In LikeMethodName
                    If (Info.ExactCase = False) Then MethodNameLike = MethodNameLike.ToUpperInvariant()
                    If (MethodName Like MethodNameLike) Then
                        For Each MethodNameNotLike As String In NotLikeMethodName
                            If (Info.ExactCase = False) Then MethodNameNotLike = MethodNameNotLike.ToUpperInvariant()
                            If (Not (MethodName Like MethodNameNotLike)) Then
                                l1.Add(m)
                            End If
                        Next
                    End If
                Next
            Next
            Return l1
        End If
        Return list
    End Function

    ''' <summary>
    ''' No Asms will be found if you have not loaded first.
    ''' </summary>
    ''' <param name="FilePath"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetAsm(ByVal FilePath As String) As System.Reflection.Assembly
        Dim FullPath As String = FilePath.ToUpper()
        For Each Asm As System.Reflection.Assembly In CurrentAppDomain.GetAssemblies()
            If (System.IO.Path.GetFullPath(FullPath).ToUpper() = System.IO.Path.GetFullPath(Asm.Location).ToUpper()) Then
                Return Asm
            End If
        Next
        Return Nothing
    End Function

    Public Function GetAsms() As System.Collections.Generic.List(Of System.Reflection.Assembly)
        Return SearchedDllAsms
    End Function

    Public Function ShouldIncludeMethod(ByVal FullNameToTest As String) As Boolean
        Dim FullName As String = FullNameToTest.ToUpper()
        For Each item In ExcludeFilter
            If (FullName.Contains(item.ToUpper())) Then
                If (String.Empty <> item) Then
                    Return False
                End If
            End If
        Next
        For Each item In OnlyIncludeFilter
            If (FullName.Contains(item.ToUpper())) Then
                Return True
            End If
        Next
        If (OnlyIncludeFilter.Count = 0) Then Return True
        Return False
    End Function

    Public Sub New()
        Init()
    End Sub

    Public Sub Init()
        InternalOnlyIncludeFilter = New System.Collections.Generic.List(Of String)
        InternalDllLocations = New System.Collections.Generic.List(Of String)
        InternalDllAsm = New System.Collections.Generic.List(Of System.Reflection.Assembly)
        InternalExcludeFilter = New System.Collections.Generic.List(Of String)
        InternalAttributes = New System.Collections.Generic.List(Of String)
    End Sub

    Public Sub InitDefaultExcludeItems()
        If (InternalExcludeFilter.Count = 0) Then
            InternalExcludeFilter.Add("ToString")
            InternalExcludeFilter.Add("GetHashCode")
            InternalExcludeFilter.Add("GetType")
            InternalExcludeFilter.Add("Equals")
        End If
    End Sub

    Public Function GetMethodsByType(ByVal ClassType As Type, ByVal TestAttributeType As System.Type) As System.Collections.Generic.List(Of MethodAndTypeInfo)
        Return GetValidMethodAndTypeInfo(TestAttributeType, Nothing, ClassType, Nothing)
    End Function

    ' IDisposable
    Private disposedValue As Boolean = False

    Protected Overridable Sub Dispose(ByVal disposing As Boolean)
        If Not Me.disposedValue Then
            If disposing Then
                SearchedDllAsms.Clear()
                Me.SearchedDllLocations.Clear()
                'RemoveHandler CurrentAppDomain.AssemblyResolve, AddressOf ResolveAsm_AppDomain

                If (Not CurrentAppDomain Is Nothing) Then
                    If (UseInternalDomain) Then System.AppDomain.Unload(CurrentAppDomain)
                End If
                'causes problems
                'If (System.IO.Directory.Exists(Framework.GetCacheDirectory())) Then
                '    For Each File As String In System.IO.Directory.GetFiles(Framework.GetCacheDirectory())
                '        Try
                '            System.IO.File.Delete(File)
                '        Catch ex As Exception

                '        End Try
                '    Next
                'End If
            End If
        End If
        Me.disposedValue = True
    End Sub

#Region " IDisposable Support "
    ' This code added by Visual Basic to correctly implement the disposable pattern.
    Public Sub Dispose() Implements IDisposable.Dispose
        ' Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub
#End Region

    Public Function GetAsm(ByVal typeToFind As Type) As System.Reflection.Assembly
        For Each asm As System.Reflection.Assembly In GetAsms()
            For Each t As System.Type In asm.GetTypes()
                If (t.FullName = typeToFind.FullName) Then
                    Return asm
                End If
            Next
        Next
        Return Nothing
    End Function

    Public Function GetTypeByString(ByVal typeAsString As String) As Type
        Dim type As System.Type = System.Type.GetType(typeAsString, False, True)
        If (type Is Nothing) Then
            For Each Asm As System.Reflection.Assembly In CurrentAppDomain.GetAssemblies()
                For Each t As System.Type In Asm.GetExportedTypes()
                    If (t.Namespace = typeAsString) Then
                        Return t
                    End If
                    If (t.Namespace & "." & t.Name = typeAsString) Then
                        Return t
                    End If
                    If (t.Namespace & "." & t.Name = typeAsString) Then
                        Return t
                    End If
                Next
            Next

        End If
        Return Nothing
    End Function

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