using System.Drawing;

namespace graphical_programming_language
{
    internal abstract class Shape : IShapes
    {
        protected Color color;
        protected int x, y;
        protected bool isColorFillOn;

        public Shape()
        {
            color = Color.Black;
            x = y = 100;
            isColorFillOn = false;
        }

        public Shape(Color color, bool isColorFillOn, int x, int y)
        {
            this.color = color;
            this.isColorFillOn = isColorFillOn;
            this.x = x;
            this.y = y;
        }

        public abstract void Draw(Graphics graphics, Pen pen);

        public virtual void Set(Color color, params int[] list)
        {
            this.color = color;
            this.x = list[0];
            this.y = list[1];
        }

        public override string ToString()
        {
            return $"{base.ToString()}  {this.x}, {this.y} : ";
        }
    }
}