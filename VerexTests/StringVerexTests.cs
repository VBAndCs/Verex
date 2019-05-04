using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RegexBuilder;

namespace RegexBuilderTests
{
    [TestClass]
    public class StringVerexTests
    {
        [TestMethod]
        public void TestStringVerex()
        {
            Assert.AreEqual("abc".AsItIs().Expression, "abc");
            Assert.AreEqual("abc".AsWholeWord().Expression, @"\babc\b");
            Assert.AreEqual("abc".AsWordStart().Expression, @"\babc");
            Assert.AreEqual("abc".AsWordEnd().Expression, @"abc\b");
            Assert.AreEqual("abc".AsWholeLine().Expression, "^abc$");
            Assert.AreEqual("abc".AsLineStart().Expression, "^abc");
            Assert.AreEqual("abc".AsLineEnd().Expression, "abc$");
            Assert.AreEqual("abc".AsTextStart().Expression, @"\Aabc");
            Assert.AreEqual("abc".AsTextEnd().Expression, @"abc\z");
            Assert.AreEqual("abc".AsEndOfLastNonEmptyLine().Expression, @"abc\Z");
            Assert.AreEqual("abc".Repeat(5, 10, RepeatMode.Lazy).Expression, "(?:abc){5,10}?");
            Assert.AreEqual("abc".Repeat(2).Expression, "(?:abc){2}");
            Assert.AreEqual("abc".AtLeast(2).Expression, "(?:abc){2,}");
            Assert.AreEqual("abc".OnceOrMore(RepeatMode.Lazy).Expression, "(?:abc)+?");
            Assert.AreEqual("abc".NoneOrMany().Expression, "(?:abc)*");
            Assert.AreEqual("abc".NoneOrOnce(RepeatMode.Lazy).Expression, "(?:abc)??");
            Assert.AreEqual("abc".Maybe().Expression, "(?:abc)?");
            Assert.AreEqual("a,b,c".AnyOfChars(",").Expression, "[abc]");
            Assert.AreEqual("abc".AnyOfChars(",").Expression, "[abc]");
            Assert.AreEqual("abc".AnyOfChars().Expression, "[abc]");
            Assert.AreEqual("abc".NoneOfChars().Expression, "[^abc]");
            Assert.AreEqual("abc".NoneOfChars(", ").Expression, "[^abc]");
            Assert.AreEqual("a, b, c".NoneOfChars(", ").Expression, "[^abc]");
            Assert.AreEqual("abc".Group().Expression, "(abc)");
            Assert.AreEqual("abc".Group("alpha").Expression, "(?'alpha'abc)");
            Assert.AreEqual("alpha".AsBackRef().Expression, @"\k'alpha'");
            Assert.AreEqual("abc".Enclose().Expression, "(?:abc)");
            Assert.AreEqual("abc".Options().IgnoreCase().Multiline(false).Expression, "(?i-m:abc)");
            Assert.AreEqual("this is a test".AsComment().Expression, "(?#this is a test)");
            Assert.AreEqual("abc".Assert().YesPattern(Patterns.Symbols.WordEdge).Expression, @"(?(abc)\b)");

        }
    }
}
