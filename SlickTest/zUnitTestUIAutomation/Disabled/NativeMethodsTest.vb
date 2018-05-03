'Imports System

'Imports NUnit.Framework

'Imports APIControls



''''<summary>
''''This is a test class for NativeMethodsTest and is intended
''''to contain all NativeMethodsTest Unit Tests
''''</summary>
'<TestFixture()>  _
'Public Class NativeMethodsTest


'

'    '''<summary>
'    '''Gets or sets the test context which provides
'    '''information about and functionality for the current test run.
'    '''</summary>
'    Public Property TestContext() As TestContext
'        Get
'            Return testContextInstance
'        End Get
'        Set(ByVal value As TestContext)
'            testContextInstance = Value
'        End Set
'    End Property

'#Region "Additional test attributes"
'    '
'    'You can use the following additional attributes as you write your tests:
'    '
'    'Use ClassInitialize to run code before running the first test in the class
'    '<ClassInitialize()>  _
'    'Public Shared Sub MyClassInitialize()
'    'End Sub
'    '
'    'Use ClassCleanup to run code after all tests in a class have run
'    '<ClassCleanup()>  _
'    'Public Shared Sub MyClassCleanup()
'    'End Sub
'    '
'    'Use TestInitialize to run code before running each test
'    '<TestInitialize()>  _
'    'Public Sub MyTestInitialize()
'    'End Sub
'    '
'    'Use TestCleanup to run code after each test has run
'    '<TestCleanup()>  _
'    'Public Sub MyTestCleanup()
'    'End Sub
'    '
'#End Region


'    '''<summary>
'    '''A test for VirtualFreeEx
'    '''</summary>
'    <Test()> _
'    Public Sub VirtualFreeExTest()
'        Dim hProcess As IntPtr = New IntPtr ' TODO: Initialize to an appropriate value
'        Dim lpAddress As IntPtr = New IntPtr ' TODO: Initialize to an appropriate value
'        Dim dwSize As IntPtr = New IntPtr ' TODO: Initialize to an appropriate value
'        Dim dwFreeType As Integer = 0 ' TODO: Initialize to an appropriate value
'        Dim expected As Boolean = False ' TODO: Initialize to an appropriate value
'        Dim actual As Boolean
'        actual = NativeMethods.VirtualFreeEx(hProcess, lpAddress, dwSize, dwFreeType)
'        Verify.AreEqual(expected, actual)
'        Verify.Inconclusive("Verify the correctness of this test method.")
'    End Sub

'    '''<summary>
'    '''A test for VirtualAllocEx
'    '''</summary>
'    <Test()> _
'    Public Sub VirtualAllocExTest()
'        Dim hProcess As IntPtr = New IntPtr ' TODO: Initialize to an appropriate value
'        Dim lpAddress As IntPtr = New IntPtr ' TODO: Initialize to an appropriate value
'        Dim dwSize As IntPtr = New IntPtr ' TODO: Initialize to an appropriate value
'        Dim flAllocationType As Integer = 0 ' TODO: Initialize to an appropriate value
'        Dim flProtect As NativeMethods.PageProtection = New NativeMethods.PageProtection ' TODO: Initialize to an appropriate value
'        Dim expected As IntPtr = New IntPtr ' TODO: Initialize to an appropriate value
'        Dim actual As IntPtr
'        actual = NativeMethods.VirtualAllocEx(hProcess, lpAddress, dwSize, flAllocationType, flProtect)
'        Verify.AreEqual(expected, actual)
'        Verify.Inconclusive("Verify the correctness of this test method.")
'    End Sub

'    '''<summary>
'    '''A test for UnmapViewOfFile
'    '''</summary>
'    <Test()> _
'    Public Sub UnmapViewOfFileTest()
'        Dim lpBaseAddress As IntPtr = New IntPtr ' TODO: Initialize to an appropriate value
'        Dim expected As Boolean = False ' TODO: Initialize to an appropriate value
'        Dim actual As Boolean
'        actual = NativeMethods.UnmapViewOfFile(lpBaseAddress)
'        Verify.AreEqual(expected, actual)
'        Verify.Inconclusive("Verify the correctness of this test method.")
'    End Sub

