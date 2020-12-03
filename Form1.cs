using System;
using System.Windows.Forms;

namespace graphical_programming_language
{
    public partial class mainForm : Form
    {
        private readonly ShapeCompiler shapeCompiler;

        public mainForm()
        {
            InitializeComponent();
            shapeCompiler = new ShapeCompiler(outputWindow, programWindow, programLog);
        }

        private void CommandTxtBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                var program = programWindow.Text.Split(new string[] { Environment.NewLine, "\n", "\r" }, StringSplitOptions.RemoveEmptyEntries);
                var input = commandLine.Text;

                if (input.ToUpper().Equals("RUN"))
                {
                    foreach (var line in program)
                    {
                        shapeCompiler.Compile(line);
                        shapeCompiler.Run();
                    }
                }
                else
                {
                    shapeCompiler.Compile(input);
                    shapeCompiler.Run();
                }
            }
        }

        private void OpenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Title = "Load a program file",
                Filter = "Text Files (*.txt) | *.txt"
            };

            DialogResult dialogResult = openFileDialog.ShowDialog();

            if (dialogResult == DialogResult.OK)
            {
                programWindow.LoadFile(openFileDialog.FileName, RichTextBoxStreamType.PlainText);
            }
        }

        private void SaveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog
            {
                Title = "Save current program to file",
                Filter = "Text files (*.txt) | *.txt"
            };

            DialogResult dialogResult = saveFileDialog.ShowDialog();

            if (dialogResult == DialogResult.OK)
            {
                programWindow.SaveFile(saveFileDialog.FileName, RichTextBoxStreamType.PlainText);
            }
        }

        private void ExitToolStripMenuItem_Click(object sender, EventArgs e)
        {
           Application.Exit();
        }
    }
}