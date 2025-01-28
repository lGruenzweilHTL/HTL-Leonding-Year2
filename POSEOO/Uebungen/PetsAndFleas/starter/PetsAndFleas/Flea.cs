namespace PetsAndFleas;

public sealed class Flea
{
    public Pet? CurrentPet { get; private set; }
    public int TotalBites { get; private set; }
    public void JumpOnPet(Pet? pet) => CurrentPet = pet;
    public int BitePet(int amount) => CurrentPet == null || amount < 0 ? 0 : (TotalBites += Math.Clamp(amount, 0, CurrentPet.RemainingBites), CurrentPet.GetBitten(Math.Clamp(amount, 0, CurrentPet.RemainingBites))).Item2;
    public override string ToString() => "I'm a flea";
}