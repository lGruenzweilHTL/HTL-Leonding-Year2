namespace PetsAndFleas;

public abstract class Pet
{
    public static int NextPetId { get; private set; }
    public int PetId { get; }
    public int RemainingBites { get; private set; } = 100;
    protected Pet() => PrintCtorInfo($"Pet with id {PetId = ++NextPetId}");
    public int GetBitten(int amount) => (Math.Clamp(amount, 0, RemainingBites), RemainingBites = Math.Clamp(RemainingBites - amount, 0, 100)).Item1;
    protected static void PrintCtorInfo(string s) => Console.WriteLine($"Ctor: {s}");
}