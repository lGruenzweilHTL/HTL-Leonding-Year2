using FluentAssertions;
using Xunit;

namespace HorseRace.Test;

public sealed class HorseTests : HorseTestBase
{
    public HorseTests()
    {
        RandomProvider.Random = new(12345);
    }

    [Fact]
    public void Construction()
    {
        const string NAME = "Henry";
        const int AGE = 8;
        const int START_NO = 5;

        var horse = new Horse(NAME, AGE, START_NO);

        horse.Position.Should().Be(0, "initial value before being moved");
        horse.StartNumber.Should().Be(START_NO);
        horse.Age.Should().Be(AGE);
        horse.Name.Should().Be(NAME);
        horse.Rank.Should().Be(0, "initial value before being calculated (race finished)");
    }

    [Theory]
    [MemberData(nameof(TryParseData))]
    public void TryParse(string line, int startNo, bool expectedResult, Horse? expectedHorse, string reason)
    {
        Horse.TryParse(line, startNo, out var parsedHorse)
            .Should().Be(expectedResult, reason);

        if (expectedResult)
        {
            parsedHorse.Should().BeEquivalentTo(expectedHorse);
        }
    }

    [Fact]
    public void Move()
    {
        var horse = new Horse("Henry Horse", 9, 5);
        SetPosition(horse, 4);
        
        horse.Move();
        horse.Position
            .Should().Be(5);

        horse.Move();
        horse.Position
            .Should().Be(6);

        for (var i = 0; i < 5; i++)
        {
            horse.Move();
        }
        horse.Position
            .Should().Be(7);

        for (var i = 0; i < 8; i++)
        {
            horse.Move();
        }
        horse.Position
            .Should().Be(12);
    }

    [Fact]
    public void CompareTo_Equal()
    {
        const int POSITION = 4;
        const int START_NO = 2;

        var horse1 = new Horse("Horse A", 5, START_NO);
        SetPosition(horse1, POSITION);
        var horse2 = new Horse("Horse B", 8, START_NO);
        SetPosition(horse2, POSITION);

        var diff = horse1.CompareTo(horse2);

        diff.Should().Be(0, "Both position and starting number are equal");
    }

    [Fact]
    public void CompareTo_HigherPosition()
    {
        const int START_NO = 2;

        var horse1 = new Horse("Horse A", 5, START_NO);
        SetPosition(horse1, 8);
        var horse2 = new Horse("Horse B", 8, START_NO);
        SetPosition(horse2, 5);

        horse1.CompareTo(horse2)
            .Should().Be(-3, "First horse has higher position");
    }

    [Fact]
    public void CompareTo_HigherStartNo()
    {
        const int POSITION = 2;

        var horse1 = new Horse("Horse A", 5, 8);
        SetPosition(horse1, POSITION);
        var horse2 = new Horse("Horse B", 8, 5);
        SetPosition(horse2, POSITION);

        horse1.CompareTo(horse2)
            .Should().Be(-3, "First horse has higher starting number");
    }

    [Fact]
    public void CompareTo_LowerPosition()
    {
        const int START_NO = 2;

        var horse1 = new Horse("Horse A", 5, START_NO);
        SetPosition(horse1, 3);
        var horse2 = new Horse("Horse B", 8, START_NO);
        SetPosition(horse2, 5);

        horse1.CompareTo(horse2)
            .Should().Be(2, "First horse has lower position");
    }

    [Fact]
    public void CompareTo_LowerStartNo()
    {
        const int POSITION = 2;

        var horse1 = new Horse("Horse A", 5, 3);
        SetPosition(horse1, POSITION);
        var horse2 = new Horse("Horse B", 8, 5);
        SetPosition(horse2, POSITION);

        horse1.CompareTo(horse2)
            .Should().Be(2, "First horse has lower starting number");
    }

    public static IEnumerable<object?[]> TryParseData => new[]
    {
        new object[]{ "Henry;5", 2, true, new Horse("Henry", 5, 2), "Parseable" },
        new object?[]{ "Henry;-5", 2, false, null, "Invalid age" },
        new object?[]{ "Henry;25", 2, false, null, "Invalid age" },
        new object?[]{ "Henry;abc", 2, false, null, "Invalid age" },
        new object?[]{ "Henry;5;22", 2, false, null, "Too many columns" },
        new object?[]{ "Henry", 2, false, null, "Not enough columns" },
        new object?[]{ ";5", 2, false, null, "Empty name" },
        new object?[]{ "Henry;5", -2, false, null, "Invalid starting number" }
    };
}