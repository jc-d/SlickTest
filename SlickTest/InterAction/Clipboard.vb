''' <summary>
''' Allows for direct access to the clipboard for text support.
''' </summary>
''' <remarks></remarks>
Public Class Clipboard

    ''' <summary>
    ''' Allows user to change the read/write format for the clipboard.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks>Defaults to ANSI text.</remarks>
    Public Property Format() As System.Windows.Forms.TextDataFormat
        Get
            Return DataFormat
        End Get
        Set(ByVal value As System.Windows.Forms.TextDataFormat)
            DataFormat = value
        End Set
    End Property

    Private DataFormat As System.Windows.Forms.TextDataFormat = Windows.Forms.TextDataFormat.Text

    ''' <summary>
    ''' Allows for direct access to the clipboard for basic text needs.
    ''' </summary>
    ''' <value>Text to place into the Clipboard</value>
    ''' <returns>Text in the Clipboard.  If there is no text in the 
    ''' clipboard, it returns a empty string.</returns>
    ''' <remarks>If additional clipboard access is required, use 
    ''' System.Windows.Forms.Clipboard</remarks>
    Public Property Text() As String
        Get
            If (System.Windows.Forms.Clipboard.ContainsText = True) Then
                Return System.Windows.Forms.Clipboard.GetText(Format)
            Else
                Return String.Empty
            End If
        End Get
        Set(ByVal value As String)
            System.Windows.Forms.Clipboard.SetText(value, Format)
        End Set
    End Property

    ''' <summary>
    ''' Clears the clipboard of all data.
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub Clear()
        System.Windows.Forms.Clipboard.Clear()
    End Sub

    Protected Friend Sub New()

    End Sub

End Class
