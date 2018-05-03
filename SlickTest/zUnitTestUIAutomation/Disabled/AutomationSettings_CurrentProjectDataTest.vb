'Imports System

'Imports System.Collections.Generic

'Imports Microsoft.VisualStudio.TestTools.UnitTesting

'Imports UIControls



''''<summary>
''''This is a test class for AutomationSettings_CurrentProjectDataTest and is intended
''''to contain all AutomationSettings_CurrentProjectDataTest Unit Tests
''''</summary>
'<TestClass()> _
'Public Class AutomationSettings_CurrentProjectDataTest


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
'    '''A test for UserAssemblies
'    '''</summary>
'    <TestMethod()> _
'    Public Sub UserAssembliesTest()
'        Dim target As AutomationSettings.CurrentProjectData = New AutomationSettings.CurrentProjectData ' TODO: Initialize to an appropriate value
'        Dim expected As List(Of String) = Nothing ' TODO: Initialize to an appropriate value
'        Dim actual As List(Of String)
'        target.UserAssemblies = expected
'        actual = target.UserAssemblies
'        Assert.AreEqual(expected, actual)
'        Assert.Inconclusive("Verify the correctness of this test method.")
'    End Sub

'    '''<summary>
'    '''A test for UseDotNet
'    '''</summary>
'    <TestMethod()> _
'    Public Sub UseDotNetTest()
'        Dim target As AutomationSettings.CurrentProjectData = New AutomationSettings.CurrentProjectData ' TODO: Initialize to an appropriate value
'        Dim expected As Boolean = False ' TODO: Initialize to an appropriate value
'        Dim actual As Boolean
'        target.UseDotNet = expected
'        actual = target.UseDotNet
'        Assert.AreEqual(expected, actual)
'        Assert.Inconclusive("Verify the correctness of this test method.")
'    End Sub


'    '''<summary>
'    '''A test for SpecialAssemblies
'    '''</summary>
'    <TestMethod()> _
'    Public Sub SpecialAssembliesTest()
'        Dim target As AutomationSettings.CurrentProjectData = New AutomationSettings.CurrentProjectData ' TODO: Initialize to an appropriate value
'        Dim expected As List(Of String) = Nothing ' TODO: Initialize to an appropriate value
'        Dim actual As List(Of String)
'        target.SpecialAssemblies = expected
'        actual = target.SpecialAssemblies
'        Assert.AreEqual(expected, actual)
'        Assert.Inconclusive("Verify the correctness of this test method.")
'    End Sub

'    '''<summary>
'    '''A test for SourceFileLocation
'    '''</summary>
'    <TestMethod()> _
'    Public Sub SourceFileLocationTest()
'        Dim target As AutomationSettings.CurrentProjectData = New AutomationSettings.CurrentProjectData ' TODO: Initialize to an appropriate value
'        Dim expected As String = String.Empty ' TODO: Initialize to an appropriate value
'        Dim actual As String
'        target.SourceFileLocation = expected
'        actual = target.SourceFileLocation
'        Assert.AreEqual(expected, actual)
'        Assert.Inconclusive("Verify the correctness of this test method.")
'    End Sub

'    '''<summary>
'    '''A test for ShowUI
'    '''</summary>
'    <TestMethod()> _
'    Public Sub ShowUITest()
'        Dim target As AutomationSettings.CurrentProjectData = New AutomationSettings.CurrentProjectData ' TODO: Initialize to an appropriate value
'        Dim expected As Boolean = False ' TODO: Initialize to an appropriate value
'        Dim actual As Boolean
'        target.ShowUI = expected
'        actual = target.ShowUI
'        Assert.AreEqual(expected, actual)
'        Assert.Inconclusive("Verify the correctness of this test method.")
'    End Sub

'    '''<summary>
'    '''A test for RuntimeTimeout
'    '''</summary>
'    <TestMethod()> _
'    Public Sub RuntimeTimeoutTest()
'        Dim target As AutomationSettings.CurrentProjectData = New AutomationSettings.CurrentProjectData ' TODO: Initialize to an appropriate value
'        Dim expected As Integer = 0 ' TODO: Initialize to an appropriate value
'        Dim actual As Integer
'        target.RuntimeTimeout = expected
'        actual = target.RuntimeTimeout
'        Assert.AreEqual(expected, actual)
'        Assert.Inconclusive("Verify the correctness of this test method.")
'    End Sub

