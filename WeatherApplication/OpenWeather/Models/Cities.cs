using Newtonsoft.Json;

namespace WeatherApplication.OpenWeather.Models
{
    public class City
    {
        
        [JsonProperty("id")]
        public int Id { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("country")]
        public string Country { get; set; }
        [JsonProperty("coord")]
        public Coordinates Coordinates { get; set; }
    }
}