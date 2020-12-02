using System;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace graphical_programming_language
{
    public class ShapeCompiler : IShapeCompiler
    {
        private readonly ShapeFactory shapeFactory;
        private readonly Panel outputWindow;
        private readonly TextBox programWindow;
        private readonly TextBox programLog;
        private Pen pen;
        private Color fillColor;

        private readonly Regex splitOnSpaces;

        private string command;
        private string[] arguments;

        private int xPos;
        private int yPos;
        private int toXPos;
        private int toYPos;
        private int width;
        private int height;

        private bool isColorFillOn;

        public ShapeCompiler()
        {
            shapeFactory = new ShapeFactory();
            isColorFillOn = false;

            splitOnSpaces = new Regex(@"\s+", RegexOptions.Compiled);

            xPos = yPos = toXPos = toYPos = 0;
            width = height = 100;
        }

        public ShapeCompiler(Panel outputWindow, TextBox programWindow, TextBox programLog)
        {
            shapeFactory = new ShapeFactory();
            this.outputWindow = outputWindow;
            this.programWindow = programWindow;
            this.programLog = programLog;
            isColorFillOn = false;

            splitOnSpaces = new Regex(@"\s+", RegexOptions.Compiled);

            xPos = yPos = toXPos = toYPos = 0;
            width = height = 100;
        }

        public void Compile(string command)
        {
            string[] commands = splitOnSpaces.Split(command);

            this.command = commands[0];
            arguments = new string[commands.Length - 1];

            if (commands.Length > 1)
            {
                Array.Copy(commands, 1, arguments, 0, arguments.Length);
            }
        }

        public void Run()
        {
            CommandParser(command, arguments);
        }

        public Pen GetPen(Color color, int size)
        {
            return new Pen(color, size);
        }

        private void CommandParser(string command, string[] arguments)
        {
            if (command.ToUpper().Equals("DRAW"))
            {
                try
                {
                    if (arguments.Length == 3)
                    {
                        if (arguments[0].ToUpper().Equals("LINE"))
                        {
                            toXPos = Int32.Parse(arguments[1]);
                            toYPos = Int32.Parse(arguments[2]);
                        }
                        else
                        {
                            xPos = Int32.Parse(arguments[1]);
                            yPos = Int32.Parse(arguments[2]);
                        }
                    }
                    else if (arguments.Length == 5)
                    {
                        if (arguments[0].ToUpper().Equals("LINE"))
                        {
                            xPos = Int32.Parse(arguments[1]);
                            yPos = Int32.Parse(arguments[2]);
                            toXPos = Int32.Parse(arguments[3]);
                            toYPos = Int32.Parse(arguments[4]);
                        }
                        else
                        {
                            xPos = Int32.Parse(arguments[1]);
                            yPos = Int32.Parse(arguments[2]);
                            width = Int32.Parse(arguments[3]);
                            height = Int32.Parse(arguments[4]);
                        }
                    }

                    if (pen is null)
                    {
                        pen = GetPen(Color.Black, 1);
                    }

                    if (arguments[0].ToUpper().Equals("LINE"))
                    {
                        Shape shape = shapeFactory.GetShape(arguments[0], fillColor, isColorFillOn, xPos, yPos, toXPos, toYPos);
                        shape.Draw(outputWindow.CreateGraphics(), pen);

                        programLog.Text = $"[*] {arguments[0]} drawn from position x1 -> {xPos}, y1 -> {yPos} to x2 -> {toXPos}, y2 -> {toYPos}";

                        xPos = toXPos;
                        yPos = toYPos;
                    }
                    else
                    {
                        Shape shape = shapeFactory.GetShape(arguments[0], fillColor, isColorFillOn, xPos, yPos, width, height);
                        shape.Draw(outputWindow.CreateGraphics(), pen);

                        programLog.Text = $"[*] {arguments[0]} drawn at position x -> {xPos}, y -> {yPos} with width -> {width}, height -> {height}";
                    }
                }
                catch (ArgumentException argEx)
                {
                    programLog.Text = $"[*] {argEx.Message}";
                }
            }
            else if (command.ToUpper().Equals("PEN"))
            {
                if (arguments[0].ToUpper().Equals("POSITION"))
                {
                    xPos = Int32.Parse(arguments[1]);
                    yPos = Int32.Parse(arguments[2]);

                    programLog.Text = $"[*] Pen position set to {xPos}, {yPos}";
                }
                else
                {
                    Color color = Color.FromName(arguments[0]);
                    int size = (arguments.Length == 2) ? Int32.Parse(arguments[1]) : 1;

                    pen = GetPen(color, size);

                    programLog.Text = $"[*] Pen color set to {color.Name} and pen size set to {size}";
                }
            }
            else if (command.ToUpper().Equals("FILL"))
            {
                if (arguments[0].ToUpper().Equals("ON"))
                {
                    isColorFillOn = true;
                    fillColor = (arguments.Length == 2) ? Color.FromName(arguments[1]) : Color.Black;

                    programLog.Text = $"[*] Color fill is now {isColorFillOn} and set to {fillColor.Name}";
                }
                else if (arguments[0].ToUpper().Equals("OFF"))
                {
                    isColorFillOn = false;

                    programLog.Text = $"[*] Color fill is now {isColorFillOn}";
                }
            }
            else if (command.ToUpper().Equals("RESET"))
            {
                xPos = yPos = toXPos = toYPos = 0;
                width = height = 100;

                programLog.Text = "[*] Reset pen position to 0, 0";
            }
            else if (command.ToUpper().Equals("CLEAR"))
            {
                outputWindow.Refresh();
                programLog.Text = "[*] Cleared output panel";
            }
            else if (command.ToUpper().Equals("EXIT"))
            {
                programLog.Text = "[*] Exiting application";
                Application.Exit();
            }
        }
    }
}