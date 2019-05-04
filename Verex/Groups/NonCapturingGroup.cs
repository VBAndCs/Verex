using System;
using System.Collections.Generic;
using System.Text;

namespace RegexBuilder
{
    public class NonCapturingGroupPattern : GroupPattern
    {

        public NonCapturingGroupPattern(params Pattern[] patterns) : base(patterns)
        {
            Prefix = "?:";
        }
    }

}
