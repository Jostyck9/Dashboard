using Dashboard.Models.Steam.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dashboard.Models.Steam
{
    interface ISteamModel
    {
        public Task<AppList> GetGameList();
        public Task<List<SteamGame>> SearchGame(string query);
        public Task<AppNewsData> GetLastNews(string appId, ulong count = 1, ulong maxLength = 500);
        public Task<ResponseData> GetCurrentPlayersGame(string appId);
        public String GetGameBanner(string appId);
        public Task<AchievementList> GetAchievementGame(string appId);
    }
}
