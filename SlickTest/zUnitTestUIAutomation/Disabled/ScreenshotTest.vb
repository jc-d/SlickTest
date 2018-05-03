'Imports System.Drawing

'Imports System

'Imports NUnit.Framework

'Imports UIControls



''''<summary>
''''This is a test class for ScreenshotTest and is intended
''''to contain all ScreenshotTest Unit Tests
''''</summary>
'<TestFixture()>  _
'Public Class ScreenshotTest


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
'    '''A test for PixelDiff
'    '''</summary>
'    <Test()> _
'    Public Sub PixelDiffTest()
'        Dim target As Screenshot = New Screenshot ' TODO: Initialize to an appropriate value
'        Dim actual As Double
'        actual = target.PixelDiff
'        Verify.Inconclusive("Verify the correctness of this test method.")
'    End Sub

'    '''<summary>
'    '''A test for TopWindow
'    '''</summary>
'    <Test()> _
'    Public Sub TopWindowTest1()
'        Dim target As Screenshot = New Screenshot ' TODO: Initialize to an appropriate value
'        Dim FilePath As String = String.Empty ' TODO: Initialize to an appropriate value
'        Dim expected As Boolean = False ' TODO: Initialize to an appropriate value
'        Dim actual As Boolean
'        actual = target.TopWindow(FilePath)
'        Verify.AreEqual(expected, actual)
'        Verify.Inconclusive("Verify the correctness of this test method.")
'    End Sub

'    '''<summary>
'    '''A test for TopWindow
'    '''</summary>
'    <Test()> _
'    Public Sub TopWindowTest()
'        Dim target As Screenshot = New Screenshot ' TODO: Initialize to an appropriate value
'        Dim FilePath As String = String.Empty ' TODO: Initialize to an appropriate value
'        Dim ScreenSection As Rectangle = New Rectangle ' TODO: Initialize to an appropriate value
'        Dim expected As Boolean = False ' TODO: Initialize to an appropriate value
'        Dim actual As Boolean
'        actual = target.TopWindow(FilePath, ScreenSection)
'        Verify.AreEqual(expected, actual)
'        Verify.Inconclusive("Verify the correctness of this test method.")
'    End Sub

'    '''<summary>
'    '''A test for SaveToDisk
'    '''</summary>
'    <Test(), _
'     DeploymentItem("InterAction.dll")> _
'    Public Sub SaveToDiskTest()
'        Dim target As Screenshot_Accessor = New Screenshot_Accessor ' TODO: Initialize to an appropriate value
'        Dim directory As String = String.Empty ' TODO: Initialize to an appropriate value
'        Dim fileName As String = String.Empty ' TODO: Initialize to an appropriate value
'        Dim image As Image = Nothing ' TODO: Initialize to an appropriate value
'        Dim expected As String = String.Empty ' TODO: Initialize to an appropriate value
'        Dim actual As String
'        actual = target.SaveToDisk(directory, fileName, image)
'        Verify.AreEqual(expected, actual)
'        Verify.Inconclusive("Verify the correctness of this test method.")
'    End Sub

'    '''<summary>
'    '''A test for Crop
'    '''</summary>
'    <Test(), _
'     DeploymentItem("InterAction.dll")> _
'    Public Sub CropTest()
'        Dim target As Screenshot_Accessor = New Screenshot_Accessor ' TODO: Initialize to an appropriate value
'        Dim X As Integer = 0 ' TODO: Initialize to an appropriate value
'        Dim Y As Integer = 0 ' TODO: Initialize to an appropriate value
'        Dim Height As Integer = 0 ' TODO: Initialize to an appropriate value
'        Dim Width As Integer = 0 ' TODO: Initialize to an appropriate value
'        Dim expected As Bitmap = Nothing ' TODO: Initialize to an appropriate value
'        Dim actual As Bitmap
'        actual = target.Crop(X, Y, Height, Width)
'        Verify.AreEqual(expected, actual)
'        Verify.Inconclusive("Verify the correctness of this test method.")
'    End Sub

'    '''<summary>
'    '''A test for CreateBitMap
'    '''</summary>
'    <Test(), _
'     DeploymentItem("InterAction.dll")> _
'    Public Sub CreateBitMapTest()
'        Dim target As Screenshot_Accessor = New Screenshot_Accessor ' TODO: Initialize to an appropriate value
'        Dim FilePath As String = String.Empty ' TODO: Initialize to an appropriate value
'        Dim X As Integer = 0 ' TODO: Initialize to an appropriate value
'        Dim Y As Integer = 0 ' TODO: Initialize to an appropriate value
'        Dim Width As Integer = 0 ' TODO: Initialize to an appropriate value
'        Dim Height As Integer = 0 ' TODO: Initialize to an appropriate value
'        Dim overWrite As Boolean = False ' TODO: Initialize to an appropriate value
'        Dim tmpHwnd As Integer = 0 ' TODO: Initialize to an appropriate value
'        Dim expected As Boolean = False ' TODO: Initialize to an appropriate value
'        Dim actual As Boolean
'        actual = target.CreateBitMap(FilePath, X, Y, Width, Height, overWrite, tmpHwnd)
'        Verify.AreEqual(expected, actual)
'        Verify.Inconclusive("Verify the correctness of this test method.")
'    End Sub

