using Xunit;
using CreditCardClearing;

namespace CreditCardClearing.Tests
{
    public class AccountTests
    {
        [Fact]
        public void IsCreditCard_OK()
        {
            string creditCardNumber = "2718281828458567";
            bool expected = true;
            var actual = Account.IsCreditCardValid(creditCardNumber);
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void IsCreditCard_Checksum_NotOK()
        {
            string creditCardNumber = "2718281828458566";
            bool expected = false;
            var actual = Account.IsCreditCardValid(creditCardNumber);
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void IsCreditCard_Length_NotOK()
        {
            string creditCardNumber = "27182818284585666";
            bool expected = false;
            var actual = Account.IsCreditCardValid(creditCardNumber);
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void IsCreditCard_ContainsLetter()
        {
            string creditCardNumber = "2718281828X58566";
            bool expected = false;
            var actual = Account.IsCreditCardValid(creditCardNumber);
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void IsCreditCard_OK_ZeroAtEnd()
        {
            string creditCardNumber = "2418281828458560";
            bool expected = true;
            var actual = Account.IsCreditCardValid(creditCardNumber);
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void IsCreditCard_OK_WithDelimiters()
        {
            string creditCardNumber = "2718-2818-2845-8567";
            bool expected = true;
            var actual = Account.IsCreditCardValid(creditCardNumber);
            Assert.Equal(expected, actual);
        }
    }
}