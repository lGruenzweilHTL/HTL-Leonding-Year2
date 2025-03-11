using Avalonia;
using Shapes.Shapes;

namespace ShapesTests;

public class RectangleTests
{
    public static TheoryData<Point, double, double, Point, bool> ContainsPointTestData => new()
    {
        // Point inside the rectangle
        { new Point(0, 0), 10, 5, new Point(0, 0), true },
        // Point outside the rectangle
        { new Point(10, 10), 10, 5, new Point(0, 0), false },
        // Point on the edge of the rectangle
        { new Point(5, 0), 10, 5, new Point(0, 0), true },
        // Point on the corner of the rectangle
        { new Point(5, 2.5), 10, 5, new Point(0, 0), true },
        // Point just outside the edge of the rectangle
        { new Point(5.1, 0), 10, 5, new Point(0, 0), false },
        // Point just outside the corner of the rectangle
        { new Point(5.1, 2.6), 10, 5, new Point(0, 0), false },
        // Point on the negative edge of the rectangle
        { new Point(-5, 0), 10, 5, new Point(0, 0), true },
        // Point on the negative corner of the rectangle
        { new Point(-5, -2.5), 10, 5, new Point(0, 0), true }
    };

    [Theory]
    [MemberData(nameof(ContainsPointTestData))]
    public void Rectangle_ContainsPoint_ShouldReturnExpectedResult(Point point, double width, double height, Point center, bool expected)
    {
        // Arrange
        var rectangle = new Rectangle(center, width, height);

        // Act
        bool result = rectangle.ContainsPoint(point);

        // Assert
        Assert.Equal(expected, result);
    }
}
