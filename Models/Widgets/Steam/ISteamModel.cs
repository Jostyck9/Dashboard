using Dashboard.Models.Steam.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dashboard.Models.Steam
{
    public interface ISteamModel
    {
        /**
        * @brief get the list of game
        *
        * @return a structure of steam informations
        */
        public Task<AppList> GetGameList();
        /**
        * @brief get a list of game from a string
        *
        * @return a list if structure of steam informations
        */
        public Task<List<SteamGame>> SearchGame(string query);
        /**
        * @brief get the last informations of a game from a application ID which is a string
        *
        * @return a structure of steam informations
        */
        public Task<AppNewsData> GetLastNews(string appId, ulong count = 1, ulong maxLength = 500);
        /**
        * @brief get the current number of players on the game from a application ID which is a string
        *
        * @return a structure of steam informations
        */
        public Task<ResponseData> GetCurrentPlayersGame(string appId);
        /**
        * @brief get the game banner of a game from a application Id which is a string
        *
        * @return a string
        */
        public String GetGameBanner(string appId);
        /**
        * @brief get the achivements of a game from a application Id which is a string
        *
        * @return a structure of steam informations
        */
        public Task<AchievementList> GetAchievementGame(string appId);
    }
}
