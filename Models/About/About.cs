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

        /**
        * @brief get the list of the services
        *
        * @return a structure of service informations
        */
        public List<ServiceData> GetListService()
        {
            List<ServiceData> res = new List<ServiceData>();

            foreach (var service in _services)
            {
                res.Add(service.GetService());
            }
            return res;
        }

        /**
        * @brief get the list of the services and the current server time
        *
        * @return a class ServerData
        */
        public ServerData GetServer()
        {
            ServerData res = new ServerData
            {
                Current_time = DateTimeOffset.Now.ToUnixTimeSeconds(),
                Services = GetListService()
            };
            return res;
        }
        /**
        * @brief get all the informations of the widgets for the about.json
        *
        * @return a string
        */
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
