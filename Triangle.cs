using System.Drawing;

namespace graphical_programming_language
{
    public class Triangle : Shape
    {
        private Point[] trianglePoints;
        private System.Drawing.Rectangle rectangle;

        public Triangle() : base()
        {
            rectangle = new System.Drawing.Rectangle(X, Y, 100, 100);
            trianglePoints = getTrianglePointsFromRectangle(rectangle);
        }

        public Triangle(Color color, bool isColorFillOn, int x, int y, int width, int height) : base(color, isColorFillOn, x, y)
        {
            rectangle = new System.Drawing.Rectangle(x, y, width, height);
            trianglePoints = getTrianglePointsFromRectangle(rectangle);
        }

        public override void Set(Color color, params int[] list)
        {
            base.Set(color, list);
            rectangle = new System.Drawing.Rectangle(X, Y, list[2], list[3]);
            trianglePoints = getTrianglePointsFromRectangle(rectangle);
        }

        public override void Draw(Graphics graphics, Pen pen)
        {
            if (IsColorFillOn)
            {
                graphics.FillPolygon(new SolidBrush(Color), trianglePoints);
            }
            graphics.DrawPolygon(pen, trianglePoints);

            graphics.Dispose();
        }

        private Point[] getTrianglePointsFromRectangle(System.Drawing.Rectangle rectangle)
        {
            Point[] points = new Point[3];

            points[0] = new Point(rectangle.Left + (rectangle.Width / 2), rectangle.Top);
            points[1] = new Point(rectangle.Left, rectangle.Bottom);
            points[2] = new Point(rectangle.Right, rectangle.Bottom);

            return points;
        }
    }
}