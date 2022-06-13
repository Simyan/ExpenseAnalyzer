using ExpenseAnalyzer.BLL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseAnalyzer.BLL.Interfaces
{
    public interface ITransactionRepository
    {
        TransactionDTO GetTransaction(long UId);
        void AddTransactions(IEnumerable<TransactionDTO> transactions);
        IEnumerable<TransactionDTO> GetTransactions();
    }
}
