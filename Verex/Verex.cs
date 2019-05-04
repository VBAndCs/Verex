using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using static RegexBuilder.Patterns;

namespace RegexBuilder
{
    //
    // Summary:
    //     Represents an immutable verbal regular expression.
    public class Verex
    {

        public static TimeSpan InfiniteMatchTimeout => Regex.InfiniteMatchTimeout;

        Regex rgx;

        public Verex(Pattern pattern) => rgx = new Regex(pattern.Expression, RegexOptions.Multiline);

        public Verex(Pattern pattern, RegexOptions options) => rgx = new Regex(pattern.Expression, options);

        public Verex(Pattern pattern, RegexOptions options, TimeSpan matchTimeout) 
            => rgx = new Regex(pattern.Expression, options, matchTimeout);

        public static int CacheSize
        {
            get => Regex.CacheSize;
            set => Regex.CacheSize = value;
        }
        
        public TimeSpan MatchTimeout => rgx.MatchTimeout;

        public RegexOptions Options => rgx.Options;

        public bool RightToLeft => rgx.RightToLeft;

        public static bool IsMatch(string input, Pattern pattern)
            => Regex.IsMatch(input, pattern.Expression, RegexOptions.Multiline);

        public static bool IsMatch(string input, Pattern pattern, RegexOptions options)
            => Regex.IsMatch(input, pattern.Expression, options);

        public static bool IsMatch(string input, Pattern pattern, RegexOptions options, TimeSpan matchTimeout)
            => Regex.IsMatch(input, pattern.Expression, options, matchTimeout);

        public static Match Match(string input, Pattern pattern, RegexOptions options, TimeSpan matchTimeout)
            => Regex.Match(input, pattern.Expression, options, matchTimeout);

        public static Match Match(string input, Pattern pattern)
            => Regex.Match(input, pattern.Expression, RegexOptions.Multiline);

        public static Match Match(string input, Pattern pattern, RegexOptions options)
           => Regex.Match(input, pattern.Expression, options);

        public static MatchCollection Matches(string input, Pattern pattern, RegexOptions options, TimeSpan matchTimeout)
            => Regex.Matches(input, pattern.Expression, options, matchTimeout);

        public static MatchCollection Matches(string input, Pattern pattern, RegexOptions options)
            => Regex.Matches(input, pattern.Expression, options);

        public static MatchCollection Matches(string input, Pattern pattern)
            => Regex.Matches(input, pattern.Expression, RegexOptions.Multiline);

        public static string Replace(string input, Pattern pattern, ReplacePattern replacement, RegexOptions options)
            => Regex.Replace(input, pattern.Expression, replacement.Expression, options);

        public static string Replace(string input, Pattern pattern, ReplacePattern replacement)
            => Regex.Replace(input, pattern.Expression, replacement.Expression, RegexOptions.Multiline);

        public static string Replace(string input, Pattern pattern, MatchEvaluator evaluator, RegexOptions options, TimeSpan matchTimeout)
            => Regex.Replace(input, pattern.Expression, evaluator, options, matchTimeout);

        public static string Replace(string input, Pattern pattern, ReplacePattern replacement, RegexOptions options, TimeSpan matchTimeout)
            => Regex.Replace(input, pattern.Expression, replacement.Expression, options, matchTimeout);

        public static string Replace(string input, Pattern pattern, MatchEvaluator evaluator)
            => Regex.Replace(input, pattern.Expression, evaluator, RegexOptions.Multiline);

        public static string Replace(string input, Pattern pattern, MatchEvaluator evaluator, RegexOptions options)
            => Regex.Replace(input, pattern.Expression, evaluator, options);

        public static string[] Split(string input, Pattern pattern, RegexOptions options, TimeSpan matchTimeout)
            => Regex.Split(input, pattern.Expression, options, matchTimeout);

        public static string[] Split(string input, Pattern pattern, RegexOptions options)
            => Regex.Split(input, pattern.Expression, options);

        public static string[] Split(string input, Pattern pattern)
            => Regex.Split(input, pattern.Expression, RegexOptions.Multiline);

        public string[] GetGroupNames() => rgx.GetGroupNames();

        public int[] GetGroupNumbers() => rgx.GetGroupNumbers();

        public string GroupNameFromNumber(int i) => rgx.GroupNameFromNumber(i);

        public int GroupNumberFromName(string name) => rgx.GroupNumberFromName(name);

        public bool IsMatch(string input) => rgx.IsMatch(input);

        public bool IsMatch(string input, int startAt) => rgx.IsMatch(input, startAt);

        public Match Match(string input) => rgx.Match(input);

        public Match Match(string input, int startAt) => rgx.Match(input, startAt);

        public Match Match(string input, int startAt, int length) 
            => rgx.Match(input, startAt, length);

