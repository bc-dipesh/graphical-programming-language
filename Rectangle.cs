using System.Drawing;

namespace graphical_programming_language
{
    /// <summary>
    /// The main Rectangle class.
    /// Inherits abstract class <see cref="graphical_programming_language.Shape"/>.
    /// </summary>
    /// <remarks>
    /// This class can draw Rectangle Shape.
    /// </remarks>
    public class Rectangle : Shape

    {
        /// <summary>
        /// Gets or sets the Width of the Rectangle.
        /// </summary>
        public int Width { get; set; }

        /// <summary>
        /// Gets or sets the Height of the Rectangle.
        /// </summary>
        public int Height { get; set; }

        /// <summary>
        /// The default constructor.
        /// </summary>
        /// <remarks>
        /// Initializes a new instance of Rectangle, with default values.
        /// </remarks>
        public Rectangle() : base()
        {
            Width = 100;
            Height = 100;
        }

        /// <summary>
        /// Parameterized constructor.
        /// </summary>
        /// <param name="color">Color of the Rectangle.</param>
        /// <param name="isColorFillOn">Boolean flag to either turn or off the Color fill for the Rectangle.</param>
        /// <param name="x">The X-Coordinate to draw Rectangle in.</param>
        /// <param name="y">The Y-Coordinate to draw Rectangle in.</param>
        /// <param name="width">The width of the Rectangle.</param>
        /// <param name="height">The height of the Rectangle.</param>
        /// <remarks>
        /// Initializes a new instances of Rectangle, with the given parameters.
        /// </remarks>
        public Rectangle(Color color, bool isColorFillOn, int x, int y, int width, int height) : base(color, isColorFillOn, x, y)
        {
            Width = width;
            Height = height;
        }

        public override void Set(Color color, params int[] list)
        {
            base.Set(color, list);
            Width = list[2];
            Height = list[3];
        }

        public override void Draw(Graphics graphics, Pen pen)
        {
            if (IsColorFillOn)
            {
                graphics.FillRectangle(new SolidBrush(Color), X, Y, Width, Height);
            }
            graphics.DrawRectangle(pen, X, Y, Width, Height);

            graphics.Dispose();
        }
    }
}