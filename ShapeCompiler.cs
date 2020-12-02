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
        private Pen pen;

        private string command;
        private string[] arguments;
        private readonly Regex splitOnSpaces;
        private int? x;
        private int? y;
        private int? width;
        private int? height;
        private bool isColorFillOn;
        private Color fillColor;

        public ShapeCompiler()
        {
            shapeFactory = new ShapeFactory();
            splitOnSpaces = new Regex(@"\s+", RegexOptions.Compiled);
            isColorFillOn = false;
        }

        public ShapeCompiler(Panel panel, TextBox outputLogTxtBox)
        {
            shapeFactory = new ShapeFactory();
            splitOnSpaces = new Regex(@"\s+", RegexOptions.Compiled);
            drawingPanel = panel;
            this.outputLogTxtBox = outputLogTxtBox;
            isColorFillOn = false;
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
                try
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

                    if (pen is null)
                    {
                        pen = GetPen(Color.Black, 1);
                    }

                    Shape shape = shapeFactory.GetShape(arguments[0], fillColor, isColorFillOn, x ?? 100, y ?? 100, width ?? 100, height ?? 100);
                    shape.Draw(drawingPanel.CreateGraphics(), pen);



                    outputLogTxtBox.Text = $"[*] {arguments[0]} drawn at position x -> {x ?? 100}, y -> {y ?? 100} with width -> {width ?? 100}, height -> {height ?? 100}";
                }
                catch (ArgumentException argEx)
                {
                    outputLogTxtBox.Text = $"[*] {argEx.Message}";
                }
            }
            else if (command.ToUpper().Equals("PEN"))
            {
                Color color = Color.FromName(arguments[0]);
                int size = (arguments.Length == 2) ? Int32.Parse(arguments[1]) : 1;

                pen = GetPen(color, size);

                outputLogTxtBox.Text = $"[*] Pen color set to {color.Name} and pen size set to {size}";
            }
            else if (command.ToUpper().Equals("FILL"))
            {
                if (arguments[0].ToUpper().Equals("ON"))
                {
                    isColorFillOn = true;
                    fillColor = (arguments.Length == 2) ? Color.FromName(arguments[1]) : Color.Black;

                    outputLogTxtBox.Text = $"[*] Color fill is now {isColorFillOn} and set to {fillColor.Name}";
                }
                else if (arguments[0].ToUpper().Equals("OFF"))
                {
                    isColorFillOn = false;

                    outputLogTxtBox.Text = $"[*] Color fill is now {isColorFillOn}";
                }
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

        private Pen GetPen(Color color, int size)
        {
            return new Pen(color, size);
        }
    }
}