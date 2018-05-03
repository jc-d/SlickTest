Public Class Assert

	#Region "Properties"
	Protected Friend Shared InternalProvideInfo as Boolean = false
	Public Shared Property ProvideInfo As Boolean
		Get
			return InternalProvideInfo
		End Get
		Set
			InternalProvideInfo = value
		End Set
	End Property

	#End Region

	#Region "AreEqual"
	
	Public Shared Sub AreEqual(expected As Object, actual As Object)
		AreEqual(expected, actual, "")
	End Sub
	
	Public Shared Sub AreEqual(expected As Object, actual As Object, message as String)
		VerifyNulls(expected, actual, message)
		If(expected Is Nothing AndAlso actual Is Nothing) Then
			Assert.WriteInfo(Assert.CreateMessage(expected, actual, message, "Success!"))
			Return
		End If		
		VerifyType(expected, actual, message)
		If(expected.Equals(actual)) Then
			WriteInfo(CreateMessage(expected, actual, message, "Success!"))
			return			
		End If
		ThrowError(CreateMessage(expected, actual, message, "Values are not equal."))
	End Sub
	
	Public Shared Sub AreNotEqual(expected As Object, actual As Object)
		AreNotEqual(expected, actual, "")
	End Sub
	
	Public Shared Sub AreNotEqual(expected As Object, actual As Object, message as String)
		If(expected Is Nothing AndAlso actual Is Nothing) Then
			ThrowError(CreateMessage(expected, actual, message, "Both values are null."))
		End If

		If(Not expected Is actual) Then
			WriteInfo(CreateMessage(expected, actual, message, "Success!"))
			Return
		End If
		ThrowError(CreateMessage(expected, actual, message, "Values are equal."))
	End Sub
	#End Region
	
	#Region "AreSame"
	Public Shared Sub AreSame(expected As Object, actual As Object)
		AreSame(expected, actual, "")
	End Sub
		
	Public Shared Sub AreSame(expected As Object, actual As Object, message as String)
		VerifyNulls(expected, actual, message)
		If(expected Is Nothing AndAlso actual Is Nothing) Then
			Assert.WriteInfo(Assert.CreateMessage(expected, actual, message, "Success!"))
			Return
		End If
		VerifyType(expected, actual, message)
		If(expected = actual) Then
			WriteInfo(CreateMessage(expected, actual, message, "Success!"))
			return
		End If
		ThrowError(CreateMessage(expected, actual, message))
	End Sub
	
	Public Shared Sub AreNotSame(expected As Object, actual As Object)
		AreNotSame(expected, actual, "")
	End Sub
	
	Public Shared Sub AreNotSame(expected As Object, actual As Object, message as String)
		If(expected = actual) Then
			ThrowError(CreateMessage(expected, actual, message))
		End If
		WriteInfo(CreateMessage(expected, actual, message, "Success!"))
	End Sub
	#End Region

	#Region "Contains"
	Public Shared Sub Contains(searchFor As Object, searchIn As System.Collections.IList)
		Contains(searchFor,searchIn, "")
	End Sub
	
	Public Shared Sub Contains(searchFor As Object, searchIn As System.Collections.IList, message as String)
		For Each item As Object In searchIn
