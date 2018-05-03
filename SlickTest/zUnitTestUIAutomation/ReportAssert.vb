Public Class Verify

    Public Shared Sub IsWithin(ByVal expectedStart As Long, ByVal expectedEnd As Long, ByVal actual As Long, Optional ByVal msg As String = "")
        DebugInfo("IsWithin", Convert.ToString(expectedStart) & " " & Convert.ToString(expectedEnd), actual)
        Dim success As Boolean = False
        If (expectedStart <= actual) Then
            If (expectedEnd >= actual) Then
                success = True
            End If
        End If
        If (success) Then Return
        Console.WriteLine(msg)
        Throw New Exception("Tested if value " & actual & " was within values " & expectedStart & " and " & expectedEnd)
    End Sub

    Public Shared Sub AreEqual(ByVal expected As Object, ByVal actual As Object, Optional ByVal msg As String = "")
        DebugInfo("AreEqual", expected, actual)
        NUnit.Framework.Assert.AreEqual(expected, actual, msg)
    End Sub

    Public Shared Sub AreNotEqual(ByVal expected As Object, ByVal actual As Object, Optional ByVal msg As String = "")
        DebugInfo("AreNotEqual", expected, actual)
        NUnit.Framework.Assert.AreNotEqual(expected, actual, msg)
    End Sub

    Public Shared Sub IsTrue(ByVal value As Boolean, Optional ByVal msg As String = "")
        DebugInfo("IsTrue", value & " - msg: " & msg)
        NUnit.Framework.Assert.IsTrue(value, msg)
    End Sub

    Public Shared Sub IsFalse(ByVal value As Boolean)
        DebugInfo("IsFalse", value)
        NUnit.Framework.Assert.IsFalse(value)
    End Sub

    Public Shared Sub IsNotEmpty(ByVal value As String)
        DebugInfo("IsNotEmpty", value)
        NUnit.Framework.Assert.IsNotEmpty(value)
    End Sub

    Public Shared Sub IsNotNull(ByVal value As Object)
        DebugInfo("IsNotNull", value)
        NUnit.Framework.Assert.IsNotNull(value)
    End Sub

    Public Shared Sub IsNull(ByVal value As Object)
        DebugInfo("IsNull", value)
        NUnit.Framework.Assert.IsNull(value)
    End Sub

    Public Shared Sub Contains(ByVal expected As String, ByVal actual As String)
        DebugInfo("Contains", expected, actual)
        NUnit.Framework.StringAssert.Contains(expected, actual)
    End Sub

    Public Shared Sub DoesNotContain(ByVal expected As String, ByVal actual As String)
        DebugInfo("DoesNotContain", expected, actual)
        NUnit.Framework.StringAssert.DoesNotContain(expected, actual)
    End Sub

    Public Shared Sub Fail(ByVal value As String)
        DebugInfo("Fail", value)
        NUnit.Framework.Assert.Fail(value)
    End Sub

    Private Shared Sub DebugInfo(ByVal comparetype As String, ByVal expected As Object, ByVal actual As Object)
        Try

            Console.WriteLine("Comparing with method: " & comparetype)
            Console.WriteLine("Expected: " & Convert.ToString(expected))
            Console.WriteLine("  Actual: " & Convert.ToString(actual))
        Catch ex As Exception

        End Try

    End Sub

    Private Shared Sub DebugInfo(ByVal comparetype As String, ByVal value As Object)
        Try
            Console.WriteLine("Comparing with method: " & comparetype)
            Console.WriteLine("Value: " & Convert.ToString(value))
        Catch ex As Exception

        End Try

    End Sub

End Class
