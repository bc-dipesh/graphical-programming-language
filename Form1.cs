using System.Windows.Forms;

namespace graphical_programming_language
{
    public partial class mainForm : Form
    {
        private readonly ShapeCompiler shapeCompiler;

        public mainForm()
        {
            InitializeComponent();
            shapeCompiler = new ShapeCompiler(codeOutputPanel, outputLogTxtBox);
        }

        private void CommandTxtBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                var command = commandTxtBox.Text;

                shapeCompiler.Compile(command);
                shapeCompiler.Run();
            }
        }
    }
}