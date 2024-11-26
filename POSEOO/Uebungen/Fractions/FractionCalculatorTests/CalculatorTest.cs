using System;
using Xunit;
using FractionCalculator;

namespace FractionCalculatorTest
{
    public class CalculatorTest
    {
        /// <summary>
        /// A test for Add
        ///</summary>
        [Fact]
        public void T10_AddTest()
        {
            Fraction result;
            Fraction left = new Fraction();
            Fraction right = new Fraction();
            Calculator calculator = new Calculator();

            left.Numerator = 3;
            left.Denominator = 4;
            right.Numerator = 5;
            right.Denominator = 7;
            calculator.LeftOperand = left;
            calculator.RightOperand = right;
            result = calculator.Add();
            Assert.Equal(41, result.Numerator);
            Assert.Equal(28, result.Denominator);

            // Result is reducible
            left = new Fraction();
            right = new Fraction();
            left.Numerator = 3;
            left.Denominator = 4;
            right.Numerator = 5;
            right.Denominator = 6;
            calculator.LeftOperand = left;
            calculator.RightOperand = right;
            result = calculator.Add();
            Assert.Equal(19, result.Numerator);
            Assert.Equal(12, result.Denominator);
        }

        /// <summary>
        /// A test for Sub
        ///</summary>
        [Fact]
        public void T11_SubTest()
        {
            Fraction result;
            Fraction left = new Fraction();
            Fraction right = new Fraction();
            Calculator calculator = new Calculator();

            left.Numerator = 3;
            left.Denominator = 4;
            right.Numerator = 5;
            right.Denominator = 7;
            calculator.LeftOperand = left;
            calculator.RightOperand = right;
            result = calculator.Sub();
            Assert.Equal(1, result.Numerator);
            Assert.Equal(28, result.Denominator);

            // Result is reducible
            left = new Fraction();
            right = new Fraction();
            left.Numerator = 3;
            left.Denominator = 4;
            right.Numerator = 5;
            right.Denominator = 6;
            calculator.LeftOperand = left;
            calculator.RightOperand = right;
            result = calculator.Sub();
            Assert.Equal(-1, result.Numerator);
            Assert.Equal(12, result.Denominator);
        }

        /// <summary>
        /// A test for Mult
        ///</summary>
        [Fact]
        public void T12_MultTest()
        {
            Fraction result;
            Fraction left = new Fraction();
            Fraction right = new Fraction();
            Calculator calculator = new Calculator();

            left.Numerator = 3;
            left.Denominator = 4;
            right.Numerator = 5;
            right.Denominator = 7;
            calculator.LeftOperand = left;
            calculator.RightOperand = right;
            result = calculator.Mul();
            Assert.Equal(15, result.Numerator);
            Assert.Equal(28, result.Denominator);

            // Result is reducible
            left = new Fraction();
            right = new Fraction();
            left.Numerator = 3;
            left.Denominator = 4;
            right.Numerator = 5;
            right.Denominator = 6;
            calculator.LeftOperand = left;
            calculator.RightOperand = right;
            result = calculator.Mul();
            Assert.Equal(5, result.Numerator);
            Assert.Equal(8, result.Denominator);
        }

        /// <summary>
        /// A test for Div
        ///</summary>
        [Fact]
        public void T13_DivTest()
        {
            Fraction result;
            Fraction left = new Fraction();
            Fraction right = new Fraction();
            Calculator calculator = new Calculator();

            left.Numerator = 3;
            left.Denominator = 4;
            right.Numerator = 7;
            right.Denominator = 5;
            calculator.LeftOperand = left;
            calculator.RightOperand = right;
            result = calculator.Div();
            Assert.Equal(15, result.Numerator);
            Assert.Equal(28, result.Denominator);
            
            // Result is reducible
            left = new Fraction();
            right = new Fraction();
            left.Numerator = 3;
            left.Denominator = 4;
            right.Numerator = 6;
            right.Denominator = 5;
            calculator.LeftOperand = left;
            calculator.RightOperand = right;
            result = calculator.Div();
            Assert.Equal(5, result.Numerator);
            Assert.Equal(8, result.Denominator);
        }
    }
}