'    '''<summary>
'    '''A test for CompareImagesByPercentDiff
'    '''</summary>
'    <Test()> _
'    Public Sub CompareImagesByPercentDiffTest()
'        Dim target As Screenshot = New Screenshot ' TODO: Initialize to an appropriate value
'        Dim bitmap1Path As String = String.Empty ' TODO: Initialize to an appropriate value
'        Dim bitmap2Path As String = String.Empty ' TODO: Initialize to an appropriate value
'        Dim saveFileOnFail As Boolean = False ' TODO: Initialize to an appropriate value
'        Dim expected As Double = 0.0! ' TODO: Initialize to an appropriate value
'        Dim actual As Double
'        actual = target.CompareImagesByPercentDiff(bitmap1Path, bitmap2Path, saveFileOnFail)
'        Verify.AreEqual(expected, actual)
'        Verify.Inconclusive("Verify the correctness of this test method.")
'    End Sub

'    '''<summary>
'    '''A test for CompareImages
'    '''</summary>
'    <Test()> _
'    Public Sub CompareImagesTest()
'        Dim target As Screenshot = New Screenshot ' TODO: Initialize to an appropriate value
'        Dim bitmap1Path As String = String.Empty ' TODO: Initialize to an appropriate value
'        Dim bitmap2Path As String = String.Empty ' TODO: Initialize to an appropriate value
'        Dim saveFileOnFail As Boolean = False ' TODO: Initialize to an appropriate value
'        Dim failIfDiffSize As Boolean = False ' TODO: Initialize to an appropriate value
'        Dim expected As Boolean = False ' TODO: Initialize to an appropriate value
'        Dim actual As Boolean
'        actual = target.CompareImages(bitmap1Path, bitmap2Path, saveFileOnFail, failIfDiffSize)
'        Verify.AreEqual(expected, actual)
'        Verify.Inconclusive("Verify the correctness of this test method.")
'    End Sub

'    '''<summary>
'    '''A test for CaptureScreenArea
'    '''</summary>
'    <Test()> _
'    Public Sub CaptureScreenAreaTest()
'        Dim target As Screenshot = New Screenshot ' TODO: Initialize to an appropriate value
'        Dim FilePath As String = String.Empty ' TODO: Initialize to an appropriate value
'        Dim ScreenSection As Rectangle = New Rectangle ' TODO: Initialize to an appropriate value
'        Dim expected As Boolean = False ' TODO: Initialize to an appropriate value
'        Dim actual As Boolean
'        actual = target.CaptureScreenArea(FilePath, ScreenSection)
'        Verify.AreEqual(expected, actual)
'        Verify.Inconclusive("Verify the correctness of this test method.")
'    End Sub

'    '''<summary>
'    '''A test for CaptureDesktop
'    '''</summary>
'    <Test()> _
'    Public Sub CaptureDesktopTest()
'        Dim target As Screenshot = New Screenshot ' TODO: Initialize to an appropriate value
'        Dim FilePath As String = String.Empty ' TODO: Initialize to an appropriate value
'        target.CaptureDesktop(FilePath)
'        Verify.Inconclusive("A method that does not return a value cannot be verified.")
'    End Sub

'    '''<summary>
'    '''A test for CaptureControl
'    '''</summary>
'    <Test()> _
'    Public Sub CaptureControlTest1()
'        Dim target As Screenshot = New Screenshot ' TODO: Initialize to an appropriate value
'        Dim FilePath As String = String.Empty ' TODO: Initialize to an appropriate value
'        Dim ScreenSection As Rectangle = New Rectangle ' TODO: Initialize to an appropriate value
'        Dim hwnd As Integer = 0 ' TODO: Initialize to an appropriate value
'        Dim expected As Boolean = False ' TODO: Initialize to an appropriate value
'        Dim actual As Boolean
'        actual = target.CaptureControl(FilePath, ScreenSection, hwnd)
'        Verify.AreEqual(expected, actual)
'        Verify.Inconclusive("Verify the correctness of this test method.")
'    End Sub

'    '''<summary>
'    '''A test for CaptureControl
'    '''</summary>
'    <Test()> _
'    Public Sub CaptureControlTest()
'        Dim target As Screenshot = New Screenshot ' TODO: Initialize to an appropriate value
'        Dim FilePath As String = String.Empty ' TODO: Initialize to an appropriate value
'        Dim hwnd As Integer = 0 ' TODO: Initialize to an appropriate value
'        Dim expected As Boolean = False ' TODO: Initialize to an appropriate value
'        Dim actual As Boolean
'        actual = target.CaptureControl(FilePath, hwnd)
'        Verify.AreEqual(expected, actual)
'        Verify.Inconclusive("Verify the correctness of this test method.")
'    End Sub

'    '''<summary>
'    '''A test for BuildFilePath
'    '''</summary>
'    <Test(), _
'     DeploymentItem("InterAction.dll")> _
'    Public Sub BuildFilePathTest()
'        Dim target As Screenshot_Accessor = New Screenshot_Accessor ' TODO: Initialize to an appropriate value
'        Dim dir As String = String.Empty ' TODO: Initialize to an appropriate value
'        Dim fileNameSeed As String = String.Empty ' TODO: Initialize to an appropriate value
'        Dim timeStamp As DateTime = New DateTime ' TODO: Initialize to an appropriate value
'        Dim expected As String = String.Empty ' TODO: Initialize to an appropriate value
'        Dim actual As String
'        actual = target.BuildFilePath(dir, fileNameSeed, timeStamp)
'        Verify.AreEqual(expected, actual)
'        Verify.Inconclusive("Verify the correctness of this test method.")
'    End Sub

'    '''<summary>
'    '''A test for Screenshot Constructor
'    '''</summary>
'    <Test()> _
'    Public Sub ScreenshotConstructorTest()
'        Dim target As Screenshot = New Screenshot
'        Verify.Inconclusive("TODO: Implement code to verify target")
'    End Sub
'End Class
