using System;
using System.Collections.Generic;
using System.Text;

namespace RegexBuilder
{
    public static partial class Patterns
    {
        public class MoreSymbols
        {
            public static Symbol AnyChars => new Symbol(".+", false);
            public static Symbol ZeroOrMoreChars => new Symbol(".*", false);
            public static Symbol AnyWordChars => new Symbol(@"\w+", false);
            public static Symbol AnyWordCharsAtStart => new Symbol(@"\b\w+", false);
            public static Symbol AnyWordCharsAtEnd => new Symbol(@"\w+\b", false);
            public static Symbol ZeroOrMoreWordChars => new Symbol(@"\w*", false);
            public static Symbol ZeroOrMoreWordCharsAtStart => new Symbol(@"\b\w*", false);
            public static Symbol ZeroOrMoreWordCharsAtEnd => new Symbol(@"\w*\b", false);


            public static Pattern WholeWord(params Pattern[] patterns)
                => Symbols.WordEdge + AndPattern(patterns) + Symbols.WordEdge;

            public static Pattern WordStartsWith(params Pattern[] patterns)
                => Symbols.WordEdge + AndPattern(patterns);

            public static Pattern WordEndsWith(params Pattern[] patterns)
                => AndPattern(patterns) + Symbols.WordEdge;

            public static Pattern WordContains(params Pattern[] patterns)
                => MoreSymbols.ZeroOrMoreWordCharsAtStart + AndPattern(patterns) + MoreSymbols.ZeroOrMoreWordCharsAtEnd;

            public static Pattern WordCharsContain(params Pattern[] patterns)
                => MoreSymbols.ZeroOrMoreWordChars + AndPattern(patterns) + MoreSymbols.ZeroOrMoreWordChars;

            public static Pattern CharsContain(params Pattern[] patterns)
                => MoreSymbols.ZeroOrMoreChars + AndPattern(patterns) + MoreSymbols.ZeroOrMoreChars;

            public static Pattern WordOfLength(byte n)
                => Symbols.WordEdge + Symbols.AnyWordChar[n] + Symbols.WordEdge;

            public static Pattern WordStartOfLength(byte n)
                => Symbols.WordEdge + Symbols.AnyWordChar[n];

            public static Pattern WordEndOfLength(byte n)
                => Symbols.AnyWordChar[n] + Symbols.WordEdge;

        }
    }
}