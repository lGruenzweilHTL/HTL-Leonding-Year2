namespace VocabularyTrainer.Test;

public class TrainerTests
{
    public TrainerTests()
    {
        RandomProvider.Random = new Random(1234);
    }
    
    [Fact]
    public void CallRandomProvider_ReturnsSameRandomNumbers()
    {
        var nums = Enumerable.Range(0, 12).Select(_ => RandomProvider.Random.Next(0, 3)).ToArray();
        var expected = new int[] { 1, 2, 0, 2, 1, 2, 2, 1, 1, 0, 1, 2 };
        Assert.Equal(expected, nums);
    }

    [Fact]
    public void ValidItems_CurrentVocabularyItem_IsNull()
    {
        var testWords = new string[][] { new[] { "word", "translation" } };
        var trainer = new Trainer(testWords);
        Assert.NotNull(trainer);
        Assert.Null(trainer.CurrentVocabularyItem);
    }
    
    [Fact]
    public void ResetCycle_WithValidItems_CurrentVocabularyItem_IsNull()
    {
        var testWords = new string[][] { new[] { "word", "translation" } };
        var trainer = new Trainer(testWords);
        trainer.ResetCycle();
        Assert.Null(trainer.CurrentVocabularyItem);
    }
    
    [Fact]
    public void Reset_WithValidItems_CurrentVocabularyItem_IsNull()
    {
        var testWords = new string[][] { new[] { "word", "translation" } };
        var trainer = new Trainer(testWords);
        trainer.Reset();
        Assert.Null(trainer.CurrentVocabularyItem);
    }
    
    [Fact]
    public void ValidItems_CallGetNextWord_ReturnsCorrectWords()
    {
        //{ 1, 2, 0, 2, 1, 2, 2, 1, 1, 0, 1, 2 };
        var testWords = new string[][] {  
                new [] { "w1", "t1" }, 
                new [] {"w2", "t2"}, 
                new [] {"w3", "t3"} 
               };
        var expectedWords = new string[] { "w2", "w3", "w1", "w3", "w2", "w3", "w3", "w2", "w2", "w1", "w2", "w3" };
        var trainer = new Trainer(testWords);
        int callCount = 0;
        while (trainer.GetNextWord())
        {
            Assert.Equal(expectedWords[callCount], trainer.CurrentVocabularyItem!.NativeWord);
            callCount++;
        }
        Assert.Equal(Trainer.CYCLE_COUNT, callCount);
    }
    
    [Fact]
    public void ValidItems_RunMultipleCycles_ReturnsCorrectResults()
    {
        //{ 1, 2, 0, 2, 1, 2, 2, 1, 1, 0, 1, 2 };
        var testWords = new string[][] {  
            new [] { "w1", "t1" }, 
            new [] {"w2", "t2"}, 
            new [] {"w3", "t3"} 
        };
        var expectedWords = new string[]
        {
            "w2", "w3", "w1", 
            "w2", "w3", "w3"
        };
        var trainer = new Trainer(testWords);
        int callCount = 0;
        //Cycle 1
        while (trainer.GetNextWord())
        {
            Assert.Equal(expectedWords[callCount], trainer.CurrentVocabularyItem!.NativeWord);
            callCount++;
        }
        Assert.Equal(Trainer.CYCLE_COUNT, callCount);
        
        //Cycle 2
        trainer.ResetCycle();
        while (trainer.GetNextWord())
        { 
            Assert.Equal(expectedWords[callCount], trainer.CurrentVocabularyItem!.NativeWord);
            callCount++;
        }
        Assert.Equal(2 * Trainer.CYCLE_COUNT, callCount);
    }

    [Fact]
    public void ValidItems_SubmitCorrectTranslations_TranslationsAreAccepted()
    {
        var testWords = new string[][] {  
            new [] { "w1", "t1" }, 
            new [] {"w2", "t2"}, 
            new [] {"w3", "t3"} 
        };
        var expectedWords = new string[]
        {
            "w2", "w3", "w1", 
            "w2", "w3", "w3"
        };
        var submittedTranslations = new string[] { "t2", "t3", "t1", "t2", "t3", "t3" };
        
        var trainer = new Trainer(testWords);
        int callCount = 0;
        while (trainer.GetNextWord())
        {
            Assert.Equal(expectedWords[callCount], trainer.CurrentVocabularyItem!.NativeWord);
            var result = trainer.TestTranslation(submittedTranslations[callCount], out var correctTranslation);
            Assert.True(result);
            Assert.Empty(correctTranslation);
            callCount++;
        }
        Assert.Equal(Trainer.CYCLE_COUNT, callCount);
    }
    
