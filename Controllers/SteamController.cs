using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dashboard.Models.Steam;
using Microsoft.AspNetCore.Mvc;

namespace Dashboard.Controllers
{
    public class SteamController : Controller
    {
        private ISteamModel Model = new SteamModel();

        /**
        * @brief get the game list for the view
        *
        * @return a view
        */
        public async Task<IActionResult> Index()
        {
            var data = await Model.GetGameList();
            return View(data);
        }

        [HttpGet("[controller]/[action]/{query}")]
        /**
        * @brief get a game from a string
        *
        * @return a view
        */
        public async Task<IActionResult> Search(string query)
        {
            var data = await Model.SearchGame(query);
            return View(data);
        }

        [HttpGet("[controller]/[action]/{appId}/{count?}")]
        /**
        * @brief get the news of the game from the application Id which is a string
        *
        * @return a view
        */

        public async Task<IActionResult> News(string appId, ulong count = 1)
        {
            var data = await Model.GetLastNews(appId, count);
            return View(data);
        }

        [HttpGet("[controller]/[action]/{appId}")]
        /**
        * @brief get the numbers of players on the game from the application id which is a string
        *
        * @return a view
        */
        public async Task<IActionResult> Players(string appId)
        {
            GameData res = new GameData();
            res.Banner = Model.GetGameBanner(appId);
            res.data = await Model.GetCurrentPlayersGame(appId);
            return View(res);
        }

        [HttpGet("[controller]/[action]/{appId}")]
        /**
        * @brief get the achivements of the game from the application Id which is a string
        *
        * @return a view
        */
        public async Task<IActionResult> Achievements(string appId)
        {
            var res = await Model.GetAchievementGame(appId);
            return View(res);
        }
    }
}