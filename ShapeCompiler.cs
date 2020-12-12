using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace graphical_programming_language
{
    /// <summary>
    /// The main class for compiling ShapeCommands.
    /// </summary>
    /// <remarks>
    /// This class implements <see cref="graphical_programming_language.IShapeCompiler"/> interface and provides abstract and virtual methods for child Shape classes to override.
    /// </remarks>
    public class ShapeCompiler : ICompiler
    {
        private Lexer lexer;
        public Dictionary<string, string> Variables { get; set; }

        private readonly ShapeFactory shapeFactory;
        private readonly Panel outputWindow;
        private readonly RichTextBox programLog;
        private Pen pen;
        private Color fillColor;

        private readonly Regex inputSplitter;

        /// <summary>
        /// Gets and sets the command for the compiler.
        /// </summary>
        public string Command { get; set; }

        /// <summary>
        /// Gets and sets the arguments for the compiler.
        /// </summary>
        public string[] Arguments { get; set; }

        private int xPos;
        private int yPos;
        private int toXPos;
        private int toYPos;
        private int width;
        private int height;
        private int radius;

        private bool isColorFillOn;

        /// <summary>
        /// The default constructor.
        /// </summary>
        /// <remarks>
        /// Initializes a new instance of ShapeCompiler, with default values.
        /// </remarks>
        public ShapeCompiler()
        {
            lexer = new Lexer();
            Variables = new Dictionary<string, string>();

            shapeFactory = new ShapeFactory();
            isColorFillOn = false;
            pen = GetPen(Color.Black, 1);

            inputSplitter = new Regex(@"[\s+,]", RegexOptions.Compiled);

            xPos = yPos = toXPos = toYPos = 0;
            width = height = 100;
        }

        /// <summary>
        /// Parameterized constructor.
        /// </summary>
        /// <param name="outputWindow">The Panel where output of command after its execution is displayed.</param>
        /// <param name="programLog">The TexBox where the log of the command during its execution is displayed.</param>
        /// <remarks>
        /// Initializes a new instances of Shape, with the given parameters.
        /// </remarks>
        public ShapeCompiler(Panel outputWindow, RichTextBox programLog)
        {
            lexer = new Lexer();
            Variables = new Dictionary<string, string>();

            shapeFactory = new ShapeFactory();
            this.outputWindow = outputWindow;
            this.programLog = programLog;
            isColorFillOn = false;
            pen = GetPen(Color.Black, 1);

            inputSplitter = new Regex(@"[\s+,]", RegexOptions.Compiled);

            xPos = yPos = toXPos = toYPos = 0;
            width = height = 100;
        }

        public void ParseUsingLexer(string input, int lineNum)
        {
            var tokens = lexer.Advance(input);
            string variable_name = "";

            for (int i = 0; i < tokens.Count; i++)
            {
                var t = tokens[i];
                var numbersList = new List<int>();
                var operators = new Queue<string>();

                // Check for variable statement
                if (t.getType() == Type.IDENTIFIER) { variable_name = t.getValue(); }

                // Check for variable in expression
                if (t.getType() == Type.OPERATOR && t.getValue() == "=" && tokens.Count > 3)
                {
                    foreach (var _token in tokens.GetRange(2, tokens.Count - 2))
                    {
                        if (_token.getType() == Type.IDENTIFIER)
                            numbersList.Add(int.Parse(Variables[_token.getValue()]));
                        if (_token.getType() == Type.NUMBER)
                            numbersList.Add(int.Parse(_token.getValue()));
                        if (_token.getType() == Type.OPERATOR)
                            operators.Enqueue(_token.getValue());
                    }
                    var result = numbersList[0];
                    for (int j = 1; j < numbersList.Count; j++)
                    {
                        switch (operators.Dequeue())
                        {
                            case "+":
                                result += numbersList[j];
                                break;

                            case "-":
                                result -= numbersList[j];
                                break;

                            case "/":
                                result /= numbersList[j];
                                break;

                            case "*":
                                result *= numbersList[j];
                                break;
                        }
                    }

                    if (!Variables.ContainsKey(variable_name))
                    {
                        Variables.Add(variable_name, result.ToString());
                    }
                    else
                    {
                        Variables[variable_name] = result.ToString();
                    }

                    break;
                }

                // Assign and store number to var
                if (t.getType() == Type.NUMBER)
                {
                    if (!Variables.ContainsKey(variable_name))
                    {
                        Variables.Add(variable_name, t.getValue());
                    }
                    else
                    {
                        Variables[variable_name] = t.getValue();
                    }
                }
            }

            foreach (KeyValuePair<string, string> kvp in Variables)
            {
                Console.WriteLine(kvp);
            }
        }

        public bool ParseUsingIf(string input)
        {
            var tokens = lexer.Advance(input);
            bool result = false;
            string op = "";
            var numbersList = new List<int>();
            foreach (var _token in tokens.GetRange(1, 3))
            {
                if (_token.getType() == Type.IDENTIFIER)
                    numbersList.Add(int.Parse(Variables[_token.getValue()]));
                if (_token.getType() == Type.NUMBER)
                    numbersList.Add(int.Parse(_token.getValue()));
                if (_token.getType() == Type.OPERATOR)
                    op = _token.getValue();
            }

            int left = numbersList[0];
            int right = numbersList[1];

            switch (op)
            {
                case "<":
                    result = left < right;
                    break;

                case ">":
                    result = left > right;
                    break;

                case "<=":
                    result = left <= right;
                    break;

                case ">=":
                    result = left >= right;
                    break;

                case "==":
                    result = left == right;
                    break;

                case "!=":
                    result = left != right;
                    break;
            }

            return result;
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

        /// <summary>
        /// Gets a new Pen Object.
        /// </summary>
        /// <param name="color">Color of the Pen.</param>
        /// <param name="size">Width of the Pen.</param>
        /// <returns>Returns a new Pen Object with the specified Color and Width.</returns>
        public Pen GetPen(Color color, int size)
        {
            return new Pen(color, size);
        }

        // Call appropriate action according to the command and arguments passed to it.
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
                            if (Variables.ContainsKey(arguments[0]))
                                radius = int.Parse(Variables[arguments[0]]);
                            else
                                radius = int.Parse(arguments[0]);
                            width = radius * 2;
                            height = radius * 2;
                        }
                    }
                    else
                    {
                        if (Variables.ContainsKey(arguments[0]) && Variables.ContainsKey(arguments[1]))
                        {
                            width = int.Parse(Variables[arguments[0]]);
                            height = int.Parse(Variables[arguments[1]]);
                        }
                        else
                        {
                            width = int.Parse(arguments[0]);
                            height = int.Parse(arguments[1]);
                        }
                    }

                    Shape shape = shapeFactory.GetShape(command, fillColor, isColorFillOn, xPos, yPos, width, height);
                    shape.Draw(outputWindow.CreateGraphics(), pen);

                    programLog.SelectionColor = Color.Black;
                    programLog.AppendText($"[*] {shape.GetType().Name} drawn at position x -> {xPos}, y -> {yPos} with width -> {width}, height -> {height}");
                    programLog.AppendText(Environment.NewLine);
                }
                catch (ArgumentException ex)
                {
                    programLog.SelectionColor = Color.Red;
                    programLog.AppendText($"[*] Error: {ex.Message}");
                    programLog.AppendText(Environment.NewLine);
                }
                catch (FormatException)
                {
                    programLog.SelectionColor = Color.Red;
                    programLog.AppendText($"[*] Error: Given argument is not in correct format");
                    programLog.AppendText(Environment.NewLine);
                }
                catch (IndexOutOfRangeException)
                {
                    string shape = command.ToUpper().Equals("RECT") ? "Rectangle" : command;

                    programLog.SelectionColor = Color.Red;
                    programLog.AppendText($"[*] Error: Please provide two parameter for drawing {shape}");
                    programLog.AppendText(Environment.NewLine);
                }
            }
            else if (command.ToUpper().Equals("DRAWTO"))
            {
                try
                {
                    if (Variables.ContainsKey(arguments[0]) && Variables.ContainsKey(arguments[1]))
                    {
                        toXPos = int.Parse(Variables[arguments[0]]);
                        toYPos = int.Parse(Variables[arguments[1]]);
                    }
                    else
                    {
                        toXPos = int.Parse(arguments[0]);
                        toYPos = int.Parse(arguments[1]);
                    }

                    Shape shape = shapeFactory.GetShape("line", fillColor, isColorFillOn, xPos, yPos, toXPos, toYPos);
                    shape.Draw(outputWindow.CreateGraphics(), pen);

                    programLog.SelectionColor = Color.Black;
                    programLog.AppendText($"[*] Line drawn from position x1 -> {xPos}, y1 -> {yPos} to position x2 -> {toXPos}, y2 -> {toYPos}");
                    programLog.AppendText(Environment.NewLine);

                    xPos = toXPos;
                    yPos = toYPos;
                }
                catch (IndexOutOfRangeException)
                {
                    programLog.SelectionColor = Color.Red;
                    programLog.AppendText($"[*] Error: Please provide two parameter to draw a line");
                    programLog.AppendText(Environment.NewLine);
                }
            }
            else if (command.ToUpper().Equals("MOVETO"))
            {
                try
                {
                    if (Variables.ContainsKey(arguments[0]) && Variables.ContainsKey(arguments[1]))
                    {
                        xPos = int.Parse(Variables[arguments[0]]);
                        yPos = int.Parse(Variables[arguments[1]]);
                    }
                    else
                    {
                        xPos = int.Parse(arguments[0]);
                        yPos = int.Parse(arguments[1]);
                    }

                    programLog.SelectionColor = Color.Black;
                    programLog.AppendText($"[*] Pen position set to {xPos}, {yPos}");
                    programLog.AppendText(Environment.NewLine);
                }
                catch (IndexOutOfRangeException)
                {
                    programLog.SelectionColor = Color.Red;
                    programLog.AppendText($"[*] Error: Please provide two parameter to move pointer");
                    programLog.AppendText(Environment.NewLine);
                }
            }
            else if (command.ToUpper().Equals("PEN"))
            {
                try
                {
                    Color color;
                    if (Variables.ContainsKey(arguments[0]))
                    {
                        color = Color.FromName(Variables[arguments[0]]);
                    }
                    else
                    {
                        color = Color.FromName(arguments[0]);
                    }
                    int size = (arguments.Length == 2) ? int.Parse(arguments[1]) : 1;

                    pen = GetPen(color, size);

                    programLog.SelectionColor = Color.Black;
                    programLog.AppendText($"[*] Pen color set to {color.Name} and pen size set to {size}");
                    programLog.AppendText(Environment.NewLine);
                }
                catch (IndexOutOfRangeException)
                {
                    programLog.SelectionColor = Color.Red;
                    programLog.AppendText($"[*] Error: Please provide one parameter for selecting the color");
                    programLog.AppendText(Environment.NewLine);
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

                        programLog.SelectionColor = Color.Black;
                        programLog.AppendText($"[*] Color fill is now {isColorFillOn} and set to {fillColor.Name}");
                        programLog.AppendText(Environment.NewLine);
                    }
                    else if (arguments[0].ToUpper().Equals("OFF"))
                    {
                        isColorFillOn = false;

                        programLog.SelectionColor = Color.Black;
                        programLog.AppendText($"[*] Color fill is now {isColorFillOn}");
                        programLog.AppendText(Environment.NewLine);
                    }
                }
                catch (IndexOutOfRangeException)
                {
                    programLog.SelectionColor = Color.Red;
                    programLog.AppendText($"[*] Error: Please provide one parameter (on/off) to either turn fill on/off");
                    programLog.AppendText(Environment.NewLine);
                }
            }
            else if (command.ToUpper().Equals("RESET"))
            {
                xPos = yPos = toXPos = toYPos = 0;
                width = height = 100;

                programLog.SelectionColor = Color.Black;
                programLog.AppendText("[*] Reset pen position to 0, 0");
                programLog.AppendText(Environment.NewLine);
            }
            else if (command.ToUpper().Equals("CLEAR"))
            {
                outputWindow.Refresh();

                programLog.SelectionColor = Color.Black;
                programLog.AppendText("[*] Cleared output panel");
                programLog.AppendText(Environment.NewLine);
            }
            else if (command.ToUpper().Equals("EXIT"))
            {
                programLog.SelectionColor = Color.Black;
                programLog.AppendText("[*] Exiting application");
                programLog.AppendText(Environment.NewLine);

                Application.Exit();
            }
            else
            {
                programLog.SelectionColor = Color.Red;
                programLog.AppendText($"[*] Error: Command {command} not found");
                programLog.AppendText(Environment.NewLine);
            }
        }

        // Generate a color from the given string.
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