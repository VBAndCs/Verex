using System;
using System.Collections.Generic;
using System.Text;

namespace RegexBuilder
{
    public static partial class Patterns
    {
        /// <summary>
        /// Unicode Categories
        /// </summary>
        public static class Unicode
        {

            public static Symbol UnicodeChar(ushort unicode)
            {
                var x = @"\u" + Convert.ToString(unicode, 16).PadLeft(4, '0');
                return new Symbol(x, encoded: true);
            }

            /// <summary>
            /// Use this method to convert a char to its regex unicode representation.
            /// This solves some problems with some matching chars, i.e 
            /// when writing ] in a char class.
            /// </summary>
            /// <param name="c"></param>
            /// <returns></returns>
            public static Symbol UnicodeChar(char c)
            {
                var x = @"\u" + Convert.ToString(c, 16).PadLeft(4, '0');
                return new Symbol(x, encoded: true);
            }

            internal static string[] UniCategories = {
                                                "Lu", "Ll", "Lt", "Lm", "Lo", "L",
                                                "Mn", "Mc", "Me", "M",
                                                "Nd", "Nl", "No", "N",
                                                "Pc", "Pd", "Ps", "Pe", "Pi", "Pf", "Po", "P",
                                                "Sm", "Sc", "Sk", "So", "S",
                                                "Zs", "Zl", "Zp", "Z",
                                                 "Cf", "Cs", "Co", "Cn", "Cc", "C",
                                                 "IsBasicLatin",
                                                 "IsLatin-1Supplement",
                                                 "IsLatinExtended-A",
                                                 "IsLatinExtended-B",
                                                 "IsIPAExtensions",
                                                 "IsSpacingModifierLetters",
                                                 "IsCombiningDiacriticalMarks",
                                                 "IsGreek",
                                                 "IsGreekandCoptic",
                                                 "IsCyrillic",
                                                 "IsCyrillicSupplement",
                                                 "IsArmenian",
                                                 "IsHebrew",
                                                 "IsArabic",
                                                 "IsSyriac",
                                                 "IsThaana",
                                                 "IsDevanagari",
                                                 "IsBengali",
                                                 "IsGurmukhi",
                                                 "IsGujarati",
                                                 "IsOriya",
                                                 "IsTamil",
                                                 "IsTelugu",
                                                 "IsKannada",
                                                 "IsMalayalam",
                                                 "IsSinhala",
                                                 "IsThai",
                                                 "IsLao",
                                                 "IsTibetan",
                                                 "IsMyanmar",
                                                 "IsGeorgian",
                                                 "IsHangulJamo",
                                                 "IsEthiopic",
                                                 "IsCherokee",
                                                 "IsUnifiedCanadianAboriginalSyllabics",
                                                 "IsOgham",
                                                 "IsRunic",
                                                 "IsTagalog",
                                                 "IsHanunoo",
                                                 "IsBuhid",
                                                 "IsTagbanwa",
                                                 "IsKhmer",
                                                 "IsMongolian",
                                                 "IsLimbu",
                                                 "IsTaiLe",
                                                 "IsKhmerSymbols",
                                                 "IsPhoneticExtensions",
                                                 "IsLatinExtendedAdditional",
                                                 "IsGreekExtended",
                                                 "IsGeneralPunctuation",
                                                 "IsSuperscriptsandSubscripts",
                                                 "IsCurrencySymbols",
                                                 "IsCombiningDiacriticalMarksforSymbols",
                                                 "IsCombiningMarksforSymbols",
                                                 "IsLetterlikeSymbols",
                                                 "IsNumberForms",
                                                 "IsArrows",
                                                 "IsMathematicalOperators",
                                                 "IsMiscellaneousTechnical",
                                                 "IsControlPictures",
                                                 "IsOpticalCharacterRecognition",
                                                 "IsEnclosedAlphanumerics",
                                                 "IsBoxDrawing",
                                                 "IsBlockElements",
                                                 "IsGeometricShapes",
                                                 "IsMiscellaneousSymbols",
                                                 "IsDingbats",
                                                 "IsMiscellaneousMathematicalSymbols-A",
                                                 "IsSupplementalArrows-A",
                                                 "IsBraillePatterns",
                                                 "IsSupplementalArrows-B",
                                                 "IsMiscellaneousMathematicalSymbols-B",
                                                 "IsSupplementalMathematicalOperators",
                                                 "IsMiscellaneousSymbolsandArrows",
                                                 "IsCJKRadicalsSupplement",
                                                 "IsKangxiRadicals",
                                                 "IsIdeographicDescriptionCharacters",
                                                 "IsCJKSymbolsandPunctuation",
                                                 "IsHiragana",
                                                 "IsKatakana",
                                                 "IsBopomofo",
                                                 "IsHangulCompatibilityJamo",
                                                 "IsKanbun",
                                                 "IsBopomofoExtended",
                                                 "IsKatakanaPhoneticExtensions",
                                                 "IsEnclosedCJKLettersandMonths",
                                                 "IsCJKCompatibility",
                                                 "IsCJKUnifiedIdeographsExtensionA",
                                                 "IsYijingHexagramSymbols",
                                                 "IsCJKUnifiedIdeographs",
                                                 "IsYiSyllables",
                                                 "IsYiRadicals",
                                                 "IsHangulSyllables",
                                                 "IsHighSurrogates",
                                                 "IsHighPrivateUseSurrogates",
                                                 "IsLowSurrogates",
                                                 "IsPrivateUseArea",
                                                 "IsCJKCompatibilityIdeographs",
                                                 "IsLettericPresentationForms",
                                                 "IsArabicPresentationForms-A",
                                                 "IsVariationSelectors",
                                                 "IsCombiningHalfMarks",
                                                 "IsCJKCompatibilityForms",
                                                 "IsSmallFormVariants",
                                                 "IsArabicPresentationForms-B",
                                                 "IsHalfwidthandFullwidthForms",
                                                 "IsSpecials"
            };

