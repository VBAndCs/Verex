using System;
using System.Collections.Generic;
using System.Text;

namespace RegexBuilder
{

    internal class OrPattern : Pattern
    {
        protected string Expr = "";

        internal OrPattern(params Pattern[] patterns)
        {
            if (patterns == null || patterns.Length == 0)
                return;

            if (patterns.Length == 1)
            {
                Expr = patterns[0].Expression;
                DoNotEnclose = patterns[0].GetRepeatExpr() == "" && patterns[0].DoNotEnclose;
            }
            else
            {
                DoNotEnclose = false;
                foreach (var pattern in patterns)
                    //if (pattern is AndPattern)
                    //    Expr += "(?:" + pattern.Expression + ")|";
                    //else
                        Expr += pattern.Expression + "|";

                Expr = Expr.Substring(0, Expr.Length - 1);
            }
        }

        public override string Expression
        {
            get
            {
                var r = GetRepeatExpr();
                if (r == "")
                    return Expr;

                if (DoNotEnclose)
                    return Expr + r;

                return "(?:" + Expr + ")" + r;
            }
        }
    }
}
