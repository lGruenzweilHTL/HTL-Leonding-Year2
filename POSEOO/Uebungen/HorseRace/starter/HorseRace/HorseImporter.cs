namespace HorseRace;

public static class HorseImporter
{
    public static bool TryReadHorses(string filePath, out Horse[]? horses)
    {
        horses = null;
        if (!File.Exists(filePath)) return false;

        string[] lines = File.ReadAllLines(filePath);
        if (lines.Length < 2) return false;
        horses = new Horse[lines.Length-1];

        int valid = 0;
        for (int i = 1; i < lines.Length; i++)
        {
            if (Horse.TryParse(lines[i], i, out var horse))
            {
                horses[valid++] = horse!;
            }
        }

        if (valid == 0)
        {
            horses = Array.Empty<Horse>();
            return false;
        }
        Array.Resize(ref horses, valid);
        return true;
    } 
}