using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using ICSharpCode.TextEditor;
using ICSharpCode.TextEditor.Document;
using System.IO;
using NRefactory = ICSharpCode.NRefactory;
using Dom = ICSharpCode.SharpDevelop.Dom;

//
namespace TextEditor
{
    //Needs to be added:
    //'Dim Undo As new ICSharpCode.TextEditor.Undo.UndoableDelete(
    //'TextBoxControl.Document.UndoStack.Push(
    public partial class TextEditorBox : ICSharpCode.TextEditor.TextEditorControl
    {
        public Boolean CurrentlySaved = true;
        public const string VBLANGUAGEEXT = ".vb";
        public const string CSHARPLANGUAGEEXT = ".cs";

        private string LanguageExt = VBLANGUAGEEXT;

        private string GetCommentForLanguageExt()
        {
            if (LanguageExt == VBLANGUAGEEXT) return "'";
            return "//";
        }

        public void SetHighlighting(bool IsVisualBasic)
        {
            if (IsVisualBasic)
                SetHighlightStyleByExt(".vb");
            else
                SetHighlightStyleByExt(".cs");
        }

        public void Copy()
        {
            if (this.ActiveTextAreaControl.SelectionManager.HasSomethingSelected == true)
            {
                this.ActiveTextAreaControl.TextArea.ClipboardHandler.Copy(null, null);
                //if (this.ActiveTextAreaControl.SelectionManager.SelectionIsReadonly == false)
                //{
                //    try
                //    {
                //        System.Windows.Forms.Clipboard.Clear();
                //        System.Windows.Forms.Clipboard.SetText(this.ActiveTextAreaControl.SelectionManager.SelectedText);
                //    }
                //    catch (System.Exception e)
                //    {
                //        //Do nothing for now?
                //    }
                    
                //}
            }
        }

        public void SetHighlightStyleByExt(string fileExt)
        {
            if (fileExt.ToLower() == CSHARPLANGUAGEEXT)
            {
                this.SetHighlighting("C#");
                LanguageExt = CSHARPLANGUAGEEXT;
            }
            else
            {
                this.SetHighlighting("VBNET");
                LanguageExt = VBLANGUAGEEXT;
            }
        }

        public void Cut()
        {
            if (this.ActiveTextAreaControl.SelectionManager.HasSomethingSelected == true)
            {
                if (this.ActiveTextAreaControl.SelectionManager.SelectionIsReadonly == false)
                {
                    this.ActiveTextAreaControl.TextArea.ClipboardHandler.Cut(null, null);
                }
            }
        }

        public void Delete()
        {
            this.ActiveTextAreaControl.TextArea.ClipboardHandler.Delete(null, null);
        }

        public void Paste()
        {
            if (System.Windows.Forms.Clipboard.ContainsText() == true)
            {
                this.ActiveTextAreaControl.TextArea.ClipboardHandler.Paste(null, null);
            }
        }

        public void SelectAll()
        {
            this.ActiveTextAreaControl.TextArea.ClipboardHandler.SelectAll(null, null);
        }

        public void DeleteAndInsertOverTop(string InsertString)
        {
            this.Delete();
            this.ActiveTextAreaControl.TextArea.InsertString(InsertString);
        }

