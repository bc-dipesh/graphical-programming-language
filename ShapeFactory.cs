using System;

namespace graphical_programming_language
{
    internal class ShapeFactory
    {
        public Shape getShape(String shapeType)
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
    }
}