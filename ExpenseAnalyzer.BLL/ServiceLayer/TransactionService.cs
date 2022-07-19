
using ExpenseAnalyzer.BLL.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExpenseAnalyzer.BLL.Interfaces;
using UglyToad.PdfPig;
using Tabula.Extractors;
using Tabula;

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
        public List<Models.TransactionDTO> ProcessRawTransactions(List<List<string>> rawTransactions)
        {
            List<Models.TransactionDTO> transactions = new List<Models.TransactionDTO>();

            try
            {
                foreach (var record in rawTransactions)
                {
                    Models.TransactionDTO transaction = new();
                    transaction.Type = TransactionType.Debit;

                    //var tokens = record.Split(" ");
                    ExtractTransaction(transaction, record);

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

        static void ExtractTransaction(Models.TransactionDTO transaction, List<string> tokens)
        {
            //TODO: Add log!
            if (tokens.Count < 4) throw new Exception("Tokens must have at least 4 items");

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

            transaction.Description = String.Join(" ", tokens[2]);

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

                if (!ExpenseByCategory.ContainsKey(item.Category))
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
            catch (Exception)
            {
                //Todo: Log the error
                return false;
            }

            return true;
        }


        public BaseResponseDTO ExtractTables(List<string> fileList)
        {
            try
            {
                List<ConfidenceMatrix> confidenceMatrices = new List<ConfidenceMatrix>();
                foreach (var fileName in fileList)
                {
                    FileStream pdfStream = File.OpenRead(fileName);
                    using (PdfDocument document = PdfDocument.Open(pdfStream, new ParsingOptions() { ClipPaths = true }))
                    {
                        var numberOfPages = document.NumberOfPages;
                        var result = GetData(document, confidenceMatrices);
                        ProcessRawTransactions(result);
                    }
                }
            }
            catch (Exception ex)
            {

            }

            return new BaseResponseDTO
            {
                IsSuccessful = true
            };
        }

        public class ConfidenceMatrix
        {
            public bool isAreaMatch { get; set; }
            public bool isTableIndexMatch { get; set; }
            public bool isRowIndexMatch { get; set; }
            public bool isColumnNamesMatch { get; set; }
        }

        public class ReportMetadata
        {
            public double TableArea { get; set; }
            public int TableIndex { get; set; }

            public int RowIndex { get; set; }

            public List<string> TableHeaders { get; set; }
        }

       

        public List<List<string>> GetData(PdfDocument document, List<ConfidenceMatrix> confidenceMatrices) {
            ReportMetadata reportMetadata = new ReportMetadata
            {
                RowIndex = 1,
                TableArea = 5000.33,
                TableHeaders = new List<string> { "Transaction Date", "Posting Date", "Description", "Amount (AED)" },
                TableIndex = 2
            };

            IExtractionAlgorithm extractionAlgorithm = new SpreadsheetExtractionAlgorithm();
            ObjectExtractor extractor = new ObjectExtractor(document);
            
            List<List<string>> reportTokens = new List<List<string>>();


            for (int i = 1; i <= document.NumberOfPages; i++)
            {
                ConfidenceMatrix confidenceMatrix = new ConfidenceMatrix();
                PageArea page = extractor.Extract(i);
                List<Table> tables = extractionAlgorithm.Extract(page);

                int tableIndex = 0;
                foreach (Table table in tables)
                {
                    confidenceMatrix.isTableIndexMatch = tableIndex == reportMetadata.TableIndex;
                    confidenceMatrix.isAreaMatch = table.Area == reportMetadata.TableArea;
                    List<List<string>> tabletokens = new List<List<string>>();

                    int rowIndex = 0;
                    foreach (var row in table.Rows)
                    {
                        confidenceMatrix.isRowIndexMatch = rowIndex == reportMetadata.RowIndex;
                        int headerTokenCount = 0;


                        if (!confidenceMatrix.isColumnNamesMatch)
                        {
                            foreach (var cell in row)
                            {
                                foreach (var text in cell.TextElements.ToList())
                                {
                                    if (reportMetadata.TableHeaders.Contains(text.GetText()))
                                    {
                                        headerTokenCount++; 
                                    }
                                }
                            }
                            
                        }
                        else
                        {
                            int tableTokenColumnIndex = 0;

                            
                            foreach (var cell in row)
                            {
                                int tableTokenRowIndex = 0;


                                foreach (var text in cell.TextElements.ToList())
                                {
                                    if(tableTokenColumnIndex == 0)
                                    {
                                        tabletokens.Add(new List<string>(cell.TextElements.Count));
                                    }

                                    if (text.GetText().ToUpperInvariant() == "SIMYAN ANWAR: 4034 XXXX XXXX 4681".ToUpperInvariant() 
                                        || text.GetText().ToUpperInvariant() == "PRIMARY CARD NUMBER".ToUpperInvariant()) 
                                            continue;
                                    
                                    tabletokens[tableTokenRowIndex].Add(text.GetText());
                                    tabletokens[tableTokenRowIndex][tableTokenColumnIndex] = text.GetText();
                                    tableTokenRowIndex++;
                                }
                                tableTokenColumnIndex++;
                                
                            }

                            
                        }

                        confidenceMatrix.isColumnNamesMatch = reportMetadata.TableHeaders.Count == headerTokenCount;
                        rowIndex++;

                        confidenceMatrices.Add(confidenceMatrix);
                    }

                    reportTokens.AddRange(tabletokens);
                    tableIndex++;
                }


            }

            return reportTokens;
        }

        
    } 
}
