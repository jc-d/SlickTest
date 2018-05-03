Imports WinAPI.API
''' <summary>
''' A TabControl is just a specialized WinObject, and so it 
''' performs everything a WinObject does.
''' </summary>
''' <remarks></remarks>
Public NotInheritable Class TabControl
    Inherits WinObject
    Private internalWindow As Window

    ''' <summary>
    ''' Gets the rectangle area for a specific tab. 
    ''' </summary>
    ''' <param name="index"></param>
    ''' <returns></returns>
    ''' <remarks>Currently failing.</remarks>
    Private Function GetTabRectangle(ByVal index As Integer) As System.Drawing.Rectangle
        Return WindowsFunctions.TabControl.GetRECT(Me.Hwnd(), index)
    End Function

#Region "Constructors"

    Protected Friend Sub New(ByVal win As Window)
        internalWindow = win
        Me.description = win.description
        Me.currentHwnd = win.currentHwnd
        Me.reporter = win.reporter
        Me.currentRectangle = win.currentRectangle
    End Sub

    Protected Friend Sub New()
        'this is a do nothing case, to ignore errors...
    End Sub

#End Region


    ''' <summary>
    ''' Selects a specific tab in within the tab control.
    ''' </summary>
    ''' <param name="index">The tab to select.</param>
    ''' <remarks>It is known that sometimes tabs do not refresh after
    ''' being selected.</remarks>
    Sub SelectTab(ByVal index As Integer)
        WindowsFunctions.TabControl.SelectTab(Me.Hwnd(), index)
    End Sub

    ''' <summary>
    ''' Gets the number of tabs in the TabControl.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetTabCount() As Integer
        Return WindowsFunctions.TabControl.GetTabCount(Me.Hwnd())
    End Function

    ''' <summary>
    ''' Gets the index of the selected tab.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetSelectedTab() As Integer
        Return WindowsFunctions.TabControl.GetSelectedTab(Me.Hwnd())
    End Function
    
    ''' <summary>
    ''' Determines if the object is a TabControl.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function IsTabControl() As Boolean
        Return WindowsFunctions.TabControl.IsTabControl(Me.Hwnd())
    End Function

End Class
