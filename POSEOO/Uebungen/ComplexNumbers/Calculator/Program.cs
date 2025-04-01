using System;
using Core;

namespace Calculator
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Complex Number Calculator");
            Console.Write("Enter the real part of the first complex number: ");
            double real1 = double.Parse(Console.ReadLine());
            Console.Write("Enter the imaginary part of the first complex number: ");
            double imag1 = double.Parse(Console.ReadLine());

            ComplexNumber num1 = new ComplexNumber(real1, imag1);

            Console.Write("Enter the real part of the second complex number: ");
            double real2 = double.Parse(Console.ReadLine());
            Console.Write("Enter the imaginary part of the second complex number: ");
            double imag2 = double.Parse(Console.ReadLine());

            ComplexNumber num2 = new ComplexNumber(real2, imag2);

            Console.WriteLine($"First complex number: {num1.Real} + {num1.Imag}i");
            Console.WriteLine($"Second complex number: {num2.Real} + {num2.Imag}i");

            ComplexNumber sum = num1 + num2;
            ComplexNumber difference = num1 - num2;
            ComplexNumber product = num1 * num2;
            ComplexNumber quotient = num1 / num2;

            Console.WriteLine($"Sum: {sum.Real} + {sum.Imag}i");
            Console.WriteLine($"Difference: {difference.Real} + {difference.Imag}i");
            Console.WriteLine($"Product: {product.Real} + {product.Imag}i");
            Console.WriteLine($"Quotient: {quotient.Real} + {quotient.Imag}i");

            Console.WriteLine("Press any key to exit.");
            Console.ReadKey();
        }
    }
}