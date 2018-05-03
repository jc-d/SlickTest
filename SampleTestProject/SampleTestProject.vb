Imports System
Imports UIControls.InterAct
Imports UIControls.Report
Imports UIControls
Imports Microsoft.VisualBasic


Public Class TestRunner
    Inherits UIControls.InterAct

    Public Shared Sub Main()
        'Dim x As New CalcTest()
        'x.Report.Runs = 3
        Dim x As New MyTestClass()
    End Sub
End Class


'test
'****Add the following line of code into shared main(): 
'Dim TestInstance_CalcTest As New CalcTest()

#Region "Description (Calculator) was generated via the recorder..."
Public Class Calculator
    'Description Code Below
    Public Shared SciCalc As UIControls.Description = UIControls.Description.Create("name:=""SciCalc""") 'Top level object
    Public Shared Button_1 As UIControls.Description = UIControls.Description.Create("name:=""Button"";;value:=""1""") 'Parent's name: SciCalc
    Public Shared Button_STAR As UIControls.Description = UIControls.Description.Create("name:=""Button"";;value:=""*""") 'Parent's name: SciCalc
    Public Shared Button_9 As UIControls.Description = UIControls.Description.Create("name:=""Button"";;value:=""9""") 'Parent's name: SciCalc
    Public Shared Button_EQUL As UIControls.Description = UIControls.Description.Create("name:=""Button"";;value:=""=""") 'Parent's name: SciCalc
    Public Shared Button_2 As UIControls.Description = UIControls.Description.Create("name:=""Button"";;value:=""2""") 'Parent's name: SciCalc
    Public Shared Edit As UIControls.Description = UIControls.Description.Create("name:=""Edit""") 'Parent's name: SciCalc
End Class
#End Region

#Region "Recorded code (CalcTest) was generated via the recorder..."
Public Class CalcTest
    Inherits UIControls.InterAct
    Public Sub New(ByVal i As Integer)
        For i1 As Integer = i To 100
            Me.Report.RecordEvent(Me.Pass, "i = " & i1, "ABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890!@#$%^&*()-=+_<>,./?\|][{}`~")
        Next
    End Sub

    Public Sub New()
        'Dim s As String = Me.Window(Calculator.SciCalc).DumpWindowData()
        'Console.WriteLine(s)
        'System.IO.File.WriteAllText("C:\Share\CalculatorDump.txt", s)
        'System.Diagnostics.Process.Start("C:\Users\Noob\Documents\Visual Studio 2008\Projects\SampleWindowsForm\SampleWindowsForm\bin\Debug\SampleWindowsForm.exe")
        'Dim Form1 As UIControls.Description = UIControls.Description.Create("name:=""Form1"";;value:=""Form1"";;processname:=""SampleWindowsForm""") 'Top level object
        'Dim s As String = Me.SwfWindow(Form1).DumpWindowData()
        'Console.WriteLine(s)
        'System.IO.File.WriteAllText("C:\Share\DotNetForm.txt", s)
        'Return
        Kill()

        System.Diagnostics.Process.Start("Calc")
        System.Threading.Thread.Sleep(100)
        'Recorded Code Below
        Dim l As New System.Collections.Generic.List(Of String)
        l.Add("Edit")
        l.Add("Copy")
        Me.Window("name:=""SciCalc""").Button("name:=""Button"";;value:=""CE""").Click(35, 12)
        Me.Window("name:=""SciCalc""").Button(Calculator.Button_1).Click(20, 17)
        Me.Window(Calculator.SciCalc).Button(Calculator.Button_STAR).Click(21, 9)
        Me.Window(Calculator.SciCalc).Button(Calculator.Button_9).Click(17, 12)
        Me.Window(Calculator.SciCalc).Button(Calculator.Button_EQUL).Click(25, 16)
        Me.Window(Calculator.SciCalc).Button(Calculator.Button_STAR).Click(10, 16)
        Me.Window(Calculator.SciCalc).Button(Calculator.Button_2).Click(24, 16)
        Me.Window(Calculator.SciCalc).Button(Calculator.Button_EQUL).Click(21, 13)
        Report.RecordEvent(Report.Pass, "Value should be 18.", Me.Window(Calculator.SciCalc).TextBox(Calculator.Edit).GetText())
        Report.RecordEvent(Report.Pass, "Contains menu?", Me.Window(Calculator.SciCalc).Menu.ContainsMenu())
        Report.RecordEvent(Report.Pass, "Contains Edit", Me.Window(Calculator.SciCalc).Menu.ContainsMenuItem("Edit"))
        Report.RecordEvent(Report.Pass, "Is Enabled?", Me.Window(Calculator.SciCalc).Menu.IsEnabled(l.ToArray()))
        Me.Window(Calculator.SciCalc).Menu.SelectMenuItem(l.ToArray())
        Dim help As String = ""
        For Each item As String In Me.Window(Calculator.SciCalc).Menu.GetMenuTextBelow("Help")
            help += item + vbNewLine
        Next
        Report.RecordEvent(Pass, "Help sub items", help)
        Report.RecordEvent(Report.Pass, "Clipboard text:", Me.Clipboard.Text)
        Kill()
    End Sub

    Sub Kill()
        For Each p As System.Diagnostics.Process In System.Diagnostics.Process.GetProcessesByName("Calc")
            p.CloseMainWindow()
            Report.RecordEvent(Report.Pass, "process killed :)", "")
        Next
    End Sub
End Class
#End Region


#Region "Description (GAmp) was generated via the recorder..."
Public Class GAmp
    'Description Code Below
    Public Shared Form1 As UIControls.Description = UIControls.Description.Create("name:=""Form1""") 'Top level object
    Public Shared OptionsButton1 As UIControls.Description = UIControls.Description.Create("name:=""OptionsButton1""") 'Parent's name: Form1
    Public Shared SettingsForm As UIControls.Description = UIControls.Description.Create("name:=""SettingsForm""") 'Top level object
    Public Shared TreeView1 As UIControls.Description = UIControls.Description.Create("name:=""TreeView1""") 'Parent's name: SettingsForm
End Class
#End Region

Public Class MyTestClass
    Inherits UIControls.InterAct
    Public Sub New()
        'Recorded Code Below
        Dim Item As Integer = 0
        Dim SelectedItem As Integer
        SwfWindow(GAmp.Form1).SwfButton(GAmp.OptionsButton1).Click(18, 3)
        For Item = 0 To 5
            SwfWindow(GAmp.SettingsForm).SwfTreeView(GAmp.TreeView1).SetSelectedItem(Item)
            System.Threading.Thread.Sleep(5000)
            SelectedItem = SwfWindow(GAmp.SettingsForm).SwfTreeView(GAmp.TreeView1).GetSelectedItemNumber()
            System.Diagnostics.Debug.WriteLine("Selected " & Item & " and UI has " & SelectedItem & " selected.")
        Next
        SwfWindow(GAmp.SettingsForm).Close()
    End Sub
End Class