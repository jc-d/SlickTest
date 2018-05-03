Imports NUnit.Framework
Imports UIControls

'''<summary>
'''This is a test class for WebRadioButtonTest and is intended
'''to contain all WebRadioButtonTest Unit Tests
'''</summary>
<TestFixture()> _
Public Class WebRadioButtonTest
    Inherits WebTests

#Region "Additional test attributes"

    Public Shared target As WebRadioButton

    'Use TestInitialize to run code before running each test
    <SetUp()> _
    Public Overrides Sub MyTestInitialize()
        MyBase.MyTestInitialize()
        Dim desc As Description = Description.Create()
        desc.Add(APIControls.Description.DescriptionData.WebID, "RadioButton1")
        target = WebBrowser.WebRadioButton(desc)
    End Sub
#End Region

    '''<summary>
    '''A test for IsReadOnly
    '''</summary>
    <Test()> _
    Public Sub IsReadOnlyTest()
        Dim expected As Boolean = False
        Dim actual As Boolean
        actual = target.IsReadOnly()
        Verify.AreEqual(expected, actual)
    End Sub

    '''<summary>
    '''A test for IsSelected
    '''</summary>
    <Test()> _
    Public Sub IsSelectedTest()
        Dim expected As Boolean = False
        Dim actual As Boolean
        actual = target.IsSelected()
        Verify.AreEqual(expected, actual)
        target.Click()
        expected = True
        actual = target.IsSelected()
        Verify.AreEqual(expected, actual)
    End Sub
End Class