Public Class SlickUnitException
	Inherits System.Exception
	Public Sub New(message As String)
		MyBase.New(message)
	End Sub
End Class

Public Class SlickUnitIgnoreException
	Inherits System.Exception
	Public Sub New(message As String)
		MyBase.New(message)
	End Sub
End Class