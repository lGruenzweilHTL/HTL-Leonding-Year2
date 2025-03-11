using Avalonia;
using Avalonia.Media;
using Shapes.Shapes;

namespace ShapesTests;

public class ShapeTests
{
    /// <summary>
    /// Shape for use in testing
    /// </summary>
    private class TestShape : Shape
    {
        public TestShape(Point center) : base(center)
        {
        }
        
        public override bool DrawSelf() => true;

        public override bool ContainsPoint(Point p) => false;
    }
    [Fact]
    public void Shape_ConstructionSimple_ShouldInitializeProperties()
    {
        // Arrange
        var center = new Point(0, 0);

        // Act
        var shape = new TestShape(center);

        // Assert
        Assert.Equal(center, shape.Center);
        Assert.NotNull(shape.Color);
        Assert.True(shape.Id > 0);
    }

    [Fact]
    public void Shape_Id_ShouldIncrement()
    {
        var center = new Point(0, 0);
        int id = Shape.CurrentId;

        // Act
        for (int i = 0; i < 100; i++)
        {
            var s = new TestShape(center);
            id++;
            
            Assert.Equal(id, s.Id);
        }
    }
}
