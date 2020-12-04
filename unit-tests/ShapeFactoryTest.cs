using graphical_programming_language;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace unit_tests
{
    /// <summary>
    /// The main class to test <see cref="ShapeFactory"/> class.
    /// </summary>
    /// <remarks>
    /// This class tests the <see cref="ShapeFactory"/> class methods<br></br>
    /// <see cref="ShapeFactory.GetShape(string)"/> and <see cref="ShapeFactory.GetShape(string, System.Drawing.Color, bool, int, int, int, int)"/>.
    /// </remarks>
    [TestClass]
    public class ShapeFactoryTest
    {
        /// <summary>
        /// Tests the GetShape method.
        /// </summary>
        /// <remarks>
        /// Tests if the factory is able to return Shape <see cref="Rectangle"/>
        /// </remarks>
        [TestMethod]
        public void GetShapeRectangle()
        {
            var shapeFactory = new ShapeFactory();

            var actualShape = shapeFactory.GetShape("rect");

            Assert.IsTrue(actualShape is Rectangle);
        }

        /// <summary>
        /// Tests the GetShape method.
        /// </summary>
        /// <remarks>
        /// Tests if the factory is able to return Shape <see cref="Triangle"/>
        /// </remarks>
        [TestMethod]
        public void GetShapeTriangle()
        {
            var shapeFactory = new ShapeFactory();

            var actualShape = shapeFactory.GetShape("triangle");

            Assert.IsTrue(actualShape is Triangle);
        }

        /// <summary>
        /// Tests the GetShape method.
        /// </summary>
        /// <remarks>
        /// Tests if the factory is able to return Shape <see cref="Circle"/>
        /// </remarks>
        [TestMethod]
        public void GetShapeCircle()
        {
            var shapeFactory = new ShapeFactory();

            var actualShape = shapeFactory.GetShape("circle");

            Assert.IsTrue(actualShape is Circle);
        }

        /// <summary>
        /// Tests the GetShape method.
        /// </summary>
        /// <remarks>
        /// Tests if the factory is able to return Shape <see cref="Line"/>
        /// </remarks>
        [TestMethod]
        public void GetShapeLine()
        {
            var shapeFactory = new ShapeFactory();

            var actualShape = shapeFactory.GetShape("line");

            Assert.IsTrue(actualShape is Line);
        }
    }
}