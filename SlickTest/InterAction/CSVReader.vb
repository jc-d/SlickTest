Imports System
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Data
Imports System.Drawing
Imports System.Text
Imports System.Windows.Forms
Imports System.Collections
Imports APIControls

''' <summary>
''' The CSV reader that enables users to read and use CSV (Comma Seperated Values) data.  
''' Generally these CSV files would be used for Data Driven Testing.
''' </summary>
''' <remarks>It is suggested you use Microsoft Office Excel(c) to create the CSV 
''' files.  NOTE: The column headers start will always start with A, even if your CSV has column headers.</remarks>
Public Class CSVReader

    Private Shared CSVData As CSV

#Region "Helper Classes"

    Private Class CSV
        ''' <summary>
        ''' The number of rows in the CSV.
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property RowCount() As Integer
            Get
                Return InternalRowCount
            End Get
            Set(ByVal value As Integer)
                If (value > InternalRowCount) Then InternalRowCount = value
            End Set
        End Property

        ''' <summary>
        ''' The number of columns in the CSV.
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property ColumnCount() As Integer
            Get
                Return InternalColumnCount
            End Get
            Set(ByVal value As Integer)
                If (value > InternalColumnCount) Then
                    InternalColumnCount = value
                End If

            End Set
        End Property

        Private InternalRowCount As Integer = 0
        Public InternalColumnCount As Integer = 0
        Public Data As System.Collections.Generic.List(Of CSVCord)

        Public Sub AddRow(ByVal RowData() As String)
            RowCount += 1
            For ColNumber As Integer = 0 To RowData.Length - 1
                Data.Add(New CSVCord(RowCount, GetColumnName(ColNumber), RowData(ColNumber)))
            Next
            ColumnCount = RowData.Length
        End Sub
        Private Const AsciiOffset As Integer = 65

        Public Shared Function GetColumnName(ByVal Index As Integer) As String
            Dim PrimaryLetterOffset As Integer = 0
            If (Index + AsciiOffset > 90) Then
                Do
                    PrimaryLetterOffset += 1
                Loop While (Index + AsciiOffset - (26 * PrimaryLetterOffset) > 90)
            End If
            If (PrimaryLetterOffset <> 0) Then
                Return (Convert.ToChar(PrimaryLetterOffset - 1 + AsciiOffset) & Convert.ToChar(Index - (26 * PrimaryLetterOffset) + AsciiOffset)).ToUpper()
            Else
                Return Convert.ToString(Convert.ToChar(Index + AsciiOffset)).ToUpper()
            End If
        End Function

        ''' <summary>
        ''' Initializes Data list.
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub New()
            Data = New System.Collections.Generic.List(Of CSVCord)
        End Sub
    End Class

    Friend Class CSVCord
        Public RowNumber As Integer
        Public ColName As String
        Public Value As String
        Public Sub New(ByVal Row As Integer, ByVal Col As String, ByVal Value As String)
            RowNumber = Row
            ColName = Col
            Me.Value = Value
        End Sub
        Public Overrides Function ToString() As String
            Return ColName & RowNumber
        End Function

        Public Overrides Function Equals(ByVal obj As Object) As Boolean
            If Not IsNothing(obj) Then
                If TypeOf obj Is CSVCord Then
                    If (CType(obj, CSVCord).RowNumber = Me.RowNumber) Then
                        If (CType(obj, CSVCord).ColName = Me.ColName) Then
                            Return True
                        End If
                    End If
                End If
            End If
            Return False
        End Function
    End Class
#End Region

