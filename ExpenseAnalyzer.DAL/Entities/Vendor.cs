using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseAnalyzer.DAL.Entities
{
    public partial class Vendor
    {
        public long Uid { get; set; }   
        public string Description { get; set; }

        public string? DisplayName { get; set; }

        public short? CategoryMasterUid { get; set; }

        public long? UserUid { get; set; }


        public CategoryMaster? CategoryMaster { get; set; }
        public User? User { get; set; }
        public ICollection<Transaction> Transactions { get; set; }
    }
}
