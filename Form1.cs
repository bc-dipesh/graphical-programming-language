using System.Windows.Forms;

namespace graphical_programming_language
{
    public partial class mainForm : Form
    {
        private readonly ShapeCompiler shapeCompiler;

        public mainForm()
        {
            InitializeComponent();
            shapeCompiler = new ShapeCompiler(outputWindow, programLog);
        }

        private void CommandTxtBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                var command = commandLine.Text;

                shapeCompiler.Compile(command);
                shapeCompiler.Run();
            }
        }
    }
}