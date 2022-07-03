
using ExpenseAnalyzer.BLL.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExpenseAnalyzer.BLL.Interfaces;

namespace ExpenseAnalyzer.BLL.ServiceLayer
{
    public class TransactionService : ITransactionService
    {
        private readonly ITransactionRepository _transactionRepository;
        private readonly IVendorRepository _vendorRepository;
        private readonly ICategoryMasterRepository _categoryMasterRepository;
        public TransactionService(
            ITransactionRepository transactionRepository,
            IVendorRepository vendorRepository,
            ICategoryMasterRepository categoryMasterRepository)
        {
            _transactionRepository = transactionRepository;
            _vendorRepository = vendorRepository;
            _categoryMasterRepository = categoryMasterRepository;
        }


        //extract details into a list of objects
        public List<Models.TransactionDTO> ProcessRawTransactions(string[] rawTransactions)
        {
            List<Models.TransactionDTO> transactions = new List<Models.TransactionDTO>();

            try
            {
                foreach (var record in rawTransactions)
                {
                    Models.TransactionDTO transaction = new();
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

        static void ExtractTransaction(Models.TransactionDTO transaction, string[] tokens)
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


        public decimal GetTotalExpsense(List<Models.TransactionDTO> transactions)
        {
            return transactions
                    .Where(w => w.Type == TransactionType.Debit)
                    .Sum(s => s.Amount);
        }

        public List<Models.TransactionDTO> GetNTopExpense(List<Models.TransactionDTO> transactions, int n = 1)
        {
            return transactions
                    .Where(w => w.Type == TransactionType.Debit)
                    .OrderByDescending(x => x.Amount)
                    .Take(n).ToList();
        }

        public void PrintTransactions(List<Models.TransactionDTO> transactions)
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

        public TransactionDTO GetTransaction(long UId)
        {
            var result = _transactionRepository.GetTransaction(UId);
            return result;
        }

        

        //Inserting transactions
        //Get vendors
        //Extract vendors from the incoming transactions
        //Find intersection 
        //Push these to vendors table 
        //Get Vendors and join with transactions it get vendor uid
        //Push transactions

        public bool SubmitTransactionsAndVendors(List<Models.TransactionDTO> transactions)
        {
            try
            {
                var vendors = _vendorRepository.GetVendors().ToList();
                //TestVendor
                var excludedVendors = new HashSet<string>(vendors.Select(x => x.Description));
                var result = transactions
                               .Where(x => !excludedVendors.Contains(x.Description))
                               .Select(s => new VendorDTO { Description = s.Description })
                               .DistinctBy(d => d.Description)
                               .ToList();


                _vendorRepository.AddVendors(result);
                vendors = _vendorRepository.GetVendors().ToList();

                var transactionsToAdd = from t in transactions
                                        join v in vendors on t.Description equals v.Description
                                        select new TransactionDTO
                                        {
                                            Amount = t.Amount,
                                            Description = t.Description,
                                            PostingDate = t.PostingDate,
                                            TransactionDate = t.TransactionDate,
                                            Type = t.Type,
                                            VendorUid = v.Uid
                                        };

                _transactionRepository.AddTransactions(transactionsToAdd);

            }
            catch (Exception ex)
            {
                throw;
            }

            return false;
        }


        public IEnumerable<TotalByCategoryDTO> GetTotalByCategory()
        {
            var transactions = _transactionRepository.GetTransactions();
            var vendors = _vendorRepository.GetVendors(); 

            var transactionVendor = from t in transactions
                                    join v in vendors on t.Description equals v.Description
                                    select new 
                                    {
                                        Amount = t.Amount,
                                        Description = t.Description,
                                        Category = v.CategoryDescription
                                    };


            Dictionary<string, decimal> ExpenseByCategory = new Dictionary<string, decimal>();
            foreach (var item in transactionVendor)
            {
                if (item.Category == null) continue;

                if ( !ExpenseByCategory.ContainsKey(item.Category))
                {
                    ExpenseByCategory.Add(item.Category, item.Amount);
                    continue;
                }
                
                ExpenseByCategory[item.Category] += item.Amount;
            }

            //foreach(var item in ExpenseByCategory)
            //{
            //    Console.WriteLine($"{item.Key} : {item.Value}");
            //}

           

            return ExpenseByCategory.Select(x => new TotalByCategoryDTO { Category = x.Key, Amount = x.Value }).ToArray();
        }

        public IEnumerable<VendorDTO> GetVendorsByUser(long UId)
        {
            var vendors = _vendorRepository.GetVendorsByUser(UId);

            return vendors;
        }

        public IEnumerable<CategoryDTO> GetCategories()
        {
            return _categoryMasterRepository.GetCategories();
        }

        public bool Update(IEnumerable<VendorDTO> vendors)
        {
            try
            {
                _vendorRepository.Update(vendors);
            }
            catch(Exception)
            {
                //Todo: Log the error
                return false;
            }
           
            return true;
        }

    }
}