'    '''<summary>
'    '''A test for RuntimeClassName
'    '''</summary>
'    <TestMethod()> _
'    Public Sub RuntimeClassNameTest()
'        Dim target As AutomationSettings.CurrentProjectData = New AutomationSettings.CurrentProjectData ' TODO: Initialize to an appropriate value
'        Dim expected As String = String.Empty ' TODO: Initialize to an appropriate value
'        Dim actual As String
'        target.RuntimeClassName = expected
'        actual = target.RuntimeClassName
'        Assert.AreEqual(expected, actual)
'        Assert.Inconclusive("Verify the correctness of this test method.")
'    End Sub

'    '''<summary>
'    '''A test for ReportDatabasePath
'    '''</summary>
'    <TestMethod()> _
'    Public Sub ReportDatabasePathTest()
'        Dim target As AutomationSettings.CurrentProjectData = New AutomationSettings.CurrentProjectData ' TODO: Initialize to an appropriate value
'        Dim expected As String = String.Empty ' TODO: Initialize to an appropriate value
'        Dim actual As String
'        target.ReportDatabasePath = expected
'        actual = target.ReportDatabasePath
'        Assert.AreEqual(expected, actual)
'        Assert.Inconclusive("Verify the correctness of this test method.")
'    End Sub

'    '''<summary>
'    '''A test for ProjectVersionNumber
'    '''</summary>
'    <TestMethod()> _
'    Public Sub ProjectVersionNumberTest()
'        Dim target As AutomationSettings.CurrentProjectData = New AutomationSettings.CurrentProjectData ' TODO: Initialize to an appropriate value
'        Dim expected As Integer = 0 ' TODO: Initialize to an appropriate value
'        Dim actual As Integer
'        target.ProjectVersionNumber = expected
'        actual = target.ProjectVersionNumber
'        Assert.AreEqual(expected, actual)
'        Assert.Inconclusive("Verify the correctness of this test method.")
'    End Sub

'    '''<summary>
'    '''A test for ProjectType
'    '''</summary>
'    <TestMethod()> _
'    Public Sub ProjectTypeTest()
'        Dim target As AutomationSettings.CurrentProjectData = New AutomationSettings.CurrentProjectData ' TODO: Initialize to an appropriate value
'        Dim expected As AutomationSettings.CurrentProjectData.ProjectTypes = New AutomationSettings.CurrentProjectData.ProjectTypes ' TODO: Initialize to an appropriate value
'        Dim actual As AutomationSettings.CurrentProjectData.ProjectTypes
'        target.ProjectType = expected
'        actual = target.ProjectType
'        Assert.AreEqual(expected, actual)
'        Assert.Inconclusive("Verify the correctness of this test method.")
'    End Sub

'    '''<summary>
'    '''A test for ProjectName
'    '''</summary>
'    <TestMethod()> _
'    Public Sub ProjectNameTest()
'        Dim target As AutomationSettings.CurrentProjectData = New AutomationSettings.CurrentProjectData ' TODO: Initialize to an appropriate value
'        Dim expected As String = String.Empty ' TODO: Initialize to an appropriate value
'        Dim actual As String
'        target.ProjectName = expected
'        actual = target.ProjectName
'        Assert.AreEqual(expected, actual)
'        Assert.Inconclusive("Verify the correctness of this test method.")
'    End Sub

'    '''<summary>
'    '''A test for OutputFolder
'    '''</summary>
'    <TestMethod()> _
'    Public Sub OutputFolderTest()
'        Dim target As AutomationSettings.CurrentProjectData = New AutomationSettings.CurrentProjectData ' TODO: Initialize to an appropriate value
'        Dim expected As String = String.Empty ' TODO: Initialize to an appropriate value
'        Dim actual As String
'        target.OutputFolder = expected
'        actual = target.OutputFolder
'        Assert.AreEqual(expected, actual)
'        Assert.Inconclusive("Verify the correctness of this test method.")
'    End Sub

