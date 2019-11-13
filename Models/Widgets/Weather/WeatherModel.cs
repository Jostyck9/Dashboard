using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Dashboard.Models.Weather
{
    public class WeatherModel : IWeatherModel
    {
        private readonly string API_KEY = "c7bd62a75ea91a09924c0d7c0c770595";
        private readonly string BASE_URL = "http://api.openweathermap.org";

        public async Task<WeatherData> GetWeatherByLocation(string city = "London")
        {
            using (var client = new HttpClient()) {
                try
                {
                    client.BaseAddress = new Uri(BASE_URL);
                    var response = await client.GetAsync($"/data/2.5/weather?q={city}&appid={API_KEY}&units=metric");
                    response.EnsureSuccessStatusCode();

                    var stringResult = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<WeatherData>(stringResult);
                }
                catch (HttpRequestException _)
                {
                    return new WeatherData();
                }
            }
        }

        public async Task<WeatherData> GetWeatherByCoord(float latitude, float longitude)
        {
            using (var client = new HttpClient())
            {
                try
                {
                    client.BaseAddress = new Uri(BASE_URL);
                    var response = await client.GetAsync($"/data/2.5/weather?lat={latitude}&lon={longitude}&appid={API_KEY}&units=metric");
                    response.EnsureSuccessStatusCode();

                    var stringResult = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<WeatherData>(stringResult);
                }
                catch (HttpRequestException _)
                {
                    return new WeatherData();
                }
            }
        }
    }
}
