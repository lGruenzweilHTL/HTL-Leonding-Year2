namespace HorseRace;

public static class HorseImporter
{
    public static bool TryReadHorses(string filePath, out Horse[]? horses)
    {
        horses = null;
        if (!File.Exists(filePath)) return false;

        string[] lines = File.ReadAllLines(filePath);
        horses = new Horse[lines.Length-1];
        
        for (int i = 1; i < lines.Length; i++)
        {
            if (!Horse.TryParse(lines[i], i, out horses[i-1]!))
            {
                horses = null;
                return false;
            }
        }
        
        return true;
    } 
}