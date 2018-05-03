Imports System
Imports System.IO
Imports System.Text
Imports System.Drawing
Imports winAPI.API

''' <summary>
''' Allows the user to create and compare screenshots in various ways.
''' </summary>
''' <remarks></remarks>
Public Class Screenshot
#Const IsAbs = 2 'set to 1 for abs position values
    Private image As System.Drawing.Image
    Private PixelDiff As Double = 0
    Private InternalImageType As System.Drawing.Imaging.ImageFormat

#Region "Private Methods"

    Private Function CreateBitMap(ByVal FilePath As String, Optional ByVal X As Integer = 0, _
    Optional ByVal Y As Integer = 0, Optional ByVal Width As Integer = 0, Optional ByVal Height As Integer = 0, _
    Optional ByVal overWrite As Boolean = False, Optional ByVal tmpHwnd As Int64 = 0) As Boolean
        Dim results As Boolean = False
        Dim hwnd As IntPtr = New IntPtr(tmpHwnd)

        If (Width > System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Width) Then
            Width = System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Width
        End If
        If (Height > System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Width) Then
            Height = System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Width
        End If

#If IsAbs = 1 Then
        X = InterAction.Mouse.AbsToRelativeCoordX(X)
        Y = InterAction.Mouse.AbsToRelativeCoordX(Y)
        Height = InterAction.Mouse.AbsToRelativeCoordX(Height)
        Width = InterAction.Mouse.AbsToRelativeCoordX(Width)
