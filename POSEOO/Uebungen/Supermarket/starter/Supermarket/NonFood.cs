using System.Globalization;

namespace Supermarket;

public sealed class NonFood(string productName, string barcode, int quantity) : Product(productName, barcode, quantity)
{
    private readonly List<Review> _reviews = new();
    public Review[] Reviews => _reviews.ToArray();
    public double? AverageRating => Reviews.Length == 0 ? null :  Reviews.Average(r => (double) r.Rating);
    protected override string[] CsvColumnNames => ["Barcode", "ProductName", "Quantity", "AverageRating"];
    protected override string[] CsvColumnValues => [Barcode, ProductName, Quantity.ToString(), AverageRating?.ToString("F1", CultureInfo.InvariantCulture) ?? ""];
    public void AddReview(Review review) => _reviews.Add(review);
}