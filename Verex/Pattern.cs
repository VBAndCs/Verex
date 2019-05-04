using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace RegexBuilder 
{

    public enum RepeatMode
    {
        Greedy = -1,
        Lazy = -2
    }

    public abstract partial class Pattern
    {
        protected struct Range<T>
        {
            public T Min;
            public T Max;

            public Range(T min, T max)
            {
                Min = min;
                Max = max;
            }

            public static implicit operator Range<T>((T from, T to) range)
            {
                return new Range<T>(range.from, range.to);
            }

            public static implicit operator (T from, T to) (Range<T> range)
            {
                return (range.Min, range.Max);
            }
        }

        #region "Repeat"        
        Range<int> Repetition = new Range<int>(-1, 0);
        protected RepeatMode RepeatBehavior = RepeatMode.Greedy;

        public Pattern this[ushort min, ushort max, RepeatMode behavior = RepeatMode.Greedy] => Repeat(min, max, behavior);
        public Pattern this[ushort times, RepeatMode behavior = RepeatMode.Greedy] => Repeat(times, behavior);

        public Pattern Repeat(ushort min, ushort max, RepeatMode behavior = RepeatMode.Greedy)
        {
            var repPattern = this.Copy();
            if (max == 0)
            {
                repPattern.Repetition.Min = min;
                repPattern.Repetition.Max = max;
            }
            else
            {
                repPattern.Repetition.Min = Math.Min(min, max);
                repPattern.Repetition.Max = Math.Max(min, max);
            }
            repPattern.RepeatBehavior = behavior;
            return repPattern;
        }

        public Pattern Repeat(ushort times, RepeatMode behavior = RepeatMode.Greedy)
        {
            if (behavior == 0)
                return Repeat(times, 0, RepeatMode.Greedy);

            var repPattern = this.Copy();
            repPattern.Repetition.Min = times;
            repPattern.Repetition.Max = times;
            repPattern.RepeatBehavior = behavior;
            return repPattern;
        }

        public Pattern AtLeast(ushort times, RepeatMode behavior = RepeatMode.Greedy)
        {
            var repPattern = this.Copy();
            repPattern.Repetition.Min = times;
            repPattern.Repetition.Max = 0;
            repPattern.RepeatBehavior = behavior;
            return repPattern;
        }

        public Pattern OnceOrMore(RepeatMode behavior = RepeatMode.Greedy) => Repeat(1, 0, behavior);
        public Pattern NoneOrMany(RepeatMode behavior = RepeatMode.Greedy) => Repeat(0, 0, behavior);
        public Pattern ZeroOrMore(RepeatMode behavior = RepeatMode.Greedy) => Repeat(0, 0, behavior);
        public Pattern NoneOrOnce(RepeatMode behavior = RepeatMode.Greedy) => Repeat(0, 1, behavior);
        public Pattern Maybe(RepeatMode behavior = RepeatMode.Greedy) => Repeat(0, 1, behavior);

        internal string GetRepeatExpr()
        {
            if (Repetition.Min == -1)
                return "";

            string s = "";
            if (Repetition.Min == 0)
                if (Repetition.Max == 0)
                    s = "*";
                else if (Repetition.Max == 1)
                    s = "?";
                else
                    s = "{0," + Repetition.Max + "}";
            else if (Repetition.Min == 1 && Repetition.Max == 0)
                s = "+";
            else if (Repetition.Max == 0)
                s = "{" + Repetition.Min + ",}";
            else if (Repetition.Max == Repetition.Min)
                s = "{" + $"{Repetition.Min}" + "}";
            else
                s = "{" + $"{Repetition.Min},{Repetition.Max}" + "}";

            return s + (RepeatBehavior == RepeatMode.Lazy ? "?" : "");
        }

        #endregion

        public virtual Pattern Copy() => (Pattern)MemberwiseClone();

        public Pattern Add(params Pattern[] patterns)
        {
            if (patterns == null || patterns.Length == 0)
                return this;

            if (patterns.Length == 1)
                return new AndPattern(this, patterns[0]);

            return new AndPattern(this, new AndPattern(patterns));
        }

        public Pattern Or(params Pattern[] patterns)
        {
            if (patterns == null || patterns.Length == 0)
                return this;

            if (patterns.Length == 1)
                return new OrPattern(this, patterns[0]);

            return new OrPattern(this, new AndPattern(patterns));
        }

        public static Pattern operator |(Pattern pattern1, Pattern pattern2)
            => pattern1.Or(pattern2);

        public static Pattern operator |(string text, Pattern pattern2)
            => new TextPattern(text).Or(pattern2);


        public static Pattern operator +(Pattern pattern1, Pattern pattern2)
             => pattern1.Add(pattern2);

        public static Pattern operator +(string text, Pattern pattern2)
            => new TextPattern(text).Add(pattern2);

        public static Pattern operator +(Pattern pattern, char c)
           => pattern.Add(new Symbol(c));

        public static Pattern operator +(char c, Pattern pattern)
           => new Symbol(c).Add(pattern);

        public static Pattern operator +(Pattern pattern, (CharOrEscape from, CharOrEscape to) range)
            => pattern.Add(new CharClassPattern(false, range));

        public static Pattern operator +((CharOrEscape from, CharOrEscape to) range, Pattern pattern)
            => new CharClassPattern(false, range).Add(pattern);
        
        public static Pattern operator &(string text, Pattern pattern2)
            => new TextPattern(text).Add(pattern2);

        public static Pattern operator &(Pattern pattern1, string text)
            => pattern1.Add(new TextPattern(text));

        public static implicit operator Pattern(string s) => new TextPattern(s);

        public virtual string Expression { get; }

        internal virtual bool DoNotEnclose { get; set; } = true;        

        public override string ToString() => Expression;

        public Pattern AsOne => new NonCapturingGroupPattern(this);
        public Pattern Combined => new NonCapturingGroupPattern(this);
        public Pattern Enclosed => new NonCapturingGroupPattern(this);

        public Pattern Grouped => new GroupPattern(this);
        public Pattern Group(string GroupName) 
            => new CaptureGroupPattern(GroupName, this);
        
        private Pattern EncloseAnd(params Pattern[] patterns)
            => new NonCapturingGroupPattern(this.Add(patterns));

        private Pattern EncloseOr(params Pattern[] patterns)
           => new NonCapturingGroupPattern(this.Or(patterns));
    }
        
}