        public MatchCollection Matches(string input, int startAt) 
            => rgx.Matches(input, startAt);

        public MatchCollection Matches(string input) 
            => rgx.Matches(input);

        public string Replace(string input, ReplacePattern replacement, int count, int startAt)
            => rgx.Replace(input, replacement.Expression, count, startAt);

        public string Replace(string input, MatchEvaluator evaluator)
            => rgx.Replace(input, evaluator);

        public string Replace(string input, ReplacePattern replacement, int count)
             => rgx.Replace(input, replacement.Expression, count);

        public string Replace(string input, MatchEvaluator evaluator, int count, int startAt)
            => rgx.Replace(input, evaluator, count, startAt);

        public string Replace(string input, MatchEvaluator evaluator, int count)
            => rgx.Replace(input, evaluator, count);

        public string Replace(string input, ReplacePattern replacement)
            => rgx.Replace(input, replacement.Expression);

        public string[] Split(string input, int count, int startAt)
            => rgx.Split(input, count, startAt);

        public string[] Split(string input, int count)
            => rgx.Split(input, count);

        public string[] Split(string input) => rgx.Split(input);

        public override string ToString() => rgx.ToString();

        public static bool ContainsBalancedBrackets(string input, string openbracket, string closebracket)
        {
            char open;
            if (openbracket.Length == 1)
                open = openbracket[0];
            else
            {
                open = FindUnusedChars(input);
                input = input.Replace(openbracket, open.ToString());
            }

            char close;
            if (closebracket.Length == 1)
                close = closebracket[0];
            else
            {
                close = FindUnusedChars(input);
                input = input.Replace(closebracket, close.ToString());
            }

            return BalancedBrackets(open, close).IsMatch(input);
        }

        private static char FindUnusedChars(string input)
        {
            for (int i = 0; i < ushort.MaxValue; i++)
            {
                var c = (char)i;
                if (!input.Contains(c.ToString()))
                    return c;
            }
            throw new Exception("Not found");
        }

        public static List<Content> BalancedContents(string input, string openbracket, string closebracket)
        {
            char open;
            if (openbracket.Length == 1)
                open = openbracket[0];
            else
            {
                open = FindUnusedChars(input);
                input = input.Replace(openbracket, open.ToString());
            }

            char close;
            if (closebracket.Length == 1)
                close = closebracket[0];
            else if (closebracket == openbracket)
                close = open;
            else
            {
                close = FindUnusedChars(input);
                input = input.Replace(closebracket, close.ToString());
            }

            var  m = BalancedBracketsContent(open, close).Match(input);

            if (m.Success)
            {
                var contents = new List<Content>();
                var G = m.Groups;
                Group grp = G[G.Count - 1];

                foreach (Capture c in grp.Captures)
                    contents.Add(new Content((ushort)c.Index, (ushort)c.Length, c.Value));

                contents.Sort((Content c1, Content c2) =>
                {
                    if (c1.Index < c2.Index)
                        return -1;
                    return 1;
                });

                bool openOk = openbracket == open.ToString();
                bool closeOk = closebracket == close.ToString();

                if (openOk && closeOk)
                    return contents;

                var sb = new StringBuilder(input.Length * 2);
                string theBracket = "";
                char theChar = '\0';

                if (closebracket == openbracket || closeOk)
                {
                    theBracket = openbracket;
                    theChar = open;
                }
                else if (openOk)
                {
                    theBracket = closebracket;
                    theChar = close;
                }

                ushort totalOffset = 0;

                if (theBracket != "")
                {
                    ushort offset = (ushort)(theBracket.Length - 1);                    
                    for (ushort i = 0; i < input.Length; i++)
                    {
                        if (input[i] == theChar)
                        {
                            sb.Append(theBracket);                            
                            foreach (Content c in contents)
                                c.Adjust((ushort)(i + totalOffset), offset);

                            totalOffset += offset;
                        }
                        else
                            sb.Append(input[i]);
                    }
                }
                else                                  
                {
                    ushort openOffset = (ushort)(openbracket.Length - 1);
                    ushort closeOffset = (ushort)(closebracket.Length - 1);

                    for (ushort i = 0; i < input.Length; i++)
                    {
                        if (input[i] == open)
                        {
                            sb.Append(openbracket);
                            foreach (Content c in contents)
                                c.Adjust((ushort)(i + totalOffset), openOffset);

                            totalOffset += openOffset;
                        }
                        else if (input[i] == close)
                        {
                            sb.Append(closebracket);
                            foreach (Content c in contents)
                                c.Adjust((ushort)(i + totalOffset), closeOffset);

                            totalOffset += closeOffset;
                        }
                        else
                            sb.Append(input[i]);
                    }
                }

                input = sb.ToString();
                foreach (Content c in contents)
                    c.UpdateValue(input);

                return contents;

            }

            return null;
        }
    }
}
