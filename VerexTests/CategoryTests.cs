using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RegexBuilder;
using static RegexBuilder.Patterns.Unicode;

namespace RegexBuilderTests
{
    [TestClass]
    public class CategoryTests
    {
        [TestMethod]
        public void TestCategory()
        {
            Assert.AreEqual(Arabic.NegateCategory().Expression, NonArabic.Expression);
            Assert.AreEqual(NonBasicLatin.NegateCategory().Expression, BasicLatin.Expression);
            Assert.AreEqual(BasicLatin.Intersect(NonLowercaseLetter).Expression, @"(?=\p{IsBasicLatin})\P{Ll}");
            Assert.AreEqual(BasicLatin.UnionAndIntersect(Arabic , NonLowercaseLetter).Expression, @"(?=\p{IsBasicLatin}|\p{IsArabic})\P{Ll}");
            var x = BasicLatin - LowercaseLetter;
            Assert.AreEqual(x.Expression, @"[\p{IsBasicLatin}-[\p{Ll}]]");
            Assert.AreEqual((MathSymbol - x).Expression, @"[\p{Sm}-[\p{IsBasicLatin}-[\p{Ll}]]]");
            Assert.AreEqual((x - ('H', 'K')).Expression, @"[\p{IsBasicLatin}-[\p{Ll}H-K]]");
            Assert.AreEqual((x - 'H').Expression, @"[\p{IsBasicLatin}-[\p{Ll}H]]");
            x = BasicLatin - (LowercaseLetter - 'a');
            Assert.AreEqual(x.Expression, @"[\p{IsBasicLatin}-[\p{Ll}-[a]]]");
            x = (BasicLatin - LowercaseLetter) - 'a';
            Assert.AreEqual(x.Expression, @"[\p{IsBasicLatin}-[\p{Ll}a]]");

             x = BasicLatin & LowercaseLetter;
            Assert.AreEqual(x.Expression, @"[\p{IsBasicLatin}\p{Ll}]");
            Assert.AreEqual((MathSymbol & x).Expression, @"[\p{Sm}\p{IsBasicLatin}\p{Ll}]");
            Assert.AreEqual((BasicLatin & ('H', 'K')).Expression, @"[\p{IsBasicLatin}H-K]");
            Assert.AreEqual((Arabic & 'H').Expression, @"[\p{IsArabic}H]");
            x = BasicLatin & (LowercaseLetter - 'a');
            Assert.AreEqual(x.Expression, @"[\p{IsBasicLatin}\p{Ll}-[a]]");
            x = (BasicLatin & LowercaseLetter) - 'a';
            Assert.AreEqual(x.Expression, @"[\p{IsBasicLatin}\p{Ll}-[a]]");
            Assert.AreEqual((Arabic & new CharClassPattern(false, 'b')).Expression, @"[\p{IsArabic}b]");
            Assert.AreEqual((Arabic & ('a', 'z')).Expression, @"[\p{IsArabic}a-z]");

            var p = BasicLatin + LowercaseLetter;
            Assert.AreEqual(p.Expression, @"\p{IsBasicLatin}\p{Ll}");
            Assert.AreEqual((MathSymbol + p).Expression, @"\p{Sm}\p{IsBasicLatin}\p{Ll}");
            p = BasicLatin + (LowercaseLetter - 'a');
            Assert.AreEqual(p.Expression, @"\p{IsBasicLatin}[\p{Ll}-[a]]");
            Assert.AreEqual((Arabic + new CharClassPattern(false, 'b')).Expression, @"\p{IsArabic}[b]");
            Assert.AreEqual((Arabic + ('a', 'z')).Expression, @"\p{IsArabic}[a-z]");


        }
    }
}
