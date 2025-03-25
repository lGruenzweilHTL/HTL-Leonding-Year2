using System;
using Xunit;
using NumberFactory;

namespace NumberFactory.Tests
{
    public class EvenNumbersUnitTest
    {
        [Fact]
        public void EvenNumbers_LowerBound_ShouldBeCorrect()
        {
            // Creates a number generator for even numbers in the range of 3 to 9
            long expected = 3;
            INumbers numbers = Factory.Create(NumberType.Even, 3, 9);

            Assert.Equal(expected, numbers.LowerBound);
        }

        [Fact]
        public void EvenNumbers_UpperBound_ShouldBeCorrect()
        {
            // Creates a number generator for even numbers in the range of 3 to 9
            long expected = 9;
            INumbers numbers = Factory.Create(NumberType.Even, 3, 9);

            Assert.Equal(expected, numbers.UpperBound);
        }

        [Fact]
        public void EvenNumbers_Count_ShouldBeCorrect()
        {
            int expected = 3;
            INumbers numbers = Factory.Create(NumberType.Even, 3, 9);

            Assert.Equal(expected, numbers.Length);
        }

        [Fact]
        public void EvenNumbers_FirstIndex_ShouldBeCorrect()
        {
            long expected = 4;
            INumbers numbers = Factory.Create(NumberType.Even, 3, 9);

            Assert.Equal(expected, numbers[0]);
        }

        [Fact]
        public void EvenNumbers_LastIndex_ShouldBeCorrect()
        {
            long expected = 8;
            INumbers numbers = Factory.Create(NumberType.Even, 3, 9);

            Assert.Equal(expected, numbers[numbers.Length - 1]);
        }

        [Fact]
        public void EvenNumbers_Content_ShouldBeCorrect()
        {
            long[] expected = { 4, 6, 8 };
            INumbers numbers = Factory.Create(NumberType.Even, 3, 9);

            for (int i = 0; i < numbers.Length; i++)
            {
                Assert.Equal(expected[i], numbers[i]);
            }
        }

        [Fact]
        public void EvenNumbers_ContentWithDifferentLowerBound_ShouldBeCorrect()
        {
            long[] expected = { 2, 4, 6, 8 };
            INumbers numbers = Factory.Create(NumberType.Even, 2, 9);

            for (int i = 0; i < numbers.Length; i++)
            {
                Assert.Equal(expected[i], numbers[i]);
            }
        }

        [Fact]
        public void EvenNumbers_ContentWithDifferentUpperBound_ShouldBeCorrect()
        {
            long[] expected = { 4, 6, 8, 10 };
            INumbers numbers = Factory.Create(NumberType.Even, 3, 10);

            for (int i = 0; i < numbers.Length; i++)
            {
                Assert.Equal(expected[i], numbers[i]);
            }
        }

        [Fact]
        public void EvenNumbers_ForeachContent_ShouldBeCorrect()
        {
            int i = -1;
            long[] expected = { 4, 6, 8 };
            INumbers numbers = Factory.Create(NumberType.Even, 3, 9);

            foreach (var item in numbers)
            {
                i++;
                Assert.Equal(expected[i], item);
            }
        }
    }
}