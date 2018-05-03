Imports NUnit.Framework
Imports UIControls

'''<summary>
'''This is a test class for WebGenericInputTest and is intended
'''to contain all WebGenericInputTest Unit Tests
'''</summary>
<TestFixture()> _
Public Class WebGenericInputTest
    Inherits WebTests

#Region "Additional test attributes"

    Public ReadOnly Property target() As WebGenericInput
        Get
            Return WebBrowser.WebGenericInput(desc)
        End Get
    End Property
    Public Shared desc As Description = Description.Create()

    Private Sub SetDescriptionId(ByVal id As String)
        desc.Add(APIControls.Description.DescriptionData.WebID, id)
    End Sub

    'Use TestInitialize to run code before running each test
    <SetUp()> _
    Public Overrides Sub MyTestInitialize()
        Me.SiteUrl = System.IO.Path.GetFullPath(".\WebPages\InputSample.html")
        MyBase.MyTestInitialize()
        desc.Reset()
    End Sub
#End Region

    ''''<summary>
    ''''A test for GetBorder
    ''''</summary>
    '<Test()> _
    'Public Sub GetBorderTest()
    '    SetDescriptionId("text")
    '    Dim expected As Object = ""
    '    Dim actual As Object
    '    actual = target.GetBorder()
    '    Verify.AreEqual(expected, actual)
    'End Sub

    '''<summary>
    '''A test for GetAlign
    '''</summary>
    <Test()> _
    Public Sub GetAlignTest()
        SetDescriptionId("image")
        Dim expected As String = "top"
        Dim actual As String
        actual = target.GetAlign()
        Verify.AreEqual(expected, actual)
    End Sub

    '''<summary>
    '''A test for GetMaxLength
    '''</summary>
    <Test()> _
    Public Sub GetMaxLengthTest()
        SetDescriptionId("text")
        Dim expected As Integer = 25
        Dim actual As Integer
        actual = target.GetMaxLength()
        Verify.AreEqual(expected, actual)
    End Sub

    '''<summary>
    '''A test for GetAlt
    '''</summary>
    <Test()> _
    Public Sub GetAltTest()
        SetDescriptionId("image")
        Dim expected As String = "alt text"
        Dim actual As String
        actual = target.GetAlt()
        Verify.AreEqual(expected, actual)
    End Sub

    ''''<summary>
    ''''A test for GetDynSrc
    ''''</summary>
    '<Test()> _
    'Public Sub GetDynSrcTest()
    '    Dim expected As String = ""
    '    Dim actual As String
    '    actual = target.GetDynSrc()
    '    Verify.AreEqual(expected, actual)
    'End Sub

    '''<summary>
    '''A test for GetHSpace
    '''</summary>
    <Test()> _
    Public Sub GetHSpaceTest()
        SetDescriptionId("image")
        Dim expected As Integer = 100
        Dim actual As Integer
        actual = target.GetHSpace()
        Verify.AreEqual(expected, actual)
    End Sub

    ''''<summary>
    ''''A test for GetLowSrc
    ''''</summary>
    '<Test()> _
    'Public Sub GetLowSrcTest()
    '    Dim expected As String = ""
    '    Dim actual As String
    '    actual = target.GetLowSrc()
    '    Verify.AreEqual(expected, actual)
    'End Sub

    '''<summary>
    '''A test for GetSize
    '''</summary>
    <Test()> _
    Public Sub GetSizeTest()
        SetDescriptionId("text")
        Dim expected As Integer = 10
        Dim actual As Integer
        actual = target.GetSize()
        Verify.AreEqual(expected, actual)
    End Sub

    '''<summary>
    '''A test for GetSrc
    '''</summary>
    <Test()> _
    Public Sub GetSrcTest()
        SetDescriptionId("image")
        Dim expected As String = "WebPages/BackGround.png"
        Dim actual As String
        actual = target.GetSrc()
        Verify.Contains(expected, actual)
    End Sub

    ''''<summary>
    ''''A test for GetStart
    ''''</summary>
    '<Test()> _
    'Public Sub GetStartTest()
    '    Dim expected As String = ""
    '    Dim actual As String
    '    actual = target.GetStart()
    '    Verify.AreEqual(expected, actual)
    'End Sub

    ''''<summary>
    ''''A test for GetStatus
    ''''</summary>
    '<Test()> _
    'Public Sub GetStatusTest()
    '    Dim expected As Boolean = False
    '    Dim actual As Boolean
    '    actual = target.GetStatus()
    '    Verify.AreEqual(expected, actual)
    'End Sub

    ''''<summary>
    ''''A test for GetUseMap
    ''''</summary>
    '<Test()> _
    'Public Sub GetUseMapTest()
    '    Dim expected As String = ""
    '    Dim actual As String
    '    actual = target.GetUseMap()
    '    Verify.AreEqual(expected, actual)
    'End Sub

    ''''<summary>
    ''''A test for GetVrml
    ''''</summary>
    '<Test()> _
    'Public Sub GetVrmlTest()
    '    Dim expected As String = ""
    '    Dim actual As String
    '    actual = target.GetVrml()
    '    Verify.AreEqual(expected, actual)
    'End Sub

    '''<summary>
    '''A test for GetVSpace
    '''</summary>
    <Test()> _
    Public Sub GetVSpaceTest()
        SetDescriptionId("image")
        Dim expected As Integer = 100
        Dim actual As Integer
        actual = target.GetVSpace()
        Verify.AreEqual(expected, actual)
    End Sub

    '''<summary>
    '''A test for IsImage
    '''</summary>
    <Test()> _
    Public Sub IsImageTest()
        SetDescriptionId("image")
        Dim expected As Boolean = True
        Dim actual As Boolean
        actual = WebGenericInput.IsImage(target)
        Verify.AreEqual(expected, actual)
    End Sub

    '''<summary>
    '''A test for IsButton
    '''</summary>
    <Test()> _
    Public Sub IsButtonTest()
        SetDescriptionId("button")
        Dim expected As Boolean = True
        Dim actual As Boolean
        actual = WebGenericInput.IsButton(target)
        Verify.AreEqual(expected, actual)
    End Sub

    '''<summary>
    '''A test for IsFileUpload
    '''</summary>
    <Test()> _
    Public Sub IsFileUploadTest()
        SetDescriptionId("file")
        Dim expected As Boolean = True
        Dim actual As Boolean
        actual = WebGenericInput.IsFileUpload(target)
        Verify.AreEqual(expected, actual)
    End Sub

    '''<summary>
    '''A test for IsCheckBox
    '''</summary>
    <Test()> _
    Public Sub IsCheckBoxTest()
        SetDescriptionId("checkbox")
        Dim expected As Boolean = True
        Dim actual As Boolean
        actual = WebGenericInput.IsCheckBox(target)
        Verify.AreEqual(expected, actual)
    End Sub

    '''<summary>
    '''A test for IsRadioButton
    '''</summary>
    <Test()> _
    Public Sub IsRadioButtonTest()
        SetDescriptionId("radio")
        Dim expected As Boolean = True
        Dim actual As Boolean
        actual = WebGenericInput.IsRadioButton(target)
        Verify.AreEqual(expected, actual)
    End Sub

    '''<summary>
    '''A test for IsTextField
    '''</summary>
    <Test()> _
    Public Sub IsTextFieldTest()
        SetDescriptionId("text")
        Dim expected As Boolean = True
        Dim actual As Boolean
        actual = WebGenericInput.IsTextField(target)
        Verify.AreEqual(expected, actual)
    End Sub

    '''<summary>
    '''A test for IsPasswordTextField
    '''</summary>
    <Test()> _
    Public Sub IsPasswordTextFieldTest()
        SetDescriptionId("password")
        Dim expected As Boolean = True
        Dim actual As Boolean
        actual = WebGenericInput.IsPasswordTextField(target)
        Verify.AreEqual(expected, actual)
    End Sub
End Class
