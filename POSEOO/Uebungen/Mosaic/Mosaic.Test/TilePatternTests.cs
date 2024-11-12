namespace Mosaic.Test;

public sealed class TilePatternTests : TestBase
{
    [Fact]
    public void Area()
    {
        Tile[] tiles = CreateSampleTiles();
        var pattern = new TilePattern(PatternStyle.Simple, tiles);

        pattern.Area
            .Should().Be(0.04095, "the pattern reports its area in m2");
    }

    [Fact]
    public void CalcProductionCost()
    {
        Tile[] tiles = CreateSampleTiles();
        var pattern1 = new TilePattern(PatternStyle.Simple, tiles);
        var pattern2 = new TilePattern(PatternStyle.Complex, tiles);

        pattern1.CalcProductionCost()
            .Should().Be(9.4616M, "the production cost of a pattern is the sum of the production cost of its tiles");
        pattern2.CalcProductionCost()
            .Should().Be(pattern1.CalcProductionCost(), "pattern style does not change the production cost");
    }

    [Fact]
    public void Construction()
    {
        Tile[] tiles = CreateSampleTiles();

        var instance = new TilePattern(PatternStyle.Complex, tiles);

        instance.Style
            .Should().Be(PatternStyle.Complex, "ctor has to set Style property correctly");
        CheckField(instance, tiles, $"_{nameof(tiles)}")
            .Should().BeTrue("ctor has to set tiles field correctly");
    }

    [Fact]
    public void Pieces()
    {
        Tile[] tiles = CreateSampleTiles();
        var pattern = new TilePattern(PatternStyle.Simple, tiles);

        pattern.Pieces
            .Should().Be(tiles.Length, "a pattern has as many pieces as it has individual tiles");
    }

    [Fact]
    public void Import()
    {
        // Input
        string[] csv =
        {
            "PatternStyle;Tiles",
            "Simple;Raw,20,30,Polished,25,35", // Valid row: Simple pattern with two valid tiles
            "Complex;PlainColor,20,40,InvalidStyle,30,30", // Invalid row: Invalid TileStyle ("InvalidStyle")
            "Complex;SimplePattern,30,20", // Invalid row: Incomplete tile information
            "Simple;Raw,25,25,Polished,20" // Invalid row: Missing height for the second tile
        };

        // Expected Outcome
        TilePattern[] expected =
        {
            new TilePattern(PatternStyle.Simple, new Tile[]
            {
                new Tile(TileStyle.Raw, 20, 30),
                new Tile(TileStyle.Polished, 25, 35)
            }),
            new TilePattern(PatternStyle.Complex, new Tile[]
            {
                new Tile(TileStyle.PlainColor, 20, 40)
            }),
            new TilePattern(PatternStyle.Complex, new Tile[]
            {
                new Tile(TileStyle.SimplePattern, 30, 20)
            })
        };
        
        TilePattern.Import(csv).Should().BeEquivalentTo(expected);
    }
}