Friend Class WebGenericImageAPI
    Inherits WebElementAPI

    Public Sub New(ByVal element As Object)
        MyBase.New(element)
        If (IsGenericImage(Me)) Then Return
        Throw New SlickTestAPIException("Element " & Me.ToString() & " is not a Image.")
    End Sub

    Private Function GetWebInputImage() As WebInputImageAPI
        Return New WebInputImageAPI(Me)
    End Function


    Public Shared Function IsGenericImage(ByVal element As WebElementAPI) As Boolean
        If (WebInputAPI.IsInput(element) = True) Then Return WebInputImageAPI.IsInputImage(element)

        Return IsImgTag(element)
    End Function

    Public Shared Function IsImgTag(ByVal element As WebElementAPI) As Boolean
        If (element.TagName.ToLowerInvariant() = "img") Then Return True
        Return False
    End Function

    Public Function ImgHeight() As Integer
        If (WebInputImageAPI.IsInputImage(Me)) Then

            Return GetWebInputImage().InputHeight
        Else
            Return HTMLImgElement.height
        End If
    End Function

    Public Function ImgWidth() As Integer
        If (WebInputImageAPI.IsInputImage(Me)) Then
            Return GetWebInputImage().InputWidth()
        Else
            Return HTMLImgElement.width
        End If
    End Function

    Public Function Align() As String
        If (WebInputImageAPI.IsInputImage(Me)) Then
            Return GetWebInputImage().Align()
        Else
            Return HTMLImgElement.align
        End If
    End Function

    Public Function UseMap() As String
        If (WebInputImageAPI.IsInputImage(Me)) Then
            Return GetWebInputImage().UseMap()
        Else
            Return HTMLImgElement.useMap
        End If
    End Function

    Public Function Vrml() As String
        If (WebInputImageAPI.IsInputImage(Me)) Then
            Return GetWebInputImage().Vrml()
        Else
            Return HTMLImgElement.vrml
        End If
    End Function

    Public Function VSpace() As Integer
        If (WebInputImageAPI.IsInputImage(Me)) Then
            Return GetWebInputImage().VSpace()
        Else
            Return HTMLImgElement.vspace
        End If
    End Function

    Public Function HSpace() As Integer
        If (WebInputImageAPI.IsInputImage(Me)) Then
            Return GetWebInputImage().HSpace()
        Else
            Return HTMLImgElement.hspace
        End If
    End Function

    Public Function FileSize() As String
        If (WebInputImageAPI.IsInputImage(Me)) Then
            Return GetWebInputImage().FileSize()
        Else
            Return HTMLImgElement.fileSize
        End If
    End Function

    Public Function Src() As String
        If (WebInputImageAPI.IsInputImage(Me)) Then
            Return GetWebInputImage().Src()
        Else
            Return HTMLImgElement.src
        End If
    End Function

    Public Function Alt() As String
        If (WebInputImageAPI.IsInputImage(Me)) Then
            Return GetWebInputImage().Alt()
        Else
            Return HTMLImgElement.alt
        End If
    End Function

    Public Function LowResolutionSrc() As String
        If (WebInputImageAPI.IsInputImage(Me)) Then
            Return GetWebInputImage().LowResolutionSrc()
        Else
            Return HTMLImgElement.lowsrc
        End If
    End Function

    Public Function MimeType() As String
        If (WebInputImageAPI.IsInputImage(Me)) Then
            Return GetWebInputImage().MimeType()
        Else
            Return HTMLImgElement.mimeType
        End If
    End Function

    Public Function FileCreatedDate() As String
        If (WebInputImageAPI.IsInputImage(Me)) Then
            Return GetWebInputImage().FileCreatedDate()
        Else
            Return HTMLImgElement.fileCreatedDate
        End If
    End Function

    Public Function FileModifiedDate() As String
        If (WebInputImageAPI.IsInputImage(Me)) Then
            Return GetWebInputImage().FileModifiedDate()
        Else
            Return HTMLImgElement.fileModifiedDate
        End If
    End Function

    Protected Friend ReadOnly Property HTMLImgElement() As mshtml.IHTMLImgElement
        Get
            Return DirectCast(HTMLElement, mshtml.IHTMLImgElement)
        End Get
    End Property

    Protected Friend ReadOnly Property HTMLImgElement2() As mshtml.IHTMLImgElement2
        Get
            Return DirectCast(HTMLElement, mshtml.IHTMLImgElement2)
        End Get
    End Property
End Class
