using Dashboard.Models.Weather;
using Xunit;

namespace XUnitTest
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
