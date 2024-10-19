using JetBrains.Annotations;
using Xunit;
using FluentAssertions;

namespace Rectangle.Tests;

[TestSubject(typeof(Rectangle))]
public class RectangleTest {

    [Fact]
    public void Construct_Empty() {
        Rectangle rect = new Rectangle();
        
        rect.Width.Should().Be(1);
        rect.Length.Should().Be(1);
        rect.XPos.Should().Be(0);
        rect.YPos.Should().Be(0);
        
        rect.Area.Should().Be(1);
        rect.Perimeter.Should().Be(4);
        
        rect.Color.Should().Be(default);
    }
    
    [Fact]
    public void Construct_Full() {
        Rectangle rect = new Rectangle(5, 6, 7, 8);
        
        rect.Width.Should().Be(5);
        rect.Length.Should().Be(6);
        rect.XPos.Should().Be(7);
        rect.YPos.Should().Be(8);
        
        rect.Area.Should().Be(30);
        rect.Perimeter.Should().Be(22);
        
        rect.Color.Should().Be(default);
    }
    
    [Fact]
    public void CompareTo_Null() {
        Rectangle rect = new Rectangle();
        rect.CompareTo(null).Should().Be(1);
    }
    
    [Theory]
    [MemberData(nameof(CompareToData))]
    public void CompareTo(Rectangle rect1, Rectangle rect2, int expected) {
        rect1.CompareTo(rect2).Should().Be(expected);
    }

    public static TheoryData<Rectangle, Rectangle, int> CompareToData = new() {
        { new Rectangle(), new Rectangle(), 0 },
        { new Rectangle(5, 6, 7, 8), new Rectangle(4, 3, 2, 1), 1},
        { new Rectangle(4, 3, 2, 1), new Rectangle(7, 9, 0, 0), -1},
        { new Rectangle(4, 3, 2, 1), new Rectangle(4, 3, 2, 1), 0}
    };
}