#End If
        If (System.IO.Path.GetFileName(FilePath) = System.IO.Path.GetFileNameWithoutExtension(FilePath)) Then
            FilePath += ".bmp"
        End If
        If (hwnd = IntPtr.Zero) Then
            If (Width = 0 And Height = 0) Then
                image = Capture.ActiveWindow()
            Else
                If (Width = 0) Then Return False
                If (Height = 0) Then Return False
                image = Capture.ScreenRectangle(New System.Drawing.Rectangle(X, Y, Width, Height))
            End If
        Else
            image = Capture.Control(hwnd)
            If (Width <> 0 And Height <> 0) Then image = Crop(X, Y, Height, Width)
        End If

        If (System.IO.File.Exists(FilePath) = False Or overWrite = True) Then
            Try
                If (System.IO.File.Exists(FilePath)) Then System.IO.File.Delete(FilePath)
                Select Case System.IO.Path.GetExtension(FilePath).ToLower
                    Case "png"
                        image.Save(FilePath, System.Drawing.Imaging.ImageFormat.Png)
                    Case "bmp"
                        image.Save(FilePath, System.Drawing.Imaging.ImageFormat.Bmp)
                    Case "jpg", "jpeg"
                        image.Save(FilePath, System.Drawing.Imaging.ImageFormat.Jpeg)
                    Case "tif"
                        image.Save(FilePath, System.Drawing.Imaging.ImageFormat.Tiff)
                    Case "wmf"
                        image.Save(FilePath, System.Drawing.Imaging.ImageFormat.Wmf)
                    Case Else
                        image.Save(FilePath, ImageType)
                End Select
            Catch ex As Exception
                Return False
            End Try
            Return True
        End If
        Return False
    End Function

    Private Function Crop(ByVal X As Integer, ByVal Y As Integer, ByVal Height As Integer, ByVal Width As Integer) As Bitmap
        Dim bmpImage As Bitmap
        Dim recCrop As Rectangle
        Dim bmpCrop As Bitmap
        Dim gphCrop As Graphics
        Dim recDest As Rectangle
        Try
            bmpImage = CType(image, System.Drawing.Bitmap)
            recCrop = New Rectangle(X, Y, Width, Height)
            bmpCrop = New Bitmap(recCrop.Width, recCrop.Height, bmpImage.PixelFormat)
            gphCrop = Graphics.FromImage(bmpCrop)
            recDest = New Rectangle(X, Y, Width, Height)
            gphCrop.DrawImage(bmpImage, recDest, recCrop.X, recCrop.Y, recCrop.Width, _
                recCrop.Height, GraphicsUnit.Pixel)
        Catch er As Exception
            Throw er
        End Try
        Return bmpCrop
    End Function

    Private Function SaveToDisk(ByVal directory As String, ByVal fileName As String, ByVal image As Drawing.Image) As String
        Dim stamp As DateTime = DateTime.Now

        If (Not image Is Nothing) Then
            'Create directory if it does not exist
            If Not System.IO.Directory.Exists(directory) Then
                System.IO.Directory.CreateDirectory(directory)
            End If
            image.Save(BuildFilePath(directory, fileName, stamp))
        Else
            Throw New APIControls.SlickTestAPIException("Image was Nothing.  Image may not have been captured.")
        End If

        Return stamp.Ticks.ToString()
    End Function

    Private Function BuildFilePath(ByVal dir As String, ByVal fileNameSeed As String, ByVal timeStamp As DateTime) As String
        Dim fileName As StringBuilder = Nothing

        fileName = New StringBuilder(101)
        fileName.Append(dir)
        If Not dir.EndsWith("\") And Not dir.EndsWith("/") Then
            fileName.Append("\")
        End If
        fileName.Append(fileNameSeed)
        fileName.Append("_")
        fileName.Append(timeStamp.Ticks.ToString())
        fileName.Append(".bmp")
        Return fileName.ToString()
    End Function

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

#End Region

    ''' <summary>
    ''' Creates a new instance of screenshot.
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub New()
        ImageType = System.Drawing.Imaging.ImageFormat.Bmp
    End Sub

    ''' <summary>
    ''' The image type to save if the file extension isn't provided or is not
    ''' supported.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property ImageType() As System.Drawing.Imaging.ImageFormat
        Get
            Return InternalImageType
        End Get
        Set(ByVal value As System.Drawing.Imaging.ImageFormat)
            InternalImageType = value
        End Set
    End Property

    ''' <summary>
    ''' Provides the percentage of pixels different between the last two different
    ''' images.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks>A value of 1 means there is no difference.  A value of 0 means that 100% of the pixels are
    ''' different.  A value of .5 means that 50% of the pixels are different.</remarks>
    Public ReadOnly Property PercentPixelDifference() As Double
        Get
            If (PixelDiff <> 0) Then
                Return (1 - PixelDiff)
            End If
            Return 1
        End Get
    End Property

#Region "Capture Image"

    ''' <summary>
    ''' Takes a screenshot of the top window (the front most window with focus).
    ''' </summary>
    ''' <param name="FilePath">The file path used to save the screenshot.</param>
    ''' <returns>Returns true if the image is successfully captured.</returns>
    ''' <remarks></remarks>
    Public Function TopWindow(ByVal FilePath As String) As Boolean
        Return CreateBitMap(FilePath, , , , , True)
    End Function

    ''' <summary>
    ''' Takes a screenshot of the top window (the front most window with focus).
    ''' This will only capture the section of the screen provided.
    ''' </summary>
    ''' <param name="FilePath">The file path used to save the screenshot.</param>
    ''' <param name="ScreenSection">The section of the screen that will have an image taken.</param>
    ''' <returns>Returns true if the image is successfully captured.</returns>
    ''' <remarks></remarks>
    Public Function TopWindow(ByVal FilePath As String, ByVal ScreenSection As System.Drawing.Rectangle) As Boolean
        Return CreateBitMap(FilePath, ScreenSection.X, ScreenSection.Y, ScreenSection.Width, ScreenSection.Height, True)
    End Function

    ''' <summary>
    ''' Takes a screenshot of a particular control or window based upon the hwnd provided.
    ''' </summary>
    ''' <param name="FilePath">The file path used to save the screenshot.</param>
    ''' <param name="hwnd">The handle of the object that you wisth to capture.</param>
    ''' <returns>Returns true if the image is successfully captured.</returns>
    ''' <remarks></remarks>
    Public Function CaptureControl(ByVal FilePath As String, ByVal hwnd As Int64) As Boolean
        Return CreateBitMap(FilePath, , , , , True, hwnd)
    End Function

    ''' <summary>
    ''' Takes a screenshot of a particular control or window based upon the hwnd provided.
    ''' This will only capture the section of the screen provided.
    ''' </summary>
    ''' <param name="FilePath">The file path used to save the screenshot.</param>
    ''' <param name="ScreenSection">The section of the screen that will have an image taken.</param>
    ''' <param name="hwnd">The handle of the object that you wisth to capture.</param>
    ''' <returns>Returns true if the image is successfully captured.</returns>
    ''' <remarks></remarks>
    Public Function CaptureControl(ByVal FilePath As String, ByVal ScreenSection As System.Drawing.Rectangle, ByVal hwnd As Int64) As Boolean
        Return CreateBitMap(FilePath, ScreenSection.X, ScreenSection.Y, ScreenSection.Width, ScreenSection.Height, True, hwnd)
    End Function

    ''' <summary>
    ''' Take a screenshot of the entire desktop.
    ''' </summary>
    ''' <param name="FilePath">The file path used to save the screenshot.</param>
    ''' <remarks></remarks>
    Public Sub CaptureDesktop(ByVal FilePath As String)
        image = Capture.FullScreen()
        image.Save(FilePath)
    End Sub

#End Region

    ''' <summary>
    ''' Captures a specific section of the screen given by a rectangle.
    ''' </summary>
    ''' <param name="FilePath">The file path used to save the screenshot.</param>
    ''' <param name="ScreenSection">The section of the screen that will have an image taken.</param>
    ''' <returns>Returns true if the image is successfully captured.</returns>
    ''' <remarks></remarks>
    Public Function CaptureScreenArea(ByVal FilePath As String, ByVal ScreenSection As System.Drawing.Rectangle) As Boolean
        Return CreateBitMap(FilePath, ScreenSection.X, ScreenSection.Y, ScreenSection.Width, ScreenSection.Height, True)
    End Function

    ''' <summary>
    ''' Compares two bitmaps to see if they are the same.
    ''' </summary>
    ''' <param name="Bitmap1Path">File path for the first bitmap</param>
    ''' <param name="Bitmap2Path">File path for the second bitmap</param>
    ''' <param name="FailIfDiffSize">By setting this to false, you can just 
    ''' compare the bitmap area that are in common.  The comparsion will start
    ''' in the top-left.</param>
    ''' <param name="SaveFileOnFail">Saves a file on fail showing where the bitmaps
    ''' are different.</param>
    ''' <returns>Returns true if Bitmaps are the same</returns>
    ''' <remarks></remarks>
    Public Function CompareImages(ByVal Bitmap1Path As String, ByVal Bitmap2Path As String, Optional ByVal SaveFileOnFail As Boolean = True, Optional ByVal FailIfDiffSize As Boolean = True) As Boolean
        Return CompareImagesByColorSimilarity(Bitmap1Path, Bitmap2Path, 0, SaveFileOnFail, FailIfDiffSize)
    End Function

    ''' <summary>
    ''' Compares two bitmaps to see if they are the same.  This allows for a certain amount of
    ''' differences based upon the percentage difference.
    ''' </summary>
    ''' <param name="bitmap1Path"></param>
    ''' <param name="bitmap2Path"></param>
    ''' <param name="saveFileOnFail"></param>
    ''' <returns>Returns the percentage difference between the two files.</returns>
    ''' <remarks></remarks>
    Public Function CompareImagesByPercentDiff(ByVal bitmap1Path As String, ByVal bitmap2Path As String, Optional ByVal saveFileOnFail As Boolean = False) As Double
        CompareImages(bitmap1Path, bitmap2Path, saveFileOnFail, False)
        Return PixelDiff
    End Function

    ''' <summary>
    ''' Compares two bitmaps to see if the pixels color groups are within N shades.
    ''' For example:<p/> Color [A=255, R=11, G=37, B=107]<p/> Color [A=255, R=10, G=36, B=108]
    ''' Would be considered the same if you said within 2 shades. 
    ''' </summary>
    ''' <param name="Bitmap1Path">File path for the first bitmap</param>
    ''' <param name="Bitmap2Path">File path for the second bitmap</param>
    ''' <param name="HowCloseColorsAre">A positive number in which the </param>
    ''' <param name="FailIfDiffSize">By setting this to false, you can just 
    ''' compare the bitmap area that are in common.  The comparsion will start
    ''' in the top-left.</param>
    ''' <param name="SaveFileOnFail">Saves a file on fail showing where the bitmaps
    ''' are different.</param>
    ''' <returns>Returns true if Bitmaps are the same</returns>
    ''' <remarks></remarks>
    Public Function CompareImagesByColorSimilarity(ByVal Bitmap1Path As String, _
                                                   ByVal Bitmap2Path As String, _
                                                   ByVal HowCloseColorsAre As Integer, _
                                                   Optional ByVal SaveFileOnFail As Boolean = True, _
                                                   Optional ByVal FailIfDiffSize As Boolean = True) As Boolean

        If (HowCloseColorsAre < 0) Then
            Throw New ArgumentException("Color similarity must be greater than or equal to 0.")
        End If
        Dim isSame As Boolean = True  'We assume true, and set false if we find a difference
        If (System.IO.File.Exists(Bitmap1Path) = False Or System.IO.File.Exists(Bitmap2Path) = False) Then
            Return False
            'Throw New Exception("Error, Either file " & bitmap1Path & " or " & bitmap2Path & " does not exist.")
        End If
        Dim bm1 As Bitmap = CType(Bitmap.FromFile(Bitmap1Path), Bitmap)
        Dim bm2 As Bitmap = CType(Bitmap.FromFile(Bitmap2Path), Bitmap)
        Dim bmDiff As Bitmap = Nothing

        Dim wid As Integer = 0, hgt As Integer = 0
        If (FailIfDiffSize = True) Then
            If (bm1.Width = bm2.Width And bm1.Height = bm2.Height) Then
                wid = bm1.Width
                hgt = bm1.Height
            Else
                wid = Math.Max(bm1.Width, bm2.Width)
                hgt = Math.Max(bm1.Height, bm2.Height)
                isSame = False
            End If
        End If
        wid = Math.Min(bm1.Width, bm2.Width)
        hgt = Math.Min(bm1.Height, bm2.Height)
        bmDiff = New Bitmap(wid, hgt)
        Dim eqColor As Color = Color.Black
        Dim diffColor As Color = Color.Red
        Dim x As Integer
        Dim y As Integer
        Dim difPixels As Integer = 0
        Dim samePixels As Integer = 0
        If (HowCloseColorsAre = 0) Then 'speeds up code for little duplication.
            For x = 0 To wid - 1
                For y = 0 To hgt - 1
                    If (bm1.Width > x And bm1.Height > y And bm2.Width > x And bm2.Height > y _
                     And bm1.GetPixel(x, y).Equals(bm2.GetPixel(x, y))) Then
                        bmDiff.SetPixel(x, y, eqColor)
                        samePixels += 1
                    Else
                        bmDiff.SetPixel(x, y, diffColor)
                        difPixels += 1
                        isSame = False
                    End If
                Next
            Next
        Else
            For x = 0 To wid - 1
                For y = 0 To hgt - 1
                    Dim samePixel As Boolean = False
                    If (bm1.Width > x And bm1.Height > y And bm2.Width > x And bm2.Height > y) Then
                        Dim px1 As System.Drawing.Color = bm1.GetPixel(x, y)
                        Dim px2 As System.Drawing.Color = bm2.GetPixel(x, y)
                        If (ProximityPlusOrMinus(px1.A, px2.A, HowCloseColorsAre) And _
                            ProximityPlusOrMinus(px1.B, px2.B, HowCloseColorsAre) And _
                            ProximityPlusOrMinus(px1.R, px2.R, HowCloseColorsAre) And _
                            ProximityPlusOrMinus(px1.G, px2.G, HowCloseColorsAre)) Then

                            bmDiff.SetPixel(x, y, eqColor)
                            samePixels += 1
                            samePixel = True
                        End If
                    End If
                    If (samePixel = False) Then
                        bmDiff.SetPixel(x, y, diffColor)
                        difPixels += 1
                        isSame = False
                    End If
                Next
            Next
        End If
        PixelDiff = samePixels / (samePixels + difPixels)
        If (isSame = False) Then
            If (SaveFileOnFail = True) Then
                Dim tmp As String = System.IO.Path.GetDirectoryName(Bitmap1Path)
                If (tmp.EndsWith("\") = False) Then tmp += "\"
                bmDiff.Save(tmp & _
                System.IO.Path.GetFileNameWithoutExtension(Bitmap1Path) & _
                "_error_" & DateTime.Now.Ticks.ToString() & ".bmp")
            End If
        End If
        bm1.Dispose()
        bm2.Dispose()
        Try
            bmDiff.Dispose()
        Catch ex As Exception
        End Try

        Return isSame
    End Function

End Class
