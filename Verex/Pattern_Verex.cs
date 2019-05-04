using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace RegexBuilder
{
    public abstract partial class Pattern
    {


        public Regex ToRegex() 
            => new Regex(Expression, RegexOptions.Multiline);

        public Regex ToRegex(RegexOptions options) 
            => new Regex(Expression, options);

        public Regex ToRegex(RegexOptions options, TimeSpan matchTimeout) 
            => new Regex(Expression, options, matchTimeout);

        public bool IsMatch(string input)
            => Regex.IsMatch(input, this.Expression, RegexOptions.Multiline);

        public bool IsMatch(string input, RegexOptions options)
            => Regex.IsMatch(input, this.Expression, options);

        public bool IsMatch(string input, RegexOptions options, TimeSpan matchTimeout)
            => Regex.IsMatch(input, this.Expression, options, matchTimeout);

        public bool IsMatch(string input, int startAt) 
            => new Regex(this.Expression,  RegexOptions.Multiline).IsMatch(input, startAt);

        public bool IsMatch(string input, int startAt, RegexOptions options) 
            => new Regex(this.Expression, options).IsMatch(input, startAt);

        public bool IsMatch(string input, int startAt, RegexOptions options, TimeSpan matchTimeout) 
            => new Regex(this.Expression, options, matchTimeout).IsMatch(input, startAt);

        public Match Match(string input, RegexOptions options, TimeSpan matchTimeout)
            => Regex.Match(input, this.Expression, options, matchTimeout);

        public Match Match(string input)
            => Regex.Match(input, this.Expression, RegexOptions.Multiline);

        public Match Match(string input, RegexOptions options)
           => Regex.Match(input, this.Expression, options);
        
        public Match Match(string input, int startAt) 
            => new Regex(this.Expression,  RegexOptions.Multiline).Match(input, startAt);

        public Match Match(string input, int startAt, RegexOptions options) 
            => new Regex(this.Expression, options).Match(input, startAt);

        public Match Match(string input, int startAt, RegexOptions options, TimeSpan matchTimeout)
            => new Regex(this.Expression, options, matchTimeout).Match(input, startAt);

        public Match Match(string input, int startAt, int length) 
            => new Regex(this.Expression,  RegexOptions.Multiline).Match(input, startAt, length);

        public Match Match(string input, int startAt, int length, RegexOptions options)
            => new Regex(this.Expression, options).Match(input, startAt, length);

        public Match Match(string input, int startAt, int length, RegexOptions options, TimeSpan matchTimeout)
            => new Regex(this.Expression, options, matchTimeout).Match(input, startAt, length);

        public MatchCollection Matches(string input, RegexOptions options, TimeSpan matchTimeout)
            => Regex.Matches(input, this.Expression, options, matchTimeout);

        public MatchCollection Matches(string input, RegexOptions options)
            => Regex.Matches(input, this.Expression, options);

        public MatchCollection Matches(string input)
            => Regex.Matches(input, this.Expression, RegexOptions.Multiline);

        public string Replace(string input, ReplacePattern replacement, RegexOptions options)
            => Regex.Replace(input, this.Expression, replacement.Expression, options);

        public string Replace(string input, ReplacePattern replacement)
            => Regex.Replace(input, this.Expression, replacement.Expression, RegexOptions.Multiline);

        public string Replace(string input, MatchEvaluator evaluator, RegexOptions options, TimeSpan matchTimeout)
            => Regex.Replace(input, this.Expression, evaluator, options, matchTimeout);

        public string Replace(string input, ReplacePattern replacement, RegexOptions options, TimeSpan matchTimeout)
            => Regex.Replace(input, this.Expression, replacement.Expression, options, matchTimeout);

        public string Replace(string input, MatchEvaluator evaluator)
            => Regex.Replace(input, this.Expression, evaluator, RegexOptions.Multiline);

        public string Replace(string input, MatchEvaluator evaluator, RegexOptions options)
            => Regex.Replace(input, this.Expression, evaluator, options);

        public string[] Split(string input, RegexOptions options, TimeSpan matchTimeout)
            => Regex.Split(input, this.Expression, options, matchTimeout);

        public string[] Split(string input, RegexOptions options)
            => Regex.Split(input, this.Expression, options);

        public string[] Split(string input)
            => Regex.Split(input, this.Expression, RegexOptions.Multiline);

        public string[] GetGroupNames() 
            => new Regex(this.Expression,  RegexOptions.Multiline).GetGroupNames();

        public int[] GetGroupNumbers() 
            => new Regex(this.Expression,  RegexOptions.Multiline).GetGroupNumbers();

        public string GroupNameFromNumber(int i)             
            => new Regex(this.Expression,  RegexOptions.Multiline).GroupNameFromNumber(i);

        public int GroupNumberFromName(string name) 
            => new Regex(this.Expression,  RegexOptions.Multiline).GroupNumberFromName(name);

        public MatchCollection Matches(string input, int startAt) 
            => new Regex(this.Expression, RegexOptions.Multiline).Matches(input, startAt);

        public MatchCollection Matches(string input, int startAt, RegexOptions options) 
            => new Regex(this.Expression, options).Matches(input, startAt);

        public MatchCollection Matches(string input, int startAt, RegexOptions options, TimeSpan matchTimeout) 
            => new Regex(this.Expression, options, matchTimeout).Matches(input, startAt);

        public string Replace(string input, ReplacePattern replacement, int count, int startAt)
            => new Regex(this.Expression,  RegexOptions.Multiline).Replace(input, replacement.Expression, count, startAt);

        public string Replace(string input, ReplacePattern replacement, int count)
             => new Regex(this.Expression,  RegexOptions.Multiline).Replace(input, replacement.Expression, count);

        public string Replace(string input, MatchEvaluator evaluator, int count, int startAt)
            => new Regex(this.Expression,  RegexOptions.Multiline).Replace(input, evaluator, count, startAt);

        public string Replace(string input, MatchEvaluator evaluator, int count)
            => new Regex(this.Expression,  RegexOptions.Multiline).Replace(input, evaluator, count);

        public string[] Split(string input, int count, int startAt)
            => new Regex(this.Expression,  RegexOptions.Multiline).Split(input, count, startAt);

        public string[] Split(string input, int count)
            => new Regex(this.Expression,  RegexOptions.Multiline).Split(input, count);

        public string Replace(string input, ReplacePattern replacement, int count, int startAt, RegexOptions options)
            => new Regex(this.Expression, options).Replace(input, replacement.Expression, count, startAt);

        public string Replace(string input, ReplacePattern replacement, int count, RegexOptions options)
            => new Regex(this.Expression, options).Replace(input, replacement.Expression, count);

        public string Replace(string input, MatchEvaluator evaluator, int count, int startAt, RegexOptions options)
            => new Regex(this.Expression, options).Replace(input, evaluator, count, startAt);

        public string Replace(string input, MatchEvaluator evaluator, int count, RegexOptions options)
            => new Regex(this.Expression, options).Replace(input, evaluator, count);

        public string[] Split(string input, int count, int startAt, RegexOptions options)
            => new Regex(this.Expression, options).Split(input, count, startAt);

        public string[] Split(string input, int count, RegexOptions options)
            => new Regex(this.Expression, options).Split(input, count);

        public string Replace(string input, ReplacePattern replacement, int count, int startAt, RegexOptions options, TimeSpan matchTimeout)
            => new Regex(this.Expression, options, matchTimeout).Replace(input, replacement.Expression, count, startAt);

        public string Replace(string input, ReplacePattern replacement, int count, RegexOptions options, TimeSpan matchTimeout)
            => new Regex(this.Expression, options, matchTimeout).Replace(input, replacement.Expression, count);

        public string Replace(string input, MatchEvaluator evaluator, int count, int startAt, RegexOptions options, TimeSpan matchTimeout)
            => new Regex(this.Expression, options, matchTimeout).Replace(input, evaluator, count, startAt);

        public string Replace(string input, MatchEvaluator evaluator, int count, RegexOptions options, TimeSpan matchTimeout)
            => new Regex(this.Expression, options, matchTimeout).Replace(input, evaluator, count);

        public string[] Split(string input, int count, int startAt, RegexOptions options, TimeSpan matchTimeout)
            => new Regex(this.Expression, options, matchTimeout).Split(input, count, startAt);

        public string[] Split(string input, int count, RegexOptions options, TimeSpan matchTimeout)
            => new Regex(this.Expression, options, matchTimeout).Split(input, count);

    }

}
