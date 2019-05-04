using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ut = Microsoft.VisualStudio.TestTools.UnitTesting;

using RegexBuilder;
using static RegexBuilder.Patterns;
using static RegexBuilder.Patterns.Symbols;

namespace RegexBuilderTests

{
    [TestClass]
    public class PatternsTest
    {
        [TestMethod]
        public void TestStatics()
        {
            ut.Assert.AreEqual(Text("abc", ".txt").Expression, @"abc\.txt");
            ut.Assert.AreEqual(Text("abc", ".txt")[1].Expression, @"(?:abc\.txt){1}");
            ut.Assert.AreEqual(Group("abc", ".txt").Expression, @"(abc\.txt)");
            ut.Assert.AreEqual(NamedGroup("group1", "abc", ".txt").AtLeast(3).Expression, @"(?'group1'abc\.txt){3,}");
            ut.Assert.AreEqual(CaptureGroup("group1", "abc", ".txt").NoneOrOnce(RepeatMode.Lazy).Expression, @"(?'group1'abc\.txt)??");
            var g = NamedGroup("group1", NewLine.OnceOrMore());
            ut.Assert.AreEqual((g + BackRef("group1")).Expression, @"(?'group1'(?:\n|\r\n)+)\k'group1'");
            ut.Assert.AreEqual((g + BackRef(1)).Expression, @"(?'group1'(?:\n|\r\n)+)\1");

            ut.Assert.AreEqual(AtomicGroup("abc", ".txt").NoneOrMany().Expression, @"(?>abc\.txt)*");
            ut.Assert.AreEqual(Greedy("abc", ".txt").Maybe().Expression, @"(?>abc\.txt)?");
            ut.Assert.AreEqual(NonBacktracking("abc", ".txt").OnceOrMore().Expression, @"(?>abc\.txt)+");
            ut.Assert.AreEqual(NoBacktrack("abc", ".txt").Expression, @"(?>abc\.txt)");

            ut.Assert.AreEqual(NonCapturingGroup("abc", ".txt").Expression, @"(?:abc\.txt)");
            ut.Assert.AreEqual(Enclose("abc", ".txt")[1].Expression, @"(?:abc\.txt){1}");

            ut.Assert.AreEqual(BalancingGroup("group1", "abc.txt").Expression, @"(?'-group1'abc\.txt)");
            ut.Assert.AreEqual(BalancingGroup("group1", "group2", "abc.txt").Expression, @"(?'group2-group1'abc\.txt)");


            ut.Assert.AreEqual(LookAhead("abc")[2, 7].Expression, "(?=abc)");
            ut.Assert.AreEqual(NegativeLookAhead("abc", "efg").Expression, "(?!abcefg)");
            ut.Assert.AreEqual(LookBehind("abc".Repeat(5)).Expression, "(?<=(?:abc){5})");
            ut.Assert.AreEqual(NegativeLookBehind("abc".AsItIs() + "d" | "f").Expression, "(?<!abcd|f)");

            ut.Assert.AreEqual(AssertNextIs("abc").Expression, "(?=abc)");
            ut.Assert.AreEqual(AssertNextIsNot("abc", "efg")[2, 7].Expression, "(?!abcefg)");
            ut.Assert.AreEqual(AssertPreviousIs("abc".Repeat(5)).Expression, "(?<=(?:abc){5})");
            ut.Assert.AreEqual(AssertPreviousIsNot("abc".AsItIs() + "d" | "f").Expression, "(?<!abcd|f)");

            var p = "q" + AssertNextIs("u" + Group(AnyChar)) +
                         AnyChar.Repeat(3) + BackRef(1);
            ut.Assert.AreEqual(p.Expression, @"q(?=u(.)).{3}\1");

            p = "qu" + Group(AnyChar) + AnyChar + BackRef(1);
            ut.Assert.AreEqual(p.Expression, @"qu(.).\1");

            p = "qu" + Group(AnyChar) + AnyChar + AssertNextIs(BackRef(1));
            ut.Assert.AreEqual(p.Expression, @"qu(.).(?=\1)");

            ut.Assert.AreEqual(Intersection(Unicode.Arabic, Unicode.Letter).Expression, @"(?=\p{IsArabic})\p{L}");
            ut.Assert.AreEqual(Difference(Unicode.Letter, Unicode.Arabic).Expression, @"(?!\p{IsArabic})\p{L}");

            var ag = IfExists("group1").ThenMatch("abc");
            ut.Assert.AreEqual(ag.Expression, "(?(group1)abc)");
            ag = ag.ElseMatch("x");
            ut.Assert.AreEqual(ag.Expression, "(?(group1)abc|x)");

            ag = Check(1).YesPattern("abc");
            ut.Assert.AreEqual(ag.Expression, "(?(1)abc)");
            ag = ag.NoPattern("x");
            ut.Assert.AreEqual(ag.Expression, "(?(1)abc|x)");

            ag = Assert(StartOfText).YesPattern("abc");
            ut.Assert.AreEqual(ag.Expression, @"(?(\A)abc)");
            ag = ag.NoPattern("x");
            ut.Assert.AreEqual(ag[2, 3].Expression, @"(?(\A)abc|x){2,3}");

            ut.Assert.AreEqual(OptionsGroup()[1].Expression, "");
            var op = new OptionsGroupPattern().IgnoreCase().Multiline(false);
            ut.Assert.AreEqual(op.Expression, "(?i-m)");

            p = OptionsGroup().SetAll(true).OnceOrMore();
            ut.Assert.AreEqual(p.Expression, "(?imnsx)");

            op = Options.SetAll(false);
            ut.Assert.AreEqual(op.Expression, "(?-imnsx)");

            op = Options.SetAll(null);
            ut.Assert.AreEqual(op.Expression, "");

            p = OptionsGroup("abc")[2, 3];
            ut.Assert.AreEqual(p.Expression, "(abc)");


            ut.Assert.AreEqual(Comment("Test").Expression, @"(?#Test)");

            var LineComments = WithLineComments(
                           ("abc", "string"),
                           (Unicode.Arabic, "Arabic letter"))[2];
            ut.Assert.AreEqual(LineComments.Expression, @"(?x:    #X-Mode: Ignore white spaces.
abc    #string
\p{IsArabic}    #Arabic letter
){2}");


            p = MoreSymbols.WholeWord(Text("cat") | "dog" | "mouse" | "fish");
            ut.Assert.AreEqual(p.Expression, @"\b(?:cat|dog|mouse|fish)\b");

            p = MoreSymbols.WholeWord((Text("cat") | "dog" | "mouse" | "fish")[2, 4]);
            ut.Assert.AreEqual(p.Expression, @"\b(?:cat|dog|mouse|fish){2,4}\b");


            p = MoreSymbols.WholeWord("ab" + ("c".AsItIs() | "d"));
            ut.Assert.AreEqual(p.Expression, @"\bab(?:c|d)\b");

            p = MoreSymbols.WholeWord(("ab" + ("c".AsItIs() | "d"))[2]);
            ut.Assert.AreEqual(p.Expression, @"\b(?:ab(?:c|d)){2}\b");

            p = MoreSymbols.WholeWord("Set" + Maybe("Value"));
            ut.Assert.AreEqual(p.Expression, @"\bSet(?:Value)?\b");

            p = MoreSymbols.AnyWordCharsAtStart + AssertPreviousIsNot("s") + WordEdge;
            ut.Assert.AreEqual(p.Expression, @"\b\w+(?<!s)\b");

            p = WordEdge + LazyNoneOrMany(AnyWordChar) +
                  NoneOf('s', Escapes.NonWordChar) + WordEdge;
            ut.Assert.AreEqual(p.Expression, @"\b\w*?[^s\W]\b");

            p = WordEdge + (
                               ("cat" + AnyWordChar[3]) |
                               (AnyWordChar + "cat" + AnyWordChar[2]) |
                               (AnyWordChar[2] + "cat" + AnyWordChar) |
                               (AnyWordChar[3] + "cat")
                          ) + WordEdge;

            ut.Assert.AreEqual(p.Expression, @"\b(?:cat\w{3}|\wcat\w{2}|\w{2}cat\w|\w{3}cat)\b");

            p = AssertNextIs(MoreSymbols.WordOfLength(6)) + MoreSymbols.WordContains("cat");
            ut.Assert.AreEqual(p.Expression, @"(?=\b\w{6}\b)\b\w*cat\w*\b");

            p = AssertNextIs(MoreSymbols.WordOfLength(6)) + MoreSymbols.WordCharsContain("cat");
            ut.Assert.AreEqual(p.Expression, @"(?=\b\w{6}\b)\w*cat\w*");

            p = AssertNextIs(MoreSymbols.WordOfLength(6)) + MoreSymbols.CharsContain("cat");
            ut.Assert.AreEqual(p.Expression, @"(?=\b\w{6}\b).*cat.*");

            p = AssertNextIs(MoreSymbols.WordOfLength(6)) + AnyChar[0, 3] + "cat" + MoreSymbols.ZeroOrMoreChars;
            ut.Assert.AreEqual(p.Expression, @"(?=\b\w{6}\b).{0,3}cat.*");

            p = AssertNextIs(MoreSymbols.WordOfLength(6)) + AnyWordChar[0, 3] + "cat" + MoreSymbols.ZeroOrMoreWordChars;
            ut.Assert.AreEqual(p.Expression, @"(?=\b\w{6}\b)\w{0,3}cat\w*");

            p = WordEdge + AssertNextIs(MoreSymbols.WordEndOfLength(6)) +
                AnyWordChar[0, 3] + "cat" + MoreSymbols.ZeroOrMoreWordChars;

            ut.Assert.AreEqual(p.Expression, @"\b(?=\w{6}\b)\w{0,3}cat\w*");


            p = WordEdge + AssertNextIs(AnyWordChar[6, 12] + WordEdge) +
                    AnyChar[0, 9] + (Text("cat") | "dog" | "mouse") + MoreSymbols.ZeroOrMoreWordChars;

            ut.Assert.AreEqual(p.Expression, @"\b(?=\w{6,12}\b).{0,9}(?:cat|dog|mouse)\w*");


            p = AssertNextIs("<" + AnyChar.AtLeast(3) + ">") +
                         AnyChar.NoneOrMany() + "cat" +
                         AnyChar.NoneOrMany();

            ut.Assert.AreEqual(p.Expression, @"(?=<.{3,}>).*cat.*");

            p = AssertPreviousIs("<" + MoreSymbols.ZeroOrMoreWordChars) +
                         "cat" +
                         AssertNextIs(MoreSymbols.ZeroOrMoreWordChars + ">");

            ut.Assert.AreEqual(p.Expression, @"(?<=<\w*)cat(?=\w*>)");

            p = AssertPreviousIs("<") +
                        MoreSymbols.WordCharsContain("cat") +
                        AssertNextIs(">");

            ut.Assert.AreEqual(p.Expression, @"(?<=<)\w*cat\w*(?=>)");


            var B4Cat = WordEdge +
                                AssertNextIs(AnyWordChar.Repeat(6) + WordEdge) +
                                AnyChar[0, 3];
            var AfterCat = MoreSymbols.ZeroOrMoreWordChars;
            p = AssertPreviousIs("<" + B4Cat) +
                     "cat" +
                     AssertNextIs(AfterCat + ">");

            ut.Assert.AreEqual(p.Expression, @"(?<=<\b(?=\w{6}\b).{0,3})cat(?=\w*>)");

            p = (AssertNextIs(StartOfLine) + "a") |
                  (AssertNextIsNot(StartOfLine) + "b");

            ut.Assert.AreEqual(p.Expression, @"(?=^)a|(?!^)b");

            p = IfExists(
                      AssertNextIs(AnyWordChar.Repeat(6) + WordEdge)
                  ).ThenMatch("a").ElseMatch(NonWordChar);

            ut.Assert.AreEqual(p.Expression, @"(?(?=\w{6}\b)a|\W)");

            p = IfExists(
                        Group(AnyWordChar.Repeat(6) + WordEdge)
                   ).ThenMatch("a").ElseMatch(NonWordChar);

            ut.Assert.AreEqual(p.Expression, @"(?((\w{6}\b))a|\W)");

            p = IfExists(
                        Enclose(AnyWordChar.Repeat(6) + WordEdge)
                   ).ThenMatch("a").ElseMatch(NonWordChar);

            ut.Assert.AreEqual(p.Expression, @"(?(\w{6}\b)a|\W)");

        }


