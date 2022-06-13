using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseAnalyzer.BLL.Models
{
    public class VendorDTO
    {
        public long Uid { get; set; }
        public string Description { get; set; }

        public string? DisplayName { get; set; }

        public short? CategoryMasterUid { get; set; }
        public string CategoryDescription { get; set; }

    }
}
