using System;
using System.Collections.Generic;

namespace RegexBuilder
{
    public class CharClassPattern : Pattern
    {
        List<CharOrEscape> Chars = new List<CharOrEscape>();
        List<Range<CharOrEscape>> Ranges = new List<Range<CharOrEscape>>();
        CharClassPattern Subtracted;
        bool IsNegative = false;

        public override Pattern Copy()
        {
            var newClass = (CharClassPattern)base.Copy();
            newClass.Chars = new List<CharOrEscape>(Chars);
            newClass.Ranges = new List<Range<CharOrEscape>>(Ranges);
            return newClass;
        }

        private CharClassPattern(bool isNegative =false)
        {
            IsNegative = isNegative;
        }

        public CharClassPattern(bool isNegative, params CharOrEscape[] chars)
        {
            IsNegative = isNegative;
            if (chars != null && chars.Length > 0)
                Chars.AddRange(chars);
        }

        public CharClassPattern(bool isNegative, params (CharOrEscape from, CharOrEscape to)[] ranges)
        {
            IsNegative = isNegative;
            if (ranges != null && ranges.Length > 0)
                foreach (var range in ranges)
                    Ranges.Add(Fix(range));
        }

        public CharClassPattern(bool isNegative, (CharOrEscape from, CharOrEscape to) range, params CharOrEscape[] chars) : this(isNegative, chars)
                 => Ranges.Add(Fix(range));

        public CharClassPattern(bool isNegative, CharOrEscape c, params (CharOrEscape from, CharOrEscape to)[] ranges) : this(isNegative, ranges)
                => Chars.Add(c);

