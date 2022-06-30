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
    public class CategoryMasterRepository : ICategoryMasterRepository
    {
        private readonly ExpenseAnalyzerContext _context;

        public CategoryMasterRepository(ExpenseAnalyzerContext context)
        {
            _context = context;
        }

        public IEnumerable<CategoryDTO> GetCategories()
        {
            return _context.CategoryMasters.Select(result => new CategoryDTO()
            {
                 Description = result.Description,
                 Uid = result.Uid
            });
        }
    }
}
