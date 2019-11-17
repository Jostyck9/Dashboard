using Dashboard.Models.About.Data;
using Dashboard.Models.About.Services;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dashboard.Models.About
{
    public class About : IAbout
    {
        private readonly List<IService> _services = new List<IService>
        {
            new SteamService(),
            new WeatherService(),
            new YoutubeService()
        };

        public List<ServiceData> GetListService()
        {
            List<ServiceData> res = new List<ServiceData>();

            foreach (var service in _services)
            {
                res.Add(service.GetService());
            }
            return res;
        }

        public ServerData GetServer()
        {
            ServerData res = new ServerData
            {
                Current_time = DateTimeOffset.Now.ToUnixTimeSeconds(),
                Services = GetListService()
            };
            return res;
        }

        public string GetAboutData(string ipClient)
        {
            AboutData res = new AboutData();
            var serializerSettings = new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            };

            res.Client = new ClientData 
            {
                Host = ipClient
            };
            res.Server = GetServer();
        
            return JsonConvert.SerializeObject(res, Formatting.Indented, serializerSettings);
        }
    }
}
