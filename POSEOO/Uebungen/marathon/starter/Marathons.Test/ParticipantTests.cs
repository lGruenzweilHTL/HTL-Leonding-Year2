using FluentAssertions;
using Xunit;

namespace Marathons.Test;

public sealed class ParticipantTests
{
    [Fact]
    public void Construction()
    {
        const string Name = "Bob";
        const int StartNo = 11;
        TimeSpan duration = new(02, 05, 04);
        
        var bob = new Participant(StartNo, Name, duration);

        (bob.Name, bob.StartNo, bob.CompletionTime).Should()
            .Be((Name, StartNo, duration));
    }

    [Fact]
    public void CompareTo_DifferentCompletionTime()
    {
        var bob = new Participant(10, "Bob", new(3, 4, 5));
        var val = new Participant(11, "Val", new(2, 4, 5));

        bob.CompareTo(val)
            .Should().Be(1, "Bob took longer for the run");
        val.CompareTo(bob)
            .Should().Be(-1, "Val was faster");
    }
    
    [Fact]
    public void CompareTo_SameCompletionTime()
    {
        // this case should normally not happen due to business rules, but we test behaviour anyway
        
        TimeSpan completionTime = new(3, 4, 5);
        var bob = new Participant(10, "Bob", completionTime);
        var val = new Participant(10, "Val", completionTime);

        bob.CompareTo(val)
            .Should().Be(0, "same time and same start no")
            .And.Be(val.CompareTo(bob), "True in both directions");
    }

    [Fact]
    public void ToString_Simple()
    {
        var bob = new Participant(2, "Bob", new(15, 01, 12));

        bob.ToString().Should().Be("Bob (Start# 002) finished in 15:01:12");
    }
}