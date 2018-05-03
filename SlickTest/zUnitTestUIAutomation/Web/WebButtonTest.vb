Imports NUnit.Framework
Imports UIControls

'''<summary>
'''This is a test class for WebButtonTest and is intended
'''to contain all WebButtonTest Unit Tests
'''</summary>
<TestFixture()> _
Public Class WebButtonTest
    Inherits WebTests

#Region "Additional test attributes"

    Public Shared target As WebButton

    'Use TestInitialize to run code before running each test
    <SetUp()> _
    Public Overrides Sub MyTestInitialize()
        Me.SiteUrl = System.IO.Path.GetFullPath(".\WebPages\MiscSample.html")
        MyBase.MyTestInitialize()
        System.Threading.Thread.Sleep(500) 'render time seems to take a while
        Dim desc As Description = Description.Create()
        desc.Add(APIControls.Description.DescriptionData.WebID, "submit")
        target = WebBrowser.WebButton(desc)
    End Sub
#End Region

    '''<summary>
    '''A test for GetButtonType
    '''</summary>
    <Test()> _
    Public Sub GetButtonTypeTest()
        Dim expected As String = "submit"
        Dim actual As String
        actual = target.GetButtonType()
        Verify.AreEqual(expected, actual)
    End Sub
End Class