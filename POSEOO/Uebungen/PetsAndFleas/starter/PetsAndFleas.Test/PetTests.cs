namespace PetsAndFleas.Test;

public sealed class PetTests
{
    [Fact]
    public void PetId()
    {
        int initialId = Pet.NextPetId;

        for (var i = 1; i < 5; i++)
        {
            int expectedId = initialId + i;
            Pet pet = i % 2 == 0
                ? new Dog(new())
                : new Cat();

            pet.PetId.Should().Be(expectedId, "each pet gets a unique id by incrementing a static counter");
        }
    }

    [Fact]
    public void RemainingBites_Initial()
    {
        Pet pet = new Cat();

        pet.RemainingBites
           .Should().Be(100, "initial value set by Pet ctor");
    }

    [Theory]
    [MemberData(nameof(GetBittenData))]
    public void GetBitten(Pet pet, int amountBites, int expectedActualBites, int expectedRemaining,
                          string reasonActualBites, string reasonRemainingBites)
    {
        pet.GetBitten(amountBites)
           .Should().Be(expectedActualBites, reasonActualBites);
        pet.RemainingBites
           .Should().Be(expectedRemaining, reasonRemainingBites);
    }

    public static TheoryData<Pet, int, int, int, string, string> GetBittenData()
    {
        Pet pet = new Dog(new());

        return new()
        {
            { pet, 40, 40, 60, "40 < 100, can perform all bites", "100 - 40 = 60" },
            { pet, 70, 60, 0, "70 > 60, can only perform 60 bytes", "no more bites possible" },
            { new Cat(), 200, 100, 0, "200 > 100, performing 100 bites", "all bites 'spent'" },
            { new Dog(new()), -50, 0, 100, "negative amount, ignored", "no bites have happened yet" }
        };
    }

    #region you wouldn't normally write such tests, only for school context

    [Fact]
    public void IsAbstract()
    {
        typeof(Pet).IsAbstract
                   .Should().BeTrue("Pet has to be abstract");
    }
    
    [Fact]
    public void InheritsDirectlyFromObject()
    {
        typeof(Pet).BaseType.Should().Be(typeof(object), 
                                         "does not inherit from anything except object");
    }

    [Fact]
    public void HasInheritors()
    {
        var dogType = typeof(Dog);
        var catType = typeof(Cat);
        var petType = typeof(Pet);

        petType.IsAssignableFrom(dogType)
               .Should().BeTrue("Pet is base class of Dog");
        petType.IsAssignableFrom(catType)
               .Should().BeTrue("Pet is base class of Cat");

        dogType.IsSealed
               .Should().BeTrue("nothing inherits from Dog");
        catType.IsSealed
               .Should().BeTrue("nothing inherits from Cat");
    }

    #endregion
}
