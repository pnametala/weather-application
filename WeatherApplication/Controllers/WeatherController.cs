using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WeatherApplication.OpenWeather.Models.OneCall;
using WeatherApplication.OpenWeather.Services.Weather;

namespace WeatherApplication.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WeatherController : ControllerBase
    {
        private readonly IWeatherService _weatherService;

        public WeatherController(IWeatherService weatherService)
        {
            _weatherService = weatherService;
        }

        [HttpGet("one-call/{lat}/{lon}")]
        public async Task<OneCallData> GetOneCall(double lat, double lon)
        {
            return await _weatherService.OneCall(lat, lon);
        }
    }
}