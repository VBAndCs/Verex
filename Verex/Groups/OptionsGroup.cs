using System;
using System.Collections.Generic;
using System.Text;

namespace RegexBuilder
{
    public class OptionsGroupPattern : GroupPattern
    {

        bool? m_IgnoreCase = null;
        bool? m_Multiline = null;
        bool? m_ExplicitCapture = null;
        bool? m_SingleLine = null;
        bool? m_IgnorePatternWhitespace = null;

        public OptionsGroupPattern() 
        {            
        }

        public OptionsGroupPattern(params Pattern[] patterns) : base(patterns)
        {          
        }

        public OptionsGroupPattern IgnoreCase(bool? enabled = true)
        {
            var options = (OptionsGroupPattern)this.Copy();
            options.m_IgnoreCase = enabled;
            options.ModifyPrefix();
            return options;
        }

        public OptionsGroupPattern Multiline(bool? enabled = true)
        {
            var options = (OptionsGroupPattern)this.Copy();
            options.m_Multiline = enabled;
            options.ModifyPrefix();
            return options;
        }

        public OptionsGroupPattern ExplicitCapture(bool? enabled = true)
        {
            var options = (OptionsGroupPattern)this.Copy();
            options.m_ExplicitCapture = enabled;
            options.ModifyPrefix();
            return options;
        }

        public OptionsGroupPattern SingleLine(bool? enabled = true)
        {
            var options = (OptionsGroupPattern)this.Copy();
            options.m_SingleLine = enabled;
            options.ModifyPrefix();
            return options;
        }

        public OptionsGroupPattern IgnorePatternWhitespace(bool? enabled = true)
        {
            var options = (OptionsGroupPattern)this.Copy();
            options.m_IgnorePatternWhitespace = enabled;
            options.ModifyPrefix();
            return options;
        }

        public OptionsGroupPattern SetAll(bool? value)
        {
            var options = (OptionsGroupPattern)this.Copy();
            options.m_IgnoreCase = value;
            options.m_Multiline = value;
            options.m_ExplicitCapture = value;
            options.m_SingleLine = value;
            options.m_IgnorePatternWhitespace = value;
            options.ModifyPrefix();
            return options;
        }

        void ModifyPrefix()
        {
            string enable = "";
            string disable = "";

            if (m_IgnoreCase.HasValue)
                if (m_IgnoreCase.Value)
                    enable += "i";
                else
                    disable += "i";

            if (m_Multiline.HasValue)
                if (m_Multiline.Value)
                    enable += "m";
                else
                    disable += "m";

            if (m_ExplicitCapture.HasValue)
                if (m_ExplicitCapture.Value)
                    enable += "n";
                else
                    disable += "n";

            if (m_SingleLine.HasValue)
                if (m_SingleLine.Value)
                    enable += "s";
                else
                    disable += "s";

            if (m_IgnorePatternWhitespace.HasValue)
                if (m_IgnorePatternWhitespace.Value)
                    enable += "x";
                else
                    disable += "x";

            if (disable == "")
            {
                if (enable == "")
                    Prefix = "";
                else
                    Prefix = $"?{enable}" + (PatternExpr == "" ? "" : ":");
            }
            else
                Prefix = $"?{enable}-{disable}" + (PatternExpr == "" ? "" : ":");
        }

        public override string Expression
        {
            get
            {
                if (Prefix == "" && PatternExpr =="")                    
                    return "";

                return $"({Prefix + PatternExpr})" ;
            }
        }

    }

}
