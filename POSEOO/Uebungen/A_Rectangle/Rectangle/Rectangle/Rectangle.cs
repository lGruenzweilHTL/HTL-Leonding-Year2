namespace Rectangle;

public class Rectangle : IComparable<Rectangle>
{
    public double Width { get; private set; }
    public double Length { get; private set; }

    /// <summary>
    /// The X-Coordinate of the Bottom-Left corner of the rectangle
    /// </summary>
    public double XPos { get; private set; }

    /// <summary>
    /// The Y-Coordinate of the Bottom-Left corner of the rectangle
    /// </summary>
    public double YPos { get; private set; }

    public ConsoleColor Color { get; set; } = ConsoleColor.White;

    public double Perimeter => 2 * (Width + Length);
    public double Area => Width * Length;

    public Rectangle(double width, double length, double xPos, double yPos)
    {
        Width = width;
        Length = length;
        XPos = xPos;
        YPos = yPos;
    }

    public Rectangle() : this(1, 1, 0, 0)
    {
    }


    public int CompareTo(Rectangle? other) => other == null ? 1 : Area.CompareTo(other.Area);

    public void Scale(double factor)
    {
        Width *= factor;
        Length *= factor;
    }

    public void Rotate() => (Width, Length) = (Length, Width);

    public override string ToString()
    {
        return
            $"Width: {Width}\nLength: {Length}\nX Position: {XPos}\nY Position: {YPos}\n\nArea: {Area}\nPerimeter: {Perimeter}";
    }

    public void PrintProperties() =>
        typeof(Rectangle)
            .GetProperties()
            .ToList()
            .ForEach(prop => Console.WriteLine($"{prop.Name}: {prop.GetValue(this)}"));

    public void Draw(char fillChar)
    {
        if (Length <= 0 || Width <= 0) return;

        Console.ForegroundColor = ConsoleColor.White;

        for (int x = 0; x < Width; x++)
        {
            int xPos = (int)XPos + x;
            
            // Clipping-Check
            if (xPos < 0 || xPos >= Console.WindowWidth) continue;
            
            for (int y = 0; y < Length; y++)
            {
                int yPos = (int)YPos + y;
                
                // Clipping-Check
                if (yPos < 0 || yPos >= Console.WindowHeight) continue;
                
                Console.SetCursorPosition(xPos, yPos);
                Console.Write(fillChar);
            }
        }

        Console.ResetColor();
    }
}