            public static Category UnicodeCategory(Categories Categories)
                 => new Category(@"\p{" + UniCategories[(int)Categories] + "}");

            public static Category NegativeUnicodeCategory(Categories Categories)
                  => new Category(@"\P{" + UniCategories[(int)Categories] + "}");

            public static Pattern Intersect(Categories category1, Categories category2)
                  => AssertNextIs(UnicodeCategory(category1)) + UnicodeCategory(category2);

            public static Pattern UnionAndIntersect(Categories unionCategory1, Categories unionCategory2, Categories intersectWithCategory)
                => AssertNextIs(UnicodeCategory(unionCategory1) | UnicodeCategory(unionCategory2))
                     + UnicodeCategory(intersectWithCategory);
            
            public static Category UppercaseLetter => new Category(@"\p{Lu}");
            public static Category LowercaseLetter => new Category(@"\p{Ll}");
            public static Category TitlecaseLetter => new Category(@"\p{Lt}");
            public static Category ModifierLetter => new Category(@"\p{Lm}");
            public static Category OtherLetter => new Category(@"\p{Lo}");
            public static Category Letter => new Category(@"\p{L}");
            public static Category NonspacingMark => new Category(@"\p{Mn}");
            public static Category SpacingCombiningMark => new Category(@"\p{Mc}");
            public static Category EnclosingMark => new Category(@"\p{Me}");
            public static Category Mark => new Category(@"\p{M}");
            public static Category DecimalDigitNumber => new Category(@"\p{Nd}");
            public static Category LetterNumber => new Category(@"\p{Nl}");
            public static Category OtherNumber => new Category(@"\p{No}");
            public static Category Number => new Category(@"\p{N}");
            public static Category ConnectorPunctuation => new Category(@"\p{Pc}");
            public static Category DashPunctuation => new Category(@"\p{Pd}");
            public static Category OpenPunctuation => new Category(@"\p{Ps}");
            public static Category ClosePunctuation => new Category(@"\p{Pe}");
            public static Category InitialquotePunctuation => new Category(@"\p{Pi}");
            public static Category FinalquotePunctuation => new Category(@"\p{Pf}");
            public static Category OtherPunctuation => new Category(@"\p{Po}");
            public static Category Punctuation => new Category(@"\p{P}");
            public static Category MathSymbol => new Category(@"\p{Sm}");
            public static Category CurrencySymbol => new Category(@"\p{Sc}");
            public static Category ModifierSymbol => new Category(@"\p{Sk}");
            public static Category OtherSymbol => new Category(@"\p{So}");
            public static Category Symbol => new Category(@"\p{S}");
            public static Category SpaceSeparator => new Category(@"\p{Zs}");
            public static Category LineSeparator => new Category(@"\p{Zl}");
            public static Category ParagraphSeparator => new Category(@"\p{Zp}");
            public static Category Separator => new Category(@"\p{Z}");
            public static Category Format => new Category(@"\p{Cf}");
            public static Category Surrogate => new Category(@"\p{Cs}");
            public static Category PrivateUse => new Category(@"\p{Co}");
            public static Category NotAssigned => new Category(@"\p{Cn}");
            public static Category OtherControl => new Category(@"\p{Cc}");
            public static Category Control => new Category(@"\p{C}");

