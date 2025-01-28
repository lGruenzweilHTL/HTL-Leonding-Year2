namespace PetsAndFleas;

public sealed class Cat : Pet
{
    private bool _isOnTree;
    public int TreesClimbed { get; private set; }
    public Cat() => PrintCtorInfo($"Cat with id {PetId}");
    public bool ClimbOnTree() => (TreesClimbed += _isOnTree ? 0 : 1, !_isOnTree && (_isOnTree = true)).Item2;
    public bool ClimbDown() => _isOnTree && (_isOnTree = false, true).Item2;
    public override string ToString() => "I'm a cat";
}