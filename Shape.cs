using System.Drawing;

namespace graphical_programming_language
{
    public abstract class Shape : IShapes
    {
        public Color Color { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public bool IsColorFillOn { get; set; }

        public Shape()
        {
            Color = Color.Black;
            X = Y = 0;
            IsColorFillOn = false;
        }

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