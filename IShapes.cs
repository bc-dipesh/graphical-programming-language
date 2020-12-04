using System.Drawing;

namespace graphical_programming_language
{
    /// <summary>
    /// The interface for Shape.
    /// </summary>
    /// <remarks>
    /// This interface defines the methods for Shapes to implement.
    /// </remarks>
    public interface IShapes
    {
        /// <summary>
        /// Sets the color and properties of the Shape.
        /// </summary>
        /// <param name="color">The color of the shape.</param>
        /// <param name="list">Array of int as properties for the Shape.</param>
        void Set(Color color, params int[] list);

        /// <summary>
        /// Draws Shape to the Graphics.
        /// </summary>
        /// <param name="graphics">The drawing surface to draw the Shape to.</param>
        /// <param name="pen">The pen used to draw the Shape.</param>
        void Draw(Graphics graphics, Pen pen);
    }
}