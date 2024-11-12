namespace Mosaic;

/// <summary>
///     Represents a single tile of a mosaic floor.
/// </summary>
public sealed class Tile
{
    private readonly TileStyle _style;
    private readonly int _width;
    private readonly int _height;
    
    /// <summary>
    /// The Area in mm^2
    /// </summary>
    public int Area => _width * _height;

    public Tile(TileStyle style, int width, int height)
    {
        _style = style;
        _width = width;
        _height = height;
    }

    public decimal CalcProductionCost()
    {
        const decimal PRICE_PER_SQUARE_CENTIMETER = 0.016m;
        decimal sizeFactor = 1M;
        if (Area < 100) sizeFactor = 1.5M;
        else if (Area < 400) sizeFactor = 1.2M;
        else if (Area > 8100) sizeFactor = 1.8M;
        else if (Area > 2500) sizeFactor = 1.6M;
        else if (Area > 400) sizeFactor = 1M;

        decimal styleFactor = _style switch
        {
            TileStyle.Raw => 0.8M,
            TileStyle.Polished or TileStyle.PlainColor => 1,
            TileStyle.FancyColor => 1.1M,
            TileStyle.SimplePattern => 1.25M,
            TileStyle.Ornate => 2.3M,
            _ => throw new ArgumentOutOfRangeException()
        };

        decimal areaSquareCentimeters = Area / 100M;
        
        return areaSquareCentimeters * PRICE_PER_SQUARE_CENTIMETER * sizeFactor * styleFactor;
    }
    
    /// <summary>
    /// Creates an array of tiles from csv lines
    /// </summary>
    /// <param name="csv">The csv lines in the format Style;Width;Height</param>
    /// <returns></returns>
    public static Tile[] Import(string[] csv)
    {
        if (csv.Length == 0) return Array.Empty<Tile>();
        
        int valid = 0;
        Tile[] tiles = new Tile[csv.Length - 1];

        for (var i = 1; i < csv.Length; i++)
        {
            var split = csv[i].Split(';');
            
            if (split.Length != 3) continue;
            
            if (!Enum.TryParse(split[0], out TileStyle style)) continue;
            if (!int.TryParse(split[1], out int width)) continue;
            if (!int.TryParse(split[2], out int height)) continue;
            
            tiles[valid++] = new Tile(style, width, height);
        }
        
        Array.Resize(ref tiles, valid);
        
        return tiles;
    }
}