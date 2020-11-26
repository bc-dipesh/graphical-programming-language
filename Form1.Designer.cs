
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
            this.codeOutputPanel = new System.Windows.Forms.Panel();
            this.codeEditorTxtBox = new System.Windows.Forms.TextBox();
            this.commandTxtBox = new System.Windows.Forms.TextBox();
            this.codeEditorLabel = new System.Windows.Forms.Label();
            this.codeOutputLabel = new System.Windows.Forms.Label();
            this.commandLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // codeOutputPanel
            // 
            this.codeOutputPanel.BackColor = System.Drawing.SystemColors.Window;
            this.codeOutputPanel.Location = new System.Drawing.Point(441, 52);
            this.codeOutputPanel.Name = "codeOutputPanel";
            this.codeOutputPanel.Size = new System.Drawing.Size(347, 386);
            this.codeOutputPanel.TabIndex = 0;
            // 
            // codeEditorTxtBox
            // 
            this.codeEditorTxtBox.Location = new System.Drawing.Point(13, 52);
            this.codeEditorTxtBox.Multiline = true;
            this.codeEditorTxtBox.Name = "codeEditorTxtBox";
            this.codeEditorTxtBox.Size = new System.Drawing.Size(422, 331);
            this.codeEditorTxtBox.TabIndex = 1;
            // 
            // commandTxtBox
            // 
            this.commandTxtBox.Location = new System.Drawing.Point(13, 418);
            this.commandTxtBox.Name = "commandTxtBox";
            this.commandTxtBox.Size = new System.Drawing.Size(422, 20);
            this.commandTxtBox.TabIndex = 2;
            this.commandTxtBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.CommandTxtBox_KeyDown);
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
            this.commandLabel.Size = new System.Drawing.Size(74, 13);
            this.commandLabel.TabIndex = 5;
            this.commandLabel.Text = "Command box";
            // 
            // mainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.commandLabel);
            this.Controls.Add(this.codeOutputLabel);
            this.Controls.Add(this.codeEditorLabel);
            this.Controls.Add(this.commandTxtBox);
            this.Controls.Add(this.codeEditorTxtBox);
            this.Controls.Add(this.codeOutputPanel);
            this.Name = "mainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Graphical Programming Language";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel codeOutputPanel;
        private System.Windows.Forms.TextBox codeEditorTxtBox;
        private System.Windows.Forms.TextBox commandTxtBox;
        private System.Windows.Forms.Label codeEditorLabel;
        private System.Windows.Forms.Label codeOutputLabel;
        private System.Windows.Forms.Label commandLabel;
    }
}

