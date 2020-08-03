using System.Collections.Generic;
using Newtonsoft.Json;

namespace WeatherApplication.OpenWeather.Models.OneCall
{
    public class BaseWeatherData
    {
        [JsonProperty("dt")]
        public double Dt { get; set; }
        
        [JsonProperty("pressure")]
        public int Pressure { get; set; }
        
        [JsonProperty("humidity")]
        public int Humidity { get; set; }
        
        [JsonProperty("dew_point")]
        public double DewPoint { get; set; }
        
        [JsonProperty("uvi")]
        public double Uvi { get; set; }
        
        [JsonProperty("clouds")]
        public int Clouds { get; set; }
        
        [JsonProperty("visibility")]
        public int Visibility { get; set; }
        
        [JsonProperty("wind_speed")]
        public double WindSpeed { get; set; }
        
        [JsonProperty("wind_deg")]
        public double WindDeg { get; set; }
        
        [JsonProperty("weather")]
        public List<Weather> Weather { get; set; }
        
    }
}