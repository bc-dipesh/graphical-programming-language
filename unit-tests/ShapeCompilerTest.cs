using graphical_programming_language;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Drawing;

namespace unit_tests
{
    /// <summary>
    /// The main class to test <see cref="graphical_programming_language.ShapeCompiler"/> class.
    /// </summary>
    /// <remarks>
    /// This class tests the <see cref="graphical_programming_language.ShapeCompiler"/> class methods<br></br>
    /// with some test methods and paramters.
    /// </remarks>
    [TestClass]
    public class ShapeCompilerTest
    {
        /// <summary>
        /// Tests the GetPen method.
        /// </summary>
        /// <remarks>
        /// Tests if the GetPen method returns correct Pen object given a Color and Width for the Pen.
        /// </remarks>
        [TestMethod]
        public void GetPen()
        {
            var color = Color.Black;
            var size = 1;
            var expectedPen = new Pen(color, size);

            ShapeCompiler shapeCompiler = new ShapeCompiler();
            Pen actualPen = shapeCompiler.GetPen(color, size);

            Assert.AreEqual(expectedPen.Color, actualPen.Color);
            Assert.AreEqual(expectedPen.Width, actualPen.Width);
        }

        /// <summary>
        /// Tests the Compile method.
        /// </summary>
        /// <remarks>
        /// Tests if the Compile method returns correct commands<br></br>
        /// after compiling the command.
        /// </remarks>
        [TestMethod]
        public void Compile()
        {
            var shapeCompiler = new ShapeCompiler();
            var expenctedCommands = new List<string>
            {
                "rect",
                "circle",
                "triangle",
                "moveto",
                "drawto",
                "pen",
                "fill",
                "fill",
                "clear",
                "reset"
            };
            var inputCommands = new List<string>
            {
                "rect 100,100",
                "circle 25",
                "triangle 100,100",
                "moveto 10, 10",
                "drawto 10, 10",
                "pen red",
                "fill on blue",
                "fill off",
                "clear",
                "reset"
            };
            var actualCommands = new List<string>();

            inputCommands.ForEach(command =>
            {
                shapeCompiler.Compile(command);
                actualCommands.Add(shapeCompiler.Command);
            });

            CollectionAssert.AreEqual(expenctedCommands, actualCommands);
        }

        /// <summary>
        /// Tests variable assignment method.
        /// </summary>
        /// <remarks>
        /// Tests if the compiler can properly assign value to a variable.
        /// </remarks>
        [TestMethod]
        public void AssignVariable()
        {
        }

        /// <summary>
        /// Tests looping method.
        /// </summary>
        /// <remarks>
        /// Tests if the compiler can handle looping command.
        /// </remarks>
        [TestMethod]
        public void RunLoop()
        {
        }

        /// <summary>
        /// Tests IfElse.
        /// </summary>
        /// <remarks>
        /// Tests if the compiler can handle IfElse command.
        /// </remarks>
        [TestMethod]
        public void IfElse()
        {
        }
    }
}