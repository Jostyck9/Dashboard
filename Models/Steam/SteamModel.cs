using Dashboard.Models.Steam.Data;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
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
                if (game.Name.ToLower().Contains(query.ToLower()))
                {
                    res.Add(game);
                }
            }
            return res;
        }

        public async Task<AppNewsData> GetLastNews(string appId, ulong count = 1, ulong maxLength = 500)
        {
            using (var client = new HttpClient())
            {
                try
                {
                    client.BaseAddress = new Uri(BASE_URL);
                    var response = await client.GetAsync($"/ISteamNews/GetNewsForApp/v0002/?appid={appId}&count={count}&maxlength={maxLength}");
                    response.EnsureSuccessStatusCode();

                    var stringResult = await response.Content.ReadAsStringAsync();
                    var res = JsonConvert.DeserializeObject<AppNewsData>(stringResult);
                    return res;
                }
                catch (HttpRequestException _)
                {
                    return new AppNewsData();
                }
            }
        }
        public async Task<ResponseData> GetCurrentPlayersGame(string appId)
        {
            using (var client = new HttpClient())
            {
                try
                {
                    client.BaseAddress = new Uri(BASE_URL);
                    var response = await client.GetAsync($"/ISteamUserStats/GetNumberOfCurrentPlayers/v1/?appid={appId}");
                    response.EnsureSuccessStatusCode();

                    var stringResult = await response.Content.ReadAsStringAsync();
                    var res = JsonConvert.DeserializeObject<ResponseData>(stringResult);
                    return res;
                }
                catch (HttpRequestException _)
                {
                    return new ResponseData { Response = null};
                }
            }
        }

        public String GetGameBanner(string appId)
        {
            return "https://steamcdn-a.akamaihd.net/steam/apps/" + appId + "/header.jpg?t=1570039639";
        }

        public async Task<AchievementList> GetAchievementGame(string appId)
        {
            using (var client = new HttpClient())
            {
                try
                {
                    client.BaseAddress = new Uri(BASE_URL);
                    var response = await client.GetAsync($"ISteamUserStats/GetGlobalAchievementPercentagesForApp/v0002/?gameid={appId}");
                    response.EnsureSuccessStatusCode();

                    var stringResult = await response.Content.ReadAsStringAsync();
                    var res = JsonConvert.DeserializeObject<AchievementResponse>(stringResult);
                    if (res != null && res.Achievementpercentages != null)
                        return res.Achievementpercentages;
                    throw new HttpRequestException();
                }
                catch (HttpRequestException _)
                {
                    return new AchievementList { Achievements = null };
                }
            }
        }
    }
}
