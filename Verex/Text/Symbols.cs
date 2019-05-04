using System;

namespace RegexBuilder
{
    public static partial class Patterns
    {
        public static class Symbols
        {
            public static Symbol Quote = new Symbol("\"");
            public static Symbol LineFeed = new Symbol(@"\n");
            public static Symbol CarriageReturn = new Symbol(@"\r");
            public static Symbol CarriageReturnLineFeed = new Symbol(@"\r\n", false);
            public static Symbol Digit = new Symbol(@"\d");
            public static Symbol NonDigit = new Symbol(@"\D");
            public static Symbol AnyChar => new Symbol(".");
            public static Symbol AnyWordChar = new Symbol(@"\w");
            public static Symbol AnyWord => new Symbol(@"\b\w+\b", false);
            public static Symbol NonWordChar = new Symbol(@"\W");
            public static Symbol Bell = new Symbol(@"\a");
            public static Symbol Alarm = new Symbol(@"\a");
            public static Symbol Backspace = new Symbol(@"\x08", encoded: true);
            public static Symbol Tab = new Symbol(@"\t");
            public static Symbol VerticalTab = new Symbol(@"\v");
            public static Symbol FormFeed = new Symbol("@\f");
            public static Symbol WhiteSpace = new Symbol(@"\s");
            public static Symbol NonWhiteSpace = new Symbol(@"\S");
            public static Symbol Escape = new Symbol(@"\e");
            public static Symbol StartOfLine => new Symbol("^", false);
            public static Symbol EndOfLine => new Symbol("$", false);
            public static Symbol StartOfText => new Symbol(@"\A", false);
            public static Symbol EndOfText => new Symbol(@"\z", false);
            public static Symbol EndOfLastNonEmptyLine => new Symbol(@"\Z", false);
            public static Symbol WordEdge => new Symbol(@"\b", false);
            public static Symbol NonWordEdge => new Symbol(@"\B", false);
            public static Symbol ContiguousMatch => new Symbol(@"\G", false);
            public static Symbol StartAfterLastMatch => new Symbol(@"\G", false);

            public static Symbol AsciiChar(byte asciiCode)
            {
                var x = @"\x" + Convert.ToString(asciiCode, 16).PadLeft(2, '0');
                return new Symbol(x, encoded: true);
            }

            /// <summary>
            /// Use this method to convert a char to its regex ASCII representation.
            /// This solves some problems with some matching chars, i.e 
            /// when writing ] in a char class.
            /// </summary>
            /// <param name="c"></param>
            /// <returns></returns>
            public static Symbol AsciiChar(char c)
            {
                if ((int)c > 255)
                    throw new ArgumentException("this is not an ASCII char");

                var x = @"\x" + Convert.ToString(c, 16).PadLeft(2, '0');
                return new Symbol(x, encoded: true);
            }

            public static Symbol ControlChar(char c)
                  => new Symbol(@"\c" + char.ToUpper(c));            
        }
    }
}
