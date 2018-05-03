Option Strict On
''' <summary>
''' Alerts are messageboxes that provide the user the ability to respond to a query within a given time
''' or with no time limit.  They also allow you to both provide the user information while logging the data.
''' </summary>
''' <remarks></remarks>
Public Class Alert
    Protected Friend Sub New()
    End Sub


    ''' <summary>
    ''' Shows an alert that provides a timeout, and allows users to automatically log the data.
    ''' </summary>
    ''' <param name="Message">The message you wish to display.</param>
    ''' <param name="Title">The Alert title. Default: "Slick Test Developer"</param>
    ''' <param name="LogInfo">Should this information be logged using 
    ''' UIControls.Log.LogData? Default: True</param>
    ''' <param name="TimeOut">Time out before the alert disappears in seconds.  Set to -1 if you require
    ''' the user to press a button.  If this is set to -1, then one of the buttons must be pressed.
    ''' Default: 20 seconds</param>
    ''' <param name="Buttons">One of the button sets found in System.Windows.Forms.MessageBoxButtons.  
    ''' Default: MessageBoxButtons.OK</param>
    ''' <param name="Icon">One of the window icons found in System.Windows.Forms.MessageBoxIcon.
    '''   Default: MessageBoxIcon.None</param>
    ''' <param name="DefaultButton">The default button selected, based upon a value from 
    ''' System.Windows.Forms.MessageBoxDefaultButton.  Default: 
    ''' MessageBoxDefaultButton.Button1</param>
    ''' <param name="ShortcutsEnabled">Should links be clickable?  Default: True</param>
    ''' <returns>Returns  Windows.Forms.DialogResult.Cancel if user does not respond 
    ''' to the GUI before the timeout, otherwise, it returns the 
    ''' Windows.Forms.DialogResult the user gave.</returns>
    ''' <remarks>
    ''' </remarks>
    Public Shared Function Show(ByVal Message As String, _
                                Optional ByVal TimeOut As Integer = 20, _
                                Optional ByVal Title As String = "Slick Test Developer", _
                                Optional ByVal LogInfo As Boolean = True, _
                                Optional ByVal Buttons As System.Windows.Forms.MessageBoxButtons = System.Windows.Forms.MessageBoxButtons.OK, _
                                Optional ByVal Icon As System.Windows.Forms.MessageBoxIcon = System.Windows.Forms.MessageBoxIcon.None, _
                                Optional ByVal DefaultButton As System.Windows.Forms.MessageBoxDefaultButton = System.Windows.Forms.MessageBoxDefaultButton.Button1, _
                                Optional ByVal ShortcutsEnabled As Boolean = True) As System.Windows.Forms.DialogResult


        If (LogInfo = True) Then UIControls.Log.Log("Alert: " & Message)

        Dim p As New InternalMessageBox
        p.Shortcuts = ShortcutsEnabled
        p.Message = Message
        p.Text = Title
        p.Buttons = Buttons
        p.Icon = Icon
        p.DefaultButton = DefaultButton
        p.TimeOut = TimeOut
        Dim Res As System.Windows.Forms.DialogResult = p.ShowDialog()
        If (LogInfo = True) Then UIControls.Log.Log("Alert Result: " & Res.ToString())
        Return Res
    End Function

    ''' <summary>
    ''' Class that actualy contains all the code for the messagebox, but should not be used directly.
    ''' </summary>
    ''' <remarks></remarks>
    Private Class InternalMessageBox
        Inherits System.Windows.Forms.Form