'    '''<summary>
'    '''A test for SendMessage
'    '''</summary>
'    <Test()> _
'    Public Sub SendMessageTest()
'        Dim hwnd As IntPtr = New IntPtr ' TODO: Initialize to an appropriate value
'        Dim wMsg As Integer = 0 ' TODO: Initialize to an appropriate value
'        Dim wParam As IntPtr = New IntPtr ' TODO: Initialize to an appropriate value
'        Dim lParam As IntPtr = New IntPtr ' TODO: Initialize to an appropriate value
'        Dim expected As IntPtr = New IntPtr ' TODO: Initialize to an appropriate value
'        Dim actual As IntPtr
'        actual = NativeMethods.SendMessage(hwnd, wMsg, wParam, lParam)
'        Verify.AreEqual(expected, actual)
'        Verify.Inconclusive("Verify the correctness of this test method.")
'    End Sub

'    '''<summary>
'    '''A test for RegisterWindowMessage
'    '''</summary>
'    <Test()> _
'    Public Sub RegisterWindowMessageTest()
'        Dim lpString As String = String.Empty ' TODO: Initialize to an appropriate value
'        Dim expected As Integer = 0 ' TODO: Initialize to an appropriate value
'        Dim actual As Integer
'        actual = NativeMethods.RegisterWindowMessage(lpString)
'        Verify.AreEqual(expected, actual)
'        Verify.Inconclusive("Verify the correctness of this test method.")
'    End Sub

'    '''<summary>
'    '''A test for ReadProcessMemory
'    '''</summary>
'    <Test()> _
'    Public Sub ReadProcessMemoryTest()
'        Dim hProcess As IntPtr = New IntPtr ' TODO: Initialize to an appropriate value
'        Dim lpBaseAddress As IntPtr = New IntPtr ' TODO: Initialize to an appropriate value
'        Dim lpBuffer() As Byte = Nothing ' TODO: Initialize to an appropriate value
'        Dim lpBufferExpected() As Byte = Nothing ' TODO: Initialize to an appropriate value
'        Dim nSize As IntPtr = New IntPtr ' TODO: Initialize to an appropriate value
'        Dim lpNumberOfBytesRead As IntPtr = New IntPtr ' TODO: Initialize to an appropriate value
'        Dim expected As Boolean = False ' TODO: Initialize to an appropriate value
'        Dim actual As Boolean
'        actual = NativeMethods.ReadProcessMemory(hProcess, lpBaseAddress, lpBuffer, nSize, lpNumberOfBytesRead)
'        Verify.AreEqual(lpBufferExpected, lpBuffer)
'        Verify.AreEqual(expected, actual)
'        Verify.Inconclusive("Verify the correctness of this test method.")
'    End Sub

'    '''<summary>
'    '''A test for OpenProcess
'    '''</summary>
'    <Test()> _
'    Public Sub OpenProcessTest()
'        Dim dwDesiredAccess As Integer = 0 ' TODO: Initialize to an appropriate value
'        Dim bInheritHandle As Boolean = False ' TODO: Initialize to an appropriate value
'        Dim dwProcessId As Integer = 0 ' TODO: Initialize to an appropriate value
'        Dim expected As IntPtr = New IntPtr ' TODO: Initialize to an appropriate value
'        Dim actual As IntPtr
'        actual = NativeMethods.OpenProcess(dwDesiredAccess, bInheritHandle, dwProcessId)
'        Verify.AreEqual(expected, actual)
'        Verify.Inconclusive("Verify the correctness of this test method.")
'    End Sub

'    '''<summary>
'    '''A test for MoveMemoryToByte
'    '''</summary>
'    <Test()> _
'    Public Sub MoveMemoryToByteTest()
'        Dim dest As Byte = 0 ' TODO: Initialize to an appropriate value
'        Dim destExpected As Byte = 0 ' TODO: Initialize to an appropriate value
'        Dim src As IntPtr = New IntPtr ' TODO: Initialize to an appropriate value
'        Dim size As Integer = 0 ' TODO: Initialize to an appropriate value
'        NativeMethods.MoveMemoryToByte(dest, src, size)
'        Verify.AreEqual(destExpected, dest)
'        Verify.Inconclusive("A method that does not return a value cannot be verified.")
'    End Sub

'    '''<summary>
'    '''A test for MoveMemoryFromByte
'    '''</summary>
'    <Test()> _
'    Public Sub MoveMemoryFromByteTest()
'        Dim dest As IntPtr = New IntPtr ' TODO: Initialize to an appropriate value
'        Dim src As Byte = 0 ' TODO: Initialize to an appropriate value
'        Dim srcExpected As Byte = 0 ' TODO: Initialize to an appropriate value
'        Dim size As Integer = 0 ' TODO: Initialize to an appropriate value
'        NativeMethods.MoveMemoryFromByte(dest, src, size)
'        Verify.AreEqual(srcExpected, src)
'        Verify.Inconclusive("A method that does not return a value cannot be verified.")
'    End Sub

