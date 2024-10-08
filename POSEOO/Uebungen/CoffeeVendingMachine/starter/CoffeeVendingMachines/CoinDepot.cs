namespace CoffeeVendingMachines;

public class CoinDepot
{
    public readonly CoinType Coin;
    public int Count { get; private set; }

    public CoinDepot(CoinType coin, int count = 0)
    {
        Coin = coin;
        Count = count;
    }

    public CoinDepot(CoinDepot depot)
    {
        Coin = depot.Coin;
        Count = depot.Count;
    }

    public void Add()
    {
        Count++;
    }

    public void Clear()
    {
        Count = 0;
    }

    public override string ToString()
    {
        return $"{Coin} x{Count}";
    }

    public bool Withdraw()
    {
        return Count >= 1;
    }
}