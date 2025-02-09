namespace Supermarket.Test;

public sealed class ProductTests
{

    [Theory]
    [InlineData("10000007")]
    [InlineData("90311017")]
    [InlineData("12345670")]
    [InlineData("73513537")]
    public void IsBarcodeValid_Valid(string barcode)
    {
        Product.IsBarcodeValid(barcode)
               .Should().BeTrue();
    }
    
    [Theory]
    [InlineData("10000008")]
    [InlineData("90311016")]
    [InlineData("12345677")]
    [InlineData("123xy670")]
    [InlineData("1234567z")]
    public void IsBarcodeValid_Invalid(string barcode)
    {
        Product.IsBarcodeValid(barcode)
               .Should().BeFalse();
    }
    
    [Theory]
    [InlineData("", "no content")]
    [InlineData(null, "no content")]
    [InlineData("1", "too short")]
    [InlineData("903110178", "too long")]
    public void IsBarcodeValid_InvalidLength(string? barcode, string reason)
    {
        Product.IsBarcodeValid(barcode)
               .Should().BeFalse(reason);
    }
}
