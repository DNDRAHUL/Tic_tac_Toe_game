using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tictoe.DAL;
using Tictoe.Repo;

namespace Tictoe.Controllers
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
        //public IConfiguration configuration;
        //public ManualDbContext manualdbcontext;

        //public WeatherForecastController(ILogger<WeatherForecastController> logger, IConfiguration _configuration, ManualDbContext _manualdbcontext)
        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
            //configuration = _configuration;
            //manualdbcontext = _manualdbcontext;
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

        //[HttpGet]
        //public IActionResult Get()
        //{
        //    UserRepo repo = new UserRepo(configuration,manualdbcontext);
        //    var users = repo.GetAllUser();
        //    return Ok(users);
        //}
    }
}