#Region "Private/Protected/Friend"

    Private Shared Function GetCSV() As System.Collections.Generic.List(Of CSVCord)
        Return CSVData.Data
    End Function

    Private Shared Function CSVParser(ByVal FileContents As String) As System.Collections.Generic.List(Of CSVCord)
        Dim CurrentChr As Char = Chr(0)
        Dim PeekChr As Char = Chr(0)
        Dim ItemContents As New System.Collections.Generic.List(Of String)
        Dim IsInsideQuotes As Boolean = False
        Dim CurrentItem As New System.Text.StringBuilder(20)
        CSVData = New CSV()
        For i As Integer = 0 To FileContents.Length - 1
            CurrentChr = FileContents(i)
            If (CurrentChr = """"c) Then 'only place we peek is when we have a quote
                If (i <> FileContents.Length - 1) Then
                    PeekChr = FileContents(i + 1)
                Else
                    PeekChr = Chr(0)
                End If
                If (PeekChr <> """"c) Then
                    IsInsideQuotes = Not IsInsideQuotes
                Else
                    i += 1 'skip next quote
                    CurrentItem.Append(CurrentChr) 'add single quote
                End If
            End If

            If (IsInsideQuotes = True AndAlso CurrentChr <> """"c) Then
                CurrentItem.Append(CurrentChr)
            Else
                Select Case CurrentChr
                    Case ","c
                        ItemContents.Add(CurrentItem.ToString())
                        CurrentItem = New System.Text.StringBuilder(20)
                    Case vbCr(0), vbLf(0)
                        If (ItemContents.Count <> 0) Then
                            If (CurrentItem.Length <> 0) Then
                                ItemContents.Add(CurrentItem.ToString())
                                CurrentItem = New System.Text.StringBuilder(20)
                            End If
                            CSVData.AddRow(ItemContents.ToArray())
                            ItemContents.Clear()
                        End If
                    Case """"c
                        'do nothing
                    Case Else
                        CurrentItem.Append(CurrentChr)
                End Select
            End If
        Next
        If (ItemContents.Count <> 0) Then
            If (CurrentItem.Length <> 0) Then
                ItemContents.Add(CurrentItem.ToString())
                CurrentItem = New System.Text.StringBuilder(20)
            End If
            CSVData.AddRow(ItemContents.ToArray())
        End If
        Return CSVData.Data
    End Function

    Private Shared Function ColumnNameToIndex(ByVal ColumnName As String) As Integer
        For i As Integer = 0 To ColumnCount() - 1
            If (CSVReader.GetColumnNameFromIndex(i) = ColumnName) Then Return i
        Next
        Return -1
    End Function

    Private Shared Function InternalRowExists(ByVal RowNumber As Integer) As Boolean
        If (CSVData Is Nothing) Then Return False
        If (CSVData.RowCount < RowNumber) Then Return False
        Return True
    End Function

    ''' <summary>
    ''' Users can't create a CSV Reader since it is shared... only one document at a time.
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub New()

    End Sub

#End Region

    ''' <summary>
    ''' Opens a CSV file for usage.
    ''' </summary>
    ''' <param name="FilePath">The file path for the CSV file.</param>
    ''' <remarks>The CSV file must be opened before you can perform any other interaction with the file.</remarks>
    Public Shared Sub OpenCSV(ByVal FilePath As String)
        If (System.IO.File.Exists(FilePath) = False OrElse System.IO.Path.GetExtension(FilePath).ToLower() <> ".csv") Then
            Throw New APIControls.SlickTestAPIException("Unable to find CSV or CSV file is using an invalid file extension.  The file extension must be .csv.")
        End If
        Dim objReader As IO.StreamReader = Nothing
        Dim count As Integer
        Try
            count = -2
            ' Create an instance of StreamReader to read from a file.
            objReader = New IO.StreamReader(FilePath)
            CSVParser(objReader.ReadToEnd)
            objReader.Close()
        Catch Ex As Exception
            If (objReader IsNot Nothing) Then
                Try
                    objReader.Close()
                Catch ex2 As Exception

                End Try
            End If
            Throw New SlickTestAPIException("The file could not be read or processed.  Error: " & Ex.Message)
        End Try
    End Sub

    ''' <summary>
    ''' Gets the column name from the column index.
    ''' </summary>
    ''' <param name="Index">The column index starts at 0.</param>
    ''' <returns>Returns a value between A and ZZ.</returns>
    ''' <remarks></remarks>
    Public Shared Function GetColumnNameFromIndex(ByVal Index As Integer) As String
        If (Index < 0 OrElse 26 * 26 < Index) Then Throw New SlickTestAPIException("Invalid index.  Index given: " & Index)
        Return CSV.GetColumnName(Index)
    End Function

    ''' <summary>
    ''' Returns the RowCount of the CSV file.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks>If the CSV file failed to open correctly this will return 0.</remarks>
    Public Shared ReadOnly Property RowCount() As Integer
        Get
            If (CSVData Is Nothing) Then
                Return 0
            Else
                Return CSVData.RowCount
            End If
        End Get
    End Property

    ''' <summary>
    ''' Returns the ColumnCount of the CSV file.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks>If the CSV file failed to open correctly this will return 0.</remarks>
    Public Shared ReadOnly Property ColumnCount() As Integer
        Get
            If (CSVData Is Nothing) Then
                Return 0
            Else
                Return CSVData.ColumnCount
            End If
        End Get
    End Property

    ''' <summary>
    ''' Returns the data for a specific column in the CSV.
    ''' </summary>
    ''' <param name="ColumnName">The column name to return.</param>
    ''' <returns></returns>
    ''' <remarks>This will return a list of empty strings if the column was not in the CSV.</remarks>
    Public Shared Function GetColumn(ByVal ColumnName As String) As String()
        Dim Col As New System.Collections.Generic.List(Of String)
        If (CSVData Is Nothing) Then Throw New SlickTestAPIException("Unable to retrieve data.  Has the CSV file been opened?")
        Dim Index As Integer = ColumnNameToIndex(ColumnName)
        If (Index = -1) Then
            For i As Integer = 0 To RowCount() - 1
                Col.Add(String.Empty)
            Next
        Else
            For Each key As CSVCord In CSVData.Data
                If (key.ColName.Equals(ColumnName)) Then
                    Col.Add(key.Value)
                End If
            Next
        End If

        Return Col.ToArray()
    End Function

    ''' <summary>
    ''' Tests to see if an specific item exists in the CSV.
    ''' </summary>
    ''' <param name="ColumnName">The column name to test for.</param>
    ''' <param name="RowNumber">The row number to test for.</param>
    ''' <returns>Returns true if the item exists.</returns>
    ''' <remarks>This tests to see if any item appeared in the CSV for a specific row and column.
    ''' It is possible that the specific column exists and that the row contains values but
    ''' the cell you are testing is not populated.  In that case this will return false but
    ''' if you were to perform a "GetRow" you would get back a blank string value for this column.</remarks>
    Public Shared Function ItemExists(ByVal ColumnName As String, ByVal RowNumber As Integer) As Boolean
        If (ColumnExists(ColumnName) = True) Then
            If (RowExists(RowNumber) = True) Then
                Return True
            End If
        End If
        Return False
    End Function

    ''' <summary>
    ''' Tests if a column exists in the CSV.
    ''' </summary>
    ''' <param name="ColumnName">The name of the column to test.</param>
    ''' <returns>Returns true if the column exists.</returns>
    ''' <remarks></remarks>
    Public Shared Function ColumnExists(ByVal ColumnName As String) As Boolean
        If (CSVData Is Nothing) Then Return False
        For Each key As CSVCord In CSVData.Data
            If (key.ColName.Equals(ColumnName) = True) Then Return True
        Next
        Return False
    End Function

    ''' <summary>
    ''' Tests to see if a specific Row number exists.
    ''' </summary>
    ''' <param name="RowNumber">The row number that you wish to test if exists.</param>
    ''' <returns>Returns true if the row exists.</returns>
    ''' <remarks>Blank rows are always ignored.</remarks>
    Public Shared Function RowExists(ByVal RowNumber As Integer) As Boolean
        If (CSVData Is Nothing) Then Return False
        If (CSVData.RowCount < RowNumber) Then Return False
        For Each key As CSVCord In CSVData.Data
            If (key.RowNumber = RowNumber) Then Return True
        Next
        Return False
    End Function

    ''' <summary>
    ''' Gets a row in the CSV.
    ''' </summary>
    ''' <param name="RowNumber">The row number you wish to get.</param>
    ''' <returns>The row in the CSV that was requested.</returns>
    ''' <remarks>If the row number is exceeds the row count, the
    ''' entire row row will be all empty strings.</remarks>
    Public Shared Function GetRow(ByVal RowNumber As Integer) As String()
        Dim Row As New System.Collections.Generic.List(Of String)
        If (CSVData Is Nothing) Then Throw New SlickTestAPIException("Unable to retrieve data.  Has the CSV file been opened?")
        If (RowNumber <= 0) Then Throw New SlickTestAPIException("Invalid row given.  Row number must be greater than 0.  Row provided: " & RowNumber)
        If (InternalRowExists(RowNumber) = False) Then
            For i As Integer = 1 To ColumnCount()
                Row.Add(String.Empty)
            Next
        Else
            For i As Integer = 1 To ColumnCount()
                Row.Add(GetItem(GetColumnNameFromIndex(i - 1), RowNumber))
            Next
        End If
        Return Row.ToArray()
    End Function

    ''' <summary>
    ''' Gets the entire CSV in a two dimensional array.
    ''' </summary>
    ''' <returns>The array is in rows then in columns.  For example:<BR/>
    '''  Dim CSV()() as string = CSVReader.GetAll()<BR/>
    ''' Log.LogData(CSV(CSVReader.RowNumber-1)(CSVReader.ColumnNumber-1))<BR/>
    ''' </returns>
    ''' <remarks>This will always return an array in which the maximum number of 
    ''' columns will be present in each row even if in some rows those columns had
    ''' no data entered in them.</remarks>
    Public Shared Function GetAll() As String()()
        If (CSVData Is Nothing) Then Return Nothing
        Dim RetVal(RowCount - 1)() As String
        For i As Integer = 1 To RowCount()
            RetVal(i - 1) = CSVReader.GetRow(i)
        Next
        Return RetVal
    End Function

    ''' <summary>
    ''' Gets an item in the CSV.
    ''' </summary>
    ''' <param name="ColumnName">The name of the Column that you are searching for.</param>
    ''' <param name="RowNumber">The Row number that you are searching for.</param>
    ''' <returns>The item in the specified row and column.</returns>
    ''' <remarks>An invalid row number or column name in the CSV will return an empty string.</remarks>
    Public Shared Function GetItem(ByVal ColumnName As String, ByVal RowNumber As Integer) As String
        If (CSVData Is Nothing) Then Throw New SlickTestAPIException("Unable to retrieve data.  Has the CSV file been opened?")
        Try
            Dim StartSearch As Integer = Math.Max((RowNumber * ColumnCount() + (ColumnNameToIndex(ColumnName) + 1)) - 5 - ColumnCount(), 0)
            For i As Integer = StartSearch To CSVData.Data.Count - 1
                If (CSVData.Data.Item(i).RowNumber = RowNumber) Then
                    If (CSVData.Data.Item(i).ColName = ColumnName) Then
                        Return CSVData.Data.Item(i).Value
                    End If
                End If
            Next
            For i As Integer = 0 To StartSearch - 1
                If (CSVData.Data.Item(i).RowNumber = RowNumber) Then
                    If (CSVData.Data.Item(i).ColName = ColumnName) Then
                        Return CSVData.Data.Item(i).Value
                    End If
                End If
            Next
        Catch ex As Exception
        End Try
        Return String.Empty

    End Function

    ''' <summary>
    ''' Gets an item in the CSV.
    ''' </summary>
    ''' <param name="ColumnNameAndRowNumber">The name of the Column and Row that the item is located
    ''' in.  Invalid values will return an empty string.</param>
    ''' <returns>Returns the value in the CSV.</returns>
    ''' <remarks>The column name and number should follow the following pattern: Column##.  For example
    ''' A44 will get the item in column A at Row 44.</remarks>
    Public Shared Function GetItem(ByVal ColumnNameAndRowNumber As String) As String
        If (CSVData Is Nothing) Then Throw New SlickTestAPIException("Unable to retrieve data.  Has the CSV file been opened?")
        For Each key As CSVCord In CSVData.Data
            If (key.ToString().Equals(ColumnNameAndRowNumber)) Then
                Return key.Value
            End If
        Next
        Return String.Empty
    End Function

End Class