Imports NUnit.Framework
Imports UIControls

'''<summary>
'''This is a test class for WebDivTest and is intended
'''to contain all WebDivTest Unit Tests
'''</summary>
<TestFixture()> _
Public Class WebTextBoxTest
    Inherits WebTests

#Region "Additional test attributes"
    Public ReadOnly Property target() As WebTextBox
        Get
            Return WebBrowser.WebTextBox(desc)
        End Get
    End Property
    Public Shared desc As Description = Description.Create()
    Private Const TextAreaStr As String = "TextArea"
    Private Const TextStr As String = "text"

    Private Sub SetDescriptionId(ByVal id As String)
        desc.Add(APIControls.Description.DescriptionData.WebID, id)
    End Sub

    'Use TestInitialize to run code before running each test
    <SetUp()> _
    Public Overrides Sub MyTestInitialize()
        Me.SiteUrl = System.IO.Path.GetFullPath(".\WebPages\MiscSample.html")
        MyBase.MyTestInitialize()
        desc.Reset()
    End Sub
#End Region

    '''<summary>
    '''A test for Wrap
    '''</summary>
    <Test()> _
    Public Sub WrapInputTextBoxTest()
        SetDescriptionId(TextStr)
        Dim expected As String = String.Empty
        Dim actual As String
        actual = target.GetWrap()
        Verify.AreEqual(expected, actual)
    End Sub

    '''<summary>
    '''A test for Type
    '''</summary>
    <Test()> _
    Public Sub TypeInputTextBoxTest()
        SetDescriptionId(TextStr)
        Dim expected As String = String.Empty
        Dim actual As String
        actual = target.TextType()
        Verify.AreEqual(expected, actual)
    End Sub

    '''<summary>
    '''A test for SetSelect
    '''</summary>
    <Test()> _
    Public Sub SetSelectInputTextBoxTest()
        SetDescriptionId(TextStr)
        target.SetSelect()
        Verify.AreEqual(Nothing, Nothing, "Not verified")
    End Sub

    '''<summary>
    '''A test for Rows
    '''</summary>
    <Test()> _
    Public Sub RowsInputTextBoxTest()
        SetDescriptionId(TextStr)
        Dim expected As Integer = -1
        Dim actual As Integer
        actual = target.GetRowCount()
        Verify.AreEqual(expected, actual)

    End Sub

    '''<summary>
    '''A test for IsReadOnly
    '''</summary>
    <Test()> _
    Public Sub IsReadOnlyInputTextBoxTest()
        SetDescriptionId(TextStr)
        Dim expected As Boolean = False
        Dim actual As Boolean
        actual = target.IsReadOnly()
        Verify.AreEqual(expected, actual)
    End Sub

    '''<summary>
    '''A test for Columns
    '''</summary>
    <Test()> _
    Public Sub ColumnsInputTextBoxTest()
        SetDescriptionId(TextStr)
        Dim expected As Integer = 1
        Dim actual As Integer
        actual = target.GetColumnCount()
        Verify.AreEqual(expected, actual)
    End Sub

    '''<summary>
    '''A test for TypeText
    '''</summary>
    <Test()> _
    Public Sub TypeTextInputTextBoxTest()
        SetDescriptionId(TextStr)
        Dim expected As String = "verify"
        Dim actual As String
        target.TypeText("verify")
        actual = target.GetText()
        Verify.AreEqual(expected, actual)
    End Sub

    '''<summary>
    '''A test for Wrap
    '''</summary>
    <Test()> _
    Public Sub WrapTextAreaTest()
        SetDescriptionId(TextAreaStr)
        Dim expected As String = "soft"
        Dim actual As String
        actual = target.GetWrap()
        Verify.AreEqual(expected, actual)
    End Sub

    '''<summary>
    '''A test for Type
    '''</summary>
    <Test()> _
    Public Sub TypeTextAreaTest()
        SetDescriptionId(TextAreaStr)
        Dim expected As String = "textarea"
        Dim actual As String
        actual = target.TextType()
        Verify.AreEqual(expected, actual)
    End Sub

    '''<summary>
    '''A test for SetSelect
    '''</summary>
    <Test()> _
    Public Sub SetSelectTextAreaTest()
        SetDescriptionId(TextAreaStr)
        target.SetSelect()
        Verify.AreEqual(Nothing, Nothing, "Not verified")
    End Sub

    '''<summary>
    '''A test for Rows
    '''</summary>
    <Test()> _
    Public Sub RowsTextAreaTest()
        SetDescriptionId(TextAreaStr)
        Dim expected As Integer = 2
        Dim actual As Integer
        actual = target.GetRowCount()
        Verify.AreEqual(expected, actual)

    End Sub

    '''<summary>
    '''A test for IsReadOnly
    '''</summary>
    <Test()> _
    Public Sub IsReadOnlyTextAreaTest()
        SetDescriptionId(TextAreaStr)
        Dim expected As Boolean = False
        Dim actual As Boolean
        actual = target.IsReadOnly()
        Verify.AreEqual(expected, actual)
    End Sub

    '''<summary>
    '''A test for Columns
    '''</summary>
    <Test()> _
    Public Sub ColumnsTextAreaTest()
        SetDescriptionId(TextAreaStr)
        Dim expected As Integer = 20
        Dim actual As Integer
        actual = target.GetColumnCount()
        Verify.AreEqual(expected, actual)
    End Sub

    '''<summary>
    '''A test for TypeText
    '''</summary>
    <Test()> _
    Public Sub TypeTextTextAreaTest()
        SetDescriptionId(TextAreaStr)
        Dim expected As String = "verify"
        Dim actual As String
        target.TypeText("verify")
        actual = target.GetText()
        Verify.AreEqual(expected, actual)
    End Sub

End Class
