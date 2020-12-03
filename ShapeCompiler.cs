using System;
using System.Drawing;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace graphical_programming_language
{
    public class ShapeCompiler : IShapeCompiler
    {
        private readonly ShapeFactory shapeFactory;
        private readonly Panel outputWindow;
        private readonly RichTextBox programWindow;
        private readonly TextBox programLog;
        private Pen pen;
        private Color fillColor;

        private readonly Regex inputSplitter;

        public string Command { get; set; }
        public string[] Arguments { get; set; }

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

            xPos = yPos = toXPos = toYPos = 0;
            width = height = 100;
        }

        public ShapeCompiler(Panel outputWindow, RichTextBox programWindow, TextBox programLog)
        {
            shapeFactory = new ShapeFactory();
            this.outputWindow = outputWindow;
            this.programWindow = programWindow;
            this.programLog = programLog;
            isColorFillOn = false;
            pen = GetPen(Color.Black, 1);

            inputSplitter = new Regex(@"[\s+,]", RegexOptions.Compiled);

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

                    programLog.Text = $"[*] {shape.GetType().Name} drawn at position x -> {xPos}, y -> {yPos} with width -> {width}, height -> {height}";
                }
                catch (ArgumentException ex)
                {
                    programLog.Text = $"[*] Error: {ex.Message}";
                }
                catch (FormatException)
                {
                    programLog.Text = $"[*] Error: Given argument is not in correct format";
                }
                catch (IndexOutOfRangeException)
                {
                    string shape = command.ToUpper().Equals("RECT") ? "Rectangle" : command;
                    programLog.Text = $"[*] Error: Please provide two parameter for drawing {shape}";
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

                    programLog.Text = $"[*] Line drawn from position x1 -> {xPos}, y1 -> {yPos} to position x2 -> {toXPos}, y2 -> {toYPos}";

                    xPos = toXPos;
                    yPos = toYPos;
                }
                catch (IndexOutOfRangeException)
                {
                    programLog.Text = $"[*] Error: Please provide two parameter to draw a line";
                }
            }
            else if (command.ToUpper().Equals("MOVETO"))
            {
                try
                {
                    xPos = Int32.Parse(arguments[0]);
                    yPos = Int32.Parse(arguments[1]);

                    programLog.Text = $"[*] Pen position set to {xPos}, {yPos}";
                } catch (IndexOutOfRangeException)
                {
                    programLog.Text = $"[*] Error: Please provide two parameter to move pointer";
                }
            }
            else if (command.ToUpper().Equals("PEN"))
            {
                try
                {
                    Color color = Color.FromName(arguments[0]);
                    int size = (arguments.Length == 2) ? Int32.Parse(arguments[1]) : 1;

                    pen = GetPen(color, size);

                    programLog.Text = $"[*] Pen color set to {color.Name} and pen size set to {size}";
                } catch (IndexOutOfRangeException)
                {
                    programLog.Text = $"[*] Error: Please provide one parameter for selecting the color";
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

                        programLog.Text = $"[*] Color fill is now {isColorFillOn} and set to {fillColor.Name}";
                    }
                    else if (arguments[0].ToUpper().Equals("OFF"))
                    {
                        isColorFillOn = false;

                        programLog.Text = $"[*] Color fill is now {isColorFillOn}";
                    }
                } catch (IndexOutOfRangeException)
                {
                    programLog.Text = $"[*] Error: Please provide one parameter (on/off) to either turn fill on/off";
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
            else
            {
                programLog.Text = $"[*] Error: Command {command} not found";
            }
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