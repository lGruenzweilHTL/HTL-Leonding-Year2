using FluentAssertions;
using Xunit;

namespace CoffeeVendingMachines.Test;

public sealed class ProductTests
{
    private const string Name = "Tea, Earl Grey, Hot";
    private const int Price = 80;
    private const int Stock = 6;

    [Fact]
    public void Construction_Simple()
    {
        var product = new Product(Name, Price, Stock);

        product.Name.Should().Be(Name);
        product.Price.Should().Be(Price, "valid price used");
        product.InStock.Should().BeTrue("stock is >0");
        product.NumberSold.Should().Be(0, "nothing sold yet");
    }

    [Fact]
    public void Construction_NoStock()
    {
        var product = new Product(Name, Price, 0);

        product.InStock.Should().BeFalse("stock 0");
    }

    [Fact]
    public void Construction_InvalidStock()
    {
        var product = new Product(Name, Price, -3);

        product.InStock.Should().BeFalse("stock set to 0, because invalid stock passed");
    }

    [Theory]
    [InlineData(36, "not a multiple of 5")]
    [InlineData(-5, "negative value")]
    [InlineData(0, "too small value")]
    public void Construction_InvalidPrice(int price, string reason)
    {
        var product = new Product(Name, price, Stock);

        product.Price.Should().Be(Product.FallbackPrice, reason);
    }

    [Fact]
    public void AddSale_Simple()
    {
        var product = new Product(Name, Price, Stock);

        product.NumberSold.Should().Be(0);
        product.AddSale().Should().BeTrue();
        product.NumberSold.Should().Be(1);
    }

    [Fact]
    public void AddSale_Multiple()
    {
        var product = new Product(Name, Price, Stock);

        product.NumberSold.Should().Be(0);
        
        for (var i = 0; i < 4; i++)
        {
            product.AddSale().Should().BeTrue();
        }

        product.NumberSold.Should().Be(4);
    }

    [Fact]
    public void AddSale_OutOfStock()
    {
        var product = new Product(Name, Price, 1);

        product.NumberSold.Should().Be(0);
        product.AddSale().Should().BeTrue();
        product.NumberSold.Should().Be(1);
        product.AddSale().Should().BeFalse("out of stock");
        product.NumberSold.Should().Be(1);
    }

    [Fact]
    public void StringRepresentation_Simple()
    {
        var product = new Product("Tea", Price, Stock);
        for (var i = 0; i < 4; i++)
        {
            product.AddSale();
        }

        product.ToString().Should()
            .NotBeNullOrWhiteSpace()
            .And.Be("Tea        € 0,80 [2 in stock | 4 sold]");
    }
}