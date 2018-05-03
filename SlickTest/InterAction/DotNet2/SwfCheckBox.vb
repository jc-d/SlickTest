'Updated On: 10/18/09
''' <summary>
''' A SwfCheckBox is just a specialized SwfButton, and so it 
''' performs everything a SwfButton does.
''' </summary>
''' <remarks></remarks>
Public Class SwfCheckBox
    Inherits SwfButton

    Private Shared BS As New ButtonStyle()
    ''' <summary>
    ''' Check box state 'unchecked'
    ''' </summary>
    ''' <remarks></remarks>
    Public Const UNCHECKED As Integer = WinAPI.API.BST_UNCHECKED
    ''' <summary>
    ''' Check box state 'checked'
    ''' </summary>
    ''' <remarks></remarks>
    Public Const CHECKED As Integer = WinAPI.API.BST_CHECKED
    ''' <summary>
    ''' Check box state 'indeterminate' (3 state checkboxes)
    ''' </summary>
    ''' <remarks></remarks>
    Public Const INDETERMINATE As Integer = WinAPI.API.BST_INDETERMINATE

#Region "Constructors"
    Protected Friend Sub New(ByVal desc As String)
        MyBase.New(desc)
    End Sub
    Protected Friend Sub New(ByVal desc As APIControls.IDescription, ByVal descObj As System.Collections.Generic.List(Of APIControls.IDescription)) ', ByVal Values As System.Collections.Specialized.StringCollection, ByVal Names As System.Collections.Specialized.StringCollection)
        MyBase.New(desc, descObj)
    End Sub
    Protected Friend Sub New()
        MyBase.New()
        'this is a do nothing case, to ignore errors...
    End Sub
    Protected Friend Sub New(ByVal desc As APIControls.IDescription)
        MyBase.New(desc)
    End Sub
    Protected Friend Sub New(ByVal desc As System.Collections.Generic.List(Of APIControls.IDescription))
        MyBase.New(desc)
    End Sub
    Protected Friend Sub New(ByVal desc As String, ByVal descObj As System.Collections.Generic.List(Of APIControls.IDescription)) ', ByVal Values As System.Collections.Specialized.StringCollection, ByVal Names As System.Collections.Specialized.StringCollection)
        MyBase.New(desc, descObj)
    End Sub
    Protected Friend Sub New(ByVal win As SwfWindow)
        Me.description = win.description
        Me.currentHwnd = win.currentHwnd
        Me.reporter = win.reporter
        Me.currentRectangle = win.currentRectangle
    End Sub
#End Region

    ''' <summary>
    ''' This returns true if and only if the checkbox is checked.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetIsChecked() As Boolean
        If (UIControls.CheckBox.CHECKED = GetChecked()) Then
            Return True
        End If
        Return False
    End Function

    ''' <summary>
    ''' Returns true if the check box is a 3 state check box.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function Is3State() As Boolean
        ExistsWithException()
        'Try
        Return BS.ContainsValue(AbstractWinObject.WindowsFunctions.GetWindowsStyle(New IntPtr(Me.Hwnd)), BS.BS_3STATE)
        'Catch ex As Exception
        '
        'End Try
        'Return False
    End Function

    ''' <summary>
    ''' Returns true if the check box is a 3 state auto check box.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function Is3StateAuto() As Boolean
        ExistsWithException()
        Return BS.ContainsValue(AbstractWinObject.WindowsFunctions.GetWindowsStyle(New IntPtr(Me.Hwnd)), BS.BS_AUTO3STATE)
    End Function

    ''' <summary>
    ''' Returns the state of a check box.  The states are as follows:
    ''' Unchecked: 0<BR/>
    ''' Checked: 1<BR/>
    ''' Indeterminate: 2
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks>-1 is returned if the framework is unable to get a state.</remarks>
    Public Function GetChecked() As Integer
        ExistsWithException()
        Try
            Return WindowsFunctions.GetCheckBoxState(New IntPtr(Hwnd))
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    ''' <summary>
    ''' Returns the state of a check box as a string.  The states are as follows:
    ''' "unchecked"<BR/>
    ''' "checked"<BR/>
    ''' "indeterminate"
    ''' </summary>
    ''' <remarks>"unknown" is returned when the framework
    ''' is unable to get a state.</remarks>
    Public Function GetCheckedString() As String
        ExistsWithException()
        Try
            Dim i As Integer = WindowsFunctions.GetCheckBoxState(New IntPtr(Hwnd))
            If (i = UIControls.CheckBox.CHECKED) Then
                Return "checked"
            ElseIf (i = UIControls.CheckBox.UNCHECKED) Then
                Return "unchecked"
            ElseIf (i = UIControls.CheckBox.INDETERMINATE) Then
                Return "indeterminate"
            Else
                Return "unknown"
            End If
        Catch ex As Exception
        End Try
        Return "unknown"
    End Function

    ''' <summary>
    ''' Sets the state of a check box.
    ''' </summary>
    ''' <param name="state">Unchecked: 0<BR/>
    ''' Checked: 1<BR/>
    ''' Indeterminate: 2
    ''' </param>
    ''' <exception cref="Exception">Throws an exception if the state is invalid or if you attempt to set
    ''' the state to Indeterminate when the check box doesn't support that state.</exception>
    ''' <remarks></remarks>
    Public Sub SetChecked(ByVal State As Integer)
        ExistsWithException()
        Select Case State
            Case UIControls.CheckBox.CHECKED, UIControls.CheckBox.UNCHECKED
                Try
                    WindowsFunctions.SetCheckBoxState(New IntPtr(Me.Hwnd), State)
                Catch ex As Exception
                    Throw ex
                End Try
            Case UIControls.CheckBox.INDETERMINATE
                Try
                    If (BS.ContainsValue(AbstractWinObject.WindowsFunctions.GetWindowsStyle(New IntPtr(Me.Hwnd)), BS.BS_AUTO3STATE) = False) Then
                        If (BS.ContainsValue(AbstractWinObject.WindowsFunctions.GetWindowsStyle(New IntPtr(Me.Hwnd)), BS.BS_3STATE) = False) Then
                            Throw New SlickTestUIException("Undefined state only works with 3state or auto3state check boxes.")
                        End If
                    End If
                    WindowsFunctions.SetCheckBoxState(New IntPtr(Me.Hwnd), State)
                Catch ex As Exception
                    'Report(UIControls.Report.Fail, "Failed to perform requested action", _
                    '                "Error: " & ex.Message)
                    'If (IgnoreInternalErrors = False) Then
                    Throw ex
                    'End If
                    'Return False
                End Try
            Case Else
                Try
                    Throw New SlickTestUIException("Invalid check state value: " & State & ".")
                Catch ex As Exception
                    Throw ex
                End Try
        End Select
    End Sub

    ''' <summary>
    ''' Set's the checkbox to checked.
    ''' </summary>
    ''' <exception cref="Exception">Throws an exception if the state is invalid or if you attempt to set
    ''' the state to Indeterminate when the check box doesn't support that state.</exception>
    ''' <remarks></remarks>
    Public Sub Check()
        SetChecked(UIControls.CheckBox.CHECKED)
    End Sub

    ''' <summary>
    ''' Set's the checkbox to unchecked.
    ''' </summary>
    ''' <exception cref="Exception">Throws an exception if the state is invalid or if you attempt to set
    ''' the state to Indeterminate when the check box doesn't support that state.</exception>
    ''' <remarks></remarks>
    Public Sub UnCheck()
        SetChecked(UIControls.CheckBox.UNCHECKED)
    End Sub

    ''' <summary>
    ''' Toggles the state between checked and unchecked.  
    ''' </summary>
    ''' <remarks>If the state is unchecked, the state will become checked.
    ''' All other states will cause the check box to be unchecked.</remarks>
    Public Sub Toggle()
        Dim state As Integer = GetChecked()
        If (state = 0) Then
            SetChecked(UIControls.CheckBox.CHECKED)
        Else
            SetChecked(UIControls.CheckBox.UNCHECKED)
        End If
    End Sub

    ''' <summary>
    ''' Determines if the object is a CheckBox.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function IsCheckBox() As Boolean
        Try
            Return WindowsFunctions.Button.IsCheckBox(New IntPtr(Me.Hwnd()))
        Catch ex As Exception
            Throw ex
        End Try
        Return False
    End Function

End Class