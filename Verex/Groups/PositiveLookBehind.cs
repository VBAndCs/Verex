using System;
using System.Collections.Generic;
using System.Text;

namespace RegexBuilder
{
    public class PositiveLookBehindPattern : LookArround
    {

        public PositiveLookBehindPattern(params Pattern[] patterns) : base(patterns)
        {
            Prefix = "?<=";
        }

    }

}
