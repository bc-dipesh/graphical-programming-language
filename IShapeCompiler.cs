using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace graphical_programming_language
{
    internal interface IShapeCompiler
    {
        List<string> Compile(string code);

        Shape Run();

    }
}
