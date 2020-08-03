using Newtonsoft.Json;

namespace WeatherApplication.OpenWeather.Models.OneCall
{
    public class CurrentData : HourlyData
    {
        [JsonProperty("sunrise")]
        public double Sunrise { get; set; }
        
        [JsonProperty("sunset")]
        public double Sunset { get; set; }
    }
}