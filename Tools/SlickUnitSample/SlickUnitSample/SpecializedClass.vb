Imports SlickUnit

Namespace MyCustomNameSpace
    Namespace MyInnerNameSpace
        <TestFixture()> _
        Public Class SpecializedClass
            <Test()> _
            Public Sub SleepTest()
                Console.WriteLine("Just about to sleep for 15 seconds")
                System.Threading.Thread.Sleep(15000)
                Console.WriteLine("Sleeping done.")
            End Sub

        End Class
    End Namespace

End Namespace