            public static Category BasicLatin => new Category(@"\p{IsBasicLatin}");
            public static Category Latin_1Supplement => new Category(@"\p{IsLatin-1Supplement}");
            public static Category LatinExtended_A => new Category(@"\p{IsLatinExtended-A}");
            public static Category LatinExtended_B => new Category(@"\p{IsLatinExtended-B}");
            public static Category IPAExtensions => new Category(@"\p{IsIPAExtensions}");
            public static Category SpacingModifierLetters => new Category(@"\p{IsSpacingModifierLetters}");
            public static Category CombiningDiacriticalMarks => new Category(@"\p{IsCombiningDiacriticalMarks}");
            public static Category Greek => new Category(@"\p{IsGreek}");
            public static Category GreekandCoptic => new Category(@"\p{IsGreekandCoptic}");
            public static Category Cyrillic => new Category(@"\p{IsCyrillic}");
            public static Category CyrillicSupplement => new Category(@"\p{IsCyrillicSupplement}");
            public static Category Armenian => new Category(@"\p{IsArmenian}");
            public static Category Hebrew => new Category(@"\p{IsHebrew}");
            public static Category Arabic => new Category(@"\p{IsArabic}");
            public static Category Syriac => new Category(@"\p{IsSyriac}");
            public static Category Thaana => new Category(@"\p{IsThaana}");
            public static Category Devanagari => new Category(@"\p{IsDevanagari}");
            public static Category Bengali => new Category(@"\p{IsBengali}");
            public static Category Gurmukhi => new Category(@"\p{IsGurmukhi}");
            public static Category Gujarati => new Category(@"\p{IsGujarati}");
            public static Category Oriya => new Category(@"\p{IsOriya}");
            public static Category Tamil => new Category(@"\p{IsTamil}");
            public static Category Telugu => new Category(@"\p{IsTelugu}");
            public static Category Kannada => new Category(@"\p{IsKannada}");
            public static Category Malayalam => new Category(@"\p{IsMalayalam}");
            public static Category Sinhala => new Category(@"\p{IsSinhala}");
            public static Category Thai => new Category(@"\p{IsThai}");
            public static Category Lao => new Category(@"\p{IsLao}");
            public static Category Tibetan => new Category(@"\p{IsTibetan}");
            public static Category Myanmar => new Category(@"\p{IsMyanmar}");
            public static Category Georgian => new Category(@"\p{IsGeorgian}");
            public static Category HangulJamo => new Category(@"\p{IsHangulJamo}");
            public static Category Ethiopic => new Category(@"\p{IsEthiopic}");
            public static Category Cherokee => new Category(@"\p{IsCherokee}");
            public static Category UnifiedCanadianAboriginalSyllabics => new Category(@"\p{IsUnifiedCanadianAboriginalSyllabics}");
            public static Category Ogham => new Category(@"\p{IsOgham}");
            public static Category Runic => new Category(@"\p{IsRunic}");
            public static Category Tagalog => new Category(@"\p{IsTagalog}");
            public static Category Hanunoo => new Category(@"\p{IsHanunoo}");
            public static Category Buhid => new Category(@"\p{IsBuhid}");
            public static Category Tagbanwa => new Category(@"\p{IsTagbanwa}");
            public static Category Khmer => new Category(@"\p{IsKhmer}");
            public static Category Mongolian => new Category(@"\p{IsMongolian}");
            public static Category Limbu => new Category(@"\p{IsLimbu}");
            public static Category TaiLe => new Category(@"\p{IsTaiLe}");
            public static Category KhmerSymbols => new Category(@"\p{IsKhmerSymbols}");
            public static Category PhoneticExtensions => new Category(@"\p{IsPhoneticExtensions}");
            public static Category LatinExtendedAdditional => new Category(@"\p{IsLatinExtendedAdditional}");
            public static Category GreekExtended => new Category(@"\p{IsGreekExtended}");
            public static Category GeneralPunctuation => new Category(@"\p{IsGeneralPunctuation}");
            public static Category SuperscriptsandSubscripts => new Category(@"\p{IsSuperscriptsandSubscripts}");
            public static Category CurrencySymbols => new Category(@"\p{IsCurrencySymbols}");
            public static Category CombiningDiacriticalMarksforSymbols => new Category(@"\p{IsCombiningDiacriticalMarksforSymbols}");
            public static Category CombiningMarksforSymbols => new Category(@"\p{IsCombiningMarksforSymbols}");
            public static Category LetterlikeSymbols => new Category(@"\p{IsLetterlikeSymbols}");
            public static Category NumberForms => new Category(@"\p{IsNumberForms}");
            public static Category Arrows => new Category(@"\p{IsArrows}");
            public static Category MathematicalOperators => new Category(@"\p{IsMathematicalOperators}");
            public static Category MiscellaneousTechnical => new Category(@"\p{IsMiscellaneousTechnical}");
            public static Category ControlPictures => new Category(@"\p{IsControlPictures}");
            public static Category OpticalCharacterRecognition => new Category(@"\p{IsOpticalCharacterRecognition}");
            public static Category EnclosedAlphanumerics => new Category(@"\p{IsEnclosedAlphanumerics}");
            public static Category BoxDrawing => new Category(@"\p{IsBoxDrawing}");
            public static Category BlockElements => new Category(@"\p{IsBlockElements}");
            public static Category GeometricShapes => new Category(@"\p{IsGeometricShapes}");
            public static Category MiscellaneousSymbols => new Category(@"\p{IsMiscellaneousSymbols}");
            public static Category Dingbats => new Category(@"\p{IsDingbats}");
            public static Category MiscellaneousMathematicalSymbols_A => new Category(@"\p{IsMiscellaneousMathematicalSymbols-A}");
            public static Category SupplementalArrows_A => new Category(@"\p{IsSupplementalArrows-A}");
            public static Category BraillePatterns => new Category(@"\p{IsBraillePatterns}");
            public static Category SupplementalArrows_B => new Category(@"\p{IsSupplementalArrows-B}");
            public static Category MiscellaneousMathematicalSymbols_B => new Category(@"\p{IsMiscellaneousMathematicalSymbols-B}");
            public static Category SupplementalMathematicalOperators => new Category(@"\p{IsSupplementalMathematicalOperators}");
            public static Category MiscellaneousSymbolsandArrows => new Category(@"\p{IsMiscellaneousSymbolsandArrows}");
            public static Category CJKRadicalsSupplement => new Category(@"\p{IsCJKRadicalsSupplement}");
            public static Category KangxiRadicals => new Category(@"\p{IsKangxiRadicals}");
            public static Category IdeographicDescriptionCharacters => new Category(@"\p{IsIdeographicDescriptionCharacters}");
            public static Category CJKSymbolsandPunctuation => new Category(@"\p{IsCJKSymbolsandPunctuation}");
            public static Category Hiragana => new Category(@"\p{IsHiragana}");
            public static Category Katakana => new Category(@"\p{IsKatakana}");
            public static Category Bopomofo => new Category(@"\p{IsBopomofo}");
            public static Category HangulCompatibilityJamo => new Category(@"\p{IsHangulCompatibilityJamo}");
            public static Category Kanbun => new Category(@"\p{IsKanbun}");
            public static Category BopomofoExtended => new Category(@"\p{IsBopomofoExtended}");
            public static Category KatakanaPhoneticExtensions => new Category(@"\p{IsKatakanaPhoneticExtensions}");
            public static Category EnclosedCJKLettersandMonths => new Category(@"\p{IsEnclosedCJKLettersandMonths}");
            public static Category CJKCompatibility => new Category(@"\p{IsCJKCompatibility}");
            public static Category CJKUnifiedIdeographsExtensionA => new Category(@"\p{IsCJKUnifiedIdeographsExtensionA}");
            public static Category YijingHexagramSymbols => new Category(@"\p{IsYijingHexagramSymbols}");
            public static Category CJKUnifiedIdeographs => new Category(@"\p{IsCJKUnifiedIdeographs}");
            public static Category YiSyllables => new Category(@"\p{IsYiSyllables}");
            public static Category YiRadicals => new Category(@"\p{IsYiRadicals}");
            public static Category HangulSyllables => new Category(@"\p{IsHangulSyllables}");
            public static Category HighSurrogates => new Category(@"\p{IsHighSurrogates}");
            public static Category HighPrivateUseSurrogates => new Category(@"\p{IsHighPrivateUseSurrogates}");
            public static Category LowSurrogates => new Category(@"\p{IsLowSurrogates}");
            public static Category PrivateUseArea => new Category(@"\p{IsPrivateUseArea}");
            public static Category CJKCompatibilityIdeographs => new Category(@"\p{IsCJKCompatibilityIdeographs}");
            public static Category LettericPresentationForms => new Category(@"\p{IsLettericPresentationForms}");
            public static Category ArabicPresentationForms_A => new Category(@"\p{IsArabicPresentationForms-A}");
            public static Category VariationSelectors => new Category(@"\p{IsVariationSelectors}");
            public static Category CombiningHalfMarks => new Category(@"\p{IsCombiningHalfMarks}");
            public static Category CJKCompatibilityForms => new Category(@"\p{IsCJKCompatibilityForms}");
            public static Category SmallFormVariants => new Category(@"\p{IsSmallFormVariants}");
            public static Category ArabicPresentationForms_B => new Category(@"\p{IsArabicPresentationForms-B}");
            public static Category HalfwidthandFullwidthForms => new Category(@"\p{IsHalfwidthandFullwidthForms}");
            public static Category Specials => new Category(@"\p{IsSpecials}");

