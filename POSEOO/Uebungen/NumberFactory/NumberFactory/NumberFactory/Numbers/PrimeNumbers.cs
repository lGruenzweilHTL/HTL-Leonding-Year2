using System.Collections.Generic;

namespace NumberFactory;

public class PrimeNumbers : AbstractNumbers
{
    public PrimeNumbers(long lowerBound, long upperBound) : base(lowerBound, upperBound)
    {
    }

    protected override List<long> GenerateNumbers(long lowerBound, long upperBound)
    {
        var list = new List<long>();
        for (var i = lowerBound; i <= upperBound; i++)
        {
            if (IsPrime(i))
            {
                list.Add(i);
            }
        }
        return list;
    }

    private bool IsPrime(long l)
    {
        // Iterative prime checker, because it doesn't need to be fast for this exercise
        if (l < 2)
        {
            return false;
        }
        for (var i = 2; i <= l / 2; i++)
        {
            if (l % i == 0)
            {
                return false;
            }
        }
        return true;
    }
}