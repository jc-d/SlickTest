
'TODO:
' -Remove Top/Bottom/Left/Right and make them all just "location" since the
' limiting factor here is the number of items I can allow
' Why?
' A: Because the bit mask is very limited in how many items it can list.
' and 4 items is very costly when 1 will do.  Also, height/width could be fixed as well.
' In the future, I may need to re-engineer the system instead of using unique IDs.
' isWindow, isListBox should be one enumerated value

''' <summary>
''' Builds a description that describes a window object.
''' </summary>
''' <remarks>Descriptions support the following values:<p/>
''' "name" - The name of the object.<p/>
''' "value" - The value, text or title of the object.<p/>
''' "top" - The top edge of the object.<p/>
''' "bottom" - The bottom edge of the object.<p/>
''' "left" - The left edge of the object.<p/>
''' "right" - The right edge of the object.<p/>
''' "width" - The width of the object.<p/>
''' "height" - The height of the object.<p/>
''' "windowtype" - The type of window the object is.<p/>
''' "hwnd" - The window handle, a unique handle for every windows object.<p/>
''' "processname" - The name of the process containing the window.<p/>
''' "index" - A index of when a certain object is created in windows.<p/>
''' "nearbylabel" - The label closest to the windows object.<p/>
''' "controlid" - An ID set for any windows object, except the windows themselves.<p/>
''' "wildcard" - Allows you to enable or disable wild cards for the name and value descriptions.<p/>
''' </remarks>
''' 
Public Class Description
    Implements IDescription
#Const isAbs = 2 ' This means it is using absolute positioning
#Const IncludeWeb = 2 'set to 1 to enable web

    Private UseWildCard As Boolean
    Private myDesc As System.Collections.Specialized.StringDictionary
    Private Shared WindowsFunctions As New APIControls.IndependentWindowsFunctionsv1()
    'Private Shared Windowsv1 As New Windowsv1()

    ''' <summary>
    ''' A list of possible describers.
    ''' </summary>
    ''' <remarks></remarks>
    Public Shadows Enum DescriptionData
        ''' <summary>The type of window the object is.</summary>
        WindowType
        ''' <summary>The height of the object.</summary>
        Height
        ''' <summary>The width of the object.</summary>
        Width
        ''' <summary>The bottom edge of the object.</summary>
        Bottom
        ''' <summary>The top edge of the object.</summary>
        Top
        ''' <summary>The left edge of the object.</summary>
        Left
        ''' <summary>The right edge of the object.</summary>
        Right
        ''' <summary>The name of the object.</summary>
        Name
        ''' <summary>The value, text or title of the object.</summary>
        Value
        ''' <summary>The name of the process containing the window.</summary>
        ProcessName
        ''' <summary>A index of when a certain object is created in windows.</summary>
        Index
        ''' <summary>The label closest to the windows object.</summary>
        NearByLabel
        ''' <summary>The control ID assigned by the developer.  In .NET this value is not used. </summary>
        ControlID
#If (IncludeWeb = 1) Then
            WebValue
            WebText
            WebTitle
            WebType
            WebID
            WebInnerHTML
            WebOuterHTML
#End If
        ''' <summary>The window handle, a unique handle for every windows object.</summary>
        Hwnd
        ''' <summary>Allows you to enable or disable wild cards for the name and value descriptions.</summary>
        WildCard
    End Enum

    ''' <summary>
    ''' Gets the name of enumerated item.
    ''' </summary>
    ''' <param name="EnumedItem">The enumerated value.</param>
    ''' <returns>The name of the enumerated value.</returns>
    ''' <remarks></remarks>
    Public Function GetItemName(ByVal EnumedItem As DescriptionData) As String Implements IDescription.GetItemName
        Select Case EnumedItem
            Case DescriptionData.Name
                Return "name"
            Case DescriptionData.Value
                Return "value"
            Case DescriptionData.Top
                Return "top"
            Case DescriptionData.Bottom
                Return "bottom"
            Case DescriptionData.Left
                Return "left"
            Case DescriptionData.Right
                Return "right"
            Case DescriptionData.Hwnd
                Return "hwnd"
            Case DescriptionData.Width
                Return "width"
            Case DescriptionData.Height
                Return "height"
            Case DescriptionData.WindowType
                Return "windowtype"
            Case DescriptionData.ProcessName
                Return "processname"
            Case DescriptionData.Index
                Return "index"
            Case DescriptionData.WildCard
                Return "wildcard"
            Case DescriptionData.NearByLabel
                Return "nearbylabel"
            Case DescriptionData.ControlID
                Return "controlid"
