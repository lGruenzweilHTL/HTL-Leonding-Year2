namespace PetsAndFleas.Test;

public sealed class FleaTests
{
    private readonly Flea _flea = new();
    
    [Fact]
    public void JumpOnPet()
    {
        Pet pet = new Cat();
        
        _flea.CurrentPet.Should().BeNull("did not yet jump on a pet");
        _flea.JumpOnPet(pet);
        _flea.CurrentPet.Should().NotBeNull().And.BeSameAs(pet);
        _flea.TotalBites.Should().Be(0, "no bites yet");
    }

    [Fact]
    public void JumpOnPet_Leave()
    {
        Pet pet = new Cat();
        
        _flea.JumpOnPet(pet);
        _flea.CurrentPet.Should().NotBeNull().And.BeSameAs(pet);
        
        _flea.JumpOnPet(null);
        _flea.CurrentPet.Should().BeNull("flea left pet");
    }

    [Fact]
    public void BitePet_Simple()
    {
        const int BiteAmount = 15;
        Pet pet = new Dog(new());
        
        _flea.JumpOnPet(pet);

        _flea.BitePet(BiteAmount)
             .Should().Be(BiteAmount, "flea sits on pet and pet has sufficient 'free bites'");
        _flea.TotalBites.Should().Be(BiteAmount);
        pet.RemainingBites.Should().Be(85, "100 - 15 = 85");
    }
    
    [Fact]
    public void BitePet_NoPet()
    {
        _flea.BitePet(10).Should().Be(0, "flea not sitting on a pet");
    }
    
    [Fact]
    public void BitePet_NegativeBites()
    {
        Pet pet = new Cat();
        
        _flea.JumpOnPet(pet);

        _flea.BitePet(-10)
             .Should().Be(0, "negative bites are not executed");
        _flea.TotalBites.Should().Be(0, "no bites performed");
        pet.RemainingBites.Should().Be(100, "no bites occurred");
    }

    [Fact]
    public void BitePet_NotEnoughAvailableBites()
    {
        Pet pet = new Cat();
        
        _flea.JumpOnPet(pet);
        _flea.BitePet(60);

        _flea.BitePet(50)
             .Should().Be(40, "there were only 40 'free bites' remaining");
        _flea.TotalBites.Should().Be(100, "60 + 40");
    }

    [Fact]
    public void BitePet_MultiplePets()
    {
        Pet pet1 = new Dog(new());
        Pet pet2 = new Cat();
        
        _flea.JumpOnPet(pet1);
        _flea.BitePet(20);
        _flea.BitePet(15);
        
        _flea.JumpOnPet(pet2);
        _flea.BitePet(40);
        
        _flea.JumpOnPet(pet1);
        _flea.BitePet(10);
        
        _flea.JumpOnPet(null);

        _flea.TotalBites.Should().Be(85, "spread across two pets");
        pet1.RemainingBites.Should().Be(55);
        pet2.RemainingBites.Should().Be(60);
        _flea.CurrentPet.Should().BeNull();
    }

    [Fact]
    public void StringRepresentation() => new Flea().ToString().Should().Be("I'm a flea");
    
    
    #region you wouldn't normally write such tests, only for school context

    [Fact]
    public void IsSealed()
    {
        typeof(Flea).IsSealed
                   .Should().BeTrue("nothing inherits from Flea");
    }

    [Fact]
    public void InheritsDirectlyFromObject()
    {
        typeof(Flea).BaseType.Should().Be(typeof(object), 
                                          "does not inherit from anything except object");
    }

    #endregion
}
