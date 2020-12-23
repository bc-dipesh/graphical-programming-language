using graphical_programming_language;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace unit_tests
{
    [TestClass]
    public class LexerTest
    {
        /// <summary>
        /// Tests the Advance method.
        /// </summary>
        /// <remarks>
        /// Tests the core functionality of Lexer class.
        /// </remarks>
        [TestMethod]
        public void Advance()
        {
            string code = "width = 100";
            Lexer lexer = new Lexer();

            var expectedOutput = new List<Token> { { new Token(graphical_programming_language.Type.IDENTIFIER, "width") }, { new Token(graphical_programming_language.Type.OPERATOR, "=") }, { new Token(graphical_programming_language.Type.NUMBER, "100") } };
            var actualOutput = lexer.Advance(code);

            Assert.AreEqual(expectedOutput.Count, actualOutput.Count);

            for (int i = 0; i < 3; i++)
            {
                Assert.AreEqual(expectedOutput[i].type, actualOutput[i].type);
                Assert.AreEqual(expectedOutput[i].keyword, actualOutput[i].keyword);
            }

        }
    }
}
