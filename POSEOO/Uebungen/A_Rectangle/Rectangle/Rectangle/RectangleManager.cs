namespace Rectangle;

public class RectangleManager {
    public readonly Rectangle[] Rectangles;
    
    public RectangleManager(params Rectangle[] rectangles) {
        Rectangles = rectangles;
    }

    public void Run() {
        bool running = true;
        while (running) {
            switch (Console.ReadKey(true).Key) {
                case ConsoleKey.D:
                    DrawRectangles();
                    break;
                
                case ConsoleKey.R:
                    RotateRectangles(Range.All);
                    break;
                
                case ConsoleKey.S:
                    ScaleRectangles(Range.All, 1.1);
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

    private void DrawRectangles() {
        Console.Clear();
        foreach (Rectangle rectangle in Rectangles) {
            rectangle.Draw('*');
        }
    }
    private void RotateRectangles(Range range) {
        foreach (Rectangle rectangle in Rectangles[range]) {
            rectangle.Rotate();
        }
    }
    private void ScaleRectangles(Range range, double factor) {
        foreach (Rectangle rectangle in Rectangles[range]) {
            rectangle.Scale(factor);
        }
    }
}