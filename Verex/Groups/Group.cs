using System;
using System.Collections.Generic;
using System.Text;

namespace RegexBuilder
{
    public class GroupPattern : Pattern
    {
        protected string PatternExpr = "";
        protected string Prefix = "";

        protected GroupPattern()
        {

        }

        public GroupPattern(params Pattern[] patterns)
                => PatternExpr = Patterns.AndPattern(patterns).Expression;
        
        public override string Expression => $"({Prefix + PatternExpr})" + GetRepeatExpr();

    }

}
