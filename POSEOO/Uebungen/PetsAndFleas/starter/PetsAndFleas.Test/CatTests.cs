namespace PetsAndFleas.Test;

public sealed class CatTests
{
    private readonly Cat _cat = new();
    
    [Fact]
    public void ClimbOnTree_Simple()
    {
        _cat.TreesClimbed.Should().Be(0, "no trees climbed yet");
        _cat.ClimbOnTree().Should().BeTrue("was not on tree, could climb");
        _cat.TreesClimbed.Should().Be(1, "1 tree climbed");
    }
    
    [Fact]
    public void ClimbOnTree_AlreadyOnTree()
    {
        _cat.TreesClimbed.Should().Be(0, "no trees climbed yet");
        _cat.ClimbOnTree().Should().BeTrue("was not on tree, could climb");
        _cat.ClimbOnTree().Should().BeFalse("already on tree");
        _cat.TreesClimbed.Should().Be(1, "1 tree climbed");
    }

    [Fact]
    public void ClimbDown_NotOnTree()
    {
        _cat.ClimbDown()
            .Should().BeFalse("was not on tree, cannot climb down");
    }
    
    [Fact]
    public void ClimbDown_OnTree()
    {
        _cat.ClimbOnTree();
        _cat.ClimbDown()
            .Should().BeTrue("was on tree, could climb down");
    }
    
    [Fact]
    public void ClimbOnTreeAndDown_Simple()
    {
        _cat.TreesClimbed.Should().Be(0, "no trees climbed yet");
        _cat.ClimbOnTree().Should().BeTrue("was not on tree, could climb");
        _cat.ClimbDown().Should().BeTrue("was on tree, could climb down");
        _cat.TreesClimbed.Should().Be(1, "1 tree climbed");
    }

    [Fact]
    public void ClimbOnTreeAndDown_Multiple()
    {
        _cat.TreesClimbed.Should().Be(0, "no trees climbed yet");
        
        _cat.ClimbOnTree();
        _cat.ClimbDown();
        _cat.ClimbOnTree();
        _cat.ClimbDown();
        
        _cat.TreesClimbed.Should().Be(2, "two trees climbed");
    }

    [Fact]
    public void StringRepresentation() => new Cat().ToString().Should().Be("I'm a cat");
}
