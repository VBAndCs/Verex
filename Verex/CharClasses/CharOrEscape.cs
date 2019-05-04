namespace RegexBuilder
{
    public class CharOrEscape
    {
        public readonly string Value;

        internal CharOrEscape(string value) => Value = value;

        internal CharOrEscape(char c)
        {
            if (c > 31)
                Value = c.ToString();
            else if (c == '\a')
                Value = Escapes.Alarm;
            else if (c == '\b')
                Value = Escapes.Backspace;
            else if (c == '\r')
                Value = Escapes.CarriageReturn;
            else if (c == '\n')
                Value = Escapes.LineFeed;
            else if (c == '\f')
                Value = Escapes.FormFeed;
            else if (c == '\t')
                Value = Escapes.Tab;
            else if (c == '\v')
                Value = Escapes.VerticalTab;
            else
                Value = Escapes.AsciiChar(c);
        }


        public static implicit operator CharOrEscape(Escapes s) => new CharOrEscape(s.ToString());

        public static implicit operator CharOrEscape(char c) => new CharOrEscape(c);

        public static implicit operator string(CharOrEscape s) => s.ToString();


        public override string ToString() => Value;
        public override bool Equals(object obj) => ((CharOrEscape)obj).Value == Value;
        public override int GetHashCode() => base.GetHashCode();
    }
}
