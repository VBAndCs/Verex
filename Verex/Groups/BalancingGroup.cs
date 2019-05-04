using System;
using System.Collections.Generic;
using System.Text;

namespace RegexBuilder
{
    public class BalancingGroupPattern : GroupPattern
    {
        string ThisGroupName = "";
        string OtherGroupName = "";

        private BalancingGroupPattern(params Pattern[] patterns) : base(patterns)
        {
        }

        public BalancingGroupPattern(string otherGroupName, params Pattern[] patterns) : base(patterns)
        {
            OtherGroupName = otherGroupName;
            Prefix = $"?'-{OtherGroupName}'";
        }

        public BalancingGroupPattern((string openGroupName, string closeGroupName) groups, params Pattern[] patterns) : base(patterns)
        {
            ThisGroupName = groups.closeGroupName;
            OtherGroupName = groups.openGroupName;
            Prefix = $"?'{ThisGroupName}-{OtherGroupName}'";
        }


    }

}
