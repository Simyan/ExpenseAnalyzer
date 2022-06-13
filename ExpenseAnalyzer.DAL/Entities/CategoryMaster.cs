using System;
using System.Collections.Generic;

namespace ExpenseAnalyzer.DAL.Entities
{
    public partial class CategoryMaster
    {
        public short Uid { get; set; }
        public string Description { get; set; } = null!;

        public ICollection<Vendor> Vendors { get; set; }  
    }
}
