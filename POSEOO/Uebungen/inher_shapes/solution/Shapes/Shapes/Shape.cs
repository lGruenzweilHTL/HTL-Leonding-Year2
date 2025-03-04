using Avalonia;
using Avalonia.Media;

namespace Shapes.Shapes;

public abstract class Shape
{
    public static int CurrentId = 0;
    public int Id { get; }
    public IBrush Color { get; }
    public Point Center { get; set; }

    protected Shape(Point center)
    {
        Id = ++CurrentId;
        Center = center;
        Color = ShapeTools.colors[Random.Shared.Next(0, ShapeTools.colors.Length)];
    }

    public abstract bool DrawSelf();
    public abstract bool ContainsPoint(Point p);
}
