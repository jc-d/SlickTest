﻿'------------------------------------------------------------------------------
' <auto-generated>
'     This code was generated by a tool.
'     Runtime Version:2.0.50727.4952
'
'     Changes to this file may cause incorrect behavior and will be lost if
'     the code is regenerated.
' </auto-generated>
'------------------------------------------------------------------------------

Option Strict On
Option Explicit On


Namespace My
    
    <Global.System.Runtime.CompilerServices.CompilerGeneratedAttribute(),  _
     Global.System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "9.0.0.0"),  _
     Global.System.ComponentModel.EditorBrowsableAttribute(Global.System.ComponentModel.EditorBrowsableState.Advanced)>  _
    Partial Friend NotInheritable Class MySettings
        Inherits Global.System.Configuration.ApplicationSettingsBase
        
        Private Shared defaultInstance As MySettings = CType(Global.System.Configuration.ApplicationSettingsBase.Synchronized(New MySettings),MySettings)
        
#Region "My.Settings Auto-Save Functionality"
#If _MyType = "WindowsForms" Then
    Private Shared addedHandler As Boolean

    Private Shared addedHandlerLockObject As New Object

    <Global.System.Diagnostics.DebuggerNonUserCodeAttribute(), Global.System.ComponentModel.EditorBrowsableAttribute(Global.System.ComponentModel.EditorBrowsableState.Advanced)> _
    Private Shared Sub AutoSaveSettings(ByVal sender As Global.System.Object, ByVal e As Global.System.EventArgs)
        If My.Application.SaveMySettingsOnExit Then
            My.Settings.Save()
        End If
    End Sub
#End If
#End Region
        
        Public Shared ReadOnly Property [Default]() As MySettings
            Get
                
#If _MyType = "WindowsForms" Then
               If Not addedHandler Then
                    SyncLock addedHandlerLockObject
                        If Not addedHandler Then
                            AddHandler My.Application.Shutdown, AddressOf AutoSaveSettings
                            addedHandler = True
                        End If
                    End SyncLock
                End If
