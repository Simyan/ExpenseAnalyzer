using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseAnalyzer.DAL.Entities
{
    public partial class ReportMetadataMaster
    {
        public int Uid { get; set; }
        public double TableArea { get; set; }
        public int TableIndex { get; set; }

        public int RowIndex { get; set; }

        public int BankMasterUid { get; set; }

        public string TableHeaders { get; set; }

        public BankMaster BankMaster { get; set; }
    }
}
