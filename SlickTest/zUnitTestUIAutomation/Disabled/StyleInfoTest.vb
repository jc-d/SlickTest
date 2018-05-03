'Imports NUnit.Framework

'Imports UIControls



''''<summary>
''''This is a test class for StyleInfoTest and is intended
''''to contain all StyleInfoTest Unit Tests
''''</summary>
'<TestFixture()>  _
'Public Class StyleInfoTest


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
'    '''A test for WS_VSCROLL
'    '''</summary>
'    <Test()> _
'    Public Sub WS_VSCROLLTest()
'        Dim target As StyleInfo = New StyleInfo ' TODO: Initialize to an appropriate value
'        Dim actual As Integer
'        actual = target.WS_VSCROLL
'        Verify.Inconclusive("Verify the correctness of this test method.")
'    End Sub

'    '''<summary>
'    '''A test for WS_VISIBLE
'    '''</summary>
'    <Test()> _
'    Public Sub WS_VISIBLETest()
'        Dim target As StyleInfo = New StyleInfo ' TODO: Initialize to an appropriate value
'        Dim actual As Integer
'        actual = target.WS_VISIBLE
'        Verify.Inconclusive("Verify the correctness of this test method.")
'    End Sub

'    '''<summary>
'    '''A test for WS_TILEDWINDOW
'    '''</summary>
'    <Test()> _
'    Public Sub WS_TILEDWINDOWTest()
'        Dim target As StyleInfo = New StyleInfo ' TODO: Initialize to an appropriate value
'        Dim actual As Integer
'        actual = target.WS_TILEDWINDOW
'        Verify.Inconclusive("Verify the correctness of this test method.")
'    End Sub

'    '''<summary>
'    '''A test for WS_TILED
'    '''</summary>
'    <Test()> _
'    Public Sub WS_TILEDTest()
'        Dim target As StyleInfo = New StyleInfo ' TODO: Initialize to an appropriate value
'        Dim actual As Integer
'        actual = target.WS_TILED
'        Verify.Inconclusive("Verify the correctness of this test method.")
'    End Sub

'    '''<summary>
'    '''A test for WS_THICKFRAME
'    '''</summary>
'    <Test()> _
'    Public Sub WS_THICKFRAMETest()
'        Dim target As StyleInfo = New StyleInfo ' TODO: Initialize to an appropriate value
'        Dim actual As Integer
'        actual = target.WS_THICKFRAME
'        Verify.Inconclusive("Verify the correctness of this test method.")
'    End Sub

'    '''<summary>
'    '''A test for WS_TABSTOP
'    '''</summary>
'    <Test()> _
'    Public Sub WS_TABSTOPTest()
'        Dim target As StyleInfo = New StyleInfo ' TODO: Initialize to an appropriate value
'        Dim actual As Integer
'        actual = target.WS_TABSTOP
'        Verify.Inconclusive("Verify the correctness of this test method.")
'    End Sub

'    '''<summary>
'    '''A test for WS_SYSMENU
'    '''</summary>
'    <Test()> _
'    Public Sub WS_SYSMENUTest()
'        Dim target As StyleInfo = New StyleInfo ' TODO: Initialize to an appropriate value
'        Dim actual As Integer
'        actual = target.WS_SYSMENU
'        Verify.Inconclusive("Verify the correctness of this test method.")
'    End Sub

'    '''<summary>
'    '''A test for WS_SIZEBOX
'    '''</summary>
'    <Test()> _
'    Public Sub WS_SIZEBOXTest()
'        Dim target As StyleInfo = New StyleInfo ' TODO: Initialize to an appropriate value
'        Dim actual As Integer
'        actual = target.WS_SIZEBOX
'        Verify.Inconclusive("Verify the correctness of this test method.")
'    End Sub

'    '''<summary>
'    '''A test for WS_POPUPWINDOW
'    '''</summary>
'    <Test()> _
'    Public Sub WS_POPUPWINDOWTest()
'        Dim target As StyleInfo = New StyleInfo ' TODO: Initialize to an appropriate value
'        Dim actual As Long
'        actual = target.WS_POPUPWINDOW
'        Verify.Inconclusive("Verify the correctness of this test method.")
'    End Sub

'    '''<summary>
'    '''A test for WS_POPUP
'    '''</summary>
'    <Test()> _
'    Public Sub WS_POPUPTest()
'        Dim target As StyleInfo = New StyleInfo ' TODO: Initialize to an appropriate value
'        Dim actual As Long
'        actual = target.WS_POPUP
'        Verify.Inconclusive("Verify the correctness of this test method.")
'    End Sub

'    '''<summary>
'    '''A test for WS_OVERLAPPEDWINDOW
'    '''</summary>
'    <Test()> _
'    Public Sub WS_OVERLAPPEDWINDOWTest()
'        Dim target As StyleInfo = New StyleInfo ' TODO: Initialize to an appropriate value
'        Dim actual As Integer
'        actual = target.WS_OVERLAPPEDWINDOW
'        Verify.Inconclusive("Verify the correctness of this test method.")
'    End Sub

