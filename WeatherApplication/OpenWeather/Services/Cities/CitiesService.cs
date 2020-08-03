using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SimpleInjector.Advanced;
using WeatherApplication.OpenWeather.Models;

namespace WeatherApplication.OpenWeather.Services.Cities
{
    public class CitiesService : ICitiesService
    {
        private List<City> Cities { get; set; }
        private List<City> SearchedCities { get; }

        public CitiesService()
        {
            LoadCities();
            SearchedCities = new List<City>();
        }

        public List<City> GetCities()
        {
            return this.Cities;
        }

        public List<City> SearchHistory()
        {
            return this.SearchedCities;
        }

        public City FindById(int id)
        {
            if (Cities.Count == 0) return null;
            var city = Cities.First(c => c.Id == id);

            AddToHistory(city);
            return city;
        }

        public List<City> Search(string query)
        {
            return query.Length <= 1 ? new List<City>() : Cities.FindAll(c => c.Name.ToLower().Contains(query.ToLower()));
        }

        private void LoadCities()
        {
            var path = Path.Combine(Environment.CurrentDirectory, "OpenWeather", "Cities", "city.list.json");
            if (!File.Exists(path)) throw new FileNotFoundException("Couldn't load cities file");
            
            var bytes = File.ReadAllText(path); 
            Cities = JsonConvert.DeserializeObject<List<City>>(bytes);
        }
        
        
        private void AddToHistory(City city)
        {
            if(!SearchedCities.Contains(city)) SearchedCities.Insert(0, city);
        }

    }
}