using Newtonsoft.Json;

namespace WeatherApplication.OpenWeather.Models.OneCall
{
    public class DailyData : BaseWeatherData
    {
        [JsonProperty("sunrise")]
        public double Sunrise { get; set; }
        
        [JsonProperty("sunset")]
        public double Sunset { get; set; }

        [JsonProperty("temp")]
        public TemperatureData Temperature { get; set; }

        [JsonProperty("feels_like")]
        public BaseTemperatureData FeelsLike { get; set; }
        
        [JsonProperty("rain")]
        public double Rain { get; set; }
        
        [JsonProperty("snow")]
        public double Snow { get; set; }
    }
}