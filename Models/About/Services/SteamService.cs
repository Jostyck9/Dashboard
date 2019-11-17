using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dashboard.Models.About.Data;

namespace Dashboard.Models.About.Services
{
    public class SteamService : IService
    {
        public WidgetData GetAchievementWidget()
        {
            WidgetData res = new WidgetData
            {
                Descrition = "Display a game's list of achievement with their global progression",
                Name = "achievements",
                Params = new List<ParamData>
                {
                    new ParamData
                    {
                        Name = "appId",
                        Type = "string"
                    }
                }
            };
            return res;
        }

        public WidgetData GetPlayersWidget()
        {
            WidgetData res = new WidgetData
            {
                Descrition = "Display the number of current players playing in a game",
                Name = "players_game",
                Params = new List<ParamData>
                {
                    new ParamData
                    {
                        Name = "appId",
                        Type = "string"
                    }
                }
            };
            return res;
        }

        public WidgetData GetNewsWidget()
        {
            WidgetData res = new WidgetData
            {
                Descrition = "Display the last news from a game",
                Name = "news_game",
                Params = new List<ParamData>
                {
                    new ParamData
                    {
                        Name = "appId",
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
                Name = "steam",
                Widgets = new List<WidgetData>
                {
                    GetAchievementWidget(),
                    GetPlayersWidget(),
                    GetNewsWidget()
                }
            };
            return res;
        }
    }
}
