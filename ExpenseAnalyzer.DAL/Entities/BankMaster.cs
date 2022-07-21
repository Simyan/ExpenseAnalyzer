using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseAnalyzer.DAL.Entities
{
    public partial class BankMaster
    {
        public int Uid { get; set; }
        public string Name { get; set; }

        public ReportMetadataMaster ReportMetadataMaster { get; set; }
        public ICollection<User> Users { get; set; }
        
    }
}
