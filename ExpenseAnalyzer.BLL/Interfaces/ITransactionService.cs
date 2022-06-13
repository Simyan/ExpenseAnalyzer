using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseAnalyzer.BLL.Interfaces
{
    public interface ITransactionService
    {
        bool SubmitTransactionsAndVendors(List<Models.TransactionDTO> transactions);
        Dictionary<string, decimal> GetTotalByCategory();
    }
}
