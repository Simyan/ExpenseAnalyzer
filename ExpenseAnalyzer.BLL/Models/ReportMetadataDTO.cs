using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseAnalyzer.BLL.Models
{
    public class ReportMetadataDTO
    {
        public double TableArea { get; set; }
        public int TableIndex { get; set; }

        public int RowIndex { get; set; }

        public List<string> TableHeaders { get; set; }
    }

}
