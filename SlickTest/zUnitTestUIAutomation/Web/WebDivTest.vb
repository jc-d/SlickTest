Imports NUnit.Framework
Imports UIControls

'''<summary>
'''This is a test class for WebDivTest and is intended
'''to contain all WebDivTest Unit Tests
'''</summary>
<TestFixture()> _
Public Class WebDivTest
    Inherits WebTests

#Region "Additional test attributes"

    Public Shared target As WebDiv

    'Use TestInitialize to run code before running each test
    <SetUp()> _
    Public Overrides Sub MyTestInitialize()
        MyBase.MyTestInitialize()
        Dim desc As Description = Description.Create()
        desc.Add(APIControls.Description.DescriptionData.WebID, "DivTest")
        target = WebBrowser.WebDiv(desc)
    End Sub
#End Region

    '''<summary>
    '''A test for GetNoWrap
    '''</summary>
    <Test()> _
    Public Sub GetNoWrapTest()

        Dim expected As Boolean = False
        Dim actual As Boolean
        actual = target.GetNoWrap()
        Verify.AreEqual(expected, actual)
    End Sub

    '''<summary>
    '''A test for GetAlign
    '''</summary>
    <Test()> _
    Public Sub GetAlignTest()

        Dim expected As String = "center"
        Dim actual As String
        actual = target.GetAlign()
        Verify.AreEqual(expected, actual)
    End Sub
End Class