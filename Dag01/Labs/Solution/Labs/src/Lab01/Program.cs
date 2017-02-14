using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lab01
{
    public class Program
    {
        static bool FilterLowCredit(BankAccount bankAccount)
        {
            return bankAccount.Credit < 200;
        }

        public static void Main(string[] args)
        {
            // Fundamental Exercises
            List<BankAccount> accounts = new List<BankAccount>
            {
                new BankAccount{ AccountNumber=1, Credit=200, Owner="John"},
                new BankAccount{ AccountNumber=2, Credit=300, Owner="Paul"},
                new BankAccount{ AccountNumber=3, Credit=600, Owner="George"},
                new BankAccount{ AccountNumber=4, Credit=100, Owner="Ringo"}
            };

            foreach (var account in accounts)
            {
                Console.WriteLine("Account Number: {0}, Owner: {1}, Credit: {2}", account.AccountNumber, account.Owner, account.Credit);
            }

            Console.WriteLine("\nLow Credit 1");
            var lowCreditAccounts = accounts.FindAll(FilterLowCredit);

            foreach (var account in lowCreditAccounts)
            {
                Console.WriteLine("Account Number: {0}, Owner: {1}, Credit: {2}", account.AccountNumber, account.Owner, account.Credit);
            }

            Console.WriteLine("\nLow Credit 2");
            var lowCreditAccounts2 = accounts.FindAll(account => account.Credit < 200);

            foreach (var account in lowCreditAccounts2)
            {
                Console.WriteLine("Account Number: {0}, Owner: {1}, Credit: {2}", account.AccountNumber, account.Owner, account.Credit);
            }


            Console.ReadKey(true);
        }
    }
}
