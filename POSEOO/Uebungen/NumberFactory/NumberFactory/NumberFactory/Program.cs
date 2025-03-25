using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NumberFactory
{
    internal class Program
    {
        public static void Main()
        {
            // Creates a number generator for even numbers in the range of 3 to 9
            INumbers evenNumbers = Factory.Create(NumberType.Even, 3, 9);

            // The number generator is iterable (foreach) and returns the result: 4 6 8
            Console.WriteLine("Even numbers between 3 and 9:");
            foreach (long n in evenNumbers)
            {
                Console.Write($"{n} ");
            }
            Console.WriteLine();

            // Creates a number generator for prime numbers in the range of 1 to 10
            INumbers primeNumbers = Factory.Create(NumberType.Prime, 1, 10);

            // The number generator is iterable (foreach) and returns the result: 2 3 5 7
            Console.WriteLine("\nPrime numbers between 1 and 10:");
            foreach (long n in primeNumbers)
            {
                Console.Write($"{n} ");
            }
            Console.WriteLine();
        }
    }
}