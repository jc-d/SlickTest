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
Public Class Description ' Create just for autocomplete
    Inherits APIControls.Description
    Implements APIControls.IDescription

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

    Protected Sub New()
        MyBase.New()
    End Sub

    Protected Sub New(ByVal Description As String)
        MyBase.New(Description)
    End Sub

    ''' <summary>
    ''' Creates a new description object.
    ''' </summary>
    ''' <param name="Description">The description to be created.</param>
    ''' <param name="IsWildCard">Allows user to set the description as a wild card for
    ''' any strings that support wild card.</param>
    ''' <returns></returns>
    ''' <remarks>Example description string: "name:=""*Button*"";;value:=""9"""</remarks>
    Public Shared Function Create(ByVal Description As String, ByVal IsWildCard As Boolean) As UIControls.Description
        Return New UIControls.Description(Description, IsWildCard)
    End Function

    ''' <summary>
    ''' Creates a new description object.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function Create() As UIControls.Description
        Return New UIControls.Description()
    End Function

    ''' <summary>
    ''' Creates a new description object.
    ''' </summary>
    ''' <param name="Description">The description to be created.</param>
    ''' <returns></returns>
    ''' <remarks>Example description string: "name:=""Button"";;value:=""9"""</remarks>
    Public Shared Function Create(ByVal Description As String) As UIControls.Description
        Return New UIControls.Description(Description)
    End Function

    Protected Sub New(ByVal Description As String, ByVal IsWildCard As Boolean)
        MyBase.New(Description, IsWildCard)
    End Sub

    ''' <summary>
    ''' Add or overwriting a value to the description based upon a type.
    ''' </summary>
    ''' <param name="type">The type of value you wish to add, such as "name", "value", "top", etc.</param>
    ''' <param name="value">The value assigned to the object.</param>
    ''' <remarks>Example usage:
    ''' desc.Add("name","ObjectName")
    ''' </remarks>
    Public Overloads Sub Add(ByVal Type As String, ByVal Value As String)
        MyBase.Add(Type, Value)
    End Sub

    ''' <summary>
    ''' Add or overwriting a value to the description based upon a type.
    ''' </summary>
    ''' <param name="type">The type of value you wish to add, such as "name", "value", "top", etc.</param>
    ''' <param name="value">The value assigned to the object.</param>
    ''' <remarks>Example usage:
    ''' desc.Add("name","ObjectName")
    ''' </remarks>
    Public Overloads Sub Add(ByVal Type As UIControls.Description.DescriptionData, ByVal Value As String)
        ''Dim InternalType As APIControls.Description.DescriptionData

        MyBase.Add(DirectCast(Type, APIControls.Description.DescriptionData), Value)
    End Sub

    ''' <summary>
    ''' Removes all items from the description and sets WildCard to false.
    ''' </summary>
    ''' <remarks>Use this if you wish to clear out a description.</remarks>
    Public Overloads Sub Reset()
        MyBase.Reset()
    End Sub

    ''' <summary>
    ''' Gets the value of an item inside the description, assuming the item
    ''' has already been set in the description.
    ''' </summary>
    ''' <param name="ItemType">This only works for the types
    ''' enumerated in DescriptionData</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Overloads Function GetItemValue(ByVal ItemType As String) As String
        Return MyBase.GetItemValue(ItemType)
    End Function

    ''' <summary>
    ''' Removes one type from the description, setting it back to a blank state.
    ''' </summary>
    ''' <param name="Type">The type of value you wish to add, such as "name", "value", "top", etc.</param>
    ''' <remarks>If the type does not exist no notification will occur.</remarks>
    Public Overloads Sub Remove(ByVal Type As String)
        MyBase.Remove(Type)
    End Sub

    ''' <summary>
    ''' Returns a collection containing all the currently used properties 
    ''' in the description in a string dictionary.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks>This method provides a useful way of getting all the items 
    ''' currently set in description.</remarks>
    Public Overloads Function GetDescriptionItems() As System.Collections.Specialized.StringDictionary
        Return MyBase.GetDescriptionItems()
    End Function

    ''' <summary>
    ''' The WildCard property can be used so that a description 
    ''' can use wild card values. For example, you can use a 
    ''' description like this: desc.add("name","*m?Name*")
    ''' </summary>
    ''' <value></value>
    ''' <returns>Returns true if wild cards are enabled.</returns>
    ''' <remarks>By default, the WildCard property is set to false.</remarks>
    Public Overloads Property WildCard() As Boolean
        Get
            Return MyBase.WildCard
        End Get
        Set(ByVal value As Boolean)
            MyBase.WildCard = value
        End Set
    End Property

    ''' <summary>
    ''' Removes one type from the description, setting it back to a blank state.
    ''' </summary>
    ''' <param name="Type">The type of value you wish to remove.</param>
    ''' <remarks>If the type does not exist no notification will occur.</remarks>
    Public Overloads Sub Remove(ByVal Type As UIControls.Description.DescriptionData)
        MyBase.Remove(DirectCast(Type, APIControls.Description.DescriptionData))
    End Sub

    ''' <summary>
    ''' Verify if the description attribute is already in the list of attributes.
    ''' </summary>
    ''' <param name="Type">The type of value you wish to test</param>
    ''' <returns>Returns true if the value is found.</returns>
    ''' <remarks></remarks>
    Public Overloads Function Contains(ByVal Type As UIControls.Description.DescriptionData) As Boolean
        Return MyBase.Contains(DirectCast(Type, APIControls.Description.DescriptionData))
    End Function

    ''' <summary>
    ''' Gets a value inside the description for a specific item type in the description object
    ''' </summary>
    ''' <param name="ItemType">This only works for the types
    ''' enumerated in DescriptionData</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Overloads Function GetItemValue(ByVal ItemType As UIControls.Description.DescriptionData) As String
        Return MyBase.GetItemValue(DirectCast(ItemType, APIControls.Description.DescriptionData))
    End Function

    Protected Friend Shared Function ConvertApiToUiDescription(ByVal description As APIControls.Description) As Description
        Dim desc As New UIControls.Description()
        For Each item As APIControls.Description.DescriptionData In [Enum].GetValues(GetType(APIControls.Description.DescriptionData))
            If (description.Contains(item)) Then
                desc.Add(description.GetItemName(item), description.GetItemValue(item))
            End If
        Next
        Return desc
    End Function

    '' <summary>
    '' Creates description from a hwnd value.
    '' </summary>
    '' <param name="hwnd">hwnd value to create the description</param>
    '' <returns>A description object with all values prefilled.</returns>
    '' <remarks>This is helpful for dynamically creating descriptions.
    '' This will not work for web sites as they do not use hwnds.</remarks>
    'Public Shared Shadows Function CreateDescriptionFromHwnd(ByVal hwnd As IntPtr) As UIControls.Description
    '    Dim a As APIControls.Description = APIControls.Description.CreateDescriptionFromHwnd(hwnd)
    '    Dim desc As New UIControls.Description()
    '    For Each key As String In a.GetDescriptionItems().Keys
    '        desc.Add(key, a.GetDescriptionItems.Item(key))
    '    Next
    '    Return desc
    'End Function




End Class
