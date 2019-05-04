using System;
using System.Collections.Generic;
using System.Text;

namespace RegexBuilder
{
    public class XModeCommentPattern : GroupPattern
    {
        public XModeCommentPattern(params (Pattern Pattern, string Comment)[] patternLines)
        {
            if (patternLines == null || patternLines.Length == 0)
                throw new ArgumentNullException("patternLines");

            Prefix = "?x:    #X-Mode: Ignore white spaces.\r\n";

            foreach (var p in patternLines)
                PatternExpr += p.Pattern.Expression + "    #" + p.Comment + "\r\n";

        }

    }

}