'    '''<summary>
'    '''A test for WS_OVERLAPPED
'    '''</summary>
'    <Test()> _
'    Public Sub WS_OVERLAPPEDTest()
'        Dim target As StyleInfo = New StyleInfo ' TODO: Initialize to an appropriate value
'        Dim actual As Integer
'        actual = target.WS_OVERLAPPED
'        Verify.Inconclusive("Verify the correctness of this test method.")
'    End Sub

'    '''<summary>
'    '''A test for WS_MINIMIZEBOX
'    '''</summary>
'    <Test()> _
'    Public Sub WS_MINIMIZEBOXTest()
'        Dim target As StyleInfo = New StyleInfo ' TODO: Initialize to an appropriate value
'        Dim actual As Integer
'        actual = target.WS_MINIMIZEBOX
'        Verify.Inconclusive("Verify the correctness of this test method.")
'    End Sub

'    '''<summary>
'    '''A test for WS_MINIMIZE
'    '''</summary>
'    <Test()> _
'    Public Sub WS_MINIMIZETest()
'        Dim target As StyleInfo = New StyleInfo ' TODO: Initialize to an appropriate value
'        Dim actual As Integer
'        actual = target.WS_MINIMIZE
'        Verify.Inconclusive("Verify the correctness of this test method.")
'    End Sub

'    '''<summary>
'    '''A test for WS_MAXIMIZEBOX
'    '''</summary>
'    <Test()> _
'    Public Sub WS_MAXIMIZEBOXTest()
'        Dim target As StyleInfo = New StyleInfo ' TODO: Initialize to an appropriate value
'        Dim actual As Integer
'        actual = target.WS_MAXIMIZEBOX
'        Verify.Inconclusive("Verify the correctness of this test method.")
'    End Sub

'    '''<summary>
'    '''A test for WS_MAXIMIZE
'    '''</summary>
'    <Test()> _
'    Public Sub WS_MAXIMIZETest()
'        Dim target As StyleInfo = New StyleInfo ' TODO: Initialize to an appropriate value
'        Dim actual As Integer
'        actual = target.WS_MAXIMIZE
'        Verify.Inconclusive("Verify the correctness of this test method.")
'    End Sub

'    '''<summary>
'    '''A test for WS_ICONIC
'    '''</summary>
'    <Test()> _
'    Public Sub WS_ICONICTest()
'        Dim target As StyleInfo = New StyleInfo ' TODO: Initialize to an appropriate value
'        Dim actual As Integer
'        actual = target.WS_ICONIC
'        Verify.Inconclusive("Verify the correctness of this test method.")
'    End Sub

'    '''<summary>
'    '''A test for WS_HSCROLL
'    '''</summary>
'    <Test()> _
'    Public Sub WS_HSCROLLTest()
'        Dim target As StyleInfo = New StyleInfo ' TODO: Initialize to an appropriate value
'        Dim actual As Integer
'        actual = target.WS_HSCROLL
'        Verify.Inconclusive("Verify the correctness of this test method.")
'    End Sub

'    '''<summary>
'    '''A test for WS_GROUP
'    '''</summary>
'    <Test()> _
'    Public Sub WS_GROUPTest()
'        Dim target As StyleInfo = New StyleInfo ' TODO: Initialize to an appropriate value
'        Dim actual As Integer
'        actual = target.WS_GROUP
'        Verify.Inconclusive("Verify the correctness of this test method.")
'    End Sub

'    '''<summary>
'    '''A test for WS_DLGFRAME
'    '''</summary>
'    <Test()> _
'    Public Sub WS_DLGFRAMETest()
'        Dim target As StyleInfo = New StyleInfo ' TODO: Initialize to an appropriate value
'        Dim actual As Integer
'        actual = target.WS_DLGFRAME
'        Verify.Inconclusive("Verify the correctness of this test method.")
'    End Sub

'    '''<summary>
'    '''A test for WS_DISABLED
'    '''</summary>
'    <Test()> _
'    Public Sub WS_DISABLEDTest()
'        Dim target As StyleInfo = New StyleInfo ' TODO: Initialize to an appropriate value
'        Dim actual As Integer
'        actual = target.WS_DISABLED
'        Verify.Inconclusive("Verify the correctness of this test method.")
'    End Sub

'    '''<summary>
'    '''A test for WS_CLIPSIBLINGS
'    '''</summary>
'    <Test()> _
'    Public Sub WS_CLIPSIBLINGSTest()
'        Dim target As StyleInfo = New StyleInfo ' TODO: Initialize to an appropriate value
'        Dim actual As Integer
'        actual = target.WS_CLIPSIBLINGS
'        Verify.Inconclusive("Verify the correctness of this test method.")
'    End Sub

