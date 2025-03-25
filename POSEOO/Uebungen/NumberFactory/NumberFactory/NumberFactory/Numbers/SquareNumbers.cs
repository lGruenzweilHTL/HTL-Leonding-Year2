using System;
using System.Collections.Generic;
using System.Linq;

namespace NumberFactory;

public class SquareNumbers : AbstractNumbers
{
    public SquareNumbers(long lowerBound, long upperBound) : base(lowerBound, upperBound)
    {
    }

    protected override List<long> GenerateNumbers(long lowerBound, long upperBound) =>
        Enumerable.Range(0, (int)upperBound + 1)
            .Select(i => (long)i * i)
            .Where(i => i >= lowerBound && i <= upperBound)
            .ToList();

}