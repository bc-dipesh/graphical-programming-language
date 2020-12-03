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
                var program = programWindow.Text.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
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

        }

        private void SaveToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
    }
}