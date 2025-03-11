using Avalonia;
using Shapes.Shapes;

namespace ShapesTests;

public class CircleTests
{
    public static TheoryData<Point, double, Point, bool> ContainsPointTestData => new()
    {
        // Point inside the circle
        { new Point(0, 0), 10, new Point(0, 0), true },
        // Point on the edge of the circle
        { new Point(10, 0), 10, new Point(0, 0), true },
        // Point outside the circle
        { new Point(10.1, 0), 10, new Point(0, 0), false },
        // Point just inside the edge of the circle
        { new Point(9.9, 0), 10, new Point(0, 0), true },
        // Point at the center of the circle
        { new Point(0, 0), 10, new Point(0, 0), true },
        // Point on the negative edge of the circle
        { new Point(-10, 0), 10, new Point(0, 0), true },
        // Point just outside the negative edge of the circle
        { new Point(-10.1, 0), 10, new Point(0, 0), false },
        // Point on the edge of the circle with non-zero center
        { new Point(15, 10), 5, new Point(10, 10), true },
        // Point outside the circle with non-zero center
        { new Point(16, 10), 5, new Point(10, 10), false },
        // Point inside the circle with non-zero center
        { new Point(12, 10), 5, new Point(10, 10), true }
    };

    [Theory]
    [MemberData(nameof(ContainsPointTestData))]
    public void Circle_ContainsPoint_ShouldReturnExpectedResult(Point point, double radius, Point center, bool expected)
    {
        // Arrange
        var circle = new Circle(center, radius);

        // Act
        bool result = circle.ContainsPoint(point);

        // Assert
        Assert.Equal(expected, result);
    }
}
