Imports APIControls
Imports System.Runtime.CompilerServices
'Prevents people from creating new reporters, but allows me to to create a
'report object for slick test or the results viewer.
<Assembly: InternalsVisibleTo("ResultsViewer")> 
<Assembly: InternalsVisibleTo("SlickTestDeveloper")> 

''' <summary>
''' Provides a way of reporting passes or fails into a database.  Currently this only
''' supports a local database, not a full sql server.  In the future a sql server
''' will be supported.
''' </summary>
''' <remarks></remarks>
Public NotInheritable Class Report
    Implements IReport

#Region "Private/Protected"

    Private Const _Info As Byte = 2
    Private Const _Warning As Byte = 3
    Private Const _Fail As Byte = 1
    Private Const _Pass As Byte = 0
    Private Const _Unknown As Byte = 255

    Private Const DBVERSION As Integer = 1

    Private Const _EnableAll As Byte = 2
    Private Const _EnableErrorsAndWarnings As Byte = 1
    Private Const _EnableErrorsOnly As Byte = 0
    Private Const _EnableNothing As Byte = 255
    Private Const _EnableInfoOnly As Byte = 3
    Private Const _EnableAllButInfo As Byte = 3

    Private runResults As Byte
    Private testResults As Byte
    Private stepResults As Byte
    Private Shared FilePath As String = String.Empty
    Private TotalNumberOfRuns As Integer = 0
    Private CurrentLoopInRuns As Integer
    Private CurrentRunNumberOfProject As Int64
    Private IgnoreWriteCommands As Boolean
    Private CurrentTestNumber As Integer
    Private CurrentTestName As String
    Private CurrentStepName As String
    Private CurrentStepNumber As Byte
    Private aFilter As Byte
    Private Shared ProjectGUID As System.Guid = System.Guid.Empty
    Private Shared RunGUID As System.Guid
    Private InternalProjectName As String
    Private Shared IsOfficalRun As Boolean = False
    Private Shared HasAttemptedUpgrade As Boolean = False
    Private sqlException As Exception = Nothing
    Private Shared NetworkConnectionString As String
    Private Shared UpdatedLastRun As Boolean

    Private Sub ResetReporter()
        ResetReporterNoStartup()
        StartReport()
    End Sub

