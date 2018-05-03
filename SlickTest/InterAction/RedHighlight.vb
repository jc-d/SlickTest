Imports System.Runtime.InteropServices
Imports System.Drawing
Imports System.Windows.Forms

Public Class RedHighlight
    Inherits System.Windows.Forms.Form

    Public PenColor As Color
    Public PenWidth As Integer
    Protected SpyHandle As IntPtr
    Protected rec As Rectangle
    Protected Friend cachedRectangle As Rectangle

#Region "Init"
    Public Sub New(ByVal SpyHandle As IntPtr)
        InitHighlighter()
        Me.SpyHandle = SpyHandle
    End Sub

    Public Sub New()
        InitHighlighter()
    End Sub
#End Region

#Region " API DECLARATIONS"

    <DllImport("user32", EntryPoint:="GetWindowRect")> _
    Protected Shared Function GetWindowRect(ByVal hwnd As IntPtr, ByVal lpRect As RECT) As Integer
    End Function
    <DllImport("user32", EntryPoint:="SetForegroundWindow")> _
    Public Shared Function SetForegroundWindow(ByVal hwnd As IntPtr) As Boolean
    End Function
    <DllImport("user32", EntryPoint:="BringWindowToTop")> _
    Public Shared Function BringWindowToTop(ByVal hwnd As IntPtr) As Boolean
    End Function

#Region " STRUCTURE "
    <StructLayout(LayoutKind.Sequential)> _
        Public Class RECT
        Public Left As Integer
        Public Top As Integer
        Public Right As Integer
        Public Bottom As Integer
    End Class

#End Region

#End Region

    Protected Friend Function GetRectangle(ByVal hwnd As IntPtr) As Rectangle
        Dim cRect As New RECT
        Dim rectangle As New Rectangle()
        GetWindowRect(hwnd, cRect)
        rectangle = rectangle.Empty
        rectangle.X = cRect.Left - 2
        rectangle.Y = cRect.Top - 2
        rectangle.Width = cRect.Right - cRect.Left + 3
        rectangle.Height = cRect.Bottom - cRect.Top + 3
        cachedRectangle = rectangle
        Return rectangle
    End Function

    Protected Friend Sub SetPlacement(ByVal rectangle As Rectangle)
        Me.Size = New System.Drawing.Size(rectangle.Width, rectangle.Height)
        'System.Console.WriteLine("Height: " & rectangle.Height & " Width: " & rectangle.Width)
        Me.Location = New System.Drawing.Point(rectangle.X, rectangle.Y)
        Me.Refresh()
    End Sub

    Protected Friend Overridable Sub DoDraw()
        Me.SetPlacement(Me.GetRectangle(Me.SpyHandle))
    End Sub


    Private Sub InitHighlighter()
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.ShowInTaskbar = False
        Me.BackColor = Color.Black
        Me.TransparencyKey = Color.Black
        Me.Size = New Size(0, 0)
        Me.Location = New Point(0, 0)
        Me.TopMost = True
        Me.WindowState = FormWindowState.Normal
        Me.Focus()
        PenColor = Color.Red
        PenWidth = 3
        rec = New Rectangle(1, 0, 0, 0)
    End Sub

    Private Sub LayerForm_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Me.Paint
        Dim p As New Pen(PenColor, PenWidth)
        e.Graphics.DrawRectangle(p, 0, 0, Me.Width, Me.Height)
        p.Dispose()
    End Sub
End Class