            public static Category NonUppercaseLetter => new Category(@"\P{Lu}");
            public static Category NonLowercaseLetter => new Category(@"\P{Ll}");
            public static Category NonTitlecaseLetter => new Category(@"\P{Lt}");
            public static Category NonModifierLetter => new Category(@"\P{Lm}");
            public static Category NonOtherLetter => new Category(@"\P{Lo}");
            public static Category NonLetter => new Category(@"\P{L}");
            public static Category SpacingMark => new Category(@"\P{Mn}");
            public static Category NonSpacingCombiningMark => new Category(@"\P{Mc}");
            public static Category NonEnclosingMark => new Category(@"\P{Me}");
            public static Category NonMark => new Category(@"\P{M}");
            public static Category NonDecimalDigitNumber => new Category(@"\P{Nd}");
            public static Category NonLetterNumber => new Category(@"\P{Nl}");
            public static Category NonOtherNumber => new Category(@"\P{No}");
            public static Category NonNumber => new Category(@"\P{N}");
            public static Category NonConnectorPunctuation => new Category(@"\P{Pc}");
            public static Category NonDashPunctuation => new Category(@"\P{Pd}");
            public static Category NonOpenPunctuation => new Category(@"\P{Ps}");
            public static Category NonClosePunctuation => new Category(@"\P{Pe}");
            public static Category NonInitialquotePunctuation => new Category(@"\P{Pi}");
            public static Category NonFinalquotePunctuation => new Category(@"\P{Pf}");
            public static Category NonOtherPunctuation => new Category(@"\P{Po}");
            public static Category NonPunctuation => new Category(@"\P{P}");
            public static Category NonMathSymbol => new Category(@"\P{Sm}");
            public static Category NonCurrencySymbol => new Category(@"\P{Sc}");
            public static Category NonModifierSymbol => new Category(@"\P{Sk}");
            public static Category NonOtherSymbol => new Category(@"\P{So}");
            public static Category NonSymbol => new Category(@"\P{S}");
            public static Category NonSpaceSeparator => new Category(@"\P{Zs}");
            public static Category NonLineSeparator => new Category(@"\P{Zl}");
            public static Category NonParagraphSeparator => new Category(@"\P{Zp}");
            public static Category NonSeparator => new Category(@"\P{Z}");
            public static Category NonFormat => new Category(@"\P{Cf}");
            public static Category NonSurrogate => new Category(@"\P{Cs}");
            public static Category NonPrivateUse => new Category(@"\P{Co}");
            public static Category Assigned => new Category(@"\P{Cn}");
            public static Category NonOtherControl => new Category(@"\P{Cc}");
            public static Category NonControl => new Category(@"\P{C}");

