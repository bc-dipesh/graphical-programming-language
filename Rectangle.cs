using System.Drawing;

namespace graphical_programming_language
{
    public class Rectangle : Shape

    {
        private int width;
        private int height;

        public Rectangle() : base()
        {
            width = 100;
            height = 100;
        }

        public Rectangle(Color color, bool isColorFillOn, int x, int y, int width, int height) : base(color, isColorFillOn, x, y)
        {
            this.width = width;
            this.height = height;
        }

        public override void Set(Color color, params int[] list)
        {
            base.Set(color, list);
            width = list[2];
            height = list[3];
        }

        public override void Draw(Graphics graphics, Pen pen)
        {
            if (IsColorFillOn)
            {
                graphics.FillRectangle(new SolidBrush(Color), X, Y, width, height);
            }
            graphics.DrawRectangle(pen, X, Y, width, height);

            graphics.Dispose();
        }
    }
}