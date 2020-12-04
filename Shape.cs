using System.Drawing;

namespace graphical_programming_language
{

    /// <summary>
    /// Parent abstract class for all the Shapes.
    /// </summary>
    /// <remarks>
    /// This class implements <see cref="graphical_programming_language.IShapes"/> interface and provides abstract and virtual methods for child Shape classes to override.
    /// </remarks>
    public abstract class Shape : IShapes
    {
        /// <summary>
        /// Gets or sets the Color of the Shape.
        /// </summary>
        public Color Color { get; set; }

        /// <summary>
        /// Gets or sets the X-Coordinate of the Shape.
        /// </summary>
        public int X { get; set; }

        /// <summary>
        /// Gets or sets the Y-Coordinate of the Shape.
        /// </summary>
        public int Y { get; set; }

        /// <summary>
        /// Gets or sets the boolean flag to either turn or off the Color fill.
        /// </summary>
        public bool IsColorFillOn { get; set; }

        /// <summary>
        /// The default constructor.
        /// </summary>
        /// <remarks>
        /// Initializes a new instance of Shape, with default values.
        /// </remarks>
        public Shape()
        {
            Color = Color.Black;
            X = Y = 0;
            IsColorFillOn = false;
        }

        /// <summary>
        /// Parameterized constructor.
        /// </summary>
        /// <param name="color">Color of the Rectangle.</param>
        /// <param name="isColorFillOn">Boolean flag to either turn or off the Color fill for the Shape.</param>
        /// <param name="x">The X-Coordinate to draw Shape in.</param>
        /// <param name="y">The Y-Coordinate to draw Shape in.</param>
        /// <remarks>
        /// Initializes a new instances of Shape, with the given parameters.
        /// </remarks>
        public Shape(Color color, bool isColorFillOn, int x, int y)
        {
            Color = color;
            IsColorFillOn = isColorFillOn;
            X = x;
            Y = y;
        }

        public abstract void Draw(Graphics graphics, Pen pen);

        public virtual void Set(Color color, params int[] list)
        {
            Color = color;
            X = list[0];
            Y = list[1];
        }

        public override string ToString()
        {
            return $"{base.ToString()}  {X}, {Y} : ";
        }
    }
}