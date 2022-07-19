using ExpenseAnalyzer.BLL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseAnalyzer.BLL.Interfaces
{
    public interface ITransactionService
    {
        bool SubmitTransactionsAndVendors(List<TransactionDTO> transactions);
        IEnumerable<TotalByCategoryDTO> GetTotalByCategory();

        IEnumerable<VendorDTO> GetVendorsByUser(long UId);
        IEnumerable<CategoryDTO> GetCategories();
        bool Update(IEnumerable<VendorDTO> vendors);
        BaseResponseDTO ExtractTables(List<string> fileList);
    }
}
