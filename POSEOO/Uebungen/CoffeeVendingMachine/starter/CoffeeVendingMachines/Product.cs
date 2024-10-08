namespace CoffeeVendingMachines;

public class Product
{
    public const int FALLBACK_PRICE = 50;

    private readonly int _price;
    private readonly int _stock;
    public readonly bool InStock;
    public readonly string Name;
    public int NumberSold { get; private set; }
    public int Price { get; private init; }

    public Product(string name, int price, int stock)
    {
        Name = name;
        _stock = stock;
        _price = price;
        
        Price = _price % 5 == 0 ? price : FALLBACK_PRICE;
    }

    public bool AddSale()
    {
        NumberSold++;
        return true;
    }

    public override string ToString()
    {
        return $"Name: {Name} € {Price} [{_stock} in stock | {NumberSold} sold]";
    }
}