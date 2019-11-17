using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dashboard.Models.Weather;
using Microsoft.AspNetCore.Mvc;

namespace Dashboard.Controllers
{
    public class WeatherController : Controller
    {
        private WeatherModel _model = new WeatherModel();

        [HttpGet("[controller]/[action]/{city}")]
        /**
        * @brief get the wheather from a city which is a string
        *
        * @return a view
        */
        public async Task<IActionResult> index(string city)
        {
            var res = await _model.GetWeatherByLocation(city);
            return View(res);
        }

        [HttpGet("[controller]/[action]/{lat}/{lon}")]
        /**
        * @brief get the wheather from the latitutde and the longitude which are floats
        *
        * @return a view
        */
        public async Task<IActionResult> index(float lat, float lon)
        {
            var res = await _model.GetWeatherByCoord(lat, lon);
            return View(res);
        }
    }
}