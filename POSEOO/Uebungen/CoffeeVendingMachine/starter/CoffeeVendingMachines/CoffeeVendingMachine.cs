namespace CoffeeVendingMachines;

public class CoffeeVendingMachine
{
    private static readonly CoinType[] _withdrawalOrder =
    {
        CoinType.Euro02, CoinType.Euro01, CoinType.Cent50, CoinType.Cent20, CoinType.Cent10, CoinType.Cent05
    };
    private readonly CoinDepot[] _currentChangeCoins = Array.Empty<CoinDepot>();
    private readonly CoinDepot[] _currentInputCoins = Array.Empty<CoinDepot>();
    private readonly Product[] _products = Array.Empty<Product>();

    public readonly string Location;
    public readonly int TotalChangeAmountInMachine = 0;
    public readonly int TotalMoneyCurrentlyInput = 0;
    public readonly Product[] AvailableProducts = Array.Empty<Product>();

    public CoffeeVendingMachine(CoinDepot[] depots, Product[] products, string location)
    {
        _currentChangeCoins = depots;
        _products = products;
        Location = location;
    }

    public CoffeeVendingMachine(string location)
    {
        Location = location;
    }

    public CoinDepot[] Cancel()
    {
        throw new NotImplementedException();
    }

    public bool InsertCoin(CoinType coin)
    {
        throw new NotImplementedException();
    }

    public bool SelectProduct(string product, out CoinDepot[]? change, out int? idk)
    {
        throw new NotImplementedException();
    }

    public override string ToString()
    {
        throw new NotImplementedException();
    }

    private void AddInputToChange()
    {
        throw new NotImplementedException();
    }

    private CoinDepot[] CreateCoinDepots(int idk)
    {
        throw new NotImplementedException();
    }

    private CoinDepot? GetDepotByType(CoinDepot[] available, CoinType type)
    {
        throw new NotImplementedException();
    }

    private int? PrepareChangeCoins(int change, out CoinDepot[] changeCoins)
    {
        throw new NotImplementedException();
    }

    private int SumDepotValue(CoinDepot[] allDepots)
    {
        throw new NotImplementedException();
    }

    private bool TryFindProduct(string productName, out Product? product)
    {
        throw new NotImplementedException();
    }
}