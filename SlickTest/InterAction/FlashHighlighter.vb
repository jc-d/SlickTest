Imports System.Drawing

Friend Class FlashHighlighter
    Private Flashes As Integer
    Private TotalFlashes As Integer
    Private Highlight As UIControls.RedHighlight
    Private cachedHwnd As IntPtr
    Public Shared WindowsFunctions As New APIControls.IndependentWindowsFunctionsv1()

    Public Property IsCompleted() As Boolean
        Get
            Return InternalIsCompleted
        End Get
        Set(ByVal value As Boolean)
            InternalIsCompleted = value
        End Set
    End Property

    Private InternalIsCompleted As Boolean

    Private WithEvents Timer1 As New System.Windows.Forms.Timer()

    Public Function Start(ByVal Hwnd As IntPtr, Optional ByVal TotalFlashes As Integer = 5, Optional ByVal FlashSpeedInMS As Integer = 225) As Boolean
        IsCompleted = False
        Flashes = 0

        Me.TotalFlashes = TotalFlashes * 2 'double for the on and off
        cachedHwnd = Hwnd
        Highlight = New UIControls.RedHighlight(Hwnd)
        Dim rec As Rectangle = Highlight.GetRectangle(Hwnd)
        If (rec.X <= 0 OrElse rec.Y <= 0) Then
            Return False
        End If
        Highlight.Show()
        Highlight.SetPlacement(rec)
        Me.Timer1.Interval = FlashSpeedInMS
        Me.Timer1.Start()
        Return True
    End Function

    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        Flashes = Flashes + 1
        If (Flashes >= TotalFlashes) Then
            Timer1.Stop()
            Flashes = 0
            IsCompleted = True
        End If
        WindowsFunctions.AppActivateByHwnd(cachedHwnd)
        'UIControls.RedHighlight.SetForegroundWindow(cachedHwnd)
        UIControls.RedHighlight.BringWindowToTop(Me.Highlight.Handle)

        If ((Flashes Mod 2) = 0) Then
            Me.Highlight.Show()
        Else
            Hide()
        End If
    End Sub

    Public Sub Close()
        If (Not Highlight Is Nothing) Then
            Me.Highlight.Hide()
            Me.Highlight.Close()
            Me.Timer1.Stop()
            IsCompleted = True
        End If
    End Sub

    Public Sub Hide()
        If (Not Highlight Is Nothing) Then Me.Highlight.Hide()
    End Sub
End Class
