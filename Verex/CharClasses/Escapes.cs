using System;
using System.Collections.Generic;
using System.Text;

namespace RegexBuilder
{


    public partial struct Escapes
    {
        private readonly string Value;
        internal Escapes(string value) => Value = value;

        public static implicit operator string(Escapes e) => e.Value;
        public override string ToString() => Value;


        public CharClassPattern ToCharClass()
            => new CharClassPattern(false, new Escapes(Value));

        public CharClassPattern ToNegativeCharClass()
            => new CharClassPattern(true, new Escapes(Value));

        public CharClassPattern Negate()
            => ToNegativeCharClass();

        public static implicit operator CharClassPattern(Escapes e) => e.ToCharClass();
        public static CharClassPattern operator !(Escapes e) => e.ToNegativeCharClass();

        public static Escapes Quote = new Escapes("\"");
        public static Escapes LineFeed => new Escapes(@"\n");
        public static Escapes CarriageReturn => new Escapes(new Escapes(@"\r"));
        public static Escapes Digit => new Escapes(@"\d");
        public static Escapes NonDigit => new Escapes(@"\D");
        public static Escapes AnyWordChar => new Escapes(@"\w");
        public static Escapes NonWordChar => new Escapes(@"\W");
        public static Escapes Bell => new Escapes(@"\a");
        public static Escapes Alarm => new Escapes(@"\a");
        public static Escapes Backspace => new Escapes(@"\b");
        public static Escapes Tab => new Escapes(@"\t");
        public static Escapes VerticalTab => new Escapes(@"\v");
        public static Escapes FormFeed => new Escapes(@"\f");
        public static Escapes WhiteSpace => new Escapes(@"\s");
        public static Escapes NonWhiteSpace => new Escapes(@"\S");
        public static Escapes Escape => new Escapes(@"\e");
        public static Escapes Hat => new Escapes(@"\^");
        public static Escapes Minus => new Escapes(@"\-");
        public static Escapes OpenSquareBracket => new Escapes(@"\[");
        public static Escapes BackSlash => new Escapes(@"\\");


        public static Escapes ControlChar(char c)
            => new Escapes(@"\c" + char.ToUpper(c));

        public static Escapes AsciiChar(byte asciiCode)
        {
            return new Escapes(@"\x" + Convert.ToString(asciiCode, 16).PadLeft(2, '0'));
        }

        /// <summary>
        /// Use this method to convert a char to its regex ASCII representation.
        /// This solves some problems with some matching chars, i.e 
        /// when writing ] in a char class.
        /// </summary>
        /// <param name="c"></param>
        /// <returns></returns>
        public static Escapes AsciiChar(char c)
        {
            if ((int)c > 255)
                throw new ArgumentException("this is not an ASCII char");

            return new Escapes(@"\x" + Convert.ToString(c, 16).PadLeft(2, '0'));
        }

    }

    
}