#If (IncludeWeb = 1) Then
            Case DescriptionData.WebText
                Return "webtext"
            Case DescriptionData.WebTitle
                Return "webtitle"
            Case DescriptionData.WebValue
                Return "webvalue"
            Case DescriptionData.WebTag
                Return "webtag"
            Case DescriptionData.WebID
                Return "webid"
            Case DescriptionData.WebInnerHTML
                Return "webinnerhtml"
            Case DescriptionData.WebOuterHTML
                Return "webouterhtml"
#End If
            Case Else
                Return ""
        End Select
    End Function

    ''' <summary>
    ''' Get the item's enumerated value from the item's name.
    ''' </summary>
    ''' <param name="ItemType"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetItemEnum(ByVal ItemType As String) As DescriptionData Implements IDescription.GetItemEnum
        Select Case ItemType.Replace(" ", "").ToLower
            Case "name"
                Return DescriptionData.Name
            Case "value" '"text", "value", "title"
                Return DescriptionData.Value
            Case "top", "y"
                Return DescriptionData.Top
            Case "bottom"
                Return DescriptionData.Bottom
            Case "left", "x"
                Return DescriptionData.Left
            Case "right"
                Return DescriptionData.Right
            Case "hwnd"
                Return DescriptionData.Hwnd
            Case "width"
                Return DescriptionData.Width
            Case "height"
                Return DescriptionData.Height
            Case "windowtype", "wintype" ', "type" 'todo
                Return DescriptionData.WindowType
            Case "processname"
                Return DescriptionData.ProcessName
            Case "index"
                Return DescriptionData.Index
            Case "wildcard"
                Return DescriptionData.WildCard
            Case "nearbylabel"
                Return DescriptionData.NearByLabel
            Case "controlid"
                Return DescriptionData.ControlID
#If (IncludeWeb = 1) Then
            Case "webtype", "webtag"
                Return DescriptionData.WebTag
            Case "webtitle"
                Return DescriptionData.WebTitle
            Case "webtext"
                Return DescriptionData.WebText
            Case "webvalue"
                Return DescriptionData.WebValue
            Case "webid"
                Return DescriptionData.WebID
            Case "webinnerhtml"
                Return DescriptionData.WebInnerHTML
            Case "webouterhtml"
                Return DescriptionData.WebOuterHTML
