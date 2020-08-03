using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using WeatherApplication.OpenWeather.Models.OneCall;
using WeatherApplication.Settings;

namespace WeatherApplication.OpenWeather.Services.Weather
{
    public class WeatherService : IWeatherService
    {
        private readonly string _apiKey;

        private readonly string _baseUrl;

        private HttpClient _client;
        
        public WeatherService(OpenWeatherApiSettings settings)
        {
            _apiKey = settings.ApiKey;
            _baseUrl = settings.BaseUrl;
            _client = new HttpClient {BaseAddress = new Uri(_baseUrl)};
        }

        public async Task<OneCallData> OneCall(double lat, double lon)
        {
            var response =
                await _client.GetAsync($"onecall?lat={lat}&lon={lon}&exclude=minutely&units=metric&appid={_apiKey}");
            
            if(!response.IsSuccessStatusCode)
                throw new Exception($"{response.StatusCode} - {response.ReasonPhrase}");

            var content = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<OneCallData>(content);
        }

        public void SetHttpClient(HttpClient client)
        {
            _client = client;
        }
    }
}