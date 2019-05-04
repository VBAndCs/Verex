using System;
using System.Collections.Generic;
using System.Text;

namespace RegexBuilder
{
    public struct ReplacePattern
    {
        readonly string Expr;
        private ReplacePattern(string expr) => Expr = expr;

        public string Expression => Expr;
        public override string ToString() => Expr;

        public static ReplacePattern ReplaceText(string text) => new ReplacePattern(text.Replace("$", "$$"));
        public static ReplacePattern GroupMatch(ushort GroupNo) => new ReplacePattern($"${GroupNo}") ;
        public static ReplacePattern GroupMatch(string GroupName) => new ReplacePattern("${" + GroupName + "}");
        public static ReplacePattern TheWholeInputText => new ReplacePattern("$_");
        public static ReplacePattern TheWholeMatch => new ReplacePattern("$&");
        public static ReplacePattern WholeTextBeforeTheMatch => new ReplacePattern("$`");
        public static ReplacePattern WholeTextAfterTheMatch => new ReplacePattern("$'");
        public static ReplacePattern LastCapturedGroupMatch => new ReplacePattern("$+");
        private static ReplacePattern Add(ReplacePattern repPattern1, ReplacePattern repPattern2) => new ReplacePattern(repPattern1.Expression + repPattern2.Expression);


        public static ReplacePattern operator &(ReplacePattern repPattern1, ReplacePattern repPattern2)
              => Add(repPattern1, repPattern2);

        public static ReplacePattern operator &(string s, ReplacePattern repPattern2)
              => Add(ReplaceText(s) , repPattern2);

        public static ReplacePattern operator &(ReplacePattern repPattern1, string s)
            => Add(repPattern1, ReplaceText(s));

        public static ReplacePattern operator +(ReplacePattern repPattern1, ReplacePattern repPattern2)
              => Add(repPattern1, repPattern2);

        public static ReplacePattern operator +(string s, ReplacePattern repPattern2)
              => Add(ReplaceText(s), repPattern2);

        public static ReplacePattern operator +(ReplacePattern repPattern1, string s)
            => Add(repPattern1, ReplaceText(s));

        public static implicit operator ReplacePattern(string s)
            => ReplaceText(s);
    }
}
