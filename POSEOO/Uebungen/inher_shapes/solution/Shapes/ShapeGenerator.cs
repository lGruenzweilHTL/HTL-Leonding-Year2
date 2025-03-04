using System.Diagnostics;
using Avalonia;
using Shapes.Shapes;

namespace Shapes;

/// <summary>
///     A random shape generator
/// </summary>
public class ShapeGenerator
{
    private const int MinShapeIdx = 1;
    private const int MaxShapeIdx = 4;
    private readonly double _maxX;
    private readonly double _maxY;
    private readonly Random _random;
    private int _nextShape;

    /// <summary>
    ///     Creates a new generator which will create random shapes within the canvas area
    /// </summary>
    /// <param name="maxX">The max x axis value where shapes may be drawn</param>
    /// <param name="maxY">The max y axis value where shapes may be drawn</param>
    public ShapeGenerator(double maxX, double maxY)
    {
        _maxX = maxX;
        _maxY = maxY;
        _nextShape = MinShapeIdx;
        _random = Random.Shared;
    }

    /// <summary>
    ///     Creates a new random shape at the specified location.
    ///     A circle, rectangle, square or triangle will be created.
    ///     The size of the shape is randomly chosen, but will not exceed the max. drawing
    ///     boundaries - except for at most 5 pixels to allow rendering in any case.
    ///     Shapes may overlap each other.
    /// </summary>
    /// <param name="atLocation">Location of the center point of the new shape</param>
    /// <returns>The newly created shape</returns>
    public Shape CreateNewShape(Point atLocation)
    {
        int shape = _nextShape++;
        if (_nextShape > MaxShapeIdx)
        {
            _nextShape = MinShapeIdx;
        }

        var (xLimit, yLimit) = FindLimits(atLocation);
        return shape switch
               {
                   1                              => CreateSquare(atLocation, Math.Min(xLimit, yLimit)),
                   2                              => CreateCircle(atLocation, Math.Min(xLimit, yLimit)),
                   3                              => CreateRectangle(atLocation, xLimit, yLimit),
                   4                              => CreateTriangle(atLocation, Math.Min(xLimit, yLimit)),
                   < MinShapeIdx or > MaxShapeIdx => throw new UnreachableException()
               };
    }

    private Shape CreateRectangle(Point center, double widthLimit, double heightLimit)
    {
        const int MinSideLength = 10;
        const int MaxSideLength = 150;

        var width = Math.Min(widthLimit, _random.Next(MinSideLength, MaxSideLength));
        var height = Math.Min(heightLimit, _random.Next(MinSideLength, MaxSideLength));

        return new Rectangle(center, width, height);
    }

    private Triangle CreateTriangle(Point center, double maxPerimeterRadius)
    {
        const int MinSideLength = 10;
        const int MaxSideLength = 150;

        var maxSideLength = maxPerimeterRadius * Math.Sqrt(3);
        var sideLength = Math.Min(maxSideLength, _random.Next(MinSideLength, MaxSideLength));

        return new Triangle(center, sideLength);
    }

    private Circle CreateCircle(Point center, double maxRadius)
    {
        const int MinRadius = 5;
        const int MaxRadius = 100;

        var radius = Math.Min(maxRadius, _random.Next(MinRadius, MaxRadius));

        return new Circle(center, radius);
    }

    private Square CreateSquare(Point center, double maxSideLength)
    {
        const int MinSideLength = 10;
        const int MaxSideLength = 120;

        var sideLength = Math.Min(maxSideLength, _random.Next(MinSideLength, MaxSideLength));

        return new Square(center, sideLength);
    }

    private (double xLimit, double yLimit) FindLimits(Point centerLocation)
    {
        var xLimit = Math.Min(centerLocation.X, _maxX - centerLocation.X);
        var yLimit = Math.Min(centerLocation.Y, _maxY - centerLocation.Y);

        return (xLimit - 1, yLimit - 1);
    }
}