'    '''<summary>
'    '''A test for OptionStrict
'    '''</summary>
'    <TestMethod()> _
'    Public Sub OptionStrictTest()
'        Dim target As AutomationSettings.CurrentProjectData = New AutomationSettings.CurrentProjectData ' TODO: Initialize to an appropriate value
'        Dim expected As Boolean = False ' TODO: Initialize to an appropriate value
'        Dim actual As Boolean
'        target.OptionStrict = expected
'        actual = target.OptionStrict
'        Assert.AreEqual(expected, actual)
'        Assert.Inconclusive("Verify the correctness of this test method.")
'    End Sub

'    '''<summary>
'    '''A test for OptionExplicit
'    '''</summary>
'    <TestMethod()> _
'    Public Sub OptionExplicitTest()
'        Dim target As AutomationSettings.CurrentProjectData = New AutomationSettings.CurrentProjectData ' TODO: Initialize to an appropriate value
'        Dim expected As Boolean = False ' TODO: Initialize to an appropriate value
'        Dim actual As Boolean
'        target.OptionExplicit = expected
'        actual = target.OptionExplicit
'        Assert.AreEqual(expected, actual)
'        Assert.Inconclusive("Verify the correctness of this test method.")
'    End Sub

'    '''<summary>
'    '''A test for LoadLocation
'    '''</summary>
'    <TestMethod()> _
'    Public Sub LoadLocationTest()
'        Dim target As AutomationSettings.CurrentProjectData = New AutomationSettings.CurrentProjectData ' TODO: Initialize to an appropriate value
'        Dim expected As String = String.Empty ' TODO: Initialize to an appropriate value
'        Dim actual As String
'        target.LoadLocation = expected
'        actual = target.LoadLocation
'        Assert.AreEqual(expected, actual)
'        Assert.Inconclusive("Verify the correctness of this test method.")
'    End Sub

'    '''<summary>
'    '''A test for LastOpenedFile
'    '''</summary>
'    <TestMethod()> _
'    Public Sub LastOpenedFileTest()
'        Dim target As AutomationSettings.CurrentProjectData = New AutomationSettings.CurrentProjectData ' TODO: Initialize to an appropriate value
'        Dim expected As String = String.Empty ' TODO: Initialize to an appropriate value
'        Dim actual As String
'        target.LastOpenedFile = expected
'        actual = target.LastOpenedFile
'        Assert.AreEqual(expected, actual)
'        Assert.Inconclusive("Verify the correctness of this test method.")
'    End Sub

'    '''<summary>
'    '''A test for IsOfficialRun
'    '''</summary>
'    <TestMethod()> _
'    Public Sub IsOfficialRunTest()
'        Dim target As AutomationSettings.CurrentProjectData = New AutomationSettings.CurrentProjectData ' TODO: Initialize to an appropriate value
'        Dim expected As Boolean = False ' TODO: Initialize to an appropriate value
'        Dim actual As Boolean
'        target.IsOfficialRun = expected
'        actual = target.IsOfficialRun
'        Assert.AreEqual(expected, actual)
'        Assert.Inconclusive("Verify the correctness of this test method.")
'    End Sub

'    '''<summary>
'    '''A test for IsDirty
'    '''</summary>
'    <TestMethod()> _
'    Public Sub IsDirtyTest()
'        Dim target As AutomationSettings.CurrentProjectData = New AutomationSettings.CurrentProjectData ' TODO: Initialize to an appropriate value
'        Dim actual As Boolean
'        actual = target.IsDirty
'        Assert.Inconclusive("Verify the correctness of this test method.")
'    End Sub

'    '''<summary>
'    '''A test for GUID
'    '''</summary>
'    <TestMethod()> _
'    Public Sub GUIDTest()
'        Dim target As AutomationSettings.CurrentProjectData = New AutomationSettings.CurrentProjectData ' TODO: Initialize to an appropriate value
'        Dim expected As Guid = New Guid ' TODO: Initialize to an appropriate value
'        Dim actual As Guid
'        target.GUID = expected
'        actual = target.GUID
'        Assert.AreEqual(expected, actual)
'        Assert.Inconclusive("Verify the correctness of this test method.")
'    End Sub

'    '''<summary>
'    '''A test for ExecuteFileName
'    '''</summary>
'    <TestMethod()> _
'    Public Sub ExecuteFileNameTest()
'        Dim target As AutomationSettings.CurrentProjectData = New AutomationSettings.CurrentProjectData ' TODO: Initialize to an appropriate value
'        Dim expected As String = String.Empty ' TODO: Initialize to an appropriate value
'        Dim actual As String
'        target.ExecuteFileName = expected
'        actual = target.ExecuteFileName
'        Assert.AreEqual(expected, actual)
'        Assert.Inconclusive("Verify the correctness of this test method.")
'    End Sub

