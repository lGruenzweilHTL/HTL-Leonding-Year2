namespace VocabularyTrainer;

/// <summary>
///     Vocabulary trainer.
///     Based on a supplied vocabulary training cycles can be performed.
/// </summary>
public sealed class Trainer
{
    public static int CYCLE_COUNT = 3;

    private readonly VocabularyItem[] _vocabularyItems;
    private bool[] _alreadyAsked;
    private int _cycleCount = 0;
    private int _currentVocabularyItemIndex = 0;

    /// <summary>
    /// Holds the current vocabulary item.
    /// </summary>
    public VocabularyItem? CurrentVocabularyItem
    {
        get
        {
            if (_currentVocabularyItemIndex < 0 || _currentVocabularyItemIndex >= _vocabularyItems.Length) return null;
            return _vocabularyItems[_currentVocabularyItemIndex];
        }
    }

    /// <summary>
    ///     Constructs a new <see cref="Trainer"/> instance based on the given vocabulary.
    /// </summary>
    /// <param name="wordsAndTranslations">Raw vocabulary read from a file</param>
    public Trainer(string[][] wordsAndTranslations)
    {
        _vocabularyItems = VocabularyItem.CreateVocabularyItems(wordsAndTranslations);
        _alreadyAsked = new bool[_vocabularyItems.Length];
        Reset();
    }

    /// <summary>
    /// Resets the cycle count. Use to start a new training cycle after the last one has been completed.
    /// Statistics are not reset.
    /// </summary>
    public void ResetCycle()
    {
        _cycleCount = 0;
        _currentVocabularyItemIndex = -1;
    }

    /// <summary>
    /// Resets all training statistics and the current word index.
    /// </summary>
    public void Reset()
    {
        ResetCycle();
        for (var i = 0; i < _alreadyAsked.Length; i++)
        {
            _alreadyAsked[i] = false;
        }
        foreach (var vocabularyItem in _vocabularyItems)
        {
            vocabularyItem.Reset();
        }
        
    }

    /// <summary>
    /// Chooses the next word to be trained, sets CurrentVocabularyItem
    /// and returns whether there are more words to be trained.
    /// </summary>
    /// <returns>true - there are more words to be trained in this cycle.</returns>
    public bool GetNextWord()
    {
        PickNextWord();
        return _cycleCount++ < CYCLE_COUNT;
    }

    /// <summary>
    /// Checks whether the given translation is correct.
    /// </summary>
    /// <param name="translation"></param>
    /// <param name="correctTranslation">set when the translation is not correct, otherwise empty.</param>
    /// <returns>true if the translation is correct, false otherwise. </returns>
    public bool TestTranslation(string translation, out string correctTranslation)
    {
        bool correct = CurrentVocabularyItem.TestTranslation(translation);
        correctTranslation = correct ? "" : CurrentVocabularyItem!.Translation;
        return correct;
    }

    /// <summary>
    /// Sorts the vocabulary items and returns a copy of the array.
    /// </summary>
    /// <returns></returns>
    public VocabularyItem[] GetSortedItems()
    {
       Sort();
       VocabularyItem[] copy = new VocabularyItem[_vocabularyItems.Length];
       
       Array.Copy(_vocabularyItems, copy, copy.Length);

       return copy;
    }

    /// <summary>
    ///     Picks the next word by random. A word that has been asked before is not chosen.
    ///     If all words have already been used any one is chosen.
    /// </summary>
    private void PickNextWord()
    {
        int nextWord;
        var count = 0;
        do
        {
            nextWord = RandomProvider.Random.Next(0, this._vocabularyItems.Length);
            count++;
        } while (_alreadyAsked[nextWord] && count < this._vocabularyItems.Length);

        // if maxCnt is reached any word will be reused
        _alreadyAsked[nextWord] = true;
        _currentVocabularyItemIndex = nextWord;
    }

    /// <summary>
    ///     Sorts the vocabulary items using the CompareTo method of the <see cref="VocabularyItem"/> class.
    /// </summary>
    private void Sort()
    {
        for (var left = 0; left < this._vocabularyItems.Length - 1; left++)
        {
            for (var right = left + 1; right < this._vocabularyItems.Length; right++)
            {
                if (this._vocabularyItems[right].CompareTo(this._vocabularyItems[left]) < 0)
                {
                    (this._vocabularyItems[right], this._vocabularyItems[left])
                        = (this._vocabularyItems[left], this._vocabularyItems[right]);
                }
            }
        }
    }
}