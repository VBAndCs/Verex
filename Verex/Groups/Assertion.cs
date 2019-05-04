using System;
using System.Collections.Generic;
using System.Text;

namespace RegexBuilder
{
    public class AssertionPattern : Pattern
    {
        string IfExpr = "";
        string YesExpr = "";
        string NoExpr = "";

        public AssertionPattern(string groupName) 
            => IfExpr = groupName;

        public AssertionPattern(ushort groupNo) 
            => IfExpr = groupNo.ToString();

        public AssertionPattern(params Pattern[] ifExprs)
        {
            var p = Patterns.AndPattern(ifExprs);
            var x = p.Expression;
            var L = x.Length;

            if (p.GetRepeatExpr() != "")
                IfExpr = x;
            else if (p is NonCapturingGroupPattern)
                IfExpr = x.Substring(3, L - 4);
            else if (p.GetType().IsSubclassOf(typeof(GroupPattern)))
                IfExpr = x.Substring(1, L - 2);
            else
                IfExpr = x;
        }

        public AssertionPattern YesPattern(params Pattern[] yesExprs)
        {
            var ag = (AssertionPattern)this.Copy();
            ag.YesExpr = Patterns.AndPattern(yesExprs).Expression;
            return ag;
        }

        public AssertionPattern ThenMatch(params Pattern[] yesExprs) => YesPattern(yesExprs);

        public AssertionPattern NoPattern(params Pattern[] noExprs)
        {
            if (YesExpr == "")
                throw new Exception("Call the YesPattern method first");

            var ag = (AssertionPattern)this.Copy();
            ag.NoExpr = Patterns.AndPattern(noExprs).Expression;

            return ag;
        }

        public AssertionPattern ElseMatch(params Pattern[] noExprs) => NoPattern(noExprs);

        public override string Expression
        {
            get
            {
                if (YesExpr == "")
                    throw new Exception("Call the YesPattern method first");

                if (NoExpr == "")
                    return $"(?({IfExpr}){YesExpr})" + GetRepeatExpr();

                return $"(?({IfExpr}){YesExpr}|{NoExpr})" + GetRepeatExpr();
            }
        }

    }

}
