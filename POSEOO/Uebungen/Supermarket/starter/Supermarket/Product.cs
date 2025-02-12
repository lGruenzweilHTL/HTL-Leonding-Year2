namespace Supermarket;

public abstract class Product(string productName, string barcode, int quantity)
{
    public const char Separator = ';';
    public const string Invalid = "Invalid!";
    public string ProductName { get; } = string.IsNullOrWhiteSpace(productName) ? Invalid : productName;
    public string Barcode { get; } = IsBarcodeValid(barcode) ? barcode : Invalid;
    public int Quantity { get; } = quantity < 0 ? 0 : quantity;
    protected virtual string[] CsvColumnNames => ["Barcode", "ProductName", "Quantity"];
    protected virtual string[] CsvColumnValues => [Barcode, ProductName, Quantity.ToString()];
    public string GetCsvHeader() => string.Join(Separator, CsvColumnNames);
    public string ToCsv() => ToCsvLine(CsvColumnValues, Separator);
    protected static string ToCsvLine(string[] values, char separator) => string.Join(separator, values);
    public static bool IsBarcodeValid(string? barcode) => barcode?.Length == 8 && barcode.All(char.IsDigit) && barcode[7] == (10 - barcode[..7].Select((c, i) => (c - '0') * (i % 2 == 0 ? 3 : 1)).Sum() % 10) % 10 + '0';
    protected static T[] AppendToArray<T>(T[] existingArray, params T[] newValues) => existingArray.Concat(newValues).ToArray();
}