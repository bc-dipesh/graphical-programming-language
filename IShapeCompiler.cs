namespace graphical_programming_language
{
    /// <summary>
    /// The interface for Compiling Commands.
    /// </summary>
    /// <remarks>
    /// This interface defines generic method for a compiler to implement.
    /// </remarks>
    public interface ICompiler
    {

        /// <summary>
        /// Compiles a command.
        /// </summary>
        /// <param name="input">The input provided by the user.</param>
        /// <remarks>
        /// Compiles an input provided by the user and uses its output to initialize current object fields.
        /// </remarks>
        void Compile(string command);

        /// <summary>
        /// Runs the code.
        /// </summary>
        /// <remarks>
        /// Runs the command generated from the compiler.
        /// </remarks>
        void Run();
    }
}