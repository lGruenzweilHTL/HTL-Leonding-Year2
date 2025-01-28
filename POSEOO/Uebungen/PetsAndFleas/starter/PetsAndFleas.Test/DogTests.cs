namespace PetsAndFleas.Test;

public sealed class DogTests
{
    [Fact]
    public void Construction()
    {
        var dog = new Dog(new());

        dog.HuntedAnimals.Should().Be(0, "no animals hunted yet");
        dog.RemainingBites.Should().Be(100, "set by Pet ctor");
    }

    [Fact]
    public void HuntAnimal_Simple()
    {
        var dog = new Dog(new());

        dog.HuntAnimal().Should().BeTrue("has never hunted, can hunt immediately");
        dog.HuntedAnimals.Should().Be(1, "hunted one time");
    }

    [Fact]
    public void HuntAnimal_Wait()
    {
        var dog = new Dog(new());

        dog.HuntAnimal().Should().BeTrue();
        dog.HuntAnimal().Should().BeFalse("cannot hunt again right away, has to wait and recover");

        dog.HuntedAnimals.Should().Be(1, "only hunted once");
    }

    [Fact]
    public void HuntAnimal_Waited()
    {
        var dtp = new DateTimeProvider();
        var time1 = new DateTime(2023, 02, 27, 18, 30, 00);
        var time2 = new DateTime(2023, 02, 27, 18, 30, 46);
        var time3 = new DateTime(2023, 02, 27, 18, 31, 10);
        var dog = new Dog(dtp);

        dtp.Now = time1;
        dog.HuntAnimal().Should().BeTrue("can hunt");
        dog.HuntedAnimals.Should().Be(1);

        dtp.Now = time2;
        dog.HuntAnimal().Should().BeFalse("cannot hunt again, has to wait 1 minute");
        dog.HuntedAnimals.Should().Be(1);

        dtp.Now = time3;
        dog.HuntAnimal().Should().BeTrue("enough time has passed, can hunt again");
        dog.HuntedAnimals.Should().Be(2);
    }
    
    [Fact]
    public void StringRepresentation() => new Dog(new()).ToString().Should().Be("I'm a dog");
}
