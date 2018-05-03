Imports System.Drawing
Imports System
Imports System.Collections.Specialized
Imports Microsoft.VisualStudio.TestTools.UnitTesting
Imports UIControls

<TestClass()> _
Public Class FindWindowsUsingDifferentDescriptions
    Inherits DotNetTests

    <TestMethod()> _
    Public Sub UseRecorder()
        SwfWindow(TestApp.Form1_Form1).SwfCheckBox(TestApp.CheckBox1).SetChecked(SwfCheckBox.UNCHECKED)
        System.Threading.Thread.Sleep(500)
        SwfWindow(TestApp.Form1_Form1).SwfButton(TestApp.CheckBox1).Click(42, 14)
        Assert.IsFalse(SwfWindow(TestApp.Form1_Form1).SwfWinObject(TestApp.GroupBox2).SwfRadioButton(TestApp.RadioButton1).GetSelected())
        SwfWindow(TestApp.Form1_Form1).SwfWinObject(TestApp.GroupBox2).SwfRadioButton(TestApp.RadioButton1).Click(44, 8)
        Assert.IsTrue(SwfWindow(TestApp.Form1_Form1).SwfWinObject(TestApp.GroupBox2).SwfRadioButton(TestApp.RadioButton1).GetSelected())
        SwfWindow(TestApp.Form1_Form1).SwfTreeView(TestApp.TreeView1).Click(62, 56)
        SwfWindow(TestApp.Form1_Form1).SwfWinObject(TestApp.GroupBox1).SwfComboBox(TestApp.ComboBox1).SwfTextBox(TestApp.Edit).Click(67, 4) 'NOTE: We get a TextBox, rather than a SWF text box
        SwfWindow(TestApp.Form1_Form1).SwfWinObject(TestApp.GroupBox1).SwfListBox(TestApp.CheckedListBox1).Click(69, 15)
        SwfWindow(TestApp.Form1_Form1).SwfWinObject(TestApp.GroupBox1).SwfListBox(TestApp.ListBox1).Click(59, 14)
        SwfWindow(TestApp.Form1_Form1).SwfListView(TestApp.ListView1).Click(74, 46)
        SwfWindow(TestApp.Form1_Form1).SwfListView(TestApp.ListView1).Click(30, 57)
        SwfWindow(TestApp.Form1_Form1).SwfTextBox(TestApp.TextBox1).Click(90, 17)
        SwfWindow(TestApp.Form1_Form1).SwfTextBox(TestApp.TextBox2).Click(85, 2)
        SwfWindow(TestApp.Form1_Form1).SwfTabControl(TestApp.TabControl1).Click(50, 13)
        SwfWindow(TestApp.Form1_Form1).SwfTabControl(TestApp.TabControl1).Click(77, 8)
        SwfWindow(TestApp.Form1_Form1).SwfTabControl(TestApp.TabControl1).SwfWinObject(TestApp.TabPage2).SwfButton(TestApp.Button4).Click(52, 8)
        SwfWindow(TestApp.Form1_Form1).SwfButton(TestApp.Button1).Click(32, 16)
        SwfWindow(TestApp.Form1_Form1).SwfButton(TestApp.Button2).Click(33, 1)
        SwfWindow(TestApp.Form1_Form1).SwfTextBox(TestApp.RichTextBox1).Click(34, 20)
        SwfWindow(TestApp.Form1_Form1).SwfStaticLabel(TestApp.Label1).Click(27, 3)
        Assert.AreEqual(SwfCheckBox.CHECKED, SwfWindow(TestApp.Form1_Form1).SwfCheckBox(TestApp.CheckBox1).GetChecked())
    End Sub

    <TestMethod()> _
    Public Sub DescriptionNearByLabel()
        Dim desc As UIControls.Description = Description.Create()
        desc.Add(APIControls.Description.DescriptionData.NearByLabel, "CheckBoxes:")

        Assert.IsTrue(SwfWindow(TestApp.Form1_Form1).SwfCheckBox(desc).Exists())
    End Sub

    <TestMethod()> _
    Public Sub DesciptionIndex()
        Dim desc As UIControls.Description = Description.Create()
        desc.Add(APIControls.Description.DescriptionData.Index, "24")

        Assert.IsTrue(SwfWindow(TestApp.Form1_Form1).SwfTextBox(desc).Exists())
        Assert.AreEqual("CheckBox2", SwfWindow(TestApp.Form1_Form1).SwfTextBox(desc).GetControlName())
    End Sub

    <TestMethod()> _
    Public Sub DesciptionValue()
        Dim desc As UIControls.Description = Description.Create()
        desc.Add(APIControls.Description.DescriptionData.Value, "TabPage1")

        Assert.IsTrue(SwfWindow(TestApp.Form1_Form1).SwfTabControl(desc).Exists())
    End Sub

    <TestMethod()> _
    Public Sub DesciptionWildCard_Value()
        Dim desc As UIControls.Description = Description.Create()
        desc.Add(APIControls.Description.DescriptionData.Value, "TreeView*")
        desc.Add(APIControls.Description.DescriptionData.WildCard, "True")

        Assert.IsTrue(SwfWindow(TestApp.Form1_Form1).SwfStaticLabel(desc).Exists())
    End Sub

    <TestMethod()> _
    Public Sub DesciptionRight()
        Dim desc As UIControls.Description = Description.Create()
        desc.Add(APIControls.Description.DescriptionData.Right, "161")
        SwfWindow(TestApp.Form1_Form1).SetWindowState(Windows.Forms.FormWindowState.Maximized)
        System.Threading.Thread.Sleep(500)

        Console.WriteLine(SwfWindow(TestApp.Form1_Form1).SwfTreeView(TestApp.TreeView1).GetRight.ToString())

        Assert.IsTrue(SwfWindow(TestApp.Form1_Form1).SwfTreeView(desc).Exists()) '	name='TreeView1'
    End Sub

    <TestMethod()> _
    Public Sub DesciptionBottom()
        Dim desc As UIControls.Description = Description.Create()
        desc.Add(APIControls.Description.DescriptionData.Bottom, "452")

        SwfWindow(TestApp.Form1_Form1).SetWindowState(Windows.Forms.FormWindowState.Maximized)
        System.Threading.Thread.Sleep(500)
        Console.WriteLine(SwfWindow(TestApp.Form1_Form1).SwfTreeView(TestApp.TreeView1).GetBottom.ToString())

        Assert.IsTrue(SwfWindow(TestApp.Form1_Form1).SwfTreeView(desc).Exists()) '	name='TreeView1'
    End Sub

    <TestMethod()> _
    Public Sub DesciptionTop()
        Dim desc As UIControls.Description = Description.Create()
        desc.Add(APIControls.Description.DescriptionData.Top, "252")

        SwfWindow(TestApp.Form1_Form1).SetWindowState(Windows.Forms.FormWindowState.Maximized)
        System.Threading.Thread.Sleep(500)
        Console.WriteLine(SwfWindow(TestApp.Form1_Form1).SwfTreeView(TestApp.TreeView1).GetTop.ToString())

        Assert.IsTrue(SwfWindow(TestApp.Form1_Form1).SwfTreeView(desc).Exists()) '	name='TreeView1'
    End Sub

    <TestMethod()> _
    Public Sub DesciptionProcessName()
        Dim desc As UIControls.Description = Description.Create()
        desc.Add(APIControls.Description.DescriptionData.ProcessName, "DotNetTest")

        Assert.IsTrue(SwfWindow(desc).Exists())
    End Sub

    <TestMethod()> _
    Public Sub DesciptionHeight()
        Dim desc As UIControls.Description = Description.Create()
        desc.Add(APIControls.Description.DescriptionData.Height, "200")

        Assert.IsTrue(SwfWindow(desc).Exists())
    End Sub

    <TestMethod()> _
    Public Sub DesciptionWidth()
        Dim desc As UIControls.Description = Description.Create()
        desc.Add(APIControls.Description.DescriptionData.Width, "148")

        Assert.IsTrue(SwfWindow(desc).Exists())
    End Sub
End Class