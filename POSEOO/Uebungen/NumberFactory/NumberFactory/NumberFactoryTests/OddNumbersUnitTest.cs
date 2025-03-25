using System;
using Xunit;
using NumberFactory;

namespace NumberFactory.Tests
{
    public class OddNumbersUnitTest
    {
        [Fact]
        public void OddNumbers_LowerBound_ShouldBeCorrect()
        {
            long expected = 4;
            INumbers numbers = Factory.Create(NumberType.Odd, 4, 10);

            Assert.Equal(expected, numbers.LowerBound);
        }

        [Fact]
        public void OddNumbers_UpperBound_ShouldBeCorrect()
        {
            long expected = 10;
            INumbers numbers = Factory.Create(NumberType.Odd, 4, 10);

            Assert.Equal(expected, numbers.UpperBound);
        }

        [Fact]
        public void OddNumbers_Count_ShouldBeCorrect()
        {
            int expected = 3;
            INumbers numbers = Factory.Create(NumberType.Odd, 4, 10);

            Assert.Equal(expected, numbers.Length);
        }

        [Fact]
        public void OddNumbers_FirstIndex_ShouldBeCorrect()
        {
            long expected = 5;
            INumbers numbers = Factory.Create(NumberType.Odd, 4, 10);

            Assert.Equal(expected, numbers[0]);
        }

        [Fact]
        public void OddNumbers_LastIndex_ShouldBeCorrect()
        {
            long expected = 9;
            INumbers numbers = Factory.Create(NumberType.Odd, 4, 10);

            Assert.Equal(expected, numbers[numbers.Length - 1]);
        }

        [Fact]
        public void OddNumbers_Content_ShouldBeCorrect()
        {
            long[] expected = { 5, 7, 9 };
            INumbers numbers = Factory.Create(NumberType.Odd, 4, 10);

            for (int i = 0; i < numbers.Length; i++)
            {
                Assert.Equal(expected[i], numbers[i]);
            }
        }

        [Fact]
        public void OddNumbers_ContentWithDifferentLowerBound_ShouldBeCorrect()
        {
            long[] expected = { 3, 5, 7, 9 };
            INumbers numbers = Factory.Create(NumberType.Odd, 3, 10);

            for (int i = 0; i < numbers.Length; i++)
            {
                Assert.Equal(expected[i], numbers[i]);
            }
        }

        [Fact]
        public void OddNumbers_ContentWithDifferentUpperBound_ShouldBeCorrect()
        {
            long[] expected = { 5, 7, 9, 11 };
            INumbers numbers = Factory.Create(NumberType.Odd, 4, 11);

            for (int i = 0; i < numbers.Length; i++)
            {
                Assert.Equal(expected[i], numbers[i]);
            }
        }

        [Fact]
        public void OddNumbers_ForeachContent_ShouldBeCorrect()
        {
            int i = -1;
            long[] expected = { 5, 7, 9 };
            INumbers numbers = Factory.Create(NumberType.Odd, 4, 10);

            foreach (var item in numbers)
            {
                i++;
                Assert.Equal(expected[i], item);
            }
        }
    }
}