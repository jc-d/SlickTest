Imports NUnit.Framework

Imports UIControls



'''<summary>
'''This is a test class for ClipboardTest and is intended
'''to contain all ClipboardTest Unit Tests
'''</summary>
<TestFixture()> _
Public Class ClipboardTest


#Region "Additional test attributes"
    '
    'You can use the following additional attributes as you write your tests:
    '
    'Use ClassInitialize to run code before running the first test in the class
    '<ClassInitialize()>  _
    'Public Shared Sub MyClassInitialize()
    'End Sub
    '
    'Use ClassCleanup to run code after all tests in a class have run
    '<ClassCleanup()>  _
    'Public Shared Sub MyClassCleanup()
    'End Sub
    '
    'Use TestInitialize to run code before running each test
    '<TestInitialize()>  _
    'Public Sub MyTestInitialize()
    'End Sub
    '
    'Use TestCleanup to run code after each test has run
    '<TestCleanup()>  _
    'Public Sub MyTestCleanup()
    'End Sub
    '
#End Region


    '''<summary>
    '''A test for Text
    '''</summary>
    <Test(), RequiresSTA()> _
    Public Sub TextTest()
        Dim target As Clipboard = New Clipboard ' TODO: Initialize to an appropriate value
        Dim expected As String = "Hello World!" ' TODO: Initialize to an appropriate value
        Dim actual As String
        target.Text = expected
        actual = target.Text
        Verify.AreEqual(expected, actual)
        'Verify.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for Clear
    '''</summary>
    <Test(), RequiresSTA()> _
    Public Sub ClearTest()
        Dim target As Clipboard = New Clipboard ' TODO: Initialize to an appropriate value
        Dim expected As String = String.Empty
        Dim actual As String = String.Empty
        target.Text = "Hello World!"
        target.Clear()
        Verify.AreEqual(expected, actual)
        'Verify.Inconclusive("A method that does not return a value cannot be verified.")
    End Sub

    '''<summary>
    '''A test for Clipboard Constructor
    '''</summary>
    <Test()> _
    Public Sub ClipboardConstructorTest()
        Dim target As Clipboard = New Clipboard
    End Sub
End Class
