using Avalonia;
using SimpleXPlatDrawing;

namespace Shapes.Shapes;

public class Rectangle : Shape
{
    public double Width { get; }
    public double Heigth { get; }
    
    private Point TopLeft => new(Center.X - Width / 2, Center.Y - Heigth / 2);
    private Point TopRight => new(Center.X + Width / 2, Center.Y - Heigth / 2);
    private Point BottomLeft => new(Center.X - Width / 2, Center.Y + Heigth / 2);
    private Point BottomRight => new(Center.X + Width / 2, Center.Y + Heigth / 2);
    
    public Rectangle(Point center, double width, double heigth) : base(center)
    {
        Width = width;
        Heigth = heigth;
    }

    public override bool DrawSelf() => SimpleDrawing.DrawRectangle(TopLeft, BottomRight, lineColor: Color, fillColor: Color);

    public override bool ContainsPoint(Point p)
    {
        (Point, Point)[] edges = [
            (TopLeft, TopRight),
            (TopRight, BottomRight),
            (BottomRight, BottomLeft),
            (BottomLeft, TopLeft)
        ];

        return !ShapeTools.CheckPointIsOnOutside(edges, p);
    }
}
