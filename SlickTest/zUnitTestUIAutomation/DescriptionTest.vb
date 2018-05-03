Imports System.Drawing
Imports System
Imports System.Collections.Specialized
Imports NUnit.Framework
Imports UIControls

'''<summary>
'''This is a test class for DescriptionTest and is intended
'''to contain all DescriptionTest Unit Tests
'''</summary>
<TestFixture()> _
Public Class DescriptionTest

#Region "Additional test attributes"
    Private Shared p As System.Diagnostics.Process
    Private Shared target As Menu
    Public Shared Sub CloseAll(Optional ByVal name As String = "calc")
        For Each pro As System.Diagnostics.Process In System.Diagnostics.Process.GetProcessesByName(name)
            pro.CloseMainWindow()
        Next
        System.Threading.Thread.Sleep(100)
    End Sub
    '
    'You can use the following additional attributes as you write your tests:
    '
    'Use ClassInitialize to run code before running the first test in the class
    <TestFixtureSetUp()> _
    Public Shared Sub MyClassInitialize()

    End Sub

    'Use ClassCleanup to run code after all tests in a class have run
    <TestFixtureTearDown()> _
    Public Shared Sub MyClassCleanup()

    End Sub

    Public Shared Sub Init()
        p = Diagnostics.Process.Start(System.IO.Path.GetFullPath("..\..\..\zUnitTestUIAutomation\Programs\calc.exe"))
        System.Threading.Thread.Sleep(2000)
        Dim a As UIControls.InterAct
        a = New UIControls.InterAct()
        target = a.Window("name:=""" & "SciCalc" & """").Menu()
        a = Nothing
    End Sub

    'Use TestInitialize to run code before running each test
    <SetUp()> _
    Public Sub MyTestInitialize()
        Init()
    End Sub

    'Use TestCleanup to run code after each test has run
    <TearDown()> _
    Public Sub MyTestCleanup()
        CloseAll()
    End Sub
    '
#End Region

    'Public Shared GAmpInterop As UIControls.Description = UIControls.Description.Create(":="""";;height:=""367"";;width:=""488"";;bottom:=""911"";;top:=""544"";;left:=""841"";;right:=""1329"";;name:=""GAmpInterop"";;value:="""";;:=""GAmp20"";;hwnd:=""5769040""")
    '''<summary>
    '''A test for ControlType
    '''</summary>
    <Test()> _
    Public Sub WindowTypeTest()
        Dim expected As String = "window"
        Dim target As Description = Description.Create("ControlType:=""" & expected & """", False) ' TODO: Initialize to an appropriate value
        expected = target.WindowType
        Verify.AreEqual(expected, target.WindowType)
    End Sub

    '''<summary>
    '''A test for WildCard
    '''</summary>
    <Test()> _
    Public Sub WildCardTest1()
        Dim expected As Boolean = False ' TODO: Initialize to an appropriate value
        Dim target As Description = Description.Create("name:=""" & "hi" & """", expected)
        Dim actual As Boolean
        target.WildCard = expected
        actual = target.WildCard
        Verify.AreEqual(expected, actual)
    End Sub

    '''<summary>
    '''A test for Width
    '''</summary>
    <Test()> _
    Public Sub WidthTest()
        Dim target As Description = Description.Create("name:=""" & "hi" & """")
        Dim actual As Integer
        Dim expected As Integer = 10
        target.Add("width", expected.ToString())
        actual = target.Width
        Verify.AreEqual(expected, actual)
    End Sub

    '''<summary>
    '''A test for Value
    '''</summary>
    <Test()> _
    Public Sub ValueTest()
        Dim expected As String = "hi world!"
        Dim target As Description = Description.Create("value:=""" & expected & """")
        Dim actual As String
        actual = target.Value
        Verify.AreEqual(expected, actual)
    End Sub

    '''<summary>
    '''A test for Top
    '''</summary>
    <Test()> _
    Public Sub TopTest()
        Dim expected As Integer = 10
        Dim target As Description = Description.Create("top:=""" & expected.ToString() & """")
        Dim actual As Integer
        actual = target.Top
        Verify.AreEqual(expected, actual)
    End Sub

    '''<summary>
    '''A test for Right
    '''</summary>
    <Test()> _
    Public Sub RightTest()
        Dim expected As Integer = 10
        Dim target As Description = Description.Create("right:=""" & expected.ToString() & """")
        Dim actual As Integer
        actual = target.Right
        Verify.AreEqual(expected, actual)
    End Sub

    '''<summary>
    '''A test for ProcessName
    '''</summary>
    <Test()> _
    Public Sub ProcessNameTest()
        Dim expected As String = "GAmp"
        Dim target As Description = Description.Create("processname:=""" & expected & """")
        Dim actual As String
        actual = target.ProcessName
        Verify.AreEqual(expected, actual)
    End Sub

    '''<summary>
    '''A test for Name
    '''</summary>
    <Test()> _
    Public Sub NameTest()
        Dim expected As String = "GAmp"
        Dim target As Description = Description.Create("name:=""" & expected & """")
        Dim actual As String
        actual = target.Name
        Verify.AreEqual(expected, actual)
    End Sub

    '''<summary>
    '''A test for Location
    '''</summary>
    <Test()> _
    Public Sub LocationTest()
        Dim expected As New Rectangle(10, 10, 10, 10)
        Dim target As Description = Description.Create()
        target.Add("width", expected.Width.ToString())
        target.Add("height", expected.Height.ToString())
        target.Add("right", expected.Right.ToString())
        target.Add("left", expected.Left.ToString())
        target.Add("bottom", expected.Bottom.ToString())
        target.Add("top", expected.Top.ToString())
        Dim actual As Rectangle
        actual = target.Location
        Verify.AreEqual(expected, actual)
    End Sub

    '''<summary>
    '''A test for Left
    '''</summary>
    <Test()> _
    Public Sub LeftTest()
        Dim expected As Integer = 10
        Dim target As Description = Description.Create("left:=""" & expected.ToString() & """")
        Dim actual As Integer
        actual = target.Left
        Verify.AreEqual(expected, actual)
    End Sub

    '''<summary>
    '''A test for Index
    '''</summary>
    <Test()> _
    Public Sub IndexTest()
        Dim expected As Integer = 10
        Dim target As Description = Description.Create("index:=""" & expected.ToString() & """")
        Dim actual As Integer
        actual = target.Index
        Verify.AreEqual(expected, actual)
    End Sub

    '''<summary>
    '''A test for Hwnd
    '''</summary>
    <Test()> _
    Public Sub HwndTest()
        Dim expected As IntPtr = New IntPtr(10)
        Dim target As Description = Description.Create("hwnd:=""" & expected.ToString() & """")
        Dim actual As IntPtr
        actual = target.Hwnd
        Verify.AreEqual(expected, actual)
    End Sub

    '''<summary>
    '''A test for Height
    '''</summary>
    <Test()> _
    Public Sub HeightTest()
        Dim expected As Integer = 10
        Dim target As Description = Description.Create("height:=""" & expected.ToString() & """")
        Dim actual As Integer
        actual = target.Height
        Verify.AreEqual(expected, actual)
    End Sub

    '''<summary>
    '''A test for Bottom
    '''</summary>
    <Test()> _
    Public Sub BottomTest()
        Dim expected As Integer = 10
        Dim target As Description = Description.Create("bottom:=""" & expected.ToString() & """")
        Dim actual As Integer
        actual = target.Bottom
        Verify.AreEqual(expected, actual)
    End Sub

    ''''<summary>
    ''''A test for ToString
    ''''</summary>
    '<Test()> _
    'Public Sub ToStringTest1()
    '    Dim target As Description = Description.Create() ' TODO: Initialize to an appropriate value
    '    Dim ObjectTypeName As String = String.Empty ' TODO: Initialize to an appropriate value
    '    Dim expected As String = String.Empty ' TODO: Initialize to an appropriate value
    '    Dim actual As String
    '    actual = target.ToString(ObjectTypeName)
    '    Verify.AreEqual(expected, actual)
    '    Verify.Inconclusive("Verify the correctness of this test method.")
    'End Sub

    ''''<summary>
    ''''A test for ToString
    ''''</summary>
    '<Test()> _
    'Public Sub ToStringTest()
    '    Dim target As Description = New Description ' TODO: Initialize to an appropriate value
    '    Dim expected As String = String.Empty ' TODO: Initialize to an appropriate value
    '    Dim actual As String
    '    actual = target.ToString
    '    Verify.AreEqual(expected, actual)
    '    Verify.Inconclusive("Verify the correctness of this test method.")
    'End Sub

    '''<summary>
    '''A test for Reset
    '''</summary>
    <Test()> _
    Public Sub ResetTest1()
        Dim expected As Integer = 10
        Dim target As Description = Description.Create("bottom:=""" & expected.ToString() & """")
        target.Reset()
        Verify.AreNotEqual(expected, target.Bottom)
    End Sub

    ''''<summary>
    ''''A test for Remove
    ''''</summary>
    '<Test()> _
    'Public Sub RemoveTest2()
    '    Dim expected As Integer = 10
    '    Dim target As Description = Description.Create("bottom:=""" & expected.ToString() & """")
    '    Dim Type As Description.DescriptionData = New UIControls.Description.DescriptionData.Bottom ' TODO: Initialize to an appropriate value
    '    target.Remove(Type)
    '    Verify.Inconclusive("A method that does not return a value cannot be verified.")
    'End Sub

    '''<summary>
    '''A test for Remove
    '''</summary>
    <Test()> _
    Public Sub RemoveTest1()
        Dim expected As Integer = 10
        Dim target As Description = Description.Create("bottom:=""" & expected.ToString() & """")
        Dim Type As String = "Bottom" ' TODO: Initialize to an appropriate value
        target.Remove(Type)
        Verify.AreNotEqual(expected, target.Bottom)
    End Sub

    ''''<summary>
    ''''A test for ReadDescription
    ''''</summary>
    '<Test(), _
    ' DeploymentItem("APIControls.dll")> _
    'Public Sub ReadDescriptionTest()
    '    Dim target As APIControls.Description_Accessor = New APIControls.Description_Accessor ' TODO: Initialize to an appropriate value
    '    Dim desc As String = String.Empty ' TODO: Initialize to an appropriate value
    '    Dim expected As Boolean = False ' TODO: Initialize to an appropriate value
    '    Dim actual As Boolean
    '    actual = target.ReadDescription(desc)
    '    Verify.AreEqual(expected, actual)
    '    Verify.Inconclusive("Verify the correctness of this test method.")
    'End Sub

    ''''<summary>
    ''''A test for Numeric
    ''''</summary>
    '<Test(), _
    ' DeploymentItem("APIControls.dll")> _
    'Public Sub NumericTest()
    '    Dim str As String = String.Empty ' TODO: Initialize to an appropriate value
    '    Dim expected As Integer = 0 ' TODO: Initialize to an appropriate value
    '    Dim actual As Integer
    '    actual = APIControls.Description_Accessor.Numeric(str)
    '    Verify.AreEqual(expected, actual)
    '    Verify.Inconclusive("Verify the correctness of this test method.")
    'End Sub

    ''''<summary>
    ''''A test for IsBool
    ''''</summary>
    '<Test(), _
    ' DeploymentItem("APIControls.dll")> _
    'Public Sub IsBoolTest()
    '    Dim target As APIControls.Description_Accessor = New APIControls.Description_Accessor ' TODO: Initialize to an appropriate value
    '    Dim str As String = String.Empty ' TODO: Initialize to an appropriate value
    '    Dim expected As Boolean = False ' TODO: Initialize to an appropriate value
    '    Dim actual As Boolean
    '    actual = target.IsBool(str)
    '    Verify.AreEqual(expected, actual)
    '    Verify.Inconclusive("Verify the correctness of this test method.")
    'End Sub

    '''<summary>
    '''A test for GetItemValue
    '''</summary>
    <Test()> _
    Public Sub GetItemValueTest2()
        Dim expected As Integer = 10
        Dim target As Description = Description.Create("bottom:=""" & expected.ToString() & """")
        Dim ItemType As String = "bottom" ' TODO: Initialize to an appropriate value
        Dim actual As String
        actual = target.GetItemValue(ItemType)
        Verify.AreEqual(expected.ToString(), actual)
    End Sub

    ''''<summary>
    ''''A test for GetItemValue
    ''''</summary>
    '<Test()> _
    'Public Sub GetItemValueTest1()
    '    Dim target As Description = New Description ' TODO: Initialize to an appropriate value
    '    Dim ItemType As Description.DescriptionData = New Description.DescriptionData ' TODO: Initialize to an appropriate value
    '    Dim expected As String = String.Empty ' TODO: Initialize to an appropriate value
    '    Dim actual As String
    '    actual = target.GetItemValue(ItemType)
    '    Verify.AreEqual(expected, actual)
    '    Verify.Inconclusive("Verify the correctness of this test method.")
    'End Sub

    '''<summary>
    '''A test for GetItemName
    '''</summary>
    <Test()> _
    Public Sub GetItemNameTest()
        Dim expected As Integer = 10
        Dim target As Description = Description.Create("bottom:=""" & expected.ToString() & """")
        Dim EnumedItem As Description.DescriptionData = Description.DescriptionData.Bottom ' TODO: Initialize to an appropriate value
        Dim actual As String
        actual = target.GetItemName(EnumedItem)
        Verify.AreEqual("bottom", actual)
    End Sub

    '''<summary>
    '''A test for GetItemEnum
    '''</summary>
    <Test()> _
    Public Sub GetItemEnumTest()
        Dim target As Description = Description.Create("bottom:=""" & "3" & """")
        Dim ItemType As String = "bottom"
        Dim expected As Description.DescriptionData = Description.DescriptionData.Bottom
        Dim actual As Description.DescriptionData
        actual = target.GetItemEnum(ItemType)
        Verify.AreEqual(expected, actual)
    End Sub

    ''''<summary>
    ''''A test for GetDescriptionItems
    ''''</summary>
    '<Test()> _
    'Public Sub GetDescriptionItemsTest1()
    '    Dim target As Description = Description.Create("bottom:=""" & "3" & """")
    '    Dim expected As StringDictionary = Nothing ' TODO: Initialize to an appropriate value
    '    Dim actual As StringDictionary
    '    actual = target.GetDescriptionItems
    '    Verify.AreEqual(expected, actual)
    '    Verify.Inconclusive("Verify the correctness of this test method.")
    'End Sub

    ''''<summary>
    ''''A test for GetBool
    ''''</summary>
    '<Test(), _
    ' DeploymentItem("APIControls.dll")> _
    'Public Sub GetBoolTest()
    '    Dim target As APIControls.Description_Accessor = New APIControls.Description_Accessor ' TODO: Initialize to an appropriate value
    '    Dim str As String = String.Empty ' TODO: Initialize to an appropriate value
    '    Dim expected As Boolean = False ' TODO: Initialize to an appropriate value
    '    Dim actual As Boolean
    '    actual = target.GetBool(str)
    '    Verify.AreEqual(expected, actual)
    '    Verify.Inconclusive("Verify the correctness of this test method.")
    'End Sub

    '''<summary>
    '''A test for Count
    '''</summary>
    <Test()> _
    Public Sub CountTest()
        Dim expected As Integer = 1
        Dim target As Description = Description.Create("bottom:=""" & "1" & """")
        Dim actual As Integer
        actual = target.Count
        Verify.AreEqual(expected, actual)
    End Sub

    '''<summary>
    '''A test for Contains
    '''</summary>
    <Test()> _
    Public Sub ContainsTest1()
        Dim target As UIControls.Description = Description.Create("bottom:=""" & "1" & """")
        Dim Type As Description.DescriptionData = Description.DescriptionData.Bottom
        Dim expected As Boolean = True
        Dim actual As Boolean
        actual = target.Contains(Type)
        Verify.AreEqual(expected, actual)
    End Sub

    '''<summary>
    '''A test for Contains
    '''</summary>
    <Test()> _
    Public Sub ContainsTest()
        Dim target As Description = Description.Create("bottom:=""" & "1" & """")
        Dim Type As String = "bottom"
        Dim expected As Boolean = True
        Dim actual As Boolean
        actual = target.Contains(Type)
        Verify.AreEqual(expected, actual)
    End Sub

    '''<summary>
    '''A test for Add
    '''</summary>
    <Test()> _
    Public Sub AddTest2()
        Dim target As Description = Description.Create("bottom:=""" & "1" & """")
        Dim Type As String = "name"
        Dim Value As String = "hello!@#%$#%&46"
        target.Add(Type, Value)
        Verify.AreEqual(Value, target.Name)
    End Sub

    '''<summary>
    '''A test for Add
    '''</summary>
    <Test()> _
    Public Sub AddTest1()
        Dim target As Description = Description.Create("bottom:=""" & "1" & """")
        Dim Type As Description.DescriptionData = Description.DescriptionData.Index
        Dim Value As String = "3"
        target.Add(Type, Value)
        Verify.AreEqual(Value, target.Index.ToString())
    End Sub

    '''<summary>
    '''A test for Description Constructor
    '''</summary>
    <Test()> _
    Public Sub DescriptionConstructorTest5()
        Dim target As Description = Description.Create()
    End Sub

    '''<summary>
    '''A test for Description Constructor
    '''</summary>
    <Test()> _
    Public Sub DescriptionConstructorTest4()
        Dim DescriptionValue As String = "bottom:=""" & "1" & """"
        Dim IsWildCard As Boolean = True
        Dim target As Description = Description.Create(DescriptionValue, IsWildCard)
    End Sub

    '''<summary>
    '''A test for Description Constructor
    '''</summary>
    <Test()> _
    Public Sub DescriptionConstructorTest3()
        Dim DescriptionValue As String = "bottom:=""" & "1" & """"
        Dim target As Description = Description.Create(DescriptionValue)
    End Sub

    '''<summary>
    '''A test for WildCard
    '''</summary>
    <Test()> _
    Public Sub WildCardTest()
        Dim target As Description = Description.Create()
        Dim expected As Boolean = True
        Dim actual As Boolean
        target.WildCard = expected
        actual = target.WildCard
        Verify.AreEqual(expected, actual)
    End Sub

    '''<summary>
    '''A test for Reset
    '''</summary>
    <Test()> _
    Public Sub ResetTest()
        Dim target As APIControls.Description = New APIControls.Description
        target.Add("Name", "test")
        target.Reset()
        Verify.AreEqual(Nothing, target.GetItemValue("Name"))
    End Sub

    '''<summary>
    '''A test for Remove
    '''</summary>
    <Test()> _
    Public Sub RemoveTest()
        Dim target As APIControls.Description = New APIControls.Description
        Dim Type As String = "bottom" ' TODO: Initialize to an appropriate value
        target.Add(Type, "12")
        target.Remove(Type)
        Dim value As String = ""
        Try
            value = target.GetItemValue(Type)
        Catch ex As Exception
        End Try
        Verify.AreEqual("-1", value)
    End Sub

    '''<summary>
    '''A test for GetItemValue
    '''</summary>
    <Test()> _
    Public Sub GetItemValueTest()
        Dim target As APIControls.Description = New APIControls.Description
        Dim ItemType As String = "bottom"
        Dim expected As String = "-1"
        Dim actual As String
        actual = target.GetItemValue(ItemType)
        Verify.AreEqual(expected, actual)
    End Sub

    '''<summary>
    '''A test for GetDescriptionItems
    '''</summary>
    <Test()> _
    Public Sub GetDescriptionItemsTest()
        Dim target As APIControls.Description = New APIControls.Description
        Dim expected As New StringDictionary
        Dim actual As StringDictionary
        actual = target.GetDescriptionItems
        Verify.AreEqual(expected.ToString(), actual.ToString())
    End Sub

    '''<summary>
    '''A test for Create
    '''</summary>
    <Test()> _
    Public Sub CreateTest2()
        Dim DescriptionValue As String = "bottom:=""" & "1" & """"
        Dim expected As Description = Description.Create(DescriptionValue)
        Dim actual As Description
        actual = Description.Create(DescriptionValue)
        Verify.AreEqual(expected.ToString(), actual.ToString())
    End Sub

End Class
