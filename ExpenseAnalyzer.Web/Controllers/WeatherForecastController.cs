using ExpenseAnalyzer.BLL.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace ExpenseAnalyzer.Web.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class WeatherForecastController : ControllerBase
    {
       private readonly ITransactionService _iTransactionService;

        

        private static readonly string[] Summaries = new[]
        {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching" 
        };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ITransactionService iTransactionService, ILogger<WeatherForecastController> logger)
        {
            _iTransactionService = iTransactionService;
            _logger = logger;
        }



        [HttpGet]
        //[Route("WeatherForecast")]
        public IEnumerable<WeatherForecast> Get()
        {
            var x = Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();

            return x;
        }

        public class DummyDTO
        {
            public int UId { get; set; }
            public decimal DummyValue { get; set; }
            public string DummyKey { get; set; }
        }


        
        [HttpGet]
        //public Dictionary<string, decimal> GetTotalByCategory()
        public IEnumerable<DummyDTO> GetTotalByCategory()
        {
            var result = _iTransactionService.GetTotalByCategory();
            var x = result.Select(x => new DummyDTO { DummyKey = x.Key, DummyValue = x.Value }).ToArray();

            //List<DummyDTO> x = new List<DummyDTO>();
            //x.Add(new DummyDTO() { UId = 1, DummyKey = "Food", DummyValue = 358 });    
            //x.Add(new DummyDTO() { UId = 2, DummyKey = "Travel", DummyValue = 3466 });

            return x;
        }
    }
}