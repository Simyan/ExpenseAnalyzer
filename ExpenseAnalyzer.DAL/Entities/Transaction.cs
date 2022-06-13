using System;
using System.Collections.Generic;

namespace ExpenseAnalyzer.DAL.Entities
{
    public partial class Transaction
    {
        public long Uid { get; set; }
        public DateTime TransactionDate { get; set; }
        public DateTime PostingDate { get; set; }
        public string Description { get; set; } = null!;
        public decimal Amount { get; set; }
        
        public byte TypeUid { get; set; }

        public long? VendorUid { get; set; }

        public Vendor Vendor { get; set; }    
        public TypeMaster TypeMaster { get; set; }
    }   
}
