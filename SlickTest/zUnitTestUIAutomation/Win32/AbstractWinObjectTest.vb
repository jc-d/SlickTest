Imports System
Imports System.Drawing
Imports NUnit.Framework
Imports UIControls
Imports System.IO

'''<summary>
'''This is a test class for AbstractWinObjectTest and is intended
'''to contain all AbstractWinObjectTest Unit Tests
'''</summary>
<TestFixture()> _
Public Class AbstractWinObjectTest
    'uses win object to fake it.


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
    <TestFixtureSetUp()> _
    Public Shared Sub MyClassInitialize()

    End Sub

    'Use ClassCleanup to run code after all tests in a class have run
    <TestFixtureTearDown()> _
    Public Shared Sub MyClassCleanup()

    End Sub

    Public Shared Sub Init()
        Dim path As String = System.IO.Path.GetFullPath("..\..\..\zUnitTestUIAutomation\Programs\calc.exe")
        System.Console.WriteLine(path)
        p = Diagnostics.Process.Start(path)
        System.Threading.Thread.Sleep(2000)

        Dim a As UIControls.InterAct
        a = New UIControls.InterAct()
        target = a.Window("name:=""" & "SciCalc" & """")
        a = Nothing
    End Sub

    'Use TestInitialize to run code before running each test
    <SetUp()> _
    Public Sub MyTestInitialize()
        Init()
    End Sub

    'Use TestCleanup to run code after each test has run
    <TearDown()> _
    Public Sub MyTestCleanup()
        CloseAll()
    End Sub

    Private Sub AreEqualImages(ByVal bmp1 As Bitmap, ByVal bmp2 As Bitmap)
        If (bmp1 Is Nothing) Then
            Verify.Fail("bmp1 is nothing")
        End If
        If (bmp2 Is Nothing) Then
            Verify.Fail("bmp2 is nothing")
        End If
        If (bmp1.Width <> bmp2.Width) Then
            Verify.Fail("bitmaps don't have same width")
        End If
        If (bmp1.Height <> bmp2.Height) Then
            Verify.Fail("bitmaps don't have same height")
        End If
        For x As Int32 = 0 To bmp1.Width - 1
            For y As Int32 = 0 To bmp1.Height - 1
                If (bmp1.GetPixel(x, y) <> bmp2.GetPixel(x, y)) Then
                    Verify.Fail("Pixels are not equal: " & x & ", " & y)
                End If
            Next
        Next
    End Sub

    Private Sub SaveFileIfNeeded(ByVal bmp As Bitmap, ByVal method As String)
        Dim TestFile As String = GetImgPath(method)
        If (System.IO.File.Exists(TestFile) = False) Then
            bmp.Save(TestFile)
            Console.WriteLine("Saved BMP: " & TestFile)
        Else
            Console.WriteLine("BMP " & TestFile & " already saved.")
        End If

    End Sub

    Private Function Load(ByVal method As String) As Bitmap
        Dim TestFile As String = GetImgPath(method)
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

    Public Function GetImgPath(ByVal method As String) As String
        Return System.IO.Path.GetTempPath() & method & ".bmp"
    End Function

    '
#End Region


    '''<summary>
    '''A test for SmartClick
    '''</summary>
    <Test()> _
    Public Sub SmartClickTest()
        target.Button("value:=""7""").SmartClick()
        Dim a As UIControls.InterAct
        a = New UIControls.InterAct()
        target = a.Window("name:=""" & "SciCalc" & """")
        a = Nothing
        Verify.IsTrue(target.TextBox("name:=""Edit""").GetText().Contains("7"))
    End Sub

    '''<summary>
    '''A test for RightClick
    '''</summary>
    <Test()> _
    Public Sub RightClickTest1()
        Dim X As Integer = 10
        Dim Y As Integer = 10
        target.RightClick(X, Y)
        target.TypeKeys("{UP}")
        AbstractWinObject.TakePicturesAfterTyping = False
        target.TypeKeys("~")
        System.Threading.Thread.Sleep(1000)
        Verify.IsTrue(p.HasExited)
    End Sub

    '''<summary>
    '''A test for RightClick
    '''</summary>
    <Test()> _
    Public Sub RightClickTest()
        target.RightClick()
        Verify.IsNotEmpty("Currently can't be verified, except for no exceptions.")
    End Sub

    '''<summary>
    '''A test for MoveMouseTo
    '''</summary>
    <Test()> _
    Public Sub MoveMouseToTest1()
        Dim x As Integer = Windows.Forms.Cursor.Position.X
        Dim y As Integer = Windows.Forms.Cursor.Position.Y
        target.MoveMouseTo()
        Verify.AreNotEqual(x, Windows.Forms.Cursor.Position.X)
        Verify.AreNotEqual(y, Windows.Forms.Cursor.Position.Y)
    End Sub

    '''<summary>
    '''A test for MoveMouseTo
    '''</summary>
    <Test()> _
    Public Sub MoveMouseToTest()
        Dim X As Integer = 10
        Dim Y As Integer = 10
        target.MoveMouseTo(X, Y)
        target.MoveMouseTo()
        Verify.AreNotEqual(X, Windows.Forms.Cursor.Position.X)
        Verify.AreNotEqual(Y, Windows.Forms.Cursor.Position.Y)
    End Sub

    '''<summary>
    '''A test for IsObjectAtAbsLocation
    '''</summary>
    <Test()> _
    Public Sub IsObjectAtAbsLocationTest()
        Dim X As Integer = 1
        Dim Y As Integer = 1
        Dim expected As Boolean = False
        Dim actual As Boolean
        'this test could fail from time to time....
        actual = target.IsObjectAtAbsLocation(X, Y)
        Verify.AreEqual(expected, actual)
    End Sub

    '''<summary>
    '''A test for IsEnabled
    '''</summary>
    <Test()> _
    Public Sub IsEnabledTest()
        Dim expected As Boolean = True
        Dim actual As Boolean
        actual = target.IsEnabled
        Verify.AreEqual(expected, actual)
    End Sub

    '''<summary>
    '''A test for GetStyleEx
    '''</summary>
    <Test()> _
    Public Sub GetStyleExTest()
        Dim actual As Integer
        actual = target.GetStyleEx
        'Verify.Inconclusive("Currently can't be verified, except for no exceptions.")
        Verify.AreNotEqual(0, actual, "This only verifies a value is returned.")
    End Sub

    '''<summary>
    '''A test for GetStyle
    '''</summary>
    <Test()> _
    Public Sub GetStyleTest()
        Dim actual As Integer
        actual = target.GetStyle
        Verify.AreNotEqual(0, actual, "This only verifies a value is returned.")
        actual = target.TextBox(Description.Create("name:=""Edit""")).GetStyle()
        Verify.AreNotEqual(0, actual, "This only verifies a value is returned.")
    End Sub

    '''<summary>
    '''A test for GetProcessName
    '''</summary>
    <Test()> _
    Public Sub GetProcessNameTest()
        Dim expected As String = "calc"
        Dim actual As String
        actual = target.GetProcessName
        Verify.AreEqual(expected, actual)
    End Sub

    '''<summary>
    '''A test for GetObjectHwnds
    '''</summary>
    <Test()> _
    Public Sub WinObjectHwndsTest()
        System.Threading.Thread.Sleep(1000) 'give more time to make sure windows closes other instance.
        Dim actual() As IntPtr
        actual = target.GetObjectHwnds
        Verify.AreEqual(1, actual.Count)
    End Sub

    '''<summary>
    '''A test for GetObjectCount
    '''</summary>
    <Test()> _
    Public Sub WinObjectCountTest()
        System.Threading.Thread.Sleep(1000) 'give more time to make sure windows closes other instance.
        Dim expected As Integer = 1
        Dim actual As Integer
        actual = target.GetObjectCount
        Verify.AreEqual(expected, actual)
    End Sub

    '''<summary>
    '''A test for GetLocationRect
    '''</summary>
    <Test()> _
    Public Sub GetLocationRectTest()
        Dim actual As Rectangle
        actual = target.GetLocationRect
        Verify.AreNotEqual(actual, Rectangle.Empty)
    End Sub

    '''<summary>
    '''A test for GetClientAreaRect
    '''</summary>
    <Test()> _
    Public Sub GetClientAreaRectTest()
        Dim actual As Rectangle
        actual = target.GetClientAreaRect()
        Verify.AreNotEqual(actual, Rectangle.Empty)
    End Sub

    '''<summary>
    '''A test for GetControlID
    '''</summary>
    <Test()> _
    Public Sub GetControlIDTest()
        Dim actual, expected As Integer
        expected = 0 'Since this is a window, it should always return 0
        actual = target.GetControlID()
        Verify.AreEqual(expected, actual)
    End Sub

    '''<summary>
    '''A test for GetLocation
    '''</summary>
    <Test()> _
    Public Sub GetLocationTest()
        Dim actual As String
        actual = target.GetLocation
        Verify.AreNotEqual(Rectangle.Empty.ToString(), actual)
    End Sub

    '''<summary>
    '''A test for Exists
    '''</summary>
    <Test()> _
    Public Sub ExistsTest1()
        Dim Time As Integer = 10
        Dim expected As Boolean = True
        Dim actual As Boolean
        actual = target.Exists(Time)
        Verify.AreEqual(expected, actual)
    End Sub

    '''<summary>
    '''A test for Exists
    '''</summary>
    <Test()> _
    Public Sub ExistsTest()
        Dim expected As Boolean = True
        Dim actual As Boolean
        actual = target.Exists
        Verify.AreEqual(expected, actual)
    End Sub

    '''<summary>
    '''A test for DoubleClick
    '''</summary>
    <Test()> _
    Public Sub DoubleClickTest1()
        Dim X As Integer = 10
        Dim Y As Integer = 10
        AbstractWinObject.TakePicturesAfterClicks = False
        System.Threading.Thread.Sleep(1900)
        target.DoubleClick(X, Y)
        System.Threading.Thread.Sleep(900)
        Verify.AreEqual(True, p.HasExited())
    End Sub



    '''<summary>
    '''A test for DoubleClick
    '''</summary>
    <Test()> _
    Public Sub DoubleClickTest()
        System.Threading.Thread.Sleep(100)
        target.DoubleClick()
        System.Threading.Thread.Sleep(100)
        Verify.AreEqual("77. ", target.TextBox("Name:=""Edit""").GetText())
    End Sub

    '''<summary>
    '''A test for Click
    '''</summary>
    <Test()> _
    Public Sub ClickTest1()
        Dim X As Integer = 10
        Dim Y As Integer = 10
        target.Click(X, Y)
        target.TypeKeys("{UP}")
        AbstractWinObject.TakePicturesAfterTyping = False
        target.TypeKeys("~")
        System.Threading.Thread.Sleep(10000)
        Verify.IsTrue(p.HasExited)
    End Sub

    '''<summary>
    '''A test for Click
    '''</summary>
    <Test()> _
    Public Sub ClickTest()
        target.Click()
        Verify.AreEqual("7. ", target.TextBox("Name:=""Edit""").GetText())
    End Sub

    '''<summary>
    '''A test for Highlight
    '''</summary>
    <Test()> _
    Public Sub HighlightTest()
        Dim start As System.DateTime = DateTime.Now
        target.Highlight()
        Console.WriteLine("Time Taken in seconds: " & (DateTime.Now - start).Seconds)
        Verify.IsTrue((DateTime.Now - start).Seconds > 2)
    End Sub

    '''<summary>
    '''A test for CaptureImage
    '''</summary>
    <Test()> _
    Public Sub CaptureImageTest1()
        Dim method As String = "CaptureImageTest1"
        Dim expected As Bitmap = Load(method)
        Dim FilePath As String = GetImgPath(method)
        Dim Rect As Rectangle = New Rectangle(10, 10, 10, 10)
        target.CaptureImage(FilePath, Rect)
        Dim actual As Bitmap = Load(method)
        AreEqualImages(expected, actual)
    End Sub

    '''<summary>
    '''A test for CaptureImage
    '''</summary>
    <Test()> _
    Public Sub CaptureImageTest()
        Dim method As String = "CaptureImageTest"

        Dim expected As Bitmap = Load(method)
        Dim FilePath As String = GetImgPath(method)
        If (System.IO.File.Exists(FilePath)) Then
            System.IO.File.Delete(FilePath)
        Else
            Console.WriteLine("Warning, test file was not found.  test likely to fail")
        End If

        target.CaptureImage(FilePath)
        Dim actual As Bitmap = Load(method)
        AreEqualImages(expected, actual)
    End Sub


End Class
