using Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace CreditCardClearing
{
    public class AccountManager
    {
        private readonly Dictionary<string, Account> _accounts;
        private readonly ILogger _logger;

        public AccountManager(ILogger logger)
        {
            _accounts = new Dictionary<string, Account>();
            _logger = logger;
        }

        /// <summary>
        /// Read CSV lines from a file into the account list. Each account has multiple payments.
        /// </summary>
        public int ReadCsv(string fileName, bool continueOnError)
        {
            int countPayments = 0;
            string[] lines = File.ReadAllLines(fileName, Encoding.Default);
            return ReadCsv(lines, continueOnError);
        }

        /// <summary>
        /// Read CSV lines into the account list. Each account has multiple payments.
        /// Invalid credit card data will throw an exception from the Account constructor.
        /// These should be sent to the logger.
        /// </summary>
        /// <param name="lines">Array of CSV lines</param>
        /// <param name="continueOnError">In case of an error, log it and continue reading</param>
        /// <returns>Number of correctly read payments</returns>
        public int ReadCsv(string[] lines, bool continueOnError) {
            int valid = 0;

            for (var i = 0; i < lines.Length; i++) {
                try {
                    string[] parts = lines[i].Split(';');
                    if (parts.Length != 5) throw new ArgumentException("Invalid number of fields in line " + i);
                    
                    if (!DateTime.TryParse(parts[0], out DateTime date)) 
                        throw new ArgumentException("Could not parse date in line " + i);
                    if (!double.TryParse(parts[4], out double amount)) 
                        throw new ArgumentException("Could not parse amount in line " + i);
                    string creditCardNumber = parts[2];
                    
                    // Find or create account
                    if (!_accounts.TryGetValue(creditCardNumber, out Account acc)) {
                        acc = new Account(parts[1], creditCardNumber);
                        _accounts.Add(creditCardNumber, acc);
                    }
                    
                    // Add payment
                    Payment p = new Payment(acc, parts[3], date, amount);
                    acc.AddPayment(p);

                    valid++;
                }
                catch (Exception e) {
                    if (!continueOnError) throw; // If we don't continue on error, rethrow same exception
                    
                    _logger.LogError(e.Message);
                }
            }

            return valid;
        }

        /// <summary>
        /// All managed accounts (as a non-modifiable IEnumerable)
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Account> GetAccounts()
        {
            return _accounts.Values.ToList();
        }

        /// <summary>
        /// Number of managed accounts
        /// </summary>
        /// <returns></returns>
        public int NrAccounts => _accounts.Count;

        /// <summary>
        /// Return all payments of all customers
        /// </summary>
        /// <returns></returns>
        private List<Payment> GetAllPayments()
        {
            List<Payment> allPayments = new List<Payment>();
            foreach (Account account in _accounts.Values)
            {
                allPayments.AddRange(account.Payments);
            }
            allPayments.Sort();
            return allPayments;
        }

        /// <summary>
        /// Return all payments to recipients whose name starts accordingly
        /// </summary>
        /// <param name="payeeStartsWith">Recipient name</param>
        /// <returns>Relevant payments, sorted by date</returns>
        public IEnumerable<Payment> GetPayments(string payeeStartsWith) {
            return GetAllPayments()
                .Where(p => p.Payee.StartsWith(payeeStartsWith));
        }

        /// <summary>
        /// Return all payments on the selected date
        /// </summary>
        /// <param name="date">Selected date</param>
        /// <returns>Payments on the date</returns>
        public IEnumerable<Payment> GetPayments(DateTime date)
        {
           return GetAllPayments()
               .Where(p => p.Date == date);
        }
    }
}