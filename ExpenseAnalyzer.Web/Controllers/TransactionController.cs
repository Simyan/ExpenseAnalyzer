using ExpenseAnalyzer.BLL.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace ExpenseAnalyzer.Web.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class TransactionController : ControllerBase
    {
        private readonly ITransactionService _iTransactionService;

        public TransactionController(ITransactionService iTransactionService)
        {
            _iTransactionService = iTransactionService;
        }

        

        public class TotalByCategoryDTO
        {
            public int UId { get; set; }
            public decimal Amount { get; set; }
            public string  Category { get; set; }
        }



        [HttpGet]
        //public Dictionary<string, decimal> GetTotalByCategory()
        public IEnumerable<TotalByCategoryDTO> GetTotalByCategory()
        {
            var result = _iTransactionService.GetTotalByCategory();
            var x = result.Select(x => new TotalByCategoryDTO { Category = x.Key, Amount = x.Value }).ToArray();

            //List<DummyDTO> x = new List<DummyDTO>();
            //x.Add(new TotalByCategoryDTO() { UId = 1, DummyKey = "Food", DummyValue = 358 });    
            //x.Add(new TotalByCategoryDTO() { UId = 2, DummyKey = "Travel", DummyValue = 3466 });

            return x;
        }

    }
}
