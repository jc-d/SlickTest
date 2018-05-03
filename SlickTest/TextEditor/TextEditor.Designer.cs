namespace TextEditor
{
    partial class TextEditorBox
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem undoToolStripMenuItem;
        //private ICSharpCode.Core.MenuSeparator menuSeparator1;
        private System.Windows.Forms.ToolStripMenuItem cutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem copyToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem pasteToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deleteToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem selectAllToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator commentSelectedLinesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem commentSelectedLinesToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem uncommentSelectedLinesToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem toUpperSelectedTextToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toLowerSelectedTextToolStripMenuItem;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.undoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            //this.menuSeparator1 = new ICSharpCode.Core.MenuSeparator();
            this.cutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.copyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pasteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.selectAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.commentSelectedLinesToolStripMenuItem = new System.Windows.Forms.ToolStripSeparator();
            this.uncommentSelectedLinesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toUpperSelectedTextToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toLowerSelectedTextToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.commentSelectedLinesToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // textAreaPanel
            // 
            this.textAreaPanel.Size = new System.Drawing.Size(500, 300);
            // 
            // undoToolStripMenuItem
            // 
            this.undoToolStripMenuItem.Name = "undoToolStripMenuItem";
            this.undoToolStripMenuItem.Size = new System.Drawing.Size(267, 22);
            this.undoToolStripMenuItem.Text = "Undo";
            this.undoToolStripMenuItem.Click += new System.EventHandler(this.undoToolStripMenuItem_Click);
            // 
            // menuSeparator1
            // 
            //this.menuSeparator1.Name = "menuSeparator1";
            //this.menuSeparator1.Size = new System.Drawing.Size(264, 6);
            // 
            // cutToolStripMenuItem
            // 
            this.cutToolStripMenuItem.Name = "cutToolStripMenuItem";
            this.cutToolStripMenuItem.Size = new System.Drawing.Size(267, 22);
            this.cutToolStripMenuItem.Text = "Cut";
            this.cutToolStripMenuItem.Click += new System.EventHandler(this.cutToolStripMenuItem_Click);
            // 
            // copyToolStripMenuItem
            // 
            this.copyToolStripMenuItem.Name = "copyToolStripMenuItem";
            this.copyToolStripMenuItem.Size = new System.Drawing.Size(267, 22);
            this.copyToolStripMenuItem.Text = "Copy";
            this.copyToolStripMenuItem.Click += new System.EventHandler(this.copyToolStripMenuItem_Click);
            // 
            // pasteToolStripMenuItem
            // 
            this.pasteToolStripMenuItem.Name = "pasteToolStripMenuItem";
            this.pasteToolStripMenuItem.Size = new System.Drawing.Size(267, 22);
            this.pasteToolStripMenuItem.Text = "Paste";
            this.pasteToolStripMenuItem.Click += new System.EventHandler(this.pasteToolStripMenuItem_Click);
            // 
            // deleteToolStripMenuItem
            // 
            this.deleteToolStripMenuItem.Name = "deleteToolStripMenuItem";
            this.deleteToolStripMenuItem.Size = new System.Drawing.Size(267, 22);
            this.deleteToolStripMenuItem.Text = "Delete";
            this.deleteToolStripMenuItem.Click += new System.EventHandler(this.deleteToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(264, 6);
            // 
            // selectAllToolStripMenuItem
            // 
            this.selectAllToolStripMenuItem.Name = "selectAllToolStripMenuItem";
            this.selectAllToolStripMenuItem.Size = new System.Drawing.Size(267, 22);
            this.selectAllToolStripMenuItem.Text = "Select All";
            this.selectAllToolStripMenuItem.Click += new System.EventHandler(this.selectAllToolStripMenuItem_Click);
            // 
            // commentSelectedLinesToolStripMenuItem
            // 
            this.commentSelectedLinesToolStripMenuItem.Name = "commentSelectedLinesToolStripMenuItem";
            this.commentSelectedLinesToolStripMenuItem.Size = new System.Drawing.Size(264, 6);
            // 
            // uncommentSelectedLinesToolStripMenuItem
            // 
            this.uncommentSelectedLinesToolStripMenuItem.Name = "uncommentSelectedLinesToolStripMenuItem";
            this.uncommentSelectedLinesToolStripMenuItem.Size = new System.Drawing.Size(267, 22);
            this.uncommentSelectedLinesToolStripMenuItem.Text = "Uncomment Selected Lines";
            this.uncommentSelectedLinesToolStripMenuItem.Click += new System.EventHandler(this.uncommentSelectedLinesToolStripMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(264, 6);
            // 
            // toUpperSelectedTextToolStripMenuItem
            // 
            this.toUpperSelectedTextToolStripMenuItem.Name = "toUpperSelectedTextToolStripMenuItem";
            this.toUpperSelectedTextToolStripMenuItem.Size = new System.Drawing.Size(267, 22);
            this.toUpperSelectedTextToolStripMenuItem.Text = "To Upper Selected Text";
            this.toUpperSelectedTextToolStripMenuItem.Click += new System.EventHandler(this.toUpperSelectedTextToolStripMenuItem_Click);
            // 
            // toLowerSelectedTextToolStripMenuItem
            // 
            this.toLowerSelectedTextToolStripMenuItem.Name = "toLowerSelectedTextToolStripMenuItem";
            this.toLowerSelectedTextToolStripMenuItem.Size = new System.Drawing.Size(267, 22);
            this.toLowerSelectedTextToolStripMenuItem.Text = "To Lower Selected Text";
            this.toLowerSelectedTextToolStripMenuItem.Click += new System.EventHandler(this.toLowerSelectedTextToolStripMenuItem_Click);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.undoToolStripMenuItem,
            //this.menuSeparator1,
            this.cutToolStripMenuItem,
            this.copyToolStripMenuItem,
            this.pasteToolStripMenuItem,
            this.deleteToolStripMenuItem,
            this.toolStripSeparator1,
            this.selectAllToolStripMenuItem,
            this.commentSelectedLinesToolStripMenuItem,
            this.commentSelectedLinesToolStripMenuItem1,
            this.uncommentSelectedLinesToolStripMenuItem,
            this.toolStripSeparator2,
            this.toUpperSelectedTextToolStripMenuItem,
            this.toLowerSelectedTextToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(268, 270);
            // 
            // commentSelectedLinesToolStripMenuItem1
            // 
            this.commentSelectedLinesToolStripMenuItem1.Name = "commentSelectedLinesToolStripMenuItem1";
            this.commentSelectedLinesToolStripMenuItem1.Size = new System.Drawing.Size(267, 22);
            this.commentSelectedLinesToolStripMenuItem1.Text = "Comment Selected Lines";
            this.commentSelectedLinesToolStripMenuItem1.Click += new System.EventHandler(this.commentSelectedLinesToolStripMenuItem1_Click);
            // 
            // TextEditorBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ContextMenuStrip = this.contextMenuStrip1;
            this.Name = "TextEditorBox";
            this.Size = new System.Drawing.Size(500, 300);
            this.Controls.SetChildIndex(this.textAreaPanel, 0);
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
    }
}
