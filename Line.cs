using System.Drawing;

namespace graphical_programming_language
{
    /// <summary>
    /// The main Line class.
    /// Inherits abstract class <see cref="graphical_programming_language.Shape"/>.
    /// </summary>
    /// <remarks>
    /// This class can draw Line.
    /// </remarks>
    public class Line : Shape
    {
        private readonly float fromXPos;
        private readonly float fromYPos;
        private float toXPos;
        private float toYPos;

        /// <summary>
        /// The default constructor.
        /// </summary>
        /// <remarks>
        /// Initializes a new instance of Line, with default values.
        /// </remarks>
        public Line() : base()
        {
            fromXPos = X;
            fromYPos = Y;
            toXPos = 100;
            toYPos = 100;
        }

        /// <summary>
        /// Parameterized constructor.
        /// </summary>
        /// <param name="fromXPos">The initial X-Coordinate of the line.</param>
        /// <param name="fromYPos">The initial Y-Coordinate of the line.</param>
        /// <param name="toXPos">The final X-Coordinate of the line to draw to.</param>
        /// <param name="toYPos">The final Y-Coordinate of the line to draw to.</param>
        /// <remarks>
        /// Initializes a new instances of Line, with the given parameters.
        /// </remarks>
        public Line(float fromXPos, float fromYPos, float toXPos, float toYPos)
        {
            this.fromXPos = fromXPos;
            this.fromYPos = fromYPos;
            this.toXPos = toXPos;
            this.toYPos = toYPos;
        }

        public override void Set(Color color, params int[] list)
        {
            base.Set(color, list);
            toXPos = list[2];
            toYPos = list[3];
        }

        public override void Draw(Graphics graphics, Pen pen)
        {
            graphics.DrawLine(pen, fromXPos, fromYPos, toXPos, toYPos);

            graphics.Dispose();
        }
    }
}
