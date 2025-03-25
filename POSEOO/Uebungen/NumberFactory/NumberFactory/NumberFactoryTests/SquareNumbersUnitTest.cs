using System;
using Xunit;
using NumberFactory;

namespace NumberFactory.Tests
{
    public class SquareNumbersUnitTest
    {
        [Fact]
        public void SquareNumbers_LowerBound_ShouldBeCorrect()
        {
            long expected = 4;
            INumbers numbers = Factory.Create(NumberType.Square, 4, 10);

            Assert.Equal(expected, numbers.LowerBound);
        }

        [Fact]
        public void SquareNumbers_UpperBound_ShouldBeCorrect()
        {
            long expected = 10;
            INumbers numbers = Factory.Create(NumberType.Square, 4, 10);

            Assert.Equal(expected, numbers.UpperBound);
        }

        [Fact]
        public void SquareNumbers_Count_ShouldBeCorrect()
        {
            int expected = 2;
            INumbers numbers = Factory.Create(NumberType.Square, 4, 10);

            Assert.Equal(expected, numbers.Length);
        }

        [Fact]
        public void SquareNumbers_FirstIndex_ShouldBeCorrect()
        {
            long expected = 4;
            INumbers numbers = Factory.Create(NumberType.Square, 4, 10);

            Assert.Equal(expected, numbers[0]);
        }

        [Fact]
        public void SquareNumbers_LastIndex_ShouldBeCorrect()
        {
            long expected = 9;
            INumbers numbers = Factory.Create(NumberType.Square, 4, 10);

            Assert.Equal(expected, numbers[numbers.Length - 1]);
        }

        [Fact]
        public void SquareNumbers_Content_ShouldBeCorrect()
        {
            long[] expected = { 4, 9 };
            INumbers numbers = Factory.Create(NumberType.Square, 4, 10);

            for (int i = 0; i < numbers.Length; i++)
            {
                Assert.Equal(expected[i], numbers[i]);
            }
        }

        [Fact]
        public void SquareNumbers_ContentWithDifferentLowerBound_ShouldBeCorrect()
        {
            long[] expected = { 4, 9 };
            INumbers numbers = Factory.Create(NumberType.Square, 3, 10);

            for (int i = 0; i < numbers.Length; i++)
            {
                Assert.Equal(expected[i], numbers[i]);
            }
        }

        [Fact]
        public void SquareNumbers_ContentWithDifferentUpperBound_ShouldBeCorrect()
        {
            long[] expected = { 4, 9 };
            INumbers numbers = Factory.Create(NumberType.Square, 4, 11);

            for (int i = 0; i < numbers.Length; i++)
            {
                Assert.Equal(expected[i], numbers[i]);
            }
        }

        [Fact]
        public void SquareNumbers_ForeachContent_ShouldBeCorrect()
        {
            int i = -1;
            long[] expected = { 4, 9 };
            INumbers numbers = Factory.Create(NumberType.Square, 4, 10);

            foreach (var item in numbers)
            {
                i++;
                Assert.Equal(expected[i], item);
            }
        }
    }
}