using System;
using System.Collections.Generic;
using System.Text;

namespace RegexBuilder
{
    public class BackReference : Pattern
    {
        string GroupName = "";

        public BackReference(string groupName)
        {
            GroupName = $@"\k'{groupName}'";
        }

        public BackReference(ushort groupNo)
        {
            GroupName = $@"\{groupNo}";
        }

        public override string Expression => GroupName + GetRepeatExpr();

    }

}
