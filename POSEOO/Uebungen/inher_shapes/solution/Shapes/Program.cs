using System.Text;
using Shapes;
using Shapes.Shapes;
using SimpleXPlatDrawing;

Console.OutputEncoding = Encoding.UTF8;

const int Size = 600;

var shapes = new List<Shape>();
var generator = new ShapeGenerator(Size, Size);

await SimpleDrawing.Init(Size, Size, HandleClick, "Shapes");

// initial rendering
Redraw();

Console.WriteLine("Press any key to exit...");
Console.ReadKey();


void HandleClick(ClickEvent @event)
{
    var pointClicked = @event.ClickedPoint;
    
    if (!RemoveExisting())
    {
        var shape = generator.CreateNewShape(pointClicked);
        shapes.Add(shape);
        Console.WriteLine($"Added shape with id {shape.Id}");
    }

    Redraw();
    
    return;
    
    bool RemoveExisting()
    {
        for (var i = 1; i <= shapes.Count; i++)
        {
            var shape = shapes[^i];
            if (!shape.ContainsPoint(pointClicked))
            {
                continue;
            }
            
            shapes.Remove(shape);
            Console.WriteLine($"Removed shape with id {shape.Id}");
            return true;
        }
        
        return false;
    }
}

void Redraw()
{
    SimpleDrawing.Clear();
    foreach (var shape in shapes)
    {
        if (!shape.DrawSelf())
        {
            Console.WriteLine($"Couldn't draw shape with id {shape.Id} 😭");
        }
    }
    SimpleDrawing.Render();
}

