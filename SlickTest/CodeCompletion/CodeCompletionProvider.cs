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

using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using ICSharpCode.TextEditor;
using ICSharpCode.TextEditor.Gui.CompletionWindow;
using ICSharpCode.SharpDevelop.DefaultEditor.Gui.Editor;

using Dom = ICSharpCode.SharpDevelop.Dom;
using NRefactoryResolver = ICSharpCode.SharpDevelop.Dom.NRefactoryResolver.NRefactoryResolver;

namespace VBEditor
{
    class CodeCompletionProvider : ICompletionDataProvider
    {
        MainForm mainForm;
        private int internalOffset;

        public CodeCompletionProvider(MainForm mainForm)
        {
            this.mainForm = mainForm;
            internalOffset = 0;
        }

        public CodeCompletionProvider(MainForm mainForm, int offset)
        {
            this.mainForm = mainForm;
            internalOffset = offset;
        }

        public ImageList ImageList
        {
            get
            {
                return mainForm.imageList1;
            }
        }
        private string internalPreSelection = null;
        public string PreSelection
        {
            get
            {
                return internalPreSelection;
            }
        }

        public int DefaultIndex
        {
            get
            {
                return -1;
            }
        }

        public CompletionDataProviderKeyResult ProcessKey(char key)
        {
            if (char.IsLetterOrDigit(key) || key == '_')
            {
                return CompletionDataProviderKeyResult.NormalKey;
            }
            else
            {
                // key triggers insertion of selected items
                return CompletionDataProviderKeyResult.InsertionKey;
            }
        }

        /// <summary>
        /// Called when entry should be inserted. Forward to the insertion action of the completion data.
        /// </summary>
        public bool InsertAction(ICompletionData data, TextArea textArea, int insertionOffset, char key)
        {
            textArea.Caret.Position = textArea.Document.OffsetToPosition(insertionOffset);
            return data.InsertAction(textArea, key);
        }

        public ICompletionData[] GenerateCompletionData(string fileName, TextArea textArea, char charTyped)
        {
            // We can return code-completion items like this:

            //return new ICompletionData[] {
            //	new DefaultCompletionData("Text", "Description", 1)
            //};

            NRefactoryResolver resolver = new NRefactoryResolver(mainForm.myProjectContent.Language);
            Dom.ResolveResult rr = resolver.Resolve(FindExpression(textArea),
                                                    mainForm.parseInformation,
                                                    textArea.MotherTextEditorControl.Text);
            List<ICompletionData> resultList = new List<ICompletionData>();
            if (rr != null)
            {
                ArrayList completionData = rr.GetCompletionData(mainForm.myProjectContent);
                if (completionData != null)
                {
                    AddCompletionData(resultList, completionData);
                }
            }
            return resultList.ToArray();
        }

        /// <summary>
        /// Find the expression the cursor is at.
        /// Also determines the context (using statement, "new"-expression etc.) the
        /// cursor is at.
        /// </summary>
        Dom.ExpressionResult FindExpression(TextArea textArea)
        {
            Dom.IExpressionFinder finder;
            if (MainForm.IsVisualBasic)
            {
                finder = new Dom.VBNet.VBExpressionFinder();
            }
            else
            {
                finder = new Dom.CSharp.CSharpExpressionFinder(mainForm.parseInformation);
            }
            internalPreSelection = null;
            string text;
            char trimCharacter = '.';

            if (internalOffset == 0)//default, reaction, don't preselect and use full text. Typically user just pressed "."
            {
                text = textArea.Document.TextContent;
            }
            else
            {
                //CUSTOM CODE
                text = textArea.Document.GetText(0, textArea.Caret.Offset);//Get code from start to caret.
                
                //1. Get the line that is the caret's line
                //2. Check if it contains a "."  if it does then the user isn't (likely) to be randomly pressing ctrl+space.
                if (textArea.Document.GetText(textArea.Document.GetLineSegment(textArea.Caret.Line)).Contains("."))
                {
                    //3. Get the last "." in the text.  Verify index isn't -1.
                    var index = text.LastIndexOf('.');
                    if (index != -1)
                    {
                        //Possible problem: text.CompareTo(te<Ctrl+Space>
                        if (text.Substring(index).Contains("("))
                        {
                            index = text.LastIndexOf("(");
                            trimCharacter = '(';
                        }
                        //4. Get the text AFTER the last "." and set that as the pre-selection
                        //5. Set internal offset to the difference between the current caret and the last "." or "("
                        internalPreSelection = text.Substring(index, text.Length - (index)).Trim(new char[] { trimCharacter });
                        internalOffset = textArea.Caret.Offset - index;
                        if (internalOffset < 0)
                        {//6. If Internal offset is negative then ctrl+space was made before the . and thus is invalid???
                            internalOffset = 0;
                            internalPreSelection = "";
                        }
                        else
                        {
                            
                            if (trimCharacter == '(')
                            {//6. Set the text to everything INCLUDING the last "(".
                                internalOffset -= 1;
                                text = text.Substring(0, index+1);
                            }
                            else
                            {//6. Set the text to everything BEFORE the last ".".
                                text = text.Substring(0, index);
                            }
                            
                        }

                    }
                    else
                    {
                        internalOffset = 0;
                    }
                }
            }
            Dom.ExpressionResult expression = finder.FindExpression(text, textArea.Caret.Offset - internalOffset);
            if (String.IsNullOrEmpty(expression.Expression))
            {
                
                if(trimCharacter=='.')
                    if(MainForm.IsVisualBasic)
                        expression.Expression = "Me";
                    else
                        expression.Expression = "this";
            }
            if (expression.Region.IsEmpty)
            {
                expression.Region = new Dom.DomRegion(textArea.Caret.Line + 1, textArea.Caret.Column + 1 - internalOffset);
            }
            return expression;
        }

        void AddCompletionData(List<ICompletionData> resultList, ArrayList completionData)
        {
            // used to store the method names for grouping overloads
            Dictionary<string, CodeCompletionData> nameDictionary = new Dictionary<string, CodeCompletionData>();

            // Add the completion data as returned by SharpDevelop.Dom to the
            // list for the text editor
            foreach (object obj in completionData)
            {
                if (obj is string)
                {
                    // namespace names are returned as string
                    resultList.Add(new DefaultCompletionData((string)obj, "namespace " + obj, 5));
                }
                else if (obj is Dom.IClass)
                {
                    Dom.IClass c = (Dom.IClass)obj;
                    resultList.Add(new CodeCompletionData(c));
                }
                else if (obj is Dom.IMember)
                {
                    Dom.IMember m = (Dom.IMember)obj;
                    if (m is Dom.IMethod && ((m as Dom.IMethod).IsConstructor))
                    {
                        // Skip constructors
                        continue;
                    }
                    // Group results by name and add "(x Overloads)" to the
                    // description if there are multiple results with the same name.

                    CodeCompletionData data;
                    if (nameDictionary.TryGetValue(m.Name, out data))
                    {
                        data.AddOverload();
                    }
                    else
                    {
                        nameDictionary[m.Name] = data = new CodeCompletionData(m);
                        resultList.Add(data);
                    }
                }
                else
                {
                    // Current ICSharpCode.SharpDevelop.Dom should never return anything else
                    throw new NotSupportedException();
                }
            }
        }
    }

}

