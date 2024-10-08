using System.Reflection;
using FluentAssertions;
using Xunit;

namespace HorseRace.Test;

public sealed class HorseRaceTests : HorseTestBase
{
    public HorseRaceTests()
    {
        RandomProvider.Random = new(12345678);
    }

    [Fact]
    public void SortByPosition() 
    {
        var horses = GetSampleHorses();
        SetPosition(horses[0], 6);
        SetPosition(horses[1], 2);
        SetPosition(horses[2], 2);
        SetPosition(horses[3], 4);
        var horsesCopy = (Horse[]) horses.Clone();
        var race = new HorseRace(horses);

        CallSortByPosition(race);

        horses[0].Should().BeEquivalentTo(horsesCopy[0], "highest position");
        horses[1].Should().BeEquivalentTo(horsesCopy[3], "second highest position");
        horses[2].Should().BeEquivalentTo(horsesCopy[1], "same position, but higher starting number");
        horses[3].Should().BeEquivalentTo(horsesCopy[2], "same position, but lower starting number");
    }

    [Fact]
    public void AssignRanks()
    {
        var horses = GetSampleHorses();
        SetPosition(horses[0], 6);
        SetPosition(horses[1], 2);
        SetPosition(horses[2], 2);
        SetPosition(horses[3], 4);
        var horsesCopy = (Horse[])horses.Clone();
        var race = new HorseRace(horses);

        CallAssignRanks(race);

        horses[0].Rank.Should().Be(1, "highest rank");
        horses[1].Rank.Should().Be(2, "second highest rank");
        horses[2].Rank.Should().Be(3, "shared third place");
        horses[3].Rank.Should().Be(3, "shared third place");
    }

    [Fact]
    public void MoveHorses_NotFinished_01()
    {
        var horses = GetSampleHorses();
        var race = new HorseRace(horses);

        for (var i = 0; i < 4; i++)
        {
            CallMoveHorses(race);
        }

        CheckIsFinished(race)
            .Should().BeFalse("race not finished yet");

        horses[0].Position.Should().Be(1);
        horses[1].Position.Should().Be(0);
        horses[2].Position.Should().Be(2);
        horses[3].Position.Should().Be(1);
    }

    [Fact]
    public void MoveHorses_NotFinished_02()
    {
        var horses = GetSampleHorses();
        var race = new HorseRace(horses);

        for (var i = 0; i < 10; i++)
        {
            CallMoveHorses(race);
        }

        CheckIsFinished(race)
            .Should().BeFalse("race not finished yet");

        horses[0].Position.Should().Be(3);
        horses[1].Position.Should().Be(2);
        horses[2].Position.Should().Be(4);
        horses[3].Position.Should().Be(4);
    }

    [Fact]
    public void MoveHorses_Finished()
    {
        var horses = GetSampleHorses();
        var race = new HorseRace(horses);

        for (var i = 0; i < 60; i++)
        {
            CallMoveHorses(race);
        }

        CheckIsFinished(race)
            .Should().BeTrue("race is finished");

        horses[0].Position.Should().Be(18);
        horses[1].Position.Should().Be(17);
        horses[2].Position.Should().Be(20, "horses are no longer moved once race is finished");
        horses[3].Position.Should().Be(17);
    }

    private static Horse[] GetSampleHorses() => new Horse[]
    {
        new("Harvey", 5, 4),
        new("Haley", 8, 3),
        new("Horace", 9, 2),
        new("Hillary", 4, 1)
    };

    #region test helper methods - ignore

    private const BindingFlags FLAGS = BindingFlags.NonPublic | BindingFlags.Instance;

    private static void CallMoveHorses(HorseRace instance)
    {
        GetMethod("MoveHorses").Invoke(instance, null);
    }

    private static void CallSortByPosition(HorseRace instance)
    {
        GetMethod("SortByPosition").Invoke(instance, null);
    }

    private static void CallAssignRanks(HorseRace instance)
    {
        GetMethod("AssignRanks").Invoke(instance, null);
    }

    private static bool CheckIsFinished(HorseRace instance)
    {
        var prop = typeof(HorseRace).GetProperty("IsFinished", FLAGS);
        return (bool) prop!.GetValue(instance)!;
    }

    private static MethodInfo GetMethod(string methodName)
    {
        return typeof(HorseRace).GetMethod(methodName, FLAGS)!;
    }

    #endregion
}