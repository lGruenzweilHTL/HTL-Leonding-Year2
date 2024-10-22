namespace Rectangle;

internal class Program {
    public static void Main() {
        Rectangle[] rects = [
            new Rectangle(5, 6, 2, 3),
            new Rectangle(3, 4, 10, 10),
            new Rectangle(15, 10, 20, 5)
        ];
        RectangleOperations operations = new RectangleOperations(rects);
        
        Console.WriteLine("===== Rectangle Operations =====");
        Console.WriteLine("=== Controls ===");
        Console.WriteLine("Press 'D' to draw rectangles");
        Console.WriteLine("Press 'R' to rotate rectangles");
        Console.WriteLine("Press 'S' to make rectangles smaller");
        Console.WriteLine("Press 'B' to make rectangles bigger");
        Console.WriteLine("Press 'ESC' to exit");
        
        Console.WriteLine("\n\n\n\nPress any key to start...");
        Console.ReadKey(true);
        
        Console.Clear();
        operations.Run();
    }
}