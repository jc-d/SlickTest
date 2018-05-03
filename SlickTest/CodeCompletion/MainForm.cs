// CSharp Editor Example with Code Completion
// Copyright (c) 2006, Daniel Grunwald
// All rights reserved.
// 
// Redistribution and use in source and binary forms, with or without modification, are
// permitted provided that the following conditions are met:
// 
// - Redistributions of source code must retain the above copyright notice, this list
//   of conditions and the following disclaimer.
// 
// - Redistributions in binary form must reproduce the above copyright notice, this list
//   of conditions and the following disclaimer in the documentation and/or other materials
//   provided with the distribution.
// 
// - Neither the name of the ICSharpCode team nor the names of its contributors may be used to
//   endorse or promote products derived from this software without specific prior written
//   permission.
// 
// THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS &AS IS& AND ANY EXPRESS
// OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED WARRANTIES OF MERCHANTABILITY
// AND FITNESS FOR A PARTICULAR PURPOSE ARE DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT OWNER OR
// CONTRIBUTORS BE LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL
// DAMAGES (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE,
// DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER
// IN CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT
// OF THE USE OF THIS SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.

//#undef UseFolding
#define UseFolding

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using System.Threading;
using ICSharpCode.SharpDevelop;

using NRefactory = ICSharpCode.NRefactory;
using Dom = ICSharpCode.SharpDevelop.Dom;


namespace VBEditor
{
    public partial class MainForm
    {
        [STAThread]
        public static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
        }

        internal Dom.ProjectContentRegistry pcRegistry;
        internal Dom.DefaultProjectContent myProjectContent;
        internal Dom.ParseInformation parseInformation = new Dom.ParseInformation();
        Dom.ICompilationUnit lastCompilationUnit;
        public System.Collections.Generic.List<String> AddedAsms;
        private bool doAddAsm;
        private static bool FirstRun = true;
        Thread parserThread;
        public CodeCompletionKeyHandler CodeCompletionKeyHandler;

        public static bool IsVisualBasic = true;

        /// <summary>
        /// Many SharpDevelop.Dom methods take a file name, which is really just a unique identifier
        /// for a file - Dom methods don't try to access code files on disk, so the file does not have
        /// to exist.
        /// SharpDevelop itself uses internal names of the kind "[randomId]/Class1.cs" to support
        /// code-completion in unsaved files.
        /// </summary>
        public static string DummyFileName { 
            get 
            {
                if (IsVisualBasic)
                    return "edited.vb";
                else
                    return "edited.cs";
            } 
        }

        public static Dom.LanguageProperties CurrentLanguageProperties = Dom.LanguageProperties.VBNet;

        public void SetHighlighting()
        {
            TxtEdit.SetHighlighting(IsVisualBasic);
        }

        private bool EnableFolding 
        { 
            get { return TxtEdit.Document.TextEditorProperties.EnableFolding; }
            set { TxtEdit.Document.TextEditorProperties.EnableFolding = value; }
        }

        public void SetupFolding(bool Enable)
        {
            EnableFolding = Enable;
            if (Enable)
            {
                TxtEdit.Document.FoldingManager.FoldingStrategy = new ICSharpCode.SharpDevelop.DefaultEditor.Gui.Editor.ParserFoldingStrategy();
            }
        }

        public void UpdateLanguageForEditor()
        {
            SetHighlighting();
            if(IsVisualBasic)
                CurrentLanguageProperties = Dom.LanguageProperties.VBNet;
            else
                CurrentLanguageProperties = Dom.LanguageProperties.CSharp;
            SetFormatStratagy();
        }

        public void UpdateLanguageForEditor(bool isVBNet)
        {
            if (IsVisualBasic == isVBNet) return;
            IsVisualBasic = isVBNet;
            UpdateLanguageForEditor();
        }

        public void SetFormatStratagy()
        {
            if (IsVisualBasic)
                //Allows for the auto complete of things like this Public Sub X()<auto completes>End Sub</auto completes>
                TxtEdit.Document.FormattingStrategy = new VBNetBinding.FormattingStrategy.VBFormattingStrategy();
            else
                TxtEdit.Document.FormattingStrategy = new ICSharpCode.TextEditor.Document.DefaultFormattingStrategy();

        }


