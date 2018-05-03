Imports NUnit.Framework
Imports UIControls


'''<summary>
'''This is a test class for IEWebBrowserTest and is intended
'''to contain all IEWebBrowserTest Unit Tests
'''</summary>
<TestFixture()> _
Public Class IEWebBrowserTest
    Inherits WebTests

    Private ReadOnly Property target() As IEWebBrowser
        Get
            Return WebBrowser
        End Get
    End Property

    '''<summary>
    '''A test for WaitTillCompleted
    '''</summary>
    <Test()> _
    Public Sub WaitTillCompletedTest()
        Dim Timeout As Integer = 0
        Dim expected As Boolean = True
        Dim actual As Boolean
        actual = target.WaitTillCompleted(Timeout)
        Verify.AreEqual(expected, actual)
    End Sub

    '''<summary>
    '''A test for SetURL
    '''</summary>
    <Test()> _
    Public Sub SetURLTest()
        Dim NewURL As String = "www.google.com"
        Dim expected As Boolean = True
        Dim actual As Boolean
        actual = target.SetURL(NewURL)
        Verify.AreEqual(expected, actual)
        target.Back()
    End Sub

    '''<summary>
    '''A test for SetURLNoWait
    '''</summary>
    <Test()> _
    Public Sub SetURLNoWaitTest()
        Dim NewURL As String = "www.google.com"
        Dim expected As Boolean = True
        target.SetURLNoWait(NewURL)
        System.Threading.Thread.Sleep(10000)
        Verify.Contains(NewURL, target.GetURL())
        target.Back()
    End Sub

    '''<summary>
    '''A test for Refresh
    '''</summary>
    <Test()> _
    Public Sub RefreshTest()
        Dim expected As Boolean = True
        Dim actual As Boolean
        actual = target.Refresh()
        Verify.AreEqual(expected, actual)
    End Sub

    '''<summary>
    '''A test for GetWebElementCount
    '''</summary>
    <Test()> _
    Public Sub GetWebElementCountTest()
        Dim expected As Integer = 0
        Dim actual As Integer
        actual = target.GetWebElementCount()
        Verify.AreNotEqual(expected, actual)
    End Sub

    '''<summary>
    '''A test for GoHome
    '''</summary>
    <Test()> _
    Public Sub GoHomeTest()
        Dim expected As Boolean = True
        Dim actual As Boolean
        actual = target.GoHome()
        Verify.AreEqual(expected, actual)
        target.Back()
    End Sub

    '''<summary>
    '''A test for GetURL
    '''</summary>
    <Test()> _
    Public Sub GetURLTest()
        Dim expected As String = "Table"
        Dim actual As String
        actual = target.GetURL()
        Verify.Contains(expected, actual)
    End Sub

    '''<summary>
    '''A test for GetState
    '''</summary>
    <Test()> _
    Public Sub GetStateTest()
        Dim expected As Byte = 0
        Dim actual As Byte
        actual = target.GetState()
        Verify.AreNotEqual(expected, actual) 'probably 4
    End Sub

    '''<summary>
    '''A test for GetPageText
    '''</summary>
    <Test()> _
    Public Sub GetPageTextTest()
        Dim expected As String = "row 1, cell 1"
        Dim actual As String
        actual = target.GetPageText()
        Verify.Contains(expected, actual)
    End Sub

    '''<summary>
    '''A test for GetPageHTML
    '''</summary>
    <Test()> _
    Public Sub GetPageHTMLTest()

        Dim expected As String = "row 1, cell 1"
        Dim actual As String
        actual = target.GetPageHTML()
        Verify.Contains(expected, actual)
    End Sub

    '''<summary>
    '''A test for Forward
    '''</summary>
    <Test()> _
    Public Sub ForwardTest()
        Dim expected As Boolean = False
        Dim actual As Boolean
        actual = target.Forward()
        Verify.AreEqual(expected, actual)
    End Sub

    '''<summary>
    '''A test for Close
    '''</summary>
    <Test()> _
    Public Sub CloseTest()
        Dim expected As Boolean = True
        Dim actual As Boolean
        actual = target.Close()
        Verify.AreEqual(expected, actual)
        System.Threading.Thread.Sleep(1000)
        Verify.AreEqual(False, target.Exists(0))
    End Sub

    '''<summary>
    '''A test for Back
    '''</summary>
    <Test()> _
    Public Sub BackTest()

        Dim expected As Boolean = False
        Dim actual As Boolean
        actual = target.Back()
        Verify.AreEqual(expected, actual)
    End Sub

    '''<summary>
    '''A test for GetWebDescriptionsWithFilter
    '''</summary>
    <Test()> _
    Public Sub GetWebDescriptionsWithFilterTest()

        Dim desc As UIControls.Description = UIControls.Description.Create()
        desc.Add(APIControls.Description.DescriptionData.WebTag, "Table")

        Dim actual As UIControls.Description()
        actual = target.GetWebDescriptionsWithFilter(desc)
        Verify.AreNotEqual(Nothing, actual)
        Verify.IsTrue(actual.Count > 0, "Verify count is greater than 0.  count: " & actual.Count)
        For Each d As UIControls.Description In actual
            Console.WriteLine(d.WebTag & " " & d.Index & " - " & d.WebText) 'verify descriptions are legit.
        Next
    End Sub

    '''<summary>
    '''A test for GetAllElementsLikeDescription
    '''</summary>
    <Test()> _
    Public Sub GetAllElementsLikeDescriptionTest()

        Dim desc As UIControls.Description = UIControls.Description.Create()
        desc.Add(APIControls.Description.DescriptionData.WebTag, "Table")
        target.WaitTillCompleted()
        System.Threading.Thread.Sleep(2000)

        Dim actual As UIControls.Description()
        actual = target.GetAllElementsLikeDescription(desc)
        Verify.AreNotEqual(Nothing, actual)
        For Each d As UIControls.Description In actual
            Console.WriteLine(d.WebTag & " " & d.Index & " - " & d.WebText) 'verify descriptions are legit.
        Next
        Verify.AreEqual(4, actual.Count) 'includes inner tables
    End Sub

    '''<summary>
    '''A test for GetNumberOfElementsLikeDescription
    '''</summary>
    <Test()> _
    Public Sub GetNumberOfElementsLikeDescriptionTest()

        Dim desc As UIControls.Description = UIControls.Description.Create()
        desc.Add(APIControls.Description.DescriptionData.WebTag, "Table")
        target.WaitTillCompleted()
        System.Threading.Thread.Sleep(2000)

        Dim actual As Integer
        actual = target.GetNumberOfElementsLikeDescription(desc)
        Verify.AreNotEqual(0, actual)
        Verify.AreEqual(4, actual) 'includes inner tables
    End Sub

End Class