        public void DeleteAndInsertOverTopWithTabing(string InsertString)
        {
            var sb = new System.Text.StringBuilder(InsertString.Length + 30);
            var splitLines = InsertString.Replace("\r","").Split(new string[]{"\n"}, StringSplitOptions.None);
            int tabs = 2;
            int oldTabs = tabs;
            bool useOldTabs = true;
            string tabsToUse;
            long count = 0;
            var keywords = new string[]{"if","end if","class","end class","for ","next",
                "do","while","try","catch","while","end while","sub","end sub",
                "function","end function","foreach","next"};
            foreach (var line in splitLines)
            {
                var tmpLine = line.Trim(new char[] { '\t', ' ' }).ToLowerInvariant();//remove tabs and spaces.
                count++;
                for (int i = 0; i != keywords.Length; i += 2)
                {
                    if (!tmpLine.StartsWith("'"))
                    {
                        if (tmpLine.StartsWith(keywords[i]))//example:if
                        {
                            if (!tmpLine.StartsWith(keywords[i + 1]))//example:end if
                            {
                                useOldTabs = true;
                                tabs++;
                            }
                            else
                            {
                               useOldTabs = false;
                                tabs--;
                            }
                            break;
                        }
                        else
                        {
                            if (tmpLine.StartsWith(keywords[i + 1]))
                            {
                                if (tmpLine.StartsWith("catch"))//special case
                                {
                                    useOldTabs = true;
                                    oldTabs = tabs - 1;
                                }
                                else
                                {
                                    useOldTabs = false;
                                    tabs--;
                                }
                                break;
                            }
                            else
                            {
                                if (tmpLine.StartsWith("end try"))//special case
                                {
                                    useOldTabs = false;
                                    tabs--;
                                    break;
                                }
                            }
                        }
                    }
                }

                if (tabs < 0) tabs = 0;
                if (oldTabs < 0) oldTabs = 0;
                if (useOldTabs)
                {
                    tabsToUse = "".PadLeft(oldTabs, '\t');
                }
                else
                {
                    tabsToUse = "".PadLeft(tabs, '\t');
                }
                if (count == splitLines.Length)
                {
                    sb.Append(tabsToUse + line);
                }
                else
                {
                    sb.AppendLine(tabsToUse + line);
                }
                oldTabs = tabs;
                
            }
            if(this.SelectedText().Length==0)//they maybe inserting in a "unclean" line
                DeleteAndInsertOverTop(System.Environment.NewLine + sb.ToString());
            else
                DeleteAndInsertOverTop(sb.ToString().TrimEnd(new char[] { '\t', ' ' }));
        }

        public string SelectedText()
        {
            if (this.ActiveTextAreaControl.TextArea.SelectionManager.HasSomethingSelected)
                return this.ActiveTextAreaControl.TextArea.SelectionManager.SelectionCollection[0].SelectedText;
            else
                return String.Empty;
        }

        private void undoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Undo();
            this.CurrentlySaved = false;
        }

