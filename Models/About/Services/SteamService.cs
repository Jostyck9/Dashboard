using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dashboard.Models.About.Data;

namespace Dashboard.Models.About.Services
{
    public class SteamService : IService
    {
        /**
        * @brief get the achivements of a game 
        *
        * @return a Widget Data class
        */
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

        /**
        * @brief get the current number of player of a game
        *
        * @return a Widget Data class
        */
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
        /**
        * @brief get the news of a game 
        *
        * @return a Widget Data class
        */
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

        /**
        * @brief get all the services of steam
        *
        * @return a Service Data class
        */
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