'TODO type
			If(searchFor.Equals(item)) Then
				AreEqual(searchFor, item, message)
				return
			End If
		Next
		ThrowError(CreateMessage(searchFor, message, "Searched list, but unable to find value."))
	End Sub
	#End Region
	
	#Region "Greater/Less"
	Public Shared Sub Greater(expected As IComparable, actual As IComparable)
		Greater(expected, actual, "")
	End Sub
		
	Public Shared Sub Greater(expected As IComparable, actual As IComparable, message As String)
		If(expected.compareto(actual)=1) Then
			WriteInfo(CreateMessage(expected, actual, message, expected.ToString() & " > " & actual.ToString()))
			return
		End If
		ThrowError(CreateMessage(expected, actual, message, expected.ToString() & " > " & actual.ToString()))
	End Sub
	
	Public Shared Sub Less(expected As IComparable, actual As IComparable)
		Less(expected, actual, "")
	End Sub
		
	Public Shared Sub Less(expected As IComparable, actual As IComparable, message As String)
		If(expected.CompareTo(actual)=-1) Then
			WriteInfo(CreateMessage(expected, actual, message, expected.ToString() & " < " & actual.ToString()))
			return
		End If
		ThrowError(CreateMessage(expected, actual, message, expected.ToString() & " < " & actual.ToString()))
	End Sub

	#End Region
	
	#Region "IsYYYY"
	Public Shared Sub IsTrue(condition As Boolean)
		IsTrue(condition, "")
	End Sub
		
	Public Shared Sub IsTrue(condition As Boolean, message As String)
		If(condition = true) Then
			WriteInfo(CreateMessage(condition, message, ""))
			return
		End If
		ThrowError(CreateMessage(condition, message, ""))
	End Sub
	
	Public Shared Sub IsFalse(condition As Boolean)
		IsFalse(condition, "")
	End Sub
		
	Public Shared Sub IsFalse(condition As Boolean, message As String)
		If(condition = false) Then
			WriteInfo(CreateMessage(condition, message, ""))
			return
		End If
		ThrowError(CreateMessage(condition, message, ""))
	End Sub

	Public Shared Sub IsNull(condition As Object)
		IsNull(condition, "")
	End Sub
		
	Public Shared Sub IsNull(condition As Object, message As String)
		If(condition is nothing) Then
			WriteInfo(CreateMessage(condition, message, ""))
			return
		End If
		ThrowError(CreateMessage(condition, message, ""))
	End Sub
	
	Public Shared Sub IsNotNull(condition As Object)
		IsNotNull(condition, "")
	End Sub
		
	Public Shared Sub IsNotNull(condition As Object, message As String)
		If(not condition is nothing) Then
			WriteInfo(CreateMessage(condition, message, ""))
			return
		End If
		ThrowError(CreateMessage(condition, message, ""))
	End Sub
	
	Public Shared Sub IsNaN(aDouble As Double)
		IsNaN(aDouble, "")
	End Sub
		
	Public Shared Sub IsNaN(aDouble As Double, message As String)
		If(Double.IsNaN(aDouble)) Then
			WriteInfo(CreateMessage(aDouble, message, ""))
			return
		End If
		ThrowError(CreateMessage(aDouble, message, ""))
	End Sub
	
	Public Shared Sub IsEmpty(aString As string)
		IsEmpty(aString, "")
	End Sub
		
	Public Shared Sub IsEmpty(aString As String, message As String)
		If(aString = String.Empty) Then
			WriteInfo(CreateMessage(aString, message, ""))
			return
		End If
		ThrowError(CreateMessage(aString, message, ""))
	End Sub
	
	Public Shared Sub IsNotEmpty(aString As string)
		IsNotEmpty(aString, "")
	End Sub
		
	Public Shared Sub IsNotEmpty(aString As String, message As String)
		If(aString <> String.Empty) Then
			WriteInfo(CreateMessage(aString, message, ""))
			return
		End If
		ThrowError(CreateMessage(aString, message, ""))
	End Sub
	
	Public Shared Sub IsNotEmpty(collection As ICollection)
		IsNotEmpty(collection, "")
	End Sub
		
	Public Shared Sub IsNotEmpty(collection As ICollection, message As String)
		If(not collection is nothing andalso collection.Count <> 0) Then
			WriteInfo(CreateMessage(collection, message, ""))
			return
		End If
		ThrowError(CreateMessage(collection, message, ""))
	End Sub
	
	Public Shared Sub IsEmpty(collection As ICollection)
		IsEmpty(collection, "")
	End Sub
		
	Public Shared Sub IsEmpty(collection As ICollection, message As String)
		If(collection is nothing orelse collection.Count = 0) Then
			WriteInfo(CreateMessage(collection, message, ""))
			return
		End If
		ThrowError(CreateMessage(collection, message, ""))
	End Sub
	#End Region

	#region "Helper Methods"
	Protected Friend Shared Sub WriteInfo(message As String)
		If(ProvideInfo) Then
			Console.WriteLine(message)
		End If
	End Sub
	
    Protected Friend Shared Sub VerifyType(ByVal expected As Object, ByVal actual As Object, ByVal message As String)
        Dim expectedTypeName As String = "Null"
        Dim actualTypeName As String = "Null"

        If (Not expected Is Nothing AndAlso Not actual Is Nothing) Then
            If (expected.GetType() Is actual.GetType()) Then
                Return
            End If
        Else
            If (Not expected Is Nothing) Then
                expectedTypeName = expected.GetType().Name
            End If
            If (Not actual Is Nothing) Then
                actualTypeName = actual.GetType().Name
            End If
        End If

        ThrowError(CreateMessage(expected, actual, message, _
        "Type of expected: " & expectedTypeName & vbNewLine & _
        "Type of actual: " & actualTypeName))
    End Sub
	
	Protected Friend Shared Sub VerifyNulls(expected As Object, actual As Object, message as String)
		If(expected Is Nothing AndAlso actual Is Nothing) Then Return
		If(expected Is Nothing) Then
			ThrowError(CreateMessage(expected, actual, message, "One of the values is null."))
		End If
		If(actual Is Nothing) Then
			ThrowError(CreateMessage(expected, actual, message, "One of the values is null."))
		End If		
	End Sub
	
	Protected Friend Shared Sub VerifyNulls(value As Object, message as String)
		If(value Is Nothing) Then
			ThrowError(CreateMessage(value, message, "Value is null."))
		End If
	End Sub
	
	Protected Friend Shared Sub ThrowError(message As String)
		throw new SlickUnitException(message)
	End Sub
	
	Protected Friend Shared function GetMethodNameStack() as String
		Dim st As System.Diagnostics.StackTrace = New StackTrace()
		Dim sf As System.Diagnostics.StackFrame = st.GetFrame(2)
		Dim mb As System.Reflection.MethodBase = sf.GetMethod()
		Return mb.Name
	End function

	Protected Friend Shared Function CreateMessage(ByVal expected as Object, ByVal actual as Object, ByVal  message As String, ByVal info as string) as String
		Dim msg As String = "Method: " & GetMethodNameStack() & VbNewLine
		If(not String.IsNullOrEmpty(info)) Then
				msg += "Info: " & info & vbNewLine
		End If
		If(not String.IsNullOrEmpty(message)) Then
			msg += "User Message: " & message & vbNewLine
		End If
		Dim printExpected As String = "Null"
		Dim printActual As String = "Null"
		
		If(not expected Is Nothing) Then
			printExpected = expected.ToString()
		End If
		
		If(not actual Is Nothing) Then
			printActual = actual.ToString()
		End If
		
		msg += "Expected: " & printExpected & vbNewLine & _
			  "Actual: " & printActual & vbNewLine

		Return msg
	End Function
	
	Protected Friend Shared Function CreateMessage(value as object, message As String, ByVal optional info as string = "") as String
		Dim msg As String = "Method: " & GetMethodNameStack() & VbNewLine
		
		If(not String.IsNullOrEmpty(info)) Then
			msg += "Info: " & info & vbNewLine
		End If
		If(not String.IsNullOrEmpty(message)) Then
			msg += "User Message: " & message & vbNewLine
		End If
		Dim printValue As String = "Null"
		
		If(not value Is Nothing) Then
			printValue = value.ToString()
		End If
		msg += "Value: " & printValue & vbNewLine
		
		Return msg
	End Function
	#End Region
	
	Public Shared Sub Fail(message As String)
		ThrowError(message)
	End Sub
	
	Public Shared Sub Ignore(message As String)
		throw new SlickUnitIgnoreException(message)
	End Sub
	

End Class