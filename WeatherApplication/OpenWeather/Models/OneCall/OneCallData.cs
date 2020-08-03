using System.Collections.Generic;
using Newtonsoft.Json;

namespace WeatherApplication.OpenWeather.Models.OneCall
{
    public class OneCallData
    
    {
        [JsonProperty("lat")]
        public double Lat { get; set; }
        
        [JsonProperty("lon")]
        public double Lon { get; set; }
        
        [JsonProperty("timezone")]
        public string Timezone { get; set; }
        
        [JsonProperty("timezone_offset")]
        public int TimezoneOffset { get; set; }
        
        [JsonProperty("current")]
        public CurrentData Current { get; set; }
        
        [JsonProperty("hourly")]
        public List<HourlyData> Hourly { get; set; }

        [JsonProperty("daily")]
        public List<DailyData> Daily { get; set; }


    }
}