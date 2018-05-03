''' <summary>
''' The Abstract Window is a list of all the abilities all object types have.
''' </summary>
''' <remarks>The Abstract WinObject can't be created, but is the base
''' for all Slick Test Developer Windows Objects.</remarks>
Friend Interface IAbstractWinObject
    Function Exists(ByVal Time As Integer) As Boolean
    Function Exists() As Boolean
    Function WinObjectCount() As Integer
    Function WinObjectHwnds() As IntPtr()

    ReadOnly Property Hwnd() As Int64
    ReadOnly Property Left() As Integer
    ReadOnly Property Right() As Integer
    ReadOnly Property Top() As Integer
    ReadOnly Property Bottom() As Integer
    ReadOnly Property Width() As Integer
    ReadOnly Property Height() As Integer
    ReadOnly Property Name() As String

    Function IsObjectAtAbsLocation(ByVal X As Integer, ByVal Y As Integer) As Boolean

    Sub Click(ByVal X As Integer, ByVal Y As Integer)
    Sub RightClick(ByVal X As Integer, ByVal Y As Integer)
    Sub Click()
    Sub RightClick()
    Sub MouseDown(ByVal X As Integer, ByVal Y As Integer, ByVal Button As Integer)
    Sub MouseUp(ByVal X As Integer, ByVal Y As Integer, ByVal Button As Integer)
    Sub MouseDown(ByVal X As Integer, ByVal Y As Integer)
    Sub MouseUp(ByVal X As Integer, ByVal Y As Integer)
    Sub MouseDown()
    Sub MouseUp()

    Sub SmartClick()
    Function IsEnabled() As Boolean
    Function GetStyle() As Int64
    Function GetStyleEx() As Int64
    Sub MoveMouseTo(ByVal X As Integer, ByVal Y As Integer)
    Sub MoveMouseTo()

    Sub CaptureImage(ByVal FilePath As String, ByVal Rect As System.Drawing.Rectangle)

    Sub CaptureImage(ByVal FilePath As String)
    Function GetLocation() As String
    Function ToString() As String
End Interface