'    '''<summary>
'    '''A test for CurrentMaxProjectVersionNumber
'    '''</summary>
'    <TestMethod()> _
'    Public Sub CurrentMaxProjectVersionNumberTest()
'        Dim target As AutomationSettings.CurrentProjectData = New AutomationSettings.CurrentProjectData ' TODO: Initialize to an appropriate value
'        Dim actual As Integer
'        actual = target.CurrentMaxProjectVersionNumber
'        Assert.Inconclusive("Verify the correctness of this test method.")
'    End Sub

'    '''<summary>
'    '''A test for CompileAsDebug
'    '''</summary>
'    <TestMethod()> _
'    Public Sub CompileAsDebugTest()
'        Dim target As AutomationSettings.CurrentProjectData = New AutomationSettings.CurrentProjectData ' TODO: Initialize to an appropriate value
'        Dim expected As Boolean = False ' TODO: Initialize to an appropriate value
'        Dim actual As Boolean
'        target.CompileAsDebug = expected
'        actual = target.CompileAsDebug
'        Assert.AreEqual(expected, actual)
'        Assert.Inconclusive("Verify the correctness of this test method.")
'    End Sub

'    '''<summary>
'    '''A test for ClassName
'    '''</summary>
'    <TestMethod()> _
'    Public Sub ClassNameTest()
'        Dim target As AutomationSettings.CurrentProjectData = New AutomationSettings.CurrentProjectData ' TODO: Initialize to an appropriate value
'        Dim expected As String = String.Empty ' TODO: Initialize to an appropriate value
'        Dim actual As String
'        target.ClassName = expected
'        actual = target.ClassName
'        Assert.AreEqual(expected, actual)
'        Assert.Inconclusive("Verify the correctness of this test method.")
'    End Sub

'    '''<summary>
'    '''A test for BuildFiles
'    '''</summary>
'    <TestMethod()> _
'    Public Sub BuildFilesTest()
'        Dim target As AutomationSettings.CurrentProjectData = New AutomationSettings.CurrentProjectData ' TODO: Initialize to an appropriate value
'        Dim expected As List(Of String) = Nothing ' TODO: Initialize to an appropriate value
'        Dim actual As List(Of String)
'        target.BuildFiles = expected
'        actual = target.BuildFiles
'        Assert.AreEqual(expected, actual)
'        Assert.Inconclusive("Verify the correctness of this test method.")
'    End Sub

'    '''<summary>
'    '''A test for Assemblies
'    '''</summary>
'    <TestMethod()> _
'    Public Sub AssembliesTest()
'        Dim target As AutomationSettings.CurrentProjectData = New AutomationSettings.CurrentProjectData ' TODO: Initialize to an appropriate value
'        Dim expected As List(Of String) = Nothing ' TODO: Initialize to an appropriate value
'        Dim actual As List(Of String)
'        target.Assemblies = expected
'        actual = target.Assemblies
'        Assert.AreEqual(expected, actual)
'        Assert.Inconclusive("Verify the correctness of this test method.")
'    End Sub

'    '''<summary>
'    '''A test for SaveProject
'    '''</summary>
'    <TestMethod()> _
'    Public Sub SaveProjectTest()
'        Dim target As AutomationSettings.CurrentProjectData = New AutomationSettings.CurrentProjectData ' TODO: Initialize to an appropriate value
'        Dim XmlFile As String = String.Empty ' TODO: Initialize to an appropriate value
'        target.SaveProject(XmlFile)
'        Assert.Inconclusive("A method that does not return a value cannot be verified.")
'    End Sub

'    '''<summary>
'    '''A test for Reset
'    '''</summary>
'    <TestMethod(), _
'     DeploymentItem("InterAction.dll")> _
'    Public Sub ResetTest()
'        Dim target As AutomationSettings_Accessor.CurrentProjectData = New AutomationSettings_Accessor.CurrentProjectData ' TODO: Initialize to an appropriate value
'        Dim IncludeAsms As Boolean = False ' TODO: Initialize to an appropriate value
'        target.Reset(IncludeAsms)
'        Assert.Inconclusive("A method that does not return a value cannot be verified.")
'    End Sub

