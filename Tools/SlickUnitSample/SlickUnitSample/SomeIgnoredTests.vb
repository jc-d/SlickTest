<SlickUnit.TestFixture()> _
Public Class SomeIgnoredTests

    <SlickUnit.Ignore(), SlickUnit.Test()> _
    Public Sub IgnoredTest()
        Console.Write("Test was not ignored...")
    End Sub

    <SlickUnit.Test()> _
    Public Sub NotIgnoredTest()
        Console.WriteLine("Not an ignored tests.")
    End Sub

    <SlickUnit.Test()> _
    Public Sub AnotherNotIgnoredTest()
        Console.WriteLine("Another not an ignored tests.")
    End Sub
End Class
