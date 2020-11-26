using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace graphical_programming_language
{
    public partial class mainForm : Form
    {
        private ShapeCompiler shapeCompiler;
        private Shape shape;
        public mainForm()
        {
            InitializeComponent();
            shapeCompiler = new ShapeCompiler();
        }

        private void CommandTxtBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                shapeCompiler.Compile(commandTxtBox.Text);
                shape = shapeCompiler.Run();

                shape.Draw(codeOutputPanel.CreateGraphics());
            }
        }
    }
}
