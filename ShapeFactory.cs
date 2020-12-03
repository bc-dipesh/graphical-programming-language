using System.Drawing;

namespace graphical_programming_language
{
    public class ShapeFactory
    {
        public Shape GetShape(string shapeType)
        {
            shapeType = shapeType.ToUpper().Trim();

            if (shapeType.Equals("RECT"))
            {
                return new Rectangle();
            }
            else if (shapeType.Equals("CIRCLE"))
            {
                return new Circle();
            }
            else if (shapeType.Equals("TRIANGLE"))
            {
                return new Triangle();
            }
            else if (shapeType.Equals("LINE"))
            {
                return new Line();
            }
            else
            {
                System.ArgumentException argumentException = new System.ArgumentException($"Factory error: {shapeType} does not exist");
                throw argumentException;
            }
        }

        public Shape GetShape(string shapeType, Color color, bool isColorFillOn, int x, int y, int width, int height)
        {
            shapeType = shapeType.ToUpper().Trim();

            if (shapeType.Equals("RECT"))
            {
                return new Rectangle(color, isColorFillOn, x, y, width, height);
            }
            else if (shapeType.Equals("CIRCLE"))
            {
                return new Circle(color, isColorFillOn, x, y, width, height);
            }
            else if (shapeType.Equals("TRIANGLE"))
            {
                return new Triangle(color, isColorFillOn, x, y, width, height);
            }
            else if (shapeType.Equals("LINE"))
            {
                return new Line(x, y, width, height);
            }
            else
            {
                System.ArgumentException argumentException = new System.ArgumentException($"Factory error: {shapeType} does not exist");
                throw argumentException;
            }
        }
    }
}