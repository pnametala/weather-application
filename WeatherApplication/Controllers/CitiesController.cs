using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WeatherApplication.OpenWeather.Models;
using WeatherApplication.OpenWeather.Services;
using WeatherApplication.OpenWeather.Services.Cities;

namespace WeatherApplication.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CitiesController : ControllerBase
    {
        private readonly ICitiesService _service;

        public CitiesController(ICitiesService service)
        {
            _service = service;
        }
        
        [HttpGet]
        public IEnumerable<City> GetCities(string query)
        {
            return _service.Search(query);
        }
        
        [HttpGet("{id}")]
        public City GetCity(int id)
        {
            return _service.FindById(id);
        }

        [HttpGet("history")]
        public List<City> GetHistory()
        {
            return _service.SearchHistory();
        }
    }
}