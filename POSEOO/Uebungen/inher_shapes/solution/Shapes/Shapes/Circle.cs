using Avalonia;
using SimpleXPlatDrawing;

namespace Shapes.Shapes;

public class Circle : Shape
{
    public double Radius { get; }

    public Circle(Point center, double radius) : base(center)
    {
        Radius = radius;
    }

    public override bool DrawSelf() => SimpleDrawing.DrawCircle(Center, Radius, lineColor: Color, fillColor: Color);

    public override bool ContainsPoint(Point p) => Point.Distance(Center, p) <= Radius;
}