        private void cutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Cut();
            this.CurrentlySaved = false;
        }

        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Copy();
        }

        private void pasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Paste();
            this.CurrentlySaved = false;
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Delete();
            this.CurrentlySaved = false;
        }

        private void selectAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.SelectAll();
        }

        private void commentSelectedLinesToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (this.ActiveTextAreaControl.TextArea.SelectionManager.HasSomethingSelected == true)
            {
                int i;
                i = this.ActiveTextAreaControl.TextArea.SelectionManager.SelectionCollection[0].StartPosition.Y;
                for (; i != this.ActiveTextAreaControl.TextArea.SelectionManager.SelectionCollection[0].EndPosition.Y+1; i++)
                {
                    this.Document.Insert(this.Document.PositionToOffset(new ICSharpCode.TextEditor.TextLocation(0,i)), this.GetCommentForLanguageExt());
                    this.CurrentlySaved = false;
                }
            }
        }

        private void uncommentSelectedLinesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.ActiveTextAreaControl.TextArea.SelectionManager.HasSomethingSelected == true)
            {
                char commentChar = this.GetCommentForLanguageExt().ToCharArray()[0];
                int i = this.ActiveTextAreaControl.TextArea.SelectionManager.SelectionCollection[0].StartPosition.Y;
                int chLoc = 0;
                char c = 'c';
                for (; i != this.ActiveTextAreaControl.TextArea.SelectionManager.SelectionCollection[0].EndPosition.Y+1; i++)
                {
                    for (chLoc = 0; chLoc != 25; chLoc++)
                    {
                        try
                        {
                            c = this.Document.GetCharAt(this.Document.PositionToOffset(new ICSharpCode.TextEditor.TextLocation(chLoc, i)));
                        }
                        catch (System.Exception)
                        {
                            break;//No character, we're done.
                        }
                        try
                        {
                            if (c == commentChar)
                            {
                                if (LanguageExt == VBLANGUAGEEXT)
                                    this.Document.Remove(this.Document.PositionToOffset(new ICSharpCode.TextEditor.TextLocation(chLoc, i)), 1);
                                else
                                {
                                    try
                                    {
                                        c = this.Document.GetCharAt(this.Document.PositionToOffset(new ICSharpCode.TextEditor.TextLocation(chLoc + 1, i)));
                                    }
                                    catch (System.Exception)
                                    {
                                        break;//No character, we're done.
                                    }

                                    if (c == commentChar)
                                    {
                                        this.Document.Remove(this.Document.PositionToOffset(new ICSharpCode.TextEditor.TextLocation(chLoc, i)), 2);
                                    }
                                }
                                break;
                            }
                            else
                            {
                                if (c != ' ' && c != '\t')
                                {
                                    break;
                                }
                            }
                        }
                        catch (System.Exception)
                        {
                            break;
                        }
                    }
                }
            }
        }

        public TextEditorBox()
        {
            InitializeComponent();
            this.SetHighlighting("VBNET");
            this.Document.TextEditorProperties.MouseWheelScrollDown = true;
            this.ShowEOLMarkers = false;
            this.EnableFolding = true;

            try
            {
                string appPath = Path.GetDirectoryName(Application.ExecutablePath);
                HighlightingManager.Manager.AddSyntaxModeFileProvider(new FileSyntaxModeProvider(appPath));
                this.Document.HighlightingStrategy = HighlightingManager.Manager.FindHighlighter("VBNET");
            }
            catch (System.Exception e)
            {
                MessageBox.Show("Unable to load syntax highlighter file.  Exception: " + e.ToString());
            }
        }

        private void toUpperSelectedTextToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.ActiveTextAreaControl.SelectionManager.HasSomethingSelected == true)
            {
                try
                {
                    string s = this.ActiveTextAreaControl.SelectionManager.SelectedText.ToUpper();
                    int start = this.Document.PositionToOffset(this.ActiveTextAreaControl.TextArea.SelectionManager.SelectionCollection[0].StartPosition);
                    int end = this.Document.PositionToOffset(this.ActiveTextAreaControl.TextArea.SelectionManager.SelectionCollection[0].EndPosition);
                    this.Document.Replace(start, end - start, s);
                }
                catch (System.Exception e1)
                {
                    System.Windows.Forms.MessageBox.Show("Unable to alter text due to the following reason: " + e1.Message);
                }               
            }
        }

        private void toLowerSelectedTextToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.ActiveTextAreaControl.SelectionManager.HasSomethingSelected == true)
            {
                try
                {
                    string s = this.ActiveTextAreaControl.SelectionManager.SelectedText.ToLower();
                    int start = this.Document.PositionToOffset(this.ActiveTextAreaControl.TextArea.SelectionManager.SelectionCollection[0].StartPosition);
                    int end = this.Document.PositionToOffset(this.ActiveTextAreaControl.TextArea.SelectionManager.SelectionCollection[0].EndPosition);
                    this.Document.Replace(start, end - start, s);
                }
                catch (System.Exception e1)
                {
                    System.Windows.Forms.MessageBox.Show("Unable to alter text due to the following reason: " + e1.Message);
                }
            }
        }

        public void AddAction(ICSharpCode.TextEditor.Actions.IEditAction editAction, Keys key)
        {
            editactions[key] = editAction;
        }


        public char GetCharacterBeforeCaret()
        {
            string text = Document.GetText(ActiveTextAreaControl.TextArea.Caret.Offset - 1, 1);
            if (text.Length > 0)
            {
                return text[0];
            }
            return '\0';
        }

        public void RemoveCharacterBeforeCaret()
        {
            Document.Remove(ActiveTextAreaControl.TextArea.Caret.Offset - 1, 1);
        }

        bool IsCaretAtDocumentStart{ get{ return ActiveTextAreaControl.TextArea.Caret.Offset == 0; } }
    }
}

/////////////////////////////////////////////////
//Possible Method: Combined Code Completion Key Handler and TextEditorBox together
//http://www.google.com/codesearch/p?hl=en&sa=N&cd=11&ct=rc#xrUUCxHyrWk/trunk/Source Code/NiftyEditor/XMLEditor/Src/XmlEditorControl.cs&q=: AbstractEditAction lang:c#
/*public class MyCtrlSpaceAction : ICSharpCode.TextEditor.Actions.AbstractEditAction
{
    public override void Execute(TextArea textArea)
    {
        var editor = textArea.MotherTextEditorControl as TextEditor.TextEditorBox;
        if (editor != null)
        {
           
        }
    }

}
*/