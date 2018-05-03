'Namespace My
'    Partial Friend Class MyApplication

'        Private Sub MyApplication_StartupNextInstance(ByVal sender As Object, ByVal e As Microsoft.VisualBasic.ApplicationServices.StartupNextInstanceEventArgs) Handles Me.StartupNextInstance
'            Try
'                If TypeOf Me.MainForm Is SlickTestDev Then
'                    Dim Args(e.CommandLine.Count) As String
'                    e.CommandLine.CopyTo(Args, 0)
'                    DirectCast(Me.MainForm, SlickTestDev).HandleArgs(Args)
'                End If
'            Catch ex As Exception
'                Dim str As String = ex.InnerException.ToString() & vbNewLine & _
'                ex.Message.ToString() & vbNewLine & ex.Source.ToString() & vbNewLine & _
'                ex.StackTrace.ToString() & vbNewLine & ex.TargetSite.ToString()

'                MessageBox.Show("Error String: " & str & vbNewLine & "Exception: " & ex.ToString)
'            End Try
'        End Sub

'        <Global.System.Diagnostics.DebuggerStepThroughAttribute()> _
'        Public Sub New()
'            MyBase.New(Global.Microsoft.VisualBasic.ApplicationServices.AuthenticationMode.Windows)
'            Me.IsSingleInstance = True
'            Me.ShutdownStyle = Global.Microsoft.VisualBasic.ApplicationServices.ShutdownMode.AfterMainFormCloses
'        End Sub

'        <Global.System.Diagnostics.DebuggerStepThroughAttribute()> _
'        Protected Overrides Sub OnCreateMainForm()
'            Me.MainForm = Global.SlickTestDeveloper.SlickTestDev
'        End Sub
'    End Class
'End Namespace
