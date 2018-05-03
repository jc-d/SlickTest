Imports NUnit.Framework
Imports UIControls

'''<summary>
'''This is a test class for WebLinkTest and is intended
'''to contain all WebLinkTest Unit Tests
'''</summary>
<TestFixture()> _
Public Class WebLinkTest
    Inherits WebTests

#Region "Additional test attributes"

    Public Shared target As WebLink

    'Use TestInitialize to run code before running each test
    <SetUp()> _
    Public Overrides Sub MyTestInitialize()
        MyBase.MyTestInitialize()
        Dim desc As Description = Description.Create()
        desc.Add(APIControls.Description.DescriptionData.WebID, "GoogleLink")
        target = WebBrowser.WebLink(desc)
    End Sub
#End Region

    '''<summary>
    '''A test for GetRev
    '''</summary>
    <Test()> _
    Public Sub GetRevTest()
        Dim expected As String = Nothing
        Dim actual As String
        actual = target.GetRev()
        Verify.AreEqual(expected, actual)
    End Sub

    '''<summary>
    '''A test for GetRel
    '''</summary>
    <Test()> _
    Public Sub GetRelTest()
        Dim expected As String = Nothing
        Dim actual As String
        actual = target.GetRel()
        Verify.AreEqual(expected, actual)
    End Sub

    '''<summary>
    '''A test for GetCharSet
    '''</summary>
    <Test()> _
    Public Sub GetCharSetTest()
        Dim expected As String = Nothing
        Dim actual As String
        actual = target.GetCharSet()
        Verify.AreEqual(expected, actual)
    End Sub

    '''<summary>
    '''A test for GetLinkType
    '''</summary>
    <Test()> _
    Public Sub GetLinkTypeTest()
        Dim expected As String = Nothing
        Dim actual As String
        actual = target.GetLinkType()
        Verify.AreEqual(expected, actual)
    End Sub

    '''<summary>
    '''A test for GetTarget
    '''</summary>
    <Test()> _
    Public Sub GetTargetTest()
        Dim expected As String = Nothing
        Dim actual As String
        actual = target.GetTarget()
        Verify.AreEqual(expected, actual)
    End Sub

    '''<summary>
    '''A test for GetHRef
    '''</summary>
    <Test()> _
    Public Sub GetHRefTest()
        Dim expected As String = "http://www.google.com/"
        Dim actual As String
        actual = target.GetHRef()
        Verify.AreEqual(expected, actual)
    End Sub

    '''<summary>
    '''A test for GetHRefLang
    '''</summary>
    <Test()> _
    Public Sub GetHRefLangTest()
        Dim expected As String = Nothing
        Dim actual As String
        actual = target.GetHRefLang()
        Verify.AreEqual(expected, actual)
    End Sub

    '''<summary>
    '''A test for GetMimeType
    '''</summary>
    <Test()> _
    Public Sub GetMimeTypeTest()
        Dim expected As String = "COM/ File"
        Dim actual As String
        actual = target.GetMimeType()
        Verify.AreEqual(expected, actual)
    End Sub

    '''<summary>
    '''A test for GetNameProp
    '''</summary>
    <Test()> _
    Public Sub GetNamePropTest()
        Dim expected As String = "www.google.com"
        Dim actual As String
        actual = target.GetNameProp()
        Verify.AreEqual(expected, actual)
    End Sub

    '''<summary>
    '''A test for GetPathName
    '''</summary>
    <Test()> _
    Public Sub GetPathNameTest()
        Dim expected As String = ""
        Dim actual As String
        actual = target.GetPathName()
        Verify.AreEqual(expected, actual)
    End Sub

    '''<summary>
    '''A test for GetProtocol
    '''</summary>
    <Test()> _
    Public Sub GetProtocolTest()
        Dim expected As String = "http:"
        Dim actual As String
        actual = target.GetProtocol()
        Verify.AreEqual(expected, actual)
    End Sub

    '''<summary>
    '''A test for GetProtocolLong
    '''</summary>
    <Test()> _
    Public Sub GetProtocolLongTest()
        Dim expected As String = "HyperText Transfer Protocol"
        Dim actual As String
        actual = target.GetProtocolLong()
        Verify.AreEqual(expected, actual)
    End Sub

End Class