using ExpenseAnalyzer.Entities;
using ExpenseAnalyzer.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseAnalyzer.ServiceLayer
{
    public class TransactionService
    {

        //extract details into a list of objects
        public List<Models.Transaction> ProcessRawTransactions(string[] rawTransactions)
        {
            List<Models.Transaction> transactions = new List<Models.Transaction>();

            try
            {
                foreach (var record in rawTransactions)
                {
                    Models.Transaction transaction = new();
                    transaction.Type = TransactionType.Debit;

                    var tokens = record.Split(" ");
                    ExtractTransaction(transaction, tokens);

                    transactions.Add(transaction);
                }
                return transactions;
            }

            catch (Exception ex)
            {
                //TODO: Add log!
                Console.WriteLine($"Error: {ex.Message}");
                throw;
            }


        }

        static void ExtractTransaction(Models.Transaction transaction, string[] tokens)
        {
            //TODO: Add log!
            if (tokens.Length < 4) throw new Exception("Tokens must have at least 4 items");

            bool isTransactionDate = DateTime.TryParseExact(tokens[0],
                                                            "dd/MM/yyyy",
                                                            CultureInfo.InvariantCulture,
                                                            DateTimeStyles.None,
                                                            out DateTime transactionDate);
            transaction.TransactionDate = transactionDate;

            bool isPostingDate = DateTime.TryParseExact(tokens[1],
                                                        "dd/MM/yyyy",
                                                        CultureInfo.InvariantCulture,
                                                        DateTimeStyles.None,
                                                        out DateTime postingDate);
            transaction.PostingDate = postingDate;

            transaction.Description = String.Join(" ", tokens[2..(tokens.Length - 1)]);

            //Handle for credited amount - has CR appended to the amount
            string amountText = tokens.Last();
            if (amountText.Substring(amountText.Length - 2) == "CR")
            {
                amountText = amountText.Substring(0, amountText.Length - 2);
                transaction.Type = TransactionType.Credit;
            }

            bool isAmount = Decimal.TryParse(amountText, out Decimal amount);
            transaction.Amount = amount;

            //TODO: add log!
            if (!(isTransactionDate && isPostingDate && isAmount))
                throw new NullReferenceException("Extracted Tokens cannot be null");
        }


        public decimal GetTotalExpsense(List<Models.Transaction> transactions)
        {
            return transactions
                    .Where(w => w.Type == TransactionType.Debit)
                    .Sum(s => s.Amount);
        }

        public List<Models.Transaction> GetNTopExpense(List<Models.Transaction> transactions, int n = 1)
        {
            return transactions
                    .Where(w => w.Type == TransactionType.Debit)
                    .OrderByDescending(x => x.Amount)
                    .Take(n).ToList();
        }

        public void PrintTransactions(List<Models.Transaction> transactions)
        {
            foreach (var record in transactions)
            {
                Console.WriteLine($"Transaction Date: {record.TransactionDate}, " +
                                  $"Posting Date: {record.PostingDate}, " +
                                  $"Description: {record.Description}, " +
                                  $"Amount: {record.Amount}, " +
                                  $"Type: {record.Type.ToString()}");
            }
        }

        public Entities.Transaction GetTransaction(long UId)
        {
            using var context = new ExpenseAnalyzerContext();
            var result = context.Transactions.Where(x => x.Uid == UId).SingleOrDefault();
            return result;
        }

        

        //Inserting transactions
        //Get vendors
        //Extract vendors from the incoming transactions
        //Find intersection 
        //Push these to vendors table 
        //Get Vendors and join with transactions it get vendor uid
        //Push transactions

        public bool SubmitTransactionsAndVendors(List<Models.Transaction> transactions)
        {
            using var context = new ExpenseAnalyzerContext();
            try
            {
                var vendors = context.Vendors.ToList();
                //TestVendor
                var excludedVendors = new HashSet<string>(vendors.Select(x => x.Description));
                var result = transactions
                               .Where(x => !excludedVendors.Contains(x.Description))
                               .Select(s => new Vendor { Description = s.Description })
                               .DistinctBy(d => d.Description)
                               .ToList();


                context.Vendors.AddRange(result);
                context.SaveChanges();
                vendors = context.Vendors.ToList();

                var transactionsToAdd = from t in transactions
                                        join v in vendors on t.Description equals v.Description
                                        select new Entities.Transaction
                                        {
                                            Amount = t.Amount,
                                            Description = t.Description,
                                            PostingDate = t.PostingDate,
                                            TransactionDate = t.TransactionDate,
                                            TypeUid = (byte)t.Type.GetHashCode(),
                                            VendorUid = v.Uid
                                        };

                context.Transactions.AddRange(transactionsToAdd);
                context.SaveChanges();

            }
            catch (Exception ex)
            {
                throw;
            }

            return false;
        }









    }
}
