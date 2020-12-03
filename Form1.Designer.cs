
namespace graphical_programming_language
{
    partial class mainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.outputWindow = new System.Windows.Forms.Panel();
            this.programLog = new System.Windows.Forms.TextBox();
            this.commandLine = new System.Windows.Forms.TextBox();
            this.codeEditorLabel = new System.Windows.Forms.Label();
            this.codeOutputLabel = new System.Windows.Forms.Label();
            this.commandLabel = new System.Windows.Forms.Label();
            this.programWindow = new System.Windows.Forms.RichTextBox();
            this.outputWindow.SuspendLayout();
            this.SuspendLayout();
            // 
            // outputWindow
            // 
            this.outputWindow.BackColor = System.Drawing.SystemColors.Window;
            this.outputWindow.Controls.Add(this.programLog);
            this.outputWindow.Location = new System.Drawing.Point(441, 52);
            this.outputWindow.Name = "outputWindow";
            this.outputWindow.Size = new System.Drawing.Size(347, 386);
            this.outputWindow.TabIndex = 3;
            // 
            // programLog
            // 
            this.programLog.Location = new System.Drawing.Point(4, 327);
            this.programLog.Multiline = true;
            this.programLog.Name = "programLog";
            this.programLog.ReadOnly = true;
            this.programLog.Size = new System.Drawing.Size(340, 56);
            this.programLog.TabIndex = 4;
            // 
            // commandLine
            // 
            this.commandLine.Location = new System.Drawing.Point(13, 418);
            this.commandLine.Name = "commandLine";
            this.commandLine.Size = new System.Drawing.Size(422, 20);
            this.commandLine.TabIndex = 2;
            this.commandLine.KeyDown += new System.Windows.Forms.KeyEventHandler(this.CommandTxtBox_KeyDown);
            // 
            // codeEditorLabel
            // 
            this.codeEditorLabel.AutoSize = true;
            this.codeEditorLabel.Location = new System.Drawing.Point(13, 22);
            this.codeEditorLabel.Name = "codeEditorLabel";
            this.codeEditorLabel.Size = new System.Drawing.Size(62, 13);
            this.codeEditorLabel.TabIndex = 3;
            this.codeEditorLabel.Text = "Code Editor";
            // 
            // codeOutputLabel
            // 
            this.codeOutputLabel.AutoSize = true;
            this.codeOutputLabel.Location = new System.Drawing.Point(441, 21);
            this.codeOutputLabel.Name = "codeOutputLabel";
            this.codeOutputLabel.Size = new System.Drawing.Size(39, 13);
            this.codeOutputLabel.TabIndex = 4;
            this.codeOutputLabel.Text = "Output";
            // 
            // commandLabel
            // 
            this.commandLabel.AutoSize = true;
            this.commandLabel.Location = new System.Drawing.Point(13, 399);
            this.commandLabel.Name = "commandLabel";
            this.commandLabel.Size = new System.Drawing.Size(77, 13);
            this.commandLabel.TabIndex = 5;
            this.commandLabel.Text = "Command Line";
            // 
            // programWindow
            // 
            this.programWindow.Location = new System.Drawing.Point(16, 52);
            this.programWindow.Name = "programWindow";
            this.programWindow.Size = new System.Drawing.Size(419, 330);
            this.programWindow.TabIndex = 6;
            this.programWindow.Text = "";
            // 
            // mainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(801, 450);
            this.Controls.Add(this.programWindow);
            this.Controls.Add(this.commandLabel);
            this.Controls.Add(this.codeOutputLabel);
            this.Controls.Add(this.codeEditorLabel);
            this.Controls.Add(this.commandLine);
            this.Controls.Add(this.outputWindow);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "mainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Graphical Programming Language";
            this.outputWindow.ResumeLayout(false);
            this.outputWindow.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel outputWindow;
        private System.Windows.Forms.TextBox commandLine;
        private System.Windows.Forms.Label codeEditorLabel;
        private System.Windows.Forms.Label codeOutputLabel;
        private System.Windows.Forms.Label commandLabel;
        private System.Windows.Forms.TextBox programLog;
        private System.Windows.Forms.RichTextBox programWindow;
    }
}