        public MainForm()
        {
            //
            // The InitializeComponent() call is required for Windows Forms designer support.
            //
            InitializeComponent();
            doAddAsm = false;
            AddedAsms = new System.Collections.Generic.List<String>();
            SetHighlighting();
            //Allows for folding to work:

#if UseFolding
            SetupFolding(true);
#endif
            TxtEdit.Document.TextEditorProperties.AutoInsertCurlyBracket = true;
            SetFormatStratagy();
            //Sets the tabs to work smartly, making it easier to write code.
            TxtEdit.ActiveTextAreaControl.TextArea.MotherTextEditorControl.IndentStyle = ICSharpCode.TextEditor.Document.IndentStyle.Smart;
            this.TxtEdit.ShowSpaces = false;
            CodeCompletionKeyHandler.Attach(this, TxtEdit);
            ToolTipProvider.Attach(this, TxtEdit);
            HostCallbackImplementation.Register(this);
            if (FirstRun == true)
            {
                try
                {//Added just so that Designer will show up.
                    new ICSharpCode.Core.CoreStartup("Slick Test").StartCoreServices();//CoreServices may need to be saved rather than
                }
                catch (Exception)
                { }
                //thrown away like this.
                FirstRun = false;
            }
            pcRegistry = new Dom.ProjectContentRegistry(); // Default .NET 2.0 registry

            // Persistence lets SharpDevelop.Dom create a cache file on disk so that
            // future starts are faster.
            // It also caches XML documentation files in an on-disk hash table, thus
            // reducing memory usage.
            String PathCC = Path.Combine(Path.GetTempPath(), "SlickTestCodeCompletion");

            try
            {
                if (System.IO.File.Exists(System.IO.Path.Combine(PathCC, "ResetCache.txt")))
                    System.IO.Directory.Delete(PathCC,true);
            }catch
            {
                MessageBox.Show("Failed to ResetCache.  You can manually delete the cache after closing Slick Test at " + PathCC + ".");
            }
            try
            {
                if (System.IO.Directory.Exists(PathCC) == false)
                    System.IO.Directory.CreateDirectory(PathCC);
                doAddAsm = true;
            }
            catch
            {
                doAddAsm = false;
            }
            pcRegistry.ActivatePersistence(PathCC);
            myProjectContent = new Dom.DefaultProjectContent();
            myProjectContent.Language = CurrentLanguageProperties;
        }

        public bool AddAssemblies()
        {
            if (doAddAsm == true)
            {
                try
                {
                    parserThread.Abort();
                    int Counter = 0;
                    do
                    {
                        if (parserThread.IsAlive == false)
                            break;
                        Counter++;
                    } while (Counter != 20);
                }
                catch
                {
                }
                parserThread = new Thread(ParserThread);
                parserThread.IsBackground = true;
                parserThread.Start(AddedAsms);
                return true;
            }
            return false;
        }

        //void ParserThread(object param)
        //{
        //    BeginInvoke(new MethodInvoker(delegate { UserInformationBar.Text = "Loading mscorlib..."; }));
        //    myProjectContent.AddReferencedContent(pcRegistry.Mscorlib);

        //    // do one initial parser step to enable code-completion while other
        //    // references are loading
        //    ParseStep();

        //    string[] referencedAssemblies = {
        //                "System", "System.Data", "System.Drawing", "System.Xml", "System.Windows.Forms", "Microsoft.VisualBasic"
        //            };
        //    foreach (string assemblyName in referencedAssemblies)
        //    {
        //        string assemblyNameCopy = assemblyName; // copy for anonymous method
        //        BeginInvoke(new MethodInvoker(delegate { UserInformationBar.Text = "Loading " + assemblyNameCopy + "..."; }));
        //        Dom.IProjectContent referenceProjectContent = pcRegistry.GetProjectContentForReference(assemblyName, assemblyName);
        //        myProjectContent.AddReferencedContent(referenceProjectContent);
        //        if (referenceProjectContent is Dom.ReflectionProjectContent)
        //        {
        //            (referenceProjectContent as Dom.ReflectionProjectContent).InitializeReferences();
        //        }
        //    }
        //    if (IsVisualBasic)
        //    {
        //        myProjectContent.DefaultImports = new Dom.DefaultUsing(myProjectContent);
        //        myProjectContent.DefaultImports.Usings.Add("System");
        //        myProjectContent.DefaultImports.Usings.Add("System.Text");
        //        myProjectContent.DefaultImports.Usings.Add("Microsoft.VisualBasic");
        //    }
        //    BeginInvoke(new MethodInvoker(delegate { UserInformationBar.Text = "Ready"; }));

        //    // Parse the current file every 2 seconds
        //    while (!IsDisposed)
        //    {
        //        ParseStep();

