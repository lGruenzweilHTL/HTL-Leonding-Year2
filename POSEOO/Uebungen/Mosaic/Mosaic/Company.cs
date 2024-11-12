using System.Globalization;

namespace Mosaic;

/// <summary>
///     Represents a flooring company
/// </summary>
public sealed class Company
{
    private const int DefaultPiecesPerHour = 25;
    private readonly decimal _hourlyWage;
    private readonly decimal _m2Price;
    private readonly int _profitMargin;
    private readonly Worker[] _workers;

    /// <summary>
    ///     Constructs a new <see cref="Company"/> instance based on the supplied configuration.
    /// </summary>
    /// <param name="name">The name of the company</param>
    /// <param name="m2Price">Base price per m2 of floor, independent of pattern type</param>
    /// <param name="hourlyWage">Hourly wage of each worker (paid by customer)</param>
    /// <param name="profitMarginPercent">Profit margin put on top of the production cost of the tiles</param>
    /// <param name="workers">Array of the company's employees</param>
    public Company(string name, decimal m2Price, decimal hourlyWage,
        int profitMarginPercent, Worker[] workers)
    {
        Name = name;
        _m2Price = m2Price;
        _hourlyWage = hourlyWage;
        _profitMargin = profitMarginPercent;
        _workers = workers;
    }

    /// <summary>
    ///     Gets the name of the company
    /// </summary>
    public string Name { get; }

    /// <summary>
    ///     Calculates how much this specific company would charge when tasked with executing the
    ///     supplied pattern. This includes production cost as well as work costs.
    /// </summary>
    /// <param name="pattern">Pattern to create</param>
    /// <returns>Cost estimate for the supplied pattern</returns>
    public decimal GetCostEstimate(TilePattern pattern)
    {
        double piecesPerHour = CalcPiecesPerHour(pattern.Style);
        decimal requiredHours = pattern.Pieces / (decimal)piecesPerHour;
        decimal profitMarginMultiplier = 1 + _profitMargin / 100m; // Convert from percent to multiplier

        decimal workCost = requiredHours * _hourlyWage * _workers.Length;
        decimal basePrice = (decimal)pattern.Area * _m2Price;
        decimal productionCost = pattern.CalcProductionCost() * profitMarginMultiplier;

        decimal priceEstimate = Math.Ceiling(workCost + productionCost + basePrice);
        return priceEstimate;
    }

    /// <summary>
    ///     Calculates how many pieces the workers of this company (together) are able to place per hour.
    ///     This takes into account the complexity of the pattern as well as the working speed of
    ///     each employee.
    /// </summary>
    /// <param name="patternStyle">Defines if the pattern is simple or complex</param>
    /// <returns>Number of tiles this company is able to place per hour</returns>
    private double CalcPiecesPerHour(PatternStyle patternStyle)
    {
        double styleFactor = patternStyle == PatternStyle.Complex ? 1 / 2d : 1d;

        return styleFactor * _workers.Sum(w =>
            // Calculate the pieces per hour (simple style) for current worker
            // Everything else is handled outside
            DefaultPiecesPerHour * w.WorkSpeed switch
            {
                WorkSpeed.Regular => 1,
                WorkSpeed.Fast => 1.2,
                WorkSpeed.Slow => 0.8,
                _ => throw new ArgumentOutOfRangeException()
            }
        );
    }

    /// <summary>
    /// Creates an array of Companies from csv lines
    /// </summary>
    /// <param name="csv">The csv lines in the format Name;M2Price;HourlyWage;ProfitMargin;Workers</param>
    /// <returns></returns>
    public static Company[] Import(string[] csv)
    {
        if (csv.Length == 0) return Array.Empty<Company>();

        int valid = 0;
        Company[] companies = new Company[csv.Length - 1];

        for (var i = 1; i < csv.Length; i++)
        {
            var split = csv[i].Split(';');
            if (split.Length != 5) continue;
            if (string.IsNullOrWhiteSpace(split[0])) continue;
            if (!decimal.TryParse(split[1], NumberStyles.Any, CultureInfo.InvariantCulture, out var m2Price)) continue;
            if (!decimal.TryParse(split[2], NumberStyles.Any, CultureInfo.InvariantCulture, out var hourlyWage))
                continue;
            if (!int.TryParse(split[3], NumberStyles.Float, CultureInfo.InvariantCulture, out var profitMargin)) continue;

            string[] workerParts = split[4].Split(',');
            int workersValid = 0;
            Worker[] workers = new Worker[workerParts.Length];
            for (var j = 0; j < workerParts.Length; j += 2)
            {
                if (string.IsNullOrWhiteSpace(workerParts[j])) continue;
                if (!Enum.TryParse(workerParts[1], out WorkSpeed speed)) continue;

                workers[workersValid++] = new Worker(workerParts[j], speed);
            }

            Array.Resize(ref workers, workersValid);

            companies[valid++] = new Company(split[0], m2Price, hourlyWage, profitMargin, workers);
        }

        Array.Resize(ref companies, valid);
        return companies;
    }
}