#Region " Windows Form Designer generated code "

        Public Sub New()
            MyBase.New()

            'This call is required by the Windows Form Designer.
            InitializeComponent()

            'Add any initialization after the InitializeComponent() call

        End Sub

        'Form overrides dispose to clean up the component list.
        Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
            If disposing Then
                If Not (components Is Nothing) Then
                    components.Dispose()
                End If
            End If
            MyBase.Dispose(disposing)
        End Sub

        'Required by the Windows Form Designer
        Private components As System.ComponentModel.IContainer

        'NOTE: The following procedure is required by the Windows Form Designer
        'It can be modified using the Windows Form Designer.  
        'Do not modify it using the code editor.
        Friend WithEvents Timer1 As System.Windows.Forms.Timer
        Friend WithEvents lblTimer As System.Windows.Forms.Label
        Friend WithEvents Button1 As System.Windows.Forms.Button
        Friend WithEvents Button2 As System.Windows.Forms.Button
        Friend WithEvents Button3 As System.Windows.Forms.Button
        Friend WithEvents RichTextBox1 As System.Windows.Forms.RichTextBox
        Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
        <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
            Me.components = New System.ComponentModel.Container
            Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
            Me.lblTimer = New System.Windows.Forms.Label
            Me.Button1 = New System.Windows.Forms.Button
            Me.Button2 = New System.Windows.Forms.Button
            Me.Button3 = New System.Windows.Forms.Button
            Me.RichTextBox1 = New System.Windows.Forms.RichTextBox
            Me.PictureBox1 = New System.Windows.Forms.PictureBox
            CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.SuspendLayout()
            '
            'Timer1
            '
            '
            'lblTimer
            '
            Me.lblTimer.Dock = System.Windows.Forms.DockStyle.Bottom
            Me.lblTimer.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.lblTimer.Location = New System.Drawing.Point(0, 120)
            Me.lblTimer.Name = "lblTimer"
            Me.lblTimer.Size = New System.Drawing.Size(280, 24)
            Me.lblTimer.TabIndex = 5
            Me.lblTimer.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
            '
            'Button1
            '
            Me.Button1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.Button1.Location = New System.Drawing.Point(40, 96)
            Me.Button1.Name = "Button1"
            Me.Button1.Size = New System.Drawing.Size(74, 22)
            Me.Button1.TabIndex = 2
            Me.Button1.Text = "Button1"
            '
            'Button2
            '
            Me.Button2.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.Button2.Location = New System.Drawing.Point(120, 96)
            Me.Button2.Name = "Button2"
            Me.Button2.Size = New System.Drawing.Size(74, 22)
            Me.Button2.TabIndex = 3
            Me.Button2.Text = "Button2"
            '
            'Button3
            '
            Me.Button3.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.Button3.Location = New System.Drawing.Point(200, 96)
            Me.Button3.Name = "Button3"
            Me.Button3.Size = New System.Drawing.Size(74, 22)
            Me.Button3.TabIndex = 4
            Me.Button3.Text = "Button3"
            '
            'RichTextBox1
            '
            Me.RichTextBox1.BackColor = System.Drawing.SystemColors.Control
            Me.RichTextBox1.BorderStyle = System.Windows.Forms.BorderStyle.None
            Me.RichTextBox1.Location = New System.Drawing.Point(48, 8)
            Me.RichTextBox1.Name = "RichTextBox1"
            Me.RichTextBox1.ReadOnly = True
            Me.RichTextBox1.Size = New System.Drawing.Size(224, 67)
            Me.RichTextBox1.TabIndex = 0
            Me.RichTextBox1.Text = "RichTextBox1"
            '
            'PictureBox1
            '
            Me.PictureBox1.Location = New System.Drawing.Point(8, 8)
            Me.PictureBox1.Name = "PictureBox1"
            Me.PictureBox1.Size = New System.Drawing.Size(32, 32)
            Me.PictureBox1.TabIndex = 6
            Me.PictureBox1.TabStop = False
            '
            'fMessageBox
            '
            Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
            Me.ClientSize = New System.Drawing.Size(280, 144)
            Me.Controls.Add(Me.PictureBox1)
            Me.Controls.Add(Me.RichTextBox1)
            Me.Controls.Add(Me.Button3)
            Me.Controls.Add(Me.Button2)
            Me.Controls.Add(Me.Button1)
            Me.Controls.Add(Me.lblTimer)
            Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog

            Me.Name = "InternalMessageBox"
            Me.Text = "InternalMessageBox"
            Me.TopMost = True
            CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
            Me.ResumeLayout(False)

        End Sub

#End Region

#Region "Members"

        Public Enum enuLanguage
            English = 0
            French = 1
        End Enum

        Private mButtons As System.Windows.Forms.MessageBoxButtons
        Private mDefaultButton As System.Windows.Forms.MessageBoxDefaultButton
        Private mintLanguage As enuLanguage
        Private mintTimeOut As Integer

        Private Const LEFT_PADDING As Integer = 12
        Private Const RIGHT_PADDING As Integer = 12
        Private Const TOP_PADDING As Integer = 12
        Private Const ITEM_PADDING As Integer = 12
        Private Const BOTTOM_PADDING As Integer = 12


        Public WriteOnly Property Shortcuts() As Boolean
            Set(ByVal value As Boolean)
                Me.RichTextBox1.ShortcutsEnabled = value
            End Set
        End Property

#End Region

