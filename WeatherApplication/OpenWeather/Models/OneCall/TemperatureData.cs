using Newtonsoft.Json;

namespace WeatherApplication.OpenWeather.Models.OneCall
{
    public class TemperatureData : BaseTemperatureData
    {
        [JsonProperty("min")]
        public double Min { get; set; }
        
        [JsonProperty("max")]
        public double Max { get; set; }
    }
}