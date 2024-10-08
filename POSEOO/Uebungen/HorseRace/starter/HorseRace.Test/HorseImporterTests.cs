using FluentAssertions;
using Xunit;

namespace HorseRace.Test;

public sealed class HorseImporterTests
{
    [Fact]
    public void NotExists()
    {
        HorseImporter.TryReadHorses("Data/none.csv", out var horses)
            .Should().BeFalse("file does not exist");
        horses.Should().BeNull();
    }

    [Fact]
    public void Empty_01()
    {
        HorseImporter.TryReadHorses("Data/empty_01.csv", out var horses)
            .Should().BeFalse("file is empty");
        horses.Should().BeNull();
    }

    [Fact]
    public void Empty_02()
    {
        HorseImporter.TryReadHorses("Data/empty_02.csv", out var horses)
            .Should().BeFalse("file contains no data rows");
        horses.Should().BeNull();
    }

    [Fact]
    public void Invalid_01()
    {
        HorseImporter.TryReadHorses("Data/invalid_01.csv", out var horses)
            .Should().BeFalse("file contains data rows, but none could be parsed");
        horses.Should().NotBeNull("there are data rows to process")
            .And.BeEmpty("no rows are valid, too many columns")
            .And.BeSameAs(Array.Empty<Horse>(), "proper empty array creation");
    }

    [Fact]
    public void Invalid_02()
    {
        HorseImporter.TryReadHorses("Data/invalid_02.csv", out var horses)
            .Should().BeTrue("file contains data rows and some could be parsed");
        horses.Should().NotBeNull("there are data rows to process")
            .And.NotBeEmpty("at least one row is valid")
            .And.HaveCount(1, "one row could be parsed")
            .And.Subject.First()
            .Should().BeEquivalentTo(new
            {
                Age = 9,
                Name = "Blitz",
                Position = 0,
                Rank = 0,
                StartNumber = 2
            }, "position and rank are not set yet, start number is based on row number");
    }

    [Fact]
    public void Valid()
    {
        HorseImporter.TryReadHorses("Data/valid.csv", out var horses)
            .Should().BeTrue("file is valid");
        horses.Should().NotBeNullOrEmpty("valid data")
            .And.HaveCount(2, "two parseable rows")
            .And.BeEquivalentTo(new[]
            {
                new Horse("Egon", 6, 1),
                new Horse("Blitz", 9, 2)
            });
    }
}