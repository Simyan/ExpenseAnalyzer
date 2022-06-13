using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseAnalyzer.DAL.Entities
{
    public partial class User
    {
        public long Uid { get; set; }
        public string Username { get; set; }

        public ICollection<Vendor> Vendors { get; set; }
        public ICollection<Transaction> Transactions { get; set; }

    }
}
