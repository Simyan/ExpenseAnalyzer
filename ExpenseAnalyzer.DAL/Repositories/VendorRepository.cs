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

        //getvendors by user 
        public IEnumerable<VendorDTO> GetVendorsByUser(long UId)
        {
            var result = _context.Vendors.Where(x => x.UserUid == UId);

            return result.Select(x => new VendorDTO()
            {
                CategoryDescription = x.CategoryMaster.Description,
                CategoryMasterUid = x.CategoryMasterUid,
                Description = x.Description,
                DisplayName = x.DisplayName,
                Uid = x.Uid
            }).OrderBy(o => o.CategoryMasterUid);

        }

        public bool Update(IEnumerable<VendorDTO> vendors)
        {
            List<Vendor> entities = new();
            try
            {
                foreach (var x in vendors)
                {
                    var entity = new Vendor()
                    {
                        CategoryMasterUid = x.CategoryMasterUid,
                        Description = x.Description,
                        DisplayName = x.DisplayName,
                        Uid = x.Uid,
                        UserUid = x.UserUid
                    };
                    _context.Attach(entity);
                    _context.Entry(entity).Property("CategoryMasterUid").IsModified = true;
                    var response = _context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            
            return true;
            //var isSuccess = response > 0 ? true : false;
        }   


    }
}
