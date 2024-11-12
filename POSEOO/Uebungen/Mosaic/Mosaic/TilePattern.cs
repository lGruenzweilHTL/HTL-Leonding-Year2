namespace Mosaic;

/// <summary>
///     Represents a mosaic consisting of several tiles.
/// </summary>
public class TilePattern
{
    private const double MM2_TO_M2 = 1000000;
    private readonly Tile[] _tiles;
    public int Pieces => _tiles.Length;

    /// <summary>
    /// The Area of all Pieces in m^2
    /// </summary>
    public double Area => _tiles.Sum(t => t.Area) / MM2_TO_M2;

    public PatternStyle Style { get; }

    public TilePattern(PatternStyle style, Tile[] tiles)
    {
        _tiles = tiles;
        Style = style;
    }

    public decimal CalcProductionCost()
    {
        return _tiles.Sum(t => t.CalcProductionCost());
    }

    /// <summary>
    /// Creates an array of TilePatterns from csv lines
    /// </summary>
    /// <param name="csv">The csv lines in the format Style;Tiles</param>
    /// <returns></returns>
    public static TilePattern[] Import(string[] csv)
    {
        if (csv.Length == 0) return Array.Empty<TilePattern>();

        int valid = 0;
        TilePattern[] patterns = new TilePattern[csv.Length - 1];

        for (var i = 1; i < csv.Length; i++)
        {
            var split = csv[i].Split(';');
            if (split.Length != 2) continue;
            if (!Enum.TryParse(split[0], out PatternStyle style)) continue;

            string[] tileParts = split[1].Split(',');
            int tilesValid = 0;
            Tile[] tiles = new Tile[tileParts.Length];
            if (tileParts.Length % 3 != 0) continue;
            for (var j = 0; j < tileParts.Length; j += 3)
            {
                if (!Enum.TryParse(tileParts[j], out TileStyle tileStyle)) continue;
                if (!int.TryParse(tileParts[j + 1], out var width)) continue;
                if (!int.TryParse(tileParts[j + 2], out var height)) continue;

                tiles[tilesValid++] = new Tile(tileStyle, width, height);
            }
            
            Array.Resize(ref tiles, tilesValid);

            patterns[valid++] = new TilePattern(style, tiles);
        }
        Array.Resize(ref patterns, valid);

        return patterns;
    }
}