using System;
using System.Collections.Generic;
using System.Text;

namespace RegexBuilder
{
    public class CommentPattern : GroupPattern
    {
        private CommentPattern(params Pattern[] patterns) : base(patterns)
        {
        }

        public CommentPattern(string comment) : base()
        {
            PatternExpr = comment;
            Prefix = $"?#";
        }

        public override string Expression => $"({Prefix + PatternExpr})";


    }

}
