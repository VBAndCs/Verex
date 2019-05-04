using System;
using System.Collections.Generic;
using System.Text;

namespace RegexBuilder
{
    public class NegativeLookBehindPattern : LookArround
    {

        public NegativeLookBehindPattern(params Pattern[] patterns) : base(patterns)
        {
            Prefix = "?<!";
        }
    }
}
