using System;
using System.Collections.Generic;
using System.Text;

namespace RegexBuilder
{
    public class Symbol : Pattern
    {
        protected readonly string Str = "";
        readonly bool CanInvert = true;
        readonly bool Encoded = false;

        internal Symbol(char c)
        {
            if (c > 31)
                Str = c.ToString();
            else if (c == '\a')
                Str = Escapes.Alarm;
            else if (c == '\b')
                Str = Escapes.Backspace;
            else  if (c == '\r')
                Str = Escapes.CarriageReturn;
            else if (c == '\n')
                Str = Escapes.LineFeed;
            else if (c == '\f')
                Str = Escapes.FormFeed;
            else if (c == '\t')
                Str = Escapes.Tab;
            else if (c == '\v')
                Str = Escapes.VerticalTab;
            else
                Str = Escapes.AsciiChar(c);
        }

        internal Symbol(string s, bool canInvert = true, bool encoded = false)
        {
            Str = s;
            CanInvert = canInvert;
            Encoded = encoded;
        }

        internal override bool DoNotEnclose
        {
            get
            {
                if (Str == "")
                    return true;

                if (Encoded || Str.Length == 1 || (Str.Length == 2 && Str.StartsWith(@"\")))
                    return true;

                if (Expression.StartsWith("(?:"))
                    return true;

                return false;
            }

            set => base.DoNotEnclose = value;
        }

        public override string Expression
        {
            get
            {
                if (Str == "")
                    return "";

                var r = GetRepeatExpr();

                if (r == "")
                    return Str;

                if (Encoded || Str.Length == 1 || (Str.Length == 2 && Str.StartsWith(@"\")))
                    return Str + r;

                return "(?:" + Str + ")" + r;
            }
        }

        public CharClassPattern ToCharClass()
        {
            if (!CanInvert || GetRepeatExpr() != "")
                throw new InvalidOperationException("This symbol can't be used in a char class");

            return  new CharClassPattern(false, new Escapes(Expression));
        }

        public static implicit operator Symbol(char c) => new Symbol(c);

        public static implicit operator CharClassPattern(Symbol s) => s.ToCharClass();

        public CharClassPattern Negate()
        {
            if (!CanInvert || GetRepeatExpr() != "")
                throw new InvalidOperationException("Can't negate this symbol");

            return new CharClassPattern(true, new Escapes(Str));
        }

        public CharClassPattern ToNegativeCharClass() => Negate();

        public static CharClassPattern operator !(Symbol s) => s.Negate();
    }

}
