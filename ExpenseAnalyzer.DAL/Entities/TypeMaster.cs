using System;
using System.Collections.Generic;

namespace ExpenseAnalyzer.DAL.Entities
{
    public partial class TypeMaster
    {
        public byte Uid { get; set; }
        public string Description { get; set; } = null!;
        public ICollection<Transaction> Transactions { get; set; }
    }
}
