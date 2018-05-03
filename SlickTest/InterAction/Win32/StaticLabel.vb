''' <summary>
''' A StaticLabel is just a specialized WinObject, and so it 
''' performs everything a WinObject does.
''' </summary>
''' <remarks></remarks>
Public Class StaticLabel
    Inherits WinObject

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
    Protected Friend Sub New(ByVal win As Window)
        Me.description = win.description
        Me.currentHwnd = win.currentHwnd
        Me.reporter = win.reporter
        Me.currentRectangle = win.currentRectangle
    End Sub
#End Region

    ''' <summary>
    ''' Determines if the object is a StaticLabel.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function IsStaticLabel() As Boolean
        Return WindowsFunctions.StaticLabel.IsStaticLabel(Me.Hwnd())
    End Function

End Class
