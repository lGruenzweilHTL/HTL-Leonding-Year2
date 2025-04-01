using SoundDataParser;

namespace Tools;

public class CsvReader
{
    public static SoundCategory[] ReadCsv(string path)
    {
        //parse the CSV.
        var soundCategories = new List<SoundCategory>();
        var csvLines = File.ReadLines(path);

        bool skipFirst = false;
        foreach (var csvLine in csvLines)
        {
            if (string.IsNullOrEmpty(csvLine)) continue;

            var data = csvLine.Split(';', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
            if (data.Length != 7)
            {
                Console.WriteLine("Ignoring invalid line: " + csvLine);
                continue;
            }

            if (!skipFirst)
            {
                if (data[0] == "Name" && data[6] == "Loudness")
                {
                    skipFirst = true;
                    continue;
                }
            }

            //we assume that soundData.csv is well-formed and only contains unique sound categories.
            var soundCategory = SoundCategory.CreateFromCsv(data);
            soundCategories.Add(soundCategory);
        }

        return soundCategories.ToArray();
    }
}