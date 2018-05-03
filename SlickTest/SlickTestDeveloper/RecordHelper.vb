Public Class RecordHelper
    Public Shared description As APIControls.Description
    Private Shared ExchangeList As New System.Collections.Specialized.StringDictionary()
    Private Shared VBNetRegex As New System.Text.RegularExpressions.Regex( _
   "(?<SpecialChars>[^!#$%&'()*+,-./:;<=>?@[\]^_`{|}~A-Za-z0-9\s\t\-'""]+)", _
   System.Text.RegularExpressions.RegexOptions.Multiline Or System.Text.RegularExpressions.RegexOptions.Compiled)

    Private Shared CSharpRegex As New System.Text.RegularExpressions.Regex( _
"(@?<SpecialChars>[^!#$%&'()*+,-./:;<=>?@[\]^_`{|}~A-Za-z0-9\s\t\-'""]+)", _
System.Text.RegularExpressions.RegexOptions.Multiline Or System.Text.RegularExpressions.RegexOptions.Compiled)


    Public Shared Function BuildDescription(Optional ByVal name As String = "Desc") As String
        If (description.Count = 0) Then
            Return ""
        End If
        Dim str As String = ""
        Dim desc As String = description.ToString()
        If (SlickTestDev.CurrentLanguage = CodeTranslator.Languages.VBNet) Then
            str = "Public Shared " & name & " As UIControls.Description = UIControls.Description.Create(" & desc & ")"
        Else
            str = "public static UIControls.Description " & name & " = UIControls.Description.Create(@" & desc & ");"
        End If
        Return str
    End Function

    Public Shared Function SmartNameBuilder(ByVal MaxDescriptionLength As Integer) As String
        If (ExchangeList.Count = 0) Then 'first run
            ExchangeList.Add("!", "")
            ExchangeList.Add("@", "_AT_")
            ExchangeList.Add("#", "_NUM_")
            ExchangeList.Add("$", "")
            ExchangeList.Add("%", "_PRCT_")
            ExchangeList.Add("^", "")
            ExchangeList.Add("&", "_AND_")
            ExchangeList.Add("*", "_STAR_")
            ExchangeList.Add("+", "_PLUS_")
            ExchangeList.Add("=", "_EQUL_")
            ExchangeList.Add("(", "_LPRN_")
            ExchangeList.Add(")", "_RPRN_")
            ExchangeList.Add("[", "_LBRK_")
            ExchangeList.Add("]", "_RBRK_")
            ExchangeList.Add("{", "_LBRK_")
            ExchangeList.Add("}", "_RBRK_")
            ExchangeList.Add("|", "_PIPE_")
            ExchangeList.Add("\", "_SLSH_")
            ExchangeList.Add("/", "_SLSH_")
            ExchangeList.Add("-", "_HYPN_")
            ExchangeList.Add(",", "_COMA_")
            ExchangeList.Add("~", "_TILD_")
            ExchangeList.Add("`", "_SPC_")
            ExchangeList.Add(".", "_DOT_")
            ExchangeList.Add(";", "_SMIC_")
            ExchangeList.Add(":", "_COLN_")
            ExchangeList.Add("""", "_QUTE_")
            ExchangeList.Add(">", "_GRTH_")
            ExchangeList.Add("<", "_LSTH_")
            ExchangeList.Add("'", "_SQUT_")
            ExchangeList.Add(" ", "_")
            ExchangeList.Add(" ", "_") 'special space character of some sort, ran into it in the wild, figure it
            'might show up in the future.
        End If
        Dim RetName As String = ""
        If (Not String.IsNullOrEmpty(description.Name)) Then
            RetName = description.Name.Substring(0, Math.Min(15, description.Name.Length))
#If IncludeWeb = 1 Then
        Else
            If (Not String.IsNullOrEmpty(description.WebTag)) Then
                RetName = description.WebTag.Substring(0, Math.Min(15, description.WebTag.Length))
            Else
                If (Not String.IsNullOrEmpty(description.WebTitle)) Then
                    RetName = description.WebTitle.Substring(0, Math.Min(15, description.WebTitle.Length))
                End If
            End If
#End If
        End If
        If (Not String.IsNullOrEmpty(description.Value)) Then
            If (Not String.IsNullOrEmpty(RetName)) Then
                RetName += "_" & description.Value.Substring(0, Math.Min(25, description.Value.Length))
            Else
                RetName += description.Value.Substring(0, Math.Min(25, description.Value.Length))
            End If
        Else
#If IncludeWeb = 1 Then
            If (Not String.IsNullOrEmpty(description.WebValue)) Then
                If (Not String.IsNullOrEmpty(RetName)) Then
                    RetName += "_" & description.WebValue.Substring(0, Math.Min(25, description.WebValue.Length))
                Else
                    RetName += description.WebValue.Substring(0, Math.Min(25, description.WebValue.Length))
                End If
            End If
#End If
        End If
        RetName = RetName.Substring(0, Math.Min(MaxDescriptionLength, RetName.Length))

        Dim ms As System.Text.RegularExpressions.MatchCollection
        If (SlickTestDev.CurrentLanguage = CodeTranslator.Languages.VBNet) Then
            ms = VBNetRegex.Matches(RetName)
        Else
            ms = CSharpRegex.Matches(RetName)
        End If
        For i As Integer = 0 To ms.Count - 1
            RetName = RetName.Replace(ms.Item(i).Groups.Item("SpecialChars").ToString(), "_SPC_")
        Next

        If (RetName = "") Then
            RetName = "MyDesc"
        Else
            RetName = FixName(RetName)
            RetName = RetName.Trim("_"c)
        End If
        If (RetName = "Static") Then
            If (SlickTestDev.CurrentLanguage = CodeTranslator.Languages.VBNet) Then RetName = "[Static]"
        End If

        Return RetName 'do more later
    End Function

    Private Shared Function FixName(ByVal ReplaceString As String) As String
        ReplaceString = ReplaceString.Trim()
        For Each item As String In ExchangeList.Keys
            If (ReplaceString.Contains(item) = True) Then
                ReplaceString = ReplaceString.Replace(item, ExchangeList.Item(item))
            End If
        Next
        If (ReplaceString = "") Then
            Return "MyDesc"
        End If
        If (Char.IsDigit(ReplaceString.Chars(0))) Then
            ReplaceString = "N" & ReplaceString
        End If
        ReplaceString = ReplaceString.Replace("__", "_")
        Return ReplaceString.Substring(0, Math.Min(45, ReplaceString.Length)) 'Try to keep the number of characters low...
    End Function

End Class

