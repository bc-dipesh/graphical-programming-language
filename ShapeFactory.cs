using System.Drawing;

namespace graphical_programming_language
{
    /// <summary>
    /// The main ShapeFactory class.
    /// </summary>
    /// <remarks>
    /// This class is a factory that provides Shapes from below.
    /// <list type="bullet">
    /// <item>
    /// <term>Rectangle</term>
    /// <description><see cref="Rectangle"/> class</description>
    /// </item>
    /// <item>
    /// <term>Triangle</term>
    /// <description><see cref="Triangle"/> class</description>
    /// </item>
    /// <item>
    /// <term>Circle</term>
    /// <description><see cref="Circle"/> class</description>
    /// </item>
    /// </list>
    /// </remarks>
    public class ShapeFactory
    {
        /// <summary>
        /// Gets the shape.
        /// </summary>
        /// <exception cref = "System.ArgumentException" > Thrown when the ShapeType is not recognized.</exception>
        /// <param name="shapeType"></param>
        /// <returns>The shape from the shapeType</returns>
        public Shape GetShape(string shapeType)
        {
            shapeType = shapeType.ToUpper().Trim();

            if (shapeType.Equals("RECT"))
            {
                return new Rectangle();
            }
            else if (shapeType.Equals("CIRCLE"))
            {
                return new Circle();
            }
            else if (shapeType.Equals("TRIANGLE"))
            {
                return new Triangle();
            }
            else if (shapeType.Equals("LINE"))
            {
                return new Line();
            }
            else
            {
                System.ArgumentException argumentException = new System.ArgumentException($"Factory error: {shapeType} does not exist");
                throw argumentException;
            }
        }

        /// <summary>
        /// Gets the shape.
        /// </summary>
        /// <exception cref = "System.ArgumentException" > Thrown when the ShapeType is not recognized.</exception>
        /// <param name="shapeType">The type of the Shape.</param>
        /// <param name="color">Color of the Shape.</param>
        /// <param name="isColorFillOn">Boolean flag to either turn or off the Color fill for the Shape.</param>
        /// <param name="x">The X-Coordinate to draw the Shape in.</param>
        /// <param name="y">The Y-Coordinate to draw the Shape in.</param>
        /// <param name="width">The width of the Shape.</param>
        /// <param name="height">The height of the Shape.</param>
        /// <returns></returns>
        public Shape GetShape(string shapeType, Color color, bool isColorFillOn, int x, int y, int width, int height)
        {
            shapeType = shapeType.ToUpper().Trim();

            if (shapeType.Equals("RECT"))
            {
                return new Rectangle(color, isColorFillOn, x, y, width, height);
            }
            else if (shapeType.Equals("CIRCLE"))
            {
                return new Circle(color, isColorFillOn, x, y, width, height);
            }
            else if (shapeType.Equals("TRIANGLE"))
            {
                return new Triangle(color, isColorFillOn, x, y, width, height);
            }
            else if (shapeType.Equals("LINE"))
            {
                return new Line(x, y, width, height);
            }
            else
            {
                System.ArgumentException argumentException = new System.ArgumentException($"Factory error: {shapeType} does not exist");
                throw argumentException;
            }
        }
    }
}