using System.Drawing;

namespace graphical_programming_language
{
    public interface IShapes
    {
        void Set(Color color, params int[] list);

        void Draw(Graphics graphics, Pen pen);
    }
}