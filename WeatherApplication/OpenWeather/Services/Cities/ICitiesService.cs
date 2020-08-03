using System.Collections.Generic;
using System.Threading.Tasks;
using WeatherApplication.OpenWeather.Models;

namespace WeatherApplication.OpenWeather.Services.Cities
{
    public interface ICitiesService
    {
        public List<City> Cities { get; set; }
        
        public List<City> SearchHistory { get; set; }

        public City FindById(int id);

        public List<City> Search(string query);
    }
}