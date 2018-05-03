''' <summary>
''' Gives the user direct control over the mouse, but very
''' little control based upon objects.
''' </summary>
''' <remarks></remarks>
Public Class Mouse

    ''' <summary>
    ''' Returns the absolute X value of the current mouse position.
    ''' </summary>
    ''' <returns>An absolute value, between 0 and 65535.</returns>
    ''' <remarks></remarks>
    Public Shared Function CurrentAbsX() As Integer
        Return UIControls.InternalMouse.CurrentAbsX()
    End Function

    ''' <summary>
    ''' Returns the absolute Y value of the current mouse position.
    ''' </summary>
    ''' <returns>An absolute value, between 0 and 65535.</returns>
    ''' <remarks></remarks>
    Public Shared Function CurrentAbsY() As Integer
        Return UIControls.InternalMouse.CurrentAbsY()
    End Function

    ''' <summary>
    ''' Returns the relative X value of the current mouse position.
    ''' </summary>
    ''' <returns>A coord which is a relative value (relative to the resolution).</returns>
    ''' <remarks></remarks>
    Public Shared Function CurrentX() As Integer
        Return UIControls.InternalMouse.CurrentX()
    End Function

    ''' <summary>
    ''' Returns the relative Y value of the current mouse position.
    ''' </summary>
    ''' <returns>A coord which is a relative value (relative to the resolution).</returns>
    ''' <remarks></remarks>
    Public Shared Function CurrentY() As Integer
        Return UIControls.InternalMouse.CurrentY()
    End Function

    ''' <summary>
    ''' Provides a user with the ability to convert absolute coordinates to a relative 
    ''' coordinate based upon the screen height.  If the screen height is not provided,
    ''' then the current screen height is assumed.
    ''' </summary>
    ''' <param name="Coord">The coordinate that is to be converted.</param>
    ''' <param name="ScreenHeight">The screen height to convert with.</param>
    ''' <returns>Returns the relative coordinate.</returns>
    ''' <remarks></remarks>
    Public Shared Function AbsToRelativeCoordY(ByVal Coord As Integer, Optional ByVal ScreenHeight As Integer = -1) As Integer
        Return UIControls.InternalMouse.AbsToRelativeCoordY(Coord, ScreenHeight)
    End Function

    ''' <summary>
    ''' Provides a user with the ability to convert absolute coordinates to a relative 
    ''' coordinate based upon the screen height.  If the screen height is not provided,
    ''' then the current screen height is assumed.
    ''' </summary>
    ''' <param name="Coord">The coordinate that is to be converted.</param>
    ''' <param name="ScreenHeight">The screen height to convert with.</param>
    ''' <returns>Returns the absolute coordinate.</returns>
    ''' <remarks></remarks>
    Public Shared Function RelativeToAbsCoordY(ByVal Coord As Integer, Optional ByVal ScreenHeight As Integer = -1) As Integer
        Return UIControls.InternalMouse.RelativeToAbsCoordY(Coord, ScreenHeight)
    End Function

    ''' <summary>
    ''' Provides a user with the ability to convert absolute coordinates to a relative 
    ''' coordinate based upon the screen height.  If the screen height is not provided,
    ''' then the current screen height is assumed.
    ''' </summary>
    ''' <param name="Coord">The coordinate that is to be converted.</param>
    ''' <param name="ScreenWidth">The screen height to convert with.</param>
    ''' <returns>Returns the relative coordinate.</returns>
    ''' <remarks></remarks>
    Public Shared Function AbsToRelativeCoordX(ByVal Coord As Integer, Optional ByVal ScreenWidth As Integer = -1) As Integer
        Return UIControls.InternalMouse.AbsToRelativeCoordX(Coord, ScreenWidth)
    End Function

    ''' <summary>
    ''' Provides a user with the ability to convert absolute coordinates to a relative 
    ''' coordinate based upon the screen height.  If the screen height is not provided,
    ''' then the current screen height is assumed.
    ''' </summary>
    ''' <param name="Coord">The coordinate that is to be converted.</param>
    ''' <param name="ScreenWidth">The screen height to convert with.</param>
    ''' <returns>Returns the absolute coordinate.</returns>
    ''' <remarks></remarks>
    Public Shared Function RelativeToAbsCoordX(ByVal Coord As Integer, Optional ByVal ScreenWidth As Integer = -1) As Integer
        Return UIControls.InternalMouse.RelativeToAbsCoordX(Coord, ScreenWidth)
    End Function

    ''' <summary>
    ''' Clicks on a Hwnd value, if the Hwnd value is greater than 0.
    ''' </summary>
    ''' <param name="Hwnd">the Windows Hwnd value.</param>
    ''' <returns>Returns true if successful.</returns>
    ''' <remarks>Some applications are known to not work well (such as winamp) with this
    ''' "smart" method of clicking.</remarks>
    Protected Friend Shared Function ClickByHwnd(ByVal Hwnd As IntPtr) As Boolean
        Return UIControls.InternalMouse.ClickByHwnd(Hwnd)
    End Function

    ''' <summary>
    ''' Use the left mouse button to click at a certain X/Y location.
    ''' </summary>
    ''' <param name="X">The X location.</param>
    ''' <param name="Y">The Y location.</param>
    ''' <remarks>This method excepts the X,Y coords are relative.</remarks>
    Public Shared Sub LeftClick(ByVal X As Integer, ByVal Y As Integer)
        UIControls.InternalMouse.LeftClickXY(X, Y)
    End Sub

    ''' <summary>
    ''' Use the right mouse button to click at a certain X/Y location.
    ''' </summary>
    ''' <param name="X">The X location.</param>
    ''' <param name="Y">The Y location.</param>
    ''' <remarks>This method excepts the X,Y coords are relative</remarks>
    Public Shared Sub RightClick(ByVal X As Integer, ByVal Y As Integer)
        UIControls.InternalMouse.RightClickXY(X, Y)
    End Sub

    ''' <summary>
    ''' Use the middle mouse button to click at a certain X/Y location.
    ''' </summary>
    ''' <param name="X">The X location.</param>
    ''' <param name="Y">The Y location.</param>
    ''' <remarks>This method excepts the X,Y coords are relative</remarks>
    Public Shared Sub MiddleClick(ByVal X As Integer, ByVal Y As Integer)
        UIControls.InternalMouse.MiddleClickXY(X, Y)
    End Sub

    ''' <summary>
    ''' Moves the mouse to a certain X/Y location
    ''' </summary>
    ''' <param name="X">The X location.</param>
    ''' <param name="Y">The Y location.</param>
    ''' <param name="SlowMovement">Move the mouse slowly like a user. By default, 
    ''' it will not move slowly.</param>
    ''' <remarks>This method excepts the X,Y coords are relative</remarks>
    Public Shared Sub GotoXY(ByVal X As Integer, ByVal Y As Integer, Optional ByVal SlowMovement As Boolean = False)
        UIControls.InternalMouse.GotoXY(X, Y, SlowMovement)
    End Sub

    ''' <summary>
    ''' Allows a user to drag and drop with the left mouse button.
    ''' </summary>
    ''' <param name="StartPoint">The point where the mouse button is pressed down.</param>
    ''' <param name="EndPoint">The point where the mouse button is released.</param>
    ''' <param name="SlowMovement">Move the mouse slowly like a user. By default, 
    ''' it will not move slowly.</param>
    ''' <remarks></remarks>
    Public Shared Sub LeftDragAndDrop(ByVal StartPoint As System.Drawing.Point, ByVal EndPoint As System.Drawing.Point, Optional ByVal SlowMovement As Boolean = False)
        UIControls.InternalMouse.DragAndDrop(StartPoint, EndPoint, SlowMovement)
    End Sub

    ''' <summary>
    ''' Allows a user to drag and drop with the right mouse button.
    ''' </summary>
    ''' <param name="StartPoint">The point where the mouse button is pressed down.</param>
    ''' <param name="EndPoint">The point where the mouse button is released.</param>
    ''' <param name="SlowMovement">Move the mouse slowly like a user. By default, 
    ''' it will not move slowly.</param>
    ''' <remarks></remarks>
    Public Shared Sub RightDragAndDrop(ByVal StartPoint As System.Drawing.Point, ByVal EndPoint As System.Drawing.Point, Optional ByVal SlowMovement As Boolean = False)
        UIControls.InternalMouse.DragAndDropRight(StartPoint, EndPoint, SlowMovement)
    End Sub

    ''' <summary>
    ''' Allows a user to drag and drop with the middle mouse button.
    ''' </summary>
    ''' <param name="StartPoint">The point where the mouse button is pressed down.</param>
    ''' <param name="EndPoint">The point where the mouse button is released.</param>
    ''' <param name="SlowMovement">Move the mouse slowly like a user. By default, 
    ''' it will not move slowly.</param>
    ''' <remarks></remarks>
    Public Shared Sub MiddleDragAndDrop(ByVal StartPoint As System.Drawing.Point, ByVal EndPoint As System.Drawing.Point, Optional ByVal SlowMovement As Boolean = False)
        UIControls.InternalMouse.DragAndDropMiddle(StartPoint, EndPoint, SlowMovement)
    End Sub

    Protected Friend Sub New()
    End Sub
End Class