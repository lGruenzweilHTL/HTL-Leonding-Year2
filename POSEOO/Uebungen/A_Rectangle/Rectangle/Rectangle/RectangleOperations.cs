namespace Rectangle;

public class RectangleOperations(params Rectangle[] rectangles)
{
    public const char FILL_CHAR = '*';
    public void Run() {
        bool running = true;
        while (running) {
            switch (Console.ReadKey(true).Key) {
                case ConsoleKey.D:
                    DrawRectangles(Range.All);
                    break;
                
                case ConsoleKey.R:
                    RotateRectangles(Range.All);
                    break;
                
                case ConsoleKey.S:
                    ScaleRectangles(Range.All, 0.8);
                    break;
                
                case ConsoleKey.B:
                    ScaleRectangles(Range.All, 1.3);
                    break;
                
                case ConsoleKey.Escape:
                    running = false;
                    break;
                default:
                    // Ignored
                    break;
            }
        }
    }

    private void DrawRectangles(Range range) {
        Console.Clear();
        foreach (Rectangle rectangle in rectangles[range]) {
            rectangle.Draw(FILL_CHAR);
        }
    }
    private void RotateRectangles(Range range) {
        foreach (Rectangle rectangle in rectangles[range]) {
            rectangle.Rotate();
            rectangle.Draw(FILL_CHAR);
        }
    }
    private void ScaleRectangles(Range range, double factor) {
        foreach (Rectangle rectangle in rectangles[range]) {
            rectangle.Scale(factor);
            rectangle.Draw(FILL_CHAR);
        }
    }
}