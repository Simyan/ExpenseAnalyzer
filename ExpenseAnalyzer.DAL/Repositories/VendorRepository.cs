using ExpenseAnalyzer.BLL.Interfaces;
using ExpenseAnalyzer.BLL.Models;
using ExpenseAnalyzer.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseAnalyzer.DAL.Repositories
{
    public class VendorRepository : IVendorRepository
    {
        private readonly ExpenseAnalyzerContext _context;

        public VendorRepository(ExpenseAnalyzerContext context)
        {
            _context = context;
        }

        public void AddVendors(IEnumerable<VendorDTO> vendors)
        {
            var vendorEntities = vendors.Select(x => new Vendor()
            {
                CategoryMasterUid = x.CategoryMasterUid,
                Description = x.Description,
                DisplayName = x.DisplayName,
                Uid = x.Uid
            });
            _context.Vendors.AddRange();
            _context.SaveChanges();
        }

        public IEnumerable<VendorDTO> GetVendors()
        {
            var result = _context.Vendors;

            return result.Select(x => new VendorDTO()
            {
                CategoryDescription = x.CategoryMaster.Description,
                CategoryMasterUid = x.CategoryMasterUid,
                Description = x.Description,
                DisplayName = x.DisplayName,
                Uid = x.Uid
            });
        }
    }
}
