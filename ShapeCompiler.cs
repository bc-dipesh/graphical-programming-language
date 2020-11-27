using System;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace graphical_programming_language
{
    internal class ShapeCompiler : IShapeCompiler
    {
        private readonly ShapeFactory shapeFactory;
        private readonly Panel drawingPanel;
        private readonly TextBox outputLogTxtBox;
        private string command;
        private string[] arguments;
        private readonly Regex splitOnSpaces;
        private string userColor;
        private Color color;
        private int? x;
        private int? y;
        private int? width;
        private int? height;

        public ShapeCompiler()
        {
            shapeFactory = new ShapeFactory();
            splitOnSpaces = new Regex(@"\s+", RegexOptions.Compiled);
        }

        public ShapeCompiler(Panel panel, TextBox outputLogTxtBox)
        {
            shapeFactory = new ShapeFactory();
            splitOnSpaces = new Regex(@"\s+", RegexOptions.Compiled);
            drawingPanel = panel;
            this.outputLogTxtBox = outputLogTxtBox;
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
                if (arguments.Length == 3)
                {
                    x = Int32.Parse(arguments[1]);
                    y = Int32.Parse(arguments[2]);
                }
                else if (arguments.Length == 5)
                {
                    x = Int32.Parse(arguments[1]);
                    y = Int32.Parse(arguments[2]);
                    width = Int32.Parse(arguments[3]);
                    height = Int32.Parse(arguments[4]);
                }

                SetPenColor(string.IsNullOrEmpty(userColor) ? "black" : userColor);
                Shape shape = shapeFactory.GetShape(arguments[0], color, x ?? 100, y ?? 100, width ?? 100, height ?? 100);
                shape.Draw(drawingPanel.CreateGraphics());

                outputLogTxtBox.Text = $"[*] Drawing {arguments[0]} at position x -> {x??100}, y -> {y??100} having width -> {width??100}, height -> {height??100}";
            }
            else if (command.ToUpper().Equals("PEN"))
            {
                SetPenColor(arguments[0]);
            }
            else if (command.ToUpper().Equals("CLEAR"))
            {
                drawingPanel.Refresh();
                outputLogTxtBox.Text = "[*] Cleared output panel";
            }
            else if (command.ToUpper().Equals("EXIT"))
            {
                outputLogTxtBox.Text = "[*] Exiting application";
                Application.Exit();
            }
        }

        private void SetPenColor(string penColor)
        {
            userColor = penColor;
            switch (userColor.ToUpper())
            {
                case "BLACK":
                    color = Color.Black;
                    break;
                case "RED":
                    color = Color.Red;
                    break;
                case "BLUE":
                    color = Color.Blue;
                    break;
            }
            outputLogTxtBox.Text = $"[*] Pen color set to {penColor}";
        }
    }
}