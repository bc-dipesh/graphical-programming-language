using System;
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

                shapeCompiler.DrawToPanel = true;
                shapeCompiler.ParseProgram(programWindow.Text, commandLine.Text);
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

        private void CheckSyntax_Click(object sender, EventArgs e)
        {
            shapeCompiler.DrawToPanel = false;
            shapeCompiler.ParseProgram(programWindow.Text, commandLine.Text);
        }
    }
}