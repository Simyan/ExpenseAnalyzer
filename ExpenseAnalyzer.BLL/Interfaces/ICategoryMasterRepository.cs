using ExpenseAnalyzer.BLL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseAnalyzer.BLL.Interfaces
{
    public interface ICategoryMasterRepository
    {
        IEnumerable<CategoryDTO> GetCategories();
    }
}
