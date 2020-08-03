using Newtonsoft.Json;

namespace WeatherApplication.OpenWeather.Models.OneCall
{
    public class BaseTemperatureData
    {
        [JsonProperty("day")]
        public double Day { get; set; }
        
        [JsonProperty("night")]
        public double Night { get; set; }
        
        [JsonProperty("eve")]
        public double Evening { get; set; }
        
        [JsonProperty("morn")]
        public double Morning { get; set; }
    }
}