using Avalonia;
using Shapes.Shapes;

namespace ShapesTests;

public class TriangleTests
{
    public static TheoryData<Point, double, Point, bool> ContainsPointTestData => new()
    {
        // Point inside the triangle
        { new Point(0, 0), 10, new Point(0, 0), true },
        // Point outside the triangle
        { new Point(0, 0), 10, new Point(0, 10), false },
        // Point on the edge of the triangle
        { new Point(10, 10), 5, new Point(11, 12.16), true },
        // Point just outside the edge of the triangle
        { new Point(10, 10), 5, new Point(10, 15), false },
        // Point on the corner of the triangle
        { new Point(10, 10), 5, new Point(10, 7.84), true },
        // Point just outside the corner of the triangle
        { new Point(10, 10), 5, new Point(10, 5), false },
    };

    [Theory]
    [MemberData(nameof(ContainsPointTestData))]
    public void Triangle_ContainsPoint_ShouldReturnExpectedResult(Point center, double sideLength, Point point, bool expected)
    {
        // Arrange
        var triangle = new Triangle(center, sideLength);

        // Act
        bool result = triangle.ContainsPoint(point);

        // Assert
        Assert.Equal(expected, result);
    }
}
