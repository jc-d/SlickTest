Imports APIControls.Description

''' <summary>
''' The interface for the description of a windows object.
''' </summary>
''' <remarks></remarks>
Public Interface IDescription

    ''' <summary>
    ''' Gets the name of enumerated item.
    ''' </summary>
    ''' <param name="EnumedItem">The enumerated value.</param>
    ''' <returns>The name of the enumerated value.</returns>
    ''' <remarks></remarks>
    Function GetItemName(ByVal EnumedItem As DescriptionData) As String

    ''' <summary>
    ''' Get the item's enumerated value from the item's name.
    ''' </summary>
    ''' <param name="ItemType"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetItemEnum(ByVal ItemType As String) As DescriptionData

    ''' <summary>
    ''' Gets the value of an item inside the description, assuming the item
    ''' has already been set in the description.
    ''' </summary>
    ''' <param name="ItemType">This only works for the types
    ''' enumerated in DescriptionData</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetItemValue(ByVal ItemType As String) As String

    ''' <summary>
    ''' Gets a value inside the description for a specific item type in the description object
    ''' </summary>
    ''' <param name="ItemType">This only works for the types
    ''' enumerated in DescriptionData</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetItemValue(ByVal ItemType As DescriptionData) As String
    ''' <summary>
    ''' Removes one type from the description, setting it back to a blank state.
    ''' </summary>
    ''' <param name="Type">The type of value you wish to remove, such as "name", "value", "top", etc.</param>
    ''' <remarks>If the type does not exist no notification will occur.</remarks>
    Sub Remove(ByVal Type As String)

    ''' <summary>
    ''' Removes one type from the description, setting it back to a blank state.
    ''' </summary>
    ''' <param name="Type">The type of value you wish to remove.</param>
    ''' <remarks>If the type does not exist no notification will occur.</remarks>
    Sub Remove(ByVal Type As DescriptionData)

    ''' <summary>
    ''' Verify if the description attribute is already in the list of attributes.
    ''' </summary>
    ''' <param name="Type">The type of value you wish to test, such as "name", "value"</param>
    ''' <returns>Returns true if the value is found.</returns>
    ''' <remarks></remarks>
    Function Contains(ByVal Type As String) As Boolean

    ''' <summary>
    ''' Verify if the description attribute is already in the list of attributes.
    ''' </summary>
    ''' <param name="Type">The type of value you wish to test</param>
    ''' <returns>Returns true if the value is found.</returns>
    ''' <remarks></remarks>
    Function Contains(ByVal Type As DescriptionData) As Boolean

    ''' <summary>
    ''' The number of attributes with in the description.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function Count() As Integer

    ''' <summary>
    ''' Removes all items from the description and sets WildCard to false.
    ''' </summary>
    ''' <remarks>Use this if you wish to clear out a description.</remarks>
    Sub Reset()

    ''' <summary>
    ''' Add or overwriting a value to the description based upon a type.
    ''' </summary>
    ''' <param name="type">The type of value you wish to add.  These are all defined in DescriptionData.</param>
    ''' <param name="value">The value assigned to the object.</param>
    ''' <remarks>Example usage:
    ''' desc.Add(Name,"ObjectName")
    ''' </remarks>
    Sub Add(ByVal Type As DescriptionData, ByVal Value As String)

    ''' <summary>
    ''' Add or overwriting a value to the description based upon a type.
    ''' </summary>
    ''' <param name="type">The type of value you wish to add, such as "name", "value", "top", etc.</param>
    ''' <param name="value">The value assigned to the object.</param>
    ''' <remarks>Example usage:
    ''' desc.Add("name","ObjectName")
    ''' </remarks>
    Sub Add(ByVal Type As String, ByVal Value As String)
    ''' <summary>
    ''' Returns a collection containing all the currently used properties 
    ''' in the description in a string dictionary.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks>This method provides a useful way of getting all the items 
    ''' currently set in description.</remarks>
    Function GetDescriptionItems() As System.Collections.Specialized.StringDictionary



#Region "Web"
#If (IncludeWeb = 1) Then
    ''' <summary>
    ''' The web ID provided by the user.
    ''' </summary>
    ''' <value></value>
    ''' <returns>The web ID for the html element, if this value is already set. 
    ''' </returns>
    ''' <remarks></remarks>
    ReadOnly Property WebID() As String

    ''' <summary>
    ''' The web ID provided by the user.
    ''' </summary>
    ''' <value></value>
    ''' <returns>The web ID for the html element, if this value is already set. 
    ''' </returns>
    ''' <remarks></remarks>
    ReadOnly Property WebInnerHTML() As String

    ''' <summary>
    ''' The inner HTML of a web object provided by the user.
    ''' </summary>
    ''' <value></value>
    ''' <returns>The inner HTML for the html element, if this value is already set. 
    ''' </returns>
    ''' <remarks></remarks>
    ReadOnly Property WebOuterHTML() As String

    ''' <summary>
    ''' The value for the web element, if this value is already set.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ReadOnly Property WebValue() As String

    ''' <summary>
    ''' The text for the web element, if this value is already set.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ReadOnly Property WebText() As String

    ''' <summary>
    ''' The title for the web element, if this value is already set.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ReadOnly Property WebTitle() As String

    ''' <summary>
    ''' The WebType for the object, if this value is already set.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ReadOnly Property WebType() As String
