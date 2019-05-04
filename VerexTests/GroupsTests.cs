using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RegexBuilder;

namespace RegexBuilderTests
{
    [TestClass]
    public class GroupsTests
    {
        [TestMethod]
        public void TestGroups()
        {
            Assert.AreEqual(new GroupPattern("abc").Expression, "(abc)");

            Assert.AreEqual(new AtomicGroupPattern("abc").Expression, "(?>abc)");
            Assert.AreEqual(new AtomicGroupPattern("abc"|Patterns.Symbols.AnyWordChar).Expression, @"(?>abc|\w)");

            Assert.AreEqual(new BackReference("abc").Expression, @"\k'abc'");
            Assert.AreEqual(new BackReference(1).Expression, @"\1");

            Assert.AreEqual(new CaptureGroupPattern("group1", "abc").Expression, "(?'group1'abc)");

            Assert.AreEqual(new CommentPattern("abc").Expression, "(?#abc)");

            Assert.AreEqual(new NonCapturingGroupPattern("abc").Expression, "(?:abc)");

            var LineComments = new XModeCommentPattern(
                ("abc", "string"),
                (Patterns.Unicode.Arabic, "Arabic letter")
                );
            Assert.AreEqual(LineComments.Expression, @"(?x:    #X-Mode: Ignore white spaces.
abc    #string
\p{IsArabic}    #Arabic letter
)");
        }

        [TestMethod]
        public void TestLookArrounds()
        {
            Assert.AreEqual(new PositiveLookAheadPattern("abc").Expression, "(?=abc)");
            Assert.AreEqual(new NegativeLookAheadPattern("abc").Expression, "(?!abc)");
            Assert.AreEqual(new PositiveLookBehindPattern("abc").Expression, "(?<=abc)");
            Assert.AreEqual(new NegativeLookBehindPattern("abc").Expression, "(?<!abc)");
        }

        [TestMethod]
        public void TestAssertionGroups()
        {
            var ag = new AssertionPattern("group1").YesPattern("abc");
            Assert.AreEqual(ag.Expression, "(?(group1)abc)");
            ag = ag.NoPattern("x");
            Assert.AreEqual(ag.Expression, "(?(group1)abc|x)");

            ag = new AssertionPattern(1).YesPattern("abc");
            Assert.AreEqual(ag.Expression, "(?(1)abc)");
            ag = ag.NoPattern("x");
            Assert.AreEqual(ag.Expression, "(?(1)abc|x)");

            ag = new AssertionPattern(Patterns.Symbols.StartOfText).YesPattern("abc");
            Assert.AreEqual(ag.Expression, @"(?(\A)abc)");
            ag = ag.NoPattern("x");
            Assert.AreEqual(ag.Expression, @"(?(\A)abc|x)");
        }

        [TestMethod]
        public void TestOptionGroup()
        {
            Assert.AreEqual(new OptionsGroupPattern().Expression, "");
            var op = new OptionsGroupPattern().IgnoreCase().Multiline(false);
            Assert.AreEqual(op.Expression, "(?i-m)");

            op = new OptionsGroupPattern().SetAll(true);
            Assert.AreEqual(op.Expression, "(?imnsx)");

            op = new OptionsGroupPattern("").SetAll(false);
            Assert.AreEqual(op.Expression, "(?-imnsx)");

            op = new OptionsGroupPattern("abc");
            Assert.AreEqual(op.Expression, "(abc)");

            op = new OptionsGroupPattern("abc").ExplicitCapture(false).SingleLine();
            Assert.AreEqual(op.Expression, "(?s-n:abc)");

            op = new OptionsGroupPattern("abc").SetAll(true);
            Assert.AreEqual(op.Expression, "(?imnsx:abc)");

            op = new OptionsGroupPattern("abc").SetAll(false);
            Assert.AreEqual(op.Expression, "(?-imnsx:abc)");
        }


        [TestMethod]
        public void TestBalancingGroup()
        {

            Assert.ThrowsException<ArgumentNullException>(() => new BalancingGroupPattern(("group1", "group2")));
            Assert.AreEqual(new BalancingGroupPattern("group1", "abc".AsItIs()).Expression, "(?'-group1'abc)");
            Assert.AreEqual(new BalancingGroupPattern(("group1", "group2"), "abc").Expression, "(?'group2-group1'abc)");
            Assert.AreEqual(Patterns.BalancedBracketsContent('[', ']').Expression, @"^[^]\[]*(?:(?:(?'Open'\[)[^]\[]*)+(?:(?'Close-Open'])[^]\[]*)+)*(?(Open)(?!))$");

            Assert.AreEqual(Verex.ContainsBalancedBrackets(
               "(2 + 1) * (3 + 2) - ( 1 + 4)",
               "(", ")"), true);

            Assert.AreEqual(Verex.ContainsBalancedBrackets(
               "(2 + 1) * ((3 + 2) - ( 1 + 4)",
               "(", ")"), false);

            Assert.AreEqual(Verex.ContainsBalancedBrackets(
                "=>=>x + 2<= * =>4 + 3<=<= / 2", "=>", "<="), true);

            Assert.AreEqual(Verex.ContainsBalancedBrackets(
                "=>=>x + 2<= * =>4 + 3<= / 2", "=>", "<="), false);

            string input = "<!--<!--x+2> * <!--4+3>>/2";
            var x = Verex.BalancedContents(input, "<!--", ">");          

            Assert.AreEqual(Join(x), @"<!--x+2> * <!--4+3>, x+2, 4+3, ");

            input = "<!--<!--x+2=> * <!--4+3=>=>/2";
            x = Verex.BalancedContents(input, "<!--", "=>");

            Assert.AreEqual(Join(x), @"<!--x+2=> * <!--4+3=>, x+2, 4+3, ");

            input = " 'y' 'x+2' * '4+3'/2";
            x = Verex.BalancedContents(input, "'", "'");

            Assert.AreEqual(Join(x), @"y' 'x+2' * '4+3,  'x+2' * , x+2, ");

            input = "--y-- --x+2-- * --4+3--/2";
            x = Verex.BalancedContents(input, "--", "--");

            Assert.AreEqual(Join(x), @"y-- --x+2-- * --4+3,  --x+2-- * , x+2, ");

        }

        private static string Join(System.Collections.Generic.List<Content> x)
        {
            var t = "";
            foreach (Content cap in x)
                t += cap.Value + ", ";
            return t;
        }
    }
}
