using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace WebApplication.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
                {
                    Date = DateTime.Now.AddDays(index),
                    TemperatureC = rng.Next(-20, 55),
                    Summary = Summaries[rng.Next(Summaries.Length)]
                })
                .ToArray();
        }

        [HttpGet("ToJson/{id}")]
        public IActionResult GetExample()
        {

            var values = new RouteValueDictionary();
            
            var queryValues = HttpContext.Request.Query;

            foreach (var value in queryValues)
            {
                values.TryAdd(value.Key, value.Value);
            }

            var routeDataValues = HttpContext.Request.HttpContext.GetRouteData().Values;

            foreach (var routeDataValue in routeDataValues)
            {
                values.TryAdd(routeDataValue.Key, routeDataValue.Value);
            }
            

            var jsonString = JsonConvert.SerializeObject(values);
            
            return Ok(jsonString);

        }
        
    }
}