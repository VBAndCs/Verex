using System;
using System.Collections.Generic;
using System.Text;

namespace RegexBuilder
{
    public class PositiveLookAheadPattern : LookArround
    {

        public PositiveLookAheadPattern(params Pattern[] patterns) : base(patterns)
        {
            Prefix = "?=";
        }

    }

}