    [Fact]
    public void ValidItems_SubmitPartiallyCorrectTranslations_StatisticsAreCorrect()
    {
        var testWords = new string[][] {  
            new [] { "w1", "t1" }, 
            new [] {"w2", "t2"}, 
            new [] {"w3", "t3"} 
        };
        var expectedWords = new string[]
        {
            "w2", "w3", "w1", 
            "w2", "w3", "w3"
        };
        var submittedTranslations = new string[] { "t2", "t1", "t1", "t2", "t1", "t1" };
        var expectedCorrectTranslations = new string[] { "t2", "t3", "t1", "t2", "t3", "t3" };
        var expectedVocabularyOrder = new string[] { "t2", "t1", "t3" };
        var expectedVocabularyCounts = new List<Tuple<int, int>>
        {
            new Tuple<int, int>(2, 0),
            new Tuple<int, int>(1, 0),
            new Tuple<int, int>(0, 3)
        };
        
        var trainer = new Trainer(testWords);
        int callCount = 0;
        for (int i = 0; i < 2; i++)
        {
            trainer.ResetCycle();

            while (trainer.GetNextWord())
            {
                Assert.Equal(expectedWords[callCount], trainer.CurrentVocabularyItem!.NativeWord);
                var result = trainer.TestTranslation(submittedTranslations[callCount], out var correctTranslation);
                if (!result)
                {
                    Assert.Equal(expectedCorrectTranslations[callCount], correctTranslation);
                }
                else
                    Assert.Empty(correctTranslation);

                callCount++;
            }
        }

        Assert.Equal(2 * Trainer.CYCLE_COUNT, callCount);

        var sortedItems = trainer.GetSortedItems();
        for (int i = 0; i < sortedItems.Length; i++)
        {
            Assert.Equal(expectedVocabularyOrder[i], sortedItems[i].Translation);
            Assert.Equal(expectedVocabularyCounts[i].Item1, sortedItems[i].CountCorrect);
            Assert.Equal(expectedVocabularyCounts[i].Item2, sortedItems[i].CountAsked - sortedItems[i].CountCorrect);
        }
    }


    [Fact]
    public void ValidItems_3Cycles_ResetStatistics_StatsAreZeroed()
    {
        var testWords = new string[][] {  
            new [] { "w1", "t1" }, 
            new [] {"w2", "t2"}, 
            new [] {"w3", "t3"} 
        };
        var submittedTranslations = new string[] { "t2", "t1", "t1", "t2", "t1", "t3" };

        var trainer = new Trainer(testWords);
        int callCount = 0;
        for (int i = 0; i < 2; i++)
        {
            trainer.ResetCycle();
            while (trainer.GetNextWord())
            {
                trainer.TestTranslation(submittedTranslations[callCount], out var correctTranslation);
                callCount++;
            }
        }
        Assert.Equal(2 * Trainer.CYCLE_COUNT, callCount);
        
        var sortedItems = trainer.GetSortedItems();
        for (int i = 0; i < sortedItems.Length; i++)
        {
            Assert.True(sortedItems[i].CountAsked > 0);
            Assert.True(sortedItems[i].CountCorrect > 0);
        }
        
        trainer.Reset();
        sortedItems = trainer.GetSortedItems();
        for (int i = 0; i < sortedItems.Length; i++)
        {
            Assert.Equal(0, sortedItems[i].CountAsked);
            Assert.Equal(0, sortedItems[i].CountCorrect);
        }
    }
    
    [Fact]
    public void InvalidItems_NoWordsAreCreated()
    {
        var testWords = new string[][] {  
            new [] { "", "t1" }, 
            new [] {"w2", ""}, 
            new [] {"", ""} 
        };
        var trainer = new Trainer(testWords);
        Assert.Empty(trainer.GetSortedItems());
    }
   
}