Public Class StringAssert
	
	#Region "Contains"
	Public Shared Sub Contains(searchFor As string, searchIn As string)
		Contains(searchFor,searchIn, "")
	End Sub
	
	Public Shared Sub Contains(searchFor As String, searchIn As String, message As String)
		If(searchFor Is Nothing OrElse searchIn Is Nothing) Then
			Assert.ThrowError(Assert.CreateMessage(searchFor, searchIn, message, "One or more values is null."))
		End If
		If(searchIn.Contains(searchFor)) Then
			Assert.WriteInfo(Assert.CreateMessage(searchFor, searchIn, message, "Success!"))
			return
		End If
		Assert.ThrowError(Assert.CreateMessage(searchFor, SearchIn, message, ""))
	End Sub
	#End Region
	
	#Region "StartsWith"
	Public Shared Sub StartsWith(searchFor As string, searchIn As string)
		StartsWith(searchFor,searchIn, "")
	End Sub
	
	Public Shared Sub StartsWith(searchFor As String, searchIn As String, message As String)
		If(searchFor Is Nothing OrElse searchIn Is Nothing) Then
			Assert.ThrowError(Assert.CreateMessage(searchFor, searchIn, message, "One or more values is null."))
		End If
		If(searchIn.StartsWith(searchFor)) Then
			Assert.WriteInfo(Assert.CreateMessage(searchFor, searchIn, message, "Success!"))
			return
		End If
		Assert.ThrowError(Assert.CreateMessage(searchFor, SearchIn, message, ""))
	End Sub
	#End Region

	#Region "EndsWith"
	Public Shared Sub EndsWith(searchFor As string, searchIn As string)
		EndsWith(searchFor,searchIn, "")
	End Sub
	
	Public Shared Sub EndsWith(searchFor As String, searchIn As String, message As String)
		If(searchFor Is Nothing OrElse searchIn Is Nothing) Then
			Assert.ThrowError(Assert.CreateMessage(searchFor, searchIn, message, "One or more values is null."))
		End If
		If(searchIn.EndsWith(searchFor)) Then
			Assert.WriteInfo(Assert.CreateMessage(searchFor, searchIn, message, "Success!"))
			return
		End If
		Assert.ThrowError(Assert.CreateMessage(searchFor, SearchIn, message, ""))
	End Sub
	#End Region
	
	#Region "AreEqualIgnoringCase"
	Public Shared Sub AreEqualIgnoringCase(expected As string, actual As string)
		AreEqualIgnoringCase(expected, actual, "")
	End Sub
	
	Public Shared Sub AreEqualIgnoringCase(expected As String, actual As String, message As String)
		Assert.VerifyNulls(expected, actual, message)
		If(expected Is Nothing AndAlso actual Is Nothing) Then
			Assert.WriteInfo(Assert.CreateMessage(expected, actual, message, "Success!"))
			Return
		End If
		
		If(expected.ToUpperInvariant().Equals(actual.ToUpperInvariant())) Then
			Assert.WriteInfo(Assert.CreateMessage(expected, actual, message, "Success!"))
			return
		End If
		Assert.ThrowError(Assert.CreateMessage(expected, actual, message, ""))
	End Sub
	#End Region

End Class
