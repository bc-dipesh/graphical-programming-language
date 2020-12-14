using System;
using System.Windows.Forms;

namespace graphical_programming_language
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        private static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new GraphicalProgrammingLanguageApp());
        }
    }
}