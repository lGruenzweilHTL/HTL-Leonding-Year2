using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NumberFactory
{
    /// <summary>
    /// This enumeration specifies the type of the number collection.
    /// </summary>
    public enum NumberType : short
    {
        Even = 1,
        Odd = 2,
        Square = 4,
        Prime = 8,
    }

    /// <summary>
    /// This factory method creates the number generator according to the type definition.
    /// </summary>
    public static class Factory
    {
        public static INumbers Create(NumberType type, long lowerBound, long upperBound) => 
            type switch
            {
                NumberType.Even => new EvenNumbers(lowerBound, upperBound),
                NumberType.Odd => new OddNumbers(lowerBound, upperBound),
                NumberType.Square => new SquareNumbers(lowerBound, upperBound),
                NumberType.Prime => new PrimeNumbers(lowerBound, upperBound),
                _ => throw new ArgumentOutOfRangeException(nameof(type), type, "Invalid number type."),
            };
        
    }
}