using ExpenseAnalyzer.BLL.Interfaces;
using ExpenseAnalyzer.BLL.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net;
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

        [HttpGet]
        public IEnumerable<CategoryDTO> GetCategories()
        {
            return _iTransactionService.GetCategories();
        }

        [HttpPost]
        public IActionResult SubmitVendors(IEnumerable<VendorDTO> request)
        {
            var response = _iTransactionService.Update(request);
            return response ? Ok() : StatusCode(500);
            
        }

        [HttpPost]
        public async Task<IActionResult> UploadFile(List<IFormFile> files)
        {
            long count = files.Count();
            long size = files.Sum(x => x.Length);
            List<string> fileList = new List<string>();

            

            foreach(IFormFile file in files)
            {
                if(file.Length > 0)
                {
                    // var filePath = Path.GetTempFileName();
                    var generatedFileName = Path.GetRandomFileName().Split('.')[0];
                    generatedFileName = generatedFileName + ".pdf";
                    var filePath = Path.Combine(@"C:\Users\simya\source\repos\Files\DownloadedFiles\", generatedFileName);
                    using (var stream = System.IO.File.Create(filePath))
                    {
                        await file.CopyToAsync(stream);
                        fileList.Add(stream.Name);

                    }
                    
                }
            }


            var response = _iTransactionService.ExtractTables(fileList);

            return Ok(new { count });
        }


    }
}
