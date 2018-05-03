Imports NUnit.Framework
Imports UIControls

'''<summary>
'''This is a test class for WebImageTest and is intended
'''to contain all WebImageTest Unit Tests
'''</summary>
<TestFixture()> _
Public Class WebImageTest
    Inherits WebTests

#Region "Additional test attributes"

    Public Shared target As WebImage

    'Use TestInitialize to run code before running each test
    <SetUp()> _
    Public Overrides Sub MyTestInitialize()
        Me.SiteUrl = System.IO.Path.GetFullPath(".\WebPages\MiscSample.html")
        MyBase.MyTestInitialize()
        System.Threading.Thread.Sleep(500) 'render time seems to take a while
        Dim desc As Description = Description.Create()
        desc.Add(APIControls.Description.DescriptionData.WebID, "image")
        target = WebBrowser.WebImage(desc)
    End Sub

#End Region

    '''<summary>
    '''A test for VSpace
    '''</summary>
    <Test()> _
    Public Sub VSpaceTest()
        Dim expected As Integer = 100
        Dim actual As Integer
        actual = target.GetVSpace()
        Verify.AreEqual(expected, actual)
    End Sub

    ''''<summary>
    ''''A test for Vrml
    ''''</summary>
    '<Test()> _
    'Public Sub VrmlTest()
    '    Dim expected As String = String.Empty ' TODO: Initialize to an appropriate value
    '    Dim actual As String
    '    actual = target.GetVrml()
    '    Verify.AreEqual(expected, actual)
    'End Sub

    '''<summary>
    '''A test for UseMap
    '''</summary>
    <Test()> _
    Public Sub UseMapTest()
        Dim expected As String = "BackGround.png"
        Dim actual As String
        actual = target.GetUseMap()
        Verify.AreEqual(expected, actual)
    End Sub

    '''<summary>
    '''A test for Src
    '''</summary>
    <Test()> _
    Public Sub SrcTest()
        Dim expected As String = "WebPages/BackGround.png"
        Dim actual As String
        actual = target.GetSrc()
        Verify.Contains(expected, actual)
    End Sub

    '''<summary>
    '''A test for MimeType
    '''</summary>
    <Test()> _
    Public Sub MimeTypeTest()

        Dim expected As String = "png".ToUpper()
        Dim actual As String
        actual = target.GetMimeType()
        Verify.Contains(expected, actual.ToUpper())
    End Sub

    '''<summary>
    '''A test for HSpace
    '''</summary>
    <Test()> _
    Public Sub HSpaceTest()
        Dim expected As Integer = 100
        Dim actual As Integer
        actual = target.GetHSpace()
        Verify.AreEqual(expected, actual)
    End Sub

    '''<summary>
    '''A test for GetAlign
    '''</summary>
    <Test()> _
    Public Sub GetAlignTest()
        Dim expected As String = "top"
        Dim actual As String
        actual = target.GetAlign()
        Verify.AreEqual(expected, actual)
    End Sub

    '''<summary>
    '''A test for FileSize
    '''</summary>
    <Test()> _
    Public Sub FileSizeTest()

        Dim expected As String = String.Empty
        Dim actual As String
        actual = target.GetFileSize()
        Verify.AreNotEqual(expected, actual)
    End Sub

    '''<summary>
    '''A test for FileModifiedDate
    '''</summary>
    <Test()> _
    Public Sub FileModifiedDateTest()

        Dim expected As String = String.Empty ' TODO: Initialize to an appropriate value
        Dim actual As String
        actual = target.GetFileModifiedDate()
        Verify.AreNotEqual(expected, actual)
    End Sub

    '''<summary>
    '''A test for FileCreatedDate
    '''</summary>
    <Test()> _
    Public Sub FileCreatedDateTest()

        Dim expected As String = String.Empty
        Dim actual As String
        actual = target.GetFileCreatedDate()
        Verify.AreNotEqual(expected, actual)
    End Sub

    '''<summary>
    '''A test for Alt
    '''</summary>
    <Test()> _
    Public Sub AltTest()

        Dim expected As String = "alt text"
        Dim actual As String
        actual = target.GetAlt()
        Verify.AreEqual(expected, actual)
    End Sub
End Class
