#Const UseAttributes = 2 'set to 1 to enable attributes
#Const IncludeWeb = 2 'set to 1 to enable web
Imports System.Windows.Forms
''' <summary>
''' Automation settings that can be accessed during run time.
''' </summary>
''' <remarks></remarks>
Public Class AutomationSettings
    Protected Friend Shared Project As New CurrentProjectData()
    Private Shared ExecutionPath As String
    Protected Friend Shared ProjectAlreadyLoaded As Boolean

    Protected Friend Sub New(ByVal Path As String)
        UIControls.AutomationSettings.Timeout = UIControls.AutomationSettings.Project.RuntimeTimeout
        If (Path.EndsWith("\") = False) Then Path += "\"
        ExecutionPath = Path
        AbstractWinObject.TakePicturesAfterClicks = AutomationSettings.TakePicturesAfterClick
        AbstractWinObject.TakePicturesAfterTyping = AutomationSettings.TakePicturesAfterTyping
        AbstractWinObject.TakePicturesBeforeClicks = AutomationSettings.TakePicturesBeforeClick
        AbstractWinObject.TakePicturesBeforeTyping = AutomationSettings.TakePicturesBeforeTyping
        AbstractWinObject.ImageLocation = Path
        ProjectAlreadyLoaded = True
    End Sub

#If (UseAttributes = 1) Then
    Public Enum TestCasePriority
        Critical = 0
        HighPriority = 1
        MediumPriority = 2
        LowPriority = 3
        LowestPriority = 4
        DoNotRun = 5
    End Enum
#End If

    ''' <summary>
    ''' The amount of time given to search for an object before the automation fails.
    ''' </summary>
    ''' <value>The time to search for an object in seconds.</value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property ExistTimeout() As Integer
        Get
            Return UIControls.AbstractWinObject.ExistTimeout
        End Get
        Set(ByVal value As Integer)
            UIControls.AbstractWinObject.ExistTimeout = value
        End Set
    End Property

    ''' <summary>
    ''' The path that the automation is running from.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks>This path returns the primary folder for the project where the project 
    ''' (stp) file is located.</remarks>
    Public ReadOnly Property AutomationPath() As String
        Get
            Return ExecutionPath
        End Get
    End Property

    ''' <summary>
    ''' Timeout value in minutes for how long between UI Interactions before the automation
    ''' automatically stops.  If set to 0, the automation will never time out.
    ''' </summary>
    ''' <value>The time to be set, in minutes.</value>
    ''' <returns>The timeout value, which by default is set to 10 minutes.</returns>
    ''' <remarks>Maximum time: 15 minutes</remarks>
    Public Shared Property Timeout() As Integer
        Get
            Return Convert.ToInt32((UIControls.InterAct.RunningTimer.Interval / 60000))
        End Get
        Set(ByVal value As Integer)
            If (value <= 0) Then
                UIControls.InterAct.RunningTimer.Stop()
                UIControls.InterAct.RunningTimer.Interval = 0
                Return
            End If
            If (value > 15) Then value = 15
            UIControls.InterAct.RunningTimer.Interval = (value * 60000)
            UIControls.InterAct.RunningTimer.Stop()
            UIControls.InterAct.RunningTimer.Start()
        End Set
    End Property

    ''' <summary>
    ''' Returns the name of the project, which is based upon the name of
    ''' the project.
    ''' </summary>
    ''' <value></value>
    ''' <returns>Returns the name of the project given at the time the project
    ''' was created.
    ''' </returns>
    ''' <remarks></remarks>
    Public Shared ReadOnly Property ProjectName() As String
        Get
            Return Project.ProjectName
        End Get
    End Property

    ''' <summary>
    ''' Returns the project type.
    ''' </summary>
    ''' <value></value>
    ''' <returns>Returns the project type.</returns>
    ''' <remarks></remarks>
    Public Shared ReadOnly Property ProjectType() As String
        Get
            Return [Enum].GetName(GetType(CurrentProjectData.ProjectTypes), Project.ProjectType)
        End Get
    End Property

    ''' <summary>
    ''' Returns the connection string. 
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks>Currently unsupported, but provided for any users who choose to
    ''' override the reporter  In the future this will be used only for external
    ''' reports.</remarks>
    Public Shared ReadOnly Property ExternalReportDatabaseConnectionString() As String
        Get
            Return Project.ExternalReportDatabaseConnectionString
        End Get
    End Property

    ''' <summary>
    ''' Returns the option for taking images before clicking.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks>In order to modify this value, use AbstractWinObject.</remarks>
    Public Shared ReadOnly Property TakePicturesBeforeClick() As Boolean
        Get
            Return Project.TakePictureBeforeClick
        End Get
    End Property

    ''' <summary>
    ''' Returns the option for taking images after clicking.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks>In order to modify this value, use AbstractWinObject.</remarks>
    Public Shared ReadOnly Property TakePicturesAfterClick() As Boolean
        Get
            Return Project.TakePictureAfterClick
        End Get
    End Property

    ''' <summary>
    ''' Returns the option for taking images before typing.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks>This excludes the usage of sendkeys.
    ''' In order to modify this value, use AbstractWinObject.</remarks>
    Public Shared ReadOnly Property TakePicturesBeforeTyping() As Boolean
        Get
            Return Project.TakePictureBeforeTyping
        End Get
    End Property

    ''' <summary>
    ''' Returns the option for taking images after typing.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks>This excludes the usage of sendkeys.
    ''' In order to modify this value, use AbstractWinObject.</remarks>
    Public Shared ReadOnly Property TakePicturesAfterTyping() As Boolean
        Get
            Return Project.TakePictureAfterTyping
        End Get
    End Property

#Region "Current Project Data"
    '/////////////Current Project Data

    Protected Friend Class CurrentProjectData
        Public ProjectLoadError As String
        Public ProjectLoadFailDueToDifferentVars As Boolean
        Friend Shared DefAsms As New System.Collections.Generic.List(Of String)
        Private LastSpecialAssemblyCount As Integer = 0
        Private LastUserAssemblyCount As Integer = 0
        Protected Friend MyIni As New XmlSettings.AppSettings
        Private LastBuildFileCount As Integer = 0

        Public Enum ProjectTypes
            RecordAndGo
            'BlankProject
            LibraryProject
        End Enum


#Region "Properties"

        Private Dirty As Boolean
        Public ReadOnly Property IsDirty() As Boolean
            Get
                Return Dirty
            End Get
        End Property

        Private IsOfficalRun1 As Boolean
        Public Property IsOfficialRun() As Boolean
            Get
                Return IsOfficalRun1
            End Get
            Set(ByVal value As Boolean)
                If (IsOfficalRun1 <> value) Then
                    Dirty = True
                End If
                IsOfficalRun1 = value
            End Set
        End Property

        Private ProjectGUID As System.Guid
        Public Property GUID() As System.Guid
            Get
                Return ProjectGUID
            End Get
            Set(ByVal value As System.Guid)
                ProjectGUID = value
            End Set
        End Property

        Private ReportDBPath As String
        Public Property ReportDatabasePath() As String
            Get
                Return ReportDBPath
            End Get
            Set(ByVal value As String)
                If (ReportDBPath <> value) Then
                    Dirty = True
                End If
                ReportDBPath = value
            End Set
        End Property

        Public Assemblies1 As New System.Collections.Generic.List(Of String)
        Public Property Assemblies() As System.Collections.Generic.List(Of String)
            Get
                If (LastSpecialAssemblyCount <> Assemblies1.Count) Then
                    LastSpecialAssemblyCount = Assemblies1.Count
                    Dirty = True
                End If
                Return Assemblies1
            End Get
            Set(ByVal value As System.Collections.Generic.List(Of String))
                If (Assemblies1.Equals(value) = False) Then
                    Dirty = True
                End If
                Assemblies1 = value
            End Set
        End Property

        Private SpecialAssemblies1 As New System.Collections.Generic.List(Of String)
        Public Property SpecialAssemblies() As System.Collections.Generic.List(Of String)
            Get
                If (LastSpecialAssemblyCount <> SpecialAssemblies1.Count) Then
                    LastSpecialAssemblyCount = SpecialAssemblies1.Count
                    Dirty = True
                End If
                Return SpecialAssemblies1
            End Get
            Set(ByVal value As System.Collections.Generic.List(Of String))
                If (SpecialAssemblies1.Equals(value) = False) Then
                    Dirty = True
                End If
                SpecialAssemblies1 = value
            End Set
        End Property

        Private UserAssemblies1 As New System.Collections.Generic.List(Of String)
        Public Property UserAssemblies() As System.Collections.Generic.List(Of String)
            Get
                If (LastUserAssemblyCount <> UserAssemblies1.Count) Then
                    LastUserAssemblyCount = UserAssemblies1.Count
                    Dirty = True
                End If
                Return UserAssemblies1
            End Get
            Set(ByVal value As System.Collections.Generic.List(Of String))
                If (UserAssemblies1.Equals(value) = False) Then
                    Dirty = True
                End If
                UserAssemblies1 = value
            End Set
        End Property

        Private BuildFiles1 As New System.Collections.Generic.List(Of String)
        Public Property BuildFiles() As System.Collections.Generic.List(Of String)
            Get
                If (LastBuildFileCount <> BuildFiles1.Count) Then
                    LastBuildFileCount = BuildFiles1.Count
                    Dirty = True
                End If
                Return BuildFiles1
            End Get
            Set(ByVal value As System.Collections.Generic.List(Of String))
                If (BuildFiles1.Equals(value) = False) Then
                    Dirty = True
                End If
                BuildFiles1 = value
            End Set
        End Property



        Private ProjectType1 As ProjectTypes
        Public Property ProjectType() As ProjectTypes
            Get
                Return ProjectType1
            End Get
            Set(ByVal value As ProjectTypes)
                If (ProjectType <> value) Then
                    Dirty = True
                End If
                ProjectType1 = value
            End Set
        End Property

        Private ProjectName1 As String
        Public Property ProjectName() As String
            Get
                Return ProjectName1
            End Get
            Set(ByVal value As String)
                If (ProjectName1 <> value) Then Dirty = True
                ProjectName1 = value
            End Set
        End Property

        Private ExecuteFileName1 As String
        Public Property ExecuteFileName() As String
            Get
                Return ExecuteFileName1
            End Get
            Set(ByVal value As String)
                If (ExecuteFileName1 <> value) Then Dirty = True
                ExecuteFileName1 = value
            End Set
        End Property

        Private OptionStrict1 As Boolean
        Public Property OptionStrict() As Boolean
            Get
                Return OptionStrict1
            End Get
            Set(ByVal value As Boolean)
                If (OptionStrict1 <> value) Then Dirty = True
                OptionStrict1 = value
            End Set
        End Property


        Private ShowUI1 As Boolean
        Public Property ShowUI() As Boolean
            Get
                Return ShowUI1
            End Get
            Set(ByVal value As Boolean)
                If (ShowUI <> value) Then Dirty = True
                ShowUI1 = value
            End Set
        End Property

        Private ClassName1 As String
        Public Property ClassName() As String
            Get
                Return ClassName1
            End Get
            Set(ByVal value As String)
                If (ClassName1 <> value) Then Dirty = True
                ClassName1 = value
            End Set
        End Property

#Region "Image clicking"
        Private TakePictureBeforeClick1 As Boolean
        Public Property TakePictureBeforeClick() As Boolean
            Get
                Return TakePictureBeforeClick1
            End Get
            Set(ByVal value As Boolean)
                If (TakePictureBeforeClick1 <> value) Then Dirty = True
                TakePictureBeforeClick1 = value
            End Set
        End Property

        Private TakePictureAfterClick1 As Boolean
        Public Property TakePictureAfterClick() As Boolean
            Get
                Return TakePictureAfterClick1
            End Get
            Set(ByVal value As Boolean)
                If (TakePictureAfterClick1 <> value) Then Dirty = True
                TakePictureAfterClick1 = value
            End Set
        End Property

        Private TakePictureAfterTyping1 As Boolean
        Public Property TakePictureAfterTyping() As Boolean
            Get
                Return TakePictureAfterTyping1
            End Get
            Set(ByVal value As Boolean)
                If (TakePictureAfterTyping1 <> value) Then Dirty = True
                TakePictureAfterTyping1 = value
            End Set
        End Property

        Private TakePictureBeforeTyping1 As Boolean
        Public Property TakePictureBeforeTyping() As Boolean
            Get
                Return TakePictureBeforeTyping1
            End Get
            Set(ByVal value As Boolean)
                If (TakePictureBeforeTyping1 <> value) Then Dirty = True
                TakePictureBeforeTyping1 = value
            End Set
        End Property

#End Region

        Private OptionExplicit1 As Boolean
        Public Property OptionExplicit() As Boolean
            Get
                Return OptionExplicit1
            End Get
            Set(ByVal value As Boolean)
                If (OptionExplicit1 <> value) Then Dirty = True
                OptionExplicit1 = value
            End Set
        End Property

        Private RuntimeTimeout1 As Integer
        Public Property RuntimeTimeout() As Integer
            Get
                Return RuntimeTimeout1
            End Get
            Set(ByVal value As Integer)
                If (RuntimeTimeout1 <> value) Then Dirty = True
                RuntimeTimeout1 = value
            End Set
        End Property

        Private RuntimeClassName1 As String
        Public Property RuntimeClassName() As String
            Get
                Return RuntimeClassName1
            End Get
            Set(ByVal value As String)
                If (RuntimeClassName1 <> value) Then Dirty = True
                RuntimeClassName1 = value
            End Set
        End Property

        Private CurrentMaxProjectVersionNumber1 As Integer
        Public ReadOnly Property CurrentMaxProjectVersionNumber() As Integer
            Get
                Return CurrentMaxProjectVersionNumber1
            End Get
        End Property

        Private ProjectVersionNumber1 As Integer
        Public Property ProjectVersionNumber() As Integer
            Get
                Return ProjectVersionNumber1
            End Get
            Set(ByVal value As Integer)
                If (ProjectVersionNumber1 <> value) Then Dirty = True
                ProjectVersionNumber1 = value
            End Set
        End Property

        Private SourceFileLocation1 As String
        Public Property SourceFileLocation() As String
            Get
                Return SourceFileLocation1
            End Get
            Set(ByVal value As String)
                If (SourceFileLocation1 <> value) Then Dirty = True
                SourceFileLocation1 = value
            End Set
        End Property

        Private LoadLocation1 As String
        Public Property LoadLocation() As String
            Get
                Return LoadLocation1
            End Get
            Set(ByVal value As String)
                If (LoadLocation1 <> value) Then Dirty = True
                LoadLocation1 = value
            End Set
        End Property

        Private LastOpenedFile1 As String
        Public Property LastOpenedFile() As String
            Get
                Return LastOpenedFile1
            End Get
            Set(ByVal value As String)
                If (LastOpenedFile1 <> value) Then Dirty = True
                LastOpenedFile1 = value
            End Set
        End Property

        Private CompileAsDebug1 As Boolean
        Public Property CompileAsDebug() As Boolean
            Get
                Return CompileAsDebug1
            End Get
            Set(ByVal value As Boolean)
                If (CompileAsDebug1 <> value) Then Dirty = True
                CompileAsDebug1 = value
            End Set
        End Property

        Private ExternalReportDatabaseConnectionString1 As String
        Public Property ExternalReportDatabaseConnectionString() As String
            Get
                Return ExternalReportDatabaseConnectionString1
            End Get
            Set(ByVal value As String)
                If (ExternalReportDatabaseConnectionString1 <> value) Then Dirty = True
                ExternalReportDatabaseConnectionString1 = value
            End Set
        End Property

        Private HiddenOutputFolder As String = String.Empty
        Public Property OutputFolder() As String
            Get
                If (HiddenOutputFolder.EndsWith("\") = True) Then Return HiddenOutputFolder
                Return HiddenOutputFolder & "\"
            End Get
            Set(ByVal value As String)
                If (value <> OutputFolder) Then
                    Dirty = True
                End If
                If (value.EndsWith("\") = False) Then
                    HiddenOutputFolder = value & "\"
                Else
                    HiddenOutputFolder = value
                End If
                'HiddenOutputFolder = value
            End Set
        End Property

        Private LanguageExt As String = String.Empty
        Public Property LanguageExtension() As String
            Get
                Return LanguageExt
            End Get
            Set(ByVal value As String)
                If (value <> LanguageExt) Then
                    Dirty = True
                End If
                LanguageExt = value
            End Set
        End Property

#End Region

#Region "Methods"

        Public Sub AddAsm(ByVal asm As String)
            If (Assemblies.Contains(asm) = False) Then
                Assemblies.Add(asm)
                Dirty = True
            End If

        End Sub

        Public Sub AddFile(ByVal file As String)
            file = System.IO.Path.GetFileName(file)
            If (BuildFiles.Contains(file) = False) Then
                BuildFiles.Add(file)
                Dirty = True
            End If

        End Sub

        Public Sub New()
            Reset(True)
            CurrentMaxProjectVersionNumber1 = 2
            Dirty = True
        End Sub

        Private Sub Reset(Optional ByVal IncludeAsms As Boolean = False)
            ExternalReportDatabaseConnectionString = "" 'No connection string by default
            IsOfficalRun1 = False
            ProjectLoadFailDueToDifferentVars = False
            LanguageExtension = ".vb"
            Assemblies1 = New System.Collections.Generic.List(Of String)
            BuildFiles1 = New System.Collections.Generic.List(Of String)
            SpecialAssemblies1 = New System.Collections.Generic.List(Of String)
            UserAssemblies1 = New System.Collections.Generic.List(Of String)
            ProjectVersionNumber = CurrentMaxProjectVersionNumber
            ProjectType = ProjectTypes.RecordAndGo
            ProjectName = "NewProject"
            OptionStrict = False
            OptionExplicit = True
            ShowUI = True
            RuntimeTimeout = 10
            OutputFolder = ".\"
            ExecuteFileName = "out.exe"
            RuntimeClassName = ""
            SourceFileLocation = ".\Source\"
            LoadLocation = String.Empty
            ClassName = String.Empty
            LastOpenedFile = String.Empty
            CompileAsDebug = False
            LastBuildFileCount = 0
            LastSpecialAssemblyCount = 0
            TakePictureBeforeTyping = False
            TakePictureAfterTyping = False
            TakePictureBeforeClick = False
            TakePictureAfterClick = False
            ProjectGUID = System.Guid.NewGuid()
            Dim path As String = System.IO.Path.GetDirectoryName(System.Windows.Forms.Application.StartupPath & "\")
            If (path.EndsWith("\") = False) Then path += "\"
            ReportDatabasePath = path & "DB\STD.sdf"
            'IgnoreInternalExceptions = False

            If (IncludeAsms) Then
                'Dim WinDir As String = Environment.GetEnvironmentVariable("WINDIR")
                'If (WinDir.EndsWith("\") = False) Then WinDir += "\"
                'WinDir += "Microsoft.NET\Framework\v2.0.50727\"
                'Assemblies.Add(WinDir & "System.dll") 'C:\WINDOWS\Microsoft.NET\Framework\v2.0.50727\
                'Assemblies.Add(WinDir & "System.Data.dll")
                'Assemblies.Add(WinDir & "System.Drawing.dll")
                'Assemblies.Add(WinDir & "Microsoft.VisualBasic.dll")
                'Assemblies.Add(WinDir & "System.Windows.Forms.dll")
                If (DefAsms.Count <> 0) Then Assemblies.AddRange(DefAsms)
                SpecialAssemblies.Add(path & "InterAction.dll")
                SpecialAssemblies.Add(path & "APIControls.dll")
                SpecialAssemblies.Add(path & "WindowsHookLib.dll")
                SpecialAssemblies.Add(path & "WinAPI.dll")
                SpecialAssemblies.Add(path & "XmlSettings.dll")
#If (IncludeWeb = 1) Then
            SpecialAssemblies.Add(path & "Interop.SHDocVw.dll")
#End If

            End If
            Dirty = True
        End Sub

        Public Sub SaveProject(ByVal XmlFile As String)
            MyIni = New XmlSettings.AppSettings("MySettings")

            MyIni.SetVal("ProjectSettings", "OptionStrict", OptionStrict, _
            "Visual Basic allows conversions of many data types to other data types. " & _
            "Data loss can occur when the value of one data type is converted to a data type " & _
            "with less precision or smaller capacity. A run-time error occurs if such a narrowing " & _
            "conversion fails. Option Strict ensures compile-time notification of these narrowing " & _
            "conversions so they can be avoided.")

            MyIni.SetVal("ProjectSettings", "OptionExplicit", OptionExplicit, _
            "When Option Explicit appears in a file, you must explicitly declare all variables " & _
            "using the Dim or ReDim statements. If you attempt to use an undeclared variable name," & _
            " an error occurs at compile time.")

            MyIni.SetVal("ProjectSettings", "ShowUI", ShowUI, "Shows a DOS console if set to true.")

            MyIni.SetVal("ProjectSettings", "RuntimeTimeout", RuntimeTimeout, _
            "Set the timer to prevent Slick Test from going into an infinite loop.  Set to 0 for infinite time.")

            'MyIni.SetVal("ProjectSettings", "OutputFolder", OutputFolder)
            MyIni.SetVal("InternalProjectSettings", "ExecuteFileName", ExecuteFileName, _
            "The executable file name.")

            MyIni.SetVal("ProjectSettings", "RuntimeClassName", _
            RuntimeClassName, "Typically this does not need to be set.")

            MyIni.SetVal("ProjectSettings", "CompileAsDebug", CompileAsDebug, _
            "Compile the executable or DLL with debug information or optimized.")

            MyIni.SetVal("ProjectSettings", "ProjectName", ProjectName, _
            "The name of the project.")

            MyIni.SetVal("ProjectSettings", "ReportDatabasePath", ReportDatabasePath, _
            "The database path which will store all the local reports.")

            MyIni.SetVal("ProjectSettings", "ExternalReportDatabaseConnectionString", ReportDatabasePath, _
            "If a external database is used for reporting, then the connection string must be provided here." & _
            "This will only be used for external reports or if you use a custom report.")

            For i As Integer = 0 To Assemblies.Count - 1
                MyIni.SetVal("Assemblies", i.ToString, Assemblies(i))
            Next
            MyIni.SetVal("Assemblies", "Count", Assemblies.Count)

            For i As Integer = 0 To SpecialAssemblies.Count - 1
                MyIni.SetVal("SpecialAssemblies", i.ToString, SpecialAssemblies(i))
            Next
            MyIni.SetVal("SpecialAssemblies", "Count", SpecialAssemblies.Count)

            MyIni.SetVal("UserAssemblies", "Count", UserAssemblies.Count)
            For i As Integer = 0 To UserAssemblies.Count - 1
                MyIni.SetVal("UserAssemblies", i.ToString, UserAssemblies(i))
            Next

            MyIni.SetVal("InternalProjectSettings", "LastOpenedFile", LastOpenedFile)

            MyIni.SetVal("InternalProjectSettings", "ProjectVersionNumber", ProjectVersionNumber)

            MyIni.SetVal("InternalProjectSettings", "ProjectGUID", ProjectGUID.ToString())

            MyIni.SetVal("InternalProjectSettings", "IsOfficalRun", IsOfficalRun1)

            MyIni.SetVal("ProjectSettings", "TakePictureBeforeTyping", TakePictureBeforeTyping, _
                         "Takes an image before typing to an object.")
            MyIni.SetVal("ProjectSettings", "TakePictureAfterTyping", TakePictureAfterTyping, _
                         "Takes an image after typing to an object.")
            MyIni.SetVal("ProjectSettings", "TakePictureBeforeClick", TakePictureBeforeClick, _
                         "Takes an image before clicking on an object.")
            MyIni.SetVal("ProjectSettings", "TakePictureAfterClick", TakePictureAfterClick, _
                         "Takes an image after clicking on an object.")

            MyIni.SetVal("ProjectSettings", "ProjectType", ProjectType, _
                         "Changes how the system compiles and runs the code (using a exe or dll).")

            MyIni.SetVal("InternalProjectSettings", "LanguageExtension", LanguageExtension, _
                         "The language used for this slick test project.")

            ProjectVersionNumber = CurrentMaxProjectVersionNumber

            MyIni.Save(XmlFile)
            LoadLocation = XmlFile
            Dirty = False
        End Sub

        Private Const msgSlickTestTitle As String = "Slick Test"
        Public Function SafeLoadProjectWithMessages(ByVal XmlFile As String) As Boolean
            If (Me.LoadProject(XmlFile) = True) Then
                Return True
            Else
                If (Me.ProjectLoadFailDueToDifferentVars = True) Then
                    If (Windows.Forms.DialogResult.Yes = _
                    System.Windows.Forms.MessageBox.Show("Unable to load project (" & XmlFile & _
                                                         "). Error: It appears that the project file is out of date." & _
                                                         " Would you like to attempt to update it now (a backup will be saved)?", _
                                                         msgSlickTestTitle, MessageBoxButtons.YesNo)) Then
                        System.IO.File.Copy(XmlFile, XmlFile & ".bak", True)
                        Try
                            Me.SaveProject(XmlFile)
                        Catch ex As Exception
                            System.Windows.Forms.MessageBox.Show("Failed to save updated project.  Error: " & _
                                                                 ex.Message, msgSlickTestTitle)
                            Return False
                        End Try
                        If (Me.LoadProject(XmlFile) = True) Then
                            Return True
                        Else
                            System.Windows.Forms.MessageBox.Show("Unable to load project (" & _
                                                                 XmlFile & "). Error: " & _
                                                                 Me.ProjectLoadError, msgSlickTestTitle)
                            Return False
                        End If
                    Else
                        Return False
                    End If
                Else
                    System.Windows.Forms.MessageBox.Show("Unable to load project (" & _
                                                         XmlFile & "). Error: " & _
                                                         Me.ProjectLoadError, msgSlickTestTitle)
                    Return False
                End If

            End If
        End Function

        Public Function LoadProject(ByVal XmlFile As String) As Boolean
            Try
                Reset()
                MyIni = New XmlSettings.AppSettings("MySettings")
                MyIni.Load(XmlFile)
                'We've loaded one var, must be good.
                ProjectLoadFailDueToDifferentVars = True
                ProjectVersionNumber = Convert.ToInt32(MyIni.GetVal("InternalProjectSettings", "ProjectVersionNumber").Value)
                OptionStrict = Convert.ToBoolean(MyIni.GetVal("ProjectSettings", "OptionStrict").Value)
                OptionExplicit = Convert.ToBoolean(MyIni.GetVal("ProjectSettings", "OptionExplicit").Value)
                ShowUI = Convert.ToBoolean(MyIni.GetVal("ProjectSettings", "ShowUI").Value)
                RuntimeTimeout = Convert.ToInt32(MyIni.GetVal("ProjectSettings", "RuntimeTimeout").Value)
                'ShowErrors = Convert.ToBoolean(MyIni.GetVal("ProjectSettings", "ShowErrors").Value)

                OutputFolder = System.IO.Path.GetDirectoryName(XmlFile)
                If (OutputFolder.EndsWith("\") = False) Then OutputFolder += "\"

                'MyIni.GetVal("ProjectSettings", "OutputFolder").Value.ToString()
                ExecuteFileName = MyIni.GetVal("InternalProjectSettings", "ExecuteFileName").Value.ToString()
                RuntimeClassName = MyIni.GetVal("ProjectSettings", "RuntimeClassName").Value.ToString()

                For i As Integer = 0 To Convert.ToInt32(MyIni.GetVal("Assemblies", "Count").Value) - 1
                    Assemblies.Add(MyIni.GetVal("Assemblies", i.ToString()).Value.ToString())
                Next

                For i As Integer = 0 To Convert.ToInt32(MyIni.GetVal("UserAssemblies", "Count").Value) - 1
                    UserAssemblies.Add(MyIni.GetVal("UserAssemblies", i.ToString()).Value.ToString())
                Next

                For i As Integer = 0 To Convert.ToInt32(MyIni.GetVal("SpecialAssemblies", "Count").Value) - 1
                    SpecialAssemblies.Add(MyIni.GetVal("SpecialAssemblies", i.ToString()).Value.ToString())
                Next

                'SourceFileLocation = MyIni.GetVal("ProjectSettings", "SourceFileLocation").Value.ToString()
                SourceFileLocation = System.IO.Path.GetDirectoryName(XmlFile)
                If (SourceFileLocation.EndsWith("\") = False) Then SourceFileLocation += "\"
                SourceFileLocation += "Source\"

                LastOpenedFile = MyIni.GetVal("InternalProjectSettings", "LastOpenedFile").Value.ToString()

                CompileAsDebug = Convert.ToBoolean(MyIni.GetVal("ProjectSettings", "CompileAsDebug").Value)

                ProjectName = MyIni.GetVal("ProjectSettings", "ProjectName").Value.ToString()

                ProjectGUID = New System.Guid(MyIni.GetVal("InternalProjectSettings", "ProjectGUID").Value.ToString())

                ReportDatabasePath = MyIni.GetVal("ProjectSettings", "ReportDatabasePath").Value.ToString()

                IsOfficialRun = Convert.ToBoolean(MyIni.GetVal("InternalProjectSettings", "IsOfficalRun").Value.ToString())

                TakePictureBeforeTyping = Convert.ToBoolean(MyIni.GetVal("ProjectSettings", "TakePictureBeforeTyping").Value.ToString())
                TakePictureAfterTyping = Convert.ToBoolean(MyIni.GetVal("ProjectSettings", "TakePictureAfterTyping").Value.ToString())
                TakePictureBeforeClick = Convert.ToBoolean(MyIni.GetVal("ProjectSettings", "TakePictureBeforeClick").Value.ToString())
                TakePictureAfterClick = Convert.ToBoolean(MyIni.GetVal("ProjectSettings", "TakePictureAfterClick").Value.ToString())

                ExternalReportDatabaseConnectionString1 = MyIni.GetVal("ProjectSettings", "ExternalReportDatabaseConnectionString").Value.ToString()

                ProjectType = System.Enum.Parse(GetType(ProjectTypes), MyIni.GetVal("ProjectSettings", "ProjectType").Value, True)

                LanguageExtension = MyIni.GetVal("InternalProjectSettings", "LanguageExtension").Value.ToString()

                LoadLocation = XmlFile
                Dirty = False
            Catch ex As Exception
                ProjectLoadError = ex.Message
                Return False
            End Try
            Return True
        End Function

        Protected Friend Sub AddSpecialAsm(ByVal asm As String)
            If (SpecialAssemblies1.Contains(asm) = True) Then SpecialAssemblies1.Remove(asm)
            SpecialAssemblies1.Add(asm)
            Dirty = True
        End Sub

        Protected Friend Sub RemoveSpecialAsm(ByVal asm As String)
            If (SpecialAssemblies1.Contains(asm) = False) Then Return
            SpecialAssemblies1.Remove(asm)
            Dirty = True
        End Sub

        Protected Friend Sub AddUserAsm(ByVal asm As String)
            If (UserAssemblies.Contains(asm) = True) Then UserAssemblies.Remove(asm)
            UserAssemblies.Add(asm)
            Dirty = True
        End Sub

        Protected Friend Sub RemoveUserAsm(ByVal asm As String)
            If (UserAssemblies.Contains(asm) = False) Then Return
            UserAssemblies.Remove(asm)
            Dirty = True
        End Sub

#End Region
    End Class

    '/////////////////////
#End Region

End Class
