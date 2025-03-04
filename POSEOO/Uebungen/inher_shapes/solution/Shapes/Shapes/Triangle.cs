using Avalonia;
using SimpleXPlatDrawing;

namespace Shapes.Shapes;

public class Triangle : Shape
{
    public Triangle(Point center, double sideLength) : base(center)
    {
        SideLength = sideLength;
    }

    public double SideLength { get; }
    public double Height => SideLength * Math.Sqrt(3) / 2;

    private Point[] Points =>
    [
        new Point(Center.X, Center.Y - Height / 2),
            new Point(Center.X + SideLength / 2, Center.Y + Height / 2),
            new Point(Center.X - SideLength / 2, Center.Y + Height / 2)
    ];
    
    public override bool DrawSelf() => SimpleDrawing.DrawPolygonByPath(Points, lineColor: Color, fillColor: Color);

    public override bool ContainsPoint(Point p)
    {
        var clockwiseEdges = new[]
        {
            (Points[0], Points[1]),
            (Points[1], Points[2]),
            (Points[2], Points[0])
        };
        return !ShapeTools.CheckPointIsOnOutside(clockwiseEdges, p);
    }
}
