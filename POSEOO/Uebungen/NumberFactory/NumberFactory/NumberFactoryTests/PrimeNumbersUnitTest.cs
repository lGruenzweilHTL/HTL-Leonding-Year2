using System;
using Xunit;
using NumberFactory;

namespace NumberFactory.Tests
{
    public class PrimeNumbersUnitTest
    {
        [Fact]
        public void PrimeNumbers_LowerBound_ShouldBeCorrect()
        {
            long expected = 0;
            INumbers numbers = Factory.Create(NumberType.Prime, 0, 10);

            Assert.Equal(expected, numbers.LowerBound);
        }

        [Fact]
        public void PrimeNumbers_UpperBound_ShouldBeCorrect()
        {
            long expected = 10;
            INumbers numbers = Factory.Create(NumberType.Prime, 0, 10);

            Assert.Equal(expected, numbers.UpperBound);
        }

        [Fact]
        public void PrimeNumbers_Count_ShouldBeCorrect()
        {
            int expected = 4;
            INumbers numbers = Factory.Create(NumberType.Prime, 0, 10);

            Assert.Equal(expected, numbers.Length);
        }

        [Fact]
        public void PrimeNumbers_FirstIndex_ShouldBeCorrect()
        {
            long expected = 2;
            INumbers numbers = Factory.Create(NumberType.Prime, 0, 10);

            Assert.Equal(expected, numbers[0]);
        }

        [Fact]
        public void PrimeNumbers_LastIndex_ShouldBeCorrect()
        {
            long expected = 7;
            INumbers numbers = Factory.Create(NumberType.Prime, 0, 10);

            Assert.Equal(expected, numbers[numbers.Length - 1]);
        }

        [Fact]
        public void PrimeNumbers_Content_ShouldBeCorrect()
        {
            long[] expected = { 2, 3, 5, 7 };
            INumbers numbers = Factory.Create(NumberType.Prime, 0, 10);

            for (int i = 0; i < numbers.Length; i++)
            {
                Assert.Equal(expected[i], numbers[i]);
            }
        }

        [Fact]
        public void PrimeNumbers_ContentWithDifferentLowerBound_ShouldBeCorrect()
        {
            long[] expected = { 2, 3, 5, 7 };
            INumbers numbers = Factory.Create(NumberType.Prime, 2, 10);

            for (int i = 0; i < numbers.Length; i++)
            {
                Assert.Equal(expected[i], numbers[i]);
            }
        }

        [Fact]
        public void PrimeNumbers_ContentWithDifferentUpperBound_ShouldBeCorrect()
        {
            long[] expected = { 2, 3, 5, 7 };
            INumbers numbers = Factory.Create(NumberType.Prime, 0, 7);

            for (int i = 0; i < numbers.Length; i++)
            {
                Assert.Equal(expected[i], numbers[i]);
            }
        }

        [Fact]
        public void PrimeNumbers_ForeachContent_ShouldBeCorrect()
        {
            int i = -1;
            long[] expected = { 2, 3, 5, 7 };
            INumbers numbers = Factory.Create(NumberType.Prime, 0, 10);

            foreach (var item in numbers)
            {
                i++;
                Assert.Equal(expected[i], item);
            }
        }

        [Fact]
        public void PrimeNumbers_ForeachContentWithLargeRange_ShouldBeCorrect()
        {
            int i = -1;
            long[] expected = { 2, 3, 5, 7, 11, 13, 17, 19, 23, 29, 31, 37, 41, 43, 47, 53, 59, 61, 67, 71, 73, 79, 83, 89, 97 };
            INumbers numbers = Factory.Create(NumberType.Prime, 0, 100);

            foreach (var item in numbers)
            {
                i++;
                Assert.Equal(expected[i], item);
            }
        }
    }
}