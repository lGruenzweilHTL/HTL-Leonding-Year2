using System.Collections.Generic;

namespace NumberFactory;

public class EvenNumbers : AbstractNumbers
{
    public EvenNumbers(long lowerBound, long upperBound) : base(lowerBound, upperBound)
    {
    }
    
    protected override List<long> GenerateNumbers(long lowerBound, long upperBound)
    {
        var list = new List<long>();
        for (var i = lowerBound; i <= upperBound; i++)
        {
            if (i % 2 == 0)
            {
                list.Add(i);
            }
        }
        return list;
    }
}