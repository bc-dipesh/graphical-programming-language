using System.Drawing;

namespace graphical_programming_language
{
    internal class ShapeFactory
    {
        public Shape GetShape(string shapeType)
        {
            shapeType = shapeType.ToUpper().Trim();

            if (shapeType.Equals("RECTANGLE"))
            {
                return new Rectangle();
            }
            else
            {
                System.ArgumentException argumentException = new System.ArgumentException($"Factory error: {shapeType} does not exist");
                throw argumentException;
            }
        }

        public Shape GetShape(string shapeType, Color color, int x, int y, int width, int height)
        {
            shapeType = shapeType.ToUpper().Trim();

            if (shapeType.Equals("RECTANGLE"))
            {
                return new Rectangle(color, x, y, width, height);
            }
            else
            {
                System.ArgumentException argumentException = new System.ArgumentException($"Factory error: {shapeType} does not exist");
                throw argumentException;
            }
        }
    }
}