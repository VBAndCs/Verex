using System;

namespace RegexBuilder
{
    public static partial class Patterns
    {
        public static TextPattern Text(params string[] texts) => new TextPattern(texts);

        internal static Pattern AndPattern(params Pattern[] patterns)
        {
            if (patterns == null || patterns.Length == 0)
                throw new ArgumentNullException("patterns");

            if (patterns.Length == 1)
                return patterns[0];

            return new AndPattern(patterns);
        }

        public static GroupPattern Group(params Pattern[] patterns) 
            => new GroupPattern(patterns);

        //public static CaptureGroup Group(string groupName, params Pattern[] patterns) => new CaptureGroup(groupName, patterns);

        public static AtomicGroupPattern AtomicGroup(params Pattern[] patterns) => new AtomicGroupPattern(patterns);
        public static AtomicGroupPattern Greedy(params Pattern[] patterns) => new AtomicGroupPattern(patterns);
        public static AtomicGroupPattern NonBacktracking(params Pattern[] patterns) => new AtomicGroupPattern(patterns);
        public static AtomicGroupPattern NoBacktrack(params Pattern[] patterns) => new AtomicGroupPattern(patterns);


        public static NonCapturingGroupPattern NonCapturingGroup(params Pattern[] patterns) 
            => new NonCapturingGroupPattern(patterns);

        public static NonCapturingGroupPattern Enclose(params Pattern[] patterns) 
            => new NonCapturingGroupPattern(patterns);

        public static NonCapturingGroupPattern NewLine => NonCapturingGroup(new Symbol(@"\n|\r\n"));

        public static CaptureGroupPattern CaptureGroup(string groupName, params Pattern[] patterns) 
            => new CaptureGroupPattern(groupName, patterns);

        public static CaptureGroupPattern NamedGroup(string groupName, params Pattern[] patterns) 
            => new CaptureGroupPattern(groupName, patterns);


        public static BackReference BackRef(string groupName) => new BackReference(groupName);

        public static BackReference BackRef(ushort groupNo) => new BackReference(groupNo);

        public static BalancingGroupPattern BalancingGroup(string openGroupName, Pattern pattern) 
            => new BalancingGroupPattern(openGroupName, pattern);

        public static Pattern BalancedBrackets(char openbracket, char closebracket)
        {            
            var SequenceWithoutBrakets = NoneOf (openbracket, closebracket).ZeroOrMore();

            var Open = NamedGroup("Open", Text(openbracket.ToString()));
            var Close = BalancingGroup("Open", Text(closebracket.ToString()));
            var p = Symbols.StartOfLine
                            + SequenceWithoutBrakets
                            + (
                                    (Open + SequenceWithoutBrakets).OnceOrMore()
                                + (Close + SequenceWithoutBrakets).OnceOrMore()
                             ).NoneOrMany()
                            + IfExists("Open").ThenMatch(NegativeLookAhead(""))
                            + Symbols.EndOfLine;

            return p;
        }


        public static BalancingGroupPattern BalancingGroup(string openGroupName, string closeGroupName, Pattern pattern)
            => new BalancingGroupPattern((openGroupName, closeGroupName), pattern);

        public static Pattern BalancedBracketsContent(char openbracket, char closebracket)
        {
            var SequenceWithoutBrakets = NoneOf(openbracket, closebracket).ZeroOrMore();

            var Open = NamedGroup("Open", Text(openbracket.ToString()));
            var Close = BalancingGroup("Open", "Close", Text(closebracket.ToString()));
            var p = Symbols.StartOfLine
                            + SequenceWithoutBrakets
                            + (
                                    (Open + SequenceWithoutBrakets).OnceOrMore()
                                + (Close + SequenceWithoutBrakets).OnceOrMore()
                             ).NoneOrMany()
                            + IfExists("Open").ThenMatch(NegativeLookAhead(""))
                            + Symbols.EndOfLine;

            return p;
        }



        public static Pattern LookAhead(params Pattern[] patterns) 
            => new PositiveLookAheadPattern(patterns);

        public static Pattern AssertNextIs(params Pattern[] patterns) 
            => new PositiveLookAheadPattern(patterns);

        public static Pattern Intersection(Pattern pattern1, Pattern pattern2)
            => AssertNextIs(pattern1) + pattern2;

        public static Pattern NegativeLookAhead(params Pattern[] patterns) 
            => new NegativeLookAheadPattern(patterns);

