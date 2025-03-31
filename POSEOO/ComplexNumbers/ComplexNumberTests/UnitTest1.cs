using Core;
using Xunit;

namespace ComplexNumberTests
{
    public class ComplexNumberTests
    {
        [Fact]
        public void Addition_TwoComplexNumbers_ReturnsCorrectSum()
        {
            var num1 = new ComplexNumber(1, 2);
            var num2 = new ComplexNumber(3, 4);
            var expected = new ComplexNumber(4, 6);

            var result = num1 + num2;

            Assert.Equal(expected, result);
        }

        [Fact]
        public void Subtraction_TwoComplexNumbers_ReturnsCorrectDifference()
        {
            var num1 = new ComplexNumber(5, 6);
            var num2 = new ComplexNumber(3, 4);
            var expected = new ComplexNumber(2, 2);

            var result = num1 - num2;

            Assert.Equal(expected, result);
        }

        [Fact]
        public void Multiplication_TwoComplexNumbers_ReturnsCorrectProduct()
        {
            var num1 = new ComplexNumber(1, 2);
            var num2 = new ComplexNumber(3, 4);
            var expected = new ComplexNumber(-5, 10);

            var result = num1 * num2;

            Assert.Equal(expected, result);
        }

        [Fact]
        public void Division_TwoComplexNumbers_ReturnsCorrectQuotient()
        {
            var num1 = new ComplexNumber(1, 2);
            var num2 = new ComplexNumber(3, 4);
            var expected = new ComplexNumber(0.44, 0.08);

            var result = num1 / num2;

            Assert.Equal(expected.Real, result.Real, 2);
            Assert.Equal(expected.Imag, result.Imag, 2);
        }

        [Fact]
        public void Magnitude_ComplexNumber_ReturnsCorrectMagnitude()
        {
            var num = new ComplexNumber(3, 4);
            var expected = 5.0;

            var result = num.Magnitude;

            Assert.Equal(expected, result);
        }

        [Fact]
        public void Equality_TwoEqualComplexNumbers_ReturnsTrue()
        {
            var num1 = new ComplexNumber(1, 2);
            var num2 = new ComplexNumber(1, 2);

            var result = num1 == num2;

            Assert.True(result);
        }

        [Fact]
        public void Inequality_TwoDifferentComplexNumbers_ReturnsTrue()
        {
            var num1 = new ComplexNumber(1, 2);
            var num2 = new ComplexNumber(3, 4);

            var result = num1 != num2;

            Assert.True(result);
        }

        [Fact]
        public void GreaterThan_ComplexNumberComparison_ReturnsCorrectResult()
        {
            var num1 = new ComplexNumber(3, 4);
            var num2 = new ComplexNumber(1, 1);

            var result = num1 > num2;

            Assert.True(result);
        }

        [Fact]
        public void LessThan_ComplexNumberComparison_ReturnsCorrectResult()
        {
            var num1 = new ComplexNumber(1, 1);
            var num2 = new ComplexNumber(3, 4);

            var result = num1 < num2;

            Assert.True(result);
        }

        [Fact]
        public void GreaterThanOrEqual_ComplexNumberComparison_ReturnsCorrectResult()
        {
            var num1 = new ComplexNumber(3, 4);
            var num2 = new ComplexNumber(3, 4);

            var result = num1 >= num2;

            Assert.True(result);
        }

        [Fact]
        public void LessThanOrEqual_ComplexNumberComparison_ReturnsCorrectResult()
        {
            var num1 = new ComplexNumber(1, 1);
            var num2 = new ComplexNumber(3, 4);

            var result = num1 <= num2;

            Assert.True(result);
        }
    }
}