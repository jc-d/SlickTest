using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ICSharpCode.TextEditor;
using ICSharpCode.TextEditor.Document;
using System.IO;
using NRefactory = ICSharpCode.NRefactory;
using Dom = ICSharpCode.SharpDevelop.Dom;

namespace VBEditor
{
    public class CtrlSpaceAction : ICSharpCode.TextEditor.Actions.AbstractEditAction
    {
        public override void Execute(TextArea textArea)
        {
            var editor = textArea.MotherTextEditorControl as TextEditor.TextEditorBox;
            if (editor != null)
            {
                var parent = editor.ParentForm as MainForm;
                if (parent != null)
                {
                    char c = editor.GetCharacterBeforeCaret();
                    if(c!='\0')//null means we are at the first character.
                        parent.CodeCompletionKeyHandler.TextAreaKeyEventHandler(c, true);
                }
            }
        }

    }
}
