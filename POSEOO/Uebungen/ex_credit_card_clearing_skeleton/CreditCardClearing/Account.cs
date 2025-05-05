using System;
using System.Collections.Generic;
using System.Linq;

namespace CreditCardClearing
{
    /// <summary>
    /// A customer account with its payments
    /// </summary>
    public partial class Account
    {
        private string _creditcardNumber;
        private readonly List<Payment> _payments;

        public Account(string customerName, string creditcardNumber)
        {
            CustomerName = customerName;
            CreditcardNumber = creditcardNumber;
            _payments = new List<Payment>();
        }

        public Payment[] Payments => _payments.ToArray();

        public string CustomerName { get; private set; }

        /// <summary>
        /// Invalid credit card number throws an exception
        /// </summary>
        public string CreditcardNumber
        {
            get { return _creditcardNumber; }
            private set {
                _creditcardNumber = value;

                if (!IsCreditCardValid(_creditcardNumber))
                    throw new ArgumentException("invalid credit card number");
            }
        }

        /// <summary>
        /// Add a payment
        /// </summary>
        /// <param name="payment"></param>
        public void AddPayment(Payment payment)
        {
            _payments.Add(payment);
        }

        public override string ToString()
        {
            return $"Kunde: {CustomerName,-15} Kreditkarte: {CreditcardNumber}";
        }

        /// <summary>
        /// Checks the validity of the credit card number
        /// </summary>
        /// <param name="creditCardNumber"></param>
        /// <returns></returns>
        public static bool IsCreditCardValid(string creditCardNumber) {
            return creditCardNumber.Length == 16 
                && creditCardNumber.All(c => char.IsNumber(c) || c == '-') 
                && CalculateCheckDigit(creditCardNumber) == creditCardNumber[^1];
        }

        /// <summary>
        /// Determine the check digit for the credit card number
        /// </summary>
        /// <param name="creditCardNumber"></param>
        /// <returns></returns>
        private static int CalculateCheckDigit(string creditCardNumber) {
            int sum = 0;
            int idx = 0;
            for (int i = 0; i < creditCardNumber.Length - 1; i++) {
                char c = creditCardNumber[i];
                if (char.IsNumber(c)) continue;

                int digit = c - '0';
                sum += digit * (idx % 2 == 0 ? 2 : 1);
                idx++;
            }

            int next10 = 10 * (int)Math.Ceiling(sum / 10d);
            return next10 - sum;
        }
    }
}