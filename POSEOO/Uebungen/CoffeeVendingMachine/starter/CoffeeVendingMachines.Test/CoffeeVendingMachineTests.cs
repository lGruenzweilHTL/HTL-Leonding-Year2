using FluentAssertions;
using Xunit;

namespace CoffeeVendingMachines.Test;

public sealed class CoffeeVendingMachineTests
{
    private const string Location = "Room 204a";

    [Fact]
    public void Construction_Default()
    {
        var machine = new CoffeeVendingMachine(Location);

        machine.Location.Should()
            .NotBeNullOrWhiteSpace()
            .And.Be(Location);
        machine.TotalMoneyCurrentlyInput
            .Should().Be(0, "no coins have been inserted yet");
        machine.TotalChangeAmountInMachine
            .Should().Be(1155, "three pieces of each coin are the default set");
        machine.AvailableProducts.Should()
            .NotBeNullOrEmpty()
            .And.HaveCount(3)
            .And.BeEquivalentTo(new Product[]
            {
                new("Cappuccino", 85, 10),
                new("Mocca", 100, 10),
                new("Cacao", 60, 10)
            }, "those three products are created by default if none are passed to the ctor");
    }

    [Fact]
    public void Construction_Full()
    {
        var products = GetDefaultProducts();
        var machine = new CoffeeVendingMachine(GetDefaultCoinDepots(),
            products, Location);

        machine.Location.Should()
            .NotBeNullOrWhiteSpace()
            .And.Be(Location);
        machine.TotalMoneyCurrentlyInput
            .Should().Be(0, "no coins have been inserted yet");
        machine.TotalChangeAmountInMachine
            .Should().Be(1385, "sum of coin depot values");
        machine.AvailableProducts.Should()
            .NotBeNullOrEmpty()
            .And.HaveCount(products.Length)
            .And.BeEquivalentTo(products);
    }

    [Fact]
    public void InsertCoin_Simple()
    {
        var machine = GetDefaultMachine();

        machine.TotalMoneyCurrentlyInput.Should().Be(0);
        
        machine.InsertCoin(CoinType.Cent20).Should().BeTrue();
        machine.TotalMoneyCurrentlyInput.Should().Be(20);
    }

    [Fact]
    public void InsertCoin_Multiple()
    {
        var machine = GetDefaultMachine();

        machine.TotalMoneyCurrentlyInput.Should().Be(0);

        machine.InsertCoin(CoinType.Cent20).Should().BeTrue();
        machine.InsertCoin(CoinType.Cent50).Should().BeTrue();
        machine.InsertCoin(CoinType.Cent50).Should().BeTrue();
        machine.InsertCoin(CoinType.Euro02).Should().BeTrue();
        machine.TotalMoneyCurrentlyInput.Should().Be(320, "20 + 2x50 + 200");
    }

    [Fact]
    public void Cancel_Simple()
    {
        var machine = new CoffeeVendingMachine(Location);

        void Insert(CoinType coin, int times)
        {
            for (var i = 0; i < times; i++)
            {
                machine.InsertCoin(coin);
            }
        }

        Insert(CoinType.Cent10, 2);
        Insert(CoinType.Cent20, 1);
        Insert(CoinType.Cent50, 3);
        Insert(CoinType.Euro01, 1);

        machine.Cancel().Should().NotBeNull()
            .And.HaveCount(4, 
                "only four kinds of coins have been inserted, the other two input depots were empty")
            .And.BeEquivalentTo(new CoinDepot[]
            {
                new(CoinType.Cent10, 2),
                new(CoinType.Cent20, 1),
                new(CoinType.Cent50, 3),
                new(CoinType.Euro01, 1)
            });
    }

    [Fact]
    public void Cancel_Empty()
    {
        var machine = new CoffeeVendingMachine(Location);

        machine.Cancel().Should().NotBeNull()
            .And.BeEmpty("no coins have been inserted");
    }

