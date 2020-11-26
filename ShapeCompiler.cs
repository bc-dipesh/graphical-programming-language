using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace graphical_programming_language
{
    internal class ShapeCompiler : IShapeCompiler
    {
        private ShapeFactory shapeFactory;
        private string command;
        private string[] arguments;
        private Regex splitOnSpaces;

        public ShapeCompiler()
        {
            shapeFactory = new ShapeFactory();
            arguments = new string[3];
            splitOnSpaces = new Regex(@"\s+", RegexOptions.Compiled);
        }

        public List<string> Compile(string code)
        {
            string[] data = splitOnSpaces.Split(code);

            command = data[0];
            Array.Copy(data, 1, arguments, 0, data.Length - 1);

            return new List<string>();
        }

        public Shape Run()
        {
            Shape shape = null;
            if (command.ToUpper().Equals("DRAW"))
            {
                if (arguments.Length > 1)
                {
                    shape = shapeFactory.getShape(arguments[0]);
                }
            }

            return shape;
        }
    }
}