'    '''<summary>
'    '''A test for RemoveUserAsm
'    '''</summary>
'    <TestMethod()> _
'    Public Sub RemoveUserAsmTest()
'        Dim target As AutomationSettings.CurrentProjectData = New AutomationSettings.CurrentProjectData ' TODO: Initialize to an appropriate value
'        Dim asm As String = String.Empty ' TODO: Initialize to an appropriate value
'        target.RemoveUserAsm(asm)
'        Assert.Inconclusive("A method that does not return a value cannot be verified.")
'    End Sub

'    '''<summary>
'    '''A test for RemoveSpecialAsm
'    '''</summary>
'    <TestMethod()> _
'    Public Sub RemoveSpecialAsmTest()
'        Dim target As AutomationSettings.CurrentProjectData = New AutomationSettings.CurrentProjectData ' TODO: Initialize to an appropriate value
'        Dim asm As String = String.Empty ' TODO: Initialize to an appropriate value
'        target.RemoveSpecialAsm(asm)
'        Assert.Inconclusive("A method that does not return a value cannot be verified.")
'    End Sub

'    '''<summary>
'    '''A test for LoadProject
'    '''</summary>
'    <TestMethod()> _
'    Public Sub LoadProjectTest()
'        Dim target As AutomationSettings.CurrentProjectData = New AutomationSettings.CurrentProjectData ' TODO: Initialize to an appropriate value
'        Dim XmlFile As String = String.Empty ' TODO: Initialize to an appropriate value
'        Dim expected As Boolean = False ' TODO: Initialize to an appropriate value
'        Dim actual As Boolean
'        actual = target.LoadProject(XmlFile)
'        Assert.AreEqual(expected, actual)
'        Assert.Inconclusive("Verify the correctness of this test method.")
'    End Sub

'    '''<summary>
'    '''A test for AddUserAsm
'    '''</summary>
'    <TestMethod()> _
'    Public Sub AddUserAsmTest()
'        Dim target As AutomationSettings.CurrentProjectData = New AutomationSettings.CurrentProjectData ' TODO: Initialize to an appropriate value
'        Dim asm As String = String.Empty ' TODO: Initialize to an appropriate value
'        target.AddUserAsm(asm)
'        Assert.Inconclusive("A method that does not return a value cannot be verified.")
'    End Sub

'    '''<summary>
'    '''A test for AddSpecialAsm
'    '''</summary>
'    <TestMethod()> _
'    Public Sub AddSpecialAsmTest()
'        Dim target As AutomationSettings.CurrentProjectData = New AutomationSettings.CurrentProjectData ' TODO: Initialize to an appropriate value
'        Dim asm As String = String.Empty ' TODO: Initialize to an appropriate value
'        target.AddSpecialAsm(asm)
'        Assert.Inconclusive("A method that does not return a value cannot be verified.")
'    End Sub

'    '''<summary>
'    '''A test for AddFile
'    '''</summary>
'    <TestMethod()> _
'    Public Sub AddFileTest()
'        Dim target As AutomationSettings.CurrentProjectData = New AutomationSettings.CurrentProjectData ' TODO: Initialize to an appropriate value
'        Dim file As String = String.Empty ' TODO: Initialize to an appropriate value
'        target.AddFile(file)
'        Assert.Inconclusive("A method that does not return a value cannot be verified.")
'    End Sub

'    '''<summary>
'    '''A test for AddAsm
'    '''</summary>
'    <TestMethod()> _
'    Public Sub AddAsmTest()
'        Dim target As AutomationSettings.CurrentProjectData = New AutomationSettings.CurrentProjectData ' TODO: Initialize to an appropriate value
'        Dim asm As String = String.Empty ' TODO: Initialize to an appropriate value
'        target.AddAsm(asm)
'        Assert.Inconclusive("A method that does not return a value cannot be verified.")
'    End Sub

'    '''<summary>
'    '''A test for CurrentProjectData Constructor
'    '''</summary>
'    <TestMethod()> _
'    Public Sub AutomationSettings_CurrentProjectDataConstructorTest()
'        Dim target As AutomationSettings.CurrentProjectData = New AutomationSettings.CurrentProjectData
'        Assert.Inconclusive("TODO: Implement code to verify target")
'    End Sub
'End Class