            public static Category NonBasicLatin => new Category(@"\P{IsBasicLatin}");
            public static Category NonLatin_1Supplement => new Category(@"\P{IsLatin-1Supplement}");
            public static Category NonLatinExtended_A => new Category(@"\P{IsLatinExtended-A}");
            public static Category NonLatinExtended_B => new Category(@"\P{IsLatinExtended-B}");
            public static Category NonIPAExtensions => new Category(@"\P{IsIPAExtensions}");
            public static Category NonSpacingModifierLetters => new Category(@"\P{IsSpacingModifierLetters}");
            public static Category NonCombiningDiacriticalMarks => new Category(@"\P{IsCombiningDiacriticalMarks}");
            public static Category NonGreek => new Category(@"\P{IsGreek}");
            public static Category NonGreekandCoptic => new Category(@"\P{IsGreekandCoptic}");
            public static Category NonCyrillic => new Category(@"\P{IsCyrillic}");
            public static Category NonCyrillicSupplement => new Category(@"\P{IsCyrillicSupplement}");
            public static Category NonArmenian => new Category(@"\P{IsArmenian}");
            public static Category NonHebrew => new Category(@"\P{IsHebrew}");
            public static Category NonArabic => new Category(@"\P{IsArabic}");
            public static Category NonSyriac => new Category(@"\P{IsSyriac}");
            public static Category NonThaana => new Category(@"\P{IsThaana}");
            public static Category NonDevanagari => new Category(@"\P{IsDevanagari}");
            public static Category NonBengali => new Category(@"\P{IsBengali}");
            public static Category NonGurmukhi => new Category(@"\P{IsGurmukhi}");
            public static Category NonGujarati => new Category(@"\P{IsGujarati}");
            public static Category NonOriya => new Category(@"\P{IsOriya}");
            public static Category NonTamil => new Category(@"\P{IsTamil}");
            public static Category NonTelugu => new Category(@"\P{IsTelugu}");
            public static Category NonKannada => new Category(@"\P{IsKannada}");
            public static Category NonMalayalam => new Category(@"\P{IsMalayalam}");
            public static Category NonSinhala => new Category(@"\P{IsSinhala}");
            public static Category NonThai => new Category(@"\P{IsThai}");
            public static Category NonLao => new Category(@"\P{IsLao}");
            public static Category NonTibetan => new Category(@"\P{IsTibetan}");
            public static Category NonMyanmar => new Category(@"\P{IsMyanmar}");
            public static Category NonGeorgian => new Category(@"\P{IsGeorgian}");
            public static Category NonHangulJamo => new Category(@"\P{IsHangulJamo}");
            public static Category NonEthiopic => new Category(@"\P{IsEthiopic}");
            public static Category NonCherokee => new Category(@"\P{IsCherokee}");
            public static Category NonUnifiedCanadianAboriginalSyllabics => new Category(@"\P{IsUnifiedCanadianAboriginalSyllabics}");
            public static Category NonOgham => new Category(@"\P{IsOgham}");
            public static Category NonRunic => new Category(@"\P{IsRunic}");
            public static Category NonTagalog => new Category(@"\P{IsTagalog}");
            public static Category NonHanunoo => new Category(@"\P{IsHanunoo}");
            public static Category NonBuhid => new Category(@"\P{IsBuhid}");
            public static Category NonTagbanwa => new Category(@"\P{IsTagbanwa}");
            public static Category NonKhmer => new Category(@"\P{IsKhmer}");
            public static Category NonMongolian => new Category(@"\P{IsMongolian}");
            public static Category NonLimbu => new Category(@"\P{IsLimbu}");
            public static Category NonTaiLe => new Category(@"\P{IsTaiLe}");
            public static Category NonKhmerSymbols => new Category(@"\P{IsKhmerSymbols}");
            public static Category NonPhoneticExtensions => new Category(@"\P{IsPhoneticExtensions}");
            public static Category NonLatinExtendedAdditional => new Category(@"\P{IsLatinExtendedAdditional}");
            public static Category NonGreekExtended => new Category(@"\P{IsGreekExtended}");
            public static Category NonGeneralPunctuation => new Category(@"\P{IsGeneralPunctuation}");
            public static Category NonSuperscriptsandSubscripts => new Category(@"\P{IsSuperscriptsandSubscripts}");
            public static Category NonCurrencySymbols => new Category(@"\P{IsCurrencySymbols}");
            public static Category NonCombiningDiacriticalMarksforSymbols => new Category(@"\P{IsCombiningDiacriticalMarksforSymbols}");
            public static Category NonCombiningMarksforSymbols => new Category(@"\P{IsCombiningMarksforSymbols}");
            public static Category NonLetterlikeSymbols => new Category(@"\P{IsLetterlikeSymbols}");
            public static Category NonNumberForms => new Category(@"\P{IsNumberForms}");
            public static Category NonArrows => new Category(@"\P{IsArrows}");
            public static Category NonMathematicalOperators => new Category(@"\P{IsMathematicalOperators}");
            public static Category NonMiscellaneousTechnical => new Category(@"\P{IsMiscellaneousTechnical}");
            public static Category NonControlPictures => new Category(@"\P{IsControlPictures}");
            public static Category NonOpticalCharacterRecognition => new Category(@"\P{IsOpticalCharacterRecognition}");
            public static Category NonEnclosedAlphanumerics => new Category(@"\P{IsEnclosedAlphanumerics}");
            public static Category NonBoxDrawing => new Category(@"\P{IsBoxDrawing}");
            public static Category NonBlockElements => new Category(@"\P{IsBlockElements}");
            public static Category NonGeometricShapes => new Category(@"\P{IsGeometricShapes}");
            public static Category NonMiscellaneousSymbols => new Category(@"\P{IsMiscellaneousSymbols}");
            public static Category NonDingbats => new Category(@"\P{IsDingbats}");
            public static Category NonMiscellaneousMathematicalSymbols_A => new Category(@"\P{IsMiscellaneousMathematicalSymbols-A}");
            public static Category NonSupplementalArrows_A => new Category(@"\P{IsSupplementalArrows-A}");
            public static Category NonBraillePatterns => new Category(@"\P{IsBraillePatterns}");
            public static Category NonSupplementalArrows_B => new Category(@"\P{IsSupplementalArrows-B}");
            public static Category NonMiscellaneousMathematicalSymbols_B => new Category(@"\P{IsMiscellaneousMathematicalSymbols-B}");
            public static Category NonSupplementalMathematicalOperators => new Category(@"\P{IsSupplementalMathematicalOperators}");
            public static Category NonMiscellaneousSymbolsandArrows => new Category(@"\P{IsMiscellaneousSymbolsandArrows}");
            public static Category NonCJKRadicalsSupplement => new Category(@"\P{IsCJKRadicalsSupplement}");
            public static Category NonKangxiRadicals => new Category(@"\P{IsKangxiRadicals}");
            public static Category NonIdeographicDescriptionCharacters => new Category(@"\P{IsIdeographicDescriptionCharacters}");
            public static Category NonCJKSymbolsandPunctuation => new Category(@"\P{IsCJKSymbolsandPunctuation}");
            public static Category NonHiragana => new Category(@"\P{IsHiragana}");
            public static Category NonKatakana => new Category(@"\P{IsKatakana}");
            public static Category NonBopomofo => new Category(@"\P{IsBopomofo}");
            public static Category NonHangulCompatibilityJamo => new Category(@"\P{IsHangulCompatibilityJamo}");
            public static Category NonKanbun => new Category(@"\P{IsKanbun}");
            public static Category NonBopomofoExtended => new Category(@"\P{IsBopomofoExtended}");
            public static Category NonKatakanaPhoneticExtensions => new Category(@"\P{IsKatakanaPhoneticExtensions}");
            public static Category NonEnclosedCJKLettersandMonths => new Category(@"\P{IsEnclosedCJKLettersandMonths}");
            public static Category NonCJKCompatibility => new Category(@"\P{IsCJKCompatibility}");
            public static Category NonCJKUnifiedIdeographsExtensionA => new Category(@"\P{IsCJKUnifiedIdeographsExtensionA}");
            public static Category NonYijingHexagramSymbols => new Category(@"\P{IsYijingHexagramSymbols}");
            public static Category NonCJKUnifiedIdeographs => new Category(@"\P{IsCJKUnifiedIdeographs}");
            public static Category NonYiSyllables => new Category(@"\P{IsYiSyllables}");
            public static Category NonYiRadicals => new Category(@"\P{IsYiRadicals}");
            public static Category NonHangulSyllables => new Category(@"\P{IsHangulSyllables}");
            public static Category NonHighSurrogates => new Category(@"\P{IsHighSurrogates}");
            public static Category NonHighPrivateUseSurrogates => new Category(@"\P{IsHighPrivateUseSurrogates}");
            public static Category NonLowSurrogates => new Category(@"\P{IsLowSurrogates}");
            public static Category NonPrivateUseArea => new Category(@"\P{IsPrivateUseArea}");
            public static Category NonCJKCompatibilityIdeographs => new Category(@"\P{IsCJKCompatibilityIdeographs}");
            public static Category NonLettericPresentationForms => new Category(@"\P{IsLettericPresentationForms}");
            public static Category NonArabicPresentationForms_A => new Category(@"\P{IsArabicPresentationForms-A}");
            public static Category NonVariationSelectors => new Category(@"\P{IsVariationSelectors}");
            public static Category NonCombiningHalfMarks => new Category(@"\P{IsCombiningHalfMarks}");
            public static Category NonCJKCompatibilityForms => new Category(@"\P{IsCJKCompatibilityForms}");
            public static Category NonSmallFormVariants => new Category(@"\P{IsSmallFormVariants}");
            public static Category NonArabicPresentationForms_B => new Category(@"\P{IsArabicPresentationForms-B}");
            public static Category NonHalfwidthandFullwidthForms => new Category(@"\P{IsHalfwidthandFullwidthForms}");
            public static Category NonSpecials => new Category(@"\P{IsSpecials}");


            public static Pattern EnglishOrArabicLetter
                => UnionAndIntersect(Categories.BasicLatin, Categories.Arabic, Categories.Letter);
        }
    }
}
