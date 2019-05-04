using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RegexBuilder;

namespace RegexBuilderTests
{
    [TestClass]
    public class TextTests
    {
        [TestMethod]
        public void TestNullText()
        {
            var t = new TextPattern();
            Assert.AreEqual(t.Expression, "");
            var t2 = t.Repeat(2, 3);
            Assert.AreEqual(t2.Expression, "");
        }

        [TestMethod]
        public void TestOneCharText()
        {
            var t = new TextPattern("a");
            Assert.AreEqual(t.Expression, "a");
            var t2 = t.Repeat(2, 3);
            Assert.AreEqual(t2.Expression, "a{2,3}");
        }

        [TestMethod]
        public void TestOneEscapedCharText()
        {
            var t = new TextPattern(@"\");
            Assert.AreEqual(t.Expression, @"\\");
           var t2 = t.Repeat(2, 3);
            Assert.AreEqual(t2.Expression, @"\\{2,3}");
        }

        [TestMethod]
        public void TestMultiCharText()
        {
            var t = new TextPattern(@"\a");
            Assert.AreEqual(t.Expression, @"\\a");
            var t2 = t.Repeat(2, 3);
            Assert.AreEqual(t2.Expression, @"(?:\\a){2,3}");
        }
    }
}
