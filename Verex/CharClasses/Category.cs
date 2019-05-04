namespace RegexBuilder
{
    public class Category: Symbol
    {
        public Category(string cat): base(cat, true, true)
        {
        }

        public Category NegateCategory()
        {
            if (Str.StartsWith(@"\p"))
                return new Category(Str.Replace(@"\p", @"\P"));

            return new Category(Str.Replace(@"\P", @"\p"));
        }

        public Pattern Intersect(Category cat)
            =>  Patterns.AssertNextIs(this) + cat;

        public Pattern UnionAndIntersect(Category unionWithCat, Category intersectWithCat)
            => Patterns.AssertNextIs(this | unionWithCat) + intersectWithCat;

        public static implicit operator CharClassPattern (Category cat)
            => new CharClassPattern (false , new CharOrEscape(cat.Expression));

        public static CharClassPattern operator -(Category cat1, Category cat2)
        => ((CharClassPattern)cat1).SubtractClass((CharClassPattern)cat2);

        public static CharClassPattern operator -(Category cat, CharClassPattern charClass)
            => ((CharClassPattern)cat).SubtractClass(charClass);

        public static CharClassPattern operator -(Category cat, (CharOrEscape from, CharOrEscape to) range)
            => ((CharClassPattern)cat).SubtractRanges(range);

        public static CharClassPattern operator -(Category cat, CharOrEscape c)
            => ((CharClassPattern)cat).SubtractChars(c);


        public static CharClassPattern operator &(Category cat1, Category cat2)
    => ((CharClassPattern)cat1).AddClass((CharClassPattern)cat2);

        public static CharClassPattern operator &(Category cat, CharClassPattern charClass)
            => ((CharClassPattern)cat).AddClass(charClass);

        public static CharClassPattern operator &(Category cat, (CharOrEscape from, CharOrEscape to) range)
            => ((CharClassPattern)cat).AddRanges(range);

        public static CharClassPattern operator &(Category cat, CharOrEscape c)
            => ((CharClassPattern)cat).AddChars(c);


    }
}