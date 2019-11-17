using Dashboard.Models.Weather;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace XUnitTestProject
{
    public class TestWeatherModel
    {
        private readonly WeatherModel _wModel = new WeatherModel();

        [Fact]
        public async void TestgetWeatherByLocation()
        {
            var res = await _wModel.GetWeatherByLocation("Nantes");

            Assert.Equal("Nantes", res.Name);
            Assert.NotEqual("", res.Main.Temp);
            Assert.NotEmpty(res.Weather);

            res = await _wModel.GetWeatherByLocation("azydguziehoizjefoijzoiefjoizejfoizjeoifj");

            Assert.Null(res.Name);
        }

        [Fact]
        public async void TestgetWeatherByCoord()
        {
            var res = await _wModel.GetWeatherByCoord(20, 20);

            Assert.Equal("", res.Name);
            Assert.NotEqual("", res.Main.Temp);
            Assert.NotEmpty(res.Weather);

            res = await _wModel.GetWeatherByCoord(987198, 8118212112);

            Assert.Null(res.Name);
        }
    }
}
