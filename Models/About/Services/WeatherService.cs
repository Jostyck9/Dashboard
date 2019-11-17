using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dashboard.Models.About.Data;

namespace Dashboard.Models.About.Services
{
    public class WeatherService : IService
    {
        public WidgetData GetWeatherWidget()
        {
            WidgetData res = new WidgetData
            {
                Descrition = "Display the current weather from a city location",
                Name = "current_weather",
                Params = new List<ParamData>
                {
                    new ParamData
                    {
                        Name = "city",
                        Type = "string"
                    }
                }
            };
            return res;
        }

        public ServiceData GetService()
        {
            ServiceData res = new ServiceData
            {
                Name = "weather",
                Widgets = new List<WidgetData>
                {
                    GetWeatherWidget()
                }
            };
            return res;
        }
    }
}
