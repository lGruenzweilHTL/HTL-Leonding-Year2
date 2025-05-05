using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreditCardClearing
{
    /// <summary>
    /// A payment to a payee on a specific date with a specific amount.
    /// </summary>
    public class Payment : IComparable<Payment>
    {
        public Payment(Account account, string payee, DateTime date, double amount)
        {
            Account = account;
            Payee = payee;
            Date = date;
            Amount = amount;
        }

        public Account Account {get; private set;}
        public DateTime Date { get; private set; }
        public string Payee { get; private set; }
        public double Amount { get; private set; }

        public override string ToString()
        {
            return $"{Account} Zahlung an {Payee,-10} {Date.ToShortDateString()}: {Amount:f2}";
        }


        #region IComparable<Payment> Members

        /// <summary>
        /// Compares two payments. First by date, then by account number.
        /// </summary>
        public int CompareTo(Payment other)
        {
            var dateComparison = Date.CompareTo(other.Date);
            var accountComparison = String.Compare(Account.CreditcardNumber, other.Account.CreditcardNumber, StringComparison.InvariantCulture);
            if (dateComparison == 0)
            {
                return accountComparison;
            }
            return dateComparison;
        }

        #endregion
    }
}
