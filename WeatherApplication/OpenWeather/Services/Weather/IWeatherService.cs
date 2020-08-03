using System.Threading.Tasks;
using WeatherApplication.OpenWeather.Models.OneCall;

namespace WeatherApplication.OpenWeather.Services.Weather
{
    public interface IWeatherService
    {
        Task<OneCallData> OneCall(double lat, double lon);
    }
}