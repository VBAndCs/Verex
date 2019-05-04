using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RegexBuilder;

namespace RegexBuilderTests
{
    [TestClass]
    public class CharClassTests
    {
        [TestMethod]
        public void TestPositiveCharClass()
        {
            var c = new CharClassPattern(false,'a');
            Assert.AreEqual(c.Expression, @"[a]");

            c = new CharClassPattern(false,'a', Escapes.Backspace, 'c');
            Assert.AreEqual(c.Expression, @"[a\bc]");

            c = new CharClassPattern(false,('c', 'd'));
            Assert.AreEqual(c.Expression, @"[c-d]");

            c = new CharClassPattern(false,('a', Escapes.Backspace), ('d', 'c'));
            Assert.AreEqual(c.Expression, @"[a-\bc-d]");

            c = new CharClassPattern(false,('d', 'c'), 'a', Escapes.Backspace);
            Assert.AreEqual(c.Expression, @"[a\bc-d]");

            var c2 = new CharClassPattern(false,'b', ('a', Escapes.Backspace), ('d', 'c'));
            Assert.AreEqual(c2.Expression, @"[ba-\bc-d]");

            c2 = c2.AddRanges(('j', 'i'));
            Assert.AreEqual(c2.Expression, @"[ba-\bc-di-j]");

            c2 = c2.AddChars('x', 'z');
            Assert.AreEqual(c2.Expression, @"[bxza-\bc-di-j]");

            c2 = c.AddAsciiChars(8, 17, 100);
            Assert.AreEqual(c2.Expression, @"[a\b\x08\x11\x64c-d]");

            c2 = c.AddAsciiChars('\0', '\x08');
            Assert.AreEqual(c2.Expression, @"[a\b\x00\x08c-d]");


            c2 = c.AddUnicodeChars(8, 17, 100);
            Assert.AreEqual(c2.Expression, @"[a\b\u0008\u0011\u0064c-d]");

            c2 = c.AddUnicodeChars('\0', '\x08');
            Assert.AreEqual(c2.Expression, @"[a\b\u0000\u0008c-d]");

            c2 = c.AddClass(Patterns.Unicode.Arabic);
            Assert.AreEqual(c2.Expression, @"[a\b\p{IsArabic}c-d]");

            var c3 = new CharClassPattern(false, Escapes.Unicode.Arabic, ('m', 'r'));
            c2 = c.AddClass(c3);
            Assert.AreEqual(c2.Expression, @"[a\b\p{IsArabic}c-dm-r]");

            c2 = c2.SubtractRanges(('j', 'i'));
            Assert.AreEqual(c2.Expression, @"[a\b\p{IsArabic}c-dm-r-[i-j]]");

            c2 = c2.SubtractChars('x', 'z');
            Assert.AreEqual(c2.Expression, @"[a\b\p{IsArabic}c-dm-r-[xzi-j]]");

            c2 = c.SubtractAsciiChars(8, 17, 100);
            Assert.AreEqual(c2.Expression, @"[a\bc-d-[\x08\x11\x64]]");

            c2 = c.SubtractAsciiChars('\0', '\x08');
            Assert.AreEqual(c2.Expression, @"[a\bc-d-[\x00\x08]]");


            c2 = c.SubtractUnicodeChars(8, 17, 100);
            Assert.AreEqual(c2.Expression, @"[a\bc-d-[\u0008\u0011\u0064]]");

            c2 = c.SubtractUnicodeChars('\0', '\x08');
            Assert.AreEqual(c2.Expression, @"[a\bc-d-[\u0000\u0008]]");

            c2 = c.SubtractClass(Patterns.Unicode.Arabic);
            Assert.AreEqual(c2.Expression, @"[a\bc-d-[\p{IsArabic}]]");

            c2 = c.SubtractClass(c3);
            Assert.AreEqual(c2.Expression, @"[a\bc-d-[\p{IsArabic}m-r]]");

            c2 = c.SubtractClass(c3.SubtractRanges(('f', 'h')));
            Assert.AreEqual(c2.Expression, @"[a\bc-d-[\p{IsArabic}m-r-[f-h]]]");
        }