#Region "Properties"

        Public Property Buttons() As System.Windows.Forms.MessageBoxButtons
            Get
                Return mButtons
            End Get
            Set(ByVal Value As System.Windows.Forms.MessageBoxButtons)
                mButtons = Value
                Select Case Value
                    Case System.Windows.Forms.MessageBoxButtons.AbortRetryIgnore
                        With Button1
                            .Text = "Abort"
                            .DialogResult = System.Windows.Forms.DialogResult.Abort
                        End With
                        With Button2
                            .Text = "Retry"
                            .DialogResult = System.Windows.Forms.DialogResult.Retry
                        End With
                        With Button3
                            .Text = "Ignore"
                            .DialogResult = System.Windows.Forms.DialogResult.Ignore
                        End With

                    Case System.Windows.Forms.MessageBoxButtons.OK
                        With Button1
                            .Text = "OK"
                            .DialogResult = System.Windows.Forms.DialogResult.OK
                        End With
                        Button2.Visible = False
                        Button3.Visible = False

                    Case System.Windows.Forms.MessageBoxButtons.OKCancel
                        With Button1
                            .Text = "OK"
                            .DialogResult = System.Windows.Forms.DialogResult.OK
                        End With
                        With Button2
                            .Text = "Cancel"
                            .DialogResult = System.Windows.Forms.DialogResult.Cancel
                        End With
                        Button3.Visible = False

                    Case System.Windows.Forms.MessageBoxButtons.RetryCancel
                        With Button1
                            .Text = "Retry"
                            .DialogResult = System.Windows.Forms.DialogResult.Retry
                        End With
                        With Button2
                            .Text = "Cancel"
                            .DialogResult = System.Windows.Forms.DialogResult.Cancel
                        End With
                        Button3.Visible = False

                    Case System.Windows.Forms.MessageBoxButtons.YesNo
                        With Button1
                            .Text = "Yes"
                            .DialogResult = System.Windows.Forms.DialogResult.Yes
                        End With
                        With Button2
                            .Text = "No"
                            .DialogResult = System.Windows.Forms.DialogResult.No
                        End With
                        Button3.Visible = False

                    Case System.Windows.Forms.MessageBoxButtons.YesNoCancel
                        With Button1
                            .Text = "Yes"
                            .DialogResult = System.Windows.Forms.DialogResult.Yes
                        End With
                        With Button2
                            .Text = "No"
                            .DialogResult = System.Windows.Forms.DialogResult.No
                        End With
                        With Button3
                            .Text = "Cancel"
                            .DialogResult = System.Windows.Forms.DialogResult.Cancel
                        End With
                End Select
            End Set
        End Property

        'Keep the value of the default button (which will receive focus in the Load event)
        Public Property DefaultButton() As System.Windows.Forms.MessageBoxDefaultButton
            Get
                Return mDefaultButton
            End Get
            Set(ByVal Value As System.Windows.Forms.MessageBoxDefaultButton)
                mDefaultButton = Value
            End Set
        End Property

        'Set the image property
        Public Shadows WriteOnly Property Icon() As System.Windows.Forms.MessageBoxIcon
            Set(ByVal Value As System.Windows.Forms.MessageBoxIcon)
                Select Case Value
                    Case System.Windows.Forms.MessageBoxIcon.Asterisk, System.Windows.Forms.MessageBoxIcon.Information '64
                        PictureBox1.Image = System.Drawing.SystemIcons.Asterisk.ToBitmap
                    Case System.Windows.Forms.MessageBoxIcon.Error, System.Windows.Forms.MessageBoxIcon.Hand, System.Windows.Forms.MessageBoxIcon.Stop '16
                        PictureBox1.Image = System.Drawing.SystemIcons.Error.ToBitmap
                    Case System.Windows.Forms.MessageBoxIcon.Exclamation, System.Windows.Forms.MessageBoxIcon.Warning '48
                        PictureBox1.Image = System.Drawing.SystemIcons.Exclamation.ToBitmap
                    Case System.Windows.Forms.MessageBoxIcon.Question
                        PictureBox1.Image = System.Drawing.SystemIcons.Question.ToBitmap
                End Select
            End Set
        End Property

        Public Property Message() As String
            Get
                Return RichTextBox1.Text
            End Get
            Set(ByVal Value As String)
                RichTextBox1.Text = Value
            End Set
        End Property

        Public Property TimeOut() As Integer
            Get
                Return mintTimeOut
            End Get
            Set(ByVal Value As Integer)
                If Value >= 0 Then
                    mintTimeOut = Value
                    lblTimer.Text = "This message will disappear in " + mintTimeOut.ToString + " seconds."
                    Timer1.Interval = 1000
                    Timer1.Enabled = True
                Else
                    mintTimeOut = 0
                    lblTimer.Visible = False
                    Timer1.Enabled = False
                End If
            End Set
        End Property

#End Region