#End If
            Case Else
                Throw New SlickTestAPIException(ItemType & " is an invalid type.")
        End Select
        Return Nothing
    End Function

    ''' <summary>
    ''' Gets the value of an item inside the description, assuming the item
    ''' has already been set in the description.
    ''' </summary>
    ''' <param name="ItemType">This only works for the types
    ''' enumerated in DescriptionData</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetItemValue(ByVal ItemType As String) As String Implements IDescription.GetItemValue
        Return GetItemValue(GetItemEnum(ItemType))
    End Function

    ''' <summary>
    ''' Gets a value inside the description for a specific item type in the description object
    ''' </summary>
    ''' <param name="ItemType">This only works for the types
    ''' enumerated in DescriptionData</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetItemValue(ByVal ItemType As DescriptionData) As String Implements IDescription.GetItemValue
        Select Case ItemType
            Case DescriptionData.Name
                Return Name
            Case DescriptionData.Hwnd
                Return Hwnd.ToString()
            Case DescriptionData.Top
                Return Top.ToString()
            Case DescriptionData.Bottom
                Return Bottom.ToString()
            Case DescriptionData.Left
                Return Left.ToString()
            Case DescriptionData.Right
                Return Right.ToString()
            Case DescriptionData.Value
                Return Value
            Case DescriptionData.Height
                Return Height.ToString()
            Case DescriptionData.Width
                Return Width.ToString()
            Case DescriptionData.WindowType
                Return WindowType()
            Case DescriptionData.ProcessName
                Return ProcessName()
            Case DescriptionData.Index
                Return Index.ToString()
            Case DescriptionData.WildCard
                Return WildCard().ToString()
            Case DescriptionData.NearByLabel
                Return NearByLabel()
            Case DescriptionData.ControlID
                Return ControlID().ToString()
#If (IncludeWeb = 1) Then
            Case DescriptionData.WebText
                Return WebText()
            Case DescriptionData.WebTitle
                Return WebTitle()
            Case DescriptionData.WebTag
                Return WebTag()
            Case DescriptionData.WebValue
                Return WebValue()
            Case DescriptionData.WebID
                Return WebID()
            Case DescriptionData.WebInnerHTML
                Return WebInnerHTML()
            Case DescriptionData.WebOuterHTML
                Return WebOuterHTML()
#End If
            Case Else
                Return ""
        End Select
    End Function

    ''' <summary>
    ''' Removes one type from the description, setting it back to a blank state.
    ''' </summary>
    ''' <param name="Type">The type of value you wish to add, such as "name", "value", "top", etc.</param>
    ''' <remarks>If the type does not exist no notification will occur.</remarks>
    Public Sub Remove(ByVal Type As String) Implements IDescription.Remove
        If (myDesc.ContainsKey(GetItemName(GetItemEnum(Type))) = True) Then myDesc.Remove(GetItemName(GetItemEnum(Type)))
    End Sub

    ''' <summary>
    ''' Removes one type from the description, setting it back to a blank state.
    ''' </summary>
    ''' <param name="Type">The type of value you wish to remove.</param>
    ''' <remarks>If the type does not exist no notification will occur.</remarks>
    Public Sub Remove(ByVal Type As DescriptionData) Implements IDescription.Remove
        If (myDesc.ContainsKey(GetItemName(Type)) = True) Then myDesc.Remove(GetItemName(Type))
    End Sub

    ''' <summary>
    ''' Verify if the description attribute is already in the list of attributes.
    ''' </summary>
    ''' <param name="Type">The type of value you wish to test</param>
    ''' <returns>Returns true if the value is found.</returns>
    ''' <remarks></remarks>
    Public Function Contains(ByVal Type As String) As Boolean Implements IDescription.Contains
        Return myDesc.ContainsKey(GetItemName(GetItemEnum(Type)))
    End Function

    ''' <summary>
    ''' Verify if the description attribute is already in the list of attributes.
    ''' </summary>
    ''' <param name="Type">The type of value you wish to test</param>
    ''' <returns>Returns true if the value is found.</returns>
    ''' <remarks></remarks>
    Public Function Contains(ByVal Type As DescriptionData) As Boolean Implements IDescription.Contains
        Return myDesc.ContainsKey(GetItemName(Type))
    End Function

    ''' <summary>
    ''' The number of attributes with in the description.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function Count() As Integer Implements IDescription.Count
        Return myDesc.Count
    End Function

    ''' <summary>
    ''' Removes all items from the description and sets WildCard to false.
    ''' </summary>
    ''' <remarks>Use this if you wish to clear out a description.</remarks>
    Public Sub Reset() Implements IDescription.Reset
        myDesc.Clear()
        UseWildCard = False
    End Sub

    ''' <summary>
    ''' Add or overwriting a value to the description based upon a type.
    ''' </summary>
    ''' <param name="type">The type of value you wish to add.  These are all defined in DescriptionData.</param>
    ''' <param name="value">The value assigned to the object.</param>
    ''' <remarks>Example usage:
    ''' desc.Add(Name,"ObjectName")
    ''' </remarks>
    Public Sub Add(ByVal Type As DescriptionData, ByVal Value As String) Implements IDescription.Add
        Remove(Type)
        Select Case Type
            Case DescriptionData.Name
                myDesc.Add("name", Value)
            Case DescriptionData.Value
                myDesc.Add("value", Value)
            Case DescriptionData.ProcessName
                myDesc.Add("processname", Value)
            Case DescriptionData.Index
                myDesc.Add("index", Value)
            Case DescriptionData.Top
                myDesc.Add("top", Numeric(Value).ToString())
            Case DescriptionData.Bottom
                myDesc.Add("bottom", Numeric(Value).ToString())
            Case DescriptionData.Left
                myDesc.Add("left", Numeric(Value).ToString())
            Case DescriptionData.Right
                myDesc.Add("right", Numeric(Value).ToString())
            Case DescriptionData.Hwnd
                myDesc.Add("hwnd", Numeric(Value).ToString())
            Case DescriptionData.Width
                myDesc.Add("width", Numeric(Value).ToString())
            Case DescriptionData.Height
                myDesc.Add("height", Numeric(Value).ToString())
            Case DescriptionData.WindowType
                myDesc.Add("windowtype", Value.ToLower())
            Case DescriptionData.WildCard
                Me.WildCard = Convert.ToBoolean(Value)
            Case DescriptionData.NearByLabel
                myDesc.Add("nearbylabel", Value.ToString())
            Case DescriptionData.ControlID
                myDesc.Add("controlid", Numeric(Value).ToString())
#If (IncludeWeb = 1) Then
            Case DescriptionData.WebText
                myDesc.Add("webtext", Value)
            Case DescriptionData.WebTitle
                myDesc.Add("webtitle", Value)
            Case DescriptionData.WebTag
                myDesc.Add("webtag", Value)
            Case DescriptionData.WebValue
                myDesc.Add("webvalue", Value)
            Case DescriptionData.WebID
                myDesc.Add("webid", Value)
            Case DescriptionData.WebInnerHTML
                myDesc.Add("webinnerhtml", Value)
            Case DescriptionData.WebOuterHTML
                myDesc.Add("webouterhtml", Value)
#End If
            Case Else
                Throw New SlickTestAPIException(Type.ToString() & " is an invalid type to add")
        End Select
    End Sub

    ''' <summary>
    ''' Add or overwriting a value to the description based upon a type.
    ''' </summary>
    ''' <param name="type">The type of value you wish to add, such as "name", "value", "top", etc.</param>
    ''' <param name="value">The value assigned to the object.</param>
    ''' <remarks>Example usage:
    ''' desc.Add("name","ObjectName")
    ''' </remarks>
    Public Sub Add(ByVal Type As String, ByVal Value As String) Implements IDescription.Add
        Add(GetItemEnum(Type), Value)
    End Sub

    ''' <summary>
    ''' Returns a collection containing all the currently used properties 
    ''' in the description in a string dictionary.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks>This method provides a useful way of getting all the items 
    ''' currently set in description.</remarks>
    Public Function GetDescriptionItems() As System.Collections.Specialized.StringDictionary Implements IDescription.GetDescriptionItems
        Dim items As New System.Collections.Specialized.StringDictionary()
        For Each item As Description.DescriptionData In [Enum].GetValues(GetType(Description.DescriptionData))
            If (Me.Contains(item) = True) Then
                items.Add(Me.GetItemName(item), Me.GetItemValue(item))
            End If
        Next
        Return items
        'Return myDesc
    End Function


    ''' <summary>
    ''' Provides a simple text version of the describing object.
    ''' </summary>
    ''' <returns>Returns a text-based description of the object.</returns>
    ''' <remarks>Format will always apppear like this:
    ''' WinObject("Name:=""MyName"";;Value:=""MyValue""").
    ''' Currently this will always return WinObject as the method name.</remarks>
    Public Overloads Function ToString(ByVal ObjectTypeName As String) As String
        Dim Window As String = ObjectTypeName & "(""" '"WinObject("""
        Dim quote As String = """"
        Dim retVal As String = Window
        'quote += quote
        Dim firstTimeInLoop As Boolean = True
        For Each item As APIControls.Description.DescriptionData In [Enum].GetValues(GetType(APIControls.Description.DescriptionData))
            If (Contains(item) = True) Then
                If (firstTimeInLoop = True) Then
                    firstTimeInLoop = False
                    retVal += GetItemName(item) & ":=" & quote & _
                    GetItemValue(item).Replace("""", quote) & quote
                Else
                    retVal += ";;" & GetItemName(item) & ":=" & quote & _
                    GetItemValue(item).Replace("""", quote) & quote
                End If
            End If
        Next
        If (retVal = "") Then 'failure... This is really bad :(
            Return ""
        Else
            If (GetItemValue(DescriptionData.WildCard).ToLower().Contains("true") = True) Then
                retVal += ";;" & GetItemName(DescriptionData.WildCard) & ":=" & quote & quote & _
                GetItemValue(DescriptionData.WildCard).Replace("""", quote) & quote & quote
            End If
            Return retVal & """)."
        End If
    End Function

    ''' <summary>
    ''' Provides a simple text version of the describing object.
    ''' </summary>
    ''' <returns>Returns a text-based description of the object.</returns>
    ''' <remarks>Format will always apppear like this:
    ''' "Name:=""MyName"";;Value:=""MyValue"""
    ''' Currently this will always return WinObject as the method name.</remarks>
    Public Overrides Function ToString() As String
        Dim quote As String = """"
        Dim retVal As String = quote
        'quote += quote
        Dim firstTimeInLoop As Boolean = True

        For Each item As APIControls.Description.DescriptionData In [Enum].GetValues(GetType(APIControls.Description.DescriptionData))
            If (Contains(item) = True) Then
                Dim TmpRetVal As String
                If (firstTimeInLoop = True) Then
                    firstTimeInLoop = False
                    TmpRetVal = GetItemName(item) & ":=" & quote & quote & _
                    GetItemValue(item).Replace("""", quote & quote) & quote & quote
                Else
                    TmpRetVal = ";;" & GetItemName(item) & ":=" & quote & quote & _
                    GetItemValue(item).Replace("""", quote & quote) & quote & quote
                End If
                TmpRetVal = TmpRetVal. _
                Replace(vbNewLine, """" & " & vbNewLine & " & """"). _
                Replace(vbCr, """" & " & vbCr & " & """"). _
                Replace(vbLf, """" & " & vbLf & " & """")

                retVal += TmpRetVal
            End If
        Next
        If (GetItemValue(DescriptionData.WildCard).ToLower().Contains("true") = True) Then
            retVal += ";;" & GetItemName(DescriptionData.WildCard) & ":=" & quote & quote & _
            GetItemValue(DescriptionData.WildCard).Replace("""", quote) & quote & quote
        End If
        Return retVal & quote
    End Function

#Region "Web"
#If (IncludeWeb = 1) Then
    ''' <summary>
    ''' The web ID provided by the user.
    ''' </summary>
    ''' <value></value>
    ''' <returns>The web ID for the html element, if this value is already set. 
    ''' </returns>
    ''' <remarks></remarks>
    Public ReadOnly Property WebID() As String
        Get
            If (myDesc.ContainsKey(GetItemName(DescriptionData.WebID)) = False) Then
                Return vbNullString
            Else
                Return myDesc.Item(GetItemName(DescriptionData.WebID))
            End If
        End Get
    End Property

    ''' <summary>
    ''' The web ID provided by the user.
    ''' </summary>
    ''' <value></value>
    ''' <returns>The web ID for the html element, if this value is already set. 
    ''' </returns>
    ''' <remarks></remarks>
    Public ReadOnly Property WebInnerHTML() As String
        Get
            If (myDesc.ContainsKey(GetItemName(DescriptionData.WebInnerHTML)) = False) Then
                Return vbNullString
            Else
                Return myDesc.Item(GetItemName(DescriptionData.WebInnerHTML))
            End If
        End Get
    End Property

    ''' <summary>
    ''' The inner HTML of a web object provided by the user.
    ''' </summary>
    ''' <value></value>
    ''' <returns>The inner HTML for the html element, if this value is already set. 
    ''' </returns>
    ''' <remarks></remarks>
    Public ReadOnly Property WebOuterHTML() As String
        Get
            If (myDesc.ContainsKey(GetItemName(DescriptionData.WebOuterHTML)) = False) Then
                Return vbNullString
            Else
                Return myDesc.Item(GetItemName(DescriptionData.WebOuterHTML))
            End If
        End Get
    End Property

    ''' <summary>
    ''' The value for the web element, if this value is already set.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property WebValue() As String
        Get
            If (myDesc.ContainsKey(GetItemName(DescriptionData.WebValue)) = False) Then
                Return vbNullString
            Else
                Return myDesc.Item(GetItemName(DescriptionData.WebValue))
            End If
            'Return myValue
        End Get
    End Property

    ''' <summary>
    ''' The text for the web element, if this value is already set.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property WebText() As String
        Get
            If (myDesc.ContainsKey(GetItemName(DescriptionData.WebText)) = False) Then
                Return vbNullString
            Else
                Return myDesc.Item(GetItemName(DescriptionData.WebText))
            End If
            'Return myValue
        End Get
    End Property

    ''' <summary>
    ''' The title for the web element, if this value is already set.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property WebTitle() As String
        Get
            If (myDesc.ContainsKey(GetItemName(DescriptionData.WebTitle)) = False) Then
                Return vbNullString
            Else
                Return myDesc.Item(GetItemName(DescriptionData.WebTitle))
            End If
            'Return myValue
        End Get
    End Property

    ''' <summary>
    ''' The WebTag for the object, if this value is already set.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property WebTag() As String
        Get
            If (myDesc.ContainsKey(GetItemName(DescriptionData.WebTag)) = False) Then
                Return "unknown"
            Else
                Return myDesc.Item(GetItemName(DescriptionData.WebTag)).ToLower()
            End If
        End Get
    End Property
#End If
#End Region

    ''' <summary>
    ''' The Hwnd provided by the user.
    ''' </summary>
    ''' <value></value>
    ''' <returns>The hwnd for the window, if this value is already set. IntPtr.Zero if it hasn't be set.</returns>
    ''' <remarks>Returns IntPtr.Zero if no value is set.</remarks>
    Public ReadOnly Property Hwnd() As IntPtr Implements IDescription.Hwnd
        Get
            'Return myHwnd
            If (myDesc.ContainsKey(GetItemName(DescriptionData.Hwnd)) = False) Then
                Return IntPtr.Zero
            Else
                Return CType(Convert.ToInt64(myDesc.Item(GetItemName(DescriptionData.Hwnd))), IntPtr)
            End If
        End Get
    End Property

    ''' <summary>
    ''' The Control ID provided by the user.
    ''' </summary>
    ''' <value></value>
    ''' <returns>The Control ID for the window object, if this value is already set. 0 if it hasn't be set.</returns>
    ''' <remarks>Returns 0 if no value is set.</remarks>
    Public ReadOnly Property ControlID() As Integer Implements IDescription.ControlID
        Get
            If (myDesc.ContainsKey(GetItemName(DescriptionData.ControlID)) = False) Then
                Return 0
            Else
                Return Convert.ToInt32(myDesc.Item(GetItemName(DescriptionData.ControlID)))
            End If
        End Get
    End Property

    ''' <summary>
    ''' The value, text or title of the object.
    ''' </summary>
    ''' <value></value>
    ''' <returns>The value, text or title of the object, if this value is already set. 
    ''' </returns>
    ''' <remarks>Returns Nothing if no value is set.</remarks>
    Public ReadOnly Property Value() As String Implements IDescription.Value
        Get
            If (myDesc.ContainsKey(GetItemName(DescriptionData.Value)) = False) Then
                Return Nothing
            Else
                Return myDesc.Item(GetItemName(DescriptionData.Value))
            End If
            'Return myValue
        End Get
    End Property

    ''' <summary>
    ''' The Name for the window, if this value is already set.
    ''' </summary>
    ''' <value></value>
    ''' <returns>Returns Nothing if no value is set.</returns>
    ''' <remarks></remarks>
    Public ReadOnly Property Name() As String Implements IDescription.Name
        Get
            If (myDesc.ContainsKey(GetItemName(DescriptionData.Name)) = False) Then
                Return Nothing
            Else
                Return myDesc.Item(GetItemName(DescriptionData.Name))
            End If
            'Return myName
        End Get
    End Property

    ''' <summary>
    ''' The Name of the label nearest the WinObject, if this value is already set.
    ''' </summary>
    ''' <value></value>
    ''' <returns>Returns Nothing if no value is set.</returns>
    ''' <remarks>The label defined as "nearest" is based upon a circle starting from
    ''' the top and searches counter-clockwise.</remarks>
    Public ReadOnly Property NearByLabel() As String Implements IDescription.NearByLabel
        Get
            If (myDesc.ContainsKey(GetItemName(DescriptionData.NearByLabel)) = False) Then
                Return Nothing
            Else
                Return myDesc.Item(GetItemName(DescriptionData.NearByLabel))
            End If
            'Return myName
        End Get
    End Property

    ''' <summary>
    ''' The ProcessName for the WindowsObject, if this value is already set.
    ''' </summary>
    ''' <value></value>
    ''' <returns>Returns Nothing if no value is set.</returns>
    ''' <remarks></remarks>
    Public ReadOnly Property ProcessName() As String Implements IDescription.ProcessName
        Get
            If (myDesc.ContainsKey(GetItemName(DescriptionData.ProcessName)) = False) Then
                Return Nothing
            Else
                Return myDesc.Item(GetItemName(DescriptionData.ProcessName))
            End If
            'Return myName
        End Get
    End Property

    ''' <summary>
    ''' The Index for a WindowObject, if this value is already set.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks>This value will never be set for windows, as they do 
    ''' not have an index.  In the case of a window, the value will return -1.</remarks>
    Public ReadOnly Property Index() As Integer Implements IDescription.Index
        Get
            If (myDesc.ContainsKey(GetItemName(DescriptionData.Index)) = False) Then
                Return -1
            Else
                Return Convert.ToInt32(myDesc.Item(GetItemName(DescriptionData.Index)))
            End If
        End Get
    End Property

    ''' <summary>
    ''' The Top for the window, if this value is already set.
    ''' </summary>
    ''' <value></value>
    ''' <returns>Returns -1 if no value is set.</returns>
    ''' <remarks></remarks>
    Public ReadOnly Property Top() As Integer Implements IDescription.Top
        Get
            If (myDesc.ContainsKey(GetItemName(DescriptionData.Top)) = False) Then
                Return -1
            Else
                Return Convert.ToInt32(myDesc.Item(GetItemName(DescriptionData.Top)))
            End If
            'Return myTop
        End Get
    End Property

    ''' <summary>
    ''' The Bottom for the window, if this value is already set.
    ''' </summary>
    ''' <value></value>
    ''' <returns>Returns -1 if no value is set.</returns>
    ''' <remarks></remarks>
    Public ReadOnly Property Bottom() As Integer Implements IDescription.Bottom
        Get
            If (myDesc.ContainsKey(GetItemName(DescriptionData.Bottom)) = False) Then
                Return -1
            Else
                Return Convert.ToInt32(myDesc.Item(GetItemName(DescriptionData.Bottom)))
            End If
            'Return myBottom
        End Get
    End Property

    ''' <summary>
    ''' The Left for the window, if this value is already set.
    ''' </summary>
    ''' <value></value>
    ''' <returns>Returns -1 if no value is set.</returns>
    ''' <remarks></remarks>
    Public ReadOnly Property Left() As Integer Implements IDescription.Left
        Get
            If (myDesc.ContainsKey(GetItemName(DescriptionData.Left)) = False) Then
                Return -1
            Else
                Return Convert.ToInt32(myDesc.Item(GetItemName(DescriptionData.Left)))
            End If
            'Return myLeft
        End Get
    End Property

    ''' <summary>
    ''' The Right for the window, if this value is already set.
    ''' </summary>
    ''' <value></value>
    ''' <returns>Returns -1 if no value is set.</returns>
    ''' <remarks></remarks>
    Public ReadOnly Property Right() As Integer Implements IDescription.Right
        Get
            If (myDesc.ContainsKey(GetItemName(DescriptionData.Right)) = False) Then
                Return -1
            Else
                Return Convert.ToInt32(myDesc.Item(GetItemName(DescriptionData.Right)))
            End If
            'Return myRight
        End Get
    End Property

    ''' <summary>
    ''' The Height for the window, if this value is already set.
    ''' </summary>
    ''' <value></value>
    ''' <returns>Returns -1 if no value is set.</returns>
    ''' <remarks></remarks>
    Public ReadOnly Property Height() As Integer Implements IDescription.Height
        Get
            If (myDesc.ContainsKey(GetItemName(DescriptionData.Height)) = False) Then
                Return -1
            Else
                Return Convert.ToInt32(myDesc.Item(GetItemName(DescriptionData.Height)))
            End If
            'Return myHeight
        End Get
    End Property

    ''' <summary>
    ''' The Width for the window, if this value is already set.
    ''' </summary>
    ''' <value></value>
    ''' <returns>Returns -1 if no value is set.</returns>
    ''' <remarks></remarks>
    Public ReadOnly Property Width() As Integer Implements IDescription.Width
        Get
            If (myDesc.ContainsKey(GetItemName(DescriptionData.Width)) = False) Then
                Return -1
            Else
                Return Convert.ToInt32(myDesc.Item(GetItemName(DescriptionData.Width)))
            End If
            'Return myWidth
        End Get
    End Property

    ''' <summary>
    ''' The WindowType for the object, if this value is already set.
    ''' </summary>
    ''' <value></value>
    ''' <returns>Returns unknown if no value is set.</returns>
    ''' <remarks></remarks>
    Public ReadOnly Property WindowType() As String Implements IDescription.WindowType
        Get
            If (myDesc.ContainsKey(GetItemName(DescriptionData.WindowType)) = False) Then
                Return "unknown"
            Else
                Return myDesc.Item(GetItemName(DescriptionData.WindowType)).ToLower()
            End If
        End Get
    End Property

    ''' <summary>
    ''' The WildCard property can be used so that a description 
    ''' can use wild card values. For example, you can use a 
    ''' description like this: desc.add("name","*m?Name*")
    ''' </summary>
    ''' <value></value>
    ''' <returns>Returns true if wild cards are enabled.</returns>
    ''' <remarks>By default, the WildCard property is set to false.
    ''' The WildCard property affects name, value and near by label.</remarks>
    Public Property WildCard() As Boolean Implements IDescription.WildCard
        Get
            Return UseWildCard
        End Get
        Set(ByVal value As Boolean)
            UseWildCard = value
        End Set
    End Property

    ''' <summary>
    ''' The exact location provided by the description.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks>NOTE: If the top, bottom, left and right are not all provided for, then
    ''' this will provide incorrect results.</remarks>
    Public ReadOnly Property Location() As System.Drawing.Rectangle Implements IDescription.Location
        Get
            Return New System.Drawing.Rectangle(Left, Top, Right - Left, Bottom - Top)
        End Get
    End Property

    Public Sub New()
        myDesc = New System.Collections.Specialized.StringDictionary()
        UseWildCard = False
    End Sub

    Public Sub New(ByVal Description As String)
        myDesc = New System.Collections.Specialized.StringDictionary()
        UseWildCard = False
        If (ReadDescription(Description) = False) Then
            Throw New SlickTestAPIException("Unable to read description: " & Description)
        End If
    End Sub

    Public Sub New(ByVal Description As String, ByVal IsWildCard As Boolean)
        myDesc = New System.Collections.Specialized.StringDictionary()
        UseWildCard = IsWildCard
        If (ReadDescription(Description) = False) Then
            Throw New SlickTestAPIException("Unable to read description: " & Description)
        End If
    End Sub

#Region "Privates"

    Private Function ReadDescription(ByVal desc As String) As Boolean
        Dim index As Integer = desc.IndexOf(":=")
        If (index = -1) Then
            Return False
        End If
        Dim type As String = desc.Substring(0, index).ToLower().Trim()
        Dim tmpValue As String = ""
        index = desc.IndexOf("""")
        Dim index2 As Integer = desc.IndexOf(";;", index + 1)
        If (index2 = -1) Then
            If (desc.Substring(desc.Length - 1) = """") Then
                index2 = desc.Length '+ 1
            Else
                index2 = desc.Length + 1
            End If
        End If
        tmpValue = desc.Substring(index + 1, index2 - (2 + index))
        Me.Add(type, tmpValue)
        If (index2 > desc.Length) Then
            index2 = desc.Length
        End If
        If (desc.IndexOf(":=", index2) <> -1) Then
            Return ReadDescription(desc.Substring(index2 + 2))
        Else
            Return True
        End If
        '"name:=""blah"";;value:="blah"
    End Function

    Private Function IsBool(ByVal str As String) As Boolean
        IsBool = False
        If (Replace(str, " ", "").ToLower.Equals("true") Or Replace(str, " ", "").ToLower.Equals("false")) Then
            IsBool = True
        End If
    End Function

    Private Function GetBool(ByVal str As String) As Boolean
        If (IsBool(str) = False) Then
            Throw New Exception("Invalid data type in description: " & str)
        End If
        If (Replace(str, " ", "").ToLower.Equals("true")) Then
            Return True
        End If
        Return False
    End Function

    Private Shared Function Numeric(ByVal str As String) As Integer
        Return Convert.ToInt32(str.Trim()) 'may throw exception if user doesn't provide an int.
    End Function

#End Region
End Class
