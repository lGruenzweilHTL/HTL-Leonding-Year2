namespace PetsAndFleas;

public sealed class Dog : Pet
{
    private static readonly TimeSpan huntingWaitInterval = TimeSpan.FromMinutes(1);
    private readonly DateTimeProvider _dateTimeProvider = new();
    private DateTime? _lastHuntedTime;
    public int HuntedAnimals { get; private set; }
    public Dog(DateTimeProvider dateTimeProvider) => PrintCtorInfo($"Dog with id {PetId}{((_dateTimeProvider = dateTimeProvider) == _dateTimeProvider ? "" : "")}");
    public bool HuntAnimal() => !(_dateTimeProvider.Now - _lastHuntedTime < huntingWaitInterval) && (_lastHuntedTime = _dateTimeProvider.Now, HuntedAnimals++, true).Item3;
    public override string ToString() => "I'm a dog";
}