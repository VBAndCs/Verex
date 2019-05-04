using System;
using System.Collections.Generic;
using System.Text;

namespace RegexBuilder
{
    public class AtomicGroupPattern : GroupPattern
    {

        public AtomicGroupPattern(params Pattern[] patterns) : base(patterns)
        {
            Prefix = "?>";
        }

    }

}
