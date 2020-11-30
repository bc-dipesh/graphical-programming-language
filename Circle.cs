using System.Drawing;

namespace graphical_programming_language
{
    internal class Circle : Shape
    {
        private int width;
        private int height;

        public Circle() : base()
        {
            width = 100;
            height = 100;
        }

        public Circle(Color color, bool isColorFillOn, int x, int y, int width, int height) : base(color, isColorFillOn, x, y)
        {
            this.width = width;
            this.height = height;
        }

        public override void Set(Color color, params int[] list)
        {
            base.Set(color, list);
            this.width = list[2];
            this.height = list[3];
        }

        public override void Draw(Graphics graphics, Pen pen)
        {
            if (isColorFillOn)
            {
                graphics.FillEllipse(new SolidBrush(color), x, y, width, height);
            }
            graphics.DrawEllipse(pen, x, y, width, height);

            graphics.Dispose();
        }
    }
}