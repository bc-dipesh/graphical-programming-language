using System.Drawing;

namespace graphical_programming_language
{
    public class Rectangle : Shape

    {
        public int Width { get; set; }
        public int Height { get; set; }

        public Rectangle() : base()
        {
            Width = 100;
            Height = 100;
        }

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