#Region "SqlRelatedFunctionality"

    Private ReadOnly Property LocalConnectionString() As String
        Get
            Return "Data Source =""" & FilePath & """;Max Database Size=4091;"

        End Get
    End Property

    Private Function GetCommandWithGUIDs() As SqlServerCe.SqlCeCommand
        Dim cmd As New SqlServerCe.SqlCeCommand()
        cmd.Parameters.AddWithValue("@pRunGUID", System.Data.SqlTypes.SqlGuid.Parse(RunGUID.ToString()).ToString())
        cmd.Parameters.AddWithValue("@pProjectGUID", System.Data.SqlTypes.SqlGuid.Parse(ProjectGUID.ToString()).ToString())
        Return cmd
    End Function

    Private Shared Function GetSQLRunName() As String
        If (IsOfficalRun = True) Then
            Return "ExternalReportRun"
        Else
            Return "ReportRun"
        End If
    End Function

    Private Shared Function GetSQLRunLineName() As String
        If (IsOfficalRun = True) Then
            Return "ExternalReportLine"
        Else
            Return "ReportLine"
        End If
    End Function

    Private Function RunNonSelectCommand(ByVal cmd As SqlServerCe.SqlCeCommand) As Boolean
        sqlException = Nothing

        Try
            Using connection As New SqlServerCe.SqlCeConnection(LocalConnectionString)
                If Not System.IO.File.Exists(connection.Database) Then
                    Dim engine As New System.Data.SqlServerCe.SqlCeEngine(LocalConnectionString)
                    engine.CreateDatabase()
                End If
                cmd.Connection = connection
                connection.Open()

                cmd.ExecuteNonQuery()
                cmd.Dispose()
                connection.Close()
            End Using
        Catch ex As Exception
            sqlException = ex
            Return False
        End Try
        UpdateLastRunStartTimeIfRequired()
        Return True 'success
    End Function

    Private Function GetTime() As String
        Return Now.ToString("yyyy-MM-dd HH:mm:ss")
    End Function

    Private Function GetTime(ByVal OffsetTime As Integer) As String
        Return Now.AddSeconds(OffsetTime).ToString("yyyy-MM-dd HH:mm:ss")
    End Function

    Private Function FixSQLStr(ByVal Str As String) As String
        Return Str 'Str.Replace("'", "''")
    End Function

#End Region

    Private Sub ResetReporterNoStartup()
        testResults = _Pass
        stepResults = _Pass
        runResults = _Pass
        CurrentLoopInRuns = 1
        CurrentTestNumber = 1
        CurrentTestName = "Test " & CurrentTestNumber
        CurrentStepNumber = 1
        CurrentStepName = "Step " & CurrentStepNumber
    End Sub

    Private Sub StartReport()
        Dim InsertStrLine As String = String.Empty
        Try
            Using connection As New SqlServerCe.SqlCeConnection(LocalConnectionString)
                If Not System.IO.File.Exists(connection.Database) Then
                    Dim engine As New System.Data.SqlServerCe.SqlCeEngine(LocalConnectionString)
                    engine.CreateDatabase()
                End If
                Dim cmd As New SqlServerCe.SqlCeCommand()
                cmd.Connection = connection
                connection.Open()
                '*****************SHOULD BE CHANGED TO GET DB VERSION EVENTUALLY.
                cmd.Parameters.AddWithValue("@ProjectGUID", System.Data.SqlTypes.SqlGuid.Parse(ProjectGUID.ToString()).ToString())
                Dim ReportRunNumber As String = "SELECT TOP(1) RunNumber FROM " & GetSQLRunName() & _
                " WHERE ProjectGUID = @ProjectGUID ORDER BY RunNumber DESC"
                Dim ReportVersion As String = "SELECT * FROM DBInfo"
                Dim rdr As System.Data.SqlServerCe.SqlCeDataReader

                cmd.CommandText = ReportVersion
                Dim Version As Integer = 0
                Try
                    rdr = cmd.ExecuteReader()
                    rdr.Read()
                    Version = rdr.GetInt32(0)
                    rdr.Close()
                Catch ex As Exception
                    'RunNumber, ReportType, ReportStartTime, ReportCompletedTime, ProjectName, ProjectGUID
                    cmd.CommandText = "CREATE TABLE ReportRun( " & vbNewLine & _
                                 "[RunNumber] [bigint] NOT NULL," & vbNewLine & _
                                 "[ReportType] [tinyint] NOT NULL," & vbNewLine & _
                                 "[ReportStartTime] [datetime] NOT NULL," & vbNewLine & _
                                 "[ReportCompletedTime] [datetime] NULL," & vbNewLine & _
                                 "[ProjectName] [nvarchar](150) NULL," & vbNewLine & _
                                 "[ProjectGUID] [nchar](36) NOT NULL," & vbNewLine & _
                                 "[RunGUID] [nchar](36) NOT NULL," & vbNewLine & _
                                 "[PCName] [nvarchar](255) NULL," & vbNewLine & _
                                 "[UserName] [nvarchar](255) NULL," & vbNewLine & _
                                 "[RunComments] [ntext] NULL" & vbNewLine & _
                                 ")"
                    InsertStrLine = cmd.CommandText
                    cmd.ExecuteNonQuery()

                    cmd.CommandText = "CREATE TABLE ReportLine( " & vbNewLine & _
                                     "[ResultID] [bigint] IDENTITY(1,1)," & vbNewLine & _
                                     "[RunNumber] [bigint] NOT NULL," & vbNewLine & _
                                     "[ReportType] [tinyint] NOT NULL," & vbNewLine & _
                                     "[MajorText] [ntext] NOT NULL," & vbNewLine & _
                                     "[MinorText] [ntext] NULL," & vbNewLine & _
                                     "[StepNumber] [int] NOT NULL," & vbNewLine & _
                                     "[StepName] [nvarchar](250) NULL," & vbNewLine & _
                                     "[TestNumber] [int] NOT NULL," & vbNewLine & _
                                     "[TestName] [nvarchar](250) NULL," & vbNewLine & _
                                     "[LoopRunNumber] [int] NOT NULL," & vbNewLine & _
                                     "[LineRunTime] [datetime] NOT NULL," & vbNewLine & _
                                     "[RunGUID] [nchar](36) NOT NULL," & vbNewLine & _
                                     "[ProjectGUID] [nchar](36) NOT NULL," & vbNewLine & _
                                     "[RunLineGUID] [nchar](36) NOT NULL" & vbNewLine & _
                                     ")"
                    InsertStrLine = cmd.CommandText
                    cmd.ExecuteNonQuery()
                    cmd.CommandText = "CREATE TABLE ExternalReportRun( " & vbNewLine & _
                                 "[RunNumber] [bigint] NOT NULL," & vbNewLine & _
                                 "[ReportType] [tinyint] NOT NULL," & vbNewLine & _
                                 "[ReportStartTime] [datetime] NOT NULL," & vbNewLine & _
                                 "[ReportCompletedTime] [datetime] NULL," & vbNewLine & _
                                 "[ProjectName] [nvarchar](150) NULL," & vbNewLine & _
                                 "[ProjectGUID] [nchar](36) NOT NULL," & vbNewLine & _
                                 "[RunGUID] [nchar](36) NOT NULL," & vbNewLine & _
                                 "[PCName] [nvarchar](255) NULL," & vbNewLine & _
                                 "[UserName] [nvarchar](255) NULL," & vbNewLine & _
                                 "[RunComments] [ntext] NULL" & vbNewLine & _
                                 ")"
                    InsertStrLine = cmd.CommandText
                    cmd.ExecuteNonQuery()

                    cmd.CommandText = "CREATE TABLE ExternalReportLine( " & vbNewLine & _
                                     "[ResultID] [bigint] IDENTITY(1,1)," & vbNewLine & _
                                     "[RunNumber] [bigint] NOT NULL," & vbNewLine & _
                                     "[ReportType] [tinyint] NOT NULL," & vbNewLine & _
                                     "[MajorText] [ntext] NOT NULL," & vbNewLine & _
                                     "[MinorText] [ntext] NULL," & vbNewLine & _
                                     "[StepNumber] [int] NOT NULL," & vbNewLine & _
                                     "[StepName] [nvarchar](250) NULL," & vbNewLine & _
                                     "[TestNumber] [int] NOT NULL," & vbNewLine & _
                                     "[TestName] [nvarchar](250) NULL," & vbNewLine & _
                                     "[LoopRunNumber] [int] NOT NULL," & vbNewLine & _
                                     "[LineRunTime] [datetime] NOT NULL," & vbNewLine & _
                                     "[RunGUID] [nchar](36) NOT NULL," & vbNewLine & _
                                     "[ProjectGUID] [nchar](36) NOT NULL," & vbNewLine & _
                                     "[RunLineGUID] [nchar](36) NOT NULL" & vbNewLine & _
                                     ")"
                    InsertStrLine = cmd.CommandText
                    cmd.ExecuteNonQuery()
                    'RunNumber, ProjectName, IsExternalRun, ProjectGUID, RunGUID, PCName
                    cmd.CommandText = "CREATE TABLE LastRunInfo( " & vbNewLine & _
                                 "[RunNumber] [bigint] NOT NULL," & vbNewLine & _
                                 "[ProjectName] [nvarchar](150) NULL," & vbNewLine & _
                                 "[IsExternalRun] [bit] NOT NULL," & vbNewLine & _
                                 "[ProjectGUID] [nchar](36) NOT NULL," & vbNewLine & _
                                 "[RunGUID] [nchar](36) NOT NULL," & vbNewLine & _
                                 "[PCName] [nvarchar](255) NULL," & vbNewLine & _
                                 "[StartRunTime] [datetime] NOT NULL" & vbNewLine & _
                                 ")"
                    InsertStrLine = cmd.CommandText
                    cmd.ExecuteNonQuery()

                    cmd.CommandText = "CREATE TABLE DBInfo( " & vbNewLine & _
                    "[DBVersion] [int] NOT NULL" & vbNewLine & _
                    ")"
                    InsertStrLine = cmd.CommandText
                    cmd.ExecuteNonQuery()

                    cmd.CommandText = "INSERT INTO DBInfo VALUES(" & DBVERSION & ")"
                    InsertStrLine = cmd.CommandText
                    cmd.ExecuteNonQuery()

                    cmd.CommandText = ReportVersion

                    rdr = cmd.ExecuteReader()
                    rdr.Read()
                    Version = rdr.GetInt32(0)
                    rdr.Close()
                End Try
                If (Version <> DBVERSION) Then
                    Alert.Show("Database version are not the same.  Please run DB update tool on database: '" & _
                                                         FilePath & "'.  Test will stop now and no results will be recorded.")
                    System.Diagnostics.Process.GetCurrentProcess.Kill()
                End If
                ''''''''''''''''''
                cmd.CommandText = ReportRunNumber
                rdr = cmd.ExecuteReader() 'Get the count.
                Try
                    rdr.Read()
                    CurrentRunNumberOfProject = Convert.ToInt64(rdr.Item("RunNumber"))
                    CurrentRunNumberOfProject += 1
                    rdr.Close()
                Catch ex As Exception
                    'Must be a new DB
                    CurrentRunNumberOfProject = 1
                    rdr.Close()
                End Try

                ''''''''''''''''''
                Dim RunGUIDTemp As String = System.Data.SqlTypes.SqlGuid.Parse(RunGUID.ToString()).ToString()
                cmd.Parameters.Clear()
                cmd.Parameters.AddWithValue("@pRunGUID", RunGUIDTemp)
                cmd.Parameters.AddWithValue("@pCurrentRunNumber", CurrentRunNumberOfProject)
                cmd.Parameters.AddWithValue("@ProjectGUID", System.Data.SqlTypes.SqlGuid.Parse(ProjectGUID.ToString()).ToString())
                cmd.Parameters.AddWithValue("@InternalProjectName", FixSQLStr(Me.InternalProjectName))
                cmd.Parameters.AddWithValue("@ComputerName", FixSQLStr(My.Computer.Name))
                Try
                    cmd.Parameters.AddWithValue("@UserName", FixSQLStr(System.Windows.Forms.SystemInformation.UserName))
                Catch ex As Exception
                    cmd.Parameters.AddWithValue("@UserName", "")
                End Try


                'RunNumber, ReportType, ReportStartTime, ReportCompletedTime, ProjectName, ProjectGUID
                InsertStrLine = "INSERT INTO " & GetSQLRunName() & " VALUES (" & "@pCurrentRunNumber" & _
                "," & _Pass & ",'" & GetTime() & "', '', " & "@InternalProjectName" & ", " & _
                "@ProjectGUID" & ", " & "@pRunGUID" & ", " & "@ComputerName" & ", @UserName,''" & ")"
                'user name is currently not included.

                cmd.CommandText = InsertStrLine
                cmd.ExecuteNonQuery()
                cmd.Parameters.Clear()


                'Always drop the table
                cmd.Parameters.Clear()
                cmd.CommandText = "DROP TABLE LastRunInfo"
                InsertStrLine = cmd.CommandText
                Try
                    cmd.ExecuteNonQuery() 'if it can't be dropped, that's ok.
                Catch
                End Try
                cmd.CommandText = "CREATE TABLE LastRunInfo( " & vbNewLine & _
                                 "[RunNumber] [bigint] NOT NULL," & vbNewLine & _
                                 "[ProjectName] [nvarchar](150) NULL," & vbNewLine & _
                                 "[IsExternalRun] [bit] NOT NULL," & vbNewLine & _
                                 "[ProjectGUID] [nchar](36) NOT NULL," & vbNewLine & _
                                 "[RunGUID] [nchar](36) NOT NULL," & vbNewLine & _
                                 "[PCName] [nvarchar](255) NULL," & vbNewLine & _
                                 "[StartRunTime] [datetime] NOT NULL" & vbNewLine & _
                                 ")"
                InsertStrLine = cmd.CommandText
                cmd.ExecuteNonQuery()
                cmd.Parameters.AddWithValue("@pCurrentRunNumber", CurrentRunNumberOfProject)
                cmd.Parameters.AddWithValue("@pProjectGUID", System.Data.SqlTypes.SqlGuid.Parse(ProjectGUID.ToString()).ToString())
                cmd.Parameters.AddWithValue("@pInternalProjectName", FixSQLStr(Me.InternalProjectName))
                cmd.Parameters.AddWithValue("@pIsExternalRun", IsOfficalRun)
                cmd.Parameters.AddWithValue("@pComputerName", FixSQLStr(My.Computer.Name))
                cmd.Parameters.AddWithValue("@pRunGUID", RunGUIDTemp)
                cmd.Parameters.AddWithValue("@pStartRunTime", Me.GetTime(-9999))


                InsertStrLine = "INSERT INTO LastRunInfo VALUES (" & "@pCurrentRunNumber" & _
               "," & "@pInternalProjectName" & ", " & "@pIsExternalRun" & ", " & _
               "@pProjectGUID" & ", @pRunGUID, " & _
                "@pComputerName, " & "@pStartRunTime" & ")"
                cmd.CommandText = InsertStrLine
                InsertStrLine = cmd.CommandText
                cmd.ExecuteNonQuery()
                UpdatedLastRun = False
                cmd.Parameters.Clear()
                'End Try
                ''''''''''''''''
                ' Always dispose data readers and commands as soon as practicable
                'rdr.Close()
                cmd.Dispose()
                connection.Close()
            End Using
        Catch ex As Exception
            If (HasAttemptedUpgrade = True) Then
                Alert.Show("Error creating DB: " & ex.ToString() & vbNewLine & _
                InsertStrLine & vbNewLine & LocalConnectionString & _
                vbNewLine & "Upgrade attempted: " & HasAttemptedUpgrade)
            Else
                HasAttemptedUpgrade = True
                If (ex.ToString().ToLower.Contains("upgrade")) Then
                    Try
                        Using connection As New SqlServerCe.SqlCeConnection(LocalConnectionString)
                            If System.IO.File.Exists(connection.Database) Then
                                Dim engine As New System.Data.SqlServerCe.SqlCeEngine(LocalConnectionString)
                                engine.Upgrade()
                                engine.Shrink()
                                'engine.Compact(LocalConnectionString) 'no sense compacting when the compact option requires new files and a user has the option to do it.
                            End If
                        End Using
                    Catch ex2 As Exception
                        Alert.Show( _
                        "Upgrade attempted but failed: " & ex2.ToString())
                    End Try
                End If
                StartReport()
            End If
        End Try
    End Sub

    Private Function UpdateTestResults(ByVal reporterType As Byte) As Boolean
        Select Case reporterType
            Case _Pass
                Return False 'Passes are never updated, as this is the default state.
            Case _Fail
                If (testResults = Fail) Then Return False
                testResults = Fail
                Return True
            Case _Info
                If (testResults = _Pass) Then
                    testResults = Info
                    Return True
                End If
                Return False
            Case _Warning
                If (testResults = _Pass Or testResults = _Info) Then
                    testResults = Warning
                    Return True
                End If
                Return False
            Case Else
                Return False
        End Select
        Return True
    End Function

    Private Sub UpdateLastRunStartTimeIfRequired()
        If (UpdatedLastRun = False) Then
            UpdatedLastRun = True 'MUST be changed first before sql query is run.
            Dim cmd As New SqlServerCe.SqlCeCommand()
            cmd.Parameters.AddWithValue("@pStartRunTime", Me.GetTime())
            cmd.CommandText = "UPDATE LastRunInfo SET StartRunTime = @pStartRunTime"
            If (RunNonSelectCommand(cmd) = False) Then
                Throw sqlException
            End If
        End If
    End Sub

    Private Function MessageTypeAsString(ByVal TypeOfMessage As Byte) As String
        Select Case TypeOfMessage
            Case Pass
                Return "Pass"
            Case Fail
                Return "Fail"
            Case Info
                Return "Info"
            Case Warning
                Return "Warning"
            Case Else
                Return "Unknown"
        End Select
    End Function

#End Region

#Region "Properties"
    ''' <summary>
    ''' Provided to implement the interface, but not currently used.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property ReportConnectionString() As String Implements IReport.ReportConnectionString
        Get
            Return NetworkConnectionString
        End Get
        Set(ByVal value As String)
            NetworkConnectionString = value
        End Set
    End Property

    ''' <summary>
    ''' The total number of runs a user wishes to execute.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks>Typically this is set at the beginning of a project.</remarks>
    Public Property Runs() As Integer Implements IReport.Runs
        Get
            Return TotalNumberOfRuns
        End Get
        Set(ByVal value As Integer)
            TotalNumberOfRuns = value
        End Set
    End Property

    ''' <summary>
    ''' The current run in a set of runs.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property CurrentRun() As Integer Implements IReport.CurrentRun
        Get
            Return CurrentLoopInRuns
        End Get
    End Property

    ''' <summary>
    ''' Gets the current step number in the current test.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property StepNumber() As Byte Implements IReport.StepNumber
        Get
            Return CurrentStepNumber
        End Get
    End Property

    ''' <summary>
    ''' This allows a user to get a test's name or change the current test's name.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks>Since the first test in a project may not be preset to
    ''' what a user desires, this allows a user to override the default
    ''' test name.<BR/>
    ''' NOTE: A test will not be recorded unless at least one event is recorded with that testname.</remarks>
    Public Property TestName() As String Implements IReport.TestName
        Get
            Return CurrentTestName
        End Get
        Set(ByVal value As String)
            'Update test name in DB here
            Dim cmd As SqlServerCe.SqlCeCommand = GetCommandWithGUIDs()
            cmd.Parameters.AddWithValue("@pNewTestName", value)
            cmd.Parameters.AddWithValue("@pOldTestName", CurrentTestName)

            cmd.CommandText = "UPDATE " & GetSQLRunLineName() & " SET TestName = " & _
                                "@pNewTestName" & " WHERE ProjectGUID = " & "@pProjectGUID" & _
                                " AND RunGUID = " & "@pRunGUID" & _
                                " AND TestName = " & "@pOldTestName" & ";"

            If (RunNonSelectCommand(cmd) = False) Then Return

            CurrentTestName = value
        End Set
    End Property

    ''' <summary>
    ''' This allows a user to get a step's name or change the current step's name.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks>Since the first step in a test may not be preset to
    ''' what a user desires, this allows a user to override the default
    ''' step name.<BR/>
    ''' NOTE: A step will not be recorded unless at least one event is recorded with that stepname.
    ''' </remarks>
    Public Property StepName() As String Implements IReport.StepName
        Get
            Return CurrentStepName
        End Get
        Set(ByVal value As String)
            'Update step name in DB here
            Dim cmd As SqlServerCe.SqlCeCommand = GetCommandWithGUIDs()
            cmd.Parameters.AddWithValue("@pNewStepName", value)
            cmd.Parameters.AddWithValue("@pOldStepName", CurrentStepName)
            cmd.Parameters.AddWithValue("@pTestName", CurrentTestName)

            cmd.CommandText = "UPDATE " & GetSQLRunLineName() & " SET StepName = " & _
                                "@pNewStepName" & " WHERE ProjectGUID = " & "@pProjectGUID" & _
                                " AND RunGUID = " & "@pRunGUID" & _
                                " AND TestName = " & "@pTestName" & _
                                " AND StepName = " & "@pOldStepName" & ";"

            If (RunNonSelectCommand(cmd) = False) Then Return

            CurrentStepName = value
        End Set
    End Property

    ''' <summary>
    ''' Gets the current test number in the current run in the project.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property TestNumber() As Integer Implements IReport.TestNumber
        Get
            Return CurrentTestNumber
        End Get
    End Property

    ''' <summary>
    ''' This is what informs Slick Test that you are on the next step.
    ''' </summary>
    ''' <param name="NextStepName">The step name.  If the step name 
    ''' is blank then the step name will be set to "Step #" where the number
    ''' is the current step number you're on.</param>
    ''' <remarks>
    ''' NOTE: A step will not be recorded unless at least one event is recorded with that stepname.</remarks>
    Public Sub NextStep(Optional ByVal NextStepName As String = "") Implements IReport.NextStep
        CurrentStepNumber = Convert.ToByte(CurrentStepNumber + 1)
        If (String.IsNullOrEmpty(NextStepName)) Then
            CurrentStepName = "Step " & CurrentStepNumber
        Else
            CurrentStepName = NextStepName
        End If
        stepResults = _Pass
    End Sub

    ''' <summary>
    ''' This is what informs Slick Test that you are on the next test.
    ''' </summary>
    ''' <param name="NextTestName">The test name.  If the test name 
    ''' is blank then the test name will be set to "Test #" where the number
    ''' is the current test number you're on.</param>
    ''' <param name="NextStepName">The step name.  If the step name 
    ''' is blank then the step name will be set to "Step 1" as with every
    ''' new test, you start on step 1.</param>
    ''' <remarks>
    ''' NOTE: A test will not be recorded unless at least one event is recorded with that testname.</remarks>
    Public Sub NextTest(Optional ByVal NextTestName As String = "", Optional ByVal NextStepName As String = "") Implements IReport.NextTest
        CurrentTestNumber += 1
        CurrentStepNumber = 1
        testResults = _Pass
        stepResults = _Pass
        If (String.IsNullOrEmpty(NextTestName)) Then
            CurrentTestName = "Test " & CurrentTestNumber
        Else
            CurrentTestName = NextTestName
        End If

        If (String.IsNullOrEmpty(NextStepName)) Then
            CurrentStepName = "Step " & CurrentStepNumber
        Else
            CurrentStepName = NextStepName
        End If

    End Sub

    ''' <summary>
    ''' This Informs Slick Test that you are on the next Run.
    ''' </summary>
    ''' <returns>Returns true if you have additional runs to perform.</returns>
    ''' <remarks>
    ''' This will log some data involving the run and loop numbers with a priority
    ''' of 5.
    ''' Example Usage:<p/>
    ''' <p/>
    ''' Do<p/>
    ''' 'Test Case here<p/>
    ''' Loop While(UIControls.Report.NextRun()=true)<p/>
    ''' 'Cleanup code here
    ''' </remarks>
    Public Function NextRun() As Boolean Implements IReport.NextRun
        CurrentLoopInRuns += 1

        UIControls.Log.LogData("TotalNumberOfRuns: " & TotalNumberOfRuns, 5)
        UIControls.Log.LogData("CurrentLoopInRuns: " & CurrentLoopInRuns, 5)

        If (CurrentLoopInRuns >= TotalNumberOfRuns) Then
            Return False
        End If
        CurrentTestNumber = 1
        CurrentStepNumber = 1
        Return True 'returns true if we are contining to run.
    End Function
#Region "Properties"
    ''' <summary>
    ''' Allows the reporter to set the report status to Warning.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks>A warning is sometimes used as a means to
    ''' "block" a test due to a test issue or because of an
    ''' invalid configuration.</remarks>
    Public ReadOnly Property Warning() As Byte Implements IReport.Warning
        Get
            Return _Warning
        End Get
    End Property

    ''' <summary>
    ''' Allows the reporter to set the report status to Fail.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property Fail() As Byte Implements IReport.Fail
        Get
            Return _Fail
        End Get
    End Property

    ''' <summary>
    ''' Allows the reporter to set the report status to Pass.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property Pass() As Byte Implements IReport.Pass
        Get
            Return _Pass
        End Get
    End Property

    ''' <summary>
    ''' Allows the report to set the reporter status to Info.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks>If a test ends with a status of info, then the
    ''' test will be marked as a pass.</remarks>
    Public ReadOnly Property Info() As Byte Implements IReport.Info
        Get
            Return _Info
        End Get
    End Property

    ''' <summary>
    ''' Sets the filter to not filter any messages.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property EnableAll() As Byte Implements IReport.EnableAll
        Get
            Return _EnableAll
        End Get
    End Property

    ''' <summary>
    ''' Sets the filter to filter out all messages but warnings and errors.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property EnableErrorsAndWarnings() As Byte Implements IReport.EnableErrorsAndWarnings
        Get
            Return _EnableErrorsAndWarnings
        End Get
    End Property

    ''' <summary>
    ''' Sets the filter to filter out all messages but Info messages.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property EnableInfoOnly() As Byte Implements IReport.EnableInfoOnly
        Get
            Return _EnableInfoOnly
        End Get
    End Property

    ''' <summary>
    ''' Sets the filter to filter out all messages but errors.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property EnableErrorsOnly() As Byte Implements IReport.EnableErrorsOnly
        Get
            Return _EnableErrorsOnly
        End Get
    End Property

    ''' <summary>
    ''' Sets the filter to filter out all messages.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property EnableNothing() As Byte Implements IReport.EnableNothing
        Get
            Return _EnableNothing
        End Get
    End Property

    ''' <summary>
    ''' Sets the filter to filter out only Info messages.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property EnableAllButInfo() As Byte Implements IReport.EnableAllButInfo
        Get
            Return _EnableAllButInfo
        End Get
    End Property


    ''' <summary>
    ''' Allows a user to filter out results during runtime.
    ''' </summary>
    ''' <value></value>
    ''' <returns>The current filter setting.</returns>
    ''' <remarks>Any reports that are filtered out will by this
    ''' filter will not be saved at all.</remarks>
    Public Property Filter() As Byte Implements IReport.Filter
        Get
            Return aFilter
        End Get
        Set(ByVal value As Byte)
            If (IsNumeric(value)) Then
                If ((value >= 0 And value < 5) OrElse value = 255) Then
                    aFilter = value
                End If
            End If
        End Set
    End Property
#End Region

#End Region

    ''' <summary>
    ''' Reports information to the Slick Test report.
    ''' </summary>
    ''' <param name="TypeOfMessage">Type of message-- Pass, Fail, Warn and Info.</param>
    ''' <param name="MainMessage">The important information you wish to report.</param>
    ''' <param name="AdditionalDetails">Any additional details, such as a file path.</param>
    ''' <remarks>Records a message into the database to be stored.  These events will cause a test case
    ''' to either pass or fail.  All data is also logged with a priority of 8.</remarks>
    Public Sub RecordEvent(ByVal TypeOfMessage As Byte, ByVal MainMessage As String, Optional ByVal AdditionalDetails As String = "") Implements IReport.RecordEvent
        ''''''Report validation and filtering
        If (aFilter = _EnableNothing) Then Return
        If (aFilter = _EnableErrorsAndWarnings) Then
            If (TypeOfMessage <> Warning And TypeOfMessage <> Fail) Then
                Return
            End If
        Else
            If (aFilter = _EnableErrorsOnly) Then
                If (TypeOfMessage <> Fail) Then
                    Return
                End If
            End If
        End If
        If (aFilter = _EnableInfoOnly) Then
            If (TypeOfMessage <> Info) Then
                Return
            End If
        Else
            If (_EnableAllButInfo = aFilter) Then
                If (TypeOfMessage = Info) Then
                    Return
                End If
            End If
        End If
        If (TypeOfMessage <> Fail AndAlso TypeOfMessage <> Pass AndAlso _
        TypeOfMessage <> Info AndAlso TypeOfMessage <> Warning AndAlso _
        TypeOfMessage <> _Unknown) Then
            Throw New SlickTestAPIException("Invalid type of message entered into report.  Type of message: " & TypeOfMessage)
        End If

        ''''''End validation and filtering
        Dim cmd As SqlServerCe.SqlCeCommand = GetCommandWithGUIDs()

        'RunNumber, ReportType, MajorText, MinorText, StepNumber, StepName, LoopRunNumber, LineRun, GUID
        cmd.Parameters.AddWithValue("@pCurrentRunNumber", CurrentRunNumberOfProject)
        cmd.Parameters.AddWithValue("@pTypeOfMessage", TypeOfMessage)
        cmd.Parameters.AddWithValue("@pMajorMessage", FixSQLStr(MainMessage))
        cmd.Parameters.AddWithValue("@pMinorMessage", FixSQLStr(AdditionalDetails))
        cmd.Parameters.AddWithValue("@pStepNumber", StepNumber)
        cmd.Parameters.AddWithValue("@pStepName", FixSQLStr(CurrentStepName))
        cmd.Parameters.AddWithValue("@pTestNumber", TestNumber)
        cmd.Parameters.AddWithValue("@pTestName", FixSQLStr(CurrentTestName))
        cmd.Parameters.AddWithValue("@pCurrentLoopInRuns", CurrentLoopInRuns)
        cmd.Parameters.AddWithValue("@pRunLineGUID", System.Data.SqlTypes.SqlGuid.Parse(System.Guid.NewGuid().ToString()).ToString())

        cmd.CommandText = _
        "INSERT INTO " & GetSQLRunLineName() & _
        "([RunNumber],[ReportType],[MajorText],[MinorText],[StepNumber]," & _
        "[StepName],[TestNumber],[TestName],[LoopRunNumber],[LineRunTime]," & _
        "[RunGUID],[ProjectGUID],[RunLineGUID]) " & _
        " VALUES(" & "@pCurrentRunNumber" & _
        ", " & "@pTypeOfMessage" & ", " & "@pMajorMessage" & ", " & "@pMinorMessage" & _
        ", " & "@pStepNumber" & " , " & "@pStepName" & ", " & "@pTestNumber" & _
        ", " & "@pTestName" & " , " & "@pCurrentLoopInRuns" & ", '" & Me.GetTime() & "'" & _
        ", " & "@pRunGUID, @pProjectGUID, @pRunLineGUID);"

        Log.LogData(MessageTypeAsString(TypeOfMessage) & " - " & TestName & " observed " & MainMessage, 8)

        If (RunNonSelectCommand(cmd) = False) Then
            Throw sqlException
        End If

        If (UpdateTestResults(TypeOfMessage) = True) Then
            cmd = New SqlServerCe.SqlCeCommand("", cmd.Connection)
            'we must update results in DB.
            cmd.Parameters.Add("@pRunGUID", System.Data.SqlTypes.SqlGuid.Parse(RunGUID.ToString()).ToString())
            cmd.Parameters.Add("@pCurrentRunNumber", CurrentRunNumberOfProject)
            cmd.Parameters.Add("@pTestResults", testResults)
            Dim UpdateStrLine As String = "UPDATE " & GetSQLRunName() & " SET ReportType = " & _
            "@pTestResults" & " WHERE RunNumber = " & "@pCurrentRunNumber" & _
            " AND RunGUID = " & "@pRunGUID" & ";"
            cmd.CommandText = UpdateStrLine
            If (RunNonSelectCommand(cmd) = False) Then
                Throw sqlException
            End If
        End If
        '''''''''''''''''
    End Sub

    ''' <summary>
    ''' Reports information to the Slick Test report if and only if the AssertValue is false.
    ''' </summary>
    ''' <param name="AssertValue">A value tested at run time.  If true, the message will not be
    ''' recorded.</param>
    ''' <param name="TypeOfMessage">Type of message-- Pass, Fail, Warn and Info.</param>
    ''' <param name="MainMessage">The important information you wish to report.</param>
    ''' <param name="AdditionalDetails">Any additional details, such as a file path.</param>
    ''' <remarks>Records a message into the database to be stored.  These events will cause a test case
    ''' to either pass or fail.
    ''' <BR/>
    ''' <BR/>
    ''' Example Usage: RecordEventAssert(TextBoxIsMultiLine,Report.Fail,"The text box is not multi-line.")
    ''' </remarks>
    Public Sub RecordEventAssert(ByVal AssertValue As Boolean, ByVal TypeOfMessage As Byte, ByVal MainMessage As String, Optional ByVal AdditionalDetails As String = "") Implements IReport.RecordEventAssert
        If (AssertValue = False) Then
            RecordEvent(TypeOfMessage, MainMessage, AdditionalDetails)
        End If
    End Sub

    Public Overrides Function ToString() As String
        Return LocalConnectionString
    End Function

#Region "Constructor"
    '#If CONFIG <> "Release" Then
    '    Protected Friend Sub New()
    '        If (FilePath = String.Empty) Then
    '            IgnoreWriteCommands = False
    '            aFilter = EnableAll
    '            ProjectGUID = System.Guid.NewGuid()
    '            FilePath = "M:\My Documents\Visual Studio 2008\Projects\SlickTestDeveloper\SlickTestDeveloper\bin\Debug\DB\STD.sdf"
    '            InternalProjectName = "Unknown"
    '            If (RunGUID.Equals(System.Guid.Empty) = True) Then
    '                RunGUID = System.Guid.NewGuid()
    '                Me.ResetReporter()
    '            End If
    '        End If
    '    End Sub
    '#End If

    Protected Friend Sub New(ByVal GUID As System.Guid, ByVal ReportFilePath As String, ByVal ProjectName As String, ByVal RunTypeOfficial As Boolean)
        IsOfficalRun = RunTypeOfficial
        IgnoreWriteCommands = False
        aFilter = EnableAll
        ProjectGUID = GUID
        FilePath = ReportFilePath
        InternalProjectName = ProjectName
        If (RunGUID.Equals(System.Guid.Empty) = True) Then
            RunGUID = System.Guid.NewGuid()
            Me.ResetReporter()
        End If

    End Sub

    ''' <summary>
    ''' This is only used to create a report in order to get at the warning, pass, fail, etc constants.
    ''' </summary>
    ''' <param name="GUID"></param>
    ''' <param name="ReportFilePath"></param>
    ''' <param name="ProjectName"></param>
    ''' <param name="RunTypeOfficial"></param>
    ''' <param name="StartupDatabase"></param>
    ''' <remarks></remarks>
    Protected Friend Sub New(ByVal GUID As System.Guid, ByVal ReportFilePath As String, ByVal ProjectName As String, ByVal RunTypeOfficial As Boolean, ByVal StartupDatabase As Boolean)
        IsOfficalRun = RunTypeOfficial
        IgnoreWriteCommands = False
        aFilter = EnableAll
        ProjectGUID = GUID
        FilePath = ReportFilePath
        InternalProjectName = ProjectName
        If (RunGUID.Equals(System.Guid.Empty) = True) Then
            RunGUID = System.Guid.NewGuid()
            If (StartupDatabase) Then
                Me.ResetReporter()
            Else
                Me.ResetReporterNoStartup()
            End If
        End If
    End Sub
#End Region
End Class