    [Fact]
    public void SelectProduct_Simple()
    {
        var machine = GetDefaultMachine();
        machine.InsertCoin(CoinType.Euro01);

        machine.SelectProduct("Coffee1", out CoinDepot[]? change, out var losses)
            .Should().BeTrue("enough money inserted, product exists and is in stock");
        change.Should().NotBeNullOrEmpty("selected product less than money inserted, change returned")
            .And.HaveCount(1, "change money returned in as few coins as possible and 10 cent coin available")
            .And.BeEquivalentTo(new[] { new CoinDepot(CoinType.Cent10, 1) });
        losses.Should().BeNull("change could be returned in total");
        machine.TotalMoneyCurrentlyInput.Should().Be(0, "product bought, change returned");
        machine.TotalChangeAmountInMachine.Should().Be(1475, "1385 + 100 - 10");
        machine.AvailableProducts[2].NumberSold.Should().Be(1, "sale registered in product");
    }

    [Fact]
    public void SelectProduct_ProductNotExists()
    {
        var machine = GetDefaultMachine();
        machine.InsertCoin(CoinType.Euro02);

        machine.SelectProduct("Potato Salad", out CoinDepot[]? change, out var losses)
            .Should().BeFalse("product does not exist");
        change.Should().BeNull("no change return triggered");
        losses.Should().BeNull("no change return triggered");
        machine.TotalMoneyCurrentlyInput.Should().Be(200, "input unchanged");
        machine.TotalChangeAmountInMachine.Should().Be(1385, "change money unchanged");
    }

    [Fact]
    public void SelectProduct_NotEnoughMoney()
    {
        var machine = GetDefaultMachine();
        machine.InsertCoin(CoinType.Cent50);

        machine.SelectProduct("Milk", out CoinDepot[]? change, out var losses)
            .Should().BeFalse("product costs more than input");
        change.Should().BeNull("no change return triggered");
        losses.Should().BeNull("no change return triggered");
        machine.TotalMoneyCurrentlyInput.Should().Be(50, "input unchanged");
        machine.TotalChangeAmountInMachine.Should().Be(1385, "change money unchanged");
    }

    [Fact]
    public void SelectProduct_ProductOutOfStock()
    {
        var machine = new CoffeeVendingMachine(GetDefaultCoinDepots(),
            new Product[] { new("Soup", 80, 0) }, Location);
        machine.InsertCoin(CoinType.Euro01);

        machine.SelectProduct("Soup", out CoinDepot[]? change, out var losses)
            .Should().BeFalse("product is out of stock");
        change.Should().BeNull("no change return triggered");
        losses.Should().BeNull("no change return triggered");
        machine.TotalMoneyCurrentlyInput.Should().Be(100, "input unchanged");
        machine.TotalChangeAmountInMachine.Should().Be(1385, "change money unchanged");
    }

    [Fact]
    public void SelectProduct_ProductGoingOutOfStock()
    {
        var machine = new CoffeeVendingMachine(GetDefaultCoinDepots(),
            new Product[] { new("Soup", 80, 1) }, Location);
        machine.InsertCoin(CoinType.Euro01);

        machine.SelectProduct("Soup", out _, out _).Should().BeTrue();
        machine.AvailableProducts.Should().BeEmpty("we had one product and that got sold");
    }

    [Fact]
    public void SelectProduct_NotEnoughChange()
    {
        var machine = new CoffeeVendingMachine(new CoinDepot[]
            {
                new(CoinType.Cent05),
                new(CoinType.Cent10),
                new(CoinType.Cent20),
                new(CoinType.Cent50, 1),
                new(CoinType.Euro01),
                new(CoinType.Euro02)
            },
            GetDefaultProducts(), Location);
        machine.InsertCoin(CoinType.Euro01);
        machine.InsertCoin(CoinType.Euro02);

        machine.SelectProduct("Soup", out CoinDepot[]? change, out var losses)
            .Should().BeTrue("product is in stock and customer inserted enough money");
        change.Should()
            .NotBeNull("customer entered more money than required so we return change")
            .And.HaveCount(1, "we only have a single 50 cent coin")
            .And.BeEquivalentTo(new[] { new CoinDepot(CoinType.Cent50, 1) });
        losses.Should()
            .NotBeNull("we are unable to return the full change of 70 cents")
            .And.Be(20, "customer looses 20 cents which we are unable to return");
        machine.TotalMoneyCurrentlyInput.Should().Be(0, "user input transferred to change depots");
        machine.TotalChangeAmountInMachine.Should().Be(300,
            "we were unable to return all the change but we kept all the input as future change money");
    }

