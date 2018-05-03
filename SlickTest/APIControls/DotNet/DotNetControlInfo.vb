Imports winAPI.API
Imports winAPI.NativeFunctions
Imports System.Runtime.InteropServices
Imports System.Text
Imports System.ComponentModel
Imports System
''' <summary>
''' Allows Slick Test to record .net names.
''' </summary>
''' <remarks></remarks>
Friend Class DotNetNames
    Private Shared GetControlNameMessage As Integer = 0

    Shared Sub New()
        GetControlNameMessage = NativeMethods.RegisterWindowMessage("WM_GETCONTROLNAME")
    End Sub 'New


    Public Shared Function GetWinFormsId(ByVal hWnd As IntPtr) As String
        Return XProcGetControlName(hWnd, GetControlNameMessage)
    End Function 'GetWinFormsId


    Protected Shared Function XProcGetControlName(ByVal hwnd As IntPtr, ByVal msg As Integer) As String
        'define the buffer that will eventually contain the desired window's WinFormsId
        Dim bytearray(65534) As Byte

        'allocate space in the target process for the buffer as shared memory
        Dim bufferMem As IntPtr = IntPtr.Zero 'base address of the allocated region for the buffer
        Dim written As IntPtr = IntPtr.Zero 'number of bytes written to memory
        Dim retHandle As IntPtr = IntPtr.Zero
        Dim retVal As Boolean


        'creating and reading from a shared memory region is done differently in Win9x then in newer OSs
        Dim processHandle As IntPtr = IntPtr.Zero
        Dim fileHandle As IntPtr = IntPtr.Zero

        If Not Environment.OSVersion.Platform = PlatformID.Win32Windows Then
            Try
                Dim size As System.UInt32 'ToDo: Unsigned Integers not supported 'the amount of memory to be allocated
                size = 65536

                processHandle = NativeMethods.OpenProcess(NativeMethods.PROCESS_VM_OPERATION Or NativeMethods.PROCESS_VM_READ Or NativeMethods.PROCESS_VM_WRITE, False, GetProcessIdFromHWnd(hwnd))

                If processHandle.ToInt64() = 0 Then
                    Throw New Win32Exception()
                End If

                bufferMem = NativeMethods.VirtualAllocEx(processHandle, IntPtr.Zero, New IntPtr(size), NativeMethods.MEM_RESERVE Or NativeMethods.MEM_COMMIT, NativeMethods.PageProtection.ReadWrite)

                If bufferMem.ToInt64() = 0 Then
                    Throw New Win32Exception()
                End If

                'send message to the control's hWnd for getting the specified control name
                retHandle = NativeMethods.SendMessage(hwnd, msg, New IntPtr(size), bufferMem)

                'now read the TVITEM's info from the shared memory location
                retVal = NativeMethods.ReadProcessMemory(processHandle, bufferMem, bytearray, New IntPtr(size), written)
                If Not retVal Then
                    Throw New Win32Exception()
                End If
            Finally
                'free the memory that was allocated
                retVal = NativeMethods.VirtualFreeEx(processHandle, bufferMem, New IntPtr(0), NativeMethods.MEM_RELEASE)
                If Not retVal Then
                    Throw New Win32Exception()
                End If
                NativeMethods.CloseHandle(processHandle)
            End Try
        Else
            Try
                Dim size2 As Integer 'the amount of memory to be allocated
                size2 = 65536

                fileHandle = NativeMethods.CreateFileMapping(New IntPtr(NativeMethods.INVALID_HANDLE_VALUE), IntPtr.Zero, NativeMethods.PageProtection.ReadWrite, 0, size2, Nothing)
                If fileHandle.ToInt64() = 0 Then
                    Throw New Win32Exception()
                End If
                'bufferMem = NativeMethods.MapViewOfFile(fileHandle, NativeMethods.FILE_MAP_ALL_ACCESS, 0, 0, New UIntPtr(0))
                bufferMem = NativeMethods.MapViewOfFile(fileHandle, NativeMethods.FILE_MAP_ALL_ACCESS, 0, 0, 0)
                If bufferMem.ToInt64() = 0 Then
                    Throw New Win32Exception()
                End If
                NativeMethods.MoveMemoryFromByte(bufferMem, bytearray(0), size2)

                retHandle = NativeMethods.SendMessage(hwnd, msg, New IntPtr(size2), bufferMem)

                'read the control's name from the specific shared memory for the buffer
                NativeMethods.MoveMemoryToByte(bytearray(0), bufferMem, 1024)

            Finally
                'unmap and close the file
                NativeMethods.UnmapViewOfFile(bufferMem)
                NativeMethods.CloseHandle(fileHandle)
            End Try
        End If

        'get the string value for the Control name
        Return ByteArrayToString(bytearray)
    End Function 'XProcGetControlName


    Private Shared Function GetProcessIdFromHWnd(ByVal hwnd As IntPtr) As Integer
        Dim pid As Integer

        NativeMethods.GetWindowThreadProcessId(hwnd, pid)

        Return pid
    End Function 'GetProcessIdFromHWnd


    Private Shared Function ByteArrayToString(ByVal bytes() As Byte) As String
        Dim chars As Char() = {ChrW(&HFD), ChrW(&HFDFD), ChrW(&HFFFD), Chr(0)}
        If Environment.OSVersion.Platform = PlatformID.Win32Windows Then
            ' Use the Ansii encoder
            Dim s As String = Encoding.Default.GetString(bytes).TrimEnd(chars) '.TrimEnd("0"c)
            Dim cArr() As Char = s.ToCharArray()
            s = ""
            For Each c As Char In cArr
                If (c <> Nothing) Then
                    s += c
                Else
                    Exit For
                End If

            Next
            Return s
        Else
            ' use Unicode
            Dim s As String = Encoding.Unicode.GetString(bytes).TrimEnd(chars)
            'Dim cArr() As Char = s.ToCharArray()
            's = ""
            'For Each c As Char In cArr
            '    If (c <> Nothing) Then
            '        s += c
            '    Else
            '        Exit For
            '    End If
            'Next
            Return s
        End If
    End Function 'ByteArrayToString

