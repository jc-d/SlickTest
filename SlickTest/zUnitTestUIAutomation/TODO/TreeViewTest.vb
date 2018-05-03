'Imports NUnit.Framework

'Imports UIControls



''''<summary>
''''This is a test class for TreeViewTest and is intended
''''to contain all TreeViewTest Unit Tests
''''</summary>
'<TestFixture()>  _
'Public Class TreeViewTest


'

'    '''<summary>
'    '''Gets or sets the test context which provides
'    '''information about and functionality for the current test run.
'    '''</summary>
'    Public Property TestContext() As TestContext
'        Get
'            Return testContextInstance
'        End Get
'        Set(ByVal value As TestContext)
'            testContextInstance = Value
'        End Set
'    End Property

'#Region "Additional test attributes"
'    '
'    'You can use the following additional attributes as you write your tests:
'    '
'    'Use ClassInitialize to run code before running the first test in the class
'    '<ClassInitialize()>  _
'    'Public Shared Sub MyClassInitialize()
'    'End Sub
'    '
'    'Use ClassCleanup to run code after all tests in a class have run
'    '<ClassCleanup()>  _
'    'Public Shared Sub MyClassCleanup()
'    'End Sub
'    '
'    'Use TestInitialize to run code before running each test
'    '<TestInitialize()>  _
'    'Public Sub MyTestInitialize()
'    'End Sub
'    '
'    'Use TestCleanup to run code after each test has run
'    '<TestCleanup()>  _
'    'Public Sub MyTestCleanup()
'    'End Sub
'    '
'#End Region


'    '''<summary>
'    '''A test for IsTreeView
'    '''</summary>
'    <Test()> _
'    Public Sub IsTreeViewTest()
'        Dim target As TreeView = New TreeView ' TODO: Initialize to an appropriate value
'        Dim expected As Boolean = False ' TODO: Initialize to an appropriate value
'        Dim actual As Boolean
'        actual = target.IsTreeView
'        Verify.AreEqual(expected, actual)
'        Verify.Inconclusive("Verify the correctness of this test method.")
'    End Sub

'    '''<summary>
'    '''A test for GetVisibleItemCount
'    '''</summary>
'    <Test()> _
'    Public Sub GetVisibleItemCountTest()
'        Dim target As TreeView = New TreeView ' TODO: Initialize to an appropriate value
'        Dim expected As Integer = 0 ' TODO: Initialize to an appropriate value
'        Dim actual As Integer
'        actual = target.GetVisibleItemCount
'        Verify.AreEqual(expected, actual)
'        Verify.Inconclusive("Verify the correctness of this test method.")
'    End Sub

'    '''<summary>
'    '''A test for GetSelectedText
'    '''</summary>
'    <Test()> _
'    Public Sub GetSelectedTextTest()
'        Dim target As TreeView = New TreeView ' TODO: Initialize to an appropriate value
'        Dim expected As String = String.Empty ' TODO: Initialize to an appropriate value
'        Dim actual As String
'        actual = target.GetSelectedText
'        Verify.AreEqual(expected, actual)
'        Verify.Inconclusive("Verify the correctness of this test method.")
'    End Sub

'    '''<summary>
'    '''A test for GetItemText
'    '''</summary>
'    <Test()> _
'    Public Sub GetItemTextTest()
'        Dim target As TreeView = New TreeView ' TODO: Initialize to an appropriate value
'        Dim Index As Integer = 0 ' TODO: Initialize to an appropriate value
'        Dim expected As String = String.Empty ' TODO: Initialize to an appropriate value
'        Dim actual As String
'        actual = target.GetItemText(Index)
'        Verify.AreEqual(expected, actual)
'        Verify.Inconclusive("Verify the correctness of this test method.")
'    End Sub

'    '''<summary>
'    '''A test for GetItemIndex
'    '''</summary>
'    <Test()> _
'    Public Sub GetItemIndexTest1()
'        Dim target As TreeView = New TreeView ' TODO: Initialize to an appropriate value
'        Dim Text As String = String.Empty ' TODO: Initialize to an appropriate value
'        Dim expected As Integer = 0 ' TODO: Initialize to an appropriate value
'        Dim actual As Integer
'        actual = target.GetItemIndex(Text)
'        Verify.AreEqual(expected, actual)
'        Verify.Inconclusive("Verify the correctness of this test method.")
'    End Sub

'    '''<summary>
'    '''A test for GetItemIndex
'    '''</summary>
'    <Test()> _
'    Public Sub GetItemIndexTest()
'        Dim target As TreeView = New TreeView ' TODO: Initialize to an appropriate value
'        Dim Text As String = String.Empty ' TODO: Initialize to an appropriate value
'        Dim UseWildCard As Boolean = False ' TODO: Initialize to an appropriate value
'        Dim expected As Integer = 0 ' TODO: Initialize to an appropriate value
'        Dim actual As Integer
'        actual = target.GetItemIndex(Text, UseWildCard)
'        Verify.AreEqual(expected, actual)
'        Verify.Inconclusive("Verify the correctness of this test method.")
'    End Sub

'    '''<summary>
'    '''A test for GetAllVisibleItems
'    '''</summary>
'    <Test()> _
'    Public Sub GetAllVisibleItemsTest()
'        Dim target As TreeView = New TreeView ' TODO: Initialize to an appropriate value
'        Dim expected() As String = Nothing ' TODO: Initialize to an appropriate value
'        Dim actual() As String
'        actual = target.GetAllVisibleItems
'        Verify.AreEqual(expected, actual)
'        Verify.Inconclusive("Verify the correctness of this test method.")
'    End Sub

'    '''<summary>
'    '''A test for TreeView Constructor
'    '''</summary>
'    <Test()> _
'    Public Sub TreeViewConstructorTest1()
'        Dim target As TreeView = New TreeView
'        Verify.Inconclusive("TODO: Implement code to verify target")
'    End Sub

'    '''<summary>
'    '''A test for TreeView Constructor
'    '''</summary>
'    <Test()> _
'    Public Sub TreeViewConstructorTest()
'        Dim win As Window = Nothing ' TODO: Initialize to an appropriate value
'        Dim target As TreeView = New TreeView(win)
'        Verify.Inconclusive("TODO: Implement code to verify target")
'    End Sub
'End Class
