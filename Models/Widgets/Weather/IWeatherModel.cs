using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dashboard.Models.Weather
{
    public interface IWeatherModel
    {
        public Task<WeatherData> GetWeatherByLocation(string location = "London");
        public Task<WeatherData> GetWeatherByCoord(float latitude, float longitude);
    }
}
