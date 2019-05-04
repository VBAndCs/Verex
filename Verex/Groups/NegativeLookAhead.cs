using System;
using System.Collections.Generic;
using System.Text;

namespace RegexBuilder
{
    public class NegativeLookAheadPattern : LookArround
    {

        public NegativeLookAheadPattern(params Pattern[] patterns) : base(patterns)
        {
            Prefix = "?!";
        }
        
    }

}
