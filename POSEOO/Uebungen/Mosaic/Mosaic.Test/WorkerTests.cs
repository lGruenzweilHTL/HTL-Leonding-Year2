using Mosaic;

namespace Mosaic.Test;

public class WorkerTests
{

    [Fact]
    public void Import()
    {
        string[] csvData = new string[]
        {
            "Name;WorkSpeed",        // Header row
            "John;Fast",             // Valid row
            "Jane;Slow",             // Valid row
            ";Fast",                 // Invalid row: Empty name
            "Tom;InvalidSpeed",      // Invalid row: Invalid enum value
            "Mary;Slow",             // Valid row
            ";;Slow",                // Invalid row: Empty name, empty speed
            "Alex;Fast",             // Valid row
            ";Fast",                 // Invalid row: Empty name
            "Charlie;Slow",          // Valid row
            " ;Fast"                 // Invalid row: Whitespace name
        };
        
        Worker[] expectedWorkers = new Worker[]
        {
            new Worker("John", WorkSpeed.Fast),   // Valid: "John;Fast"
            new Worker("Jane", WorkSpeed.Slow),   // Valid: "Jane;Slow"
            // Invalid: ";Fast" (empty name)
            // Invalid: "Tom;InvalidSpeed" (invalid enum)
            new Worker("Mary", WorkSpeed.Slow),   // Valid: "Mary;Slow"
            // Invalid: ";;Slow" (empty name and speed)
            new Worker("Alex", WorkSpeed.Fast),   // Valid: "Alex;Fast"
            // Invalid: ";Fast" (empty name)
            new Worker("Charlie", WorkSpeed.Slow) // Valid: "Charlie;Slow"
            // Invalid: " ;Fast" (whitespace name)
        };
        
        Worker.Import(csvData).Should().BeEquivalentTo(expectedWorkers);
    }
}