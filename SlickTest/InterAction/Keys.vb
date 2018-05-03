
'===========================================================
' Name:  Class Keys
' Purpose:
' Functions:
' Properties:
' Methods:
' Author:
' Start:
' Modified:
'===========================================================
Public Class Keys

'===========================================================
' Name: Public Function SendKeys
' Input: 
'   ByVal  InputToType As String
' Output:  As Boolean
' Purpose:
' Remarks:
'   Example Code: 
'===========================================================
    Public Shared Function SendKeys(ByVal InputToType As String) As Boolean
        If (InputToType.Length >= 1) Then
            Try
                System.Windows.Forms.SendKeys.SendWait(InputToType)
                Return True
            Catch ex As Exception

            End Try
        End If
        Return False
    End Function

End Class
