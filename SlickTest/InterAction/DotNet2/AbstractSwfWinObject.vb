Imports System.Windows.Automation
''' <summary>
''' The Abstract Window is a list of all the abilities all object types have.
''' </summary>
''' <remarks>The Abstract System.Windows.Forms WinObject can't be created, but is the base
''' for all Slick Test Developer SWF Windows Objects.</remarks>
Public MustInherit Class AbstractSWFWindow
    Inherits AbstractWinObject
    Implements IAbstractWinObject
#Const IsAbs = 2 'set to 1 for abs position values

    ''' <summary>
    ''' Not used.
    ''' </summary>
    ''' <remarks></remarks>
    Protected Friend Sub New()
    End Sub

#Region "Dot net specific code"
    ''' <summary>
    ''' Returns the object's name.
    ''' </summary>
    ''' <value></value>
    ''' <returns>This will return the dotnet name of the window.</returns>
    ''' <remarks></remarks>
    Public Shadows ReadOnly Property Name() As String
        Get
            ExistsWithException()
            Return WindowsFunctions.GetDotNetClassName(New IntPtr(Me.Hwnd))
        End Get
    End Property
#End Region

End Class
