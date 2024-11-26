using System;
using Xunit;
using FractionCalculator;

namespace FractionCalculatorTest
{
    public class FractionTest
    {
        /// <summary>
        /// Creation of rational numbers including reduction
        ///</summary>
        [Fact]
        public void T01_CreateFractionTest()
        {
            Fraction fraction = new Fraction();
            // After object creation, numerator and denominator must be initialized to 1
            Assert.Equal(1, fraction.Numerator);
            Assert.Equal(1, fraction.Denominator);
            // Simple case without reduction
            fraction.Numerator = 3;
            fraction.Denominator = 4;
            Assert.Equal(3, fraction.Numerator);
            Assert.Equal(4, fraction.Denominator);
            // With reduction
            fraction = new Fraction();
            fraction.Numerator = 6;
            fraction.Denominator = 8;
            fraction.Normalize();
            Assert.Equal(3, fraction.Numerator);
            Assert.Equal(4, fraction.Denominator);
            // With denominator == 0
            fraction = new Fraction();
            fraction.Numerator = 6;
            fraction.Denominator = 0;
            Assert.Equal(6, fraction.Numerator);
            Assert.Equal(0, fraction.Denominator);
        }

        /// <summary>
        /// Conversion to a double number
        ///</summary>
        [Fact]
        public void T02_GetValue()
        {
            Fraction fraction = new Fraction();
            fraction.Numerator = 3;
            fraction.Denominator = 4;
            Assert.Equal(3.0 / 4, fraction.GetValue(), 0.001);
            fraction = new Fraction();
            fraction.Numerator = 4;
            fraction.Denominator = 4;
            Assert.Equal(1.0, fraction.GetValue(), 0.001);
            // Division by 0
            fraction = new Fraction();
            fraction.Numerator = 4;
            fraction.Denominator = 0;
            Assert.Equal(double.MaxValue, fraction.GetValue(), 0.001);
            fraction = new Fraction();
            fraction.Numerator = -4;
            fraction.Denominator = 0;
            Assert.Equal(double.MaxValue * (-1), fraction.GetValue(), 0.001);
        }

        /// <summary>
        /// A test for Normalize
        ///</summary>
        [Fact]
        public void T03_NormalizeTest()
        {
            Fraction fraction = new Fraction();

            fraction.Numerator = 6;
            fraction.Denominator = 8;
            fraction.Normalize();
            Assert.Equal(3, fraction.Numerator);
            Assert.Equal(4, fraction.Denominator);

            fraction.Numerator = -6;
            fraction.Denominator = 8;
            fraction.Normalize();
            Assert.Equal(-3, fraction.Numerator);
            Assert.Equal(4, fraction.Denominator);

            fraction.Numerator = 13;
            fraction.Denominator = 2;
            fraction.Normalize();
            Assert.Equal(13, fraction.Numerator);
            Assert.Equal(2, fraction.Denominator);

            fraction.Numerator = 13;
            fraction.Denominator = 26;
            fraction.Normalize();
            Assert.Equal(1, fraction.Numerator);
            Assert.Equal(2, fraction.Denominator);
        }

        /// <summary>
        /// A test for IsEqual
        ///</summary>
        [Fact]
        public void T04_IsEqualTest()
        {
            // Unequal fractions
            Fraction fraction = new Fraction();
            Fraction other = new Fraction();
            fraction.Numerator = 3;
            fraction.Denominator = 4;
            other.Numerator = 7;
            other.Denominator = 5;
            Assert.False(fraction.IsEqual(other));
            // After reduction, equal fractions
            fraction = new Fraction();
            other = new Fraction();
            fraction.Numerator = 3;
            fraction.Denominator = 4;
            other.Numerator = 6;
            other.Denominator = 8;
            Assert.True(fraction.IsEqual(other));
            // Denominator 0 and same sign ==> equal
            fraction = new Fraction();
            other = new Fraction();
            fraction.Numerator = 3;
            fraction.Denominator = 0;
            other.Numerator = 6;
            other.Denominator = 0;
            Assert.True(fraction.IsEqual(other));
            // Denominator 0 and different sign ==> unequal
            fraction = new Fraction();
            other = new Fraction();
            fraction.Numerator = 3;
            fraction.Denominator = 0;
            other.Numerator = -6;
            other.Denominator = 0;
            Assert.False(fraction.IsEqual(other));
        }

        /// <summary>
        /// A test for ConvertToString
        ///</summary>
        [Fact]
        public void T05_ConvertToStringTest()
        {
            Fraction fraction = new Fraction();
            fraction.Numerator = 3;
            fraction.Denominator = 4;
            string actual = fraction.ConvertToString();
            string expected = "3/4";
            Assert.Equal(expected, actual);
        }
    }
}