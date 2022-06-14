using ExpenseAnalyzer.BLL.Interfaces;
using ExpenseAnalyzer.BLL.Models;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace ExpenseAnalyzer.Web.Controllers
{
    [ApiController]
    [Route("[controller]/[action]/{id?}")]
    public class TransactionController : ControllerBase
    {
        private readonly ITransactionService _iTransactionService;

        public TransactionController(ITransactionService iTransactionService)
        {
            _iTransactionService = iTransactionService;
        }

        

        



        [HttpGet]
        //public Dictionary<string, decimal> GetTotalByCategory()
        public IEnumerable<TotalByCategoryDTO> GetTotalByCategory()
        {
            var result = _iTransactionService.GetTotalByCategory();
            
            //List<DummyDTO> x = new List<DummyDTO>();
            //x.Add(new TotalByCategoryDTO() { UId = 1, DummyKey = "Food", DummyValue = 358 });    
            //x.Add(new TotalByCategoryDTO() { UId = 2, DummyKey = "Travel", DummyValue = 3466 });

            return result;
        }

        [HttpGet]
        
        public IEnumerable<VendorDTO> GetVendorsByUser(long id)
        {
            return _iTransactionService.GetVendorsByUser(id);
        }

    }
}
