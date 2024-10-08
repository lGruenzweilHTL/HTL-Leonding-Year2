using FluentAssertions;
using Xunit;

namespace CoffeeVendingMachines.Test;

public sealed class CoinDepotTests
{
    private const CoinType Type = CoinType.Cent10;
    private const int Count = 5;

    [Fact]
    public void Construction_Simple()
    {
        var depot = new CoinDepot(Type, Count);

        depot.Coin.Should().Be(Type);
        depot.Count.Should().Be(Count);
    }

    [Fact]
    public void Construction_NoCountProvided()
    {
        var depot = new CoinDepot(Type);

        depot.Coin.Should().Be(Type);
        depot.Count.Should().Be(0, "initialized with default value");
    }

    [Fact]
    public void Construction_Copy()
    {
        var depot = new CoinDepot(CoinType.Euro02, 12);

        var copy = new CoinDepot(depot);

        copy.Coin.Should().Be(depot.Coin);
        copy.Count.Should().Be(depot.Count);
    }

    [Fact]
    public void Add_Simple()
    {
        var depot = new CoinDepot(Type, Count);

        depot.Add();

        depot.Count.Should().Be(Count + 1);
    }

    [Fact]
    public void Add_Multiple()
    {
        var depot = new CoinDepot(Type, Count);

        depot.Add();
        depot.Add();

        depot.Count.Should().Be(Count + 2);
    }

    [Fact]
    public void Withdraw_Simple()
    {
        var depot = new CoinDepot(Type, Count);

        depot.Withdraw()
            .Should().BeTrue("there are still coins left in the depot");

        depot.Count.Should().Be(Count - 1);
    }

    [Fact]
    public void Withdraw_Multiple()
    {
        var depot = new CoinDepot(Type, Count);

        depot.Withdraw();
        depot.Withdraw()
            .Should().BeTrue("there are still coins left in the depot");

        depot.Count.Should().Be(Count - 2);
    }

    [Fact]
    public void Withdraw_NoCoins()
    {
        var depot = new CoinDepot(Type, 0);

        depot.Withdraw()
            .Should().BeFalse("no more coins left in the depot");

        depot.Count.Should().Be(0, "unchanged, because withdrawal failed");
    }

    [Fact]
    public void Clear_Simple()
    {
        var depot = new CoinDepot(Type, Count);

        depot.Clear();

        depot.Count.Should().Be(0);
    }

    [Fact]
    public void StringRepresentation_Simple()
    {
        var depot = new CoinDepot(Type, Count);

        depot.ToString().Should()
            .NotBeNullOrWhiteSpace()
            .And.Be("Cent10 x5");
    }
}