        //        Thread.Sleep(2000);
        //    }
        //}

        void ParserThread(object param)
        {
            System.Collections.Generic.List<String> AddedItems = (System.Collections.Generic.List<String>)param;
            System.Collections.Generic.List<String> ItemsSuccessfullyAdded = new System.Collections.Generic.List<String>();
            BeginInvoke(new MethodInvoker(delegate { UserInformationBar.Text = "Loading mscorlib..."; }));
            myProjectContent = new Dom.DefaultProjectContent();
            myProjectContent.Language = CurrentLanguageProperties;

            myProjectContent.AddReferencedContent(pcRegistry.Mscorlib);

            // do one initial parser step to enable code-completion while other
            // references are loading
            ParseStep();
            if (AddedItems.Count > 0)
            {
                string[] ReferencedAssemblies = AddedItems.ToArray();

                foreach (string AssemblyName in ReferencedAssemblies)
                {
                    string AssemblyLocation = AssemblyName;
                    { // block for anonymous method (capture assemblyNameCopy correctly)
                        string AssemblyNameCopy;

                        if (AssemblyName.Contains(";") == false)
                        {
                            AssemblyNameCopy = AssemblyName;
                            if (System.IO.File.Exists(AssemblyNameCopy) == true)
                            {
                                AssemblyLocation = AssemblyNameCopy;//maybe bad not workingness
                                AssemblyNameCopy = System.Reflection.AssemblyName.GetAssemblyName(AssemblyNameCopy).Name;
                            }
                            try
                            {
                                BeginInvoke(new MethodInvoker(delegate { UserInformationBar.Text = "Loading " + AssemblyNameCopy + "..."; }));
                            }
                            catch (Exception)
                            {
                                return;
                            }

                        }
                        else
                        {
                            AssemblyNameCopy = AssemblyName.Split(';')[0];
                            BeginInvoke(new MethodInvoker(delegate { UserInformationBar.Text = "Loading " + AssemblyNameCopy + "..."; }));
                            AssemblyLocation = AssemblyName.Split(';')[1];
                        }
                    }
                    if (System.IO.File.Exists(AssemblyLocation) == true || AssemblyLocation == AssemblyName)
                    {
                        if (ItemsSuccessfullyAdded.Contains(AssemblyLocation) == false)
                        {
                            myProjectContent.AddReferencedContent(pcRegistry.GetProjectContentForReference(AssemblyName, AssemblyLocation));
                            ItemsSuccessfullyAdded.Add(AssemblyLocation);
                        }

                    }
                }
            }
            BeginInvoke(new MethodInvoker(delegate { UserInformationBar.Text = "Ready"; }));
            //AddedItems.Clear();
            // Parse the current file every 2 seconds
            while (!IsDisposed)
            {
                try
                {
                    ParseStep();
                }
                catch
                {
                    Thread.Sleep(600);
                }

                Thread.Sleep(2500);
                //if (AddedItems.Count != 0)//allows to add items later...
                //{
                //    string[] AddItemsArray;

                //    lock (AddedItems)
                //    {
                //        AddItemsArray = AddedItems.ToArray();
                //        AddedItems.Clear();
                //    }
                //    foreach (string AssemblyName in AddItemsArray)
                //    {
                //        string AssemblyLocation = AssemblyName;
                //        { // block for anonymous method (capture assemblyNameCopy correctly)
                //            string AssemblyNameCopy = String.Empty;
                //            if (AssemblyName.Contains(";") == false)
                //            {
                //                AssemblyNameCopy = AssemblyName;

                //                if (System.IO.File.Exists(AssemblyNameCopy) == true)
                //                {
                //                    AssemblyLocation = AssemblyNameCopy;//maybe bad not workingness
                //                    AssemblyNameCopy = System.Reflection.AssemblyName.GetAssemblyName(AssemblyNameCopy).Name;
                //                }
                //                if (ItemsSuccessfullyAdded.Contains(AssemblyLocation) == false)
                //                {
                //                    BeginInvoke(new MethodInvoker(delegate { UserInformationBar.Text = "Loading " + AssemblyNameCopy + "..."; }));
                //                }
                //            }
                //            else
                //            {
                //                AssemblyNameCopy = AssemblyName.Split(';')[0];
                //                AssemblyLocation = AssemblyName.Split(';')[1];
                //                if (ItemsSuccessfullyAdded.Contains(AssemblyLocation) == false)
                //                {
                //                    BeginInvoke(new MethodInvoker(delegate { UserInformationBar.Text = "Loading " + AssemblyNameCopy + "..."; }));
                //                }

                //                //readAssembly(AssemblyLocation, true);
                //            }
                //        }
                //        if (System.IO.File.Exists(AssemblyLocation) == true || AssemblyLocation == AssemblyName)
                //        {
                //            if (ItemsSuccessfullyAdded.Contains(AssemblyLocation) == false)
                //            {
                //                try
                //                {
                //                    myProjectContent.AddReferencedContent(pcRegistry.GetProjectContentForReference(AssemblyName, AssemblyLocation));
                //                    ItemsSuccessfullyAdded.Add(AssemblyLocation);
                //                    BeginInvoke(new MethodInvoker(delegate { UserInformationBar.Text = "Ready"; }));
                //                }
                //                catch
                //                { }
                //            }
                //        }

                //    }

                //}
            }
        }