        public static Pattern AssertNextIsNot(params Pattern[] patterns) 
            => new NegativeLookAheadPattern(patterns);

        public static Pattern Difference(Pattern pattern1, Pattern pattern2)
           => AssertNextIsNot(pattern2) + pattern1;

        public static Pattern LookBehind(params Pattern[] patterns) 
            => new PositiveLookBehindPattern(patterns);

        public static Pattern AssertPreviousIs(params Pattern[] patterns) 
            => new PositiveLookBehindPattern(patterns);

        public static Pattern NegativeLookBehind(params Pattern[] patterns) 
            => new NegativeLookBehindPattern(patterns);

        public static Pattern AssertPreviousIsNot(params Pattern[] patterns) 
            => new NegativeLookBehindPattern(patterns);

        public static AssertionPattern IfExists(string groupName) => new AssertionPattern(groupName);
        public static AssertionPattern IfExists(ushort groupNo) => new AssertionPattern(groupNo);
        public static AssertionPattern IfExists(params Pattern[] ifExprs) => new AssertionPattern(ifExprs);
        public static AssertionPattern Check(string groupName) => new AssertionPattern(groupName);
        public static AssertionPattern Check(ushort groupNo) => new AssertionPattern(groupNo);
        public static AssertionPattern Check(params Pattern[] ifExprs) => new AssertionPattern(ifExprs);
        public static AssertionPattern Assert(string groupName) => new AssertionPattern(groupName);
        public static AssertionPattern Assert(ushort groupNo) => new AssertionPattern(groupNo);
        public static AssertionPattern Assert(params Pattern[] ifExprs) => new AssertionPattern(ifExprs);

        public static OptionsGroupPattern OptionsGroup(params Pattern[] patterns)
        {
            if (patterns == null || patterns.Length == 0)
                return new OptionsGroupPattern();

            return  new OptionsGroupPattern(patterns);
        }
        public static OptionsGroupPattern OptionsGroup() =>  new OptionsGroupPattern();
        
        public static OptionsGroupPattern Options => new OptionsGroupPattern();

        public static CommentPattern Comment(string comment) 
            => new CommentPattern(comment);

        public static XModeCommentPattern XModeComments(params (Pattern Pattern, string Comment)[] patternLines) 
            => new XModeCommentPattern(patternLines);

        public static XModeCommentPattern WithLineComments(params (Pattern Pattern, string Comment)[] patternLines) 
            => new XModeCommentPattern(patternLines);

        // Char Classes
        public static CharClassPattern CharClass(params CharOrEscape[] chars) => new CharClassPattern(false,chars);
        public static CharClassPattern CharClass(params (CharOrEscape from, CharOrEscape to)[] ranges) => new CharClassPattern(false,ranges);
        public static CharClassPattern NegativeCharClass(params CharOrEscape[] chars) => new CharClassPattern(true, chars);
        public static CharClassPattern NegativeCharClass(params (CharOrEscape from, CharOrEscape to)[] ranges) => new CharClassPattern(true, ranges);
        public static CharClassPattern AnyOf(params CharOrEscape[] chars) => new CharClassPattern(false,chars);
        public static CharClassPattern Not(CharOrEscape c) => new CharClassPattern(true, c);
        public static CharClassPattern NoneOf(params CharOrEscape[] chars) => new CharClassPattern(true, chars);
        public static CharClassPattern InRange(CharOrEscape from, CharOrEscape to) => new CharClassPattern(false, (from, to));
        public static CharClassPattern InRange(params (CharOrEscape from, CharOrEscape to)[] ranges) => new CharClassPattern(false, ranges);
        public static CharClassPattern OutOfRange(CharOrEscape from, CharOrEscape to) => new CharClassPattern(true, (from, to));
        public static CharClassPattern OutOfRange(params (CharOrEscape from, CharOrEscape to)[] ranges) => new CharClassPattern(true, ranges);
        public static CharClassPattern EnglishLetter => new CharClassPattern(false, ('a', 'z'), ('A', 'Z'));
        public static CharClassPattern NonEnglish_Letter => new CharClassPattern(true, Escapes.NonWordChar, Escapes.Digit, '_').AddRanges(('a', 'z'), ('A', 'Z'));
        public static CharClassPattern EnglishLetterOrHyphen => new CharClassPattern(false,'_', ('a', 'z'), ('A', 'Z'));
        public static CharClassPattern EnglishLetterOrHyphenOrDigit => new CharClassPattern(false,'_', ('a', 'z'), ('A', 'Z'), ('0', '9'));
        public static CharClassPattern EnglishLetterOrDigit => new CharClassPattern(false,('a', 'z'), ('A', 'Z'), ('0', '9'));
        public static CharClassPattern ArabicLetter => new CharClassPattern(false,'ل', 'ى', 'ي').AddRanges(('ء', 'ض'), ('ط', 'غ'), ('ف', 'ك'), ('م', 'و'));
        public static CharClassPattern NonArabic_Letter => ArabicLetter.Negate().AddChars(Escapes.NonWordChar, Escapes.Digit, '_');
        public static CharClassPattern ArabicLetterOrADigit => ArabicLetter.AddChars(Escapes.Digit);
        public static CharClassPattern ArabicLetterOrTashkeelOrKasheda => ArabicLetter.AddChars('َ', 'ً', 'ُ', 'ٌ', 'ِ', 'ٍ', 'ّ', 'ْ', 'ـ') ;
        public static CharClassPattern Arabic => ArabicLetterOrTashkeelOrKasheda.AddChars(Escapes.Digit);
        public static CharClassPattern ArabicTashkeel => new CharClassPattern(false,'َ', 'ً', 'ُ', 'ٌ', 'ِ', 'ٍ', 'ّ', 'ْ');
        public static CharClassPattern ArabicTashkeelOrKasheda => ArabicTashkeel &  'ـ';
        public static CharClassPattern LowerCaseEnglishLetter => new CharClassPattern(false,('a', 'z'));
        public static CharClassPattern UpperCaseEnglishLetter => new CharClassPattern(false,('A', 'Z'));
        public static CharClassPattern Letter => NonLetter.Negate();
        public static CharClassPattern NonLetter => new CharClassPattern(false, ('0', '9'), Escapes.NonWordChar, '_') ;
        public static CharClassPattern LetterOrHyphen => new CharClassPattern(true, ('0', '9'), Escapes.NonWordChar);