End Class 'WinFormsUtilities

Friend Class NativeMethods

    <Flags()> _
   Public Enum PageProtection
        NoAccess = &H1
        [ReadOnly] = &H2
        ReadWrite = &H4
        WriteCopy = &H8
        Execute = &H10
        ExecuteRead = &H20
        ExecuteReadWrite = &H40
        ExecuteWriteCopy = &H80
        Guard = &H100
        NoCache = &H200
        WriteCombine = &H400
    End Enum

    <DllImport("kernel32.dll")> _
    Public Shared Function OpenProcess(ByVal dwDesiredAccess As Integer, ByVal bInheritHandle As Boolean, ByVal dwProcessId As Integer) As IntPtr

    End Function
    <DllImport("kernel32.dll")> _
    Public Shared Function VirtualAllocEx(ByVal hProcess As IntPtr, ByVal lpAddress As IntPtr, ByVal dwSize As IntPtr, ByVal flAllocationType As Integer, ByVal flProtect As PageProtection) As IntPtr 'ToDo: Unsigned Integers not supported
    End Function
    <DllImport("user32.dll", SetLastError:=True)> _
    Public Shared Function GetWindowThreadProcessId(ByVal hWnd As IntPtr, ByRef lpdwProcessId As Integer) As Long

    End Function
    <DllImport("kernel32.dll")> _
    Public Shared Function VirtualFreeEx(ByVal hProcess As IntPtr, ByVal lpAddress As IntPtr, ByVal dwSize As IntPtr, ByVal dwFreeType As Integer) As Boolean 'ToDo: Unsigned Integers not supported

    End Function
    <DllImport("kernel32.dll")> _
    Public Shared Function CloseHandle(ByVal hObject As IntPtr) As Boolean

    End Function
    <DllImport("kernel32.dll")> _
    Public Shared Function MapViewOfFile(ByVal hFileMappingObject As IntPtr, ByVal dwDesiredAccess As Long, ByVal dwFileOffsetHigh As Long, ByVal dwFileOffsetLow As Long, ByVal dwNumberOfBytesToMap As Long) As IntPtr

    End Function
    <DllImport("kernel32.dll")> _
    Public Shared Function UnmapViewOfFile(ByVal lpBaseAddress As IntPtr) As Boolean

    End Function
    <DllImport("kernel32.dll", SetLastError:=True)> _
    Public Shared Function CreateFileMapping(ByVal hFile As IntPtr, ByVal lpFileMappingAttributes As IntPtr, ByVal flProtect As PageProtection, ByVal dwMaximumSizeHigh As Integer, ByVal dwMaximumSizeLow As Integer, ByVal lpName As String) As IntPtr

    End Function
    <DllImport("user32.dll")> _
    Public Shared Function SendMessage(ByVal hwnd As IntPtr, ByVal wMsg As Integer, ByVal wParam As IntPtr, ByVal lParam As IntPtr) As IntPtr

    End Function
    <DllImport("kernel32.dll")> _
    Public Shared Function ReadProcessMemory(ByVal hProcess As IntPtr, ByVal lpBaseAddress As IntPtr, <Out()> ByVal lpBuffer() As Byte, ByVal nSize As IntPtr, ByVal lpNumberOfBytesRead As IntPtr) As Boolean

    End Function
    <DllImport("Kernel32.dll", EntryPoint:="RtlMoveMemory", SetLastError:=False)> _
    Public Shared Sub MoveMemoryFromByte(ByVal dest As IntPtr, ByRef src As Byte, ByVal size As Integer)

    End Sub
    <DllImport("Kernel32.dll", EntryPoint:="RtlMoveMemory", SetLastError:=False)> _
    Public Shared Sub MoveMemoryToByte(ByRef dest As Byte, ByVal src As IntPtr, ByVal size As Integer)

    End Sub
    <DllImport("user32.dll", SetLastError:=True, CharSet:=CharSet.Auto)> _
    Public Shared Function RegisterWindowMessage(ByVal lpString As String) As Integer

    End Function



    '=========== Win95/98/ME Shared memory staff===============
    Public Const STANDARD_RIGHTS_REQUIRED As Integer = &HF0000
    Public Const SECTION_QUERY As Short = &H1
    Public Const SECTION_MAP_WRITE As Short = &H2
    Public Const SECTION_MAP_READ As Short = &H4
    Public Const SECTION_MAP_EXECUTE As Short = &H8
    Public Const SECTION_EXTEND_SIZE As Short = &H10
    Public Const SECTION_ALL_ACCESS As Integer = STANDARD_RIGHTS_REQUIRED Or SECTION_QUERY Or SECTION_MAP_WRITE Or SECTION_MAP_READ Or SECTION_MAP_EXECUTE Or SECTION_EXTEND_SIZE
    Public Const FILE_MAP_ALL_ACCESS As Integer = SECTION_ALL_ACCESS

    '============NT Shared memory constant======================
    Public Const PROCESS_VM_OPERATION As Short = &H8
    Public Const PROCESS_VM_READ As Short = &H10
    Public Const PROCESS_VM_WRITE As Short = &H20
    Public Const PROCESS_ALL_ACCESS As Long = &H1F0FFF
    Public Const MEM_COMMIT As Short = &H1000
    Public Const MEM_RESERVE As Short = &H2000
    Public Const MEM_DECOMMIT As Short = &H4000
    Public Const MEM_RELEASE As Integer = &H8000
    Public Const MEM_FREE As Integer = &H10000
    Public Const MEM_PRIVATE As Integer = &H20000
    Public Const MEM_MAPPED As Integer = &H40000
    Public Const MEM_TOP_DOWN As Integer = &H100000


    Public Const INVALID_HANDLE_VALUE As Integer = -1
End Class 'NativeMethods 