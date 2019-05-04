using System;
using System.Collections.Generic;
using System.Text;

namespace RegexBuilder
{
    internal class AndPattern : Pattern
    {
        protected string Expr = "";
       internal bool MustEnclose = false;
        internal bool EncloseAtRepeatOnly = false;

        internal AndPattern(params Pattern[] patterns)
        {
            if (patterns == null || patterns.Length == 0)
                return;

            if (patterns.Length == 1)
            {
                #region "Note"
                // Sending one pattern only to AndPattern can cause problems 
                // in enclosing the patterns when nessecary.
                // it can happen because some methods and classes accept params array,                   
                // I added 3 fileds to adjust, and it wirks nearly fine.
                // But I solved the problem, by not creating the AndPattern directly.
                // Classes and methods now use the Patterns.AndPattern method, 
                // which return the same pattern if it is sent alone, 
                // and don't create an AndPattern Object unless the param array has two patterns at least.
                // So now, this case is not executable, and no need for the 3 fields!
                // I just keep them as a precaution
                #endregion

                Expr = patterns[0].Expression;
                DoNotEnclose = patterns[0].GetRepeatExpr() != "" || patterns[0].DoNotEnclose;
                MustEnclose =  !(DoNotEnclose || (patterns[0] is AndPattern && ((AndPattern)patterns[0]).EncloseAtRepeatOnly)) ;
            }
            else
            {
                DoNotEnclose = false;
                EncloseAtRepeatOnly = true;
                foreach (var pattern in patterns)
                    if (pattern.GetRepeatExpr() == "" && 
                        (pattern is OrPattern || (pattern is AndPattern && ((AndPattern)pattern).MustEnclose)))
                    {
                        Expr += "(?:" + pattern.Expression + ")";
                    }
                    else
                        Expr += pattern.Expression;
            }
        }

        public override string Expression
        {
            get
            {
                var r = GetRepeatExpr();
                if (r == "")
                    return Expr;

                if (DoNotEnclose)
                    return Expr + r;

                MustEnclose = false;
                return "(?:" + Expr + ")" + r;
            }
        }
    }
}
