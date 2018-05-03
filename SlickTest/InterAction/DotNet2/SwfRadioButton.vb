'Updated On: 8/23/08

''' <summary>
''' A SwfRadioButton is just a specialized SwfButton, and so it 
''' performs everything a SwfButton does.
''' </summary>
''' <remarks></remarks>
Public Class SwfRadioButton
    Inherits SwfButton


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
    ''' Returns the state of a radio button.
    ''' </summary>
    ''' <returns>Returns true if selected and false if unselected.</returns>
    ''' <remarks></remarks>
    Public Function GetSelected() As Boolean
        ExistsWithException()
        Try
            Dim i As Integer = WindowsFunctions.GetRadioButtonState(New IntPtr(Hwnd))
            If (i = 0) Then
                Return False
            Else
                Return True
            End If
        Catch ex As Exception
            Return False
        End Try
    End Function

    ''' <summary>
    ''' Sets the state of a RadioButton to selected.
    ''' </summary>
    ''' <remarks>Because a RadioButtons are not meant to always have
    ''' one item selected, users can only select a RadioButton, but
    ''' can't unselect a RadioButton.  In order to unselect a RadioButton
    ''' you must select another RadioButton.</remarks>
    Public Sub [Select]()
        Me.SmartClick()
        'If (ExistsWithException() = False) Then
        '    Return False
        'End If
        'If (State = False) Then
        '    WindowsFunctions.SetCheckBoxState(New IntPtr(Me.Hwnd), 0)
        'Else
        '    WindowsFunctions.SetCheckBoxState(New IntPtr(Me.Hwnd), 1)
        'End If

        'Return True
    End Sub

End Class