        protected string EscapeInCharClass(string s)
        {
            if (s == "^" || s == "-" || s == "[" || s == @"\")
                s = @"\" + s;

            return s;
        }

        public CharClassPattern AddChars(params CharOrEscape[] chars)
        {
            if (chars == null || chars.Length == 0)
                throw new ArgumentNullException("chars");

            var newClass = (CharClassPattern)this.Copy();
            foreach (var c in chars)
                if (!newClass.Chars.Contains(c))
                    newClass.Chars.Add(c);

            return newClass;
        }

        public CharClassPattern AddAsciiChars(params char[] chars)
        {
            if (chars == null || chars.Length == 0)
                throw new ArgumentNullException("chars");

            var newClass = (CharClassPattern)this.Copy();
            foreach (var c in chars)
                {
                    var a = Escapes.AsciiChar(c);
                    if (!newClass.Chars.Contains(a))
                    newClass.Chars.Add(a);
                }

            return newClass;
        }

        public CharClassPattern AddAsciiChars(params byte[] ascii)
        {
            if (ascii == null || ascii.Length == 0)
                throw new ArgumentNullException("chars");

            var newClass = (CharClassPattern)this.Copy();
            foreach (var c in ascii)
            {
                var a = Escapes.AsciiChar(c);
                if (!newClass.Chars.Contains(a))
                    newClass.Chars.Add(a);
            }

            return newClass;
        }

        public CharClassPattern AddUnicodeChars(params char[] chars)
        {
            if (chars == null || chars.Length == 0)
                throw new ArgumentNullException("chars");

            var newClass = (CharClassPattern)this.Copy();
            foreach (var c in chars)
                {
                    var a = Escapes.Unicode.UnicodeChar(c);
                    if (!newClass.Chars.Contains(a))
                    newClass.Chars.Add(a);
                }

            return newClass;
        }

        public CharClassPattern AddUnicodeChars(params ushort[] unicodes)
        {
            if (unicodes == null || unicodes.Length == 0)
                throw new ArgumentNullException("chars");

            var newClass = (CharClassPattern)this.Copy();
            foreach (var c in unicodes)
            {
                var a = Escapes.Unicode.UnicodeChar(c);
                if (!newClass.Chars.Contains(a))
                    newClass.Chars.Add(a);
            }

            return newClass;
        }

        public CharClassPattern AddRange(CharOrEscape from, CharOrEscape to)
        {
            var newClass = (CharClassPattern)this.Copy();
            var range = (from, to);
            if (!newClass.Ranges.Contains(range))
                newClass.Ranges.Add(Fix(range));

            return newClass;
        }

        public CharClassPattern AddRanges(params (CharOrEscape from, CharOrEscape to)[] ranges)
        {
            if (ranges == null || ranges.Length == 0)
                throw new ArgumentNullException("ranges");

            var newClass = (CharClassPattern)this.Copy();
            foreach (var range in ranges)
                if (!newClass.Ranges.Contains(range))
                    newClass.Ranges.Add(Fix(range));

            return newClass;
        }

        protected Range<CharOrEscape> Fix((CharOrEscape from, CharOrEscape to) range)
        {
            var from = range.from.ToString();
            var to = range.to.ToString();

            if (from.StartsWith(@"\") || to.StartsWith(@"\") || from.CompareTo(to) != 1)
                return new Range<CharOrEscape>(range.from, range.to);

            return new Range<CharOrEscape>(range.to, range.from);

        }

        public CharClassPattern AddClass(CharClassPattern charClass)
        {
            if (this.IsNegative != charClass.IsNegative)
                throw new ArgumentException("The + operator can only be used to add two positive char classes or two negative char classes.. Use the the & operator instead to add a positive and a negative classes.");

            if (charClass.GetRepeatExpr() != "")
                throw new ArgumentException("Added char class can't be repeated!");

            if (charClass == null)
                throw new ArgumentNullException("charClass");

            var newClass = (CharClassPattern)this.Copy();
            newClass.Chars.AddRange(charClass.Chars);
            newClass.Ranges.AddRange(charClass.Ranges);

            if (charClass.Subtracted != null)
            {
                if (newClass.Subtracted == null)
                    newClass.Subtracted = new CharClassPattern(false);

                newClass.Subtracted = newClass.Subtracted.AddClass(charClass.Subtracted);
            }

            return newClass;
        }

        public CharClassPattern SubtractRange(CharOrEscape from, CharOrEscape to)
        {
            var newClass = (CharClassPattern)this.Copy();
            if (newClass.Subtracted == null)
                newClass.Subtracted = new CharClassPattern(false, (from, to));
            else
                newClass.Subtracted = newClass.Subtracted.AddRange(from, to);

            return newClass;
        }

        public CharClassPattern SubtractRanges(params (CharOrEscape from, CharOrEscape to)[] ranges)
        {
            if (ranges == null || ranges.Length == 0)
                throw new ArgumentNullException("ranges");

            var newClass = (CharClassPattern)this.Copy();
            if (newClass.Subtracted == null)
                newClass.Subtracted = new CharClassPattern(false,ranges);
            else
                newClass.Subtracted = newClass.Subtracted.AddRanges(ranges);

            return newClass;
        }

        public CharClassPattern SubtractChars(params CharOrEscape[] chars)
        {
            if (chars == null || chars.Length == 0)
                throw new ArgumentNullException("chars");

            var newClass = (CharClassPattern)this.Copy();
            if (newClass.Subtracted == null)
                newClass.Subtracted = new CharClassPattern(false, chars);
            else
                newClass.Subtracted = newClass.Subtracted.AddChars(chars);

            return newClass;
        }

        public CharClassPattern SubtractAsciiChars(params char[] chars)
        {
            if (chars == null || chars.Length == 0)
                throw new ArgumentNullException("chars");

            var newClass = (CharClassPattern)this.Copy();
            if (newClass.Subtracted == null)
                newClass.Subtracted = new CharClassPattern(false);

            newClass.Subtracted = newClass.Subtracted.AddAsciiChars(chars);

            return newClass;
        }

        public CharClassPattern SubtractAsciiChars(params byte[] ascii)
        {
            if (ascii == null || ascii.Length == 0)
                throw new ArgumentNullException("chars");

            var newClass = (CharClassPattern)this.Copy();
            if (newClass.Subtracted == null)
                newClass.Subtracted = new CharClassPattern(false);

            newClass.Subtracted = newClass.Subtracted.AddAsciiChars(ascii);

            return newClass;
        }

        public CharClassPattern SubtractUnicodeChars(params char[] chars)
        {
            if (chars == null || chars.Length == 0)
                throw new ArgumentNullException("chars");

            var newClass = (CharClassPattern)this.Copy();
            if (newClass.Subtracted == null)
                newClass.Subtracted = new CharClassPattern(false);

            newClass.Subtracted = newClass.Subtracted.AddUnicodeChars(chars);

            return newClass;
        }

        public CharClassPattern SubtractUnicodeChars(params ushort[] unicodes)
        {
            if (unicodes == null || unicodes.Length == 0)
                throw new ArgumentNullException("chars");

            var newClass = (CharClassPattern)this.Copy();
            if (newClass.Subtracted == null)
                newClass.Subtracted = new CharClassPattern(false);

            newClass.Subtracted = newClass.Subtracted.AddUnicodeChars(unicodes);

            return newClass;
        }

        public CharClassPattern SubtractClass(CharClassPattern charClass)
        {
            if (charClass.GetRepeatExpr() != "")
                throw new ArgumentException("Subtracted char class can't be repeated!");

            var newClass = (CharClassPattern)this.Copy();
            if (newClass.Subtracted == null)
                newClass.Subtracted = charClass;
            else
                newClass.Subtracted = newClass.Subtracted.AddClass(charClass);
            return newClass;
        }



        public CharClassPattern Negate()
        {
            var newClass = (CharClassPattern)this.Copy ();
            newClass.IsNegative = !newClass.IsNegative;
            return newClass;
        }

        public static CharClassPattern operator -(CharClassPattern charClass1, CharClassPattern charClass2)
            => charClass1.SubtractClass(charClass2);

        public static CharClassPattern operator -(CharClassPattern charClass1, (CharOrEscape from, CharOrEscape to) range)
            => charClass1.SubtractRanges(range);

        public static CharClassPattern operator -(CharClassPattern charClass1, char c)
            => charClass1.SubtractChars(c);

        public static CharClassPattern operator &(CharClassPattern charClass1, (CharOrEscape from, CharOrEscape to) range)
            => charClass1.AddRanges(range);

        public static CharClassPattern operator &(CharClassPattern charClass1, CharOrEscape c)
            => charClass1.AddChars(c);

        public static CharClassPattern operator &(CharClassPattern charClass1, CharClassPattern charClass2)
            => charClass1.AddClass(charClass2);

        public static CharClassPattern operator !(CharClassPattern charClass)
            => charClass.Negate();

        public override string Expression
        {
            get
            {
                string s = "";
                foreach (var c in Chars)
                    if (c.ToString() == "]")
                        s = c + s;  // but ] as start to avoid possible errors when there is \[
                    else
                        s += EscapeInCharClass(c);

                foreach (var r in Ranges)
                    s += EscapeInCharClass(r.Min) + "-" + EscapeInCharClass(r.Max);

                if (Subtracted == null)
                    return $"[{(IsNegative ? "^" : "") + s}]" + GetRepeatExpr();

                return $"[{(IsNegative ? "^" : "") + s}-{Subtracted.Expression}]" + GetRepeatExpr();
            }
        }

    }

}
