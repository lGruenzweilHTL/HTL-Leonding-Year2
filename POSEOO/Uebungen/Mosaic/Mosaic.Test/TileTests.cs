namespace Mosaic.Test;

public sealed class TileTests : TestBase
{
    [Theory]
    [InlineData(12, 12, 144)]
    [InlineData(20, 16, 320)]
    [InlineData(30, 30, 900)]
    [InlineData(8, 8, 64)]
    [InlineData(10, 20, 200)]
    public void Area(int width, int height, int expectedArea)
    {
        var tile = new Tile(TileStyle.Raw, width, height);

        tile.Area
            .Should().Be(expectedArea);
    }

    [Theory]
    [MemberData(nameof(CalcProductionCostData))]
    public void CalcProductionCost(TileStyle style, int width, int height, decimal expectedPrice, string reason)
    {
        var tile = new Tile(style, width, height);

        tile.CalcProductionCost()
            .Should().Be(expectedPrice, reason);
    }

    [Theory]
    [InlineData(TileStyle.PlainColor, 12, 50)]
    [InlineData(TileStyle.Raw, 43, 8)]
    [InlineData(TileStyle.SimplePattern, 20, 20)]
    [InlineData(TileStyle.Ornate, 500, 500)]
    public void Construction(TileStyle style, int width, int height)
    {
        var instance = new Tile(style, width, height);

        CheckFields(instance, style, width, height)
            .Should().BeTrue("ctor has to set value of all three fields correctly");
    }

    public static IEnumerable<object[]> CalcProductionCostData()
    {
        const int BaseWidth = 20;
        const int BaseHeight = BaseWidth;
        const TileStyle BaseStyle = TileStyle.Polished;
        return new[]
        {
            new object[] { BaseStyle, BaseWidth, BaseHeight, 0.064M, "no modifiers applied to base cm2 price" },
            new object[] { TileStyle.Raw, BaseWidth, BaseHeight, 0.0512M, "raw tiles are cheaper" },
            new object[] { TileStyle.PlainColor, BaseWidth, BaseHeight, 0.064M, "no modifiers applied" },
            new object[]
            {
                TileStyle.FancyColor, BaseWidth, BaseHeight, 0.0704M, "fancy colored tiles are slightly more expensive"
            },
            new object[]
            {
                TileStyle.SimplePattern, BaseWidth, BaseHeight, 0.08M, "tiles with a pattern are even more expensive"
            },
            new object[]
            {
                TileStyle.Ornate, BaseWidth, BaseHeight, 0.1472M,
                "hand colored tiles with an ornate pattern are very expensive"
            },
            new object[] { BaseStyle, 8, 8, 0.01536M, "very small tiles are more expensive" },
            new object[] { BaseStyle, 15, 15, 0.0432M, "small tiles are a little more expensive" },
            new object[] { BaseStyle, 30, 30, 0.144M, "regular sized tile" },
            new object[] { BaseStyle, 45, 20, 0.144M, "price is based on area not width/height relation" },
            new object[] { BaseStyle, 20, 45, 0.144M, "price is based on area not width/height relation" },
            new object[] { BaseStyle, 50, 55, 0.704M, "bigger tiles are a little more expensive" },
            new object[] { BaseStyle, 50, 50, 0.4M, "this size is still normally priced" },
            new object[] { BaseStyle, 90, 95, 2.4624M, "big tiles are more expensive" },
            new object[]
                { TileStyle.SimplePattern, 200, 20, 1.28M, "a combination of size and style modifiers is applied" }
        };
    }

    [Fact]
    public void Import()
    {
        string[] csvData = new string[]
        {
            "Style;Width;Height",             // Header row
            "Raw;100;200",                   // Valid row (Raw)
            "Polished;150;300",              // Valid row (Polished)
            "PlainColor;200;100",            // Valid row (PlainColor)
            "FancyColor;100;250",            // Valid row (FancyColor)
            "SimplePattern;120;220",         // Valid row (SimplePattern)
            "Ornate;180;350",                // Valid row (Ornate)
            "InvalidStyle;150;200",          // Invalid: Invalid enum value for style
            "Raw;100;NaN",                   // Invalid: NaN as height
            "FancyColor;NaN;200",            // Invalid: NaN as width
            ";100;200",                      // Invalid: Missing style (empty style)
            "Raw;100;200;ExtraColumn",       // Invalid: Extra column
            "Raw;NotANumber;200"             // Invalid: Invalid width (non-integer)
        };
        Tile[] expectedTiles = new Tile[]
        {
            new Tile(TileStyle.Raw, 100, 200),          // "Raw;100;200"
            new Tile(TileStyle.Polished, 150, 300),     // "Polished;150;300"
            new Tile(TileStyle.PlainColor, 200, 100),   // "PlainColor;200;100"
            new Tile(TileStyle.FancyColor, 100, 250),   // "FancyColor;100;250"
            new Tile(TileStyle.SimplePattern, 120, 220),// "SimplePattern;120;220"
            new Tile(TileStyle.Ornate, 180, 350)        // "Ornate;180;350"
            // Invalid rows:
            // "InvalidStyle;150;200" (Invalid enum "InvalidStyle")
            // "Raw;100;NaN" (NaN is not a valid integer)
            // "FancyColor;NaN;200" (NaN is not a valid integer for width)
            // ";100;200" (Missing style)
            // "Raw;100;200;ExtraColumn" (Extra column, invalid format)
            // "Raw;NotANumber;200" (Invalid width "NotANumber")
        };
        
        Tile.Import(csvData).Should().BeEquivalentTo(expectedTiles);
    }

    #region test helper - ignore

    private static bool CheckFields(Tile tile, TileStyle style, int width, int height)
    {
        return CheckField(tile, style, Prefix(nameof(style)))
               && CheckField(tile, width, Prefix(nameof(width)))
               && CheckField(tile, height, Prefix(nameof(height)));
    }

    #endregion
}