using Newtonsoft.Json;

namespace WeatherApplication.OpenWeather.Models
{
    public class Coordinates
    {
        [JsonProperty("lat")]
        public double Lat { get; set; }
        [JsonProperty("lon")]
        public double Lon { get; set; }
    }
}