#End If
                Return defaultInstance
            End Get
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("False")>  _
        Public Property Recorder_Default__XY__Record__Style() As Boolean
            Get
                Return CType(Me("Recorder_Default__XY__Record__Style"),Boolean)
            End Get
            Set
                Me("Recorder_Default__XY__Record__Style") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("True")>  _
        Public Property Reporter_Show__Report() As Boolean
            Get
                Return CType(Me("Reporter_Show__Report"),Boolean)
            End Get
            Set
                Me("Reporter_Show__Report") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("144, 144")>  _
        Public Property FormLocation() As Global.System.Drawing.Point
            Get
                Return CType(Me("FormLocation"),Global.System.Drawing.Point)
            End Get
            Set
                Me("FormLocation") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("704, 366")>  _
        Public Property FormSize() As Global.System.Drawing.Size
            Get
                Return CType(Me("FormSize"),Global.System.Drawing.Size)
            End Get
            Set
                Me("FormSize") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("Normal")>  _
        Public Property FormState() As Global.System.Windows.Forms.FormWindowState
            Get
                Return CType(Me("FormState"),Global.System.Windows.Forms.FormWindowState)
            End Get
            Set
                Me("FormState") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("")>  _
        Public Property Project_Default__ClassName() As String
            Get
                Return CType(Me("Project_Default__ClassName"),String)
            End Get
            Set
                Me("Project_Default__ClassName") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("True")>  _
        Public Property Project_Default__Show__UI() As Boolean
            Get
                Return CType(Me("Project_Default__Show__UI"),Boolean)
            End Get
            Set
                Me("Project_Default__Show__UI") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("True")>  _
        Public Property Project_Default__Option__Explicit() As Boolean
            Get
                Return CType(Me("Project_Default__Option__Explicit"),Boolean)
            End Get
            Set
                Me("Project_Default__Option__Explicit") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("False")>  _
        Public Property Project_Default__Option__Strict() As Boolean
            Get
                Return CType(Me("Project_Default__Option__Strict"),Boolean)
            End Get
            Set
                Me("Project_Default__Option__Strict") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("Courier New, 9.75pt")>  _
        Public Property UI_Font() As Global.System.Drawing.Font
            Get
                Return CType(Me("UI_Font"),Global.System.Drawing.Font)
            End Get
            Set
                Me("UI_Font") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("False")>  _
        Public Property UI_Highlight__Edit__Line() As Boolean
            Get
                Return CType(Me("UI_Highlight__Edit__Line"),Boolean)
            End Get
            Set
                Me("UI_Highlight__Edit__Line") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("True")>  _
        Public Property UI_Show__Line__Numbers() As Boolean
            Get
                Return CType(Me("UI_Show__Line__Numbers"),Boolean)
            End Get
            Set
                Me("UI_Show__Line__Numbers") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("True")>  _
        Public Property UI_Show__Tab__Markers() As Boolean
            Get
                Return CType(Me("UI_Show__Tab__Markers"),Boolean)
            End Get
            Set
                Me("UI_Show__Tab__Markers") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("True")>  _
        Public Property ShouldUpdateSettings() As Boolean
            Get
                Return CType(Me("ShouldUpdateSettings"),Boolean)
            End Get
            Set
                Me("ShouldUpdateSettings") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("False")>  _
        Public Property UI_Show__Space__Markers() As Boolean
            Get
                Return CType(Me("UI_Show__Space__Markers"),Boolean)
            End Get
            Set
                Me("UI_Show__Space__Markers") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("False")>  _
        Public Property UI_Show__New__Line__Markers() As Boolean
            Get
                Return CType(Me("UI_Show__New__Line__Markers"),Boolean)
            End Get
            Set
                Me("UI_Show__New__Line__Markers") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("True")>  _
        Public Property UI_Show__Matching__Brackets() As Boolean
            Get
                Return CType(Me("UI_Show__Matching__Brackets"),Boolean)
            End Get
            Set
                Me("UI_Show__Matching__Brackets") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("<?xml version=""1.0"" encoding=""utf-16""?>"&Global.Microsoft.VisualBasic.ChrW(13)&Global.Microsoft.VisualBasic.ChrW(10)&"<ArrayOfString xmlns:xsi=""http://www.w3."& _ 
            "org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"">"&Global.Microsoft.VisualBasic.ChrW(13)&Global.Microsoft.VisualBasic.ChrW(10)&"  <s"& _ 
            "tring>C:\WINDOWS\Microsoft.NET\Framework\v2.0.50727\System.dll</string>"&Global.Microsoft.VisualBasic.ChrW(13)&Global.Microsoft.VisualBasic.ChrW(10)&"  <stri"& _ 
            "ng>C:\WINDOWS\Microsoft.NET\Framework\v2.0.50727\System.Data.dll</string>"&Global.Microsoft.VisualBasic.ChrW(13)&Global.Microsoft.VisualBasic.ChrW(10)&"  <st"& _ 
            "ring>C:\WINDOWS\Microsoft.NET\Framework\v2.0.50727\System.Drawing.dll</string>"&Global.Microsoft.VisualBasic.ChrW(13)&Global.Microsoft.VisualBasic.ChrW(10)& _ 
            "  <string>C:\WINDOWS\Microsoft.NET\Framework\v2.0.50727\Microsoft.VisualBasic.dl"& _ 
            "l</string>"&Global.Microsoft.VisualBasic.ChrW(13)&Global.Microsoft.VisualBasic.ChrW(10)&"  <string>C:\WINDOWS\Microsoft.NET\Framework\v2.0.50727\System.Windo"& _ 
            "ws.Forms.dll</string>"&Global.Microsoft.VisualBasic.ChrW(13)&Global.Microsoft.VisualBasic.ChrW(10)&"</ArrayOfString>")>  _
        Public Property Project_Default__DLL__Auto__Includes() As Global.System.Collections.Specialized.StringCollection
            Get
                Return CType(Me("Project_Default__DLL__Auto__Includes"),Global.System.Collections.Specialized.StringCollection)
            End Get
            Set
                Me("Project_Default__DLL__Auto__Includes") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("10")>  _
        Public Property Project_Default__Runtime__Timeout() As Integer
            Get
                Return CType(Me("Project_Default__Runtime__Timeout"),Integer)
            End Get
            Set
                Me("Project_Default__Runtime__Timeout") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("False")>  _
        Public Property Recorder_Absolute__Coordinates() As Boolean
            Get
                Return CType(Me("Recorder_Absolute__Coordinates"),Boolean)
            End Get
            Set
                Me("Recorder_Absolute__Coordinates") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("True")>  _
        Public Property Project_Default__Load__Last__Project() As Boolean
            Get
                Return CType(Me("Project_Default__Load__Last__Project"),Boolean)
            End Get
            Set
                Me("Project_Default__Load__Last__Project") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("")>  _
        Public Property LastProject() As String
            Get
                Return CType(Me("LastProject"),String)
            End Get
            Set
                Me("LastProject") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("\Template\NewFile.vb")>  _
        Public Property Project_Default__New__File__Template() As String
            Get
                Return CType(Me("Project_Default__New__File__Template"),String)
            End Get
            Set
                Me("Project_Default__New__File__Template") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("\Template\NewProjectFile.vb")>  _
        Public Property Project_Default__Main__File__Template() As String
            Get
                Return CType(Me("Project_Default__Main__File__Template"),String)
            End Get
            Set
                Me("Project_Default__Main__File__Template") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("False")>  _
        Public Property Project_Default__Take__Picture__Before__Clicking() As Boolean
            Get
                Return CType(Me("Project_Default__Take__Picture__Before__Clicking"),Boolean)
            End Get
            Set
                Me("Project_Default__Take__Picture__Before__Clicking") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("False")>  _
        Public Property Project_Default__Take__Picture__After__Clicking() As Boolean
            Get
                Return CType(Me("Project_Default__Take__Picture__After__Clicking"),Boolean)
            End Get
            Set
                Me("Project_Default__Take__Picture__After__Clicking") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("False")>  _
        Public Property Project_Default__Take__Picture__Before__Typing() As Boolean
            Get
                Return CType(Me("Project_Default__Take__Picture__Before__Typing"),Boolean)
            End Get
            Set
                Me("Project_Default__Take__Picture__Before__Typing") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("False")>  _
        Public Property Project_Default__Take__Picture__After__Typing() As Boolean
            Get
                Return CType(Me("Project_Default__Take__Picture__After__Typing"),Boolean)
            End Get
            Set
                Me("Project_Default__Take__Picture__After__Typing") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("")>  _
        Public Property Project_Library__Location() As String
            Get
                Return CType(Me("Project_Library__Location"),String)
            End Get
            Set
                Me("Project_Library__Location") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("False")>  _
        Public Property Project_Library__Projects__Auto__Copied() As Boolean
            Get
                Return CType(Me("Project_Library__Projects__Auto__Copied"),Boolean)
            End Get
            Set
                Me("Project_Library__Projects__Auto__Copied") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("")>  _
        Public Property Project_Default__External__Report__Connection__String() As String
            Get
                Return CType(Me("Project_Default__External__Report__Connection__String"),String)
            End Get
            Set
                Me("Project_Default__External__Report__Connection__String") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("0")>  _
        Public Property Recorder_Default__Sleep__Time__MS() As Integer
            Get
                Return CType(Me("Recorder_Default__Sleep__Time__MS"),Integer)
            End Get
            Set
                Me("Recorder_Default__Sleep__Time__MS") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("")>  _
        Public Property Recorder_Default__Object__Reference() As String
            Get
                Return CType(Me("Recorder_Default__Object__Reference"),String)
            End Get
            Set
                Me("Recorder_Default__Object__Reference") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("35")>  _
        Public Property Recorder_Default__Total__Description__Length() As Integer
            Get
                Return CType(Me("Recorder_Default__Total__Description__Length"),Integer)
            End Get
            Set
                Me("Recorder_Default__Total__Description__Length") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("True")>  _
        Public Property UI_Enable__Folding() As Boolean
            Get
                Return CType(Me("UI_Enable__Folding"),Boolean)
            End Get
            Set
                Me("UI_Enable__Folding") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("/verbose+ /errorreport:none")>  _
        Public Property Project_Default__Additional__Compiler__Options() As String
            Get
                Return CType(Me("Project_Default__Additional__Compiler__Options"),String)
            End Get
            Set
                Me("Project_Default__Additional__Compiler__Options") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("SlickUnit")>  _
        Public Property UnitRunner_Default__Execution__Type() As Global.SlickUnitRunner.FrameworkType
            Get
                Return CType(Me("UnitRunner_Default__Execution__Type"),Global.SlickUnitRunner.FrameworkType)
            End Get
            Set
                Me("UnitRunner_Default__Execution__Type") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("")>  _
        Public Property UnitRunner_External__UnitTest__FileDirectory() As String
            Get
                Return CType(Me("UnitRunner_External__UnitTest__FileDirectory"),String)
            End Get
            Set
                Me("UnitRunner_External__UnitTest__FileDirectory") = value
            End Set
        End Property
    End Class
End Namespace

Namespace My
    
    <Global.Microsoft.VisualBasic.HideModuleNameAttribute(),  _
     Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
     Global.System.Runtime.CompilerServices.CompilerGeneratedAttribute()>  _
    Friend Module MySettingsProperty
        
        <Global.System.ComponentModel.Design.HelpKeywordAttribute("My.Settings")>  _
        Friend ReadOnly Property Settings() As Global.SlickTestDeveloper.My.MySettings
            Get
                Return Global.SlickTestDeveloper.My.MySettings.Default
            End Get
        End Property
    End Module
End Namespace
