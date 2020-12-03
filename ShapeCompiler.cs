using System;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace graphical_programming_language
{
    public class ShapeCompiler : IShapeCompiler
    {
        private readonly ShapeFactory shapeFactory;
        private readonly Panel outputWindow;
        private readonly RichTextBox programLog;
        private Pen pen;
        private Color fillColor;

        private readonly Regex inputSplitter;

        public string Command { get; set; }
        public string[] Arguments { get; set; }
        public StringBuilder Logs { get; set; }

        private int xPos;
        private int yPos;
        private int toXPos;
        private int toYPos;
        private int width;
        private int height;
        private int radius;

        private bool isColorFillOn;

        public ShapeCompiler()
        {
            shapeFactory = new ShapeFactory();
            isColorFillOn = false;
            pen = GetPen(Color.Black, 1);

            inputSplitter = new Regex(@"[\s+,]", RegexOptions.Compiled);
            Logs = new StringBuilder();

            xPos = yPos = toXPos = toYPos = 0;
            width = height = 100;
        }

        public ShapeCompiler(Panel outputWindow, RichTextBox programLog)
        {
            shapeFactory = new ShapeFactory();
            this.outputWindow = outputWindow;
            this.programLog = programLog;
            isColorFillOn = false;
            pen = GetPen(Color.Black, 1);

            inputSplitter = new Regex(@"[\s+,]", RegexOptions.Compiled);
            Logs = new StringBuilder();

            xPos = yPos = toXPos = toYPos = 0;
            width = height = 100;
        }

        public void Compile(string input)
        {
            string[] data = inputSplitter.Split(input).Where(token => token != String.Empty).ToArray<string>();

            Command = data[0];
            Arguments = new string[data.Length - 1];

            if (data.Length > 1)
            {
                Array.Copy(data, 1, Arguments, 0, Arguments.Length);
            }
        }

        public void Run()
        {
            CommandParser(Command, Arguments);
        }

        public Pen GetPen(Color color, int size)
        {
            return new Pen(color, size);
        }

        private void CommandParser(string command, string[] arguments)
        {
            if (command.ToUpper().Equals("RECT") || command.ToUpper().Equals("CIRCLE") || command.ToUpper().Equals("TRIANGLE"))
            {
                try
                {
                    if (command.ToUpper().Equals("CIRCLE"))
                    {
                        if (arguments.Length == 0) { throw new ArgumentException("Circle command need 1 more parameter that represents its radius"); }
                        else
                        {
                            radius = Int32.Parse(arguments[0]);
                            width = radius * 2;
                            height = radius * 2;
                        }
                    }
                    else
                    {
                        width = Int32.Parse(arguments[0]);
                        height = Int32.Parse(arguments[1]);
                    }

                    Shape shape = shapeFactory.GetShape(command, fillColor, isColorFillOn, xPos, yPos, width, height);
                    shape.Draw(outputWindow.CreateGraphics(), pen);

                    Logs.Append($"[*] {shape.GetType().Name} drawn at position x -> {xPos}, y -> {yPos} with width -> {width}, height -> {height}");
                    Logs.Append(Environment.NewLine);
                }
                catch (ArgumentException ex)
                {
                    Logs.Append($"[*] Error: {ex.Message}");
                    Logs.Append(Environment.NewLine);
                }
                catch (FormatException)
                {
                    Logs.Append($"[*] Error: Given argument is not in correct format");
                    Logs.Append(Environment.NewLine);
                }
                catch (IndexOutOfRangeException)
                {
                    string shape = command.ToUpper().Equals("RECT") ? "Rectangle" : command;

                    Logs.Append($"[*] Error: Please provide two parameter for drawing {shape}");
                    Logs.Append(Environment.NewLine);
                }
            }
            else if (command.ToUpper().Equals("DRAWTO"))
            {
                try
                {
                    toXPos = Int32.Parse(arguments[0]);
                    toYPos = Int32.Parse(arguments[1]);

                    Shape shape = shapeFactory.GetShape("line", fillColor, isColorFillOn, xPos, yPos, toXPos, toYPos);
                    shape.Draw(outputWindow.CreateGraphics(), pen);

                    Logs.Append($"[*] Line drawn from position x1 -> {xPos}, y1 -> {yPos} to position x2 -> {toXPos}, y2 -> {toYPos}");
                    Logs.Append(Environment.NewLine);

                    xPos = toXPos;
                    yPos = toYPos;
                }
                catch (IndexOutOfRangeException)
                {
                    Logs.Append($"[*] Error: Please provide two parameter to draw a line");
                    Logs.Append(Environment.NewLine);
                }
            }
            else if (command.ToUpper().Equals("MOVETO"))
            {
                try
                {
                    xPos = Int32.Parse(arguments[0]);
                    yPos = Int32.Parse(arguments[1]);

                    Logs.Append($"[*] Pen position set to {xPos}, {yPos}");
                    Logs.Append(Environment.NewLine);
                }
                catch (IndexOutOfRangeException)
                {
                    Logs.Append($"[*] Error: Please provide two parameter to move pointer");
                    Logs.Append(Environment.NewLine);
                }
            }
            else if (command.ToUpper().Equals("PEN"))
            {
                try
                {
                    Color color = Color.FromName(arguments[0]);
                    int size = (arguments.Length == 2) ? Int32.Parse(arguments[1]) : 1;

                    pen = GetPen(color, size);

                    Logs.Append($"[*] Pen color set to {color.Name} and pen size set to {size}");
                    Logs.Append(Environment.NewLine);
                }
                catch (IndexOutOfRangeException)
                {
                    Logs.Append($"[*] Error: Please provide one parameter for selecting the color");
                    Logs.Append(Environment.NewLine);
                }
            }
            else if (command.ToUpper().Equals("FILL"))
            {
                try
                {
                    if (arguments[0].ToUpper().Equals("ON"))
                    {
                        isColorFillOn = true;
                        fillColor = (arguments.Length == 2) ? GetColor(arguments[1]) : Color.Black;

                        Logs.Append($"[*] Color fill is now {isColorFillOn} and set to {fillColor.Name}");
                        Logs.Append(Environment.NewLine);
                    }
                    else if (arguments[0].ToUpper().Equals("OFF"))
                    {
                        isColorFillOn = false;

                        Logs.Append($"[*] Color fill is now {isColorFillOn}");
                        Logs.Append(Environment.NewLine);
                    }
                }
                catch (IndexOutOfRangeException)
                {
                    Logs.Append($"[*] Error: Please provide one parameter (on/off) to either turn fill on/off");
                    Logs.Append(Environment.NewLine);
                }
            }
            else if (command.ToUpper().Equals("RESET"))
            {
                xPos = yPos = toXPos = toYPos = 0;
                width = height = 100;

                Logs.Append("[*] Reset pen position to 0, 0");
                Logs.Append(Environment.NewLine);
            }
            else if (command.ToUpper().Equals("CLEAR"))
            {
                outputWindow.Refresh();

                Logs.Append("[*] Cleared output panel");
                Logs.Append(Environment.NewLine);
            }
            else if (command.ToUpper().Equals("EXIT"))
            {
                Logs.Append("[*] Exiting application");
                Logs.Append(Environment.NewLine);

                Application.Exit();
            }
            else
            {
                Logs.Append($"[*] Error: Command {command} not found");
                Logs.Append(Environment.NewLine);
            }

            DisplayLogs();
        }

        private void DisplayLogs()
        {
            programLog.Text = Logs.ToString();
        }

        private Color GetColor(string color)
        {
            foreach (KnownColor _color in Enum.GetValues(typeof(KnownColor)))
            {
                if (_color.ToString().ToUpper().Equals(color.ToUpper()))
                {
                    return Color.FromName(color);
                }
            }
            return Color.Black;
        }
    }
}