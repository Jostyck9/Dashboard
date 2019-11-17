using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dashboard.Models.Weather
{
    public interface IWeatherModel
    {
        /**
        * @brief get the weather informations from a location which is a string
        *
        * @return a structure of weather informations
        */
        public Task<WeatherData> GetWeatherByLocation(string location = "London");
        /**
        * @brief get the weather informations from coordinates which are floats
        *
        * @return a structure of weather informations
        */
        public Task<WeatherData> GetWeatherByCoord(float latitude, float longitude);
    }
}
