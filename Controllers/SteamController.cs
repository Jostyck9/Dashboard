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

        public async Task<IActionResult> Index()
        {
            var data = await Model.GetGameList();
            return View(data);
        }

        [HttpGet("[controller]/[action]/{query}")]
        public async Task<IActionResult> Search(string query)
        {
            var data = await Model.SearchGame(query);
            return View(data);
        }

        [HttpGet("[controller]/[action]/{appId}/{count?}")]
        public async Task<IActionResult> News(string appId, ulong count = 1)
        {
            var data = await Model.GetLastNews(appId, count);
            return View(data);
        }
    }
}