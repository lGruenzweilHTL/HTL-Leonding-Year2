namespace Supermarket;

public sealed class Food : Product
{
    private const char AllergenSeparator = '|';
    private readonly SortedSet<AllergenType> _allergens;
    public Food(string productName, string barcode, int quantity, params AllergenType[] allergens):base(productName, barcode, quantity) => _allergens = new SortedSet<AllergenType>(allergens);
    public AllergenType[] Allergens => _allergens.ToArray();
    protected override string[] CsvColumnNames => ["Barcode", "ProductName", "Quantity", "Allergens"];
    protected override string[] CsvColumnValues => [Barcode, ProductName, Quantity.ToString(), string.Join(AllergenSeparator, Allergens.Select(allergen => allergen.ToString()))];
    public bool AddAllergen(AllergenType allergen) => _allergens.Add(allergen);
    public bool RemoveAllergen(AllergenType allergen) => _allergens.Remove(allergen);
    public bool ContainsAnyAllergen(params AllergenType[] allergens) => allergens.Any(allergen => _allergens.Contains(allergen));
}