        [TestMethod]
        public void TestStaticClasses()
        {
            ut.Assert.AreEqual(CharClass('a', 'b', 'c')[2,5].Expression, "[abc]{2,5}");
            ut.Assert.AreEqual(CharClass(('a', 'z'), ('A', 'Z'))[2, 5].Expression, "[a-zA-Z]{2,5}");
            ut.Assert.AreEqual(NegativeCharClass('a', 'b', 'c')[2,5].Expression, "[^abc]{2,5}");
            ut.Assert.AreEqual(NegativeCharClass(('a', 'z'), ('A', 'Z'))[2, 5].Expression, "[^a-zA-Z]{2,5}");
            ut.Assert.AreEqual(AnyOf('a', 'b', 'c')[2, 5].Expression, "[abc]{2,5}");
            ut.Assert.AreEqual(InRange(('a', 'z'), ('A', 'Z'))[2, 5].Expression, "[a-zA-Z]{2,5}");
            ut.Assert.AreEqual(NoneOf('a', 'b', 'c')[2, 5].Expression, "[^abc]{2,5}");
            ut.Assert.AreEqual(OutOfRange(('a', 'z'), ('A', 'Z'))[2, 5].Expression, "[^a-zA-Z]{2,5}");

            ut.Assert.AreEqual(EnglishLetter.Expression, "[a-zA-Z]");
            ut.Assert.AreEqual(NonEnglish_Letter.Expression, @"[^\W\d_a-zA-Z]");
            ut.Assert.AreEqual(EnglishLetterOrHyphen.Expression, "[_a-zA-Z]");
            ut.Assert.AreEqual(EnglishLetterOrHyphenOrDigit.NoneOrOnce().Expression, "[_a-zA-Z0-9]?");
            ut.Assert.AreEqual(EnglishLetterOrDigit.NoneOrOnce(RepeatMode.Lazy).Expression, "[a-zA-Z0-9]??");

            ut.Assert.AreEqual(ArabicLetter.NoneOrMany(RepeatMode.Lazy).Expression, "[لىيء-ضط-غف-كم-و]*?");
            ut.Assert.AreEqual(NonArabic_Letter.NoneOrMany(RepeatMode.Lazy).Expression, @"[^لىي\W\d_ء-ضط-غف-كم-و]*?");

            ut.Assert.AreEqual(ArabicLetterOrADigit[2, RepeatMode.Lazy].Expression, @"[لىي\dء-ضط-غف-كم-و]{2}?");

            ut.Assert.AreEqual(NonLetter[2, 4, RepeatMode.Lazy].Expression, @"[\W_0-9]{2,4}?");

            ut.Assert.AreEqual(While("a").Expression, "a+");

            ut.Assert.AreEqual(While(Digit).Expression, @"\d+");

            ut.Assert.AreEqual(While(CarriageReturnLineFeed).Expression, @"(?:\r\n)+");

            ut.Assert.AreEqual(Until('a').Expression, "[^a]+");

            var p = Digit + Comment("a digit") +
                       AnyWordChar.OnceOrMore() + Comment("a word");

            ut.Assert.AreEqual(p.Expression, @"\d(?#a digit)\w+(?#a word)");

        }
    }
}
