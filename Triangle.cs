using System.Drawing;

namespace graphical_programming_language
{
    /// <summary>
    /// The main Triangle class.
    /// Inherits abstract class <see cref="graphical_programming_language.Shape"/>.
    /// </summary>
    /// <remarks>
    /// This class can draw Triangle Shape.
    /// </remarks>
    public class Triangle : Shape
    {
        private Point[] trianglePoints;
        private System.Drawing.Rectangle rectangle;

        /// <summary>
        /// The default constructor.
        /// </summary>
        /// <remarks>
        /// Initializes a new instance of Triangle, with default values.
        /// </remarks>
        public Triangle() : base()
        {
            rectangle = new System.Drawing.Rectangle(X, Y, 100, 100);
            trianglePoints = GetTrianglePointsFromRectangle(rectangle);
        }

        /// <summary>
        /// Parameterized constructor.
        /// </summary>
        /// <param name="color">Color of the Triangle.</param>
        /// <param name="isColorFillOn">Boolean flag to either turn or off the Color fill for the Triangle.</param>
        /// <param name="x">The X-Coordinate to draw Triangle in.</param>
        /// <param name="y">The Y-Coordinate to draw Triangle in.</param>
        /// <param name="width">The width of the Triangle.</param>
        /// <param name="height">The height of the Triangle.</param>
        /// <remarks>
        /// Initializes a new instances of Triangle, with the given parameters.
        /// </remarks>
        public Triangle(Color color, bool isColorFillOn, int x, int y, int width, int height) : base(color, isColorFillOn, x, y)
        {
            rectangle = new System.Drawing.Rectangle(x, y, width, height);
            trianglePoints = GetTrianglePointsFromRectangle(rectangle);
        }

        public override void Set(Color color, params int[] list)
        {
            base.Set(color, list);
            rectangle = new System.Drawing.Rectangle(X, Y, list[2], list[3]);
            trianglePoints = GetTrianglePointsFromRectangle(rectangle);
        }

        public override void Draw(Graphics graphics, Pen pen)
        {
            if (IsColorFillOn)
            {
                graphics.FillPolygon(new SolidBrush(Color), trianglePoints);
            }
            graphics.DrawPolygon(pen, trianglePoints);

            graphics.Dispose();
        }

        // Returns the points to draw a triangle given a triangle.
        // param System.Drawing.Rectangle rectangle: The rectangle used to generate the points for the traingle.
        // Generates points from the rectangle to draw a triangle.
        private Point[] GetTrianglePointsFromRectangle(System.Drawing.Rectangle rectangle)
        {
            Point[] points = new Point[3];

            points[0] = new Point(rectangle.Left + (rectangle.Width / 2), rectangle.Top);
            points[1] = new Point(rectangle.Left, rectangle.Bottom);
            points[2] = new Point(rectangle.Right, rectangle.Bottom);

            return points;
        }
    }
}