Imports NUnit.Framework
Imports UIControls

'''<summary>
'''This is a test class for WebSpanTest and is intended
'''to contain all WebSpanTest Unit Tests
'''</summary>
<TestFixture()> _
Public Class WebCheckboxTest
    Inherits WebTests

#Region "Additional test attributes"

    Public Shared target As WebCheckbox

    'Use TestInitialize to run code before running each test
    <SetUp()> _
    Public Overrides Sub MyTestInitialize()
        MyBase.MyTestInitialize()
        Dim desc As Description = Description.Create()
        desc.Add(APIControls.Description.DescriptionData.WebID, "Checkbox1")
        target = WebBrowser.WebCheckbox(desc)
    End Sub
#End Region

    '''<summary>
    '''A test for GetChecked
    '''</summary>
    <Test()> _
    Public Sub GetCheckedTest()
        Dim expected As Boolean = True
        Dim actual As Boolean
        actual = target.GetChecked()
        Verify.AreEqual(expected, actual)
    End Sub

    '''<summary>
    '''A test for SetChecked
    '''</summary>
    <Test()> _
    Public Sub SetCheckedTest()
        Dim expected As Boolean
        Dim actual As Boolean
        actual = target.GetChecked()
        expected = Not actual
        target.SetChecked(expected)
        actual = target.GetChecked()
        Verify.AreEqual(expected, actual)
    End Sub

    '''<summary>
    '''A test for IsIndeterminate
    '''</summary>
    <Test()> _
    Public Sub IsIndeterminateTest()
        Dim expected As Boolean
        Dim actual As Boolean
        actual = target.IsIndeterminate()
        Verify.AreEqual(expected, actual)
    End Sub

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

End Class
