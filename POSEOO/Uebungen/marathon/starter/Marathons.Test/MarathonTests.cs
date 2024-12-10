using FluentAssertions;
using Xunit;

namespace Marathons.Test;

public sealed class MarathonTests
{
    [Fact]
    public void Construction()
    {
        const string City = "Leonding";
        DateOnly date = new(2022, 11, 15);

        var m = new Marathon(City, date);

        (m.City, m.Date, m.ParticipantCount)
            .Should().Be((City, date, 0), "no participants added so far");
    }

    [Fact]
    public void AddParticipant_Single()
    {
        var m = CreateSampleMarathon();
        var participant = CreateSampleParticipants()[0];

        // should not throw
        m.AddParticipant(participant);

        m.ParticipantCount.Should().Be(1, "one participant added");
    }

    [Fact]
    public void AddParticipant_Multiple()
    {
        var m = CreateSampleMarathon();
        Participant[] participants = CreateSampleParticipants();

        foreach (var participant in participants)
        {
            m.AddParticipant(participant);
        }

        m.ParticipantCount.Should().Be(participants.Length, "multiple participants added");
    }

    [Fact]
    public void RemoveParticipant_Single()
    {
        var m = CreateSampleMarathon();
        var participant = CreateSampleParticipants()[0];

        m.AddParticipant(participant);
        m.RemoveParticipant(participant.StartNo)
            .Should().BeTrue("previously added participant removed");
        m.ParticipantCount.Should().Be(0, "no participants remain");
    }

    [Fact]
    public void RemoveParticipant_Empty()
    {
        var m = CreateSampleMarathon();
        var participant = CreateSampleParticipants()[0];

        m.RemoveParticipant(participant.StartNo)
            .Should().BeFalse("no participants added");
    }

    [Fact]
    public void RemoveParticipant_NotExists()
    {
        var m = CreateSampleMarathon();
        var participant1 = CreateSampleParticipants()[1];
        var participant2 = CreateSampleParticipants()[2];

        m.AddParticipant(participant1);

        m.RemoveParticipant(participant2.StartNo)
            .Should().BeFalse("not in list");
        m.ParticipantCount.Should().Be(1, "one added participant still remains");
    }

    [Fact]
    public void RemoveParticipant_Multiple()
    {
        var m = CreateSampleMarathon();
        Participant[] participants = CreateSampleParticipants();

        foreach (var participant in participants)
        {
            m.AddParticipant(participant);
        }

        m.RemoveParticipant(participants[2].StartNo)
            .Should().BeTrue("was added previously and can now be removed");
        m.RemoveParticipant(participants[2].StartNo)
            .Should().BeFalse("no longer in the list");
        m.ParticipantCount.Should().Be(4, "three remaining participants");
    }

    [Fact]
    public void GetResultList_Empty()
    {
        var m = CreateSampleMarathon();

        m.GetResultList()
            .Should().NotBeNull()
            .And.BeEmpty("no participants added")
            .And.BeSameAs(Array.Empty<string>());
    }

    [Fact]
    public void GetResultList_Simple()
    {
        var m = CreateSampleMarathon();
        Participant[] participants = CreateSampleParticipants();

        foreach (var participant in participants)
        {
            m.AddParticipant(participant);
        }
        m.RemoveParticipant(participants[1].StartNo);

        m.GetResultList().Should()
            .NotBeNullOrEmpty()
            .And.HaveCount(4, "one participant was removed again")
            .And.ContainInOrder(new[]
            {
                "#01 Val (Start# 408) finished in 02:05:41",
                "#02 Jeb (Start# 591) finished in 02:05:41",
                "#03 Bob (Start# 167) finished in 03:34:18",
                "#04 Wernher (Start# 008) finished in 05:25:41"
            }, "insert in list has to happen in order!");
    }

    [Fact]
    public void ToString_Simple()
    {
        var m = CreateSampleMarathon();

        m.ToString().Should().Be("Leonding marathon on 15.11.2022");
    }

    private static Marathon CreateSampleMarathon() => new("Leonding", new(2022, 11, 15));

    private static Participant[] CreateSampleParticipants() => new Participant[]
    {
        new(591, "Jeb", new(2, 05, 41)),
        new(24, "Bill", new(4, 15, 23)),
        new(167, "Bob", new(3, 34, 18)),
        new(408, "Val", new(2, 05, 41)),
        new(8, "Wernher", new(5, 25, 41))
    };
}