using System.Collections.Generic;
using System.Threading.Tasks;
using WeatherApplication.OpenWeather.Models;

namespace WeatherApplication.OpenWeather.Services.Cities
{
    public interface ICitiesService
    {
        List<City> GetCities();

        List<City> SearchHistory();

        City FindById(int id);

        List<City> Search(string query);
    }
}