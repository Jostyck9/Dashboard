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
        public int IdWidget { get; set; }
        public ulong Delay { get; set; }

        public ulong GetDelay()
        {
            return Delay;
        }

        public int GetId()
        {
            return IdWidget;
        }

        WidgetsId IWidget.GetType()
        {
            return Widgets.WidgetsId.WEATHER;
        }
    }
}
