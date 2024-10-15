namespace VocabularyTrainer;

/// <summary>
///     Represents one word in the vocabulary
/// </summary>
public sealed class VocabularyItem
{
    private static int WORD_IDX = 0;
    private static int TRANSLATION_IDX = 1;

    public int CountCorrect { get; private set; }
    public int CountAsked { get; private set; }

    public VocabularyItem(string nativeWord, string translation)
    {
        NativeWord = nativeWord;
        Translation = translation;
    }

    /// <summary>
    ///     Gets the native word
    /// </summary>
    public string NativeWord { get; }

    /// <summary>
    ///     Gets the translation
    /// </summary>
    public string Translation { get; }

    /// <summary>
    ///     A translation attempt is checked for correctness.
    /// </summary>
    /// <param name="translationAttempt">The user provided translation</param>
    /// <returns>True if the translation was correct; false otherwise</returns>
    public bool TestTranslation(string translationAttempt)
    {
        CountAsked++;
        bool correct = translationAttempt.Equals(Translation, StringComparison.CurrentCultureIgnoreCase);
        // correct = lev(Translation.ToLower(), translationAttempt.ToLower()) < 3;
        if (correct) CountCorrect++;

        return correct;
    }

    // Levenshtein distance algorithm
    private int lev(string a, string b)
    {
        string head(string s) => s[0].ToString();
        string tail(string s) => s[1..];
        if (a.Length == 0) return b.Length;
        if (b.Length == 0) return a.Length;

        if (head(a) == head(b)) return lev(tail(a), tail(b));

        return 1 + Math.Min(Math.Min(lev(tail(a), b), lev(a, tail(b))), lev(tail(a), tail(b)));
    }

    /// <summary>
    ///     The vocabulary item is compared to another. First the number of correct answers is compared.
    ///     If it is equal the native words are compared ordinal.
    /// </summary>
    /// <param name="other">The <see cref="VocabularyItem"/> to compare with</param>
    /// <returns>0 if equal; less than 0 if this item is smaller; greater than 0 otherwise</returns>
    public int CompareTo(VocabularyItem other)
    {
        var diffCorrect = other.CountCorrect - this.CountCorrect;
        return diffCorrect == 0 
            ? CompareStrings(this.NativeWord, other.NativeWord)
            : diffCorrect;
    }

    /// <summary>
    ///     Overrides the default string representation to display the word and translation statistics.
    /// </summary>
    /// <returns>A string containing the word, its translation and the training statistics</returns>
    public override string ToString()
    {
        return $"{NativeWord,-10} {Translation,-10} {this.CountAsked,5} {this.CountCorrect,7}";
    }

    /// <summary>
    ///     Compares two strings by ordinal value, ignoring case.
    /// </summary>
    /// <param name="a">First string</param>
    /// <param name="b">Second string</param>
    /// <returns>Less than 0 if a precedes b in the sorting order; greater than 0 if b precedes a; 0 otherwise</returns>
    private static int CompareStrings(string a, string b) => string.Compare(a, b, StringComparison.OrdinalIgnoreCase);

    public void Reset()
    {
        this.CountAsked = 0;
        this.CountCorrect = 0;
    }
    
    /// <summary>
    ///     Creates a <see cref="VocabularyItem"/> array from the raw words.
    /// </summary>
    /// <param name="wordsAndTranslations">Raw vocabulary read from a file</param>
    /// <returns>A <see cref="VocabularyItem"/> for each (valid) word</returns>
    public static VocabularyItem[] CreateVocabularyItems(string[][] wordsAndTranslations)
    {
        //      It returns an array of VocabularyItem instances based on the given wordsAndTranslations.
        //      Invalid entries (e.g. empty or null words or translations) are ignored.
        //      The returned array must not contain any null entries.
        //      Use the constants WORD_IDX and TRANSLATION_IDX to access the word and translation in the wordsAndTranslations array.
        VocabularyItem[] vocabularyItems = new VocabularyItem[wordsAndTranslations.Length];
        int validItems = 0;
        foreach (var wordAndTranslation in wordsAndTranslations)
        {
            if (wordAndTranslation.Length != 2) continue;
            
            string word = wordAndTranslation[0];
            string translation = wordAndTranslation[1];
            
            if (string.IsNullOrEmpty(translation) || string.IsNullOrEmpty(word)) continue;
            
            vocabularyItems[validItems++] = new VocabularyItem(word, translation);
        }
        
        Array.Resize(ref vocabularyItems, validItems);
        return vocabularyItems;
    }
}