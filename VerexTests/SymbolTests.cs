using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RegexBuilder;
using static RegexBuilder.Patterns.Symbols;
using static RegexBuilder.Patterns.MoreSymbols;

namespace RegexBuilderTests
{
    [TestClass]
    public class SymbolTests
    {
        [TestMethod]
        public void TestSymbols()
        {
            var s1 = LineFeed.Negate().Maybe();
            Assert.AreEqual(s1.Expression, @"[^\n]?");

            var s2 = CarriageReturn.OnceOrMore();
            Assert.AreEqual(s2.Expression, @"\r+");

            s1 = Backspace.Negate().Maybe();
            Assert.AreEqual(s1.Expression, @"[^\x08]?");

            s2 = Backspace.OnceOrMore();
            Assert.AreEqual(s2.Expression, @"\x08+");

            s2 = CarriageReturnLineFeed.NoneOrMany();
            Assert.AreEqual(s2.Expression, @"(?:\r\n)*");

            var p = (LineFeed | CarriageReturnLineFeed).AtLeast(2);
            Assert.AreEqual(p.Expression, @"(?:\n|\r\n){2,}");

            p = ("abc" + LineFeed | CarriageReturnLineFeed).AtLeast(2);
            Assert.AreEqual(p.Expression, @"(?:abc\n|\r\n){2,}");

            p = ("abc" + (LineFeed | CarriageReturnLineFeed)).Repeat(5);
            Assert.AreEqual(p.Expression, @"(?:abc(?:\n|\r\n)){5}");

            p = "Hi." + AnyChar;
            Assert.AreEqual(p.Expression, @"Hi\..");


            s2 = AnyWordChars[3,4];
            Assert.AreEqual(s2.Expression, @"(?:\w+){3,4}");

            s2 = (AnyWordChars | "; ") + EndOfLine;
            Assert.AreEqual(s2.Expression, @"(?:\w+|;\ )$");

            s2 = WhiteSpace  + AnyWordChars + "; " + EndOfLine;
            Assert.AreEqual(s2.Expression, @"\s\w+;\ $");

            s2 = AnyWordChars | s2;
            Assert.AreEqual(s2.Expression, @"\w+|\s\w+;\ $");

            s2 = s2 + AnyWordChars;
            Assert.AreEqual(s2.Expression, @"(?:\w+|\s\w+;\ $)\w+");

            s2 = s2 | AnyWordChars;
            Assert.AreEqual(s2.Expression, @"(?:\w+|\s\w+;\ $)\w+|\w+");

            s2 = (
                        (AnyWordChars
                           | (WhiteSpace + AnyWordChars + "; " + EndOfLine)
                        ) 
                        + AnyWordChars
                    ) 
                    | AnyWordChars;
            Assert.AreEqual(s2.Expression, @"(?:\w+|\s\w+;\ $)\w+|\w+");

            var c = AsciiChar('\t')[2];
            Assert.AreEqual(c.Expression, @"\x09{2}");

            c = AsciiChar(222)[2, 0];
            Assert.AreEqual(c.Expression, @"\xde{2,}");

            c = AsciiChar(0xaf)[0,0];
            Assert.AreEqual(c.Expression, @"\xaf*");
        }
    }
}