'    '''<summary>
'    '''A test for WS_CLIPCHILDREN
'    '''</summary>
'    <Test()> _
'    Public Sub WS_CLIPCHILDRENTest()
'        Dim target As StyleInfo = New StyleInfo ' TODO: Initialize to an appropriate value
'        Dim actual As Integer
'        actual = target.WS_CLIPCHILDREN
'        Verify.Inconclusive("Verify the correctness of this test method.")
'    End Sub

'    '''<summary>
'    '''A test for WS_CHILDWINDOW
'    '''</summary>
'    <Test()> _
'    Public Sub WS_CHILDWINDOWTest()
'        Dim target As StyleInfo = New StyleInfo ' TODO: Initialize to an appropriate value
'        Dim actual As Integer
'        actual = target.WS_CHILDWINDOW
'        Verify.Inconclusive("Verify the correctness of this test method.")
'    End Sub

'    '''<summary>
'    '''A test for WS_CHILD
'    '''</summary>
'    <Test()> _
'    Public Sub WS_CHILDTest()
'        Dim target As StyleInfo = New StyleInfo ' TODO: Initialize to an appropriate value
'        Dim actual As Integer
'        actual = target.WS_CHILD
'        Verify.Inconclusive("Verify the correctness of this test method.")
'    End Sub

'    '''<summary>
'    '''A test for WS_CAPTION
'    '''</summary>
'    <Test()> _
'    Public Sub WS_CAPTIONTest()
'        Dim target As StyleInfo = New StyleInfo ' TODO: Initialize to an appropriate value
'        Dim actual As Integer
'        actual = target.WS_CAPTION
'        Verify.Inconclusive("Verify the correctness of this test method.")
'    End Sub

'    '''<summary>
'    '''A test for WS_BORDER
'    '''</summary>
'    <Test()> _
'    Public Sub WS_BORDERTest()
'        Dim target As StyleInfo = New StyleInfo ' TODO: Initialize to an appropriate value
'        Dim actual As Integer
'        actual = target.WS_BORDER
'        Verify.Inconclusive("Verify the correctness of this test method.")
'    End Sub

'    '''<summary>
'    '''A test for TextBoxStyle
'    '''</summary>
'    <Test()> _
'    Public Sub TextBoxStyleTest()
'        Dim actual As TextBoxStyle
'        actual = StyleInfo.TextBoxStyle
'        Verify.Inconclusive("Verify the correctness of this test method.")
'    End Sub

'    '''<summary>
'    '''A test for RadioButtonStyle
'    '''</summary>
'    <Test()> _
'    Public Sub RadioButtonStyleTest()
'        Dim actual As ButtonStyle
'        actual = StyleInfo.RadioButtonStyle
'        Verify.Inconclusive("Verify the correctness of this test method.")
'    End Sub

'    '''<summary>
'    '''A test for CheckBoxStyle
'    '''</summary>
'    <Test()> _
'    Public Sub CheckBoxStyleTest()
'        Dim actual As ButtonStyle
'        actual = StyleInfo.CheckBoxStyle
'        Verify.Inconclusive("Verify the correctness of this test method.")
'    End Sub

'    '''<summary>
'    '''A test for ButtonStyle
'    '''</summary>
'    <Test()> _
'    Public Sub ButtonStyleTest()
'        Dim actual As ButtonStyle
'        actual = StyleInfo.ButtonStyle
'        Verify.Inconclusive("Verify the correctness of this test method.")
'    End Sub

'    '''<summary>
'    '''A test for ValueInfo
'    '''</summary>
'    <Test()> _
'    Public Sub ValueInfoTest()
'        Dim target As StyleInfo = New StyleInfo ' TODO: Initialize to an appropriate value
'        Dim StyleValue As Integer = 0 ' TODO: Initialize to an appropriate value
'        Dim expected As String = String.Empty ' TODO: Initialize to an appropriate value
'        Dim actual As String
'        actual = target.ValueInfo(StyleValue)
'        Verify.AreEqual(expected, actual)
'        Verify.Inconclusive("Verify the correctness of this test method.")
'    End Sub

'    '''<summary>
'    '''A test for ContainsValue
'    '''</summary>
'    <Test()> _
'    Public Sub ContainsValueTest()
'        Dim target As StyleInfo = New StyleInfo ' TODO: Initialize to an appropriate value
'        Dim StyleValue As Integer = 0 ' TODO: Initialize to an appropriate value
'        Dim WS_Value As Integer = 0 ' TODO: Initialize to an appropriate value
'        Dim expected As Boolean = False ' TODO: Initialize to an appropriate value
'        Dim actual As Boolean
'        actual = target.ContainsValue(StyleValue, WS_Value)
'        Verify.AreEqual(expected, actual)
'        Verify.Inconclusive("Verify the correctness of this test method.")
'    End Sub

'    '''<summary>
'    '''A test for StyleInfo Constructor
'    '''</summary>
'    <Test()> _
'    Public Sub StyleInfoConstructorTest()
'        Dim target As StyleInfo = New StyleInfo
'        Verify.Inconclusive("TODO: Implement code to verify target")
'    End Sub
'End Class
