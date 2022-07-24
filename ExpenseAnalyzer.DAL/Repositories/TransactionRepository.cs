using ExpenseAnalyzer.BLL.Interfaces;
using ExpenseAnalyzer.BLL.Models;
using ExpenseAnalyzer.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseAnalyzer.DAL.Repositories
{
    public class TransactionRepository : ITransactionRepository
    {
        private readonly ExpenseAnalyzerContext _context;

        public TransactionRepository(ExpenseAnalyzerContext context)
        {
            _context = context;
        }

        public void AddTransactions(IEnumerable<TransactionDTO> transactions)
        {
            try
            {
                var transactionEntities = transactions.Select(x => new Transaction()
                {
                    Amount = x.Amount,
                    Description = x.Description,
                    PostingDate = x.PostingDate,
                    TransactionDate = x.TransactionDate,
                    TypeUid = (byte)x.Type,
                    VendorUid = (byte)x.VendorUid,
                });
                _context.Transactions.AddRange(transactionEntities);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {

            }

           
            
        }

        public TransactionDTO GetTransaction(long UId)
        {
            var result = _context.Transactions.Where(x => x.Uid == UId).SingleOrDefault();

            return new TransactionDTO
            {
                Amount = result.Amount,
                Description = result.Description,
                PostingDate = result.PostingDate,
                TransactionDate = result.TransactionDate,
                Type = (TransactionType)result.TypeUid
            };
        }

        public IEnumerable<TransactionDTO> GetTransactions()
        {
            var result = _context.Transactions;

            return result.Select(result => new TransactionDTO()
            {
                 Amount = result.Amount,
                 Description = result.Description,
                 PostingDate = result.PostingDate,
                 TransactionDate = result.TransactionDate,
                 Type = (TransactionType)result.TypeUid,
                 VendorUid = result.VendorUid
            });
        }

    }
}
    