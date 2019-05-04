using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rp = RegexBuilder.ReplacePattern;
using RegexBuilder;

namespace VerexTests
{
    [TestClass]
    public class ReplacePatternTests
    {
        [TestMethod]
        public void TestReplacePattern()
        {
            var r = "test" +
                        Rp.GroupMatch("group1") +
                        Rp.ReplaceText("x") +
                        Rp.TheWholeInputText +
                        Rp.TheWholeMatch +
                        Rp.LastCapturedGroupMatch +
                        Rp.WholeTextBeforeTheMatch +
                        Rp.WholeTextAfterTheMatch +
                        Rp.GroupMatch(1) +
                        "abc_$".AsReplace();

            Assert.AreEqual(r.Expression, "test${group1}x$_$&$+$`$'$1abc_$$");

            var p = "eat" + Patterns.Symbols.WordEdge;
            var S = p.Replace("I eat meat", "hate", 1);
            Assert.AreEqual(S, "I hate meat");


        }
    }
}