#Region "Methods"
        Private Function MeasureString(ByVal pStr As String, ByVal pMaxWidth As Integer, ByVal pfont As Drawing.Font) As Drawing.Size
            Dim g As Drawing.Graphics = Me.CreateGraphics()
            Dim strRectSizeF As Drawing.SizeF = g.MeasureString(pStr, pfont, pMaxWidth)
            g.Dispose()

            Return New Drawing.Size(Convert.ToInt32(Math.Ceiling(strRectSizeF.Width)), Convert.ToInt32(Math.Ceiling(strRectSizeF.Height)))
        End Function

        Private Sub SetFormSize()
            'The current width of the form
            Dim intCurrentWidth As Integer = Me.Width - Me.ClientSize.Width
            Dim intCurrentHeight As Integer = Me.Height - Me.ClientSize.Height
            'The width of the Messagebox (including its left padding)
            Dim intMessageRowWidth As Integer = RichTextBox1.Width + RichTextBox1.Left
            'The required size
            Dim intRequiredWidth As Integer = LEFT_PADDING + Math.Max(Me.Width, intMessageRowWidth) + RIGHT_PADDING + intCurrentWidth
            'Dim intRequiredHeight As Integer = TOP_PADDING + RichTextBox1.Height + ITEM_PADDING + chkDontShowAgain.Height + ITEM_PADDING + Button1.Height + BOTTOM_PADDING + intCurrentHeight
            Dim intRequiredHeight As Integer = TOP_PADDING + RichTextBox1.Height + ITEM_PADDING + ITEM_PADDING + Button1.Height + BOTTOM_PADDING + intCurrentHeight
            'The maximum width available (to be sure not to cover the whole screen
            Dim intMaxWidth As Integer = Convert.ToInt32(System.Windows.Forms.SystemInformation.WorkingArea.Width * 0.6)
            Dim intMaxHeight As Integer = Convert.ToInt32(System.Windows.Forms.SystemInformation.WorkingArea.Height * 0.9)

            'Fix the bug where if the message text is huge then the buttons are overwritten.
            'Incase the required height is more than the max height then adjust that in the message height
            If intRequiredHeight > intMaxHeight Then
                RichTextBox1.Height -= intRequiredHeight - intMaxHeight
            End If
            'Resize the form
            Me.Size = New Drawing.Size(Math.Min(intRequiredWidth, intMaxWidth), Math.Min(intRequiredHeight, intMaxHeight))
            'Recenter the form
            Me.Location = New Drawing.Point((System.Windows.Forms.SystemInformation.WorkingArea.Width - Me.Width) \ 2, (System.Windows.Forms.SystemInformation.WorkingArea.Height - Me.Height) \ 2)
        End Sub

        Private Sub SetMessageSize()
            If RichTextBox1.Text Is Nothing OrElse RichTextBox1.Text.Trim.Length = 0 Then
                RichTextBox1.Size = Drawing.Size.Empty
                RichTextBox1.Visible = False
            Else
                'Not to use the complete screen area
                Dim intMaxWidth As Integer = Convert.ToInt32(System.Windows.Forms.SystemInformation.WorkingArea.Width * 0.6)
                intMaxWidth -= RichTextBox1.Left - RIGHT_PADDING

                'We need to account for scroll bar width and height, otherwise for certains
                'kinds of text the scroll bar shows up unnecessarily
                intMaxWidth -= System.Windows.Forms.SystemInformation.VerticalScrollBarWidth
                Dim sizMessageRect As Drawing.Size = MeasureString(RichTextBox1.Text, intMaxWidth, Me.Font)
                sizMessageRect.Height += BOTTOM_PADDING

                RichTextBox1.Size = sizMessageRect
                RichTextBox1.Visible = True
            End If
        End Sub

#End Region

#Region "Events"

        Private Sub MessageBox_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
            Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow

            Select Case DefaultButton
                Case System.Windows.Forms.MessageBoxDefaultButton.Button1
                    Button1.Focus()
                Case System.Windows.Forms.MessageBoxDefaultButton.Button2
                    If Button2.Visible Then
                        Button2.Focus()
                    Else
                        Button1.Focus()
                    End If
                Case System.Windows.Forms.MessageBoxDefaultButton.Button3
                    If Button3.Visible Then
                        Button3.Focus()
                    Else
                        Button1.Focus()
                    End If
            End Select

            SetMessageSize()
            SetFormSize()
        End Sub

        Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
            mintTimeOut -= 1
            If mintTimeOut <= 0 Then
                Timer1.Enabled = False
                DialogResult = System.Windows.Forms.DialogResult.Cancel
            Else
                lblTimer.Text = "This message will disappear in " + mintTimeOut.ToString + " seconds."
            End If
        End Sub

        Private Sub RichTextBox1_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkClickedEventArgs) Handles RichTextBox1.LinkClicked
            System.Diagnostics.Process.Start(e.LinkText)
        End Sub

        Private Sub InternalMessageBox_MouseClick(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles Me.MouseClick
            Timer1.Enabled = False
        End Sub
#End Region

    End Class

End Class
