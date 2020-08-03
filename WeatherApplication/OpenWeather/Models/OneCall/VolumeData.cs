using Newtonsoft.Json;

namespace WeatherApplication.OpenWeather.Models.OneCall
{
    public class VolumeData
    {
        [JsonProperty("1h")]
        public double LastHour { get; set; }
    }
}