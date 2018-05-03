Imports System.Windows.Forms

Imports Microsoft.VisualStudio.TestTools.UnitTesting

Imports UIControls



'''<summary>
'''This is a test class for AlertTest and is intended
'''to contain all AlertTest Unit Tests
'''</summary>
<TestClass()> _
Public Class AlertTest


    Private testContextInstance As TestContext

    '''<summary>
    '''Gets or sets the test context which provides
    '''information about and functionality for the current test run.
    '''</summary>
    Public Property TestContext() As TestContext
        Get
            Return testContextInstance
        End Get
        Set(ByVal value As TestContext)
            testContextInstance = Value
        End Set
    End Property

#Region "Additional test attributes"
    '
    'You can use the following additional attributes as you write your tests:
    '
    'Use ClassInitialize to run code before running the first test in the class
    '<ClassInitialize()>  _
    'Public Shared Sub MyClassInitialize(ByVal testContext As TestContext)
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
    '''A test for Show
    '''</summary>
    <TestMethod()> _
    Public Sub ShowTest()
        Dim Message As String = "Hello world"
        Dim TimeOut As Integer = 5
        Dim Title As String = "Hello world"
        Dim LogInfo As Boolean = False
        Dim Buttons As MessageBoxButtons = MessageBoxButtons.OKCancel
        Dim Icon As MessageBoxIcon = MessageBoxIcon.Exclamation
        Dim DefaultButton As MessageBoxDefaultButton = MessageBoxDefaultButton.Button1
        Dim ShortcutsEnabled As Boolean = False ' TODO: Initialize to an appropriate value
        Dim expected As DialogResult = DialogResult.Cancel
        Dim actual As DialogResult
        actual = Alert.Show(Message, TimeOut, Title, LogInfo, Buttons, Icon, DefaultButton, ShortcutsEnabled)
        Assert.AreEqual(expected, actual)
    End Sub
End Class