#End If
#End Region

    ''' <summary>
    ''' The Hwnd provided by the user.
    ''' </summary>
    ''' <value></value>
    ''' <returns>The hwnd for the window, if this value is already set. IntPtr.Zero if it hasn't be set.</returns>
    ''' <remarks>Returns IntPtr.Zero if no value is set.</remarks>
    ReadOnly Property Hwnd() As IntPtr

    ''' <summary>
    ''' The Control ID provided by the user.
    ''' </summary>
    ''' <value></value>
    ''' <returns>The Control ID for the window object, if this value is already set. 0 if it hasn't be set.</returns>
    ''' <remarks>Returns 0 if no value is set.</remarks>
    ReadOnly Property ControlID() As Integer

    ''' <summary>
    ''' The value, text or title of the object.
    ''' </summary>
    ''' <value></value>
    ''' <returns>The value, text or title of the object, if this value is already set. 
    ''' </returns>
    ''' <remarks>Returns Nothing if no value is set.</remarks>
    ReadOnly Property Value() As String

    ''' <summary>
    ''' The Name for the window, if this value is already set.
    ''' </summary>
    ''' <value></value>
    ''' <returns>Returns Nothing if no value is set.</returns>
    ''' <remarks></remarks>
    ReadOnly Property Name() As String

    ''' <summary>
    ''' The Name of the label nearest the WinObject, if this value is already set.
    ''' </summary>
    ''' <value></value>
    ''' <returns>Returns Nothing if no value is set.</returns>
    ''' <remarks>The label defined as "nearest" is based upon a circle starting from
    ''' the top and searches counter-clockwise.</remarks>
    ReadOnly Property NearByLabel() As String

    ''' <summary>
    ''' The ProcessName for the WindowsObject, if this value is already set.
    ''' </summary>
    ''' <value></value>
    ''' <returns>Returns Nothing if no value is set.</returns>
    ''' <remarks></remarks>
    ReadOnly Property ProcessName() As String

    ''' <summary>
    ''' The Index for a WindowObject, if this value is already set.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks>This value will never be set for windows, as they do 
    ''' not have an index.  In the case of a window, the value will return -1.</remarks>
    ReadOnly Property Index() As Integer

    ''' <summary>
    ''' The Top for the window, if this value is already set.
    ''' </summary>
    ''' <value></value>
    ''' <returns>Returns -1 if no value is set.</returns>
    ''' <remarks></remarks>
    ReadOnly Property Top() As Integer

    ''' <summary>
    ''' The Bottom for the window, if this value is already set.
    ''' </summary>
    ''' <value></value>
    ''' <returns>Returns -1 if no value is set.</returns>
    ''' <remarks></remarks>
    ReadOnly Property Bottom() As Integer

    ''' <summary>
    ''' The Left for the window, if this value is already set.
    ''' </summary>
    ''' <value></value>
    ''' <returns>Returns -1 if no value is set.</returns>
    ''' <remarks></remarks>
    ReadOnly Property Left() As Integer

    ''' <summary>
    ''' The Right for the window, if this value is already set.
    ''' </summary>
    ''' <value></value>
    ''' <returns>Returns -1 if no value is set.</returns>
    ''' <remarks></remarks>
    ReadOnly Property Right() As Integer

    ''' <summary>
    ''' The Height for the window, if this value is already set.
    ''' </summary>
    ''' <value></value>
    ''' <returns>Returns -1 if no value is set.</returns>
    ''' <remarks></remarks>
    ReadOnly Property Height() As Integer

    ''' <summary>
    ''' The Width for the window, if this value is already set.
    ''' </summary>
    ''' <value></value>
    ''' <returns>Returns -1 if no value is set.</returns>
    ''' <remarks></remarks>
    ReadOnly Property Width() As Integer

    ''' <summary>
    ''' The WindowType for the object, if this value is already set.
    ''' </summary>
    ''' <value></value>
    ''' <returns>Returns unknown if no value is set.</returns>
    ''' <remarks></remarks>
    ReadOnly Property WindowType() As String

    ''' <summary>
    ''' The WildCard property can be used so that a description 
    ''' can use wild card values. For example, you can use a 
    ''' description like this: desc.add("name","*m?Name*")
    ''' </summary>
    ''' <value></value>
    ''' <returns>Returns true if wild cards are enabled.</returns>
    ''' <remarks>By default, the WildCard property is set to false.
    ''' The WildCard property affects name, value and near by label.</remarks>
    Property WildCard() As Boolean

    ''' <summary>
    ''' The exact location provided by the description.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks>NOTE: If the top, bottom, left and right are not all provided for, then
    ''' this will provide incorrect results.</remarks>
    ReadOnly Property Location() As System.Drawing.Rectangle

End Interface
