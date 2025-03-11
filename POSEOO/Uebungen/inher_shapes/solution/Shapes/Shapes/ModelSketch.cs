using Avalonia;
using Avalonia.Media;

namespace Shapes.Shapes;
//TODO: 1 Perform OOA
//TODO: 2 Perform OOD
//TODO: 3 Perform OOP
//TODO: 4 Document the classes as PUML
//TODO: 5 Implement the classes using a test driven approach
//TODO: 6 Make the program run again


/// <summary>
/// Collection of helper methods for shapes
/// </summary>
public static class ShapeTools
{
    private static int _nextId = 1;
   
    public static int GetNextId() => _nextId++;
    
    public static readonly IBrush[] colors =
    {
        Brushes.Blue,
        Brushes.Cyan,
        Brushes.DarkGreen,
        Brushes.Firebrick,
        Brushes.Lime,
        Brushes.Orange,
        Brushes.Plum,
        Brushes.Yellow
    };
    
    public static bool CheckPointIsOnOutside((Point, Point)[] clockwiseEdges, Point pointToCheck)
    {
        foreach (var (start, end) in clockwiseEdges)
        {
            if (!IsOnRightSideOfEdge(start, end, pointToCheck))
            {
                return true;
            }
        }

        return false;
    }

    public static Point OffsetPoint(Point point, double xOffset, double yOffset) =>
        new(point.X + xOffset, point.Y + yOffset);
    
    /// <summary>
    ///     Checks if the given point is on the left hand side of the edge defined by two points.
    ///     This assumes that the edges of the shape are iterated in clockwise direction.
    /// </summary>
    /// <param name="edgeStart">Start point of the edge</param>
    /// <param name="edgeEnd">End point of the edge</param>
    /// <param name="pointToCheck">The point to check</param>
    /// <returns>
    ///     True if the point to check is located on the right hand side
    ///     of the given edge; false otherwise
    /// </returns>
    public static bool IsOnRightSideOfEdge(Point edgeStart, Point edgeEnd, Point pointToCheck)
    {
        double d = (edgeEnd.X - edgeStart.X) * (pointToCheck.Y - edgeStart.Y)
                   - (pointToCheck.X - edgeStart.X) * (edgeEnd.Y - edgeStart.Y);

        return d >= 0D; // Changed from '>' to fix an issue with the ContainsPoint method
    }
}
