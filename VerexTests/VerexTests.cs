using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ut = Microsoft.VisualStudio.TestTools.UnitTesting;
using RegexBuilder;
using static RegexBuilder.Patterns;
using static RegexBuilder.Patterns.Symbols;

namespace RegexBuilderTests
{
    [TestClass]
    public class VerexTests
    {
        [TestMethod]
        public void TestVerex()
        {
            ut.Assert.AreEqual(
                (AnyWordChar[2, 6] + 
                   AssertNextIs("s" + WordEdge)
                ).IsMatch("this is a test")
                , true);

            var p = (
                            CharClass('a', 'b', '-', '^', Escapes.Digit)
                            & ('0', '9') & ('j', 'r')
                        ) [5, RepeatMode.Lazy];

            ut.Assert.AreEqual(p.Expression, @"[ab\-\^\d0-9j-r]{5}?");
            
            p = MoreSymbols.WholeWord(("Get".AsItIs() | "Set") + Maybe("Value"));
            ut.Assert.AreEqual(p.Expression, @"\b(?:Get|Set)(?:Value)?\b");

            p = ("[".AsItIs() +Digit + "]").Repeat(3, 7);
            ut.Assert.AreEqual(p.Expression, @"(?:\[\d]){3,7}");

            var SequenceWithoutAngleBrakets = NoneOf('<', '>').NoneOrMany();
            var OpenAngle = NamedGroup("Open", "<");
            var CloseAngle = BalancingGroup("Open", "Close", ">");
            p = Symbols.StartOfLine
                            + SequenceWithoutAngleBrakets
                            + (
                                    (OpenAngle + SequenceWithoutAngleBrakets).OnceOrMore()
                                + (CloseAngle + SequenceWithoutAngleBrakets).OnceOrMore()
                             ).NoneOrMany()
                            + IfExists("Open").ThenMatch(NegativeLookAhead(""))
                            + EndOfLine;

            ut.Assert.AreEqual(p.Expression, @"^[^<>]*(?:(?:(?'Open'<)[^<>]*)+(?:(?'Close-Open'>)[^<>]*)+)*(?(Open)(?!))$");

            p = NamedGroup("Char", AnyChar)
                  + AssertNextIs(AnyChar.NoneOrMany(RepeatMode.Lazy))
                  + BackRef("Char");

            ut.Assert.AreEqual(p.Expression, @"(?'Char'.)(?=.*?)\k'Char'");

            p = Unicode.Arabic |
                            ((InRange(('A', 'Z')) - ('k', 'y') & AnyOf('a', 'z')) - Escapes.Unicode.Arrows)[5];

            ut.Assert.AreEqual(p.Expression, @"\p{IsArabic}|[azA-Z-[\p{IsArrows}k-y]]{5}");

            var ValidFirstChar = EnglishLetterOrHyphen;
            var ValidChars = EnglishLetterOrHyphenOrDigit & '.' & '-';

            var email = WordEdge
                 + ValidFirstChar
                 + ValidChars[0, 19]
                 + "@"
                 + ValidFirstChar
                 + ValidChars[2, 14]
                 + "."
                 + ValidFirstChar
                 + ValidChars[1, 9]
                 + WordEdge;

            ut.Assert.AreEqual(email.Expression, @"\b[_a-zA-Z][_.\-a-zA-Z0-9]{0,19}@[_a-zA-Z][_.\-a-zA-Z0-9]{2,14}\.[_a-zA-Z][_.\-a-zA-Z0-9]{1,9}\b");

            p = StartAfterLastMatch + "(" + Symbols.Digit + ")";
            ut.Assert.AreEqual(p.Expression, @"\G\(\d\)");

            p = Options.IgnoreCase().IgnorePatternWhitespace(false).Multiline(false).SingleLine()
                + StartAfterLastMatch
                + Comment("Test")
                + "(" + Symbols.Digit + ")"
                + XModeComments(
                    (Digit, "any digit"),
                    ("a", "this word")
                    )[2];

            ut.Assert.AreEqual(p.Expression, 
@"(?is-mx)\G(?#Test)\(\d\)(?x:    #X-Mode: Ignore white spaces.
\d    #any digit
a    #this word
){2}");

            p= AssertNextIs(
                            AssertNextIsNot("abc") + (
                                       (AssertPreviousIsNot("a") + AssertNextIsNot("bc")).Enclosed
                                   + (AssertPreviousIsNot("ab") + AssertNextIsNot("c")).Enclosed
                                ),
                                 Group(AnyChar.OnceOrMore(RepeatMode.Lazy)),
                                 "abc"
                               )
                              + BackRef(1);

            ut.Assert.AreEqual(p.Expression, 
                @"(?=(?!abc)(?:(?<!a)(?!bc))(?:(?<!ab)(?!c))(.+?)abc)\1");

            p = Text("a")
                  + ("b" + InRange(('t', 'v'))).Enclosed 
                  + AnyOf('a', 'b', 'c')
                  + ("x".AsItIs() + "c").Enclosed;

            ut.Assert.AreEqual(p.Expression, "a(?:b[t-v])[abc](?:xc)");

            var Name = (InRange(('A', 'Z'), ('a', 'z'), ('0', '9')) & AnyOf('-')).OnceOrMore();
            var url = WordEdge+
                           ("http" + "s".NoneOrOnce() + "://").NoneOrOnce() +
                           "www.".NoneOrOnce() +
                           Name + ("." + Name).NoneOrMany() +
                           "." + OptionsGroup(Text("com") | "org"| "net").IgnoreCase() +
                           WordEdge;
            ut.Assert.AreEqual(url.Expression, @"\b(?:https?://)?(?:www\.)?[\-A-Za-z0-9]+(?:\.[\-A-Za-z0-9]+)*\.(?i:com|org|net)\b");

            var validNo = (Maybe("1") + Digit[1, 2]) |
                                   ("2" + 
                                        (
                                            (InRange('0', '4') + Digit) | ("5" + InRange('0', '5'))
                                        )
                                   );

            var IP = WordEdge + validNo + ("." + validNo)[3] + WordEdge;
            ut.Assert.AreEqual(IP.Expression, @"\b(?:1?\d{1,2}|2(?:[0-4]\d|5[0-5]))(?:\.(?:1?\d{1,2}|2(?:[0-4]\d|5[0-5]))){3}\b");

            var delimit = AssertNextIsNot("abc" |
                         (AssertPreviousIs("a") + AssertNextIs("bc")) |
                         (AssertPreviousIs("ab") + AssertNextIs("c"))) +
                         ('a'.Negate() | ("a" + AssertNextIsNot("bc"))).OnceOrMore();

            ut.Assert.AreEqual(delimit.Expression, @"(?!abc|(?<=a)(?=bc)|(?<=ab)(?=c))(?:[^a]|a(?!bc))+");

            delimit = AssertPreviousIs("abc" | StartOfText) +
                               (
                                   NoneOf('a', '\r', '\n') | ("a" + AssertNextIsNot("bc"))
                               ).OnceOrMore();

            ut.Assert.AreEqual(delimit.Expression, @"(?<=abc|\A)(?:[^a\r\n]|a(?!bc))+");

           var repeatedWords = NamedGroup("wrd", WordEdge + MoreSymbols.AnyWordChars)
                + (WhiteSpace - AnyOf('\r', '\n')).OnceOrMore()
                + BackRef("wrd")
                + WordEdge;

            ut.Assert.AreEqual(repeatedWords.Expression, @"(?'wrd'\b\w+)[\s-[\r\n]]+\k'wrd'\b");

            var caps = InRange('A', 'Z');
            var Tags = "<" + Group(
                  caps + NoneOrMany(caps & Digit)
                  ) + ">" + LazyNoneOrMany(AnyChar) +
                  "</" + BackRef(1) + ">";

            ut.Assert.AreEqual(Tags.Expression, @"<([A-Z][\dA-Z]*)>.*?</\1>");

            p = Group(AnyChar) +
                  Group(BackRef(1) + AnyChar) +
                  BackRef(2);

            ut.Assert.AreEqual(p.Expression, @"(.)(\1.)\2");

             p = StartOfLine + (
                AnyChar.NoneOrMany(RepeatMode.Lazy) +
                ",").Repeat(3) +
                "P" + AnyChar.NoneOrMany();

            ut.Assert.AreEqual(p.Expression, @"^(?:.*?,){3}P.*");


            p = StartOfLine + Group(
               AnyChar.NoneOrMany(RepeatMode.Lazy) +
               ",").Repeat(3) +
               "P" + AnyChar.NoneOrMany();

            ut.Assert.AreEqual(p.Expression, @"^(.*?,){3}P.*");

            p = StartOfLine +
                AtomicGroup(
                    AtomicGroup(
                        LazyNoneOrMany(NoneOf(',', '\n', '\r')) +
                        ",")
                    .Repeat(11)
                ) +
               "P" + AnyChar.NoneOrMany();

            ut.Assert.AreEqual(p.Expression, @"^(?>(?>[^,\n\r]*?,){11})P.*");
        }
    }
}
