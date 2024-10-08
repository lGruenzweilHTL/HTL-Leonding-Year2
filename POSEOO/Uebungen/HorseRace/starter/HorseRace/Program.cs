using System.Text;
using HorseRace;

Console.OutputEncoding = Encoding.UTF8;

Console.WriteLine($"*** Horse Race ***{Environment.NewLine}");

if (!HorseImporter.TryReadHorses("Data/horses.csv", out var horses)
    || horses == null)
{
    Console.WriteLine("Failed to parse horses");
    return;
}

if (horses.Length == 0)
{
    Console.WriteLine("No horses available");
    return;
}

var race = new HorseRace.HorseRace(horses);

race.PrintStartList();

Console.ReadKey();

race.PerformRace();

Console.WriteLine();

race.PrintResults();