Imports System

Imports System.Drawing

Imports Microsoft.VisualStudio.TestTools.UnitTesting

Imports UIControls



'''<summary>
'''This is a test class for CaptureTest and is intended
'''to contain all CaptureTest Unit Tests
'''</summary>
<TestClass()> _
Public Class CaptureTest


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
            testContextInstance = Value
        End Set
    End Property

#Region "Additional test attributes"
    Private Shared p As System.Diagnostics.Process
    Private Shared target As Window
    Private Shared hwnd As IntPtr
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
        hwnd = p.MainWindowHandle
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

    Private Sub AreSimilarImages(ByVal bmp1 As Bitmap, ByVal bmp2 As Bitmap)
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
        Dim FailMsg As New System.Text.StringBuilder(bmp1.Width * bmp1.Height)
        Dim bmDiff As Bitmap = Nothing

        bmDiff = New Bitmap(bmp1.Width, bmp1.Height)
        Dim eqColor As Color = Color.Black
        Dim diffColor As Color = Color.Red
        Dim HowCloseColorsAre As Integer = 4
        For x As Int32 = 0 To bmp1.Width - 1
            For y As Int32 = 0 To bmp1.Height - 1
                Dim samePixel As Boolean = False
                If (bmp1.Width > x And bmp1.Height > y And bmp2.Width > x And bmp2.Height > y) Then
                    Dim px1 As System.Drawing.Color = bmp1.GetPixel(x, y)
                    Dim px2 As System.Drawing.Color = bmp2.GetPixel(x, y)
                    If (ProximityPlusOrMinus(px1.A, px2.A, HowCloseColorsAre) And _
                        ProximityPlusOrMinus(px1.B, px2.B, HowCloseColorsAre) And _
                        ProximityPlusOrMinus(px1.R, px2.R, HowCloseColorsAre) And _
                        ProximityPlusOrMinus(px1.G, px2.G, HowCloseColorsAre)) Then

                        bmDiff.SetPixel(x, y, eqColor)
                        samePixel = True
                    End If
                End If
                If (samePixel = False) Then
                    bmDiff.SetPixel(x, y, diffColor)
                    If (FailMsg.Length < 2000) Then
                        FailMsg.AppendLine("Pixels are not equal at x " & x & ", y " & y & _
                                           " Color org=" & bmp1.GetPixel(x, y).ToString() & " Color comp = " & bmp2.GetPixel(x, y).ToString())
                    End If

                End If
            Next
        Next

        If (FailMsg.Length <> 0) Then
            Dim Filename As String = TestContext.TestDir.Substring(0, TestContext.TestDir.IndexOf(System.Windows.Forms.SystemInformation.UserName) - 1) & "\" & _
        TestContext.TestName & "_DiffImage.bmp"

            bmDiff.Save(Filename)
            bmDiff.Dispose()
        End If
        Assert.AreEqual("", FailMsg.ToString())
    End Sub

    Private Shared Function ProximityPlusOrMinus(ByVal Value1 As Integer, ByVal Value2 As Integer, ByVal HowClose As Integer) As Boolean
        ProximityPlusOrMinus = False
        If (Value1 + HowClose > Value2) Then
            If (Value2 >= Value1) Then
                ProximityPlusOrMinus = True
            End If
        End If
        If (Value1 - HowClose < Value2) Then
            If (Value2 <= Value1) Then
                ProximityPlusOrMinus = True
            End If
        End If
    End Function


    Private Sub SaveFileIfNeeded(ByVal bmp As Bitmap)
        Dim TestFile As String = TestContext.TestDir.Substring(0, TestContext.TestDir.IndexOf(System.Windows.Forms.SystemInformation.UserName) - 1) & "\" & _
        TestContext.TestName & ".bmp"
        If (System.IO.File.Exists(TestFile) = False) Then
            bmp.Save(TestFile)
            Console.WriteLine("Saved BMP: " & TestFile)
        Else
            If (System.IO.File.Exists(TestFile & "Current.bmp")) Then System.IO.File.Delete(TestFile & "Current.bmp")
            bmp.Save(TestFile & "Current.bmp")
            Console.WriteLine("BMP " & TestFile & " already saved.")
        End If

    End Sub

    Private Function Load() As Bitmap
        Dim TestFile As String = TestContext.TestDir.Substring(0, TestContext.TestDir.IndexOf("Noob") - 1) & "\" & _
        TestContext.TestName & ".bmp"
        Dim bmp As Bitmap
        If (System.IO.File.Exists(TestFile)) Then
            bmp = New Bitmap(TestFile)
            Console.WriteLine("Loaded BMP: " & TestFile)
        Else
            Console.WriteLine("**FAILED TO Load BMP: " & TestFile)
            bmp = Nothing
        End If
        Return bmp
    End Function

#End Region


    '''<summary>
    '''A test for Window
    '''</summary>
    <TestMethod()> _
    Public Sub WindowTest()

        Dim expected As Bitmap = Load()
        Dim actual As Bitmap
        actual = Capture.Window(hwnd)
        SaveFileIfNeeded(actual)
        AreSimilarImages(expected, actual)
    End Sub


    '''<summary>
    '''A test for ScreenRectangle
    '''</summary>
    <TestMethod()> _
    Public Sub ScreenRectangleTest()
        Dim imageRect As Rectangle = target.GetLocationRect()
        Dim expected As Bitmap = Load()
        Dim actual As Bitmap
        actual = Capture.ScreenRectangle(imageRect)
        SaveFileIfNeeded(actual)
        AreSimilarImages(expected, actual)
        'Assert.Inconclusive("Currently can't be verified, except for no exceptions.")
    End Sub

    '''<summary>
    '''A test for FullScreen
    '''</summary>
    <TestMethod()> _
    Public Sub FullScreenTest()
        Dim actual As Bitmap
        actual = Capture.FullScreen

        Assert.AreNotEqual(0, actual.Width)
    End Sub

    '''<summary>
    '''A test for Control
    '''</summary>
    <TestMethod()> _
    Public Sub ControlTest1()
        Dim expected As Bitmap = Load()
        Dim actual As Bitmap
        actual = Capture.Control(hwnd)
        SaveFileIfNeeded(actual)


        'Assert.Inconclusive("Currently can't be verified, except for no exceptions.")
    End Sub

    '''<summary>
    '''A test for Control
    '''</summary>
    <TestMethod()> _
    Public Sub ControlTest()
        Dim p As Point = target.GetLocationRect.Location ' TODO: Initialize to an appropriate value
        Dim expected As Bitmap = Load()
        Dim actual As Bitmap
        actual = Capture.Control(p)
        SaveFileIfNeeded(actual)
        AreSimilarImages(expected, actual)
        'Assert.Inconclusive("Currently can't be verified, except for no exceptions.")
    End Sub

    '''<summary>
    '''A test for ActiveWindow
    '''</summary>
    <TestMethod()> _
    Public Sub ActiveWindowTest()
        Dim expected As Bitmap = Load()
        target.AppActivateByHwnd(hwnd)
        Dim actual As Bitmap
        actual = Capture.ActiveWindow
        SaveFileIfNeeded(actual)
        AreSimilarImages(expected, actual)
        'Assert.Inconclusive("Currently can't be verified, except for no exceptions.")
    End Sub

End Class
