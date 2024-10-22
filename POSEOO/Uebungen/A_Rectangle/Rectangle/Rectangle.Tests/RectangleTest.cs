using System;
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
        
        rect.Color.Should().Be(ConsoleColor.White);
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
        
        rect.Color.Should().Be(ConsoleColor.White);
    }

    [Fact]
    public void TestColorSet()
    {
        Rectangle rect = new Rectangle();

        rect.Color.Should().Be(ConsoleColor.White);

        rect.Color = ConsoleColor.Red;
        rect.Color.Should().Be(ConsoleColor.Red);
    }

    [Fact]
    public void TestRotate_Valid()
    {
        Rectangle rect = new(1, 2, 3, 4);
        rect.Width.Should().Be(1);
        rect.Length.Should().Be(2);
        
        rect.Rotate();

        rect.Length.Should().Be(1);
        rect.Width.Should().Be(2);
    }

    [Theory]
    [InlineData(1, 4, 2)]
    [InlineData(2, 8, 4)]
    [InlineData(1.32, 5.28, 2.64)]
    [InlineData(-5, -20, -10)]
    public void TestScale_Valid(double factor, double expectedWidth, double expectedLength)
    {
        Rectangle rect = new(4, 2, 0, 0);
        rect.Scale(factor);

        rect.Width.Should().Be(expectedWidth);
        rect.Length.Should().Be(expectedLength);
    }
    
    [Fact]
    public void CompareTo_Null_ReturnsBigger() {
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