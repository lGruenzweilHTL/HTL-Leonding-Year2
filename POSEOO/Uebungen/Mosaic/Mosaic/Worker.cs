namespace Mosaic;

public class Worker
{
    public Worker(string name, WorkSpeed workSpeed)
    {
        Name = name;
        WorkSpeed = workSpeed;
    }

    public string Name { get; }
    public WorkSpeed WorkSpeed { get; }

    /// <summary>
    /// Creates an array of workers from csv lines
    /// </summary>
    /// <param name="csv">The csv lines in the format Name;WorkSpeed</param>
    /// <returns></returns>
    public static Worker[] Import(string[] csv)
    {
        if (csv.Length == 0) return Array.Empty<Worker>();
        
        int valid = 0;
        Worker[] workers = new Worker[csv.Length - 1];

        for (var i = 1; i < csv.Length; i++)
        {
            var split = csv[i].Split(';');
            if (split.Length != 2) continue;
            if (string.IsNullOrWhiteSpace(split[0])) continue;
            if (!Enum.TryParse(split[1], out WorkSpeed speed)) continue;
            
            workers[valid++] = new Worker(split[0], speed);
        }
        
        Array.Resize(ref workers, valid);
        
        return workers;
    }
}