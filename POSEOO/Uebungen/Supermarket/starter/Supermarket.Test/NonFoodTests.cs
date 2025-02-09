namespace Supermarket.Test;

public sealed class NonFoodTests
{
    private const string Barcode = "12345670";
    
    [Theory]
    [InlineData("", Barcode, 4, Product.Invalid, Barcode, 4, "empty name")]
    [InlineData("Foo", "", 4, "Foo", Product.Invalid, 4, "empty barcode")]
    [InlineData("Foo", "1234", 4, "Foo", Product.Invalid, 4, "invalid barcode")]
    [InlineData("Foo", Barcode, -4, "Foo", Barcode, 0, "negative quantity set to 0")]
    [InlineData("Foo", Barcode, 4, "Foo", Barcode, 4, "correct values")]
    public void Construction_ProductBasics(string name, string barcode, int quantity,
                                           string expectedName, string expectedBarcode, int expectedQuantity,
                                           string reason)
    {
        var product = new NonFood(name, barcode, quantity);

        (product.ProductName, product.Barcode, product.Quantity)
            .Should().Be((expectedName, expectedBarcode, expectedQuantity), reason);
    }

    [Fact]
    public void Construction_Simple()
    {
        const string Name = "Chalk";
        const int Quantity = 9;
        
        var chalk = new NonFood(Name, Barcode, Quantity);

        chalk.ProductName.Should().Be(Name);
        chalk.Quantity.Should().Be(Quantity);
        chalk.Barcode.Should().Be(Barcode);
        chalk.AverageRating.Should().BeNull("no reviews yet");
        chalk.Reviews.Should()
             .NotBeNull("empty list was created")
             .And.BeEmpty("no reviews yet");
        chalk.Should().BeAssignableTo<Product>("a non food item is a product");
    }

    [Fact]
    public void AddReview_Single()
    {
        var review = new Review(DateTime.Now, Rating.FourStars, "Great fridge!");
        var fridge = new NonFood("Fridge", Barcode, 6);
        
        fridge.AddReview(review);

        fridge.Reviews.Should()
              .NotBeEmpty()
              .And.HaveCount(1, "one review added")
              .And.Contain(review);
        fridge.AverageRating.Should().BeApproximately(4D, double.Epsilon, 
                                                      "with a single review that is the average");
    }
    
    [Fact]
    public void AddReview_Multiple()
    {
        var fridge = new NonFood("Fridge", Barcode, 6);
        
        fridge.AddReview(new (DateTime.Now, Rating.FourStars, "Great fridge!"));
        fridge.AddReview(new (DateTime.Now, Rating.OneStar, "Broke on the first day :("));

        fridge.Reviews.Should()
              .NotBeEmpty()
              .And.HaveCount(2, "two reviews added");
        fridge.AverageRating.Should().BeApproximately(2.5D, double.Epsilon, 
                                                      "average rating");
    }
    
    [Fact]
    public void GetCsvHeader()
    {
        var fridge = new NonFood("Fridge", Barcode, 5);

        fridge.GetCsvHeader().Should().Be("Barcode;ProductName;Quantity;AverageRating");
    }

    [Fact]
    public void ToCsv()
    {
        var fridge = new NonFood("Fridge", Barcode, 5);

        fridge.ToCsv().Should().Be($"{Barcode};Fridge;5;", 
                                   "with no ratings the avg. rating column is present but empty");
        
        fridge.AddReview(new (DateTime.Now, Rating.FourStars, "Great fridge!"));
        fridge.AddReview(new (DateTime.Now, Rating.FiveStars, "Amazing fridge!"));
        fridge.AddReview(new (DateTime.Now, Rating.TwoStars, "Very loud"));
        fridge.ToCsv().Should().Be($"{Barcode};Fridge;5;3.7", 
                                   "avg. rating is rounded to 1 decimal place and uses invariant culture");
    }
}
