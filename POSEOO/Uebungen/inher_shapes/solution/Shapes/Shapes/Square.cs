using Avalonia;

namespace Shapes.Shapes;

public class Square : Rectangle
{
    public Square(Point center, double sideLength) : base(center, sideLength, sideLength)
    {
    }
}
