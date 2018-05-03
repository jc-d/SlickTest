Public Class CodeTranslator

    Public Enum Languages
        CSharp
        VBNet
    End Enum

    ''' <summary>
    ''' Warning: This is not a universal translator, this only works for my 
    ''' very basic recording work.  This only works one line at a time.
    ''' </summary>
    ''' <param name="CurrentLanguage"></param>
    ''' <param name="ConvertTo"></param>
    ''' <param name="ValueToConvert"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function Convert(ByVal CurrentLanguage As Languages, _
                    ByVal ConvertTo As Languages, ByVal ValueToConvert As String) As String
        Return ConvertEndOfCommand(CurrentLanguage, ConvertTo, ConvertStringAppend(CurrentLanguage, ConvertTo, _
                                   ConvertStringNewLines(CurrentLanguage, ConvertTo, _
                                                         ValueToConvert)))
    End Function

    Public Shared Function FixVBStringToCSharpVbLikeString(ByVal ValueToConvert As String) As String
        Return ConvertFromTo(ValueToConvert, "(""", "(@""", Languages.VBNet)
    End Function

    Public Shared Function ConvertEndOfCommand(ByVal CurrentLanguage As Languages, _
                    ByVal ConvertTo As Languages, ByVal ValueToConvert As String) As String
        Select Case ConvertTo
            Case Languages.CSharp
                Return ValueToConvert.TrimEnd(vbNewLine) & ";" 'No new line for you
            Case Languages.VBNet
                Return ValueToConvert.TrimEnd(vbNewLine).TrimEnd(";"c) & vbNewLine
        End Select
        Throw New Exception("Unable to convert to " & ConvertTo)
    End Function

    Public Shared Function ConvertStringAppend(ByVal CurrentLanguage As Languages, _
                    ByVal ConvertToLanguage As Languages, ByVal ValueToConvert As String) As String
        Dim StringStarts As Integer = ValueToConvert.IndexOf("""")
        If (StringStarts < 0) Then Return ValueToConvert
        Dim ConvertFrom As String = String.Empty
        Dim ConvertTo As String = String.Empty
        Select Case ConvertToLanguage
            Case Languages.CSharp
                ConvertFrom = "&"
                ConvertTo = "+"
            Case Languages.VBNet
                ConvertFrom = "+"
                ConvertTo = "&"
        End Select
        Return ConvertFromTo(ValueToConvert, ConvertFrom, ConvertTo, CurrentLanguage)
    End Function

    Private Shared Function ConvertFromTo(ByVal ValueToConvert As String, ByVal ConvertFrom As String, ByVal ConvertTo As String, ByVal CurrentLanguage As Languages) As String
        If (Not String.IsNullOrEmpty(ConvertFrom)) Then
            Dim ConvertFromIndex As Integer = ValueToConvert.IndexOf(ConvertFrom, System.StringComparison.InvariantCultureIgnoreCase)
            Dim TimesConvertFromFound As Integer = TimesSubStringIsFoundInString(ValueToConvert, ConvertFrom, True)
            Dim TimesSearched As Integer = 0
            While (ConvertFromIndex >= 0)
                If (Not IsInQuotes(CurrentLanguage, ValueToConvert, ConvertFrom, , TimesSearched)) Then
                    Dim StartValue As String = ValueToConvert.Substring(0, ConvertFromIndex)
                    Dim EndValue As String = ValueToConvert.Substring(ConvertFromIndex + ConvertFrom.Length)
                    ValueToConvert = StartValue & ConvertTo & EndValue
                End If
                ConvertFromIndex = ValueToConvert.IndexOf(ConvertFrom, ConvertFromIndex + ConvertFrom.Length, System.StringComparison.InvariantCultureIgnoreCase)
                TimesSearched += 1
            End While
        End If
        Return ValueToConvert

    End Function

    Private Shared Function TimesSubStringIsFoundInString(ByVal SearchIn As String, ByVal SearchFor As String, Optional ByVal ConsiderCase As Boolean = False) As Integer
        If (String.IsNullOrEmpty(SearchFor)) Then Return 0
        If (ConsiderCase = False) Then
            SearchIn = SearchIn.ToUpperInvariant()
            SearchFor = SearchFor.ToUpperInvariant()
        End If
        Dim StringLengthDif As Integer = SearchIn.Length - SearchIn.Replace(SearchFor, "").Length
        Return StringLengthDif / SearchFor.Length
    End Function

    Private Shared Function IsInQuotes(ByVal CurrentLanguage As Languages, ByVal ValueToBeSearched As String, ByVal ValueToSearchFor As String, Optional ByVal CompareType As System.StringComparison = StringComparison.InvariantCultureIgnoreCase, Optional ByVal TimesToFindIndex As Integer = 0) As Boolean
        Dim Index As Integer = 0
        While (TimesToFindIndex + 1 > 0)
            Index = ValueToBeSearched.Substring(Index).IndexOf(ValueToSearchFor, CompareType)
            If (Index < 0) Then Return False
            TimesToFindIndex -= 1
        End While

        Select Case CurrentLanguage
            Case Languages.CSharp
                ValueToBeSearched = ValueToBeSearched.Replace("\""", "") 'Quotes inside of strings can be ignored.
            Case Languages.VBNet
                ValueToBeSearched = ValueToBeSearched.Replace("""""", "") 'Double quotes (empty strings or quotes inside of quotes) can be ignored.
        End Select
        Dim StringIndex As Integer = ValueToBeSearched.IndexOf("""")
        If (StringIndex < 0) Then Return False

        'Example: Quote starts at index of 10, searched for item is index 15 and end quote is index 20.
        '"test&"
        While (StringIndex < Index)
            Dim EndOfStringIndex As Integer = ValueToBeSearched.IndexOf("""", StringIndex + 1)
            If (EndOfStringIndex > Index) Then Return True
            StringIndex = ValueToBeSearched.IndexOf("""", EndOfStringIndex + 1)
        End While

        Return False
    End Function

    Public Shared Function ConvertStringNewLines(ByVal CurrentLanguage As Languages, _
                    ByVal ConvertTo As Languages, ByVal ValueToConvert As String) As String
        If (CurrentLanguage = ConvertTo) Then
            Return ValueToConvert
        End If
        Dim NewLineInVB As String = "vbnewline"
        Select Case ConvertTo
            Case Languages.CSharp
                Dim index As Integer = ValueToConvert.IndexOf(NewLineInVB, System.StringComparison.InvariantCultureIgnoreCase)
                While (index >= 0)
                    Dim StartValue As String = ValueToConvert.Substring(0, index)
                    Dim EndValue As String = ValueToConvert.Substring(index + NewLineInVB.Length)
                    ValueToConvert = StartValue & "\n\r" & EndValue
                    index = ValueToConvert.IndexOf(NewLineInVB, System.StringComparison.InvariantCultureIgnoreCase)
                End While
                Return ValueToConvert
            Case Languages.VBNet

        End Select
        Throw New Exception("Unable to convert to " & ConvertTo)
    End Function

End Class
