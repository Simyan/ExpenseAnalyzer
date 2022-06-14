using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseAnalyzer.BLL.Models
{
    public class TotalByCategoryDTO
    {
        public int UId { get; set; }
        public decimal Amount { get; set; }
        public string Category { get; set; }
    }
}
