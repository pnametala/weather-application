using Newtonsoft.Json;

namespace WeatherApplication.OpenWeather.Models.OneCall
{
    public class HourlyData : BaseWeatherData
    {
        [JsonProperty("rain")]
        public VolumeData Rain { get; set; }
        
        [JsonProperty("snow")]
        public VolumeData Snow { get; set; }
        
        [JsonProperty("temp")]
        public double Temperature { get; set; }
        
        [JsonProperty("feels_like")]
        public double FeelsLike { get; set; }
        
    }
}