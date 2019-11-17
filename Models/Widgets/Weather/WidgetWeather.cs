using Dashboard.Models.Weather;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dashboard.Models.Widgets.Weather
{
    public class WidgetWeather : IWidget
    {
        public WeatherData Data { get; set; }
        WidgetsId IWidget.GetType()
        {
            return Widgets.WidgetsId.WEATHER;
        }
    }
}
