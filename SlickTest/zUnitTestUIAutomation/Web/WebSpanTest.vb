Imports NUnit.Framework
Imports UIControls

'''<summary>
'''This is a test class for WebSpanTest and is intended
'''to contain all WebSpanTest Unit Tests
'''</summary>
<TestFixture()> _
Public Class WebSpanTest
    Inherits WebTests

#Region "Additional test attributes"

    Public Shared target As WebSpan

    'Use TestInitialize to run code before running each test
    <SetUp()> _
    Public Overrides Sub MyTestInitialize()
        MyBase.MyTestInitialize()
        Dim desc As Description = Description.Create()
        desc.Add(APIControls.Description.DescriptionData.WebID, "SpanTest")
        target = WebBrowser.WebSpan(desc)
    End Sub
#End Region

    '''<summary>
    '''A test for GetTitle
    '''</summary>
    <Test()> _
    Public Sub GetTitleTest()
        Dim expected As String = "test"
        Dim actual As String
        actual = target.GetTitle()
        Verify.AreEqual(expected, actual)
    End Sub

End Class