        public static Pattern While(TextPattern s) => s.OnceOrMore();

        public static Pattern While(Symbol s) => s.OnceOrMore();

        public static Pattern Until(CharOrEscape c) => new CharClassPattern(true, c).OnceOrMore();

        public static Pattern Maybe(params Pattern[] patterns) => AndPattern(patterns).NoneOrOnce();
        public static Pattern NoneOrOnce(params Pattern[] patterns) => AndPattern(patterns).NoneOrOnce();
        public static Pattern NoneOrMany(params Pattern[] patterns) => AndPattern(patterns).NoneOrMany();
        public static Pattern ZeroOrMore(params Pattern[] patterns) => AndPattern(patterns).NoneOrMany();
        public static Pattern OnceOrMore(params Pattern[] patterns) => AndPattern(patterns).OnceOrMore();
        public static Pattern AtLeast(ushort times, params Pattern[] patterns) => AndPattern(patterns).AtLeast(times);
        public static Pattern Repeat(ushort times, params Pattern[] patterns) => AndPattern(patterns).Repeat(times);
        public static Pattern Repeat(ushort min, ushort max, params Pattern[] patterns) => AndPattern(patterns).Repeat(min, max);

        public static Pattern LazyMaybe(params Pattern[] patterns) => AndPattern(patterns).NoneOrOnce(RepeatMode.Lazy);
        public static Pattern LazyNoneOrOnce(params Pattern[] patterns) => AndPattern(patterns).NoneOrOnce(RepeatMode.Lazy);
        public static Pattern LazyNoneOrMany(params Pattern[] patterns) => AndPattern(patterns).NoneOrMany(RepeatMode.Lazy);
        public static Pattern LazyZeroOrMore(params Pattern[] patterns) => AndPattern(patterns).NoneOrMany(RepeatMode.Lazy);
        public static Pattern LazyOnceOrMore(params Pattern[] patterns) => AndPattern(patterns).OnceOrMore(RepeatMode.Lazy);
        public static Pattern LazyAtLeast(ushort times, params Pattern[] patterns) => AndPattern(patterns).AtLeast(times, RepeatMode.Lazy);
        public static Pattern LazyRepeat(ushort times, params Pattern[] patterns) => AndPattern(patterns).Repeat(times, RepeatMode.Lazy);
        public static Pattern LazyRepeat(ushort min, ushort max, params Pattern[] patterns) => AndPattern(patterns).Repeat(min, max, RepeatMode.Lazy);

    }
}
