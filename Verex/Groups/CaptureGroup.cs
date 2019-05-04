using System;
using System.Collections.Generic;
using System.Text;

namespace RegexBuilder
{
    public class CaptureGroupPattern : GroupPattern
    {
        string GroupName = "";

        private CaptureGroupPattern(params Pattern[] patterns) : base(patterns)
        {
        }

        public CaptureGroupPattern(string groupName, params Pattern[] patterns) : base(patterns)
        {
            GroupName = groupName;
            Prefix = $"?'{groupName}'";
        }
    }

}
