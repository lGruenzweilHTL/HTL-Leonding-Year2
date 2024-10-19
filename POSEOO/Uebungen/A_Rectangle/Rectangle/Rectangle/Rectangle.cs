namespace Rectangle;

public class Rectangle : IComparable<Rectangle> {
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

    public ConsoleColor Color = default;

    public double Perimeter => 2 * (Width + Length);
    public double Area => Width * Length;

    public Rectangle(double width, double length, double xPos, double yPos) {
        Width = width;
        Length = length;
        XPos = xPos;
        YPos = yPos;
    }

    public Rectangle() : this(1, 1, 0, 0) { }


    public int CompareTo(Rectangle? other) => other == null ? 1 : Area.CompareTo(other.Area);

    public void Scale(double factor) {
        Width *= factor;
        Length *= factor;
    }

    public void Rotate() => (Width, Length) = (Length, Width);

    public override string ToString() {
        return $"Width: {Width}\nLength: {Length}\nX Position: {XPos}\nY Position: {YPos}\n\nArea: {Area}\nPerimeter: {Perimeter}";
    }

    public void PrintProperties() {
        Console.WriteLine(this.ToString());
    }

    public void Draw(char fillChar) {
        Console.ForegroundColor = ConsoleColor.White;

        for (int i = (int)YPos; i < YPos + Length; i++) {
            Console.SetCursorPosition((int)XPos, i);
            Console.Write(fillChar);
        }

        for (int i = (int)YPos; i < YPos + Length; i++) {
            Console.SetCursorPosition((int)(XPos + Width), i);
            Console.Write(fillChar);
        }

        for (int i = (int)XPos; i < XPos + Width; i++) {
            Console.SetCursorPosition(i, (int)YPos);
            Console.Write(fillChar);
        }

        for (int i = (int)XPos; i < XPos + Width; i++) {
            Console.SetCursorPosition(i, (int)(YPos + Length));
            Console.Write(fillChar);
        }

        Console.ResetColor();
    }
}