namespace Supermarket.Test;

public sealed class FoodTests
{
    private const string Barcode = "12345670";

    [Fact]
    public void Construction_Simple()
    {
        const string Name = "Fischstäbchen";
        const int Quantity = 4;

        var fishStick = new Food(Name, Barcode, Quantity,
                                 AllergenType.A, AllergenType.C, AllergenType.D);

        fishStick.ProductName.Should().Be(Name);
        fishStick.Barcode.Should().Be(Barcode);
        fishStick.Quantity.Should().Be(Quantity);
        fishStick.Allergens.Should()
                 .NotBeNull()
                 .And.HaveCount(3)
                 .And.ContainInOrder(AllergenType.A, AllergenType.C, AllergenType.D);
        fishStick.Should().BeAssignableTo<Product>("a food item is a product");
    }

    [Fact]
    public void Construction_DuplicateAllergens()
    {
        var fishStick = new Food("Fischstäbchen", Barcode, 18,
                                 AllergenType.A, AllergenType.A, AllergenType.C, AllergenType.D);

        fishStick.Allergens.Should()
                 .NotBeNull()
                 .And.HaveCount(3, "duplicates are not added")
                 .And.ContainInOrder(AllergenType.A, AllergenType.C, AllergenType.D);
    }

    [Fact]
    public void Construction_AllergensInWrongOrder()
    {
        var fishStick = new Food("Fischstäbchen", Barcode, 18,
                                 AllergenType.C, AllergenType.D, AllergenType.A);

        fishStick.Allergens.Should()
                 .NotBeNull()
                 .And.HaveCount(3)
                 .And.ContainInOrder(new[] { AllergenType.A, AllergenType.C, AllergenType.D },
                                     "allergens are always stored in order");
    }

    [Fact]
    public void Construction_NoAllergens()
    {
        const string Name = "Steak";
        const int Quantity = 11;

        static void CheckSteak(Food steak)
        {
            steak.ProductName.Should().Be(Name);
            steak.Barcode.Should().Be(Barcode);
            steak.Quantity.Should().Be(Quantity);
            steak.Allergens.Should()
                 .NotBeNull("an empty list is always created")
                 .And.BeEmpty();
        }

        CheckSteak(new Food(Name, Barcode, Quantity));
        CheckSteak(new Food(Name, Barcode, Quantity, Array.Empty<AllergenType>()));
    }

    [Fact]
    public void ContainsAnyAllergen()
    {
        var cake = new Food("Torte", Barcode, 5,
                            AllergenType.A, AllergenType.C, AllergenType.F, AllergenType.G);

        cake.ContainsAnyAllergen(AllergenType.A).Should().BeTrue("allergen is contained");
        cake.ContainsAnyAllergen(AllergenType.F).Should().BeTrue("allergen is contained");
        cake.ContainsAnyAllergen(AllergenType.C, AllergenType.G)
            .Should().BeTrue("all allergens are contained");
        cake.ContainsAnyAllergen(AllergenType.F, AllergenType.C, AllergenType.A, AllergenType.G)
            .Should().BeTrue("all allergens are contained, order is irrelevant");

        cake.ContainsAnyAllergen(AllergenType.B).Should().BeFalse("allergen is not contained");
        cake.ContainsAnyAllergen(AllergenType.B, AllergenType.M)
            .Should().BeFalse("none of the allergens are contained");

        cake.ContainsAnyAllergen(AllergenType.B, AllergenType.F)
            .Should().BeTrue("some of the allergens are contained");
    }

    [Fact]
    public void AddAllergen_Simple()
    {
        var cake = new Food("Torte", Barcode, 5);

        cake.AddAllergen(AllergenType.B).Should().BeTrue("can be added");
        cake.Allergens.Should()
            .NotBeNull()
            .And.HaveCount(1)
            .And.ContainInOrder(AllergenType.B);

        cake.AddAllergen(AllergenType.A).Should().BeTrue("can be added");
        cake.Allergens.Should()
            .NotBeNull()
            .And.HaveCount(2)
            .And.ContainInOrder(new[] { AllergenType.A, AllergenType.B }, "insert happens in order");
    }

    [Fact]
    public void AddAllergen_AlreadyContained()
    {
        var cake = new Food("Torte", Barcode, 5);

        cake.AddAllergen(AllergenType.A).Should().BeTrue("can be added");
        cake.Allergens.Should()
            .NotBeNull()
            .And.HaveCount(1)
            .And.ContainInOrder(AllergenType.A);

        cake.AddAllergen(AllergenType.A).Should().BeFalse("already in the list");
        cake.Allergens.Should()
            .NotBeNull()
            .And.HaveCount(1, "unchanged")
            .And.ContainInOrder(AllergenType.A);
    }

    [Fact]
    public void RemoveAllergen_Simple()
    {
        var cake = new Food("Torte", Barcode, 5, AllergenType.A, AllergenType.B);

        cake.RemoveAllergen(AllergenType.A).Should().BeTrue("can be removed");
        cake.Allergens.Should()
            .NotBeNull()
            .And.HaveCount(1, "decreased")
            .And.ContainInOrder(AllergenType.B);

        cake.RemoveAllergen(AllergenType.B).Should().BeTrue("can be removed");
        cake.Allergens.Should()
            .NotBeNull()
            .And.BeEmpty("now empty");
    }

    [Fact]
    public void RemoveAllergen_NotContained()
    {
        var cake = new Food("Torte", Barcode, 5,
                            AllergenType.A, AllergenType.B, AllergenType.C);

        cake.RemoveAllergen(AllergenType.B).Should().BeTrue("can be removed");
        cake.Allergens.Should()
            .NotBeNull()
            .And.HaveCount(2, "one removed")
            .And.ContainInOrder(AllergenType.A, AllergenType.C);

        cake.RemoveAllergen(AllergenType.B).Should().BeFalse("already removed");
        cake.Allergens.Should()
            .NotBeNull()
            .And.HaveCount(2, "unchanged")
            .And.ContainInOrder(AllergenType.A, AllergenType.C);

        cake.RemoveAllergen(AllergenType.F).Should().BeFalse("was never contained");
        cake.Allergens.Should()
            .NotBeNull()
            .And.HaveCount(2, "unchanged")
            .And.ContainInOrder(AllergenType.A, AllergenType.C);
    }

    [Fact]
    public void GetCsvHeader()
    {
        var burger = new Food("Burger", Barcode, 2,
                              AllergenType.A, AllergenType.F, AllergenType.M);

        burger.GetCsvHeader().Should().Be("Barcode;ProductName;Quantity;Allergens");
    }

    [Fact]
    public void ToCsv()
    {
        var burger = new Food("Burger", Barcode, 2,
                              AllergenType.A, AllergenType.F, AllergenType.M);
        var water = new Food("Leitungswasser", Barcode, 1);

        burger.ToCsv().Should().Be($"{Barcode};Burger;2;A|F|M");
        water.ToCsv().Should().Be($"{Barcode};Leitungswasser;1;", 
                                  "no allergens, but column is still present");
    }
}
