#Const IncludeWeb = 2 'set to 1 to enable web
''' <summary>
''' Creates a IE Object which allows you to get information about that object and
''' interact with WebElements inside of IE.
''' </summary>
''' <remarks></remarks>
Friend Class InternetExplorer
#If (IncludeWeb = 1) Then
    Private WithEvents IE As SHDocVw.InternetExplorer
    Private Const MAXTRIES As Integer = 5
    Private Shared WindowsFunctions As New APIControls.IndependentWindowsFunctionsv1()
    Private Shared LastGoodInternalHwnd As IntPtr = IntPtr.Zero
    Private Shared ElementsList As New System.Collections.Generic.List(Of Element)

    Public Sub New()
    End Sub

    ''' <summary>
    ''' Gets the left edge of IE.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetIELeft() As Integer
        Return IE.Left
    End Function

    ''' <summary>
    ''' Gets the right edge of IE.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetIERight() As Integer
        Return IE.Left + IE.Width
    End Function

    ''' <summary>
    ''' Gets the Bottom edge of IE.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetIEBottom() As Integer
        Return IE.Height + IE.Top
    End Function

    ''' <summary>
    ''' Gets the Top edge of IE.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetIETop() As Integer
        Return IE.Top
    End Function

    ''' <summary>
    ''' Get the X coordinate (Left Edge) of IE.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetIEX() As Integer
        Return IE.Left
    End Function

    ''' <summary>
    ''' Get the Y coordinate (Top Edge) of IE.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetIEY() As Integer
        Return IE.Top
    End Function

    ''' <summary>
    ''' Gets IEs current location.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetIELocation() As System.Drawing.Rectangle
        Return New System.Drawing.Rectangle(GetIEX(), GetIEY(), IE.Width, IE.Height)
    End Function

    ''' <summary>
    ''' The various states IE maybe in.
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum IEState As Byte
        ''' <summary>IE has completely loaded the page and rendered it.</summary>
        Complete = 1
        Interactive = 2
        ''' <summary>IE has completely loaded the page.</summary>
        Loaded = 3
        ''' <summary>IE has started loading the page.</summary>
        Loading = 4
        Uninitialized = 5
        ''' <summary>Any invalid state provided by IE.</summary>
        Unknown = 6
    End Enum

    ''' <summary>
    ''' Returns the current state of IE.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function State() As IEState
        Dim CurrentState As IEState
        Select Case IE.ReadyState
            Case SHDocVw.tagREADYSTATE.READYSTATE_COMPLETE
                CurrentState = IEState.Complete
            Case SHDocVw.tagREADYSTATE.READYSTATE_INTERACTIVE
                CurrentState = IEState.Interactive
            Case SHDocVw.tagREADYSTATE.READYSTATE_LOADED
                CurrentState = IEState.Loaded
            Case SHDocVw.tagREADYSTATE.READYSTATE_LOADING
                CurrentState = IEState.Loading
            Case SHDocVw.tagREADYSTATE.READYSTATE_UNINITIALIZED
                CurrentState = IEState.Uninitialized
            Case Else
                CurrentState = IEState.Unknown
        End Select
        Return CurrentState
    End Function

    ''' <summary>
    ''' Closes IE.
    ''' </summary>
    ''' <returns>Returns true if and only if no exception occurs.</returns>
    ''' <remarks></remarks>
    Public Function Close() As Boolean
        Try
            IE.Quit()
        Catch ex As Exception
            Return False
        End Try
        Return True
    End Function

    ''' <summary>
    ''' Refreshs IE.  Does not wait for any particular state.
    ''' </summary>
    ''' <returns>Returns true if and only if no exception occurs.</returns>
    ''' <remarks></remarks>
    Public Function Refresh() As Boolean
        Try
            IE.Refresh()
            Return True
        Catch ex As Exception
            Return False
        End Try
        Return True
    End Function

    ''' <summary>
    ''' Goes to the previous page.
    ''' </summary>
    ''' <returns>Returns true if and only if no exception occurs.</returns>
    ''' <remarks></remarks>
    Public Function Back() As Boolean
        Try
            IE.GoBack()
        Catch ex As Exception
            Return False
        End Try
        Return True
    End Function

    ''' <summary>
    ''' Goes to the next page.
    ''' </summary>
    ''' <returns>Returns true if and only if no exception occurs.</returns>
    ''' <remarks></remarks>
    Public Function Forward() As Boolean
        Try
            IE.GoForward()
        Catch ex As Exception
            Return False
        End Try
        Return True
    End Function

    ''' <summary>
    ''' Goes to the home page.
    ''' </summary>
    ''' <returns>Returns true if and only if no exception occurs.</returns>
    ''' <remarks></remarks>
    Public Function Home() As Boolean
        Try
            IE.GoHome()
        Catch ex As Exception
            Return False
        End Try
        Return True
    End Function

    ''' <summary>
    ''' Allows the user to interact with the current address (URL).  If you set the
    ''' URL, the address bar will navigate to the given URL.
    ''' </summary>
    ''' <value></value>
    ''' <returns>The current URL in the address bar.</returns>
    ''' <remarks></remarks>
    Public Property URL() As String
        Get
            Return IE.LocationURL
        End Get
        Set(ByVal value As String)
            IE.Navigate(value)
        End Set
    End Property

    Private Function GetDocument(Optional ByVal myIE As SHDocVw.InternetExplorer = Nothing) As mshtml.HTMLDocument
        If (myIE Is Nothing) Then myIE = IE
        'If (myIE.Busy = True) Then
        '    'should I throw an exception?
        '    System.Threading.Thread.Sleep(200) 'Give em a little more time... just for test.
        'End If
        Dim HtmlDoc As mshtml.HTMLDocument = Nothing
        If TypeOf myIE.Document Is mshtml.HTMLDocument Then
            'HtmlDoc = DirectCast(myIE,mshtml.HTMLDocument)).Document
            Return DirectCast(myIE.Document, mshtml.HTMLDocument)
        End If
        Return HtmlDoc
    End Function

    ''' <summary>
    ''' Searches for an IE Object using the title and possibly a like statement.
    ''' </summary>
    ''' <param name="Title">The title of the IE object.</param>
    ''' <param name="UseLike">Search using a like statement.</param>
    ''' <returns>Returns true if IE is successfully found.</returns>
    ''' <remarks>This may not exist in the future.</remarks>
    Public Function TakeOverIESearch(ByVal Title As String, Optional ByVal UseLike As Boolean = False) As Boolean
        Dim TempWindow As SHDocVw.InternetExplorer = Nothing
        Dim IEWindows As New SHDocVw.ShellWindows()
        Dim success As Boolean = False
        Dim FailureCounter As Integer = 0
        Dim doc As mshtml.HTMLDocument = Nothing
        Try
            Dim Counter As Integer
            For Counter = 0 To IEWindows.Count
                Try
                    Do
                        Try
                            TempWindow = DirectCast(IEWindows.Item(Counter), SHDocVw.InternetExplorer)
                            doc = GetDocument(TempWindow)
                            success = True
                        Catch ex2 As Exception
                            FailureCounter += 1
                            success = False
                        End Try
                        If (FailureCounter = MAXTRIES) Then 'plenty of tries
                            Return False
                        End If
                    Loop Until (success = True)
                    FailureCounter = 0
                    success = False

                    If (UseLike = False) Then
                        If (doc.title() = Title) Then
                            Do
                                Try
                                    IE = TempWindow
                                    success = True
                                Catch ex2 As Exception
                                    FailureCounter += 1
                                    success = False
                                End Try
                                If (FailureCounter = MAXTRIES) Then 'plenty of tries
                                    Return False
                                End If
                            Loop Until (success = True)
                            Return True
                        End If
                    Else
                        If (Title.ToUpperInvariant() Like doc.title.ToUpperInvariant()) Then
                            Do
                                Try
                                    IE = TempWindow
                                    success = True
                                Catch ex2 As Exception
                                    FailureCounter += 1
                                    success = False
                                End Try
                                If (FailureCounter = MAXTRIES) Then 'plenty of tries
                                    Return False
                                End If
                            Loop Until (success = True)
                            Return True
                        End If
                    End If

                Catch ex1 As Exception

                End Try
            Next
        Catch ex As Exception
            Return False
        End Try
        Return False
    End Function

    ''' <summary>
    ''' Searches for an IE Object using the title and possibly a like statement.
    ''' </summary>
    ''' <param name="Hwnd">The handle of the IE to take over.</param>
    ''' <returns>Returns true if IE is successfully found.</returns>
    ''' <remarks>This may not exist in the future.</remarks>
    Public Function TakeOverIESearch(ByVal Hwnd As IntPtr) As Boolean
        Dim TempWindow As SHDocVw.InternetExplorer = Nothing
        Dim IEWindows As New SHDocVw.ShellWindowsClass()

        Dim success As Boolean = False
        Dim FailureCounter As Integer = 0
        Try
            Dim Counter As Integer
            For Counter = IEWindows.Count - 1 To 0 Step -1
                Try
                    'Dim t1 as System.type = TypeOf(String)
                    Do
                        Try
                            TempWindow = DirectCast(IEWindows.Item(Counter), SHDocVw.InternetExplorer)
                            success = True
                        Catch ex2 As Exception
                            'System.Console.WriteLine("Failed to get item (IE).")
                            FailureCounter += 1
                            success = False
                        End Try
                        If (FailureCounter = MAXTRIES) Then 'plenty of tries
                            'System.Console.WriteLine("Giving up at get item")
                            IEWindows = Nothing
                            Return False
                        End If
                    Loop Until (success = True)

                    If (New IntPtr(TempWindow.HWND) = Hwnd) Then
                        'System.Console.WriteLine("Window is equal to hwnd request")
                        Do
                            Try
                                IE = TempWindow
                                success = True
                            Catch ex2 As Exception
                                'System.Console.WriteLine("Failed to set IE as IE to be automated")
                                FailureCounter += 1
                                success = False
                            End Try
                            If (FailureCounter = MAXTRIES) Then 'plenty of tries
                                System.Console.WriteLine("Giving up setting IE")
                                IEWindows = Nothing
                                Return False
                            End If
                        Loop Until (success = True)
                        IEWindows = Nothing
                        Return True
                    End If

                Catch ex1 As Exception
                    'IEWindows = Nothing
                    'System.Console.WriteLine("Unknown error")
                End Try
            Next
        Catch ex As Exception
            IEWindows = Nothing
            'System.Console.WriteLine("Unknown error 2")
            Return False
        End Try
        IEWindows = Nothing
        'System.Console.WriteLine("Unknown error 3")
        Return False

    End Function

    'Public Function GetElements() As String
    '    Try
    '        Dim doc As mshtml.HTMLDocument = GetDocument()
    '        If (doc.hasChildNodes = True) Then
    '            While (doc.hasChildNodes)
    '                Dim i As mshtml.IHTMLDOMNode = doc.firstChild
    '            End While
    '        End If
    '    Catch ex As Exception

    '    End Try

    '    Return ""
    'End Function

    ''' <summary>
    ''' Gets an element from a X, Y coordinates.
    ''' </summary>
    ''' <param name="X">The x coordinate within the web browser.</param>
    ''' <param name="Y">The y coordinate within the web browser.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetElement(ByVal X As Integer, ByVal Y As Integer) As Element
        Return New Element(GetDocument().elementFromPoint(X, Y))
    End Function

    ''' <summary>
    ''' Warning, this may not be the exact instance if using IE 7+.  This is only meant to get locations
    ''' since all tabs will have the same location.... I think.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetIEInternalInstance() As IntPtr
        Dim desc As New Description()
        If (LastGoodInternalHwnd <> IntPtr.Zero) Then
            Try
                WindowsFunctions.GetClassName(LastGoodInternalHwnd)
            Catch ex As Exception
                LastGoodInternalHwnd = IntPtr.Zero
            End Try
        End If
        If (LastGoodInternalHwnd = IntPtr.Zero) Then
            desc.Add(Description.DescriptionData.Name, "Internet Explorer_Server")
            Try
                LastGoodInternalHwnd = WindowsFunctions.SearchForObj(desc, New IntPtr(IE.HWND))
            Catch ex1 As Exception
                Try
                    LastGoodInternalHwnd = WindowsFunctions.HandlesList(0)
                Catch ex2 As Exception
                    Return IntPtr.Zero
                End Try
            End Try
        End If
        Return LastGoodInternalHwnd
    End Function

    ''' <summary>
    ''' Do not use, created for internal testing.
    ''' </summary>
    ''' <param name="X">The x coordinate within the web browser.</param>
    ''' <param name="Y">The y coordinate within the web browser.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function TestGetElementPoint(ByVal X As Integer, ByVal Y As Integer) As System.Drawing.Point
        Dim MyX As Integer = X
        Dim MyY As Integer = Y
        System.Console.WriteLine("Pre test convert point: " & MyX & ", " & MyY)
        IE.ClientToWindow(MyX, MyY)
        Return New System.Drawing.Point(MyX, MyY)
    End Function

    ''' <summary>
    ''' Gets an element based upon
    ''' </summary>
    ''' <param name="X">The x coordinate within the web browser.</param>
    ''' <param name="Y">The y coordinate within the web browser.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetElementLocation(ByVal X As Integer, ByVal Y As Integer) As System.Drawing.Rectangle
        Dim e As Element = GetElement(X, Y)
        Return GetElementLocation(e)
    End Function

    ''' <summary>
    ''' Returns the (approximate) location of the element based upon the screen
    ''' not relative to the web browser.
    ''' </summary>
    ''' <param name="e">The element to get the location.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetElementLocation(ByVal e As Element) As System.Drawing.Rectangle
        Try
            'gets the rect of the internal IE window (the HTML part of the window).
            Dim rec3 As System.Drawing.Rectangle = WindowsFunctions.GetLocation(GetIEInternalInstance())
            'Takes the internal location and adds the IE location offset to it.
            Dim rec2 As New System.Drawing.Rectangle(e.htmlElement2.getBoundingClientRect.left + rec3.Left, _
                    e.htmlElement2.getBoundingClientRect.top + rec3.Top + 0, _
                    e.htmlElement2.getBoundingClientRect.right - e.htmlElement2.getBoundingClientRect.left, _
                    (e.htmlElement2.getBoundingClientRect.bottom - e.htmlElement2.getBoundingClientRect.top))
            Return rec2
        Catch ex As Exception
            Return New System.Drawing.Rectangle
        End Try
    End Function

    ''' <summary>
    ''' Returns the body as a Element
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetBody() As Element
        Dim doc As mshtml.HTMLDocument = GetDocument()
        Return New Element(doc.body)
    End Function

    ''' <summary>
    ''' Finds an element using a description.
    ''' </summary>
    ''' <param name="desc">The description used to search.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function FindElement(ByVal desc As Description) As Element
        Dim element As Element = GetBody()
        ElementsList.Clear()
        RecursiveGetElements(element)
        System.Console.WriteLine("Elements started with: " & ElementsList.Count)
        Return TestElements(desc)
    End Function

    Private Function SearchElementData(ByVal Type As Description.DescriptionData, ByVal Value As String, ByVal desc As Description) As Description
        Dim PossibleElements As New System.Collections.Generic.List(Of Element)
        Dim retDesc As Description = desc
        desc.Add(Type, Value)
        For Each Elem As Element In ElementsList
            If (desc.WildCard = False) Then
                If GetElemData(Elem, Type) = Value Then
                    PossibleElements.Add(Elem)
                End If
            Else
                If GetElemData(Elem, Type) Like Value Then
                    PossibleElements.Add(Elem)
                End If
            End If
        Next

        If (PossibleElements.Count >= 1) Then
            retDesc.Add(Type, Value)
            ElementsList.Clear() 'in if because of CPU cycles
            ElementsList.AddRange(PossibleElements.ToArray())
            Return retDesc
        End If

        If (ElementsList.Count = 0) Then
            Return Nothing
        Else
            Return retDesc
        End If
    End Function

    Private Function GetElemData(ByVal elem As IElement, ByVal Data As Description.DescriptionData) As String
        Select Case Data
            Case Description.DescriptionData.WebTitle
                Return elem.Title
            Case Description.DescriptionData.WebText
                Return elem.Text
            Case Description.DescriptionData.WebInnerHTML
                Return elem.InnerHTML
            Case Description.DescriptionData.WebOuterHTML
                Return elem.OuterHTML
            Case Description.DescriptionData.WebID
                If (elem.Id Is Nothing) Then Return "" 'somewhat typical
                Return elem.Id
            Case Description.DescriptionData.WebTag
                Return elem.TagName
                'Case Description.DescriptionData.Webclassname
                'Return elem.ClassName
            Case Description.DescriptionData.Name
                Try
                    Return elem.Attribute("name")
                Catch ex As Exception
                    Return ""
                End Try
            Case Description.DescriptionData.WebValue
                Try
                    Return elem.Attribute("value")
                Catch ex As Exception
                    Return ""
                End Try
            Case Else
                Return ""
        End Select
    End Function

    ''' <summary>
    ''' Develops a description for an element based upon a given element.
    ''' </summary>
    ''' <param name="TestElem">The element to search for.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function FindGoodDescription(ByVal TestElem As Element) As Description
        'search name if needed
        Dim element As Element = GetBody()
        ElementsList.Clear()
        System.Console.WriteLine("***Recursive Element read: " & Now.TimeOfDay.ToString())
        RecursiveGetElements(element) 'Get all current Elements
        If (ElementsList.Count = 0) Then
            Return Nothing
        End If
        System.Console.WriteLine("***Recursive Element read done: " & Now.TimeOfDay.ToString())

        Dim desc As New Description()
        'Dim retDesc As Description = New Description()
        Dim AllTypesToPrioritize As New System.Collections.Generic.List(Of Description.DescriptionData)
        AllTypesToPrioritize.Add(Description.DescriptionData.WebID) 'IDs are good, since they must all be different
        AllTypesToPrioritize.Add(Description.DescriptionData.Name) 'do this next, as most programmers seem to use it.
        AllTypesToPrioritize.Add(Description.DescriptionData.WebTag)
        AllTypesToPrioritize.Add(Description.DescriptionData.WebTitle) 'text type are low, as they require more messing with... they typically change more often.
        AllTypesToPrioritize.Add(Description.DescriptionData.WebText)

        Dim LowPriorityTypes As New System.Collections.Generic.List(Of Description.DescriptionData)

        Dim HighPriorityTypes As New System.Collections.Generic.List(Of Description.DescriptionData)
        For Each type As Description.DescriptionData In AllTypesToPrioritize
            If (GetElemData(TestElem, type) <> "") Then
                HighPriorityTypes.Add(type)
            Else
                LowPriorityTypes.Add(type)
            End If
        Next
        LowPriorityTypes.Add(Description.DescriptionData.WebValue)
        LowPriorityTypes.Add(Description.DescriptionData.WebInnerHTML)
        LowPriorityTypes.Add(Description.DescriptionData.WebOuterHTML)

        For Each type As Description.DescriptionData In HighPriorityTypes
            desc = SearchElementData(type, GetElemData(TestElem, type), desc)
            If (desc Is Nothing) Then Return Nothing
            If (ElementsList.Count = 1) Then Return desc
        Next

        For Each type As Description.DescriptionData In LowPriorityTypes
            desc = SearchElementData(type, GetElemData(TestElem, type), desc)
            If (desc Is Nothing) Then Return Nothing
            If (ElementsList.Count = 1) Then Return desc
        Next

        'If (ElementsList.Count = 1) Then
        Return desc 'Returns this even if it's bad data.
        'End If
        'Return Nothing
    End Function

    Private Function TestElementData(ByVal Type As Description.DescriptionData, ByVal Value As String, ByVal desc As Description) As Boolean
        ''///3
        Dim LikelyElements As New System.Collections.Generic.List(Of Element)
        Dim tmpText As String
        If (Type = Description.DescriptionData.WebID Or Type = Description.DescriptionData.WebTag) Then
            Value = Value.ToLower()
            For Each Elem As Element In ElementsList
                tmpText = GetElemData(Elem, Type).ToLower()
                If (desc.WildCard = False) Then
                    If tmpText = Value Then
                        LikelyElements.Add(Elem)
                    End If
                Else
                    If tmpText Like Value Then
                        LikelyElements.Add(Elem)
                    End If
                End If
            Next
        Else
            For Each Elem As Element In ElementsList
                tmpText = GetElemData(Elem, Type)
                If (desc.WildCard = False) Then
                    If tmpText = Value Then
                        LikelyElements.Add(Elem)
                    End If
                Else
                    If tmpText Like Value Then
                        LikelyElements.Add(Elem)
                    End If
                End If
            Next
        End If

        ElementsList.Clear()
        ElementsList.AddRange(LikelyElements)
        If (ElementsList.Count = 0) Then
            Return False 'end search
        Else
            Return True 'continue search
        End If

    End Function

    Private Function TestElements(ByVal desc As Description) As Element
        Dim LikelyElements As New System.Collections.Generic.List(Of Element)
        'search name if needed
        'Dim tmpText As String
        'If (desc.Contains(Description.DescriptionData.WebTitle) = True) Then
        For Each Type As APIControls.Description.DescriptionData In [Enum].GetValues(GetType(APIControls.Description.DescriptionData))
            System.Console.WriteLine("Elements considering search of : " & desc.GetItemName(Type))

            If (desc.Contains(Type) = True) Then
                System.Console.WriteLine("++Elements IS searching : " & desc.GetItemName(Type) & " Presearch count: " & ElementsList.Count)
                If (ElementsList.Count = 1) Then
                    System.Console.WriteLine("One Item searched for: " & desc.GetItemValue(Type))
                    System.Console.WriteLine("One Item contains: " & GetElemData(ElementsList.Item(0), Type))
                End If
                If (TestElementData(Type, desc.GetItemValue(Type), desc) = False) Then
                    Console.WriteLine("TestElements returns, and has the following number of elements: " & ElementsList.Count)
                    If (ElementsList.Count = 1) Then
                        Return ElementsList.ToArray()(0)
                    End If
                    Return Nothing
                End If
            End If
        Next
        Console.WriteLine("TestElements returns, and has the following number of elements: " & ElementsList.Count)
        If (ElementsList.Count = 1) Then
            Return ElementsList.ToArray()(0)
        End If
        Return Nothing
        '    For Each Elem As Element In ElementsList
        '        tmpText = Elem.Title
        '        If (desc.WildCard = False) Then
        '            If tmpText = desc.WebTitle Then
        '                LikelyElements.Add(Elem)
        '            End If
        '        Else
        '            If tmpText Like desc.WebTitle Then
        '                LikelyElements.Add(Elem)
        '            End If
        '        End If
        '    Next
        '    ElementsList.Clear()
        '    ElementsList.AddRange(LikelyElements)
        '    If (ElementsList.Count = 0) Then
        '        Return Nothing
        '    End If
        'End If

        'If (desc.Contains(Description.DescriptionData.WebType) = True) Then
        '    For Each Elem As Element In ElementsList
        '        tmpText = Elem.TagName
        '        If (desc.WildCard = False) Then
        '            If tmpText = desc.WebType Then
        '                LikelyElements.Add(Elem)
        '            End If
        '        Else
        '            If tmpText Like desc.WebType Then
        '                LikelyElements.Add(Elem)
        '            End If
        '        End If
        '    Next
        '    ElementsList.Clear()
        '    ElementsList.AddRange(LikelyElements)
        '    If (ElementsList.Count = 0) Then
        '        Return Nothing
        '    End If
        'End If

        'If (desc.Contains(Description.DescriptionData.WebID) = True) Then
        '    For Each Elem As Element In ElementsList
        '        tmpText = Elem.Id
        '        If (desc.WildCard = False) Then
        '            If tmpText = desc.WebID Then
        '                LikelyElements.Add(Elem)
        '            End If
        '        Else
        '            If tmpText Like desc.WebID Then
        '                LikelyElements.Add(Elem)
        '            End If
        '        End If
        '    Next
        '    ElementsList.Clear()
        '    ElementsList.AddRange(LikelyElements)
        '    If (ElementsList.Count = 0) Then
        '        Return Nothing
        '    End If
        'End If

        'If (desc.Contains(Description.DescriptionData.WebText) = True) Then
        '    For Each Elem As Element In ElementsList
        '        tmpText = Elem.Text
        '        If (desc.WildCard = False) Then
        '            If tmpText = desc.WebText Then
        '                LikelyElements.Add(Elem)
        '            End If
        '        Else
        '            If tmpText Like desc.WebText Then
        '                LikelyElements.Add(Elem)
        '            End If
        '        End If
        '    Next
        '    ElementsList.Clear()
        '    ElementsList.AddRange(LikelyElements)
        '    If (ElementsList.Count = 0) Then
        '        Return Nothing
        '    End If
        'End If


    End Function


    'Private Sub RecursiveGetElements(ByVal elem As Element)
    '    If (elem.htmlElement2 Is Nothing) Then Return
    '    Dim VElem As New VElement(elem)
    '    ElementsList.Add(VElem)
    '    'System.Console.WriteLine("***Recursive Element convert done: " & Now.TimeOfDay.ToString())
    '    Dim elems() As Element = elem.GetChildren()
    '    If (elems Is Nothing) Then
    '        Return
    '    End If
    '    For Each Element1 As Element In elems
    '        RecursiveGetElements(Element1)
    '    Next
    'End Sub

    Private Sub RecursiveGetElements(ByVal elem As Element)
        If (elem.htmlElement2 Is Nothing) Then Return
        ElementsList.Add(elem)
        System.Console.WriteLine("***Recursive Element get at: " & Now.TimeOfDay.ToString())
        Dim elems() As Element = elem.GetAllChildren()
        System.Console.WriteLine("***Recursive Element get done at: " & Now.TimeOfDay.ToString())
        If (elems Is Nothing) Then
            System.Console.WriteLine("No sub elements?")
            Return
        End If
        ElementsList.AddRange(elems)
    End Sub
    ''' <summary>
    ''' Gets the entire page's text.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function PageText() As String
        Return GetDocument.documentElement.outerText
    End Function

    ''' <summary>
    ''' Gets the entire page's html.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function PageHTML() As String
        Return GetDocument.documentElement.outerHTML
    End Function
#End If
End Class