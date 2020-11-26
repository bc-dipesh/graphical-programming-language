using System;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace graphical_programming_language
{
    internal class ShapeCompiler : IShapeCompiler
    {
        private ShapeFactory shapeFactory;
        private Panel drawingPanel;
        private string command;
        private string[] arguments;
        private Regex splitOnSpaces;

        public ShapeCompiler()
        {
            shapeFactory = new ShapeFactory();
            splitOnSpaces = new Regex(@"\s+", RegexOptions.Compiled);
        }

        public ShapeCompiler(Panel panel)
        {
            shapeFactory = new ShapeFactory();
            splitOnSpaces = new Regex(@"\s+", RegexOptions.Compiled);
            drawingPanel = panel;
        }

        public void Compile(string code)
        {
            string[] data = splitOnSpaces.Split(code);

            command = data[0];
            arguments = new string[data.Length - 1];

            if (data.Length > 1)
            {
                Array.Copy(data, 1, arguments, 0, arguments.Length);
            }
        }

        public void Run()
        {
            if (command.ToUpper().Equals("DRAW"))
            {
                if (arguments.Length == 1)
                {
                    Shape shape = shapeFactory.getShape(arguments[0]);
                    shape.Draw(drawingPanel.CreateGraphics());
                }
            }
            else if (command.ToUpper().Equals("CLEAR"))
            {
                drawingPanel.Refresh();
            }
            else if (command.ToUpper().Equals("EXIT"))
            {
                Application.Exit();
            }
        }
    }
}