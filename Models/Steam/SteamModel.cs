using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Dashboard.Models.Steam
{
    public class SteamModel : ISteamModel
    {
        private readonly string API_KEY = "907CC8E30666701472D392675DE32EDB";
        private readonly string BASE_URL = "http://api.steampowered.com";
        private static AppList GameList = null;

        public async Task<AppList> GetGameList()
        {
            if (GameList != null)
                return GameList;
            using (var client = new HttpClient())
            {
                try
                {
                    client.BaseAddress = new Uri(BASE_URL);
                    var response = await client.GetAsync($"/ISteamApps/GetAppList/v0002/");
                    response.EnsureSuccessStatusCode();

                    var stringResult = await response.Content.ReadAsStringAsync();
                    var res = JsonConvert.DeserializeObject<AppList>(stringResult);
                    if (GameList == null && res != null && res.Applist != null && res.Applist.Apps != null && res.Applist.Apps.Count() != 0)
                        GameList = res;
                    return res;
                }
                catch (HttpRequestException _)
                {
                    return new AppList();
                }
            }
        }

        public async Task<List<SteamGame>> SearchGame(string query)
        {
            var list = await GetGameList();
            var res = new List<SteamGame>();

            foreach (var game in list.Applist.Apps)
            {
                if (game.Name.Contains(query))
                {
                    res.Add(game);
                }
            }
            return res;
        }
    }
}
