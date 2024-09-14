using FluentAssertions;
using Xunit;

namespace Highscore.Test;

public sealed class HighscoreProcessingTests
{
    [Fact]
    public void TrimArray_Simple()
    {
        GameScore?[] scores =
        {
            new(new(1, "Test"), 23, DateTime.Now),
            new(new(1, "Test"), 32, DateTime.Now.AddDays(-1)), null
        };

        HighscoreProcessing.TrimArray(scores)
            .Should().NotBeNull()
            .And.NotBeEmpty().And.HaveCount(2)
            .And.ContainInOrder(new[] { scores[0]!, scores[1]! }, "the two not null elements");
    }

    [Fact]
    public void TrimArray_Gap()
    {
        GameScore?[] scores =
        {
            new(new(1, "Test"), 23, DateTime.Now),
            null, new(new(1, "Test"), 32, DateTime.Now.AddDays(-1))
        };

        HighscoreProcessing.TrimArray(scores)
            .Should().NotBeNull()
            .And.NotBeEmpty().And.HaveCount(2)
            .And.ContainInOrder(new[] { scores[0]!, scores[2]! }, "the two not null elements");
    }

    [Fact]
    public void TrimArray_EndOnly()
    {
        GameScore?[] scores = { null, null, new(new(1, "Test"), 32, DateTime.Now.AddDays(-1)) };

        HighscoreProcessing.TrimArray(scores)
            .Should().NotBeNull()
            .And.NotBeEmpty().And.HaveCount(1)
            .And.ContainInOrder(new[] { scores[2]! }, "the one not null elements");
    }

    [Fact]
    public void TrimArray_Empty01()
    {
        HighscoreProcessing.TrimArray(Array.Empty<GameScore>())
            .Should().NotBeNull()
            .And.BeSameAs(Array.Empty<GameScore>());
    }

    [Fact]
    public void TrimArray_Empty02()
    {
        HighscoreProcessing.TrimArray(new GameScore?[10])
            .Should().NotBeNull()
            .And.HaveCount(0, "only null elements in passed array")
            .And.BeSameAs(Array.Empty<GameScore>());
    }

    [Fact]
    public void SortGameScores_Simple()
    {
        var scores = new GameScore[]
        {
            new(new(1, "Test1"), 23, DateTime.Now),
            new(new(2, "Test2"), 32, DateTime.Now),
            new(new(3, "Test3"), 24, DateTime.Now)
        };
        var copy = (GameScore[])scores.Clone();

        HighscoreProcessing.SortGameScores(scores);

        scores.Should()
            .NotBeNullOrEmpty().And.HaveCount(3, "no items are removed or added")
            .And.ContainInOrder(new[] { copy[1], copy[2], copy[0] }, "sorted by score");
    }

    [Fact]
    public void SortGameScores_DateSort()
    {
        var scores = new GameScore[]
        {
            new(new(1, "Test1"), 32, DateTime.Now),
            new(new(1, "Test1"), 32, DateTime.Now.AddDays(-1)),
            new(new(3, "Test3"), 24, DateTime.Now)
        };
        var copy = (GameScore[])scores.Clone();

        HighscoreProcessing.SortGameScores(scores);

        scores.Should()
            .NotBeNullOrEmpty().And.HaveCount(3, "no items are removed or added")
            .And.ContainInOrder(new[] { copy[1], copy[0], copy[2] },
                "sorted by score first, then by date, older entry wins");
    }

    [Fact]
    public void FindBestScorePerUser_OneEntryPerUser()
    {
        var scores = new GameScore[]
        {
            new(new(1, "Test1"), 23, DateTime.Now),
            new(new(2, "Test2"), 32, DateTime.Now),
            new(new(3, "Test3"), 24, DateTime.Now)
        };

        HighscoreProcessing.FindBestScorePerUser(scores)
            .Should().NotBeNullOrEmpty().And.HaveCount(3,
                "three entries from three different users")
            .And.Contain(scores);
    }

    [Fact]
    public void FindBestScorePerUser_MultipleEntryPerUser()
    {
        var scores = new GameScore[]
        {
            new(new(1, "Test1"), 23, DateTime.Now),
            new(new(1, "Test1"), 50, DateTime.Now.AddDays(-4)),
            new(new(2, "Test2"), 32, DateTime.Now),
            new(new(3, "Test3"), 24, DateTime.Now),
            new(new(3, "Test3"), 12, DateTime.Now.AddDays(-2)),
            new(new(4, "Test4"), 60, DateTime.Now),
            new(new(4, "Test4"), 60, DateTime.Now.AddDays(-1))
        };

        HighscoreProcessing.FindBestScorePerUser((GameScore[])scores.Clone())
            .Should().NotBeNullOrEmpty().And.HaveCount(4,
                "consolidated to one entry per user")
            .And.Contain(new[] { scores[1], scores[2], scores[3], scores[6] },
                "best result per user, earliest wins in case of equal score");
    }

    [Theory]
    [MemberData(nameof(TryParseGameScoreData))]
    public void TryParseGameScore(string line, bool expectedSuccess,
        GameScore? expectedScore, string reason)
    {
        var result = HighscoreProcessing.TryParseGameScore(line, out var parsed);

        result.Should().Be(expectedSuccess, reason);
        if (expectedSuccess)
        {
            parsed.Should().NotBeNull()
                .And.BeEquivalentTo(expectedScore, "expected parse result");
        }
    }

