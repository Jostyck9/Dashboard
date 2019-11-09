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
    }
}
