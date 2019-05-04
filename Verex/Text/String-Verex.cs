using System;
using static RegexBuilder.Patterns;

namespace RegexBuilder
{
    public static class StringVerex
    {
        public static Pattern AsItIs(this string text) 
            => new TextPattern(text);

        public static ReplacePattern AsReplace(this string text) 
            => ReplacePattern.ReplaceText(text);

        public static Pattern AsWholeWord(this string text) 
            => Symbols.WordEdge + new TextPattern(text) + Symbols.WordEdge;

        public static Pattern AsWordStart(this string text) 
            => Symbols.WordEdge + new TextPattern(text);

        public static Pattern AsWordEnd(this string text) 
            => new TextPattern(text) + Symbols.WordEdge;

        public static Pattern AsWholeLine(this string text)
            => Symbols.StartOfLine + new TextPattern(text) + Symbols.EndOfLine;

        public static Pattern AsLineStart(this string text)
            => Symbols.StartOfLine + new TextPattern(text);

        public static Pattern AsLineEnd(this string text) 
            => new TextPattern(text) + Symbols.EndOfLine;

        public static Pattern AsTextStart(this string text)
            => Symbols.StartOfText + new TextPattern(text);

        public static Pattern AsTextEnd(this string text) 
            => new TextPattern(text) + Symbols.EndOfText;

        public static Pattern AsEndOfLastNonEmptyLine(this string text) 
            => new TextPattern(text) + Symbols.EndOfLastNonEmptyLine;

        #region "Repeat"        

        public static Pattern Repeat(this string text, ushort min, ushort max, RepeatMode behavior = RepeatMode.Greedy)
              => new TextPattern(text)[min, max, behavior];

        public static Pattern Repeat(this string text, ushort times, RepeatMode behavior = RepeatMode.Greedy)
            => new TextPattern(text)[times, behavior];

        public static Pattern AtLeast(this string text, ushort times, RepeatMode behavior = RepeatMode.Greedy)
            => new TextPattern(text).AtLeast(times, behavior);

        public static Pattern OnceOrMore(this string text, RepeatMode behavior = RepeatMode.Greedy) 
            => new TextPattern(text).Repeat(1, 0, behavior);

        public static Pattern NoneOrMany(this string text, RepeatMode behavior = RepeatMode.Greedy) 
            => new TextPattern(text).Repeat(0, 0, behavior);
        public static Pattern ZeroOrMore(this string text, RepeatMode behavior = RepeatMode.Greedy) 
            => new TextPattern(text).Repeat(0, 0, behavior);

        public static Pattern NoneOrOnce(this string text, RepeatMode behavior = RepeatMode.Greedy) 
            => new TextPattern(text).Repeat(0, 1, behavior);

        public static Pattern Maybe(this string text, RepeatMode behavior = RepeatMode.Greedy)
            => new TextPattern(text).Repeat(0, 1, behavior);

        #endregion

        public static Pattern AnyOfChars(this string text, string separator = "")
        {
            var chars = (separator == ""? text: text.Replace(separator, ""));
            return new CharClassPattern(false,Array.ConvertAll(chars.ToCharArray(), item => (CharOrEscape)item));
        }

        public static Pattern NoneOfChars(this string text,  string separator = "")
        {
            var chars = (separator == "" ? text : text.Replace(separator, ""));
            return new CharClassPattern(true, Array.ConvertAll(chars.ToCharArray(), item => (CharOrEscape)item));
        }

        public static Pattern Group(this string text) 
            => new GroupPattern(new TextPattern(text));

        public static Pattern Group(this string text, string GroupName)
            => new CaptureGroupPattern(GroupName, new TextPattern(text));

        public static Pattern AsBackRef(this string groupName)
            => new BackReference(groupName);

        public static Pattern Enclose(this string text)
            => new NonCapturingGroupPattern(new TextPattern(text));

        public static OptionsGroupPattern Options(this string text) 
            => new OptionsGroupPattern(new TextPattern(text));

        public static AssertionPattern Assert(this string text) 
            => new AssertionPattern(new TextPattern(text));

        public static CommentPattern AsComment(this string text)
            => new CommentPattern(text);

        public static TextPattern AsItIs(this char c)
            => new TextPattern(c.ToString());

        public static CharClassPattern AsCharClass(this char c)
            => new CharClassPattern(false, c);

        public static CharClassPattern Negate(this char c)
            => new CharClassPattern(true, c);
    }
}
