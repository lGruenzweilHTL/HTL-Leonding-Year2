using CreditCardClearing;
using Logging;
using System;
using System.Collections.Generic;

namespace ConsoleApp
{
    public class Program
    {
        // Call with -ce true|false [-p paymentsfile] [-l logfile]
        public static void Main(string[] args)
        {
            ILogger consoleLogger = new ConsoleLogger(LogLevel.Info);
            
            ParseCommandLine(args, out bool continueOnError, out string paymentsFileName, out string logFileName);
            
            var accounts = SetupAccountManager(logFileName, continueOnError, consoleLogger);
            
            try
            {
                Console.WriteLine("Creditcard Clearing");
                Console.WriteLine("===================");
                int nrPayments = accounts.ReadCsv(paymentsFileName, continueOnError);
                int nrAccounts = accounts.NrAccounts;
                Console.WriteLine($"{nrPayments} payments for {nrAccounts} accounts read.");

                WriteListToConsole("Account Overview", accounts.GetAccounts());
                WriteListToConsole("Payments to Mediamarkt", accounts.GetPayments("Media"));
                WriteListToConsole("Payments on 28.9.2008", accounts.GetPayments(DateTime.Parse("28.9.2008")));

            }
            catch (ArgumentException e)
            {
                consoleLogger.LogError("Abort reading payments!\n" + e.Message);
            }
            catch (System.IO.FileNotFoundException e)
            {
                consoleLogger.LogError("File not found!\n" + e.Message);
            }
            catch (Exception e)
            {
                consoleLogger.LogError("Unknown error!\n" + e.Message);
            }
        }

        /// <summary>
        /// Factory method to create an AccountManager with a logger.
        /// </summary>
        /// <param name="logFileName"></param>
        /// <param name="continueOnError"></param>
        /// <param name="consoleLogger"></param>
        /// <returns></returns>
        private static AccountManager SetupAccountManager(string logFileName, bool continueOnError, ILogger consoleLogger)
        {
            ILogger fileLogger = new FileLogger(logFileName, LogLevel.Warning);
            ILogger logger = new LoggerCollection(fileLogger, consoleLogger);
            return new AccountManager(logger);
        }
        
        // -ce true|false [-p paymentsfile] [-l logfile]
        public static void ParseCommandLine(string[] args, out bool continueOnError, out string paymentsFile, out string logFile)
        {
            continueOnError = false;
            paymentsFile = "Payments.csv";
            logFile = "log.txt";
            
            if (args.Length <= 0) throw new ArgumentException("No command line arguments given!");
                
            for (var i = 0; i < args.Length; i++) {
                var arg = args[i];

                switch (arg) {
                    case "-ce":
                        if (!bool.TryParse(args[++i], out continueOnError)) 
                            throw new ArgumentException("Could not parse CE.", nameof(continueOnError));
                        break;
                    case "-p":
                        paymentsFile = args[++i];
                        break;
                    case "-l":
                        logFile = args[++i];
                        break;
                }
            }
        }

        static void WriteListToConsole<T>(string title, IEnumerable<T> objects)
        {
            Console.WriteLine();
            Console.WriteLine($"{title}");
            Console.WriteLine(new String('-', title.Length));
            foreach (T o in objects)
            {
                Console.WriteLine(o.ToString());
            }
            Console.WriteLine();
        }
    }
}