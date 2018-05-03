Imports System
Imports System.Drawing
Imports Microsoft.VisualStudio.TestTools.UnitTesting
Imports UIControls
Imports System.IO

'''<summary>
'''This is a test class for AbstractWinObjectTest and is intended
'''to contain all AbstractWinObjectTest Unit Tests
'''</summary>
<TestClass()> _
Public Class AbstractWinObjectTest
    'uses win object to fake it.

    Private testContextInstance As TestContext

    '''<summary>
    '''Gets or sets the test context which provides
    '''information about and functionality for the current test run.
    '''</summary>
    Public Property TestContext() As TestContext
        Get
            Return testContextInstance
        End Get
        Set(ByVal value As TestContext)
            testContextInstance = value
        End Set
    End Property

#Region "Additional test attributes"
    Private Shared p As System.Diagnostics.Process
    Private Shared target As WinObject
    Public Shared Sub CloseAll(Optional ByVal name As String = "calc")
        For Each pro As System.Diagnostics.Process In System.Diagnostics.Process.GetProcessesByName(name)
            pro.CloseMainWindow()
        Next
        System.Threading.Thread.Sleep(100)
    End Sub
    '
    'You can use the following additional attributes as you write your tests:
    '
    'Use ClassInitialize to run code before running the first test in the class
    <ClassInitialize()> _
    Public Shared Sub MyClassInitialize(ByVal testContext As TestContext)

    End Sub

    'Use ClassCleanup to run code after all tests in a class have run
    <ClassCleanup()> _
    Public Shared Sub MyClassCleanup()

    End Sub

    Public Shared Sub Init()
        p = Diagnostics.Process.Start("calc.exe")
        System.Threading.Thread.Sleep(2000)

        Dim a As UIControls.InterAct
        a = New UIControls.InterAct()
        target = a.Window("name:=""" & "SciCalc" & """")
        a = Nothing
    End Sub

    'Use TestInitialize to run code before running each test
    <TestInitialize()> _
    Public Sub MyTestInitialize()
        Init()
    End Sub

    'Use TestCleanup to run code after each test has run
    <TestCleanup()> _
    Public Sub MyTestCleanup()
        CloseAll()
    End Sub

    Private Sub AreEqualImages(ByVal bmp1 As Bitmap, ByVal bmp2 As Bitmap)
        If (bmp1 Is Nothing) Then
            Assert.Fail("bmp1 is nothing")
        End If
        If (bmp2 Is Nothing) Then
            Assert.Fail("bmp2 is nothing")
        End If
        If (bmp1.Width <> bmp2.Width) Then
            Assert.Fail("bitmaps don't have same width")
        End If
        If (bmp1.Height <> bmp2.Height) Then
            Assert.Fail("bitmaps don't have same height")
        End If
        For x As Int32 = 0 To bmp1.Width - 1
            For y As Int32 = 0 To bmp1.Height - 1
                If (bmp1.GetPixel(x, y) <> bmp2.GetPixel(x, y)) Then
                    Assert.Fail("Pixels are not equal: " & x & ", " & y)
                End If
            Next
        Next
    End Sub

    Private Sub SaveFileIfNeeded(ByVal bmp As Bitmap)
        Dim TestFile As String = GetImgPath()
        If (System.IO.File.Exists(TestFile) = False) Then
            bmp.Save(TestFile)
            Console.WriteLine("Saved BMP: " & TestFile)
        Else
            Console.WriteLine("BMP " & TestFile & " already saved.")
        End If

    End Sub

    Private Function Load() As Bitmap
        Dim TestFile As String = GetImgPath()
        Dim bmp As Bitmap
        If (System.IO.File.Exists(TestFile)) Then
            Dim fs As FileStream = New FileStream(TestFile, FileMode.Open, _
            FileAccess.Read, FileShare.ReadWrite)
            bmp = System.Drawing.Image.FromStream(fs)
            fs.Close()
            fs = Nothing
            Console.WriteLine("Loaded BMP: " & TestFile)
        Else
            Console.WriteLine("**FAILED TO Load BMP: " & TestFile)
            bmp = Nothing
        End If
        Return bmp
    End Function

    Public Function GetImgPath() As String
        Return TestContext.TestDir.Substring(0, TestContext.TestDir.IndexOf("Noob") - 1) & "\" & _
        TestContext.TestName & ".bmp"
    End Function

    '
#End Region


    '''<summary>
    '''A test for SmartClick
    '''</summary>
    <TestMethod()> _
    Public Sub SmartClickTest()
        target.Button("value:=""7""").SmartClick()
        Dim a As UIControls.InterAct
        a = New UIControls.InterAct()
        target = a.Window("name:=""" & "SciCalc" & """")
        a = Nothing
        Assert.IsTrue(target.TextBox("name:=""Edit""").GetText().Contains("7"))
    End Sub

    '''<summary>
    '''A test for RightClick
    '''</summary>
    <TestMethod()> _
    Public Sub RightClickTest1()
        Dim X As Integer = 10
        Dim Y As Integer = 10
        target.RightClick(X, Y)
        target.TypeKeys("{UP}")
        AbstractWinObject.TakePicturesAfterTyping = False
        target.TypeKeys("~")
        System.Threading.Thread.Sleep(1000)
        Assert.IsTrue(p.HasExited)
    End Sub

    '''<summary>
    '''A test for RightClick
    '''</summary>
    <TestMethod()> _
    Public Sub RightClickTest()
        target.RightClick()
        Assert.Inconclusive("Currently can't be verified, except for no exceptions.")
    End Sub

    '''<summary>
    '''A test for MoveMouseTo
    '''</summary>
    <TestMethod()> _
    Public Sub MoveMouseToTest1()
        Dim x As Integer = Windows.Forms.Cursor.Position.X
        Dim y As Integer = Windows.Forms.Cursor.Position.Y
        target.MoveMouseTo()
        Assert.AreNotEqual(x, Windows.Forms.Cursor.Position.X)
        Assert.AreNotEqual(y, Windows.Forms.Cursor.Position.Y)
    End Sub

    '''<summary>
    '''A test for MoveMouseTo
    '''</summary>
    <TestMethod()> _
    Public Sub MoveMouseToTest()
        Dim X As Integer = 10
        Dim Y As Integer = 10
        target.MoveMouseTo(X, Y)
        target.MoveMouseTo()
        Assert.AreNotEqual(X, Windows.Forms.Cursor.Position.X)
        Assert.AreNotEqual(Y, Windows.Forms.Cursor.Position.Y)
    End Sub

    '''<summary>
    '''A test for IsObjectAtAbsLocation
    '''</summary>
    <TestMethod()> _
    Public Sub IsObjectAtAbsLocationTest()
        Dim X As Integer = 1
        Dim Y As Integer = 1
        Dim expected As Boolean = False
        Dim actual As Boolean
        'this test could fail from time to time....
        actual = target.IsObjectAtAbsLocation(X, Y)
        Assert.AreEqual(expected, actual)
    End Sub

    '''<summary>
    '''A test for IsEnabled
    '''</summary>
    <TestMethod()> _
    Public Sub IsEnabledTest()
        Dim expected As Boolean = True
        Dim actual As Boolean
        actual = target.IsEnabled
        Assert.AreEqual(expected, actual)
    End Sub

    '''<summary>
    '''A test for GetStyleEx
    '''</summary>
    <TestMethod()> _
    Public Sub GetStyleExTest()
        Dim actual As Integer
        actual = target.GetStyleEx
        'Assert.Inconclusive("Currently can't be verified, except for no exceptions.")
        Assert.AreNotEqual(0, actual, "This only verifies a value is returned.")
    End Sub

    '''<summary>
    '''A test for GetStyle
    '''</summary>
    <TestMethod()> _
    Public Sub GetStyleTest()
        Dim actual As Integer
        actual = target.GetStyle
        Assert.AreNotEqual(0, actual, "This only verifies a value is returned.")
        actual = target.TextBox(Description.Create("name:=""Edit""")).GetStyle()
        Assert.AreNotEqual(0, actual, "This only verifies a value is returned.")
    End Sub

    '''<summary>
    '''A test for GetProcessName
    '''</summary>
    <TestMethod()> _
    Public Sub GetProcessNameTest()
        Dim expected As String = "calc"
        Dim actual As String
        actual = target.GetProcessName
        Assert.AreEqual(expected, actual)
    End Sub

    '''<summary>
    '''A test for GetObjectHwnds
    '''</summary>
    <TestMethod()> _
    Public Sub WinObjectHwndsTest()
        System.Threading.Thread.Sleep(1000) 'give more time to make sure windows closes other instance.
        Dim actual() As IntPtr
        actual = target.GetObjectHwnds
        Assert.AreEqual(1, actual.Count)
    End Sub

    '''<summary>
    '''A test for GetObjectCount
    '''</summary>
    <TestMethod()> _
    Public Sub WinObjectCountTest()
        System.Threading.Thread.Sleep(1000) 'give more time to make sure windows closes other instance.
        Dim expected As Integer = 1
        Dim actual As Integer
        actual = target.GetObjectCount
        Assert.AreEqual(expected, actual)
    End Sub

    '''<summary>
    '''A test for GetLocationRect
    '''</summary>
    <TestMethod()> _
    Public Sub GetLocationRectTest()
        Dim actual As Rectangle
        actual = target.GetLocationRect
        Assert.AreNotEqual(actual, Rectangle.Empty)
    End Sub

    '''<summary>
    '''A test for GetClientAreaRect
    '''</summary>
    <TestMethod()> _
    Public Sub GetClientAreaRectTest()
        Dim actual As Rectangle
        actual = target.GetClientAreaRect()
        Assert.AreNotEqual(actual, Rectangle.Empty)
    End Sub

    '''<summary>
    '''A test for GetControlID
    '''</summary>
    <TestMethod()> _
    Public Sub GetControlIDTest()
        Dim actual, expected As Integer
        expected = 0 'Since this is a window, it should always return 0
        actual = target.GetControlID()
        Assert.AreEqual(expected, actual)
    End Sub

    '''<summary>
    '''A test for GetLocation
    '''</summary>
    <TestMethod()> _
    Public Sub GetLocationTest()
        Dim actual As String
        actual = target.GetLocation
        Assert.AreNotEqual(Rectangle.Empty.ToString(), actual)
    End Sub

    '''<summary>
    '''A test for Exists
    '''</summary>
    <TestMethod()> _
    Public Sub ExistsTest1()
        Dim Time As Integer = 10
        Dim expected As Boolean = True
        Dim actual As Boolean
        actual = target.Exists(Time)
        Assert.AreEqual(expected, actual)
    End Sub

    '''<summary>
    '''A test for Exists
    '''</summary>
    <TestMethod()> _
    Public Sub ExistsTest()
        Dim expected As Boolean = True
        Dim actual As Boolean
        actual = target.Exists
        Assert.AreEqual(expected, actual)
    End Sub

    '''<summary>
    '''A test for DoubleClick
    '''</summary>
    <TestMethod()> _
    Public Sub DoubleClickTest1()
        Dim X As Integer = 10
        Dim Y As Integer = 10
        AbstractWinObject.TakePicturesAfterClicks = False
        System.Threading.Thread.Sleep(1900)
        target.DoubleClick(X, Y)
        System.Threading.Thread.Sleep(900)
        Assert.AreEqual(True, p.HasExited())
    End Sub



    '''<summary>
    '''A test for DoubleClick
    '''</summary>
    <TestMethod()> _
    Public Sub DoubleClickTest()
        System.Threading.Thread.Sleep(100)
        target.DoubleClick()
        System.Threading.Thread.Sleep(100)
        Assert.AreEqual("77. ", target.TextBox("Name:=""Edit""").GetText())
    End Sub

    '''<summary>
    '''A test for Click
    '''</summary>
    <TestMethod()> _
    Public Sub ClickTest1()
        Dim X As Integer = 10
        Dim Y As Integer = 10
        target.Click(X, Y)
        target.TypeKeys("{UP}")
        AbstractWinObject.TakePicturesAfterTyping = False
        target.TypeKeys("~")
        System.Threading.Thread.Sleep(10000)
        Assert.IsTrue(p.HasExited)
    End Sub

    '''<summary>
    '''A test for Click
    '''</summary>
    <TestMethod()> _
    Public Sub ClickTest()
        target.Click()
        Assert.AreEqual("7. ", target.TextBox("Name:=""Edit""").GetText())
    End Sub

    '''<summary>
    '''A test for CaptureImage
    '''</summary>
    <TestMethod()> _
    Public Sub CaptureImageTest1()
        Dim expected As Bitmap = Load()
        Dim FilePath As String = GetImgPath()
        Dim Rect As Rectangle = New Rectangle(10, 10, 10, 10)
        target.CaptureImage(FilePath, Rect)
        Dim actual As Bitmap = Load()
        AreEqualImages(expected, actual)
    End Sub

    '''<summary>
    '''A test for CaptureImage
    '''</summary>
    <TestMethod()> _
    Public Sub CaptureImageTest()
        Dim expected As Bitmap = Load()
        Dim FilePath As String = GetImgPath()
        If (System.IO.File.Exists(FilePath)) Then
            System.IO.File.Delete(FilePath)
        Else
            Console.WriteLine("Warning, test file was not found.  test likely to fail")
        End If

        target.CaptureImage(FilePath)
        Dim actual As Bitmap = Load()
        AreEqualImages(expected, actual)
    End Sub


End Class
