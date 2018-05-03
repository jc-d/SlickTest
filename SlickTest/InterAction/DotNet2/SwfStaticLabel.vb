'Updated On: 8/23/08
''' <summary>
''' A SwfStaticLabel is just a specialized SwfWinObject, and so it 
''' performs everything a SwfWinObject does.
''' </summary>
''' <remarks></remarks>
Public Class SwfStaticLabel
    Inherits SwfWinObject

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


End Class