        [TestMethod]
        public void TestNegativeCharClass()
        {
            var c = new CharClassPattern(true, 'a');
            Assert.AreEqual(c.Expression, @"[^a]");

            c = new CharClassPattern(true, 'a', Escapes.Backspace, 'c');
            Assert.AreEqual(c.Expression, @"[^a\bc]");

            c = new CharClassPattern(true, ('c', 'd'));
            Assert.AreEqual(c.Expression, @"[^c-d]");

            c = new CharClassPattern(true, ('a', Escapes.Backspace), ('d', 'c'));
            Assert.AreEqual(c.Expression, @"[^a-\bc-d]");

            c = new CharClassPattern(true, ('d', 'c'), 'a', Escapes.Backspace);
            Assert.AreEqual(c.Expression, @"[^a\bc-d]");

            var c2 = new CharClassPattern(true, 'b', ('a', Escapes.Backspace), ('d', 'c'));
            Assert.AreEqual(c2.Expression, @"[^ba-\bc-d]");

            c2 = c2.AddRanges(('j', 'i'));
            Assert.AreEqual(c2.Expression, @"[^ba-\bc-di-j]");

            c2 = c2.AddChars('x', 'z');
            Assert.AreEqual(c2.Expression, @"[^bxza-\bc-di-j]");

            c2 = c.AddAsciiChars(8, 17, 100);
            Assert.AreEqual(c2.Expression, @"[^a\b\x08\x11\x64c-d]");

            c2 = c.AddAsciiChars('\0', '\x08');
            Assert.AreEqual(c2.Expression, @"[^a\b\x00\x08c-d]");


            c2 = c.AddUnicodeChars(8, 17, 100);
            Assert.AreEqual(c2.Expression, @"[^a\b\u0008\u0011\u0064c-d]");

            c2 = c.AddUnicodeChars('\0', '\x08');
            Assert.AreEqual(c2.Expression, @"[^a\b\u0000\u0008c-d]");

            c2 = c.AddClass(Patterns.Unicode.Arabic.Negate());
            Assert.AreEqual(c2.Expression, @"[^a\b\p{IsArabic}c-d]");

            var c3 = new CharClassPattern(false, Escapes.Unicode.Arabic, ('m', 'r'));
            c2 = c.AddClass(c3.Negate());
            Assert.AreEqual(c2.Expression, @"[^a\b\p{IsArabic}c-dm-r]");

            c2 = c2.SubtractRanges(('j', 'i'));
            Assert.AreEqual(c2.Expression, @"[^a\b\p{IsArabic}c-dm-r-[i-j]]");

            c2 = c2.SubtractChars('x', 'z');
            Assert.AreEqual(c2.Expression, @"[^a\b\p{IsArabic}c-dm-r-[xzi-j]]");

            c2 = c.SubtractAsciiChars(8, 17, 100);
            Assert.AreEqual(c2.Expression, @"[^a\bc-d-[\x08\x11\x64]]");

            c2 = c.SubtractAsciiChars('\0', '\x08');
            Assert.AreEqual(c2.Expression, @"[^a\bc-d-[\x00\x08]]");


            c2 = c.SubtractUnicodeChars(8, 17, 100);
            Assert.AreEqual(c2.Expression, @"[^a\bc-d-[\u0008\u0011\u0064]]");

            c2 = c.SubtractUnicodeChars('\0', '\x08');
            Assert.AreEqual(c2.Expression, @"[^a\bc-d-[\u0000\u0008]]");

            c2 = c.SubtractClass(Patterns.Unicode.Arabic);
            Assert.AreEqual(c2.Expression, @"[^a\bc-d-[\p{IsArabic}]]");

            c2 = c.SubtractClass(c3);
            Assert.AreEqual(c2.Expression, @"[^a\bc-d-[\p{IsArabic}m-r]]");

            c2 = c.SubtractClass(c3.SubtractRanges(('f', 'h')));
            Assert.AreEqual(c2.Expression, @"[^a\bc-d-[\p{IsArabic}m-r-[f-h]]]");

            c2 = c.SubtractClass(!c3.SubtractRanges(('f', 'h')));
            Assert.AreEqual(c2.Expression, @"[^a\bc-d-[^\p{IsArabic}m-r-[f-h]]]");

            c2 = c.SubtractClass(!c3.SubtractClass(new CharClassPattern(true, ('f', 'h'))));
            Assert.AreEqual(c2.Expression, @"[^a\bc-d-[^\p{IsArabic}m-r-[^f-h]]]");

        }

        [TestMethod]
        public void TestCharClassOperators()
        {
            var c1 = new CharClassPattern(false, 'a', 'b');
            var c2 = new CharClassPattern(true, ('m', 'r'));

            Assert.AreEqual( (!c1).Expression, "[^ab]");
            Assert.AreEqual((!c2).Expression, "[m-r]");

            Assert.ThrowsException<ArgumentException>(()=>c1 & c2);
            Assert.AreEqual((!c1 & c2).Expression, "[^abm-r]");
            Assert.AreEqual((c1 & !c2).Expression, "[abm-r]");
            Assert.AreEqual((c1 & 'x').Expression, "[abx]");
            Assert.AreEqual((c1 & ('s', 'u')).Expression, "[abs-u]");
            Assert.AreEqual((c2 & 'x').Expression, "[^xm-r]");
            Assert.AreEqual((c2 & ('s', 'u')).Expression, "[^m-rs-u]");

            Assert.AreEqual((c1 + c2).Expression, "[ab][^m-r]");
            Assert.AreEqual((!c1 + c2).Expression, "[^ab][^m-r]");
            Assert.AreEqual((c1 + !c2).Expression, "[ab][m-r]");

            Assert.AreEqual((c1 | c2).Expression, "[ab]|[^m-r]");
            Assert.AreEqual((!c1 | c2).Expression, "[^ab]|[^m-r]");
            Assert.AreEqual((c1 | !c2).Expression, "[ab]|[m-r]");


            Assert.AreEqual((c1 - c2).Expression, "[ab-[^m-r]]");
            Assert.AreEqual((!c1 - c2).Expression, "[^ab-[^m-r]]");
            Assert.AreEqual((c1 - !c2).Expression, "[ab-[m-r]]");

            Assert.AreEqual((c1 - 'x').Expression, "[ab-[x]]");
            Assert.AreEqual(((c1 - 'x' & 'i') - 'p').Expression, "[abi-[xp]]");
            Assert.AreEqual((c1 - ('s', 'u')).Expression, "[ab-[s-u]]");
            Assert.AreEqual((c1 - ('s', 'u') - 'f').Expression, "[ab-[fs-u]]");
            Assert.AreEqual((c2 - 'x').Expression, "[^m-r-[x]]");
            Assert.AreEqual(((c2 - 'x' & 'i') - 'p').Expression, "[^im-r-[xp]]");
            Assert.AreEqual((c2 - ('s', 'u')).Expression, "[^m-r-[s-u]]");
            Assert.AreEqual((c2 - ('s', 'u') - 'f').Expression, "[^m-r-[fs-u]]");
        }

    }
}