        void ParseStep()
        {
            string code = null;
            try
            {
                Invoke(new MethodInvoker(delegate
                {
                    code = TxtEdit.Text;
                }));
            }
            catch
            {
            }

            TextReader textReader = new StringReader(code);
            Dom.ICompilationUnit newCompilationUnit;
            NRefactory.SupportedLanguage supportedLanguage;
            if (IsVisualBasic)
                supportedLanguage = NRefactory.SupportedLanguage.VBNet;
            else
                supportedLanguage = NRefactory.SupportedLanguage.CSharp;


            using (NRefactory.IParser p = NRefactory.ParserFactory.CreateParser(supportedLanguage, textReader))
            {
                p.Parse();
                newCompilationUnit = ConvertCompilationUnit(p.CompilationUnit);
#if UseFolding
                if (EnableFolding)
                {
                    //This code is for folding.
                    int Count = 0;
                    ICSharpCode.NRefactory.PreprocessingDirective directive = null;
                    ICSharpCode.NRefactory.PreprocessingDirective nextDirective = null;

                    foreach (NRefactory.ISpecial item in p.Lexer.SpecialTracker.CurrentSpecials)
                    {
                        if (item is ICSharpCode.NRefactory.PreprocessingDirective)
                        {
                            if ((Count % 2) == 0)
                            {
                                directive = (ICSharpCode.NRefactory.PreprocessingDirective)item;
                            }
                            else
                            {
                                nextDirective = (ICSharpCode.NRefactory.PreprocessingDirective)item;
                                //newCompilationUnit.FoldingRegions.Add(new ICSharpCode.SharpDevelop.Dom.FoldingRegion(directive.Arg.Trim('"'), new ICSharpCode.SharpDevelop.Dom.DomRegion(directive.StartPosition, nextDirective.EndPosition)));
                                newCompilationUnit.FoldingRegions.Add(
                                    new ICSharpCode.SharpDevelop.Dom.FoldingRegion(
                                        directive.Arg.Trim('"'), new ICSharpCode.SharpDevelop.Dom.DomRegion(
                                            directive.StartPosition.Line, directive.StartPosition.Column, nextDirective.EndPosition.Line, nextDirective.EndPosition.Column)));
                            }
                            Count++;
                        }
                    }
                    Invoke(new MethodInvoker(delegate
                   {
                       this.TxtEdit.Refresh();
                   }));
                }
#endif
            }
            // Remove information from lastCompilationUnit and add information from newCompilationUnit.
            myProjectContent.UpdateCompilationUnit(lastCompilationUnit, newCompilationUnit, DummyFileName);
            lastCompilationUnit = newCompilationUnit;
            parseInformation.SetCompilationUnit(newCompilationUnit);
#if UseFolding
            if (EnableFolding)
            {
                try
                {
                    TxtEdit.Document.FoldingManager.UpdateFoldings(DummyFileName, parseInformation);
                    //TxtEdit.Document.FoldingManager.UpdateFoldings(TxtEdit.Document.FoldingManager.FoldingStrategy.GenerateFoldMarkers(TxtEdit.Document, DummyFileName, parseInformation));
                }
                catch (Exception)
                {
                }
            }
#endif
        }

        Dom.ICompilationUnit ConvertCompilationUnit(NRefactory.Ast.CompilationUnit cu)
        {
            Dom.NRefactoryResolver.NRefactoryASTConvertVisitor converter;
            converter = new Dom.NRefactoryResolver.NRefactoryASTConvertVisitor(myProjectContent);
            cu.AcceptVisitor(converter, null);
            return converter.Cu;
        }
    }
}