    [Fact]
    public void SelectProduct_InputAsChange()
    {
        var machine = new CoffeeVendingMachine(new CoinDepot[]
            {
                new(CoinType.Cent05),
                new(CoinType.Cent10),
                new(CoinType.Cent20),
                new(CoinType.Cent50, 1),
                new(CoinType.Euro01),
                new(CoinType.Euro02)
            },
            GetDefaultProducts(), Location);
        machine.InsertCoin(CoinType.Cent10);
        machine.InsertCoin(CoinType.Cent05);
        machine.InsertCoin(CoinType.Cent05);
        machine.InsertCoin(CoinType.Cent20);
        machine.InsertCoin(CoinType.Euro02);

        machine.SelectProduct("Soup", out CoinDepot[]? change, out var losses)
            .Should().BeTrue("product is in stock and customer inserted enough money");
        change.Should()
            .NotBeNull("customer entered more money than required so we return change")
            .And.HaveCount(1, "we return a 10 cent coin")
            .And.BeEquivalentTo(new CoinDepot[]
            {
                new(CoinType.Cent10, 1)
            });
        losses.Should()
            .BeNull("we are able to return the full change of 10 cents");
        machine.TotalMoneyCurrentlyInput.Should().Be(0, "user input transferred to change depots");
        machine.TotalChangeAmountInMachine.Should().Be(280,
            "we used some of the input as change, but we kept the rest as future change money");
    }

    [Fact]
    public void StringRepresentation_Simple()
    {
        var machine = GetDefaultMachine();
        machine.InsertCoin(CoinType.Cent50);
        machine.InsertCoin(CoinType.Cent20);
        machine.InsertCoin(CoinType.Cent20);
        machine.InsertCoin(CoinType.Cent20);
        machine.SelectProduct("Cacao", out _, out _);

        machine.ToString().Should()
            .NotBeNullOrWhiteSpace()
            .And.Be(@"|=======================================|
|Machine located at: Room 204a          |
|=======================================|
|Cent05 x5                              |
|Cent10 x9                              |
|Cent20 x11                             |
|Cent50 x7                              |
|Euro01 x4                              |
|Euro02 x2                              |
|=======================================|
|Milk       € 0,75 [4 in stock | 0 sold]|
|Soup       € 2,30 [2 in stock | 0 sold]|
|Coffee1    € 0,90 [8 in stock | 0 sold]|
|Coffee2    € 0,85 [8 in stock | 0 sold]|
|Cacao      € 1,00 [4 in stock | 1 sold]|
|=======================================|"
                );
    }

    private static CoffeeVendingMachine GetDefaultMachine() => new(GetDefaultCoinDepots(),
        GetDefaultProducts(), Location);
    private static Product[] GetDefaultProducts() => new Product[]
    {
        new("Milk", 75, 4),
        new("Soup", 230, 2),
        new("Coffee1", 90, 8),
        new("Coffee2", 85, 8),
        new("Cacao", 100, 5)
    };
    private static CoinDepot[] GetDefaultCoinDepots() => new CoinDepot[]
    {
        new(CoinType.Cent05, 05),
        new(CoinType.Cent10, 10),
        new(CoinType.Cent20, 08),
        new(CoinType.Cent50, 06),
        new(CoinType.Euro01, 04),
        new(CoinType.Euro02, 02)
    };
    private static CoinDepot[] GetEmptyCoinDepots() => new CoinDepot[]
    {
        new(CoinType.Cent05),
        new(CoinType.Cent10),
        new(CoinType.Cent20),
        new(CoinType.Cent50),
        new(CoinType.Euro01),
        new(CoinType.Euro02)
    };
}