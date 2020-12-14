using System;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace graphical_programming_language
{
    /// <summary>
    /// The main Form class.
    /// Inherits class <see cref="Form"/>.
    /// </summary>
    /// <remarks>
    /// This class displays the application form/view.
    /// </remarks>
    public partial class GraphicalProgrammingLanguageApp : Form
    {
        private readonly ShapeCompiler shapeCompiler;
        private StringBuilder aboutMessage;

        private Lexer lexer;

        /// <summary>
        /// The default constructor.
        /// </summary>
        /// <remarks>
        /// Initializes a new instance of GraphicalProgrammingLanguageApp.<br></br>
        /// with default values, and sets up some private properties for the App
        /// </remarks>
        public GraphicalProgrammingLanguageApp()
        {
            InitializeComponent();
            SetUpAboutMessage();
            lexer = new Lexer();

            shapeCompiler = new ShapeCompiler(outputWindow, programLog);
        }

        // Sets up the about message for this app shown in the MessageBox.
        private void SetUpAboutMessage()
        {
            aboutMessage = new StringBuilder();
            aboutMessage.Append("A custom programming language made using C# that");
            aboutMessage.Append(Environment.NewLine);
            aboutMessage.Append("demonstrates the basics of sequence, selection and iteration");
            aboutMessage.Append(Environment.NewLine);
            aboutMessage.Append("and allows a student programmer to explore them using graphics.");
            aboutMessage.Append(Environment.NewLine);
            aboutMessage.Append(Environment.NewLine);
            aboutMessage.Append(Environment.NewLine);
            aboutMessage.Append("Author: Dipesh BC");
            aboutMessage.Append(Environment.NewLine);
            aboutMessage.Append("Version: v1.1.0");
            aboutMessage.Append(Environment.NewLine);
            aboutMessage.Append("License: MIT");
        }

        // Event handler to handle event when user presses a button.
        private void CommandTxtBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                // Stop the ding sound after the button is pressed.
                e.SuppressKeyPress = true;

                //var program = programWindow.Text.Split(new string[] { Environment.NewLine, "\n", "\r" }, StringSplitOptions.RemoveEmptyEntries);
                //var input = commandLine.Text;

                ParseProgram(programWindow.Text);
            }
        }

        private void ParseProgram(string programCode)
        {
            var program = programCode.Split(new string[] { Environment.NewLine, "\n", "\r" }, StringSplitOptions.RemoveEmptyEntries);
            var input = commandLine.Text;
            var cursor = 0;

            if (!string.IsNullOrWhiteSpace(input))
            {
                if (input.ToUpper().Equals("RUN"))
                {
                    //shapeCompiler.Variables.Clear();
                    for (int lineNumber = 0; lineNumber < program.Length; lineNumber++)
                    {
                        // If the line is not blank or null
                        if (!String.IsNullOrWhiteSpace(program[lineNumber]))
                        {
                            if (program[lineNumber].Contains("=") || program[lineNumber].Contains("if") || program[lineNumber].Contains("endif") || program[lineNumber].Contains("while") || program[lineNumber].Contains("function") || program[lineNumber].Contains("()"))
                            {
                                if (program[lineNumber].Contains("endfunction"))
                                {
                                    lineNumber = cursor;
                                }
                                else if (program[lineNumber].Contains("function"))
                                {
                                    var tokens = lexer.Advance(program[lineNumber]);
                                    int functionLineNum = lineNumber;
                                    for (; functionLineNum < program.Length; functionLineNum++)
                                    {
                                        if (program[functionLineNum].Contains("endfunction"))
                                        {
                                            break;
                                        }
                                    }
                                    shapeCompiler.Variables.Add(tokens[1].getValue(), lineNumber + "," + (functionLineNum - 1));
                                    lineNumber = functionLineNum;
                                }
                                else if (program[lineNumber].Contains("()"))
                                {
                                    cursor = lineNumber;
                                    var tokens = lexer.Advance(program[lineNumber]);
                                    var functionLines = shapeCompiler.Variables[tokens[0].getValue()].Split(',');
                                    lineNumber = Int32.Parse(functionLines[0]);
                                }
                                else if (program[lineNumber].Contains("while") && !program[lineNumber].Contains("endwhile"))
                                {
                                    int whileNum = lineNumber;
                                    whileNum++;
                                    while (shapeCompiler.ParseUsingIf(program[lineNumber]))
                                    {
                                        if (program[whileNum].Contains("endwhile"))
                                        {
                                            whileNum = lineNumber;
                                        }
                                        else
                                        {
                                            ParseProgram(program[whileNum]);
                                        }
                                        whileNum++;
                                    }
                                    lineNumber = whileNum;
                                }
                                else if (program[lineNumber].Contains("endif"))
                                {
                                    continue;
                                }
                                else if (program[lineNumber].Contains("if"))
                                {
                                    if (!shapeCompiler.ParseUsingIf(program[lineNumber]))
                                    {
                                        bool hasEndIf = false;
                                        int currentLineNumber = lineNumber;

                                        for (; lineNumber < program.Length; lineNumber++)
                                        {
                                            if (program[lineNumber].Contains("endif"))
                                            {
                                                hasEndIf = true;
                                                break;
                                            }
                                        }

                                        if (!hasEndIf)
                                        {
                                            lineNumber = ++currentLineNumber;
                                        }
                                    }
                                }
                                else if (program[lineNumber].Contains("="))
                                {
                                    shapeCompiler.ParseUsingLexer(program[lineNumber], lineNumber);
                                }
                            }
                            else
                            {  // Call the parse command method passing the line
                                shapeCompiler.Compile(program[lineNumber]);
                                shapeCompiler.Run();
                            }
                        }
                    }
                }
                else
                {
                    shapeCompiler.Compile(input);
                    shapeCompiler.Run();
                }
            }
            else
            {
                programLog.SelectionColor = Color.Red;
                programLog.AppendText($"[*] Error: Please provide a command to run");
                programLog.AppendText(Environment.NewLine);
            }
        }

        // Opens a FileDialog to choose a file and display its conentes in the programWindow.
        private void OpenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Title = "Load a program file",
                Filter = "Text Files (*.txt) | *.txt"
            };

            DialogResult dialogResult = openFileDialog.ShowDialog();

            if (dialogResult == DialogResult.OK)
            {
                programWindow.LoadFile(openFileDialog.FileName, RichTextBoxStreamType.PlainText);
            }
        }

        // Opens a FileDialog to save current contents of programWindow to a file.
        private void SaveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog
            {
                Title = "Save current program to file",
                Filter = "Text files (*.txt) | *.txt"
            };

            DialogResult dialogResult = saveFileDialog.ShowDialog();

            if (dialogResult == DialogResult.OK)
            {
                programWindow.SaveFile(saveFileDialog.FileName, RichTextBoxStreamType.PlainText);
            }
        }

        // Exits the application.
        private void ExitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        // Displays a MessageBox with the message containing information about this application.
        private void AboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show(aboutMessage.ToString(), "About", MessageBoxButtons.OK, MessageBoxIcon.Question);
        }
    }
}