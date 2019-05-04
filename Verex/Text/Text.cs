using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace RegexBuilder
{
    public class TextPattern : Pattern
    {
        readonly string Str = "";

        public TextPattern(params string[] texts)
        {
            var s = string.Concat(texts);
            Str = Regex.Escape(s);
        }

        internal override bool DoNotEnclose
        {
            get
            {
                if (Str == "")
                    return true;

                if (Str.Length == 1 || (Str.Length == 2 && Str.StartsWith(@"\")))
                    return true;

                if (Expression.StartsWith("(?:"))
                    return true;

                return false;
            }

            set => base.DoNotEnclose = value;
        }

        public override string Expression
        {
            get
            {
                if (Str == "")
                    return "";

                var r = GetRepeatExpr();

                if (r =="" || Str.Length == 1 || (Str.Length == 2 && Str.StartsWith(@"\")))
                    return Str + r;

                return "(?:" + Str + ")" + r;
            }
        }

        public static implicit operator TextPattern(string s) 
            => new TextPattern(s);
    }
}
