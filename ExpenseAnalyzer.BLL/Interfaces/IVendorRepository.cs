using ExpenseAnalyzer.BLL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseAnalyzer.BLL.Interfaces
{
    public interface IVendorRepository
    {
        IEnumerable<VendorDTO> GetVendors();
        void AddVendors(IEnumerable<VendorDTO> vendors);

    }
}