    [Fact]
    public void LoadScoresFromFile_Simple()
    {
        HighscoreProcessing.LoadScoresFromFile("Data/valid.csv")
            .Should().NotBeNullOrEmpty()
            .And.HaveCount(5, "one user can have multiple scores")
            .And.Contain(new[]
            {
                new GameScore(new(3, "Annihilator"), 2478, new(2021, 06, 24, 02, 43, 28)),
                new GameScore(new(85, "Iron-Cut"), 1013, new(2022, 08, 07, 14, 43, 28)),
                new GameScore(new(27, "Digital"), 6828, new(2022, 07, 26, 05, 43, 28)),
                new GameScore(new(27, "Digital"), 119, new(2022, 04, 15, 17, 43, 28)),
                new GameScore(new(42, "Harry Dotter"), 9244, new(2021, 05, 21, 03, 43, 28))
            });
    }

    [Fact]
    public void LoadScoresFromFile_EmptyFile_01()
    {
        HighscoreProcessing.LoadScoresFromFile("Data/empty_01.csv")
            .Should().NotBeNull("an array is always allocated")
            .And.BeEmpty("file does not contain data")
            .And.BeSameAs(Array.Empty<GameScore>(), "proper empty array creation");
    }

    [Fact]
    public void LoadScoresFromFile_EmptyFile_02()
    {
        HighscoreProcessing.LoadScoresFromFile("Data/empty_02.csv")
            .Should().NotBeNull("an array is always allocated")
            .And.BeEmpty("file does not contain data")
            .And.BeSameAs(Array.Empty<GameScore>(), "proper empty array creation");
    }

    [Fact]
    public void LoadScoresFromFile_Invalid()
    {
        HighscoreProcessing.LoadScoresFromFile("Data/invalid.csv")
            .Should().NotBeNullOrEmpty()
            .And.HaveCount(3, "two entries are invalid")
            .And.Contain(new[]
            {
                new GameScore(new(85, "Iron-Cut"), 1013, new(2022, 08, 07, 14, 43, 28)),
                new GameScore(new(27, "Digital"), 6828, new(2022, 07, 26, 05, 43, 28)),
                new GameScore(new(27, "Digital"), 119, new(2022, 04, 15, 17, 43, 28))
            }, "invalid entries have been trimmed from final returned array");
    }

    [Fact]
    public void LoadScoresFromFile_FileNotExists()
    {
        HighscoreProcessing.LoadScoresFromFile("Data/nope.csv")
            .Should().NotBeNull("an array is always allocated")
            .And.BeEmpty("file does not exist")
            .And.BeSameAs(Array.Empty<GameScore>(), "proper empty array creation");
    }

    [Fact]
    // console output not tested
    public void WriteOutputFileAndPrint_Simple()
    {
        const string FILE_PATH = "Data/output.csv";

        if (File.Exists(FILE_PATH))
        {
            File.Delete(FILE_PATH);
        }

        HighscoreProcessing.WriteOutputFileAndPrint(outputTestScores, FILE_PATH);

        var lines = File.ReadAllLines(FILE_PATH);
        lines.Should().NotBeNullOrEmpty("valid file should have been created")
            .And.HaveCount(4, "three data rows + header")
            .And.ContainInOrder(outputExpectedLines, "output of passed scores in expected format");
    }

    [Fact]
    // console output not tested
    public void WriteOutputFileAndPrint_FileExists()
    {
        const string FILE_PATH = "Data/output.csv";

        File.WriteAllLines(FILE_PATH, new []{"some", "content"});

        HighscoreProcessing.WriteOutputFileAndPrint(outputTestScores, FILE_PATH);

        var lines = File.ReadAllLines(FILE_PATH);
        lines.Should().NotBeNullOrEmpty("valid file should have been created despite one existing at that path")
            .And.HaveCount(4, "three data rows + header")
            .And.ContainInOrder(outputExpectedLines, "output of passed scores in expected format");
    }

    private static readonly GameScore[] outputTestScores =
    {
        new(new(27, "Digital"), 6828, new(2022, 07, 26, 05, 43, 28)),
        new(new(3, "Annihilator"), 2478, new(2021, 06, 24, 02, 43, 28)),
        new(new(85, "Iron-Cut"), 1013, new(2022, 08, 07, 14, 43, 28))
    };

    private static readonly string[] outputExpectedLines =
    {
        "Score;Date;Player",
        "6828;26.07.2022;Digital (#27)",
        "2478;24.06.2021;Annihilator (#3)",
        "1013;07.08.2022;Iron-Cut (#85)"
    };

    public static IEnumerable<object?[]> TryParseGameScoreData => new[]
    {
        new object[]
        {
            "12;Test;2022-08-16T19:33:56;589", true,
            new GameScore(new(12, "Test"), 589,
                new(2022, 08, 16, 19, 33, 56)),
            "simple success case"
        },
        new object?[]
        {
            "Test;12;2022-08-16T19:33:56;589", false,
            null, "wrong order"
        },
        new object?[]
        {
            "Test;2022-08-16T19:33:56;589", false,
            null, "missing element"
        },
        new object?[]
        {
            "Test;12;2022-08-16T19:33:56;true;589", false,
            null, "too many elements"
        },
        new object?[]
        {
            "12;Test;2022-16-08T19:33:56;589", false,
            null, "invalid date"
        },
        new object?[]
        {
            "12;Test;2022-08-16T19:33:56;-589", false,
            null, "invalid score"
        },
        new object?[]
        {
            "12;;2022-08-16T19:33:56;589", false,
            null, "empty name"
        },
        new object?[]
        {
            "12,Test,2022-08-16T19:33:56,589", false,
            null, "wrong separator"
        }
    };
}