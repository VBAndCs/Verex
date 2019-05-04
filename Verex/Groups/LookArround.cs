using System;
using System.Collections.Generic;
using System.Text;

namespace RegexBuilder
{
    public abstract class LookArround: GroupPattern
    {

        public LookArround(params Pattern[] patterns) : base(patterns)
        {        
        }

        // LookAhead Groups can not be repeated!
        public override string Expression
               => $"({Prefix + PatternExpr})";
    }
}