'    '''<summary>
'    '''A test for MapViewOfFile
'    '''</summary>
'    <Test()> _
'    Public Sub MapViewOfFileTest()
'        Dim hFileMappingObject As IntPtr = New IntPtr ' TODO: Initialize to an appropriate value
'        Dim dwDesiredAccess As Long = 0 ' TODO: Initialize to an appropriate value
'        Dim dwFileOffsetHigh As Long = 0 ' TODO: Initialize to an appropriate value
'        Dim dwFileOffsetLow As Long = 0 ' TODO: Initialize to an appropriate value
'        Dim dwNumberOfBytesToMap As Long = 0 ' TODO: Initialize to an appropriate value
'        Dim expected As IntPtr = New IntPtr ' TODO: Initialize to an appropriate value
'        Dim actual As IntPtr
'        actual = NativeMethods.MapViewOfFile(hFileMappingObject, dwDesiredAccess, dwFileOffsetHigh, dwFileOffsetLow, dwNumberOfBytesToMap)
'        Verify.AreEqual(expected, actual)
'        Verify.Inconclusive("Verify the correctness of this test method.")
'    End Sub

'    '''<summary>
'    '''A test for GetWindowThreadProcessId
'    '''</summary>
'    <Test()> _
'    Public Sub GetWindowThreadProcessIdTest()
'        Dim hWnd As IntPtr = New IntPtr ' TODO: Initialize to an appropriate value
'        Dim lpdwProcessId As Integer = 0 ' TODO: Initialize to an appropriate value
'        Dim lpdwProcessIdExpected As Integer = 0 ' TODO: Initialize to an appropriate value
'        Dim expected As Long = 0 ' TODO: Initialize to an appropriate value
'        Dim actual As Long
'        actual = NativeMethods.GetWindowThreadProcessId(hWnd, lpdwProcessId)
'        Verify.AreEqual(lpdwProcessIdExpected, lpdwProcessId)
'        Verify.AreEqual(expected, actual)
'        Verify.Inconclusive("Verify the correctness of this test method.")
'    End Sub

'    '''<summary>
'    '''A test for CreateFileMapping
'    '''</summary>
'    <Test()> _
'    Public Sub CreateFileMappingTest()
'        Dim hFile As IntPtr = New IntPtr ' TODO: Initialize to an appropriate value
'        Dim lpFileMappingAttributes As IntPtr = New IntPtr ' TODO: Initialize to an appropriate value
'        Dim flProtect As NativeMethods.PageProtection = New NativeMethods.PageProtection ' TODO: Initialize to an appropriate value
'        Dim dwMaximumSizeHigh As Integer = 0 ' TODO: Initialize to an appropriate value
'        Dim dwMaximumSizeLow As Integer = 0 ' TODO: Initialize to an appropriate value
'        Dim lpName As String = String.Empty ' TODO: Initialize to an appropriate value
'        Dim expected As IntPtr = New IntPtr ' TODO: Initialize to an appropriate value
'        Dim actual As IntPtr
'        actual = NativeMethods.CreateFileMapping(hFile, lpFileMappingAttributes, flProtect, dwMaximumSizeHigh, dwMaximumSizeLow, lpName)
'        Verify.AreEqual(expected, actual)
'        Verify.Inconclusive("Verify the correctness of this test method.")
'    End Sub

'    '''<summary>
'    '''A test for CloseHandle
'    '''</summary>
'    <Test()> _
'    Public Sub CloseHandleTest()
'        Dim hObject As IntPtr = New IntPtr ' TODO: Initialize to an appropriate value
'        Dim expected As Boolean = False ' TODO: Initialize to an appropriate value
'        Dim actual As Boolean
'        actual = NativeMethods.CloseHandle(hObject)
'        Verify.AreEqual(expected, actual)
'        Verify.Inconclusive("Verify the correctness of this test method.")
'    End Sub

'    '''<summary>
'    '''A test for NativeMethods Constructor
'    '''</summary>
'    <Test()> _
'    Public Sub NativeMethodsConstructorTest()
'        Dim target As NativeMethods = New NativeMethods
'        Verify.Inconclusive("TODO: Implement code to verify target")
'    End Sub
'End Class
