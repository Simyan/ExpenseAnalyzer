using ExpenseAnalyzer.BLL.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using Microsoft.AspNetCore.Authorization;

namespace ExpenseAnalyzer.Web.Controllers
{
    [Authorize]
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

       


       
    }
}